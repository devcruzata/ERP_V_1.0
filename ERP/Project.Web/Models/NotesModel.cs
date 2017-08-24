using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class NotesModel
    {
        public long Note_ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string DateTaken { get; set; }

        public long RelatedLead_ID { get; set; }

        public string RelatedLead_Name { get; set; }

        public long RelatedOpportunity_ID { get; set; }

        public string RelatedOpportunity_Name { get; set; }

        public long RelatedContact_ID { get; set; }

        public string RelatedContact_Name { get; set; }

        public long Note_Owner_ID { get; set; }

        public long Note_Owner_Name { get; set; }

        public List<Notes> notes { get; set; }

    }
}