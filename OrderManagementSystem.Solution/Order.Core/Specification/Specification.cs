using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Specification
{
    public class Specification<T> : ISpecification<T> where T : BaseModel
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes { get ; set ; }

        public Specification()
        {
            //Includes = new List<Expression<Func<T, object>>>();
        }
        public Specification(Expression<Func<T, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
            Includes = new List<Expression<Func<T, object>>>();

        }
    }
}
