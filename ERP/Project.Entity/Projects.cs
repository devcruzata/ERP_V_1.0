using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Projects
    {
        public long Project_ID_PK { get; set; }

        public string ProjectID { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public long Client_ID { get; set; }

        public long Estimation_ID { get; set; }

        public long Category_ID { get; set; }

        public Int32 Language_ID { get; set; }

        public long Dev_Team_ID { get; set; }

        public long Sales_Team_ID { get; set; }

        public string Model { get; set; }

        public List<string> Task { get; set; }

        public List<string> Description { get; set; }

        public List<string> Hours { get; set; }

        public List<string> Price { get; set; }

        public string Note { get; set; }

        public string ClientName { get; set; }

        public string CategoryName { get; set; }

        public string Status { get; set; }

        public string ProjectCost { get; set; }

        public List<string> PayMent_Date { get; set; }

        public List<string> PayMent_Upfront { get; set; }

        public List<string> PayMent_Remaining { get; set; }

        public bool isSigned { get; set; }
    }
}
