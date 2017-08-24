using BAL.Common;
using BAL.User;
using Project.Entity;
using Project.ViewModel;
using Project.Web.Common;
using Project.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers
{
    public class CommonController : Controller
    {
        SessionHelper session;
        //
        // GET: /Common/
        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult GetRelateToListing(string RelatedTable)
        {           
            session = new SessionHelper();
            List<SelectListItem> rlist = new List<SelectListItem>();
            List<TextValue> rlisting = new List<TextValue>();
            try
            {
                rlisting = UtilityManager.getRelatedUserListing(Convert.ToInt64(session.UserSession.PIN), session.UserSession.UserId, RelatedTable);
                if (RelatedTable == "LEAD")
                {
                    rlist.Add(new SelectListItem { Value = "0", Text = "Choose A Lead" });
                }
                else if (RelatedTable == "OPPORTUNITY")
                {
                    rlist.Add(new SelectListItem { Value = "0", Text = "Choose A Opportunity" });
                }
                else
                {
                    rlist.Add(new SelectListItem { Value = "0", Text = "Choose A Client" });
                }

                foreach (var user in rlisting)
                {
                    rlist.Add(new SelectListItem { Value = user.Value, Text = user.Text });
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetRelateToListing Common Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));            
            }
             return Json(rlist, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult GetUserListing()
        {
            UserManager objUserManager = new UserManager();
            session = new SessionHelper();
            List<Users> UserList = new List<Users>();
            List<SelectListItem> ulist = new List<SelectListItem>();
            try
            {
                                
                UserList = objUserManager.GetUsers(session.UserSession.PIN);

                ulist.Add(new SelectListItem { Value = "0", Text = "Choose A User" });

                foreach (var user in UserList)
                {
                    ulist.Add(new SelectListItem { Value = user.User_ID_PK.ToString(), Text = user.FirstName +" "+user.LastName });
                }

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetUserListing Common Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Json(ulist, JsonRequestBehavior.AllowGet);
        }

    }
}
