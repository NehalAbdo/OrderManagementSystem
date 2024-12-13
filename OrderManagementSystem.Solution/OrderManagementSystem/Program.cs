using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.Core.Entities;
using Order.Core.Repository;
using Order.Repository;
using Order.Repository.Data;
using Order.Repository.Identity;
using Order.Services;
using OrderManagementSystem.Errors;
using OrderManagementSystem.Extentions;
using OrderManagementSystem.Helpers;
using OrderManagementSystem.Middlewares;
using OrderManagementSystem.Setting;
using System;

namespace OrderManagementSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<OrderMangementSystemContext>(Options=>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddScoped<Helpers.IMailService, EmailServices>();
            builder.Services.AddIdentityServices(builder.Configuration);
            var app = builder.Build();
            #region
            using var Scope = app.Services.CreateScope();
            var Services = Scope.ServiceProvider;
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var DbContext = Services.GetRequiredService<OrderMangementSystemContext>();
                await DbContext.Database.MigrateAsync();

                var IdentityDbContext =Services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync();

                var roleManager = Services.GetRequiredService<RoleManager<IdentityRole>>();
                await AppIdentityDbContextSeed.SeedRolesAsync(roleManager);

                var UserManager = Services.GetRequiredService<UserManager<User>>();
                await AppIdentityDbContextSeed.SeedUserAsync(UserManager);
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Occured During Applying The Migration");
            }
            #endregion
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleware>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStatusCodePagesWithReExecute("/error/{0}");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
          

            app.MapControllers();

          

            app.Run();
        }
    }
}
