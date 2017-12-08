using LiveScore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LiveScoreDbModule.DAL
{
    /// <summary>
    /// Generic query object implementation with fluid API.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Query<TEntity> : IQuery<TEntity> where TEntity : class, IEntity
    {
        private IQueryable<TEntity> query;

        /// <summary>
        /// Constructor that receives queryable source parameter.
        /// </summary>
        /// <param name="queryableSource">Queryable source collection</param>
        public Query(IQueryable<TEntity> queryableSource)
        {
            this.query = queryableSource;
        }

        /// <summary>
        /// This method executes query object.
        /// </summary>
        /// <returns>Entities obtained with created query</returns>
        public IEnumerable<TEntity> Execute()
        {
            return query.ToList();
        }

        /// <summary>
        /// This method adds property that will be eagerly loaded with parent entity.
        /// </summary>
        /// <param name="navigationPropertyPath">Property that will be loaded</param>
        /// <returns>Existing query object</returns>
        public IQuery<TEntity> Include(Expression<Func<TEntity, object>> navigationPropertyPath)
        {
            query = query.Include(navigationPropertyPath);
            return this;
        }

        /// <summary>
        /// This method adds filtering condition to query object.
        /// </summary>
        /// <param name="filter">Filtering condition</param>
        /// <returns>Existing query object</returns>
        public IQuery<TEntity> Where(Expression<Func<TEntity, bool>> filter)
        {
            query = query.Where(filter);
            return this;
        }
    }
}
