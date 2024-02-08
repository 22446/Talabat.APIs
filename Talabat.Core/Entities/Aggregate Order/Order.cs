using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Aggregate_Order
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }
        public Order( string buyerEmail, Address shippingAddress, Delivery_Method deliveryMethod, ICollection<OrderItem> items, decimal subTotal , string _PaymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = _PaymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
        public int DeliveryMethodId { get; set; }
        public Delivery_Method DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public decimal getTotal()
        {
            return SubTotal + DeliveryMethod.Cost;
        }
        public string PaymentIntentId { get; set; } 

    }
}
