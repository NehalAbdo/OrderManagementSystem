using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Order.Core.Entities;
using Order.Core.Services;
using Order.Repository.Identity;
using Order.Services;
using System.Text;

namespace OrderManagementSystem.Extentions
{
    public static class IdentityServices
    {
        public static IServiceCollection AddIdentityServices (this IServiceCollection Services , IConfiguration _configuration) 
        {
            Services.AddScoped<ITokenServices, TokenServices>();
            Services.AddIdentity<User, IdentityRole>()
              .AddEntityFrameworkStores<AppIdentityDbContext>();
            Services.AddAuthentication(Options=>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(Options => 
                {
                    Options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer= _configuration["JWT:Issuer"],
                        ValidateAudience = true,
                        ValidAudience= _configuration["JWT:Audience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]))

                };
                });
            return Services;
        }

    }
}
