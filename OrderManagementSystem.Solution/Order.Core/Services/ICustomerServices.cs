using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Services
{
    public interface ICustomerServices
    {
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<IReadOnlyList<Orders>> GetOrdersForSpecificByCustomer(int customerId);



    }
}
