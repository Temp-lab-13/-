using Microsoft.EntityFrameworkCore;
using TestBD3.Model;

namespace TestBD3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Добавляем юзера с сообщением и указанием Гендера.
            /*
            using (var ctx = new TestContext())
            {
                var userOne = new User()
                {
                    Name = "Нахида",
                    GenderId = GenderId.Female,
                    Messages = new HashSet<Message>()
                };
                userOne.Messages.Add(new Message { MessageContent = "Приятных снов!" });

                ctx.Users.Add(userOne);
                ctx.SaveChanges();
            }
            */
             // Транзакции. Суть в том, что транзакция выполняется, только если выполнился весь код.
             // если на пол пути возникла ошибка, то выполненная часть не может быть исполнена, даже если код это позволяет.
            /*
            using (var ctx = new TestContext())
            {
                using (var tra = ctx.Database.BeginTransaction()) // токен транзакции.
                {
                    var userTwo = new User()
                    {
                        Name = "Лиза",
                        GenderId = GenderId.Female,
                        Messages = new HashSet<Message>()
                    };
                    userTwo.Messages.Add(new Message { MessageContent = "Привет милашки~" });

                    ctx.Users.Add(userTwo);
                    ctx.SaveChanges(); // запись в базу данных нового пользователя с его сообщениями.
                                       // однако, в базе новой записи не появится, поскольку транзакция ещё не закончена.

                    Console.WriteLine($"Id = {userTwo.Id}");

                    if (userTwo.Id % 2 == 0)
                    {
                        userTwo.Messages.Add(new Message { MessageContent = "У меня счастливый номер!" });
                    }
                    else
                    {
                        userTwo.Messages.Add(new Message { MessageContent = "Сегодня приятный денёк~" });
                    }
                    ctx.SaveChanges();
                    tra.Commit(); // Здесь происходит подтверждение завершения транзакции. Только после этой части всё выполненное раньше исполнится.
                }
                    
            }*/

            // Перебираем юзеров и выводим их имена, гендор и все отправленные ими сообщения.
            using (var ctx = new TestContext())
            {
                var users = ctx.Users.ToList();
                foreach (var userItem in users)
                {
                    Console.WriteLine($"Имя: {userItem.Name}, пол {userItem.GenderId}");

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
