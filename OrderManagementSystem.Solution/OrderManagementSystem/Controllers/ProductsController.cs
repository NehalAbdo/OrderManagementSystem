using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Core.Entities;
using Order.Core.Repository;
using Order.Core.Services;
using Order.Services;
using OrderManagementSystem.DTO;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("products")] 

        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var Products = await _unitOfWork.Repository<Product>().GetAllAsync();
            return Ok(Products);
        }
        [HttpGet("{id}")] 
        public async Task <ActionResult<Product>> GetProductById(int id)
        {
            var Product= await _unitOfWork.Repository<Product>().GetByIdAsync(id);
            return Ok(Product); 
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] 

        public async Task<ActionResult<Product>> CreateProduct(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Stock = productDto.Stock
            };

            await _unitOfWork.Repository<Product>().Add(product);
            await _unitOfWork.CompleteAsync(); 

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }
        [HttpGet("{productId}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> UpdateProduct(int productId, ProductDto productDto)
        {
            var product = await _unitOfWork.Repository<Product>().GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Stock = productDto.Stock;

            _unitOfWork.Repository<Product>().Update(product);
            await _unitOfWork.CompleteAsync(); 

            return NoContent();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Orders>>> GetAllOrders()
        {
            var orders = await _unitOfWork.Repository<Product>().GetAllAsync();
            return Ok(orders);
        }
    }
}
