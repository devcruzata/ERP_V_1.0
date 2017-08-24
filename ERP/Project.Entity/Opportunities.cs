using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Opportunities
    {
        public long Opportunity_ID { get; set; }

        public long RealateTo_ID { get; set; }

        public string RelateTo_Name { get; set; }

        public string Name { get; set; }

        public DateTime ExpectedCloseDate { get; set; }

        public long Opportunity_Owner { get; set; }

        public string Opportunity_Owner_Name { get; set; }

        public long AssignTO_ID { get; set; }

        public string AssignTO_Name { get; set; }

        public string Type { get; set; }

        public string stageId { get; set; }

        public string Stage { get; set; }

        public string Probability { get; set; }

        public string Source { get; set; }

        public string Amount { get; set; }

        public string LostReason { get; set; }

        public string Status { get; set; }

        public string Description { get; set; }
    }
}
