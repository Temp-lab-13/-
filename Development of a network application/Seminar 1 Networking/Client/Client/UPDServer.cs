using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class UPDServer
    {
        public static void Server(string message)
        {
            UdpClient udpClient = new UdpClient(12345); // Устанавливаем клиентский Порт
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0); // Слушаем любой ip.

            Console.WriteLine($"{message} Сервер запущен."); // Если ничего не упало, пишем, об успехе запуска и ожидания.

            while (true)
            {
                byte[] buffer = udpClient.Receive(ref iPEndPoint); // Получаем сообщения.

                if (buffer == null) break;
                var messageText = Encoding.UTF8.GetString(buffer); // Декодируем.

                Message msg = Message.DeserializeFromJson(messageText); // Создаём и заполняем поля соощения, десириализуя полученные данные.
                //msg.Print(); //Распечатываем.
                
                if (msg.Print()) //Распечатываем, если метод отработал, то отпправляем клиенту сообщение о успешной доставке. При этом, целостность сообщения на сервере не проверяется.
                {
                    string confirmation = "Сообщение доставлено."; // Шаблон сообщения.
                    byte[] Confirmation = Encoding.UTF8.GetBytes(confirmation); // Превращаем ответ в массив байт. Так как это тупо строка, не вижу смыла серилизовать.
                    udpClient.Send(Confirmation, Confirmation.Length, iPEndPoint); // Отпровляем. Можно сделдать подтверждение отправления, но это уже хлам.
                }
            }
        }
    }
}
