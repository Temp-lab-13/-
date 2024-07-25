using Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
namespace ClientOne
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1"; // ip фиксированный, что бы каждый раз при запуске не писать. Всё равно у нас всё локально.
            UPDClient.SendMessage(args[0], ip); // Передаём Имя клиента, под которым будут отправляться сообщения. И наш ip.
        }
    }
}
