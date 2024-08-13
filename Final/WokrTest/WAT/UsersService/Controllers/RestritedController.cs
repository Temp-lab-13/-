using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using UsersService.Abstract;
using UsersService.Models.EssenceModel;
using UsersService.Models.RolesModel;

namespace UsersService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RestritedController : ControllerBase // Сдесь будут методы для раных ролей.
    {
        private readonly IConfiguration _configuration;
        private readonly IMethods methods;

        public RestritedController(IConfiguration configuration, IMethods methods)
        {
            _configuration = configuration;
            this.methods = methods;
        }

        [HttpGet]
        [Route("Send")]
        [Authorize(Roles = "Administrator, User")]
        public IActionResult Send(string adress, string topic, string text)  // Функционал пользователй.
        {
            var res = methods.sendMessedg(adress, topic, text);
            if (res)
            {
                return Ok("Сообщение отправлено");
            } return NotFound("Адресат не найден");
        }

        [HttpGet]
        [Route("Read")]
        [Authorize(Roles = "Administrator, User")]
        public IActionResult sendMessedg(UserModel user)  // Функционал пользователй.
        {
            var res = methods.sendMessedg(user);
            if (res is not null)
            {
                return Ok(res);
            }
            return NotFound("Адресат не найден");
        }

        private UserModel GetUser() // Метод получения юзера. Получает тллько самого себя пока. Настроит на получение всего списка.
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModel
                {
                    UserEmail = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                    Role = (UserRole)Enum.Parse(typeof(UserRole), userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value)
                };
            }
            return null;
        }

        /*[HttpGet]
        [Route("Admin")]
        [Authorize(Roles = "Administrator")]
        public IActionResult AdminEndPoint() // Администраторский функционал
        {
            var user = GetUser();
            return Ok($"Дпобро пожаловать {user.Role}");
        }*/

        /*[HttpGet]
        [Route("Users")]
        [Authorize(Roles = "Administrator, User")]
        public IActionResult UserEndPoint()  // Функционал пользователй.
        {
            var user = GetUser();
            return Ok($"Дпобро пожаловать {user.Role}");
        }*/
    }
}
