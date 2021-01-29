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
using personnel.Models;

namespace personnel.Views
{
    /// <summary>
    /// Interaction logic for Rest_Details.xaml
    /// </summary>
    public partial class Rest_Details : UserControl
    {
        PersonelDBContext db;

        public Rest_Details()
        {
            InitializeComponent();
            db = new PersonelDBContext();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Decision d = (Decision)DataContext;
            //List<string> emp_id=new List<string>();
            
            //List<Rest> Custom_Dec= db.Rests.Where(x => x.DecisionId == d.DecisionId).ToList();
            //foreach (Rest r in Custom_Dec) {

            //  User user=  db.Users.Where(x => x.UserId == r.PersonId).FirstOrDefault();
            //    emp_id.Add(user.UserName);
               
            
            
            //}
            //MessageBox.Show(emp_id.Count.ToString());
           // emp_name.ItemsSource = emp_id;



        }
    }
}
