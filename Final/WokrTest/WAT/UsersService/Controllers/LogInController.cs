using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UsersService.Abstract;
using UsersService.Models.EssenceModel;
using UsersService.Models.InputModel;
using UsersService.Models.RolesModel;

namespace UsersService.Controllers
{
    public static class RSATools // Вывести в отделный класс
    {
        public static RSA GetPrivateKey()
        {
            var file = File.ReadAllText("RSA/private_key.pem");
            var rsa = RSA.Create();
            rsa.ImportFromPem(file);
            return rsa;
        }
    }
    [Route("[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepo _userRepo;
        private UserModel user;

        public LogInController(IConfiguration configuration, IUserRepo userRepo) 
        {
            _configuration = configuration;
            _userRepo = userRepo;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([FromBody] LoginModel login) // Метод логина. Надо сделать аворизацию атоматической, что бы после введения пароля с почтой, токен автоматически применялся. 
        {
            try
            {
                var roleId = _userRepo.UserCheck(login.Email, login.Password);
                user = new UserModel
                {
                    UserEmail = login.Email,
                    Password = login.Password,
                    Role = RoleIDToRole(roleId)
                };
                var token = GenerateToken(user);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous] //админ у нас будет один. первый зарегеный юзер. Он и правит сервесом. Позже дам ему возможность простых юзереов делать админами. Сделать одного админа изначально?
        [HttpPost]
        [Route("AddAdmin")]
        public ActionResult AddAdmin([FromBody] LoginModel login)
        {
            try
            {
                _userRepo.UserAdd(login.Email, login.Password, RoleId.Administrator);
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
            return Ok();
        }

        [AllowAnonymous] // По идеи, каждый может зарегится как простой юзер
        [HttpPost]
        [Route("AddUser")]
        public ActionResult AddUser([FromBody] LoginModel login)
        {
            try
            {
                _userRepo.UserAdd(login.Email, login.Password, RoleId.User);
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
            return Ok();
        }


        private UserRole RoleIDToRole(RoleId id) // Переходник между enum. Вынести в отдельный класс. 
        {
            if (id == RoleId.Administrator) return UserRole.Administrator;
            return UserRole.User;
        }

        private string GenerateToken(UserModel user) // Генератор токена. Вынести в отдельный класс.
        {

            var securityKey = new RsaSecurityKey(RSATools.GetPrivateKey());

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.RsaSha256Signature);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Password),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(6000),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
