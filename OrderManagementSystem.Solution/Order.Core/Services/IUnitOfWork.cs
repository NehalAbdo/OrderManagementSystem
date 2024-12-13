using Order.Core.Entities;
using Order.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Services
{
    public interface IUnitOfWork :IAsyncDisposable
    {
        Task<int> CompleteAsync();
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity:BaseModel;
    }
}
