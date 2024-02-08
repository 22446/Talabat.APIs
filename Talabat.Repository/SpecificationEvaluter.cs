using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvaluter<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> StartInput, ISpecifacation<T> Spec)
        {
            var Query = StartInput;
            if (Spec.Createria is not null)
            {
                Query = Query.Where(Spec.Createria);
            }
            if (Spec.OrderByAsc is not null)
            {
                Query = Query.OrderBy(Spec.OrderByAsc);
            }
            if (Spec.OrderByDesc is not null)
            {
                Query = Query.OrderByDescending(Spec.OrderByDesc);
            }
            if(Spec.IsPaginationApplied)
            {
                Query = Query.Skip(Spec.Skip).Take(Spec.Take);
            }
            Query = Spec.Inculde.Aggregate(Query, (CurrentQuery, ListOfIncludes) => CurrentQuery.Include(ListOfIncludes));

            return Query;
        }
    }
}
