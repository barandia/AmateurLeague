using System;
using Xunit;
using System.Collections;


namespace AmateurLeague.Tests
{
    public class LeagueManagerTests
    {
        //#region Player tests
        //[Fact]
        //public void CreateNewPlayerTest()
        //{
        //    var newPlayer = GeneratePlayer();
        //    var result = LeagueManager.CreatePlayer(newPlayer.EmailAddress, newPlayer.FirstName, newPlayer.LastName, newPlayer.Gender, newPlayer.DateOfBirth);
        //    Assert.NotNull(result);
        //    Assert.NotNull(LeagueManager.GetPlayer(newPlayer.EmailAddress));
        //    Assert.Equal(newPlayer.EmailAddress, LeagueManager.GetPlayer(newPlayer.EmailAddress).EmailAddress);
        //    Assert.Equal(result, LeagueManager.GetPlayer(newPlayer.EmailAddress));
        //}

        //[Fact]
        //public void CreateNewPlayerWithoutEmailTest()
        //{
        //    var emailAddress = "";
        //    LeagueManager.CreatePlayer(emailAddress, "Benjie", "Arandia", GenderType.Male, new DateTime(1984, 1, 1));
        //    Assert.Null(LeagueManager.GetPlayer(emailAddress));
        //}

        //[Fact]
        //public void CreateNewPlayerAlreadyExistsTest()
        //{
        //    var newPlayer = GeneratePlayer();
        //    LeagueManager.CreatePlayer(newPlayer.EmailAddress, newPlayer.FirstName, newPlayer.LastName, newPlayer.Gender, newPlayer.DateOfBirth);
        //    Assert.NotNull(LeagueManager.GetPlayer(newPlayer.EmailAddress));
        //    Assert.Equal(newPlayer.EmailAddress, LeagueManager.GetPlayer(newPlayer.EmailAddress).EmailAddress);

        //    var result = LeagueManager.CreatePlayer(newPlayer.EmailAddress, "", "", GenderType.Male, new DateTime(1984, 1, 1));
        //    Assert.Null(result);
        //}

        //[Fact]
        //public void RetrievePlayerTest()
        //{
        //    var newPlayer = GeneratePlayer();
        //    LeagueManager.CreatePlayer(newPlayer.EmailAddress, newPlayer.FirstName, newPlayer.LastName, newPlayer.Gender, newPlayer.DateOfBirth);
        //    Assert.NotNull(LeagueManager.GetPlayer(newPlayer.EmailAddress));
        //    var result = LeagueManager.GetPlayer(newPlayer.EmailAddress);
        //    Assert.Equal(newPlayer.EmailAddress, result.EmailAddress);
        //    Assert.Equal(newPlayer.FirstName, result.FirstName);
        //    Assert.Equal(newPlayer.LastName, result.LastName);
        //    Assert.Equal(newPlayer.Gender, result.Gender);
        //    Assert.Equal(newPlayer.DateOfBirth, result.DateOfBirth);
        //}

        //[Fact]
        //public void UpdatePlayerTest()
        //{
        //    var newPlayer = GeneratePlayer();
        //    LeagueManager.CreatePlayer(newPlayer.EmailAddress, newPlayer.FirstName, newPlayer.LastName, newPlayer.Gender, newPlayer.DateOfBirth);

        //    var newFirstName = "newFirstName";
        //    newPlayer.FirstName = newFirstName;
        //    LeagueManager.UpdatePlayer(newPlayer);

        //    var result = LeagueManager.GetPlayer(newPlayer.EmailAddress);
        //    Assert.Equal(newFirstName, result.FirstName);
        //}

        //[Fact]
        //public void DeletePlayerTest()
        //{
        //    var newPlayer = GeneratePlayer();
        //    LeagueManager.CreatePlayer(newPlayer.EmailAddress, newPlayer.FirstName, newPlayer.LastName, newPlayer.Gender, newPlayer.DateOfBirth);
        //    Assert.True(LeagueManager.DeletePlayer(newPlayer.EmailAddress));
        //    Assert.Null(LeagueManager.GetPlayer(newPlayer.EmailAddress));
        //}

        //[Fact]
        //public void DeleteNonExistentPlayerTest()
        //{
        //    Assert.False(LeagueManager.DeletePlayer("some email"));
        //}
        //#endregion Player tests

        //#region Team tests
        //[Fact]
        //public void CreateNewTeamTest()
        //{
        //    var newTeam = GenerateTeam();
            
        //    Assert.NotNull(LeagueManager.CreateTeam(newTeam.Name, newTeam.League));
        //    var result = LeagueManager.GetTeam(newTeam.Name);
        //    Assert.Equal(newTeam.Name, result.Name);
        //    Assert.Equal(newTeam.League.Name, result.League.Name);
        //}

