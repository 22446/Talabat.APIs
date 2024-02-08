using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Aggregate_Order;

namespace Talabat.Repository.Data.Configurations
{
    internal class OrderItemsConfigration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(a => a.Price).HasColumnType("decimal(18,2)");
            builder.OwnsOne(a => a.Product, q => q.WithOwner());
           
        }
    }
}
