﻿using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for UpdateEmp.xaml
    /// </summary>
    public partial class UpdateEmp : Window
    {
        PersonelDBContext db;
       public SelfCard emp=new SelfCard();
        public UpdateEmp(SelfCard s)
        {
            InitializeComponent();
            DataContext = s;
           

            db = new PersonelDBContext();
            List<string> workplaces = db.Works.Select(w => w.WorkPlace).ToList();
            workplace.ItemsSource = workplaces;
            emp = s;
            List<string> cert = db.Certificates.Select(x => x.CertName).ToList();
            certificate.ItemsSource = cert;
            certificate.SelectedItem = s.Certificate;

            //firstname.Text = s.FirstName;
            //last.Text = s.LastName;
            //father.Text = s.FatherName;
            //mother.Text = s.MotherName;
            //sex.SelectedItem = s.Sex;





        }

        

        //private void grade_Selected(object sender, RoutedEventArgs e)
        //{
        //    db = new PersonelDBContext();
        //    db.Jobs.Load();

        //    string cat = grade.Text;
        //    List<Job> job_titles = db.Jobs.Where(x => x.Category.Contains(cat)).ToList();
        //    job.ItemsSource = job_titles.Select(x => x.JobTitle);
        //}

        private void grade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            db = new PersonelDBContext();
            db.Jobs.Load();

            string cat = grade.Text;
            List<Job> job_titles = db.Jobs.Where(x => x.Category.Contains(cat)).ToList();
            job.ItemsSource = job_titles.Select(x => x.JobTitle);
        }

        private void update_emp(object sender, RoutedEventArgs e)
        {
            try
            {
                db.SelfCards.Update(emp);
                db.SaveChanges();
                MessageBox.Show("تم التعديل بنجاح");
                Employee em = new Employee();
                this.Close();
                em.Show();
             
            }
            catch (Exception ee) { MessageBox.Show("حدث خطأ بالاتصال بالشبكة"); }

        }
        private void grade_DropDownClosed(object sender, EventArgs e)
        {
            

            db.Jobs.Load();

            string cat = grade.Text;
            List<Job> job_titles = db.Jobs.Where(x => x.Category.Contains(cat)).ToList();
            job.ItemsSource = job_titles.Select(x => x.JobTitle);
            job.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Employee el = new Employee();
            this.Close();
            el.Show();
        }
    }
}
