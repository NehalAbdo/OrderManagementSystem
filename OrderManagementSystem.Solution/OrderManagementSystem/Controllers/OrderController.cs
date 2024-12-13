using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Core.Entities;
using Order.Core.Services;
using Order.Repository;
using Order.Services;
using OrderManagementSystem.Errors;
using System.Security.Claims;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderServices _orderService;
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IOrderServices orderService,IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _unitOfWork = unitOfWork;
        }
        [ProducesResponseType(typeof(Orders),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<ActionResult<Orders>> CreateOrder(Orders orders)
        {
            var order= await _orderService.CreateOrderAsync(orders);
            if (order is null) return BadRequest(new APIResponse(400,"There is an error with your order"));
            return Ok(order);
        }

        [HttpGet("{customerId}/orders/{orderId}")]
        [Authorize(Roles = "Customer")]
        [ProducesResponseType(typeof(Orders), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Orders>> GetDetailsOfOrder(int customerId, int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderBIdForSpeceficCustomer(customerId, orderId);

                if (order == null)
                {
                    return NotFound();
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<Orders>>> GetAllOrders()
        {
            var orders = await _unitOfWork.Repository<Orders>().GetAllAsync();
            return Ok(orders);
        }



    }
}
