using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository.Configrations
{
    public class OrderConfig : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.HasMany(o => o.OrderItems)
                           .WithOne(oi => oi.Order)
                           .HasForeignKey(oi => oi.OrderId);
            builder.HasOne(o => o.Customer)
               .WithMany(c => c.Orders)
               .HasForeignKey(o => o.CustomerId);
            builder.Property(i => i.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Property(O=>O.Status)
                .HasConversion(Ostatus=>Ostatus.ToString(),OStatus=>(OrderStatus)Enum.Parse(typeof(OrderStatus),OStatus));

        }
    }
}
