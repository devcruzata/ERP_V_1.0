using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class DocModel
    {
        public long Doc_ID_Auto_PK { get; set; }

        public string Title { get; set; }

        public string FileName { get; set; }

        public long RelateToContact_ID { get; set; }

        public string RelateToContact_Name { get; set; }

        public long RelateToOpportunity_ID { get; set; }

        public string RelateToOpportunity_Name { get; set; }

        public long DocOwner_ID { get; set; }

        public string DocOwner_Name { get; set; }

        public List<Docs> doc { get; set; }

    }
}