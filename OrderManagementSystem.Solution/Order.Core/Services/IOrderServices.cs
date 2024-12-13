using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Services
{
    public interface IOrderServices
    {
        Task<Orders?> CreateOrderAsync(Orders orders);
        Task<Orders?> GetOrderBIdForSpeceficCustomer(int customerId, int orderId);


    }
}
