using System;
using System.Collections.Generic;
using System.Text;

namespace AmateurLeague
{
    public class TeamPlayer
    {
        public string TeamId { get; set; }
        public Team Team { get; set; }
        public string PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
