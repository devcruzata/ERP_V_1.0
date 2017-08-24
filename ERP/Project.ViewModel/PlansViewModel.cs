using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.ViewModel
{
    public class PlansViewModel
    {
        public int planId { get; set; }

        public string planName { get; set; }

        public string planType { get; set; }

        public string planPrice { get; set; }

        public List<TextValue> features { get; set; }
    }
}
