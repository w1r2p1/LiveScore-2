using LiveScore.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LiveScoreDbModule.DAL
{
    public class DbSetRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private ScoresDbContext ctx;
        private DbSet<TEntity> dbSet;

        public DbSetRepository(ScoresDbContext context)
        {
            this.ctx = context;
            this.dbSet = context.Set<TEntity>();
        }

        public void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (ctx.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }

            dbSet.Remove(entityToDelete);
        }

        public TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void InsertBulk(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            ctx.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public IEnumerable<TEntity> Query<TProperty>(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, TProperty>>[] navigationPropertyPath)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in navigationPropertyPath)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
    }
}
