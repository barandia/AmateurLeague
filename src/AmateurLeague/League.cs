using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AmateurLeague
{
    /**
     * A league can have many teams
     */
    public class League
    {
        public string Name { get; set;}
        public Sport Sport {get; set;}
        public ICollection<Team> Teams { get; set; }

        public void AddTeam(Team team)
        {
            Teams.Add(team);
        }

        public void RemoveTeam(Team team)
        {
            Teams.Remove(team);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
