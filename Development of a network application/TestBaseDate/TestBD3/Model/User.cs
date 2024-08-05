using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBD3.Model
{
    public partial class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

        public GenderId GenderId { get; set; }
        public virtual Gender Gender { get; set; }
    }
}
