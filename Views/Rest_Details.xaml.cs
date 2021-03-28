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

       
    }
}
