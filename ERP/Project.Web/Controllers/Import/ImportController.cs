using BAL.Import;
using BAL.Leads;
using OfficeOpenXml;
using Project.Entity;
using Project.Web.Common;
using Project.Web.FileImporter;
using Project.Web.Filters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Import
{
    public class ImportController : Controller
    {
        SessionHelper session;
        ImportManager objImportManager = new ImportManager();
        //
        // GET: /Import/
        [Authorize]
        [SessionTimeOut]
        public ActionResult ImportFile()
        {
            return View();
        }
        
        [Authorize]
        [SessionTimeOut]
        public ActionResult UploadExcelFile()
        {
            return View();
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult UploadCsvFile()
        {
            return View();
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult UploadContactExcelFile()
        {
            return View();
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult UploadContactCsvFile()
        {
            return View();
        }
        

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult ImportExcelFileToLeadDataTable()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            List<Project.Entity.Leads> leads = new List<Entity.Leads>();
            BAL.Leads.LeadsManager objLeadsManager = new LeadsManager();
            DataTable dt = new DataTable();

            // string Result="";
            try
            {
                string fname;

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
                        if ((file != null) && (file.ContentLength != 0) && !string.IsNullOrEmpty(file.FileName))
                        {
                            string fileName = file.FileName;
                            string fileContentType = file.ContentType;
                            byte[] fileBytes = new byte[file.ContentLength];
                            var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        }

                        var excel = new ExcelPackage(file.InputStream);
                         dt = ExcelPackageExtensions.ToDataTable(excel);
                         int count = 0;
                         foreach (DataRow dr in dt.Rows)
                         {
                             Project.Entity.Leads objLead = new Entity.Leads();

                             objLead.Name = dr[0].ToString();
                             objLead.CompanyName = dr[1].ToString();
                             objLead.Email = dr[2].ToString();
                             objLead.Alternate_Email = dr[3].ToString();
                             objLead.ContactNo = dr[4].ToString();
                             objLead.SkypeNo = dr[5].ToString();
                             objLead.AddressLine1 = dr[6].ToString();
                             objLead.AddressLine2 = dr[7].ToString();
                             objLead.City = dr[8].ToString();
                             objLead.State = dr[9].ToString();
                             objLead.Country = dr[10].ToString();
                             objLead.ZipCode = dr[11].ToString();
                             objLead.Source = dr[12].ToString();

                             Response = objLeadsManager.ImportLead(objLead, session.UserSession.Username, Convert.ToInt64(session.UserSession.PIN));
                             if (Response.ErrorCode == 0)
                             {
                                 if (Response.ErrorMessage != "Lead with same Email Already Exists")
                                 {
                                     count++;
                                 }
                             }
                             else
                             {
                                 return Json("Fail", JsonRequestBehavior.AllowGet);
                             }
                             
                         }
                         return Json("Success," + count.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("ImportExcelFileToLeadDataTable", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult ImportCsvFileToLeadDataTable()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            List<Project.Entity.Leads> leads = new List<Entity.Leads>();
            BAL.Leads.LeadsManager objLeadsManager = new LeadsManager();
            DataTable dt = new DataTable();

            // string Result="";
            try
            {
                string fname;

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
                            string filePath = Server.MapPath(file.FileName);
                            string newFileName = "IMP_Csv_" + session.UserSession.UserId + "_" + fname;
                            string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Import_Csv_Dir"]) + newFileName;
                            file.SaveAs(newFilePath);
                            dt = FileImporter.CsvPackageExtension.GetDataTableFromCSVFile(newFilePath);
                            int count = 0;
                            foreach (DataRow dr in dt.Rows)
                            {
                             Project.Entity.Leads objLead = new Entity.Leads();

                             objLead.Name = dr[0].ToString();
                             objLead.CompanyName = dr[1].ToString();
                             objLead.Email = dr[2].ToString();
                             objLead.Alternate_Email = dr[3].ToString();
                             objLead.ContactNo = dr[4].ToString();
                             objLead.SkypeNo = dr[5].ToString();
                             objLead.AddressLine1 = dr[6].ToString();
                             objLead.AddressLine2 = dr[7].ToString();
                             objLead.City = dr[8].ToString();
                             objLead.State = dr[9].ToString();
                             objLead.Country = dr[10].ToString();
                             objLead.ZipCode = dr[11].ToString();
                             objLead.Source = dr[12].ToString();

                             Response = objLeadsManager.ImportLead(objLead, session.UserSession.Username, Convert.ToInt64(session.UserSession.PIN));
                             if (Response.ErrorCode == 0)
                             {
                                 if (Response.ErrorMessage != "Lead with same Email Already Exists")
                                 {
                                     count++;
                                 }
                             }
                             else
                             {
                                 return Json("fail", JsonRequestBehavior.AllowGet);
                             }           

                        }
                            if (System.IO.File.Exists(newFilePath))
                            {
                                System.IO.File.Delete(newFilePath);
                            }
                            return Json("Success," + count.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("ImportCsvFileToLeadDataTable", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult ImportExcelFileToContactDataTable()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            BAL.Clients.ClientManager objClientManager = new BAL.Clients.ClientManager();
            DataTable dt = new DataTable();

            // string Result="";
            try
            {
                string fname;

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
                        if ((file != null) && (file.ContentLength != 0) && !string.IsNullOrEmpty(file.FileName))
                        {
                            string fileName = file.FileName;
                            string fileContentType = file.ContentType;
                            byte[] fileBytes = new byte[file.ContentLength];
                            var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                        }

                        var excel = new ExcelPackage(file.InputStream);
                        dt = ExcelPackageExtensions.ToDataTable(excel);
                        int count = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            Project.Entity.Clients objClient = new Entity.Clients();

                            objClient.Name = dr[0].ToString(); 
                            objClient.CompanyName = dr[1].ToString(); 
                            objClient.Email = dr[2].ToString(); 
                            objClient.Alternate_Email = dr[3].ToString();
                            objClient.ContactNo = dr[4].ToString(); 
                            objClient.SkypeNo = dr[5].ToString();            
                            objClient.AddressLine1 = dr[6].ToString(); 
                            objClient.AddressLine2 = dr[7].ToString();
                            objClient.City = dr[8].ToString();
                            objClient.State = dr[9].ToString();
                            objClient.Country = dr[10].ToString();
                            objClient.ZipCode = dr[11].ToString(); 
                            objClient.Source = dr[12].ToString();
                            objClient.Model = dr[13].ToString(); ;

                            Response = objClientManager.ImportClient(objClient, session.UserSession.Username, Convert.ToInt64(session.UserSession.PIN));
                            if (Response.ErrorCode == 0)
                            {
                                if (Response.ErrorMessage != "Client with same Email Already Exists")
                                {
                                    count++;
                                }
                            }
                            else
                            {
                                return Json("fail", JsonRequestBehavior.AllowGet);
                            }

                        }
                        return Json("Success," + count.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("ImportExcelFileToContactDataTable", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult ImportCsvFileToContactDataTable()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();            
            BAL.Clients.ClientManager objClinetManager = new BAL.Clients.ClientManager();
            DataTable dt = new DataTable();

            // string Result="";
            try
            {
                string fname;

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
                        string filePath = Server.MapPath(file.FileName);
                        string newFileName = "IMP_Csv_" + session.UserSession.UserId + "_" + fname;
                        string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Import_Csv_Dir"]) + newFileName;
                        file.SaveAs(newFilePath);
                        dt = FileImporter.CsvPackageExtension.GetDataTableFromCSVFile(newFilePath);
                        int count = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            Project.Entity.Clients objClient = new Entity.Clients();

                            objClient.Name = dr[0].ToString();
                            objClient.CompanyName = dr[1].ToString();
                            objClient.Email = dr[2].ToString();
                            objClient.Alternate_Email = dr[3].ToString();
                            objClient.ContactNo = dr[4].ToString();
                            objClient.SkypeNo = dr[5].ToString();
                            objClient.AddressLine1 = dr[6].ToString();
                            objClient.AddressLine2 = dr[7].ToString();
                            objClient.City = dr[8].ToString();
                            objClient.State = dr[9].ToString();
                            objClient.Country = dr[10].ToString();
                            objClient.ZipCode = dr[11].ToString();
                            objClient.Source = dr[12].ToString();
                            objClient.Model = dr[13].ToString(); ;

                            Response = objClinetManager.ImportClient(objClient, session.UserSession.Username, Convert.ToInt64(session.UserSession.PIN));
                            if (Response.ErrorCode == 0)
                            {
                                if (Response.ErrorMessage != "Client with same Email Already Exists")
                                {
                                    count++;
                                }
                            }
                            else
                            {
                                return Json("fail", JsonRequestBehavior.AllowGet);
                            }

                        }
                        if (System.IO.File.Exists(newFilePath))
                        {
                            System.IO.File.Delete(newFilePath);
                        }
                        return Json("Success," + count.ToString(), JsonRequestBehavior.AllowGet);
                    }
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("ImportCsvFileToContactDataTable", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult DownLoadSample(string file_path)
        {
            try
            {
                string newFilePath = Server.MapPath(ConfigurationManager.AppSettings["Import_Sample_Dir"]) + file_path;
                string contentType = "application/pdf";
                return File(newFilePath, contentType, file_path);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DownLoadSample Req", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View("500");
            }
        }
    }
}
