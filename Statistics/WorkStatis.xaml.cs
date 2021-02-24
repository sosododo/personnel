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

namespace personnel.Statistics
{
    /// <summary>
    /// Interaction logic for ZatReport.xaml
    /// </summary>
    public partial class WorkStatis : Window
    {
        string cert_name; string status;
        public WorkStatis(string cert_name1,string status1 )
        {
            InitializeComponent();
            
            reportViewer2.Load += ReportViewer_Load;
            this.cert_name = cert_name1;
            this.status = status1;
            

        }
        private bool _isReportViewerLoaded;

        private void ReportViewer_Load(object sender, EventArgs e)
        {
           



            if (!_isReportViewerLoaded)
            {



                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new
             Microsoft.Reporting.WinForms.ReportDataSource();
                //     Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2= new
                // Microsoft.Reporting.WinForms.ReportDataSource();
                //     Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new
                //Microsoft.Reporting.WinForms.ReportDataSource();
                //     Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new
                //Microsoft.Reporting.WinForms.ReportDataSource();


                //     ZATDataSet1 dataset = new ZATDataSet1();
                WorkDataSet1 ds = new WorkDataSet1();
                //     dataset.BeginInit();
                ds.BeginInit();
                //     reportDataSource1.Name = "DataSet1";
                reportDataSource1.Name = "DataSet1";
                //     reportDataSource2.Name = "DataSet2";
                //     reportDataSource3.Name = "DataSet3";
                //     reportDataSource4.Name = "DataSet4";
                //     //Name of the report dataset in our .RDLC file

                //     
                reportDataSource1.Value = ds.work_rep;
                //     reportDataSource2.Value = dataset.zat_fun;
                //     reportDataSource3.Value = dataset.zat_pun;
                //     reportDataSource4.Value = dataset.zat_rest;
                //     ReportParameter rp = new ReportParameter("pid", id.ToString());
                //     this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
                //     this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
                //     this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
                //     this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
               ReportParameter rp = new ReportParameter("certname",cert_name);
                ReportParameter rp2 = new ReportParameter("status", status);
                this.reportViewer2.LocalReport.ReportPath = "./Statistics/workstatis.rdlc";
                this.reportViewer2.LocalReport.DataSources.Add(reportDataSource1);
                /*this.reportViewer2.LocalReport.SetParameters(new ReportParameter[] { rp , rp2});*/



                WorkDataSet1TableAdapters.work_repTableAdapter rr = new WorkDataSet1TableAdapters.work_repTableAdapter();
         
                //ZATDataSet1TableAdapters.alTableAdapter sel = new ZATDataSet1TableAdapters.alTableAdapter();
                //ZATDataSet1TableAdapters.zat_funTableAdapter fun = new ZATDataSet1TableAdapters.zat_funTableAdapter();
                //ZATDataSet1TableAdapters.zat_punTableAdapter pun = new ZATDataSet1TableAdapters.zat_punTableAdapter();
                //ZATDataSet1TableAdapters.zat_restTableAdapter res = new ZATDataSet1TableAdapters.zat_restTableAdapter();

                //sel.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["personelConfig"].ConnectionString;
                //fun.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["personelConfig"].ConnectionString;
                //pun.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["personelConfig"].ConnectionString;
                //res.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["personelConfig"].ConnectionString;
                rr.Connection.ConnectionString= ConfigurationManager.ConnectionStrings["personelConfig"].ConnectionString;
                rr.ClearBeforeFill = true;
                rr.Fill(ds.work_rep,cert_name,status);
                
                //sel.ClearBeforeFill = true;
                //fun.ClearBeforeFill = true;
                //pun.ClearBeforeFill = true;
                //res.ClearBeforeFill = true;
                //sel.Fill(dataset.al, id);
                //fun.Fill(dataset.zat_fun, id);
                //pun.Fill(dataset.zat_pun, id);
                //res.Fill(dataset.zat_rest, id);




                reportViewer2.RefreshReport();
                //dataset.EndInit();
                _isReportViewerLoaded = true;



         


            }
        }
    }
}
