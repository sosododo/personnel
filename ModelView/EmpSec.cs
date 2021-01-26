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
  public  class EmpSec
    {

        PersonelDBContext db;



        public IEnumerable GetSec(long id)
        {

            db = new PersonelDBContext();


            IEnumerable query = (from c in db.Secondments
                                 join d in db.Decisions on c.DecisionId equals d.DecisionId
                                 where c.PersonId == id
                                 select new { c.DecisionId, fu = (d.DecisionNumber + " " + d.DecisionType + " " + d.DecisionYear), c.SecondmentType, c.PeriodNum, c.PeriodType, c.SecondmentStart  , c.SecondmentEnd,c.SecondmentPlace,c.Notes}).ToList();





            return query;


        }
    }
}
