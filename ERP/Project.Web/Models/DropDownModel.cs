using Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class DropDownModel
    {
        public Int32 OptionID { get; set; }

        public string Option { get; set; }        

        public List<TextValue> options { get; set; }
    
    }
}