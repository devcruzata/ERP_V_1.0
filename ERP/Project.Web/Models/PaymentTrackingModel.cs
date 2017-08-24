using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class PaymentTrackingModel
    {
        public long Trackin_ID_PK { get; set; }

        public long Project_ID_Fk { get; set; }

        public string Project_Title { get; set; }

        public string Date { get; set; }

        public decimal TotalCost { get; set; }

        public decimal AmountPaid { get; set; }

        public decimal AmountRemainig { get; set; }

         public decimal Conv_Rate { get; set; }

        public string Task { get; set; }

        public string Descripton { get; set; }

        public long Client_ID_Fk { get; set; }

        public string ClientName { get; set; }

        public string Status { get; set; }

        public List<Project.Entity.Tracker> tracker { get; set; }

        public List<Project.Entity.ProjectData> project_data { get; set; }

        public List<Project.Entity.PaymentData> payment_data { get; set; }

        public List<string> AmntPaid { get; set; }

        public List<string> AmntPaidInInr { get; set; }               

        public List<string> Dat { get; set; }

       
    }
}