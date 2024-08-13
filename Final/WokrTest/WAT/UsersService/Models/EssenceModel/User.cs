using UsersService.Models.RolesModel;

namespace UsersService.Models.EssenceModel
{
    public partial class User
    {
        public Guid Id { get; set; }
        public string? Email { get; set; } = null!;
        public string? Text { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public RoleId RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    }
}
