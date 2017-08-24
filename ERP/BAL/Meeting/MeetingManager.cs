using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Meeting
{
  public  class MeetingManager
    {
      //public objResponse AddMeeting(string Title, DateTime start,DateTime end,long Relate_To_ID,string Agenda ,string RemindMe ,string Status,string LogedUser,long PIN,long OwnerID)
      //{
      //    objResponse Response = new objResponse();
      //    try
      //    {
      //        SqlParameter[] sqlParameter = new SqlParameter[12];

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

      //        sqlParameter[11] = new SqlParameter("OwnerID", SqlDbType.BigInt, 10);
      //        sqlParameter[11].Value = OwnerID;

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

      public List<Project.Entity.Meetings> getMeetingsByRelateToID(long PIN, long RelateToID , long LoagedUSerID)
      {
          objResponse Response = new objResponse();
          List<Project.Entity.Meetings> Meetings = new List<Project.Entity.Meetings>();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[3];

              sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
              sqlParameter[0].Value = PIN;

              sqlParameter[1] = new SqlParameter("@RelateToID", SqlDbType.BigInt, 10);
              sqlParameter[1].Value = RelateToID;

              sqlParameter[2] = new SqlParameter("@LoagedUSerID", SqlDbType.BigInt, 10);
              sqlParameter[2].Value = LoagedUSerID;

              

              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetMeetings", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


              if (Response.ResponseData.Tables[0].Rows.Count > 0)
              {
                  Response.ErrorCode = 0;
                  foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                  {
                      Project.Entity.Meetings objMeeting = new Project.Entity.Meetings();
                      objMeeting.Meeting_ID_PK = Convert.ToInt64(dr["Meeting_ID_Auto_PK"]);
                      objMeeting.Title = Convert.ToString(dr["Title"]);
                      objMeeting.Date = Convert.ToDateTime(dr["Date"]).ToString("d MMM yyyy");
                      objMeeting.Agenda = Convert.ToString(dr["Agenda"]);
                      objMeeting.RelateTo = Convert.ToInt64(dr["RelateTo_ID_Fk"]);
                      objMeeting.RelateToName = Convert.ToString(dr["Name"]);
                      objMeeting.CreatedBy = Convert.ToString(dr["CreatedBy"]);
                      objMeeting.CreatedByName = Convert.ToString(dr["CreatedByName"]);
                      objMeeting.Status = Convert.ToString(dr["Status"]);
                      objMeeting.Summary = Convert.ToString(dr["Summary"]);

                      Meetings.Add(objMeeting);
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
              BAL.Common.LogManager.LogError("getMeetingsByRelateToID", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }
          return Meetings;
      }

      public List<Project.Entity.Meetings> getAllMeetings(long PIN)
      {
          objResponse Response = new objResponse();
          List<Project.Entity.Meetings> Meetings = new List<Project.Entity.Meetings>();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[1];

              sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
              sqlParameter[0].Value = PIN;

              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetMeetingsByPin", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


              if (Response.ResponseData.Tables[0].Rows.Count > 0)
              {
                  Response.ErrorCode = 0;
                  foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                  {
                      Project.Entity.Meetings objMeeting = new Project.Entity.Meetings();
                      objMeeting.Meeting_ID_PK = Convert.ToInt64(dr["Meeting_ID_Auto_PK"]);
                      objMeeting.Title = Convert.ToString(dr["Title"]);
                      objMeeting.Date = Convert.ToDateTime(dr["Date"]).ToString("d MMM yyyy");
                      objMeeting.Agenda = Convert.ToString(dr["Agenda"]);
                      objMeeting.RelateTo = Convert.ToInt64(dr["RelateTo_ID_Fk"]);
                      objMeeting.RelateToName = Convert.ToString(dr["Name"]);
                      objMeeting.CreatedBy = Convert.ToString(dr["CreatedBy"]);
                      objMeeting.CreatedByName = Convert.ToString(dr["CreatedByName"]);
                      objMeeting.Status = Convert.ToString(dr["Status"]);
                      objMeeting.Summary = Convert.ToString(dr["Summary"]);

                      Meetings.Add(objMeeting);
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
              BAL.Common.LogManager.LogError("getAllMeetings", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }
          return Meetings;
      }
    }
}
