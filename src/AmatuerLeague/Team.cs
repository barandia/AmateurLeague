using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AmatuerLeague
{
    /**
     * A team can have one or many players (Roster).
     * A team is exactly in one league
     */
    public class Team
    {
        public string Id { get; private set;}
        public string Name { get; set;}
        // Todo: const or readonly? Need to figure out how to set this property only once.
        public string LeagueName { get; private set;}   
        public Player Captain { get; set;}
        public Player CoCaptain { get; set;} 
        public List<Player> Roster = new List<Player>();

        public Team(string leagueName) 
        {
            Id = Guid.NewGuid().ToString();
            LeagueName = leagueName;
        }

        public void AddPlayer(Player player) 
        {
            Roster.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            Roster.Remove(player);
        }

        public bool IsPlayerOnRoster(Player player)
        {
            return Roster.Contains(player);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
