using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Order.Core.Entities;
using Order.Core.Services;
using OrderManagementSystem.DTO;
using OrderManagementSystem.Errors;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenServices _tokenServices;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager,ITokenServices tokenServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenServices;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,

            };
            var Result= await _userManager.CreateAsync(user,model.Password);
            if (!Result.Succeeded) return BadRequest(new APIResponse(400));
            if (!string.IsNullOrEmpty(model.Role))
            {
                var roleExists = await _userManager.GetRolesAsync(user);
                if (!roleExists.Contains(model.Role))
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                }
            }
            var returnedUser = new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenServices.CreateTokenAsync(user,_userManager)
            };
            return Ok(returnedUser);
        }


        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login (LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return BadRequest(new APIResponse(401));
            var Result =  await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!Result.Succeeded) return Unauthorized(new APIResponse(401));
          
            return Ok(new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenServices.CreateTokenAsync(user, _userManager)
            });

        }
    }
}
