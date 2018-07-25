using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Models
{
    public class GameState
    {
        public Guid Id { get; set; }
        public Guid ActivePlayer { get; set; }
        public Dictionary<Guid, PlayerState> Players { get; set; }
        public bool GameReady { get; set; }
        public bool GameFinished { get; set; }
        public Guid GameWinner { get; set; }

        public GameState()
        {
            Id = Guid.NewGuid();
            ActivePlayer = GameWinner = Guid.Empty;
            Players = new Dictionary<Guid, PlayerState>();
            GameReady = GameFinished = false;
        }
    }
}