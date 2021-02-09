using personnel.Models;
using personnel.ModelView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace personnel.Views
{
    /// <summary>
    /// Interaction logic for Add_Bonus.xaml
    /// </summary>
    public partial class Add_Special_Bonus : UserControl
    {

        PersonelDBContext db;
        string emp;

        public Add_Special_Bonus()
        {
            InitializeComponent();
            db = new PersonelDBContext();








            Decision d = (Decision)DataContext;
        }





        private void insert_rest(object sender, RoutedEventArgs e)
        {
            try
            {

                excutee();
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void excutee()
        {

            emp = empname.Text;
            var id = (from x in db.SelfCards
                      where x.Status == "قائم على رأس عمله" && x.FirstName + " " + x.FatherName + " " + x.LastName == emp
                      select new { x.PersonId, x.Salary, x.maxsalary, x.Category }).Single();


            double amount = 0;


            long eid = id.PersonId;
            int c = db.Bonuses.Where(x => x.DecisionId == long.Parse(dec_id.Text) && x.PersonId == eid).Count();
            if (c > 0)
            {
                MessageBox.Show("  تم تطبيق هذا القرار على الموظف  " + emp);
            }
            else

            {
                var query = (from p in db.Bonuses
                             join d in db.Decisions on p.DecisionId equals d.DecisionId
                             where p.PersonId == eid && d.DecisionContent == "قرار ترفيعة استثنائية"
                             select new { p.DecisionId, fu = (d.DecisionNumber + " " + d.DecisionType + " " + d.DecisionYear) }).Count();






                if (query > 0)
                {
                    MessageBox.Show("   تم منح ترفيعة استثنائية من قبل  للموظف " +emp);


                }
                else
                {
                    if (id.Category == "الأولى") { amount = 1000; }

                    else if (id.Category == "الثانية") { amount = 600; }
                    Bonuse r = new Bonuse
                    {

                        PersonId = id.PersonId,
                        DecisionId = long.Parse(dec_id.Text),
                        Bouns = amount,
                        FromYear = null,
                        ToYear = null,
                        Salary = (double)id.Salary,
                        NumDays = 0,
                        SalaryBouns = (double)(id.Salary + amount)


                    };
                    db.Bonuses.Add(r);
                    db.SaveChanges();
                    var d = db.Decisions.Where(c => c.DecisionId == long.Parse(dec_id.Text)).Single();
                    excute.IsChecked = true;
                    // d.IsExcute = true;
                    d.IsExcute = true;

                    db.Decisions.Update(d);

                    db.SaveChanges();
                    (from p in db.SelfCards
                     where p.PersonId == eid
                     select p).ToList()
                         .ForEach(x => x.Salary = (double)(id.Salary + amount));
                    db.SaveChanges();
                }





            }






            this.Visibility = Visibility.Collapsed;



            MessageBox.Show("تمت عملية الانتهاء من تنفيذ قرار الترفيعة");
            Decision_View dv = new Decision_View();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
            dv.Show();

        }







    }
}

