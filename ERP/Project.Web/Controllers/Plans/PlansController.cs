using BAL.Subscription;
using Project.Entity;
using Project.Web.Filters;
using Project.Web.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Plans
{
    public class PlansController : Controller
    {
        //
        // GET: /Plans/
        SubscriptionManager objSubscriptionManager = new SubscriptionManager();
        [Authorize]
        public ActionResult Plans()            
        {
            PlanModel objPlanModel = new PlanModel();
            objPlanModel.plans = objSubscriptionManager.GetPlans();
            return View(objPlanModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeletePlans(string Plan_ID)
        {
            objResponse Response = new objResponse();

            try
                {
                    Response = objSubscriptionManager.DeletePlan(Convert.ToInt64(Plan_ID));

                    if (Response.ErrorCode == 0)
                    {
                        var planService = new StripePlanService();
                        planService.Delete(Plan_ID);

                        PlanModel objPlanModel = new PlanModel();
                        objPlanModel.plans = objSubscriptionManager.GetPlans();
                        return View("AjaxAddPlan", objPlanModel);
                    }
                    else
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }                 
                 
                }
              catch(Exception ex)
                   {
                      
                       BAL.Common.LogManager.LogError("DeletePlan Post method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.StackTrace), Convert.ToString(ex.Message));
                       return Json("", JsonRequestBehavior.AllowGet);
                   }
        }

        

        [Authorize]
        [SessionTimeOut]
        public ActionResult AjaxAddPlan(string PlanName, string PlanType, string PlanPrice,string Features)
        {
            objResponse Response = new objResponse();
            string planID = Guid.NewGuid().ToString();
            try
            {
                Response = objSubscriptionManager.AddPlans(PlanName, PlanPrice, PlanType, "usd",planID);

                if (Response.ErrorCode == 0)
                {
                    var myPlan = new StripePlanCreateOptions();
                    myPlan.Id = planID;
                    myPlan.Amount = Convert.ToInt32(PlanPrice)*100;           // all amounts on Stripe are in cents, pence, etc
                    myPlan.Currency = "usd";        // "usd" only supported right now
                    myPlan.Interval = PlanType;      // "month" or "year"
                    //myPlan.IntervalCount = 1;       // optional
                    myPlan.Name = PlanName;
                    myPlan.TrialPeriodDays = 0;    // amount of time that will lapse before the customer is billed

                    var planService = new StripePlanService();
                    StripePlan response = planService.Create(myPlan);
                    List<string> temp = Features.Split(',').ToList<string>();                    
                    foreach (var feature in temp)
                    {
                        Response = objSubscriptionManager.AddPlanFeature(Convert.ToInt32(Response.ErrorMessage), feature);

                        if (Response.ErrorCode != 0)
                        {
                            break;
                        }
                    }
                    PlanModel objPlanModel = new PlanModel();
                    objPlanModel.plans = objSubscriptionManager.GetPlans();
                    return View(objPlanModel);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
