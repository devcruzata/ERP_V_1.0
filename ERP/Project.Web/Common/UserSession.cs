namespace Project.Web.Common
{
    public class UserSession
    {
        public long UserId { get; set; }
        public string uid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }       
        public string UserType { get; set; }
        public string Subscription_ID { get; set; }
        public string PIN { get; set; }

        

    }

    public class SyncSession
    {
        public string lastContactSync { get; set; }
        public string lastCalenderSync { get; set; }
    }

    public class UserSubsInfo
    {
        public string sCustID { get; set; }

        public string sSubscriptionID { get; set; }

        public string planType { get; set; }

        public string planId { get; set; }

        public string sToken { get; set; }
    }

    public class NotificationSession
    {
        public string lastLeadAssignToNf { get; set; }

        public string lastTaskAssignToNf { get; set; }

        public string lastNotificationAt { get; set; }

        public string lastNotificationViewedAt { get; set; }

        public int totalNoOfNotification { get; set; }
      

    }

    public class UserSetingInfo
    {
        public string smtpHost { get; set; }

        public string smtpPort { set; get; }

        public bool smtpIsSsl { get; set; }

        public string smtpUsername { get; set; }

        public string smtpPassword { get; set; }

        public string inboundMailboxType { get; set; }

        public string inboundMailboxHost { get; set; }

        public string inboundMailport { get; set; }

        public bool inboundIsSsl { get; set; }

        public string inboundMailUsername { get; set; }

        public string inboundMailPassword { get; set; }
    }

    public class UserPermission
    {
        public string systemWideLeads { get; set; }

        public string systemWideOpportunities { get; set; }

        public string systemWideClients { get; set; }

        public string associatedLeads { get; set; }

        public string associatedClients { get; set; }

        public string associatedOpportunities { get; set; }

        public string calendar { get; set; }

        public string task { get; set; }

        public string notes { get; set; }

        public string docs { get; set; }

        public string userManagement { get; set; }

        public string siteManagement { get; set; }

        public string helpDeskTicket { get; set; }

        public string leadDistribution { get; set; }



    }
}