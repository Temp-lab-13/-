using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public enum Тewsletter // Список вариантов рассылки.
    {
        ToOne,
        ToAll,
        Default
    }
    internal class UDPServer
    {
        static private CancellationTokenSource _cts = new CancellationTokenSource(); // Создаём токен
        static private CancellationToken __ct = _cts.Token;

        public string Name { get => "Server"; }
        public Dictionary<string, IPEndPoint> Users { get; set; } // Список активных пользвателей. 
        private readonly UdpClient udpClient;
        private IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
        private Servitor servitor;

        public UDPServer()  // Создаём сервер
        {
            udpClient = new UdpClient(12345); 
            iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            servitor = new Servitor(this);
            Users = new Dictionary<string, IPEndPoint>(); // Список создаём сразу, что бы избежать ошибок при поиске в списке юзеров, когда ещё не один юзер не был зарегистрирован.
        }

        public Message Listent() // Слушаем входящие сообщения.
        {
            try
            {
                byte[] buffer = udpClient.Receive(ref iPEndPoint); // Получаем сообщения.
                var messageText = Encoding.UTF8.GetString(buffer); // Декодируем из байт в стрингу
                return Message.DeserializeFromJson(messageText); // Десериализуем в класс и возращаем результат из метода.
            }
            catch (Exception ex) // Получили какую-то ошибку при прослушке, то возращаем null. И после проверки на null, ничего не делаем.
            {
                return null;
            }
            
        }

        public bool Send(Тewsletter tupe, Message msg) // Шлём сообщения, другим пользвателям.
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg.SerializeMassageToJason());
            switch (tupe)
            {
                case Тewsletter.ToAll: // Всем из списка зарегистрированных, если не указано имя пользователя.
                    foreach (IPEndPoint ip in Users.Values)
                    {
                        udpClient.Send(buffer, buffer.Length, iPEndPoint);
                    }
                    return true;
                    //break;
                case Тewsletter.ToOne: // Конекретному пользователю, если оно указано.
                    if (Users.TryGetValue(msg.NickNameTo, out IPEndPoint ep))
                    {
                        udpClient.Send(buffer, buffer.Length, ep);
                    }
                    return true;
                    //break;
                default: return false;
            }
        }

        public void End(CancellationToken token) // Принимаем по ссылке флаг главного цикла. 
        {
            _cts.Cancel();
            token.ThrowIfCancellationRequested();
        }
        public void Starto() // Метод запуска сервока.
        {
            Console.WriteLine($"Сервер запущен."); // Пишем о запуске.


            while (!__ct.IsCancellationRequested) // Переписать на токен.
            {
                var bufferMSG = Listent(); 
                try
                {
                    if (bufferMSG != null) 
                    {
                        var tupe = servitor.Execute(bufferMSG, iPEndPoint, __ct);

                        ThreadPool.QueueUserWorkItem(del =>
                        {
                            Send(tupe, bufferMSG);
                        }, __ct);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("Завершена работа сервера.");
                }
            }

        }

    }
}
