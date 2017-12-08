using LiveScore.Contracts;
using LiveScore.Models.Business;
using System;
using System.Collections.Generic;
using LiveScore.Models.Request;
using LiveScore.Filters.Games;
using System.Linq;

namespace LiveScore.Filters
{
    /// <summary>
    /// Filter factory class for game-related filters.
    /// </summary>
    public class GamesFilterFactory : IFilterFactory<Game>
    {
        private readonly Dictionary<FilterType, Type> gameFilters;

        /// <summary>
        /// Default constructor.
        /// </summary>
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

        /// <summary>
        /// This method creates single filter from single filter input model.
        /// </summary>
        /// <param name="filter">Filter input (request) model</param>
        /// <returns>Filter implementation</returns>
        public IFilter<Game> Create(Filter filter)
        {
            return Activator.CreateInstance(gameFilters[filter.Name]) as IFilter<Game>;
        }

        /// <summary>
        /// This method creates multiple filter from multiple filter input model.
        /// </summary>
        /// <param name="filters">Filter input (request) models</param>
        /// <returns>Multiple filter implementation</returns>
        public IEnumerable<IFilter<Game>> Create(Filter[] filters)
        {
            return filters.Select(f => Activator.CreateInstance(gameFilters[f.Name])).Cast<IFilter<Game>>();
        }
    }
}
