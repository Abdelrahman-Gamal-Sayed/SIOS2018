using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Microsoft.Win32;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;
using System.Globalization;
using System.Threading;
using WpfApplication2.ReportsLayer;

    
 
namespace WpfApplication2
{


    public partial class fwateer : Window
    {
        public fwateer()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

             startfun();
        }
        DB db = new DB();

        void startfun()
        {
            fillcompanyNumber();
            fillProvider();
            fillClass();
            if (User.CompanyID == "10000" || User.Type == "DMS Member")
            {
                cbxStartCompNum.IsEnabled = true;
                cbxEndCompNum.IsEnabled = true;
            }
            else
            {
                cbxStartCompNum.IsEnabled = false;
                cbxEndCompNum.IsEnabled = false;
                cbxStartCompNum.Text = User.CompanyID;
                cbxEndCompNum.Text = User.CompanyID;
            }
        }
        DataSet dt1;
        private void fillcompanyNumber()
        {
         //   dt1 = db.RunReaderds("select distinct COMP_ID from A_REP_01 ORDER BY COMP_ID");
          //  cbxStartCompNum.ItemsSource = cbxEndCompNum.ItemsSource = dt1.Tables[0].DefaultView;
            cbxStartCompNum.ItemsSource = User.ALL_Company().DefaultView;
        }

        private void fillProvider()
        {
            //  dt1 = db.RunReaderds("select distinct pr_code from irs_serv_providers order by pr_code");
            //  cbxStartProvider.ItemsSource = cbxEndProvider.ItemsSource = dt1.Tables[0].DefaultView;

        }
        private void fillClass()
        {
            dt1 = db.RunReaderds("select distinct CLASS_CODE from V_CLASS_NAME order by CLASS_CODE");
            cbxStartClass.ItemsSource = cbxEndClass.ItemsSource = dt1.Tables[0].DefaultView;
        }



        private void btnExite_Click(object sender, RoutedEventArgs e)
        {
            Close();

            /*     Int32 comp1, comp2;
            int prv1, prv2;
            DateTime sdat1, sdat2, cdat1, cdat2;
            string cls1, cls2;

            comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
            comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
            prv1 = cbxStartProvider.Text == string.Empty ? 0 : Convert.ToInt16(cbxStartProvider.Text);
            prv2 = cbxEndProvider.Text == string.Empty ? 999999 : Convert.ToInt16(cbxEndProvider.Text);
            cls1 = cbxStartClass.Text == string.Empty ? " " : cbxStartClass.Text;
            cls2 = cbxEndClass.Text == string.Empty ? "FFFFF" : cbxEndClass.Text;

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            sdat1 = (DateTime)startdateclm.SelectedDate.Value.Date;
            sdat2 = (DateTime)enddateclm.SelectedDate.Value.Date;
            cdat1 = (DateTime)startdatecreat.SelectedDate.Value.Date;
            cdat2 = (DateTime)enddatecreat.SelectedDate.Value.Date;

            View_Report showreport = new View_Report();
            ReportOnline___Copy_1 repo4 = new ReportOnline___Copy_1();
          //  ReportIRS___Copy_2 repo4 = new ReportIRS___Copy_2();
            repo4.SetDatabaseLogon("APP", "12369");
            repo4.SetParameterValue("comp1", comp1);
            repo4.SetParameterValue("comp2", comp2);

            repo4.SetParameterValue("crda1", cdat1);
            repo4.SetParameterValue("crda2", cdat2);
            repo4.SetParameterValue("seda1", sdat1);
            repo4.SetParameterValue("seda2", sdat2);

            repo4.SetParameterValue("prv1", prv1);
            repo4.SetParameterValue("prv2", prv2);
            repo4.SetParameterValue("cls1", cls1);
            repo4.SetParameterValue("cls2", cls2);

            repo4.SetParameterValue("SRT", 0);



            showreport.crystalReportViewer1.ReportSource = repo4;
            showreport.ShowDialog();

            if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ExportOptions exp = new ExportOptions();
                DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                PdfFormatOptions expdf = new PdfFormatOptions();
                string sa = "";

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Pdf file|*.pdf";
                sfd.FileName = "Report";
                if (sfd.ShowDialog() == true)
                    sa = sfd.FileName;

                dis.DiskFileName = sa;
                exp = repo4.ExportOptions;
                exp.ExportDestinationType = ExportDestinationType.DiskFile;
                exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                exp.ExportFormatOptions = expdf;
                exp.ExportDestinationOptions = dis;
                repo4.Export();

                MessageBox.Show("Successfull Export to Pdf");

                if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExcelFormatOptions exexl = new ExcelFormatOptions();
                    string sa1 = "";

                    SaveFileDialog sfd1 = new SaveFileDialog();
                    sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    sfd1.FileName = "Report";
                    if (sfd1.ShowDialog() == true)
                        sa1 = sfd1.FileName;

                    dis.DiskFileName = sa1;
                    exp = repo4.ExportOptions;

                    exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    exp.ExportFormatType = ExportFormatType.ExcelRecord;
                    exp.ExportFormatOptions = exexl;
                    exp.ExportDestinationOptions = dis;
                    repo4.Export();
                    MessageBox.Show("Successfull Export to Excel");

                }
                else
                    MessageBox.Show("Thank you");
            }
            else if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ExportOptions exp = new ExportOptions();
                DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                ExcelFormatOptions exexl = new ExcelFormatOptions();
                string sa1 = "";

                SaveFileDialog sfd1 = new SaveFileDialog();
                sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                sfd1.FileName = "Report";
                if (sfd1.ShowDialog() == true)
                    sa1 = sfd1.FileName;

                dis.DiskFileName = sa1;
                exp = repo4.ExportOptions;

                exp.ExportDestinationType = ExportDestinationType.DiskFile;
                exp.ExportFormatType = ExportFormatType.ExcelRecord;

                exp.ExportFormatOptions = exexl;
                exp.ExportDestinationOptions = dis;
                repo4.Export();
                MessageBox.Show("Successfull Export to Excel");
            }
            else
                MessageBox.Show("Thank you");
        * */

        }

