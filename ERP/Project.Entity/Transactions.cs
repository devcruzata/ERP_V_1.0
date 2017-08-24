using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Transactions
    {
        public string Transaction_ID { get; set; }

        public long CustomerID { get; set; }

        public Int32 Product_ID { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public DateTime Transaction_Date { get; set; }

        public string PaymentBy { get; set; }

        public string Status { get; set; }
    }
}
