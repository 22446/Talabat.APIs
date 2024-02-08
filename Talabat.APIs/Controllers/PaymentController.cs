using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    [Authorize]
    public class PaymentController : ApiEntityBase    
    {
        private readonly IMapper mapper;
        private readonly IPaymentSrevices _paymentSrevices;

        public PaymentController(IMapper mapper,IPaymentSrevices paymentSrevices)
        {
            this.mapper = mapper;
            _paymentSrevices = paymentSrevices;
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> basketDto(string basketId)
        {
            var basket =await _paymentSrevices.CreateOrUpdatePaymentIntent(basketId);
            if (basket is null) return BadRequest(new ApiRespones(401));
            var mappedBasket = mapper.Map<CustomerBasket, CustomerBasketDto>(basket);
            return Ok(mappedBasket);
        }
    }
}
