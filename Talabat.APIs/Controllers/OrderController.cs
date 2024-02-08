using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Security.Claims;
using Talabat.APIs.DTO;
using Talabat.APIs.Errors;
using Talabat.Core;
using Talabat.Core.Entities.Aggregate_Order;
using Talabat.Core.Services;

namespace Talabat.APIs.Controllers
{
    
    public class OrderController : ApiEntityBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        private readonly IOrderServices _orderServices;

        public OrderController(IUnitOfWork unitOfWork,IMapper mapper,IOrderServices orderServices)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
          _orderServices = orderServices;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDto, Address>(orderDTO.ShippingAddress);
            var order =await _orderServices.CreateOrder(email, orderDTO.BasketId, orderDTO.DeliveryMethodId, MappedAddress);
            if (order is null) return BadRequest(new ApiRespones(400));
            return order;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> getOrderForSpecificUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Order = await _orderServices.GetOrdersForSpecificUser(Email);
            var mappedorder = _mapper.Map<IReadOnlyList<Order>,IReadOnlyList<OrderToReturnDto>>(Order);
            if (mappedorder is null) return NotFound(new ApiRespones(404,"k"));
            return Ok(mappedorder);

        }
        [HttpGet("{Id}")]
        [Authorize]
        public async Task<ActionResult<OrderToReturnDto>>getUserByIDAndEmail(int Id)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Order = await _orderServices.GetOrderByIdForSpecificUser(Email,Id);
            var mappedorder = _mapper.Map<Order, OrderToReturnDto>(Order);
            if (mappedorder is null) return NotFound(new ApiRespones(404, "k"));
            return Ok(mappedorder);
          
        }
        [HttpGet("getDeliveryMethod")]
        public async Task<ActionResult<Delivery_Method>> GetAllDeliveryMethod()
        {
            var MethodDelivery =await unitOfWork.Repository<Delivery_Method>().GetAllAsync();

            return Ok(MethodDelivery);
        }
    }
}
