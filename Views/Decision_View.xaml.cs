﻿using personnel.Models;
using personnel.ModelView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for Decision.xaml
    /// </summary>
    public partial class Decision_View : Window
    { 
        PersonelDBContext db;
        static List<Decision> decisions;
        //static bool s=false;
        //static IEnumerable decisions = db.Decisions.Select(x => new { x.DecisionId, fu = (x.DecisionNumber + " " + x.DecisionType + " " + x.DecisionYear), x.DecisionContent, x.DecisionSource, x.DecisionStart, x.DecisionEnd, x.Result, x.EffectType }).ToList();
        public Decision_View()
        {
            
            InitializeComponent();
            db = new PersonelDBContext();
            decisions = (from x in db.Decisions select x).ToList();

        }
        private void che_year(object sender, RoutedEventArgs e)
        {
            tx_year.IsEnabled = true;
        }
        private void che_id(object sender, RoutedEventArgs e)
        {
            tx_id.IsEnabled = true;
        }
        private void che_type(object sender, RoutedEventArgs e)
        {
            tx_type.IsEnabled = true;
        }



        private void search(object sender, RoutedEventArgs e)
        {
            db = new PersonelDBContext();
            int did;
            int dec_year;
            string dec_type = tx_type.Text;

            if (ch_year.IsChecked == true && ch_id.IsChecked == false && ch_type.IsChecked == false)
            {
                if (tx_year.Text != "")
                {
                    dec_year = Int32.Parse(tx_year.Text);

                    //decisions = db.Decisions.Where(x => x.DecisionYear == dec_year).ToList();
                    
                    decisions = (from x in db.Decisions select x).ToList();

                    List<Decision> dec = decisions.Where(d => d.DecisionYear == dec_year).ToList();
                    if (dec.Count == 0) {
                        MessageBox.Show("لم يتم العثور على عناصر مطابقة");
                    
                    }
                    result.ItemsSource = dec;
                }
                else
                { MessageBox.Show("من فضلك تأكد من بيانات البحث"); }
            }


            else if (ch_year.IsChecked == true && ch_id.IsChecked == true && ch_type.IsChecked == false)
            {
                if (tx_id.Text != "" && tx_year.Text != "")
                {
                    did = Int32.Parse(tx_id.Text);
                    dec_year = Int32.Parse(tx_year.Text);
                    List<Decision> dec = decisions.Where(d => d.DecisionYear == dec_year && d.DecisionNumber==did).ToList();
                    if (dec.Count == 0)
                    {
                        MessageBox.Show("لم يتم العثور على عناصر مطابقة");

                    }
                    
                    result.ItemsSource = dec;

                }
                else
                { MessageBox.Show("من فضلك تأكد من بيانات البحث"); }
            }

            else if (ch_id.IsChecked == false && ch_year.IsChecked == true && ch_type.IsChecked == true)
            {
                if (tx_type.Text != "" && tx_year.Text != "")
                {
                  //  did = Int32.Parse(tx_id.Text);
                    dec_year = Int32.Parse(tx_year.Text);
                    dec_type = tx_type.Text;
                    List<Decision> dec = decisions.Where(d => d.DecisionYear == dec_year && d.DecisionType == dec_type).ToList();
                    if (dec.Count == 0)
                    {
                        MessageBox.Show("لم يتم العثور على عناصر مطابقة");

                    }
                    
                    result.ItemsSource = dec;

                }
                else
                { MessageBox.Show("من فضلك تأكد من بيانات البحث"); }

            }

            else if (ch_id.IsChecked == true && ch_year.IsChecked == false && ch_type.IsChecked == true)
            {
                if (tx_id.Text != "" && tx_type.Text != "")
                {
                    did = Int32.Parse(tx_id.Text);
                   // dec_year = Int32.Parse(tx_year.Text);
                    dec_type = tx_type.Text;
                    List<Decision> dec = decisions.Where(d => d.DecisionNumber == did && d.DecisionType == dec_type).ToList();
                    if (dec.Count == 0)
                    {
                        MessageBox.Show("لم يتم العثور على عناصر مطابقة");

                    }
                    
                    result.ItemsSource = dec;

                }
                else
                { MessageBox.Show("من فضلك تأكد من بيانات البحث"); }

            }
            else if (ch_id.IsChecked == true && ch_year.IsChecked == true && ch_type.IsChecked == true)
            {
                if (tx_id.Text != "" && tx_year.Text != "" && tx_type.Text!="")
                {
                    did = Int32.Parse(tx_id.Text);
                    dec_year = Int32.Parse(tx_year.Text);
                    dec_type = tx_type.Text;
                    List<Decision> dec = decisions.Where(d => d.DecisionYear == dec_year && d.DecisionNumber == did && d.DecisionType == dec_type).ToList();
                    if (dec.Count == 0)
                    {
                        MessageBox.Show("لم يتم العثور على عناصر مطابقة");

                    }
                    
                    result.ItemsSource = dec;

                }


                else
                { MessageBox.Show("من فضلك تأكد من بيانات البحث"); }

            }

            else if (ch_id.IsChecked == true && ch_year.IsChecked == false && ch_type.IsChecked == false)
            {
                if (tx_id.Text != "" )
                {
                    did = Int32.Parse(tx_id.Text);
                   // dec_year = Int32.Parse(tx_year.Text);
                    //dec_type = tx_type.Text;
                    List<Decision> dec = decisions.Where(d =>d.DecisionNumber == did).ToList();
                    if (dec.Count == 0)
                    {
                        MessageBox.Show("لم يتم العثور على عناصر مطابقة");

                    }
                    
                    result.ItemsSource = dec;

                }
                else
                { MessageBox.Show("من فضلك تأكد من بيانات البحث"); }

            }

            else if (ch_id.IsChecked == false && ch_year.IsChecked == false && ch_type.IsChecked == true)
            {
                if (tx_type.Text != "" )
                {
                  //  did = Int32.Parse(tx_id.Text);
                    //dec_year = Int32.Parse(tx_year.Text);
                    dec_type = tx_type.Text;
                    List<Decision> dec = decisions.Where(d => d.DecisionType == dec_type).ToList();
                    if (dec.Count == 0)
                    {
                        MessageBox.Show("لم يتم العثور على عناصر مطابقة");

                    }
                    
                    result.ItemsSource = dec;

                }
                else
                { MessageBox.Show("من فضلك تأكد من بيانات البحث"); }
                 
            }
            else
            { MessageBox.Show("من فضلك تأكد من بيانات البحث"); }


            srch.IsEnabled = false;
            ch_id.IsEnabled = false;
            ch_type.IsEnabled = false;
            ch_year.IsEnabled = false;
            tx_id.IsEnabled = false;
            tx_type.IsEnabled = false;
            tx_year.IsEnabled = false;
        }
        private void edit(object sender, RoutedEventArgs e)


        {



            secondment.Visibility = Visibility.Collapsed;
            fc.Visibility = Visibility.Collapsed;
            mm.Visibility = Visibility.Collapsed;
            ins.Visibility = Visibility.Collapsed;
            details.Visibility = Visibility.Collapsed;
            de.Visibility = Visibility.Collapsed;
            punish.Visibility = Visibility.Collapsed;
            sd.Visibility = Visibility.Collapsed;
            dd.Visibility = Visibility.Collapsed;
            bd.Visibility = Visibility.Collapsed;
            rdd.Visibility = Visibility.Collapsed;
            pd.Visibility = Visibility.Collapsed;
            reward.Visibility = Visibility.Collapsed;
            fcd.Visibility = Visibility.Collapsed;

            Decision dec = (Decision)result.SelectedItem;

           
                       
            mm.DataContext = dec;
            
          
            
           mm.Visibility = Visibility.Visible;



        }
        private void butt(object sender, RoutedEventArgs e)

        {
            ////Decision dec = new Decision();
            ////dec = (Decision)result.SelectedItem;
            //result.SelectedItem = null;
            //result.ItemsSource = null;
            //tx_id.Text = "";
            //tx_id.IsEnabled = false;
            //tx_year.Text = "";
            //tx_year.IsEnabled = false;
            //tx_type.Text = "";
            //tx_type.IsEnabled = false;
            //ch_id.IsChecked = false;
            //ch_year.IsChecked = false;
            //ch_type.IsChecked = false;
            //result.SelectedItem = null;
       
            Decision_View dv = new Decision_View();
            this.Close();
            dv.Show();


        }

        private void add(object sender, RoutedEventArgs e)
        {
            
            //result.ItemsSource = "";
            tx_id.Text = "";
            tx_id.IsEnabled = false;
            tx_year.Text = "";
            tx_year.IsEnabled = false;
            tx_type.Text = "";
            tx_type.IsEnabled = false;
            ch_id.IsChecked = false;
            ch_year.IsChecked = false;
            ch_type.IsChecked = false;
            ins.dec_con.Text = null;
            ins.dec_effect.Text = null;
            ins.dec_end.Text = null;
            ins.dec_num.Text = null;
            ins.dec_result.Text = null;
            ins.dec_source.Text = null;
            ins.dec_start.Text = null;
            ins.dec_type.Text = null;
            ins.dec_year.Text = null;


            secondment.Visibility = Visibility.Collapsed;
            fc.Visibility = Visibility.Collapsed;
            mm.Visibility = Visibility.Collapsed;
            ins.Visibility = Visibility.Collapsed;
            details.Visibility = Visibility.Collapsed;
            de.Visibility = Visibility.Collapsed;
            punish.Visibility = Visibility.Collapsed;
            sd.Visibility = Visibility.Collapsed;
            dd.Visibility = Visibility.Collapsed;
            bd.Visibility = Visibility.Collapsed;
            rdd.Visibility = Visibility.Collapsed;
            pd.Visibility = Visibility.Collapsed;
            reward.Visibility = Visibility.Collapsed;
            fcd.Visibility = Visibility.Collapsed;
            ins.Visibility = Visibility.Visible;



        }

        private void select_item(object sender, RoutedEventArgs e)
        {


            secondment.Visibility = Visibility.Collapsed;
            fc.Visibility = Visibility.Collapsed;
            mm.Visibility = Visibility.Collapsed;
            ins.Visibility = Visibility.Collapsed;
            details.Visibility = Visibility.Collapsed;
            de.Visibility = Visibility.Collapsed;
            punish.Visibility = Visibility.Collapsed;
            sd.Visibility = Visibility.Collapsed;
            dd.Visibility = Visibility.Collapsed;
            bd.Visibility = Visibility.Collapsed;
            rdd.Visibility = Visibility.Collapsed;
            pd.Visibility = Visibility.Collapsed;
            reward.Visibility = Visibility.Collapsed;
            fcd.Visibility = Visibility.Collapsed;

            edit_but.IsEnabled = true;
            Decision dec = (Decision)result.SelectedItem;
              bool s = dec.IsExcute;
          
            if (s == false)
            {
                excute_but.IsEnabled = true;
                show_but.IsEnabled = false;


            }
            else if (s == true)
            {

                excute_but.IsEnabled = false;
                show_but.IsEnabled = true;

            }

         
            

        }

        private void excute_but_Click(object sender, RoutedEventArgs e)
        {
            Decision dec = (Decision)result.SelectedItem;
            string con = dec.DecisionContent;
            long m = dec.DecisionId;


            if (con == "تبدل وظيفي")
            {

                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                reward.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;

                fc.dec_id.Text = m.ToString();
                fc.dec_id.Visibility = Visibility.Visible;
                fc.Visibility = Visibility.Visible;
                de.Visibility= Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                fc.DataContext = dec;
            }
            else if (con == "طي تبدل")
            {

                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                reward.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;
                fc.dec_id.Text = m.ToString();
                fc.dec_id.Visibility = Visibility.Visible;

                fc.Visibility = Visibility.Visible;
                de.Visibility = Visibility.Collapsed;
                fc.DataContext = dec;
            }
            else if (con == "قرارإجازة")
            {

                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                reward.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;
                res.dec_id.Text = m.ToString();
               // res.dec_id.Visibility = Visibility.Visible;
                res.Visibility = Visibility.Visible;
                de.Visibility = Visibility.Collapsed;
                res.DataContext = dec;



            }
           
            else if (con == "قرار إعارة")
            {

                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                reward.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;
                secondment.dec_id.Text = m.ToString();
                secondment.Visibility = Visibility.Visible;
                secondment.DataContext = dec;
               
               
              


            }

            else if (con == "قرار إيفاد")
            {

                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                reward.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;
                de.dec_id.Text = m.ToString();
                de.dec_id.Visibility = Visibility.Collapsed;
                res.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Visible;
                de.DataContext = dec;




            }

            else if (con == "قرار ندب")
            {

            
            
            
            }

            else if (con == "قرار عقوبة")
            {
                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                punish.dec_id.Text = m.ToString();
                secondment.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Visible;
                punish.DataContext = dec;
            }
            else if (con == "قرار ترفيعة")
            {

                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                reward.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;

                punish.Visibility = Visibility.Collapsed;
                bonus.dec_id.Text = m.ToString();
               

                bonus.Visibility = Visibility.Visible;
                bonus.DataContext = dec;


            }
            else if (con == "قرار مكافأة")
            {

                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                reward.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;

                reward.dec_id.Text = m.ToString();
                secondment.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;

                reward.Visibility = Visibility.Visible;
                reward.DataContext = dec;



            }

            //int dec_year = Int32.Parse(tx_year.Text);
            //  decisions = (from x in db.Decisions select x).ToList();

            //  List<Decision> dec = decisions.Where(d => d.DecisionYear == dec_year).ToList();
            //  result.ItemsSource = dec;
            excute_but.IsEnabled = false;
        
    }

        private void show_but_Click(object sender, RoutedEventArgs e)
        {

            Decision dec = (Decision)result.SelectedItem;

            if (dec.DecisionContent == "قرارإجازة")
            {


                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                reward.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;
                ObservableCollection<RestDetails> all = new ObservableCollection<RestDetails>();

                List<Rest> Custom_Dec = db.Rests.Where(x => x.DecisionId == dec.DecisionId).ToList();

                foreach (Rest r in Custom_Dec)
                {
                    RestDetails rd = new RestDetails();


                    SelfCard user = db.SelfCards.Where(x => x.PersonId == r.PersonId).FirstOrDefault();
                    rd.PersonName = user.FirstName + " " + user.FatherName + " " + user.LastName;
                    rd.restType = r.RestType;
                    rd.restPeriod = r.RestPeriod;
                    rd.period = r.Period;
                    rd.startDate = (DateTime)r.RestStart;
                    rd.endDate = (DateTime)r.RestEnd;
                    rd.note = r.Notes;
                    rd.attachment = r.Attachment;

                    all.Add(rd);


                }

                details.detailsDataGrid.ItemsSource = all;
                details.Visibility = Visibility.Visible;



            }

            //----------------------------------------

            if (dec.DecisionContent == "تبدل وظيفي")
            {

                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                reward.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;
                ObservableCollection<FunctionChangeDetails> all = new ObservableCollection<FunctionChangeDetails>();

                List<FunctionalChange> Custom_Dec = db.FunctionalChanges.Where(x => x.DecisionId == dec.DecisionId).ToList();

                foreach (FunctionalChange r in Custom_Dec)
                {
                    FunctionChangeDetails rd = new FunctionChangeDetails();


                    SelfCard user = db.SelfCards.Where(x => x.PersonId == r.PersonId).FirstOrDefault();
                    rd.PersonName = user.FirstName + " " + user.FatherName + " " + user.LastName;
                    rd.Category = r.Category;
                    rd.ChangeDate = r.ChangeDate;
                    rd.WorkPlace = r.WorkPlace;
                    rd.JobTitle = r.JobTitle;
                    rd.Status = r.Status;
                    rd.Mission = r.Mission;
                    rd.Salary =(double) r.Salary;


                    all.Add(rd);


                }

                fcd.detailsDataGrid.ItemsSource = all;
                fcd.Visibility = Visibility.Visible;
                


            }
            else if (dec.DecisionContent == "طي تبدل")
            {
                
            }
        

            else if (dec.DecisionContent == "قرار إعارة")
            {

                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                reward.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;
                ObservableCollection<SecondmentDetails> all = new ObservableCollection<SecondmentDetails>();

                List<Secondment> Custom_Dec = db.Secondments.Where(x => x.DecisionId == dec.DecisionId).ToList();

                foreach (Secondment r in Custom_Dec)
                {
                    SecondmentDetails rd = new SecondmentDetails();


                    SelfCard user = db.SelfCards.Where(x => x.PersonId == r.PersonId).FirstOrDefault();
                    rd.PersonName = user.FirstName + " " + user.FatherName + " " + user.LastName;
                    rd.SecondmentType = r.SecondmentType;
                    rd.PeriodNum = (int)r.PeriodNum;
                    rd.PeriodType = r.PeriodType;
                    rd.SecondmentStart = (DateTime)r.SecondmentStart;
                    rd.SecondmentEnd = (DateTime)r.SecondmentEnd;
                    rd.SecondmentPlace = r.SecondmentPlace;
                    
                    rd.Notes = r.Notes;


                    all.Add(rd);


                }

                sd.detailsDataGrid.ItemsSource = all;
                sd.Visibility = Visibility.Visible;
              





            }

            else if (dec.DecisionContent == "قرار إيفاد")
            {

                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                reward.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;
                ObservableCollection<DelegationDetails> all = new ObservableCollection<DelegationDetails>();

                List<Delegating> Custom_Dec = db.Delegatings.Where(x => x.DecisionId == dec.DecisionId).ToList();

                foreach (Delegating r in Custom_Dec)
                {

                    secondment.Visibility = Visibility.Collapsed;
                    fc.Visibility = Visibility.Collapsed;
                    mm.Visibility = Visibility.Collapsed;
                    ins.Visibility = Visibility.Collapsed;
                    details.Visibility = Visibility.Collapsed;
                    de.Visibility = Visibility.Collapsed;
                    punish.Visibility = Visibility.Collapsed;
                    sd.Visibility = Visibility.Collapsed;
                    dd.Visibility = Visibility.Collapsed;
                    bd.Visibility = Visibility.Collapsed;
                    rdd.Visibility = Visibility.Collapsed;
                    pd.Visibility = Visibility.Collapsed;
                    reward.Visibility = Visibility.Collapsed;
                    fcd.Visibility = Visibility.Collapsed;
                    DelegationDetails rd = new DelegationDetails();


                    SelfCard user = db.SelfCards.Where(x => x.PersonId == r.PersonId).FirstOrDefault();
                    rd.PersonName = user.FirstName + " " + user.FatherName + " " + user.LastName;
                    rd.DelegatingType = r.DelegatingType;
                    rd.PeriodNum = (int)r.PeriodNum;
                    rd.PeriodType = r.PeriodType;
                    rd.DelegatingStart = (DateTime)r.DelegatingStart;
                    rd.DelegatingEnd = (DateTime)r.DelegatingEnd;
                    rd.DelegatingReason = r.DelegatingReason;
                    rd.DelegatingCountry = r.DelegatingCountry;
                    rd.Notes = r.Notes;


                    all.Add(rd);


                }

                dd.detailsDataGrid.ItemsSource = all;
                dd.Visibility = Visibility.Visible;



            }

            else if (dec.DecisionContent == "قرار ندب")
            {




            }

            else if (dec.DecisionContent == "قرار عقوبة")
            {

                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                reward.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;
                ObservableCollection<PunishmentDetails> all = new ObservableCollection<PunishmentDetails>();

                List<Punishment> Custom_Dec = db.Punishments.Where(x => x.DecisionId == dec.DecisionId).ToList();

                foreach (Punishment r in Custom_Dec)
                {
                    PunishmentDetails rd = new PunishmentDetails();


                    SelfCard user = db.SelfCards.Where(x => x.PersonId == r.PersonId).FirstOrDefault();
                    rd.PersonName = user.FirstName + " " + user.FatherName + " " + user.LastName;
                    rd.PunishmentType = r.PunishmentType;
                    rd.Period = (int)r.Period;
                    rd.periodType = r.periodType;
                    rd.StartDate = (DateTime)r.StartDate;
                    rd.EndDate = (DateTime)r.EndDate;
                    rd.Reason = r.Reason;
                    rd.Discount =(double) r.Discount;
                    rd.Notes = r.Notes;


                    all.Add(rd);


                }

                pd.detailsDataGrid.ItemsSource = all;
                pd.Visibility = Visibility.Visible;


            }
            else if (dec.DecisionContent == "قرار ترفيعة")
            {


                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                reward.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;
                ObservableCollection<BonusDetails> all = new ObservableCollection<BonusDetails>();

                List<Bonuse> Custom_Dec = db.Bonuses.Where(x => x.DecisionId == dec.DecisionId).ToList();

                foreach (Bonuse r in Custom_Dec)
                {
                    BonusDetails rd = new BonusDetails();


                    SelfCard user = db.SelfCards.Where(x => x.PersonId == r.PersonId).FirstOrDefault();
                    rd.PersonName = user.FirstName + " " + user.FatherName + " " + user.LastName;
                    rd.Salary = r.Salary;
                    rd.Bouns = (int)r.Bouns;
                    rd.SalaryBouns = r.SalaryBouns;
                    rd.NumDays = r.NumDays;



                    all.Add(rd);


                }

                bd.detailsDataGrid.ItemsSource = all;
                bd.Visibility = Visibility.Visible;


            }
            else if (dec.DecisionContent == "قرار مكافأة")
            {

                secondment.Visibility = Visibility.Collapsed;
                fc.Visibility = Visibility.Collapsed;
                mm.Visibility = Visibility.Collapsed;
                ins.Visibility = Visibility.Collapsed;
                details.Visibility = Visibility.Collapsed;
                de.Visibility = Visibility.Collapsed;
                punish.Visibility = Visibility.Collapsed;
                sd.Visibility = Visibility.Collapsed;
                dd.Visibility = Visibility.Collapsed;
                bd.Visibility = Visibility.Collapsed;
                rdd.Visibility = Visibility.Collapsed;
                pd.Visibility = Visibility.Collapsed;
                reward.Visibility = Visibility.Collapsed;
                fcd.Visibility = Visibility.Collapsed;
                ObservableCollection<RewardDetails> all = new ObservableCollection<RewardDetails>();

                List<Reward> Custom_Dec = db.Rewards.Where(x => x.DecisionId == dec.DecisionId).ToList();

                foreach (Reward r in Custom_Dec)
                {
                    RewardDetails rd = new RewardDetails();


                    SelfCard user = db.SelfCards.Where(x => x.PersonId == r.PersonId).FirstOrDefault();
                    rd.PersonName = user.FirstName + " " + user.FatherName + " " + user.LastName;
                    rd.RewardType = r.RewardType;
                    rd.Reason = r.Reason;
                    rd.Amount = r.Amount;
                    rd.RewardDate = (DateTime)r.RewardDate;
                    rd.Notes = r.Notes;

                    all.Add(rd);


                }

                rdd.detailsDataGrid.ItemsSource = all;
                rdd.Visibility = Visibility.Visible;



            }

        }

     
    }

}