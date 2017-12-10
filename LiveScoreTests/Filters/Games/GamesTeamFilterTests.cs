using LiveScore.Filters.Games;
using LiveScore.Models.Business;
using Xunit;

namespace LiveScoreTests.Filters.Games
{
    public class GamesTeamFilterTests
    {
        private readonly GamesTeamFilter gameFilter;

        public GamesTeamFilterTests()
        {
            gameFilter = new GamesTeamFilter("1");
        }

        [Theory]
        [InlineData(1, 2)]
        public void CheckGameByTeamSuccess(int homeTeamId, int awayTeamId)
        {
            var game = new Game
            {
                Id = 1,
                HomeTeam = new Team
                {
                    Id = homeTeamId
                },
                AwayTeam = new Team
                {
                    Id = awayTeamId
                }
            };

            Assert.True(gameFilter.Check(game));
        }

        [Theory]
        [InlineData(3, 2)]
        public void CheckGameByTeamFailure(int homeTeamId, int awayTeamId)
        {
            var game = new Game
            {
                Id = 1,
                HomeTeam = new Team
                {
                    Id = homeTeamId
                },
                AwayTeam = new Team
                {
                    Id = awayTeamId
                }
            };

            Assert.False(gameFilter.Check(game));
        }
    }
}
