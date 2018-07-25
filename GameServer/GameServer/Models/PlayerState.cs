using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Models
{
    public class PlayerState
    {
        public int Health { get; set; }
        public PlayerState()
        {
            Health = Constants.MAX_HEALTH;
        }
    }
}