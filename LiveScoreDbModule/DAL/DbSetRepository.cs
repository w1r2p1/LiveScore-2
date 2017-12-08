using LiveScore.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LiveScoreDbModule.DAL
{
    /// <summary>
    /// Generic repository implementation.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DbSetRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private ScoresDbContext ctx;
        private DbSet<TEntity> dbSet;

        /// <summary>
        /// Constuctor that receives DB context.
        /// </summary>
        /// <param name="context"></param>
        public DbSetRepository(ScoresDbContext context)
        {
            this.ctx = context;
            this.dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// This method removes entity from repository by given Id.
        /// </summary>
        /// <param name="id">Id of entitity that will be removed</param>
        public void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// This method removes provided entitity from repository.
        /// </summary>
        /// <param name="entityToDelete">Entity to be removed</param>
        public void Delete(TEntity entityToDelete)
        {
            if (ctx.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }

            dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// This method returns single entity obtained by unique Id.
        /// </summary>
        /// <param name="id">Unique entity Id</param>
        /// <returns>Business model entity</returns>
        public TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// This method adds provided entity to repository.
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// This method adds multiple entities to repository.
        /// </summary>
        /// <param name="entity">Entities to be added</param>
        public void InsertBulk(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        /// <summary>
        /// This method updates existing repository entity.
        /// </summary>
        /// <param name="entityToUpdate">Entity to be updated.</param>
        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            ctx.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// This method provides query object for repository.
        /// </summary>
        /// <returns>Fluent API query object</returns>
        public IQuery<TEntity> Query()
        {
            return new Query<TEntity>(dbSet);
        }

        /// <summary>
        /// This method returns all the entities of certain business model type.
        /// </summary>
        /// <returns>Business model entities</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return new Query<TEntity>(dbSet).Execute();
        }
    }
}
