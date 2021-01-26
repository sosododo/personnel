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
              List<int> decNum=  pdb.Decisions.Select(x => x.DecisionNumber).ToList<int>();
                foreach (int num in decNum) {
                    if (num ==Int32.Parse( dec_num.Text)) {
                        count++;
                    }
                
                }


                if (dec_con.Text != null && dec_year.Text != null && dec_type.Text != null && dec_start.Text != null && dec_end.Text != null && dec_source.Text != null && dec_effect.Text != null && dec_result.Text != null && dec_num.Text != null)
                {
                    if (count == 0)
                    {
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
                            Result = dec_result.Text



                        };
                        pdb.Decisions.Add(dec);
                        pdb.SaveChanges();
                        MessageBox.Show("تمت الاضافة بنجاح");
                        this.Visibility = Visibility.Collapsed;
                    }
                    else {
                        MessageBox.Show("رقم القرار موجود مسبقا");
                    }
                }
                else
                {

                    MessageBox.Show("تأكد من ادخال جميع البيانات");

                }





            }
            catch (Exception ex) { MessageBox.Show(ex.StackTrace); }

        }
    }
}
