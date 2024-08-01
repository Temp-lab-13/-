using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Seminar_4_Design_patterns.Server.obj;

namespace Seminar_4_Design_patterns.Server
{
    public enum Тewsletter // Список вариантов рассылки.
    {
        ToOne,
        ToAll,
        Default
    }
    internal class UPDServer
    {
        /*
         * Доработаем систему команд. Имя пользователя сервера всегда будет Server. Если сервер получает команду (как текст сообщения):
         * register : то он добавляет клиента в свой список.
         * delete: он удаляет клиента из списка
         * если сервер не получает имени получателя то он отправляет сообщение всем клиентам
         * если сервер получает имя получателя то он отправляет сообщение одному конкретному клиенту. 
        */
        public string Name { get => "Server-1"; }
        public Dictionary<string, IPEndPoint> Users { get; set; }
        private readonly UdpClient udpClient = new UdpClient(12345);
        private IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
        private Servitor servitor;

        public UPDServer()  // Создаём сервер
        {
            udpClient = new UdpClient(12345);
            iPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            servitor = new Servitor(this);
        }

        public Message Listent() // Слушаем входящие сообщения.
        {
            byte[] buffer = udpClient.Receive(ref iPEndPoint); // Получаем сообщения.
            var messageText = Encoding.UTF8.GetString(buffer); // Декодируем из байт в стрингу
            return Message.DeserializeFromJson(messageText); // Десериализуем в класс и возращаем результат из метода.
        }

        public void Send(Тewsletter tupe, Message msg) // Шлём сообщения, другим пользвателям.
        {
            byte[] buffer = Encoding.UTF8.GetBytes(msg.SerializeMassageToJason());
            switch (tupe)
            {
                case Тewsletter.ToAll: // Всем из списка зарегистрированных, если не указано имя пользователя.
                    foreach (IPEndPoint ip in Users.Values)
                    {
                        udpClient.Send(buffer, buffer.Length, iPEndPoint);
                    }
                    break;
                case Тewsletter.ToOne: // Конекретному пользователю, если оно указано.
                    if(Users.TryGetValue(msg.NickNameTo, out IPEndPoint ep))
                    {
                        udpClient.Send(buffer, buffer.Length, ep);
                    }
                    break;
            }
        }
        public void Starto() // Метод запуска сервока.
        {
            Console.WriteLine($"Сервер запущен."); // Пишем о запуске.
            var bufferMSG = Listent();
            var tupe = servitor.Execute(bufferMSG, iPEndPoint);
            bool run = true;
            while (run) // Переписать на токен.
            {
                ThreadPool.QueueUserWorkItem(del =>
                {
                    Send(tupe, bufferMSG);
                });
            }

        }

        private static void End(ref bool run) // Принимаем по ссылке флаг главного цикла. 
        {
            Console.WriteLine("Нажмите любую клавишу, что бы завершить работу сервера."); // Ожидаем ввода со стороны сервера, что бы завершить работу.
            Console.ReadKey();
            run = false; // Опускаем флаг.
            throw new Exception("Работа сервера завершена."); // Кидаем ошибку с сообщением, которое у нас засветится при окончании работы.
        }
    }
}
