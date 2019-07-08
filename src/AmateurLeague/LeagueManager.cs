using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AmateurLeague
{
    /**
     * Responsible for managing a League 
     *  - CRUD for League
     *  - CRUD for Team
     *  - CRUD for Player
     */
    public static class LeagueManager
    {
        private static readonly NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();
        private static readonly Dictionary<string, League> Leagues = new Dictionary<string, League>();
        private static readonly Dictionary<string, Team> Teams = new Dictionary<string, Team>();
        private static readonly Dictionary<string, Player> Players = new Dictionary<string, Player>();


        public static List<Sport> Sports { get; } = new List<Sport>() {new Sport("Basketball", SportGenderTypes.Men),
                                                                        new Sport("Basketball", SportGenderTypes.Women),
                                                                        new Sport("Basketball", SportGenderTypes.Coed),
                                                                        new Sport("Baseball", SportGenderTypes.Men),
                                                                        new Sport("Baseball", SportGenderTypes.Women),
                                                                        new Sport("Baseball", SportGenderTypes.Coed),
                                                                        new Sport("Soccer", SportGenderTypes.Men),
                                                                        new Sport("Soccer", SportGenderTypes.Women),
                                                                        new Sport("Soccer", SportGenderTypes.Coed),
                                                                        new Sport("Flag Football", SportGenderTypes.Men),
                                                                        new Sport("Flag Football", SportGenderTypes.Women),
                                                                        new Sport("Flag Football", SportGenderTypes.Coed)};
        #region Methods for League
        /// <summary>
        /// Create a new league
        /// </summary>
        /// <param name="leagueName">Name of the League</param>
        /// <param name="owner">Owner of the League</param>
        /// <param name="sport">Sports played on this League</param>
        /// <returns></returns>
        public static League CreateLeague(string leagueName, Sport sport)
        {
            var leagueNameToLower = leagueName.ToLower();
            LOGGER.Debug($"Creating new {sport} league with name {leagueNameToLower}");
            if (Leagues.ContainsKey(leagueNameToLower))
            {
                LOGGER.Debug($"Failed to create new {sport} league with name {leagueNameToLower} - League name already exists");
                return null;
            }

            var newLeague = new League(sport)
            {
                Name = leagueNameToLower
            };

            LOGGER.Debug($"Successfully created new {sport} league with name {leagueNameToLower}");
            Leagues.Add(leagueNameToLower, newLeague);

            return newLeague;
        }

        /// <summary>
        /// Retrieve League
        /// </summary>
        /// <param name="leagueName">Name of the league to retrieve</param>
        /// <returns>The League to retrieve</returns>
        public static League GetLeague(string leagueName)
        {
            var leagueNameToLower = leagueName.ToLower();
            LOGGER.Debug($"Retrieving league with name {leagueNameToLower}");
            if (Leagues.ContainsKey(leagueNameToLower))
            {
                LOGGER.Debug($"Successfully retrieved league with name {leagueNameToLower}");
                return Leagues[leagueNameToLower];
            } else
            {
                LOGGER.Error($"Failed to retreive {leagueNameToLower} league - does not exist");
                return null;
            }
        }

        /// <summary>
        /// Retrieve all leagues
        /// </summary>
        /// <returns>All leagues</returns>
        public static Dictionary<string, League> GetAllLeagues()
        {
            return Leagues;
        }

        public static bool IsLeagueExists(string leagueName)
        {
            return Leagues.ContainsKey(leagueName);
        }

        /// <summary>
        /// Update league information
        /// </summary>
        /// <param name="league">League to update</param>
        public static void UpdateLeague(string leagueName, League league)
        {
            var leagueNameToLower = leagueName.ToLower();
            var newLeagueNameToLower = league.Name.ToLower();
            LOGGER.Debug($"Updating league with name {newLeagueNameToLower}");
            if (!string.Equals(leagueNameToLower, newLeagueNameToLower))
            {
                LOGGER.Debug($"Removing old league with name {leagueNameToLower}");
                Leagues.Remove(leagueNameToLower);
            }

            LOGGER.Debug($"Successfully updated league with name {newLeagueNameToLower}");
            Leagues[newLeagueNameToLower] = league;
        }

        /// <summary>
        /// Deletes a league
        /// </summary>
        /// <param name="leagueName">league to delete</param>
        /// <returns></returns>
        public static bool DeleteLeague(string leagueName)
        {
            var leagueNameToLower = leagueName.ToLower();
            bool result;
            if (Leagues.ContainsKey(leagueNameToLower))
            {
                // To do - League must be empty (i.e. no teams) before deleting
                result = Leagues.Remove(leagueNameToLower);
            } else
            {
                LOGGER.Error($"Attempting to delete {leagueNameToLower} league - does not exist");
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
        public static Team CreateTeam(string teamName, string leagueName, string captainEmailAddress, string coCaptainEmailAddress = null)
        {
            var teamNameToLower = teamName.ToLower();
            LOGGER.Debug($"Creating new Team with name {teamNameToLower} in league {leagueName}");
            if (Teams.ContainsKey(teamNameToLower))
            {
                LOGGER.Debug($"Failed to create new Team with name {teamNameToLower} - Team name already exists");
                return null;
            }

            var newTeam = new Team(leagueName)
            {
                Name = teamNameToLower,
                CaptainEmailAddress = captainEmailAddress,
                CoCaptainEmailAddress = coCaptainEmailAddress
            };

            LOGGER.Debug($"Successfully created new Team with name {teamNameToLower} in league {leagueName}");
            Teams.Add(teamNameToLower, newTeam);
            return newTeam;
        }
        public static bool IsTeamExist(string teamName)
        {
            return Teams.ContainsKey(teamName.ToLower());
        }

        /// <summary>
        /// Retrieve all teams
        /// </summary>
        /// <returns>All teams</returns>
        public static Dictionary<string, Team> GetAllTeams()
        {
            return Teams;
        }

        /// <summary>
        /// Retrieves a team
        /// </summary>
        /// <param name="teamName">The name of the team to retrieve</param>
        /// <returns></returns>
        public static Team GetTeam(string teamName)
        {
            var teamNameToLower = teamName.ToLower();
            LOGGER.Debug($"Retrieving Team: {teamNameToLower}");
            if (Teams.ContainsKey(teamNameToLower))
            {
                LOGGER.Debug($"Retrieving Team: {Teams[teamNameToLower]}");
                return Teams[teamNameToLower];
            }
            else
            {
                LOGGER.Debug($"Failed to retreive team {teamNameToLower} - does not exist");
                throw new Exception($"Failed to retreive {teamNameToLower} team - does not exist");
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
            var teamNameToLower = teamName.ToLower();
            LOGGER.Debug($"Deleting Team: {teamNameToLower}");
            bool result;
            if (Teams.ContainsKey(teamNameToLower))
            {
                LOGGER.Debug($"Deleting Team: {Teams[teamNameToLower]}");
                result = Teams.Remove(teamNameToLower);
            }
            else
            {
                LOGGER.Error($"Failed to delete {teamNameToLower} team - does not exist");
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
            var teamNameToLower = teamName.ToLower();
            LOGGER.Debug($"Adding player {player} to team {teamNameToLower}");
            var result = true;
            if (Teams.ContainsKey(teamNameToLower))
            {
                if (!Teams[teamNameToLower].IsPlayerOnRoster(player.EmailAddress))
                {
                    LOGGER.Debug($"Successfully added player {player} to team {teamNameToLower}");
                    Teams[teamNameToLower].AddPlayer(player.EmailAddress);

                    if (!Players.ContainsKey(player.EmailAddress))
                    {
                        Players.Add(player.EmailAddress, player);
                    }
                }
                else
                {
                    LOGGER.Error($"Failed to add player {player} to team {teamNameToLower} - player already on the roster");
                    result = false;
                }
            }
            else
            {
                LOGGER.Error($"Failed to add player {player} to team {teamNameToLower} - team does not exist");
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
            var teamNameToLower = teamName.ToLower();
            LOGGER.Debug($"Removing player {player} from team {teamNameToLower}");
            var result = true;
            if (Teams.ContainsKey(teamNameToLower))
            {
                if (Teams[teamNameToLower].IsPlayerOnRoster(player.EmailAddress))
                {
                    Teams[teamNameToLower].RemovePlayer(player.EmailAddress);
                    LOGGER.Debug($"Successfully removed player {player} from team {teamNameToLower}");
                }
                else
                {
                    LOGGER.Error($"Failed to remove player {player.EmailAddress} to {teamNameToLower} team - player is not on the roster");
                    result = false;
                }
            }
            else
            {
                LOGGER.Error($"Failed to remove player {player.EmailAddress} to {teamNameToLower} team - team does not exist");
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
            var emailLower = emailAddress.ToLower();
            LOGGER.Debug($"Creating new player with email address {emailLower}");
            if (string.IsNullOrEmpty(emailLower))
            {
                LOGGER.Debug($"Failed to create new player - email address cannot be null nor empty");
                return null;
            }
            else if (Players.ContainsKey(emailLower))
            {
                LOGGER.Debug($"Failed to create new player - email address already exists");
                return null;
            }

            var newPlayer = new Player()
            {
                EmailAddress = emailLower,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                DateOfBirth = dateOfBirth
            };

            LOGGER.Debug($"Created new player {newPlayer}");
            Players.Add(emailLower, newPlayer);
            return newPlayer;
        }

        /// <summary>
        /// Retrieve all players
        /// </summary>
        /// <returns>All players</returns>
        public static Dictionary<string, Player> GetAllPlayers()
        {
            return Players;
        }

        /// <summary>
        /// Retrieve player
        /// </summary>
        /// <param name="emailAddress">The email address of the player to retrieve</param>
        /// <returns>player, otherwise throws an exception</returns>
        public static Player GetPlayer(string emailAddress)
        {
            var emailLower = emailAddress.ToLower();
            LOGGER.Debug($"Retrieving player with email address {emailLower}");
            if (Players.ContainsKey(emailLower))
            {
                LOGGER.Debug($"Successfully retrieved player with email address {emailLower}: Players[emailAddress]");
                return Players[emailLower];
            }
            else
            {
                LOGGER.Error($"Failed to retreive player with email address {emailLower} - does not exist");
                return null;
            }
        }

        /// <summary>
        /// Check if player exist in the system
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static bool IsPlayerExists(string emailAddress)
        {
            var emailLower = emailAddress.ToLower();
            return Players.ContainsKey(emailLower);
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
            var emailLower = emailAddress.ToLower();
            LOGGER.Debug($"Deleting player with email address {emailLower}");
            bool result;
            if (Players.ContainsKey(emailLower))
            {
                // To do - check if player is on a roster of any teams. If yes, do not allow to delete player
                LOGGER.Debug($"Successfully deleted player with email address {emailLower}");
                result = Players.Remove(emailLower);
            }
            else
            {
                LOGGER.Error($"Failed to delete player with email address {emailLower} - does not exist");
                result = false;
            }

            return result;
        }
        #endregion Methods for Player
    }
}
