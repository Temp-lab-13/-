using UsersService.Abstract;
using UsersService.Models;
using UsersService.Models.Context;
using UsersService.Models.EssenceModel;
using UsersService.Models.RolesModel;
using System.Security.Cryptography;
using System.Text;

namespace UsersService.Services
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDBContext context;
        public UserRepo(AppDBContext dbContext) 
        {
            context = dbContext;
        }
        public void UserAdd(string name, string password, RoleId role)
        {
            using (context)
            {
                if (role == RoleId.Administrator)
                {
                    var temp = context.Users.Count(x => x.RoleId == RoleId.Administrator);

                    if (temp > 0)
                    {
                        throw new Exception("Лимит админимстраторов");
                    }
                }

                var user = new User()
                {
                    Email = name,
                    RoleId = role,
                    Salt = new byte[16]
                };
                new Random().NextBytes(user.Salt);
                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray();

                SHA512 ahaM = new SHA512Managed();
                user.Password = ahaM.ComputeHash(data);
                context.Add(user);
                context.SaveChanges();
            }
        }

        public RoleId UserCheck(string name, string password)
        {
            using (context)
            {
                var user = context.Users.FirstOrDefault(x => x.Email == name);

                if(user == null)
                {
                    throw new Exception("User is not found");
                }
                var data = Encoding.ASCII.GetBytes(password).Concat(user?.Salt).ToArray(); 
                SHA512 sHA = new SHA512Managed();
                var bpassword = sHA.ComputeHash(data);

                if (user.Password.SequenceEqual(bpassword)) { return user.RoleId; }
                else { throw new Exception("Wrong password"); }
            }
        }
    }
}
