using HomeWork.Model;
using NetMQ.Sockets;
using NetMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Abstarcts
{
    public interface IMessageSourseClient
    {
        NetMessage ReceiveMQ(DealerSocket socket);
        Task SendAsyncMQ(NetMessage message, DealerSocket socket);
    }
}
