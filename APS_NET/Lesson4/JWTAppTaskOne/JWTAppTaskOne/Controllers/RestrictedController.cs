using JWTAppTaskOne.AuthorizationModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JWTAppTaskOne.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RestrictedController : ControllerBase
    {
        [HttpGet]
        [Route("Admins")]
        [Authorize(Roles = "Administration")]
        public IActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi, {currentUser.Role}");
        }

        [HttpGet]
        [Route("Users")]
        [Authorize(Roles = "Administration, User")]
        public IActionResult UserEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi, {currentUser.Role}");
        }


        private UserModel GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity is not null)
            {
                var uderClaims = identity.Claims;
                return new UserModel
                {
                    Login = uderClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = (UserRole)Enum.Parse(typeof(UserRole), uderClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value)
                };
            }
            return null;
        }

    }
}
