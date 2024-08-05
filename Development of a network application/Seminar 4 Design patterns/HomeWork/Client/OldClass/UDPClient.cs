using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client.OldClass
{

    internal class UDPClient
    {
        static private CancellationTokenSource _cts = new CancellationTokenSource(); // Создаём токен
        static private CancellationToken __ct = _cts.Token;
        public void Listent(UdpClient udpClient, ref IPEndPoint iPEndPoint) // Слушаем входящие сообщения.
        {
            byte[] buffer = udpClient.Receive(ref iPEndPoint); // Получаем сообщения.
            var messageText = Encoding.UTF8.GetString(buffer); // Декодируем из байт в стрингу
            Message.DeserializeFromJson(messageText).Print(); // Десериализуем в класс и возращаем результат из метода.
        }

        public void End(CancellationToken token) // Принимаем по ссылке флаг главного цикла. 
        {
            _cts.Cancel(); // Отменяем токен.
            token.ThrowIfCancellationRequested(); // Бросаем ошибку. TODO.Проверить, закроется ли клиент при следующем такте без Throw
        }
        public void StartClietn(string From, string ip, Commands command) // Метод старта клиента. 
        {
            UdpClient udpClient = new UdpClient();                               // Линкуемся к серверу. 
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), 12345);  // IP получаем из вне, при старте. Порт зашит жёстко.

            while (!__ct.IsCancellationRequested) // Цикл проверяет состояние токена на каждом витке.
            {
                new Thread(() =>
                { // Отдельный поток для для слухача. В теории, он должен получать сообщения, даже в момент ожидания ввода сообщения клиентом. 
                    try // Внезапно, слухачь матерится и крашит всё, ели не получает при запуске сообщений. Это как-то можно пофиксить, без тупого сброса ошибки обработчиком?  
                    {
                        Listent(udpClient, ref iPEndPoint); // Слухач. Нерерывно слушает сообщения от сервера.

                        // Старый код. ПРМЕР ТОГО, КАК НЕ НАДО СОХРАНЯТЬ СТАРЫЕ НЕ ИПОЛЬЗЕМЫЕ КУСКИ КОДА!!! 
                        //byte[] buffer = udpClient.Receive(ref iPEndPoint); // Получаем сообщения.
                        //var messageText = Encoding.UTF8.GetString(buffer); // Декодируем из байт в стрингу
                        //Message.DeserializeFromJson(messageText).Print(); // Десериализуем в класс и возращаем результат из метода.
                    }
                    catch (Exception ex) // смысл обработчика, что бы не крашилось приложение клиента. Тип ошибки не имет значения.
                    {
                        //Console.WriteLine(ex);
                    }
                }).Start();


                string message;
                string niceTo = string.Empty;
                do
                {
                    //Console.Clear();
                    Thread.Sleep(500);
                    Console.Write("Кому: ");
                    niceTo = Console.ReadLine();
                    Console.Write("Введите сообщение: ");
                    message = Console.ReadLine();
                } while (string.IsNullOrEmpty(message)); // Цикл пашет, пока мы хоть что-то не введём в сообщение.
                                                         // При этом - кому, не важно. Если адресат не указан, то сообщение улетит всем активным пользователям.

                Message msg = new Message() //Экземпляр сообщения с заполнением его полей. 
                {
                    Text = message,
                    dateTime = DateTime.Now,
                    NickNameFrom = From, // От кого
                    NickNameTo = niceTo, // Кому
                    commands = command
                };

                if (msg.Text.ToLower().Equals("exit")) // если в сообщении указано ключевое сообщение. То клиент Завершает работу.
                {
                    try
                    {
                        End(__ct); // Инициализация заверщения работы.
                    }
                    catch (OperationCanceledException ex)
                    {
                        Console.WriteLine("Завершение работы.");
                        msg.commands = Commands.Delete; // Автоматом удаляем пользователя из списка активных пользователей.
                    }
                }

                string json = msg.SerializeMassageToJason(); // Сериализуем.
                byte[] date = Encoding.UTF8.GetBytes(json); // Кодируем в массив байт.
                int cou = udpClient.Send(date, date.Length, iPEndPoint); //Отправляем, и получем колличество байт, которые удалось переслать.
                // Цепочка ответсвенная за отправку сообщения клиенту о доставке его сообщения пока удалена. 
            }
        }
    }
}
