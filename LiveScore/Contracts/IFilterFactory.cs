using LiveScore.Models.Request;
using System.Collections.Generic;

namespace LiveScore.Contracts
{
    /// <summary>
    /// Interface that defines filter factory.
    /// </summary>
    /// <typeparam name="TEntity">Type of busines model</typeparam>
    public interface IFilterFactory<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// This method creates single filter from single filter input model.
        /// </summary>
        /// <param name="filter">Filter input (request) model</param>
        /// <returns>Filter implementation</returns>
        IFilter<TEntity> Create(Filter filter);

        /// <summary>
        /// This method creates multiple filter from multiple filter input model.
        /// </summary>
        /// <param name="filters">Filter input (request) models</param>
        /// <returns>Multiple filter implementation</returns>
        IEnumerable<IFilter<TEntity>> Create(Filter[] filters);
    }
}
