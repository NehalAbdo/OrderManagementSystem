using Order.Core.Entities;
using Order.Core.Services;
using Order.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Services
{
    public class CustomerService :ICustomerServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            var customers = await _unitOfWork.Repository<Customer>().GetAllAsync();
            return customers.FirstOrDefault(c => c.Email == email);
        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            await _unitOfWork.Repository<Customer>().Add(customer);
            await _unitOfWork.CompleteAsync();

            return customer;
        }
        public Task<IReadOnlyList<Orders>> GetOrdersForSpecificByCustomer(int customerId)
        {
            var Spec = new OrderSpecification(customerId);
            var orders = _unitOfWork.Repository<Orders>().GetAllWithSpecAsync(Spec);
            return orders;
        }
    }
}
