using UsersService.Models.EssenceModel;
using UsersService.Models.InputModel;

namespace UsersService.OldCode
{
    public interface IUserAuthenticationService
    {
        UserModel Authenticate(LoginModel login);
    }
}
