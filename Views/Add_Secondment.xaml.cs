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
    /// Interaction logic for Add_Secondment.xaml
    /// </summary>
    public partial class Add_Secondment : UserControl
    {

        PersonelDBContext db;
        static string sec_per;
        private int _numValue = 0;
        public Add_Secondment()
        {
            InitializeComponent();
            db = new PersonelDBContext();
            List<string> employ = db.SelfCards.Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();
            emp_name.ItemsSource = employ;
        //    Decision d = (Decision)DataContext;
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
                int c = db.Secondments.Where(x => x.DecisionId == long.Parse(dec_id.Text) && x.PersonId == empId).Count();
                if (c > 0)
                {
                    MessageBox.Show("تم تطبيق هذا القرار مسبقا على الموظف المحدد");
                }
                else {

                    if (sec_per1.IsChecked == true)
                    { sec_per = sec_per1.Content.ToString(); }
                    if (sec_per2.IsChecked == true)
                    { sec_per = sec_per2.Content.ToString(); }
                    if (sec_per3.IsChecked == true)
                    { sec_per = sec_per3.Content.ToString(); }


                    Secondment r = new Secondment
                    {

                        PersonId = id.PersonId,
                        DecisionId = long.Parse(dec_id.Text),
                        SecondmentType = sec_type.Text,
                        PeriodNum = Int32.Parse(perod.Text),
                        PeriodType = sec_per,
                        SecondmentPlace= place.Text,
                        SecondmentStart = sec_start.SelectedDate,
                        SecondmentEnd=sec_end.SelectedDate,
                        Notes=note.Text
                      

                    };
                    db.Add(r);
                    db.SaveChanges();
                    if (sec_type.Text == "إعارة داخلية")
                    {
                        SelfCard person = new SelfCard();
                        person = db.SelfCards.Where(x => x.PersonId == empId).FirstOrDefault();
                        person.Status = "معار داخلياً";
                        db.SelfCards.Update(person);
                        db.SaveChanges();

                    }
                    else if (sec_type.Text == "إعارة خارجية") {
                        SelfCard person = new SelfCard();
                        person = db.SelfCards.Where(x => x.PersonId == empId).FirstOrDefault();
                        person.Status = "معار خارجياً";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    MessageBox.Show("تم تنفيذ قرار الاعارة");

                    string message = "هل انتهى تنفيذ القرار؟";
                    string caption = "تنبيه";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Question);


                    if (result == MessageBoxResult.Yes)
                    {
                        sec_per1.IsChecked = false;
                        sec_per2.IsChecked = false;
                        sec_per3.IsChecked = false;
                        sec_start.Text = null;
                        sec_type.Text = null;
                        sec_end.Text = null;
                        emp_name.Text = null;
                        place.Text = null;
                      
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
                        sec_per1.IsChecked = false;
                        sec_per2.IsChecked = false;
                        sec_per3.IsChecked = false;
                        sec_start.Text = null;
                        sec_type.Text = null;
                        sec_end.Text = null;
                        emp_name.Text = null;
                        place.Text = null;
                        note.Text = "";
                        perod.Text = "";

                     
                    }



                }



            }
            catch (Exception ex) { MessageBox.Show("يجب التأكد من ادخال جميع البيانات"); }
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
