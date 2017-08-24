using DAL;
using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Setings
{
   public class SetingManager
    {
       public objResponse getGenralSeting(long CustomerID)
       {
           objResponse Response = new objResponse();           
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@CustomerID", SqlDbType.BigInt, 50);
               sqlParameter[0].Value = CustomerID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetGenralSetings", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("getGenralSeting", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse AddCompanyProfile(GenralSeting objSetings,long logedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[12];

               sqlParameter[0] = new SqlParameter("@CustomerID", SqlDbType.BigInt, 50);
               sqlParameter[0].Value = objSetings.Customer_ID;

               sqlParameter[1] = new SqlParameter("@Company", SqlDbType.NVarChar, 80);
               sqlParameter[1].Value = objSetings.Company;

               sqlParameter[2] = new SqlParameter("@Address", SqlDbType.NVarChar, 200);
               sqlParameter[2].Value = objSetings.Address;

               sqlParameter[3] = new SqlParameter("@City", SqlDbType.NVarChar, 100);
               sqlParameter[3].Value = objSetings.City;

               sqlParameter[4] = new SqlParameter("@State", SqlDbType.NVarChar, 80);
               sqlParameter[4].Value = objSetings.Stete;

               sqlParameter[5] = new SqlParameter("@Country", SqlDbType.NVarChar, 20);
               sqlParameter[5].Value = objSetings.Country;

               sqlParameter[6] = new SqlParameter("@Zipcode", SqlDbType.NVarChar, 20);
               sqlParameter[6].Value = objSetings.Zipcode;

               sqlParameter[7] = new SqlParameter("@Phone", SqlDbType.NVarChar, 20);
               sqlParameter[7].Value = objSetings.Phone;

               sqlParameter[8] = new SqlParameter("@Website", SqlDbType.NVarChar, 50);
               sqlParameter[8].Value = objSetings.Website;

               sqlParameter[9] = new SqlParameter("@Currency", SqlDbType.NVarChar, 20);
               sqlParameter[9].Value = objSetings.Currency;

               sqlParameter[10] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 10);
               sqlParameter[10].Value = logedUser;

               sqlParameter[11] = new SqlParameter("@CreatedDate", SqlDbType.DateTime, 60);
               sqlParameter[11].Value = DateTime.Now;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddCompanyProfile", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("AddCompanyProfile", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public  List<TextValue> GetSourceForSetings(long CustomerID)
       {

           objResponse Response = new objResponse();
           List<TextValue> Source = new List<TextValue>();

           try
           {

               SqlParameter[] sqlParameter = new SqlParameter[1];

               sqlParameter[0] = new SqlParameter("@CustomerID", SqlDbType.BigInt, 50);
               sqlParameter[0].Value = CustomerID;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetSource", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = dr["Source_ID_PK"].ToString();
                       objText.Text = dr["Source_Text"].ToString();
                       Source.Add(objText);
                   }
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetSourceForStings", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Source;
       }

       public objResponse ManageDroplistOption(string Option , long OptionID,string Module , string DropName ,long CustomerID, long logedUser)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[6];

               sqlParameter[0] = new SqlParameter("@CustomerID", SqlDbType.BigInt, 50);
               sqlParameter[0].Value = CustomerID;

               sqlParameter[1] = new SqlParameter("@Option", SqlDbType.NVarChar, 100);
               sqlParameter[1].Value = Option;

               sqlParameter[2] = new SqlParameter("@OptionID", SqlDbType.BigInt, 10);
               sqlParameter[2].Value = OptionID;

               sqlParameter[3] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 10);
               sqlParameter[3].Value = logedUser;               

               sqlParameter[4] = new SqlParameter("@Module", SqlDbType.NVarChar, 50);
               sqlParameter[4].Value = Module;

               sqlParameter[5] = new SqlParameter("@DropName", SqlDbType.NVarChar, 50);
               sqlParameter[5].Value = DropName;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddDropListOption", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


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
               BAL.Common.LogManager.LogError("ManageDroplistOptio", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public string DeleteDropListOption(long OptionID,string Module , string DropName, long CustomerID)
       {
           objResponse Response = new objResponse();
           string Result = "";
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[4];

               sqlParameter[0] = new SqlParameter("@CustomerID", SqlDbType.BigInt, 50);
               sqlParameter[0].Value = CustomerID;               

               sqlParameter[1] = new SqlParameter("@OptionID", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = OptionID;

               sqlParameter[2] = new SqlParameter("@Module", SqlDbType.NVarChar, 50);
               sqlParameter[2].Value = Module;

               sqlParameter[3] = new SqlParameter("@DropName", SqlDbType.NVarChar, 50);
               sqlParameter[3].Value = DropName;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_DeleteDropListOption", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);


               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = "Success";
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
               BAL.Common.LogManager.LogError("DeleteDropListOption", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Result;
       }

       public List<TextValue> GetDropDownListing(string Module, string Dropdownlistname, long CustomerID)
       {

           objResponse Response = new objResponse();
           List<TextValue> Source = new List<TextValue>();

           try
           {

               SqlParameter[] sqlParameter = new SqlParameter[3];

               sqlParameter[0] = new SqlParameter("@CustomerID", SqlDbType.BigInt, 50);
               sqlParameter[0].Value = CustomerID;

               sqlParameter[1] = new SqlParameter("@Module", SqlDbType.NVarChar, 50);
               sqlParameter[1].Value = Module;

               sqlParameter[2] = new SqlParameter("@DropName", SqlDbType.NVarChar, 80);
               sqlParameter[2].Value = Dropdownlistname;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetDropdownListing", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                   {
                       TextValue objText = new TextValue();
                       objText.Value = dr["DropDown_Option_ID_Auto_Pk"].ToString();
                       objText.Text = dr["Opation_Text"].ToString();
                       Source.Add(objText);
                   }
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetDropDownListing", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Source;
       }

       public objResponse AddMailSetings(string hosttype, string hosturl, string port, string username, string password, bool issssl, long logedUser, long PIN, long msetting_Id, string settingtype)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[10];

               sqlParameter[0] = new SqlParameter("@logedUser", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = logedUser;

               sqlParameter[1] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = PIN;

               sqlParameter[2] = new SqlParameter("@isssl", SqlDbType.Bit, 1);
               sqlParameter[2].Value = issssl;

               sqlParameter[3] = new SqlParameter("@hosttype", SqlDbType.NVarChar, 30);
               sqlParameter[3].Value = hosttype;

               sqlParameter[4] = new SqlParameter("@hosturl", SqlDbType.NVarChar, 50);
               sqlParameter[4].Value = hosturl;

               sqlParameter[5] = new SqlParameter("@username", SqlDbType.NVarChar, 100);
               sqlParameter[5].Value = username;

               sqlParameter[6] = new SqlParameter("@password", SqlDbType.NVarChar, 50);
               sqlParameter[6].Value = password;

               sqlParameter[7] = new SqlParameter("@port", SqlDbType.NVarChar, 10);
               sqlParameter[7].Value = port;

               sqlParameter[8] = new SqlParameter("@msetting_Id", SqlDbType.BigInt, 10);
               sqlParameter[8].Value = msetting_Id;

               sqlParameter[9] = new SqlParameter("@settingtype", SqlDbType.NVarChar, 30);
               sqlParameter[9].Value = settingtype;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ManageMailSetting", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetDropDownListing", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

       public objResponse GetEmailSetings( long logedUser, long PIN)
       {
           objResponse Response = new objResponse();
           try
           {
               SqlParameter[] sqlParameter = new SqlParameter[2];

               sqlParameter[0] = new SqlParameter("@logedUser", SqlDbType.BigInt, 10);
               sqlParameter[0].Value = logedUser;

               sqlParameter[1] = new SqlParameter("@PIN", SqlDbType.BigInt, 10);
               sqlParameter[1].Value = PIN;

               DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetMailSetting", sqlParameter, DB_CONSTANTS.ConnectionString_ERP_CRUZATA);

               if (Response.ResponseData.Tables[0].Rows.Count > 0)
               {
                   Response.ErrorCode = 0;
                   Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
               }
           }
           catch (Exception ex)
           {
               Response.ErrorCode = 2001;
               BAL.Common.LogManager.LogError("GetEmailSetings", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
           }
           return Response;
       }

    }
}
