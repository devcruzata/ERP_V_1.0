using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.SubscriptionHolder
{
    public class SubscriptionHolderManager
    {
        public List<Users> GetSubscriptionHolders()
        {
            objResponse Response = new objResponse();
            List<Users> users = new List<Users>();

            try
            {
                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetSubscriptionHolder", DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;

                    foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                    {
                        Users objUser = new Users();

                        objUser.User_ID_PK = Convert.ToInt64(dr["User_ID_Auto_PK"]);                                             
                        objUser.FullName = dr["User_FirstName"].ToString() + " " + dr["User_LastName"].ToString();
                        objUser.Email = dr["User_Email"].ToString();
                        objUser.Mobile = dr["User_Contact"].ToString();
                        objUser.Subscription = dr["Name"].ToString();
                        objUser.Date = Convert.ToDateTime(dr["CreatedDate"]);
                        objUser.Status = dr["Status"].ToString();

                        users.Add(objUser);
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
                BAL.Common.LogManager.LogError("getSubscriptionHolder", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return users;
        }

        public string DeleteSubscriptionHolder(Int64 User_ID_PK)
        {
            objResponse response = new objResponse();
            DataTable dt = new DataTable();
            string result = " ";

            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@User_ID_PK", SqlDbType.BigInt, 0);
                sqlParameter[0].Value = User_ID_PK;

                DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteSubscriptionHolder", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

                dt = response.ResponseData.Tables[0];

                if (dt.Rows.Count > 0)
                {
                    response.ErrorCode = 0;
                    response.ErrorMessage = "Sucess";
                    result = Convert.ToString(dt.Rows[0][0]);
                }
            }

            catch (Exception ex)
            {
                response.ErrorCode = 2001;
                response.ErrorMessage = "Error While Processing" + ex.Message;
                BAL.Common.LogManager.LogError("Delete Subscription Holder", 1, Convert.ToString(ex.Message), Convert.ToString(ex.Source), Convert.ToString(ex.StackTrace));

            }
            return result;
        }
    }
}