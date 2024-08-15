using HomeWork.Abstarcts;
using HomeWork.Model;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


// Сервер я не стал переписывать под новый обощённый интерфейс в библиотеке, и он работает на старом коде находящемся непосредственно в проекте HomeWork.
// Однако, клиент полностью переведён и адаптирован под испоьзоавние библиотек: ChatApp, ChatCommon, ChatDb, ChatNetwork(пустой, что там должно было быть я не знаю.)
// Всё приложение работает чисто на MqNet. Где-то отсались кусочки прошлого udp-кода, но он неактивен или закомечнен(большинство я удалил.)
// К слову, на UDP у меня приложение тоже прекрасно работает. 

namespace HomeWork.Service
{
    public class UDPServer
    {
        private readonly IMessageSourse _messageSourse;

        public Dictionary<string, NetMQMessage> clientsMQ; // словарь для активных пользователей. Принимает имя пользователя и его адрес.
        private CancellationTokenSource CTS;
        private RouterSocket Socket;

        public UDPServer() { 
            _messageSourse = new MessageSource();
            CTS = new CancellationTokenSource();
            clientsMQ = new Dictionary<string, NetMQMessage>();
            Socket = new RouterSocket();
        }

        // Метод для обработки регистрации нового клиента
        private async Task Register(NetMessage message)
        {
            Console.WriteLine("Регистрация клиента, имя = " + message.NickNameFrom);

            if (clientsMQ.TryAdd(message.NickNameFrom, message.NetMQFrames))
            {
                using (ChatContext ctx = new ChatContext())
                {
                    if (ctx.Users.FirstOrDefault(x => x.FullName == message.NickNameFrom) != null) return;
                    ctx.Users.Add(new User() { FullName = message.NickNameFrom });
                    await ctx.SaveChangesAsync();
                }
            }
            else
            {
                Console.WriteLine(message.NickNameFrom + ", уже существует.");
            }


        } 
        // Подтверждение статуса "доставлено" или нет.
        async Task ConfirmMessageReceived(int? id)
        {
            Console.WriteLine("Message confirmation id=" + id);
            using (var ctx = new ChatContext())
            {
                var msg = ctx.Messages.FirstOrDefault(x => x.MessageId == id);
                if (msg != null)
                {
                    msg.IsSent = true;
                   await ctx.SaveChangesAsync();
                }
            }
        }

        // Внесение сообщения в базу данных.
        private async Task RelyMessage(NetMessage message)
        {
            if (clientsMQ.TryGetValue(message.NickNameTo, out NetMQMessage netMQFrames))
            {
                int id = 0;
                using (ChatContext ctx = new ChatContext())
                {
                    var fromUser = ctx.Users.First(x => x.FullName == message.NickNameFrom);
                    var toUser = ctx.Users.First(x => x.FullName == message.NickNameTo);
                    var msg = new Message()
                    {
                        UserFrom = fromUser,
                        UserTO = toUser,
                        IsSent = false,
                        Text = message.Text
                    };
                    ctx.Messages.Add(msg);
                    ctx.SaveChanges();
                    id = msg.MessageId;
                }
                message.Id = id;

                await _messageSourse.SendAsyncMQ(message, netMQFrames, Socket);

                Console.WriteLine($"Message Relied, from = {message.NickNameFrom} to = {message.NickNameTo}");
            }
            else
            {
                Console.WriteLine("Пользователь не найден.");
            }
        }

        async Task ProcessMessage(NetMessage message)
        {
            switch (message.Command)
            {
                case Command.Register:
                    await Register(message);
                    break;
                case Command.Confirmation:
                    await ConfirmMessageReceived(message.Id);
                    break;
                case Command.Message:
                    await RelyMessage(message);
                    break;
                default:
                    Console.WriteLine("Ничего не случилось. Где-то было упущено добавление команды к сообщению.");
                    break;
            }
        }

        // Запуск сервера.
        public async Task StartServer()
        {
                Socket.Bind("tcp://*:12345");
            
                Console.WriteLine("Сервер запущен.");
                CancellationTokenSource CTS = new CancellationTokenSource();
                while (!CTS.IsCancellationRequested)
                {
                    try
                    {
                        var message = _messageSourse.ReceiveMQ(Socket);
                        message.PrintMessageFrom();
                        await ProcessMessage(message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка при обработке сообщения: " + ex.Message);
                    }
                }
        }

        // Закрытие сервера.
        // TODO реализовать команду закрытия.
        public void Stop() 
        {
            CTS.Cancel();
        }
    }
}
