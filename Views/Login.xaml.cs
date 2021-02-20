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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Login : Window
    {
        PersonelDBContext db = new PersonelDBContext();
        public static User currentUser = new User();
        public static int user_id;
        public Login()
        {
            InitializeComponent();
            db.Users.Load();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PersonelDBContext db = new PersonelDBContext();
            User login_user = new User();
            MainWindow win1 = new MainWindow();

            string rule = "";
            string user = username.Text;
            string pass = password.Password;
            currentUser = db.Users.Where(x => x.UserName == user && x.Password == pass).FirstOrDefault(); 
            List<User> user1 = db.Users.Where(x => x.UserName == user && x.Password == pass).ToList();
            if(user1.Count==1)
                      
            {
                login_user = user1.FirstOrDefault();
                App.Current.Properties["user_id"] = login_user.UserId;
                rule = login_user.Rule;
                win1.currentu.Text = "أهلا بك"+" " + login_user.UserName;
                switch (rule)
                {
                    case "teacher":
                        win1.teach.IsEnabled = true;
                        win1.em.IsEnabled = false;
                        win1.teach.IsChecked = true;
                        win1.em.IsChecked = false;
                        win1.Show();
                       this.Close();
                        break;
                    case "employee":
                        win1.teach.IsEnabled = false;
                        win1.em.IsEnabled = true;
                        win1.teach.IsChecked = false;
                        win1.em.IsChecked = true;
                        win1.Show();
                        this.Close();
                        break;
                    case "admin":
                        win1.teach.IsEnabled = true;
                        win1.em.IsEnabled = true;
                        win1.Show();
                        this.Close();
                        break;
                }
              
            }
            else
            {
                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة ");
                username.Text = "";
                password.Password = "";
                
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            { Button_Click(sender, e); }         
        }

        private void username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                password.Focus();
                e.Handled = true;
            }

        }

       
    }
}



//using personnel.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//namespace personnel.Views
//{
//    /// <summary>
//    /// Interaction logic for Window1.xaml
//    /// </summary>
//    public partial class Login : Window
//    {
//        public static int user_id;
//        public Login()
//        {
//            InitializeComponent();


//        }

//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            PersonelDBContext db = new PersonelDBContext();
//            User login_user = new User();
//            MainWindow win1 = new MainWindow();

//            string rule = "";
//            string user = username.Text;
//            string pass = password.Text;
//            int num = db.Users.Where(x => x.UserName == user && x.Password == pass).Count();

//            if (num == 1)
//            {
//                login_user = db.Users.Where(x => x.UserName == user && x.Password == pass).FirstOrDefault();
//                App.Current.Properties["user_id"] = login_user.UserId;
//                rule = login_user.Rule;
//                win1.currentu.Text = login_user.UserName + "أهلا بك";
//                switch (rule)
//                {
//                    case "duser":
//                        //win1.b1.IsEnabled = false;
//                        //win1.Show();
//                        //this.Close();
//                        break;
//                    case "suser":
//                        //win1.b2.IsEnabled = false;
//                        //win1.Show();
//                        //this.Close();
//                        break;
//                    case "admin":
//                        win1.Show();
//                        this.Close();
//                        break;
//                }

//            }
//            else
//            {
//                MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة ");
//                username.Text = "";
//                password.Text = "";

//            }
//        }

//        private void Window_KeyDown(object sender, KeyEventArgs e)
//        {

//            if (e.Key == Key.Enter)
//            { Button_Click(sender, e); }
//        }

//        private void username_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.Key == Key.Enter)
//            {
//                password.Focus();
//                e.Handled = true;
//            }

//        }


//    }
//}

