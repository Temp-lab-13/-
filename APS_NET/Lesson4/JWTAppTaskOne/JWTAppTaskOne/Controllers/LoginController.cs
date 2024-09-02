using JWTAppTaskOne.Abstract;
using JWTAppTaskOne.AuthorizationModel;
using JWTAppTaskOne.AuthorizationModel.Abstract;
using JWTAppTaskOne.Context.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWTAppTaskOne.Controllers
{
    public static class RsaTool
    {
        public static RSA GetPrivateKey()   //  Читаем файл с public rsa токеном, генерем rsa и возращаем.
        {
            var readFile = File.ReadAllText("RSA/private_key.pem");
            var rsa = RSA.Create();
            rsa.ImportFromPem(readFile);
            return rsa;
        }
    }


    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IAuthenticationService service;
        private readonly IUserService userService;

        public LoginController(IConfiguration configuration, IAuthenticationService service, IUserService userService)
        {
            this.configuration = configuration;
            this.service = service;
            this.userService = userService;
        }

        
        [AllowAnonymous]
        [HttpPost]
        [Route("AddAdmin")]
        public ActionResult AddAdmin([FromBody] LoginModel model) 
        {
            try
            {
                userService.AddUser(model.Login, model.Password, RoleId.Administration);
                return Ok();
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AddUser")]
        public ActionResult AddUser([FromBody] LoginModel model)
        {
            try
            {
                userService.AddUser(model.Login, model.Password, RoleId.User);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] LoginModel model)
        {
            
            try
            {
                var roleId = userService.UserCheck(model.Login, model.Password);
                var user = new UserModel { Login =  model.Login, Password = model.Password, Role = RoleToRoleId(roleId)};
                var tocen = GenerateToken(user);
                return Ok(tocen);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        private static UserRole RoleToRoleId(RoleId id)
        {
            if (id == RoleId.Administration) return UserRole.Administration;
            else
                return UserRole.User;
        }



        /*
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginNotBase([FromBody] LoginModel model)
        {
            var user = service.Authenticate(model);
            if (user is not null)
            {
                var tocen = GenerateToken(user);
                return Ok(tocen);
            }
            return NotFound("Такого пользователя нет.");
        }*/

        private string GenerateToken(UserModel model) 
        {
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            //var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Этот алгоритм был предназначен для нашего ключа из конфигурационного файла.
            var key = new RsaSecurityKey(RsaTool.GetPrivateKey());
            var credentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);   //  А этот предназначен уже для работы с токеном из файла.
            var claim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.Login),
                new Claim(ClaimTypes.Role, model.Role.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claim,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
