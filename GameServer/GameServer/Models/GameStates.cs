using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
namespace GameServer.Models
{
    public class GameStates
    {
        public static ConcurrentDictionary<Guid, GameState> Instance = new ConcurrentDictionary<Guid, GameState>();
    }
}