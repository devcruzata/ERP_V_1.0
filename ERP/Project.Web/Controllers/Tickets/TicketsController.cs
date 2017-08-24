using BAL.Ticket;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Tickets
{
    public class TicketsController : Controller
    {

        TicketManager objTctManager = new TicketManager();
        //
        // GET: /Tickets/

        [Authorize]
        [SessionTimeOut]
        public ActionResult TicketsHome()
        {
            TicketModel objTct = new TicketModel();
            objTct.ticket = objTctManager.getAllTickets();
            return View(objTct);
        }        

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult OpenTicket(string subject, string body)
        {
            SessionHelper session = new SessionHelper();
            objResponse Response = new objResponse();
            try
            {
                Response = objTctManager.OpenTicket(subject,body,session.UserSession.UserId);

                if (Response.ErrorCode == 0)
                {
                    DataTable dt = Response.ResponseData.Tables[0];
                    string mbody = BAL.Helper.MailBodyBuilder.PopulateTicketConfEmailBody(dt.Rows[0]["name"].ToString(), dt.Rows[0]["TicketNo"].ToString(), dt.Rows[0]["subject"].ToString(), dt.Rows[0]["status"].ToString(), ConfigurationManager.AppSettings["TctOpenEmailTmp"].ToString());
                    BAL.Helper.Helper.SendEmail(dt.Rows[0]["User_Email"].ToString(), "Clouderac Support", mbody);
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult CloseTicket(string ticketID)
        {
            objResponse Response = new objResponse();
            TicketModel objTctModel = new TicketModel();
            try
            {
                Response = objTctManager.closeTicket(Convert.ToInt64(ticketID));

                if (Response.ErrorCode == 0)
                {
                    DataTable dt = Response.ResponseData.Tables[0];
                    string mbody = BAL.Helper.MailBodyBuilder.PopulateTicketClosureEmailBody(dt.Rows[0]["TicketNo"].ToString(), ConfigurationManager.AppSettings["TctStCloseEmailTmp"].ToString());
                    BAL.Helper.Helper.SendEmail(dt.Rows[0]["User_Email"].ToString(), "Clouderac Support", mbody);
                    objTctModel.ticket = objTctManager.getAllTickets();
                    return View("AjaxTicket", objTctModel);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("CloseTicket post ", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult Deleteicket(string ticketID)
        {
            objResponse Response = new objResponse();
            TicketModel objTctModel = new TicketModel();
            try
            {
                Response = objTctManager.DeleteTicket(Convert.ToInt64(ticketID));

                if (Response.ErrorCode == 0)
                {
                    objTctModel.ticket = objTctManager.getAllTickets();
                    return View("AjaxTicket", objTctModel);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("Deleteicket post ", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }


    }
}
