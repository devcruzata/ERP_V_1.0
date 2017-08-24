using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Leads
    {
        public long Lead_ID_Auto_PK { get; set; }        

        public DateTime Date { get; set; }

        public DateTime FollowUpDate { get; set; }

        public string Name { get; set; }       

        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string Alternate_Email { get; set; }

        public string ContactNo { get; set; }

        public string SkypeNo { get; set; }

        public string Category_ID { get; set; }

        public string Model { get; set; }

        public string Source { get; set; }

        public string ZipCode { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Requirement { get; set; }

        public string Remarks { get; set; }

        public string UploadedDocName { get; set; }

        public Boolean HasAttachment { get; set; }

        public string Status { get; set; }

        public string AssignTo { get; set; }

        public string AssignToName { get; set; }

        public long PIN { get; set; }

        public string CreatedBy { get; set; }

        public string JobDescription { get; set; }
    }
}
