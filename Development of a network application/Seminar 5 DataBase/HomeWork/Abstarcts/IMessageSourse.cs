using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HomeWork.Model;

namespace HomeWork.Abstarcts
{
    public interface IMessageSourse
    {
        Task SendAsync(NetMessage message, IPEndPoint iPEndPoint);
        NetMessage Receive(ref IPEndPoint iPEndPoint);
    }
}
