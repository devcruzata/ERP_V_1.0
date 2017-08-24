using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Activity
    {
       public long Activity_ID { get; set; }

       public long RelateTo_ID { get; set; }

       public string RelateTo_Name { get; set; }

       public string Title { get; set; }

       public string CreatedBy { get; set; }

       public string CreatedByName { get; set; }

       public string CreatedDate { get; set; }

       public string CreatedTime { get; set; }

       public string Status { get; set; }

       public long PIN { get; set; }

       public string ActivityType { get; set; }

       public string FromAdd { get; set; }

       public string ToAdd { get; set; }
    }
}
