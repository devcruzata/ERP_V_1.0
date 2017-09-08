using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Roles
{
   public class RolesManager
    {
       public objResponse AddRole(UserRoles objRoles, long PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[15];

               sqlParameter[0] = new SqlParameter("@AssociatedLeads", SqlDbType.NVarChar, 5);
               sqlParameter[0].Value = objRoles.AssociatedLeads;

               sqlParameter[1] = new SqlParameter("@SystemwideLeads", SqlDbType.NVarChar, 5);
               sqlParameter[1].Value = objRoles.SystemwideLeads;

               sqlParameter[2] = new SqlParameter("@AssociatedOpportunity", SqlDbType.NVarChar, 5);
               sqlParameter[2].Value = objRoles.AssociatedOpportunity;

               sqlParameter[3] = new SqlParameter("@SystemwideOpportunity", SqlDbType.NVarChar, 5);
               sqlParameter[3].Value = objRoles.SystemwideOpportunity;

               sqlParameter[4] = new SqlParameter("@AssociatedClients", SqlDbType.NVarChar, 5);
               sqlParameter[4].Value = objRoles.AssociatedClients;

               sqlParameter[5] = new SqlParameter("@SystemwideClients", SqlDbType.NVarChar, 5);
               sqlParameter[5].Value = objRoles.SystemwideClients;

               sqlParameter[6] = new SqlParameter("@Task", SqlDbType.NVarChar, 5);
               sqlParameter[6].Value = objRoles.Task;

               sqlParameter[7] = new SqlParameter("@Notes", SqlDbType.NVarChar, 5);
               sqlParameter[7].Value = objRoles.Notes;

               sqlParameter[8] = new SqlParameter("@Documents", SqlDbType.NVarChar, 5);
               sqlParameter[8].Value = objRoles.Documents;         

               sqlParameter[9] = new SqlParameter("@LeadDistribution", SqlDbType.NVarChar, 5);
               sqlParameter[9].Value = objRoles.LeadDistribution;             

               sqlParameter[10] = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
               sqlParameter[10].Value = objRoles.RoleName;

               sqlParameter[11] = new SqlParameter("@Calendar", SqlDbType.NVarChar, 5);
               sqlParameter[11].Value = objRoles.Calendar;              

               sqlParameter[12] = new SqlParameter("@UserManagement", SqlDbType.NVarChar, 5);
               sqlParameter[12].Value = objRoles.UserManagement;

               sqlParameter[13] = new SqlParameter("@SiteManagement", SqlDbType.NVarChar, 5);
               sqlParameter[13].Value = objRoles.SiteManagement;

               sqlParameter[14] = new SqlParameter("@PIN", SqlDbType.BigInt, 5);
               sqlParameter[14].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddUserRole", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("AddRole", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public List<UserRoles> GetAllRoles()
       {
           objResponse Response = new objResponse();
           List<UserRoles> roles = new List<UserRoles>();
           try
           {
               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetRoles", DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = "Success";

                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       UserRoles objUserRoles = new UserRoles();
                       objUserRoles.RoleName = dr["User_Role_Desc"].ToString();
                       objUserRoles.Role_ID = Convert.ToInt64(dr["User_Role_ID_Auto_Pk"]);

                       roles.Add(objUserRoles);
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
               BAL.Common.LogManager.LogError("GetAllRoles", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return roles;
       }

       public objResponse GetRolesForEdit(long RoleID)
       {
           objResponse Response = new objResponse();           
           try
           {

               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@RoleID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = RoleID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetRolesForEdit", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               Response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("GetAllRoles", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse UpdateRole(UserRoles objRoles)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[15];

               sqlParameter[0] = new SqlParameter("@AssociatedLeads", SqlDbType.NVarChar, 5);
               sqlParameter[0].Value = objRoles.AssociatedLeads;

               sqlParameter[1] = new SqlParameter("@SystemwideLeads", SqlDbType.NVarChar, 5);
               sqlParameter[1].Value = objRoles.SystemwideLeads;

               sqlParameter[2] = new SqlParameter("@AssociatedOpportunity", SqlDbType.NVarChar, 5);
               sqlParameter[2].Value = objRoles.AssociatedOpportunity;

               sqlParameter[3] = new SqlParameter("@SystemwideOpportunity", SqlDbType.NVarChar, 5);
               sqlParameter[3].Value = objRoles.SystemwideOpportunity;

               sqlParameter[4] = new SqlParameter("@AssociatedClients", SqlDbType.NVarChar, 5);
               sqlParameter[4].Value = objRoles.AssociatedClients;

               sqlParameter[5] = new SqlParameter("@SystemwideClients", SqlDbType.NVarChar, 5);
               sqlParameter[5].Value = objRoles.SystemwideClients;

               sqlParameter[6] = new SqlParameter("@Task", SqlDbType.NVarChar, 5);
               sqlParameter[6].Value = objRoles.Task;

               sqlParameter[7] = new SqlParameter("@Notes", SqlDbType.NVarChar, 5);
               sqlParameter[7].Value = objRoles.Notes;

               sqlParameter[8] = new SqlParameter("@Documents", SqlDbType.NVarChar, 5);
               sqlParameter[8].Value = objRoles.Documents;

               sqlParameter[9] = new SqlParameter("@LeadDistribution", SqlDbType.NVarChar, 5);
               sqlParameter[9].Value = objRoles.LeadDistribution;

               sqlParameter[10] = new SqlParameter("@RoleName", SqlDbType.NVarChar, 50);
               sqlParameter[10].Value = objRoles.RoleName;

               sqlParameter[11] = new SqlParameter("@Calendar", SqlDbType.NVarChar, 5);
               sqlParameter[11].Value = objRoles.Calendar;

               sqlParameter[12] = new SqlParameter("@UserManagement", SqlDbType.NVarChar, 5);
               sqlParameter[12].Value = objRoles.UserManagement;

               sqlParameter[13] = new SqlParameter("@SiteManagement", SqlDbType.NVarChar, 5);
               sqlParameter[13].Value = objRoles.SiteManagement;

               sqlParameter[14] = new SqlParameter("@Role_ID", SqlDbType.BigInt, 10);
               sqlParameter[14].Value = objRoles.Role_ID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_UpdateUserRole", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("UpdateRole", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse DeleteUserRole(Int64 Role_ID)
       {
           objResponse response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@Role_ID", SqlDbType.BigInt, 0);
               sqlParameter[0].Value = Role_ID;

               DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteUserRole", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   response.ErrorCode = 0;
                   response.ErrorMessage = response.ResponseData.Tables[0].Rows[0][0].ToString();
               }
               else
               {
                   response.ErrorCode = 2001;
                   response.ErrorMessage = "There is an Error. Please Try After some time.";
               }
           }
           catch(Exception ex)
           {
               response.ErrorMessage = ex.Message.ToString();
               BAL.Common.LogManager.LogError("GetAllRoles", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));

           }
           return response;
       }
    }
}
