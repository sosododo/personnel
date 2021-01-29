using personnel.Models;
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

namespace personnel.Views
{
    /// <summary>
    /// Interaction logic for UpdateDecision.xaml
    /// </summary>
    /// 
    public partial class UpdateDecision : UserControl
    {
        PersonelDBContext db;
       
        public UpdateDecision()
        {
           
             InitializeComponent();
            
           
          


        }


        private void updat(object sender, RoutedEventArgs e)
        {
            Decision d = (Decision)DataContext;
            try
            {
                db = new PersonelDBContext();
                db.Decisions.Update(d);
                db.SaveChanges();
                MessageBox.Show("تم التعديل بنجاح");
               // this.Visibility = Visibility.Collapsed;

            }
            catch (Exception m)
            { MessageBox.Show("يوجد خطأ"); }
            }
    }
}
