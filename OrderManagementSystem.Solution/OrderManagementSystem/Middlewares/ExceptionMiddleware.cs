using OrderManagementSystem.Errors;
using System.Net;
using System.Text.Json;

namespace OrderManagementSystem.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate Next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = Next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context )
        {
            try
            {
                await _next.Invoke(context);

            }
            catch( Exception ex ) 
            {
                _logger.LogError(ex , ex.Message );
                context.Response.ContentType = "application/json";
                context.Response.StatusCode =(int) HttpStatusCode.InternalServerError;
               
                var Response = _env.IsDevelopment() ? new APIExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) : new APIExceptionResponse((int)HttpStatusCode.InternalServerError);
                var JsonResponse = JsonSerializer.Serialize(Response);
                context.Response.WriteAsync(JsonResponse);
            }
        }
    }
}
