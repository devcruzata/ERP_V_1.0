using DAL;
using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Opportunity
{
   public class OpportunityManager
    {
       public objResponse AddOpportunity(Project.Entity.Opportunities objOpportunity, long LogedUser ,long PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[15];

               sqlParameter[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 200);
               sqlParameter[0].Value = objOpportunity.Name;

               sqlParameter[1] = new SqlParameter("@Amount", SqlDbType.NVarChar, 20);
               sqlParameter[1].Value = objOpportunity.Amount;

               sqlParameter[2] = new SqlParameter("@Client", SqlDbType.BigInt, 60);
               sqlParameter[2].Value = objOpportunity.RealateTo_ID;               

               sqlParameter[3] = new SqlParameter("@Es_date", SqlDbType.Date, 40);
               sqlParameter[3].Value = objOpportunity.ExpectedCloseDate;

               sqlParameter[4] = new SqlParameter("@Stage", SqlDbType.NVarChar, 80);
               sqlParameter[4].Value = objOpportunity.Stage;

               sqlParameter[5] = new SqlParameter("@Type", SqlDbType.NVarChar, 50);
               sqlParameter[5].Value = objOpportunity.Type;

               sqlParameter[6] = new SqlParameter("@Source", SqlDbType.NVarChar, 100);
               sqlParameter[6].Value = objOpportunity.Source;

               sqlParameter[7] = new SqlParameter("@Probability", SqlDbType.NVarChar, 4);
               sqlParameter[7].Value = objOpportunity.Probability;

               sqlParameter[8] = new SqlParameter("@LostReason", SqlDbType.NVarChar, 100);
               sqlParameter[8].Value = objOpportunity.LostReason;

               sqlParameter[9] = new SqlParameter("@AssignTo", SqlDbType.BigInt, 10);
               sqlParameter[9].Value = objOpportunity.AssignTO_ID;

               sqlParameter[10] = new SqlParameter("@Description", SqlDbType.NVarChar, 60);
               sqlParameter[10].Value = objOpportunity.Description;

               sqlParameter[11] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 60);
               sqlParameter[11].Value = LogedUser;

               sqlParameter[12] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[12].Value = DateTime.Now;

               sqlParameter[13] = new SqlParameter("@Status", SqlDbType.NVarChar, 40);
               sqlParameter[13].Value = "Active";

               sqlParameter[14] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[14].Value = PIN;


               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddOpportunity", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("AddOpportunity", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public List<TextValue> getClientOnSearch(long PIN,string SearchString)
       {
           objResponse Response = new objResponse();
           List<TextValue> nameList = new List<TextValue>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               sqlParameter[1] = new SqlParameter("@searchString", SqlDbType.NVarChar, 100);
               sqlParameter[1].Value = SearchString;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetClientForAutoCompleteBox", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = Convert.ToString(dr["Lead_ID_Auto_PK"]);
                       objText.Text = Convert.ToString(dr["Name"]);
                       nameList.Add(objText);
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
               BAL.Common.LogManager.LogError("getLead", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return nameList;
       }

       public List<Opportunities> getAllOpportunities(long PIN)
       {
           objResponse Response = new objResponse();
           List<Opportunities> opportunities = new List<Opportunities>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;


               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetAllOpportunities", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Opportunities objOpportunity = new Opportunities();
                       objOpportunity.Opportunity_ID = Convert.ToInt64(dr["Opportunity_ID_Auto_PK"]);
                       objOpportunity.Name = dr["Name"].ToString();
                       objOpportunity.Amount = dr["Amount"].ToString();
                       objOpportunity.RelateTo_Name = dr["RealtedTo"].ToString();
                       objOpportunity.RealateTo_ID = Convert.ToInt64(dr["Relate_To_ID_FK"]);
                       objOpportunity.stageId = dr["Stage"].ToString();
                       objOpportunity.Stage = dr["StageName"].ToString();
                       objOpportunity.Type = dr["Type"].ToString();
                       objOpportunity.Probability = dr["Probability"].ToString();
                       objOpportunity.AssignTO_ID = Convert.ToInt64(dr["AssignTo"]);
                       objOpportunity.AssignTO_Name = dr["AssignToName"].ToString();
                       objOpportunity.Description = dr["Description"].ToString();
                       objOpportunity.Source = dr["Source"].ToString();
                       opportunities.Add(objOpportunity);
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
               BAL.Common.LogManager.LogError("getAllOpportunities", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return opportunities;
       }

       public objResponse ViewOpportunities(long PIN , long OpportiunityID)
       {
           objResponse Response = new objResponse();
           List<Opportunities> opportunities = new List<Opportunities>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               sqlParameter[1] = new SqlParameter("@Opportunity_ID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = OpportiunityID;


               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetOpportunityByID", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;             
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
               BAL.Common.LogManager.LogError("ViewOpportunities", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse UpdateOpportunity(Project.Entity.Opportunities objOpportunity, string LogedUser, string Field)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[24];

               sqlParameter[0] = new SqlParameter("@ExpectedCloseDate", SqlDbType.Date, 50);
               sqlParameter[0].Value = objOpportunity.ExpectedCloseDate;

               sqlParameter[1] = new SqlParameter("@Name", SqlDbType.NVarChar, 200);
               sqlParameter[1].Value = objOpportunity.Name;

               sqlParameter[2] = new SqlParameter("@Amount", SqlDbType.NVarChar, 20);
               sqlParameter[2].Value = objOpportunity.Amount;

               sqlParameter[3] = new SqlParameter("@RealateTo_ID", SqlDbType.BigInt, 10);
               sqlParameter[3].Value = objOpportunity.RealateTo_ID;

               sqlParameter[4] = new SqlParameter("@Stage", SqlDbType.NVarChar, 80);
               sqlParameter[4].Value = objOpportunity.Stage;

               sqlParameter[5] = new SqlParameter("@Type", SqlDbType.NVarChar, 50);
               sqlParameter[5].Value = objOpportunity.Type;

               sqlParameter[6] = new SqlParameter("@Probability", SqlDbType.NVarChar, 4);
               sqlParameter[6].Value = objOpportunity.Probability;

               sqlParameter[7] = new SqlParameter("@LostReason", SqlDbType.NVarChar, 100);
               sqlParameter[7].Value = objOpportunity.LostReason;

               sqlParameter[8] = new SqlParameter("@Description", SqlDbType.NVarChar, 4000);
               sqlParameter[8].Value = objOpportunity.Description;

               sqlParameter[9] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[9].Value = LogedUser;

               sqlParameter[10] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[10].Value = DateTime.Now;

               sqlParameter[11] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[11].Value = "Active";

               sqlParameter[22] = new SqlParameter("@AssignTO_ID", SqlDbType.BigInt, 10);
               sqlParameter[22].Value = objOpportunity.AssignTO_ID;

               sqlParameter[22] = new SqlParameter("@Opportunity_ID", SqlDbType.BigInt, 10);
               sqlParameter[22].Value = objOpportunity.Opportunity_ID;

               sqlParameter[22] = new SqlParameter("@Opportunity_Owner", SqlDbType.BigInt, 10);
               sqlParameter[22].Value = objOpportunity.Opportunity_Owner;

               sqlParameter[22] = new SqlParameter("@Source", SqlDbType.NVarChar, 100);
               sqlParameter[22].Value = objOpportunity.Source;

               sqlParameter[23] = new SqlParameter("@Field", SqlDbType.NVarChar, 100);
               sqlParameter[23].Value = Field;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateLead", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("UpdateLead", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse ChangeStatus(long Opportunity_Id, string Status, string Notes, long LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {

               SqlParameter[] sqlParameter = new SqlParameter[5];

               sqlParameter[0] = new SqlParameter("@OpportunityID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Opportunity_Id;

               sqlParameter[1] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[1].Value = Status;

               sqlParameter[2] = new SqlParameter("@Notes", SqlDbType.NVarChar, 4000);
               sqlParameter[2].Value = Notes;

               sqlParameter[3] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 10);
               sqlParameter[3].Value = LogedUser;

               sqlParameter[4] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 60);
               sqlParameter[4].Value = DateTime.Now;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UChangeOpportunityStage", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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

       public string DeleteOpportunity(Int64 Oppo_id_pk)
       {
           objResponse response = new objResponse();
           DataTable dt = new DataTable();
           string result = "";
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Oppo_id_pk", SqlDbType.BigInt, 0);
               sqlParameter[0].Value = Oppo_id_pk;

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteOpportunity", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

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
               BAL.Common.LogManager.LogError("DeleteOpportunity", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return result;
       }

       public objResponse UpdateOpportunity(Project.Entity.Opportunities objOpportunity, long LogedUser, long PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[16];

               sqlParameter[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 200);
               sqlParameter[0].Value = objOpportunity.Name;

               sqlParameter[1] = new SqlParameter("@Amount", SqlDbType.NVarChar, 20);
               sqlParameter[1].Value = objOpportunity.Amount;

               sqlParameter[2] = new SqlParameter("@Client", SqlDbType.BigInt, 60);
               sqlParameter[2].Value = objOpportunity.RealateTo_ID;

               sqlParameter[3] = new SqlParameter("@Es_date", SqlDbType.Date, 40);
               sqlParameter[3].Value = objOpportunity.ExpectedCloseDate;

               sqlParameter[4] = new SqlParameter("@Stage", SqlDbType.NVarChar, 80);
               sqlParameter[4].Value = objOpportunity.Stage;

               sqlParameter[5] = new SqlParameter("@Type", SqlDbType.NVarChar, 50);
               sqlParameter[5].Value = objOpportunity.Type;

               sqlParameter[6] = new SqlParameter("@Source", SqlDbType.NVarChar, 100);
               sqlParameter[6].Value = objOpportunity.Source;

               sqlParameter[7] = new SqlParameter("@Probability", SqlDbType.NVarChar, 4);
               sqlParameter[7].Value = objOpportunity.Probability;

               sqlParameter[8] = new SqlParameter("@LostReason", SqlDbType.NVarChar, 100);
               sqlParameter[8].Value = objOpportunity.LostReason;

               sqlParameter[9] = new SqlParameter("@AssignTo", SqlDbType.BigInt, 10);
               sqlParameter[9].Value = objOpportunity.AssignTO_ID;

               sqlParameter[10] = new SqlParameter("@Description", SqlDbType.NVarChar, 60);
               sqlParameter[10].Value = objOpportunity.Description;

               sqlParameter[11] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 60);
               sqlParameter[11].Value = LogedUser;

               sqlParameter[12] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[12].Value = DateTime.Now;

               sqlParameter[13] = new SqlParameter("@Status", SqlDbType.NVarChar, 40);
               sqlParameter[13].Value = "Active";

               sqlParameter[14] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[14].Value = PIN;

               sqlParameter[15] = new SqlParameter("@Opportunity_ID", SqlDbType.BigInt, 10);
               sqlParameter[15].Value = objOpportunity.Opportunity_ID;


               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateOpportunity", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("UpdateOpportunity", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse AssignOpportunity(long Opportunity_ID, long UserID, long PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@Opportunity_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Opportunity_ID;

               sqlParameter[1] = new SqlParameter("@UserID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = UserID;

               sqlParameter[2] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[2].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AssignOpportunity", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("AssignOpportunity", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }
    }
}
