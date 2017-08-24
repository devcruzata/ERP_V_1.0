using DAL;
using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BAL.Calender
{
   public class CalenderManager
    {
        public List<Project.Entity.Calender> LoadAllAppointmentsInDateRange(string start, string end)
        {
            objResponse Response = new objResponse();
            //var fromDate = ConvertFromUnixTimestamp(start);
           // var toDate = ConvertFromUnixTimestamp(end);
            List<Project.Entity.Calender> result = new List<Project.Entity.Calender>();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[2];

                sqlParameter[0] = new SqlParameter("@fromDate", SqlDbType.DateTime, 100);
                sqlParameter[0].Value = Convert.ToDateTime(start);

                sqlParameter[1] = new SqlParameter("@toDate", SqlDbType.DateTime, 100);
                sqlParameter[1].Value = Convert.ToDateTime(end);

                //sqlParameter[2] = new SqlParameter("@LogedUser", SqlDbType.BigInt, 10);
                //sqlParameter[2].Value = LogedUser;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetTasksByDateRange", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "Success";
                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        Project.Entity.Calender rec = new Project.Entity.Calender();
                        rec.ID = Convert.ToInt32(dr["Task_ID_Auto_PK"]);
                        rec.EventOwner = Convert.ToInt32(dr["CreatedByName"]);
                        rec.StartDateString = Convert.ToDateTime(dr["StartDate"]).ToString("s"); //item.DateTimeScheduled.ToString("s"); // "s" is a preset format that outputs as: "2009-02-27T12:12:22"
                        rec.EndDateString = Convert.ToDateTime(dr["EndDate"]).ToString("s"); //item.DateTimeScheduled.AddMinutes(item.AppointmentLength).ToString("s"); // field AppointmentLength is in minutes
                        rec.Title = dr["Title"].ToString();                                                  //item.Title + " - " + item.AppointmentLength.ToString() + " mins";
                       // rec.StatusString = Enums.GetName<AppointmentStatus>((AppointmentStatus)item.StatusENUM);
                       // rec.StatusColor = Enums.GetEnumDescription<AppointmentStatus>(rec.StatusString);
                        //string ColorCode = rec.StatusColor.Substring(0, rec.StatusColor.IndexOf(":"));
                       // rec.ClassName = rec.StatusColor.Substring(rec.StatusColor.IndexOf(":") + 1, rec.StatusColor.Length - ColorCode.Length - 1);
                       // rec.StatusColor = ColorCode;
                        result.Add(rec);
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
                BAL.Common.LogManager.LogError("LoadAllAppointmentsInDateRange calmanager", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
                return result;            

        }


        public  List<Project.Entity.Calender> LoadAppointmentSummaryInDateRange(string start, string end)
        {

            objResponse Response = new objResponse();
           // var fromDate = ConvertFromUnixTimestamp(start);
           // var toDate = ConvertFromUnixTimestamp(end);
            List<Project.Entity.Calender> result = new List<Project.Entity.Calender>();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[2];

                sqlParameter[0] = new SqlParameter("@start", SqlDbType.DateTime, 100);
                sqlParameter[0].Value = Convert.ToDateTime(start);

                sqlParameter[1] = new SqlParameter("@end", SqlDbType.DateTime, 100);
                sqlParameter[1].Value = Convert.ToDateTime(end); 

                //sqlParameter[2] = new SqlParameter("@LogedUser", SqlDbType.BigInt, 10);
                //sqlParameter[2].Value = LogedUser;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetEvent", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "Success";
                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        Project.Entity.Calender rec = new Project.Entity.Calender();
                        rec.ID = Convert.ToInt32(dr["Event_ID_Auto_PK"]);
                        rec.EventColor = Convert.ToString(dr["Colour"]);
                        rec.StartDateString = Convert.ToDateTime(dr["StartDate"]).ToString("yyyy/MM/dd hh:mm tt");
                        rec.EndDateString = Convert.ToDateTime(dr["EndDate"]).ToString("yyyy/MM/dd hh:mm tt");
                        rec.Title = dr["Title"].ToString();                                                  
                        
                        result.Add(rec);
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
                BAL.Common.LogManager.LogError("LoadAppointmentSummaryInDateRange calmanager", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return result;            

        }

        public objResponse GetEventInfo(int EventId)
        {

            objResponse Response = new objResponse();
            string result = "";
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@Event_ID", SqlDbType.Int, 100);
                sqlParameter[0].Value = EventId;



                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetEventInfoById", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "Success";
                   // result = Response.ResponseData.Tables[0].Rows[0][0].ToString();
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
                BAL.Common.LogManager.LogError("GetEventInfo calmanager", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;

        }

        //public static void UpdateDiaryEvent(int id, string NewEventStart, string NewEventEnd)
        //{
        //    // EventStart comes ISO 8601 format, eg:  "2000-01-10T10:00:00Z" - need to convert to DateTime
        //    using (DiaryContainer ent = new DiaryContainer())
        //    {
        //        var rec = ent.AppointmentDiary.FirstOrDefault(s => s.ID == id);
        //        if (rec != null)
        //        {
        //            DateTime DateTimeStart = DateTime.Parse(NewEventStart, null, DateTimeStyles.RoundtripKind).ToLocalTime(); // and convert offset to localtime
        //            rec.DateTimeScheduled = DateTimeStart;
        //            if (!String.IsNullOrEmpty(NewEventEnd))
        //            {
        //                TimeSpan span = DateTime.Parse(NewEventEnd, null, DateTimeStyles.RoundtripKind).ToLocalTime() - DateTimeStart;
        //                rec.AppointmentLength = Convert.ToInt32(span.TotalMinutes);
        //            }
        //            ent.SaveChanges();
        //        }
        //    }

        //}


        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        //public static bool CreateNewEvent(string Title, string StartDate,string StartTime,string EndDate,string EndTime)
        //{
        //    try
        //    {
        //        DiaryContainer ent = new DiaryContainer();
        //        AppointmentDiary rec = new AppointmentDiary();
        //        rec.Title = Title;
        //        rec.DateTimeScheduled = DateTime.ParseExact(NewEventDate + " " + NewEventTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
        //        rec.AppointmentLength = Int32.Parse(NewEventDuration);
        //        ent.AppointmentDiary.Add(rec);
        //        ent.SaveChanges();


        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //    return true;
        //}


        public objResponse AddNewEvent(string Title, DateTime StartDate, DateTime EndDate,string eColor ,string RelatedTo,string RelatedTable,long CreatedBy,long PIN)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[8];

                sqlParameter[0] = new SqlParameter("@Title", SqlDbType.NVarChar, 1000);
                sqlParameter[0].Value = Title;

                sqlParameter[1] = new SqlParameter("@StartDate", SqlDbType.DateTime, 60);
                sqlParameter[1].Value = StartDate; 

                sqlParameter[2] = new SqlParameter("@EndDate", SqlDbType.DateTime, 60);
                sqlParameter[2].Value = EndDate;

                sqlParameter[3] = new SqlParameter("@RelatedTo", SqlDbType.BigInt, 16);
                sqlParameter[3].Value = Convert.ToInt64(RelatedTo);                

                sqlParameter[4] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 16);
                sqlParameter[4].Value = CreatedBy;

                sqlParameter[5] = new SqlParameter("@RelatedTable", SqlDbType.NVarChar, 50);
                sqlParameter[5].Value = RelatedTable;

                sqlParameter[6] = new SqlParameter("@PIN", SqlDbType.BigInt, 16);
                sqlParameter[6].Value = PIN;

                sqlParameter[7] = new SqlParameter("@eColor", SqlDbType.NVarChar, 50);
                sqlParameter[7].Value = eColor;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddNewEventUpdated", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
                BAL.Common.LogManager.LogError("AddNewEvent", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }


        public objResponse UpdateEventDate(int Eventid, DateTime StartDate, DateTime EndDate, long CreatedBY,long PIN)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[5];

                sqlParameter[0] = new SqlParameter("@Eventid", SqlDbType.Int, 10);
                sqlParameter[0].Value = Eventid;

                sqlParameter[1] = new SqlParameter("@StartDate", SqlDbType.DateTime, 60);
                sqlParameter[1].Value = StartDate;

                sqlParameter[2] = new SqlParameter("@EndDate", SqlDbType.DateTime, 60);
                sqlParameter[2].Value = EndDate;                

                sqlParameter[3] = new SqlParameter("@CreatedBY", SqlDbType.BigInt, 20);
                sqlParameter[3].Value = CreatedBY;

                sqlParameter[4] = new SqlParameter("@PIN", SqlDbType.BigInt, 20);
                sqlParameter[4].Value = PIN;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateEventDate", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
                BAL.Common.LogManager.LogError("UpdateEventDate", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public objResponse UpdateEventType1(int EventId, string Title, DateTime StartDate, DateTime EndDate,string eColour ,string RelateTo,string RelatedTable,long CreatedBy,long PIN)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[9];

                sqlParameter[0] = new SqlParameter("@Title", SqlDbType.NVarChar, 1000);
                sqlParameter[0].Value = Title;

                sqlParameter[1] = new SqlParameter("@StartDate", SqlDbType.DateTime, 60);
                sqlParameter[1].Value = StartDate;

                sqlParameter[2] = new SqlParameter("@EndDate", SqlDbType.DateTime, 60);
                sqlParameter[2].Value = EndDate;

                sqlParameter[3] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 16);
                sqlParameter[3].Value = CreatedBy;                

                sqlParameter[4] = new SqlParameter("@EventId", SqlDbType.Int, 10);
                sqlParameter[4].Value = EventId;

                sqlParameter[5] = new SqlParameter("@RelatedTo", SqlDbType.BigInt, 16);
                sqlParameter[5].Value = Convert.ToInt64(RelateTo);               

                sqlParameter[6] = new SqlParameter("@RelatedTable", SqlDbType.NVarChar, 50);
                sqlParameter[6].Value = RelatedTable;

                sqlParameter[7] = new SqlParameter("@PIN", SqlDbType.BigInt, 16);
                sqlParameter[7].Value = PIN;

                sqlParameter[8] = new SqlParameter("@eColour", SqlDbType.NVarChar, 50);
                sqlParameter[8].Value = eColour;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateEvent1", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
                BAL.Common.LogManager.LogError("UpdateEventType1", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public objResponse UpdateEventType2(int EventId,string Title,string EColor, string Description)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[4];

                sqlParameter[0] = new SqlParameter("@Title", SqlDbType.NVarChar, 1000);
                sqlParameter[0].Value = Title;                

                sqlParameter[1] = new SqlParameter("@EColor", SqlDbType.NVarChar, 16);
                sqlParameter[1].Value = EColor;

                sqlParameter[2] = new SqlParameter("@Description", SqlDbType.NVarChar, 200);
                sqlParameter[2].Value = Description;

                sqlParameter[3] = new SqlParameter("@EventId", SqlDbType.Int, 10);
                sqlParameter[3].Value = EventId;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateEvent2", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
                BAL.Common.LogManager.LogError("UpdateEventType2", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public objResponse DeleteEvent(int EventId)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];                

                sqlParameter[0] = new SqlParameter("@EventId", SqlDbType.Int, 10);
                sqlParameter[0].Value = EventId;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_DeleteEvent", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
                BAL.Common.LogManager.LogError("DeleteEvent", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public List<TextValue> GetLeadForCalender()
        {
            objResponse Response = new objResponse();
            List<TextValue> leadList = new List<TextValue>();
            try
            {
                

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetLeadForCalender",  DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "success";
                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        TextValue leads = new TextValue();
                        leads.Text = dr["DBA"].ToString();
                        leads.Value = dr["Lead_ID_Auto_PK"].ToString();
                        //leads.Add(string.Format("{0}-{1}", dr["DBA"], dr["Lead_ID_Auto_PK"]));
                        //leadList.Add(string.Format("{0}/{1}", dr["DBA"].ToString(), dr["Lead_ID_Auto_PK"].ToString()));   
                        leadList.Add(leads);
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
                BAL.Common.LogManager.LogError("GetLeadForCalender", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return leadList;
        }

        
    }
}
