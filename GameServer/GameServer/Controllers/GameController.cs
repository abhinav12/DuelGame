using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GameServer.Controllers
{
    public class GameController : ApiController
    {

        [Route("Ping")]
        [HttpPost]
        public string Ping()
        {
            return "Server is alive";
        }

        [Route("JoinGame")]
        [HttpPost]
        public GameState JoinGame([FromBody] JoinRequest request)
        {
            Guid playerid = Guid.Parse(request.playerid);
            GameState response = null;
            Guid GameId = Guid.Empty;
            PlayerGameMap.Instance.TryGetValue(playerid, out GameId);
            //Player already joined
            if (GameId != Guid.Empty)
            {
                GameStates.Instance.TryGetValue(GameId, out response);
            }
            else
            {
                var game = GameStates.Instance.Where(entry => entry.Value.GameReady == false);
                //Check if there are any ready games
                if(game.Count() != 0)
                {
                    Guid gameId = game.First().Key;
                    GameState gameStatePrev = game.First().Value;
                    GameState gameState = gameStatePrev;
                    gameState.Players.Add(playerid, new PlayerState());
                    gameState.GameReady = gameState.Players.Count() == Constants.MAX_PLAYERS;
                    gameState.ActivePlayer = gameState.Players.First().Key;
                    GameStates.Instance.TryUpdate(gameState.Id, gameState, gameStatePrev);
                    PlayerGameMap.Instance.TryAdd(playerid, gameId);
                    response = gameState;
                }
                else
                {
                    GameState gameState = new GameState();
                    gameState.Players.Add(playerid, new PlayerState());
                    GameStates.Instance.TryAdd(gameState.Id, gameState);
                    PlayerGameMap.Instance.TryAdd(playerid, gameState.Id);
                    response = gameState;
                }
            }
            return response;
        }

        [Route("Waiting/{id}")]
        [HttpGet]
        public GameState Waiting(string id)
        {
            Guid playerId = Guid.Parse(id);
            GameState response = null;
            Guid GameId = Guid.Empty;
            PlayerGameMap.Instance.TryGetValue(playerId, out GameId);
            if (GameId != Guid.Empty)
            {
                GameStates.Instance.TryGetValue(GameId, out response);
            }
            return response;
        }

        [Route("Attack")]
        [HttpPost]
        public GameState Attack([FromBody]AttackRequest attackRequest)
        {
            try { 
                Guid Attacked = attackRequest.Attacked;
                Guid Attacker = attackRequest.Attacker;
                int Damage = attackRequest.Damage;

                Guid GameId = Guid.Empty;
                PlayerGameMap.Instance.TryGetValue(Attacked, out GameId);

                GameState gameState = null;
                GameStates.Instance.TryGetValue(GameId, out gameState);

                gameState.Players[Attacked].Health -= Damage;
                //Game End
                if (gameState.Players[Attacked].Health <= 0)
                {
                    GameState gameStatePrev = gameState;
                    gameState.GameWinner = Attacker;
                    gameState.GameFinished = true;
                    GameStates.Instance.TryUpdate(gameState.Id, gameState, gameStatePrev);
                }
                //Next Player Turn
                else
                {
                    GameState gameStatePrev = gameState;
                    //hardcoded for 2 players
                    gameState.ActivePlayer = Attacked;
                    GameStates.Instance.TryUpdate(gameState.Id, gameState, gameStatePrev);
                }
                return gameState;
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
