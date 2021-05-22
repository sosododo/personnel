using personnel.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
namespace personnel.Views
{
    /// <summary>
    /// Interaction logic for work_add.xaml
    /// </summary>
    public partial class Certificate_Add : UserControl
    {
        PersonelDBContext db;
        List<Certificate> w;
        public Certificate_Add()
        {
            InitializeComponent();
            db = new PersonelDBContext();
            IEnumerable w = db.Certificates.Where(mw => mw.CertName != null).ToList();
            cert_grid.ItemsSource = w;
            cert.Text = null;


        }

        private void add_certificate(object sender, RoutedEventArgs e)
        {
            if (cert.Text != null && cert.Text != "")
            {
                Certificate new_cert = new Certificate();
                new_cert.CertName = cert.Text;

                db = new PersonelDBContext();
                db.Certificates.Add(new_cert);
                db.SaveChanges();
                cert.Text = null;
                MessageBox.Show("تمت الاضافة بنجاح");
                IEnumerable w = db.Certificates.Where(mw => mw.CertName != null).ToList();
                cert_grid.ItemsSource = w;
            }

        }

        private void delete(object sender, RoutedEventArgs e)
        {
            if (cert_grid.SelectedItem != null)
            {
                var w = (Certificate)cert_grid.SelectedItem;

                db.Certificates.Remove(w);
                db.SaveChanges();

                MessageBox.Show("تمت  عملية الحذف  بنجاح");
                IEnumerable a = db.Certificates.Where(mw => mw.CertName != null).ToList();
                cert_grid.ItemsSource = a;
            }
            else
            { MessageBox.Show("يرجى اختيار سطر للحذف"); }

        }
        private void update(object sender, RoutedEventArgs e)
        {
            

            if (cert_grid.SelectedItem != null)

            {
                var w = (Certificate)cert_grid.SelectedItem;
                MessageBox.Show("أدخل القيمة الجديدة من فضلك");
                db.Certificates.Where(m => m.CertId == w.CertId);
                w.CertName = cert.Text;
               
                db.Certificates.Update(w);
                db.SaveChanges();
                cert.Text = null;
                MessageBox.Show("تمت  عملية التعديل بنجاح");
                IEnumerable a = db.Certificates.Where(mw => mw.CertName != null).ToList();
                cert_grid.ItemsSource = a;

            }
        }
    }
    }

