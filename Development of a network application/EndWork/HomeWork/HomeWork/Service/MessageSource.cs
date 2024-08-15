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
    public class MessageSource : IMessageSourse
    {
        
        // Получение сообщения.
        public NetMessage Receive(ref IPEndPoint iPEndPoint, ref UdpClient UdpClient)
        {
            byte[] buffer = UdpClient.Receive(ref iPEndPoint);
            string str = Encoding.UTF8.GetString(buffer);
            if (str == null || str.Length == 0) { return new NetMessage(); } else return NetMessage.DeserializeMessgeFromJSON(str);  
        }
        // Отправка сообщения.
        public async Task SendAsync(NetMessage message, IPEndPoint iPEndPoint, UdpClient UdpClient)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message.SerialazeMessagerToJSON());
            await UdpClient.SendAsync(buffer, buffer.Length, iPEndPoint);
        }
    }
}
