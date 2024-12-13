using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Order.Core.Entities;
using Order.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository
{
    public static class SpecificationEvalutor <T> where T : BaseModel
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,ISpecification<T> Spec)
        {
            var Query = inputQuery;
            if(Spec.Criteria is not null)
            {
                Query=Query.Where(Spec.Criteria);
            }
            Query = Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            return Query;
        }
    }
}
