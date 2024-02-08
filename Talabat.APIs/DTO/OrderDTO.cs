using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities.Aggregate_Order;

namespace Talabat.APIs.DTO
{
    public class OrderDTO
    {
       [Required]
       public string BasketId      { get; set; }
       public int DeliveryMethodId   { get; set; }
       public AddressDto ShippingAddress { get; set; }
    }
}
