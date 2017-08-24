using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Docs
    {
        public long Doc_ID_Auto_PK { get; set; }

        public string Title { get; set; }

        public string FileName { get; set; }

        public string FileID { get; set; }

        public string RelateToLead_ID { get; set; }

        public string RelateToLead_Name { get; set; }

        public string RelateToContact_ID { get; set; }

        public string RelateToContact_Name { get; set; }

        public string RelateToOpportunity_ID { get; set; }

        public string RelateToOpportunity_Name { get; set; }

        public long DocOwner_ID { get; set; }

        public string DocOwner_Name { get; set; }

        public DateTime UploadedDate { get; set; }
    }
}
