using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Core.Entities;
using Order.Core.Services;
using Order.Services;
using OrderManagementSystem.DTO;
using OrderManagementSystem.Errors;
using System.Security.Claims;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerService;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(ICustomerServices customerService,IUnitOfWork unitOfWork)
        {
            _customerService = customerService;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDto customerDTO)
        {
            try
            {
                if (customerDTO == null)
                {
                    return BadRequest("Customer object is null");
                }

                var existingCustomer = await _customerService.GetCustomerByEmailAsync(customerDTO.Email);
                if (existingCustomer != null)
                {
                    return Conflict("Customer with this email already exists.");
                }

                var customer = new Order.Core.Entities.Customer
                {
                    Name = customerDTO.Name,
                    Email = customerDTO.Email
                };

                var createdCustomer = await _customerService.CreateCustomerAsync(customer);

                return CreatedAtAction(nameof(CreateCustomer), new { id = createdCustomer.Id }, createdCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [ProducesResponseType(typeof(IReadOnlyList<Orders>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status400BadRequest)]
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<IReadOnlyList<Orders>>> GetAllOrdersForCustomer(int customerId)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // var customer = await _unitOfWork.Repository<Customer>().GetByIdAsync(customerId);
            //if (customer.UserId != userId) return Forbid();

            var orders = await _customerService.GetOrdersForSpecificByCustomer(customerId);

            if (orders == null || orders.Count == 0)
            {
                return NotFound(new { Message = "No orders found for this customer." });
            }

            return Ok(orders);
        }
    }
    }

