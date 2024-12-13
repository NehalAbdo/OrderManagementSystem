using Order.Core.Entities;
using Order.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Repository
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task Add(T entity);
        void Update(T entity);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync (ISpecification<T> specification);
        Task<T> GetByIDSpecAsync(ISpecification<T> specification);

    }
}
