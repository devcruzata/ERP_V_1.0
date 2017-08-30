using BAL.Common;
using BAL.Document;
using BAL.Events;
using BAL.Meeting;
using BAL.Note;
using BAL.Task;
using BAL.User;
using Project.Entity;
using Project.ViewModel;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Leads
{
    public class LeadsController : Controller
    {
        BAL.Leads.LeadsManager objLeadsManager = new BAL.Leads.LeadsManager();
        BAL.Setings.SetingManager objSetingManager = new BAL.Setings.SetingManager();
        SessionHelper session;
        //
        // GET: /Leads/
        [Authorize]
        [SessionTimeOut]
        public ActionResult LeadHome()
        {
            session = new SessionHelper();
            LeadsModel objLeadmodel = new LeadsModel();
            try
            {
                
                objLeadmodel.leads = objLeadsManager.getLeads(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(session.UserSession.UserId), session.UserSession.UserType);
                UserManager objUserManager = new UserManager();
                List<Users> UserList = new List<Users>();
                UserList = objUserManager.GetUsers(session.UserSession.PIN);

                List<SelectListItem> list2 = new List<SelectListItem>();
                list2.Add(new SelectListItem { Value = "0", Text = "Choose A User" });

                foreach (var user in UserList)
                {
                    list2.Add(new SelectListItem { Value = user.User_ID_PK.ToString(), Text = user.FullName });
                }

                ViewBag.Users = list2;
               // ViewBag.leads = objLeadmodel.leads;
                return View(objLeadmodel);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("MLeadHome Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View();
            }
            
            
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult test()
        {
            session = new SessionHelper();
            LeadsModel objLeadmodel = new LeadsModel();
            try
            {
                objLeadmodel.leads = objLeadsManager.getLeads(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(session.UserSession.UserId), session.UserSession.UserType);
                UserManager objUserManager = new UserManager();
                List<Users> UserList = new List<Users>();
                UserList = objUserManager.GetUsers(session.UserSession.PIN);

                List<SelectListItem> list2 = new List<SelectListItem>();
                list2.Add(new SelectListItem { Value = "0", Text = "Choose A User" });

                foreach (var user in UserList)
                {
                    list2.Add(new SelectListItem { Value = user.User_ID_PK.ToString(), Text = user.FullName });
                }

                ViewBag.Users = list2;
                // ViewBag.leads = objLeadmodel.leads;
                return View(objLeadmodel);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("MLeadHome Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objLeadmodel);
            }


        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult NewLeads()
        {

            session = new SessionHelper();
            LeadsModel model = new LeadsModel();
            try
            {
                model.leads = objLeadsManager.getNewLeads(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(session.UserSession.UserId), session.UserSession.UserType);
                return View(model);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("NewLeads Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
            
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult WorkingLeads()
        {

            session = new SessionHelper();
            LeadsModel model = new LeadsModel();
            try
            {
                model.leads = objLeadsManager.getLeads(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(session.UserSession.UserId), session.UserSession.UserType);
                return View(model);
             
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("WorkingLeads Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult NotRepliedLeads()
        {

            session = new SessionHelper();
            LeadsModel model = new LeadsModel();
            try
            {
                model.leads = objLeadsManager.getNotRepliedLeads(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(session.UserSession.UserId), session.UserSession.UserType);
                return View(model);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("NotRepliedLeads Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AjaxAssignLead(string Lead_ID, string User_ID)
        {

            objResponse Response = new objResponse();            
            session = new Common.SessionHelper();            
            LeadsModel model = new LeadsModel();
            try
            {

                string[] Lead_ID_PK = Lead_ID.Split(',');

                for (int i = 1; i < Lead_ID_PK.Length; i++)
                {
                    if (Lead_ID_PK[i].ToString() != "")
                    {
                        Response = objLeadsManager.AssignLead(Convert.ToInt64(Lead_ID_PK[i]), Convert.ToInt64(User_ID), Convert.ToInt64(session.UserSession.PIN));

                        if (Response.ErrorCode != 0)
                        {
                            break;
                        }
                    }                    
                }               

                model.leads = objLeadsManager.getNewLeads(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(session.UserSession.UserId), session.UserSession.UserType);
                return View("NewLeads", model);             
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAssignLead Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
              
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult ManageLead()        {
            session = new SessionHelper();
            List<TextValue> source = new List<TextValue>();
            source = objSetingManager.GetDropDownListing("Lead","Source",Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var cat in source)
            {
                list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
            }
            ViewBag.Source_List = list;
            return View();
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult ManageLead(LeadsModel objLeadmodel)
        {
            
            objResponse Response = new objResponse();
            Project.Entity.Leads objLead = new Entity.Leads();
            session = new SessionHelper();

            List<TextValue> source = new List<TextValue>();
            source = objSetingManager.GetDropDownListing("Lead", "Source", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var cat in source)
            {
                list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
            }


            try
            {
                objLead.Date = DateTime.Now;             
                objLead.Name = objLeadmodel.Name;
                objLead.CompanyName = objLeadmodel.CompanyName;
                objLead.Email = objLeadmodel.Email;
                objLead.ContactNo = objLeadmodel.ContactNo;
                objLead.SkypeNo = objLeadmodel.SkypeNo;
                objLead.Category_ID = objLeadmodel.Category.ToString();                
                objLead.ZipCode = objLeadmodel.ZipCode;              
                objLead.AddressLine1 = objLeadmodel.AddressLine1;
                objLead.AddressLine2 = objLeadmodel.AddressLine2;
                objLead.City = objLeadmodel.City;
                objLead.State = objLeadmodel.State;
                objLead.Country = objLeadmodel.Country;
                objLead.Alternate_Email = objLeadmodel.Alternate_Email;
                objLead.Source = objLeadmodel.Source;
                objLead.JobDescription = objLeadmodel.JobDescription;                
                session = new SessionHelper();
                Response = objLeadsManager.AddLead(objLead, Convert.ToInt64(session.UserSession.UserId), Convert.ToInt64(session.UserSession.PIN));

                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "Lead with same Email Already Exists")
                    {                       
                         return RedirectToRoute("LeadHome");  
                        
                       
                        
                    }
                    else
                    {
                        ViewBag.Error_Msg = Response.ErrorMessage;
                        ViewBag.Source_List = list;
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error_Msg = Response.ErrorMessage;
                    ViewBag.Source_List = list;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = Response.ErrorMessage;
                ViewBag.Source_List = list;
                BAL.Common.LogManager.LogError("ManageLead Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View();
            }
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult ViewLead(string sessionid , string Leadid)
        {
            MeetingManager objMeetingManager = new MeetingManager();
            TaskManager objTaskManager = new TaskManager();
            DocumentManager objDocManager = new DocumentManager();
            NoteManager objNoteManager = new NoteManager();
            EventManager objEventManager = new EventManager();
            objResponse Response = new objResponse();            
            session = new SessionHelper();
            List<TextValue> source = new List<TextValue>();
            source = objSetingManager.GetDropDownListing("Lead", "Source", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var cat in source)
            {
                list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
            }

            UserManager objUserManager = new UserManager();
            List<Users> UserList = new List<Users>();
            UserList = objUserManager.GetUsers(session.UserSession.PIN);
           

            LeadsModel objLeadModel = new LeadsModel();
            try
            {
                Response = objLeadsManager.ViewLeads(Leadid);

                if (Response.ErrorCode == 0)
                {
                    objLeadModel.Lead_ID = Convert.ToInt64(Leadid);
                    objLeadModel.Date = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Date"]);
                    objLeadModel.FollowUpDate = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["FutureFollowUp"]);
                    objLeadModel.Name = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Name"]);
                    objLeadModel.CompanyName = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["CompanyName"]);
                    objLeadModel.Email = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Email"]);
                    objLeadModel.ContactNo = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["ContactNo"]);
                    objLeadModel.SkypeNo = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["SkypeNo"]);                   
                    objLeadModel.Status = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Status"]);
                    objLeadModel.Source = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Source"]);
                    objLeadModel.AddressLine1 = Response.ResponseData.Tables[0].Rows[0]["AddressLine1"].ToString();
                    objLeadModel.AddressLine2 = Response.ResponseData.Tables[0].Rows[0]["AddressLine2"].ToString();
                    objLeadModel.City = Response.ResponseData.Tables[0].Rows[0]["City"].ToString(); ;
                    objLeadModel.State = Response.ResponseData.Tables[0].Rows[0]["State"].ToString();
                    objLeadModel.Country = Response.ResponseData.Tables[0].Rows[0]["Country"].ToString();
                    objLeadModel.ZipCode = Response.ResponseData.Tables[0].Rows[0]["Zipcode"].ToString();
                    objLeadModel.JobDescription = Response.ResponseData.Tables[0].Rows[0]["JobDescription"].ToString();
                    objLeadModel.Alternate_Email = Response.ResponseData.Tables[0].Rows[0]["Alternate_Email"].ToString();
                    objLeadModel.CreatedBy = Response.ResponseData.Tables[0].Rows[0]["Owner"].ToString();

                  //  objLeadModel.Events = objMeetingManager.getMeetingsByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Leadid), session.UserSession.UserId);
                    objLeadModel.Events = objEventManager.getEventsByRelateToID(Convert.ToInt64(session.UserSession.PIN),session.UserSession.UserId,Convert.ToInt64(Leadid),"LEAD");
                    objLeadModel.Activity = UtilityManager.getActivityByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Leadid), "LEAD");
                    objLeadModel.Task = objTaskManager.getTasksByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Leadid), session.UserSession.UserId, "LEAD");
                    objLeadModel.Doc = objDocManager.getDocsRelatedToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Leadid), "LEAD", session.UserSession.UserId);
                    objLeadModel.Notes = objNoteManager.getNotesByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Leadid), session.UserSession.UserId, "LEAD");
                    ViewBag.Source_List = list;
                    ViewBag.Users = UserList;
                    return View(objLeadModel);
                }
                else
                {
                    ViewBag.Source_List = list;
                    ViewBag.Users = UserList;
                    ViewBag.Error_Msg = "There is error in Fetching Lead Details";
                    return View(objLeadModel);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Source_List = list;
                ViewBag.Users = UserList;
                ViewBag.Error_Msg = ex.Message.ToString(); ;
                BAL.Common.LogManager.LogError("ViewLead Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objLeadModel);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult UpdateLeadDetails(string LeadID , string status , string Note , string Followupdate)
        {
            session = new SessionHelper();
            objResponse Response = new objResponse();
            try
            {
                DateTime FollowUp = BAL.Helper.Helper.ConvertToDateNullable(Followupdate , "dd/MM/yyyy");
                Response = objLeadsManager.UpdateLeadDetails(Convert.ToInt64(LeadID), status, Note, FollowUp, session.UserSession.UserId);

                if (Response.ErrorCode == 0)
                {
                    LeadsModel model = new LeadsModel();
                    model.leads = objLeadsManager.getLeads(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(session.UserSession.UserId), session.UserSession.UserType);
                    //if (status == "NEW")
                    //{
                    //    return View("NewLeads", model);
                    //}
                    //else if (status == "WORKING")
                    //{
                    //    return View("WorkingLeads", model);
                    //}
                    //else
                    //{
                    //    return View("WorkingLeads", model);
                    //}
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = ex.Message.ToString(); ;
                BAL.Common.LogManager.LogError("UpdateLeadDetils Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult UpdateLeads(string Lead_ID_Pk)
        {
            objResponse Response = new objResponse();
            LeadsModel model = new LeadsModel();
            session = new SessionHelper();

            List<TextValue> source = new List<TextValue>();
            source = objSetingManager.GetDropDownListing("Lead", "Source", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var cat in source)
            {
                list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
            }

            try
            {
                Response = objLeadsManager.getLeadForUpdate(Convert.ToInt64(Lead_ID_Pk));

                if (Response.ErrorCode == 0)
                {
                    model.Lead_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Lead_ID_Auto_PK"]);              
                    model.Name = Response.ResponseData.Tables[0].Rows[0]["Name"].ToString();
                    model.CompanyName = Response.ResponseData.Tables[0].Rows[0]["CompanyName"].ToString();
                    model.Email = Response.ResponseData.Tables[0].Rows[0]["Email"].ToString();
                    model.Alternate_Email = Response.ResponseData.Tables[0].Rows[0]["Alternate_Email"].ToString();
                    model.ContactNo = Response.ResponseData.Tables[0].Rows[0]["ContactNo"].ToString();
                    model.SkypeNo = Response.ResponseData.Tables[0].Rows[0]["SkypeNo"].ToString(); ;
                    model.AddressLine1 = Response.ResponseData.Tables[0].Rows[0]["AddressLine1"].ToString();
                    model.AddressLine2 = Response.ResponseData.Tables[0].Rows[0]["AddressLine2"].ToString();
                    model.City = Response.ResponseData.Tables[0].Rows[0]["City"].ToString(); ;
                    model.State = Response.ResponseData.Tables[0].Rows[0]["State"].ToString();
                    model.Country = Response.ResponseData.Tables[0].Rows[0]["Country"].ToString();                   
                    model.ZipCode = Response.ResponseData.Tables[0].Rows[0]["Zipcode"].ToString();                    
                    model.Source = Response.ResponseData.Tables[0].Rows[0]["Source"].ToString();
                    model.JobDescription = Response.ResponseData.Tables[0].Rows[0]["JobDescription"].ToString();

                    ViewBag.Source_List = list;
                    return View(model);
                }
                else
                {
                    ViewBag.Source_List = list;
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Source_List = list;
                BAL.Common.LogManager.LogError("UpdateLeads Get Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(model);
            }            
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult UpdateLeads(LeadsModel objLeadmodel)
        {
            objResponse Response = new objResponse();
            Project.Entity.Leads objLead = new Entity.Leads();
            session = new SessionHelper();

            List<TextValue> source = new List<TextValue>();
            source = UtilityManager.GetSourceForDropDown(Convert.ToInt64(session.UserSession.PIN), "Client", "Source");

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var cat in source)
            {
                list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
            }
            string route = "/Leads/ViewLead?sessionid=" + Guid.NewGuid().ToString() + "&Leadid=" + objLeadmodel.Lead_ID;

            try
            {
                //objLead.Date = BAL.Helper.Helper.ConvertToDateNullable(objLeadmodel.Date, "dd/MM/yyyy");
                //  objLead.FollowUpDate = BAL.Helper.Helper.ConvertToDateNullable(objLeadmodel.FollowUpDate, "dd/MM/yyyy");
                objLead.Name = objLeadmodel.Name;
                objLead.CompanyName = objLeadmodel.CompanyName;
                objLead.Email = objLeadmodel.Email;
                objLead.ContactNo = objLeadmodel.ContactNo;
                objLead.SkypeNo = objLeadmodel.SkypeNo;              
                objLead.ZipCode = objLeadmodel.ZipCode;             
                objLead.AddressLine1 = objLeadmodel.AddressLine1;
                objLead.AddressLine2 = objLeadmodel.AddressLine2;
                objLead.City = objLeadmodel.City;
                objLead.State = objLeadmodel.State;
                objLead.Country = objLeadmodel.Country;
                objLead.Alternate_Email = objLeadmodel.Alternate_Email;
                objLead.Source = objLeadmodel.Source;
                objLead.JobDescription = objLeadmodel.JobDescription;
                objLead.Lead_ID_Auto_PK = objLeadmodel.Lead_ID;
                session = new SessionHelper();
                Response = objLeadsManager.UpdateLead(objLead, session.UserSession.UserId);

                if (Response.ErrorCode == 0)
                {
                    ViewBag.Source_List = list;
                   // return RedirectToRoute("LeadHome");
                    //return RedirectToRoute("ViewLead"); 
                    //return View(objLeadmodel);     
                    return Redirect(route);
                }
                else
                {
                    ViewBag.Error_Msg = Response.ErrorMessage;
                    ViewBag.Source_List = list;
                   // return RedirectToRoute("LeadHome");
                    //return RedirectToRoute("ViewLead");  
                    return View(objLeadmodel);

                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = Response.ErrorMessage;
                ViewBag.Source_List = list;
                BAL.Common.LogManager.LogError("UpdateLead Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
               // return RedirectToRoute("LeadHome");
                //return RedirectToRoute("ViewLead");  
                return View(objLeadmodel);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult DeleteLead(string Lead_ID_PK)
        {
            string response = "";
            try
            {
                response = objLeadsManager.DeleteLead(Lead_ID_PK);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteLead Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize]
        [SessionTimeOut]
        public ActionResult DownLoad(string file_path, string file_Name)
        {
            try
            {
                string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Lead_Req_Dir"]) + file_path;
                string contentType = "application/pdf";
                return File(newFilePath, contentType, file_Name);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("Download Req", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                 return View("500");
            }
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult Remove(string file_path, string file_Name, string Lead_id)
        {
            string response = "";
            try
            {
                string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Lead_Req_Dir"]) + file_path;
                if (System.IO.File.Exists(newFilePath))
                {
                    System.IO.File.Delete(newFilePath);
                }

                response = objLeadsManager.DeleteLeadUpload(Convert.ToInt64(Lead_id), file_Name);
                return RedirectToAction("UpdateLeads", "Leads", new { Lead_ID_Pk = Lead_id });
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteLeadUpload Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return RedirectToAction("UpdateLeads", "Leads", new { Lead_ID_Pk = Lead_id });
            }
        }

        //public ActionResult test()
        //{
        //    objResponse Response = new objResponse();
        //    LeadsModel model = new LeadsModel();
        //    session = new SessionHelper();
        //    List<TextValue> category = new List<TextValue>();
        //    category = UtilityManager.GetSourceForDropDown(Convert.ToInt64(session.UserSession.PIN), "Client", "Source");

        //    List<SelectListItem> list = new List<SelectListItem>();
        //    list.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

        //    foreach (var cat in category)
        //    {
        //        list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
        //    }
        //    ViewBag.Source_List = list;
        //    return View(model);
        //}

        //[Authorize]
        //[HttpPost]
        //public ActionResult AjaxUpdate(LeadsModel objLeadmodel, string TextField)
        //{
        //    objResponse Response = new objResponse();
        //    Project.Entity.Leads objLead = new Entity.Leads();
        //    try
        //    {
        //        if (objLeadmodel.Date != null)
        //        {
        //            objLead.Date = BAL.Helper.Helper.ConvertToDateNullable(objLeadmodel.Date, "dd/MM/yyyy");
        //        }                
             
        //        objLead.Name = objLeadmodel.Name;
        //        objLead.CompanyName = objLeadmodel.CompanyName;
        //        objLead.Email = objLeadmodel.Email;
        //        objLead.ContactNo = objLeadmodel.ContactNo;
        //        objLead.SkypeNo = objLeadmodel.SkypeNo;
        //        objLead.Category_ID = objLeadmodel.Category.ToString(); ;
        //        objLead.Model = objLeadmodel.Model;
        //        objLead.Requirement = objLeadmodel.Requirement;
        //        objLead.ZipCode = objLeadmodel.ZipCode;
               
        //        objLead.AddressLine1 = objLeadmodel.AddressLine1;
        //        objLead.AddressLine2 = objLeadmodel.AddressLine2;
        //        objLead.City = objLeadmodel.City;
        //        objLead.State = objLeadmodel.State;
        //        objLead.Country = objLeadmodel.Country;
        //        objLead.Alternate_Email = objLeadmodel.Alternate_Email;
        //        objLead.Source = objLeadmodel.Source;
        //        objLead.Lead_ID_Auto_PK = objLeadmodel.Lead_ID;
        //        session = new SessionHelper();
        //        Response = objLeadsManager.UpdateLead(objLead, session.UserSession.Username, TextField);


        //        return Json("Success", JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        BAL.Common.LogManager.LogError("AjaxUpdate Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //        return Json("Fail", JsonRequestBehavior.AllowGet);
        //    }
        //}

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AjaxActivity(string LeadId)
        {
            LeadsModel objLeadModel = new LeadsModel();
            session = new SessionHelper();
            try
            {
                objLeadModel.Activity = UtilityManager.getActivityByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(LeadId), "LEAD");
                return View(objLeadModel);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxActivity Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize]
        //[HttpPost]
        //public ActionResult TempMeetingData()
        //{
        //    MeetingModel objMeetingModel = new MeetingModel();
        //    MeetingManager objMeetingManager = new MeetingManager();
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

        [Authorize]
        [SessionTimeOut]
        public ActionResult testView()
        {
            return View();
        }

    }
}
