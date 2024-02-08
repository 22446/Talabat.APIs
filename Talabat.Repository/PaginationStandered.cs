using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Repository
{
    public class PaginationStandered<T>
    {
        public PaginationStandered()
        {
        }

        public PaginationStandered( int _pageindex, int _pagesize, IReadOnlyList<T> _produtreturn,int count)
        {
            PageIndex = _pageindex;
            PageSize = _pagesize;
            Count = count;
            ProdutReturn = _produtreturn;
        }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
      
        public int Count { get; set; }
        public IReadOnlyList<T> ProdutReturn { get; set; }
    }
}
