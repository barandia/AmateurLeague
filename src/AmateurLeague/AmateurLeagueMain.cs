using System;
using System.Collections.Generic;

namespace AmateurLeague
{
    class AmateurLeagueMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to AmatuerLeague Manager!");

            var mainMenuOptions = new List<string>() { "Exit",
                                                        "Create new league",
                                                        "Create new team",
                                                        "Create new player",
                                                        "Add player to a team",
                                                        "Print all leagues",
                                                        "Print all teams",
                                                        "Print all players",};

            while (true)
            {
                Console.WriteLine("============ Main Menu ============");
                for (int i = 0; i < mainMenuOptions.Count; i++)
                {
                    Console.WriteLine($"{i} - {mainMenuOptions[i]}");
                }
                Console.WriteLine("===================================");
                Console.Write("Select an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        Console.WriteLine("Thank you for using Amateur League Manager");
                        return;
                    case "1": // create new league
                        CreateNewLeague();
                        break;
                    case "2": // create new team and add to a league. At least 1 league and 1 player must exist to create a new team.
                        CreateNewTeam();
                        break;
                    case "3": // create a new player
                        CreateNewPlayer();
                        break;
                    case "4": // add player to a team. At least 1 team must exist to successfully add a player to a team
                        AddPlayerToATeam();
                        break;
                    case "5": // print all leagues
                        PrintAllLeagues();
                        break;
                    case "6": // print all teams
                        PrintAllTeams();
                        break;
                    case "7": // print all players
                        PrintAllPlayers();
                        break;
                    default:
                        Console.Error.WriteLine($"Invalid selection - {choice}");
                        break;
                }
            }
        }

        private static void CreateNewLeague()
        {
            try
            {
                Console.Write("League name: ");
                var name = Console.ReadLine();

                // check if name already exists
                if (LeagueManager.IsLeagueExists(name))
                {
                    Console.Error.WriteLine($"Failed to create a league - league name already exists - {name}");
                    return;
                }

                Console.WriteLine("Choose league type: ");
                for (int i = 0; i < LeagueManager.Sports.Count; i++)
                {
                    Console.WriteLine($"\t{i}. {LeagueManager.Sports[i].Name} - {LeagueManager.Sports[i].Type}");
                }

                Console.Write("Sports: ");
                int sportChoice = int.Parse(Console.ReadLine().Trim());
                if (sportChoice < 0 || sportChoice >= LeagueManager.Sports.Count)
                {
                    Console.Error.WriteLine($"Failed to create a league - invalid sports choice - {sportChoice}");
                    return;
                }

                LeagueManager.CreateLeague(name, LeagueManager.Sports[sportChoice]);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Failed to create a league - {e.Message}");
            }
        }

        private static void CreateNewTeam()
        {
            try
            {
                var leagues = LeagueManager.GetAllLeagues();
                if (leagues.Count == 0)
                {
                    Console.Error.WriteLine($"Failed to add a new team - no leagues available");
                    return;
                }
                Console.Write("Team name: ");
                var teamName = Console.ReadLine();

                Console.WriteLine("Choose league to join: ");
                League leagueSelected = PrintOptionsAndSelect(leagues, new League[leagues.Count]);
                
                if(leagueSelected == null)
                {
                    return;
                }

                Console.Write("Team captain's email address: ");
                var captainsEmailAddress = Console.ReadLine();

                // check if email address exists
                if (!LeagueManager.IsPlayerExists(captainsEmailAddress))
                {
                    Console.Error.WriteLine($"Failed to create team - player does not exist - {captainsEmailAddress}");
                    return;
                }

                LeagueManager.CreateTeam(teamName, leagueSelected);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Failed to create a team - {e.Message}");
                return;
            }
        }

        private static void CreateNewPlayer()
        {
            try
            {
                Console.Write("Email address: ");
                var emailAddress = Console.ReadLine();

                Console.Write("First name: ");
                var firstName = Console.ReadLine();

                Console.Write("Last name: ");
                var lastName = Console.ReadLine();

                Console.Write("Gender [Male or Female]: ");
                GenderType gender;
                if (!Enum.TryParse(Console.ReadLine(), true, out gender))
                {
                    Console.Error.WriteLine($"Failed to create a player - gender must be \"male\" or \"female\"");
                    return;
                }

                Console.Write("Date of birth (MM/dd/yyyy): ");
                var dateOfBirth = DateTime.Parse(Console.ReadLine().Trim());
                LeagueManager.CreatePlayer(emailAddress, firstName, lastName, gender, dateOfBirth);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Failed to create a player - {e.Message}");
            }
        }

        private static void AddPlayerToATeam()
        { 
            try
            {
                // select a team - show all teams
                var allTeams = LeagueManager.GetAllTeams();
                if (allTeams.Count == 0)
                {
                    Console.Error.WriteLine("Failed to add player to a team - no available teams");
                    return;
                }
                Console.WriteLine("Choose team: ");
                Team teamToUpdate = PrintOptionsAndSelect(allTeams, new Team[allTeams.Count]);

                // select player to add - show all players
                var allPlayers = LeagueManager.GetAllPlayers();
                if (allPlayers.Count == 0)
                {
                    Console.Error.WriteLine("Failed to add player to a team - no available players to add");
                    return;
                }
                Console.WriteLine($"Choose player to add to team {teamToUpdate.Name}: ");
                Player playerToAdd = PrintOptionsAndSelect(allPlayers, new Player[allPlayers.Count]);

                if (teamToUpdate != null && playerToAdd != null)
                {
                    LeagueManager.AddPlayerToTeam(teamToUpdate.Name, playerToAdd);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Failed to create a team - {e.Message}");
                return;
            }

            
        }

        private static T PrintOptionsAndSelect<T>(Dictionary<string, T> all, T[] options)
        {
            all.Values.CopyTo(options, 0);
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"\t{i}. {options[i]}");
            }

            Console.Write("selection: ");
            int choice = int.Parse(Console.ReadLine().Trim());

            if (choice < 0 || choice >= options.Length)
            {
                Console.Error.WriteLine($"Invalid selection - {choice}");
                return default;
            }

            return options[choice];
        }

        private static void PrintAllLeagues()
        {
            var allLeagues = LeagueManager.GetAllLeagues();
            if (allLeagues.Count == 0)
            {
                Console.WriteLine("No leagues has been created");
            }
            else
            {
                Console.WriteLine("Leagues: ");
                foreach (var league in allLeagues.Values)
                {
                    Console.WriteLine($"\t{league}");
                }
            }
        }

        private static void PrintAllTeams()
        {
            var allTeams = LeagueManager.GetAllTeams();
            if (allTeams.Count == 0)
            {
                Console.WriteLine("No teams has been created");
            }
            else
            {
                Console.WriteLine("Teams: ");
                foreach (var team in allTeams.Values)
                {
                    Console.WriteLine($"\t{team}");
                }
            }
        }

        private static void PrintAllPlayers()
        {
            var allPlayers = LeagueManager.GetAllPlayers();
            if (allPlayers.Count == 0)
            {
                Console.WriteLine("No players has been created");
            }
            else
            {
                Console.WriteLine("Players: ");
                foreach (var player in allPlayers.Values)
                {
                    Console.WriteLine($"\t{player}");
                }
            }
        }
    }
}
