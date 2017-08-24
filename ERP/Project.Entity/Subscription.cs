using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Subscription
    {
        public Int32 Subscription_ID { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public Int32 NoOfUsers { get; set; }

        public string Status { get; set; }

        public Int32 AnnualDiscount { get; set; }

        public List<TextValue> features { get; set; }
    }

   
}
