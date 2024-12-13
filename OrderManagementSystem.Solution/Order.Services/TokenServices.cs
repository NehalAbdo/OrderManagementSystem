using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Order.Core.Entities;
using Order.Core.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Order.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;

        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(User user, UserManager<User> userManager)
        {
            var AuthClaims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName , user.UserName),
                new Claim(ClaimTypes.Email , user.Email)
            };
            var UserRoles = await userManager.GetRolesAsync(user);
            foreach (var Role in UserRoles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, Role));
            }
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                claims: AuthClaims,
                signingCredentials : new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
