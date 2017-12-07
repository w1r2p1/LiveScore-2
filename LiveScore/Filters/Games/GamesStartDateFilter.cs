using LiveScore.Contracts;
using LiveScore.Models.Business;
using System;

namespace LiveScore.Filters.Games
{
    public class GamesStartDateFilter : IFilter<Game>
    {
        private readonly DateTime startDate;

        public GamesStartDateFilter(DateTime startDate)
        {
            this.startDate = startDate;
        }

        public bool Check(Game game)
        {
            return game.KickOff.CompareTo(startDate) >= 0;
        }
    }
}
