using System;
using System.Collections.Generic;
using System.Text;

namespace personnel.ModelView
{
    class RewardDetails
    {

        public string PersonName { get; set; }
        public string RewardType { get; set; }
        public string Reason { get; set; }
        public DateTime RewardDate { get; set; }

        public string Amount { get; set; }
        public string Notes { get; set; }
    }
}
