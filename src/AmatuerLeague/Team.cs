using System;
using System.Collections.Generic;

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
        public string LeagueId { get; set;}   
        public Player Captain { get; set;}
        public Player CoCaptain { get; set;} 
        public List<Player> Roster = new List<Player>();

        public Team() 
        {
            Id = Guid.NewGuid().ToString();
        }
        public void AddPlayer(Player player) 
        {
            Roster.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            Roster.Remove(player);
        }
    }
}
