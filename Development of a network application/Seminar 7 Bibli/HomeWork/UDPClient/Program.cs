using HomeWork.Service;
using System.Net;

namespace UDPClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //string adress = "127.0.0.1";
            //string name = "Сара";
            //int port = 12345;
            //Client client1 = new Client(name, adress, port);
            string adress = "127.0.0.1";
            string port = "12345";
            string name = "Кли";
            Client client2 = new Client(name, adress, port);
            await client2.StartClient();
  
        }
    }
}
