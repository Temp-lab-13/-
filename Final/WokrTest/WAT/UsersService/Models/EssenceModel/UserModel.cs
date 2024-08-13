using UsersService.Models.RolesModel;

namespace UsersService.Models.EssenceModel
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string? UserEmail { get; set; }
        public string? Password { get; set; }
        public UserRole Role { get; set; }
    }
}
