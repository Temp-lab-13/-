using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Server
{
    internal class UDPSever
    {
        public static void Server(string message)
        {
            UdpClient udpClient = new UdpClient(12345); 
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0); 

            Console.WriteLine($"{message} Сервер запущен."); // Если ничего не упало, пишем, об успехе запуска и ожидания.

            bool run = true;
            while (run)
            {
                byte[] buffer = udpClient.Receive(ref iPEndPoint); // Получаем сообщения.
                var messageText = Encoding.UTF8.GetString(buffer); // Декодируем.


                ThreadPool.QueueUserWorkItem(obj => //Закидываем работу цикла в пул потов.
                {
                    Message msg = Message.DeserializeFromJson(messageText);

                    if (msg.Print()) // Петаем и отправлям подтверждение о оставке сообщения.
                    {
                        string confirmation = "Сообщение доставлено."; // Шаблон сообщения.
                        byte[] Confirmation = Encoding.UTF8.GetBytes(confirmation);
                        udpClient.Send(Confirmation, Confirmation.Length, iPEndPoint);
                    }
                    else // Блок выхода из приложения.
                    {
                        try // Ловим ошибку, которую сами же и создаём. Такая была задача указана преподователем на семинаре.~ 
                        {
                            
                            End(ref run); // Запускаем метод выхода
                        } catch (Exception ex) // Обрабатываем ошибку.
                        {
                            byte[] Confirmation = Encoding.UTF8.GetBytes($"Сервер остановлен."); // Сообщение о завершении работы сервера разбиваем на массив байт
                            udpClient.Send(Confirmation, Confirmation.Length, iPEndPoint); // отправлеям.
                            Console.WriteLine(ex); // Выводим сообщение о краше сервака. 
                            
                        }
                    }

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
