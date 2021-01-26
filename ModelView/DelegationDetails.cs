using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Xml.XPath;
using personnel.Models;
using personnel.Views;


namespace personnel.ModelView
{
    class DelegationDetails
    {
        public string PersonName { get; set; }
        public string DelegatingType { get; set; }
        public string DelegatingReason { get; set; }
        public int PeriodNum { get; set; }
        public string PeriodType { get; set; }
        public DateTime DelegatingStart { get; set; }
        public DateTime DelegatingEnd { get; set; }
        public string DelegatingCountry { get; set; }
        public string Notes { get; set; }
    

    }
}
