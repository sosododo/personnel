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
    public partial class Statistics_Work : Window
    {
        public Statistics_Work()
        {
            InitializeComponent();
            PersonelDBContext db = new PersonelDBContext();


            List<string> certs = db.Certificates.Select(c => c.CertName).ToList();
            certificate.ItemsSource = certs;
            certificate.SelectedIndex = 0;
        }
        private void grade_DropDownClosed(object sender, EventArgs e)
        {
            PersonelDBContext db = new PersonelDBContext();

            db.Jobs.Load();


        }
        private void Exit(object sender, RoutedEventArgs e)
        {

            Statistics v = new Statistics();
            this.Close();
            v.Show();

        }

        private void show(object sender, RoutedEventArgs e)
        {
            try

            {
                PersonelDBContext dbc = new PersonelDBContext();






                List<SelfCard> selves = dbc.SelfCards.Where(x => x.Certificate == certificate.Text && x.Status == status.Text).ToList();

                search_emp.ItemsSource = selves;

            }
            catch
            {

                MessageBox.Show(" خطأ اتصال بقاعدة البيانات");
            }

        }

        private void print(object sender, RoutedEventArgs e)
        {

            Window win = new WorkStatis(certificate.Text, status.Text);
            win.Show();

        }
    }
}
