using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AmatuerLeague
{
    /**
     * Responsible for managing a League 
     *  - CRUD for League
     *  - CRUD for Team
     *  - CRUD for Player
     */
    public static class LeagueManager
    {
        private static NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();
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
            LOGGER.Debug($"Creating new {sport} league with name {name}");
            if (Leagues.ContainsKey(name))
            {
                LOGGER.Debug($"Failed to create new {sport} league with name {name} - League name already exists");
                return null;
            }

            var newLeague = new League(sport)
            {
                Name = name,
                Owner = owner
            };

            LOGGER.Debug($"Successfully created new {sport} league with name {name}");
            Leagues.Add(name, newLeague);

            return newLeague;
        }

        /// <summary>
        /// Retrieve League
        /// </summary>
        /// <param name="leagueName">Name of the league to retrieve</param>
        /// <returns>The League to retrieve</returns>
        public static League GetLeague(string leagueName)
        {
            LOGGER.Debug($"Retrieving league with name {leagueName}");
            if (Leagues.ContainsKey(leagueName))
            {
                LOGGER.Debug($"Successfully retrieved league with name {leagueName}");
                return Leagues[leagueName];
            } else
            {
                LOGGER.Error($"Failed to retreive {leagueName} league - does not exist");
                return null;
            }
        }

        /// <summary>
        /// Update league information
        /// </summary>
        /// <param name="league">League to update</param>
        public static void UpdateLeague(string leagueName, League league)
        {

            LOGGER.Debug($"Updating league with name {league.Name}");
            if (!string.Equals(leagueName, league.Name))
            {
                LOGGER.Debug($"Removing old league with name {leagueName}");
                Leagues.Remove(leagueName);
            }

            LOGGER.Debug($"Successfully updated league with name {league.Name}");
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
                LOGGER.Error($"Attempting to delete {leagueName} league - does not exist");
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
            LOGGER.Debug($"Creating new Team with name {teamName} in league {leagueName}");
            if (Teams.ContainsKey(teamName))
            {
                LOGGER.Debug($"Failed to create new Team with name {teamName} - Team name already exists");
                return null;
            }

            var newTeam = new Team(leagueName)
            {
                Name = teamName,
                Captain = captain,
                CoCaptain = coCaptain
            };

            LOGGER.Debug($"Successfully created new Team with name {teamName} in league {leagueName}");
            Teams.Add(teamName, newTeam);
            return newTeam;
        }

        /// <summary>
        /// Retrieves a team
        /// </summary>
        /// <param name="teamName">The name of the team to retrieve</param>
        /// <returns></returns>
        public static Team GetTeam(string teamName)
        {
            LOGGER.Debug($"Retrieving Team: {teamName}");
            if (Teams.ContainsKey(teamName))
            {
                LOGGER.Debug($"Retrieving Team: {Teams[teamName]}");
                return Teams[teamName];
            }
            else
            {
                LOGGER.Debug($"Failed to retreive team {teamName} - does not exist");
                throw new Exception($"Failed to retreive {teamName} team - does not exist");
            }
        }

        /// <summary>
        /// Updates team information
        /// </summary>
        /// <param name="team">The team to update</param>
        public static void UpdateTeam(Team team)
        {
            LOGGER.Debug($"Upating Team: {team}");
            Teams[team.Name] = team;
        }

        /// <summary>
        /// Deletes a team
        /// </summary>
        /// <param name="teamName">The name of the team to delete</param>
        /// <returns>true if the deletion was successfull, otherwise false</returns>
        public static bool DeleteTeam(string teamName)
        {
            LOGGER.Debug($"Deleting Team: {teamName}");
            bool result;
            if (Teams.ContainsKey(teamName))
            {
                LOGGER.Debug($"Deleting Team: {Teams[teamName]}");
                result = Teams.Remove(teamName);
            }
            else
            {
                LOGGER.Error($"Failed to delete {teamName} team - does not exist");
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
            LOGGER.Debug($"Adding player {player} to team {teamName}");
            var result = true;
            if (Teams.ContainsKey(teamName))
            {
                if (!Teams[teamName].IsPlayerOnRoster(player))
                {
                    LOGGER.Debug($"Successfully added player {player} to team {teamName}");
                    Teams[teamName].AddPlayer(player);

                    if (!Players.ContainsKey(player.EmailAddress))
                    {
                        Players.Add(player.EmailAddress, player);
                    }
                }
                else
                {
                    LOGGER.Error($"Failed to add player {player} to team {teamName} - player already on the roster");
                    result = false;
                }
            }
            else
            {
                LOGGER.Error($"Failed to add player {player} to team {teamName} - team does not exist");
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
            LOGGER.Debug($"Removing player {player} from team {teamName}");
            var result = true;
            if (Teams.ContainsKey(teamName))
            {
                if (Teams[teamName].IsPlayerOnRoster(player))
                {
                    Teams[teamName].RemovePlayer(player);
                    LOGGER.Debug($"Successfully removed player {player} from team {teamName}");
                }
                else
                {
                    LOGGER.Error($"Failed to remove player {player.EmailAddress} to {teamName} team - player is not on the roster");
                    result = false;
                }
            }
            else
            {
                LOGGER.Error($"Failed to remove player {player.EmailAddress} to {teamName} team - team does not exist");
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
            LOGGER.Debug($"Creating new player with email address {emailAddress}");
            if (string.IsNullOrEmpty(emailAddress))
            {
                LOGGER.Debug($"Failed to create new player - email address cannot be null nor empty");
                return null;
            }
            else if (Players.ContainsKey(emailAddress))
            {
                LOGGER.Debug($"Failed to create new player - email address already exists");
                return null;
            }

            var newPlayer = new Player()
            {
                EmailAddress = emailAddress,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                DateOfBirth = dateOfBirth
            };

            LOGGER.Debug($"Created new player {newPlayer}");
            Players.Add(emailAddress, newPlayer);
            return newPlayer;
        }


        /// <summary>
        /// Retrieve player
        /// </summary>
        /// <param name="emailAddress">The email address of the player to retrieve</param>
        /// <returns>player, otherwise throws an exception</returns>
        public static Player GetPlayer(string emailAddress)
        {
            LOGGER.Debug($"Retrieving player with email address {emailAddress}");
            if (Players.ContainsKey(emailAddress))
            {
                LOGGER.Debug($"Successfully retrieved player with email address {emailAddress}: Players[emailAddress]");
                return Players[emailAddress];
            }
            else
            {
                LOGGER.Error($"Failed to retreive player with email address {emailAddress} - does not exist");
                return null;
            }
        }

        /// <summary>
        /// Updates player
        /// </summary>
        /// <param name="player"></param>
        public static void UpdatePlayer(Player player)
        {
            LOGGER.Debug($"Updating player {player}");
            Players[player.EmailAddress] = player;
        }

        /// <summary>
        /// Deletes a player. This will inherently delete the player from any roster.
        /// </summary>
        /// <param name="emailAddress">The email address of the player to delete</param>
        /// <returns>true if successfully deleted, otherwise returns false</returns>
        public static bool DeletePlayer(string emailAddress)
        {
            LOGGER.Debug($"Deleting player with email address {emailAddress}");
            bool result;
            if (Players.ContainsKey(emailAddress))
            {
                // To do - check if player is on a roster of any teams. If yes, do not allow to delete player
                LOGGER.Debug($"Successfully deleted player with email address {emailAddress}");
                result = Players.Remove(emailAddress);
            }
            else
            {
                LOGGER.Error($"Failed to delete player with email address {emailAddress} - does not exist");
                result = false;
            }

            return result;
        }
        #endregion Methods for Player
    }
}
