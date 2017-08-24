using Project.Entity;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.PaymentTracker
{
    public class PaymentTrackerController : Controller
    {
        BAL.PaymentTracker.TrackingManager objTrackingManager = new BAL.PaymentTracker.TrackingManager();
        Project.Web.Common.SessionHelper session;

        //
        // GET: /PaymentTracker/
        [Authorize]
        public ActionResult TrackerHome()
        {
            PaymentTrackingModel objModel = new PaymentTrackingModel();
            objModel.tracker = objTrackingManager.GetPaymentRecords();
            objModel.project_data = objTrackingManager.GetProjectData(); ;
            return View(objModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddProjectForTracking(string Project_ID)
        {
            objResponse Response = new objResponse();
            PaymentTrackingModel objModel = new PaymentTrackingModel();
            session = new Common.SessionHelper();
            try
            {
                
                string[] Project_ID_PK = Project_ID.Split(',');                

                for (int i = 1; i < Project_ID_PK.Length; i++)
                {
                    Response = objTrackingManager.AddProjectForTracking(Convert.ToInt64(Project_ID_PK[i]), session.UserSession.Username);

                    if (Response.ErrorCode != 0)
                    {                        
                        break;                        
                    }
                }
               // objModel.tracker = objTrackingManager.GetPaymentRecords();
                //return View("TempData", objModel);   
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AddProjectForTracking Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                //return View("TempData", objModel);
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        public ActionResult ViewPaymentTrackingRecord(string Tracking_ID)
        {
            objResponse Response = new objResponse();
            PaymentTrackingModel objModel = new PaymentTrackingModel();
            List<PaymentData> objTrackerList = new List<PaymentData>();
            try
            {
                Response = objTrackingManager.GetPaymentTrackingDetails(Convert.ToInt64(Tracking_ID));

                if (Response.ErrorCode == 0)
                {
                    objModel.Trackin_ID_PK = Convert.ToInt64(Response.ResponseData.Tables[0].Rows[0]["Tracking_ID_PK"]);
                    objModel.Project_Title = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Project_Title"]);
                    objModel.ClientName = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["Name"]);
                    objModel.TotalCost = Convert.ToDecimal(Response.ResponseData.Tables[0].Rows[0]["TotalCost"]);
                    objModel.AmountPaid = Convert.ToDecimal(Response.ResponseData.Tables[0].Rows[0]["AmountPaid"]);

                    foreach (DataRow dr in Response.ResponseData.Tables[1].Rows)
                    {
                        Project.Entity.PaymentData objPayment = new PaymentData();
                        objPayment.TrackRecord_ID = Convert.ToInt64(dr["Track_Record_ID_PK"]);
                        objPayment.AmountPaid = Convert.ToDecimal(dr["Amount_Paid"]);
                       // objPayment.AmountRemaining = Convert.ToDecimal(dr["Amount_Remaining"]);
                        objPayment.ConvRate = Convert.ToDecimal(dr["Conv_Rate"]);
                        objPayment.Date = Convert.ToDateTime(dr["Date"]).ToString("d MMM yyyy");                        

                        objTrackerList.Add(objPayment);
                    }
                    objModel.payment_data = objTrackerList;
                    return View(objModel);
                    
                }
                else
                {
                    return View(objModel);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("ViewPaymentTrackingRecord", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objModel);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult TempData(PaymentTrackingModel objPaymentModel)
        {
            objResponse Response = new objResponse();
            PaymentTrackingModel objModel = new PaymentTrackingModel();
            List<PaymentData> objTrackerList = new List<PaymentData>();
            session = new Common.SessionHelper();
            try
            {
                for(int i=0;i<objPaymentModel.AmntPaid.Count;i++)
                {
                    decimal convRate = (Convert.ToDecimal(objPaymentModel.AmntPaid[i]) / Convert.ToDecimal(objPaymentModel.AmntPaidInInr[i]));

                    Response = objTrackingManager.AddPayment(objPaymentModel.Trackin_ID_PK, Convert.ToDecimal(objPaymentModel.AmntPaid[i]), convRate, BAL.Helper.Helper.ConvertToDateNullable(objPaymentModel.Dat[i],"dd/MM/yyyy"), session.UserSession.Username);

                    if (Response.ErrorCode == 0)
                    {
                        break;
                    }

                }
                objModel.tracker = objTrackingManager.GetPaymentRecords();
                return View(objModel);
                
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("TempData Contro", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objModel);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult RemoveProject(string Tracking_ID_PK)
        {
            string response = "";
            try
            {
                response = objTrackingManager.RemoveProjectFromTracking(Convert.ToInt64(Tracking_ID_PK));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("RemoveProject", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
