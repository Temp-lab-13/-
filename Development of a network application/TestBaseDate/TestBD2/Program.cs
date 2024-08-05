using Microsoft.EntityFrameworkCore;
using TestBD2.Model;

namespace TestBD2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestDbContext>().UseNpgsql("Host=localhost;Username=postgres;Password=lotta;Database=Test").UseLazyLoadingProxies();

            using (var ctx = new TestDbContext(optionsBuilder.Options))
            {
                // Обращаемся к базе данных и выводим всех юзеров и их сообщения.
                /*
                var users = ctx.Users.ToList();

                foreach (var user in users)
                {
                    Console.WriteLine($"Имя: {user.Name}");

                    var messages = user.Messages;
                    foreach (var message in messages)
                    {
                        Console.WriteLine($"   - {message.MessageContent}");
                    }
                }
                */

                // Добавляем нового юзера и его новые сообщения. Работает! В базе данных новый пользователь появился. Проверено в админере.
                /*
                var user = new User();
                user.Name = "Кли";
                user.Messages = new HashSet<Message>();
                user.Messages.Add(new Message { MessageContent = "Та-та-Та!" });
                user.Messages.Add(new Message { MessageContent = "Кли, здесь!!" });

                ctx.Add(user);

                int changes = ctx.SaveChanges();

                Console.WriteLine($"Было сделано записей {changes} записей");

                var users = ctx.Users.ToList();

                foreach (var userItem in users)
                {
                    Console.WriteLine($"Имя: {userItem.Name}");

                    var messages = userItem.Messages;
                    foreach (var message in messages)
                    {
                        Console.WriteLine($"   - {message.MessageContent}");
                    }
                }
                */

                // Здесь мы изменяем уже имеющиеся данные. Меняем в базе данных имя Анна на - Хозяйка, и стерев предыдущие сообщения добавляем новые.
                var user = ctx.Users.FirstOrDefault(x => x.Name == "Анна");

                if (user != null) 
                {
                    user.Name = "Хозяйка";
                    user.Messages.Clear();
                    user.Messages.Add(new Message {MessageContent = "Дорого времени суток, Дамы. Зовите меня 'Хозяйка' - не ошибётесь." });
                    ctx.SaveChanges();
                }

                var users = ctx.Users.ToList();

                foreach (var userItem in users)
                {
                    Console.WriteLine($"Имя: {userItem.Name}");

                    var messages = userItem.Messages;
                    foreach (var message in messages)
                    {
                        Console.WriteLine($"   - {message.MessageContent}");
                    }
                }



            }
        }
    }
}
