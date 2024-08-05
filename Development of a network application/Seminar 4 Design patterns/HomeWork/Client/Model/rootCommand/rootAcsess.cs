using Client.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.rootCommand
{
    class rootAcsess : ICommander
    {
        public Message Commander(Message msg)
        {
            string? com = null;
            do
            {
                Console.WriteLine("Cписок комманд: ");
                Console.WriteLine("1 - Зарегистрировать пользователя; ");
                Console.WriteLine("2 - Удалить пользователя и закрыть приложение; ");
                com = Console.ReadLine();

                switch (com)
                {
                    case "1": msg.commands = Commands.Register; break;
                    case "2": msg.commands = Commands.Delete; break;
                    default: com = null; break;
                }
            } while (string.IsNullOrEmpty(com));
            return msg;
        }
    }
}
