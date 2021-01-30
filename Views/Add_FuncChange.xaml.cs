using Microsoft.EntityFrameworkCore;
using personnel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace personnel.Views
{
    /// <summary>
    /// Interaction logic for Add_FuncChange.xaml
    /// </summary>
    public partial class Add_FuncChange : UserControl
    {
        PersonelDBContext db;
        public Add_FuncChange()
        {
           
            InitializeComponent();

            db = new PersonelDBContext();
            //db.Works.Load();
            List<string> worp = db.Works.Select(w => w.WorkPlace).ToList();
            pp.ItemsSource = worp;


            pp.SelectedIndex = 0;
            List<string> employ = db.SelfCards.Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();
            emp_name.ItemsSource = employ;
            Decision d = (Decision)DataContext;

        }

        private void grade_DropDownClosed(object sender, EventArgs e)
        {
            //List<string> worp = db.Works.Select(w => w.WorkPlace).ToList();
            //pp.ItemsSource = worp;

            db.Jobs.Load();

            string cat = grade.Text;
            List<Job> job_titles = db.Jobs.Where(x => x.Category.Contains(cat)).ToList();
            job.ItemsSource = job_titles.Select(x => x.JobTitle);
            job.SelectedIndex = 0;
        }

        private void Add_Change(object sender, RoutedEventArgs e)
        {
            try
            {

                var emp_id = (from m in db.SelfCards select new { m.PersonId, full = m.FirstName + " " + m.FatherName + " " + m.LastName }).ToList();

                var id = emp_id.Where(d => d.full == emp_name.Text).ToList().ElementAt(0);

                long empId = id.PersonId;

                int c = db.FunctionalChanges.Where(x => x.DecisionId == long.Parse(dec_id.Text) && x.PersonId == empId).Count();
                if (c > 0)
                {
                    MessageBox.Show("تم تطبيق هذا القرار مسبقا على الموظف المحدد");
                }
                else
                {

                    FunctionalChange fn = new FunctionalChange
                    {
                        PersonId = empId,
                        DecisionId = int.Parse(dec_id.Text),
                        Category = grade.Text,
                        WorkPlace = pp.Text,
                        Mission = mission.Text,
                        ChangeDate = (DateTime)chandate1.SelectedDate,
                        Status = status.Text,
                        Salary = Int32.Parse(salary.Text),
                        JobTitle = job.Text







                    };
                    db.FunctionalChanges.Add(fn);
                    db.SaveChanges();
                    MessageBox.Show("تم إجراءالتبدل الوظيفي بنجاح...");

                    this.Visibility = Visibility.Collapsed;

                    var d = db.Decisions.Where(c => c.DecisionId == long.Parse(dec_id.Text)).Single();

                    d.IsExcute = true;
                    db.Decisions.Update(d);

                    db.SaveChanges();
                    emp_name.SelectedItem = null;
                    chandate1.SelectedDate = null;
                    pp.SelectedItem = null;
                    grade.SelectedItem = null;
                    job.SelectedItem = null;
                    mission.SelectedItem = null;
                    status.SelectedItem = null;
                    salary.Text = null;
                    this.Visibility = Visibility.Collapsed;
                    var dd = db.Decisions.Where(c => c.DecisionId == long.Parse(dec_id.Text)).Single();
                    excute.IsChecked = true;
                    // d.IsExcute = true;
                    dd.IsExcute = true;

                    db.Decisions.Update(dd);

                    db.SaveChanges();

                    Decision_View dv = new Decision_View();
                    Window parentWindow = Window.GetWindow(this);
                    parentWindow.Close();
                    dv.Show();




                }




                            }
            catch (Exception ex) {  MessageBox.Show("يجب التأكد من ادخال جميع البيانات"); }
        }
    }
}
