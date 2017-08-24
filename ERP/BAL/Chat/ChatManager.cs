using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Chat
{
   public class ChatManager
    {
       public List<Users> GetUsersForChat(string PIN , long LogedUserID)
       {
           objResponse Response = new objResponse();
           List<Users> user = new List<Users>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Convert.ToInt64(PIN);

               sqlParameter[1] = new SqlParameter("@LogedUserID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = LogedUserID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetUsersForChat", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Users objUser = new Users();
                       objUser.User_ID_PK = Convert.ToInt64(dr["User_ID_Auto_PK"]);
                       objUser.FullName = dr["Name"].ToString();
                       objUser.UnReadReadCount = dr["UnreadMsg"].ToString();                      
                       
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
               BAL.Common.LogManager.LogError("GetUsersForChat", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return user;
       }

       public List<Project.Entity.Chat> GetUsersChat(string Sender ,string Reciever , string PIN)
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Chat> chat = new List<Project.Entity.Chat>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@Sender", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Convert.ToInt64(Sender);

               sqlParameter[1] = new SqlParameter("@Reciever", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = Convert.ToInt64(Reciever);       

               sqlParameter[2] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[2].Value = Convert.ToInt64(PIN);

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetUserChat", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.Entity.Chat objUserChat = new Project.Entity.Chat();                      
                       objUserChat.Msg = dr["Msg"].ToString();
                       objUserChat.SenderID = Convert.ToInt64(dr["Sender_ID"]);
                       objUserChat.RecieverID = Convert.ToInt64(dr["Reciever_ID"]);
                       objUserChat.Date = Convert.ToString(dr["date"]);

                       chat.Add(objUserChat);
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
               BAL.Common.LogManager.LogError("GetUsersChat", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return chat;
       }

       public objResponse AddUserchat(string SenderID, string RecieverID, string Msg, string PIN, long LogedUserID)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[6];

               sqlParameter[0] = new SqlParameter("@SenderID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Convert.ToInt64(SenderID);

               sqlParameter[1] = new SqlParameter("@RecieverID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = Convert.ToInt64(RecieverID);

               sqlParameter[2] = new SqlParameter("@Msg", SqlDbType.NVarChar, 4000);
               sqlParameter[2].Value = Msg;

               sqlParameter[3] = new SqlParameter("@Date", SqlDbType.DateTime, 10);
               sqlParameter[3].Value = DateTime.Now;

               sqlParameter[4] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[4].Value = Convert.ToInt64(PIN);

               sqlParameter[5] = new SqlParameter("@LogedUserID", SqlDbType.BigInt, 10);
               sqlParameter[5].Value = LogedUserID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddUserChat", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = "Success";
               }
               else
               {
                   Response.ErrorCode = 2001;
                   Response.ErrorMessage = "Error";
               }
           }
           catch (Exception ex)
           {
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("AddUserchat", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }
    }
}
