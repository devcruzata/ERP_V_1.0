using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Events
{
    public class EventManager
    {
        public List<Event> getEventsByRelateToID(long PIN, long LogedUserID, long relateToId, string relationType)
        {
            objResponse Response = new objResponse();
            List<Event> events = new List<Event>();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[4];

                sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = PIN;                

                sqlParameter[1] = new SqlParameter("@LogedUserID", SqlDbType.BigInt, 10);
                sqlParameter[1].Value = LogedUserID;

                sqlParameter[2] = new SqlParameter("@relateToId", SqlDbType.BigInt, 10);
                sqlParameter[2].Value = relateToId;

                sqlParameter[3] = new SqlParameter("@relationType", SqlDbType.NVarChar, 30);
                sqlParameter[3].Value = relationType;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetEventsByRelateToID", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "Success";

                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        Event objEvent = new Event();

                        objEvent.title = dr["Title"].ToString();
                        objEvent.createdDate = dr["StartDate"].ToString();
                        objEvent.id = Convert.ToInt64(dr["Event_ID_Auto_PK"]);

                        events.Add(objEvent);
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
                BAL.Common.LogManager.LogError("getEventsByRelateToID", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return events;
        }

        public objResponse DeleteEvent(long EventID, long RelateToID, string RelatedTable, long LogedUser, long PIN)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[5];

                sqlParameter[0] = new SqlParameter("@EventID", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = EventID;

                sqlParameter[1] = new SqlParameter("@RelateToID", SqlDbType.BigInt, 10);
                sqlParameter[1].Value = RelateToID;

                sqlParameter[2] = new SqlParameter("@RelatedTable", SqlDbType.NVarChar, 20);
                sqlParameter[2].Value = RelatedTable;

                sqlParameter[3] = new SqlParameter("@LogedUser", SqlDbType.BigInt, 10);
                sqlParameter[3].Value = LogedUser;

                sqlParameter[4] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
                sqlParameter[4].Value = PIN;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_DaleteEvents", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
                BAL.Common.LogManager.LogError("DeleteEvent", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }
    }
}
