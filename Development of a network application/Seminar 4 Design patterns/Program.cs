namespace Seminar_4_Design_patterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            string name = "Kro";
            Console.WriteLine("Hello, World!");
            UDPClietn.StartClietn(name, ip);
        }
    }
}
