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
    public class UdpMessageSource : IMessageSourse
    {
        private readonly UdpClient _udpClient;

        public UdpMessageSource()
        {
            _udpClient = new UdpClient(12345);
        }
        // Получение сообщения.
        public NetMessage Receive(ref IPEndPoint iPEndPoint)
        {
            IPEndPoint iP = new IPEndPoint(IPAddress.Any, 0);
            byte[] buffer = _udpClient.Receive(ref iPEndPoint);
            //byte[] buffer = _udpClient.Receive(ref iP);
            string str = Encoding.UTF8.GetString(buffer);
            return NetMessage.DeserializeMessgeFromJSON(str);//?? new NetMessage(); // На случай, если прилетит null, мы вернём пустой, но проинациализрованный месседж. меседж
        }
        // Отправка сообщения.
        public async Task SendAsync(NetMessage message, IPEndPoint iPEndPoint)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message.SerialazeMessagerToJSON());
            await _udpClient.SendAsync(buffer, buffer.Length, iPEndPoint);
        }
    }
}
