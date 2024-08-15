using HomeWork.Abstarcts;
using HomeWork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Service
{
    public class UDPServer
    {
        private readonly IMessageSourse _messageSourse;
        public Dictionary<string, IPEndPoint> clients { get; set; }
        private UdpClient UdpClient;
        private IPEndPoint EndPoint;
        private CancellationTokenSource CTS;

        public UDPServer() { 
            _messageSourse = new MessageSource();
            UdpClient = new UdpClient(12345);
            EndPoint = new IPEndPoint(IPAddress.Any, 0);
            CTS = new CancellationTokenSource();
            this.clients = new Dictionary<string, IPEndPoint>();
        }

        // Метод для обработки регистрации нового клиента
        private async Task Register(NetMessage message)
        {
            Console.WriteLine("Message Register, name = " + message.NickNameFrom);
            //IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0); // понять, какого хрена я не могу получть эндпоинт
            message.NickAddress = EndPoint;
      
            if (clients.TryAdd(message.NickNameFrom, message.NickAddress)) // В теории, мы должны были получать эндпоин от пользователя, в его сообщении регистраци.
            {                                                              // Но, эндпоит не сереализуется, ввиду чего метод просто не завершается и пользователь вообще не регистрируется.
                                                                           // Попробовал передать эндпоин строкой и интом, что бы потом их сформировать в эндпоинт уже в этм методе,
                using (ChatContext ctx = new ChatContext())                // однако, почему-то это не работает. Сообщения просто не летят клиенту. Пока, Хз, в чём дело.
                {
                    if (ctx.Users.FirstOrDefault(x => x.FullName == message.NickNameFrom) != null) return;
                    ctx.Users.Add(new User() { FullName = message.NickNameFrom });
                    await ctx.SaveChangesAsync();
                }
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
            if (clients.TryGetValue(message.NickNameTo, out IPEndPoint ep))
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

                await _messageSourse.SendAsync(message, ep, UdpClient);
                
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
                    Console.WriteLine("Ничего");
                    break;
            }
        }

        // Запуск сервера.
        public async Task StartServer()
        {
            Console.WriteLine("Сервер запущен.");
            CancellationTokenSource CTS = new CancellationTokenSource();
            while (!CTS.IsCancellationRequested)
            {
                try
                {
                    var message = _messageSourse.Receive(ref EndPoint, ref UdpClient);
                    
                    message.PrintMessageFrom();
                    await ProcessMessage(message);
                }
                catch (Exception ex)
                {
                    // Console.WriteLine("Ошибка при обработке сообщения: " + ex.Message);
                    // Вот эта фигня совершенно не нужна, так как оишбки будут сыпаться непрерывно.
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
