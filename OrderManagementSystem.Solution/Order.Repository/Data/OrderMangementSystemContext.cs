using Microsoft.EntityFrameworkCore;
using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository.Data
{
    public class OrderMangementSystemContext :DbContext
    {
        public OrderMangementSystemContext(DbContextOptions<OrderMangementSystemContext> options): base(options) 
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Orders> Orders {  get; set; }
        public DbSet<Product> Products { get; set; }


    }
}
