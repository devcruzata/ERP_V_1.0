using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class SourceModel
    {
        public Int32 SourceID { get; set; }

        public string Source_Name { get; set; }

        public string Option { get; set; }

        public long CustomerID { get; set; }

        public long Created_By { get; set; }

        public string Created_Date { get; set; }

        public long Updated_By { get; set; }

        public string Updated_Date { get; set; }

        public List<TextValue> sources { get; set; }
    //    public List<Source> sources { get; set; }
    }
}