using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIs.DTO;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Aggregate_Order;

namespace Talabat.APIs.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                    .ForMember(p => p.ProductBrand, o => o.MapFrom(o => o.ProductBrand.Name))
                    .ForMember(p => p.ProductType, o => o.MapFrom(o => o.ProductType.Name))
                    .ForMember(p => p.PictureUrl, o => o.MapFrom<PictureResolver>());
            CreateMap<Core.Entities.Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, Core.Entities.Aggregate_Order.Address>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(P => P.DeliveryMethod, o => o.MapFrom(a => a.DeliveryMethod.ShortName))
                .ForMember(P => P.DeliveryMethodCost, o => o.MapFrom(a => a.DeliveryMethod.Cost));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(a => a.ProductId, a => a.MapFrom(a => a.Product.ProductId))
                .ForMember(a => a.ProductName, a => a.MapFrom(a => a.Product.ProductName))
                .ForMember(a => a.PictureUrl, a => a.MapFrom<OrderPictureUrl>());

        }
    }
}
