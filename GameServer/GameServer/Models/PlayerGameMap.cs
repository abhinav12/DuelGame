using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;

namespace GameServer.Models
{
    public class PlayerGameMap
    {
        public static ConcurrentDictionary<Guid, Guid> Instance = new ConcurrentDictionary<Guid, Guid>();
    }
}