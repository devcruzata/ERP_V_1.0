using OpenPop.Mime;
using OpenPop.Pop3;
using Project.ViewModel;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.MailBox
{
    public class MailBoxController : Controller
    {
        
        SessionHelper session;
        //
        // GET: /MailBox/
        //[Authorize]
        //public ActionResult Inbox()
        //{
        //    MailBoxModel MailList = new MailBoxModel();
        //    try
        //    {
                
        //        Pop3Client pop3Client;
        //        if (Session["Pop3Client"] == null)
        //        {
        //            pop3Client = new Pop3Client();
        //            pop3Client.Connect("pop.cruzata.com", int.Parse("110"), false);
        //            pop3Client.Authenticate("abhishek.k@cruzata.com", "QdCcaOV6");
        //            Session["Pop3Client"] = pop3Client;
        //        }
        //        else
        //        {
        //            pop3Client = (Pop3Client)Session["Pop3Client"];
        //        }
        //        int count = pop3Client.GetMessageCount();
        //        //DataTable dtMessages = new DataTable();
        //        //dtMessages.Columns.Add("MessageNumber");
        //        //dtMessages.Columns.Add("From");
        //        //dtMessages.Columns.Add("Subject");
        //        //dtMessages.Columns.Add("DateSent");
        //        int counter = 0;
        //        for (int i = count; i >= 1; i--)
        //        {
        //            Mail objMail = new Mail();
        //            Message message = pop3Client.GetMessage(i);
        //            objMail.MailNo = i;
        //            objMail.From = message.Headers.From.ToString();
        //            objMail.Subject = message.Headers.Subject;
        //            objMail.Date = message.Headers.DateSent.ToString("d MMM yyyy");
        //            counter++;
        //            MailList.Mails.Add(objMail);
        //            if (counter > 20)
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //    }
            
        //    return View(MailList);
        //}

        //[Authorize]
        //[HttpPost]
        //public ActionResult ReadMessage(string MessageNo)
        //{
        //    Mail objModel = new Mail();
        //    Pop3Client pop3Client = (Pop3Client)Session["Pop3Client"];
        //   // int messageNumber = int.Parse(Request.QueryString["MessageNumber"]);
        //    int messageNumber = Convert.ToInt32(MessageNo);
        //    Message message = pop3Client.GetMessage(messageNumber);
        //    MessagePart messagePart = message.MessagePart.MessageParts[0];
        //    objModel.From = message.Headers.From.Address;
        //    objModel.Subject = message.Headers.Subject;
        //    objModel.Body = messagePart.BodyEncoding.GetString(messagePart.Body);

        //    return View(objModel);
        //}

    
        //public ActionResult MailBoxHome()
        //{
        //    session = new SessionHelper();
        //    List<TextValue> folders = new List<TextValue>();
        //    try
        //    {
        //        MailBoxRepo.MailBoxConnector objcon = new MailBoxRepo.MailBoxConnector("abhishekkhemariya29@gmail.com", "Abhi@prag@1249", "imap.gmail.com", true);
        //        folders = objcon.getFolders();
        //        ViewBag.folders = folders;
        //    }
        //    catch (Exception ex)
        //    {
        //        BAL.Common.LogManager.LogError("MailBoxHome", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //    }
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult GetSubfoder(string parentFolder)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

    }
}
