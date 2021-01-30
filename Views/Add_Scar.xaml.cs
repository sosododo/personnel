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
    /// Interaction logic for Add_Scar.xaml
    /// </summary>
    public partial class Add_Scar : UserControl
    {
        private int _numValue = 0;
        PersonelDBContext db;
        long empId;
        static string del_per;
        public Add_Scar()
        {
            InitializeComponent();
            db = new PersonelDBContext();
            List<string> employ = db.SelfCards.Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();
            emp_name.ItemsSource = employ;
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
            try
            {

                var emp_id = (from d in db.SelfCards select new { d.PersonId, full = d.FirstName + " " + d.FatherName + " " + d.LastName }).ToList();

                var id = emp_id.Where(d => d.full == emp_name.Text).ToList().ElementAt(0);

                long empId = id.PersonId;
                // فحص اذا كان القرار مطبق مسبقا على هذا الموظف
                int c = db.Scars.Where(x => x.DecisionId == long.Parse(dec_id.Text) && x.PersonId == empId).Count();
                if (c > 0)
                {
                    MessageBox.Show("تم تطبيق هذا القرار مسبقا على الموظف المحدد");
                }
                else
                {
                    if (del_per1.IsChecked == true)
                    { del_per = del_per1.Content.ToString(); }
                    if (del_per2.IsChecked == true)
                    { del_per = del_per2.Content.ToString(); }
                    if (del_per3.IsChecked == true)
                    { del_per = del_per3.Content.ToString(); }

                    Scar del = new Scar
                    {
                        PersonId = empId,
                        DecisionId = long.Parse(dec_id.Text),
                      
                        PeriodType = del_per,
                        PeriodNum = Int32.Parse(perod.Text),
                        ScarReason = reason.Text,
                        ScarPlace = country.Text,
                        ScarStart = del_start.SelectedDate,
                        ScarEnd = del_end.SelectedDate,
                        Notes = note.Text
                    };
                    db.Scars.Add(del);
                    db.SaveChanges();



                    MessageBox.Show("تم إضافة تفصيل قرار الندب بنجاح");


                    string message = "هل انتهى تنفيذ القرار؟";
                    string caption = "تنبيه";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        del_per1.IsChecked = false;
                        del_per2.IsChecked = false;
                        del_per3.IsChecked = false;
                        del_start.Text = null;
                       
                        del_end.Text = null;
                        country.Text = "";
                        reason.Text = null;
                        
                        note.Text = "";
                        perod.Text = "";
                        this.Visibility = Visibility.Collapsed;
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
                    else if (result == MessageBoxResult.No)
                    {
                        del_per1.IsChecked = false;
                        del_per2.IsChecked = false;
                        del_per3.IsChecked = false;
                        del_start.Text = null;
                        
                        del_end.Text = null;
                        country.Text = "";
                        reason.Text = null;
                       
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
