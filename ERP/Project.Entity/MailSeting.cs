using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class MailSeting
    {
       public long inMailSeting_Id_Pk { get; set; }

       public long oMailSeting_Id_Pk { get; set; }

       public string inHostType { get; set; }

       public string inHostUrl { get; set; }

       public string inPort { get; set; }

       public bool inIsssl { get; set; }

       public string inUsername { get; set; }

       public string inPassword { get; set; }

       public string oHostUrl { get; set; }

       public string oPort { get; set; }

       public bool oIsssl { get; set; }

       public string oUsername { get; set; }

       public string oPassword { get; set; }
    }
}
