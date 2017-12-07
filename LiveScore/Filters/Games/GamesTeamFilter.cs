using LiveScore.Contracts;
using LiveScore.Models.Business;

namespace LiveScore.Filters.Games
{
    public class GamesTeamFilter : IFilter<Game>
    {
        private readonly int teamId;

        public GamesTeamFilter(int teamId)
        {
            this.teamId = teamId;
        }

        public bool Check(Game game)
        {
            return game.HomeTeam.Id == teamId || game.AwayTeam.Id == teamId;
        }
    }
}
