using LiveScore.Contracts;
using LiveScore.Models.Business;

namespace LiveScore.Filters.Games
{
    /// <summary>
    /// Filter that checks whether teams that participated in provided game belong to provided group.
    /// </summary>
    public class GamesGroupFilter : IFilter<Game>
    {
        private readonly int groupId;

        /// <summary>
        /// Constructor that receives group Id parameter.
        /// </summary>
        /// <param name="groupId">Group Id parameter</param>
        public GamesGroupFilter(int groupId)
        {
            this.groupId = groupId;
        }

        /// <summary>
        /// This method checks whether provided game has teams that belong to defined group.
        /// </summary>
        /// <param name="game">Game to be tested</param>
        /// <returns>Whether or not provided game teams belong to defined group.</returns>
        public bool Check(Game game)
        {
            return game.HomeTeam.Group.Id == groupId && game.AwayTeam.Group.Id == groupId;
        }
    }
}
