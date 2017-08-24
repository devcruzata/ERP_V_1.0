using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Chat
    {
       public long ChatID { get; set; }

       public long SenderID { get; set; }

       public long RecieverID { get; set; }

       public string Msg { get; set; }

       public string Date { get; set; }

       
    }
}
