using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Entities
{
    public class Customer :BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<Orders> Orders { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
