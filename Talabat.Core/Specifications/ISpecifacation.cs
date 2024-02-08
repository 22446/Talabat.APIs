using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecifacation<T>where T : BaseEntity
    {
        public Expression<Func<T,bool>>Createria{ get; set; }
        public List<Expression<Func<T,object>>>Inculde{ get; set; }

        public Expression<Func<T, object>> OrderByAsc { get; set; }
        public Expression<Func<T, object>> OrderByDesc {get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationApplied { get; set; }

    }
}
