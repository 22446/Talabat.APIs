using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Aggregate_Order;
using Talabat.Core.Repository;
using Talabat.Core.Services;
using Talabat.Core.Specifications.AggregateSpec;

namespace Talabat.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketGenric;

        public OrderServices( IUnitOfWork unitOfWork,IBasketRepository BasketGenric)
        {
             _unitOfWork = unitOfWork;
            _basketGenric = BasketGenric;
        }
        public async Task<Order?> CreateOrder(string BuyerEmail, string BasketId, int DeliveryMethodId, Core.Entities.Aggregate_Order.Address ShippingAddress)
        {
            var OrderItems = new List<OrderItem>();
            var Basket = await _basketGenric.GetBasketAsync(BasketId);

            if (Basket?.Items.Count > 0)
                foreach (var item in Basket.Items)
                {
                    var Product = await _unitOfWork.Repository<Product>().GetById(item.Id);
                    ProductItemOrdered productItemOrdered = new ProductItemOrdered(Product.Id, Product.Name, Product.PictureUrl);
                    var orderItem = new OrderItem(productItemOrdered, Product.Price, item.Quantity);
                    OrderItems.Add(orderItem);
                }
            var Subtotal = OrderItems.Sum(i => i.Price * i.Quantity);

            var Delivery = await _unitOfWork.Repository<Delivery_Method>().GetById(DeliveryMethodId);
            Order order = new Order(BuyerEmail, ShippingAddress, Delivery, OrderItems, Subtotal, Basket.PaymentIntentId);
            await _unitOfWork.Repository<Order>().AddAsync(order);
            var result =await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;
            return order;

        }

        public async Task<Order> GetOrderByIdForSpecificUser(string ByuerEmail, int OrderId)
        {
            var spec = new OrderSpecification(OrderId, ByuerEmail);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForSpecificUser(string BuyerEmail)
        {
            var Spec = new OrderSpecification(BuyerEmail);
            var order =await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(Spec);
            return order;
        }
    }
}
