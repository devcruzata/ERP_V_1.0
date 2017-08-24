using BAL.Transaction;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers.PayPal
{
    public class PayPalController : Controller
    {
        SessionHelper session;
        //
        // GET: /PayPal/

        //public ActionResult RedirectFromPaypal()
        //{
            
        //    objResponse Response = new objResponse();
        //    Transactions objTransaction = new Transactions();
        //    TransactionManager objTransManager = new TransactionManager();
           
        //    try
        //    {
        //        objTransaction.CustomerID = Convert.ToInt64(session.UserSession.Subscription_ID);
        //        objTransaction.Product_ID = session.UserTransactionSession.ProductID;
        //        objTransaction.ProductPrice = session.UserTransactionSession.ProductPrice;
        //        objTransaction.Transaction_Date = BAL.Helper.Helper.ConvertToDateNullable(DateTime.Now.ToString(), "dd/MM/yyyy");
        //        objTransaction.PaymentBy = "PayPal";
        //        objTransaction.Status = "Success";

        //        Response = objTransManager.AddTransaction(objTransaction,session.UserTransactionSession.SubscriptionTYpe, session.UserSession.Username);

        //        if (Response.ErrorCode == 0)
        //        {
        //            TempData["Success_Msg"] = "Success";
        //            return RedirectToRoute("Subscriptions");
        //        }
        //        else
        //        {
        //            TempData["Error_Msg"] = "Error";
        //            return RedirectToRoute("Subscriptions");
        //        }                 
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error_Msg"] = "Error";
        //        return RedirectToRoute("Subscriptions");
        //    }

        //}

        public ActionResult CancelFromPaypal()
        {           
            try
            {                  
                    TempData["Error_Msg"] = "Error";
                    return RedirectToRoute("Subscriptions");
               
            }
            catch (Exception ex)
            {
                TempData["Error_Msg"] = "Error";
                BAL.Common.LogManager.LogError("CancelFromPaypal Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return RedirectToRoute("Subscriptions");
            }

        }
       

        [HttpPost]
        //public ActionResult ValidateCommand(CustomerTransactionModel objModel)
        //{
        //    session = new SessionHelper();
        //    string subType = "";
        //    bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSandbox"]);
        //    var paypal = new Project.Web.Models.PaypalModel(useSandbox);

        //    paypal.item_name = objModel.ProductName.ToString(); 
        //    paypal.amount = objModel.ProductPrice.ToString();
        //    if (objModel.Subs_Type == "12")
        //    {
        //        subType = "Annual";
        //    }
        //    else
        //    {
        //        subType = "Monthly";
        //    }
        //    session.UserTransactionSession = new UserTransactionSession()
        //    {
        //        CustomerID = objModel.CustomerID,
        //        ProductID = objModel.Product_ID,
        //        ProductName = objModel.ProductName,
        //        ProductPrice = objModel.ProductPrice ,
        //        SubscriptionTYpe = subType

        //    };
        //    return View(paypal);
        //}

        //[HttpPost]
        //public ActionResult IPN()
        //{
        //    try
        //    {
        //        session = new SessionHelper();

        //        // Receive IPN request from PayPal and parse all the variables returned
        //        var formVals = new Dictionary<string, string>();
        //        formVals.Add("cmd", "_notify-validate");
        //        formVals.Add("at", "2nSZtD7VFE_per_AcErFj_iriUqfFw4E5N8j5lKru3KDYZLjYO3qp5elBne");
        //        formVals.Add("tx", Request.QueryString.Get("tx"));

        //        // if you want to use the PayPal sandbox change this from false to true
        //        string response = GetPayPalResponse(formVals, true);

        //        if (response.Contains("VERIFIED"))
        //        {
        //            //string transactionID = GetPDTValue(response, "txn_id");
        //           // string sAmountPaid = GetPDTValue(response, "mc_gross");
        //           // string deviceID = GetPDTValue(response, "custom");
        //           // string payerEmail = GetPDTValue(response, "payer_email");
        //           // string Item = GetPDTValue(response, "item_name");

        //            string transactionID = Request["txn_id"];
        //            string sAmountPaid = Request["mc_gross"];
        //            string deviceID = Request["custom"];
        //            //validate the order
        //            Decimal amountPaid = 0;
        //            // Decimal.TryParse(sAmountPaid, out amountPaid);
        //            Decimal.TryParse(sAmountPaid, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out amountPaid);
        //            //if (sAmountPaid == session.UserTransactionSession.ProductPrice.ToString())
        //            if (amountPaid == session.UserTransactionSession.ProductPrice)
        //            {
        //                // take the information returned and store this into a subscription table
        //                // this is where you would update your database with the details of the tran
        //                Transactions objTransaction = new Transactions();
        //                TransactionManager objTransManager = new TransactionManager();
        //                objResponse Response = new objResponse();

        //                try
        //                {
        //                    objTransaction.CustomerID = Convert.ToInt64(session.UserSession.PIN);
        //                    objTransaction.Product_ID = session.UserTransactionSession.ProductID;
        //                    objTransaction.ProductPrice = session.UserTransactionSession.ProductPrice;
        //                    objTransaction.Transaction_ID = transactionID;
        //                    objTransaction.Transaction_Date = BAL.Helper.Helper.ConvertToDateNullable(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy");
        //                    objTransaction.PaymentBy = "PayPal";
        //                    objTransaction.Status = "Success";

        //                    Response = objTransManager.AddTransaction(objTransaction,session.UserTransactionSession.SubscriptionTYpe, session.UserSession.Username);

        //                    if (Response.ErrorCode == 0)
        //                    {
        //                        TempData["Success_Msg"] = "Your Transaction Loged Successfully , For More Details check Your Transaction History.";
        //                    }
        //                    else
        //                    {
        //                        TempData["Error_Msg"] = "There is an error in loging your transaction , For More Details check Your Transaction History.";
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    BAL.Common.LogManager.LogError("PDT  Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //                }
        //                return RedirectToRoute("Subscriptions");
        //            }
        //            else
        //            {
        //                // let fail - this is the PDT so there is no viewer
        //                // you may want to log something here
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        BAL.Common.LogManager.LogError("PDT AddTransaction Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //    }
        //    return View();
        //}

       
        //public ActionResult PDT()
        //{
        //    string authToken, txToken, query;
        //    string strResponse;
        //    string url;
        //    try
        //    {
        //        // Used parts from https://www.paypaltech.com/PDTGen/
        //        // Visit above URL to auto-generate PDT script

        //        authToken = ConfigurationManager.AppSettings["PDTToken"].ToString();

        //        //read in txn token from querystring
        //        txToken = Request.QueryString.Get("tx");


        //        query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, authToken);

        //        // Create the request back
        //        if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsSandbox"]) == true)
        //        {
        //             url = ConfigurationManager.AppSettings["test_url"];
        //        }
        //        else
        //        {
        //             url = ConfigurationManager.AppSettings["Prod_url"];
        //        }
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

        //        // Set values for the request back
        //        req.Method = "POST";
        //        req.ContentType = "application/x-www-form-urlencoded";
        //        req.ContentLength = query.Length;

        //        // Write the request back IPN strings
        //        StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
        //        stOut.Write(query);
        //        stOut.Close();

        //        // Do the request to PayPal and get the response
        //        StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
        //        strResponse = stIn.ReadToEnd();
        //        stIn.Close();                

        //        // If response was SUCCESS, parse response string and output details
        //        if (strResponse.StartsWith("SUCCESS"))
        //        {
        //            PDTHolder pdt = PDTHolder.Parse(strResponse);
        //            Transactions objTransaction = new Transactions();
        //            TransactionManager objTransManager = new TransactionManager();
        //            objResponse Response = new objResponse();
        //            session = new SessionHelper();
        //            try
        //            {
        //                objTransaction.CustomerID = Convert.ToInt64(session.UserSession.PIN);
        //                objTransaction.Product_ID = session.UserTransactionSession.ProductID;
        //                objTransaction.ProductPrice = session.UserTransactionSession.ProductPrice;
        //                objTransaction.Transaction_ID = pdt.TransactionId;
        //                objTransaction.Transaction_Date = DateTime.Now;
        //                objTransaction.PaymentBy = "PayPal";
        //                objTransaction.Status = pdt.PaymentStatus;

        //                if (objTransaction.Status == "Completed")
        //                {
        //                    Response = objTransManager.AddTransaction(objTransaction, session.UserTransactionSession.SubscriptionTYpe, session.UserSession.Username);

        //                    if (Response.ErrorCode == 0)
        //                    {
        //                        if (Response.ErrorMessage != "Transaction ID already Exists")
        //                        {
        //                            TempData["Success_Msg"] = "Success";
        //                        }                                                               
        //                    }
        //                    else
        //                    {
        //                        TempData["Error_Msg"] = "Error";
        //                    }
        //                }
        //                else
        //                {
        //                    TempData["Error_Msg"] = "Error";
        //                }                       
        //            }
        //            catch (Exception ex)
        //            {
        //                BAL.Common.LogManager.LogError("PDT  Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //            }
        //            return RedirectToRoute("Subscriptions");                  
        //        }
        //        else
        //        {                 
        //            TempData["Error_Msg"] = "Error";
        //            return RedirectToRoute("Subscriptions");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error_Msg"] = "Error";
        //        BAL.Common.LogManager.LogError("PDT  Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
        //        return RedirectToRoute("Subscriptions");
        //    }             
        //}



        /// <summary>
        /// Handles the PDT Response from PayPal
        /// </summary>
        /// <returns></returns>
        //public ActionResult PDT()
        //{
        //    //_logger.Info("PDT Invoked");
        //    string transactionID = Request.QueryString["tx"];
        //    string sAmountPaid = Request.QueryString["amt"];
        //    string orderID = Request.QueryString["cm"];


        //    Dictionary<string, string> formVals = new Dictionary<string, string>();
        //    formVals.Add("cmd", "_notify-synch");
        //    formVals.Add("at", SiteData.PayPalPDTToken);
        //    formVals.Add("tx", transactionID);

        //    string response = GetPayPalResponse(formVals, true);
        //    //_logger.Info("PDT Response received: " + response);
        //    if (response.StartsWith("SUCCESS"))
        //    {
        //        //_logger.Info("PDT Response received for order " + orderID);

        //        //validate the order
        //        Decimal amountPaid = 0;
        //        Decimal.TryParse(sAmountPaid, out amountPaid);


        //        Order order = null;

        //        if (AmountPaidIsValid(order, amountPaid))
        //        {


        //            Address add = new Address();
        //            add.FirstName = GetPDTValue(response, "first_name");
        //            add.LastName = GetPDTValue(response, "last_name");
        //            add.Email = GetPDTValue(response, "payer_email");
        //            add.Street1 = GetPDTValue(response, "address_street");
        //            add.City = GetPDTValue(response, "address_city");
        //            add.StateOrProvince = GetPDTValue(response, "address_state");
        //            add.Country = GetPDTValue(response, "address_country");
        //            add.Zip = GetPDTValue(response, "address_zip");
        //            add.UserName = order.UserName;


        //            //process it
        //            try
        //            {
        //                // _pipeline.AcceptPalPayment(order, transactionID, amountPaid);
        //                // _logger.Info("PDT Order successfully transacted: " + orderID);
        //                return RedirectToAction("Receipt", "Order", new { id = order.ID });
        //            }
        //            catch
        //            {
        //                //HandleProcessingError(order, x);
        //                return View();
        //            }

        //        }
        //        else
        //        {
        //            //Payment amount is off
        //            //this can happen if you have a Gift cert at PayPal
        //            //be careful of this!
        //            //HandleProcessingError(order, new InvalidOperationException("Amount paid (" + amountPaid.ToString("C") + ") was below the order total"));
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        ViewData["message"] = "Your payment was not successful with PayPal";
        //        return View();
        //    }
        //}


        /// <summary>
        /// Utility method for handling PayPal Responses
        /// </summary>
        string GetPayPalResponse(Dictionary<string, string> formVals, bool useSandbox)
        {
            string response = "";

            try
            {
                // Parse the variables
                // Choose whether to use sandbox or live environment
                string paypalUrl = useSandbox ? "https://www.sandbox.paypal.com/cgi-bin/webscr" : "https://www.paypal.com/cgi-bin/webscr";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(paypalUrl);

                // Set values for the request back
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";

                byte[] param = Request.BinaryRead(Request.ContentLength);
                string strRequest = Encoding.ASCII.GetString(param);

                StringBuilder sb = new StringBuilder();
                sb.Append(strRequest);

                foreach (string key in formVals.Keys)
                {
                    sb.AppendFormat("&{0}={1}", key, formVals[key]);
                }
                strRequest += sb.ToString();
                req.ContentLength = strRequest.Length;

                //for proxy
                //WebProxy proxy = new WebProxy(new Uri("http://urlort#");
                //req.Proxy = proxy;
                //Send the request to PayPal and get the response
                
                using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII))
                {

                    streamOut.Write(strRequest);
                    streamOut.Close();
                    using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                    {
                        response = streamIn.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetPayPalResponse Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            
 
              return response;
          }

        string GetPDTValue(string pdt, string Key)
        {
            string[] Keys = pdt.Split('\n');
            string thisVal = "";
            string thisKey = "";
            try
            {                
                foreach (string s in Keys)
                {
                    string[] bits = s.Split('=');
                    if (bits.Length > 1)
                    {
                        thisVal = bits[1];
                        thisKey = bits[0];
                        if (thisKey.Equals(Key, StringComparison.InvariantCultureIgnoreCase))
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetPDTValue Conto", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }            
            return thisVal;
        }
    }
}
