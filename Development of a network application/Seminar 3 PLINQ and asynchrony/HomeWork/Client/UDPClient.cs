using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class UDPClient
    {
        public static void SendMessage(string From, string ip) // Принимаем при вызове имя клиента и ip сервера, к которому цепляемся.
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);
            while (true)
            {
                string message;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Введите сообщение: ");
                    message = Console.ReadLine();
                } while (string.IsNullOrEmpty(message)); 

                Message msg = new Message() 
                {
                    Text = message,
                    dateTime = DateTime.Now,
                    NickNameFrom = From,
                    NickNameTo = "Server"
                };

                string json = msg.SerializeMassageToJason(); 

                byte[] date = Encoding.UTF8.GetBytes(json);
                int cou = udpClient.Send(date, date.Length, iPEndPoint); 


                byte[] bufferAnswer = udpClient.Receive(ref iPEndPoint); 

                
                if (bufferAnswer != null && cou == date.Length) // Если ответ прилетел, а колличество перданных байт соответсвует размеру переведённого в массив байт сообщения.
                {
                    var answer = Encoding.UTF8.GetString(bufferAnswer); //То рашифровываем полученный ответ от сервера
                    Console.Write(answer); // И печатем его.
                    Console.WriteLine(" Нажмите любую клавишу, что бы завершить работу:");
                    Console.ReadKey();
                    if (answer == "Сервер остановлен.") // Если сообщение от сервера является его оповещением об остановке. То мы завершаем работу клиента.
                    {
                        udpClient.Send(date, date.Length, iPEndPoint); // но, без него, сервер виснет, не закрывая приложение (если только не дать ошибке его закрашить).
                        return;
                    }
                }

                else // Если не получилось отправить сообщение целиком или мы не получили ответ, то пишем об ошибке доставки.
                {
                    Console.WriteLine("Сообщение не доставлено.");
                }
            }
        }
    }
}
