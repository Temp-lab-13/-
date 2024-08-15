//using HomeWork.Abstarcts;
//using HomeWork.Model;
using ChatCommon.Abstarcts;
using ChatCommon.Models;
using NetMQ.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace UDPClient //HomeWork.Service
{
    public class Client<T>
    {
        private readonly string _name;
        private readonly string address;
        private readonly string port;
        private readonly IMessageSourseClient<T> _messageSourse;
        //private IPEndPoint endPoint;
        //private UdpClient udpClient;
        private T Socket;

        public Client(string name, string address, string port, IMessageSourseClient<T> messageSourse)
        {
            this._name = name;
            this.address = address;
            this.port = port;
            _messageSourse = messageSourse;
            //endPoint = new IPEndPoint(IPAddress.Parse(address), port);
            //udpClient = new UdpClient(54321);

            Socket = _messageSourse.CreatSocker(address, port);

        }


        public async Task ClientListener()
        {
            while (true)
            {
                try
                {
                    var messageReceived = _messageSourse.ReceiveMQ(Socket);

                    Console.WriteLine($"Получено сообщение от {messageReceived.NickNameFrom}: ");
                    Console.WriteLine(messageReceived.Text);

                    await Confirm(messageReceived);
                }
                catch (Exception ex) 
                {
                    Console.WriteLine($"Ошибка при получении сообщения: {ex.Message}");
                }
            }
        }

        async Task Confirm(NetMessage messageReceived)
        {
            messageReceived.Command = Command.Confirmation;
            await _messageSourse.SendAsyncMQ(messageReceived, Socket); // Метод отправки.
        }

        async Task Register()
        {
            var message = new NetMessage()
            {
                NickNameFrom = _name,
                NickNameTo = null,
                Text = null,
                Command = Command.Register,
                DateSend = DateTime.Now,
            };

            await _messageSourse.SendAsyncMQ(message, Socket);
        }
        public async Task ClientSender()
        {
            await Register();

            while (true)
            {
                try
                {
                    Thread.Sleep(500); // Костыль для теста полученных ответов.
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


                    await _messageSourse.SendAsyncMQ(message, Socket);

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
            //Socket.Connect($"tcp://{address}:{port}");
            //ClientSender();
            //ClientListener();
            new Thread(() => ClientSender()).Start();
            new Thread(() => ClientListener()).Start(); 
        }
        
    }


}
