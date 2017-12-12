using LiveScore.Contracts;
using LiveScore.Exceptions;
using LiveScore.Models.Business;
using LiveScore.Models.Request;
using LiveScore.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace LiveScoreTests.Services
{
    public class ScoresServiceTests
    {
        private readonly ScoresService service;
        

        public ScoresServiceTests()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock
                .SetupGet(uow => uow.Games)
                .Returns(GetGameRepositoryMock());
            unitOfWorkMock
                .SetupGet(uow => uow.Scores)
                .Returns(GetScoreRepositoryMock());
            unitOfWorkMock
                .SetupGet(uow => uow.Leagues)
                .Returns(GetLeagueRepositoryMock());
            unitOfWorkMock
                .SetupGet(uow => uow.Teams)
                .Returns(GetTeamsRepositoryMock());
            unitOfWorkMock
                .SetupGet(uow => uow.MatchDays)
                .Returns(GetMatchDayRepositoryMock());

            var groupServiceMock = new Mock<IGroupService>();
            groupServiceMock
                .Setup(gs => gs.GetLeagueName(It.Is<int>(id => id == 1)))
                .Returns("Champions League 2017/18");

            var filters = Mocks.GetFilters();

            var filterFactoryMock = new Mock<IFilterFactory<Game>>();
            filterFactoryMock
                .Setup(ff => ff.Create(It.Is<Filter>(f => f.Name == FilterType.StartDate)))
                .Returns(filters[0]);
            filterFactoryMock
                .Setup(ff => ff.Create(It.Is<Filter>(f => f.Name == FilterType.EndDate)))
                .Returns(filters[1]);
            filterFactoryMock
                .Setup(ff => ff.Create(It.Is<Filter>(f => f.Name == FilterType.Group)))
                .Returns(filters[2]);
            filterFactoryMock
                .Setup(ff => ff.Create(It.Is<Filter>(f => f.Name == FilterType.Team)))
                .Returns(filters[3]);

            service = new ScoresService(unitOfWorkMock.Object,
                groupServiceMock.Object, filterFactoryMock.Object);
        }

        [Fact]
        public void GetAllScoresSuccess()
        {
            var result = service.GetScores();

            Assert.NotNull(result);
            Assert.True(result.Length == 8);
        }

        [Fact]
        public void GetStartDateFilterScoresSuccess()
        {
            var filter = new Filter { Name = FilterType.StartDate };
            var result = service.GetScores(new Filter[] { filter });

            Assert.NotNull(result);
            Assert.True(result.Length == 6);
        }

        [Fact]
        public void GetStartAndEndDateFilterScoresSuccess()
        {
            var filter1 = new Filter { Name = FilterType.StartDate, Relation = FilterRelation.AND };
            var filter2 = new Filter { Name = FilterType.EndDate, Relation = FilterRelation.AND };
            var result = service.GetScores(new Filter[] { filter1, filter2 });

            Assert.NotNull(result);
            Assert.True(result.Length == 3);
        }

        [Fact]
        public void SubmitScoresSuccess()
        {
            var resultBefore = service.GetScores();

            var gameScore = new GameScore
            {
                AwayTeam = "MANUTD",
                HomeTeam = "BENFIKA",
                Group = "A",
                KickOffAt = "2017-09-28T23:00:00",
                LeagueTitle = "Champions League 2017/18",
                MatchDay = 3,
                Score = "0:0"
            };

            service.SubmitScores(new GameScore[] { gameScore });

            var resultAfter = service.GetScores();

            Assert.NotNull(resultAfter);
            Assert.True(resultAfter.Length == resultBefore.Length + 1);
        }

        [Fact]
        public void SubmitScoresFailure()
        {
            var gameScore = new GameScore
            {
                AwayTeam = "MANUTD",
                HomeTeam = "BENFIKA",
                Group = "A",
                KickOffAt = "2017-09-28T66:00:00",
                LeagueTitle = "Champions League 2017/18",
                MatchDay = 3,
                Score = "0:0"
            };

            Assert.Throws<ClientRequestException>(() => service.SubmitScores(new GameScore[] { gameScore }));
        }

        [Fact]
        public void UpdateScoresSuccess()
        {
            var resultBefore = service.GetScores();

            var gameScore = new GameScore
            {
                AwayTeam = "BASEL",
                HomeTeam = "MANUTD",
                Group = "A",
                KickOffAt = "2017-09-28T19:00:00",
                LeagueTitle = "Champions League 2017/18",
                MatchDay = 1,
                Score = "2:0"
            };

            service.UpdateScores(new GameScore[] { gameScore });

            var resultAfter = service.GetScores();

            Assert.NotNull(resultAfter);
            Assert.True(resultAfter.Length == resultBefore.Length);
        }

        [Fact]
        public void UpdateScoresFailure()
        {
            var gameScore = new GameScore
            {
                AwayTeam = "BASEL",
                HomeTeam = "MANUTD",
                Group = "A",
                KickOffAt = "2017-09-28T66:00:00",
                LeagueTitle = "Champions League 2017/18",
                MatchDay = 1,
                Score = "2:0"
            };

            Assert.Throws<ClientRequestException>(() => service.UpdateScores(new GameScore[] { gameScore }));
        }

        private IRepository<Game> GetGameRepositoryMock()
        {
            var tmpGames = Mocks.GetGames().ToList();

            var gameRepositoryMock = new Mock<IRepository<Game>>();
            gameRepositoryMock
                .Setup(r => r.Get())
                .Returns(tmpGames);
            gameRepositoryMock
                .Setup(r => r.Get())
                .Returns(tmpGames);
            gameRepositoryMock
                .Setup(r => r.Insert(It.IsAny<Game>()))
                .Callback<Game>(g => tmpGames.Add(g));

            return gameRepositoryMock.Object;
        }

        private IRepository<Score> GetScoreRepositoryMock()
        {
            var tmpScores = Mocks.GetScores().ToList();

            var scoreRepositoryMock = new Mock<IRepository<Score>>();
            scoreRepositoryMock
                .Setup(r => r.Insert(It.IsAny<Score>()))
                .Callback<Score>(s => tmpScores.Add(s));
            scoreRepositoryMock
                .Setup(r => r.Update(It.Is<Score>(s => s.Id == 8)))
                .Callback<Score>(s => tmpScores[7] = s);

            return scoreRepositoryMock.Object;
        }

        private IRepository<League> GetLeagueRepositoryMock()
        {
            var leagueRepositoryMock = new Mock<IRepository<League>>();
            leagueRepositoryMock
                .Setup(r => r.Get())
                .Returns(new List<League> { Mocks.GetLeague() });

            return leagueRepositoryMock.Object;
        }

        private IRepository<Team> GetTeamsRepositoryMock()
        {
            var teamRepositoryMock = new Mock<IRepository<Team>>();
            teamRepositoryMock
                .Setup(r => r.Get(It.IsAny<int>()))
                .Returns<int>(tid => Mocks.GetTeams().Single(t => t.Id == tid));
            teamRepositoryMock
                .Setup(r => r.Get())
                .Returns(Mocks.GetTeams());

            return teamRepositoryMock.Object;
        }

        private IRepository<MatchDay> GetMatchDayRepositoryMock()
        {
            var matchDayRepositoryMock = new Mock<IRepository<MatchDay>>();
            matchDayRepositoryMock
                .Setup(r => r.Get(It.IsAny<int>()))
                .Returns<int>(tid => Mocks.GetMatchDays().Single(t => t.Id == tid));
            matchDayRepositoryMock
                .Setup(r => r.Get())
                .Returns(Mocks.GetMatchDays());

            return matchDayRepositoryMock.Object;
        }
    }
}
