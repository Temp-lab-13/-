﻿using ChatCommon;
using ChatCommon.Abstarcts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace ChatApp
{
    public class UdpMessageSource : IMessageSourse<IPEndPoint>
    {
        private readonly UdpClient _udpClient;

        public UdpMessageSource()
        {
            _udpClient = new UdpClient(12345); //????
        }

        public IPEndPoint CopyEndpoint(IPEndPoint iPEndPoint)
        {
            return new IPEndPoint(iPEndPoint.Address, iPEndPoint.Port);
        }

        public IPEndPoint CreateEndpoit()
        {
            return new IPEndPoint(IPAddress.Any, 0);
        }

        public NetMessage Receive(ref IPEndPoint iPEndPoint)
        {
            byte[] buffer = _udpClient.Receive(ref iPEndPoint);
            string str = Encoding.UTF8.GetString(buffer);
            return NetMessage.DeserializeMessgeFromJSON(str)?? new NetMessage(); // На случай, если прилетит null, мы вернём пустой, но проинациализрованный месседж. меседж
        }

        public async Task SendAsync(NetMessage message, IPEndPoint iPEndPoint)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message.SerialazeMessagerToJSON());
            await _udpClient.SendAsync(buffer, buffer.Length, iPEndPoint);
        }
    }
}