using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for ZatReport.xaml
    /// </summary>
    public partial class ZatReport : Window
    {
       long id;
        public ZatReport(long id1 )
        {
            InitializeComponent();
            
            reportViewer1.Load += ReportViewer_Load;
            id = id1;
            

        }
        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            //if (!_isReportViewerLoaded)
            //    {
            //    Microsoft.Reporting.WinForms.ReportDataSource r = new
            //    Microsoft.Reporting.WinForms.ReportDataSource();
            //ZATDataSet1 dataset = new ZATDataSet1();








            //    //fill data into WpfApplication4DataSet
            //    ZATDataSet1TableAdapters.alTableAdapter ad1 = new
            //    ZATDataSet1TableAdapters.alTableAdapter();
            //    long m = 2;

            //    ad1.ClearBeforeFill = true;
            //    ReportParameter rp = new ReportParameter("pid", m.ToString());
            //    ad1.Fill(dataset.al, m);
            //    r.Name = "DataSet1";
            //    //Name of the report dataset in our .RDLC file

            //    r.Value = dataset.al;
            //    this.reportViewer1.LocalReport.ReportPath = "./Report/repzat.rdlc";
            //    reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });
            //    this.reportViewer1.LocalReport.DataSources.Add(r);



            //    dataset.EndInit();
            //    reportViewer1.RefreshReport();
            //    _isReportViewerLoaded = true;

            if (!_isReportViewerLoaded)
            {
                //Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
                //Microsoft.Reporting.WinForms.ReportDataSource();

                //ZATDataSet1 dataset = new ZATDataSet1();

                //dataset.BeginInit();

                //reportDataSource1.Name = "DataSet1";
                ////Name of the report dataset in our .RDLC file

                //reportDataSource1.Value = dataset.al;

                //ReportParameter rp = new ReportParameter("pid", 3.ToString());
                //this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);


                //this.reportViewer1.LocalReport.ReportPath = "./Report/repzat.rdlc";

                //this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });




                ////fill data into WpfApplication4DataSet
                //ZATDataSet1TableAdapters.alTableAdapter sel = new
                //ZATDataSet1TableAdapters.alTableAdapter();


                //sel.ClearBeforeFill = true;
                //sel.Fill(dataset.al, 3);
                //reportViewer1.RefreshReport();
                //dataset.EndInit();
                //_isReportViewerLoaded = true;


                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
             Microsoft.Reporting.WinForms.ReportDataSource();
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2= new
            Microsoft.Reporting.WinForms.ReportDataSource();
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new
           Microsoft.Reporting.WinForms.ReportDataSource();
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new
           Microsoft.Reporting.WinForms.ReportDataSource();
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new
                         Microsoft.Reporting.WinForms.ReportDataSource();
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new
                         Microsoft.Reporting.WinForms.ReportDataSource();

                ZATDataSet1 dataset = new ZATDataSet1();
                FirstDataSet1 dataset2 = new FirstDataSet1();
                DataSet6 dataset3 = new DataSet6();
                dataset.BeginInit();
                dataset2.BeginInit();
                dataset3.BeginInit();
                reportDataSource1.Name = "DataSet1";
                reportDataSource2.Name = "DataSet2";
                reportDataSource3.Name = "DataSet3";
                reportDataSource4.Name = "DataSet4";
                reportDataSource5.Name = "DataSet5";
                reportDataSource6.Name = "DataSet6";
                //Name of the report dataset in our .RDLC file

                reportDataSource1.Value = dataset.al;
                reportDataSource2.Value = dataset.zat_fun;
                reportDataSource3.Value = dataset.zat_pun;
                reportDataSource4.Value = dataset.zat_rest;
                reportDataSource5.Value = dataset2.zat_fun_first;
                reportDataSource6.Value = dataset3.zat_fun1;
                ReportParameter rp = new ReportParameter("pid", id.ToString());
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource5);
                this.reportViewer1.LocalReport.DataSources.Add(reportDataSource6);
                this.reportViewer1.LocalReport.ReportPath = "./Report/repzat.rdlc";

                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp });




                //fill data into WpfApplication4DataSet
                ZATDataSet1TableAdapters.alTableAdapter sel = new ZATDataSet1TableAdapters.alTableAdapter();
                ZATDataSet1TableAdapters.zat_funTableAdapter fun = new ZATDataSet1TableAdapters.zat_funTableAdapter();
                ZATDataSet1TableAdapters.zat_punTableAdapter pun = new ZATDataSet1TableAdapters.zat_punTableAdapter();
                ZATDataSet1TableAdapters.zat_restTableAdapter res = new ZATDataSet1TableAdapters.zat_restTableAdapter();
                FirstDataSet1TableAdapters.zat_fun_firstTableAdapter fr = new FirstDataSet1TableAdapters.zat_fun_firstTableAdapter();
                DataSet6TableAdapters.zat_fun1TableAdapter fun1 = new DataSet6TableAdapters.zat_fun1TableAdapter();
                sel.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["personelConfig"].ConnectionString;
                fun.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["personelConfig"].ConnectionString;
                pun.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["personelConfig"].ConnectionString;
                res.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["personelConfig"].ConnectionString;
                fr.Connection.ConnectionString= ConfigurationManager.ConnectionStrings["personelConfig"].ConnectionString;
                fun1.Connection.ConnectionString= ConfigurationManager.ConnectionStrings["personelConfig"].ConnectionString;
                sel.ClearBeforeFill = true;
                fun.ClearBeforeFill = true;
                pun.ClearBeforeFill = true;
                res.ClearBeforeFill = true;
                fr.ClearBeforeFill = true;
                fun1.ClearBeforeFill = true;
                sel.Fill(dataset.al,id);
                fun.Fill(dataset.zat_fun,id);
                pun.Fill(dataset.zat_pun,id);
                res.Fill(dataset.zat_rest,id);
                fr.Fill(dataset2.zat_fun_first,id);
                fun1.Fill(dataset3.zat_fun1, id);


                reportViewer1.RefreshReport();
                dataset.EndInit();
                _isReportViewerLoaded = true;



                //viewer.LocalReport.DataSources.Add(new ReportDataSource(reportDataSource, dataset.Tables[0]));
                //viewer.LocalReport.DataSources.Add(new ReportDataSource("reportDataSource1", dataset.Tables[1]));
                //viewer.LocalReport.DataSources.Add(new ReportDataSource("reportDataSource2", dataset.Tables[2]));
                //viewer.LocalReport.DataSources.Add(new ReportDataSource("reportDataSource3", dataset.Tables[3]));
                //viewer.LocalReport.DataSources.Add(new ReportDataSource("reportDataSource4", dataset.Tables[4]));


            }
        }
    }
}
