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
using System.Windows.Shapes;

namespace personnel.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (em.IsChecked==true)
            {
                Employee emp = new Employee();
                emp.Show();
                this.Close(); }
            else if(teach.IsChecked==true)

            {
                Teacher teacher = new Teacher();
                teacher.Show();
                this.Close();
            }
        }

        private void GoToDecisions (object sender, RoutedEventArgs e)
        {
            Decision_View de = new Decision_View();
            de.Show();
            this.Close();

        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Statistics_emp();
            win.Show();
            this.Close();
        }
    }
}
