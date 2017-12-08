using LiveScore.Contracts;
using LiveScore.Models.Business;
using System;

namespace LiveScore.Filters.Games
{
    /// <summary>
    /// Filter that checks whether game kick-off time is before provided end date.
    /// </summary>
    public class GamesEndDateFilter : IFilter<Game>
    {
        private readonly DateTime endDate;

        /// <summary>
        /// Constructor that recieves end date parameter.
        /// </summary>
        /// <param name="endDate">End date parameter</param>
        public GamesEndDateFilter(DateTime endDate)
        {
            this.endDate = endDate;
        }

        /// <summary>
        /// This method checks whether provided game started before defined end date.
        /// </summary>
        /// <param name="game">Game to be tested</param>
        /// <returns>Whether or not provided game started before defined end date.</returns>
        public bool Check(Game game)
        {
            return game.KickOff.CompareTo(endDate) <= 0;
        }
    }
}
