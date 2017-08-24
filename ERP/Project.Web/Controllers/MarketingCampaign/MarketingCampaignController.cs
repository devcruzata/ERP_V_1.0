using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.MarketingCampaign
{
    public class MarketingCampaignController : Controller
    {
        //
        // GET: /MarketingCampaign/
        [Authorize]
        public ActionResult EmailMarketingHome()
        {
            return View();
        }

        [Authorize]
        public ActionResult AddEmailCampaign()
        {
            return View();
        }

        [Authorize]
        public ActionResult TemplateEditor()
        {
            return View();
        }

    }
}
