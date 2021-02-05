using System;
using System.Collections.Generic;
using System.Text;

namespace personnel.ModelView
{
    class BonusDetails
    {

        public string PersonName { get; set; }
        public double Salary { get; set; }
        public double Bouns { get; set; }
        public double SalaryBouns { get; set; }
    
        public int NumDays { get; set; }
        public DateTime FromYear { get; set; }
        public DateTime ToYear { get; set; }

    }
}
