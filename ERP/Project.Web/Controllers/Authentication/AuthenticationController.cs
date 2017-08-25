using BAL.User;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project.Web.Controllers.Authentication
{
    public class AuthenticationController : Controller
    {
        UserManager objUserManager = new UserManager();
        //
        // GET: /Authentication/

        public ActionResult Login()
        {            
            return View();
        }

       

        //[HttpPost]
        //public ActionResult Login(LoginModel model)
        //{
        //    objResponse Response = new objResponse();
            
        //    try
        //    {
        //        Response = objUserManager.validateUser(model.UserName, model.Password);

        //        if (Response.ErrorCode == 0)
        //        {
        //            if (Response.ErrorMessage != "Incorrect UserName" && Response.ErrorMessage != "Incorrect Password")
        //            {
        //                FormsAuthentication.SetAuthCookie(model.UserName, false);
        //                //Session["User"] = Response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + Response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString();
        //               // Session["User_Type"] = Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString();
        //                  Session["UserName"] = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString();
        //              //  Session["UserID_PK"] = Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"].ToString();
        //             //   Session["UserID_PK"] = Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"].ToString();
        //                SessionHelper session = new SessionHelper();
        //                session.UserSession = new UserSession()
        //                {
        //                    UserId = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"]),
        //                    uid = Response.ResponseData.Tables[0].Rows[0]["UserId"].ToString(),
        //                    Username = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString(),
        //                    FullName = Response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + Response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString(),
        //                    Phone = Response.ResponseData.Tables[0].Rows[0]["User_Contact"].ToString(),
        //                    Email = Response.ResponseData.Tables[0].Rows[0]["User_Email"].ToString(),
        //                    Address = Response.ResponseData.Tables[0].Rows[0]["User_Address"].ToString(),
        //                    UserType = Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString(),
        //                    //Subscription_ID = Response.ResponseData.Tables[0].Rows[0]["Subscription_ID"].ToString(),
        //                    PIN = Response.ResponseData.Tables[0].Rows[0]["PIN"].ToString()
        //                };
        //                session.UserSubscription = new UserSubsInfo()
        //                {
        //                    sToken = Response.ResponseData.Tables[0].Rows[0]["stripeToken"].ToString(),
        //                    sCustID = Response.ResponseData.Tables[0].Rows[0]["stripeCustoerID"].ToString(),
        //                    sSubscriptionID = Response.ResponseData.Tables[0].Rows[0]["stripeSubscriptionID"].ToString(),
        //                    planType = Response.ResponseData.Tables[0].Rows[0]["plan_ID_FK"].ToString()
        //                };
        //                session.NotificationSession = new NotificationSession() {
        //                    totalNoOfNotification = 0,
        //                    lastNotificationAt = Response.ResponseData.Tables[0].Rows[0]["LogoutAt"].ToString()
        //                };
        //                if (Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString() == "SUP")
        //                {
        //                    return RedirectToAction("SuperAdminDashboard", "Home");
        //                }
        //                else if (Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString() == "ADM")
        //                {                                     
        //                    return RedirectToRoute("AdminDashboard_V_2");                          
        //                }
        //                else
        //                {                                         
        //                    return RedirectToRoute("AdminDashboard_V_2");
        //                }
                        
        //            }
        //            else
        //            {
        //                ViewBag.Error_Msg = Response.ErrorMessage;
        //                TempData["Error_Msg"] = Response.ErrorMessage;                      
        //                return RedirectToAction("Login","Authentication");
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.Error_Msg = Response.ErrorMessage;
        //            TempData["Error_Msg"] = Response.ErrorMessage;                 
        //            return RedirectToAction("Login", "Authentication");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error_Msg = ex.Message.ToString();
        //        TempData["Error_Msg"] = ex.Message.ToString();
        //        BAL.Common.LogManager.LogError("Login Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));             
        //        return RedirectToAction("Login", "Authentication");
        //    }
        //}

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            objResponse Response = new objResponse();
            UserSetingInfo objUserSetings = new UserSetingInfo();
            try
            {
                Response = objUserManager.validateUser(model.UserName, model.Password);

                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "Incorrect UserName" && Response.ErrorMessage != "Incorrect Password")
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, false);
                        //Session["User"] = Response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + Response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString();
                        // Session["User_Type"] = Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString();
                        Session["UserName"] = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString();
                        //  Session["UserID_PK"] = Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"].ToString();
                        //   Session["UserID_PK"] = Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"].ToString();
                        SessionHelper session = new SessionHelper();
                        session.UserSession = new UserSession()
                        {
                            UserId = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"]),
                            uid = Response.ResponseData.Tables[0].Rows[0]["UserId"].ToString(),
                            Username = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString(),
                            FullName = Response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + Response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString(),
                            Phone = Response.ResponseData.Tables[0].Rows[0]["User_Contact"].ToString(),
                            Email = Response.ResponseData.Tables[0].Rows[0]["User_Email"].ToString(),
                            Address = Response.ResponseData.Tables[0].Rows[0]["User_Address"].ToString(),
                            UserType = Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString(),
                            PIN = Response.ResponseData.Tables[0].Rows[0]["PIN"].ToString()
                        };
                        session.UserSubscription = new UserSubsInfo()
                        {
                            sToken = Response.ResponseData.Tables[0].Rows[0]["stripeToken"].ToString(),
                            sCustID = Response.ResponseData.Tables[0].Rows[0]["stripeCustoerID"].ToString(),
                            sSubscriptionID = Response.ResponseData.Tables[0].Rows[0]["stripeSubscriptionID"].ToString(),
                            planType = Response.ResponseData.Tables[0].Rows[0]["plan_ID_FK"].ToString(),
                            planId = Response.ResponseData.Tables[0].Rows[0]["plan_ID_FK"].ToString()
                        };
                        session.NotificationSession = new NotificationSession()
                        {
                            totalNoOfNotification = 0,
                            lastNotificationAt = Response.ResponseData.Tables[0].Rows[0]["LogoutAt"].ToString(),
                            lastNotificationViewedAt = Response.ResponseData.Tables[0].Rows[0]["LogoutAt"].ToString()
                        };
                        foreach (DataRow dr in Response.ResponseData.Tables[1].Rows)
                        {
                            if (dr["SetingType"].ToString() == "OUTBOUND")
                            {
                                objUserSetings.smtpHost = dr["HostUrl"].ToString();
                                objUserSetings.smtpPort = dr["HportNo"].ToString();
                                objUserSetings.smtpIsSsl = Convert.ToBoolean(dr["IsSSlEnabled"]);
                                objUserSetings.smtpUsername = dr["Username"].ToString();
                                objUserSetings.smtpPassword = dr["Password"].ToString();
                            }
                            if (dr["SetingType"].ToString() == "INBOUND")
                            {
                                objUserSetings.inboundMailboxHost = dr["HostUrl"].ToString();
                                objUserSetings.inboundMailboxType = dr["HostType"].ToString();
                                objUserSetings.inboundMailport = dr["HportNo"].ToString();
                                objUserSetings.inboundIsSsl = Convert.ToBoolean(dr["IsSSlEnabled"]);
                                objUserSetings.inboundMailUsername = dr["Username"].ToString();
                                objUserSetings.inboundMailPassword = dr["Password"].ToString();
                            }
                        }
                        session.UserSetingSession = objUserSetings;

                        if (Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString() == "SUP")
                        {
                            return RedirectToAction("SuperAdminDashboard", "Home");
                        }
                        else if (Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString() == "ADM")
                        {
                            return RedirectToRoute("AdminDashboard_V_2");
                        }
                        else
                        {
                            return RedirectToRoute("AdminDashboard_V_2");
                        }

                    }
                    else
                    {
                        ViewBag.Error_Msg = Response.ErrorMessage;
                        TempData["Error_Msg"] = Response.ErrorMessage;
                        return RedirectToAction("Login", "Authentication");
                    }
                }
                else
                {
                    ViewBag.Error_Msg = Response.ErrorMessage;
                    TempData["Error_Msg"] = Response.ErrorMessage;
                    return RedirectToAction("Login", "Authentication");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = ex.Message.ToString();
                TempData["Error_Msg"] = ex.Message.ToString();
                BAL.Common.LogManager.LogError("Login Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return RedirectToAction("Login", "Authentication");
            }
        }

        [Authorize]
        public ActionResult Logout()
        {
            SessionHelper session = new SessionHelper();

            objUserManager.LogUser(DateTime.Now, Convert.ToInt64(session.UserSession.UserId), Convert.ToInt64(session.UserSession.PIN));
            FormsAuthentication.SignOut();
            Session.Clear();            
            return Redirect("~/");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Authorize]
        public ActionResult LockScreen()
        {
            SessionHelper session = new SessionHelper();
            ViewBag.Username = session.UserSession.Email;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult LockScreen(string Username , string Password , string LastPage)
        {
            objResponse Response = new objResponse();
            UserSetingInfo objUserSetings = new UserSetingInfo();
            try
            {
                Response = objUserManager.validateUser(Username, Password);

                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "Incorrect UserName" && Response.ErrorMessage != "Incorrect Password")
                    {
                        FormsAuthentication.SetAuthCookie(Username, false);
                        //Session["User"] = Response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + Response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString();
                        // Session["User_Type"] = Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString();
                        Session["UserName"] = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString();
                        //  Session["UserID_PK"] = Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"].ToString();
                        //   Session["UserID_PK"] = Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"].ToString();
                        SessionHelper session = new SessionHelper();
                        session.UserSession = new UserSession()
                        {
                            UserId = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"]),
                            uid = Response.ResponseData.Tables[0].Rows[0]["UserId"].ToString(),
                            Username = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString(),
                            FullName = Response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + Response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString(),
                            Phone = Response.ResponseData.Tables[0].Rows[0]["User_Contact"].ToString(),
                            Email = Response.ResponseData.Tables[0].Rows[0]["User_Email"].ToString(),
                            Address = Response.ResponseData.Tables[0].Rows[0]["User_Address"].ToString(),
                            UserType = Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString(),
                            PIN = Response.ResponseData.Tables[0].Rows[0]["PIN"].ToString()
                        };
                        session.UserSubscription = new UserSubsInfo()
                        {
                            sToken = Response.ResponseData.Tables[0].Rows[0]["stripeToken"].ToString(),
                            sCustID = Response.ResponseData.Tables[0].Rows[0]["stripeCustoerID"].ToString(),
                            sSubscriptionID = Response.ResponseData.Tables[0].Rows[0]["stripeSubscriptionID"].ToString(),
                            planType = Response.ResponseData.Tables[0].Rows[0]["plan_ID_FK"].ToString(),
                            planId = Response.ResponseData.Tables[0].Rows[0]["plan_ID_FK"].ToString()
                        };
                        session.NotificationSession = new NotificationSession()
                        {
                            totalNoOfNotification = 0,
                            lastNotificationAt = Response.ResponseData.Tables[0].Rows[0]["LogoutAt"].ToString(),
                            lastNotificationViewedAt = Response.ResponseData.Tables[0].Rows[0]["LogoutAt"].ToString()

                        };
                        foreach (DataRow dr in Response.ResponseData.Tables[1].Rows)
                        {
                            if (dr["SetingType"].ToString() == "OUTBOUND")
                            {
                                objUserSetings.smtpHost = dr["HostUrl"].ToString();
                                objUserSetings.smtpPort = dr["HportNo"].ToString();
                                objUserSetings.smtpIsSsl = Convert.ToBoolean(dr["IsSSlEnabled"]);
                                objUserSetings.smtpUsername = dr["Username"].ToString();
                                objUserSetings.smtpPassword = dr["Password"].ToString();
                            }
                            if (dr["SetingType"].ToString() == "INBOUND")
                            {
                                objUserSetings.inboundMailboxHost = dr["HostUrl"].ToString();
                                objUserSetings.inboundMailboxType = dr["HostType"].ToString();
                                objUserSetings.inboundMailport = dr["HportNo"].ToString();
                                objUserSetings.inboundIsSsl = Convert.ToBoolean(dr["IsSSlEnabled"]);
                                objUserSetings.inboundMailUsername = dr["Username"].ToString();
                                objUserSetings.inboundMailPassword = dr["Password"].ToString();
                            }
                        }
                        session.UserSetingSession = objUserSetings;

                        if (Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString() == "SUP")
                        {
                            return RedirectToAction("SuperAdminDashboard", "Home");
                        }
                        else if (Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString() == "ADM")
                        {
                            return RedirectToRoute("AdminDashboard_V_2");
                        }
                        else
                        {
                            return RedirectToRoute("AdminDashboard_V_2");
                        }

                    }
                    else
                    {
                        ViewBag.Error_Msg = Response.ErrorMessage;
                        TempData["Error_Msg"] = Response.ErrorMessage;
                        return RedirectToAction("Login", "Authentication");
                    }
                }
                else
                {
                    ViewBag.Error_Msg = Response.ErrorMessage;
                    TempData["Error_Msg"] = Response.ErrorMessage;
                    return RedirectToAction("Login", "Authentication");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = ex.Message.ToString();
                TempData["Error_Msg"] = ex.Message.ToString();
                BAL.Common.LogManager.LogError("Login Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return RedirectToAction("Login", "Authentication");
            }
        }

        public ActionResult AccountActivation(string SubscriptionID, string Email)
        {

            try
            {
                ViewBag.Email = Email;
                ViewBag.PIN = SubscriptionID;
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Email = Email;
                ViewBag.PIN = SubscriptionID;
                BAL.Common.LogManager.LogError("AccountActivation Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View();
            }
        }

        [HttpPost]
        public ActionResult AccountActivation(string Username, string Password, string PIN)
        {
            objResponse Response = new objResponse();
            UserSetingInfo objUserSetings = new UserSetingInfo();
            try
            {
                Response = objUserManager.ActivateAccount(Username, Password, PIN);

                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "Incorrect SubscriptionID" && Response.ErrorMessage != "Incorrect Email")
                    {
                        FormsAuthentication.SetAuthCookie(Username, false);
                        //Session["User"] = Response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + Response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString();
                        // Session["User_Type"] = Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString();
                        Session["UserName"] = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString();
                        //  Session["UserID_PK"] = Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"].ToString();
                        //   Session["UserID_PK"] = Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"].ToString();
                        SessionHelper session = new SessionHelper();
                        session.UserSession = new UserSession()
                        {
                            UserId = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["User_ID_Auto_PK"]),
                            uid = Response.ResponseData.Tables[0].Rows[0]["UserId"].ToString(),
                            Username = Response.ResponseData.Tables[0].Rows[0]["UserName"].ToString(),
                            FullName = Response.ResponseData.Tables[0].Rows[0]["User_FirstName"].ToString() + " " + Response.ResponseData.Tables[0].Rows[0]["User_LastName"].ToString(),
                            Phone = Response.ResponseData.Tables[0].Rows[0]["User_Contact"].ToString(),
                            Email = Response.ResponseData.Tables[0].Rows[0]["User_Email"].ToString(),
                            Address = Response.ResponseData.Tables[0].Rows[0]["User_Address"].ToString(),
                            UserType = Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString(),
                            PIN = Response.ResponseData.Tables[0].Rows[0]["PIN"].ToString()
                        };
                        session.UserSubscription = new UserSubsInfo()
                        {
                            sToken = Response.ResponseData.Tables[0].Rows[0]["stripeToken"].ToString(),
                            sCustID = Response.ResponseData.Tables[0].Rows[0]["stripeCustoerID"].ToString(),
                            sSubscriptionID = Response.ResponseData.Tables[0].Rows[0]["stripeSubscriptionID"].ToString(),
                            planType = Response.ResponseData.Tables[0].Rows[0]["plan_ID_FK"].ToString(),
                            planId = Response.ResponseData.Tables[0].Rows[0]["plan_ID_FK"].ToString()
                        };
                        session.NotificationSession = new NotificationSession()
                        {
                            totalNoOfNotification = 0,
                            lastNotificationAt = Response.ResponseData.Tables[0].Rows[0]["LogoutAt"].ToString(),
                            lastNotificationViewedAt = Response.ResponseData.Tables[0].Rows[0]["LogoutAt"].ToString()
                        };
                        foreach (DataRow dr in Response.ResponseData.Tables[1].Rows)
                        {
                            if (dr["SetingType"].ToString() == "OUTBOUND")
                            {
                                objUserSetings.smtpHost = dr["HostUrl"].ToString();
                                objUserSetings.smtpPort = dr["HportNo"].ToString();
                                objUserSetings.smtpIsSsl = Convert.ToBoolean(dr["IsSSlEnabled"]);
                                objUserSetings.smtpUsername = dr["Username"].ToString();
                                objUserSetings.smtpPassword = dr["Password"].ToString();
                            }
                            if (dr["SetingType"].ToString() == "INBOUND")
                            {
                                objUserSetings.inboundMailboxHost = dr["HostUrl"].ToString();
                                objUserSetings.inboundMailboxType = dr["HostType"].ToString();
                                objUserSetings.inboundMailport = dr["HportNo"].ToString();
                                objUserSetings.inboundIsSsl = Convert.ToBoolean(dr["IsSSlEnabled"]);
                                objUserSetings.inboundMailUsername = dr["Username"].ToString();
                                objUserSetings.inboundMailPassword = dr["Password"].ToString();
                            }
                        }
                        session.UserSetingSession = objUserSetings;

                        if (Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString() == "SUP")
                        {
                            return RedirectToAction("SuperAdminDashboard", "Home");
                        }
                        else if (Response.ResponseData.Tables[0].Rows[0]["User_Type"].ToString() == "ADM")
                        {
                            return RedirectToRoute("AdminDashboard_V_2");
                        }
                        else
                        {
                            return RedirectToRoute("AdminDashboard_V_2");
                        }

                    }
                    else
                    {
                        ViewBag.Error_Msg = Response.ErrorMessage;
                        TempData["Error_Msg"] = Response.ErrorMessage;
                        return RedirectToAction("Login", "Authentication");
                    }
                }
                else
                {
                    ViewBag.Error_Msg = Response.ErrorMessage;
                    TempData["Error_Msg"] = Response.ErrorMessage;
                    return RedirectToAction("Login", "Authentication");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error_Msg = ex.Message.ToString();
                TempData["Error_Msg"] = ex.Message.ToString();
                BAL.Common.LogManager.LogError("Login Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return RedirectToAction("Login", "Authentication");
            }
        }

    }
}
