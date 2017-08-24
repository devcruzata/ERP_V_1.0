using Project.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Filters
{
    public class SessionTimeOutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext context = HttpContext.Current;
           // SessionHelper session = new SessionHelper();
            // check if session supported
            if (context.Session != null)
            {
                if (context.Session["username"] == null)
                {
                    context.Response.Redirect("~/Authentication/Login");
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}