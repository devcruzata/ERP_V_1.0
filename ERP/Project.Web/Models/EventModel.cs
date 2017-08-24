using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class EventModel
    {
        public long EventID { get; set; }

        public string Relation_ID_Fk { get; set; }

        public string RelationType { get; set; }

        public string Title { get; set; }

        public string sDate { get; set; }

        public string eDate { get; set; }

        public string Description { get; set; }

        public bool isAllDayEvent { get; set; }

        public string CreatedBy { get; set; }

        public string Status { get; set; }

        public string RelatedToName { get; set; }

        public string RelatedToContact { get; set; }

        public string RelatedToEmail { get; set; }

        public string EventStartDate { get; set; }

        public string EventStartTime { get; set; }

        public string EventEndDate { get; set; }

        public string EventEndTime { get; set; }

        public string colour { get; set; }

        public List<Event> events {get;set;}
    }
}