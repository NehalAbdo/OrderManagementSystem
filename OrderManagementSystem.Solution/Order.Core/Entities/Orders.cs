using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Entities
{
    public class Orders :BaseModel
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public PaymentMethod PaymentMethod { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set;}


    }
}
