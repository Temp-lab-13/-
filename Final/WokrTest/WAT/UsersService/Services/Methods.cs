using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using UsersService.Abstract;
using UsersService.Models.Context;
using UsersService.Models.EssenceModel;
using UsersService.Models.RolesModel;

namespace UsersService.Services
{
    public class Methods : IMethods
    {
        private readonly AppDBContext context;

        public Methods(AppDBContext context)
        {
            this.context = context;
        }

        public bool sendMessedg(string adress, string topic, string text)
        {
            using (context)
            {
                var user = context.Users.FirstOrDefault(x => x.Email.Equals(adress)); // получаем, есть ли такой перс в системе вообще
                if (user is not null) // если да формируем сообщение
                {
                    var msg = new Message()
                    {
                        Topic = topic, // заголовок
                        Text = text, // само сообщение
                        ClientId = user.Id // кому.
                    };
                    context.Add(msg);
                    context.SaveChanges();
                    return true;
                };
                return false; // напишем, что такого персонажа нет. реализуем в контроллере ошибкой. если успею
            }
        }

        public IEnumerable<Message> sendMessedg(UserModel user)
        {
            using (context)
            {
                var message = context.Messages.Where(x => x.ClientId == user.Id).ToList();
                return message;
            }
        }
    }
}
