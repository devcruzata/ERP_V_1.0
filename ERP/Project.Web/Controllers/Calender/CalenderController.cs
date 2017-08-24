using Project.Entity;
using Project.ViewModel;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Calender
{
    public class CalenderController : Controller
    {
        BAL.Calender.CalenderManager objCalender = new BAL.Calender.CalenderManager();
        SessionHelper session;
        //
        // GET: /Calender/
        [Authorize]
        [SessionTimeOut]
        public ActionResult ManageCalender()
        {
            return View();
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        [ActionName("GetLeads")]
        public ActionResult GetLeads()
        {          

            List<TextValue> leads = objCalender.GetLeadForCalender();

            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Value = "0", Text = "Choose a Lead" });
            foreach (TextValue ld in leads)
            {
                list.Add(new SelectListItem { Value = ld.Value, Text = ld.Text });
            }

            JsonResult jResult = Json(list, JsonRequestBehavior.AllowGet);
            return jResult;
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public JsonResult SaveEvent(string Title, string StartDate, string EndDate, string RelatedTo ,string RelatedTable , string eColor)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                DateTime fromDate = Convert.ToDateTime(StartDate);
                //DateTime toDate = Convert.ToDateTime(EndDate).AddDays(-1);
                DateTime toDate = Convert.ToDateTime(EndDate);

                // Response = Response = objCalender.AddNewEvent(Title, BAL.Helper.Helper.ConvertToDateNullable(StartDate, "YYYY/MM/DD hh:mm a"), BAL.Helper.Helper.ConvertToDateNullable(StartDate, "YYYY/MM/DD hh:mm a"), EventColor,Description);
                Response = objCalender.AddNewEvent(Title, fromDate, toDate, eColor,RelatedTo, RelatedTable,session.UserSession.UserId,Convert.ToInt64(session.UserSession.PIN));
                if (Response.ErrorCode == 0)
                {
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("SaveEvent conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public JsonResult GetEventInfo(string EventId)
        {
            objResponse Response = new objResponse();
            Response = objCalender.GetEventInfo(Convert.ToInt32(EventId));
            List<string> temp1 = new List<string>();
            List<string> temp2 = new List<string>();
            EventModel objModel = new EventModel();

            objModel.RelatedToName = Response.ResponseData.Tables[0].Rows[0]["Name"].ToString();
            objModel.Relation_ID_Fk = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Relation_ID_FK"]);
            objModel.RelationType = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Relation_Type"]);
            objModel.RelatedToContact = Response.ResponseData.Tables[0].Rows[0]["Mobile"].ToString();
            objModel.RelatedToEmail = Response.ResponseData.Tables[0].Rows[0]["BusinessEmaol"].ToString();
            objModel.colour = Response.ResponseData.Tables[0].Rows[0]["Colour"].ToString();
            string LeadEventStart = Convert.ToDateTime(Response.ResponseData.Tables[0].Rows[0]["StartDate"]).ToString("MM/dd/yyyy hh:mm tt");

            temp1 = LeadEventStart.Split(' ').ToList();
            objModel.EventStartDate = temp1[0];
            objModel.EventStartTime = temp1[1] + " " + temp1[2];

            string LeadEventEnd = Convert.ToDateTime(Response.ResponseData.Tables[0].Rows[0]["EndDate"]).AddDays(-1).ToString("MM/dd/yyyy hh:mm tt");

            temp2 = LeadEventEnd.Split(' ').ToList();
            objModel.EventEndDate = temp2[0];
            objModel.EventEndTime = temp2[1] + " " + temp2[2];

            return Json(objModel, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public JsonResult UpdateEvntDate(string EventID, string StartDate, string EndDate)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                DateTime fromDate = Convert.ToDateTime(StartDate);
                //DateTime toDate = Convert.ToDateTime(EndDate).AddDays(-1);
                DateTime toDate = Convert.ToDateTime(EndDate);

                Response = Response = objCalender.UpdateEventDate(Convert.ToInt32(EventID), fromDate, toDate,session.UserSession.UserId,Convert.ToInt64(session.UserSession.PIN));
                if (Response.ErrorCode == 0)
                {
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("UpdateEvntDate conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        public JsonResult GetEvents(string start, string end)
        {
            var ApptListForDate = objCalender.LoadAppointmentSummaryInDateRange(start, end);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                color = e.EventColor,
                                allDay = false,
                                currentTimezone = "local",
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public JsonResult UpdateEvent(string Title, string StartDate, string EndDate, string DateFlag, string EventId, string RelatedTo, string RelatedTable, string uColour)
        {  
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                List<string> temp1 = new List<string>();
                List<string> temp2 = new List<string>();

                 
                   temp1= StartDate.Split(' ').ToList(); 
                   temp2= EndDate.Split(' ').ToList();
                   

                DateTime fromDate = Convert.ToDateTime(temp1[0].Split('-').ToList()[2]+"/"+temp1[0].Split('-').ToList()[0]+"/"+temp1[0].Split('-').ToList()[1]+" "+temp1[1]+" "+temp1[2]);
                DateTime toDate = Convert.ToDateTime(temp2[0].Split('-').ToList()[2]+"/"+temp2[0].Split('-').ToList()[0]+"/"+temp2[0].Split('-').ToList()[1]+" "+temp2[1]+" "+temp2[2]).AddDays(1);
                
                  //  DateTime fromDate = Convert.ToDateTime(StartDate);
                  //  DateTime toDate = Convert.ToDateTime(EndDate).AddDays(1);
                    //DateTime toDate = Convert.ToDateTime(EndDate);

                Response = Response = objCalender.UpdateEventType1(Convert.ToInt32(EventId), Title, fromDate, toDate,uColour ,RelatedTo, RelatedTable, Convert.ToInt64(session.UserSession.UserId), Convert.ToInt64(session.UserSession.PIN));
                    if (Response.ErrorCode == 0)
                    {
                        return Json("1", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("0", JsonRequestBehavior.AllowGet);
                    }           

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("UpdateEvent conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public JsonResult DeleteEvent(string EventId)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                Response = Response = objCalender.DeleteEvent(Convert.ToInt32(EventId));
                if (Response.ErrorCode == 0)
                {
                    return Json("1", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteEvent conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpPost]
        //public List<string> GetLeads(string term)
        //{
        //    List<string> leadlist = new List<string>();
        //    objResponse Rewsponse = new objResponse();

          
        //    leadlist = objCalender.GetLeadForCalender(term);
        //    //var leads = (from lead in leadlist
        //    //             select new
        //    //             {
        //    //                 label = lead.Text,
        //    //                 val = lead.Value
        //    //             }).ToList();
        //    return leadlist;
           

        //}

    }
}
