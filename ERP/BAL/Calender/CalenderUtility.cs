using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace BAL.Calender
{

    /// <summary>
    /// CalenderUtility class is the main class which interacts with the database. SQL Server express edition
    /// has been used.
    /// the event information is stored in a table named 'event' in the database.
    ///
    /// Here is the table format:
    /// event(event_id int, title varchar(100), description varchar(200),event_start datetime, event_end datetime)
    /// event_id is the primary key
    /// </summary>
    public class CalenderUtility
    {
        //this method retrieves all events within range start-end
        //public static List<CalendarEvent> getEvents(DateTime start, DateTime end , long Owner_ID)
        //{
        //    objResponse Response = new objResponse();
        //    List<CalendarEvent> events = new List<CalendarEvent>();
        //    try
        //    {
        //        SqlParameter[] sqlParameter = new SqlParameter[3];

        //        sqlParameter[0] = new SqlParameter("@start", SqlDbType.DateTime, 100);
        //        sqlParameter[0].Value = start;

        //        sqlParameter[1] = new SqlParameter("@end", SqlDbType.DateTime, 100);
        //        sqlParameter[1].Value = end;

        //        sqlParameter[2] = new SqlParameter("@Owner_ID", SqlDbType.BigInt, 100);
        //        sqlParameter[2].Value = Owner_ID;


        //        DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetEventByOwnerID", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


        //        if (Response.ResponseData.Tables[0].Rows.Count > 0)
        //        {
        //            Response.ErrorCode = 0;
        //            Response.ErrorMessage = "success";

        //            foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
        //            {
        //                CalendarEvent cevent = new CalendarEvent();
        //                cevent.id = Convert.ToInt32(dr["Event_ID_Auto_PK"]);
        //                cevent.title = Convert.ToString(dr["title"]);
        //                cevent.description = Convert.ToString(dr["description"]);
        //                cevent.start = Convert.ToDateTime(dr["event_start"]);
        //                cevent.end = Convert.ToDateTime(dr["event_end"]);
        //                cevent.allDay = Convert.ToBoolean(dr["All_Day"]);
        //                events.Add(cevent);
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
        //        BAL.Common.LogManager.LogError("getEvents", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //    }
        //    return events;
        //}

        //this method updates the event title and description
        public static void updateEvent(int id, String title, String description)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[3];

                sqlParameter[0] = new SqlParameter("@title", SqlDbType.NVarChar, 100);
                sqlParameter[0].Value = title;

                sqlParameter[1] = new SqlParameter("@description", SqlDbType.NVarChar, 300);
                sqlParameter[1].Value = description;

                sqlParameter[2] = new SqlParameter("@event_id", SqlDbType.Int, 100);
                sqlParameter[2].Value = id;


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateEventTitleAndDesc", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
            catch(Exception ex)
            {
                Response.ErrorCode = 3001;
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("updateEvent", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
        }

        //this method updates the event start and end time ... allDay parameter added for FullCalendar 2.x
        //public static void updateEventTime(int id, DateTime start, DateTime end, bool allDay)
        //{
        //    objResponse Response = new objResponse();
        //    List<CalendarEvent> events = new List<CalendarEvent>();
        //    try
        //    {
        //        SqlParameter[] sqlParameter = new SqlParameter[4];

        //        sqlParameter[0] = new SqlParameter("@event_start", SqlDbType.DateTime, 100);
        //        sqlParameter[0].Value = start;

        //        sqlParameter[1] = new SqlParameter("@event_end", SqlDbType.DateTime, 100);
        //        sqlParameter[1].Value = end;

        //        sqlParameter[2] = new SqlParameter("@event_id", SqlDbType.Int, 100);
        //        sqlParameter[2].Value = id;

        //        sqlParameter[3] = new SqlParameter("@all_day", SqlDbType.Bit, 1);
        //        sqlParameter[3].Value = allDay;


        //        DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateEventTime", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


        //        if (Response.ResponseData.Tables[0].Rows.Count > 0)
        //        {
        //            Response.ErrorCode = 0;
        //            Response.ErrorMessage = "success";                    
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
        //        BAL.Common.LogManager.LogError("updateEventTime", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //    }
        //}

        //this mehtod deletes event with the id passed in.
        public static void deleteEvent(int id)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@event_id", SqlDbType.Int, 10);
                sqlParameter[0].Value = id;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_DeleteEvent", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

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
                BAL.Common.LogManager.LogError("deleteEvent", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
        }

        //this method adds events to the database
        //public static int addEvent(CalendarEvent cevent,long OwnerID)
        //{
        //    objResponse Response = new objResponse();
        //    int key = 0;
        //    try
        //    {
        //        SqlParameter[] sqlParameter = new SqlParameter[6];

        //        sqlParameter[0] = new SqlParameter("@title", SqlDbType.NVarChar, 100);
        //        sqlParameter[0].Value = cevent.title;

        //        sqlParameter[1] = new SqlParameter("@description", SqlDbType.NVarChar, 300);
        //        sqlParameter[1].Value = cevent.description;

        //        sqlParameter[2] = new SqlParameter("@event_start", SqlDbType.DateTime, 100);
        //        sqlParameter[2].Value = cevent.start;

        //        sqlParameter[3] = new SqlParameter("@event_end", SqlDbType.DateTime, 100);
        //        sqlParameter[3].Value = cevent.end;

        //        sqlParameter[4] = new SqlParameter("@all_day", SqlDbType.Bit, 1);
        //        sqlParameter[4].Value = cevent.allDay;

        //        sqlParameter[5] = new SqlParameter("@Owner_ID", SqlDbType.BigInt, 10);
        //        sqlParameter[5].Value = OwnerID;

        //        DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddNewEvent", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

        //        if (Response.ResponseData.Tables[0].Rows.Count > 0)
        //        {
        //            Response.ErrorCode = 0;
        //            Response.ErrorMessage = "success";
        //            key = Convert.ToInt32(Response.ResponseData.Tables[0].Rows[0][0]);
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
        //        BAL.Common.LogManager.LogError("addEvent", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //    }
        //    return key;
        //}
    }
}
