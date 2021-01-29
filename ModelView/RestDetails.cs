using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.ObjectModel;

namespace personnel.ModelView
{
    class RestDetails 

    {
        public string PersonName { get; set; }
        public string restType { get; set; }
        public int restPeriod { get; set; }
        public string period { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string note { get; set; }
        public string attachment { get; set; }

    }
}
