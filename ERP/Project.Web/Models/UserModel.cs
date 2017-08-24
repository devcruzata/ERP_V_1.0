using Project.Entity;
using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class UserModel
    {
        public long User_ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Addres { get; set; }

        public string UserType { get; set; }

        public bool IsAdmin { get; set; }

        public List<Users> users { get; set; }

        public List<TextValue> userTypes { get; set; }
    }
}