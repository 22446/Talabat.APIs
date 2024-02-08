using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIs.DTO;
using Talabat.Core.Entities.Aggregate_Order;

namespace Talabat.APIs.Helper
{
    public class OrderPictureUrl : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrderPictureUrl(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
          if(source.Product.PictureUrl is not null)
            {
              return $"{configuration["BaseUrl"]}{source.Product.PictureUrl}  ";
            }
            return string.Empty;
        }
    }
}
