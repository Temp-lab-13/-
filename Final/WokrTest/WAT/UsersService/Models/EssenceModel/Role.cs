using UsersService.Models.RolesModel;

namespace UsersService.Models.EssenceModel
{
    public partial class Role
    {
        public RoleId RoleId { get; set; }
        public string? Email { get; set; }
        public virtual List<User> Users { get; set; }  
    }
}
