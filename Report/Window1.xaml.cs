using Microsoft.Reporting.WinForms;
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


namespace personnel.Report
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            reportViewer1.Load += ReportViewer_Load;

        }
        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                //Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new      
                //Microsoft.Reporting.WinForms.ReportDataSource();
                //personnelDataSet dataset = new personnelDataSet();

                //dataset.BeginInit();

                //reportDataSource1.Name = "DataSet1";
                ////Name of the report dataset in our .RDLC file

                //reportDataSource1.Value = dataset.self_cards;
                //this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);

                //this.reportViewer1.LocalReport.ReportPath = "./Report/PersReport.rdlc";                    
                //dataset.EndInit();




                ////fill data into WpfApplication4DataSet
                //personnelDataSetTableAdapters.self_cardsTableAdapter sel  = new   
                //personnelDataSetTableAdapters.self_cardsTableAdapter();

                //sel.ClearBeforeFill = true;
                //sel.Fill(dataset.self_cards);                    
                //reportViewer1.RefreshReport();
                //_isReportViewerLoaded = true;
                if (!_isReportViewerLoaded)
                {
                    Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                    Microsoft.Reporting.WinForms.ReportDataSource();
                    ds dataset = new ds();

                    dataset.BeginInit();

                    reportDataSource1.Name = "ss";
                    //Name of the report dataset in our .RDLC file

                    reportDataSource1.Value = dataset.sa;
                    ReportParameter rp = new ReportParameter("pid", 3.ToString());
                    this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);

                    this.reportViewer1.LocalReport.ReportPath = "./Report/Report1.rdlc";
                    
                    this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
                    dataset.EndInit();



                    //fill data into WpfApplication4DataSet
                    dsTableAdapters.saTableAdapter sel = new
                    dsTableAdapters.saTableAdapter();
                   

                    sel.ClearBeforeFill = true;
                    sel.Fill(dataset.sa, 3);
                    reportViewer1.RefreshReport();
                    _isReportViewerLoaded = true;

                }
            }
        }
    }
}

   

