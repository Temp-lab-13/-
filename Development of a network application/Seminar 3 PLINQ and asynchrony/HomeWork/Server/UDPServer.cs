using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class UDPServer
    {
        // Домашняя работа №3
        static private CancellationTokenSource _cts = new CancellationTokenSource(); // Создаём токен
        static private CancellationToken __ct = _cts.Token;
        public static void Server(string message)
        {
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);


            Console.WriteLine($"{message} Сервер запущен."); 

            while (!__ct.IsCancellationRequested) // устанавливаем токен, как условие в цикл.
            {
                byte[] buffer = udpClient.Receive(ref iPEndPoint); 
                var messageText = Encoding.UTF8.GetString(buffer); 


                ThreadPool.QueueUserWorkItem(obj => 
                {
                    Message msg = Message.DeserializeFromJson(messageText);
                    
                    if (msg.Print()) 
                    {
                        string confirmation = "Сообщение доставлено."; 
                        byte[] Confirmation = Encoding.UTF8.GetBytes(confirmation);
                        udpClient.Send(Confirmation, Confirmation.Length, iPEndPoint);
                    }
                    else // Блок выхода из приложения.
                    {
                        try
                        {
                            End(__ct); // Запускаем метод выхода
                        }
                        catch (OperationCanceledException ex) // Обрабатываем ошибку вызванную отменой токена.
                        {
                            byte[] Confirmation = Encoding.UTF8.GetBytes($"Сервер остановлен."); // Сообщение о завершении работы сервера разбиваем на массив байт
                            udpClient.Send(Confirmation, Confirmation.Length, iPEndPoint); // отправлеям.
                            Console.WriteLine(ex); // Выводим сообщение об отмене токена. 
                        }
                    }

                }, __ct); // Кидаем в пул
            }



        }

        private static void End(CancellationToken run)
        {
            _cts.Cancel(); // Отменяем токен.
            Console.WriteLine("Нажмите любую клавишу, что бы завершить работу сервера."); // Ожидаем ввода со стороны сервера, что бы завершить работу.
            Console.ReadKey();
            run.ThrowIfCancellationRequested(); // Выкидываем ошибку.
        }
    }
}
