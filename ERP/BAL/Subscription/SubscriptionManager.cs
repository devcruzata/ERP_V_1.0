using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Subscription
{
   public class SubscriptionManager
    {
       //public List<Project.ViewModel.PlansViewModel> GetPlansForDisplay()
       //{
       //    objResponse Response = new objResponse();
       //    List<Project.Entity.Subscription> subscription = new List<Project.Entity.Subscription>();
       //    try
       //    {

       //        DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetPlansForDisplay", DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


       //        if (Response.ResponseData.Tables[0].Rows.Count > 0)
       //        {
       //            Response.ErrorCode = 0;
       //            foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
       //            {
       //                Project.ViewModel.PlansViewModel objPlanVm = new Project.ViewModel.PlansViewModel(); ;
       //                objPlanVm.planId = Convert.ToInt32(dr["[Subscription_ID_PK"]);
       //                objPlanVm.planName = dr["Name"].ToString();
       //                objPlanVm.planPrice = dr["Price"].ToString();
       //                objPlanVm.NoOfUsers = Convert.ToInt32(dr["NoOFUser"]);
       //                objPlanVm.Status = dr["Status"].ToString();
       //                objPlanVm.AnnualDiscount = Convert.ToInt32(dr["AnnualDiscount"]);

       //                subscription.Add(objSubscription);
       //            }
       //        }
       //        else
       //        {
       //            Response.ErrorCode = 2001;
       //            Response.ErrorMessage = "There is an Error. Please Try After some time.";
       //        }
       //    }
       //    catch (Exception ex)
       //    {
       //        Response.ErrorMessage = ex.Message.ToString();
       //        BAL.Common.LogManager.LogError("GetSubscriptions", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
       //    }
       //    return subscription;
       //}

       public List<Project.Entity.Plans> GetPlans()
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Plans> plan = new List<Project.Entity.Plans>();
           try
           {

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetPlans", DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.Entity.Plans objPlan = new Project.Entity.Plans();
                       objPlan.Plan_ID = Convert.ToInt32(dr["Plan_ID_PK"]);
                       objPlan.Name = dr["Name"].ToString();
                       objPlan.Price = dr["Price"].ToString();
                       objPlan.Type = Convert.ToString(dr["PlanType"]);
                       objPlan.Currency = Convert.ToString(dr["CurrencyType"]);
                       objPlan.Status = dr["Status"].ToString();
                     

                       plan.Add(objPlan);
                   }
               }
               else
               {
                   Response.ErrorCode = 2001;
                   Response.ErrorMessage = "There is an Error. Please Try After some time.";
               }
           }
           catch (Exception ex)
           {
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("GetPlans", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return plan;
       }

       public objResponse DeletePlan(Int64 Plan_ID_PK)
       {
           objResponse response = new objResponse();         

           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Plan_ID_PK", SqlDbType.BigInt, 0);
               sqlParameter[0].Value = Plan_ID_PK;

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeletePlan", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

            

               if (response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   response.ErrorCode = 0;
                   response.ErrorMessage = Convert.ToString(response.ResponseData.Tables[0].Rows[0][0]);                
               }
           }

           catch (Exception ex)
           {
               response.ErrorCode = 2001;
               response.ErrorMessage = "Error While Processing" + ex.Message;
               BAL.Common.LogManager.LogError("Delete Subscription", 1, Convert.ToString(ex.Message), Convert.ToString(ex.Source), Convert.ToString(ex.StackTrace));

           }
           return response;
       }

       public objResponse AddPlans(string Name, string Price, string PlanType,string Currency,string planID)
       {
           objResponse response = new objResponse();

           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[5];

               sqlParameter[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 50);
               sqlParameter[0].Value = Name;

               sqlParameter[1] = new SqlParameter("@Price", SqlDbType.NVarChar, 20);
               sqlParameter[1].Value = Price;

               sqlParameter[2] = new SqlParameter("@PlanType", SqlDbType.NVarChar, 30);
               sqlParameter[2].Value = PlanType;

               sqlParameter[3] = new SqlParameter("@Currency", SqlDbType.NVarChar, 30);
               sqlParameter[3].Value = Currency;

               sqlParameter[4] = new SqlParameter("@planID", SqlDbType.NVarChar, 50);
               sqlParameter[4].Value = planID;
               

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_AddPlan", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   response.ErrorCode = 0;
                   response.ErrorMessage = response.ResponseData.Tables[0].Rows[0][0].ToString();

               }
               else
               {
                   response.ErrorCode = 2001;
                   response.ErrorMessage = "There is an error, please try after some time";

               }
           }
           catch (Exception ex)
           {
               response.ErrorCode = 3001;
               response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("Add Plans", 1, Convert.ToString(ex.Message), Convert.ToString(ex.Source), Convert.ToString(ex.StackTrace));
           }
           return response;
       }

       public objResponse AddPlanFeature(Int32 PlanId, string Feature)
       {
           objResponse response = new objResponse();

           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@PlanId", SqlDbType.BigInt, 20);
               sqlParameter[0].Value = PlanId;

               sqlParameter[1] = new SqlParameter("@Feature", SqlDbType.NVarChar, 200);
               sqlParameter[1].Value = Feature;


               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_AddPlanFeature", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   response.ErrorCode = 0;
                   response.ErrorMessage = response.ResponseData.Tables[0].Rows[0][0].ToString();

               }
               else
               {
                   response.ErrorCode = 2001;
                   response.ErrorMessage = "There is an error, please try after some time";

               }
           }
           catch (Exception ex)
           {
               response.ErrorCode = 3001;
               response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("AddPlanFeature", 1, Convert.ToString(ex.Message), Convert.ToString(ex.Source), Convert.ToString(ex.StackTrace));
           }
           return response;
       }

       public objResponse SubscribeUser(string StrieCustomerID,string StripeSubsID,string PlanID,long PIN)
       {
           objResponse response = new objResponse();

           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[4];

               sqlParameter[0] = new SqlParameter("@StrieCustomerID", SqlDbType.NVarChar, 50);
               sqlParameter[0].Value = StrieCustomerID;

               sqlParameter[1] = new SqlParameter("@StripeSubsID", SqlDbType.NVarChar, 50);
               sqlParameter[1].Value = StripeSubsID;

               sqlParameter[2] = new SqlParameter("@PlanID", SqlDbType.NVarChar, 50);
               sqlParameter[2].Value = PlanID;              

               sqlParameter[3] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[3].Value = PIN;

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_SubscribeUser", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   response.ErrorCode = 0;
                   response.ErrorMessage = response.ResponseData.Tables[0].Rows[0][0].ToString();

               }
               else
               {
                   response.ErrorCode = 2001;
                   response.ErrorMessage = "There is an error, please try after some time";

               }
           }
           catch (Exception ex)
           {
               response.ErrorCode = 3001;
               response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("SubscribeUser", 1, Convert.ToString(ex.Message), Convert.ToString(ex.Source), Convert.ToString(ex.StackTrace));
           }
           return response;
       }

       public objResponse UpdateUserSubscription(string PlanID, long PIN)
       {
           objResponse response = new objResponse();

           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];               

               sqlParameter[0] = new SqlParameter("@PlanID", SqlDbType.NVarChar, 50);
               sqlParameter[0].Value = PlanID;

               sqlParameter[1] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = PIN;

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_UpdateUserSubscription", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   response.ErrorCode = 0;
                   response.ErrorMessage = response.ResponseData.Tables[0].Rows[0][0].ToString();

               }
               else
               {
                   response.ErrorCode = 2001;
                   response.ErrorMessage = "There is an error, please try after some time";

               }
           }
           catch (Exception ex)
           {
               response.ErrorCode = 3001;
               response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("UpdateUserSubscription", 1, Convert.ToString(ex.Message), Convert.ToString(ex.Source), Convert.ToString(ex.StackTrace));
           }
           return response;
       }
    }
}
