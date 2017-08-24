using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class DashboardModel
    {
        public long UserID { get; set; }

        public string TotalLeads { get; set; }

        public string LeadsTradition { get; set; }

        public string LeadsPercentageChange { get; set; }

        public string TotalDeals { get; set; }

        public string DealsTraditions { get; set; }

        public string DealsPercentageChange { get; set; }

        public string TotalClients { get; set; }

        public string ClientsTraditions { get; set; }

        public string ClientsPercentageChange { get; set; }

        public string TotalDealsRevenue { get; set; }

        public string DealsRevenueTraditions { get; set; }

        public string DealsRevenuePercentageChange { get; set; }

        public string TaskCompletedPercentageToday { get; set; }

        public string TaskCompletedPercentageYesterday { get; set; }

        public List<Opportunities> TopFiveDeals { get; set; }

       public List<Source> TopThreeSources { get; set; }

        public List<Clients> Contacts { get; set; }

        public DashboardModel()
        {
            TopFiveDeals = new List<Opportunities>();
            TopThreeSources = new List<Source>();
            Contacts = new List<Clients>();
        }


    }
}