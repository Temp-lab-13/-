namespace JWTAppTaskOne.AuthorizationModel
{
    public class UserModel
    {
        public string Login {  get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
