using System.Net.Sockets;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            string name = "Kro"; // Заколебался каждый раз прописывать имя.
            //UDPClient.SendMessage(args[0], ip); 
            UDPClient.SendMessage(name, ip);
        }
    }
}
