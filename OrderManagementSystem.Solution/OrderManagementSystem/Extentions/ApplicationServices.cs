using Microsoft.AspNetCore.Mvc;
using Order.Core.Repository;
using Order.Core.Services;
using Order.Repository;
using Order.Services;
using OrderManagementSystem.Errors;
using OrderManagementSystem.Helpers;

namespace OrderManagementSystem.Extentions
{
    public static class ApplicationServices
    {
        public static IServiceCollection  AddApplicationServices (this IServiceCollection Services)
        {
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped(typeof(IOrderServices),typeof(OrderService));
            Services.AddScoped(typeof(ICustomerServices),typeof(CustomerService));
            //Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddAutoMapper(typeof(MappingProfile));
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                        .SelectMany(P => P.Value.Errors)
                                                        .Select(E => E.ErrorMessage)
                                                        .ToArray();
                    var ValidationErrorResponse = new APIValidationErrorResponse()
                    { Errors = errors };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });
            return Services;
        }
    }
}
