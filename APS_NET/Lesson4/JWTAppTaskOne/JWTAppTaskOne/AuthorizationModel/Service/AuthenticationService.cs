using JWTAppTaskOne.AuthorizationModel.Abstract;

namespace JWTAppTaskOne.AuthorizationModel.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        public UserModel Authenticate(LoginModel model)
        {
            if (model.Login.Equals("admin") && model.Password.Equals("password")) 
            {
                return new UserModel {Password = model.Password, Login = model.Login, Role = UserRole.Administration};
            }
            if (model.Login.Equals("user") && model.Password.Equals("pass"))
            {
                return new UserModel { Password = model.Password, Login = model.Login, Role = UserRole.User };
            }
            return null;
        }
    }
}
