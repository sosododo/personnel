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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace personnel.Views
{
    /// <summary>
    /// Interaction logic for Add_Decision.xaml
    /// </summary>
    public partial class Add_Decision : UserControl
    { 
        PersonelDBContext pdb;
        List<Decision>  decisions= new List<Decision>();
        public Add_Decision()
        {
            InitializeComponent();
          
     

        }

        private void insert_decision(object sender, RoutedEventArgs e)
        {
            try
            {

                
                int count = 0; 
                pdb = new PersonelDBContext();
            count=    pdb.Decisions.Where(x => x.DecisionNumber == Int32.Parse(dec_num.Text) && x.DecisionYear == Int32.Parse(dec_year.Text) && x.DecisionSource == dec_source.Text).Count() ;
            

                if (dec_con.Text != null && dec_year.Text != null && dec_type.Text != null && dec_start.Text != null && dec_end.Text != null && dec_source.Text != null && dec_effect.Text != null && dec_result.Text != null && dec_num.Text != null)
                {
                    if (count == 0)
                    {
                        if (dec_source.Text== "جهة أخرى")
                        {
                            Decision dec = new Decision
                            {
                                DecisionNumber = Int32.Parse(dec_num.Text),
                                DecisionYear = Int32.Parse(dec_year.Text),
                                DecisionType = dec_type.Text,
                                DecisionContent = dec_con.Text,
                                DecisionStart = dec_start.SelectedDate,
                                DecisionEnd = dec_end.SelectedDate,
                                DecisionSource = others.Text,
                                EffectType = dec_effect.Text,
                                Result = dec_result.Text,
                                DecisionStatus = "قرار ساري",
                                CollapsDate = null



                            };
                            decisions.Add(dec);
                            pdb.Decisions.Add(dec);
                            pdb.SaveChanges();
                            MessageBox.Show("تمت الاضافة بنجاح");
                            string message = "هل تريد تنفيذ القرار؟";
                            string caption = "تنبيه";
                            var result = MessageBox.Show(message, caption,
                                                         MessageBoxButton.YesNo,
                                                         MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {

                                Decision_View dv = new Decision_View();


                                dv.Show();

                                dv.result.ItemsSource = decisions;
                                var myWindow = Window.GetWindow(this);
                                myWindow.Close();
                            }
                            else if (result == MessageBoxResult.No)
                            {
                                this.Visibility = Visibility.Collapsed;
                            }
                            

                        }
                        else {
                            Decision dec = new Decision
                            {
                                DecisionNumber = Int32.Parse(dec_num.Text),
                                DecisionYear = Int32.Parse(dec_year.Text),
                                DecisionType = dec_type.Text,
                                DecisionContent = dec_con.Text,
                                DecisionStart = dec_start.SelectedDate,
                                DecisionEnd = dec_end.SelectedDate,
                                DecisionSource = dec_source.Text,
                                EffectType = dec_effect.Text,
                                Result = dec_result.Text,
                                DecisionStatus = "قرار ساري",
                                CollapsDate = null



                            };
                            decisions.Add(dec);
                            pdb.Decisions.Add(dec);
                            pdb.SaveChanges();
                            MessageBox.Show("تمت الاضافة بنجاح");

                            string message = "هل تريد تنفيذ القرار؟";
                            string caption = "تنبيه";
                            var result = MessageBox.Show(message, caption,
                                                         MessageBoxButton.YesNo,
                                                         MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {

                                Decision_View dv = new Decision_View();


                                dv.Show();

                                dv.result.ItemsSource = decisions;
                                var myWindow = Window.GetWindow(this);
                                myWindow.Close();
                            }
                            else if (result == MessageBoxResult.No)
                            {
                                this.Visibility = Visibility.Collapsed;
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("لا يمكن إضافة هذا القرار، قد يكون هناك تكرار في رقم القرار أو مصدره أو سنة صدوره");
                    }
                }
                else
                {

                    MessageBox.Show("تأكد من ادخال جميع البيانات");

                }





            }
            catch (Exception ex) { MessageBox.Show("تأكد من ادخال كافة البيانات"); }

        }

        private void dec_source_DropDownClosed(object sender, EventArgs e)
        {
            if (dec_source.Text == "جهة أخرى") {
                dec_source.Visibility = Visibility.Collapsed;
                others.Visibility = Visibility.Visible;
            }
        }

        private void ex_Click(object sender, RoutedEventArgs e)
        {



          




        }
    }
}
