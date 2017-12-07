using LiveScore.Contracts;
using LiveScore.Models.Business;

namespace LiveScore.Filters.Games
{
    public class GamesGroupFilter : IFilter<Game>
    {
        private readonly int groupId;

        public GamesGroupFilter(int groupId)
        {
            this.groupId = groupId;
        }

        public bool Check(Game game)
        {
            return game.HomeTeam.Group.Id == groupId && game.AwayTeam.Group.Id == groupId;
        }
    }
}
