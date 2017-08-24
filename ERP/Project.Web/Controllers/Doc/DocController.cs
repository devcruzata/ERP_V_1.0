using BAL.Document;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Doc
{
    public class DocController : Controller
    {
        DocumentManager objDocManager = new DocumentManager();
        SessionHelper session;
        //
        // GET: /Doc/

        [Authorize]
        [SessionTimeOut]
        public ActionResult DocHome()
        {
            session = new SessionHelper();
            DocModel objDocModel = new DocModel();
            objDocModel.doc = objDocManager.getDocs(Convert.ToInt64(session.UserSession.PIN),Convert.ToInt64(session.UserSession.UserId));
            return View(objDocModel);
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult DeleteDoc(string Doc_ID_PK, string FileName, string FileID, string RelatedTable, string RelateToID)
        {
            objResponse response = new objResponse();
            session = new SessionHelper();
            try
            {
                response = objDocManager.DeleteDocument(Convert.ToInt64(Doc_ID_PK),Convert.ToInt64(session.UserSession.PIN));
                if (response.ErrorCode == 0)
                {
                    string Doc = "DOC0" + session.UserSession.UserId.ToString() + "_" + FileID + "_" + FileName;
                    string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Doc_Dir"]) + Doc;
                    if (System.IO.File.Exists(newFilePath))
                    {
                        System.IO.File.Delete(newFilePath);
                    }
                    if (RelatedTable == "LEAD")
                    {
                        LeadsModel objLeadModel = new LeadsModel();
                        objLeadModel.Doc = objDocManager.getDocsRelatedToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateToID), "LEAD", session.UserSession.UserId);
                        return View("AjaxDoc", objLeadModel);
                    }
                    else if (RelatedTable == "OPPORTUNITY")
                    {
                        OpportunityModel objOppoModel = new OpportunityModel();
                        objOppoModel.Doc = objDocManager.getDocsRelatedToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateToID), RelatedTable, session.UserSession.UserId);
                        return View("AjaxOppoDoc", objOppoModel);
                    }
                    else
                    {
                        ClientModel objClientModel = new ClientModel();
                        objClientModel.Doc = objDocManager.getDocsRelatedToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateToID), RelatedTable, session.UserSession.UserId);
                        return View("AjaxClientDoc", objClientModel);
                    }    
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                            
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteDoc Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AddNewDoc()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                Int64 RelateToID = Convert.ToInt64(Request.Form["RelateTo"]);
                string RelatedTable = Request.Form["RelatedTable"].ToString();
                
                string Title = Request.Form["Title"].ToString();               
                string fname;
                Guid FileID = System.Guid.NewGuid();

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


                    string newFileName = "DOC0" + session.UserSession.UserId.ToString() + "_" + FileID.ToString()+"_" + fname;


                    string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Doc_Dir"]) + newFileName;
                    file.SaveAs(newFilePath);

                    Response = objDocManager.AddDoc(Title, RelateToID,fname,FileID.ToString(), session.UserSession.UserId, session.UserSession.PIN,RelatedTable);
                    if (Response.ErrorCode == 0)
                    {
                        if (RelatedTable == "LEAD")
                        {
                            LeadsModel objLeadModel = new LeadsModel();
                            objLeadModel.Doc = objDocManager.getDocsRelatedToID(Convert.ToInt64(session.UserSession.PIN), RelateToID, RelatedTable, session.UserSession.UserId);
                            return View("AjaxDoc",objLeadModel);
                        }
                        else if (RelatedTable == "OPPORTUNITY")
                        {
                            OpportunityModel objOppoModel = new OpportunityModel();
                            objOppoModel.Doc = objDocManager.getDocsRelatedToID(Convert.ToInt64(session.UserSession.PIN), RelateToID, RelatedTable, session.UserSession.UserId);
                            return View("AjaxOppoDoc", objOppoModel);
                        }
                        else
                        {
                            ClientModel objClientModel = new ClientModel();
                            objClientModel.Doc = objDocManager.getDocsRelatedToID(Convert.ToInt64(session.UserSession.PIN), RelateToID, RelatedTable, session.UserSession.UserId);
                            return View("AjaxClientDoc", objClientModel);
                        }
                        
                    }
                    else
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    } 
                }
                return Json("", JsonRequestBehavior.AllowGet);
                       
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AddNewDoc Post method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize]
        [SessionTimeOut]
        public ActionResult DownLoad(string FileName, string FileID)
        {
            session = new SessionHelper();
            try
            {
                string Doc = "DOC0" + session.UserSession.UserId.ToString() + "_" + FileID + "_" + FileName;
                string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Doc_Dir"]) + Doc;
                string contentType = "application/pdf";
                return File(newFilePath, contentType, FileName);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("Download Req", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View("500");
            }
        }

    }
}
