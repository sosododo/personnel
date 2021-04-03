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
using personnel.Models;

namespace personnel.Views
{
    /// <summary>
    /// Interaction logic for Decision_Collaps.xaml
    /// </summary>
    public partial class Decision_Collaps : UserControl
    {
        PersonelDBContext db;
        public Decision_Collaps()
        {
            InitializeComponent();
            Decision d = (Decision)DataContext;
        }

        private void collaps_Click(object sender, RoutedEventArgs e)
        {
            //Decision d = (Decision)DataContext;

            try
            {
                Decision des = new Decision();
                db = new PersonelDBContext();
                int number = Int32.Parse(num.Text);
                des = db.Decisions.Where(x => x.DecisionNumber == number && x.DecisionYear ==Int32.Parse( num_Copy.Text ) && x.DecisionSource==sorce.Text).FirstOrDefault();
                if (des == null)
                {
                    MessageBox.Show("هذا القرار غير موجود");

                }
                else
                {
                    string con = des.DecisionContent;
                    if (con == "تبدل وظيفي")
                    {
                        var x = (from y in db.FunctionalChanges
                                 where y.DecisionId == des.DecisionId
                                 select y);
                        foreach (var row in x.ToList())
                            db.FunctionalChanges.Remove(row);
                    }
                    else if (con == "قرار طي تبدل")
                    {

                    }
                    else if (con == "قرارإجازة")
                    {
                        var x = (from y in db.Rests
                                 where y.DecisionId == des.DecisionId
                                 select y);
                        foreach (var row in x.ToList())
                            db.Rests.Remove(row);
                    }

                    else if (con == "قرار إعارة")
                    {
                        var x = (from y in db.Secondments
                                 where y.DecisionId == des.DecisionId
                                 select y);
                        foreach (var row in x.ToList())
                            db.Secondments.Remove(row);

                    }

                    else if (con == "قرار إيفاد")
                    {
                        var x = (from y in db.Delegatings
                                 where y.DecisionId == des.DecisionId
                                 select y);
                        foreach (var row in x.ToList())
                            db.Delegatings.Remove(row);

                    }

                    else if (con == "قرار ندب")
                    {
                        var x = (from y in db.Scars
                                 where y.DecisionId == des.DecisionId
                                 select y);
                        foreach (var row in x.ToList())
                            db.Scars.Remove(row);

                    }

                    else if (con == "قرار عقوبة")
                    {
                        var x = (from y in db.Punishments
                                 where y.DecisionId == des.DecisionId
                                 select y);
                        foreach (var row in x.ToList())
                            db.Punishments.Remove(row);

                    }
                    else if (con == "قرار ترفيعة")
                    {
                        var x = (from y in db.Bonuses
                                 where y.DecisionId == des.DecisionId
                                 select y);
                        foreach (var row in x.ToList())
                            db.Bonuses.Remove(row);

                    }
                    else if (con == "قرار ترفيعة استثنائية")
                    {
                        var x = (from y in db.Bonuses
                                 where y.DecisionId == des.DecisionId
                                 select y);
                        foreach (var row in x.ToList())
                            db.Bonuses.Remove(row);

                    }
                    else if (con == "قرار ترفيعة تدريسية")
                    {
                        var x = (from y in db.Bonuses
                                 where y.DecisionId == des.DecisionId
                                 select y);
                        foreach (var row in x.ToList())
                            db.Bonuses.Remove(row);

                    }
                    else if (con == "قرار مكافأة")
                    {
                        var x = (from y in db.Rewards
                                 where y.DecisionId == des.DecisionId
                                 select y);
                        foreach (var row in x.ToList())
                            db.Rewards.Remove(row);
                    }
                    else if (con == "قرار زيادة")
                    {
                        //var x = (from y in db.FunctionalChanges
                        //         where y.DecisionId == des.DecisionId
                        //         select y).FirstOrDefault();
                        //db.FunctionalChanges.Remove(x);
                        //db.SaveChanges();

                    }

                    des.DecisionStatus = "قرار مطوي";
                    des.CollapsDate = date.SelectedDate;
                    db.Decisions.Update(des);
                    db.SaveChanges();
                    MessageBox.Show("تم طي القرار بنجاح");

                    string message = "هل انتهى تنفيذ القرار؟";
                    string caption = "تنبيه";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {

                        this.Visibility = Visibility.Collapsed;
                        var d = db.Decisions.Where(c => c.DecisionId == long.Parse(dec_id.Text)).Single();
                        excute.IsChecked = true;
                        d.IsExcute = true;
                        db.Decisions.Update(d);
                        db.SaveChanges();


                        Decision_View dv = new Decision_View();
                        Window parentWindow = Window.GetWindow(this);
                        parentWindow.Close();
                        dv.Show();
                        //Window parentWindow = Window.GetWindow(this);
                        //object obj = parentWindow.FindName("result");
                        //DataGrid dg = (DataGrid)obj;
                        //dg.UnselectAll();

                    }
                    else if (result == MessageBoxResult.No)
                    {
                        num.Text = null;
                        date.SelectedDate = null;
                        num_Copy.Text = null;
                        sorce.SelectedItem = null;

                    }
                }
            }


            catch (Exception m)
            { MessageBox.Show("يوجد خطأ"); }
        }
    }
    }

