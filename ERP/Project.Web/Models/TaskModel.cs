using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class TaskModel
    {
        public long Task_ID { get; set; }        

        public string Title { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Description { get; set; }      

        public long RelateTo { get; set; }

        public string RelateToName { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedByName { get; set; }

        public string CreatedDate { get; set; }

        public string Status { get; set; }

        public List<Tasks> task { get; set; }

        public TaskModel()
        {
            task = new List<Tasks>();
        }


    }
}