using System;

namespace GameServer.Controllers
{
    public class AttackRequest
    {
        public Guid Attacker { get; set; }
        public Guid Attacked { get; set; }
        public int Damage { get; set; }
    }
}