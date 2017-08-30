using BAL.Common;
using BAL.Document;
using BAL.Events;
using BAL.Meeting;
using BAL.Note;
using BAL.Opportunity;
using BAL.Setings;
using BAL.Task;
using BAL.User;
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

namespace Project.Web.Controllers.Opportunity
{
    public class OpportunityController : Controller
    {
        OpportunityManager objOpportunityManager = new OpportunityManager();
        SetingManager objSetingManager = new SetingManager();
        SessionHelper session;
        //
        // GET: /Opportunity/

        [Authorize]
        [SessionTimeOut]
        public ActionResult OpportunityHome()
        {
            session = new SessionHelper();
            OpportunityModel objOpportuNityModel = new OpportunityModel();

            UserManager objUserManager = new UserManager();
            List<Users> UserList = new List<Users>();

            List<TextValue> stage = new List<TextValue>();
            stage = objSetingManager.GetDropDownListing("Opportunity", "Status", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list3 = new List<SelectListItem>();
            list3.Add(new SelectListItem { Value = "0", Text = "Choose a Stage" });

            foreach (var stg in stage)
            {
                list3.Add(new SelectListItem { Value = stg.Value, Text = stg.Text });
            }

            
            UserList = objUserManager.GetUsers(session.UserSession.PIN);

            List<SelectListItem> list2 = new List<SelectListItem>();
            list2.Add(new SelectListItem { Value = "0", Text = "Choose A User" });

            foreach (var user in UserList)
            {
                list2.Add(new SelectListItem { Value = user.User_ID_PK.ToString(), Text = user.FullName });
            }

            objOpportuNityModel.Opportunity = objOpportunityManager.getAllOpportunities(Convert.ToInt64(session.UserSession.PIN));
            ViewBag.Users = list2;
            ViewBag.Stage_List = list3;
            return View(objOpportuNityModel);
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult ManageOpportunity()
        {
            session = new SessionHelper();
            List<TextValue> source = new List<TextValue>();
          
            source = objSetingManager.GetDropDownListing("Opportunity", "Source", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var cat in source)
            {
                list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
            }
            List<TextValue> stage = new List<TextValue>();
            stage = objSetingManager.GetDropDownListing("Opportunity", "Status", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list3 = new List<SelectListItem>();
            list3.Add(new SelectListItem { Value = "0", Text = "Choose a Stage" });

            foreach(var stg in stage)
            {
                list3.Add(new SelectListItem { Value = stg.Value, Text = stg.Text });
            }

            List<TextValue> reason = new List<TextValue>();
            reason = objSetingManager.GetDropDownListing("Opportunity", "Lost Reason", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list4 = new List<SelectListItem>();
            list4.Add(new SelectListItem { Value = "0", Text = "Please Choose A Lost Reason" });

            foreach (var rsn in reason)
            {
                list4.Add(new SelectListItem { Value = rsn.Value, Text = rsn.Text });
            }

            List<TextValue> client = new List<TextValue>();
            client = UtilityManager.GetClientsForDropDown();

            List<SelectListItem> list2 = new List<SelectListItem>();
            list2.Add(new SelectListItem { Value = "0", Text = "Choose A Client" });

            foreach (var clients in client)
            {
                list2.Add(new SelectListItem { Value = clients.Value, Text = clients.Text });
            }

            UserManager objUserManager = new UserManager();
            List<Users> UserList = new List<Users>();
            UserList = objUserManager.GetUsers(session.UserSession.PIN);

            ViewBag.Users = UserList;
            ViewBag.Client = list2;
            ViewBag.Source_List = list;
            ViewBag.Stage_List = list3;
            ViewBag.Reason_List = list4;
            return View();
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult ManageOpportunity(OpportunityModel objModel)
        {
            session = new SessionHelper();
            Opportunities objOpportunities = new Opportunities();
            objResponse Response = new objResponse();
            List<TextValue> source = new List<TextValue>();
            source = objSetingManager.GetDropDownListing("Opportunity", "Source", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var cat in source)
            {
                list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
            }

            List<TextValue> stage = new List<TextValue>();
            stage = objSetingManager.GetDropDownListing("Opportunity", "Status", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list3 = new List<SelectListItem>();
            list3.Add(new SelectListItem { Value = "0", Text = "Choose a Stage" });

            foreach (var stg in stage)
            {
                list3.Add(new SelectListItem { Value = stg.Value, Text = stg.Text });
            }

            List<TextValue> reason = new List<TextValue>();
            reason = objSetingManager.GetDropDownListing("Opportunity", "Lost Reason", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list4 = new List<SelectListItem>();
            list4.Add(new SelectListItem { Value = "0", Text = "Please Choose A Lost Reason" });

            foreach (var rsn in reason)
            {
                list4.Add(new SelectListItem { Value = rsn.Value, Text = rsn.Text });
            }

            List<TextValue> client = new List<TextValue>();
            client = UtilityManager.GetClientsForDropDown();

            List<SelectListItem> list2 = new List<SelectListItem>();
            list2.Add(new SelectListItem { Value = "0", Text = "Choose A Client" });

            foreach (var clients in client)
            {
                list2.Add(new SelectListItem { Value = clients.Value, Text = clients.Text });
            }

            UserManager objUserManager = new UserManager();
            List<Users> UserList = new List<Users>();
            UserList = objUserManager.GetUsers(session.UserSession.PIN);

            try
            {
                objOpportunities.Name = objModel.Name;
                objOpportunities.Opportunity_Owner = objModel.Opportunity_Owner;
                objOpportunities.Amount = objModel.Amount;
                objOpportunities.RealateTo_ID = objModel.RealateTo_ID;
                if (objModel.ExpectedCloseDate != null)
                {
                    objOpportunities.ExpectedCloseDate = BAL.Helper.Helper.ConvertToDateNullable(objModel.ExpectedCloseDate, "dd/MM/yyyy"); 
                }
                
                objOpportunities.Stage = objModel.Stage;
                objOpportunities.Type = objModel.Type;
                objOpportunities.Source = objModel.Source;
                objOpportunities.Probability = objModel.Probability;
                objOpportunities.LostReason = objModel.LostReason;

                if (objModel.AssignTO_ID == 0)
                {
                    objOpportunities.AssignTO_ID = Convert.ToInt64(session.UserSession.UserId);
                }
                else
                {
                    objOpportunities.AssignTO_ID = objModel.AssignTO_ID;
                }

                objOpportunities.Description = objModel.Description;
                Response = objOpportunityManager.AddOpportunity(objOpportunities,session.UserSession.UserId,Convert.ToInt64(session.UserSession.PIN));

                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "Opportunity With Same Name already Exists")
                    {
                        return RedirectToRoute("OpportunityHome");
                    }
                    else
                    {
                        ViewBag.Users = UserList;
                        ViewBag.Source_List = list;
                        ViewBag.Client = list2;
                        ViewBag.Stage_List = list3;
                        ViewBag.Reason_List = list4;
                        ViewBag.Error_Msg = Response.ErrorMessage;
                        return View();
                    }
                }
                else
                {
                    ViewBag.Users = UserList;
                    ViewBag.Source_List = list;
                    ViewBag.Client = list2;
                    ViewBag.Stage_List = list3;
                    ViewBag.Reason_List = list4;
                    ViewBag.Error_Msg = "There is an error in processing your request. Please try again.";
                    return View();
                }
               
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AddOpportunity", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                ViewBag.Users = UserList;
                ViewBag.Source_List = list;
                ViewBag.Client = list2;
                ViewBag.Stage_List = list3;
                ViewBag.Reason_List = list4;
                ViewBag.Error_Msg = "There is an error in processing your request. Please try again.";
                BAL.Common.LogManager.LogError("ManageOpportunity Post", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
               
                return View();
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public JsonResult AutoClientSearch(string searchText)
        {
            List<TextValue> namelist = new List<TextValue>();
            session = new SessionHelper();
            try
            {
                namelist = objOpportunityManager.getClientOnSearch(Convert.ToInt64(session.UserSession.PIN),searchText);
                return Json(namelist, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AutoClientSearch", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(namelist, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult ViewOpportunity(string sessionid , string Opportunityid)
        {
            objResponse Response = new objResponse();
            OpportunityModel objOpportunity = new OpportunityModel();          
            TaskManager objTaskManager = new TaskManager();
            DocumentManager objDocManager = new DocumentManager();
            NoteManager objNoteManager = new NoteManager();
            EventManager objEventManager = new EventManager();
            session = new SessionHelper();

            UserManager objUserManager = new UserManager();
            List<Users> UserList = new List<Users>();
            UserList = objUserManager.GetUsers(session.UserSession.PIN);
            try
            {
                Response = objOpportunityManager.ViewOpportunities(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Opportunityid));
                if (Response.ErrorCode == 0)
                {
                    objOpportunity.Opportunity_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Opportunity_ID_Auto_PK"]);
                    objOpportunity.Name = Response.ResponseData.Tables[0].Rows[0]["Name"].ToString();
                    objOpportunity.Amount = Response.ResponseData.Tables[0].Rows[0]["Amount"].ToString();
                    objOpportunity.RelateTo_Name = Response.ResponseData.Tables[0].Rows[0]["RealtedTo"].ToString();
                    objOpportunity.RealateTo_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Relate_To_ID_FK"]);
                    objOpportunity.RelateTo_ContactNo = Response.ResponseData.Tables[0].Rows[0]["RelateToContactNo"].ToString();
                    objOpportunity.RelateTo_Email = Response.ResponseData.Tables[0].Rows[0]["RelateToContactEmail"].ToString();
                    objOpportunity.Stage = Response.ResponseData.Tables[0].Rows[0]["Stage"].ToString();
                    objOpportunity.Type = Response.ResponseData.Tables[0].Rows[0]["Type"].ToString();
                    objOpportunity.Probability = Response.ResponseData.Tables[0].Rows[0]["Probability"].ToString();
                    objOpportunity.AssignTO_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["AssignTo"]);
                    objOpportunity.AssignTO_Name = Response.ResponseData.Tables[0].Rows[0]["AssignToName"].ToString();
                    objOpportunity.Description = Response.ResponseData.Tables[0].Rows[0]["Description"].ToString();
                    objOpportunity.Source = Response.ResponseData.Tables[0].Rows[0]["Source"].ToString();
                    objOpportunity.Opportunity_Owner = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["CreatedBy"]);
                    objOpportunity.Opportunity_Owner_Name = Response.ResponseData.Tables[0].Rows[0]["OwnerName"].ToString();

                    objOpportunity.Events = objEventManager.getEventsByRelateToID(Convert.ToInt64(session.UserSession.PIN), session.UserSession.UserId, Convert.ToInt64(Opportunityid), "OPPORTUNITY");
                    objOpportunity.activities = UtilityManager.getActivityByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Opportunityid), "OPPORTUNITY");
                    objOpportunity.Task = objTaskManager.getTasksByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Opportunityid), session.UserSession.UserId, "OPPORTUNITY");
                    objOpportunity.Doc = objDocManager.getDocsRelatedToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Opportunityid), "OPPORTUNITY", session.UserSession.UserId);
                    objOpportunity.Notes = objNoteManager.getNotesByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Opportunityid), session.UserSession.UserId, "OPPORTUNITY");

                    ViewBag.Users = UserList;
                    return View(objOpportunity);
                }
                else
                {
                    ViewBag.Users = UserList;
                    return View(objOpportunity);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Users = UserList;
                BAL.Common.LogManager.LogError("ViewOpportunity", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objOpportunity);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AjaxUpdate(OpportunityModel objOpportunitymodel, string TextField)
        {
            objResponse Response = new objResponse();
            Project.Entity.Opportunities objOpportunity = new Entity.Opportunities();
            try
            {
                if (objOpportunitymodel.ExpectedCloseDate != null)
                {
                    objOpportunity.ExpectedCloseDate = BAL.Helper.Helper.ConvertToDateNullable(objOpportunitymodel.ExpectedCloseDate, "dd/MM/yyyy");
                }

                objOpportunity.Name = objOpportunitymodel.Name;
                objOpportunity.Amount = objOpportunitymodel.Amount;
                objOpportunity.RealateTo_ID = objOpportunitymodel.RealateTo_ID;
                objOpportunity.Opportunity_Owner = objOpportunitymodel.Opportunity_Owner;
                objOpportunity.Stage = objOpportunitymodel.Stage;
                objOpportunity.Type = objOpportunitymodel.Type;
                objOpportunity.Probability = objOpportunitymodel.Probability;
                objOpportunity.LostReason = objOpportunitymodel.LostReason;
                objOpportunity.Source = objOpportunitymodel.Source;
                objOpportunity.AssignTO_ID = objOpportunitymodel.AssignTO_ID;
                objOpportunity.Description = objOpportunitymodel.Description;
               
                session = new SessionHelper();
              //  Response = objLeadsManager.UpdateLead(objOpportunity, session.UserSession.Username, TextField);


                return Json("Success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxUpdate Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AjaxOppoActivity(string OpportunityID)
        {
            OpportunityModel objOpportunityModel = new OpportunityModel();
            session = new SessionHelper();
            try
            {
                objOpportunityModel.activities = UtilityManager.getActivityByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(OpportunityID), "OPPORTUNITY");
                return View(objOpportunityModel);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxActivity Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult ChangeStatus(string OpportunityId, string status, string Note)
        {
            session = new SessionHelper();
            objResponse Response = new objResponse();
            OpportunityModel model = new OpportunityModel();
            try
            {
                //List<TextValue> stage = new List<TextValue>();
                //stage = objSetingManager.GetDropDownListing("Opportunity", "Status", Convert.ToInt64(session.UserSession.PIN));

                //List<SelectListItem> list3 = new List<SelectListItem>();
                //list3.Add(new SelectListItem { Value = "0", Text = "Choose a Stage" });

                //foreach (var stg in stage)
                //{
                //    list3.Add(new SelectListItem { Value = stg.Value, Text = stg.Text });
                //}
             
                Response = objOpportunityManager.ChangeStatus(Convert.ToInt64(OpportunityId), status, Note, session.UserSession.UserId);

                if (Response.ErrorCode == 0)
                {
                   // OpportunityModel objOpportuNityModel = new OpportunityModel();
                    //objOpportuNityModel.Opportunity = objOpportunityManager.getAllOpportunities(Convert.ToInt64(session.UserSession.PIN));
                  //  ViewBag.Stage_List = list3;
                    model.Opportunity = objOpportunityManager.getAllOpportunities(Convert.ToInt64(session.UserSession.PIN));
                    return View("AjOpportunity", model);
                   // return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                   // ViewBag.Stage_List = list3;
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
              
                ViewBag.Error_Msg = ex.Message.ToString(); ;
                BAL.Common.LogManager.LogError("ChangeStatus Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult DeleteOpportunity(string Opportunity_ID_PK)
        {
            string response = "";
            session = new SessionHelper();
            OpportunityModel model = new OpportunityModel();
            try
            {
                response = objOpportunityManager.DeleteOpportunity(Convert.ToInt64(Opportunity_ID_PK));

                model.Opportunity = objOpportunityManager.getAllOpportunities(Convert.ToInt64(session.UserSession.PIN));
                return View("AjOpportunity", model);
             
               
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteOpportunity Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult UpdateOpportunity(string Opportunity_ID_PK)
        {
            session = new SessionHelper();
            OpportunityModel objOpportunity = new OpportunityModel();
            objResponse Response = new objResponse();
            List<TextValue> source = new List<TextValue>();

            source = objSetingManager.GetDropDownListing("Opportunity", "Source", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var cat in source)
            {
                list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
            }
            List<TextValue> stage = new List<TextValue>();
            stage = objSetingManager.GetDropDownListing("Opportunity", "Status", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list3 = new List<SelectListItem>();
            list3.Add(new SelectListItem { Value = "0", Text = "Choose a Stage" });

            foreach (var stg in stage)
            {
                list3.Add(new SelectListItem { Value = stg.Value, Text = stg.Text });
            }

            List<TextValue> reason = new List<TextValue>();
            reason = objSetingManager.GetDropDownListing("Opportunity", "Lost Reason", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list4 = new List<SelectListItem>();
            list4.Add(new SelectListItem { Value = "0", Text = "Please Choose A Lost Reason" });

            foreach (var rsn in reason)
            {
                list4.Add(new SelectListItem { Value = rsn.Value, Text = rsn.Text });
            }

            List<TextValue> client = new List<TextValue>();
            client = UtilityManager.GetClientsForDropDown();

            List<SelectListItem> list2 = new List<SelectListItem>();
            list2.Add(new SelectListItem { Value = "0", Text = "Choose A Client" });

            foreach (var clients in client)
            {
                list2.Add(new SelectListItem { Value = clients.Value, Text = clients.Text });
            }

            UserManager objUserManager = new UserManager();
            List<Users> UserList = new List<Users>();
            UserList = objUserManager.GetUsers(session.UserSession.PIN);
            try
            {
                Response = objOpportunityManager.ViewOpportunities(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Opportunity_ID_PK));
                if (Response.ErrorCode == 0)
                {
                    objOpportunity.Opportunity_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Opportunity_ID_Auto_PK"]);
                    objOpportunity.Name = Response.ResponseData.Tables[0].Rows[0]["Name"].ToString();
                    objOpportunity.Amount = Response.ResponseData.Tables[0].Rows[0]["Amount"].ToString();
                    objOpportunity.RelateTo_Name = Response.ResponseData.Tables[0].Rows[0]["RealtedTo"].ToString();
                    objOpportunity.RealateTo_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Relate_To_ID_FK"]);
                    objOpportunity.RelateTo_ContactNo = Response.ResponseData.Tables[0].Rows[0]["RelateToContactNo"].ToString();
                    objOpportunity.RelateTo_Email = Response.ResponseData.Tables[0].Rows[0]["RelateToContactEmail"].ToString();
                    objOpportunity.Stage = Response.ResponseData.Tables[0].Rows[0]["Stage"].ToString();
                    objOpportunity.Type = Response.ResponseData.Tables[0].Rows[0]["Type"].ToString();
                    objOpportunity.Probability = Response.ResponseData.Tables[0].Rows[0]["Probability"].ToString();
                    objOpportunity.AssignTO_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["AssignTo"]);
                    objOpportunity.AssignTO_Name = Response.ResponseData.Tables[0].Rows[0]["AssignToName"].ToString();
                    objOpportunity.Description = Response.ResponseData.Tables[0].Rows[0]["Description"].ToString();
                    objOpportunity.Source = Response.ResponseData.Tables[0].Rows[0]["Source"].ToString();
                    objOpportunity.Opportunity_Owner = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["CreatedBy"]);
                    objOpportunity.Opportunity_Owner_Name = Response.ResponseData.Tables[0].Rows[0]["OwnerName"].ToString();
                    objOpportunity.LostReason = Response.ResponseData.Tables[0].Rows[0]["LostReason"].ToString();

                    ViewBag.Users = UserList;
                    ViewBag.Source_List = list;
                    ViewBag.Client = list2;
                    ViewBag.Stage_List = list3;
                    ViewBag.Reason_List = list4;
                    return View(objOpportunity);
                }
                else
                {
                    ViewBag.Users = UserList;
                    ViewBag.Source_List = list;
                    ViewBag.Client = list2;
                    ViewBag.Stage_List = list3;
                    ViewBag.Reason_List = list4;
                    return View(objOpportunity);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Users = UserList;
                ViewBag.Source_List = list;
                ViewBag.Client = list2;
                ViewBag.Stage_List = list3;
                ViewBag.Reason_List = list4;
                BAL.Common.LogManager.LogError("UpdateOpportunity Get Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objOpportunity);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult UpdateOpportunity(OpportunityModel objModel)
        {
            session = new SessionHelper();
            Opportunities objOpportunities = new Opportunities();
            objResponse Response = new objResponse();
            List<TextValue> source = new List<TextValue>();

            source = objSetingManager.GetDropDownListing("Opportunity", "Source", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var cat in source)
            {
                list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
            }
            List<TextValue> stage = new List<TextValue>();
            stage = objSetingManager.GetDropDownListing("Opportunity", "Status", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list3 = new List<SelectListItem>();
            list3.Add(new SelectListItem { Value = "0", Text = "Choose a Stage" });

            foreach (var stg in stage)
            {
                list3.Add(new SelectListItem { Value = stg.Value, Text = stg.Text });
            }

            List<TextValue> reason = new List<TextValue>();
            reason = objSetingManager.GetDropDownListing("Opportunity", "Lost Reason", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list4 = new List<SelectListItem>();
            list4.Add(new SelectListItem { Value = "0", Text = "Please Choose A Lost Reason" });

            foreach (var rsn in reason)
            {
                list4.Add(new SelectListItem { Value = rsn.Value, Text = rsn.Text });
            }

            List<TextValue> client = new List<TextValue>();
            client = UtilityManager.GetClientsForDropDown();

            List<SelectListItem> list2 = new List<SelectListItem>();
            list2.Add(new SelectListItem { Value = "0", Text = "Choose A Client" });

            foreach (var clients in client)
            {
                list2.Add(new SelectListItem { Value = clients.Value, Text = clients.Text });
            }
           

            UserManager objUserManager = new UserManager();
            List<Users> UserList = new List<Users>();
            UserList = objUserManager.GetUsers(session.UserSession.PIN);
            string route = "/Opportunity/ViewOpportunity?sessionid=" + Guid.NewGuid().ToString() + "&Opportunityid=" + objModel.Opportunity_ID;

            try
            {
                objOpportunities.Name = objModel.Name;
                objOpportunities.Opportunity_Owner = objModel.Opportunity_Owner;
                objOpportunities.Amount = objModel.Amount;
                objOpportunities.RealateTo_ID = objModel.RealateTo_ID;
                if (objModel.ExpectedCloseDate != null)
                {
                    objOpportunities.ExpectedCloseDate = BAL.Helper.Helper.ConvertToDateNullable(objModel.ExpectedCloseDate, "dd/MM/yyyy");
                }

                objOpportunities.Stage = objModel.Stage;
                objOpportunities.Type = objModel.Type;
                objOpportunities.Source = objModel.Source;
                objOpportunities.Probability = objModel.Probability;
                objOpportunities.LostReason = objModel.LostReason;
                objOpportunities.Opportunity_ID = objModel.Opportunity_ID;
                if (objModel.AssignTO_ID == 0)
                {
                    objOpportunities.AssignTO_ID = Convert.ToInt64(session.UserSession.UserId);
                }
                else
                {
                    objOpportunities.AssignTO_ID = objModel.AssignTO_ID;
                }

                objOpportunities.Description = objModel.Description;

                Response = objOpportunityManager.UpdateOpportunity(objOpportunities, session.UserSession.UserId, Convert.ToInt64(session.UserSession.PIN));

                if (Response.ErrorCode == 0)
                {
                   // return RedirectToRoute("OpportunityHome");
                    return Redirect(route); 
                }
                else
                {
                    ViewBag.Users = UserList;
                    ViewBag.Source_List = list;
                    ViewBag.Client = list2;
                    ViewBag.Stage_List = list3;
                    ViewBag.Reason_List = list4;
                    ViewBag.Error_Msg = "There is an error in processing your request. Please try again.";
                    return View(objModel);
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("UpdateOpportunity Post", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                ViewBag.Users = UserList;
                ViewBag.Source_List = list;
                ViewBag.Client = list2;
                ViewBag.Error_Msg = "There is an error in processing your request. Please try again.";
                BAL.Common.LogManager.LogError("ManageOpportunity Post", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));

                return View(objModel);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AjaxAssignOpportunity(string Opportunity_ID, string User_ID)
        {

            objResponse Response = new objResponse();
            session = new Common.SessionHelper();
            OpportunityModel model = new OpportunityModel();
            try
            {

                string[] Lead_ID_PK = Opportunity_ID.Split(',');

                for (int i = 1; i < Lead_ID_PK.Length; i++)
                {
                    if (Lead_ID_PK[i].ToString() != "")
                    {
                        Response = objOpportunityManager.AssignOpportunity(Convert.ToInt64(Lead_ID_PK[i]), Convert.ToInt64(User_ID), Convert.ToInt64(session.UserSession.PIN));

                        if (Response.ErrorCode != 0)
                        {
                            break;
                        }
                    }
                }

                model.Opportunity = objOpportunityManager.getAllOpportunities(Convert.ToInt64(session.UserSession.PIN));
                return View("AjOpportunity", model);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAssignOpportunity Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));

                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

    }
}
