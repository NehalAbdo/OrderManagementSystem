using Microsoft.AspNetCore.Http;

namespace OrderManagementSystem.Errors
{
    public class APIResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public APIResponse(int statusCode, string? message=null) 
        {
            StatusCode = statusCode;
            Message = message ?? GetErrorMessage(statusCode);
        }
        public string GetErrorMessage(int statusCode)
           => statusCode switch
           {
               500 => "Internal Server Error",
               404 => "Not Found",
               401 => "UnAuthorized",
               400 => "Bad Request",
               _ => ""
           };
    }
}