        //[Fact]
        //public void CreateNewTeamNameAlreadyExistTest()
        //{
        //    var newTeam = GenerateTeam();

        //    Assert.NotNull(LeagueManager.CreateTeam(newTeam.Name, newTeam.League));
        //    Assert.Null(LeagueManager.CreateTeam(newTeam.Name, newTeam.League));
        //}

        //[Fact]
        //public void AddNewPlayerToTeamTest()
        //{
        //    var newTeam = GenerateTeam();
        //    Assert.NotNull(LeagueManager.CreateTeam(newTeam.Name, newTeam.League));

        //    var newPlayer = GeneratePlayer();
        //    Assert.True(LeagueManager.AddPlayerToTeam(newTeam.Name, newPlayer));
        //}

        //[Fact]
        //public void AddExistingPlayerToTeamTest()
        //{
        //    var newTeam = GenerateTeam();
        //    Assert.NotNull(LeagueManager.CreateTeam(newTeam.Name, newTeam.League));

        //    var newPlayer = GeneratePlayer();
        //    Assert.True(LeagueManager.AddPlayerToTeam(newTeam.Name, newPlayer));
        //    Assert.False(LeagueManager.AddPlayerToTeam(newTeam.Name, newPlayer));
        //}

        //[Fact]
        //public void AddPlayerToNonExistentTeamTest()
        //{
        //    var newPlayer = GeneratePlayer();
        //    Assert.False(LeagueManager.AddPlayerToTeam("Some team", newPlayer));
        //}

        //[Fact]
        //public void RemovePlayerFromTeamTest()
        //{
        //    var newTeam = GenerateTeam();
        //    Assert.NotNull(LeagueManager.CreateTeam(newTeam.Name, newTeam.League));

        //    var newPlayer = GeneratePlayer();
        //    Assert.True(LeagueManager.AddPlayerToTeam(newTeam.Name, newPlayer));
        //    Assert.True(LeagueManager.RemovePlayerFromTeam(newTeam.Name, newPlayer));
        //}

        //[Fact]
        //public void RemovePlayerFromNonExistentTeamTest()
        //{
        //    var newPlayer = GeneratePlayer();
        //    Assert.False(LeagueManager.RemovePlayerFromTeam("Some team", newPlayer));
        //}
        //#endregion Player tests

        //#region League tests
        //[Fact]
        //public void CreateNewLeagueTest()
        //{
        //    var expected = GenerateLeague();
        //    Assert.NotNull(LeagueManager.CreateLeague(expected.Name, expected.Sport));

        //    var actual = LeagueManager.GetLeague(expected.Name);
        //    Assert.NotNull(actual);
        //    Assert.Equal(expected.Name, actual.Name);
        //    Assert.Equal(expected.Sport, actual.Sport);
        //}

        //[Fact]
        //public void CreateNewLeaguWithNameExistTest()
        //{
        //    var expected = GenerateLeague();
        //    Assert.NotNull(LeagueManager.CreateLeague(expected.Name, expected.Sport));
        //    Assert.Null(LeagueManager.CreateLeague(expected.Name, expected.Sport));
        //}

        //[Fact]
        //public void RetrieveNonExistentLeagueTest()
        //{
        //    Assert.Null(LeagueManager.GetLeague("some league"));
        //}

        //[Fact]
        //public void UpdateLeagueTest()
        //{
        //    var newLeague = GenerateLeague();
        //    Assert.NotNull(LeagueManager.CreateLeague(newLeague.Name, newLeague.Sport));

        //    var expected = LeagueManager.GetLeague(newLeague.Name);
        //    Assert.NotNull(expected);

        //    var newName = "new name";
        //    expected.Name = newName;
        //    LeagueManager.UpdateLeague(newLeague.Name, expected);

        //    var actual = LeagueManager.GetLeague(newName);
        //    Assert.Equal(expected.Name, actual.Name);
        //}
        //#endregion League tests

        //private Player GeneratePlayer() => new Player
        //{
        //    EmailAddress = Guid.NewGuid().ToString(),
        //    FirstName = Guid.NewGuid().ToString(),
        //    LastName = Guid.NewGuid().ToString(),
        //    Gender = (GenderType) new Random().Next(0,1),
        //    DateOfBirth = DateTime.Now.AddDays(new Random().Next(-10000, 0))
        //};

        //private Team GenerateTeam() => new Team()
        //{
        //    Name = Guid.NewGuid().ToString(),
        //    League = GenerateLeague()
        //};

        //private League GenerateLeague() => new League()
        //{
        //    Name = Guid.NewGuid().ToString(),
        //    Sport = new Sport { Name = "Basketball", Type = SportGenderTypes.Men }
        //};
    }
}
