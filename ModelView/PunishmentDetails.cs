using System;
using System.Collections.Generic;
using System.Text;

namespace personnel.ModelView
{
    class PunishmentDetails
    {
        public string PersonName { get; set; }
        public string PunishmentType { get; set; }
        public string periodType { get; set; }
        public int Period { get; set; }
        public string Reason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Discount { get; set; }
        public string Notes { get; set; }


    }
}
