using Order.Core.Entities;
using Order.Core.Repository;
using Order.Core.Services;
using Order.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly OrderMangementSystemContext _orderContext;
        private Hashtable _repository;
        public UnitOfWork(OrderMangementSystemContext orderContext)
        {
            _orderContext = orderContext;
            _repository = new Hashtable() ;
        }

        public async Task<int> CompleteAsync()
        => await _orderContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        =>await _orderContext.DisposeAsync();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel
        {
            var type= typeof(TEntity).Name;
            if (!_repository.ContainsKey(type))
            {
                var Repository = new GenericRepository<TEntity>(_orderContext);
                _repository.Add(type, Repository);
            }
            return _repository[type] as IGenericRepository<TEntity>;
        }
    }
}
