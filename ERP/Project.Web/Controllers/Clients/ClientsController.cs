using BAL.Common;
using BAL.Document;
using BAL.Events;
using BAL.Meeting;
using BAL.Note;
using BAL.Setings;
using BAL.Task;
using BAL.User;
using Google.Contacts;
using Google.GData.Client;
using Google.GData.Contacts;
using Google.GData.Extensions;
using Newtonsoft.Json;
using Project.Entity;
using Project.ViewModel;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Client
{
    public class ClientsController : Controller
    {
        BAL.Clients.ClientManager objClientsManager = new BAL.Clients.ClientManager();
        SetingManager objSetingManager = new SetingManager();
        SessionHelper session;
        //
        // GET: /Clients/
        [Authorize]
        [SessionTimeOut]
        public ActionResult ClientHome()
        {
            session = new SessionHelper();
            ClientModel model = new ClientModel();
            model.clients = objClientsManager.getClients(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(session.UserSession.UserId), session.UserSession.UserType);
            return View(model);
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult ManageClient()
        {
            session = new SessionHelper();
            List<TextValue> source = new List<TextValue>();
            source = objSetingManager.GetDropDownListing("Client", "Source", Convert.ToInt64(session.UserSession.PIN));

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
        public ActionResult ManageClient(ClientModel objClientModel)
        {
            objResponse Response = new objResponse();
            Project.Entity.Clients objClient = new Entity.Clients();
            session = new SessionHelper();

            session = new SessionHelper();
            List<TextValue> source = new List<TextValue>();
            source = objSetingManager.GetDropDownListing("Client", "Source", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var cat in source)
            {
                list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
            }
            
            try
            {
                objClient.Date = DateTime.Now;                
                objClient.Name = objClientModel.Name;
                objClient.CompanyName = objClientModel.CompanyName;
                objClient.Email = objClientModel.Email;
                objClient.ContactNo = objClientModel.ContactNo;
                objClient.SkypeNo = objClientModel.SkypeNo;               
                objClient.jobDescription = objClientModel.JobDescription;                
                objClient.ZipCode = objClientModel.ZipCode;              
                objClient.AddressLine1 = objClientModel.AddressLine1;
                objClient.AddressLine2 = objClientModel.AddressLine2;
                objClient.City = objClientModel.City;
                objClient.State = objClientModel.State;
                objClient.Country = objClientModel.Country;
                objClient.Alternate_Email = objClientModel.Alternate_Email;
                objClient.Source = objClientModel.Source;
                session = new SessionHelper();
                Response = objClientsManager.AddClient(objClient, session.UserSession.UserId,session.UserSession.PIN);

                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "Client with same Email Already Exists")
                    {
                        return RedirectToRoute("ClientHome");
                    }
                    else
                    {
                        ViewBag.Source_List = list;
                        ViewBag.Error_Msg = Response.ErrorMessage;                        
                        return View();
                    }
                }
                else
                {
                    ViewBag.Source_List = list;
                    ViewBag.Error_Msg = Response.ErrorMessage;                    
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Source_List = list;
                ViewBag.Error_Msg = Response.ErrorMessage;                
                BAL.Common.LogManager.LogError("ManageClient Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View();
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult DeleteClient(string Client_ID_PK)
        {
            string response = "";
            try
            {
                response = objClientsManager.DeleteClient(Convert.ToInt64(Client_ID_PK));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteClient Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult ViewClient(string sessionid, string Clientid)
        {
            objResponse Response = new objResponse();
            ClientModel model = new ClientModel();
            session = new SessionHelper();
            EventManager objEventManager = new EventManager();
            TaskManager objTaskManager = new TaskManager();
            DocumentManager objDocManager = new DocumentManager();
            NoteManager objNoteManager = new NoteManager();

            UserManager objUserManager = new UserManager();
            List<Users> UserList = new List<Users>();
            UserList = objUserManager.GetUsers(session.UserSession.PIN);
            try
            {
                Response = objClientsManager.ViewClients(Convert.ToInt64(Clientid));

                if (Response.ErrorCode == 0)
                {
                    model.Client_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Client_ID_Auto_PK"]);
                    model.Date = Response.ResponseData.Tables[0].Rows[0]["Date"].ToString();                   
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
                    model.Client_Owner_Name = Response.ResponseData.Tables[0].Rows[0]["Owner"].ToString();
                    model.Source = Response.ResponseData.Tables[0].Rows[0]["Source"].ToString();
                    model.JobDescription = Response.ResponseData.Tables[0].Rows[0]["JobDescription"].ToString();

                    model.Events = objEventManager.getEventsByRelateToID(Convert.ToInt64(session.UserSession.PIN), session.UserSession.UserId, Convert.ToInt64(Clientid), "CLIENT");
                    model.activities = UtilityManager.getActivityByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Clientid), "CLIENT");
                    model.Task = objTaskManager.getTasksByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Clientid), session.UserSession.UserId, "CLIENT");
                    model.Doc = objDocManager.getDocsRelatedToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Clientid), "CLIENT", session.UserSession.UserId);
                    model.Notes = objNoteManager.getNotesByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(Clientid), session.UserSession.UserId, "CLIENT");

                    ViewBag.Users = UserList;
                    return View(model);
                }
                else
                {
                    ViewBag.Users = UserList;
                    ViewBag.Error_Msg = "There is error in Fetching Client Details";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Users = UserList;
                ViewBag.Error_Msg = ex.Message.ToString(); ;
                BAL.Common.LogManager.LogError("ViewClient Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(model);
            }
        }

        //[Authorize]
        //[HttpPost]
        //public ActionResult AjaxUpdate(ClientModel objClientmodel, string TextField)
        //{
        //    objResponse Response = new objResponse();
        //    Project.Entity.Clients objClient = new Entity.Clients();
        //    try
        //    {
        //        if (objClientmodel.Date != null)
        //        {
        //            objClient.Date = BAL.Helper.Helper.ConvertToDateNullable(objClientmodel.Date, "dd/MM/yyyy");
        //        }

        //        objClient.Name = objClientmodel.Name;
        //        objClient.CompanyName = objClientmodel.CompanyName;
        //        objClient.Email = objClientmodel.Email;
        //        objClient.ContactNo = objClientmodel.ContactNo;
        //        objClient.SkypeNo = objClientmodel.SkypeNo;
        //        objClient.Model = objClientmodel.Model;
        //        objClient.ZipCode = objClientmodel.ZipCode;

        //        objClient.AddressLine1 = objClientmodel.AddressLine1;
        //        objClient.AddressLine2 = objClientmodel.AddressLine2;
        //        objClient.City = objClientmodel.City;
        //        objClient.State = objClientmodel.State;
        //        objClient.Country = objClientmodel.Country;
        //        objClient.Alternate_Email = objClientmodel.Alternate_Email;
        //        objClient.Source = objClientmodel.Source;
        //        objClient.Client_ID_Auto_PK = objClientmodel.Client_ID;
        //        session = new SessionHelper();
        //        Response = objClientsManager.UpdateClient(objClient, session.UserSession.Username, TextField);


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
        public ActionResult UpdateStatus(string ClientID, string status, string Note, string Followupdate)
        {
            session = new SessionHelper();
            objResponse Response = new objResponse();
            try
            {
                DateTime FollowUp = BAL.Helper.Helper.ConvertToDateNullable(Followupdate, "dd/MM/yyyy");
                //Response = objClientsManager.UpdateLeadDetails(Convert.ToInt64(ClientID), status, Note, FollowUp, session.UserSession.Username);

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
                BAL.Common.LogManager.LogError("UpdateStatus Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult addComment(string ClientID, string coment)
        {
            session = new SessionHelper();
            objResponse res = new objResponse();
            try
            {
                res = objClientsManager.AddComment(Convert.ToInt64(ClientID), coment, session.UserSession.UserId);

                if (res.ErrorCode == 0)
                {
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
                BAL.Common.LogManager.LogError("ChangeStatus Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);

            }


        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AjaxClientActivity(string ClientID)
        {
            ClientModel obClientModel = new ClientModel();
            session = new SessionHelper();
            try
            {
                obClientModel.activities = UtilityManager.getActivityByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(ClientID), "CLIENT");
                return View(obClientModel);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxClientActivity Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult UpdateClient(string Client_Id)
        {
            ClientModel objClientModel = new ClientModel();
            objResponse Response = new objResponse();
            session = new SessionHelper();
            List<TextValue> source = new List<TextValue>();
            source = objSetingManager.GetDropDownListing("Client", "Source", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var cat in source)
            {
                list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
            }
            ViewBag.Source_List = list;
            
            try
            {
                Response = objClientsManager.getClientforUpdate(Convert.ToInt64(Client_Id));
                if (Response.ErrorCode == 0)
                {
                    objClientModel.Client_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Client_ID_Auto_PK"]);
                    objClientModel.Name = Response.ResponseData.Tables[0].Rows[0]["Name"].ToString();
                    objClientModel.CompanyName = Response.ResponseData.Tables[0].Rows[0]["CompanyName"].ToString();
                    objClientModel.ContactNo = Response.ResponseData.Tables[0].Rows[0]["ContactNo"].ToString();
                    objClientModel.AddressLine1 = Response.ResponseData.Tables[0].Rows[0]["AddressLine1"].ToString();
                    objClientModel.AddressLine2 = Response.ResponseData.Tables[0].Rows[0]["AddressLine2"].ToString();
                    objClientModel.Alternate_Email = Response.ResponseData.Tables[0].Rows[0]["Alternate_Email"].ToString();
                    objClientModel.Email = Response.ResponseData.Tables[0].Rows[0]["Email"].ToString();
                    objClientModel.Country = Response.ResponseData.Tables[0].Rows[0]["Country"].ToString();
                    objClientModel.City = Response.ResponseData.Tables[0].Rows[0]["City"].ToString();
                    objClientModel.SkypeNo = Response.ResponseData.Tables[0].Rows[0]["SkypeNo"].ToString();
                    objClientModel.Model = Response.ResponseData.Tables[0].Rows[0]["Model"].ToString();
                    objClientModel.ZipCode = Response.ResponseData.Tables[0].Rows[0]["ZipCode"].ToString();
                    objClientModel.Source = Response.ResponseData.Tables[0].Rows[0]["Source"].ToString();
                    objClientModel.State = Response.ResponseData.Tables[0].Rows[0]["State"].ToString();
                    objClientModel.JobDescription = Response.ResponseData.Tables[0].Rows[0]["JobDescription"].ToString();

                    ViewBag.Source_List = list;
                    return View(objClientModel);


                }
                else
                {
                    ViewBag.Source_List = list;
                    return View(objClientModel);
                }

            }
            catch (Exception ex)
            {
                ViewBag.Source_List = list;
                BAL.Common.LogManager.LogError("UpdateClient Get Method", 1, Convert.ToString(ex.Message), Convert.ToString(ex.Source), Convert.ToString(ex.StackTrace));
                return View(objClientModel);
            }



        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult UpdateClient(ClientModel objClientModel)
        {
            objResponse Response = new objResponse();
            Project.Entity.Clients objClient = new Entity.Clients();
            session = new SessionHelper();
            List<TextValue> source = new List<TextValue>();
            source = objSetingManager.GetDropDownListing("Client", "Source", Convert.ToInt64(session.UserSession.PIN));

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "0", Text = "Choose a Source" });

            foreach (var cat in source)
            {
                list.Add(new SelectListItem { Value = cat.Value, Text = cat.Text });
            }
            ViewBag.Source_List = list;
            string route = "/Clients/ViewClient?sessionid=" + Guid.NewGuid().ToString() + "&Clientid=" + objClientModel.Client_ID;
            try
            {
                //objClient.Date = BAL.Helper.Helper.ConvertToDateNullable(objClientModel.Date, "dd/MM/yyyy");                
                objClient.Name = objClientModel.Name;
                objClient.CompanyName = objClientModel.CompanyName;
                objClient.Email = objClientModel.Email;
                objClient.ContactNo = objClientModel.ContactNo;
                objClient.SkypeNo = objClientModel.SkypeNo;
                objClient.Model = objClientModel.Model;
                objClient.ZipCode = objClientModel.ZipCode;
                objClient.AddressLine1 = objClientModel.AddressLine1;
                objClient.AddressLine2 = objClientModel.AddressLine2;
                objClient.City = objClientModel.City;
                objClient.State = objClientModel.State;
                objClient.Country = objClientModel.Country;
                objClient.Alternate_Email = objClientModel.Alternate_Email;
                objClient.Source = objClientModel.Source;
                objClient.jobDescription = objClientModel.JobDescription;
                objClient.Client_ID_Auto_PK = objClientModel.Client_ID;
                session = new SessionHelper();

                Response = objClientsManager.UpdateClient(objClient, session.UserSession.UserId );

                if (Response.ErrorCode == 0)
                {
                    //ViewBag.Source_list = list;
                    //return View(objClientModel);
                    //return RedirectToRoute("ClientHome");
                    return Redirect(route);

                }
                else
                {
                    ViewBag.Error_msg = Response.ErrorMessage;

                    //ViewBag.Source_List = List;
                    return View(objClientModel);
                    //return RedirectToRoute("ClientHome");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_msg = Response.ErrorMessage;
                BAL.Common.LogManager.LogError("UpdateClient Post method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objClientModel);
               // return RedirectToRoute("ClientHome");


            }

        }

        [Authorize]        
        public ActionResult DataSync()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async void GetContactPermission()
        {
            GooglePlusAccessToken objToken = new GooglePlusAccessToken();
            List<TextValue> objNameEmailCollection = new List<TextValue>();
            session = new SessionHelper();
            try
            {
                if (Request.QueryString["code"] != null)
                {    
                    objNameEmailCollection = objClientsManager.getClientContactForSync(session.UserSession.UserId,Convert.ToInt64(session.UserSession.PIN));
                    objToken = await GetAccessToken(Request.QueryString["code"].ToString());
                    await AddContacts(objToken,objNameEmailCollection);
                }
                else
                {
                    string clientId = "995459998390-h5ea19firkf4q99mp44uic5qk8djtrnb.apps.googleusercontent.com";
                    string redirectUrl = "http://testerp.easysavemerchants.com/UserSetings/GetContactPermission";
                    Response.Redirect("https://accounts.google.com/o/oauth2/auth?redirect_uri=" + redirectUrl + "&response_type=code&client_id=" + clientId + "&scope=https://www.google.com/m8/feeds/&approval_prompt=force&access_type=offline");
                }
                
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetContactPermission Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                // return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ContactPermissionCallback()
        {
            GooglePlusAccessToken objToken = new GooglePlusAccessToken();
            objResponse Response = new objResponse();
          //  DataSyncManager objData = new DataSyncManager();
            session = new SessionHelper();
            try
            {
                if (Request.QueryString["code"] != null)
                {
                  //  objToken = GetAccessToken(Request.QueryString["code"].ToString());                
                }
                else
                {
                    return RedirectToRoute("UserSetings");
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("ContactPermissionCallback Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return RedirectToRoute("UserSetings");
            }
            return RedirectToRoute("ClientHome");
        }

        #region G-Contacts Methods
        public async Task<GooglePlusAccessToken> GetAccessToken(string code)
        {
            // string code = Request.QueryString["code"];
            string google_client_id = "995459998390-h5ea19firkf4q99mp44uic5qk8djtrnb.apps.googleusercontent.com";
            string google_client_sceret = "yCrtm5O-K_aWSmE3hzqq3pWD";
            string google_redirect_url = "http://testerp.easysavemerchants.com/UserSetings/ContactPermissionCallback";

            /*Get Access Token and Refresh Token*/
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
            webRequest.Method = "POST";
            string parameters = "code=" + code + "&client_id=" + google_client_id + "&client_secret=" + google_client_sceret + "&redirect_uri=" + google_redirect_url + "&grant_type=authorization_code";
            byte[] byteArray = Encoding.UTF8.GetBytes(parameters);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            Stream postStream = webRequest.GetRequestStream();

            // Add the post data to the web request
            await postStream.WriteAsync(byteArray, 0, byteArray.Length);
            postStream.Close();
            WebResponse response = await webRequest.GetResponseAsync();
            postStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(postStream);
            string responseFromServer = await reader.ReadToEndAsync();
            GooglePlusAccessToken serStatus = JsonConvert.DeserializeObject<GooglePlusAccessToken>(responseFromServer);
            /*End*/
            return serStatus;
            // GetContacts(serStatus);
        }

        public class GooglePlusAccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
        }

        public void GetContacts(GooglePlusAccessToken serStatus)
        {
            /*Get Google Contacts From Access Token and Refresh Token*/
            string refreshToken = serStatus.refresh_token;
            string accessToken = serStatus.access_token;
            string scopes = "https://www.google.com/m8/feeds/contacts/default/full/";
            OAuth2Parameters oAuthparameters = new OAuth2Parameters()
            {
                Scope = scopes,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            RequestSettings settings = new RequestSettings("<var>YOUR_APPLICATION_NAME</var>", oAuthparameters);
            ContactsRequest cr = new ContactsRequest(settings);
            ContactsQuery query = new ContactsQuery(ContactsQuery.CreateContactsUri("default"));
            query.NumberToRetrieve = 5000;
            Feed<Contact> feed = cr.Get<Contact>(query);

            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (Contact entry in feed.Entries)
            {
                foreach (EMail email in entry.Emails)
                {
                    sb.Append("<span>" + i + ". </span>").Append(email.Address)
                      .Append("<br/>");
                    i++;
                }
            }
            /*End*/

            //dataDiv.InnerHtml = sb.ToString();
        }

        public async Task<bool> AddContacts(GooglePlusAccessToken serStatus, List<TextValue> objContacts)
        {
            /*Get Google Contacts From Access Token and Refresh Token*/
            string refreshToken = serStatus.refresh_token;
            string accessToken = serStatus.access_token;
            string scopes = "https://www.google.com/m8/feeds/contacts/default/full/";
            OAuth2Parameters oAuthparameters = new OAuth2Parameters()
            {
                Scope = scopes,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            RequestSettings settings = new RequestSettings("<var>YOUR_APPLICATION_NAME</var>", oAuthparameters);
            ContactsRequest cr = new ContactsRequest(settings);
            foreach(var contact in objContacts)
            {
                await SynchContact(cr, contact.Text, contact.Value);
            }
            return true;
        }

        public async Task<bool> SynchContact(ContactsRequest cr, string Name, string Email)
        {
            try
            {
                Contact newEntry = new Contact();
                newEntry.Name = new Name()
                {
                    FullName = Name
                };

                newEntry.Emails.Add(new EMail()
                {
                    Primary = true,
                    Rel = ContactsRelationships.IsWork,
                    Address = Email
                });

                newEntry.IMs.Add(new IMAddress()
                {
                    Primary = true,
                    Rel = ContactsRelationships.IsWork,
                    Protocol = ContactsProtocols.IsGoogleTalk,
                });

                Uri feedUri = new Uri(ContactsQuery.CreateContactsUri("default"));
                Contact createdEntry = cr.Insert(feedUri, newEntry);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
       
        #endregion
    }
}
