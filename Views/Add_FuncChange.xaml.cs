﻿using Microsoft.EntityFrameworkCore;
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
            if (Login.currentUser.Rule == "تدريسي") {
                
                List<string> employ = db.SelfCards.Where(x=> x.FileClass== "تدريسي").Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();
            
                emp_name.ItemsSource = employ;
            }
          else  if (Login.currentUser.Rule == "فني")
            {

                List<string> employ = db.SelfCards.Where(x => x.FileClass == "فني").Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();

                emp_name.ItemsSource = employ;
            }
          else  if (Login.currentUser.Rule == "معيد")
            {

                List<string> employ = db.SelfCards.Where(x => x.FileClass == "معيد").Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();

                emp_name.ItemsSource = employ;
            }

            else if (Login.currentUser.Rule == "الأولى")
            {

                List<string> employ = db.SelfCards.Where(x => x.Category == "الأولى").Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();

                emp_name.ItemsSource = employ;
            }
            else if (Login.currentUser.Rule == "الثانية")
            {

                List<string> employ = db.SelfCards.Where(x => x.Category == "الثانية/اداريين" || x.Category== "الثانية/مخبريين").Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();

                emp_name.ItemsSource = employ;
            }
            else if (Login.currentUser.Rule == "الثالثة")
            {

                List<string> employ = db.SelfCards.Where(x => x.Category == "الثالثة" || x.Category=="الرابعة").Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();

                emp_name.ItemsSource = employ;
            }
            else if (Login.currentUser.Rule == "الخامسة")
            {

                List<string> employ = db.SelfCards.Where(x => x.Category == "الخامسة").Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();

                emp_name.ItemsSource = employ;
            }
            else if (Login.currentUser.Rule == "عقود")
            {

                List<string> employ = db.SelfCards.Where(x => x.FileClass == "عقد").Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();

                emp_name.ItemsSource = employ;
            }
            else if (Login.currentUser.Rule == "admin")
            {

                List<string> employ = db.SelfCards.Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();

                emp_name.ItemsSource = employ;
            }

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
                        person.InsuranceCard = "ضمن الخدمة";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                       // status.Text = "قائم على رأس عمله";
                    }

                    if (mission_Copy.SelectedItem == null)
                    {
                        MessageBox.Show("ادخل نوع التبدل الوظيفي من فضلك");
                    }
                    else if (mission_Copy.Text == "استقالة") {
                        status.Text = "مستقيل";
                        person.Status = "مستقيل";
                        person.InsuranceCard = "خارج الخدمة";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "تعيين")
                    {
                        status.Text = "قائم على رأس عمله";
                        person.Status = "قائم على رأس عمله";
                        person.Salary =double.Parse(salary.Text);
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "نهاية خدمة") {
                        status.Text = "متقاعد";
                        person.Status = "متقاعد";
                        person.InsuranceCard = "خارج الخدمة";
                        db.SelfCards.Update(person);
                        db.SaveChanges();

                    }
                    else if (mission_Copy.Text == "تجميد") {
                        status.Text = "مجمد";
                        person.Status = "مجمد";
                        person.InsuranceCard = "خارج الخدمة";
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
                        person.InsuranceCard = "خارج الخدمة";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "صرف من الخدمة")
                    {
                       status.Text = "مصروف من الخدمة";
                        person.Status = "مصروف من الخدمة";
                        person.InsuranceCard = "خارج الخدمة";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "طي اسم")
                    {
                      status.Text = "متوفى";
                        person.Status = "متوفى";
                        person.InsuranceCard = "خارج الخدمة";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "احالة على المعاش")
                    {
                        status.Text= "متقاعد";
                        person.Status = "متقاعد";
                        person.InsuranceCard = "خارج الخدمة";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "تسريح صحي")
                    {
                        status.Text = "مسرح صحياً";
                        person.Status = "مسرح صحياً";
                        person.InsuranceCard = "خارج الخدمة";
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

                    else if (mission_Copy.Text == "نقل خارج الجامعة")
                    {
                        pp.Text = "";
                        person.Status = "منقول خارج الجامعة";
                        person.InsuranceCard = "خارج الخدمة";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (mission_Copy.Text == "اجازة بلا أجر")
                    {

                        status.Text = "بلا أجر";
                        person.Status = "بلا أجر";
                        person.InsuranceCard = "خارج الخدمة";

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
                        Salary = double.Parse(salary.Text),
                        JobTitle = job.Text,Register= Login.regName

                    };
                    db.FunctionalChanges.Add(fn);
                    db.SaveChanges();
                    MessageBox.Show("تم إجراءالتبدل الوظيفي بنجاح...");


                    string message = "هل انتهى تنفيذ القرار؟";
                    string caption = "تنبيه";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
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
                        mission_Copy.SelectedItem = null;
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
                    else if (result == MessageBoxResult.No)
                    {
                        emp_name.SelectedItem = null;
                        chandate1.SelectedDate = null;
                        pp.SelectedItem = null;
                        grade.SelectedItem = null;
                        job.SelectedItem = null;
                        mission.SelectedItem = null;
                        status.SelectedItem = null;
                        salary.Text = null;
                        mission_Copy.SelectedItem = null;
                        
                    }



                }




                            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                // MessageBox.Show("يجب التأكد من ادخال جميع البيانات");
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void emp_name_DropDownClosed(object sender, EventArgs e)
        {
            
            var emp_id = (from m in db.SelfCards select new { m.PersonId, full = m.FirstName + " " + m.FatherName + " " + m.LastName,m.Category,m.JobTitle,m.Mission,m.Status,m.Workplace,m.Salary }).ToList();

            var id = emp_id.Where(d => d.full == emp_name.Text).ToList().FirstOrDefault();
            if (id != null)
            {
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
        }

        private void mission_Copy_DropDownClosed(object sender, EventArgs e)
        {
            if (mission_Copy.Text == "اعادة إلى العمل")
            {
                status.Text = "قائم على رأس عمله";
               
                // status.Text = "قائم على رأس عمله";
            }
            if (mission_Copy.Text == "تعيين")
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

            else if (mission_Copy.Text == "نقل خارج الجامعة")
            {
                status.Text = "منقول خارج الجامعة";

            }
            else if (mission_Copy.Text == "اجازة بلا أجر")
            {

                status.Text = "بلا أجر";
          
            }


        }
    }
}
