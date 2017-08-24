using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class SubscriptionModel
    {
        public Int32 Subscription_ID { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public Int32 NoOfUsers { get; set; }

        public string Status { get; set; }

        public List<Subscription> subscription { get; set; }
    }
}