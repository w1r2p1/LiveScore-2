using System.Collections.Generic;

namespace LiveScore.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(object id);
        IEnumerable<TEntity> GetAll();
        void Insert(TEntity entity);
        void InsertBulk(IEnumerable<TEntity> entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        IQuery<TEntity> Query();
    }
}
