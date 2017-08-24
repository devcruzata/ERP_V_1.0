using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class PlanModel
    {
        public Int32 Plan_ID { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Type { get; set; }

        public string Currency { get; set; }

        public Int32 NoOfUsers { get; set; }

        public string Status { get; set; }

        public List<Plans> plans { get; set; }
    }
}