using DAL;
using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Clients
{
   public class ClientManager
    {
       public objResponse AddClient(Project.Entity.Clients objClient, long LogedUser , string PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[19];

               sqlParameter[0] = new SqlParameter("@Date", SqlDbType.Date, 50);
               sqlParameter[0].Value = objClient.Date;

               sqlParameter[1] = new SqlParameter("@Name", SqlDbType.NVarChar, 80);
               sqlParameter[1].Value = objClient.Name;

               sqlParameter[2] = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 80);
               sqlParameter[2].Value = objClient.CompanyName;

               sqlParameter[3] = new SqlParameter("@Email", SqlDbType.NVarChar, 50);
               sqlParameter[3].Value = objClient.Email;

               sqlParameter[4] = new SqlParameter("@Contact", SqlDbType.NVarChar, 13);
               sqlParameter[4].Value = objClient.ContactNo;

               sqlParameter[5] = new SqlParameter("@Skype", SqlDbType.NVarChar, 13);
               sqlParameter[5].Value = objClient.SkypeNo;               

               sqlParameter[6] = new SqlParameter("@Zipcode", SqlDbType.NVarChar, 8);
               sqlParameter[6].Value = objClient.ZipCode;               

               sqlParameter[7] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 60);
               sqlParameter[7].Value = LogedUser;

               sqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[8].Value = DateTime.Now;

               sqlParameter[9] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[9].Value = "Active";               

               sqlParameter[10] = new SqlParameter("@AddressLine1", SqlDbType.NVarChar, 100);
               sqlParameter[10].Value = objClient.AddressLine1;

               sqlParameter[11] = new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100);
               sqlParameter[11].Value = objClient.AddressLine2;

               sqlParameter[12] = new SqlParameter("@State", SqlDbType.NVarChar, 80);
               sqlParameter[12].Value = objClient.State;

               sqlParameter[13] = new SqlParameter("@Country", SqlDbType.NVarChar, 80);
               sqlParameter[13].Value = objClient.Country;               

               sqlParameter[14] = new SqlParameter("@Altername_Email", SqlDbType.NVarChar, 60);
               sqlParameter[14].Value = objClient.Alternate_Email;

               sqlParameter[15] = new SqlParameter("@Source", SqlDbType.NVarChar, 40);
               sqlParameter[15].Value = objClient.Source;

               sqlParameter[16] = new SqlParameter("@jobDescription", SqlDbType.NVarChar, 80);
               sqlParameter[16].Value = objClient.jobDescription;

               sqlParameter[17] = new SqlParameter("@City", SqlDbType.NVarChar, 50);
               sqlParameter[17].Value = objClient.City;

               sqlParameter[18] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[18].Value = Convert.ToInt64(PIN);

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddClient", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("addClient", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public List<Project.Entity.Clients> getClients(long PIN, long User_ID, string UserRole)
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Clients> clients = new List<Project.Entity.Clients>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               sqlParameter[1] = new SqlParameter("@User_ID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = User_ID;

               sqlParameter[2] = new SqlParameter("@UserRole", SqlDbType.NVarChar, 10);
               sqlParameter[2].Value = UserRole;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetClients", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.Entity.Clients objClient = new Project.Entity.Clients();
                       objClient.Client_ID_Auto_PK = Convert.ToInt64(dr["Client_ID_Auto_PK"]);
                       objClient.Name = Convert.ToString(dr["Name"]);
                       objClient.Date = Convert.ToDateTime(dr["Date"]);                      
                       objClient.Email = Convert.ToString(dr["Email"]);
                       objClient.SkypeNo = Convert.ToString(dr["SkypeNo"]);                      
                       objClient.Status = Convert.ToString(dr["Status"]);
                       objClient.AssignTO_Name = Convert.ToString(dr["AssignToUser"]);

                       clients.Add(objClient);
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
               BAL.Common.LogManager.LogError("getClient", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return clients;
       }

       public string DeleteClient(Int64 Client_id_pk)
       {
           objResponse response = new objResponse();
           DataTable dt = new DataTable();
           string result = "";
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Client_id_pk", SqlDbType.BigInt, 0);
               sqlParameter[0].Value = Client_id_pk;

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteClient", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

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
               BAL.Common.LogManager.LogError("DeleteClient", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return result;
       }

       public objResponse ViewClients(long Client_Id)
       {
           objResponse Response = new objResponse();
           try
           {

               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Client_Id", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Client_Id;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ViewClients", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("ViewClients", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }       

       public objResponse ImportClient(Project.Entity.Clients objClient, string LogedUser, long PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[19];

               sqlParameter[0] = new SqlParameter("@Date", SqlDbType.Date, 50);
               sqlParameter[0].Value = DateTime.Now;

               sqlParameter[1] = new SqlParameter("@Name", SqlDbType.NVarChar, 80);
               sqlParameter[1].Value = objClient.Name;

               sqlParameter[2] = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 80);
               sqlParameter[2].Value = objClient.CompanyName;

               sqlParameter[3] = new SqlParameter("@Email", SqlDbType.NVarChar, 50);
               sqlParameter[3].Value = objClient.Email;

               sqlParameter[4] = new SqlParameter("@Contact", SqlDbType.NVarChar, 13);
               sqlParameter[4].Value = objClient.ContactNo;

               sqlParameter[5] = new SqlParameter("@Skype", SqlDbType.NVarChar, 13);
               sqlParameter[5].Value = objClient.SkypeNo;

               sqlParameter[6] = new SqlParameter("@Zipcode", SqlDbType.NVarChar, 8);
               sqlParameter[6].Value = objClient.ZipCode;

               sqlParameter[7] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[7].Value = LogedUser;

               sqlParameter[8] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[8].Value = DateTime.Now;

               sqlParameter[9] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[9].Value = "Active";

               sqlParameter[10] = new SqlParameter("@AddressLine1", SqlDbType.NVarChar, 100);
               sqlParameter[10].Value = objClient.AddressLine1;

               sqlParameter[11] = new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100);
               sqlParameter[11].Value = objClient.AddressLine2;

               sqlParameter[12] = new SqlParameter("@State", SqlDbType.NVarChar, 80);
               sqlParameter[12].Value = objClient.State;

               sqlParameter[13] = new SqlParameter("@Country", SqlDbType.NVarChar, 80);
               sqlParameter[13].Value = objClient.Country;

               sqlParameter[14] = new SqlParameter("@Altername_Email", SqlDbType.NVarChar, 60);
               sqlParameter[14].Value = objClient.Alternate_Email;

               sqlParameter[15] = new SqlParameter("@Source", SqlDbType.NVarChar, 40);
               sqlParameter[15].Value = objClient.Source;

               sqlParameter[16] = new SqlParameter("@Model", SqlDbType.NVarChar, 50);
               sqlParameter[16].Value = objClient.Model;

               sqlParameter[17] = new SqlParameter("@City", SqlDbType.NVarChar, 50);
               sqlParameter[17].Value = objClient.City;

               sqlParameter[18] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[18].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddClient", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("ImportClient", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse getClientforUpdate(long Client_Id)
       {
           objResponse Response = new objResponse();

           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Client_Id", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Client_Id;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetClientForUpdate", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = "Sucess";
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
               BAL.Common.LogManager.LogError("getClientsforUpdate", 1, Convert.ToString(ex.Message), Convert.ToString(ex.Source), Convert.ToString(ex.StackTrace));

           }
           return Response;
       }

       public objResponse UpdateClient(Project.Entity.Clients objClient, long LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[17];

               sqlParameter[0] = new SqlParameter("@Name", SqlDbType.NVarChar, 80);
               sqlParameter[0].Value = objClient.Name;

               sqlParameter[1] = new SqlParameter("@CompanyName", SqlDbType.NVarChar, 80);
               sqlParameter[1].Value = objClient.CompanyName;

               sqlParameter[2] = new SqlParameter("@Email", SqlDbType.NVarChar, 50);
               sqlParameter[2].Value = objClient.Email;

               sqlParameter[3] = new SqlParameter("@Contact", SqlDbType.NVarChar, 13);
               sqlParameter[3].Value = objClient.ContactNo;

               sqlParameter[4] = new SqlParameter("@Skype", SqlDbType.NVarChar, 13);
               sqlParameter[4].Value = objClient.SkypeNo;

               sqlParameter[5] = new SqlParameter("@Zipcode", SqlDbType.NVarChar, 8);
               sqlParameter[5].Value = objClient.ZipCode;

               sqlParameter[6] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 10);
               sqlParameter[6].Value = LogedUser;

               sqlParameter[7] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[7].Value = DateTime.Now;

               sqlParameter[8] = new SqlParameter("@AddressLine1", SqlDbType.NVarChar, 100);
               sqlParameter[8].Value = objClient.AddressLine1;

               sqlParameter[9] = new SqlParameter("@AddressLine2", SqlDbType.NVarChar, 100);
               sqlParameter[9].Value = objClient.AddressLine2;

               sqlParameter[10] = new SqlParameter("@State", SqlDbType.NVarChar, 80);
               sqlParameter[10].Value = objClient.State;

               sqlParameter[11] = new SqlParameter("@Country", SqlDbType.NVarChar, 80);
               sqlParameter[11].Value = objClient.Country;

               sqlParameter[12] = new SqlParameter("@Altername_Email", SqlDbType.NVarChar, 60);
               sqlParameter[12].Value = objClient.Alternate_Email;

               sqlParameter[13] = new SqlParameter("@Source", SqlDbType.NVarChar, 40);
               sqlParameter[13].Value = objClient.Source;

               sqlParameter[14] = new SqlParameter("@City", SqlDbType.NVarChar, 50);
               sqlParameter[14].Value = objClient.City;

               sqlParameter[15] = new SqlParameter("@Client_ID_Auto_PK", SqlDbType.BigInt, 50);
               sqlParameter[15].Value = objClient.Client_ID_Auto_PK;

               sqlParameter[16] = new SqlParameter("@JobDescription", SqlDbType.NVarChar, 100);
               sqlParameter[16].Value = objClient.jobDescription;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateClient", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("UpdateClient", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public List<TextValue> getClientContactForSync(long logedUser , long PIN)
       {
           objResponse Response = new objResponse();
           List<TextValue> objContactList = new List<TextValue>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@logedUser", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = logedUser;

               sqlParameter[1] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "getClientContactForSynch", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = "Sucess";
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objTextValue = new TextValue();
                       objTextValue.Text = dr["Name"].ToString();
                       objTextValue.Value = dr["Email"].ToString();
                       objContactList.Add(objTextValue);
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
               BAL.Common.LogManager.LogError("getClientContactForSync", 1, Convert.ToString(ex.Message), Convert.ToString(ex.Source), Convert.ToString(ex.StackTrace));

           }
           return objContactList;
       }

       public objResponse AddComment(long ClientId, string Comment, long logedUser)
       {
           objResponse res = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[4];

               sqlParameter[0] = new SqlParameter("@ClientId", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = ClientId;

               sqlParameter[1] = new SqlParameter("@logedUser", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = logedUser;

               sqlParameter[2] = new SqlParameter("@Comment", SqlDbType.NVarChar, 4000);
               sqlParameter[2].Value = Comment;

               sqlParameter[3] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 60);
               sqlParameter[3].Value = DateTime.Now;

               DAL.DATA_ACCESS_LAYER.Fill(res.ResponseData, "usp_AddComment", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (res.ResponseData.Tables[0].Rows.Count > 0)
               {
                   res.ErrorCode = 0;
                   res.ErrorMessage = "Sucess";
               }
               else
               {
                   res.ErrorCode = 2001;
                   res.ErrorMessage = "There is an Error. Please Try After some time.";
               }

           }
           catch (Exception ex)
           {
               res.ErrorCode = 3001;
               res.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("AddComment", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return res;

       }
    }
}
