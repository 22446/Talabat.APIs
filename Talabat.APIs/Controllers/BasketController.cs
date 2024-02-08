using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repository;
using Talabat.Repository;

namespace Talabat.APIs.Controllers
{

    public class BasketController : ApiEntityBase
    {
        private readonly IMapper mapper;
        private readonly IBasketRepository basketRepository;

        public BasketController(IMapper mapper, IBasketRepository basketRepository)
        {
            this.mapper = mapper;
            this.basketRepository = basketRepository;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>>GetBasket(string Id)
        {
           var basket= await basketRepository.GetBasketAsync(Id);
            if (basket is null)
            {
                return new CustomerBasket(Id);
            }
            else
                return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreaateOrUpdateBasket(CustomerBasketDto customerBasketDto)
        {
          var MappedBasket = mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasketDto);
          var Basket= await basketRepository.UpdateBasketAsync(MappedBasket);
          if(Basket is null)
            {
                return BadRequest(new ApiRespones(400));
            }
            else
            {
                return Ok(Basket);
            }
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> CreaateOrUpdateBasket(string BasketId)
        {
            var Basket = await basketRepository.DeleteBasketAsync(BasketId);
            if (Basket == false)
            {
                return BadRequest(new ApiRespones(400));
            }
            else
            {
                return Ok(Basket);
            }
        }
    }
}