        private void cbxStartCompNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsNumber(e.Key))
                e.Handled = true;

        }
        private bool IsNumber(Key key)
        {
            switch (key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                case Key.Back:
                case Key.Delete:
                    return true;
            }
            return false;
        }


        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Int32 comp1, comp2;
            Int32 prv1, prv2;
            DateTime sdat1, sdat2, cdat1, cdat2;
            string cls1, cls2;

            comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
            comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
            prv1 = cbxStartProvider.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartProvider.Text);
            prv2 = cbxEndProvider.Text == string.Empty ? 999999 : Convert.ToInt32(cbxEndProvider.Text);
            cls1 = cbxStartClass.Text == string.Empty ? " " : cbxStartClass.Text;
            cls2 = cbxEndClass.Text == string.Empty ? "zzzzzzz" : cbxEndClass.Text;

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            sdat1 = (DateTime)startdateclm.SelectedDate.Value.Date;
            sdat2 = (DateTime)enddateclm.SelectedDate.Value.Date;
            cdat1 = (DateTime)startdatecreat.SelectedDate.Value.Date;
            cdat2 = (DateTime)enddatecreat.SelectedDate.Value.Date;

            View_Report showreport = new View_Report();
            if (comp1 == 500114 || comp2 == 500114)
            {
                //ReportIRS repo4 = new ReportIRS();
                ReportIRS___Copy repo4 = new ReportIRS___Copy();
                //ReportIRS___Copy_1 repo4 = new ReportIRS___Copy_1();
                // repo4.SetDatabaseLogon("APP", "12369");
                // repo4.SetDatabaseLogon("DMS", "dms911care");
                repo4.SetDatabaseLogon("APP", "12369");
                repo4.SetParameterValue("comp1", comp1);
                repo4.SetParameterValue("comp2", comp2);

                repo4.SetParameterValue("crda1", cdat1);
                repo4.SetParameterValue("crda2", cdat2);
                repo4.SetParameterValue("seda1", sdat1);
                repo4.SetParameterValue("seda2", sdat2);

                repo4.SetParameterValue("prv1", prv1);
                repo4.SetParameterValue("prv2", prv2);
                repo4.SetParameterValue("cls1", cls1);
                repo4.SetParameterValue("cls2", cls2);

                repo4.SetParameterValue("SRT", 0);


                showreport.crystalReportViewer1.ReportSource = repo4;
                showreport.ShowDialog();

                if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExportOptions exp = new ExportOptions();
                    DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                    ExcelFormatOptions exexl = new ExcelFormatOptions();
                    string sa1 = "";

                    SaveFileDialog sfd1 = new SaveFileDialog();
                    sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    sfd1.FileName = "Report";
                    if (sfd1.ShowDialog() == true)
                        sa1 = sfd1.FileName;

                    dis.DiskFileName = sa1;
                    exp = repo4.ExportOptions;

                    exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    exp.ExportFormatType = ExportFormatType.ExcelRecord;

                    exp.ExportFormatOptions = exexl;
                    exp.ExportDestinationOptions = dis;
                    repo4.Export();
                    MessageBox.Show("Successfull Export to Excel");
                }
                else
                    MessageBox.Show("Thank you");
            }
            else
            {
                ReportIRS repo4 = new ReportIRS();

                // ReportIRSTest2222 repo4 = new ReportIRSTest2222();
                //ReportIRS___Copy repo4 = new ReportIRS___Copy();
                //ReportIRS___Copy_1 repo4 = new ReportIRS___Copy_1();
                // repo4.SetDatabaseLogon("APP", "12369");
                // repo4.SetDatabaseLogon("DMS", "dms911care");
                repo4.SetDatabaseLogon("APP", "12369");
                repo4.SetParameterValue("comp1", comp1);
                repo4.SetParameterValue("comp2", comp2);

                repo4.SetParameterValue("crda1", cdat1);
                repo4.SetParameterValue("crda2", cdat2);
                repo4.SetParameterValue("seda1", sdat1);
                repo4.SetParameterValue("seda2", sdat2);

                repo4.SetParameterValue("prv1", prv1);
                repo4.SetParameterValue("prv2", prv2);
                repo4.SetParameterValue("cls1", cls1);
                repo4.SetParameterValue("cls2", cls2);

                repo4.SetParameterValue("SRT", 0);


                showreport.crystalReportViewer1.ReportSource = repo4;
                showreport.ShowDialog();

                if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExportOptions exp = new ExportOptions();
                    DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                    ExcelFormatOptions exexl = new ExcelFormatOptions();
                    string sa1 = "";

                    SaveFileDialog sfd1 = new SaveFileDialog();
                    sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    sfd1.FileName = "Report";
                    if (sfd1.ShowDialog() == true)
                        sa1 = sfd1.FileName;

                    dis.DiskFileName = sa1;
                    exp = repo4.ExportOptions;

                    exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    exp.ExportFormatType = ExportFormatType.ExcelRecord;

                    exp.ExportFormatOptions = exexl;
                    exp.ExportDestinationOptions = dis;
                    repo4.Export();
                    MessageBox.Show("Successfull Export to Excel");
                }
                else
                    MessageBox.Show("Thank you");
            }
        }

        private void btnPrint_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Int32 comp1, comp2;
            Int32 prv1, prv2;
            DateTime sdat1, sdat2, cdat1, cdat2;
            string cls1, cls2;

            comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
            comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
            prv1 = cbxStartProvider.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartProvider.Text);
            prv2 = cbxEndProvider.Text == string.Empty ? 999999 : Convert.ToInt32(cbxEndProvider.Text);
            cls1 = cbxStartClass.Text == string.Empty ? " " : cbxStartClass.Text;
            cls2 = cbxEndClass.Text == string.Empty ? "zzzzzzz" : cbxEndClass.Text;

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            sdat1 = (DateTime)startdateclm.SelectedDate.Value.Date;
            sdat2 = (DateTime)enddateclm.SelectedDate.Value.Date;
            cdat1 = (DateTime)startdatecreat.SelectedDate.Value.Date;
            cdat2 = (DateTime)enddatecreat.SelectedDate.Value.Date;

            View_Report showreport = new View_Report();

            if (comp1 == 800800 || comp2 == 800800)
            {
                //  ReportOnline repo4 = new ReportOnline();
                // ReportOnline2 repo4 = new ReportOnline2();

                ReportOnline2___Copy_1 repo4 = new ReportOnline2___Copy_1();

                repo4.SetDatabaseLogon("APP", "12369");
                repo4.SetParameterValue("comp1", comp1);
                repo4.SetParameterValue("comp2", comp2);

                repo4.SetParameterValue("crda1", cdat1);
                repo4.SetParameterValue("crda2", cdat2);
                repo4.SetParameterValue("seda1", sdat1);
                repo4.SetParameterValue("seda2", sdat2);

                repo4.SetParameterValue("prv1", prv1);
                repo4.SetParameterValue("prv2", prv2);
                repo4.SetParameterValue("cls1", cls1);
                repo4.SetParameterValue("cls2", cls2);

                repo4.SetParameterValue("SRT", 0);



                showreport.crystalReportViewer1.ReportSource = repo4;
                showreport.ShowDialog();

                if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExportOptions exp = new ExportOptions();
                    DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                    ExcelFormatOptions exexl = new ExcelFormatOptions();
                    string sa1 = "";

                    SaveFileDialog sfd1 = new SaveFileDialog();
                    sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    sfd1.FileName = "Report";
                    if (sfd1.ShowDialog() == true)
                        sa1 = sfd1.FileName;

                    dis.DiskFileName = sa1;
                    exp = repo4.ExportOptions;

                    exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    exp.ExportFormatType = ExportFormatType.ExcelRecord;

                    exp.ExportFormatOptions = exexl;
                    exp.ExportDestinationOptions = dis;
                    repo4.Export();
                    MessageBox.Show("Successfull Export to Excel");
                }
                else
                    MessageBox.Show("Thank you");
            }
            //else if ((comp1 >= 500118 && comp1 <= 500122) || (comp2 >= 500118 && comp2 <= 500122))
            else if (comp1 == 500114 || comp2 == 500114)
            {
                //  ReportOnline repo4 = new ReportOnline();
                // ReportOnline2 repo4 = new ReportOnline2();
                //ReportOnline2___Copy_1___Copy repo4 = new ReportOnline2___Copy_1___Copy();
                //TOP
                //ReportOnlinecopy2 repo4 = new ReportOnlinecopy2();

                ReportOnlinecopy500114Test repo4 = new ReportOnlinecopy500114Test();
                repo4.SetDatabaseLogon("APP", "12369");
                repo4.SetParameterValue("comp1", comp1);
                repo4.SetParameterValue("comp2", comp2);

                repo4.SetParameterValue("crda1", cdat1);
                repo4.SetParameterValue("crda2", cdat2);
                repo4.SetParameterValue("seda1", sdat1);
                repo4.SetParameterValue("seda2", sdat2);

                repo4.SetParameterValue("prv1", prv1);
                repo4.SetParameterValue("prv2", prv2);
                repo4.SetParameterValue("cls1", cls1);
                repo4.SetParameterValue("cls2", cls2);

                repo4.SetParameterValue("SRT", 0);
                repo4.SetParameterValue("comp", comp1);


                showreport.crystalReportViewer1.ReportSource = repo4;
                showreport.ShowDialog();

                if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExportOptions exp = new ExportOptions();
                    DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                    ExcelFormatOptions exexl = new ExcelFormatOptions();
                    string sa1 = "";

                    SaveFileDialog sfd1 = new SaveFileDialog();
                    sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    sfd1.FileName = "Report";
                    if (sfd1.ShowDialog() == true)
                        sa1 = sfd1.FileName;

                    dis.DiskFileName = sa1;
                    exp = repo4.ExportOptions;

                    exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    exp.ExportFormatType = ExportFormatType.ExcelRecord;

                    exp.ExportFormatOptions = exexl;
                    exp.ExportDestinationOptions = dis;
                    repo4.Export();
                    MessageBox.Show("Successfull Export to Excel");
                }
                else
                    MessageBox.Show("Thank you");
            }
            else if (comp1 == 500118 || comp2 == 500118)
            {
                //  ReportOnline repo4 = new ReportOnline();
                // ReportOnline2 repo4 = new ReportOnline2();
                //ReportOnline2___Copy_1___Copy repo4 = new ReportOnline2___Copy_1___Copy();

                //TOP
                //ReportOnline500118 repo4 = new ReportOnline500118();
                ReportOnline500118Test repo4 = new ReportOnline500118Test();

                repo4.SetDatabaseLogon("APP", "12369");
                repo4.SetParameterValue("comp1", comp1);
                repo4.SetParameterValue("comp2", comp2);

                repo4.SetParameterValue("crda1", cdat1);
                repo4.SetParameterValue("crda2", cdat2);
                repo4.SetParameterValue("seda1", sdat1);
                repo4.SetParameterValue("seda2", sdat2);

                repo4.SetParameterValue("prv1", prv1);
                repo4.SetParameterValue("prv2", prv2);
                repo4.SetParameterValue("cls1", cls1);
                repo4.SetParameterValue("cls2", cls2);

                repo4.SetParameterValue("SRT", 0);
                repo4.SetParameterValue("comp", comp1);




                showreport.crystalReportViewer1.ReportSource = repo4;
                showreport.ShowDialog();

                if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExportOptions exp = new ExportOptions();
                    DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                    ExcelFormatOptions exexl = new ExcelFormatOptions();
                    string sa1 = "";

                    SaveFileDialog sfd1 = new SaveFileDialog();
                    sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    sfd1.FileName = "Report";
                    if (sfd1.ShowDialog() == true)
                        sa1 = sfd1.FileName;

                    dis.DiskFileName = sa1;
                    exp = repo4.ExportOptions;

                    exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    exp.ExportFormatType = ExportFormatType.ExcelRecord;

                    exp.ExportFormatOptions = exexl;
                    exp.ExportDestinationOptions = dis;
                    repo4.Export();
                    MessageBox.Show("Successfull Export to Excel");
                }
                else
                    MessageBox.Show("Thank you");
            }
            else
            {

                ReportOnlineAllTest repo4 = new ReportOnlineAllTest();



                //  ReportOnline___Copy repo4 = new ReportOnline___Copy();

                //  ReportOnline_copy_2___Copy_1 repo4 = new ReportOnline_copy_2___Copy_1();

                //Top
                //ReportOnline_copy_2 repo4 = new ReportOnline_copy_2();


                repo4.SetDatabaseLogon("APP", "12369");




                repo4.SetParameterValue("comp1", comp1);
                repo4.SetParameterValue("comp2", comp2);

                repo4.SetParameterValue("crda1", cdat1);
                repo4.SetParameterValue("crda2", cdat2);
                repo4.SetParameterValue("seda1", sdat1);
                repo4.SetParameterValue("seda2", sdat2);

                repo4.SetParameterValue("prv1", prv1);
                repo4.SetParameterValue("prv2", prv2);
                repo4.SetParameterValue("cls1", cls1);
                repo4.SetParameterValue("cls2", cls2);

                repo4.SetParameterValue("SRT", 0);

                //repo4.SetDatabaseLogon("SH_01", "***");
                repo4.SetParameterValue("comp", comp1);

                showreport.crystalReportViewer1.ReportSource = repo4;
                showreport.ShowDialog();

                if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExportOptions exp = new ExportOptions();
                    DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                    ExcelFormatOptions exexl = new ExcelFormatOptions();
                    string sa1 = "";

                    SaveFileDialog sfd1 = new SaveFileDialog();
                    sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    sfd1.FileName = "Report";
                    if (sfd1.ShowDialog() == true)
                        sa1 = sfd1.FileName;

                    dis.DiskFileName = sa1;
                    exp = repo4.ExportOptions;

                    exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    exp.ExportFormatType = ExportFormatType.ExcelRecord;

                    exp.ExportFormatOptions = exexl;
                    exp.ExportDestinationOptions = dis;
                    repo4.Export();
                    MessageBox.Show("Successfull Export to Excel");
                }
                else
                    MessageBox.Show("Thank you");
            }
        }

        private void btnPrint_Copy2_Click(object sender, RoutedEventArgs e)
        {
            Int32 comp1, comp2;
            Int32 prv1, prv2;
            DateTime sdat1, sdat2, cdat1, cdat2;
            string cls1, cls2;

            comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
            comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
            prv1 = cbxStartProvider.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartProvider.Text);
            prv2 = cbxEndProvider.Text == string.Empty ? 999999 : Convert.ToInt32(cbxEndProvider.Text);
            cls1 = cbxStartClass.Text == string.Empty ? " " : cbxStartClass.Text;
            cls2 = cbxEndClass.Text == string.Empty ? "zzzzzzz" : cbxEndClass.Text;

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            sdat1 = (DateTime)startdateclm.SelectedDate.Value.Date;
            sdat2 = (DateTime)enddateclm.SelectedDate.Value.Date;
            cdat1 = (DateTime)startdatecreat.SelectedDate.Value.Date;
            cdat2 = (DateTime)enddatecreat.SelectedDate.Value.Date;

            View_Report showreport = new View_Report();


            if (comp1 == 800800 || comp2 == 800800)
            {
                ReportAll repo4 = new ReportAll();
                // repo4.SetDatabaseLogon("APP", "12369");
                repo4.SetDatabaseLogon("IRS", "2020", "217.139.89.20/ora11g", "IRS");
                repo4.SetDatabaseLogon("APP", "12369", "171.0.1.96 /ora11g", "APP");
                repo4.SetParameterValue("comp1", comp1);
                repo4.SetParameterValue("comp2", comp2);

                repo4.SetParameterValue("crda1", cdat1);
                repo4.SetParameterValue("crda2", cdat2);
                repo4.SetParameterValue("seda1", sdat1);
                repo4.SetParameterValue("seda2", sdat2);

                repo4.SetParameterValue("prv1", prv1);
                repo4.SetParameterValue("prv2", prv2);
                repo4.SetParameterValue("cls1", cls1);
                repo4.SetParameterValue("cls2", cls2);

                repo4.SetParameterValue("SRT", 0);


                showreport.crystalReportViewer1.ReportSource = repo4;
                showreport.ShowDialog();

                if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExportOptions exp = new ExportOptions();
                    DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                    ExcelFormatOptions exexl = new ExcelFormatOptions();
                    string sa1 = "";

                    SaveFileDialog sfd1 = new SaveFileDialog();
                    sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    sfd1.FileName = "Report";
                    if (sfd1.ShowDialog() == true)
                        sa1 = sfd1.FileName;

                    dis.DiskFileName = sa1;
                    exp = repo4.ExportOptions;

                    exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    exp.ExportFormatType = ExportFormatType.ExcelRecord;

                    exp.ExportFormatOptions = exexl;
                    exp.ExportDestinationOptions = dis;
                    repo4.Export();
                    MessageBox.Show("Successfull Export to Excel");
                }
                else
                    MessageBox.Show("Thank you");
            }
            else
            {
                ReportAll___Copy repo4 = new ReportAll___Copy();


                repo4.SetDatabaseLogon("IRS", "2020", "217.139.89.20/ora11g", "IRS");
                repo4.SetDatabaseLogon("APP", "12369", "171.0.1.96 /ora11g", "APP");

                repo4.SetParameterValue("comp1", comp1);
                repo4.SetParameterValue("comp2", comp2);

                repo4.SetParameterValue("crda1", cdat1);
                repo4.SetParameterValue("crda2", cdat2);
                repo4.SetParameterValue("seda1", sdat1);
                repo4.SetParameterValue("seda2", sdat2);

                repo4.SetParameterValue("prv1", prv1);
                repo4.SetParameterValue("prv2", prv2);
                repo4.SetParameterValue("cls1", cls1);
                repo4.SetParameterValue("cls2", cls2);

                repo4.SetParameterValue("SRT", 0);


                showreport.crystalReportViewer1.ReportSource = repo4;
                showreport.ShowDialog();

                if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExportOptions exp = new ExportOptions();
                    DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                    ExcelFormatOptions exexl = new ExcelFormatOptions();
                    string sa1 = "";

                    SaveFileDialog sfd1 = new SaveFileDialog();
                    sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    sfd1.FileName = "Report";
                    if (sfd1.ShowDialog() == true)
                        sa1 = sfd1.FileName;

                    dis.DiskFileName = sa1;
                    exp = repo4.ExportOptions;

                    exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    exp.ExportFormatType = ExportFormatType.ExcelRecord;

                    exp.ExportFormatOptions = exexl;
                    exp.ExportDestinationOptions = dis;
                    repo4.Export();
                    MessageBox.Show("Successfull Export to Excel");
                }
                else
                    MessageBox.Show("Thank you");
            }
        }

        private void BtnRayLab_Click(object sender, RoutedEventArgs e)
        {

            Int32 comp1, comp2;
            Int32 prv1, prv2;
            DateTime sdat1, sdat2, cdat1, cdat2;
            string cls1, cls2;

            comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
            comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
            prv1 = cbxStartProvider.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartProvider.Text);
            prv2 = cbxEndProvider.Text == string.Empty ? 999999 : Convert.ToInt32(cbxEndProvider.Text);
            cls1 = cbxStartClass.Text == string.Empty ? " " : cbxStartClass.Text;
            cls2 = cbxEndClass.Text == string.Empty ? "zzzzzzz" : cbxEndClass.Text;

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            sdat1 = (DateTime)startdateclm.SelectedDate.Value.Date;
            sdat2 = (DateTime)enddateclm.SelectedDate.Value.Date;
            cdat1 = (DateTime)startdatecreat.SelectedDate.Value.Date;
            cdat2 = (DateTime)enddatecreat.SelectedDate.Value.Date;

            View_Report showreport = new View_Report();
            if (comp1 == 500114 || comp2 == 500114)
            {
                //ReportLabRay2 repo4 = new ReportLabRay2();
                ReportLabRayDetails2 repo4 = new ReportLabRayDetails2();

                repo4.SetDatabaseLogon("APP", "12369");
                repo4.SetParameterValue("comp1", comp1);
                repo4.SetParameterValue("comp2", comp2);

                repo4.SetParameterValue("crda1", cdat1);
                repo4.SetParameterValue("crda2", cdat2);
                repo4.SetParameterValue("seda1", sdat1);
                repo4.SetParameterValue("seda2", sdat2);

                repo4.SetParameterValue("prv1", prv1);
                repo4.SetParameterValue("prv2", prv2);
                repo4.SetParameterValue("cls1", cls1);
                repo4.SetParameterValue("cls2", cls2);

                repo4.SetParameterValue("SRT", 0);


                showreport.crystalReportViewer1.ReportSource = repo4;
                showreport.ShowDialog();

                if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExportOptions exp = new ExportOptions();
                    DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                    ExcelFormatOptions exexl = new ExcelFormatOptions();
                    string sa1 = "";

                    SaveFileDialog sfd1 = new SaveFileDialog();
                    sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    sfd1.FileName = "Report";
                    if (sfd1.ShowDialog() == true)
                        sa1 = sfd1.FileName;

                    dis.DiskFileName = sa1;
                    exp = repo4.ExportOptions;

                    exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    exp.ExportFormatType = ExportFormatType.ExcelRecord;

                    exp.ExportFormatOptions = exexl;
                    exp.ExportDestinationOptions = dis;
                    repo4.Export();
                    MessageBox.Show("Successfull Export to Excel");
                }
                else
                    MessageBox.Show("Thank you");
            }
            else
            {
                //ReportLabRay repo4 = new ReportLabRay();
                ReportLabRayDetails repo4 = new ReportLabRayDetails();
                repo4.SetDatabaseLogon("APP", "12369");
                repo4.SetParameterValue("comp1", comp1);
                repo4.SetParameterValue("comp2", comp2);

                repo4.SetParameterValue("crda1", cdat1);
                repo4.SetParameterValue("crda2", cdat2);
                repo4.SetParameterValue("seda1", sdat1);
                repo4.SetParameterValue("seda2", sdat2);

                repo4.SetParameterValue("prv1", prv1);
                repo4.SetParameterValue("prv2", prv2);
                repo4.SetParameterValue("cls1", cls1);
                repo4.SetParameterValue("cls2", cls2);

                repo4.SetParameterValue("SRT", 0);


                showreport.crystalReportViewer1.ReportSource = repo4;
                showreport.ShowDialog();

                if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExportOptions exp = new ExportOptions();
                    DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                    ExcelFormatOptions exexl = new ExcelFormatOptions();
                    string sa1 = "";

                    SaveFileDialog sfd1 = new SaveFileDialog();
                    sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    sfd1.FileName = "Report";
                    if (sfd1.ShowDialog() == true)
                        sa1 = sfd1.FileName;

                    dis.DiskFileName = sa1;
                    exp = repo4.ExportOptions;

                    exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    exp.ExportFormatType = ExportFormatType.ExcelRecord;

                    exp.ExportFormatOptions = exexl;
                    exp.ExportDestinationOptions = dis;
                    repo4.Export();
                    MessageBox.Show("Successfull Export to Excel");
                }
                else
                    MessageBox.Show("Thank you");
            }

        }

    }
}


