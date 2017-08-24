using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class TicketModel
    {
        public long ticketID { get; set; }

        public string ticketNo { get; set; }

        public string query { get; set; }

        public string priority { get; set; }

        public string Status { get; set; }

        public List<Tickets> ticket { get; set; }
    }
}