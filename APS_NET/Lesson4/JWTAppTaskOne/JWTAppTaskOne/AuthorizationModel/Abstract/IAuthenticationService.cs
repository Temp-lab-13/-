namespace JWTAppTaskOne.AuthorizationModel.Abstract
{
    public interface IAuthenticationService
    {
        UserModel Authenticate(LoginModel model);
    }
}
