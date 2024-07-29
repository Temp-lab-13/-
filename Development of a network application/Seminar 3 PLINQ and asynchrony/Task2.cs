using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace Seminar_3_PLINQ_and_asynchrony
{
    /*
     * Напишите многопоточное приложение, которое определяет все IP-адреса интернет-ресурса 
     * и определяет до которого из них лучше Ping. 
     * Приложение должно работать с помощью Task.
    */
    public class Task2
    {
        public static async Task Taska2()
        {
            const string sait = "yandex.ru";

            IPAddress[] iPAddress = Dns.GetHostAddresses(sait, System.Net.Sockets.AddressFamily.InterNetwork);

            foreach (var item in iPAddress)
            {
                Console.WriteLine(item);
            }

            Dictionary<IPAddress, long> pings = new Dictionary<IPAddress, long>();

            List<Task> taska = new List<Task>();
            foreach (var item in iPAddress)
            {
                var task1 = Task.Run(async() =>
                {
                    Ping p = new Ping();
                    PingReply pingReply = await p.SendPingAsync(item);
                    pings.Add(item, pingReply.RoundtripTime);
                    Console.WriteLine($"{item} : {pingReply.RoundtripTime}");
                });
                taska.Add(task1);
            }

            Task.WaitAll(taska.ToArray());

            long minPing = pings.Min(x => x.Value);

            Console.WriteLine(minPing);

        }
    }
}
