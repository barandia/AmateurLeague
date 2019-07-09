using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AmateurLeague
{
    /**
     * A team can have one or many players (Roster).
     * A team is exactly in one league
     */
    public class Team
    {
        public string Name { get; set;}
        public League League { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }

        public void AddPlayer(Player playerToAdd)
        {
            if (TeamPlayers == null)
            {
                TeamPlayers = new List<TeamPlayer>();
            }

            TeamPlayers.Add(new TeamPlayer() { PlayerId = playerToAdd.EmailAddress, TeamId = Name });
        }

        public void RemovePlayer(Player playerToRemove)
        {
            if (TeamPlayers != null)
            {
                TeamPlayer teamPlayerToRemove = null;
                foreach(TeamPlayer teamPlayer in TeamPlayers)
                {
                    if (teamPlayer.PlayerId.ToLower().Equals(playerToRemove.EmailAddress.ToLower()))
                    {
                        teamPlayerToRemove = teamPlayer;
                        break;
                    }
                }

                if (teamPlayerToRemove != null)
                {
                    TeamPlayers.Remove(teamPlayerToRemove);
                }
            }
        }
        public bool IsPlayerOnRoster(string emailAddress)
        {
            if (TeamPlayers != null)
            {
                foreach (TeamPlayer teamPlayer in TeamPlayers)
                {
                    if (teamPlayer.PlayerId.ToLower().Equals(emailAddress.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
