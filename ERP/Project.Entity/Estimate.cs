using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public  class Estimate
    {
        public long Estimate_ID_Auto_PK { get; set; }

        public long Lead_ID_Fk { get; set; }

        public long Client_ID_Fk { get; set; }

        public DateTime Date { get; set; }

        public Int64 Category { get; set; }

        public Int32 Language { get; set; }

        public long AssignTo { get; set; }

        public string Requirment { get; set; }

        public string Status { get; set; }

        public string CategoryName { get; set; }

        public string LanguageName { get; set; }

        public string Assigne { get; set; }

        public string Priority { get; set; }

        public string Lead { get; set; }

        public string Client { get; set; }
    }
}
