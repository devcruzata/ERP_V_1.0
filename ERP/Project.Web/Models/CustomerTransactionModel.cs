using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class CustomerTransactionModel
    {
        public long Transaction_ID { get; set; }

        public long CustomerID { get; set; }

        public Int32 Product_ID { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public string Subs_Type { get; set; }
    }
}