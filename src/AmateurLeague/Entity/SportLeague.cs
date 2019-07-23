using System;
using System.Collections.Generic;
using System.Text;

namespace AmateurLeague.Entity
{
    public class SportLeague
    {
        public string SportId { get; set; }
        public string LeagueId { get; set; }
        public Sport Sport { get; set; }
        public League League { get; set; }
    }
}
