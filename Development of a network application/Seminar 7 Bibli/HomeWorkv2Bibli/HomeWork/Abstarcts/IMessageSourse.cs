using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using HomeWork.Model;
using NetMQ;
using NetMQ.Sockets;

namespace HomeWork.Abstarcts
{
    // Интерфейс. Позволяет исползовать методы получения и отправки сообщения из одного места без копипасты.
    public interface IMessageSourse
    {
        NetMessage ReceiveMQ(RouterSocket socket);
        Task SendAsyncMQ(NetMessage message, NetMQMessage netMQFrames, RouterSocket socket);
    }
}
