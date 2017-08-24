using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class LeadsModel
    {
        public long Lead_ID { get; set; }

        public string Date { get; set; }        

        public string FollowUpDate { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string Alternate_Email { get; set; }

        public string ContactNo { get; set; }

        public string SkypeNo { get; set; }

        public long Category { get; set; }

        public string CategoryName { get; set; }

        public string Model { get; set; }

        public string Source { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }

        public string Requirement { get; set; }

        public string Remarks { get; set; }

        public HttpPostedFileBase[] UploadedDoc { get; set; }

        public string UploadedDocName { get; set; }

        public List<Project.Entity.Leads> leads { get; set; }

        public string Status { get; set; }

        public List<Notes> Notes { get; set; }

        public List<Event> Events { get; set; }

        public List<Activity> Activity { get; set; }

        public List<Docs> Doc { get; set; }

        public List<Tasks> Task { get; set; }

        public List<Mails> mails { get; set; }

        public string AssignTo { get; set; }

        public string JobDescription { get; set; }

        public string CreatedBy { get; set; }

        public LeadsModel()
        {
            leads = new List<Leads>();
            Notes = new List<Notes>();
        }
    }

    
}