using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Event
    {
        public long id { get; set; }

        public long Relation_ID_Fk { get; set; }

        public string RelationType { get; set; }

        public string title { get; set; }

        public string start { get; set; }

        public string end { get; set; }

        public string Description { get; set; }

        public bool allDay { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedByName { get; set; }

        public string createdDate { get; set; }

        public string Status { get; set; }

        public string RelateTo { get; set; }

        public long RelateToID { get; set; }
    }
}
