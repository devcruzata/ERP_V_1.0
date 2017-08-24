using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Ticket
{
   public class TicketManager
    {
       public List<Tickets> getAllTickets()
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Tickets> ticket = new List<Project.Entity.Tickets>();
           try
           {

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetAllTickets", DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.Entity.Tickets objtct = new Project.Entity.Tickets();
                       objtct.ticketID = Convert.ToInt32(dr["Ticket_ID_Auto_Pk"]);
                       objtct.ticketNo = Convert.ToString(dr["TicketNo"]);
                       objtct.subject = Convert.ToString(dr["subject"]);
                       objtct.query = dr["query"].ToString();
                       objtct.addedOn = Convert.ToDateTime(dr["createdon"]);                      
                       objtct.Status = Convert.ToString(dr["status"]);
                       ticket.Add(objtct);
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
               BAL.Common.LogManager.LogError("getAllTickets", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return ticket;

       }

       public objResponse OpenTicket(string subject , string body , long logedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@subject", SqlDbType.NVarChar, 200);
               sqlParameter[0].Value = subject;

               sqlParameter[1] = new SqlParameter("@body", SqlDbType.NVarChar, 500);
               sqlParameter[1].Value = body;

               sqlParameter[2] = new SqlParameter("@logedUser", SqlDbType.BigInt, 10);
               sqlParameter[2].Value = logedUser;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_OpenTicket", sqlParameter,DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("OpenTicket", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;

       }

       public objResponse closeTicket(long ticketID)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@ticketId", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = ticketID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_closeTicket", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("closeTicket", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;

       }

       public objResponse DeleteTicket(long ticketID)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@ticketId", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = ticketID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_deleteTicket", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("DeleteTicket", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;

       }
    }
}
