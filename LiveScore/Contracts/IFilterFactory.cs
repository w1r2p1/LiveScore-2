using LiveScore.Models.Request;
using System.Collections.Generic;

namespace LiveScore.Contracts
{
    public interface IFilterFactory<TEntity>
    {
        IFilter<TEntity> Create(Filter filter);
        IEnumerable<IFilter<TEntity>> Create(Filter[] filters);
    }
}
