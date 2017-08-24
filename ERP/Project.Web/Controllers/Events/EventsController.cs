using Project.Entity;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Events
{
    public class EventsController : Controller
    {
        BAL.Events.EventManager objEventManager = new BAL.Events.EventManager();
        SessionHelper session;
        //
        // GET: /Events/
        [Authorize]
        public ActionResult Calender()
        {
            return View();
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult DeleteEvents(string Events_ID, string RelatedTable, string RelateToID)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                Response = objEventManager.DeleteEvent(Convert.ToInt64(Events_ID), Convert.ToInt64(RelateToID), RelatedTable, Convert.ToInt64(session.UserSession.UserId), Convert.ToInt64(session.UserSession.PIN));


                if (Response.ErrorCode == 0)
                {
                    if (RelatedTable == "LEAD")
                    {
                        LeadsModel objLeadModel = new LeadsModel();
                        objLeadModel.Events = objEventManager.getEventsByRelateToID(Convert.ToInt64(session.UserSession.PIN), session.UserSession.UserId, Convert.ToInt64(RelateToID), RelatedTable);                      
                        return View("AjaxEvents", objLeadModel);
                    }
                    else if (RelatedTable == "OPPORTUNITY")
                    {
                        OpportunityModel objOppoModel = new OpportunityModel();
                        objOppoModel.events = objEventManager.getEventsByRelateToID(Convert.ToInt64(session.UserSession.PIN), session.UserSession.UserId, Convert.ToInt64(RelateToID), RelatedTable);
                        return View("AjaxEvents", objOppoModel);
                    }
                    else
                    {
                        ClientModel objClientModel = new ClientModel();
                        objClientModel.Events = objEventManager.getEventsByRelateToID(Convert.ToInt64(session.UserSession.PIN), session.UserSession.UserId, Convert.ToInt64(RelateToID), RelatedTable);
                        return View("AjaxEvents", objClientModel);
                    }
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteEvents conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AddRelatedEvent(string Title, string StartDate, string EndDate, string RelatedTo, string RelatedTable, string uColour)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            BAL.Calender.CalenderManager objCalender = new BAL.Calender.CalenderManager();          
            try
            {
                List<string> temp1 = new List<string>();
                List<string> temp2 = new List<string>();


                temp1 = StartDate.Split(' ').ToList();
                temp2 = EndDate.Split(' ').ToList();


                DateTime fromDate = Convert.ToDateTime(temp1[0].Split('-').ToList()[2] + "/" + temp1[0].Split('-').ToList()[0] + "/" + temp1[0].Split('-').ToList()[1] + " " + temp1[1] + " " + temp1[2]);
                DateTime toDate = Convert.ToDateTime(temp2[0].Split('-').ToList()[2] + "/" + temp2[0].Split('-').ToList()[0] + "/" + temp2[0].Split('-').ToList()[1] + " " + temp2[1] + " " + temp2[2]).AddDays(1);

                //  DateTime fromDate = Convert.ToDateTime(StartDate);
                //  DateTime toDate = Convert.ToDateTime(EndDate).AddDays(1);
                //DateTime toDate = Convert.ToDateTime(EndDate);

              //  Response = objCalender.AddNewEvent(Convert.ToInt32(EventId), Title, fromDate, toDate, uColour, RelatedTo, RelatedTable, Convert.ToInt64(session.UserSession.UserId), Convert.ToInt64(session.UserSession.PIN));
                Response = objCalender.AddNewEvent(Title, fromDate, toDate, uColour, RelatedTo, RelatedTable, session.UserSession.UserId, Convert.ToInt64(session.UserSession.PIN));
                if (Response.ErrorCode == 0)
                {
                    if (RelatedTable == "LEAD")
                    {
                        LeadsModel objLeadModel = new LeadsModel();
                        objLeadModel.Events = objEventManager.getEventsByRelateToID(Convert.ToInt64(session.UserSession.PIN), session.UserSession.UserId, Convert.ToInt64(RelatedTo), RelatedTable);
                        return View("AjaxEvents", objLeadModel);
                    }
                    else if (RelatedTable == "OPPORTUNITY")
                    {
                        OpportunityModel objOppoModel = new OpportunityModel();
                        objOppoModel.Events = objEventManager.getEventsByRelateToID(Convert.ToInt64(session.UserSession.PIN), session.UserSession.UserId, Convert.ToInt64(RelatedTo), RelatedTable);
                        return View("AjaxEvents", objOppoModel);
                    }
                    else
                    {
                        ClientModel objClientModel = new ClientModel();
                        objClientModel.Events = objEventManager.getEventsByRelateToID(Convert.ToInt64(session.UserSession.PIN), session.UserSession.UserId, Convert.ToInt64(RelatedTo), RelatedTable);
                        return View("AjaxEvents", objClientModel);
                    }
                   
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AddRelatedEvent conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

    }
}
