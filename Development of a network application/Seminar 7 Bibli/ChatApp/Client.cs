using ChatCommon;
using ChatCommon.Abstarcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public class Client<T>
    {
        private readonly string _name;
   
        private readonly IMessageSourseClient<T> _messageSourse;
        private T endPoint;

        public Client(string name, IMessageSourseClient<T> messageSourse)
        {
            this._name = name;
            _messageSourse = messageSourse;
            endPoint = _messageSourse.CreateEndpoit();
        }

        UdpClient udpClient = new UdpClient();

        async Task ClientListener()
        {
            while (true)
            {
                try
                {
                    var messageReceived = _messageSourse.Receive(ref endPoint);

                    Console.WriteLine($"Получено сообщение от {messageReceived.NickNameFrom}: ");
                    Console.WriteLine(messageReceived.Text);

                    await Confirm(messageReceived, endPoint);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine($"Ошибка при получении сообщения: {ex.Message}");
                }
            }
        }

        async Task Confirm(NetMessage messageReceived, T endPoint)
        {
            messageReceived.Command = Command.Confirmation;
            await _messageSourse.SendAsync(messageReceived, endPoint);
        }

        async Task Register(T iPEndPoint)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            var message = new NetMessage()
            {
                NickNameFrom = _name,
                NickNameTo = null,
                Text = null,
                Command = Command.Register,
                NickAddress = ep
            };
            await _messageSourse.SendAsync(message, iPEndPoint);
        }
        async Task ClientSender()
        {
            Register(endPoint);

            while (true)
            {
                try
                {
                    Console.Write("Введите имя получателя: ");
                    var nameTo = Console.ReadLine();
                    Console.WriteLine("Введите сообщение: ");
                    var text = Console.ReadLine();

                    var message = new NetMessage()
                    {
                        NickNameFrom = _name,
                        NickNameTo = nameTo,
                        Text = text,
                        Command = Command.Message
                    };

                    await _messageSourse.SendAsync(message, endPoint);

                    Console.WriteLine("Сообщение отправлено.");
                }
                catch (Exception ex) 
                {
                    Console.WriteLine($"Ошибка при обработке сообщения: {ex.Message}");
                }
            }

        }
        public async Task StartClient()
        {
            new Thread(async () => await ClientListener()).Start();

            await ClientSender();
        }
    }


}
