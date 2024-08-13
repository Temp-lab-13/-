using WATask3.Models.Roles;

namespace WATask3.Models.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserRole RoleId { get; set; }
        public virtual Role Role { get; set; }

    }
}
