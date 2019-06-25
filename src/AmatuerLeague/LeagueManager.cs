using System;
using System.Collections.Generic;

namespace AmatuerLeague
{
    /**
     * Responsible for managing a League 
     *  - CRUD for League
     *  - CRUD for Team
     *  - CRUD for Player
     */
    static class LeagueManager
    {
        private static readonly Dictionary<string, League> Leagues = new Dictionary<string, League>();
        private static readonly Dictionary<string, Team> Teams = new Dictionary<string, Team>();
        private static readonly Dictionary<string, Player> Players = new Dictionary<string, Player>();

        #region Methods for League
        /// <summary>
        /// Create a new league
        /// </summary>
        /// <param name="name">Name of the League</param>
        /// <param name="owner">Owner of the League</param>
        /// <param name="sport">Sports played on this League</param>
        /// <returns></returns>
        public static League CreateLeague(string name, Player owner, SportTypes sport)
        {
            return new League(sport)
            {
                Name = name,
                Owner = owner
            };
        }

        /// <summary>
        /// Retrieve League
        /// </summary>
        /// <param name="leagueName">Name of the league to retrieve</param>
        /// <returns>The League to retrieve</returns>
        public static League GetLeague(string leagueName)
        {
            if (Leagues.ContainsKey(leagueName))
            {
                return Leagues[leagueName];
            } else
            {
                throw new Exception($"Attempting to retreive {leagueName} league - does not exist");
            }
        }

        /// <summary>
        /// Update league information
        /// </summary>
        /// <param name="league">League to update</param>
        public static void UpdateLeague(League league)
        {
            Leagues[league.Name] = league;
        }

        /// <summary>
        /// Deletes a league
        /// </summary>
        /// <param name="leagueName">league to delete</param>
        /// <returns></returns>
        public static bool DeleteLeague(string leagueName)
        {
            bool result;
            if (Leagues.ContainsKey(leagueName))
            {
                // To do - League must be empty (i.e. no teams) before deleting
                result = Leagues.Remove(leagueName);
            } else
            {
                // Log error
                Console.WriteLine($"[Error] Attempting to delete {leagueName} league - does not exist");
                result = false;
            }
            return result;
        }
        #endregion Methods for League

        #region Methods for Teams
        /// <summary>
        /// Creates a new team and assigns to a league
        /// </summary>
        /// <param name="teamName">Name of the team to create</param>
        /// <param name="leagueName">Name of the League to join</param>
        /// <param name="captain">Captain of this team (owner)</param>
        /// <param name="coCaptain">Co-Captain of this team (co-owner)</param>
        /// <returns>The newly created team</returns>
        public static Team CreateTeam(string teamName, string leagueName, Player captain, Player coCaptain = null)
        {
            var newTeam = new Team(leagueName)
            {
                Name = teamName,
                Captain = captain,
                CoCaptain = coCaptain
            };

            return newTeam;
        }

        /// <summary>
        /// Retrieves a team
        /// </summary>
        /// <param name="teamName">The name of the team to retrieve</param>
        /// <returns></returns>
        public static Team GetTeam(string teamName)
        {
            if (Teams.ContainsKey(teamName))
            {
                return Teams[teamName];
            }
            else
            {
                throw new Exception($"Attempting to retreive {teamName} team - does not exist");
            }
        }

        /// <summary>
        /// Updates team information
        /// </summary>
        /// <param name="team">The team to update</param>
        public static void UpdateTeam(Team team)
        {
            Teams[team.Name] = team;
        }

        /// <summary>
        /// Deletes a team
        /// </summary>
        /// <param name="teamName">The name of the team to delete</param>
        /// <returns>true if the deletion was successfull, otherwise false</returns>
        public static bool DeleteTeam(string teamName)
        {
            bool result;
            if (Teams.ContainsKey(teamName))
            {
                result = Teams.Remove(teamName);
            }
            else
            {
                // Log error
                Console.WriteLine($"[Error] Attempting to delete {teamName} team - does not exist");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Adds a player to a team
        /// </summary>
        /// <param name="teamName">The name of the team to add the player</param>
        /// <param name="player">The player to add to a team</param>
        /// <returns>true if player</returns>
        public static bool AddPlayerToTeam(string teamName, Player player)
        {
            var result = true;
            if (Teams.ContainsKey(teamName))
            {
                if (!Teams[teamName].IsPlayerOnRoster(player))
                {
                    Teams[teamName].AddPlayer(player);
                }
                else
                {
                    Console.WriteLine($"[Error] Failed to add player {player.EmailAddress} to {teamName} team - player already on the roster");
                    result = false;
                }
            }
            else
            {
                Console.WriteLine($"[Error] Failed to add player {player.EmailAddress} to {teamName} team - team does not exist");
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Removes player from a team roster
        /// </summary>
        /// <param name="teamName">The name of the team where the player belongs to</param>
        /// <param name="player">The player to remove</param>
        /// <returns></returns>
        public static bool RemovePlayerFromTeam(string teamName, Player player)
        {
            var result = true;
            if (Teams.ContainsKey(teamName))
            {
                if (Teams[teamName].IsPlayerOnRoster(player))
                {
                    Teams[teamName].RemovePlayer(player);
                }
                else
                {
                    Console.WriteLine($"[Error] Failed to remove player {player.EmailAddress} to {teamName} team - player is not on the roster");
                    result = false;
                }
            }
            else
            {
                Console.WriteLine($"[Error] Failed to remove player {player.EmailAddress} to {teamName} team - team does not exist");
                result = false;
            }

            return result;
        }
        #endregion Methods for Teams

        #region Methods for Players
        /// <summary>
        /// Create a new player
        /// </summary>
        /// <param name="emailAddress">The email address of the player</param>
        /// <param name="firstName">Player's first name</param>
        /// <param name="lastName">Player's last name</param>
        /// <param name="gender">Player's gender</param>
        /// <param name="dateOfBirth">Player's date of birth</param>
        /// <returns></returns>
        public static Player CreatePlayer(string emailAddress, string firstName, string lastName, GenderType gender, DateTime dateOfBirth)
        {
            return new Player()
            {
                EmailAddress = emailAddress,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                DateOfBirth = dateOfBirth
            };
        }


        /// <summary>
        /// Retrieve player
        /// </summary>
        /// <param name="emailAddress">The email address of the player to retrieve</param>
        /// <returns>player, otherwise throws an exception</returns>
        public static Player GetPlayer(string emailAddress)
        {
            if (Players.ContainsKey(emailAddress))
            {
                return Players[emailAddress];
            }
            else
            {
                throw new Exception($"Attempting to retreive player with email address {emailAddress} - does not exist");
            }
        }

        /// <summary>
        /// Updates player
        /// </summary>
        /// <param name="player"></param>
        public static void UpdatePlayer(Player player)
        {
            Players[player.EmailAddress] = player;
        }

        /// <summary>
        /// Deletes a player. This will inherently delete the player from any roster.
        /// </summary>
        /// <param name="emailAddress">The email address of the player to delete</param>
        /// <returns>true if successfully deleted, otherwise returns false</returns>
        public static bool DeletePlayer(string emailAddress)
        {
            bool result;
            if (Players.ContainsKey(emailAddress))
            {
                // To do - check if player is on a roster of any teams. If yes, do not allow to delete player

                result = Players.Remove(emailAddress);
            }
            else
            {
                // Log error
                Console.WriteLine($"[Error] Attempting to delete player with email address {emailAddress} - does not exist");
                result = false;
            }

            return result;
        }
        #endregion Methods for Player
    }
}
