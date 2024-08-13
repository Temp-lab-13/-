using ChatCommon;
using ChatCommon.Abstarcts;
using ChatDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public class UDPServer<T>
    {
        Dictionary<string, T> clients = new Dictionary<string, T>();
        private readonly IMessageSourse<T> _messageSourse;
        private T EndPoint;
        private CancellationTokenSource CTS;

        public UDPServer(IMessageSourse<T> messageSourse) { 
            _messageSourse = messageSourse;
            EndPoint = _messageSourse.CreateEndpoit();
            CTS = new CancellationTokenSource();
        }

        // Метод для обработки регистрации нового клиента
        private async Task Register(NetMessage message)
        {
            Console.WriteLine("Message Register, name = " + message.NickNameFrom);

            if(clients.TryAdd(message.NickNameFrom, _messageSourse.CopyEndpoint(message.NickAddress)))
            {
                using (ChatContext ctx = new ChatContext())
                {
                    if (ctx.Users.FirstOrDefault(x => x.FullName == message.NickNameFrom) != null) return;
                    ctx.Users.Add(new User() { FullName = message.NickNameFrom });
                    await ctx.SaveChangesAsync();
                }
            }
        }

        async Task ConfirmMessageReceived(int? id)
        {
            Console.WriteLine("Message confirmation id=" + id);
            // Изменяем статус получения сообщения в базе данных
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

        private async Task RelyMessage(NetMessage message)
        {
            
            if (clients.TryGetValue(message.NickNameTo, out T ep))
            {
                // Добавляем сообщение в базу данных
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

                await _messageSourse.SendAsync(message, ep);
                
                Console.WriteLine($"Message Relied, from = {message.NickNameFrom} to = {message.NickNameTo}");
            }
            else
            {
                Console.WriteLine("Пользователь не найден.");
            }
        }

        async Task ProcessMessage(NetMessage message)
        {
            Console.WriteLine();
            Console.WriteLine(message.Text);

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
                default: break;
            }
        }

        public async Task StartServer()
        {
            CancellationTokenSource CTS = new CancellationTokenSource();
            while (!CTS.IsCancellationRequested)
            {
                try
                {
                    var message = _messageSourse.Receive(ref EndPoint);
                    Console.WriteLine(message.ToString());
                    await ProcessMessage(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при обработке сообщения: " + ex.Message);
                }
            }
        }

        public void Stop() 
        {
            CTS.Cancel();
        }
    }
}
