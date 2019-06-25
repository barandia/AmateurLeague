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
        public Player Owner { get; set;}
        public SportTypes Sport {get; private set;}

        public League(SportTypes sport) 
        {
            Id = Guid.NewGuid().ToString();
            Sport = sport;
        }
    }
}
