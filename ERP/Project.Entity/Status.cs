using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
  public class Status
    {
        public long Status_ID { get; set; }

        public long CustomerID { get; set; }

        public long Created_By { get; set; }

        public string Created_Date { get; set; }

        public long Updated_By { get; set; }

        public string Updated_Date { get; set; }
    }
}
