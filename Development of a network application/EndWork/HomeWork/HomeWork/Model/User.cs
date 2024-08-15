using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Model
{
    public class User
    {
        public virtual List<Message>? messagesTo { get; set; } = new List<Message>();
        public virtual List<Message>? messagesFrom { get; set; } = new List<Message>();
        public int Id { get; set; }
        public string? FullName { get; set; }


    }
}
