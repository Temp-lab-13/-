using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Client.Model.Interfaces;
using Client.Model.rootCommand;

namespace Client.Model.Singleton
{
    // Напишем Singleton для клиента. Чисто для практики.   
    public class SingUser
    {
        private static readonly Lazy<SingUser> _Li = new Lazy<SingUser>(() => new SingUser());

        private CancellationTokenSource _cts;
        private CancellationToken __ct;
        private UdpClient udpClient;


        private SingUser() 
        { 
            udpClient = new UdpClient(); // udp-Клиент
            _cts = new CancellationTokenSource(); // Получаем токены.
            __ct = _cts.Token;
        }

        public static SingUser Instance => _Li.Value;

        public void StartClietn(string From, string ip, Commands command) 
        {
                                          
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);  

            while (!__ct.IsCancellationRequested) 
            {
                new Thread(() => { 
                    try  
                    {
                        Listent(udpClient, ref iPEndPoint);
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex);
                    }
                }).Start();

                var msg = EnterInConsol(From, command); // Метод ввода сообщения через консоль.

                if (msg.Text == "#") // типо возможность войти под "root"-пользователя и использовать системные команды. Очень условно. 
                {
                    rootAcsess rootAcsess = new rootAcsess();
                    msg = rootAcsess.Commander(msg);
                } else if (msg.Text.ToLower() == "exit") // простой способ завершить работу клиента. 
                {
                    try
                    {
                        End(__ct);
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine("Работа завершена.");
                        Console.ReadKey();
                        msg.commands = Commands.Delete; // удаляем клиента из списка активных пользователей.
                        msg.NickNameTo = "";
                        msg.Text = $"Клиент {msg.NickNameFrom} завершил сессию."; // это сообщение по идеи, должно прилететь всем.
                    }
                    
                }

                string json = msg.SerializeMassageToJason(); // Сериализуем.
                byte[] date = Encoding.UTF8.GetBytes(json); // Кодируем в массив байт.
                int cou = udpClient.Send(date, date.Length, iPEndPoint); //Отправляем, и получем колличество байт, которые удалось переслать.
                // Цепочка ответсвенная за отправку сообщения клиенту о доставке его сообщения пока удалена. 
            }
        }
   

        public void Listent(UdpClient udpClient, ref IPEndPoint iPEndPoint) // Слушаем входящие сообщения.
        {
            byte[] buffer = udpClient.Receive(ref iPEndPoint); // Получаем сообщения.
            var messageText = Encoding.UTF8.GetString(buffer); // Декодируем из байт в стрингу
            Message.DeserializeFromJson(messageText).Print(); // Десериализуем в класс и возращаем результат из метода.
        }

        public Message EnterInConsol(string From, Commands command)
        {
            string message;
            string nikeTo = string.Empty;
            do
            {
                Thread.Sleep(500);
                Console.Write("Кому: ");
                nikeTo = Console.ReadLine();
                Console.Write("Введите сообщение: ");
                message = Console.ReadLine();
            } while (string.IsNullOrEmpty(message));

            Message msg = new Message()
            {
                Text = message,
                dateTime = DateTime.Now,
                NickNameFrom = From,
                NickNameTo = nikeTo,
                commands = command
            };

            return msg;
        }


        public void End(CancellationToken token) // Принимаем по ссылке флаг главного цикла. 
        {
            _cts.Cancel(); // Отменяем токен.
            token.ThrowIfCancellationRequested(); // Бросаем ошибку. TODO.Проверить, закроется ли клиент при следующем такте без Throw
        }

        
    }
}
