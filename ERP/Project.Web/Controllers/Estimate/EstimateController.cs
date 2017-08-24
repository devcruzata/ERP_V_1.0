using BAL.Common;
using Project.Entity;
using Project.ViewModel;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Estimate
{
    public class EstimateController : Controller
    {
        BAL.Estimate.EstimateManager objEstimateManager = new BAL.Estimate.EstimateManager();
        SessionHelper session;
        //
        // GET: /Estimate/
        [Authorize]
        public ActionResult EstimateHome()
        {
            EstimateModel model = new EstimateModel();
            model.Estimations = objEstimateManager.getEstimate();
            return View(model);
        }

        //[Authorize]
        //[HttpPost]
        //public ActionResult GetLanguageByCategory(string Category_ID)
        //{
        //    try
        //    {
        //        List<TextValue> language = new List<TextValue>();
        //        language = UtilityManager.GetLanguageByCategoryForDropDown(Convert.ToInt64(Category_ID));

        //        List<SelectListItem> list = new List<SelectListItem>();
        //        list.Add(new SelectListItem { Value = "0", Text = "Choose a Language/Framework" });

        //        foreach (var lang in language)
        //        {
        //            list.Add(new SelectListItem { Value = lang.Value, Text = lang.Text });
        //        }
        //        return Json(list, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        BAL.Common.LogManager.LogError("GetLanguageByCategory Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //        return Json("", JsonRequestBehavior.AllowGet);
        //    }
        //}

        //[Authorize]
        //[HttpPost]
        //public ActionResult GetTeamByLanguage(string Language_ID)
        //{
        //    try
        //    {
        //        List<TextValue> team = new List<TextValue>();
        //        team = UtilityManager.GetUserByLanguageForDropDown(Convert.ToInt32(Language_ID), "DEV");

        //        List<SelectListItem> list = new List<SelectListItem>();
        //        list.Add(new SelectListItem { Value = "0", Text = "Choose A Team" });

        //        foreach (var lang in team)
        //        {
        //            list.Add(new SelectListItem { Value = lang.Value, Text = lang.Text });
        //        }
        //        return Json(list, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        BAL.Common.LogManager.LogError("GetLanguageByCategory Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //        return Json("", JsonRequestBehavior.AllowGet);
        //    }
        //}

        [Authorize]
        public ActionResult GetLeads()
        {
            try
            {
                List<TextValue> lead = new List<TextValue>();
                lead = UtilityManager.GetLeadsForDropDown();

                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem { Value = "0", Text = "Choose A Lead" });

                foreach (var lang in lead)
                {
                    list.Add(new SelectListItem { Value = lang.Value, Text = lang.Text });
                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetLeads Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult ManageEstimate()
        {
            //List<TextValue> category = new List<TextValue>();
            //category = UtilityManager.GetCategoriesForDropDown();

            //List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem { Value = "0", Text = "Choose a Category" });

            //foreach (var cat in category)
            //{
            //    list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
            //}

            List<TextValue> lead = new List<TextValue>();
            lead = UtilityManager.GetLeadsForDropDown();

            List<SelectListItem> list1 = new List<SelectListItem>();
            list1.Add(new SelectListItem { Value = "0", Text = "Choose A Lead" });

            foreach (var leads in lead)
            {
                list1.Add(new SelectListItem { Value = leads.Value, Text = leads.Text });
            }

            List<TextValue> client = new List<TextValue>();
            client = UtilityManager.GetClientsForDropDown();

            List<SelectListItem> list2 = new List<SelectListItem>();
            list2.Add(new SelectListItem { Value = "0", Text = "Choose A Client" });

            foreach (var clients in client)
            {
                list2.Add(new SelectListItem { Value = clients.Value, Text = clients.Text });
            }

            //ViewBag.Category_List = list;
            ViewBag.Leads = list1;
            ViewBag.Clients = list2;
            return View();
        }
       
        [Authorize]
        [HttpPost]
        public ActionResult ManageEstimate(EstimateModel model)
        {
            //List<TextValue> category = new List<TextValue>();
            //category = UtilityManager.GetCategoriesForDropDown();

            //List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem { Value = "0", Text = "Choose a Category" });

            //foreach (var cat in category)
            //{
            //    list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
            //}

            List<TextValue> lead = new List<TextValue>();
            lead = UtilityManager.GetLeadsForDropDown();

            List<SelectListItem> list1 = new List<SelectListItem>();
            list1.Add(new SelectListItem { Value = "0", Text = "Choose A Lead" });

            foreach (var leads in lead)
            {
                list1.Add(new SelectListItem { Value = leads.Value, Text = leads.Text });
            }

            List<TextValue> client = new List<TextValue>();
            client = UtilityManager.GetClientsForDropDown();

            List<SelectListItem> list2 = new List<SelectListItem>();
            list2.Add(new SelectListItem { Value = "0", Text = "Choose A Client" });

            foreach (var clients in client)
            {
                list2.Add(new SelectListItem { Value = clients.Value, Text = clients.Text });
            }

            objResponse Response = new objResponse();

            try
            {
                session = new SessionHelper();
                Project.Entity.Estimate objEstimate = new Entity.Estimate();
                objEstimate.Date = BAL.Helper.Helper.ConvertToDateNullable(model.Date,"dd/MM/yyyy");
                objEstimate.Lead_ID_Fk = model.Lead_ID_Fk;
                objEstimate.Client_ID_Fk = model.Client_ID_Fk;
                objEstimate.Priority = model.Priority;
                objEstimate.Category = model.Category;
                objEstimate.Language = model.Language;
                objEstimate.AssignTo = model.AssignTo;
                objEstimate.Requirment = model.Requirment;

                Response = objEstimateManager.AddEstimate(objEstimate,session.UserSession.Username);

                if (Response.ErrorCode == 0)
                {
                    long Estimate_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0][0]);
                    // Uploading Requirment Document and Saving Details to DB
                    try
                    {
                        foreach (HttpPostedFileBase file in model.UploadedDoc)
                        {
                            if (file != null)
                            {
                                string filename = System.IO.Path.GetFileName(file.FileName);
                                Debug.WriteLine("file name is: " + filename);
                                string[] fileext = filename.Split('.');

                               // string newFileName = "EST0" + Estimate_ID + "_" + filename + "." + fileext[1];
                                string newFileName = "EST0" + Estimate_ID + "_" + filename;
                                string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Estimation_Req_Dir"]) + newFileName;
                                file.SaveAs(newFilePath);

                                if (filename != "")
                                {
                                    Response = objEstimateManager.AddEstimationUpload(Estimate_ID, filename, session.UserSession.Username, 0);
                                    if (Response.ErrorCode == 0)
                                    {
                                        return RedirectToRoute("EstimateHome");
                                    }
                                    else
                                    {
                                        if (System.IO.File.Exists(newFilePath))
                                        {
                                            System.IO.File.Delete(newFilePath);
                                        }
                                        ViewBag.Error_Msg = "Unable To Upload Document. Please go to update Estimate and upload document again.";
                                      //  ViewBag.Category_List = list;
                                        ViewBag.Leads = list1;
                                        ViewBag.Clients = list2;
                                        return View();
                                    }
                                }

                            }
                            else
                            {
                                return RedirectToRoute("EstimateHome");
                            }

                        }

                        // If we got this far , than there is something wrong. Redirect to LeadsHome Page
                        return RedirectToRoute("EstimateHome");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error_Msg = ex.Message.ToString();
                       // ViewBag.Category_List = list;
                        ViewBag.Leads = list1;
                        ViewBag.Clients = list2;
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error_Msg = Response.ErrorMessage;
                  //  ViewBag.Category_List = list;
                    ViewBag.Leads = list1;
                    ViewBag.Clients = list2;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = Response.ErrorMessage;
               // ViewBag.Category_List = list;
                ViewBag.Leads = list1;
                ViewBag.Clients = list2;
                BAL.Common.LogManager.LogError("ManageEstimate Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteEstimate(string Estimate_ID_PK)
        {
            string response = "";
            try
            {
                response = objEstimateManager.DeleteEstimate(Convert.ToInt64(Estimate_ID_PK));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteEstimate Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        //[Authorize]
        //public ActionResult ViewEstimate(string Estimate_ID_PK)
        //{
        //    objResponse Response = new objResponse();
        //    EstimateModel objEstimate = new EstimateModel();

        //    List<TextValue> category = new List<TextValue>();
        //    category = UtilityManager.GetCategoriesForDropDown();

        //    List<SelectListItem> list = new List<SelectListItem>();
        //    list.Add(new SelectListItem { Value = "0", Text = "Choose a Category" });

        //    foreach (var cat in category)
        //    {
        //        list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
        //    }
        //    try
        //    {
        //        Response = objEstimateManager.ViewEstimateDetail(Convert.ToInt64(Estimate_ID_PK));
        //        if (Response.ErrorCode == 0)
        //        {                    
        //            objEstimate.Estimate_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Estimation_ID_Auto_PK"]);
        //            objEstimate.Lead = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Lead"]);
        //            objEstimate.Client = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Client"]);
        //            objEstimate.Priority = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Priority"]);
        //            objEstimate.Date = Convert.ToDateTime(Response.ResponseData.Tables[0].Rows[0]["Date"]).ToString("d MMM yyyy");
        //            objEstimate.Category = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Category"]);
        //            objEstimate.CategoryName = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["CategoryName"]);
        //            objEstimate.Language = Convert.ToInt32(Response.ResponseData.Tables[0].Rows[0]["LanguageId"]);
        //            objEstimate.LanguageName = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Language"]);
        //            objEstimate.AssignTo = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Team"]);
        //            objEstimate.Assigne = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["FullName"]);
        //            objEstimate.Status = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Status"]);
        //            objEstimate.AssigBy = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["AssignBy"]);
        //            objEstimate.Requirment = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Requirment"]);

        //            // Fetching Requirment Uploads For Estimate From DB
        //            foreach (DataRow dr1 in Response.ResponseData.Tables[2].Rows)
        //            {
        //                EstimationUpload objEstUpload = new EstimationUpload();
        //                objEstUpload.FileName = dr1["FileName"].ToString();
        //                objEstUpload.FilePath = "EST0" + objEstimate.Estimate_ID + "_" + dr1["FileName"].ToString();
        //                FileInfo file = new FileInfo(Server.MapPath(ConfigurationManager.AppSettings["Estimation_Req_Dir"])+objEstUpload.FilePath);

        //                if (file.Length > 1048576)
        //                {
        //                    objEstUpload.FileSize = (file.Length / 1048576).ToString() + "Mb";
        //                }
        //                else
        //                {
        //                    objEstUpload.FileSize = (file.Length / 1024).ToString() + "Kb";
        //                }

        //                objEstUpload.UploadedDate = Convert.ToDateTime(dr1["CreatedDate"]).ToString("d MMM yyyy");
        //                objEstimate.Uploads.Add(objEstUpload);
        //            }

        //            // Fetching Estimation Comment And Coment Uploads
        //            foreach (DataRow dr in Response.ResponseData.Tables[1].Rows)
        //            {
        //                EstimationComments objComment = new EstimationComments();
        //                objComment.CommentDate = Convert.ToDateTime(dr["Date"]).ToString("d MMM yyyy");
        //                objComment.Comment = dr["Comment"].ToString();
        //                objComment.CommentBy = dr["CommentBy"].ToString();
        //                objComment.CommentByID = Convert.ToInt64(dr["CommentBy_ID_FK"]);
        //                objComment.CommentID = Convert.ToInt64(dr["Estimation_Comment_ID_Auto_PK"]);

        //                Response = objEstimateManager.GetEstimationUpload(objEstimate.Estimate_ID, objComment.CommentID, Convert.ToDateTime(dr["Date"]));

        //                foreach (DataRow drr in Response.ResponseData.Tables[0].Rows)
        //                {
        //                    CommentUpload objUpload = new CommentUpload();
        //                    objUpload.UploadFileName = drr["FileName"].ToString();
        //                    objUpload.CommentID = Convert.ToInt64(drr["CommentID"]);

        //                    objComment.CommUploads.Add(objUpload);
        //                }
        //                objEstimate.Comments.Add(objComment);
        //            }                   
        //        }
        //        ViewBag.Category_List = list;
        //        return View(objEstimate);
        //    }
        //    catch (Exception ex)
        //    {
        //        BAL.Common.LogManager.LogError("ViewEstimate", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //        ViewBag.Category_List = list;
        //        return View(objEstimate);
        //    }
        //}

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(EstimateModel objEstimate, string Comment)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                Response = objEstimateManager.AddEstimationComment(objEstimate.Estimate_ID,Comment,DateTime.Now,session.UserSession.UserId,session.UserSession.Username);
                if (Response.ErrorCode == 0)
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AddComment", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddCommentWithFile()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                Int64 EstimateID = Convert.ToInt64(Request.Form["Estimate_ID"]);
                string comment = Request.Form["Comment"].ToString();
                string fname;

                Response = objEstimateManager.AddEstimationComment(EstimateID, comment, DateTime.Now, session.UserSession.UserId, session.UserSession.Username);
                if (Response.ErrorCode == 0)
                {
                    if (Request.Files.Count > 0)
                    {
                        HttpFileCollectionBase files = Request.Files;
                        for (int i = 0; i < files.Count; i++)
                        {
                            HttpPostedFileBase file = files[i];

                            // Checking for Internet Explorer  
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }

                            string newFileName = "ESTCOMM0" + EstimateID + "_" + fname;
                            string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Estimation_Req_Dir"]) + newFileName;
                            file.SaveAs(newFilePath);

                            Response = objEstimateManager.AddEstimationUpload(EstimateID, fname, session.UserSession.Username, Convert.ToInt64(Response.ErrorMessage));
                            if (Response.ErrorCode == 0)
                            {
                                return Json("success", JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                if (System.IO.File.Exists(newFilePath))
                                {
                                    System.IO.File.Delete(newFilePath);
                                }
                                return Json("success", JsonRequestBehavior.AllowGet);
                            }
                        }
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }                    
                }
                else
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }       
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AddComment", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult test()
        //{
        //    List<TextValue> category = new List<TextValue>();
        //    category = UtilityManager.GetCategoriesForDropDown();

        //    List<SelectListItem> list = new List<SelectListItem>();
        //    list.Add(new SelectListItem { Value = "0", Text = "Choose a Category" });

        //    foreach (var cat in category)
        //    {
        //        list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
        //    }

        //    List<TextValue> lead = new List<TextValue>();
        //    lead = UtilityManager.GetLeadsForDropDown();

        //    List<SelectListItem> list1 = new List<SelectListItem>();
        //    list1.Add(new SelectListItem { Value = "0", Text = "Choose A Lead" });

        //    foreach (var leads in lead)
        //    {
        //        list1.Add(new SelectListItem { Value = leads.Value, Text = leads.Text });
        //    }

        //    List<TextValue> client = new List<TextValue>();
        //    client = UtilityManager.GetClientsForDropDown();

        //    List<SelectListItem> list2 = new List<SelectListItem>();
        //    list2.Add(new SelectListItem { Value = "0", Text = "Choose A Client" });

        //    foreach (var clients in client)
        //    {
        //        list2.Add(new SelectListItem { Value = clients.Value, Text = clients.Text });
        //    }

        //    ViewBag.Category_List = list;
        //    ViewBag.Leads = list1;
        //    ViewBag.Clients = list2;
        //    return View();
        //}

        [Authorize]
        [HttpPost]
        public ActionResult AjaxUpdate(EstimateModel objEstimatemodel, string TextField)
        {
            objResponse Response = new objResponse();
            Project.Entity.Estimate objEstimate = new Entity.Estimate();
            session = new SessionHelper();
            try
            {
                if (objEstimatemodel.Date != null)
                {
                    objEstimate.Date = BAL.Helper.Helper.ConvertToDateNullable(objEstimatemodel.Date, "dd/MM/yyyy");
                }
                objEstimate.Estimate_ID_Auto_PK = objEstimatemodel.Estimate_ID;
                objEstimate.Language = objEstimatemodel.Language;
                objEstimate.AssignTo = objEstimatemodel.AssignTo;
                objEstimate.Priority = objEstimatemodel.Priority;
                objEstimate.Requirment = objEstimatemodel.Requirment;

                Response = objEstimateManager.UpdateEstimate(objEstimate, session.UserSession.Username, TextField);
                if (Response.ErrorCode == 0)
                {
                    return Json("Success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Fail", JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxUpdate Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult DownLoad(string file_path, string file_Name)
        {
            try
            {
                string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Estimation_Req_Dir"]) + file_path;
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
        [HttpPost]
        public ActionResult DeleteComment(string Comment_ID)
        {
            string response = "";
            try
            {
                response = objEstimateManager.DeleteEstimateComment(Convert.ToInt64(Comment_ID));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteEstimateCooment Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxChangeStatus(string EstimateID, string status, string Note)
        {
            session = new SessionHelper();
            objResponse Response = new objResponse();
            try
            {
                //DateTime FollowUp = BAL.Helper.Helper.ConvertToDateNullable(DateTime.Now.ToString(), "dd/MM/yyyy");
                Response = objEstimateManager.ChangeStatus(Convert.ToInt64(EstimateID), status, Note, DateTime.Now, session.UserSession.UserId, session.UserSession.Username);

                if (Response.ErrorCode == 0)
                {
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = ex.Message.ToString(); ;
                BAL.Common.LogManager.LogError("UpdateLeadDetils Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }


    }
}
