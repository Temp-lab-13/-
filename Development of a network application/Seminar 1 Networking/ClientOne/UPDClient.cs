using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientOne
{
    internal class UPDClient
    {
        public static void SendMessage(string From, string ip) // Принимаем при вызове имя клиента и ip сервера(?), к которому цепляемся.
        {
            UdpClient udpClient = new UdpClient(); 
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345); // Задаём ip и порт.

            while (true)
            {
                string message;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Введите сообщение: ");
                    message = Console.ReadLine();
                } while (string.IsNullOrEmpty(message)); // Цикл пашет, пока мы хоть что-то не введём.

                Message msg = new Message() //Экземпляр сообщения с заполнением его полей.
                {
                    Text = message,
                    dateTime = DateTime.Now,
                    NickNameFrom = From,
                    NickNameTo = "Server"
                };

                string json = msg.SerializeMassageToJason(); //Сериализуем.

                byte[] date = Encoding.UTF8.GetBytes(json); //Получем массив байт.
                int cou = udpClient.Send(date, date.Length, iPEndPoint); //Отправляем, и получем колличество байт, которые удалось переслать.


                //Это часть - домашнее задание. Суть - получить ответ сервера о доставке соощения.
                byte[] bufferAnswer = udpClient.Receive(ref iPEndPoint); // Запрашиваем ответ.
                if (bufferAnswer != null && cou == date.Length) // Если ответ прилетел, а колличество перданных байт соответсвует размеру переведённого в массив байт сообщения.
                {
                    var answer = Encoding.UTF8.GetString(bufferAnswer); //То рашифровываем полученный ответ от сервера.
                    Console.WriteLine(answer); // И печатем его.
                    Console.ReadKey(); // Жмякнуть клавишу, что бы продолжить. Надо что бы подтверждение увидеть.
                    // В идеале сделать счётчик отправленных сообщений, который будет светиться постоянно,
                    // и задержку, позволяющую увидеть подтверждение, которое вскоре само исчезнет. 
                }
                else
                {
                    Console.WriteLine("Сообщение не доставлено.");
                }
                
            }
        }
    }
}
