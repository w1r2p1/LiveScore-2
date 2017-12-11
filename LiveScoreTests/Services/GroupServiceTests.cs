using LiveScore.Contracts;
using LiveScore.Models.Business;
using LiveScore.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace LiveScoreTests.Services
{
    public class GroupServiceTests
    {
        private readonly GroupService service;

        public GroupServiceTests()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .SetupGet(uow => uow.Groups)
                .Returns(GetGroupRepositoryMock());
            unitOfWorkMock
                .SetupGet(uow => uow.MatchDays)
                .Returns(GetMatchDayRepositoryMock());
            unitOfWorkMock
                .SetupGet(uow => uow.Games)
                .Returns(GetGameRepositoryMock());
            unitOfWorkMock
                .SetupGet(uow => uow.Leagues)
                .Returns(GetLeagueRepositoryMock());

            service = new GroupService(unitOfWorkMock.Object);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CheckGroupIdSuccess(int groupId)
        {
            Assert.True(service.CheckGroupId(groupId));
        }

        [Fact]
        public void CheckGroupIdFailure()
        {
            Assert.False(service.CheckGroupId(3));
        }

        [Fact]
        public void GetLeagueNameSuccess()
        {
            Assert.True(service.GetLeagueName(1).Equals("Champions League 2017/18"));
        }

        [Fact]
        public void GetLeagueNameFailure()
        {
            Assert.Throws<NullReferenceException>(() => service.GetLeagueName(2));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetGroupStandingsSuccess(int groupId)
        {
            var result = service.GetStandings(new int[] { groupId });

            Assert.NotNull(result);
            Assert.True(result.Length == 1);
            Assert.True(result[0].Standings.Length == 4);
        }

        [Fact]
        public void GetGroupStandingsFailure()
        {
            var result = service.GetStandings(new int[] { 0 });

            Assert.NotNull(result);
            Assert.True(result.Length == 0);
        }

        [Fact]
        public void GetAllStandingsSuccess()
        {
            var result = service.GetStandings();

            Assert.NotNull(result);
            Assert.True(result.Length == 2);
            Assert.True(result[0].Standings.Length == 4);
            Assert.True(result[1].Standings.Length == 4);
            Assert.True(result[0].Standings[0].Team == "MANUTD");
            Assert.True(result[0].Standings[1].Team == "BENFIKA");
            Assert.True(result[0].Standings[2].Team == "BASEL");
            Assert.True(result[0].Standings[3].Team == "CSKA");
            Assert.True(result[1].Standings[0].Team == "DINKYV");
            Assert.True(result[1].Standings[1].Team == "PARTIZAN");
            Assert.True(result[1].Standings[2].Team == "OLYMP");
            Assert.True(result[1].Standings[3].Team == "MANCTY");
        }

        private IRepository<League> GetLeagueRepositoryMock()
        {
            var league = new League
            {
                Id = 1,
                StartYear = 2017,
                EndYear = 2018,
                Name = "Champions League"
            };

            var leagueRepositoryMock = new Mock<IRepository<League>>();
            leagueRepositoryMock
                .Setup(r => r.Get(It.Is<int>(id => id == 1)))
                .Returns(league);

            return leagueRepositoryMock.Object;
        }

        private IRepository<Game> GetGameRepositoryMock()
        {
            var league = new League
            {
                Id = 1,
                StartYear = 2017,
                EndYear = 2018,
                Name = "Champions League"
            };

            var team1 = new Team { Id = 1, Name = "MANUTD" };
            var team2 = new Team { Id = 2, Name = "BASEL" };
            var team3 = new Team { Id = 3, Name = "BENFIKA" };
            var team4 = new Team { Id = 4, Name = "CSKA" };
            var team5 = new Team { Id = 5, Name = "MANCTY" };
            var team6 = new Team { Id = 6, Name = "DINKYV" };
            var team7 = new Team { Id = 7, Name = "PARTIZAN" };
            var team8 = new Team { Id = 8, Name = "OLYMP" };

            var score1 = new Score { Id = 1, HomeTeamGoals = 1, AwayTeamGoals = 0 };
            var score2 = new Score { Id = 2, HomeTeamGoals = 0, AwayTeamGoals = 0 };
            var score3 = new Score { Id = 3, HomeTeamGoals = 3, AwayTeamGoals = 2 };
            var score4 = new Score { Id = 4, HomeTeamGoals = 2, AwayTeamGoals = 2 };
            var score5 = new Score { Id = 5, HomeTeamGoals = 3, AwayTeamGoals = 1 };
            var score6 = new Score { Id = 6, HomeTeamGoals = 2, AwayTeamGoals = 0 };
            var score7 = new Score { Id = 7, HomeTeamGoals = 1, AwayTeamGoals = 1 };
            var score8 = new Score { Id = 8, HomeTeamGoals = 0, AwayTeamGoals = 0 };

            var game1 = new Game { Id = 1, HomeTeam = team1, AwayTeam = team2, Score = score1 };
            var game2 = new Game { Id = 1, HomeTeam = team3, AwayTeam = team4, Score = score2 };
            var game3 = new Game { Id = 1, HomeTeam = team1, AwayTeam = team4, Score = score3 };
            var game4 = new Game { Id = 1, HomeTeam = team3, AwayTeam = team2, Score = score4 };
            var game5 = new Game { Id = 1, HomeTeam = team5, AwayTeam = team6, Score = score5 };
            var game6 = new Game { Id = 1, HomeTeam = team7, AwayTeam = team8, Score = score6 };
            var game7 = new Game { Id = 1, HomeTeam = team6, AwayTeam = team8, Score = score7 };
            var game8 = new Game { Id = 1, HomeTeam = team7, AwayTeam = team6, Score = score8 };

            var games = new List<Game> { game1, game2, game3, game4, game5, game6, game7, game8 };

            var gameRepositoryMock = new Mock<IRepository<Game>>();
            gameRepositoryMock
                .Setup(r => r.Get(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns((Expression<Func<Game, bool>> predicate) => games.Where(predicate.Compile()));

            return gameRepositoryMock.Object;
        }

        private IRepository<MatchDay> GetMatchDayRepositoryMock()
        {
            var league = new League
            {
                Id = 1,
                StartYear = 2017,
                EndYear = 2018,
                Name = "Champions League"
            };

            var matchDay1 = new MatchDay
            {
                Id = 1,
                Number = 1,
                League = league
            };

            var matchDay2 = new MatchDay
            {
                Id = 2,
                Number = 2,
                League = league
            };

            var matchDays = new List<MatchDay> { matchDay1, matchDay2 };

            var matchDayRepositoryMock = new Mock<IRepository<MatchDay>>();
            matchDayRepositoryMock
                .Setup(r => r.Get(It.IsAny<Expression<Func<MatchDay, bool>>>()))
                .Returns((Expression<Func<MatchDay, bool>> predicate) => matchDays.Where(predicate.Compile()));

            return matchDayRepositoryMock.Object;
        }

        private IRepository<Group> GetGroupRepositoryMock()
        {
            var league = new League
            {
                Id = 1,
                StartYear = 2017,
                EndYear = 2018,
                Name = "Champions League"
            };

            var team1 = new Team { Id = 1, Name = "MANUTD" };
            var team2 = new Team { Id = 2, Name = "BASEL" };
            var team3 = new Team { Id = 3, Name = "BENFIKA" };
            var team4 = new Team { Id = 4, Name = "CSKA" };
            var team5 = new Team { Id = 5, Name = "MANCTY" };
            var team6 = new Team { Id = 6, Name = "DINKYV" };
            var team7 = new Team { Id = 7, Name = "PARTIZAN" };
            var team8 = new Team { Id = 8, Name = "OLYMP" };

            var group1 = new Group
            {
                Id = 1,
                Name = "A",
                League = league,
                Teams = new List<Team> { team1, team2, team3, team4 }
            };

            var group2 = new Group
            {
                Id = 2,
                Name = "B",
                League = league,
                Teams = new List<Team> { team5, team6, team7, team8 }
            };

            var groups = new List<Group> { group1, group2 };

            var groupRepositoryMock = new Mock<IRepository<Group>>();
            groupRepositoryMock
                .Setup(r => r.Get(It.Is<int>(id => id == 1)))
                .Returns(group1);
            groupRepositoryMock
                .Setup(r => r.Get(It.Is<int>(id => id == 2)))
                .Returns(group2);
            groupRepositoryMock
                .Setup(r => r.Get())
                .Returns(groups);
            groupRepositoryMock
                .Setup(r => r.Get(It.IsAny<Expression<Func<Group, bool>>>()))
                .Returns((Expression<Func<Group, bool>> predicate) => groups.Where(predicate.Compile()));

            return groupRepositoryMock.Object;
        }
    }
}
