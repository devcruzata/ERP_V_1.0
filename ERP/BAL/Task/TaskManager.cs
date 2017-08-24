using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Task
{
    public class TaskManager
    {
        public objResponse AddTask(string Title, long Relate_To_ID, string Description, string NotiFicationFlag, string Status, long LogedUser, long PIN, string AssignTo, string RelatedTable)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[10];

                sqlParameter[0] = new SqlParameter("@Title", SqlDbType.NVarChar, 1000);
                sqlParameter[0].Value = Title;                

                sqlParameter[1] = new SqlParameter("@Relate_To_ID", SqlDbType.BigInt, 10);
                sqlParameter[1].Value = Relate_To_ID;

                sqlParameter[2] = new SqlParameter("@Description", SqlDbType.NVarChar, 4000);
                sqlParameter[2].Value = Description;

                sqlParameter[3] = new SqlParameter("@NotiFicationFlag", SqlDbType.NVarChar, 2);
                sqlParameter[3].Value = NotiFicationFlag;                

                sqlParameter[4] = new SqlParameter("@Status", SqlDbType.NVarChar, 3);
                sqlParameter[4].Value = Status;

                sqlParameter[5] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 10);
                sqlParameter[5].Value = LogedUser;

                sqlParameter[6] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 60);
                sqlParameter[6].Value = DateTime.Now;

                sqlParameter[7] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
                sqlParameter[7].Value = PIN;                

                sqlParameter[8] = new SqlParameter("@AssignTo", SqlDbType.BigInt, 10);
                sqlParameter[8].Value = Convert.ToInt64(AssignTo);

                sqlParameter[9] = new SqlParameter("@RelatedTable", SqlDbType.NVarChar, 30);
                sqlParameter[9].Value = RelatedTable;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddTask", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
                BAL.Common.LogManager.LogError("AddTask", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public List<Project.Entity.Tasks> getTasksByRelateToID(long PIN, long RelateToID, long LogedUserID, string RelatedTable)
        {
            objResponse Response = new objResponse();
            List<Project.Entity.Tasks> Task = new List<Project.Entity.Tasks>();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[4];

                sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = PIN;

                sqlParameter[1] = new SqlParameter("@RelateToID", SqlDbType.BigInt, 10);
                sqlParameter[1].Value = RelateToID;

                sqlParameter[2] = new SqlParameter("@LogedUserID", SqlDbType.BigInt, 10);
                sqlParameter[2].Value = LogedUserID;

                sqlParameter[3] = new SqlParameter("@RelatedTable", SqlDbType.NVarChar, 30);
                sqlParameter[3].Value = RelatedTable;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetTasks", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        Project.Entity.Tasks objTask = new Project.Entity.Tasks();
                        objTask.Task_ID = Convert.ToInt64(dr["Task_ID_Auto_PK"]);
                        objTask.Title = Convert.ToString(dr["Title"]);                       
                        objTask.Description = Convert.ToString(dr["Description"]);
                        objTask.RelateTo = Convert.ToInt64(dr["RelateTo_ID"]);
                        objTask.RelateToName = Convert.ToString(dr["Name"]);
                        objTask.CreatedBy = Convert.ToString(dr["CreatedBy"]);
                        objTask.CreatedByName = Convert.ToString(dr["CreatedByName"]);
                        objTask.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]).ToString("d MMM yyyy");
                        objTask.Status = Convert.ToString(dr["Status"]);
                        objTask.AssignTo = Convert.ToString(dr["AssignTo_ID"]);
                        objTask.AssignToName = Convert.ToString(dr["AssignBy"]);
                        Task.Add(objTask);
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
                BAL.Common.LogManager.LogError("getTasksByRelateToID", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Task;
        }

        public objResponse DeleteTask(long TaskID)
        {
            objResponse Response = new objResponse();          
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@TaskID", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = TaskID;

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_DaleteTask", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
                BAL.Common.LogManager.LogError("DeleteTask", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }
    }
}
