using LiveScore.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
        private IEnumerable<PropertyInfo> includedProperties;

        /// <summary>
        /// Constuctor that receives DB context.
        /// </summary>
        /// <param name="context"></param>
        public DbSetRepository(ScoresDbContext context)
        {
            this.ctx = context;
            this.dbSet = context.Set<TEntity>();
            this.includedProperties = typeof(TEntity)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p =>
                    typeof(IEntity).IsAssignableFrom(p.PropertyType) ||
                    p.PropertyType.IsGenericType &&
                    (p.PropertyType.GetGenericTypeDefinition() == typeof(List<>)) &&
                    typeof(IEntity).IsAssignableFrom(p.PropertyType.GetGenericArguments()[0]));
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
        public TEntity Get(int id)
        {
            var query = dbSet.Where(e => e.Id == id);

            foreach (var includeProperty in includedProperties)
            {
                query = query.Include(includeProperty.Name);
            }

            return query.ToList().FirstOrDefault();
        }

        /// <summary>
        /// This method returns all the entities of certain business model.
        /// </summary>
        /// <returns>Business model entities</returns>
        public IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = dbSet;

            foreach (var includeProperty in includedProperties)
            {
                query = query.Include(includeProperty.Name);
            }

            return query.ToList();
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
    }
}
