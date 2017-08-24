using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Transaction
{
   public class TransactionManager
    {
       public objResponse AddTransaction(Project.Entity.Transactions objTransaction,string Trans_Type ,string LogedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[10];

               sqlParameter[0] = new SqlParameter("@CustomerID", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = objTransaction.CustomerID;

               sqlParameter[1] = new SqlParameter("@Product_ID", SqlDbType.Int, 10);
               sqlParameter[1].Value = objTransaction.Product_ID;

               sqlParameter[2] = new SqlParameter("@ProductPrice", SqlDbType.Decimal, 20);
               sqlParameter[2].Value = objTransaction.ProductPrice;

               sqlParameter[3] = new SqlParameter("@Transaction_Date", SqlDbType.DateTime, 50);
               sqlParameter[3].Value = objTransaction.Transaction_Date;               

               sqlParameter[4] = new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 60);
               sqlParameter[4].Value = LogedUser;

               sqlParameter[5] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 50);
               sqlParameter[5].Value = DateTime.Now;

               sqlParameter[6] = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
               sqlParameter[6].Value = objTransaction.Status;

               sqlParameter[7] = new SqlParameter("@PaymentBy", SqlDbType.NVarChar, 40);
               sqlParameter[7].Value = objTransaction.PaymentBy;

               sqlParameter[8] = new SqlParameter("@Payment_Trans_ID", SqlDbType.NVarChar, 70);
               sqlParameter[8].Value = objTransaction.Transaction_ID;

               sqlParameter[9] = new SqlParameter("@Trans_Type", SqlDbType.NVarChar, 30);
               sqlParameter[9].Value = Trans_Type;


               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddTransaction", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("AddTransaction", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }
    }
}
