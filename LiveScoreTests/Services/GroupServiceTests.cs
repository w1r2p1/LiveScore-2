using LiveScore.Contracts;
using LiveScore.Models.Business;
using LiveScore.Services;
using Moq;
using System;
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
            var teams = Mocks.GetTeams().ToList();
            var result = service.GetStandings();

            Assert.NotNull(result);
            Assert.True(result.Length == 2);
            Assert.True(result[0].Standings.Length == 4);
            Assert.True(result[1].Standings.Length == 4);
            Assert.True(result[0].Standings[0].Team.Equals(teams[0].Name));
            Assert.True(result[0].Standings[1].Team.Equals(teams[2].Name));
            Assert.True(result[0].Standings[2].Team.Equals(teams[1].Name));
            Assert.True(result[0].Standings[3].Team.Equals(teams[3].Name));
            Assert.True(result[1].Standings[0].Team.Equals(teams[5].Name));
            Assert.True(result[1].Standings[1].Team.Equals(teams[6].Name));
            Assert.True(result[1].Standings[2].Team.Equals(teams[7].Name));
            Assert.True(result[1].Standings[3].Team.Equals(teams[4].Name));
        }

        private IRepository<League> GetLeagueRepositoryMock()
        {
            var leagueRepositoryMock = new Mock<IRepository<League>>();
            leagueRepositoryMock
                .Setup(r => r.Get(It.Is<int>(id => id == 1)))
                .Returns(Mocks.GetLeague());

            return leagueRepositoryMock.Object;
        }

        private IRepository<Game> GetGameRepositoryMock()
        {
            var gameRepositoryMock = new Mock<IRepository<Game>>();
            gameRepositoryMock
                .Setup(r => r.Get(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns((Expression<Func<Game, bool>> predicate) => Mocks.GetGames().Where(predicate.Compile()));

            return gameRepositoryMock.Object;
        }

        private IRepository<MatchDay> GetMatchDayRepositoryMock()
        {
            var matchDayRepositoryMock = new Mock<IRepository<MatchDay>>();
            matchDayRepositoryMock
                .Setup(r => r.Get(It.IsAny<Expression<Func<MatchDay, bool>>>()))
                .Returns((Expression<Func<MatchDay, bool>> predicate) => Mocks.GetMatchDays().Where(predicate.Compile()));

            return matchDayRepositoryMock.Object;
        }

        private IRepository<Group> GetGroupRepositoryMock()
        {
            var groups = Mocks.GetGroups().ToList();

            var groupRepositoryMock = new Mock<IRepository<Group>>();
            groupRepositoryMock
                .Setup(r => r.Get(It.Is<int>(id => id == 1)))
                .Returns(groups[0]);
            groupRepositoryMock
                .Setup(r => r.Get(It.Is<int>(id => id == 2)))
                .Returns(groups[1]);
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
