using personnel.Models;
using personnel.ModelView;
using personnel.Report;
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
    /// Interaction logic for Add_Rest.xaml
    /// </summary>
    public partial class Add_Rest : UserControl
    { PersonelDBContext db;
        static string res_per;
        private int _numValue = 0;

        public Add_Rest()
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
            Decision d = (Decision)DataContext;

        }

        public int NumValue
        {
            get { return _numValue; }
            set
            {
                _numValue = value;
                perod.Text = value.ToString();
            }
        }
        private void insert_rest(object sender, RoutedEventArgs e)
        {
            try
            {
              

                    var emp_id = (from d in db.SelfCards select new { d.PersonId, full = d.FirstName + " " + d.FatherName + " " + d.LastName,d.Salary,d.Workplace,d.JobTitle,d.Category,d.Register }).ToList();

                   var  id = emp_id.Where(d => d.full == emp_name.Text).ToList().ElementAt(0);

                    long empId = id.PersonId;
                    int c = db.Rests.Where(x => x.DecisionId == long.Parse(dec_id.Text) && x.PersonId == empId).Count();
                    if (c > 0)
                    {
                        MessageBox.Show("تم تطبيق هذا القرار مسبقا على الموظف المحدد");
                    }
                    else
                    {

                      



                        EmpRest er = new EmpRest();
                    if (res_type.Text == "إجازة ادارية")
                    {
                        int m = er.canrest(empId);
                        int can = m - (Int32.Parse(perod.Text));

                        if (can < 0)
                        {
                            MessageBox.Show("لا يحق لهذا الموظف العدد المطلوب من الاجازات الادارية. عدد الأيام المسموح به  " + m.ToString());
                        }
                        else

                        {
                            dec_excute();

                        }




                    }
                    else if (res_type.Text == "إجازة بلا أجر")
                    {  
                        int sum = er.calculate(empId);
                        int can = restcount();

                        if ((sum + can) > (5 * 360))
                        {
                            MessageBox.Show("لا يحق لهذا الموظف العدد المطلوب من الاجازات بلا أجر. عدد الأيام المسموح به   " +"  "+ ((360 * 5) - sum).ToString());
                        }
                        else

                        {
                            SelfCard person = new SelfCard();
                            person = db.SelfCards.Where(x => x.PersonId == empId).FirstOrDefault();
                            if(int.Parse(perod.Text)>=90)
                             person.Status = "بلا أجر";
                            db.SelfCards.Update(person);
                            db.SaveChanges();
                            dec_excute();
                            //RestReport rs = new RestReport();
                            RestReport rs = new RestReport(long.Parse(dec_id.Text));

                        }
                    }

                    else if (res_type.Text == "إجازة صحية") { dec_excute(); }
                    else if (res_type.Text == "احتياط") {
                        SelfCard person = new SelfCard();
                        person = db.SelfCards.Where(x => x.PersonId == empId).FirstOrDefault();
                        person.Status = "احتياط";
                        db.SelfCards.Update(person);
                        db.SaveChanges();

                        dec_excute(); }
                    else if (res_type.Text == "الزامي") {
                        SelfCard person = new SelfCard();
                        person = db.SelfCards.Where(x => x.PersonId == empId).FirstOrDefault();
                        person.Status = "خدمة الزامية";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                        dec_excute(); }
                    else if (res_type.Text == "دراسية بتمام الأجر") {

                        SelfCard person = new SelfCard();
                        person = db.SelfCards.Where(x => x.PersonId == empId).FirstOrDefault();
                        person.Status = "اجازة دراسية بتمام الأجر";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                        dec_excute(); }
                    else if (res_type.Text == "دراسية بلا أجر") {

                        int mothetcount = db.Rests.Where(x => x.PersonId == empId && x.RestType == "دراسية بلا أجر").Count();
                        MessageBox.Show("عدد الاجازات الدراسية السابقة لهذا الموظف"+ "  "+ mothetcount);
                        if (mothetcount == 2) {
                            MessageBox.Show("لا يحق لهذا الموظف اجازة دراسية بلا اجر");

                        }
                        else {

                            SelfCard person = new SelfCard();
                            person = db.SelfCards.Where(x => x.PersonId == empId).FirstOrDefault();
                            person.Status = "اجازة دراسية بلا أجر";
                            db.SelfCards.Update(person);
                            db.SaveChanges();
                            dec_excute();
                        }

                     }
                    else if (res_type.Text == "حج") { dec_excute(); }
                    else if (res_type.Text == "أمومة") {

                  int mothetcount=      db.Rests.Where(x => x.PersonId == empId && x.RestType == "أمومة").Count();

                        MessageBox.Show("عدد اجازات الامومة السابقة لهذا الموظف:   " + mothetcount);
                        if (mothetcount == 3)
                        {
                            MessageBox.Show("لا يحق لهذا الموظف اجازة أمومة");

                        }
                        else
                        {

                            dec_excute();
                        }
                    }
                    else if (res_type.Text == "استكمال أمومة") {

                        int mothetcount = db.Rests.Where(x => x.PersonId == empId && x.RestType == "استكمال أمومة").Count();

                        MessageBox.Show("عدد اجازات استكمال الامومة السابقة لهذا الموظف:   " + mothetcount);
                        if (mothetcount == 3)
                        {
                            MessageBox.Show("لا يحق لهذا الموظف اجازة استكمال أمومة");

                        }
                        else
                        {

                            dec_excute();
                        }
                      }
                    else if (res_type.Text == "زواج") { dec_excute(); }
                    else if (res_type.Text == "وفاة") { dec_excute(); }
                }


 
            }





            catch (Exception ex) { MessageBox.Show("يجب التأكد من ادخال جميع البيانات"); }
            
        }
     
        public long getid(Decision dec)
        {
            long dec_id = dec.DecisionId;
            return dec_id;
        }

        public void dec_excute() {
            var emp_id = (from d in db.SelfCards select new { d.PersonId, full = d.FirstName + " " + d.FatherName + " " + d.LastName,d.Salary,d.Workplace,d.JobTitle,d.Category,d.Register }).ToList();

            var id = emp_id.Where(d => d.full == emp_name.Text).ToList().ElementAt(0);

            if (res_per1.IsChecked == true)
            { res_per = res_per1.Content.ToString(); }
            if (res_per2.IsChecked == true)
            { res_per = res_per2.Content.ToString(); }
            if (res_per3.IsChecked == true)
            { res_per = res_per3.Content.ToString(); }


            Rest r = new Rest
            {

                PersonId = id.PersonId,
                DecisionId = long.Parse(dec_id.Text),
                RestType = res_type.Text,
                RestStart = res_start.SelectedDate,
                RestEnd = res_end.SelectedDate,
                Period = res_per,
                RestPeriod = Int32.Parse(perod.Text),
                Attachment = att.Text,
                Notes = note.Text,
                Salary=id.Salary,Workplace=id.Workplace,JobTitle=id.JobTitle,Category=id.Category,Register=id.Register

            };
            db.Rests.Add(r);
            db.SaveChanges();

            MessageBox.Show("تم إضافة الاجازة بنجاح");


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
                res_start.Text = null;
                res_type.Text = null;
                res_end.Text = null;
                emp_name.Text = null;
                att.Text = "";
                note.Text = "";
                perod.Text = "";
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
                res_start.Text = null;
                res_type.Text = null;
                res_end.Text = null;
                emp_name.Text = null;
                att.Text = "";
                note.Text = "";
                perod.Text = "";
            }
        }

        public int restcount()
             
        {

            if (res_per1.IsChecked == true)
            { res_per = res_per1.Content.ToString(); }
            if (res_per2.IsChecked == true)
            { res_per = res_per2.Content.ToString(); }
            if (res_per3.IsChecked == true)
            { res_per = res_per3.Content.ToString(); }
            int c=0;
            if (res_per == "يوم")
            { c = Int32.Parse(perod.Text); }
            else if (res_per == "شهر")
            { c = (Int32.Parse(perod.Text) * 30); }
            else if (res_per == "سنة")
            { c = (Int32.Parse(perod.Text) * 360); }

          return c  ;
        }

        private void cmdDown_Copy2_Click(object sender, RoutedEventArgs e)
        {
            NumValue--;
        }

        private void cmdUp_Copy2_Click(object sender, RoutedEventArgs e)
        {
            NumValue++;
        }

        private void res_type_DropDownClosed(object sender, EventArgs e)
        {

            var emp_id = (from d in db.SelfCards select new { d.PersonId, full = d.FirstName + " " + d.FatherName + " " + d.LastName }).ToList();

            var id = emp_id.Where(d => d.full == emp_name.Text).ToList().ElementAt(0);

            long empId = id.PersonId;
            if (res_type.Text == "أمومة")
            {

                int mothetcount = db.Rests.Where(x => x.PersonId == empId && x.RestType == "أمومة").Count();
                if (mothetcount == 0)
                {
                    perod.Text = "120";
                    res_per1.IsChecked = true;
                }

                if (mothetcount == 1)
                {
                    perod.Text = "90";
                    res_per1.IsChecked = true;
                }

                if (mothetcount == 2)
                {
                    perod.Text = "75";
                    res_per1.IsChecked = true;
                }

                if (mothetcount == 3)
                {
                    MessageBox.Show("لا يحق لهذا الموظف اجازة أمومة");

                }
            }

            if (res_type.Text == "استكمال أمومة") {

                int mothetcount = db.Rests.Where(x => x.PersonId == empId && x.RestType == "استكمال أمومة").Count();
                if (mothetcount == 0)
                {
                    perod.Text = "1";
                    res_per2.IsChecked = true;
                }

                if (mothetcount == 1)
                {
                    perod.Text = "1";
                    res_per2.IsChecked = true;
                }

                if (mothetcount == 2)
                {
                    perod.Text = "1";
                    res_per2.IsChecked = true;
                }

                if (mothetcount == 3)
                {
                    MessageBox.Show("لا يحق لهذا الموظف اجازة استكمال أمومة");

                }


            }
            if (res_type.Text == "دراسية بلا أجر") {
                int mothetcount = db.Rests.Where(x => x.PersonId == empId && x.RestType == "دراسية بلا أجر").Count();
                if (mothetcount <2)
                {
                    perod.Text = "1";
                    res_per3.IsChecked = true;
                }
                if (mothetcount ==2)
                {
                    MessageBox.Show(" لا يحق لهذا الموظف اجازة دراسية بلا اجر ");
                }

            }
            if (res_type.Text == "إجازة ادارية")
            {

                res_per2.Visibility = Visibility.Hidden;
                res_per3.Visibility = Visibility.Hidden;
                
            
            
            }

            //if (res_type.Text == "قطع بلا أجر") {


                //    //text1.Visibility = Visibility.Collapsed;
                //    //perod.Visibility = Visibility.Collapsed;
                //    //cmdDown_Copy2.Visibility = Visibility.Collapsed;
                //    //cmdUp_Copy2.Visibility = Visibility.Collapsed;
                //    //res_per1.Visibility = Visibility.Collapsed;
                //    //res_per2.Visibility = Visibility.Collapsed;
                //    //res_per3.Visibility = Visibility.Collapsed;
                //    //sta.Visibility = Visibility.Collapsed;
                //    //res_start.Visibility = Visibility.Collapsed;
                //    //en.Visibility = Visibility.Collapsed;
                //    //res_end.Visibility = Visibility.Collapsed;
                //    //att.Visibility = Visibility.Collapsed;
                //    //at.Visibility = Visibility.Collapsed;
                //    //not.Visibility = Visibility.Collapsed;
                //    //note.Visibility = Visibility.Collapsed;
                //}
        }
    }
}