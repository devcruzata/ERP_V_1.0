using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class OpportunityModel
    {
        public long Opportunity_ID { get; set; }

        public long RealateTo_ID { get; set; }

        public string RelateTo_Name { get; set; }

        public string RelateTo_ContactNo { get; set; }

        public string RelateTo_Email { get; set; }

        public string Name { get; set; }

        public string ExpectedCloseDate {get;set;}

        public long Opportunity_Owner { get; set; }

        public string Opportunity_Owner_Name { get; set; }

        public long AssignTO_ID { get; set; }

        public string AssignTO_Name { get; set; }

        public string Type { get; set; }

        public string Stage { get; set; }

        public string Probability { get; set; }

        public string Source { get; set; }

        public string Amount { get; set; }

        public string LostReason { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        //public List<Meetings> meetings { get; set; }

        //public List<Tasks> tasks { get; set; }

        public List<Activity> activities { get; set; }

        public List<Opportunities> Opportunity { get; set; }

        //public List<Notes> notes { get; set; }

        //public List<Docs> docs { get; set; }

        //public List<Event> events { get; set; }

        public List<Notes> Notes { get; set; }

        public List<Event> Events { get; set; }

        public List<Activity> Activity { get; set; }

        public List<Docs> Doc { get; set; }

        public List<Tasks> Task { get; set; }
    }
    
}