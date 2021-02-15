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
    /// Interaction logic for Add_Delegation.xaml
    /// </summary>
    public partial class Add_Delegation : UserControl
    {
        private int _numValue = 0;
        PersonelDBContext db;
        long empId;
        static string del_per;
        
        public Add_Delegation()
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
        private void insert_del(object sender, RoutedEventArgs e)
        {
            try
            {




                var emp_id = (from d in db.SelfCards select new { d.PersonId, full = d.FirstName + " " + d.FatherName + " " + d.LastName }).ToList();

                var id = emp_id.Where(d => d.full == emp_name.Text).ToList().ElementAt(0);

                long empId = id.PersonId;
                // فحص اذا كان القرار مطبق مسبقا على هذا الموظف
                int c = db.Delegatings.Where(x => x.DecisionId == long.Parse(dec_id.Text) && x.PersonId == empId).Count();
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

                    Delegating del = new Delegating
                    {
                        PersonId = empId,
                        DecisionId = long.Parse(dec_id.Text),
                        DelegatingType = del_type.Text,
                        PeriodType = del_per,
                        PeriodNum = Int32.Parse(perod.Text),
                        DelegatingReason = reson.Text,
                        DelegatingCountry = country.Text,
                        DelegatingStart = del_start.SelectedDate,
                        DelegatingEnd = del_end.SelectedDate,
                        Notes = note.Text
                    };
                    db.Delegatings.Add(del);
                    db.SaveChanges();
                    if (del_type.Text == "إيفاد داخلي")
                    {
                        SelfCard person = new SelfCard();
                        person = db.SelfCards.Where(x => x.PersonId == empId).FirstOrDefault();
                        person.Status = "موفد داخلياً";
                        db.SelfCards.Update(person);
                        db.SaveChanges();
                    }
                    else if (del_type.Text == "إيفاد خارجي")
                    {
                        SelfCard person = new SelfCard();
                        person = db.SelfCards.Where(x => x.PersonId == empId).FirstOrDefault();
                        person.Status = "موفد خارجياً";
                        db.SelfCards.Update(person);
                        db.SaveChanges();

                    }

          

                    MessageBox.Show("تم إضافة تفصيل الايفاد بنجاح");


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
                        del_type.Text = null;
                        del_end.Text = null;
                        country.Text = "";
                        reson.Text = null;
                        
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
                        //Window parentWindow = Window.GetWindow(this);
                        //object obj = parentWindow.FindName("result");
                        //DataGrid dg = (DataGrid)obj;
                        //dg.UnselectAll();

                    }
                    else if (result == MessageBoxResult.No)
                    {
                        del_per1.IsChecked = false;
                        del_per2.IsChecked = false;
                        del_per3.IsChecked = false;
                        del_start.Text = null;
                        del_type.Text = null;
                        del_end.Text = null;
                        country.Text = "";
                        reson.Text = null;
                        del_type.Text = null;
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

        private void emp_name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    }

  