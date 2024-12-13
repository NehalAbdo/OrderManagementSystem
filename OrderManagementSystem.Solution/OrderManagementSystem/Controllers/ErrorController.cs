using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Errors;

namespace OrderManagementSystem.Controllers
{
    [Route("error/{Code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : ControllerBase
    {
        public ActionResult Error(int Code)
        {
            return NotFound(new APIResponse(Code));
        }
    }
}
