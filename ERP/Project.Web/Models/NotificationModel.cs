using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class NotificationModel
    {
        //private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        //{
        //    if (e.Type == SqlNotificationType.)
        //    {
        //       // MessagesHub.SendMessages();
        //    }
        //}

        public string totalLeadAssigned { get; set; }

        public string totalTaskAssigned { get; set; }
    }
}