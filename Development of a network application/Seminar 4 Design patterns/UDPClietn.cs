using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Seminar_4_Design_patterns
{
    public class UDPClietn
    {
        //Register,
        //Delete,
        //Exit

        public static void StartClietn(string From, string ip)
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);

            while (true)
            {
                string message;
                string comand;
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
                    NickNameTo = "Server",
                    commands = Commands.Register
                    
                };

                string json = msg.SerializeMassageToJason(); // Сериализуем.

                byte[] date = Encoding.UTF8.GetBytes(json); // Кодируем в массив байт.
                int cou = udpClient.Send(date, date.Length, iPEndPoint); //Отправляем, и получем колличество байт, которые удалось переслать.





                /*
                //Это часть - домашнее задание. Суть - получить ответ сервера о доставке соощения.
                byte[] bufferAnswer = udpClient.Receive(ref iPEndPoint); // Запрашиваем ответ.
                if (bufferAnswer != null && cou == date.Length) // Если ответ прилетел, а колличество перданных байт соответсвует размеру переведённого в массив байт сообщения.
                {
                    var answer = Encoding.UTF8.GetString(bufferAnswer); //То рашифровываем полученный ответ от сервера.
                    Console.WriteLine(answer); // И печатем его.
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Сообщение не доставлено.");
                }

                if (message.ToLower() == "exit")
                {
                    date = Encoding.UTF8.GetBytes("Клиент остановлен."); // Вообще, это сообщение уже не достигнет сервер и там ничего не выведется.
                    udpClient.Send(date, date.Length, iPEndPoint); // но, без него, сервер виснет, не закрывая приложение (если только не дать ошибке его закрашить). 
                    udpClient.Close();
                    Console.WriteLine("Нажмите любую клавишу, что бы завершить работу сервера.");
                    return;
                }*/

            }
        }
    }
}
