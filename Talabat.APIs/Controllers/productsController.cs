using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repository;
using Talabat.Core.Specifications;
using Talabat.Repository;

namespace Talabat.APIs.Controllers
{

    public class productsController : ApiEntityBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;
        public productsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this.unitOfWork = unitOfWork;

        }
        [HttpGet]
        public async Task<ActionResult<PaginationStandered<ProductToReturnDto>>> GetProducts([FromQuery] SpecificationParams Params)
        {
            var spec = new ProductTypeWithBeandSpecification(Params);
            var Products = await unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);
            var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);
            var Spec2 = new GetCountOnly(Params);
            var count = await unitOfWork.Repository<Product>().GetCountSpecAsync(Spec2);
            var Returned = new PaginationStandered<ProductToReturnDto>()
            {
                PageIndex = Params.pageIndex,
                PageSize = Params.PageSize,
                Count = count,
                ProdutReturn = MappedProducts
            };
            return Ok(Returned);
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(ProductToReturnDto), 200)]
        [ProducesResponseType(typeof(ApiRespones), 404)]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProduct(int Id)
        {
            var spec = new ProductTypeWithBeandSpecification(Id);
            var Product = await unitOfWork.Repository<Product>().GetByIdWithSpecAsync(spec);
            if (Product is null) { return NotFound(new ApiRespones(404)); };
            var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(Product);

            return Ok(MappedProduct);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllProductType()
        {
            var Types = await unitOfWork.Repository<ProductType>().GetAllAsync();
            return Ok(Types);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllProductBrand()
        {
            var Brands = await unitOfWork.Repository<ProductBrand>().GetAllAsync();
            return Ok(Brands);
        }
    }
}
