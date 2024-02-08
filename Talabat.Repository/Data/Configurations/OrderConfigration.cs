using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Aggregate_Order;

namespace Talabat.Repository.Data.Configurations
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(p=>p.Status)
                .HasConversion(o=>o.ToString(),o=>(OrderStatus)Enum.Parse(typeof(OrderStatus),o));
            builder.Property(a => a.SubTotal)
                .HasColumnType("decimal(18,2)");
            builder.OwnsOne(a => a.ShippingAddress, a => a.WithOwner());
            builder.HasOne(a => a.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.NoAction);
                
        }
    }
}
