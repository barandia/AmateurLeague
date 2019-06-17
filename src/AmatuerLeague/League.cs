using System;
using System.Collections.Generic;

namespace AmatuerLeague
{
    public enum SportTypes
    {
        BasketballWomen,
        BasketballMen,
        BasketballCoed,
        BaseballWomen,
        BaseballMen,
        BaseballCoed,
        SoccerWomen,
        SoccerMen,
        SoccerCoed,
        FlagFootballWomen,
        FlagFootballMen,
        FlagFootballCoed,
    }

    /**
     * A league can have many teams
     */
    class League
    {
        public string Id { get; private set;}
        public string Name { get; set;}
        public Player Commissioner { get; set;}
        public SportTypes Sport {get; private set;}
        public List<Team> Teams = new List<Team>();

        public League() 
        {
            Id = Guid.NewGuid().ToString();
        }
        
        public void AddTeam(Team team)
        {
            Teams.Add(team);
        }

        public void RemoveTeam(Team team)
        {
            Teams.Remove(team);
        }
    }
}
