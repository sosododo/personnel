using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using personnel.Models;
using System.Collections;

namespace personnel.ModelView
{
    class Teach_delegate
    {
       

        public  int getDelegate(long empID)
        {
            PersonelDBContext db = new PersonelDBContext();
            int period_sum=0;
           List<Delegating> dl = db.Delegatings.Where(x => x.PersonId == empID && x.DelegatingReason == "الحصول على مؤهل علمي").ToList();
            
            foreach (Delegating d in dl)
            { 
                if (d.PeriodType == "سنة")
                    period_sum = period_sum +(int) d.PeriodNum ;

 
            }

            return period_sum;
        }


        public IEnumerable GetDelegations(long id)
        {
            PersonelDBContext db = new PersonelDBContext();

            db = new PersonelDBContext();

            IEnumerable query = (from r in db.Delegatings
                                 join d in db.Decisions on r.DecisionId equals d.DecisionId
                                 where r.PersonId == id
                                 select new { r.DecisionId, fu = (d.DecisionNumber + " " + d.DecisionType + " " + d.DecisionYear), r.DelegatingType, r.PeriodNum, r.PeriodType, r.DelegatingStart, r.DelegatingEnd, r.DelegatingReason, r.DelegatingCountry, r.Notes }).ToList();





            //List<Rest> query = db.Rests.Where(x => x.PersonId == id).ToList();).ToList

            return query;


        }

    }
}

