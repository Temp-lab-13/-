using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SeminarWork.Abstarcts;
using SeminarWork.Model;
using SeminarWork.Service;

namespace ServerTest
{
    public class MockMessageSourse : IMessageSourse
    {
        private Queue<NetMessage> message = new Queue<NetMessage>();
        private UDPServer server;
        private IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
        public void AddServer(UDPServer _server)
        {
            server = _server;
        }
        public NetMessage Receive(ref IPEndPoint iPEndPoint)
        {
            iPEndPoint = endPoint;

            if(message.Count == 0)
            {
                server.Stop();
                return null;
            }
            return message.Dequeue();
        }

        public Task SendAsync(NetMessage message, IPEndPoint iPEndPoint)
        {
            throw new NotImplementedException();
        }

        public MockMessageSourse()
        {
            message.Enqueue(new NetMessage { Command = Command.Register, NickNameFrom = "Кли" });
            message.Enqueue(new NetMessage { Command = Command.Register, NickNameFrom = "Нахида" });
            message.Enqueue(new NetMessage { Command = Command.Register, NickNameFrom = "Кокоми", NickNameTo = "Сара", Text = "Сдавайтесь, генерал." });
            message.Enqueue(new NetMessage { Command = Command.Register, NickNameFrom = "Сара", NickNameTo = "Кокоми", Text = "Нет!" });
        }
    }
}
