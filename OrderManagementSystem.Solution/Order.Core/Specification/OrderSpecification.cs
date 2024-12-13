using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Specification
{
    public class OrderSpecification :Specification<Orders>
    {
        public OrderSpecification(int id):base(o=>o.CustomerId== id) 
        {
            Includes.Add(o => o.OrderItems);
            Includes.Add(o => o.PaymentMethod);
            Includes.Add(o => o.Status);
        }

        public OrderSpecification(int customerId,int orderId) : base(o => o.CustomerId == customerId &&o.Id==orderId)
        {
            Includes.Add(o => o.OrderItems);
            Includes.Add(o => o.PaymentMethod);
            Includes.Add(o => o.Status);

        }
    }
}
