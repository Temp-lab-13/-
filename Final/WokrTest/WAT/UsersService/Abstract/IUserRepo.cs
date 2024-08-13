using UsersService.Models.RolesModel;

namespace UsersService.Abstract
{
    public interface IUserRepo
    {
        public void UserAdd(string username, string password, RoleId role);
        public RoleId UserCheck(string username, string password);

        //public void SendMessage(string topick string message, );
    }
}
