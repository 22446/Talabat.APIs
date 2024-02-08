using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class GetCountOnly:BaseSpecification<Product>
    {
        public GetCountOnly(SpecificationParams Params) : base
            (
                P =>
                (string.IsNullOrEmpty(Params.Search) || P.Name.ToLower().Contains(Params.Search))
                &&
                (!Params.typeId.HasValue || P.ProductTypeId == Params.typeId)
                &&
                (!Params.brandId.HasValue || P.ProductBrandId == Params.brandId)
            )
        {
            
        }
    }
}
