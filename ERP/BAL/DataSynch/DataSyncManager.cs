using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.DataSynch
{
   public class DataSyncManager
    {
       public objResponse SetGContactSeting(string access_token, string token_type, Int32 expires_in, string refresh_token,string reqType ,long PIN, long logedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[7];

               sqlParameter[0] = new SqlParameter("@PIN", SqlDbType.BigInt, 50);
               sqlParameter[0].Value = PIN;

               sqlParameter[1] = new SqlParameter("@access_token", SqlDbType.NVarChar, 100);
               sqlParameter[1].Value = access_token;

               sqlParameter[2] = new SqlParameter("@token_type", SqlDbType.NVarChar, 100);
               sqlParameter[2].Value = token_type;

               sqlParameter[3] = new SqlParameter("@UserIDFk", SqlDbType.BigInt, 10);
               sqlParameter[3].Value = logedUser;

               sqlParameter[4] = new SqlParameter("@expires_in", SqlDbType.Int, 20);
               sqlParameter[4].Value = expires_in;

               sqlParameter[5] = new SqlParameter("@refresh_token", SqlDbType.NVarChar, 100);
               sqlParameter[5].Value = refresh_token;

               sqlParameter[6] = new SqlParameter("@reqType", SqlDbType.NVarChar, 40);
               sqlParameter[6].Value = reqType;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_addDataSyncSeting", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("SetGContactSeting", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

      
    }
}
