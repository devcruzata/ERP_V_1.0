using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Tracker
    {
        public long Trackin_ID_PK { get; set; }

        public long Project_ID_Fk { get; set; }

        public string Project_Title { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalCost { get; set; }

        public decimal AmountPaid { get; set; }

        public decimal AmountRemainig { get; set; }

        public decimal Conv_Rate { get; set; }

        public string Task { get; set; }

        public string Descripton { get; set; }

        public long Client_ID_Fk { get; set; }

        public string ClientName { get; set; }

        public string Status { get; set; }
    }

   public class ProjectData
   {
       public long Project_ID_Fk { get; set; }

       public string Title { get; set; }

       public long Client_ID { get; set; }

       public string Client_Name { get; set; }

       public decimal Total_Cost { get; set; }
   }

   public class PaymentData
   {
       public long TrackRecord_ID { get; set; }

       public decimal AmountPaid { get; set; }

       public decimal AmountRemaining { get; set; }

       public decimal ConvRate { get; set; }

       public string Date { get; set; }

       public string Description { get; set; }
   }
}
