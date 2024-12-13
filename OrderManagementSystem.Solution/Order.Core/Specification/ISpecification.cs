using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Specification
{
    public interface ISpecification<T> where T : BaseModel
    {
        Expression<Func<T, bool>> Criteria { get; set; }
        List<Expression<Func<T, object>>> Includes { get; set; }
    }
}
