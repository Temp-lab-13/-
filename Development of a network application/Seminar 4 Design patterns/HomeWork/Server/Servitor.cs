using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Servitor
    {
        private UDPServer _server;
       

        public Servitor(UDPServer server) => _server = server;
  

        public Тewsletter Execute(Message message, IPEndPoint iPEndPoint, CancellationToken token) //Получаем месседж, если в нём есть команда на регистрацию/удаления, выполняем. Нет, скипаем и шлём сообщение.
        {
            switch (message.commands)
            {
                case Commands.Delete: Delet(message.NickNameFrom, message.Text); break;
                case Commands.Register: Register(message.NickNameFrom, iPEndPoint); break;
                case Commands.Exit: Сlose(token); break;
            }
            
            return Send(message); // Заглушка-возращался для класса. Ничего не делает.
        }

        public Тewsletter Send(Message msg) // Определяем кому слать сообщение. Всем или конкретному пользователю.
        {
            if (string.IsNullOrEmpty(msg.NickNameTo))
            {
                return Тewsletter.ToAll;
            }
            else
            {
                return Тewsletter.ToOne;
            }
        }
        public void Register(string user, IPEndPoint iPEndPoint) // Метод регистрации пользователя на сервере. 
        {
            if (!_server.Users.ContainsKey(user))
            {
                _server.Users.Add(user, iPEndPoint);
                Console.WriteLine($"Пользователь {user} зарегистрирован.");
            }    
        }

        public void Delet(string user, string msg) // Метод удаления пользователя с сервера.
        {
            if (_server.Users.ContainsKey(user))
            {
                _server.Users.Remove(user);
                Console.WriteLine($"Пользователь {user} удалён.");
            }
            else 
            {
                Console.WriteLine($"Пользователь {user} не найден.");
            } 
        }

        public void Сlose(CancellationToken token)
        {
            Console.WriteLine("Нажмите любую клавишу, что бы завершить работу сервера."); // Ожидаем ввода со стороны сервера, что бы завершить работу.
            Console.ReadKey();
            _server.End(token);
        }
    }
}
