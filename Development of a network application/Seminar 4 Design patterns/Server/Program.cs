using Seminar_4_Design_patterns.Server;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UPDServer server = new UPDServer();
            server.Starto(); 
        }
    }
}
