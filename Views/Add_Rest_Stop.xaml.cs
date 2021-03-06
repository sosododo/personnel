using personnel.Models;
using personnel.ModelView;
using System;
using System.Collections;
using System.Collections.Generic;
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
    /// Interaction logic for Add_Rest_Stop.xaml
    /// </summary>
    public partial class Add_Rest_Stop : UserControl
    {

        PersonelDBContext db;
        static string res_per;
        
        public Add_Rest_Stop()
        {
            InitializeComponent();
          
            db = new PersonelDBContext();
            if (Login.currentUser.Rule == "تدريسي")
            {

                List<string> employ = db.SelfCards.Where(x => x.FileClass == "تدريسي").Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();

                emp_name.ItemsSource = employ;
            }
            else if (Login.currentUser.Rule == "فني")
            {

                List<string> employ = db.SelfCards.Where(x => x.FileClass == "فني").Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();

                emp_name.ItemsSource = employ;
            }
            else if (Login.currentUser.Rule == "معيد")
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

                List<string> employ = db.SelfCards.Where(x => x.Category == "الثانية/اداريين" || x.Category == "الثانية/مخبريين").Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();

                emp_name.ItemsSource = employ;
            }
            else if (Login.currentUser.Rule == "الثالثة")
            {

                List<string> employ = db.SelfCards.Where(x => x.Category == "الثالثة" || x.Category == "الرابعة").Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();

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

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelfCard person = new SelfCard();
            var emp_id = (from d in db.SelfCards select new { d.PersonId, full = d.FirstName + " " + d.FatherName + " " + d.LastName }).ToList();

            var id = emp_id.Where(d => d.full == emp_name.Text).ToList().ElementAt(0);

            long empId = id.PersonId;

            Decision des = new Decision();
            
            int number = Int32.Parse(num_Copy.Text);
            des = db.Decisions.Where(x => x.DecisionNumber == number && x.DecisionYear == Int32.Parse(year.Text) && x.DecisionSource == sorce.Text).FirstOrDefault();
            if (des == null)
            {
                MessageBox.Show("هذا القرار غير موجود");

            }
            else
            {
                Rest r = new Rest();
                r = db.Rests.Where(x => x.DecisionId == des.DecisionId && x.PersonId == empId && x.RestType == "إجازة بلا أجر").FirstOrDefault();

                if (r == null)
                {
                    MessageBox.Show("لا يمكن تطبيق قرار قطع إجازة بلا أجر على الموظف المحدد");

                }
                else
                {
                    r.RestPeriod = Int32.Parse(period.Text);

                    if (res_per1.IsChecked == true)
                    { res_per = res_per1.Content.ToString(); }
                    if (res_per2.IsChecked == true)
                    { res_per = res_per2.Content.ToString(); }
                    if (res_per3.IsChecked == true)
                    { res_per = res_per3.Content.ToString(); }

                    var dd = db.Decisions.Where(c => c.DecisionId == long.Parse(dec_id.Text)).Single();

                    r.RestEnd = dd.DecisionStart;
                    r.Period = res_per;
                     
                    db.Rests.Update(r);
                    db.SaveChanges();
                    ///////////////////////////////////////////////
                   person = db.SelfCards.Where(x => x.PersonId == empId).FirstOrDefault();
                    person.Status = "قائم على رأس عمله";
                    db.SelfCards.Update(person);
                    db.SaveChanges();

                    MessageBox.Show("تم تطبيق قرار قطع الإجازة بنجاح");


                    string message = "هل انتهى تنفيذ القرار؟";
                    string caption = "تنبيه";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Question);


                    if (result == MessageBoxResult.Yes)
                    {
                        res_per1.IsChecked = false;
                        res_per2.IsChecked = false;
                        res_per3.IsChecked = false;
                        num_Copy.Text = null;
                        year.Text = null;
                        sorce.Text = null;
                        emp_name.Text = null;
                        period.Text = null;
                        this.Visibility = Visibility.Collapsed;
                        var d = db.Decisions.Where(c => c.DecisionId == long.Parse(dec_id.Text)).Single();
                        excute.IsChecked = true;
                        // d.IsExcute = true;
                        d.IsExcute = true;

                        db.Decisions.Update(d);

                        db.SaveChanges();

                        Decision_View dv = new Decision_View();
                        Window parentWindow = Window.GetWindow(this);
                        parentWindow.Close();
                        dv.Show();




                    }
                    else if (result == MessageBoxResult.No)
                    {
                        res_per1.IsChecked = false;
                        res_per2.IsChecked = false;
                        res_per3.IsChecked = false;
                        num_Copy.Text = null;
                        year.Text = null;
                        sorce.Text = null;
                        emp_name.Text = null;
                        period.Text = null;

                    }



                }
            }

        }
    }
}
