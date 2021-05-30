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
    public partial class Work_Add : UserControl
    {
        PersonelDBContext db;
        List<Work> w;
        public Work_Add()
        {
            InitializeComponent();
            db = new PersonelDBContext();
            IEnumerable w = db.Works.Where(mw => mw.WorkPlace != null).ToList();
            work_grid.ItemsSource = w;
            work.Text = null;


        }

        private void add_work(object sender, RoutedEventArgs e)
        {
            if (work.Text != null && work.Text != "")
            {
                Work new_work = new Work();
                new_work.WorkPlace = work.Text;

                db = new PersonelDBContext();
                db.Works.Add(new_work);
                db.SaveChanges();
                work.Text = null;
                MessageBox.Show("تمت الاضافة بنجاح");
                IEnumerable w = db.Works.Where(mw => mw.WorkPlace != null).ToList();
                work_grid.ItemsSource = w;
                
            }

        }

        private void delete(object sender, RoutedEventArgs e)
        {
            if (work_grid.SelectedItem != null)
            {
                var w = (Work)work_grid.SelectedItem;

                db.Works.Remove(w);
                db.SaveChanges();

                MessageBox.Show("تمت  عملية الحذف  بنجاح");
                IEnumerable a = db.Works.Where(mw => mw.WorkPlace != null).ToList();
                work_grid.ItemsSource = a;
                up_but.IsEnabled = false;
                del_but.IsEnabled = false;
                add_but.IsEnabled = true;
                work.Text = null;
                work_grid.SelectedItem = null;
            }
            else
            { MessageBox.Show("يرجى اختيار سطر للحذف"); }

        }
        private void update(object sender, RoutedEventArgs e)
        {


            if (work_grid.SelectedItem != null)

            {
                var w = (Work)work_grid.SelectedItem;
                db.Works.Where(m => m.PlaceId == w.PlaceId);
                w.WorkPlace = work.Text;

                db.Works.Update(w);
                db.SaveChanges();
                work.Text = null;
                MessageBox.Show("تمت  عملية التعديل بنجاح");
                IEnumerable a = db.Works.Where(mw => mw.WorkPlace != null).ToList();
                work_grid.ItemsSource = a;
                up_but.IsEnabled = false;
                del_but.IsEnabled = false;
                add_but.IsEnabled = true;
              
                work_grid.SelectedItem = null;

            }
        }
        private void select_item(object sender, RoutedEventArgs e)

        {
            if (work_grid.SelectedItem != null)
            {
                Work w = (Work)work_grid.SelectedItem;
                work.Text = w.WorkPlace;
                up_but.IsEnabled = true;
                del_but.IsEnabled = true;
                add_but.IsEnabled = false;

            }





        }





    }
    }

