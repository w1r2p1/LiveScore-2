using LiveScore.Filters.Games;
using LiveScore.Models.Business;
using LiveScore.Utils;
using Xunit;

namespace LiveScoreTests.Filters.Games
{
    public class GamesEndDateFilterTests
    {
        private readonly GamesEndDateFilter gameFilter;

        public GamesEndDateFilterTests()
        {
            gameFilter = new GamesEndDateFilter("2017-09-28T20:45:00");
        }

        [Theory]
        [InlineData("2017-09-28T20:30:00")]
        public void CheckGameByEndDateSuccess(string kickoff)
        {
            var game = new Game
            {
                Id = 1,
                KickOff = DateTimeParser.ParseDate(kickoff)
            };

            Assert.True(gameFilter.Check(game));
        }

        [Theory]
        [InlineData("2017-09-28T21:00:00")]
        public void CheckGameByEndDateFailure(string kickoff)
        {
            var game = new Game
            {
                Id = 1,
                KickOff = DateTimeParser.ParseDate(kickoff)
            };

            Assert.False(gameFilter.Check(game));
        }
    }
}
