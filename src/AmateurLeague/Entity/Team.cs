using System.Collections.Generic;
using System.Text;

namespace AmateurLeague.Entity
{
    /**
     * A team can have one or many players (Roster).
     * A team is exactly in one league
     */
    //[JsonObject(IsReference = true)]
    public class Team
    {
        public string Name { get; set; }
        public League League { get; set; }
        public ICollection<TeamPlayer> TeamPlayers { get; set; }

        public void AddPlayer(Player playerToAdd)
        {
            if (TeamPlayers == null)
            {
                TeamPlayers = new List<TeamPlayer>();
            }

            TeamPlayers.Add(new TeamPlayer() { PlayerId = playerToAdd.EmailAddress.Trim().ToLower(), TeamId = Name });
        }

        public void RemovePlayer(Player playerToRemove)
        {
            if (TeamPlayers != null)
            {
                TeamPlayer teamPlayerToRemove = null;
                foreach (TeamPlayer teamPlayer in TeamPlayers)
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
            var strBldr = new StringBuilder();
            strBldr.Append($"{{\"Name\": \"{Name}\",");
            strBldr.Append($"\"League\": \"{League.Name}\"");
            if (TeamPlayers != null)
            {
                var players = new HashSet<Player>();
                foreach(var teamplayers in TeamPlayers)
                {
                    players.Add(teamplayers.Player);
                }
                strBldr.Append(",\"Players\": [");
                strBldr.Append(string.Join(",", players));
                strBldr.Append("]");
            }
            strBldr.Append("}");

            return strBldr.ToString();
        }
    }
}
