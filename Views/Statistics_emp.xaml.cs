using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics_emp : Window
    {
        public Statistics_emp()
        {
            InitializeComponent();
            PersonelDBContext db = new PersonelDBContext();
          

            List<string> workplaces = db.Works.Select(w => w.WorkPlace).ToList();
            workplace.ItemsSource = workplaces;
            workplace.SelectedIndex = 0;
        }
        private void grade_DropDownClosed(object sender, EventArgs e)
        {
            PersonelDBContext db = new PersonelDBContext();

            db.Jobs.Load();

            string cat = grade.Text;
            List<Job> job_titles = db.Jobs.Where(x => x.Category.Contains(cat)).ToList();
            job.ItemsSource = job_titles.Select(x => x.JobTitle);
            job.SelectedIndex = 0;
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            
            MainWindow v = new MainWindow();
            this.Close();
            v.Show();
          
        }

        private void show(object sender, RoutedEventArgs e)
        {
            try

            {
                PersonelDBContext dbc = new PersonelDBContext();






                List<SelfCard> selves = dbc.SelfCards.Where(x => x.Sex == sex.Text && x.Religion == religion.Text).ToList();

                search_emp.ItemsSource = selves;

            }
            catch
            {
               
                MessageBox.Show( " خطأ اتصال بقاعدة البيانات"); }
            
        }
    }
}
