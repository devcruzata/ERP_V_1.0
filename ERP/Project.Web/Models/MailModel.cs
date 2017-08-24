using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class MailModel
    {
        public long Mail_ID_Pk { get; set; }

        public long RelateTo_ID { get; set; }

        public string RelateTo_Name { get; set; }

        public long MailBy_ID { get; set; }

        public string MailBy_Name { get; set; }

        public string ToAddress { get; set; }

        public string CcAddress { get; set; }

        public string BccAddress { get; set; }

        public string FromAddress { get; set; }

        public string Subject { get; set; }

        public string MailBody { get; set; }

        public HttpPostedFileBase[] Attechments { get; set; }

        public string Date { get; set; }

        public string Status { get; set; }

        public List<Mails> mails { get; set; }
    }
}