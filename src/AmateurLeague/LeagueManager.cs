using AmateurLeague.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
        private static readonly AmateurLeagueContext db = new AmateurLeagueContext();

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
            LOGGER.Debug($"Creating new {sport} league with name {leagueName}");
            if (IsLeagueExists(leagueName))
            {
                LOGGER.Debug($"Failed to create new {sport} league with name {leagueName} - League name already exists");
                return null;
            }

            var newLeague = new League()
            {
                LeagueName = leagueName,
                Sport = sport
            };

            LOGGER.Debug($"Successfully created new {sport} league with name {leagueName}");
            db.Leagues.Add(newLeague);
            db.SaveChanges();
            //Leagues.Add(leagueNameToLower, newLeague);

            return newLeague;
        }

        /// <summary>
        /// Retrieve League
        /// </summary>
        /// <param name="leagueName">Name of the league to retrieve</param>
        /// <returns>The League to retrieve</returns>
        public static League GetLeague(int leagueId)
        {
            LOGGER.Debug($"Retrieving league with Id {leagueId}");
            return db.Leagues.Find(leagueId);
        }

        /// <summary>
        /// Retrieve all leagues
        /// </summary>
        /// <returns>All leagues</returns>
        public static IEnumerable<League> GetAllLeagues()
        {
            return db.Leagues.Include(league => league.Sport);
        }

        public static bool IsLeagueExists(string leagueName)
        {
            return db.Leagues.Any(l => l.LeagueName == leagueName);
        }

        public static bool IsLeagueExists(int leagueId)
        {
            return db.Leagues.Find(leagueId) != null;
        }

        /// <summary>
        /// Update league information
        /// </summary>
        /// <param name="league">League to update</param>
        public static void UpdateLeague(League league)
        {
            LOGGER.Debug($"Updating league with name {league.LeagueName}");
            db.Leagues.Update(league);
            db.SaveChanges();
            LOGGER.Debug($"Successfully updated league with name {league.LeagueName}");
        }

        /// <summary>
        /// Deletes a league
        /// </summary>
        /// <param name="leagueName">league to delete</param>
        /// <returns></returns>
        public static bool DeleteLeague(string leagueName)
        {
            LOGGER.Debug($"Deleting league with name {leagueName}");
            bool result;
            if (IsLeagueExists(leagueName))
            {
                // To do - League must be empty (i.e. no teams) before deleting
                result = db.Leagues.Remove(db.Leagues.Find(leagueName)) != null;
                if (result)
                {
                    db.SaveChanges();
                }
            } else
            {
                LOGGER.Error($"Attempting to delete {leagueName} league - does not exist");
                result = false;
            }
            return result;
        }

        public static bool DeleteLeague(int leagueId)
        {
            LOGGER.Debug($"Deleting league with Id {leagueId}");
            bool result = db.Leagues.Remove(db.Leagues.Find(leagueId)) != null;
            if (result)
            {
                db.SaveChanges();
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
        public static Team CreateTeam(string teamName, League league)
        {
            LOGGER.Debug($"Creating new Team with name {teamName} in league {league}");
            if (IsTeamExist(teamName))
            {
                LOGGER.Debug($"Failed to create new Team with name {teamName} - Team name already exists");
                return null;
            }

            var newTeam = new Team()
            {
                TeamName = teamName,
                League = league
            };

            LOGGER.Debug($"Successfully created new Team with name {teamName} in league {league}");
            db.Teams.Add(newTeam);
            db.SaveChanges();
            return newTeam;
        }
        public static bool IsTeamExist(string teamName)
        {
            return db.Teams.Any(t => t.TeamName == teamName);
        }

        /// <summary>
        /// Retrieve all teams
        /// </summary>
        /// <returns>All teams</returns>
        public static IEnumerable<Team> GetAllTeams()
        {
            return db.Teams;
        }

        /// <summary>
        /// Retrieves a team
        /// </summary>
        /// <param name="teamName">The name of the team to retrieve</param>
        /// <returns></returns>
        public static Team GetTeam(string teamName)
        {
            LOGGER.Debug($"Retrieving Team: {teamName}");
            if (IsTeamExist(teamName))
            {
                LOGGER.Debug($"Retrieving Team with name {teamName}");
                return db.Teams.Find(teamName);
            }
            else
            {
                LOGGER.Debug($"Failed to retreive team {teamName} - does not exist");
                // throw new Exception($"Failed to retreive {teamName} team - does not exist");
                return null;
            }
        }

        /// <summary>
        /// Updates team information
        /// </summary>
        /// <param name="team">The team to update</param>
        public static void UpdateTeam(Team team)
        {
            LOGGER.Debug($"Updating team with name {team.TeamName}");
            db.Teams.Update(team);
            db.SaveChanges();
            LOGGER.Debug($"Successfully updated team with name {team.TeamName}");
        }

        /// <summary>
        /// Deletes a team
        /// </summary>
        /// <param name="teamName">The name of the team to delete</param>
        /// <returns>true if the deletion was successfull, otherwise false</returns>
        public static bool DeleteTeam(string teamName)
        {
            LOGGER.Debug($"Deleting Team with name {teamName}");
            bool result;
            if (IsTeamExist(teamName))
            {
                result = db.Teams.Remove(db.Teams.Find(teamName)) != null;
                if (result)
                {
                    db.SaveChanges();
                }
            }
            else
            {
                LOGGER.Error($"Failed to delete team with name {teamName} - does not exist");
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
        public static bool AddPlayerToTeam(int teamId, Player player)
        {
            var team = db.Teams.Find(teamId);
            var result = true;
            if (team != null)
            {
                LOGGER.Debug($"Adding player {player} to team {team.TeamName}");
                if (!team.IsPlayerOnRoster(player.PlayerId))
                {
                    team.AddPlayer(player);
                    db.SaveChanges();
                    LOGGER.Debug($"Successfully added player {player} to team {team.TeamName}");
                }
                else
                {
                    LOGGER.Error($"Failed to add player {player} to team {team.TeamName} - player already on the roster");
                    result = false;
                }
            }else
            {
                LOGGER.Error($"Failed to add player {player} to team {team.TeamName} - team does not exist");
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
            if (IsTeamExist(teamName))
            {
                var team = db.Teams.Find(teamName);
                if (team.IsPlayerOnRoster(player.PlayerId))
                {
                    team.RemovePlayer(player);
                    db.SaveChanges();
                    LOGGER.Debug($"Successfully removed player {player} to team {teamName}");
                }
                else
                {
                    LOGGER.Error($"Failed to remove player {player} to {teamName} team - player is not on the roster");
                    result = false;
                }
            }
            else
            {
                LOGGER.Error($"Failed to remove player {player} to {teamName} team - team does not exist");
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
            var emailAddressLower = emailAddress.Trim().ToLower();
            LOGGER.Debug($"Creating new player with email address {emailAddressLower}");
            if (string.IsNullOrEmpty(emailAddressLower))
            {
                LOGGER.Debug($"Failed to create new player with email address {emailAddressLower} - email address cannot be null nor empty");
                return null;
            }
            else if (IsPlayerExists(emailAddressLower))
            {
                LOGGER.Debug($"Failed to create new player with email address {emailAddressLower} - email address already exists");
                return null;
            }

            var newPlayer = new Player()
            {
                EmailAddress = emailAddressLower,
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                DateOfBirth = dateOfBirth
            };

            db.Players.Add(newPlayer);
            db.SaveChanges();
            LOGGER.Debug($"Created new player {newPlayer}");
            
            return newPlayer;
        }

        /// <summary>
        /// Retrieve all players
        /// </summary>
        /// <returns>All players</returns>
        public static IEnumerable<Player> GetAllPlayers()
        {
            return db.Players;
        }

        /// <summary>
        /// Retrieve player
        /// </summary>
        /// <param name="emailAddress">The email address of the player to retrieve</param>
        /// <returns>player, otherwise throws an exception</returns>
        public static Player GetPlayer(string emailAddress)
        {
            var emailAddressLower = emailAddress.Trim().ToLower();
            LOGGER.Debug($"Retrieving player with email address {emailAddressLower}");
            if (IsPlayerExists(emailAddressLower))
            {
                LOGGER.Debug($"Successfully retrieved player with email address {emailAddressLower}");
                return db.Players.Find(emailAddressLower);
            }
            else
            {
                LOGGER.Error($"Failed to retreive player with email address {emailAddressLower} - does not exist");
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
            return db.Players.Any(p => p.EmailAddress == emailAddress);
        }

        /// <summary>
        /// Updates player
        /// </summary>
        /// <param name="player"></param>
        public static void UpdatePlayer(Player player)
        {
            LOGGER.Debug($"Updating player with email address {player.EmailAddress}");
            db.Players.Update(player);
            db.SaveChanges();
            LOGGER.Debug($"Successfully updated player with email address {player.EmailAddress}");
        }

        /// <summary>
        /// Deletes a player. This will inherently delete the player from any roster.
        /// </summary>
        /// <param name="emailAddress">The email address of the player to delete</param>
        /// <returns>true if successfully deleted, otherwise returns false</returns>
        public static bool DeletePlayer(string emailAddress)
        {
            var emailAddressLower = emailAddress.Trim().ToLower();
            LOGGER.Debug($"Deleting player with email address {emailAddressLower}");
            bool result;
            if (IsPlayerExists(emailAddressLower))
            {
                // To do - check if player is on a roster of any teams. If yes, do not allow to delete player
                LOGGER.Debug($"Successfully deleted player with email address {emailAddressLower}");
                result = db.Players.Remove(db.Players.Find(emailAddressLower)) != null;
                if (result)
                {
                    db.SaveChanges();
                }
            }
            else
            {
                LOGGER.Error($"Failed to delete player with email address {emailAddressLower} - does not exist");
                result = false;
            }

            return result;
        }
        #endregion Methods for Player

        public static IEnumerable<Sport> GetAllSports()
        {
            return db.Sports;
        }
    }
}
