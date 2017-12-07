using LiveScore.Contracts;
using LiveScore.Models.Business;
using System;
using System.Collections.Generic;
using LiveScore.Models.Request;
using LiveScore.Filters.Games;
using System.Linq;

namespace LiveScore.Filters
{
    public class GamesFilterFactory : IFilterFactory<Game>
    {
        private readonly Dictionary<FilterType, Type> gameFilters;

        public GamesFilterFactory()
        {
            gameFilters = new Dictionary<FilterType, Type>
            {
                { FilterType.StartDate, typeof(GamesStartDateFilter) },
                { FilterType.EndDate, typeof(GamesEndDateFilter) },
                { FilterType.Team, typeof(GamesTeamFilter) },
                { FilterType.Group, typeof(GamesGroupFilter) }
            };
        }

        public IFilter<Game> Create(Filter filter)
        {
            return Activator.CreateInstance(gameFilters[filter.Name]) as IFilter<Game>;
        }

        public IEnumerable<IFilter<Game>> Create(Filter[] filters)
        {
            return filters.Select(f => Activator.CreateInstance(gameFilters[f.Name])).Cast<IFilter<Game>>();
        }
    }
}
