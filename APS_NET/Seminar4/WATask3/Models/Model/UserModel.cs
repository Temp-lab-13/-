using WATask3.Models.Roles;

namespace WATask3.Models.Model
{
    public class UserModel
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserRole Role { get; set; }
    }
}
