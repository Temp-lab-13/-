
using ChatCommon.Abstarcts;
using ChatCommon.Models;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Service
{
    public class MessageSourseClient : IMessageSourseClient<DealerSocket>
    {
        public NetMessage ReceiveMQ(DealerSocket socket)
        {
            var buffer = socket.ReceiveMultipartMessage();
            string str = Encoding.UTF8.GetString(buffer.Last.ToByteArray());  
            if (str == null || str.Length == 0)
            {
                Console.WriteLine("Сообщение пустышка");
                return new NetMessage();
            }
            else 
            {
                var temp = NetMessage.DeserializeMessgeFromJSON(str);
                return temp;
            }

        }

        public async Task SendAsyncMQ(NetMessage message, DealerSocket socket)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message.SerialazeMessagerToJSON());
            socket.SendFrame(buffer);
        }

        public DealerSocket CreatSocker(string address, string port)
        {
            string connect = $"tcp://{address}:{port}";
            return new DealerSocket(connect);
        }

        public DealerSocket GetServer()
        {
            return null;
        }


    }
}
