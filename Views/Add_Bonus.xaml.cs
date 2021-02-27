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
    public partial class Add_Bonus : UserControl
    {
      
        PersonelDBContext db;
        List<string> emp;
        public Add_Bonus()
        {
            InitializeComponent();
            db = new PersonelDBContext();

            if (Login.currentUser.Rule == "teacher")
            {


                //emp = (from p in db.SelfCards
                //       where (p.Status == "قائم على رأس عمله" && p.Salary < p.maxsalary && p.FileClass == "تدريسي")
                //       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                //list.ItemsSource = emp;
            }


            else if (Login.currentUser.Rule == "الأولى")
            {
                emp = (from p in db.SelfCards
                       where (p.Status == "قائم على رأس عمله" && p.Salary < p.maxsalary && p.Category == "الأولى")
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;
               
            }

            else if (Login.currentUser.Rule == "الثانية")
            {
                emp = (from p in db.SelfCards
                       where (p.Status == "قائم على رأس عمله" && p.Salary < p.maxsalary && (p.Category == "الثانية/اداريين" || p.Category == "الثانية/مخبريين"))
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;

            }

            else if (Login.currentUser.Rule == "الثالثة")
            {
                emp = (from p in db.SelfCards
                       where (p.Status == "قائم على رأس عمله" && p.Salary < p.maxsalary && (p.Category == "الثالثة" || p.Category== "الرابعة"))
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;

            }

            else if (Login.currentUser.Rule == "الخامسة")
            {
                emp = (from p in db.SelfCards
                       where (p.Status == "قائم على رأس عمله" && p.Salary < p.maxsalary && p.Category == "الخامسة")
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;

            }

            else if (Login.currentUser.Rule == "عقود")
            {
                emp = (from p in db.SelfCards
                       where (p.Status == "قائم على رأس عمله" && p.Salary < p.maxsalary && p.FileClass == "عقد")
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;

            }
            else if (Login.currentUser.Rule == "admin")
            {

                emp = (from p in db.SelfCards
                       where (p.Status == "قائم على رأس عمله" && p.Salary < p.maxsalary && p.FileClass == "إداري")
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;
            }

            Decision d = (Decision)DataContext;
        }



   
      
        private void insert_rest(object sender, RoutedEventArgs e)
        {
            try {

                excutee();
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void excutee() {
            DateTime fromDate;
            DateTime toDate;
            double bouns = 0.09;
            int days;
            var emp_id = (from d in db.SelfCards
            where d.Status == "قائم على رأس عمله" 
            select new { d.PersonId, full = d.FirstName + " " + d.FatherName + " " + d.LastName,d.Salary ,d.maxsalary }).ToList();




            foreach (string ss in emp.ToList())
            {
                int sum1 = 0;
                double amount = 0;
            
                double allDays =0;
                var id = emp_id.Where(d => d.full == ss).ToList().ElementAt(0);
                long eid = id.PersonId;
                int c = db.Bonuses.Where(x => x.DecisionId == long.Parse(dec_id.Text) && x.PersonId == eid).Count();
                if (c > 0)
                {
                    MessageBox.Show("  تم تطبيق هذا القرار على الموظف  " + id.full);
                }
                else
                {
                   // حساب عدد ايام الدوام
                    fromDate = (DateTime)from.SelectedDate;

                    toDate = (DateTime)to.SelectedDate;
                    //days in first yaer
                    List<Rest> rests1=   db.Rests.Where(x => x.PersonId == eid && x.RestType == "إجازة بلا أجر" && x.RestStart >= fromDate && x.RestStart <= toDate).ToList();
             
                    foreach (Rest rset in rests1)
                    {
                        if (rset.Period == "يوم")
                        {
                            sum1 += rset.RestPeriod;
                        }
                        else if (rset.Period == "شهر")
                        {
                            sum1 += (rset.RestPeriod * 30);
                        }
                        else if (rset.Period == "سنة")
                        {
                            sum1 += (rset.RestPeriod * 360);
                        }

                    }

               
                    allDays = 720 - sum1;
                    if (allDays >= 0)
                    {
                        amount = (double)((allDays / 720) * (0.09 * id.Salary));
                        if (id.Salary + amount <= id.maxsalary)
                        {
                            Bonuse r = new Bonuse
                            {

                                PersonId = id.PersonId,
                                DecisionId = long.Parse(dec_id.Text),
                                Bouns = 0.09,
                                FromYear = (DateTime)from.SelectedDate,
                                ToYear = (DateTime)to.SelectedDate,
                                Salary = (double)id.Salary,
                                NumDays = (int)allDays,
                                SalaryBouns = (double)(id.Salary + ((allDays / 720) * (0.09 * id.Salary)))


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
                                 .ForEach(x => x.Salary = (double)(id.Salary + ((allDays / 720) * (0.09 * id.Salary))));
                            db.SaveChanges();
                        } 
                        else if (id.Salary+amount>id.maxsalary) {
                            Bonuse r = new Bonuse
                            {

                                PersonId = id.PersonId,
                                DecisionId = long.Parse(dec_id.Text),
                                Bouns = 0.09,
                                FromYear = (DateTime)from.SelectedDate,
                                ToYear = (DateTime)to.SelectedDate,
                                Salary = (double)id.Salary,
                                NumDays = (int)allDays,
                                SalaryBouns = (double)(id.Salary + (id.maxsalary - id.Salary))


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
                                 .ForEach(x => x.Salary = (double)(id.Salary + ((allDays / 720) * (0.09 * id.Salary))));
                            db.SaveChanges();

                        }
                    }
                    else if (allDays < 0)
                    {
                        Bonuse r = new Bonuse
                        {

                            PersonId = id.PersonId,
                            DecisionId = long.Parse(dec_id.Text),
                            Bouns = 0.09,
                            FromYear = (DateTime)from.SelectedDate,
                            ToYear = (DateTime)to.SelectedDate,
                            Salary = (double)id.Salary,
                            NumDays = 0,
                            SalaryBouns = (double)id.Salary


                        };
                        db.Bonuses.Add(r);
                        db.SaveChanges();
                        var d = db.Decisions.Where(c => c.DecisionId == long.Parse(dec_id.Text)).Single();
                        excute.IsChecked = true;
                        // d.IsExcute = true;
                        d.IsExcute = true;

                        db.Decisions.Update(d);

                        db.SaveChanges();
                    }





                }

                }
            list.SelectedItem = null;
         
          

            this.Visibility = Visibility.Collapsed;
          


            MessageBox.Show("تمت عملية الانتهاء من تنفيذ قرار الترفيعة");
            Decision_View dv = new Decision_View();
            Window parentWindow = Window.GetWindow(this);
            parentWindow.Close();
            dv.Show();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           string l= list.SelectedItem as string ;
            
                foreach (string ss in emp.ToList()) {

                    if (ss==l) {
                        emp.Remove(ss);
                    
                    }
                
                
            
            }
            list.ClearValue(ItemsControl.ItemsSourceProperty);
            list.ItemsSource = emp;


        }

   



  
    }
}
