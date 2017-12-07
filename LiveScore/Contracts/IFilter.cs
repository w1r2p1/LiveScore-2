using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveScore.Contracts
{
    public interface IFilter<TEntity>
    {
        bool Check(TEntity entity);
    }
}
