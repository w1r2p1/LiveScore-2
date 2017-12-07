using LiveScore.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LiveScoreDbModule.DAL
{
    public class Query<TEntity> : IQuery<TEntity> where TEntity : class
    {
        private IQueryable<TEntity> query;

        public Query(IQueryable<TEntity> queryableSource)
        {
            this.query = queryableSource;
        }

        public IEnumerable<TEntity> Execute()
        {
            return query.ToList();
        }

        public IQuery<TEntity> Include(Expression<Func<TEntity, object>> navigationPropertyPath)
        {
            query = query.Include(navigationPropertyPath);
            return this;
        }

        public IQuery<TEntity> Where(Expression<Func<TEntity, bool>> filter)
        {
            query = query.Where(filter);
            return this;
        }
    }
}
