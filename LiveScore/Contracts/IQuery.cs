using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LiveScore.Contracts
{
    /// <summary>
    /// Interface that defines query-related contract for fluent-API query object.
    /// </summary>
    /// <typeparam name="TEntity">Type of busines model</typeparam>
    public interface IQuery<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// This method adds filtering condition to query object.
        /// </summary>
        /// <param name="filter">Filtering condition</param>
        /// <returns>Existing query object</returns>
        IQuery<TEntity> Where(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// This method adds property that will be eagerly loaded with parent entity.
        /// </summary>
        /// <param name="navigationPropertyPath">Property that will be loaded</param>
        /// <returns>Existing query object</returns>
        IQuery<TEntity> Include(Expression<Func<TEntity, object>> navigationPropertyPath);

        /// <summary>
        /// This method executes query object.
        /// </summary>
        /// <returns>Entities obtained with created query</returns>
        IEnumerable<TEntity> Execute();
    }
}
