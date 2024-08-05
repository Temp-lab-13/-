using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client
{
    public enum Commands
    {
        Register,
        Delete,
        Exit
    }
    public class Message
    {
        public Commands commands { get; set; }
        public string Text { get; set; }
        public DateTime dateTime { get; set; }
        public string NickNameFrom { get; set; }
        public string NickNameTo { get; set; }

        public string SerializeMassageToJason() => JsonSerializer.Serialize(this);

        public static Message? DeserializeFromJson(string json) => JsonSerializer.Deserialize<Message>(json);

        public void Print()
        {
                Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return $"\n{this.dateTime} получено сообщение от {this.NickNameFrom}: {this.Text};";
        }
    }
}
