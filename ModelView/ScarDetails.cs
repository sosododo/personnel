using System;
using System.Collections.Generic;
using System.Text;

namespace personnel.ModelView
{
    class ScarDetails
    {

        public string PersonName { get; set; }
        public string DelegatingType { get; set; }
        public string ScarReason { get; set; }
        public int PeriodNum { get; set; }
        public string PeriodType { get; set; }
        public DateTime ScarStart { get; set; }
        public DateTime ScarEnd { get; set; }
        public string ScarPlace { get; set; }
        public string Notes { get; set; }
    }
}
