using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductTypeWithBeandSpecification : BaseSpecification<Product>
    {
        public ProductTypeWithBeandSpecification(SpecificationParams Params) : base
            (
             
                P => 
                (string.IsNullOrEmpty(Params.Search)||P.Name.ToLower().Contains(Params.Search))
                &&
                (!Params.typeId.HasValue || P.ProductTypeId == Params.typeId)
                &&
                   (!Params.brandId.HasValue || P.ProductBrandId == Params.brandId)
            )
        {
            Inculde.Add(p => p.ProductBrand);
            Inculde.Add(p => p.ProductType);
            if (!string.IsNullOrEmpty(Params.sort))
            {
                switch (Params.sort)
                {
                    case "PriceAsc":
                        OrderByAscending(o => o.Price);
                        break;
                    case "PriceDesc":
                        OrderByDescending(o => o.Price);
                        break;
                    default:
                        OrderByAscending(a => a.Name);
                        break;
                }
            }
           
            AppliedPagination(Params.PageSize, (Params.PageSize) * (Params.pageIndex - 1));
           
        }
        public ProductTypeWithBeandSpecification(int id) : base(p => p.Id == id)
        {
            Inculde.Add(p => p.ProductBrand);
            Inculde.Add(p => p.ProductType);
        }
    }
}
