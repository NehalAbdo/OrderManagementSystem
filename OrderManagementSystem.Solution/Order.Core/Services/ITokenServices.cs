using Microsoft.AspNetCore.Identity;
using Order.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Core.Services
{
    public interface ITokenServices
    {
       Task <string> CreateTokenAsync(User user,UserManager<User> userManager);

    }
}
