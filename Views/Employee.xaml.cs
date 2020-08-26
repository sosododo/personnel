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
    public partial class Employee : Window
    {
        PersonelDBContext db = new PersonelDBContext();
       // SelfCard sc = new SelfCard();
        public Employee()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateEmp up = new UpdateEmp();
            up.Show();
            this.Close();
        }

        private void EmployeeSerch(object sender, RoutedEventArgs e)
        {
            //FULL NAME FOR SEARCH????
            string inputsearch = search.Text;
          
            if (sname.IsChecked == true)
            {
                int num = db.SelfCards.Where(x => x.FirstName == inputsearch).Count();

                if (num == 1)
                {
                    var sc = db.SelfCards.Where(x => x.FirstName == inputsearch).ToList();
                    search_emp.ItemsSource = sc;



                }
            }
            else
            {
                int num = db.SelfCards.Where(x => x.FirstName == inputsearch).Count();

                if (num == 1)
                {
                 //   sc = db.SelfCards.Where(x => x.Workplace == inputsearch).FirstOrDefault();
                }
            }

        }

        private void Edit_Employee(object sender, RoutedEventArgs e)
        {

        }
    }
}
