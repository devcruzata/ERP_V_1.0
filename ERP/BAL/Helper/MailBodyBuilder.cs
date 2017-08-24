using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;

namespace BAL.Helper
{
   public class MailBodyBuilder
    {
       public static string PopulateTicketConfEmailBody(string Name, string ticketNo, string subject, string status, string templatePath)
       {
           string body = string.Empty;
           using (StreamReader reader = new StreamReader(HostingEnvironment.MapPath(templatePath)))
           {
               body = reader.ReadToEnd();
           }
           body = body.Replace("{Name}", Name);
           body = body.Replace("{ticketNo}", ticketNo);
           body = body.Replace("{subject}", subject);          
           body = body.Replace("{status}", status);
           return body;
       }      

       public static string PopulateTicketClosureEmailBody(string ticketNo, string templatePath)
       {
           string body = string.Empty;
           using (StreamReader reader = new StreamReader(HostingEnvironment.MapPath(templatePath)))
           {
               body = reader.ReadToEnd();
           }
           body = body.Replace("{ticketNo}", ticketNo);           
           return body;
       }
    }
}
