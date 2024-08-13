using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsersService.Models.EssenceModel;
using UsersService.Models.InputModel;
using UsersService.Models.RolesModel;

namespace UsersService.OldCode
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        public UserModel Authenticate(LoginModel login)
        {
            if (login.Email == "admin" && login.Password == "password")
            {
                return new UserModel
                {
                    Password = login.Password,
                    UserEmail = login.Email,
                    Role = UserRole.Administrator
                };
            }
            if (login.Email == "user" && login.Password == "password")
            {
                return new UserModel
                {
                    Password = login.Password,
                    UserEmail = login.Email,
                    Role = UserRole.User
                };
            }
            return null;
        }


    }
}
