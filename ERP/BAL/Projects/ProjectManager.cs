using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Projects
{
   public class ProjectManager
    {
       public objResponse AddProject(Project.Entity.Projects objProject, string LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[9];

               sqlParameter[0] = new SqlParameter("@Date", SqlDbType.Date, 50);
               sqlParameter[0].Value = objProject.Date;

               sqlParameter[1] = new SqlParameter("@Titel", SqlDbType.NVarChar, 200);
               sqlParameter[1].Value = objProject.Title;

               sqlParameter[2] = new SqlParameter("@Client_ID", SqlDbType.BigInt, 8);
               sqlParameter[2].Value = objProject.Client_ID;

               sqlParameter[3] = new SqlParameter("@Model", SqlDbType.NVarChar, 50);
               sqlParameter[3].Value = objProject.Model;              

               sqlParameter[4] = new SqlParameter("@Category_ID", SqlDbType.NVarChar, 30);
               sqlParameter[4].Value = objProject.Category_ID;               

               sqlParameter[5] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[5].Value = LogedUser;

               sqlParameter[6] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[6].Value = DateTime.Now;

               sqlParameter[7] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[7].Value = "NEW";

               sqlParameter[8] = new SqlParameter("@Note", SqlDbType.NVarChar, 4000);
               sqlParameter[8].Value = objProject.Note;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddProject", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("addProject", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse AddSOW(long project_id,string task,string description , string hours , decimal price, string LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[8];

               sqlParameter[0] = new SqlParameter("@Project_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = project_id;

               sqlParameter[1] = new SqlParameter("@task", SqlDbType.NVarChar, 500);
               sqlParameter[1].Value = task;

               sqlParameter[2] = new SqlParameter("@description", SqlDbType.NVarChar, 1000);
               sqlParameter[2].Value = description;

               sqlParameter[3] = new SqlParameter("@hours", SqlDbType.NVarChar, 5);
               sqlParameter[3].Value = hours;

               sqlParameter[4] = new SqlParameter("@price", SqlDbType.Decimal, 20);
               sqlParameter[4].Value = price;
               
               sqlParameter[5] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[5].Value = LogedUser;

               sqlParameter[6] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[6].Value = DateTime.Now;

               sqlParameter[7] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[7].Value = "Active";



               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddSowForProject", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("AddSOW", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse AddPaymentData(long project_id, string dat, string upfront, decimal remaining, string LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[7];

               sqlParameter[0] = new SqlParameter("@Project_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = project_id;

               sqlParameter[1] = new SqlParameter("@dat", SqlDbType.Date, 50);
               sqlParameter[1].Value = BAL.Helper.Helper.ConvertToDateNullable(dat,"dd/MM/yyyy");

               sqlParameter[2] = new SqlParameter("@upfront", SqlDbType.NVarChar, 20);
               sqlParameter[2].Value = upfront;               

               sqlParameter[3] = new SqlParameter("@remaining", SqlDbType.Decimal, 20);
               sqlParameter[3].Value = remaining;

               sqlParameter[4] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[4].Value = LogedUser;

               sqlParameter[5] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[5].Value = DateTime.Now;

               sqlParameter[6] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[6].Value = "NEW";



               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddPaymentDataForProject", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("AddPaymentData", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public List<Project.Entity.Projects> getProjects()
       {
           objResponse Response = new objResponse();
           List<Project.Entity.Projects> projects = new List<Project.Entity.Projects>();
           try
           {
               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetProjects", DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.Entity.Projects objProjects = new Project.Entity.Projects();
                       objProjects.Project_ID_PK = Convert.ToInt64(dr["Project_ID_Auto_PK"]);
                       objProjects.Title = Convert.ToString(dr["Title"]);
                       objProjects.Date = Convert.ToDateTime(dr["Date"]);
                       objProjects.CategoryName = Convert.ToString(dr["CategoryName"]);
                       objProjects.ClientName = Convert.ToString(dr["Name"]);
                       objProjects.Model = Convert.ToString(dr["Model"]);
                       objProjects.Status = Convert.ToString(dr["Status"]);
                       objProjects.ProjectCost = Convert.ToString(dr["ProjectCost"]);
                       if(Convert.ToString(dr["ClientSign"]) != "")
                       {
                           objProjects.isSigned = true;
                       }
                       else
                       {
                           objProjects.isSigned = false;
                       }
                       projects.Add(objProjects);
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
               BAL.Common.LogManager.LogError("getProject", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return projects;
       }

       public objResponse getClientDetailByProject(long Project_ID)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Project_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Project_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetClientDetailByProject", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
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
               BAL.Common.LogManager.LogError("getClientByProject", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public List<Project.ViewModel.ProjectSowViewModel> getProjectSow(long Project_ID)
       {
           objResponse Response = new objResponse();
           List<Project.ViewModel.ProjectSowViewModel> projectSow = new List<Project.ViewModel.ProjectSowViewModel>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Project_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Project_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetProjectSow", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);              


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.ViewModel.ProjectSowViewModel objProjectSow = new Project.ViewModel.ProjectSowViewModel();
                       objProjectSow.Task = Convert.ToString(dr["Task"]);
                       objProjectSow.Description = Convert.ToString(dr["Description"]);
                       objProjectSow.Hours = Convert.ToString(dr["Hours"]);
                       objProjectSow.Price = Convert.ToString(dr["Price"]);


                       projectSow.Add(objProjectSow);
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
               BAL.Common.LogManager.LogError("getProjectSow", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return projectSow;
       }       

       public List<Project.ViewModel.ProjectPaymentDataViewModel> getProjectPaymentData(long Project_ID)
       {
           objResponse Response = new objResponse();
           List<Project.ViewModel.ProjectPaymentDataViewModel> projectPaymentData = new List<Project.ViewModel.ProjectPaymentDataViewModel>();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Project_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Project_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetProjectPaymentData", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       Project.ViewModel.ProjectPaymentDataViewModel objProjectPaymentData = new Project.ViewModel.ProjectPaymentDataViewModel();
                       objProjectPaymentData.DueDate = Convert.ToDateTime(dr["Date"]).ToString("d MMM yyyy");
                       objProjectPaymentData.Upfront = Convert.ToString(dr["Upfront"]);
                       objProjectPaymentData.Remaining = Convert.ToString(dr["Remaining"]);



                       projectPaymentData.Add(objProjectPaymentData);
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
               BAL.Common.LogManager.LogError("getProjectPaymentData", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return projectPaymentData;
       }

       public objResponse AddSignToProject( string Project_ID ,string Sign)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@Project_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Convert.ToInt64(Project_ID);

               sqlParameter[1] = new SqlParameter("@Sign", SqlDbType.NVarChar, 4000);
               sqlParameter[1].Value = Sign;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddSign", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = "Success";
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
               BAL.Common.LogManager.LogError("AddSignToProject", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public string GetSign(long Project_ID)
       {
           string sign = "";
           objResponse Response = new objResponse();

           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Project_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Project_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetProjectSign", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   sign = Convert.ToString(Response.ResponseData.Tables[0].Rows[0]["ClientSign"]);                     
                  
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
               BAL.Common.LogManager.LogError("GetSign BAL", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return sign;
       }

       public objResponse GetProjectInfo(long Project_ID)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Project_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Project_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetProjectInfo", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
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
               BAL.Common.LogManager.LogError("GetProjectInfo", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse ClearSowForProject(long Project_ID)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Project_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Project_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ClearSowForProject", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

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
               BAL.Common.LogManager.LogError("ClearSowForProject", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse UpdateSOW(long project_id, string task, string description, string hours, decimal price,string notes ,string LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[9];

               sqlParameter[0] = new SqlParameter("@Project_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = project_id;

               sqlParameter[1] = new SqlParameter("@task", SqlDbType.NVarChar, 500);
               sqlParameter[1].Value = task;

               sqlParameter[2] = new SqlParameter("@description", SqlDbType.NVarChar, 1000);
               sqlParameter[2].Value = description;

               sqlParameter[3] = new SqlParameter("@hours", SqlDbType.NVarChar, 5);
               sqlParameter[3].Value = hours;

               sqlParameter[4] = new SqlParameter("@price", SqlDbType.Decimal, 20);
               sqlParameter[4].Value = price;

               sqlParameter[5] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[5].Value = LogedUser;

               sqlParameter[6] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[6].Value = DateTime.Now;

               sqlParameter[7] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[7].Value = "Active";

               sqlParameter[8] = new SqlParameter("@Notes", SqlDbType.NVarChar, 4000);
               sqlParameter[8].Value = notes;



               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateSowForProject", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("UpdateSOW", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public string DeleteProject(long Project_ID)
       {
           objResponse Response = new objResponse();
           string Result = "";
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Project_ID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = Project_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_DeleteProject", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Result = Response.ResponseData.Tables[0].Rows[0][0].ToString();
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
               BAL.Common.LogManager.LogError("DeleteProject", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Result;
       }

       public objResponse UpdateProject(Project.Entity.Projects objProjects, string LogedUser, string Field)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[9];

               sqlParameter[0] = new SqlParameter("@Date", SqlDbType.Date, 50);
               sqlParameter[0].Value = objProjects.Date;

               sqlParameter[1] = new SqlParameter("@Title", SqlDbType.NVarChar, 200);
               sqlParameter[1].Value = objProjects.Title;

               sqlParameter[2] = new SqlParameter("@Client_ID", SqlDbType.BigInt, 10);
               sqlParameter[2].Value = objProjects.Client_ID;

               sqlParameter[3] = new SqlParameter("@Model", SqlDbType.NVarChar, 30);
               sqlParameter[3].Value = objProjects.Model;

               sqlParameter[4] = new SqlParameter("@Category_ID", SqlDbType.BigInt, 10);
               sqlParameter[4].Value = objProjects.Category_ID;

               sqlParameter[5] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[5].Value = LogedUser;

               sqlParameter[6] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[6].Value = DateTime.Now;

               sqlParameter[7] = new SqlParameter("@Project_ID_PK", SqlDbType.BigInt, 10);
               sqlParameter[7].Value = objProjects.Project_ID_PK;

               sqlParameter[8] = new SqlParameter("@Field", SqlDbType.NVarChar, 100);
               sqlParameter[8].Value = Field;



               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateEProject", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("UpdateProject", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }
    }
}
