using BAL.Note;
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
    public class NotesController : Controller
    {
        NoteManager objNoteManager = new NoteManager();
        SessionHelper session;
        //
        // GET: /Notes/

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult AjaxAddNote(string RelateTo, string Note,string RelatedTable )
        {
            objResponse Response = new objResponse();          
            session = new SessionHelper();
            try
            {
                Response = objNoteManager.AddNote(Convert.ToInt64(RelateTo), Note, session.UserSession.UserId, Convert.ToInt64(session.UserSession.PIN), RelatedTable);

                if (Response.ErrorCode == 0)
                {
                    if (RelatedTable == "LEAD")
                    {
                        LeadsModel objLeadModel = new LeadsModel();
                        objLeadModel.Notes = objNoteManager.getNotesByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateTo), session.UserSession.UserId, RelatedTable);
                        return View("AjaxLeadNotes", objLeadModel);
                    }
                    else if (RelatedTable == "OPPORTUNITY")
                    {
                        OpportunityModel objOppoModel = new OpportunityModel();
                        objOppoModel.Notes = objNoteManager.getNotesByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateTo), session.UserSession.UserId, RelatedTable);
                        return View("AjaxOppoNotes", objOppoModel);
                    }
                    else
                    {
                        ClientModel objClientModel = new ClientModel();
                        objClientModel.Notes = objNoteManager.getNotesByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateTo), session.UserSession.UserId, RelatedTable);
                        return View("AjaxClientNote", objClientModel);
                    }

                    
                }
                else
                {
                    return Json("Fail", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("AjaxAddNote conto Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
                return Json("Fail", JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]
        [SessionTimeOut]
        [HttpPost]
        public ActionResult DeleteNotes(string Notes_ID, string RelatedTable , string RelateToID)
        {
            objResponse Response = new objResponse();
            session = new SessionHelper();          
            try
            {
                Response = objNoteManager.DeleteNotes(Convert.ToInt64(Notes_ID), Convert.ToInt64(RelateToID),RelatedTable,Convert.ToInt64(session.UserSession.UserId),Convert.ToInt64(session.UserSession.PIN));


                if (Response.ErrorCode == 0)
                {
                    if (RelatedTable == "LEAD")
                    {
                        LeadsModel objLeadModel = new LeadsModel();
                        objLeadModel.Notes = objNoteManager.getNotesByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateToID), session.UserSession.UserId,"LEAD");
                        return View("AjaxLeadNotes", objLeadModel);
                    }
                    else if (RelatedTable == "OPPORTUNITY")
                    {
                        OpportunityModel objOppoModel = new OpportunityModel();
                        objOppoModel.Notes = objNoteManager.getNotesByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateToID), session.UserSession.UserId, RelatedTable);
                        return View("AjaxOppoNotes", objOppoModel);
                    }
                    else
                    {
                        ClientModel objClientModel = new ClientModel();
                        objClientModel.Notes = objNoteManager.getNotesByRelateToID(Convert.ToInt64(session.UserSession.PIN), Convert.ToInt64(RelateToID), session.UserSession.UserId, RelatedTable);
                        return View("AjaxClientNote", objClientModel);
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
