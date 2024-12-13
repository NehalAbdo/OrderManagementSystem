using Microsoft.AspNetCore.Identity;
using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(new IdentityRole("Customer"));
            }
        }

        public static async Task SeedUserAsync(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User()
                {
                    Email = "NehalAbdelrahman7@gmail.com",
                    UserName = "NehalAbdelrahman",
                    PhoneNumber = "01277952816"
                };
                await userManager.CreateAsync(user, "Order@22");
            }
        }
    }
}
