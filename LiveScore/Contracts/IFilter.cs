namespace LiveScore.Contracts
{
    /// <summary>
    /// Interface that defines contract for filtering implementations.
    /// </summary>
    /// <typeparam name="TEntity">Type of busines model</typeparam>
    public interface IFilter<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// This method checks whether the provided entity satisfies filter condition.
        /// </summary>
        /// <param name="entity">Provided entity to be tested.</param>
        /// <returns>Whether or not provided entity satisfies filter condition.</returns>
        bool Check(TEntity entity);
    }
}
