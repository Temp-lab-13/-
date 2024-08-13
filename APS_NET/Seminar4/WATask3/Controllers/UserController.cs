using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WATask3.Models.Model;
using WATask3.Services.Abstract;

namespace WATask3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(string login, string password)
        {
            var token = _userService.UserCheck(login, password);
            if (!token.IsNullOrEmpty())
            {
                return Ok(token);
            }
            return NotFound("User not found");
        }

    }
}
