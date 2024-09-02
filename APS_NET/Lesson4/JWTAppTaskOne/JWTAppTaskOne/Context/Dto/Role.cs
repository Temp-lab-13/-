namespace JWTAppTaskOne.Context.Dto
{
    public partial class Role
    {
        public RoleId RoleId { get; set; }
        public string Login { get; set; }
        public virtual List<User> Users { get; set; }
    }
}
