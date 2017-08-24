using BAL.Mail;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using System.Web.Mvc;

namespace Project.Web.Controllers.Mails
{
    public class MailsController : Controller
    {
        MailManager objMailManager = new MailManager();
        SessionHelper session;
        //
        // GET: /Mails/

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult SendMail()
        {
            objResponse Response = new objResponse();
            Project.Entity.Mails objMail = new Project.Entity.Mails();
            session = new SessionHelper();

            try
            {
                //objMail.ToAddress = objMailModel.ToAddress;
                //objMail.RelateTo_ID = objMailModel.RelateTo_ID;
                //objMail.RelateTo_Name = objMailModel.RelateTo_Name;
                //objMail.CcAddress = objMailModel.CcAddress;
                //objMail.BccAddress = objMailModel.BccAddress;
                //objMail.FromAddress = objMailModel.FromAddress;
                //objMail.MailBy_ID = objMailModel.MailBy_ID;
                //objMail.MailBy_Name = objMailModel.MailBy_Name;
                //objMail.Subject = objMailModel.Subject;
                //objMail.MailBody = objMailModel.MailBody;
                //objMail.Date = DateTime.Now.ToString();

                //MailManager.SendSimpleMessageBySMTP("abhishekkhemariya29@gmail.com", "shabbir@cruzata.com", "abhishekkhemariya29@gmail.com", "abhishekkhemariya29@gmail.com", objMail.Subject, objMail.MailBody);

                //1.The ACCOUNT
                MailAddress fromAddress = new MailAddress(session.UserSetingSession.smtpUsername, "Clouderac");
                String fromPassword = session.UserSetingSession.smtpPassword;

                //2.The Destination email Addresses , CC Address , Bcc Address
                MailAddressCollection TO_addressList = new MailAddressCollection();
                MailAddressCollection CC_addressList = new MailAddressCollection();
                MailAddressCollection BCC_addressList = new MailAddressCollection();

                //3.Prepare the Destination email Addresses list , CC Address List and BCC Address List
                foreach (var curr_address in Request.Form["ToAddress"].ToString().Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    MailAddress mytoAddress = new MailAddress(curr_address);
                    TO_addressList.Add(mytoAddress);
                }

                foreach (var cc_address in Request.Form["ccAddress"].ToString().Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    MailAddress myccAddress = new MailAddress(cc_address);
                    CC_addressList.Add(myccAddress);
                }

                foreach (var Bcc_address in Request.Form["bccAddress"].ToString().Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    MailAddress myBccAddress = new MailAddress(Bcc_address);
                    BCC_addressList.Add(myBccAddress);
                }
                //4.Subject Email Body Message And Attahments
                String body = Request.Form["body"].ToString(); ;
                String Subject = Request.Form["Subject"].ToString();
                //HttpFileCollectionBase files = Request.Files;
                //  for (int i = 0; i < files.Count; i++)
                //  {
                //      HttpPostedFileBase uploadfile = files[i];
                //      string fileName = Path.GetFileName(uploadfile.FileName);
                //      if (uploadfile.ContentLength > 0)
                //      {
                //          uploadfile.SaveAs(Server.MapPath("~/Uploads/MailUpload/") + fileName);
                //      }
                //  }
                //  string Uplodefile = Request.PhysicalApplicationPath + "Uploads\\MailUpload\\";
                //  string[] S1 = Directory.GetFiles(Uplodefile);
                
            
                //5.Prepare GMAIL SMTP: with SSL on port 587
                //var smtp = new SmtpClient
                //{
                //    Host = "smtp.gmail.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtpUser"].ToString(), ConfigurationManager.AppSettings["smtpPass"].ToString()),
                //    Timeout = 30000
                //};

                var smtp = new SmtpClient
                {
                    Host = session.UserSetingSession.smtpHost,
                    Port = Convert.ToInt32(session.UserSetingSession.smtpPort),
                    EnableSsl = session.UserSetingSession.smtpIsSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(session.UserSetingSession.smtpUsername, session.UserSetingSession.smtpPassword),
                    Timeout = 30000
                };

                //6.Complete the message and SEND the email:
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
               
                    message.From = fromAddress;
                    message.Subject = Subject;
                    message.Body = body;
                    //foreach (string files in S1)
                    //{
                    //   message.Attachments.Add(new Attachment(files));
                    //}
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase uploadfile = files[i];
                        string fileName = Path.GetFileName(uploadfile.FileName);
                        message.Attachments.Add(new Attachment(uploadfile.InputStream, fileName));
                    }
                    //if (objMailModel.Attechments != null)
                    //{
                    //    foreach (var att in objMailModel.Attechments)
                    //    {
                    //        string fileName = Path.GetFileName(att.FileName);
                    //        message.Attachments.Add(new Attachment(att.InputStream, fileName));
                    //    }                        
                    //}
                    message.To.Add(TO_addressList.ToString());
                    if (CC_addressList.Count > 0)
                    {
                        message.CC.Add(CC_addressList.ToString());
                    }
                    if (BCC_addressList.Count > 0)
                    {
                        message.Bcc.Add(BCC_addressList.ToString());
                    }
                    
                    smtp.Send(message);

                    message.Dispose();
                    //System.IO.DirectoryInfo di = new DirectoryInfo(Uplodefile);

                    //foreach (FileInfo file in di.GetFiles())
                    //{
                    //    file.Delete();
                    //}   
                    
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("SendMail Contro Post", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("success", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult SendMailUsingGodaddy()
        {
            objResponse Response = new objResponse();
            Project.Entity.Mails objMail = new Project.Entity.Mails();


            try
            {
               

                //1.The ACCOUNT
                MailAddress fromAddress = new MailAddress("dev@cruzata.com", "Clouderac");
                String fromPassword = ConfigurationManager.AppSettings["smtpPass"].ToString();

                //2.The Destination email Addresses , CC Address , Bcc Address
                MailAddressCollection TO_addressList = new MailAddressCollection();
                MailAddressCollection CC_addressList = new MailAddressCollection();
                MailAddressCollection BCC_addressList = new MailAddressCollection();

                //3.Prepare the Destination email Addresses list , CC Address List and BCC Address List
                foreach (var curr_address in Request.Form["ToAddress"].ToString().Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    MailAddress mytoAddress = new MailAddress(curr_address);
                    TO_addressList.Add(mytoAddress);
                }

                foreach (var cc_address in Request.Form["ccAddress"].ToString().Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    MailAddress myccAddress = new MailAddress(cc_address);
                    CC_addressList.Add(myccAddress);
                }

                foreach (var Bcc_address in Request.Form["bccAddress"].ToString().Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    MailAddress myBccAddress = new MailAddress(Bcc_address);
                    BCC_addressList.Add(myBccAddress);
                }
                //4.Subject Email Body Message 
                String body = Request.Form["body"].ToString(); ;
                String Subject = Request.Form["Subject"].ToString();
               


                //5.Prepare GMAIL SMTP: with SSL on port 587
                var smtp = new SmtpClient
                {
                    Host = "relay-hosting.secureserver.net",
                    Port = 25,                    
                    EnableSsl = false,
                    UseDefaultCredentials = true,
                   // Credentials = new NetworkCredential(ConfigurationManager.AppSettings["smtpUser"].ToString(), ConfigurationManager.AppSettings["smtpPass"].ToString()),
                   // Timeout = 30000
                };


                //6.Complete the message and SEND the email:
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

                message.From = fromAddress;
                message.Subject = Subject;
                message.Body = body;
                message.Priority = System.Net.Mail.MailPriority.High;
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase uploadfile = files[i];
                    string fileName = Path.GetFileName(uploadfile.FileName);
                    message.Attachments.Add(new Attachment(uploadfile.InputStream, fileName));
                }
                
                message.To.Add(TO_addressList.ToString());

                if (CC_addressList.Count > 0)
                {
                    message.CC.Add(CC_addressList.ToString());
                }
                if (BCC_addressList.Count > 0)
                {
                    message.Bcc.Add(BCC_addressList.ToString());
                }

                smtp.Send(message);

                message.Dispose();
                //System.IO.DirectoryInfo di = new DirectoryInfo(Uplodefile);

                //foreach (FileInfo file in di.GetFiles())
                //{
                //    file.Delete();
                //}   

                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("SendMail Contro Post", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("success", JsonRequestBehavior.AllowGet);
            }
        }

    }
}
