using personnel.Models;
using System;
using System.Collections;
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
    /// Interaction logic for Configuration.xaml
    /// </summary>
    public partial class Configuration : Window
    {
      PersonelDBContext db = new PersonelDBContext();
      IEnumerable w;
        public Configuration()
        {
            InitializeComponent();
         
          
            

            
        }

        private void ret(object sender, RoutedEventArgs e)
        {
            
            Window win = new MainWindow();
            this.Close();
            win.Show();
        }
    }
    }

