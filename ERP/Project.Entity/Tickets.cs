using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Tickets
    {
        public long ticketID { get; set; }

        public string ticketNo { get; set; }

        public string stage { get; set; }

        public DateTime addedOn { get; set; }

        public string subject { get; set; }

        public string query { get; set; }

        public string priority { get; set; }

        public string Status { get; set; }
    }
}
