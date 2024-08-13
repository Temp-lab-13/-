using WATask3.Models.Model;

namespace WATask3.Models.Roles
{
    public partial class Role
    {
        public UserRole RoleId {  get; set; }
        public string Name { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
