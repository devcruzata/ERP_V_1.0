using BAL.Subscription;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.Subscription
{
    public class SubscriptionController : Controller
    {
        SubscriptionManager objSubManager = new SubscriptionManager();
        SessionHelper session;
        //
        // GET: /Subscription/

        [Authorize]
        [SessionTimeOut]
        public ActionResult SubscriptionHome()
        {
            //session = new SessionHelper();
            //SubscriptionModel objModel = new SubscriptionModel();
            //objModel.subscription = objSubManager.GetSubscriptions();
            //int i=0;
            //foreach (var sub in objModel.subscription)
            //{
            //    if (sub.Name != "Trial Pack")
            //    {
            //        i++;
            //        TempData["PlanName" + i] = sub.Name;
            //        TempData["Plan_" + i + "_ID"] = sub.Subscription_ID;
            //        TempData["PlanPrice" + i] = sub.Price;
            //        TempData["PlanAnnualDisc" + i] = sub.AnnualDiscount;
            //    }                
            //}
            //ViewBag.CustomerID = session.UserSession.PIN;
            //ViewBag.SubscriptionID = session.UserSession.Subscription_ID;
            return View();
        }
        

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult ManageSubscription(string sToken ,string sCustomerID ,string PlanID)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            
            try
            {
                if (sCustomerID == "")
                {
                    var myCustomer = new StripeCustomerCreateOptions();

                    // set these properties if it makes you happy
                    myCustomer.Email = session.UserSession.Email;
                    myCustomer.Description = session.UserSession.FullName;
                    myCustomer.SourceToken = sToken;


                    var customerService = new StripeCustomerService();
                    StripeCustomer stripeCustomer = customerService.Create(myCustomer);
                    var StripeCustomerId = stripeCustomer.Id;

                    var subscriptionService = new StripeSubscriptionService();
                    StripeSubscription stripeSubscription = subscriptionService.Create(StripeCustomerId, PlanID);

                    Response = objSubManager.SubscribeUser(StripeCustomerId.ToString(),stripeSubscription.Id,PlanID,Convert.ToInt64(session.UserSession.PIN));

                    if (Response.ErrorCode == 0)
                    {
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var subscriptionService = new StripeSubscriptionService();                    
                    var stripeUpdateOption = new StripeSubscriptionUpdateOptions(){
                        PlanId = PlanID                        
                    };
                    StripeSubscription stripeSubscription = subscriptionService.Update(session.UserSubscription.sSubscriptionID, stripeUpdateOption);

                    Response = objSubManager.UpdateUserSubscription(PlanID,Convert.ToInt64(session.UserSession.PIN));

                    if (Response.ErrorCode == 0)
                    {
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("", JsonRequestBehavior.AllowGet);
                    }
                }

                
                //stripeSubscription.i
              //  stripeSubscription.Status
                
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("SubscribeUser Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
