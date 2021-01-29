using Microsoft.EntityFrameworkCore;
using personnel.Models;
using personnel.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace personnel.Views
{
    /// <summary>
    /// Interaction logic for ShowEmployee.xaml
    /// </summary>
    public partial class ShowEmployee : Window
    {
        PersonelDBContext db;
        public SelfCard emp = new SelfCard();
        public ShowEmployee(SelfCard s)
        {
            InitializeComponent();

            db = new PersonelDBContext();
            DataContext = s;


            db = new PersonelDBContext();
            List<string> workplaces = db.Works.Select(w => w.WorkPlace).ToList();
            workplace.ItemsSource = workplaces;
            workplace.SelectedValue = s.Workplace;
            emp = s;
           


        }
        private void grade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            db = new PersonelDBContext();
            db.Jobs.Load();

            string cat = grade.Text;
            List<Job> job_titles = db.Jobs.Where(x => x.Category.Contains(cat)).ToList();
            job.ItemsSource = job_titles.Select(x => x.JobTitle);
        }

        private void rest(object sender, RoutedEventArgs e)
        {
            cc.Visibility = Visibility.Collapsed;
            pp.Visibility = Visibility.Collapsed;
            EmpRest es = new EmpRest();
           
             //es.GetRests(emp.PersonId);
           
            ss.RS.ItemsSource = es.GetRests(emp.PersonId);
            DateTime date1 = (emp.BeginningDate).Value;
            //string count = es.MinRest(int.Parse(es.service_count(emp.BeginningDate.Value))).ToString(); 
            int service = es.service_count(emp.BeginningDate.Value);
            
            int count = es.AllRest(service);

     
            //ss.all.Text = count.ToString();
            int minrest = es.MinRest(count,emp.PersonId);
            //ss.tb.Text = minrest.ToString();
            string lab = "  المتبقي من الاجازات الإدارية للعام الحالي" +"  "+ minrest.ToString() + " من أصل"+"  " + count.ToString();
            ss.tb.Text = lab;
            ss.Visibility = Visibility.Visible;

            
           

        }
        private void punish(object sender, RoutedEventArgs e)
        {
            ss.Visibility = Visibility.Collapsed;
            cc.Visibility = Visibility.Collapsed;
            EmpPunish ep = new EmpPunish();

            pp.ps.ItemsSource = ep.Getpun(emp.PersonId);


            pp.Visibility = Visibility.Visible;




        }
        private void changfun(object sender, RoutedEventArgs e)
        {
            ss.Visibility = Visibility.Collapsed;
            pp.Visibility = Visibility.Collapsed;
            EmpChan ec = new EmpChan();
            //es.GetRests(emp.PersonId);

            cc.ch.ItemsSource = ec.GetChange(emp.PersonId);
            cc.Visibility = Visibility.Visible;




        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ss.Visibility = Visibility.Collapsed;
            pp.Visibility = Visibility.Collapsed;
            cc.Visibility = Visibility.Collapsed;
            Employee el = new Employee();
            this.Close();
            el.Show();


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window window1 = new Report.Window1();
            window1.Show();
            //Window w = new Report.RestReport("لينا");
            //w.Show();

        }

        private void EXIT(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show(); 
        }
    }
}
