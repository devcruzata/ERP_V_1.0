using DAL;
using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Common
{
   public static class UtilityManager
    {
      

       public static List<TextValue>  GetSourceForDropDown(long PIN , string Module , string DropDown)
       {

           objResponse Response = new objResponse();
           List<TextValue> Category = new List<TextValue>();

           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetSourceForDropDown", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = dr["Source_Val"].ToString();
                       objText.Text = dr["Source_Text"].ToString();
                       Category.Add(objText);
                   }
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetSourceForDropDown", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Category;
       }       

       public static List<TextValue> GetLeadsForDropDown()
       {

           objResponse Response = new objResponse();
           List<TextValue> User = new List<TextValue>();

           try
           {

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetLeadsForDropDown", DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = dr["Lead_ID_Auto_PK"].ToString();
                       objText.Text = dr["Name"].ToString();
                       User.Add(objText);
                   }
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetLeadsForDropDown", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return User;
       }

       public static List<TextValue> GetClientsForDropDown()
       {

           objResponse Response = new objResponse();
           List<TextValue> User = new List<TextValue>();

           try
           {

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetClientsForDropDown", DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                      
                       TextValue objText = new TextValue();

                       objText.Value = dr["Client_ID_Auto_PK"].ToString();
                       objText.Text = dr["Name"].ToString();
                       User.Add(objText);
                   }
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetClientsForDropDown", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return User;
       }

       public static List<Project.Entity.Activity> getActivityByRelateToID(long PIN,long RelateToID,string RelatedTable)
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Activity> activity = new List<Project.Entity.Activity>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               sqlParameter[1] = new SqlParameter("@RelateToID", SqlDbType.BigInt, 0);
               sqlParameter[1].Value = RelateToID;

               sqlParameter[2] = new SqlParameter("@RelatedTable", SqlDbType.NVarChar, 30);
               sqlParameter[2].Value = RelatedTable;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetActivities", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.Entity.Activity objActivity = new Project.Entity.Activity();
                       objActivity.Activity_ID = Convert.ToInt64(dr["Activity_ID_Auto_PK"]);
                       objActivity.Title = Convert.ToString(dr["Title"]);
                       objActivity.RelateTo_ID = Convert.ToInt64(dr["RelateTo_ID_Fk"]);
                       objActivity.RelateTo_Name = Convert.ToString(dr["Name"]);
                       objActivity.CreatedBy = Convert.ToString(dr["CreatedBy"]);
                       objActivity.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]).ToString("d MMM yyyy");
                       objActivity.CreatedTime = Convert.ToDateTime(dr["CreatedDate"]).ToString("hh:mm tt");
                       objActivity.CreatedByName = Convert.ToString(dr["CreatedByName"]);
                       objActivity.Status = Convert.ToString(dr["Status"]);
                       objActivity.ActivityType = Convert.ToString(dr["ActivityType"]);
                       objActivity.FromAdd = Convert.ToString(dr["FromAddress"]);
                       objActivity.ToAdd = Convert.ToString(dr["ToAddress"]);
                      

                       activity.Add(objActivity);
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
               BAL.Common.LogManager.LogError("getActivityByRelateToID", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return activity;
       }

       public static List<TextValue> getRelatedUserListing(long PIN, long logedUser, string RelatedTable)
       {
           objResponse Response = new objResponse();
           List<TextValue> rlist = new List<TextValue>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               sqlParameter[1] = new SqlParameter("@RelateToID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = logedUser;

               sqlParameter[2] = new SqlParameter("@RelatedTable", SqlDbType.NVarChar, 30);
               sqlParameter[2].Value = RelatedTable;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetRelateTOListing", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objTV = new TextValue();
                       objTV.Value = Convert.ToString(dr["RelateToID"]);
                       objTV.Text = Convert.ToString(dr["RelateToName"]);
                       rlist.Add(objTV);
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
               BAL.Common.LogManager.LogError("getRelatedUserListing", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return rlist;
       }
    }
}
