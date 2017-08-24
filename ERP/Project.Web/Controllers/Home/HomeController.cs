using BAL.Home;
using Project.Entity;
using Project.ViewModel;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers
{
    public class HomeController : Controller
    {
        HomeManager objHomeManager = new HomeManager();
        SessionHelper session;

        [Authorize]
        [SessionTimeOut]
        public ActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult AdminDashboard_V_2()
        {
            DashboardModel objDashboardModel = new DashboardModel();
            objResponse Response = new objResponse();
            session = new SessionHelper();
            try
            {
                Response = objHomeManager.GetAdminDashboardData(Convert.ToInt64(session.UserSession.PIN));
                if (Response.ErrorCode == 0)
                {
                    objDashboardModel.TotalLeads = Response.ResponseData.Tables[0].Rows[0][0].ToString();
                    objDashboardModel.TotalDeals = Response.ResponseData.Tables[1].Rows[0][0].ToString();
                    objDashboardModel.TotalClients = Response.ResponseData.Tables[2].Rows[0][0].ToString();
                    objDashboardModel.TotalDealsRevenue = Response.ResponseData.Tables[3].Rows[0][0].ToString();

                    objDashboardModel.LeadsTradition = Response.ResponseData.Tables[4].Rows[0][0].ToString();
                    objDashboardModel.DealsTraditions = Response.ResponseData.Tables[5].Rows[0][0].ToString();
                    objDashboardModel.ClientsTraditions = Response.ResponseData.Tables[6].Rows[0][0].ToString();
                    objDashboardModel.DealsRevenueTraditions = Response.ResponseData.Tables[7].Rows[0][0].ToString();

                    objDashboardModel.LeadsPercentageChange = Response.ResponseData.Tables[8].Rows[0][0].ToString();
                    objDashboardModel.DealsPercentageChange = Response.ResponseData.Tables[9].Rows[0][0].ToString();
                    objDashboardModel.ClientsPercentageChange = Response.ResponseData.Tables[10].Rows[0][0].ToString();
                    objDashboardModel.DealsRevenuePercentageChange= Response.ResponseData.Tables[11].Rows[0][0].ToString();

                    objDashboardModel.TaskCompletedPercentageToday = Response.ResponseData.Tables[12].Rows[0][0].ToString();
                    objDashboardModel.TaskCompletedPercentageYesterday = Response.ResponseData.Tables[13].Rows[0][0].ToString();

                    if(Response.ResponseData.Tables[14].Rows.Count > 0)
                    {
                        foreach (DataRow dr in Response.ResponseData.Tables[14].Rows)
                        {
                          Opportunities objOpp = new Opportunities();
                          objOpp.Source = dr["Source"].ToString();
                          objOpp.RelateTo_Name = dr["RealtedTo"].ToString();
                          objOpp.Amount = dr["Amount"].ToString();
                          objOpp.Stage = dr["Stage"].ToString();
                          objOpp.Opportunity_Owner_Name = dr["Op_owner"].ToString();
                          objOpp.AssignTO_Name = dr["AssignToName"].ToString();

                          objDashboardModel.TopFiveDeals.Add(objOpp);
                       }
                    }

                    if (Response.ResponseData.Tables[15].Rows.Count > 0)
                    {
                        foreach (DataRow dr in Response.ResponseData.Tables[15].Rows)
                        {
                            Source objSource = new Source();
                            objSource.Source_Name = dr["Source_Text"].ToString();
                            objSource.TotalLeads = dr["TotalLeads"].ToString();

                            objDashboardModel.TopThreeSources.Add(objSource);
                        }
                    }

                    if (Response.ResponseData.Tables[16].Rows.Count > 0)
                    {
                        foreach (DataRow dr in Response.ResponseData.Tables[16].Rows)
                        {
                            Project.Entity.Clients objClient = new Project.Entity.Clients();
                            objClient.Client_ID_Auto_PK = Convert.ToInt64(dr["Client_ID_Auto_PK"]);
                            objClient.Name = Convert.ToString(dr["Name"]);
                            objClient.Date = Convert.ToDateTime(dr["Date"]);
                            objClient.Email = Convert.ToString(dr["Email"]);
                            objClient.SkypeNo = Convert.ToString(dr["SkypeNo"]);
                            objClient.Status = Convert.ToString(dr["Status"]);

                            objDashboardModel.Contacts.Add(objClient);
                        }
                    }
                    return View(objDashboardModel);
                }
                else
                {
                    return View(objDashboardModel);
                }
                
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetOpportunityLostByMonth Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return View(objDashboardModel);
            }
            
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult CRM_User_Dashboard()
        {
            return View();
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult SuperAdminDashboard()
        {
            return View();
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult GetClientData()
        {
            objResponse Response = new objResponse();
            List<TextValue> mapData = new List<TextValue>();
            try
            {
                
                mapData = objHomeManager.GetMapData();
                return Json(mapData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetClientData Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(mapData, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult GetOpportunityGraphData()
        {
            objResponse Response = new objResponse();
            List<TextValue> OppoGraphData = new List<TextValue>();            
            try
            {
                OppoGraphData = objHomeManager.GetOpportunityGraphData();
                return Json(OppoGraphData, JsonRequestBehavior.AllowGet);               
                
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetOpportunityGraphData Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(OppoGraphData, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult GetOpportunityRevenueGraphData()
        {
            objResponse Response = new objResponse();
            List<TextValue> OppoRevenueGraphData = new List<TextValue>();
            try
            {
                OppoRevenueGraphData = objHomeManager.GetOpportunityRevenueGraphData();
                return Json(OppoRevenueGraphData, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetOpportunityRevenueGraphData Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(OppoRevenueGraphData, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult GetOpportunityLostByMonth()
        {
            objResponse Response = new objResponse();
            List<TextValue> OptData = new List<TextValue>();
            session = new SessionHelper();
            try
            {
                OptData = objHomeManager.GetOpportunityLostByMonth(Convert.ToInt64(session.UserSession.PIN));
                return Json(OptData, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetOpportunityLostByMonth Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(OptData, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult GetOpportunityWinByMonth()
        {
            objResponse Response = new objResponse();
            List<TextValue> OppoData = new List<TextValue>();
            session = new SessionHelper();
            try
            {
                OppoData = objHomeManager.GetOpportunityWonByMonth(Convert.ToInt64(session.UserSession.PIN));
                return Json(OppoData, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetOpportunityWinByMonth Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(OppoData, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult GetTopThreeSource()
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();
            int totalLeads;
            string top1="";
            string top2 = "";
            string top3 = "";
            string top1Name = "";
            string top2Name = "";
            string top3Name = "";
            
            try
            {
                Response =  objHomeManager.GetTopThreeSources(Convert.ToInt64(session.UserSession.PIN));
                if (Response.ErrorCode == 0)
                {
                    totalLeads = Convert.ToInt32(Response.ResponseData.Tables[0].Rows[0][0]);
                    int count = 0;
                    foreach (DataRow dr in Response.ResponseData.Tables[1].Rows)
                    {
                        count++;
                        if (count == 1)
                        {
                            top1 = ((Convert.ToInt32(dr["TotalLeads"]) * 100) / totalLeads).ToString();
                            top1Name = dr["Source_Text"].ToString();
                        }

                        if (count == 2)
                        {
                            top2 = ((Convert.ToInt32(dr["TotalLeads"]) * 100) / totalLeads).ToString();
                            top2Name = dr["Source_Text"].ToString();
                        }

                        if (count == 3)
                        {
                            top3 = ((Convert.ToInt32(dr["TotalLeads"]) * 100) / totalLeads).ToString();
                            top3Name = dr["Source_Text"].ToString();
                        }

                    }
                    return Json(top1 + "," + top1Name + "," + top2 + "," + top2Name + "," + top3 + "," + top3Name, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(top1 + "," + top1Name + "," +top2 + "," + top2Name + "," + top3+ "," + top3Name, JsonRequestBehavior.AllowGet);
                }
                

            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("GetTopThreeSource Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json(top1 + "," + top1Name + "," + top2 + "," + top2Name + "," + top3 + "," + top3Name, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionTimeOut]
        public ActionResult AdminHome()
        {
            return View();
        }
    }
}
