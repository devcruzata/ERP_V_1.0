using BAL.Notifications;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Notificatin
{
    public class NotificationController : Controller
    {
        NotificationManager objnfmanager = new NotificationManager();
        SessionHelper session;
        //
        // GET: /Notification/
        [Authorize]
        //[SessionTimeOut]
        [HttpPost]
        public async Task<ActionResult> GetLeadAssignToNotification()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                Response =  objnfmanager.GetNoOfLeadAssigned(Convert.ToDateTime(session.NotificationSession.lastNotificationViewedAt),Convert.ToInt64(session.UserSession.PIN),session.UserSession.UserId);
                if (Response.ErrorCode == 0)
                {
                    session.NotificationSession.totalNoOfNotification = session.NotificationSession.totalNoOfNotification + Convert.ToInt32(Response.ErrorMessage);
                    session.NotificationSession.lastLeadAssignToNf = DateTime.Now.ToString();
                    return Json(Response.ErrorMessage, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetLeadAssignToNotification Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }            
        }

        [Authorize]
        //[SessionTimeOut]
        [HttpPost]
        public async Task<ActionResult> GetTaskAssignToNotification()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                Response = objnfmanager.GetNoOfTaskAssigned(Convert.ToDateTime(session.NotificationSession.lastNotificationViewedAt), Convert.ToInt64(session.UserSession.PIN), session.UserSession.UserId);
                if (Response.ErrorCode == 0)
                {
                    session.NotificationSession.totalNoOfNotification = session.NotificationSession.totalNoOfNotification + Convert.ToInt32(Response.ErrorMessage);
                    session.NotificationSession.lastTaskAssignToNf = DateTime.Now.ToString();
                    return Json(Response.ErrorMessage, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetTaskAssignToNotification Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        //[SessionTimeOut]
        [HttpPost]
        public async Task<ActionResult> GetTotalNotification()
        {
            session = new SessionHelper();
            objResponse Response = new objResponse();
            
            try
            {
                if (session.NotificationSession == null)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string lastSync = session.NotificationSession.lastNotificationAt;
                    session.NotificationSession.lastNotificationAt = DateTime.Now.ToString();
                    //session.NotificationSession.lastNotificationAt = DateTime.Now.ToString();
                    Response = objnfmanager.GetNewNotification(Convert.ToDateTime(lastSync),Convert.ToInt64(session.UserSession.PIN),session.UserSession.UserId);
                    session.NotificationSession.totalNoOfNotification = session.NotificationSession.totalNoOfNotification + Convert.ToInt32(Response.ErrorMessage);
                    return Json(session.NotificationSession.totalNoOfNotification, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetTotalNotification Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        //[SessionTimeOut]
        [HttpPost]
        public async Task<ActionResult> GetNotificationData()
        {
            session = new SessionHelper();
            objResponse Response = new objResponse();
            NotificationModel objNoModel = new NotificationModel();
            try
            {
                if (session.NotificationSession == null)
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    session.NotificationSession.lastNotificationViewedAt = DateTime.Now.ToString();
                    
                    string lastViewed = session.NotificationSession.lastNotificationViewedAt;
                    Response = objnfmanager.GetNotificationData(Convert.ToDateTime(lastViewed), Convert.ToInt64(session.UserSession.PIN), session.UserSession.UserId);
                    if (Response.ErrorCode == 0)
                    {
                        if (Response.ResponseData.Tables[0].Rows[0][0].ToString() != "0")
                        {
                            objNoModel.totalLeadAssigned = Response.ResponseData.Tables[0].Rows[0][0].ToString();
                        }

                        if (Response.ResponseData.Tables[0].Rows[0][0].ToString() != "0")
                        {
                            objNoModel.totalTaskAssigned = Response.ResponseData.Tables[1].Rows[0][0].ToString();
                        }
                        
                        session.NotificationSession.totalNoOfNotification = 0;
                        return Json(objNoModel, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }                    
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetNotificationData Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        //[SessionTimeOut]
        [HttpPost]
        public async Task<ActionResult> ResetTotalNotification()
        {
            session = new SessionHelper();
            try
            {
                session.NotificationSession.totalNoOfNotification = 0;
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("ResetTotalNotification Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

    }
}
