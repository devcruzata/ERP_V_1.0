using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class ClientModel
    {
        public long Client_ID { get; set; }

        public string Date { get; set; }        

        public string Name { get; set; }

        public string JobDescription { get; set; }

        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string Alternate_Email { get; set; }

        public string ContactNo { get; set; }

        public string SkypeNo { get; set; }       

        public string Model { get; set; }

        public string Source { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }         

        public string Status { get; set; }

        public long Client_Owner { get; set; }

        public string Client_Owner_Name { get; set; }

        public long AssignTO_ID { get; set; }

        public string AssignTO_Name { get; set; }

        public List<Project.Entity.Clients> clients { get; set; }

        //public List<Meetings> meetings { get; set; }

        //public List<Tasks> tasks { get; set; }

        public List<Activity> activities { get; set; }

        public List<Opportunities> Opportunity { get; set; }

        //public List<Notes> notes { get; set; }

        //public List<Docs> doc { get; set; }

        //public List<Event> vents { get; set; }

        public List<Notes> Notes { get; set; }

        public List<Event> Events { get; set; }

        public List<Activity> Activity { get; set; }

        public List<Docs> Doc { get; set; }

        public List<Tasks> Task { get; set; }
    }
}