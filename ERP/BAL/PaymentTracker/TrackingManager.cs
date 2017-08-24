using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.PaymentTracker
{
   public class TrackingManager
    {
       public objResponse AddProjectForTracking(long Project_ID,  string LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[4];               

               sqlParameter[0] = new SqlParameter("@Project_ID_Fk", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Project_ID;

               sqlParameter[1] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[1].Value = LogedUser;

               sqlParameter[2] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[2].Value = DateTime.Now;

               sqlParameter[3] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[3].Value = "Active";

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddProjectForTracking", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
               }
               else
               {
                   Response.ErrorCode = 2001;
                   Response.ErrorMessage = "There is an Error. Please Try After some time.";
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 3001;
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("AddProjectForTracking", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public List<Project.Entity.Tracker> GetPaymentRecords()
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Tracker> paymentTracker = new List<Tracker>();
           try
           {
               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetPaymentRecords",  DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Tracker objTracker = new Tracker();
                       objTracker.Trackin_ID_PK = Convert.ToInt64(dr["Tracking_ID_PK"]);
                       objTracker.Project_Title = Convert.ToString(dr["Project_Title"]);
                       objTracker.Client_ID_Fk = Convert.ToInt64(dr["Client_ID_Fk"]);
                       objTracker.ClientName = Convert.ToString(dr["Name"]);
                       objTracker.TotalCost = Convert.ToDecimal(dr["TotalCost"]);
                       objTracker.AmountPaid = Convert.ToDecimal(dr["AmountPaid"]);
                       objTracker.AmountRemainig = Convert.ToDecimal(dr["AmountRemaining"]);
                       objTracker.Status = Convert.ToString(dr["Status"]);

                       paymentTracker.Add(objTracker);
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
               Response.ErrorCode = 3001;
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("GetPaymentRecords", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return paymentTracker;
       }

       public List<Project.Entity.ProjectData> GetProjectData()
       {
           objResponse Response = new objResponse();
           List<Project.Entity.ProjectData> projectData = new List<ProjectData>();
           try
           {
               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetProjectData", DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       ProjectData objProjectData = new ProjectData();
                       objProjectData.Project_ID_Fk = Convert.ToInt64(dr["Project_ID_Auto_PK"]);
                       objProjectData.Title = Convert.ToString(dr["Title"]);
                       objProjectData.Client_ID = Convert.ToInt64(dr["Client_ID_Fk"]);
                       objProjectData.Client_Name = Convert.ToString(dr["Name"]);
                       objProjectData.Total_Cost = Convert.ToDecimal(dr["TotalCost"]);

                       projectData.Add(objProjectData);
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
               Response.ErrorCode = 3001;
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("GetProjectData", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return projectData;
       }

       public objResponse GetPaymentTrackingDetails(long TrackinID)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@TrackinID", SqlDbType.BigInt, 20);
               sqlParameter[0].Value = TrackinID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ViewPaymentTrackingRecords", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = "success";
               }
               else
               {
                   Response.ErrorCode = 2001;
                   Response.ErrorMessage = "There is an Error. Please Try After some time.";
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 3001;
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("GetPaymentTrackingDetails", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse AddPayment(long TrackingID, decimal AmountPaid, decimal ConvRate, DateTime Dat, string LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[7];

               sqlParameter[0] = new SqlParameter("@TrackingID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = TrackingID;

               sqlParameter[1] = new SqlParameter("@AmountPaid", SqlDbType.Decimal, 20);
               sqlParameter[1].Value = AmountPaid;               

               sqlParameter[2] = new SqlParameter("@ConvRate", SqlDbType.Decimal, 20);
               sqlParameter[2].Value = ConvRate;

               sqlParameter[3] = new SqlParameter("@Date", SqlDbType.Date, 50);
               sqlParameter[3].Value = Dat;

               sqlParameter[4] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[4].Value = LogedUser;

               sqlParameter[5] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[5].Value = DateTime.Now;

               sqlParameter[6] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[6].Value = "Active";
               

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddPayment", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
               }
               else
               {
                   Response.ErrorCode = 2001;
                   Response.ErrorMessage = "There is an Error. Please Try After some time.";
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 3001;
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("AddPayment", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public string RemoveProjectFromTracking(long TrackinID)
       {
           objResponse Response = new objResponse();
           string Result = "";
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@TrackinID", SqlDbType.BigInt, 20);
               sqlParameter[0].Value = TrackinID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_DeleteTracker", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Result = Response.ResponseData.Tables[0].Rows[0][0].ToString(); 
               }
               else
               {
                   Response.ErrorCode = 2001;
                   Response.ErrorMessage = "There is an Error. Please Try After some time.";
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 3001;
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("RemoveProjectFromTracking", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Result;
       }
    }
}
