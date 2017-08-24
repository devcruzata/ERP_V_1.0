using BAL.Meeting;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Meeting
{
    public class MeetingController : Controller
    {
        MeetingManager objMeetingManager = new MeetingManager();
        SessionHelper session;
        
        [Authorize]
        public ActionResult MeetingHome()
        {
            session = new SessionHelper();
            MeetingModel objMeetingModel = new MeetingModel();
            objMeetingModel.meetings = objMeetingManager.getAllMeetings(Convert.ToInt64(session.UserSession.PIN));
            return View(objMeetingModel);
        }
        

        //[Authorize]
        //[HttpPost]
        //public ActionResult AjaxAddMeeting(string Titele, string Priority,string StartDate, string EndDate,string RelateTo, string Description, string RemindMe)
        //{
        //    objResponse Response = new objResponse();
        //    MeetingModel objMeetingModel = new MeetingModel();
        //    session = new SessionHelper();
        //    try
        //    {
        //        Response = objMeetingManager.AddMeeting(Titele, BAL.Helper.Helper.ConvertToDateNullable(SheduledDate, "dd/MM/yyyy"), Convert.ToInt64(RelateTo), Agenda, RemindMe, Hours, Minutes, "Planed", session.UserSession.Username, Convert.ToInt64(session.UserSession.PIN),session.UserSession.UserId);

        //        if (Response.ErrorCode == 0)
        //        {                   
        //            return Json("Success", JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json("Fail", JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BAL.Common.LogManager.LogError("AjaxAddMeeting conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //        return Json("Fail", JsonRequestBehavior.AllowGet);
        //    }
            
        //}

        //[Authorize]
        //[HttpPost]
        //public ActionResult TempMeetingData()
        //{
        //    MeetingModel objMeetingModel = new MeetingModel();
        //    try
        //    {
        //        session = new SessionHelper();
        //        objMeetingModel.meetings = objMeetingManager.getMeetings(Convert.ToInt64(session.UserSession.PIN));
        //        return View("TempMeetingData", objMeetingModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("TempMeetingData", objMeetingModel);
        //    }

        //}

    }
}
