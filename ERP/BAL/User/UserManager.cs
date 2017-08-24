using DAL;
using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.User
{
   public class UserManager
    {
       public objResponse validateUser(string UserName , string Password)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 60);
               sqlParameter[0].Value = UserName;

               sqlParameter[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 20);
               sqlParameter[1].Value = Password;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ValidateUser", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString(); ;
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
               BAL.Common.LogManager.LogError("validate User", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));              
           }

           return Response;
       }

       public objResponse AddUser(string uid,string FirstName, string LastName, string Email, string Type_Id, string LogedUser, string PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[9];

               sqlParameter[0] = new SqlParameter("@FirstName", SqlDbType.NVarChar, 60);
               sqlParameter[0].Value = FirstName;

               sqlParameter[1] = new SqlParameter("@LastName", SqlDbType.NVarChar, 60);
               sqlParameter[1].Value = LastName;

               sqlParameter[2] = new SqlParameter("@Email", SqlDbType.NVarChar, 80);
               sqlParameter[2].Value = Email;

               sqlParameter[3] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[3].Value = LogedUser;

               sqlParameter[4] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 20);
               sqlParameter[4].Value = DateTime.Now;

               sqlParameter[5] = new SqlParameter("@Status", SqlDbType.NVarChar, 30);
               sqlParameter[5].Value = "Invitation Pending";

               sqlParameter[6] = new SqlParameter("@User_Type", SqlDbType.BigInt, 10);
               sqlParameter[6].Value = Convert.ToInt64(Type_Id);

               sqlParameter[7] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[7].Value = Convert.ToInt64(PIN);

               sqlParameter[8] = new SqlParameter("@Uid", SqlDbType.NVarChar, 40);
               sqlParameter[8].Value = uid;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddUser", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString(); ;
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
               BAL.Common.LogManager.LogError("AddUser", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }

           return Response;
       }

       public List<Users> GetUsers(string PIN)
       {
           objResponse Response = new objResponse();
           List<Users> user = new List<Users>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Convert.ToInt64(PIN);

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetUsers", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Users objUser = new Users();
                       objUser.User_ID_PK = Convert.ToInt64(dr["User_ID_Auto_PK"]);
                       objUser.Uid = dr["UserId"].ToString();
                       objUser.FullName = dr["User_FirstName"].ToString() + " " + dr["User_LastName"].ToString();
                       objUser.Email = dr["User_Email"].ToString();
                       objUser.Status = dr["Status"].ToString();
                       objUser.Date = Convert.ToDateTime(dr["CreatedDate"]);                      
                       objUser.UserType = dr["User_Type"].ToString();
                       
                       user.Add(objUser);
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
               BAL.Common.LogManager.LogError("GetUsers", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return user;
       }

       public List<TextValue> GetUserType(string PIN)
       {
           objResponse Response = new objResponse();
           List<TextValue> usertype = new List<TextValue>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Convert.ToInt64(PIN);

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetUserType", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objUserTypes = new TextValue();
                       objUserTypes.Value = Convert.ToString(dr["User_Role_ID_Auto_PK"]);
                       objUserTypes.Text = dr["User_Role_Desc"].ToString();

                       usertype.Add(objUserTypes);
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
               BAL.Common.LogManager.LogError("GetUserType", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return usertype;
       }

       public objResponse ActivateAccount(string UserName, string Password, string PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@UserName", SqlDbType.NVarChar, 60);
               sqlParameter[0].Value = UserName;

               sqlParameter[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 20);
               sqlParameter[1].Value = Password;

               sqlParameter[2] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[2].Value = Convert.ToInt64(PIN);

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ActivateAccount", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString(); ;
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
               BAL.Common.LogManager.LogError("ActivateAccount", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }

           return Response;
       }

       public objResponse DeleteUser(long User_ID_Auto_Pk)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@User_ID_Auto_Pk", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Convert.ToInt64(User_ID_Auto_Pk);

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_deleteUser", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("DeleteUser", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }

           return Response;
       }

       public objResponse ChangeUserStatus(long User_ID_Auto_Pk)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@User_ID_Auto_Pk", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Convert.ToInt64(User_ID_Auto_Pk);

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_changeUserStatus", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("ChangeUserStatus", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }

           return Response;
       }

       public objResponse LogUser(DateTime logOut,long User_ID_Auto_PK, long PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@logOut", SqlDbType.NVarChar, 60);
               sqlParameter[0].Value = logOut;

               sqlParameter[1] = new SqlParameter("@LogedUser", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = User_ID_Auto_PK;

               sqlParameter[2] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[2].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_logUser", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString(); ;
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
               BAL.Common.LogManager.LogError("LogUser", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));              
           }
           return Response;
       }

    }
}
