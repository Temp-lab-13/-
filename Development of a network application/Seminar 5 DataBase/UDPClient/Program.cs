using HomeWork.Service;

namespace UDPClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await new Client("Сара",
                             "127.0.0.1",
                             12345).StartClient();
        }
    }
}
