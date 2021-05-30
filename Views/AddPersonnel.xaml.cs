using Microsoft.EntityFrameworkCore;
using personnel.Models;
using personnel.ModelView;
using System;
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
using System.Windows.Shapes;

namespace personnel.Views
{
    /// <summary>
    /// Interaction logic for AddPersonnel.xaml
    /// </summary>
    public partial class AddPersonnel : Window
    {
        PersonelDBContext db;
        public AddPersonnel()
        {
            InitializeComponent();
            db = new PersonelDBContext();
            // Work w = new Work();

            List<string> workplaces = db.Works.Select(w => w.WorkPlace).ToList();
            workplace.ItemsSource = workplaces;
            workplace.SelectedIndex = 0;
        //    List<string> cert = db.Certificates.Select(x => x.CertName).ToList();
        //    certificate.ItemsSource = cert;
        //    certificate.SelectedIndex = 0;
        }

        private void Clear_Form(object sender, RoutedEventArgs e)
        {
            AddPersonnel ad = new AddPersonnel();
           
           
            ad.Show();
            this.Close();


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
            Employee emp = new Employee();
            this.Close();
            emp.Show();
           




        }

        private void Add_person(object sender, RoutedEventArgs e)
        {
            Emp_Maxsalary em = new Emp_Maxsalary();
            maxsalary.Text = em.getmaxsal(grade.Text).ToString();
            float mx = float.Parse(maxsalary.Text);
            salary.Text=em.getbigsal(grade.Text).ToString();
            float sal= float.Parse(salary.Text);
            try
            {
                //PersonelDBContext db = new PersonelDBContext();
                SelfCard emp = new SelfCard
                {
                    EmployeeId = Int32.Parse(empid.Text),
                    InsuranceId = insurid.Text,
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

                    Specialization = specialization.Text,
                    Certificate = certificate.Text,
                    FileId = fileid.Text,
                    FileClass = fileclass.Text,

                    Mission = mission.Text,

                    Employer = employer.Text,
                    JobTitle = job.Text,
                    //Salary = sal,
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
                    IsTeacher = 0,
                    maxsalary = mx

            };



                db.SelfCards.Add(emp);
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
        List<string>  vs = job_titles.Select(x => x.JobTitle).ToList<string>() ;
            vs.Add("غير ذلك");
            job.ItemsSource = vs;
            job.SelectedIndex = 0;
            

        }
    }

}
