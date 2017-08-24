using BAL.Common;
using Project.Entity;
using Project.ViewModel;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Project.Web.Controllers.Projects
{
    public class ProjectsController : Controller
    {
        BAL.Projects.ProjectManager objProjectManager = new BAL.Projects.ProjectManager();
        SessionHelper session;

        //
        // GET: /Projects/

        [Authorize]
        public ActionResult ProjectsHome()
        {
            ProjectModel model = new ProjectModel();
            model.Projectss = objProjectManager.getProjects();
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

        //[Authorize]       
        //public ActionResult ManageProjects()
        //{
        //    List<TextValue> category = new List<TextValue>();
        //    category = UtilityManager.GetCategoriesForDropDown();

        //    List<SelectListItem> list = new List<SelectListItem>();
        //    list.Add(new SelectListItem { Value = "0", Text = "Choose a Category" });

        //    foreach (var cat in category)
        //    {
        //        list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
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
        //    ViewBag.Clients = list2;
        //    return View();
        //}

        //[Authorize]
        //[HttpPost]
        //public ActionResult ManageProjects(ProjectModel objModel)
        //{
        //    objResponse Response = new objResponse();
        //    Project.Entity.Projects objProject = new Entity.Projects();
        //    session = new SessionHelper();

        //    List<TextValue> category = new List<TextValue>();
        //    category = UtilityManager.GetCategoriesForDropDown();

        //    List<SelectListItem> list = new List<SelectListItem>();
        //    list.Add(new SelectListItem { Value = "0", Text = "Choose a Category" });

        //    foreach (var cat in category)
        //    {
        //        list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
        //    }

        //    List<TextValue> client = new List<TextValue>();
        //    client = UtilityManager.GetClientsForDropDown();

        //    List<SelectListItem> list2 = new List<SelectListItem>();
        //    list2.Add(new SelectListItem { Value = "0", Text = "Choose A Client" });

        //    foreach (var clients in client)
        //    {
        //        list2.Add(new SelectListItem { Value = clients.Value, Text = clients.Text });
        //    }


        //    try
        //    {
        //        if (objModel.Task.Count <= 1 && objModel.Task[0] == "")
        //        {
        //            ViewBag.Error_Msg = "Please Provide Valid SOW Data For Project";
        //            ViewBag.Category_List = list;
        //            ViewBag.Clients = list2;
        //            return View();
        //        }
        //        else
        //        {
        //            objProject.Title = objModel.Title;
        //            objProject.Date = BAL.Helper.Helper.ConvertToDateNullable(objModel.Date, "dd/MM/yyyy");
        //            objProject.Client_ID = objModel.Client_ID;
        //            objProject.Model = objModel.Model;
        //            objProject.Category_ID = objModel.Category_ID;
        //            objProject.Note = objModel.Note;

        //            Response = objProjectManager.AddProject(objProject, session.UserSession.Username);

        //            if (Response.ErrorCode == 0)
        //            {
        //                if (Response.ErrorMessage != "Projects With Same Title Already Exists")
        //                {
        //                    long Project_ID = Convert.ToInt64(Response.ErrorMessage);

        //                    for (int i = 0; i < objModel.Task.Count; i++)
        //                    {
        //                        if (objModel.Task[i] != "")
        //                        {
        //                            Response = objProjectManager.AddSOW(Project_ID, objModel.Task[i], objModel.Description[i], objModel.Hours[i], Convert.ToDecimal(objModel.Price[i]), session.UserSession.Username);

        //                            if (Response.ErrorCode != 0)
        //                            {
        //                                ViewBag.Error_Msg = Response.ErrorMessage;
        //                                ViewBag.Category_List = list;
        //                                ViewBag.Clients = list2;
        //                                return View();
        //                            }
        //                        }
        //                        else
        //                        {
        //                            break;
        //                        }

        //                    }
        //                    return RedirectToRoute("ProjectHome");
        //                }
        //                else
        //                {
        //                    ViewBag.Error_Msg = Response.ErrorMessage;
        //                    ViewBag.Category_List = list;
        //                    ViewBag.Clients = list2;
        //                    return View();
        //                }
        //            }
        //            else
        //            {
        //                ViewBag.Error_Msg = Response.ErrorMessage;
        //                ViewBag.Category_List = list;
        //                ViewBag.Clients = list2;
        //                return View();
        //            }
        //        }
                
        //    }
        //    catch(Exception ex)
        //    {
        //        ViewBag.Error_Msg = ex.Message.ToString();
        //        ViewBag.Category_List = list;
        //        ViewBag.Clients = list2;
        //        BAL.Common.LogManager.LogError("ManageProject Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //        return View();
        //    }
        //}

        [Authorize]
        [HttpPost]
        public ActionResult SendAgreement(ProjectModel objModel)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {

                for (int i = 0; i < objModel.PayMent_Date.Count; i++)
                {
                    Response = objProjectManager.AddPaymentData(objModel.Project_ID_PK, objModel.PayMent_Date[i], objModel.PayMent_Upfront[i], Convert.ToDecimal(objModel.PayMent_Remaining[i]), session.UserSession.Username);

                    if (Response.ErrorCode != 0)
                    {
                        ViewBag.Error_Msg = Response.ErrorMessage;                        
                        return View();
                    }
                }
                                
                Response = objProjectManager.getClientDetailByProject(Convert.ToInt64(objModel.Project_ID_PK));
                string FullName = Response.ResponseData.Tables[0].Rows[0]["Name"].ToString();
                string CompanyName = Response.ResponseData.Tables[0].Rows[0]["CompanyName"].ToString();
                string Email = Response.ResponseData.Tables[0].Rows[0]["Email"].ToString();
                string link1 = ConfigurationManager.AppSettings["Agreement_Link"].ToString() + "?Project_ID_Pk=" + objModel.Project_ID_PK + "&ClientName=" + FullName + "&CompName=" + CompanyName;
                string link = ConfigurationManager.AppSettings["Agreement_Link"].ToString() + "?Project_ID_Pk=" + objModel.Project_ID_PK + "&ClientName=" + FullName + "&CompName=" + CompanyName + "&Link="+link1;
                string body = "Dear " + FullName + ", <br/><br/>Please fill up the complete contact information in the Agreement & NDA Form with the signature to acknowledge and submit. <br/><br/>Please Click the below link to view the agreement <br/><br/><a href=" + link + ">" + link + "</a><br/><br/>If you have any trouble in viewing Agreement than please contact to us.<br/><br/>We look forward to build a long-term & perpetual relations.<br/><br/>Thank You,<br/><br/>Cruzata Technology";
                //if (BAL.Helper.Helper.SendEmail(Email, "Project Agreement CRUZATA TECHNOLOGIES", body))
                //{
                //    return RedirectToRoute("ProjectHome");
                //}
                //else
                //{
                //    return RedirectToRoute("ProjectHome");
                //}
                if (BAL.Helper.Helper.SendEmailUsingGoDaddy(Email, "Project Agreement CRUZATA TECHNOLOGIES", body))
                {
                    return RedirectToRoute("ProjectHome");
                }
                else
                {
                    return RedirectToRoute("ProjectHome");
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("Send Agreement", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return RedirectToRoute("ProjectsHome");
            }            
        }

        public ActionResult Agreement(string Project_ID_Pk, string ClientName, string CompName , string Link)
        {
            objResponse Response = new objResponse();
            ProjectModel objModel = new ProjectModel();
            string sign = "";
            try
            {
                objModel.projectSowData = objProjectManager.getProjectSow(Convert.ToInt64(Project_ID_Pk));
                objModel.projectPaymentData = objProjectManager.getProjectPaymentData(Convert.ToInt64(Project_ID_Pk));
                
                ViewBag.ClientName = ClientName;
                if (CompName != "")
                {
                    ViewBag.CompName = CompName;
                }
                else
                {
                    ViewBag.CompName = ClientName;
                }
                sign = objProjectManager.GetSign(Convert.ToInt64(Project_ID_Pk));
                ViewBag.Sign = sign;
                ViewBag.Link = Link;
                ViewBag.Project_ID = Project_ID_Pk;
                return View(objModel);
            }
            catch (Exception ex)
            {
                ViewBag.Sign = sign;
                ViewBag.Link = Link;
                ViewBag.Project_ID = Project_ID_Pk;
                ViewBag.ClientName = ClientName;
                ViewBag.CompName = CompName;
                BAL.Common.LogManager.LogError("Send Agreement", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objModel);
            }
            
        }

        //public ActionResult MailAsPDF(string Project_ID, string PdfLink)
        //{
        //    // create the HTML to PDF converter
        //    HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

        //    // set browser width
        //    htmlToPdfConverter.BrowserWidth = int.Parse(textBoxBrowserWidth.Text);

        //    // set browser height if specified, otherwise use the default
        //    if (textBoxBrowserHeight.Text.Length > 0)
        //        htmlToPdfConverter.BrowserHeight = int.Parse(textBoxBrowserHeight.Text);

        //    // set HTML Load timeout
        //    htmlToPdfConverter.HtmlLoadedTimeout = int.Parse(textBoxLoadHtmlTimeout.Text);

        //    // set PDF page size and orientation
        //    htmlToPdfConverter.Document.PageSize = GetSelectedPageSize();
        //    htmlToPdfConverter.Document.PageOrientation = GetSelectedPageOrientation();

        //    // set the PDF standard used by the document
        //    htmlToPdfConverter.Document.PdfStandard = checkBoxPdfA.Checked ? PdfStandard.PdfA : PdfStandard.Pdf;

        //    // set PDF page margins
        //    htmlToPdfConverter.Document.Margins = new PdfMargins(5);

        //    // set triggering mode; for WaitTime mode set the wait time before convert
        //    switch (dropDownListTriggeringMode.SelectedValue)
        //    {
        //        case "Auto":
        //            htmlToPdfConverter.TriggerMode = ConversionTriggerMode.Auto;
        //            break;
        //        case "WaitTime":
        //            htmlToPdfConverter.TriggerMode = ConversionTriggerMode.WaitTime;
        //            htmlToPdfConverter.WaitBeforeConvert = int.Parse(textBoxWaitTime.Text);
        //            break;
        //        case "Manual":
        //            htmlToPdfConverter.TriggerMode = ConversionTriggerMode.Manual;
        //            break;
        //        default:
        //            htmlToPdfConverter.TriggerMode = ConversionTriggerMode.Auto;
        //            break;
        //    }

        //    // set header and footer
        //    SetHeader(htmlToPdfConverter.Document);
        //    SetFooter(htmlToPdfConverter.Document);

        //    // convert HTML to PDF
        //    byte[] pdfBuffer = null;

        //    if (radioButtonConvertUrl.Checked)
        //    {
        //        // convert URL to a PDF memory buffer
        //        string url = textBoxUrl.Text;

        //        pdfBuffer = htmlToPdfConverter.ConvertUrlToMemory(url);
        //    }
        //    else
        //    {
        //        // convert HTML code
        //        string htmlCode = textBoxHtmlCode.Text;
        //        string baseUrl = textBoxBaseUrl.Text;

        //        // convert HTML code to a PDF memory buffer
        //        pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);
        //    }

        //    // inform the browser about the binary data format
        //    HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

        //    // let the browser know how to open the PDF document, attachment or inline, and the file name
        //    HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("{0}; filename=HtmlToPdf.pdf; size={1}",
        //        checkBoxOpenInline.Checked ? "inline" : "attachment", pdfBuffer.Length.ToString()));

        //    // write the PDF buffer to HTTP response
        //    HttpContext.Current.Response.BinaryWrite(pdfBuffer);

        //    // call End() method of HTTP response to stop ASP.NET page processing
        //    HttpContext.Current.Response.End();
        //}

        public ActionResult SignAgreement(string Project_ID,string Sign )
        {
            objResponse Response = new objResponse();
            try
            {
                Response = objProjectManager.AddSignToProject(Project_ID , Sign);
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
                BAL.Common.LogManager.LogError("SignAgreement Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult ViewAgreement(string Project_ID_Pk )
        {
            objResponse Response = new objResponse();
            ProjectModel objModel = new ProjectModel();
            string sign = "";
            Response = objProjectManager.getClientDetailByProject(Convert.ToInt64(Project_ID_Pk));
            string FullName = Response.ResponseData.Tables[0].Rows[0]["Name"].ToString();
            string CompanyName = Response.ResponseData.Tables[0].Rows[0]["CompanyName"].ToString();
            try
            {
               


                sign = objProjectManager.GetSign(Convert.ToInt64(Project_ID_Pk));                              
                objModel.projectSowData = objProjectManager.getProjectSow(Convert.ToInt64(Project_ID_Pk));
                objModel.projectPaymentData = objProjectManager.getProjectPaymentData(Convert.ToInt64(Project_ID_Pk));

                ViewBag.Sign = sign;
                ViewBag.ClientName = FullName;
                if (CompanyName != "")
                {
                    ViewBag.CompName = CompanyName;
                }
                else
                {
                    ViewBag.CompName = FullName;
                }                
               
                return View(objModel);
            }
            catch (Exception ex)
            {
                ViewBag.Sign = sign;
                ViewBag.ClientName = FullName;
                ViewBag.CompName = CompanyName;
                BAL.Common.LogManager.LogError("View Agreement", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objModel);
            }

        }

        //[Authorize]
        //public ActionResult ViewProjects(string Project_ID_Pk)
        //{
        //    objResponse Response = new objResponse();
        //    ProjectModel objModel = new ProjectModel();

        //    List<TextValue> category = new List<TextValue>();
        //    category = UtilityManager.GetCategoriesForDropDown();

        //    List<SelectListItem> list = new List<SelectListItem>();
        //    list.Add(new SelectListItem { Value = "0", Text = "Choose a Category" });

        //    foreach (var cat in category)
        //    {
        //        list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
        //    }

        //    List<TextValue> client = new List<TextValue>();
        //    client = UtilityManager.GetClientsForDropDown();

        //    List<SelectListItem> list2 = new List<SelectListItem>();
        //    list2.Add(new SelectListItem { Value = "0", Text = "Choose A Client" });

        //    foreach (var clients in client)
        //    {
        //        list2.Add(new SelectListItem { Value = clients.Value, Text = clients.Text });
        //    }
        //    try
        //    {
        //        Response = objProjectManager.GetProjectInfo(Convert.ToInt64(Project_ID_Pk));

        //        if (Response.ErrorCode == 0)
        //        {
        //            objModel.Project_ID_PK = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Project_ID_Auto_PK"]);
        //            objModel.Title = Response.ResponseData.Tables[0].Rows[0]["Title"].ToString();
        //            objModel.Date = Convert.ToDateTime(Response.ResponseData.Tables[0].Rows[0]["Date"]).ToString("d MMM yyyy");
        //            objModel.Client_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Client_ID_FK"]);
        //            objModel.Client_Name = Response.ResponseData.Tables[0].Rows[0]["Name"].ToString();
        //            objModel.Model = Response.ResponseData.Tables[0].Rows[0]["Model"].ToString();
        //            objModel.Category_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Category_ID_FK"]);
        //            objModel.CategoryName = Response.ResponseData.Tables[0].Rows[0]["CategoryName"].ToString();
        //            objModel.Note = Response.ResponseData.Tables[0].Rows[0]["Note"].ToString();

        //            if (Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["ClientSign"]) != "")
        //            {
        //                objModel.isSigned = true;
        //            }
        //            else
        //            {
        //                objModel.isSigned = false;
        //            }

        //            foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
        //            {                       
        //                    objModel.Task.Add(dr["Task"].ToString());
        //                    objModel.Description.Add(dr["Description"].ToString());
        //                    objModel.Hours.Add(dr["Hours"].ToString());
        //                    objModel.Price.Add(dr["Price"].ToString());                                                
        //            }



        //            ViewBag.Category_List = list;
        //            ViewBag.Clients = list2;
        //            return View(objModel);
        //        }
        //        else
        //        {
        //            return View(objModel);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Category_List = list;
        //        ViewBag.Clients = list2;
        //        BAL.Common.LogManager.LogError("ViewProjects", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //        return View(objModel);
        //    }
        //}

        [Authorize]
        [HttpPost]
        public ActionResult Temp_Sow(ProjectModel objModel)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                if ((objModel.Task.Count <= 1 && objModel.Task[0] == "" && objModel.Description[0] == "" && objModel.Hours[0] == "" && objModel.Price[0] == "") || objModel.Task[0] == "" || objModel.Description[0] == "" || objModel.Hours[0] == "" || objModel.Price[0] == "")
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Response = objProjectManager.ClearSowForProject(objModel.Project_ID_PK);
                    if (Response.ErrorCode == 0)
                    {

                        for (int i = 0; i < objModel.Task.Count; i++)
                        {
                            Response = objProjectManager.UpdateSOW(objModel.Project_ID_PK, objModel.Task[i], objModel.Description[i], objModel.Hours[i], Convert.ToDecimal(objModel.Price[i]), objModel.Note, session.UserSession.Username);

                            if (Response.ErrorCode != 0)
                            {
                                ViewBag.Error_Msg = Response.ErrorMessage;
                                return Json("", JsonRequestBehavior.AllowGet);
                            }
                        }
                        //return Json("success", JsonRequestBehavior.AllowGet);
                        return View(objModel);
                    }
                    else
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                }                                
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("Temp SOW", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteProject(string Project_ID_PK)
        {
            string response = "";
            try
            {
                response = objProjectManager.DeleteProject(Convert.ToInt64(Project_ID_PK));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteProject Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxUpdate(ProjectModel objProjectemodel, string TextField)
        {
            objResponse Response = new objResponse();
            Project.Entity.Projects objProjects = new Entity.Projects();
            session = new SessionHelper();
            try
            {
                if (objProjectemodel.Date != null)
                {
                    objProjects.Date = BAL.Helper.Helper.ConvertToDateNullable(objProjectemodel.Date, "dd/MM/yyyy");
                }
                objProjects.Project_ID_PK = objProjectemodel.Project_ID_PK;
                objProjects.Title = objProjectemodel.Title;
                objProjects.Client_ID = objProjectemodel.Client_ID;
                objProjects.Model = objProjectemodel.Model;
                objProjects.Category_ID = objProjectemodel.Category_ID;

                Response = objProjectManager.UpdateProject(objProjects, session.UserSession.Username, TextField);
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
                BAL.Common.LogManager.LogError("AjaxUpdate Project Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
        }

    }
}
