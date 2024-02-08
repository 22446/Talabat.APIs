using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Aggregate_Order;

namespace Talabat.Core.Specifications.AggregateSpec
{
    public class OrderSpecification:BaseSpecification<Order>
    {
        public OrderSpecification(string Email):base(a=>a.BuyerEmail==Email)
        {
            Inculde.Add(a => a.DeliveryMethod);
            Inculde.Add(a => a.Items);
            OrderByDescending(a => a.OrderDate);
        }
        public OrderSpecification(int id,string Email):base(a=>a.Id==id && a.BuyerEmail==Email)
        {
            Inculde.Add(a => a.DeliveryMethod);
            Inculde.Add(a => a.Items);
        }
    }
}
