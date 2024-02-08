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
    internal class DeiveryMethodConfigration : IEntityTypeConfiguration<Delivery_Method>
    {
        public void Configure(EntityTypeBuilder<Delivery_Method> builder)
        {
            builder.Property(a => a.Cost).HasColumnType("decimal(18,2)");
        }
    }
}
