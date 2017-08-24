using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class MailBoxModel
    {
        public List<Mail> Mails { get; set; }

        public MailBoxModel()
        {
            Mails = new List<Mail>();
        }
    }

    public class Mail
    {
        public Int32 MailNo { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string Date { get; set; }

        public string Body { get; set; }
    }
}