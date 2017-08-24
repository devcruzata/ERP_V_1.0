using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class RolesModel
    {
        public long Role_ID { get; set; }

        public string RoleName { get; set; }

        public string AssociatedLeads { get; set; }
        public string SystemwideLeads { get; set; }
        public string AssociatedOpportunity { get; set; }
        public string SystemwideOpportunity { get; set; }
        public string AssociatedClients { get; set; }
        public string SystemwideClients { get; set; }
        public string Calendar { get; set; }
        public string Task { get; set; }
        public string Notes { get; set; }
        public string Documents { get; set; }        
        public string UserManagement { get; set; }
        public string SiteManagement { get; set; }        
        public string LeadDistribution { get; set; }
       

        public List<UserRoles> roles { get; set; }
    }
}