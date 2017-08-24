using BAL.Chat;
using BAL.User;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Chat
{
    public class ChatController : Controller
    {
        ChatManager objChatManager = new ChatManager();
        SessionHelper session;
        //
        // GET: /Chat/

        [Authorize]
        [HttpPost]
        public ActionResult GetUserForChat()
        {
            UserModel objUser = new UserModel();            
            session = new SessionHelper();
            objUser.users = objChatManager.GetUsersForChat(session.UserSession.PIN, session.UserSession.UserId);
            return View(objUser);
        }


        [Authorize]        
        [HttpPost]
        public ActionResult RenderChat(string SenderID, string RecieverID)
        {
            session = new SessionHelper();
            ChatModel objChatModel = new ChatModel();
            objChatModel.chats = objChatManager.GetUsersChat(SenderID, RecieverID, session.UserSession.PIN);
            return View(objChatModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SendMsg(string SenderID, string RecieverID, string Msg)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                Response = objChatManager.AddUserchat(SenderID,RecieverID,Msg,session.UserSession.PIN,session.UserSession.UserId);
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
                BAL.Common.LogManager.LogError("SendMsg Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }
           
        }

    }
}
