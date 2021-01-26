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
    /// Interaction logic for Add_Reward.xaml
    /// </summary>
    public partial class Add_Reward : UserControl
    {
        PersonelDBContext db;
        public Add_Reward()
        {
            InitializeComponent();
            db = new PersonelDBContext();
            List<string> employ = db.SelfCards.Select(x => x.FirstName + " " + x.FatherName + " " + x.LastName).ToList();
            emp_name.ItemsSource = employ;
            Decision d = (Decision)DataContext;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try {


                var emp_id = (from d in db.SelfCards select new { d.PersonId, full = d.FirstName + " " + d.FatherName + " " + d.LastName }).ToList();

                var id = emp_id.Where(d => d.full == emp_name.Text).ToList().ElementAt(0);

                long empId = id.PersonId;
                int c = db.Rewards.Where(x => x.DecisionId == long.Parse(dec_id.Text) && x.PersonId == empId).Count();
                if (c > 0)
                {
                    MessageBox.Show("تم تطبيق هذا القرار مسبقا على الموظف المحدد");
                }
                else {
                    excute();
                
                }
            }

            catch (Exception ex) { MessageBox.Show("يجب التأكد من ادخال جميع البيانات"); }


        }

        public void excute() {

            var emp_id = (from d in db.SelfCards select new { d.PersonId, full = d.FirstName + " " + d.FatherName + " " + d.LastName }).ToList();

            var id = emp_id.Where(d => d.full == emp_name.Text).ToList().ElementAt(0);




            Reward r = new Reward
            {

                PersonId = id.PersonId,
                DecisionId = long.Parse(dec_id.Text),
                RewardType = rew_type.Text,
                Reason = reason.Text,
                RewardDate = res_start.SelectedDate,
                Amount=amount.Text,
                Notes=note.Text
               

            };
            db.Rewards.Add(r);
            db.SaveChanges();

            MessageBox.Show("تم إضافة الاجازة بنجاح");


            string message = "هل انتهى تنفيذ القرار؟";
            string caption = "تنبيه";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButton.YesNo,
                                         MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                
                res_start.Text = null;
                res_type.Text = null;
                
                emp_name.Text = null;
                
                note.Text = "";
                rew_type.Text = null;
                reason.Text = null;
                this.Visibility = Visibility.Collapsed;
                var d = db.Decisions.Where(c => c.DecisionId == long.Parse(dec_id.Text)).Single();

                //   excute.IsChecked = true;
                // d.IsExcute = true;
                excutee.IsChecked = true;
                d.IsExcute = true;

                db.Decisions.Update(d);

                db.SaveChanges();






            }
            else if (result == MessageBoxResult.No)
            {
                res_start.Text = null;
                res_type.Text = null;

                emp_name.Text = null;

                note.Text = "";
                rew_type.Text = null;
                reason.Text = null;
            }


        }
    }
}
