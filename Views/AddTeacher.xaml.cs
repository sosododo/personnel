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
    /// Interaction logic for AddTeacher.xaml
    /// </summary>
    public partial class AddTeacher : Window
    {
        PersonelDBContext db;
        public AddTeacher()
        {
            InitializeComponent();
            db = new PersonelDBContext();
            // Work w = new Work();

            List<string> workplaces = db.Works.Select(w => w.WorkPlace).ToList();
            workplace.ItemsSource = workplaces;
            workplace.SelectedIndex = 0;
            List<string> ca = db.Jobs.Where(c => c.JobId > 89).Select(x => x.Category).Distinct().ToList();
           
             

            grade.ItemsSource = ca;
            List<string> cert = db.Certificates.Select(x => x.CertName).ToList();
            certificate.ItemsSource = cert;
            certificate.SelectedIndex = 0;  


        }

        private void Clear_Form(object sender, RoutedEventArgs e)
        {
            AddTeacher ad = new AddTeacher();
            this.Close();
            ad.Show();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            //    MessageBox.Show("هل تريد الخروج؟");
            //    this.Close();
            //}

            //MessageBoxResult result = MessageBox.Show("هل تريد الخروج ؟ ",
            //                                  "تأكيد",
            //                                  MessageBoxButton.OKCancel,
            //                                  MessageBoxImage.Question);
            //if (result == MessageBoxResult.OK)
            //{
            //    Application.Current.Shutdown();
            //}
            Teacher mw = new Teacher();
            this.Close();
            mw.Show();
        }

        private void Add_person(object sender, RoutedEventArgs e)
        {
            try
            {
                //PersonelDBContext db = new PersonelDBContext();
                SelfCard teatcher = new SelfCard
                {
                    EmployeeId =Int32.Parse( empid.Text), 
                    FirstName = firstname.Text,
                    LastName = last.Text,
                    FatherName = father.Text,
                    MotherName = mother.Text,
                    Sex = sex.Text,
                    Religion = religion.Text,
                    SocialSituation = social.Text,
                    Address = address.Text,
                    BirthPlace = birth.Text,
                    Birthday = birthday.SelectedDate,
                    Phone = phone.Text,
                    Military = military.Text,
                    Recruitment = recruitment.Text,
                    RegisteredId = registered_id.Text,
                    Nationality = nationalty.Text,
                    InsuranceId = insurid.Text,
                    Specialization = specialization.Text,
                    Certificate = certificate.Text,
                    FileId = fileid.Text,
                    FileClass = fileclass.Text,

                    Mission = mission.Text,

                    Employer = employer.Text,
                    JobTitle = job.Text,
                    //  Salary = long.Parse(salary.Text),
                    Salary = 0,
                    Workplace = workplace.Text,
                    Photo = photo.Text,
                    Section = dept.Text,
                    Division = div.Text,
                    Category = grade.Text,
                    Status = status.Text,
                    Notes = note.Text,
                    BeginningDate = begindate.SelectedDate,
                    NominationType = type.Text,
                    InsuranceCard = card.Text,
                    Register = register.Text,
                    WorkContracts = workcontract.Text,
                    Languages = lang.Text,
                    TrainingCourses = course.Text,
                    IsTeacher=1


                };



                db.SelfCards.Add(teatcher);
                db.SaveChanges();
                MessageBox.Show("تمت الاضافة بنجاح");
            }


            catch (Exception ex) { MessageBox.Show(ex.StackTrace); }
            

        }
        //private void Get_Job(object sender, RoutedEventArgs e)
        //{
        //    PersonelDBContext db = new PersonelDBContext();

        //    db.Jobs.Load();

        //    string cat = grade.Text;
        //    List<Job> job_titles = db.Jobs.Where(x=> x.Category.Contains(cat)).ToList();
        //    job.ItemsSource = job_titles.Select(x => x.JobTitle);
        //    job.SelectedIndex = 0;
           
        //}

        private void grade_DropDownClosed(object sender, EventArgs e)
        {
            PersonelDBContext db = new PersonelDBContext();

            db.Jobs.Load();

            string cat = grade.Text;
            List<Job> job_titles = db.Jobs.Where(x => x.Category.Contains(cat)).ToList();
            job.ItemsSource = job_titles.Select(x => x.JobTitle);
            job.SelectedIndex = 0;
        }
    }

}
