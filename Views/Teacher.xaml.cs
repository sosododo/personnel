using personnel.Models;
using System;
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
using System.Windows.Shapes;

namespace personnel.Views
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Teacher : Window
    {

        


        PersonelDBContext db = new PersonelDBContext();
       // SelfCard sc = new SelfCard();
        public Teacher()
        {
            InitializeComponent();
            List<string> workplaces = db.Works.Select(w => w.WorkPlace).ToList();
            workplace.ItemsSource = workplaces;
            workplace.SelectedIndex = 0;
          
            
        }

        private void Edit_Employee(object sender, RoutedEventArgs e)
        {
            SelfCard self = new SelfCard();
            if (search_emp.SelectedItem != null)
            {
                self = (SelfCard)search_emp.SelectedItem;
                UpdateTeacher up = new UpdateTeacher(self);
                up.Show();
                this.Close();
            }
            else {
                MessageBox.Show("اختر موظفً");
            }
        }

        private void Search_Employee(object sender, RoutedEventArgs e)
        {
            string work = workplace.Text;
            string inputsearch = search.Text;
            string lastname = last.Text;
            List<SelfCard> selfCards = new List<SelfCard>();
            if (sname.IsChecked == true)
            {

                if (inputsearch == "" || inputsearch == "الاسم الأول")
                {

                    selfCards = db.SelfCards.Where(x => x.IsTeacher == 1 && x.LastName == lastname).ToList();
                }
                else if (lastname == "" || lastname == "الكنية")
                {
                    selfCards = db.SelfCards.Where(x => x.IsTeacher == 1 && x.FirstName == inputsearch).ToList();
                }
                else
                {


                    selfCards = db.SelfCards.Where(x => x.IsTeacher == 1 && x.FirstName == inputsearch && x.LastName == lastname).ToList();
                }
               

       

                if (selfCards.Count!=0)
                {
                    
                    search_emp.ItemsSource = selfCards;



                }
                else
                { MessageBox.Show("لا يوجد بيانات لهذا البحث"); }

            }
            else if (splace.IsChecked == true)
            {
                int num = db.SelfCards.Where(x => x.IsTeacher==1 && x.Workplace == work).Count();

                if (num >= 1)
                {
                    var sw = db.SelfCards.Where(x =>x.IsTeacher==1 && x.Workplace == work).ToList();
                    search_emp.ItemsSource = sw;

                }
                else
                { MessageBox.Show("لا يوجد بيانات لهذا البحث"); }



            }
            //else
            //{
            //    int num = db.SelfCards.Where(x => x.FirstName == inputsearch).Count();

            //    if (num == 1)
            //    {
            //        sc = db.SelfCards.Where(x => x.Workplace == inputsearch).FirstOrDefault();
            //    }
            //}

        }

        private void View_Employee(object sender, RoutedEventArgs e)
        {
            SelfCard self1 = new SelfCard();
            if (search_emp.SelectedItem != null)
            {
                self1 = (SelfCard)search_emp.SelectedItem;
                ShowTeacher up1 = new ShowTeacher(self1);
                up1.Show();
                this.Close();
            }
            else {
                MessageBox.Show(" اختر موظفاً");
            }
        }
        private void Insert_Employee(object sender, RoutedEventArgs e)
        {
            AddTeacher ad = new AddTeacher();
            ad.Show();
            this.Hide();
        }
        private void Return(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Hide();
        }
        


        private void splace_Checked(object sender, RoutedEventArgs e)
        {
           
            search.Visibility = Visibility.Hidden;
            last.Visibility = Visibility.Hidden;
            workplace.Visibility = Visibility.Visible;
            search_emp.ItemsSource = null;

        }

        private void sname_Checked(object sender, RoutedEventArgs e)
        {
            search.Text = "الاسم الأول";
            last.Text = "الكنية";
            search.Visibility = Visibility.Visible;
            last.Visibility = Visibility.Visible;
            workplace.Visibility = Visibility.Hidden;
            search_emp.ItemsSource = null;
        }

        private void clear_first(object sender, RoutedEventArgs e)
        {
            search.Text = "";

        }
        private void clear_last(object sender, RoutedEventArgs e)
        {
            last.Text = "";

        }
    }
}


