using WATask1.DTO;

namespace WATask1.Repositori
{
    public interface IUserRepo
    {
        public void AddUser(UserDto user);
        public bool Exist(string email);
    }
}
