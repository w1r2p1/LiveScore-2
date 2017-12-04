using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LiveScore.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void InsertBulk(IEnumerable<TEntity> entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        IEnumerable<TEntity> Query<TProperty>(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, TProperty>>[] navigationPropertyPath);
    }
}
