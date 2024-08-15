using ChatApp.Service;
//using HomeWork.Service;
using NetMQ.Sockets;
using System.Net;

namespace UDPClient
{
    // Клиет работает на билиотеках и адаптирован под обошённый интерфейс MessageSourseClient(). 
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
            
            Client<DealerSocket> client2 = new (name, adress, port, new MessageSourseClient()); // Чисто адаптивный когд с семинара. На самом деле соурс тут просто пустышка, которая тут на*рен не нужна.
            await client2.StartClient();
  
        }
    }
}
