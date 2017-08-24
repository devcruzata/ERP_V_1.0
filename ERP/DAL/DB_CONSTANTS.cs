using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DB_CONSTANTS
    {
        private static string connString_ERP_CRUZATA = ConfigurationManager.AppSettings["connection_ERP_CRUZATA"].ToString();
        //private static string connString_master = ConfigurationManager.ConnectionStrings["connection_master"].ConnectionString;


        public static string ConnectionString_ERP_CRUZATA
        {
            get { return connString_ERP_CRUZATA; }
        }

        //public static string ConnectionString_master
        //{
        //    get { return connString_master; }
        //}
    }
}
