using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecifacation<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Createria { get; set; }
        public List<Expression<Func<T, object>>> Inculde { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderByAsc { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPaginationApplied { get; set; }

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> CreiateriaExpression)
        {
            Createria = CreiateriaExpression;
        }
        public void OrderByAscending(Expression<Func<T, object>> _OrderByAsc)
        {
            OrderByAsc = _OrderByAsc;
        }
        public void OrderByDescending(Expression<Func<T, object>> _OrderByDesc)
        {
            OrderByDesc = _OrderByDesc;
        }
        public void AppliedPagination(int _Take,int _Skip)
        {
            IsPaginationApplied = true;
            Take = _Take;
            Skip = _Skip;
        }
    }
}
