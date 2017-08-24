using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class ProjectModel
    {
        public long Project_ID_PK { get; set; }

        public string ProjectID { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }

        public long Client_ID { get; set; }

        public string Client_Name { get; set; }

        public long Estimation_ID { get; set; }

        public long Category_ID { get; set; }

        public string CategoryName { get; set; }

        public Int32 Language_ID { get; set; }

        public long Dev_Team_ID { get; set; }

        public long Sales_Team_ID { get; set; }   

        public string Model { get; set; }

        public List<string> Task { get; set; }

        public List<string> Description { get; set; }

        public List<string> Hours { get; set; }

        public List<string> Price { get; set; }

        public string Note { get; set; }

        public List<Project.Entity.Projects> Projectss { get; set; }

        public List<string> PayMent_Date { get; set; }

        public List<string> PayMent_Upfront { get; set; }

        public List<string> PayMent_Remaining { get; set; }

        public List<Project.ViewModel.ProjectPaymentDataViewModel> projectPaymentData { get; set; }

        public List<Project.ViewModel.ProjectSowViewModel> projectSowData { get; set; }

        public bool isSigned { get; set; }

        public ProjectModel()
        {
            Task = new List<string>();
            Description = new List<string>();
            Hours = new List<string>();
            Price = new List<string>();
        }

    }
}