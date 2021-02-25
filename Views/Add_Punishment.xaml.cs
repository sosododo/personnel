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
    /// Interaction logic for Add_Punishment.xaml
    /// </summary>
    public partial class Add_Punishment : UserControl
    {

        PersonelDBContext db;
        static string res_per;
        private int _numValue = 0;

        public Add_Punishment()
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

                List<string> employ = db.SelfCards.Where(x => x.FileClass == "عقود").Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try {


                var emp_id = (from d in db.SelfCards select new { d.PersonId, full = d.FirstName + " " + d.FatherName + " " + d.LastName }).ToList();

                var id = emp_id.Where(d => d.full == emp_name.Text).ToList().ElementAt(0);

                long empId = id.PersonId;
                int c = db.Punishments.Where(x => x.DecisionId == long.Parse(dec_id.Text) && x.PersonId == empId).Count();
                if (c > 0)
                {
                    MessageBox.Show("تم تطبيق هذا القرار مسبقا على الموظف المحدد");
                }

                else {


                    dec_excute();
                
                }
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void dec_excute()
        {
            var emp_id = (from d in db.SelfCards select new { d.PersonId, full = d.FirstName + " " + d.FatherName + " " + d.LastName }).ToList();

            var id = emp_id.Where(d => d.full == emp_name.Text).ToList().ElementAt(0);
            if (res_per1.IsChecked == true)
            { res_per = res_per1.Content.ToString(); }
            if (res_per2.IsChecked == true)
            { res_per = res_per2.Content.ToString(); }
            if (res_per3.IsChecked == true)
            { res_per = res_per3.Content.ToString(); }


            Punishment r = new Punishment
            {

                PersonId = id.PersonId,
                DecisionId = long.Parse(dec_id.Text),
                PunishmentType = p_type.Text,
                Period=Int32.Parse(  perod.Text),
                StartDate = p_start.SelectedDate,
                EndDate = p_end.SelectedDate,
                Reason = p_reason.Text,
                periodType=res_per,
                Discount=double.Parse( p_dis.Text),
                
                Notes = note.Text

            };
            db.Punishments.Add(r);
            db.SaveChanges();

            MessageBox.Show("تم إضافة الاجازة بنجاح");


            string message = "هل انتهى تنفيذ القرار؟";
            string caption = "تنبيه";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
             
                p_start.Text = null;
                p_end.Text = null;
                perod.Text = null;
                emp_name.Text = null;
                note.Text = "";
                p_reason.Text = null;
                p_dis.Text = null;
                p_type.Text = null;
                res_per1.IsChecked = false;
                res_per2.IsChecked = false;
                res_per3.IsChecked = false;
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
                p_start.Text = null;
                p_end.Text = null;
                perod.Text = null;
                emp_name.Text = null;
                note.Text = "";
                p_reason.Text = null;
                p_dis.Text = null;
                p_type.Text = null;
                res_per1.IsChecked = false;
                res_per2.IsChecked = false;
                res_per3.IsChecked = false;

            }
        }

        private void res_per2_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void p_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void p_type_DropDownClosed(object sender, EventArgs e)
        {
            if (p_type.Text == "حسم اجر")
            {
                per.Visibility = Visibility.Visible;
                res_per1.Visibility = Visibility.Visible;
                res_per2.Visibility = Visibility.Visible;
                res_per3.Visibility = Visibility.Visible;
                perod.Visibility = Visibility.Visible;
                des.Visibility = Visibility.Visible;
                p_dis.Visibility = Visibility.Visible;
                cmdDown_Copy2.Visibility = Visibility.Visible;
                cmdUp_Copy2.Visibility = Visibility.Visible;


            }
            else
            {

                per.Visibility = Visibility.Collapsed;
                res_per1.Visibility = Visibility.Collapsed;
                res_per2.Visibility = Visibility.Collapsed;
                res_per3.Visibility = Visibility.Collapsed;
                perod.Visibility = Visibility.Collapsed;
                des.Visibility = Visibility.Collapsed;
                p_dis.Visibility = Visibility.Collapsed;
                cmdDown_Copy2.Visibility = Visibility.Collapsed;
                cmdUp_Copy2.Visibility = Visibility.Collapsed;
                perod.Text = "0";
                p_dis.Text = "0";


            }
        }

        private void cmdUp_Copy2_Click(object sender, RoutedEventArgs e)
        {
            NumValue++;
        }

        private void cmdDown_Copy2_Click(object sender, RoutedEventArgs e)
        {
            NumValue--;
        }
    }
}
