using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Aggregate_Order;

namespace Talabat.Core.Services
{
    public interface IOrderServices
    {
        public Task<Order?> CreateOrder(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress);
        public Task<IReadOnlyList<Order>> GetOrdersForSpecificUser(string BuyerEmail);
        public Task<Order> GetOrderByIdForSpecificUser(string ByuerEmail, int OrderId);
    }
}
