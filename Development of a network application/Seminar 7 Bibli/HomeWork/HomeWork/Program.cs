using HomeWork.Model;
using HomeWork.Service;

namespace HomeWork;

// Важно. Для использования dotnet ef надо установить через консоль слудещее: dotnet tool install --global dotnet-ef
internal class Program
{
    static async Task Main(string[] args)
    {
        await new UDPServer().StartServer();

    }
}
