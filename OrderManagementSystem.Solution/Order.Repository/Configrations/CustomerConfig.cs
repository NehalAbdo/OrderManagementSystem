using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository.Configrations
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.HasMany(c => c.Orders)
                           .WithOne(o => o.Customer)
                           .HasForeignKey(o => o.CustomerId);
            builder.Property(c=>c.Name).IsRequired();
            builder.Property(c=>c.Email).IsRequired();

            builder.HasOne(c => c.User)
                   .WithMany()
                   .HasForeignKey(c => c.UserId);

        }
    }
}
