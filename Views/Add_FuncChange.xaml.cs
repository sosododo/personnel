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
                    SelfCard person = new SelfCard();
                    person = db.SelfCards.Where(x => x.PersonId == empId).FirstOrDefault();
                    if (mission_Copy.Text == "اعادة إلى العمل") {
                        status.Text = "قائم على رأس عمله";
                        person.Status = "قائم على رأس عمله";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                       // status.Text = "قائم على رأس عمله";
                    }
                    else if (mission_Copy.Text == "استقالة") {
                        status.Text = "مستقيل";
                        person.Status = "مستقيل";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "نهاية خدمة") {
                        status.Text = "متقاعد";
                        person.Status = "متقاعد";
                        db.SelfCards.Update(person);
                        db.SaveChanges();

                    }
                    else if (mission_Copy.Text == "تجميد") {
                        status.Text = "مجمد";
                        person.Status = "مجمد";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    
                    }
                    else if (mission_Copy.Text == "تفريغ للحزب") {
                        status.Text = "مفرغ للحزب";
                        person.Status = "مفرغ للحزب";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "بحكم المستقيل")
                    {
                        status.Text = "بحكم المستقيل";
                        person.Status = "بحكم المستقيل";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "صرف من الخدمة")
                    {
                       status.Text = "مصروف من الخدمة";
                        person.Status = "مصروف من الخدمة";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "طي اسم")
                    {
                      status.Text = "متوفى";
                        person.Status = "متوفى";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "احالة على المعاش")
                    {
                        status.Text= "متقاعد";
                        person.Status = "متقاعد";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "تسريح صحي")
                    {
                        status.Text = "مسرح صحياً";
                        person.Status = "مسرح صحياً";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "كف يد")
                    {
                        status.Text = "كف اليد";
                        person.Status = "كف اليد";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
               
                    else if (mission_Copy.Text == "تغيير فئة")
                    {
                        grade.Text = "";
                        person.Category = grade.Text;
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "تغيير مسمى وظيفي")
                    {
                        job.Text = "";
                        person.JobTitle = job.Text;
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "تغيير مهمة")
                    {
                        mission.Text = "";
                        person.Mission = mission.Text;
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "نقل")
                    {
                        pp.Text = "";
                        person.Workplace = pp.Text;
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "اجازة بلا أجر")
                    {

                        status.Text = "بلا أجر";
                        person.Status = "بلا أجر";
                       
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    

                    FunctionalChange fn = new FunctionalChange
                    {
                        PersonId = empId,
                        DecisionId = int.Parse(dec_id.Text),
                        Category = grade.Text,
                        WorkPlace = pp.Text,
                        functional_changes_type=mission_Copy.Text,
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void emp_name_DropDownClosed(object sender, EventArgs e)
        {
            
            var emp_id = (from m in db.SelfCards select new { m.PersonId, full = m.FirstName + " " + m.FatherName + " " + m.LastName,m.Category,m.JobTitle,m.Mission,m.Status,m.Workplace,m.Salary }).ToList();

            var id = emp_id.Where(d => d.full == emp_name.Text).ToList().ElementAt(0);
            long empId = id.PersonId;
            SelfCard person = new SelfCard();
            person = db.SelfCards.Where(x => x.PersonId == empId).FirstOrDefault();
          
            grade.Text = person.Category;
            job.Text = person.JobTitle;
            mission.Text = person.Mission;
            salary.Text = person.Salary.ToString();
            status.Text = person.Status;
            pp.Text = person.Workplace;
        }

        private void mission_Copy_DropDownClosed(object sender, EventArgs e)
        {
            if (mission_Copy.Text == "اعادة إلى العمل")
            {
                status.Text = "قائم على رأس عمله";
               
                // status.Text = "قائم على رأس عمله";
            }
            else if (mission_Copy.Text == "استقالة")
            {
                status.Text = "مستقيل";
                
            }
            else if (mission_Copy.Text == "نهاية خدمة")
            {
                status.Text = "متقاعد";
              

            }
            else if (mission_Copy.Text == "تجميد")
            {
                status.Text = "مجمد";
             

            }
            else if (mission_Copy.Text == "تفريغ للحزب")
            {
                status.Text = "مفرغ للحزب";
              
            }
            else if (mission_Copy.Text == "بحكم المستقيل")
            {
                status.Text = "بحكم المستقيل";
             
            }
            else if (mission_Copy.Text == "صرف من الخدمة")
            {
                status.Text = "مصروف من الخدمة";
              
            }
            else if (mission_Copy.Text == "طي اسم")
            {
                status.Text = "متوفى";
                
            }
            else if (mission_Copy.Text == "احالة على المعاش")
            {
                status.Text = "متقاعد";
              
            }
            else if (mission_Copy.Text == "تسريح صحي")
            {
                status.Text = "مسرح صحياً";
          
               
            }
            else if (mission_Copy.Text == "كف يد")
            {
                status.Text = "كف اليد";
            
            }

            else if (mission_Copy.Text == "تغيير فئة")
            {
                grade.Text = "";
               
            }
            else if (mission_Copy.Text == "تغيير مسمى وظيفي")
            {
                job.Text = "";
                
            }
            else if (mission_Copy.Text == "تغيير مهمة")
            {
                mission.Text = "";
              
            }
            else if (mission_Copy.Text == "نقل")
            {
                pp.Text = "";
             
            }
            else if (mission_Copy.Text == "اجازة بلا أجر")
            {

                status.Text = "بلا أجر";
          
            }


        }
    }
}
