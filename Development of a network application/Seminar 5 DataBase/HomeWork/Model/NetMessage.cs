using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeWork.Model
{
    // Команды, кэп
    public enum Command
    {
        Register,
        Message,
        Confirmation
    }
    // Шаблон сообщения между передающихся между юзерами.
    public class NetMessage
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateSend { get; set; }
        public string? NickNameFrom { get; set; }
        public string? NickNameTo { get; set; }
        public IPEndPoint? NickAddress { get; set; }

        public Command Command { get; set; }

        public string SerialazeMessagerToJSON() => JsonSerializer.Serialize(this);

        public static NetMessage? DeserializeMessgeFromJSON(string message) => JsonSerializer.Deserialize<NetMessage>(message);

        public void PrintMessageFrom()
        {
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return $"{DateSend} От {NickNameFrom} получено сообщение: \n";
        }
    }
}
