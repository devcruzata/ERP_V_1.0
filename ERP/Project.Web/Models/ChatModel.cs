using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class ChatModel
    {
        public long ChatID { get; set; }

        public long SenderID { get; set; }

        public long RecieverID { get; set; }

        public string Msg { get; set; }

        public string Date { get; set; }

        public List<Project.Entity.Chat> chats { get; set; }
    }
}