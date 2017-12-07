using LiveScore.Contracts;
using LiveScore.Models.Business;
using System;

namespace LiveScore.Filters.Games
{
    public class GamesEndDateFilter : IFilter<Game>
    {
        private readonly DateTime endDate;

        public GamesEndDateFilter(DateTime endDate)
        {
            this.endDate = endDate;
        }

        public bool Check(Game game)
        {
            return game.KickOff.CompareTo(endDate) <= 0;
        }
    }
}
