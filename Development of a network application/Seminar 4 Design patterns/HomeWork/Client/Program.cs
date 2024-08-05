using Client.Model.Singleton;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Реализуем запуск клиента через Singlenon. Что бы было =__=
            SingUser.Instance.StartClietn("One", "127.0.0.1", Commands.Register);










            //UDPClient client1 = new UDPClient();
            //client1.StartClietn("One", "127.0.0.1", Commands.Register);

        }
    }
}
