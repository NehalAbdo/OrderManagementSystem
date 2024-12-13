using Microsoft.EntityFrameworkCore;
using Order.Core.Entities;
using Order.Core.Repository;
using Order.Core.Specification;
using Order.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly OrderMangementSystemContext _orderContext;

        public GenericRepository(OrderMangementSystemContext orderContext)
        {
            _orderContext = orderContext;
        }

        public async Task Add(T entity)
         =>await _orderContext.Set<T>().AddAsync(entity);
        public async Task<IReadOnlyList<T>> GetAllAsync() => await _orderContext.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _orderContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIDSpecAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public IQueryable<T> ApplySpecification(ISpecification<T> specification) 
        {
            return SpecificationEvalutor<T>.GetQuery(_orderContext.Set<T>(), specification);
        }

        public void Update (T entity)
            => _orderContext.Set<T>().Update(entity);


       
    }
}
