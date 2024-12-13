using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Core.Entities;
using Order.Core.Services;
using OrderManagementSystem.Errors;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {


        private readonly IUnitOfWork _unitOfWork;

        public InvoiceController( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{invoiceId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Invoice>> GetInvoiceDetails(int invoiceId)
        {
            var invoice = await _unitOfWork.Repository<Invoice>().GetByIdAsync(invoiceId);
            if (invoice == null)
            {
                return NotFound(new APIResponse(404, $"Invoice with Id {invoiceId} not found."));
            }

            return Ok(invoice);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<Invoice>>> GetAllInvoices()
        {
            var invoices = await _unitOfWork.Repository<Invoice>().GetAllAsync();
            return Ok(invoices);
        }
    }


}

