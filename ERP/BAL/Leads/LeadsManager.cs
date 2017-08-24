using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Leads
{
   public class LeadsManager
    {
       public objResponse AddLead(Project.Entity.Leads objLead , long LogedUser , long PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[19];

                sqlParameter[0] = new SqlParameter("@Date", SqlDbType.Date, 50);
                sqlParameter[0].Value = objLead.Date;

                sqlParameter[1] = new SqlParameter("@Name", SqlDbType.NVarChar, 80);
                sqlParameter[1].Value = objLead.Name;

                sqlParameter[2] = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 80);
                sqlParameter[2].Value = objLead.CompanyName;

                sqlParameter[3] = new SqlParameter("@Email", SqlDbType.NVarChar, 50);
                sqlParameter[3].Value = objLead.Email;

                sqlParameter[4] = new SqlParameter("@Contact", SqlDbType.NVarChar, 13);
                sqlParameter[4].Value = objLead.ContactNo;

                sqlParameter[5] = new SqlParameter("@Skype", SqlDbType.NVarChar, 13);
                sqlParameter[5].Value = objLead.SkypeNo;                

                sqlParameter[6] = new SqlParameter("@Zipcode", SqlDbType.NVarChar, 8);
                sqlParameter[6].Value = objLead.ZipCode;                

                sqlParameter[7] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 60);
                sqlParameter[7].Value = LogedUser;

                sqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
                sqlParameter[8].Value = DateTime.Now;

                sqlParameter[9] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
                sqlParameter[9].Value = "NEW";                

                sqlParameter[10] = new SqlParameter("@AddressLine1", SqlDbType.NVarChar, 100);
                sqlParameter[10].Value = objLead.AddressLine1;

                sqlParameter[11] = new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100);
                sqlParameter[11].Value = objLead.AddressLine2;

                sqlParameter[12] = new SqlParameter("@State", SqlDbType.NVarChar, 80);
                sqlParameter[12].Value = objLead.State;

                sqlParameter[13] = new SqlParameter("@Country", SqlDbType.NVarChar, 80);
                sqlParameter[13].Value = objLead.Country;              

                sqlParameter[14] = new SqlParameter("@Altername_Email", SqlDbType.NVarChar, 60);
                sqlParameter[14].Value = objLead.Alternate_Email;

                sqlParameter[15] = new SqlParameter("@Source", SqlDbType.NVarChar, 40);
                sqlParameter[15].Value = objLead.Source;                

                sqlParameter[16] = new SqlParameter("@City", SqlDbType.NVarChar, 50);
                sqlParameter[16].Value = objLead.City;

                sqlParameter[17] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
                sqlParameter[17].Value = PIN;

                sqlParameter[18] = new SqlParameter("@JobDescription", SqlDbType.NVarChar, 80);
                sqlParameter[18].Value = objLead.JobDescription;                

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddLead", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
           catch(Exception ex)
           {
                Response.ErrorCode = 3001;
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("addLead", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse AddLeadUpload(long Lead_ID , string FileName , string LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[5];

               sqlParameter[0] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 50);
               sqlParameter[0].Value = Lead_ID;

               sqlParameter[1] = new SqlParameter("@FileName", SqlDbType.NVarChar, 200);
               sqlParameter[1].Value = FileName;               

               sqlParameter[2] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[2].Value = LogedUser;

               sqlParameter[3] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[3].Value = DateTime.Now;

               sqlParameter[4] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[4].Value = "Active";

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddLeadUploads", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("addLeadUpload", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public List<Project.Entity.Leads> getLeads(long PIN ,long User_ID,string UserRole)
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Leads> Leads = new List<Project.Entity.Leads>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               sqlParameter[1] = new SqlParameter("@User_ID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = User_ID;

               sqlParameter[2] = new SqlParameter("@UserRole", SqlDbType.NVarChar, 10);
               sqlParameter[2].Value = UserRole;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetLeads", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.Entity.Leads objLead = new Project.Entity.Leads();
                       objLead.Lead_ID_Auto_PK = Convert.ToInt64(dr["Lead_ID_Auto_PK"]);                       
                       objLead.Name = Convert.ToString(dr["Name"]);
                       if (dr["Date"].ToString()!= "") 
                       { 
                       objLead.Date = Convert.ToDateTime(dr["Date"]);
                      
                       }
                       if (dr["FutureFollowUp"].ToString() != "")
                       {
                           objLead.FollowUpDate = Convert.ToDateTime(dr["FutureFollowUp"]);
                       }
                       objLead.Email = Convert.ToString(dr["Email"]);
                       objLead.SkypeNo = Convert.ToString(dr["SkypeNo"]);
                       
                       objLead.Status = Convert.ToString(dr["Status"]);
                       objLead.PIN = Convert.ToInt64(dr["PIN"]);
                       objLead.AssignTo = dr["AssignTo"].ToString();
                       objLead.AssignToName = dr["AssignToUser"].ToString();
                       objLead.CreatedBy = dr["CreatedBy"].ToString();
                       Leads.Add(objLead);
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
           return Leads;
       }

       public List<Project.Entity.Leads> getNewLeads(long PIN, long User_ID, string UserRole)
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Leads> Leads = new List<Project.Entity.Leads>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               sqlParameter[1] = new SqlParameter("@User_ID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = User_ID;

               sqlParameter[2] = new SqlParameter("@UserRole", SqlDbType.NVarChar, 20);
               sqlParameter[2].Value = UserRole;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetNewLeads", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.Entity.Leads objLead = new Project.Entity.Leads();
                       objLead.Lead_ID_Auto_PK = Convert.ToInt64(dr["Lead_ID_Auto_PK"]);                       
                       objLead.Name = Convert.ToString(dr["Name"]);
                       objLead.Date = Convert.ToDateTime(dr["Date"]);
                       if (dr["FutureFollowUp"].ToString() != "")
                       {
                           objLead.FollowUpDate = Convert.ToDateTime(dr["FutureFollowUp"]);
                       }
                       
                       objLead.Email = Convert.ToString(dr["Email"]);
                       objLead.SkypeNo = Convert.ToString(dr["SkypeNo"]);
                      // objLead.HasAttachment = Convert.ToBoolean(dr["HasAttachment"]);
                       objLead.Status = Convert.ToString(dr["Status"]);
                       objLead.PIN = Convert.ToInt64(dr["PIN"]);
                       objLead.AssignTo = dr["AssignTo"].ToString();
                       objLead.AssignToName = dr["AssignToUser"].ToString();
                       objLead.CreatedBy = dr["CreatedBy"].ToString();
                       Leads.Add(objLead);
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
           return Leads;
       }

       public List<Project.Entity.Leads> getNotRepliedLeads(long PIN, long User_ID, string UserRole)
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Leads> Leads = new List<Project.Entity.Leads>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               sqlParameter[1] = new SqlParameter("@User_ID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = User_ID;

               sqlParameter[2] = new SqlParameter("@UserRole", SqlDbType.NVarChar, 10);
               sqlParameter[2].Value = UserRole;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetNotRepliedLeads", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.Entity.Leads objLead = new Project.Entity.Leads();
                       objLead.Lead_ID_Auto_PK = Convert.ToInt64(dr["Lead_ID_Auto_PK"]);                       
                       objLead.Name = Convert.ToString(dr["Name"]);
                       objLead.Date = Convert.ToDateTime(dr["Date"]);
                       objLead.FollowUpDate = Convert.ToDateTime(dr["FutureFollowUp"]);
                       objLead.Email = Convert.ToString(dr["Email"]);
                       objLead.SkypeNo = Convert.ToString(dr["SkypeNo"]);
                       //objLead.HasAttachment = Convert.ToBoolean(dr["HasAttachment"]);
                       objLead.Status = Convert.ToString(dr["Status"]);
                       objLead.PIN = Convert.ToInt64(dr["PIN"]);
                       objLead.AssignTo = dr["AssignTo"].ToString();
                       objLead.AssignToName = dr["AssignToUser"].ToString();
                       objLead.CreatedBy = dr["CreatedBy"].ToString();
                       Leads.Add(objLead);
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
               BAL.Common.LogManager.LogError("getNotRepliedLeads", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Leads;
       }

       public objResponse ViewLeads(string Lead_Id)
       {
           objResponse Response = new objResponse();          
           try
           {

               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Convert.ToInt64(Lead_Id);

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ViewLeads", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("ViewLeads", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse UpdateLeadDetails(long Lead_Id , string Status , string Notes , DateTime FollowupDate ,long LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {

               SqlParameter[] sqlParameter = new SqlParameter[7];

               sqlParameter[0] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Lead_Id;

               sqlParameter[1] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[1].Value = Status;

               sqlParameter[2] = new SqlParameter("@Notes", SqlDbType.NVarChar, 4000);
               sqlParameter[2].Value = Notes;

               sqlParameter[3] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 10);
               sqlParameter[3].Value = LogedUser;

               sqlParameter[4] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 60);
               sqlParameter[4].Value = DateTime.Now;

               sqlParameter[5] = new SqlParameter("@DateTaken", SqlDbType.Date, 60);
               sqlParameter[5].Value = DateTime.Now;

               sqlParameter[6] = new SqlParameter("@FollowupDate", SqlDbType.Date, 60);
               sqlParameter[6].Value = FollowupDate;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateLeadDetails", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("UpdateLeadDetails", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse UpdateLead(Project.Entity.Leads objLead,long LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[17];               

               sqlParameter[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 80);
               sqlParameter[0].Value = objLead.Name;

               sqlParameter[1] = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 80);
               sqlParameter[1].Value = objLead.CompanyName;

               sqlParameter[2] = new SqlParameter("@Email", SqlDbType.NVarChar, 50);
               sqlParameter[2].Value = objLead.Email;

               sqlParameter[3] = new SqlParameter("@Contact", SqlDbType.NVarChar, 13);
               sqlParameter[3].Value = objLead.ContactNo;

               sqlParameter[4] = new SqlParameter("@Skype", SqlDbType.NVarChar, 13);
               sqlParameter[4].Value = objLead.SkypeNo;              

               sqlParameter[5] = new SqlParameter("@Zipcode", SqlDbType.NVarChar, 8);
               sqlParameter[5].Value = objLead.ZipCode;               

               sqlParameter[6] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 10);
               sqlParameter[6].Value = LogedUser;

               sqlParameter[7] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[7].Value = DateTime.Now;               

               sqlParameter[8] = new SqlParameter("@AddressLine1", SqlDbType.NVarChar, 100);
               sqlParameter[8].Value = objLead.AddressLine1;

               sqlParameter[9] = new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100);
               sqlParameter[9].Value = objLead.AddressLine2;

               sqlParameter[10] = new SqlParameter("@State", SqlDbType.NVarChar, 80);
               sqlParameter[10].Value = objLead.State;

               sqlParameter[11] = new SqlParameter("@Country", SqlDbType.NVarChar, 80);
               sqlParameter[11].Value = objLead.Country;               

               sqlParameter[12] = new SqlParameter("@Altername_Email", SqlDbType.NVarChar, 60);
               sqlParameter[12].Value = objLead.Alternate_Email;

               sqlParameter[13] = new SqlParameter("@Source", SqlDbType.NVarChar, 40);
               sqlParameter[13].Value = objLead.Source;               

               sqlParameter[14] = new SqlParameter("@City", SqlDbType.NVarChar, 50);
               sqlParameter[14].Value = objLead.City;

               sqlParameter[15] = new SqlParameter("@Lead_ID_PK", SqlDbType.BigInt, 50);
               sqlParameter[15].Value = objLead.Lead_ID_Auto_PK;

               sqlParameter[16] = new SqlParameter("@JobDescription", SqlDbType.NVarChar, 100);
               sqlParameter[16].Value = objLead.JobDescription;

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

       public objResponse getLeadForUpdate(long Lead_Id)
       {
           objResponse Response = new objResponse();
           
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Lead_Id", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Lead_Id;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetLeadForUpdate", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("getLeadForUpdate", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public string DeleteLead(string Lead_id_pk)
       {
           objResponse response = new objResponse();
           DataTable dt = new DataTable();
           string result = "";
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Lead_id_pk", SqlDbType.BigInt,10);
               sqlParameter[0].Value =Convert.ToInt64(Lead_id_pk);

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteLead", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

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
               BAL.Common.LogManager.LogError("DeleteLead", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return result;
       }

       public string DeleteLeadUpload(Int64 Lead_id_pk , string fileName)
       {
           objResponse response = new objResponse();
           DataTable dt = new DataTable();
           string result = "";
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@Lead_id_pk", SqlDbType.BigInt, 0);
               sqlParameter[0].Value = Lead_id_pk;

               sqlParameter[1] = new SqlParameter("@fileName", SqlDbType.NVarChar, 200);
               sqlParameter[1].Value = fileName;

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteLeadUpload", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

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
               BAL.Common.LogManager.LogError("DeleteLead", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return result;
       }

       public objResponse AssignLead(long Lead_ID, long UserID , long PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Lead_ID;

               sqlParameter[1] = new SqlParameter("@UserID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = UserID;

               sqlParameter[2] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[2].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AssignLead", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("AssignLead", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse ImportLead(Project.Entity.Leads objLead, string LogedUser, long PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[23];

               sqlParameter[0] = new SqlParameter("@Date", SqlDbType.Date, 50);
               sqlParameter[0].Value = DateTime.Now;

               sqlParameter[1] = new SqlParameter("@Name", SqlDbType.NVarChar, 80);
               sqlParameter[1].Value = objLead.Name;

               sqlParameter[2] = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 80);
               sqlParameter[2].Value = objLead.CompanyName;

               sqlParameter[3] = new SqlParameter("@Email", SqlDbType.NVarChar, 50);
               sqlParameter[3].Value = objLead.Email;

               sqlParameter[4] = new SqlParameter("@Contact", SqlDbType.NVarChar, 13);
               sqlParameter[4].Value = objLead.ContactNo;

               sqlParameter[5] = new SqlParameter("@Skype", SqlDbType.NVarChar, 13);
               sqlParameter[5].Value = objLead.SkypeNo;

               sqlParameter[6] = new SqlParameter("@Category", SqlDbType.NVarChar, 30);
               sqlParameter[6].Value = objLead.Category_ID;

               sqlParameter[7] = new SqlParameter("@Zipcode", SqlDbType.NVarChar, 8);
               sqlParameter[7].Value = objLead.ZipCode;

               sqlParameter[8] = new SqlParameter("@Requirment", SqlDbType.NVarChar, 4000);
               sqlParameter[8].Value = objLead.Requirement;

               sqlParameter[9] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[9].Value = LogedUser;

               sqlParameter[10] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[10].Value = DateTime.Now;

               sqlParameter[11] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[11].Value = "Working";

               sqlParameter[12] = new SqlParameter("@Remarks", SqlDbType.NVarChar, 4000);
               sqlParameter[12].Value = objLead.Remarks;

               sqlParameter[13] = new SqlParameter("@AddressLine1", SqlDbType.NVarChar, 100);
               sqlParameter[13].Value = objLead.AddressLine1;

               sqlParameter[14] = new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100);
               sqlParameter[14].Value = objLead.AddressLine2;

               sqlParameter[15] = new SqlParameter("@State", SqlDbType.NVarChar, 80);
               sqlParameter[15].Value = objLead.State;

               sqlParameter[16] = new SqlParameter("@Country", SqlDbType.NVarChar, 80);
               sqlParameter[16].Value = objLead.Country;

               sqlParameter[17] = new SqlParameter("@FollowUpDate", SqlDbType.Date, 80);
               sqlParameter[17].Value = objLead.FollowUpDate;

               sqlParameter[18] = new SqlParameter("@Altername_Email", SqlDbType.NVarChar, 60);
               sqlParameter[18].Value = objLead.Alternate_Email;

               sqlParameter[19] = new SqlParameter("@Source", SqlDbType.NVarChar, 40);
               sqlParameter[19].Value = objLead.Source;

               sqlParameter[20] = new SqlParameter("@Model", SqlDbType.NVarChar, 50);
               sqlParameter[20].Value = objLead.Model;

               sqlParameter[21] = new SqlParameter("@City", SqlDbType.NVarChar, 50);
               sqlParameter[21].Value = objLead.City;

               sqlParameter[22] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[22].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddLead", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("ImportLead", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }
    }
}
