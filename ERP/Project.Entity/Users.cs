using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
    public class Users
    {
        public long User_ID_PK { get; set; }

        public string Uid { get; set; }

        public DateTime Date { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Addres { get; set; }

        public string UserType { get; set; }

        public bool IsAdmin { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }

        public string UnReadReadCount { get; set; }

        public string Subscription { get; set; }
    }
}
