using HomeWork.Service;
using System.Net;

namespace UDPClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string adress = "127.0.0.1";
            string name = "Сара";
            int port = 12345;
            Client client1 = new Client(name, adress, port);
            await client1.StartClient();
  
        }
    }
}
