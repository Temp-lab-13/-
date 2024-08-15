using NetMQ.Sockets;
using NetMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatCommon.Models;

namespace ChatCommon.Abstarcts
{
    public interface IMessageSourseClient<T>
    {
        NetMessage ReceiveMQ(T socket);
        Task SendAsyncMQ(NetMessage message, T socket);

        T CreatSocker(string address, string port);
        T GetServer();
        

    }
}
