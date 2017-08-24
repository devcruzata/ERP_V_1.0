using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class StatusModel
    {
        public long Status_ID { get; set; }

        public long CustomerID { get; set; }

        public long Created_By { get; set; }

        public string Created_Date { get; set; }

        public long Updated_By { get; set; }

        public string Updated_Date { get; set; }

        public string Status_Text { get; set; }

        public string Status_Value { get; set; }

        public List<StatusModel> status { get; set; }
    }
}