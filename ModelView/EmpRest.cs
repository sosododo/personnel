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
   public class EmpRest
    {
        PersonelDBContext db;
        public EmpRest()
        {


          
           
        }
        public string getdata(long desId)
        {
            db = new PersonelDBContext();
            Decision des;
            des = (Decision)db.Decisions.Where(x => x.DecisionId == desId);
            string  result = des.DecisionNumber + " " + des.DecisionType + " " + des.DecisionYear;
            return result;
        }
           //جلب جميع الاجازات
        public IEnumerable GetRests(long id)
        {
          
            db = new PersonelDBContext();

           IEnumerable query=          (from r in db.Rests join d in db.Decisions on r.DecisionId equals d.DecisionId
                                                where r.PersonId == id && d.DecisionStatus!="قرار مطوي"
                                                select new { r.DecisionId, fu=( d.DecisionNumber +" " + d.DecisionType + " " + d.DecisionYear), r.RestType,r.RestPeriod,r.Period, r.RestStart,r.RestEnd,r.Attachment,r.Notes }).ToList();
             
             

         

            //List<Rest> query = db.Rests.Where(x => x.PersonId == id).ToList();).ToList

            return query;


        }
        //حساب عدد سنوات الخدمة بالاعتماد على تاريخ المباشرة
        public int service_count(DateTime direct)
        {
            db = new PersonelDBContext();

            int reyear = direct.Year;
            int nowyear = DateTime.Now.Year;
            int service_years = nowyear - reyear;
            

            return service_years;


        }
        //جلب تاريخ المباشرة بالاعتماد على رقم الموظف
        public DateTime getbegining(long id)
        {
            db = new PersonelDBContext();
            DateTime direct = (DateTime)db.SelfCards.Where(x => x.PersonId == id).Select(m => m.BeginningDate).ToList().ElementAt(0);
            return direct;
        }
        //عدد الاجازات الادارية السنوية المسموح بها بالاعتماد على تاريخ المباشرة
          
            public int AllRest(int service)
        {
            int count1 = 0;
            if (service <= 5)
            {
                count1 = 15;

            }
            else if (service > 5 && service <= 10)
            {
                count1 = 21;
            }
            else if (service > 10 && service <= 20)
            {
                count1 = 26;
            }
            else
            {
                count1 = 30;
            }

            return count1;
        }

        // عدد الاجازات الادارية المتبقية لموظف ما بالاعتماد على رقمه وعدد الاجازات الكلي المسموح به
        public int MinRest(int count ,long id)
        { db = new PersonelDBContext();
            int minrest = 0;
            int days=0;
            int now = DateTime.Now.Year;
            List<Rest> rests = db.Rests.Where(x => x.RestType == "إجازة ادارية" && x.RestStart.Value.Year == now && x.PersonId==id).ToList();
            //var query = (from s in db.Rests where s.PersonId == id && s.RestType == "إجازة إدارية" && s.RestStart.Value.Year == now select new { s.RestPeriod });
            //int c= query.Count();
       
            //for (int i = 0; i < c; c++)
            //{
            //  min=days+q

            //}

            foreach (Rest r in rests)
            {
                days += r.RestPeriod;
            }
            minrest = count - days;
            return minrest;
        }



        //
        public int canrest( long id)
        {
            //DateTime begin = getbegining(id);
            //int ser = service_count(begin);
            //int con = AllRest(ser);
            int min = MinRest(AllRest(service_count(getbegining(id))), id);
            //int min = MinRest(con, id);
            return min;

                
        }
        public int calculate(long id)
        
        {
            int sum1=0;
            db = new PersonelDBContext();
            List<Rest> rests2 = db.Rests.Where(x => x.RestType == "إجازة بلا أجر" && x.PersonId == id).ToList();
            foreach (Rest r in rests2)
            {
                if (r.Period == "يوم")
                {
                    sum1 += r.RestPeriod;
                }
                else if (r.Period == "شهر")
                {
                    sum1 += (r.RestPeriod * 30);
                }
                else if (r.Period == "سنة")
                {
                    sum1+= (r.RestPeriod * 360);
                }
           
                   
                    
                
            }
            return sum1;

        }


   

    }


    }
