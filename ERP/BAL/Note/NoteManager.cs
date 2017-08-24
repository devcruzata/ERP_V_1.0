using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Note
{
   public class NoteManager
    {
       public objResponse AddNote(long Relate_To_ID, string Note, long LogedUser, long PIN,string RelatedTable)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[6];                        

                sqlParameter[0] = new SqlParameter("@Relate_To_ID", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = Relate_To_ID;

                sqlParameter[1] = new SqlParameter("@Note", SqlDbType.NVarChar, 4000);
                sqlParameter[1].Value = Note;                

                sqlParameter[2] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 10);
                sqlParameter[2].Value = LogedUser;

                sqlParameter[3] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 60);
                sqlParameter[3].Value = DateTime.Now;
                                                       
                sqlParameter[4] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
                sqlParameter[4].Value = PIN;               

                sqlParameter[5] = new SqlParameter("@RelatedTable", SqlDbType.NVarChar, 100);
                sqlParameter[5].Value = RelatedTable;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddNote", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
                BAL.Common.LogManager.LogError("AddNote", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

       public List<Project.Entity.Notes> getNotesByRelateToID(long PIN, long RelateToID, long LoagedUSerID, string RelatedTable)
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Notes> notes = new List<Project.Entity.Notes>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[4];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = PIN;

               sqlParameter[1] = new SqlParameter("@RelateToID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = RelateToID;

               sqlParameter[2] = new SqlParameter("@LoagedUSerID", SqlDbType.BigInt, 10);
               sqlParameter[2].Value = LoagedUSerID;

               sqlParameter[3] = new SqlParameter("@RelatedTable", SqlDbType.NVarChar, 20);
               sqlParameter[3].Value = RelatedTable;



               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetNotes", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.Entity.Notes objNote = new Project.Entity.Notes();
                       objNote.Note_ID = Convert.ToInt64(dr["Notes_ID_Auto_PK"]);                      
                       objNote.Description = Convert.ToString(dr["Note"]);
                       if (dr["RelatedTable"].ToString() == "LEAD")
                       {
                           objNote.RelatedLead_ID = Convert.ToInt64(dr["Refrence_ID_FK"]);
                           objNote.RelatedLead_Name = Convert.ToString(dr["Name"]);
                       }
                       else if (dr["RelatedTable"].ToString() == "OPPORUNITY")
                       {
                           objNote.RelatedOpportunity_ID = Convert.ToInt64(dr["Refrence_ID_FK"]);
                           objNote.RelatedOpportunity_Name = Convert.ToString(dr["Name"]);
                       }
                       else
                       {
                           objNote.RelatedContact_ID = Convert.ToInt64(dr["Refrence_ID_FK"]);
                           objNote.RelatedContact_Name = Convert.ToString(dr["Name"]);
                       }

                       objNote.Note_Owner_ID = Convert.ToString(dr["CreatedBy"]);
                       objNote.Note_Owner_Name = Convert.ToString(dr["CreatedByName"]);
                       objNote.DateTaken = Convert.ToDateTime(dr["CreatedDate"]).ToString("d MMM yyyy");

                       notes.Add(objNote);
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
           return notes;
       }

        //public List<Project.Entity.Meetings> getAllMeetings(long PIN)
        //{
        //    objResponse Response = new objResponse();
        //    List<Project.Entity.Meetings> Meetings = new List<Project.Entity.Meetings>();
        //    try
        //    {
        //        SqlParameter[] sqlParameter = new SqlParameter[1];

        //        sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
        //        sqlParameter[0].Value = PIN;

        //        DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetMeetingsByPin", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
        //        BAL.Common.LogManager.LogError("getAllMeetings", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //    }
        //    return Meetings;
        //}

       public objResponse DeleteNotes(long NotesID , long RelateToID , string RelatedTable,long LogedUser,long PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[5];

               sqlParameter[0] = new SqlParameter("@NotesID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = NotesID;

               sqlParameter[1] = new SqlParameter("@RelateToID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = RelateToID;

               sqlParameter[2] = new SqlParameter("@RelatedTable", SqlDbType.NVarChar, 20);
               sqlParameter[2].Value = RelatedTable;

               sqlParameter[3] = new SqlParameter("@LogedUser", SqlDbType.BigInt, 10);
               sqlParameter[3].Value = LogedUser;

               sqlParameter[4] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[4].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_DaleteNotes", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("DeleteNotes", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }
    }
}
