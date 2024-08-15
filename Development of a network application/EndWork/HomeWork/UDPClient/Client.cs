using HomeWork.Abstarcts;
using HomeWork.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace HomeWork.Service
{
    public class Client
    {
        private readonly string _name;
        private readonly string address;
        private readonly int port;
        private readonly IMessageSourse _messageSourse;
        private IPEndPoint endPoint;
        private UdpClient udpClient;

        public Client(string name, string address, int port)
        {
            this._name = name;
            this.address = address;
            this.port = port;
            _messageSourse = new MessageSource();
            endPoint = new IPEndPoint(IPAddress.Parse(address), port);
            udpClient = new UdpClient(54321);
        }

         

        public void ClientListener()
        {
            while (true)
            {
                try
                {
                    var messageReceived = _messageSourse.Receive(ref endPoint, ref udpClient);

                    Console.WriteLine($"Получено сообщение от {messageReceived.NickNameFrom}: ");
                    Console.WriteLine(messageReceived.Text);

                    Confirm(messageReceived, endPoint);
                }
                catch (Exception ex) 
                {
                    //Console.WriteLine($"Ошибка при получении сообщения: {ex.Message}");
                    // Как и в сервере, не нужная вещь ,если мы не хотим утонуть в сообщениях об ошибках.
                }
            }
        }

        async Task Confirm(NetMessage messageReceived, IPEndPoint endPoint)
        {
            messageReceived.Command = Command.Confirmation;
            await _messageSourse.SendAsync(messageReceived, endPoint, udpClient);
        }

        async Task Register()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            var message = new NetMessage()
            {
                NickNameFrom = _name,
                NickNameTo = null,
                Text = null,
                Command = Command.Register,
                //NickAddress = ep
            };

            await _messageSourse.SendAsync(message, endPoint, udpClient);
        }
        public async Task ClientSender()
        {
            await Register();

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
                        DateSend = DateTime.Now,
                        Command = Command.Message
                    };


                    await _messageSourse.SendAsync(message, endPoint, udpClient);

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
            //ClientSender();
            //ClientListener();
            new Thread(() => ClientSender()).Start();
            new Thread(() => ClientListener()).Start(); 
            

        }
        
    }


}
