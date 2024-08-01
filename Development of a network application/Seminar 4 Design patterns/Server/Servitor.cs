using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_4_Design_patterns.Server.obj
{
    internal class Servitor // Класс отвечает за обработку команд юзера.
    {
        private UPDServer _server;
        public Servitor(UPDServer server) => _server = server;
        

        public Тewsletter Execute(Message message, IPEndPoint iPEndPoint) //Получаем месседж, если в нём есть команда на регистрацию/удаления, выполняем. Нет, скипаем и шлём сообщение.
        {
            switch (message.commands)
            {
                case Commands.Delete: Delet(message.NickNameFrom); break;
                case Commands.Register: Register(message.NickNameFrom, iPEndPoint); break;
                case Commands.Exit: Exit(); break;
                default: return Send(message);

            }
            return Тewsletter.Default; // Заглушка-возращался для класса. Ничего не делает.
        }

        public Тewsletter Send(Message msg) // Определяем кому слать сообщение. Всем или конкретному пользователю.
        {
            if (string.IsNullOrEmpty(msg.NickNameTo))
            {
                return Тewsletter.ToAll;
            } else
            {
                return Тewsletter.ToOne;
            }
        }
        public void Register(string user, IPEndPoint iPEndPoint) // Метод регистрации пользователя на сервере. 
        {
            if (_server.Users == null)
                _server.Users = new Dictionary<string, IPEndPoint>();
            _server.Users.Add(user, iPEndPoint);
            Console.WriteLine("Пользователь зарегистрирован.");
        }

        public void Delet(string user) // Метод удаления пользователя с сервера.
        {
            _server.Users.Remove(user);
            Console.WriteLine("Пользователь удалён.");
        }

        public void Exit()
        {

        }
    }
}
