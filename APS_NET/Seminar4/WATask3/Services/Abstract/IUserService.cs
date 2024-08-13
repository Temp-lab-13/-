using WATask3.Models.Model;
using WATask3.Models.Roles;

namespace WATask3.Services.Abstract
{
    public interface IUserService
    {
        public void UserAdd(string name, string password, UserRole userRole);
        public string UserCheck(string name, string password);
    }
}
