using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ChatCommon.Models;
using NetMQ;
using NetMQ.Sockets;

namespace ChatCommon.Abstarcts
{
    // Интерфейс. Позволяет исползовать методы получения и отправки сообщения из одного места без копипасты.
    public interface IMessageSourse<T>
    {
        NetMessage ReceiveMQ(T socket);
        Task SendAsyncMQ(NetMessage message, NetMQMessage netMQFrames, T socket);
        T CreatSocket();
    }
}
