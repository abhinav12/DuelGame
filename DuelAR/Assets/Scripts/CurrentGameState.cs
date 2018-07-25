using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class CurrentGameState
    {
        private static GameStateModel instance;
        public static GameStateModel GetInstance() {
            return instance;
        }
        public static bool SetInstance(GameStateModel value, DateTime ts)
        {
            if(ts > Timestamp)
            {
                instance = value;
                Timestamp = ts;
                return true;
            }
            return false;
        }
        private static DateTime Timestamp = DateTime.MinValue;
    }
}
