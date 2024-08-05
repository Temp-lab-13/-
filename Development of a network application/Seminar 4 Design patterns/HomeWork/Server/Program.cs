using Server;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {

             UDPServer uDPServer = new UDPServer();
             uDPServer.Starto();
        }
    }
}
