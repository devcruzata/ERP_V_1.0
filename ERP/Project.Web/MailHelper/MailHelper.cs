using Project.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Project.Web.MailHelper
{
    public class MailHelper
    {
        public async static Task<Boolean> SendEmail(string emailid, string subject, string message)
        {
            SessionHelper session = new SessionHelper();
            try
            {

                var fromAddress = new MailAddress(session.UserSetingSession.smtpUsername, "Clouderac");
                var toAddress = new MailAddress(emailid, "");
                string fromPassword = session.UserSetingSession.smtpPassword;
                var smtp = new SmtpClient
                {
                    Host = session.UserSetingSession.smtpHost,
                    Port = Convert.ToInt32(session.UserSetingSession.smtpPort),
                    EnableSsl = session.UserSetingSession.smtpIsSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                MailMessage msg = new System.Net.Mail.MailMessage(session.UserSetingSession.smtpUsername, emailid, subject, message);
                msg.IsBodyHtml = true;
                smtp.SendAsync(msg, null);
                // await smtp.SendAsync(msg, null);
                //  smtp.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}