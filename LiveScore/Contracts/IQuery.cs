using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LiveScore.Contracts
{
    public interface IQuery<TEntity> where TEntity : class
    {
        IQuery<TEntity> Where(Expression<Func<TEntity, bool>> filter);
        IQuery<TEntity> Include(params Expression<Func<TEntity, object>>[] navigationPropertyPath);
        IEnumerable<TEntity> Execute();
    }
}
