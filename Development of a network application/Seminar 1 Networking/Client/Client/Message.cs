using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Server
{
    public class Message
    {
        public string Text { get; set; }
        public DateTime dateTime { get; set; }
        public string NickNameFrom { get; set; }
        public string NickNameTo { get; set; }

        public string SerializeMassageToJason() => JsonSerializer.Serialize(this);
       
        public static Message? DeserializeFromJson(string json) => JsonSerializer.Deserialize<Message>(json);

        public bool Print()
        {
            Console.WriteLine(ToString());
            return true;
        }

        public override string ToString()
        {
            return $"{this.dateTime} получено сообщение от {this.NickNameFrom}: {this.Text};";
        }
    }
}
