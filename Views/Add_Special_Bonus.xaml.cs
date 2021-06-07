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
    /// Interaction logic for Add_Special_Bonus.xaml
    /// </summary>
    public partial class Add_Special_Bonus : UserControl
    {

        PersonelDBContext db;
        string emp;

        public Add_Special_Bonus()
        {
            InitializeComponent();
            db = new PersonelDBContext();
          
            
                List<string> employ = db.SelfCards.Where(x => x.Salary == x.maxsalary && x.Status == "قائم على رأس عمله" && (x.Category == "الأولى" || x.Category == "الثانية")).Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();
                emp_list.ItemsSource = employ;
            

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

            //var emp_id = (from d in db.SelfCards select new { d.PersonId, full = d.FirstName + " " + d.FatherName + " " + d.LastName }).ToList();

            //emp= emp_id.Where(d => d.full == emp_list.Text).FirstOrDefault().ToString();

            //long empId = id.PersonId;
            //emp = empname.Text;///////////
            var id = (from x in db.SelfCards
                      where x.FirstName + " " + x.FatherName + " " + x.LastName == emp_list.Text
                      select new { x.PersonId, x.Salary, x.maxsalary, x.Category,x.Workplace,x.JobTitle }).Single();


            double amount = 0;


            long eid = id.PersonId;
            int c = db.Bonuses.Where(x => x.DecisionId == long.Parse(dec_id.Text) && x.PersonId == eid).Count();
            if (c > 0)
            {
                MessageBox.Show("  تم تطبيق هذا القرار على الموظف  " + emp_list.Text);
            }
            else

            {
                var query = (from p in db.Bonuses
                             join d in db.Decisions on p.DecisionId equals d.DecisionId
                             where p.PersonId == eid && d.DecisionContent == "قرار ترفيعة استثنائية"
                             select new { p.DecisionId, fu = (d.DecisionNumber + " " + d.DecisionType + " " + d.DecisionYear) }).Count();






                if (query > 0)
                {
                    MessageBox.Show("   تم منح ترفيعة استثنائية من قبل  للموظف " +emp_list.Text);


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
                        SalaryBouns = (double)(id.Salary + amount),
                        Workplace = id.Workplace,
                        JobTitle = id.JobTitle,
                        Category = id.Category,
                        Register = Login.regName


                    };
                    db.Bonuses.Add(r);
                    db.SaveChanges();
                    (from p in db.SelfCards
                     where p.PersonId == eid
                     select p).ToList()
                       .ForEach(x => x.Salary = (double)(id.Salary + amount));
                    db.SaveChanges();

                    MessageBox.Show("تمت عملية الانتهاء من تنفيذ قرار الترفيعة");
                    string message = "هل انتهى تنفيذ القرار؟";
                    string caption = "تنبيه";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        emp_list.Text = null;
                        this.Visibility = Visibility.Collapsed;
                        var d = db.Decisions.Where(c => c.DecisionId == long.Parse(dec_id.Text)).Single();
                        excute.IsChecked = true;
                        // d.IsExcute = true;
                        d.IsExcute = true;

                        db.Decisions.Update(d);

                        db.SaveChanges();


                        this.Visibility = Visibility.Collapsed;



                     
                        Decision_View dv = new Decision_View();
                        Window parentWindow = Window.GetWindow(this);
                        parentWindow.Close();
                        dv.Show();

                    }
                    else if (result == MessageBoxResult.No)
                    {
                        emp_list.Text = null;
                    }
                   
                  
                }





            }







        }







    }
}

