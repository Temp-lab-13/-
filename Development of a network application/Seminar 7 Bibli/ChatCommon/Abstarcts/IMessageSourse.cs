using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ChatCommon.Abstarcts
{
    public interface IMessageSourse<T>
    {
        Task SendAsync(NetMessage message, T iPEndPoint);
        NetMessage Receive(ref T iPEndPoint);

        T CreateEndpoit();
        T CopyEndpoint(IPEndPoint iPEndPoint);
    }
}
