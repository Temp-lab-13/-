using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBD3.Model
{
    public partial class Message
    {

        public int Id { get; set; }

        public string? MessageContent { get; set; }

        public int UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
