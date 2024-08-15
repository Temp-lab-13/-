using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatCommon.Models
{
    // Сообщение работающее с базой данных.
    public class Message
    {
        public int MessageId { get; set; } // id каждого сообщения, позволяет его найти
        public string? Text { get; set; }
        public DateTime DateSend { get; set; }
        public bool IsSent { get; set; } // Статус сообщения. Доставлено/нет

        public int UserTOId { get; set; }
        public int UserFromId { get; set; }
        public virtual User? UserTO { get; set; }
        public virtual User? UserFrom { get; set; }

    }
}
