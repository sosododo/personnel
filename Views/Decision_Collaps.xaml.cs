using System;
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
using personnel.Models;

namespace personnel.Views
{
    /// <summary>
    /// Interaction logic for Decision_Collaps.xaml
    /// </summary>
    public partial class Decision_Collaps : UserControl
    {
        PersonelDBContext db;
        public Decision_Collaps()
        {
            InitializeComponent();
        }

        private void collaps_Click(object sender, RoutedEventArgs e)
        {
            Decision d = (Decision)DataContext;
          
            try
            {
                db = new PersonelDBContext();
                db.Decisions.Update(d);
                db.SaveChanges();
                MessageBox.Show("تم طي القرار بنجاح");
                // this.Visibility = Visibility.Collapsed;

            }
            catch (Exception m)
            { MessageBox.Show("يوجد خطأ"); }
        }
    }
    }

