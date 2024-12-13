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
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasOne(i => i.Order)
               .WithOne()
               .HasForeignKey<Invoice>(i => i.OrderId);
            builder.Property(i => i.TotalAmount).HasColumnType("decimal(18,2)");
        }
    }
}
