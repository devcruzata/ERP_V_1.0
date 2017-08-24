using BAL.Task;
using Project.Entity;
using Project.Web.Common;
using Project.Web.Filters;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Web.Controllers
{
    public class TaskController : Controller
    {
        SessionHelper session;
        TaskManager objTaskManager = new TaskManager();
        //
        // GET: /Task/

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AjaxAddTask(string Titele, string RelateTo, string Description, string notificationFlag,string AssignTo,string RelatedTable)
        {
            objResponse Response = new objResponse();          
            session = new SessionHelper();
           
            try
            {
                if (AssignTo == "0")
                {
                    AssignTo = session.UserSession.UserId.ToString();
                }
                Response = objTaskManager.AddTask(Titele, Convert.ToInt64(RelateTo), Description, notificationFlag, "Planed", session.UserSession.UserId, Convert.ToInt64(session.UserSession.PIN), AssignTo, RelatedTable);

                if (Response.ErrorCode == 0)
                {
                    
                    if (RelatedTable == "LEAD")
                    {
                        LeadsModel objLeadModel = new LeadsModel();
                        objLeadModel.Task = objTaskManager.getTasksByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateTo), session.UserSession.UserId, RelatedTable);
                        return View("AjaxTasks", objLeadModel);
                    }
                    else if (RelatedTable == "OPPORTUNITY")
                    {
                        OpportunityModel objOppoModel = new OpportunityModel();
                        objOppoModel.Task = objTaskManager.getTasksByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateTo), session.UserSession.UserId, RelatedTable);
                        return View("AjaxTasks", objOppoModel);
                    }
                    else
                    {
                        ClientModel objClientModel = new ClientModel(); ;
                        objClientModel.Task = objTaskManager.getTasksByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateTo), session.UserSession.UserId, RelatedTable);
                        return View("AjaxTasks", objClientModel);
                    }
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddTask conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }


        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult DeleteTask(string Task_ID, string RelateTo, string RelatedTable)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();          
            try
            {
                Response = objTaskManager.DeleteTask(Convert.ToInt64(Task_ID));


                if (Response.ErrorCode == 0)
                {
                    if (RelatedTable == "LEAD")
                    {
                        LeadsModel objLeadModel = new LeadsModel();
                        objLeadModel.Task = objTaskManager.getTasksByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateTo), session.UserSession.UserId, RelatedTable);
                        return View("AjaxTasks", objLeadModel);
                    }
                    else if (RelatedTable == "OPPORTUNITY")
                    {
                        OpportunityModel objOppoModel = new OpportunityModel();
                        objOppoModel.Task = objTaskManager.getTasksByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateTo), session.UserSession.UserId, RelatedTable);
                        return View("AjaxTasks", objOppoModel);
                    }
                    else
                    {
                        ClientModel objClientModel = new ClientModel(); ;
                        objClientModel.Task = objTaskManager.getTasksByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateTo), session.UserSession.UserId, RelatedTable);
                        return View("AjaxTasks", objClientModel);
                    }
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddTask conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("", JsonRequestBehavior.AllowGet);
            }

        }

    }
}
