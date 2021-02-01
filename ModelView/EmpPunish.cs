using personnel.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace personnel.ModelView
{
   
    class EmpPunish
    {
        PersonelDBContext db;

        public IEnumerable Getpun(long id)
        {

            db = new PersonelDBContext();


            IEnumerable query = (from p in db.Punishments
                                 join d in db.Decisions on p.DecisionId equals d.DecisionId
                                 where p.PersonId == id && d.DecisionStatus != "قرار مطوي"
                                 select new { p.DecisionId, fu = (d.DecisionNumber + " " + d.DecisionType + " " + d.DecisionYear), p.PunishmentType,p.Reason,p.StartDate,p.EndDate,p.Period,p.Notes,p.Discount}).ToList();





            return query;


        }
    }
}
