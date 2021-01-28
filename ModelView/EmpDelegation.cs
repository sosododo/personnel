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
  public  class EmpDelegation
    {

        PersonelDBContext db;
        public EmpDelegation()
        {




        }

        public IEnumerable GetDele(long id)
        {

            db = new PersonelDBContext();


            IEnumerable query = (from p in db.Delegatings
                                 join d in db.Decisions on p.DecisionId equals d.DecisionId
                                where p.PersonId == id && p.DelegatingReason== "الحصول على مؤهل علمي"
                                 select new { p.DecisionId, fu = (d.DecisionNumber + " " + d.DecisionType + " " + d.DecisionYear), p.DelegatingType, p.DelegatingReason, p.DelegatingStart, p.DelegatingEnd, p.PeriodNum, p.Notes, p.PeriodType,p.DelegatingCountry }).ToList();

            return query;


        }
    }
}
