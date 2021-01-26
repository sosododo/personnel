using personnel.Models;
using personnel.ModelView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Add_Bonus.xaml
    /// </summary>
    public partial class Add_Bonus : UserControl
    {
        private int _numValueSal = 0;
        private int _numValueBon = 0;
        private int _numValueDay = 0;
        private int _numValueSalBon = 0;
        PersonelDBContext db;
        List<string> emp;
        public Add_Bonus()
        {
            InitializeComponent();
            db = new PersonelDBContext();


            emp = (from p in db.SelfCards
                   where p.Status == "قائم على رأس عمله"
                   select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();
   
 
           list.ItemsSource = emp;
            Decision d = (Decision)DataContext;
        }

        public int NumValueSal
        {
            get { return _numValueSal; }
            set
            {
                _numValueSal = value;
                sal.Text = value.ToString();
            }
        }
        public int NumValueBon
        {
            get { return _numValueBon; }
            set
            {
                _numValueBon = value;
                bon.Text = value.ToString();
            }
        }

        public int NumValueDay
        {
            get { return _numValueDay; }
            set
            {
                _numValueDay= value;
                day.Text = value.ToString();
            }
        }
        public int NumValueSalBon
        {
            get { return _numValueSalBon; }
            set
            {
                _numValueSalBon = value;
                sal_bonus.Text = value.ToString();
            }
        }
        private void insert_rest(object sender, RoutedEventArgs e)
        {
            try {

                excutee();
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void excutee() { 
            var emp_id = (from d in db.SelfCards
            where d.Status == "قائم على رأس عمله"
            select new { d.PersonId, full = d.FirstName + " " + d.FatherName + " " + d.LastName }).ToList();




            foreach (string ss in emp.ToList())
            {

                var id = emp_id.Where(d => d.full == ss).ToList().ElementAt(0);
                long eid = id.PersonId;
                int c = db.Bonuses.Where(x => x.DecisionId == long.Parse(dec_id.Text) && x.PersonId == eid).Count();
                if (c > 0)
                {
                    MessageBox.Show("  تم تطبيق هذا القرار على الموظف  " + id.full);
                }
                else
                {
                    Bonuse r = new Bonuse
                    {

                        PersonId = id.PersonId,
                        DecisionId = long.Parse(dec_id.Text),
                        Salary = Int32.Parse(sal.Text),
                        Bouns = Int32.Parse(bon.Text),
                        SalaryBouns = Int32.Parse(sal_bonus.Text),
                        NumDays = Int32.Parse(day.Text)



                    };
                    db.Bonuses.Add(r);
                    db.SaveChanges();
                    var d = db.Decisions.Where(c => c.DecisionId == long.Parse(dec_id.Text)).Single();
                    excute.IsChecked = true;
                    // d.IsExcute = true;
                    d.IsExcute = true;

                    db.Decisions.Update(d);

                    db.SaveChanges();



                }

                }
            list.SelectedItem = null;
            sal.Text = null;
            sal_bonus.Text = null;
            bon.Text = null;
            day.Text = null;

            this.Visibility = Visibility.Collapsed;
          


            MessageBox.Show("تمت عملية الانتهاء من تنفيذ قرار الترفيعة");
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           string l= list.SelectedItem as string ;
            
                foreach (string ss in emp.ToList()) {

                    if (ss==l) {
                        emp.Remove(ss);
                    
                    }
                
                
            
            }
            list.ClearValue(ItemsControl.ItemsSourceProperty);
            list.ItemsSource = emp;


        }

   

        private void cmdUp_Copy_Click(object sender, RoutedEventArgs e)
        {
            NumValueSal++;
        }

        private void cmdDown_Click_1(object sender, RoutedEventArgs e)
        {
            NumValueSal--;
        }

        private void cmdUp_Copy1_Click(object sender, RoutedEventArgs e)
        {
            NumValueBon++;
        }

        private void cmdDown_Copy1_Click(object sender, RoutedEventArgs e)
        {
            NumValueBon--;
        }

        private void cmdDown_Copy_Click(object sender, RoutedEventArgs e)
        {
            NumValueDay--;
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            NumValueDay++;
        }

        private void cmdUp_Copy2_Click(object sender, RoutedEventArgs e)
        {
            NumValueSalBon++;
        }

        private void cmdDown_Copy2_Click(object sender, RoutedEventArgs e)
        {
            NumValueSalBon--;

        }
    }
}
