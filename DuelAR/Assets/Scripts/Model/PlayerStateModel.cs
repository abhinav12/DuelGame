using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameServer.Models
{
    public class PlayerStateModel
    {
        public Guid PlayerId { get; set; }
        public int Health { get; set; }
    }
}