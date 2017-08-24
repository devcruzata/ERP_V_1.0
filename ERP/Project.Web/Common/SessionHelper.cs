using System.Web;

namespace Project.Web.Common
{
    public class SessionHelper
    {
        public string CurrentCulture
        {
            get
            {
                if (HttpContext.Current.Session["CurrentCulture"] == null)
                {
                    HttpContext.Current.Session["CurrentCulture"] = "en-US";
                }
                return (string)HttpContext.Current.Session["CurrentCulture"];
            }
            set
            {
                HttpContext.Current.Session["CurrentCulture"] = value;
            }
        }

        public UserSession UserSession
        {
            get
            {
                if (HttpContext.Current.Session["UserSession"] == null)
                {
                    return null;
                }
                return (UserSession)HttpContext.Current.Session["UserSession"];
            }
            set
            {
                HttpContext.Current.Session["UserSession"] = value;
            }
        }

        public UserSubsInfo UserSubscription
        {
            get
            {
                if (HttpContext.Current.Session["UserSubscriptionSession"] == null)
                {
                    return null;
                }
                return (UserSubsInfo)HttpContext.Current.Session["UserSubscriptionSession"];
            }
            set
            {
                HttpContext.Current.Session["UserSubscriptionSession"] = value;
            }
        }

        public SyncSession DataSyncSession
        {
            get
            {
                if (HttpContext.Current.Session["DataSyncSession"] == null)
                {
                    return null;
                }
                return (SyncSession)HttpContext.Current.Session["DataSyncSession"];
            }
            set
            {
                HttpContext.Current.Session["DataSyncSession"] = value;
            }
        }

        public NotificationSession NotificationSession
        {
            get
            {
                if (HttpContext.Current.Session["NotificationSession"] == null)
                {
                    return null;
                }
                return (NotificationSession)HttpContext.Current.Session["NotificationSession"];
            }
            set
            {
                HttpContext.Current.Session["NotificationSession"] = value;
            }
        }

        public UserSetingInfo UserSetingSession
        {
            get
            {
                if (HttpContext.Current.Session["UserSetingSession"] == null)
                {
                    return null;
                }
                return (UserSetingInfo)HttpContext.Current.Session["UserSetingSession"];
            }
            set
            {
                HttpContext.Current.Session["UserSetingSession"] = value;
            }
        }
    }
}