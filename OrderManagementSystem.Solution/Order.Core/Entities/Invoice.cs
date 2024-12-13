using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Entities
{
    public class Invoice:BaseModel
    {
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int OrderId { get; set; }
        public Orders Order { get; set; }

    }
}
