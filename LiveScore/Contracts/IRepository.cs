using System.Collections.Generic;

namespace LiveScore.Contracts
{
    /// <summary>
    /// Interface that defines generic repository functionalities.
    /// </summary>
    /// <typeparam name="TEntity">Type of busines model</typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// This method returns single entity obtained by unique Id.
        /// </summary>
        /// <param name="id">Unique entity Id</param>
        /// <returns>Business model entity</returns>
        TEntity GetById(object id);
        
        /// <summary>
        /// This method returns all the entities of certain business model type.
        /// </summary>
        /// <returns>Business model entities</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// This method adds provided entity to repository.
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        void Insert(TEntity entity);

        /// <summary>
        /// This method adds multiple entities to repository.
        /// </summary>
        /// <param name="entity">Entities to be added</param>
        void InsertBulk(IEnumerable<TEntity> entity);

        /// <summary>
        /// This method removes entity from repository by given Id.
        /// </summary>
        /// <param name="id">Id of entitity that will be removed</param>
        void Delete(object id);

        /// <summary>
        /// This method removes provided entitity from repository.
        /// </summary>
        /// <param name="entityToDelete">Entity to be removed</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// This method updates existing repository entity.
        /// </summary>
        /// <param name="entityToUpdate">Entity to be updated.</param>
        void Update(TEntity entityToUpdate);

        /// <summary>
        /// This method provides query object for repository.
        /// </summary>
        /// <returns>Fluent API query object</returns>
        IQuery<TEntity> Query();
    }
}
