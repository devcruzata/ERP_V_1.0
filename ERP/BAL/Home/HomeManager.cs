using DAL;
using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Home
{
   public class HomeManager
    {
       public List<TextValue> GetMapData()
       {
           objResponse Response = new objResponse();
           List<TextValue> mapData = new List<TextValue>();
           try
           {
               //SqlParameter[] sqlParameter = new SqlParameter[1];

               //sqlParameter[0] = new SqlParameter("@Project_ID", SqlDbType.BigInt, 10);
               //sqlParameter[0].Value = Project_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetMapData", DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = dr["NoOFClient"].ToString();
                       objText.Text = dr["Country"].ToString();
                       mapData.Add(objText);
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
               BAL.Common.LogManager.LogError("GetMapData", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return mapData;
       }

       public List<TextValue> GetOpportunityGraphData()
       {
           objResponse Response = new objResponse();
           List<TextValue> OppoGraphData = new List<TextValue>();
           try
           {

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "GetOpportunityGraphData", DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   //foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   //{
                   //    TextValue objText = new TextValue();
                   //    objText.Value = dr["NoOfProjects"].ToString();
                   //    objText.Text = dr["mont"].ToString();
                   //    ProjGraphData.Add(objText);
                   //}

                   for (int i = 0; i < Convert.ToInt32(Response.ResponseData.Tables[1].Rows[0][0]);i++ )
                   {
                       TextValue objText = new TextValue();
                       if (Response.ResponseData.Tables[0].Rows[0][i].ToString() == "")
                       {
                           objText.Value = "0";
                       }
                       else
                       {
                           objText.Value = Response.ResponseData.Tables[0].Rows[0][i].ToString();
                       }                       
                       objText.Text = i.ToString();
                       OppoGraphData.Add(objText);
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
               BAL.Common.LogManager.LogError("GetOpportunityGraphData", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return OppoGraphData;
       }

       public List<TextValue> GetOpportunityRevenueGraphData()
       {
           objResponse Response = new objResponse();
           List<TextValue> OppoRevemueGraphData = new List<TextValue>();
           try
           {

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "GetOpportunityRevenueGraphData", DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;                   

                   for (int i = 0; i < Convert.ToInt32(Response.ResponseData.Tables[1].Rows[0][0]); i++)
                   {
                       TextValue objText = new TextValue();
                       if (Response.ResponseData.Tables[0].Rows[0][i].ToString() == "")
                       {
                           objText.Value = "0";
                       }
                       else
                       {
                           objText.Value = Response.ResponseData.Tables[0].Rows[0][i].ToString();
                       }
                       objText.Text = i.ToString();
                       OppoRevemueGraphData.Add(objText);
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
               BAL.Common.LogManager.LogError("GetOpportunityRevenueGraphData", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return OppoRevemueGraphData;
       }

       public List<TextValue> GetOpportunityLostByMonth(long PIN)
       {
           objResponse Response = new objResponse();
           List<TextValue> OppoData = new List<TextValue>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "GetLostOpportunityDataByMonth", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;

                   for (int i = 0; i < Convert.ToInt32(Response.ResponseData.Tables[1].Rows[0][0]); i++)
                   {
                       TextValue objText = new TextValue();
                       if (Response.ResponseData.Tables[0].Rows[0][i].ToString() == "")
                       {
                           objText.Value = "0";
                       }
                       else
                       {
                           objText.Value = Response.ResponseData.Tables[0].Rows[0][i].ToString();
                       }
                       objText.Text = i.ToString();
                       OppoData.Add(objText);
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
               BAL.Common.LogManager.LogError("GetOpportunityLostByMonth", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return OppoData;
       }

       public List<TextValue> GetOpportunityWonByMonth(long PIN)
       {
           objResponse Response = new objResponse();
           List<TextValue> OppoData = new List<TextValue>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "GetWonOpportunityDataByMonth", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;

                   for (int i = 0; i < Convert.ToInt32(Response.ResponseData.Tables[1].Rows[0][0]); i++)
                   {
                       TextValue objText = new TextValue();
                       if (Response.ResponseData.Tables[0].Rows[0][i].ToString() == "")
                       {
                           objText.Value = "0";
                       }
                       else
                       {
                           objText.Value = Response.ResponseData.Tables[0].Rows[0][i].ToString();
                       }
                       objText.Text = i.ToString();
                       OppoData.Add(objText);
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
               BAL.Common.LogManager.LogError("GetOpportunityWonByMonth", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return OppoData;
       }

       public objResponse GetTopThreeSources(long PIN)
       {
           objResponse Response = new objResponse();
           
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "GetTopThreeSources", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0 && Response.ResponseData.Tables[1].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = "Succes";
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
               BAL.Common.LogManager.LogError("GetTopThreeSources", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
           
       }

       public objResponse GetAdminDashboardData(long PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetAdminDashboardData", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0 )               
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = "Success";                  
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
               BAL.Common.LogManager.LogError("GetAdminDashboardData", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }
    }
}
