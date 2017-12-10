using LiveScore.Contracts;
using LiveScore.Models.Business;
using LiveScore.Utils;
using System;

namespace LiveScore.Filters.Games
{
    /// <summary>
    /// Filter that checks whether game kick-off time is after provided start date.
    /// </summary>
    public class GamesStartDateFilter : IFilter<Game>
    {
        private readonly DateTime startDate;

        /// <summary>
        /// Constructor that recieves start date parameter.
        /// </summary>
        /// <param name="endDate">Start date parameter</param>
        public GamesStartDateFilter(string startDate)
        {
            this.startDate = DateTimeParser.ParseDate(startDate);
        }

        /// <summary>
        /// This method checks whether provided game started after defined start date.
        /// </summary>
        /// <param name="game">Game to be tested</param>
        /// <returns>Whether or not provided game started after defined start date.</returns>
        public bool Check(Game game)
        {
            return game.KickOff.CompareTo(startDate) >= 0;
        }
    }
}
