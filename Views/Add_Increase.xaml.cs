﻿using personnel.Models;
using personnel.ModelView;
using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace personnel.Views
{
    /// <summary>
    /// Interaction logic for Increase.xaml
    /// </summary>
    public partial class Add_Increase : UserControl
    {
        PersonelDBContext db;
        List<string> emp;
        public Add_Increase()
        {
            InitializeComponent();
            
            db = new PersonelDBContext();


            emp = (from p in db.SelfCards
                   where p.Status == "قائم على رأس عمله"
                   select p.FirstName + " " + p.FatherName + " " + p.LastName).ToList<string>();


            list.ItemsSource = emp;
            Decision d = (Decision)DataContext;
        }

        private void excute_increase(object sender, RoutedEventArgs e)
        {
            try
            {

                excutee();
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void excutee()
        {







            var emp_id = (from Z in db.SelfCards
                          where Z.Status == "قائم على رأس عمله"
                          select new { Z.PersonId, full = Z.FirstName + " " + Z.FatherName + " " + Z.LastName, Z.Salary }).ToList();
            foreach (string ss in emp.ToList())
            {

                var id = emp_id.Where(d => d.full == ss).ToList().ElementAt(0);
                long eid = id.PersonId;
                long before = (long)id.Salary;
                int c = db.SalaryIncrease.Where(x => x.DecisionId == long.Parse(dec_id.Text) && x.PersonId == eid).Count();
                if (c > 0)
                {
                    MessageBox.Show("  تم تطبيق هذا القرار على الموظف  " + id.full);
                }
                else
                {
                    SalaryIncrease sn = new SalaryIncrease
                    {
                        PersonId = eid,
                        DecisionId = long.Parse(dec_id.Text),
                        SalaryBefore = before,
                        SalaryAfter = before + long.Parse(increase1.Text),
                        Increase = long.Parse(increase1.Text),
                    };
                    db.SalaryIncrease.Add(sn);
                    db.SaveChanges();
                    var self = db.SelfCards.Where(x => x.PersonId == eid).Single();
                    self.Salary = before + long.Parse(increase1.Text);
                    db.SelfCards.Update(self);
                    db.SaveChanges();


                }

            }
            var d = db.Decisions.Where(c => c.DecisionId == long.Parse(dec_id.Text)).Single();
            excute.IsChecked = true;
            d.IsExcute = true;

            db.Decisions.Update(d);

            db.SaveChanges();
        

        MessageBox.Show("تمت عملية الانتهاء من تنفيذ قرار الزيادة");
            Decision_View dv = new Decision_View();
        Window parentWindow = Window.GetWindow(this);
        parentWindow.Close();
            dv.Show();

        }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        string l = list.SelectedItem as string;

        foreach (string ss in emp.ToList())
        {

            if (ss == l)
            {
                emp.Remove(ss);

            }



        }
        list.ClearValue(ItemsControl.ItemsSourceProperty);
        list.ItemsSource = emp;


    }

}
}