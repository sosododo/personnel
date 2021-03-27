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
    public partial class Statistics_Job: Window
    {
        public Statistics_Job()
        {
            InitializeComponent();
            PersonelDBContext db = new PersonelDBContext();
            


        }
        private void grade_DropDownClosed(object sender, EventArgs e)
        {
            PersonelDBContext db = new PersonelDBContext();

            //db.Jobs.Load();
            string cat = catecory.Text;
            db.Jobs.Load();
            List<Job> job_titles = db.Jobs.Where(x => x.Category.Contains(cat)).ToList();
            job.ItemsSource = job_titles.Select(x => x.JobTitle);
            job.SelectedIndex = 0;

        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            
            Statistics v = new Statistics();
        
            v.Show();
            this.Close();
        }
        private void grade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PersonelDBContext db = new PersonelDBContext();
         

            string cat = catecory.Text;
            db.Jobs.Load();
            List<Job> job_titles = db.Jobs.Where(x => x.Category.Contains(cat)).ToList();
            job.ItemsSource = job_titles.Select(x => x.JobTitle);
        }

        private void show(object sender, RoutedEventArgs e)
        {
            try

            {
                PersonelDBContext dbc = new PersonelDBContext();
                List<SelfCard> selves = dbc.SelfCards.Where(x => x.Category == catecory.Text && x.Status == status.Text && x.JobTitle == job.Text).ToList();

                search_emp.ItemsSource = selves;







            }
            catch
            {
               
                MessageBox.Show( " خطأ اتصال بقاعدة البيانات"); }
            
        }

        private void print(object sender, RoutedEventArgs e)
        {
            Window win = new  JobStatis(catecory.Text, status.Text,job.Text);
            win.Show();


        }
    }
}
