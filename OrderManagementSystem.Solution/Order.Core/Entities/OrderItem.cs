using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Entities
{
    public class OrderItem :BaseModel
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int OrderId { get; set; }
        public Orders Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
