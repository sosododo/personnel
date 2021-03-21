using Microsoft.EntityFrameworkCore;
using personnel.Models;
using personnel.Statistics;
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
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics_Special : Window
    {
        public Statistics_Special()
        {
            InitializeComponent();
           


           
        }
       
        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
            Statistics v = new Statistics();
     
            v.Show();

        }

        private void show(object sender, RoutedEventArgs e)
        {
            try

            {
                PersonelDBContext dbc = new PersonelDBContext();






                List<SelfCard> selves = dbc.SelfCards.Where(x => x.Notes == note.Text && x.Status == status.Text).ToList();

                search_emp.ItemsSource = selves;

            }
            catch
            {

                MessageBox.Show(" خطأ اتصال بقاعدة البيانات");
            }

        }

        private void print(object sender, RoutedEventArgs e)
        {

            Window win = new SpecStatis(note.Text, status.Text);
            this.Close();
            win.Show();

        }
    }
}
