using Order.Core.Entities;
using Order.Core.Services;
using Order.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Services
{
    public class OrderService :IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Orders?> CreateOrderAsync(Orders orders)
        {
            if (orders.OrderItems == null) throw new Exception("No Order Items found");
            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;
            foreach (var item in orders.OrderItems)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.ProductId);
                if (product == null) throw new Exception($"Product with Id {item.ProductId} not found.");
                if (product.Stock < item.Quantity) throw new Exception($"Insufficient stock for product {product.Name}");


                item.Price = product.Price;
                totalAmount += item.Quantity * item.Price;

                orderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Discount = 0 
                });

                
                 UpdateProductStock(product, item.Quantity);
            }

            var discount = CalculateDiscount(totalAmount);
            orders.TotalAmount = totalAmount - discount;
            orders.OrderDate = DateTime.UtcNow;
            orders.OrderItems = orderItems;

            await _unitOfWork.Repository<Orders>().Add(orders);
            var Result= await _unitOfWork.CompleteAsync();
            if (Result <= 0) return null;
            return orders;
        }
        public decimal CalculateDiscount(decimal totalAmount)
        {
            if (totalAmount > 200)
            {
                return totalAmount * 0.10m;
            }
            else if (totalAmount > 100)
            {
                return totalAmount * 0.05m;
            }
            return 0;
        }
        public void UpdateProductStock(Product product, int quantity)
        {
            product.Stock -= quantity;
            _unitOfWork.Repository<Product>().Update(product);
        }

        public async Task<Orders> GetOrderBIdForSpeceficCustomer (int customerId ,int orderId)
        {
            var Spec= new OrderSpecification(customerId, orderId);
            var Order=await _unitOfWork.Repository<Orders>().GetByIDSpecAsync(Spec);
            return Order;
        }
    

    }
}
