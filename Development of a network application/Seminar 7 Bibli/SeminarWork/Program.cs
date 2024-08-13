using ChatApp;
using SeminarWork.Service;
using System.Net;
using UdpMessageSource = ChatApp.UdpMessageSource;

namespace SeminarWork
{
    internal class Program
    {
        static async void Main(string[] args)
        {
            if (args.Length == 0)
            {
                var server = new UDPServer<IPEndPoint>(new UdpMessageSource());
                await server.StartServer();
            }
            else
            {
                if (args.Length == 1)
                 {
                        var client = new Client<IPEndPoint>(args[0], new UdpMessageSourceClietn());
                        await client.StartClient();
                    
                }
            }
        }
    }
}
