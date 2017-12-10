using LiveScore.Filters.Games;
using LiveScore.Models.Business;
using Xunit;

namespace LiveScoreTests.Filters.Games
{
    public class GamesGroupFilterTests
    {
        private readonly GamesGroupFilter gameFilter;

        public GamesGroupFilterTests()
        {
            gameFilter = new GamesGroupFilter("1");
        }

        [Theory]
        [InlineData(1)]
        public void CheckGameByGroupSuccess(int groupId)
        {
            var tmpGroup = new Group
            {
                Id = groupId
            };

            var game = new Game
            {
                Id = 1,
                HomeTeam = new Team
                {
                    Id = 1,
                    Group = tmpGroup
                },
                AwayTeam = new Team
                {
                    Id = 2,
                    Group = tmpGroup
                }
            };

            Assert.True(gameFilter.Check(game));
        }

        [Theory]
        [InlineData(2)]
        public void CheckGameByGroupFailure(int groupId)
        {
            var tmpGroup = new Group
            {
                Id = groupId
            };

            var game = new Game
            {
                Id = 1,
                HomeTeam = new Team
                {
                    Id = 1,
                    Group = tmpGroup
                },
                AwayTeam = new Team
                {
                    Id = 2,
                    Group = tmpGroup
                }
            };

            Assert.False(gameFilter.Check(game));
        }
    }
}
