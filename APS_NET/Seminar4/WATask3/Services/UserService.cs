using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WATask3.Models.Context;
using WATask3.Models.Model;
using WATask3.Models.Roles;
using WATask3.Services.Abstract;

namespace WATask3.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbConext _appDbConext;
        private readonly IConfiguration _configuration;

        public UserService(AppDbConext appDbConext)
        {
            _appDbConext = appDbConext;
        }

        public void UserAdd(string name, string password, UserRole userRole)
        {
            var user = new List<User>();
            using (_appDbConext)
            {
                var userExist = _appDbConext.Users.Where(x => !x.Name.ToLower().Equals(name.ToLower())).ToList();
                User newUser = null;
                if (userExist != null)
                {
                    // ипо ошибка. сказать что такой перс уже есть
                }
                else
                {
                    newUser = new User()
                    {
                        Id =1, // присвоить новый id
                        Name = name,
                        Password = password,
                        RoleId = userRole
                    };
                }
            }
        }

        public string UserCheck(string name, string password)
        {
            using (_appDbConext)
            {
                var buf = (_appDbConext.Users.FirstOrDefault(x =>
                x.Name.ToLower().Equals(name.ToLower()) &&
                x.Password.Equals(password)));

                if (buf == null) return "";

                var user = new UserModel
                {
                    UserName = buf.Name,
                    Password = buf.Password,
                    Role = buf.RoleId
                };
                return GenerateToken(user);
            }
        }

        private string GenerateToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwc:Key"]));
            var credentials =  new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt: Audience"], claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
