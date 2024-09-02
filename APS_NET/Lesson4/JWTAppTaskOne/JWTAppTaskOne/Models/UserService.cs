using JWTAppTaskOne.Abstract;
using JWTAppTaskOne.Context;
using JWTAppTaskOne.Context.Dto;
using System.Security.Cryptography;
using System.Text;

namespace JWTAppTaskOne.Models
{
    public class UserService : IUserService
    {
        private readonly AppDbContext context;

        public UserService(AppDbContext context)
        {
            this.context = context;
        }

        public void AddUser(string username, string password, RoleId role)
        {
            
            using(context)
            {
                if (role == RoleId.Administration)
                {
                    var c = context.Users.Count(x => x.RoleId == RoleId.Administration);
                    if (c > 0)
                    {
                        throw new Exception("Создание администраторов не возможно.");
                    }
                }

                var user = new User()
                {
                    Login = username,
                    RoleId = role,
                    Salt = new byte[16]
                };

                new Random().NextBytes(user.Salt);
                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray();
                SHA512 shaM = new SHA512Managed();
                user.Password = shaM.ComputeHash(data);
                context.Add(user);
                context.SaveChanges();
            }
        }

        public RoleId UserCheck(string username, string password)
        {
            using (context)
            {
                var user = context.Users.FirstOrDefault(x => x.Login.Equals(username));
                if (user is null)
                {
                    throw new Exception($"{username} is not registered.");
                }

                var data = Encoding.ASCII.GetBytes(password).Concat(user.Salt).ToArray();
                SHA512 shaM = new SHA512Managed();
                var bpassword = shaM.ComputeHash(data);

                if (user.Password.SequenceEqual(bpassword))
                {
                    return user.RoleId;
                }
                else
                {
                    throw new Exception("Wrong password");
                }
            }
        }
    }
}
