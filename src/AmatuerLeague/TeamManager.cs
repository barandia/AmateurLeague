using System;
using System.Collections.Generic;

namespace AmatuerLeague
{
    /**
     * Responsible for managing a team 
     *  - Create a new team
     *  - set/update team name
     *  - Add/Remove players to the team
     *  - set/update captain and co-captain
     */
    static class TeamManager
    {
        public static Team CreateTeam(string name, string leagueId, Player captain = null, Player coCaptain = null)
        {
            var newTeam = new Team()
            {
                Name = name,
                LeagueId = leagueId,
                Captain = captain,
                CoCaptain = coCaptain
            };

            return newTeam;     
        }

        public static void AddPlayer(Team team, Player player)
        {
            team.AddPlayer(player);
        }
    }
}
