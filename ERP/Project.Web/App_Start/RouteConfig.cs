using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           routes.MapRoute(
           name: "MyAccount",
           url: "clouderac/MyAccount",
           defaults: new { controller = "Home", action = "AdminHome" }
           );           

          routes.MapRoute(
          name: "AdminDashboard_V_2",
          url: "Home/Admin",
          defaults: new { controller = "Home", action = "AdminDashboard_V_2" }
          );

         routes.MapRoute(
         name: "SuperAdmin",
         url: "Home/SuperAdmin",
         defaults: new { controller = "Home", action = "SuperAdminDashboard" }
         );

         routes.MapRoute(
         name: "Plans",
         url: "Plans/view",
         defaults: new { controller = "Plans", action = "Plans" }
         );

     //routes.MapRoute(
     //name: "Opportunity",
     //url: "Opportunity/ManageOpportunity",
     //defaults: new { controller = "Opportunity", action = "OpportunityHome" }
     //);

         routes.MapRoute(
            name: "SubscriptionHolder",
            url: "subscriptionholder/view",
            defaults: new { controller = "SubscriptionHolder", action = "SubscriptionHolderUser" }
            );

         routes.MapRoute(
         name: "Tickets",
         url: "tickets/view",
         defaults: new { controller = "Tickets", action = "TicketsHome" }
         );

          routes.MapRoute(
          name: "CrmUserHome",
          url: "CRM/User",
          defaults: new { controller = "Home", action = "CRM_User_Dashboard" }
          );

           routes.MapRoute(
           name: "LeadHome",
           url: "Leads/leads",
           defaults: new { controller = "Leads", action = "LeadHome" }
           );

            routes.MapRoute(
            name: "ManageLeads",
            url: "Leads/leads/{act}/{id}",
            defaults: new { controller = "Leads", action = "ManageLead", id = UrlParameter.Optional }
            );

           

            routes.MapRoute(
            name: "ClientHome",
            url: "Client/client",
            defaults: new { controller = "Clients", action = "ClientHome" }
            );

            routes.MapRoute(
            name: "ManageClient",
            url: "Client/clients/{act}/{id}",
            defaults: new { controller = "Clients", action = "ManageClient", id = UrlParameter.Optional }
            );           

            routes.MapRoute(
            name: "UserHome",
            url: "User/Users",
            defaults: new { controller = "User", action = "UserHome" }
            );

            routes.MapRoute(
            name: "Subscriptions",
            url: "Subscriptions/subscriptions",
            defaults: new { controller = "Subscription", action = "SubscriptionHome" }
            );

            routes.MapRoute(
            name: "UserSetings",
            url: "UserSetings/setings",
            defaults: new { controller = "UserSetings", action = "SetingsHome" }
            );

            routes.MapRoute(
            name: "DroplistSetings",
            url: "UserSetings/droplistsetings",
            defaults: new { controller = "UserSetings", action = "DroplistSetings" }
            );

            routes.MapRoute(
            name: "EmailSetings",
            url: "UserSetings/mailsetings",
            defaults: new { controller = "UserSetings", action = "MailSeting" }
            );

            routes.MapRoute(
            name: "MarketingCampaign",
            url: "MarketingCampaign/mailmarketing",
            defaults: new { controller = "MarketingCampaign", action = "EmailMarketingHome" }
            );

            routes.MapRoute(
            name: "MeetingHome",
            url: "Meetings/meetings",
            defaults: new { controller = "Meeting", action = "MeetingHome" }
            );

            routes.MapRoute(
            name: "OpportunityHome",
            url: "Opportunities/opportinities",
            defaults: new { controller = "Opportunity", action = "OpportunityHome" }
            );


            routes.MapRoute(
            name: "ManageOpportunity",
            url: "Opportunity/opportunities/{act}/{id}",
            defaults: new { controller = "Opportunity", action = "ManageOpportunity", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "DocumentHome",
            url: "Documents/document",
            defaults: new { controller = "Doc", action = "DocHome" }
            );

            routes.MapRoute(
            name: "Calender",
            url: "Leads/ManageCalender",
            defaults: new { controller = "Calender", action = "ManageCalender" }
            );
            

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Authentication", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}