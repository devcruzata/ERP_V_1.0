using BAL.DataSynch;
using BAL.Setings;
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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Project.Web.Controllers.UserSetings
{
    public class UserSetingsController : Controller
    {
        SetingManager objSetingManager = new SetingManager();
        JavaScriptSerializer objJavaScriptSerializer = new JavaScriptSerializer();

        BAL.Roles.RolesManager objRoleManager = new BAL.Roles.RolesManager();
        SessionHelper session;

        //
        // GET: /UserSetings/
        

        [Authorize]
        [SessionTimeOut]
        public ActionResult SetingsHome()
        {
            objResponse Response = new objResponse();
            GenralSetingModel objSetings = new GenralSetingModel();
            session = new SessionHelper();
            try
            {
                Response = objSetingManager.getGenralSeting(Convert.ToInt64(session.UserSession.PIN));
                if (Response.ErrorCode == 0)
                {
                    objSetings.GenralSeting_ID_Auto_PK = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Genral_Seting_ID_Auto_PK"]);
                    objSetings.Company = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["ComapanyName"]);
                    objSetings.Address = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Address"]);
                    objSetings.City = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["City"]);
                    objSetings.Stete = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["State"]);
                    objSetings.Country = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Country"]);
                    objSetings.Zipcode = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Zipcode"]);
                    objSetings.Phone = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Phone"]);
                    objSetings.Website = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Website"]);
                    objSetings.Currency = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Currency"]);

                  //  objSetings.roles = objRoleManager.GetAllRoles();


                    return View(objSetings);
                }
                else
                {
                    objSetings.roles = objRoleManager.GetAllRoles();
                    return View(objSetings);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("SetingsHome conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objSetings);
            }
        }


        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AjaxGenralSetings(GenralSetingModel objModel)
        {
            objResponse Response = new objResponse();
            GenralSetingModel objSetings = new GenralSetingModel();
            GenralSeting objGenralSetings = new GenralSeting();
            session = new SessionHelper();
            try
            {
                objGenralSetings.Company = objModel.Company;
                objGenralSetings.Address = objModel.Address;
                objGenralSetings.City = objModel.City;
                objGenralSetings.Stete = objModel.Stete;
                objGenralSetings.Country = objModel.Country;
                objGenralSetings.Zipcode = objModel.Zipcode;
                objGenralSetings.Phone = objModel.Phone;
                objGenralSetings.Website = objModel.Website;
                objGenralSetings.Currency = objModel.Currency;
                objGenralSetings.Customer_ID = Convert.ToInt64(session.UserSession.PIN);

                Response = objSetingManager.AddCompanyProfile(objGenralSetings, session.UserSession.UserId);

                if (Response.ErrorCode == 0)
                {

                    Response = objSetingManager.getGenralSeting(Convert.ToInt64(session.UserSession.PIN));
                    if (Response.ErrorCode == 0)
                    {
                        objSetings.GenralSeting_ID_Auto_PK = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Genral_Seting_ID_Auto_PK"]);
                        objSetings.Company = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["ComapanyName"]);
                        objSetings.Address = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Address"]);
                        objSetings.City = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["City"]);
                        objSetings.Stete = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["State"]);
                        objSetings.Country = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Country"]);
                        objSetings.Zipcode = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Zipcode"]);
                        objSetings.Phone = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Phone"]);
                        objSetings.Website = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Website"]);
                        objSetings.Currency = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Currency"]);
                       
                    }                    
                    return View(objSetings);
                }
                else
                {
                    return View(objSetings);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxGenralSetings Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objSetings);
            }
        }
        

        [Authorize]
        [SessionTimeOut]
        public ActionResult GetDropListing(string Module, string DropName)
        {
            DropDownModel objModel = new DropDownModel();
            if (Module == null || DropName == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                session = new SessionHelper();
                List<TextValue> options = new List<TextValue>();
                objModel.options = objSetingManager.GetDropDownListing(Module, DropName, Convert.ToInt64(session.UserSession.PIN));               
                return View("TempData", objModel);
            }            
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AjaxManageDropListOption(string OptionText ,string OptionID ,string Module , string DropListName)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            DropDownModel objModel = new DropDownModel();          
            try
            {
               
                    if (OptionID == "")
                    {                        
                        OptionID = "0";                        
                    }

                    Response = objSetingManager.ManageDroplistOption(OptionText,Convert.ToInt64(OptionID), Module, DropListName, Convert.ToInt64(session.UserSession.PIN), session.UserSession.UserId);

                    if (Response.ErrorCode == 0)
                    {

                        objModel.options = objSetingManager.GetDropDownListing(Module, DropListName, Convert.ToInt64(session.UserSession.PIN));                      
                        return View("TempData", objModel);
                        
                    }
                    else
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }             
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxManageDropListOption Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult DeleteDropListOption(string OptionID, string Module, string DropListName)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            DropDownModel objModel = new DropDownModel();    
            
            try
            {
                string Result = objSetingManager.DeleteDropListOption(Convert.ToInt64(OptionID),Module,DropListName, Convert.ToInt64(session.UserSession.PIN));

                if (Result == "1")
                {
                    objModel.options = objSetingManager.GetDropDownListing(Module, DropListName, Convert.ToInt64(session.UserSession.PIN));                  
                    return View("TempData", objModel);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteDropListOptio Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult MailSeting()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetRoles()
        {
            objResponse Response = new objResponse();           
            RolesModel objRolesModel = new RolesModel();
            try
            {                
                    objRolesModel.roles = objRoleManager.GetAllRoles();
                    return View("AjaxManageRoles", objRolesModel);               

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxUpdateRoles Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult AjaxManageRoles(RolesModel objModel)
        {
            objResponse Response = new objResponse();
            UserRoles objRoles = new UserRoles();
            RolesModel objRolesModel = new RolesModel();
            session = new SessionHelper();
            try
            {
                objRoles = objJavaScriptSerializer.Deserialize<Project.Entity.UserRoles>(objJavaScriptSerializer.Serialize(objModel));
                Response = objRoleManager.AddRole(objRoles,Convert.ToInt64(session.UserSession.PIN));

                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "Role Already Exists")
                    {
                        objRolesModel.roles = objRoleManager.GetAllRoles();
                        return View(objRolesModel);
                    }
                    else
                    {
                        return Json("Role Already Exists", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxManageRoles Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult GetRolesForEdit(string RoleID)
        {
            objResponse Response = new objResponse();
            RolesModel objUserRoles = new RolesModel();
            try
            {
                Response = objRoleManager.GetRolesForEdit(Convert.ToInt64(RoleID));
                if (Response.ErrorCode == 0)
                {
                    objUserRoles.RoleName = Response.ResponseData.Tables[0].Rows[0]["User_Role_Desc"].ToString();
                    objUserRoles.Role_ID = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["User_Role_ID_Auto_Pk"]);

                    objUserRoles.AssociatedLeads = Response.ResponseData.Tables[0].Rows[0]["AssociatedLeads"].ToString();
                    objUserRoles.SystemwideLeads = Response.ResponseData.Tables[0].Rows[0]["SystemwideLeads"].ToString();
                    objUserRoles.AssociatedOpportunity = Response.ResponseData.Tables[0].Rows[0]["AssociatedOpportunity"].ToString();
                    objUserRoles.SystemwideOpportunity = Response.ResponseData.Tables[0].Rows[0]["SystemwideOpportunity"].ToString();
                    objUserRoles.AssociatedClients = Response.ResponseData.Tables[0].Rows[0]["AssociatedClients"].ToString();
                    objUserRoles.SystemwideClients = Response.ResponseData.Tables[0].Rows[0]["SystemwideClients"].ToString();
                    objUserRoles.Calendar = Response.ResponseData.Tables[0].Rows[0]["Calendar"].ToString();
                    objUserRoles.Task = Response.ResponseData.Tables[0].Rows[0]["Task"].ToString();
                    objUserRoles.Notes = Response.ResponseData.Tables[0].Rows[0]["Notes"].ToString();
                    objUserRoles.Documents = Response.ResponseData.Tables[0].Rows[0]["Documents"].ToString();                    
                    objUserRoles.UserManagement = Response.ResponseData.Tables[0].Rows[0]["UserManagement"].ToString();
                    objUserRoles.SiteManagement = Response.ResponseData.Tables[0].Rows[0]["SiteManagement"].ToString();                   

                    return Json(objUserRoles, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetRolesForEdit Get Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxUpdateRoles(RolesModel objModel)
        {
            objResponse Response = new objResponse();
            UserRoles objRoles = new UserRoles();
            RolesModel objRolesModel = new RolesModel();
            try
            {
                objRoles = objJavaScriptSerializer.Deserialize<Project.Entity.UserRoles>(objJavaScriptSerializer.Serialize(objModel));
                Response = objRoleManager.UpdateRole(objRoles);

                if (Response.ErrorCode == 0)
                {
                    objRolesModel.roles = objRoleManager.GetAllRoles();
                    return View("AjaxManageRoles", objRolesModel);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxUpdateRoles Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public void GetContactPermission()
        {
            try
            {
                string clientId = "995459998390-h5ea19firkf4q99mp44uic5qk8djtrnb.apps.googleusercontent.com";
                string redirectUrl = "http://testerp.easysavemerchants.com/UserSetings/ContactPermissionCallback";
                Response.Redirect("https://accounts.google.com/o/oauth2/auth?redirect_uri="+redirectUrl+"&response_type=code&client_id=" + clientId + "&scope=https://www.google.com/m8/feeds/&approval_prompt=force&access_type=offline");
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
            DataSyncManager objData = new DataSyncManager();
            session = new SessionHelper();
            try
            {
                if (Request.QueryString["code"] != null)
                {
                    objToken = GetAccessToken(Request.QueryString["code"].ToString());
                    Response = objData.SetGContactSeting(objToken.access_token, objToken.token_type, objToken.expires_in, objToken.refresh_token, "contact", Convert.ToInt64(session.UserSession.PIN),session.UserSession.UserId);

                    if (Response.ErrorCode == 0)
                    {
                        return RedirectToRoute("UserSetings");
                    }
                    else
                    {
                        return RedirectToRoute("UserSetings");
                    }
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
            
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddMailSetings(string hosttype,string hosturl,string port,string username,string password,bool isssl,string msettingId,string settingtype)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                if (msettingId == "")
                {
                    msettingId = "0";
                }
                Response = objSetingManager.AddMailSetings(hosttype,hosturl,port,username,password,isssl,session.UserSession.UserId,Convert.ToInt64(session.UserSession.PIN),Convert.ToInt64(msettingId),settingtype);

                if (Response.ErrorCode == 0)
                {
                    if (settingtype == "INBOUND")
                    {
                        session.UserSetingSession.inboundMailboxType = hosttype;
                        session.UserSetingSession.inboundMailboxHost = hosturl;
                        session.UserSetingSession.inboundMailport = port;
                        session.UserSetingSession.inboundMailUsername = username;
                        session.UserSetingSession.inboundMailPassword = password;
                        session.UserSetingSession.inboundIsSsl = isssl;
                    }
                    else
                    {
                        session.UserSetingSession.smtpUsername = username;
                        session.UserSetingSession.smtpPassword = password;
                        session.UserSetingSession.smtpHost = hosturl;
                        session.UserSetingSession.smtpPort = port;
                        session.UserSetingSession.smtpIsSsl = isssl;
                    }
                    return Json(Response.ErrorMessage, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AddMailSetings Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetMailSetings()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            MailSeting objMailSeting = new MailSeting();
            try
            {
                Response = objSetingManager.GetEmailSetings(session.UserSession.UserId, Convert.ToInt64(session.UserSession.PIN));

                if (Response.ErrorCode == 0)
                {
                    objMailSeting.inMailSeting_Id_Pk = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Mail_Setting_ID_Auto_Pk"]);
                    objMailSeting.inHostType = Response.ResponseData.Tables[0].Rows[0]["HostType"].ToString();
                    objMailSeting.inHostUrl = Response.ResponseData.Tables[0].Rows[0]["HostUrl"].ToString();
                    objMailSeting.inPort = Response.ResponseData.Tables[0].Rows[0]["HportNo"].ToString();
                    objMailSeting.inIsssl = Convert.ToBoolean(Response.ResponseData.Tables[0].Rows[0]["IsSSlEnabled"]);
                    objMailSeting.inUsername = Response.ResponseData.Tables[0].Rows[0]["Username"].ToString();
                    objMailSeting.inPassword = Response.ResponseData.Tables[0].Rows[0]["Password"].ToString();
                    objMailSeting.oMailSeting_Id_Pk = Convert.ToInt64(Response.ResponseData.Tables[1].Rows[0]["Mail_Setting_ID_Auto_Pk"]);
                    objMailSeting.oHostUrl = Response.ResponseData.Tables[1].Rows[0]["HostUrl"].ToString();
                    objMailSeting.oPort = Response.ResponseData.Tables[1].Rows[0]["HportNo"].ToString();
                    objMailSeting.oIsssl = Convert.ToBoolean(Response.ResponseData.Tables[1].Rows[0]["IsSSlEnabled"]);
                    objMailSeting.oUsername = Response.ResponseData.Tables[1].Rows[0]["Username"].ToString();
                    objMailSeting.oPassword = Response.ResponseData.Tables[1].Rows[0]["Password"].ToString();

                    return Json(objMailSeting, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetMailSetings Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        #region G-Contacts Methods
        public GooglePlusAccessToken GetAccessToken(string code)
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
            postStream.Write(byteArray, 0, byteArray.Length);
            postStream.Close();
            WebResponse response = webRequest.GetResponse();
            postStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(postStream);
            string responseFromServer = reader.ReadToEnd();
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
                    sb.Append("<span>"+ i + ". </span>").Append(email.Address)
                      .Append("<br/>");
                    i++;
                }
            }
            /*End*/

            //dataDiv.InnerHtml = sb.ToString();
       }

        //public void AddContact(GooglePlusAccessToken serStatus)
        //{
        //    /*Get Google Contacts From Access Token and Refresh Token*/
        //    string refreshToken = serStatus.refresh_token;
        //    string accessToken = serStatus.access_token;
        //    string scopes = "https://www.google.com/m8/feeds/contacts/default/full/";
        //    OAuth2Parameters oAuthparameters = new OAuth2Parameters()
        //    {
        //        Scope = scopes,
        //        AccessToken = accessToken,
        //        RefreshToken = refreshToken
        //    };

        //    RequestSettings settings = new RequestSettings("<var>YOUR_APPLICATION_NAME</var>", oAuthparameters);
        //    ContactsRequest cr = new ContactsRequest(settings);

        //    Contact newEntry = new Contact();
        //    newEntry.Name = new Name();
        //    newEntry.Name.FullName = "Abhishek k";

        //    newEntry.Emails = new
        //}

        public static Contact CreateContact(ContactsRequest cr)
{
              Contact newEntry = new Contact();
              // Set the contact's name.
              newEntry.Name = new Name()
                  {
                    FullName = "Elizabeth Bennet",
                    GivenName = "Elizabeth",
                    FamilyName = "Bennet",
                  };
              newEntry.Content = "Notes";
              // Set the contact's e-mail addresses.
              newEntry.Emails.Add(new EMail()
                  {
                    Primary = true,
                    Rel = ContactsRelationships.IsHome,
                    Address = "liz@gmail.com"
                  });
              newEntry.Emails.Add(new EMail()
                  {
                    Rel = ContactsRelationships.IsWork,
                    Address = "liz@example.com"
                  });
              // Set the contact's phone numbers.
              newEntry.Phonenumbers.Add(new PhoneNumber()
                  {
                    Primary = true,
                    Rel = ContactsRelationships.IsWork,
                    Value = "(206)555-1212",
                  });
              newEntry.Phonenumbers.Add(new PhoneNumber()
                  {
                    Rel = ContactsRelationships.IsHome,
                    Value = "(206)555-1213",
                  });
              // Set the contact's IM information.
              newEntry.IMs.Add(new IMAddress()
                  {
                    Primary = true,
                    Rel = ContactsRelationships.IsHome,
                    Protocol = ContactsProtocols.IsGoogleTalk,
                  });
  // Set the contact's postal address.
  newEntry.PostalAddresses.Add(new StructuredPostalAddress()
      {
        Rel = ContactsRelationships.IsWork,
        Primary = true,
        Street = "1600 Amphitheatre Pkwy",
        City ="Mountain View",
        Region = "CA",
        Postcode = "94043",
        Country = "United States",
        FormattedAddress = "1600 Amphitheatre Pkwy Mountain View",
      });
  // Insert the contact.
  Uri feedUri = new Uri(ContactsQuery.CreateContactsUri("default"));
  Contact createdEntry = cr.Insert(feedUri, newEntry);
  Console.WriteLine("Contact's ID: " + createdEntry.Id);
  return createdEntry;
}
        #endregion
     
   }
}
