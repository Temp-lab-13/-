using JWTAppTaskOne.Context.Dto;

namespace JWTAppTaskOne.Abstract
{
    public interface IUserService
    {
        void AddUser(string username, string password, RoleId role);
        RoleId UserCheck(string username, string password);
    }
}
