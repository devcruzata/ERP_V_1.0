using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Estimate
{
   public class EstimateManager
    {
       public objResponse AddEstimate(Project.Entity.Estimate objEstimate, string LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[11];

               sqlParameter[0] = new SqlParameter("@Date", SqlDbType.Date, 50);
               sqlParameter[0].Value = objEstimate.Date;              

               sqlParameter[1] = new SqlParameter("@Category", SqlDbType.BigInt, 80);
               sqlParameter[1].Value = objEstimate.Category;

               sqlParameter[2] = new SqlParameter("@Language", SqlDbType.Int, 50);
               sqlParameter[2].Value = objEstimate.Language;

               sqlParameter[3] = new SqlParameter("@AssignTo", SqlDbType.BigInt, 13);
               sqlParameter[3].Value = objEstimate.AssignTo;               

               sqlParameter[4] = new SqlParameter("@Requirment", SqlDbType.NVarChar, 4000);
               sqlParameter[4].Value = objEstimate.Requirment;

               sqlParameter[5] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[5].Value = LogedUser;

               sqlParameter[6] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[6].Value = DateTime.Now;

               sqlParameter[7] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[7].Value = "In-Process";

               sqlParameter[8] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 20);
               sqlParameter[8].Value = objEstimate.Lead_ID_Fk;

               sqlParameter[9] = new SqlParameter("@Client_ID", SqlDbType.BigInt, 20);
               sqlParameter[9].Value = objEstimate.Client_ID_Fk;

               sqlParameter[10] = new SqlParameter("@Priority", SqlDbType.NVarChar, 20);
               sqlParameter[10].Value = objEstimate.Priority;



               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddEstimate", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("AddEstimate", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse AddEstimationUpload(long Estimation_ID, string FileName, string LogedUser, long Comment_ID_FK)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[6];

               sqlParameter[0] = new SqlParameter("@Estimation_ID", SqlDbType.BigInt, 50);
               sqlParameter[0].Value = Estimation_ID;

               sqlParameter[1] = new SqlParameter("@FileName", SqlDbType.NVarChar, 200);
               sqlParameter[1].Value = FileName;

               sqlParameter[2] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[2].Value = LogedUser;

               sqlParameter[3] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[3].Value = DateTime.Now;

               sqlParameter[4] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[4].Value = "Active";

               sqlParameter[5] = new SqlParameter("@Comment_ID_FK", SqlDbType.BigInt, 60);
               sqlParameter[5].Value = Comment_ID_FK;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddEstimationUploads", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("AddEstimationUpload", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse GetEstimationUpload(long Estimation_ID, long Comment_ID_FK, DateTime UploadedDate)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@Estimation_ID", SqlDbType.BigInt, 50);
               sqlParameter[0].Value = Estimation_ID;

               sqlParameter[1] = new SqlParameter("@Comment_ID_FK", SqlDbType.BigInt, 50);
               sqlParameter[1].Value = Comment_ID_FK;

               sqlParameter[2] = new SqlParameter("@UploadedDate", SqlDbType.DateTime, 50);
               sqlParameter[2].Value = UploadedDate;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetEstimationUpload", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("GetEstimationUpload", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public List<Project.Entity.Estimate> getEstimate()
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Estimate> Estimate = new List<Project.Entity.Estimate>();
           try
           {
               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetEstimate", DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.Entity.Estimate objEstimate = new Project.Entity.Estimate();
                       objEstimate.Estimate_ID_Auto_PK = Convert.ToInt64(dr["Estimation_ID_Auto_PK"]);
                       objEstimate.Lead = Convert.ToString(dr["Lead"]);
                       objEstimate.Client = Convert.ToString(dr["Client"]);
                       objEstimate.Priority = Convert.ToString(dr["Priority"]);
                       objEstimate.Date = Convert.ToDateTime(dr["Date"]);
                       objEstimate.CategoryName = Convert.ToString(dr["CategoryName"]);
                       objEstimate.LanguageName = Convert.ToString(dr["Language"]);
                       objEstimate.Assigne = Convert.ToString(dr["FullName"]);
                       objEstimate.Status = Convert.ToString(dr["Status"]);


                       Estimate.Add(objEstimate);
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
               BAL.Common.LogManager.LogError("getEstimate", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Estimate;
       }

       public string DeleteEstimate(Int64 Estimate_id_pk)
       {
           objResponse response = new objResponse();
           DataTable dt = new DataTable();
           string result = "";
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Estimate_id_pk", SqlDbType.BigInt, 0);
               sqlParameter[0].Value = Estimate_id_pk;

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteEstimate", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               dt = response.ResponseData.Tables[0];
               if (dt.Rows.Count > 0)
               {
                   response.ErrorCode = 0;
                   response.ErrorMessage = "Success";
                   result = Convert.ToString(dt.Rows[0][0]);
               }

           }
           catch (Exception ex)
           {
               response.ErrorCode = 2001;
               response.ErrorMessage = "Error while processing: " + ex.Message;
               BAL.Common.LogManager.LogError("DeleteEstimate", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return result;
       }

       public objResponse ViewEstimateDetail(Int64 Estimate_id_pk)
       {
           objResponse response = new objResponse();
           DataTable dt = new DataTable();           
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Estimate_ID", SqlDbType.BigInt, 0);
               sqlParameter[0].Value = Estimate_id_pk;

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_ViewEstimate", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               dt = response.ResponseData.Tables[0];
               if (dt.Rows.Count > 0)
               {
                   response.ErrorCode = 0;
                   response.ErrorMessage = "Success";                   
               }

           }
           catch (Exception ex)
           {
               response.ErrorCode = 2001;
               response.ErrorMessage = "Error while processing: " + ex.Message;
               BAL.Common.LogManager.LogError("ViewEstimateDetail", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return response;
       }

       public objResponse AddEstimationComment(long Estimation_ID, string Comment,DateTime Date ,long CommentBy ,string LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[7];

               sqlParameter[0] = new SqlParameter("@Estimation_ID", SqlDbType.BigInt, 50);
               sqlParameter[0].Value = Estimation_ID;

               sqlParameter[1] = new SqlParameter("@Comment", SqlDbType.NVarChar, 4000);
               sqlParameter[1].Value = Comment;

               sqlParameter[2] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[2].Value = LogedUser;

               sqlParameter[3] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[3].Value = DateTime.Now;

               sqlParameter[4] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[4].Value = "Active";

               sqlParameter[5] = new SqlParameter("@Date", SqlDbType.Date, 40);
               sqlParameter[5].Value = Date;

               sqlParameter[6] = new SqlParameter("@Comment_By", SqlDbType.BigInt, 40);
               sqlParameter[6].Value = CommentBy;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddEstimationComment", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("AddEstimationComment", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse UpdateEstimate(Project.Entity.Estimate objEstimate, string LogedUser , string Field)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[10];

               sqlParameter[0] = new SqlParameter("@Date", SqlDbType.Date, 50);
               sqlParameter[0].Value = objEstimate.Date;

               sqlParameter[1] = new SqlParameter("@Category", SqlDbType.BigInt, 80);
               sqlParameter[1].Value = objEstimate.Category;

               sqlParameter[2] = new SqlParameter("@Language", SqlDbType.Int, 50);
               sqlParameter[2].Value = objEstimate.Language;

               sqlParameter[3] = new SqlParameter("@AssignTo", SqlDbType.BigInt, 13);
               sqlParameter[3].Value = objEstimate.AssignTo;

               sqlParameter[4] = new SqlParameter("@Requirment", SqlDbType.NVarChar, 4000);
               sqlParameter[4].Value = objEstimate.Requirment;

               sqlParameter[5] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[5].Value = LogedUser;

               sqlParameter[6] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[6].Value = DateTime.Now;               
              
               sqlParameter[7] = new SqlParameter("@Priority", SqlDbType.NVarChar, 20);
               sqlParameter[7].Value = objEstimate.Priority;

               sqlParameter[8] = new SqlParameter("@Estimate_ID_Auto_PK", SqlDbType.BigInt, 10);
               sqlParameter[8].Value = objEstimate.Estimate_ID_Auto_PK;

               sqlParameter[9] = new SqlParameter("@Field", SqlDbType.NVarChar, 100);
               sqlParameter[9].Value = Field;



               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateEstimate", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("UpdateEstimate", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public string DeleteComment(Int64 Comment_id_pk)
       {
           objResponse response = new objResponse();
           DataTable dt = new DataTable();
           string result = "";
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Comment_id_pk", SqlDbType.BigInt, 0);
               sqlParameter[0].Value = Comment_id_pk;

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteEstimate", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               dt = response.ResponseData.Tables[0];
               if (dt.Rows.Count > 0)
               {
                   response.ErrorCode = 0;
                   response.ErrorMessage = "Success";
                   result = Convert.ToString(dt.Rows[0][0]);
               }

           }
           catch (Exception ex)
           {
               response.ErrorCode = 2001;
               response.ErrorMessage = "Error while processing: " + ex.Message;
               BAL.Common.LogManager.LogError("DeleteEstimateComment", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return result;
       }

       public objResponse ChangeStatus(long Estimate_ID, string Status, string Notes, DateTime Date, long CommentBy, string LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {

               SqlParameter[] sqlParameter = new SqlParameter[7];

               sqlParameter[0] = new SqlParameter("@Estimate_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Estimate_ID;

               sqlParameter[1] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[1].Value = Status;

               sqlParameter[2] = new SqlParameter("@Notes", SqlDbType.NVarChar, 4000);
               sqlParameter[2].Value = Notes;

               sqlParameter[3] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[3].Value = LogedUser;

               sqlParameter[4] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 60);
               sqlParameter[4].Value = DateTime.Now;

               sqlParameter[5] = new SqlParameter("@Date", SqlDbType.Date, 40);
               sqlParameter[5].Value = Date;

               sqlParameter[6] = new SqlParameter("@Comment_By", SqlDbType.BigInt, 40);
               sqlParameter[6].Value = CommentBy;

               
               

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ChangeEstimateStatus", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
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
               BAL.Common.LogManager.LogError("ChangeStatus", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public string DeleteEstimateComment(Int64 Comment_id_pk)
       {
           objResponse response = new objResponse();
           DataTable dt = new DataTable();
           string result = "";
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Comment_id_pk", SqlDbType.BigInt, 0);
               sqlParameter[0].Value = Comment_id_pk;

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteEstimateComment", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               dt = response.ResponseData.Tables[0];
               if (dt.Rows.Count > 0)
               {
                   response.ErrorCode = 0;
                   response.ErrorMessage = "Success";
                   result = Convert.ToString(dt.Rows[0][0]);
               }

           }
           catch (Exception ex)
           {
               response.ErrorCode = 2001;
               response.ErrorMessage = "Error while processing: " + ex.Message;
               BAL.Common.LogManager.LogError("DeleteEstimateComment", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return result;
       }
    }
}
