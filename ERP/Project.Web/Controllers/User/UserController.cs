using BAL.User;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.User
{
    public class UserController : Controller
    {
        UserManager objUserManager = new UserManager();

        SessionHelper session;

        //
        // GET: /User/
        [Authorize]
        [SessionTimeOut]
        public ActionResult UserHome()
        {
            UserModel objModel = new UserModel();
            session = new SessionHelper();
            objModel.users = objUserManager.GetUsers(session.UserSession.PIN);
            objModel.userTypes = objUserManager.GetUserType(session.UserSession.PIN);
            return View(objModel);
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AjaxAddUser(string FirstName, string LastName, string Email , string UserType )
        {
            UserModel objModel = new UserModel();
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {

                string Link = ConfigurationManager.AppSettings["Activation_Link"].ToString() + "?SubscriptionID=" + session.UserSession.PIN + "&Email=" + Email;
                Response = objUserManager.AddUser(Guid.NewGuid().ToString(),FirstName, LastName, Email, UserType, session.UserSession.uid,session.UserSession.PIN);

                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "User Already Added")
                    {
                        string body = "<b>Hi " + FirstName + " " + LastName + ",<b> <br/><br/>" + session.UserSession.FullName + " added you in his Cloderac Account. <br/><br/><a href=" +Link + ">Click Here To Activate Your Account</a><br/><br/>Enjoy,<br/><br/>Cloderac Team";
                        // await BAL.Helper.Helper.SendEmailUsingGoDaddy(Email, "Clouderac", body);

                        BAL.Helper.Helper.SendEmail(Email, "Clouderac", body);
                      
                        objModel.users = objUserManager.GetUsers(session.UserSession.PIN);
                        return View(objModel);
                       
                    }
                    else
                    {
                        return Json("1", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("2", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("TempData Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("2", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AjaxDeleteUser(string User_ID_PK)
        {
            UserModel objModel = new UserModel();
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                Response = objUserManager.DeleteUser(Convert.ToInt64(User_ID_PK));
                if (Response.ErrorCode == 0)
                {
                    objModel.users = objUserManager.GetUsers(session.UserSession.PIN);
                    return View(objModel);
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }
                
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("DeleteUser Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AjaxChangeUserStatus(string User_ID_PK)
        {
            UserModel objModel = new UserModel();
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                Response = objUserManager.ChangeUserStatus(Convert.ToInt64(User_ID_PK));
                if (Response.ErrorCode == 0)
                {
                    if (Response.ErrorMessage != "0")
                    {
                        objModel.users = objUserManager.GetUsers(session.UserSession.PIN);
                        return View(objModel);
                    }
                    else
                    {
                        return Json("0", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json("0", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxChangeUserStatus Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        

    }
}
