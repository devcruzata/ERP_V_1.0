using BAL.Subscription;
using BAL.SubscriptionHolder;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.SubscriptionHolder
{
    public class SubscriptionHolderController : Controller
    {
        SubscriptionHolderManager objSubscriptionHolderManager = new SubscriptionHolderManager();
        //
        // GET: /SubscriptionHolder/
        [Authorize]
        public ActionResult SubscriptionHolderUser()
        {
            UserModel objUserModel = new UserModel();
            objUserModel.users = objSubscriptionHolderManager.GetSubscriptionHolders();
            return View(objUserModel);
            
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteSubscriptionHolder(string User_ID_PK)
        {
            string response = "";

            try
            {
                response = objSubscriptionHolderManager.DeleteSubscriptionHolder(Convert.ToInt64(User_ID_PK));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                BAL.Common.LogManager.LogError("Delete SubscriptionHolder", 1, Convert.ToString(ex.Message), Convert.ToString(ex.Source), Convert.ToString(ex.StackTrace));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            
        }

       

    }
}
