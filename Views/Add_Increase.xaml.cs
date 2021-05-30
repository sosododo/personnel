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
    /// Interaction logic for Increase.xaml
    /// </summary>
    public partial class Add_Increase : UserControl
    {
        PersonelDBContext db;
        List<string> emp;
        public Add_Increase()
        {
            InitializeComponent();
            
            db = new PersonelDBContext();



            if (Login.currentUser.Rule == "تدريسي")
            {
                emp = (from p in db.SelfCards
                       where (p.Status != "بحكم المستقيل" && p.Status != "كف اليد" && p.Status != "مصروف من الخدمة" && p.FileClass == "تدريسي")
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;
            }
            else if (Login.currentUser.Rule == "فني")
            {

                emp = (from p in db.SelfCards
                       where (p.Status != "بحكم المستقيل" && p.Status != "كف اليد" && p.Status != "مصروف من الخدمة"  && p.FileClass == "فني")
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;
            }
            else if (Login.currentUser.Rule == "معيد")
            {
                emp = (from p in db.SelfCards
                       where (p.Status != "بحكم المستقيل" && p.Status != "كف اليد" && p.Status != "مصروف من الخدمة" && p.FileClass == "معيد")
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;
            }

            else if (Login.currentUser.Rule == "الأولى")
            {
                emp = (from p in db.SelfCards
                       where (p.Status != "بحكم المستقيل" && p.Status != "كف اليد" && p.Status != "مصروف من الخدمة" && p.Category == "الأولى")
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;
            }
            else if (Login.currentUser.Rule == "الثانية")
            {

                emp = (from p in db.SelfCards
                       where (p.Status != "بحكم المستقيل" && p.Status != "كف اليد" && p.Status != "مصروف من الخدمة" && (p.Category == "الثانية/مخبريين" || p.Category== "الثانية/اداريين"))
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;
            }
            else if (Login.currentUser.Rule == "الثالثة")
            {
                emp = (from p in db.SelfCards
                       where (p.Status != "بحكم المستقيل" && p.Status != "كف اليد" && p.Status != "مصروف من الخدمة" && (p.Category == "الثالثة" || p.Category == "الرابعة"))
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;

            }
            else if (Login.currentUser.Rule == "الخامسة")
            {
                emp = (from p in db.SelfCards
                       where (p.Status != "بحكم المستقيل" && p.Status != "كف اليد" && p.Status != "مصروف من الخدمة" && p.Category == "الخامسة")
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;
            }
            else if (Login.currentUser.Rule == "عقود")
            {

                emp = (from p in db.SelfCards
                       where (p.Status != "بحكم المستقيل" && p.Status != "كف اليد" && p.Status != "مصروف من الخدمة" && p.FileClass == "عقد")
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;
            }
            else if (Login.currentUser.Rule == "admin")
            {
                emp = (from p in db.SelfCards
                       where (p.Status != "بحكم المستقيل" && p.Status != "كف اليد" && p.Status != "مصروف من الخدمة")
                       select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


                list.ItemsSource = emp;
            }
            /////////////////////////////////////////////////////


            Decision d = (Decision)DataContext;
        }

        private void excute_increase(object sender, RoutedEventArgs e)
        {
            try
            {

                excutee();
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void excutee()
        {


            var emp_id = (from Z in db.SelfCards
                          where Z.Status != "بحكم المستقيل" && Z.Status != "كف اليد" && Z.Status != "مصروف من الخدمة"
                          select new { Z.PersonId, full = Z.FirstName + " " + Z.FatherName + " " + Z.LastName, Z.Salary,Z.maxsalary,Z.Status }).ToList();
            foreach (string ss in emp.ToList())
            {

                var id = emp_id.Where(d => d.full == ss).ToList().ElementAt(0);
                long eid = id.PersonId;
                
                long before = (long)id.Salary;
                int c = db.SalaryIncrease.Where(x => x.DecisionId == long.Parse(dec_id.Text) && x.PersonId == eid).Count();
                if (c > 0)
                {
                    MessageBox.Show("  تم تطبيق هذا القرار على الموظف  " + id.full);
                }
                else
                {
                    if (id.Status == "متقاعد") {
                        SalaryIncrease sn = new SalaryIncrease
                        {
                            PersonId = eid,
                            DecisionId = long.Parse(dec_id.Text),
                            SalaryBefore = before,
                            SalaryAfter = before + long.Parse(off.Text),
                            Increase = long.Parse(off.Text),
                        };
                        db.SalaryIncrease.Add(sn);
                        db.SaveChanges();
                        var self = db.SelfCards.Where(x => x.PersonId == eid).Single();
                        self.Salary = before + long.Parse(off.Text);
                        self.maxsalary = id.maxsalary + long.Parse(off.Text);
                        db.SelfCards.Update(self);
                        db.SaveChanges();
                    }
                    else
                    {
                        SalaryIncrease sn = new SalaryIncrease
                        {
                            PersonId = eid,
                            DecisionId = long.Parse(dec_id.Text),
                            SalaryBefore = before,
                            SalaryAfter = before + long.Parse(increase1.Text),
                            Increase = long.Parse(increase1.Text),
                        };
                        db.SalaryIncrease.Add(sn);
                        db.SaveChanges();
                        var self = db.SelfCards.Where(x => x.PersonId == eid).Single();
                        self.Salary = before + long.Parse(increase1.Text);
                        self.maxsalary = id.maxsalary + long.Parse(increase1.Text);
                        db.SelfCards.Update(self);
                        db.SaveChanges();
                    }
                  


                }

            }
           
        

        MessageBox.Show("تمت عملية الانتهاء من تنفيذ قرار الزيادة");
            var d = db.Decisions.Where(c => c.DecisionId == long.Parse(dec_id.Text)).Single();
            excute.IsChecked = true;
            d.IsExcute = true;
            db.Decisions.Update(d);
            db.SaveChanges();
            Decision_View dv = new Decision_View();
        Window parentWindow = Window.GetWindow(this);
        parentWindow.Close();
            dv.Show();

        }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        string l = list.SelectedItem as string;

        foreach (string ss in emp.ToList())
        {

            if (ss == l)
            {
                emp.Remove(ss);

            }



        }
        list.ClearValue(ItemsControl.ItemsSourceProperty);
        list.ItemsSource = emp;


    }

}
}