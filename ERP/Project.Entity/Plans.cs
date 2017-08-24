using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Plans
    {
        public Int32 Plan_ID { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Type { get; set; }

        public string Currency { get; set; }

        public Int32 NoOfUsers { get; set; }

        public string Status { get; set; }
    }
}
