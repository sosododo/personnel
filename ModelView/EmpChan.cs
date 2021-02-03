using personnel.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace personnel.ModelView
{

    class EmpChan
    {
        PersonelDBContext db;



    public IEnumerable GetChange(long id)
    {

        db = new PersonelDBContext();


        IEnumerable query = (from c in db.FunctionalChanges
                             join d in db.Decisions on c.DecisionId equals d.DecisionId
                             where c.PersonId == id && d.DecisionStatus != "قرار مطوي"
                             select new { c.DecisionId, fu = (d.DecisionNumber + " " + d.DecisionType + " " + d.DecisionYear), c.ChangeReasone,c.ChangeDate,c.Category,c.Status }).ToList();





        return query;


    }
}
}
