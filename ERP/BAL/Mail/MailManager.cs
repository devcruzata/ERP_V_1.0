using DAL;
using Project.Entity;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Mail;

namespace BAL.Mail
{
   public class MailManager
    {

       public static Boolean SendSimpleMessageBySMTP(string FromAddress, string ToAddress, string BccAddress, string CcAddress, string Subject, string MailBody)
       {
           // Compose a message
           System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage(FromAddress, ToAddress);
           mail.Subject = Subject;
           mail.Body = MailBody;

           // Send it!
           SmtpClient client = new SmtpClient();
           client.Port = 587;
           client.DeliveryMethod = SmtpDeliveryMethod.Network;
           client.UseDefaultCredentials = false;
           client.Credentials = new System.Net.NetworkCredential("abhishek@sandbox90952ac641ab46a8a83bc50f23d98215.mailgun.org", "Abhi@1249");
           client.Host = "smtp.mailgun.org";

           client.Send(mail);
           return true;
       }

       public static IRestResponse SendSimpleMessageUsingAPI(string FromAddress,string ToAddress,string BccAddress,string CcAddress , string Subject , string MailBody)
       {
           RestClient client = new RestClient();
           client.BaseUrl = new Uri("https://api.mailgun.net/v3/sandbox90952ac641ab46a8a83bc50f23d98215.mailgun.org");
           client.Authenticator = new HttpBasicAuthenticator("api", "key-92aa7c64fa2bcf2bc563eda1ebdfa45c");
           RestRequest request = new RestRequest();
           request.AddParameter("domain","sandbox90952ac641ab46a8a83bc50f23d98215.mailgun.org", ParameterType.UrlSegment);
           request.Resource = "{domain}/messages";
           request.AddParameter("from", FromAddress);
           request.AddParameter("to", ToAddress);
           request.AddParameter("to", BccAddress);
           request.AddParameter("subject", Subject);
           request.AddParameter("text", MailBody);
           request.Method = Method.POST;
           return client.Execute(request);
       }

        //public objResponse AddMeeting(string Title, DateTime SheduledDate, long Relate_To_ID, string Agenda, string RemindMe, string Hours, string Minutes, string Status, string LogedUser, long PIN)
        //{
        //    objResponse Response = new objResponse();
        //    try
        //    {
        //        SqlParameter[] sqlParameter = new SqlParameter[11];

        //        sqlParameter[0] = new SqlParameter("@Title", SqlDbType.NVarChar, 1000);
        //        sqlParameter[0].Value = Title;

        //        sqlParameter[1] = new SqlParameter("@SheduledDate", SqlDbType.DateTime, 60);
        //        sqlParameter[1].Value = SheduledDate;

        //        sqlParameter[2] = new SqlParameter("@Relate_To_ID", SqlDbType.BigInt, 10);
        //        sqlParameter[2].Value = Relate_To_ID;

        //        sqlParameter[3] = new SqlParameter("@Agenda", SqlDbType.NVarChar, 4000);
        //        sqlParameter[3].Value = Agenda;

        //        sqlParameter[4] = new SqlParameter("@RemindMe", SqlDbType.NVarChar, 2);
        //        sqlParameter[4].Value = RemindMe;

        //        sqlParameter[5] = new SqlParameter("@Hours", SqlDbType.NVarChar, 3);
        //        sqlParameter[5].Value = Hours;

        //        sqlParameter[6] = new SqlParameter("@Minutes", SqlDbType.NVarChar, 3);
        //        sqlParameter[6].Value = Minutes;

        //        sqlParameter[7] = new SqlParameter("@Status", SqlDbType.NVarChar, 3);
        //        sqlParameter[7].Value = Status;

        //        sqlParameter[8] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
        //        sqlParameter[8].Value = LogedUser;

        //        sqlParameter[9] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 60);
        //        sqlParameter[9].Value = DateTime.Now;

        //        sqlParameter[10] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
        //        sqlParameter[10].Value = PIN;

        //        DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddMeeting", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


        //        if (Response.ResponseData.Tables[0].Rows.Count > 0)
        //        {
        //            Response.ErrorCode = 0;
        //            Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
        //        }
        //        else
        //        {
        //            Response.ErrorCode = 2001;
        //            Response.ErrorMessage = "There is an Error. Please Try After some time.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.ErrorCode = 3001;
        //        Response.ErrorMessage = ex.Message.ToString();
        //        BAL.Common.LogManager.LogError("AddMeeting", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //    }
        //    return Response;
        //}

        //public List<Project.Entity.Meetings> getMeetingsByRelateToID(long PIN, long RelateToID)
        //{
        //    objResponse Response = new objResponse();
        //    List<Project.Entity.Meetings> Meetings = new List<Project.Entity.Meetings>();
        //    try
        //    {
        //        SqlParameter[] sqlParameter = new SqlParameter[2];

        //        sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
        //        sqlParameter[0].Value = PIN;

        //        sqlParameter[1] = new SqlParameter("@RelateToID", SqlDbType.BigInt, 10);
        //        sqlParameter[1].Value = RelateToID;

        //        DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetMeetings", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


        //        if (Response.ResponseData.Tables[0].Rows.Count > 0)
        //        {
        //            Response.ErrorCode = 0;
        //            foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
        //            {
        //                Project.Entity.Meetings objMeeting = new Project.Entity.Meetings();
        //                objMeeting.Meeting_ID_PK = Convert.ToInt64(dr["Meeting_ID_Auto_PK"]);
        //                objMeeting.Title = Convert.ToString(dr["Title"]);
        //                objMeeting.Date = Convert.ToDateTime(dr["Date"]).ToString("d MMM yyyy");
        //                objMeeting.Agenda = Convert.ToString(dr["Agenda"]);
        //                objMeeting.RelateTo = Convert.ToInt64(dr["RelateTo_ID_Fk"]);
        //                objMeeting.RelateToName = Convert.ToString(dr["Name"]);
        //                objMeeting.CreatedBy = Convert.ToString(dr["CreatedBy"]);
        //                objMeeting.CreatedByName = Convert.ToString(dr["CreatedByName"]);
        //                objMeeting.Status = Convert.ToString(dr["Status"]);
        //                objMeeting.Summary = Convert.ToString(dr["Summary"]);

        //                Meetings.Add(objMeeting);
        //            }
        //        }
        //        else
        //        {
        //            Response.ErrorCode = 2001;
        //            Response.ErrorMessage = "There is an Error. Please Try After some time.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.ErrorCode = 3001;
        //        Response.ErrorMessage = ex.Message.ToString();
        //        BAL.Common.LogManager.LogError("getMeetingsByRelateToID", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //    }
        //    return Meetings;
        //}
    }
}
