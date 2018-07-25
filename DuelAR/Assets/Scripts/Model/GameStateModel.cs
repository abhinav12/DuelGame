using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Models
{
    public class GameStateModel
    {
        public Guid Id { get; set; }
        public Guid ActivePlayer { get; set; }
        public Dictionary<Guid, PlayerStateModel> Players { get; set; }
        public bool GameReady { get; set; }
        public bool GameFinished { get; set; }
        public Guid GameWinner { get; set; }
    }
}