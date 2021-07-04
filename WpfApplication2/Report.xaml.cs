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
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {

        public Report(string comp)
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            startfun();

        }
        DB db = new DB();
        string compstartnum, compendnum;
        void startfun()
        {
            fillcompanyNumber();
            // fillcatagory();
            //fillCardNum();
            //   fillArea();


        }
        DataSet dt1, dt2, dt3;
        private void fillcompanyNumber()
        {
            dt1 = db.RunReaderds(" select distinct C_COMP_ID from V_COMPANIES ORDER BY C_COMP_ID ");
            cbxStartCompNum.ItemsSource = dt1.Tables[0].DefaultView;
            cbxEndCompNum.ItemsSource = dt1.Tables[0].DefaultView;

        }
        private void fillArea()
        {
            DataSet dt = db.RunReaderds(" select distinct AREA_CODE from IRS_AREAS_CODES ORDER BY AREA_CODE ");
            cbxRegain.ItemsSource = dt.Tables[0].DefaultView;


        }

        private void fillcatagorystart()
        {
            DataSet dt = db.RunReaderds(" select distinct CLASS_CODE from IRS_EMPLOYEES WHERE COMP_ID=" + compstartnum + " ORDER BY CLASS_CODE ");
            //  cbxEndCatog.ItemsSource = dt.Tables[0].DefaultView;
            cbxStartCatog.ItemsSource = dt.Tables[0].DefaultView;

        }
        private void fillcatagoryend()
        {
            DataSet dt = db.RunReaderds(" select distinct CLASS_CODE from IRS_EMPLOYEES WHERE COMP_ID=" + compendnum + " ORDER BY CLASS_CODE ");
            cbxEndCatog.ItemsSource = dt.Tables[0].DefaultView;
            //  cbxStartCatog.ItemsSource = dt.Tables[0].DefaultView;

        }
        private void fillcatagory()
        {
            DataSet dt = db.RunReaderds(" select distinct CLASS_CODE from IRS_EMPLOYEES  ORDER BY CLASS_CODE ");
            cbxEndCatog.ItemsSource = dt.Tables[0].DefaultView;
            cbxStartCatog.ItemsSource = dt.Tables[0].DefaultView;

        }

        private void fillCardNumstart()
        {
            DataSet dt = db.RunReaderds(" select  CARD_NO from IRS_EMPLOYEES WHERE COMP_ID=" + compstartnum + " ORDER BY CARD_NO ");
            cbxStartCardNum.ItemsSource = dt.Tables[0].DefaultView;
        }
        private void fillCardNumend()
        {
            DataSet dt = db.RunReaderds(" select  CARD_NO from IRS_EMPLOYEES WHERE COMP_ID=" + compendnum + " ORDER BY CARD_NO ");
            cbxEndCardNum.ItemsSource = dt.Tables[0].DefaultView;

        }
        private void fillCardNum()
        {
            DataSet dt = db.RunReaderds(" select  CARD_NO from IRS_EMPLOYEES  ORDER BY CARD_NO ");
            cbxEndCardNum.ItemsSource = dt.Tables[0].DefaultView;
            cbxStartCardNum.ItemsSource = dt.Tables[0].DefaultView;

        }


        private void btnExite_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void cbxEndCompNum_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                compendnum = dt1.Tables[0].Rows[cbxEndCompNum.SelectedIndex][0].ToString();
                fillCardNumend();
                fillcatagoryend();


            }
            catch
            { }
        }

        private void cbxEndCompNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                compendnum = dt1.Tables[0].Rows[cbxEndCompNum.SelectedIndex][0].ToString();
                fillCardNumend();
                fillcatagoryend();


            }
            catch
            { }
        }

        private void cbxStartCardNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxStartCompNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                compstartnum = dt1.Tables[0].Rows[cbxStartCompNum.SelectedIndex][0].ToString();
                fillCardNumstart();
                fillcatagorystart();


            }
            catch
            { }
        }

        private void cbxReportTyp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxReportTypbad.Text = "";
        }

        private void cbxReportTypbad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbxReportTyp.Text = "";
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Int32 comp1, comp2, are1, are2, per, lrg, sml, prv1, prv2;
            string card1, card2, cls1, cls2;
            DateTime dreg1, dreg2, srda1, srda2;



            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            dreg1 = (DateTime)dpStartRegest.SelectedDate;
            dreg2 = (DateTime)dpEndRegast.SelectedDate;
            srda1 = (DateTime)dpStartServes.SelectedDate;
            srda2 = (DateTime)dpEndServes.SelectedDate;

            per = tmpercent.Text == string.Empty ? 0 : Convert.ToInt32(tmpercent.Text);
            lrg = txtakbermn.Text == string.Empty ? 0 : Convert.ToInt32(txtakbermn.Text);
            sml = txtas8urmn.Text == string.Empty ? 0 : Convert.ToInt32(txtas8urmn.Text);

            prv1 = cbxStartProvider.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartProvider.Text);
            prv2 = cbxEndProvider.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndProvider.Text);
            int tst = 0;

            if (AmountOnly.IsChecked == true)
                tst = 0;
            else if (GrossOnly.IsChecked == true)
                tst = 1;

            View_Report showreport = new View_Report();
            try
            {
                if (cbxReportTyp.SelectedIndex == 10)
                {
                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);

                    ReportConsMed repo15 = new ReportConsMed();
                    repo15.SetDatabaseLogon("APP", "12369");

                    repo15.SetParameterValue("comp1", comp1);
                    repo15.SetParameterValue("comp2", comp2);

                    //repo14.SetParameterValue("larg", lrg);
                    //repo14.SetParameterValue("small", sml);

                    showreport.crystalReportViewer1.ReportSource = repo15;
                    showreport.ShowDialog();
                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        ExportOptions exp = new ExportOptions();
                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                        PdfFormatOptions expdf = new PdfFormatOptions();
                        string sa = "";

                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Pdf file|*.pdf";
                        sfd.FileName = "Medication Per Consumption and Expected";
                        if (sfd.ShowDialog() == true)
                            sa = sfd.FileName;

                        dis.DiskFileName = sa;
                        exp = repo15.ExportOptions;
                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                        exp.ExportFormatOptions = expdf;
                        exp.ExportDestinationOptions = dis;
                        repo15.Export();

                        MessageBox.Show("Successfull Export to Pdf");

                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                            string sa1 = "";

                            SaveFileDialog sfd1 = new SaveFileDialog();
                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                            sfd1.FileName = "Medication Per Consumption and Expected";
                            if (sfd1.ShowDialog() == true)
                                sa1 = sfd1.FileName;

                            dis.DiskFileName = sa1;
                            exp = repo15.ExportOptions;

                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                            exp.ExportFormatOptions = exexl;
                            exp.ExportDestinationOptions = dis;
                            repo15.Export();
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
                        sfd1.FileName = "Medication Per Consumption and Expected";
                        if (sfd1.ShowDialog() == true)
                            sa1 = sfd1.FileName;

                        dis.DiskFileName = sa1;
                        exp = repo15.ExportOptions;

                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                        exp.ExportFormatOptions = exexl;
                        exp.ExportDestinationOptions = dis;
                        repo15.Export();
                        MessageBox.Show("Successfull Export to Excel");
                    }
                    else
                        MessageBox.Show("Thank you");
                }
                else
                {
                    if (AmountAndGross.IsChecked != true)
                    {
                        if (cbxReportTyp.Text != string.Empty)
                        {
                            switch (cbxReportTyp.SelectedIndex)
                            {
                                case 0:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;


                                    //Report1 repo = new Report1();
                                    Report1ON repo = new Report1ON();
                                    repo.SetDatabaseLogon("APP", "12369");
                                    repo.SetParameterValue("crda1", dreg1);
                                    repo.SetParameterValue("crda2", dreg2);
                                    repo.SetParameterValue("comp1", comp1);
                                    repo.SetParameterValue("comp2", comp2);
                                    repo.SetParameterValue("crd1", card1);
                                    repo.SetParameterValue("crd2", card2);
                                    repo.SetParameterValue("srda1", srda1);
                                    repo.SetParameterValue("srda2", srda2);
                                    repo.SetParameterValue("prv1", prv1);
                                    repo.SetParameterValue("prv2", prv2);
                                    repo.SetParameterValue("tst", tst);

                                    showreport.crystalReportViewer1.ReportSource = repo;
                                    showreport.ShowDialog();

                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Employee VS. Service";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Employee VS. Service";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo.Export();
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
                                        sfd1.FileName = "Consumption Per Employee VS. Service";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 1:

                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;

                                    // Report2 repo2 = new Report2();
                                    Report2ON repo2 = new Report2ON();
                                    repo2.SetDatabaseLogon("APP", "12369");
                                    repo2.SetParameterValue("crda1", dreg1);
                                    repo2.SetParameterValue("crda2", dreg2);
                                    repo2.SetParameterValue("comp1", comp1);
                                    repo2.SetParameterValue("comp2", comp2);
                                    repo2.SetParameterValue("crd1", card1);
                                    repo2.SetParameterValue("crd2", card2);
                                    repo2.SetParameterValue("srda1", srda1);
                                    repo2.SetParameterValue("srda2", srda2);
                                    repo2.SetParameterValue("prv1", prv1);
                                    repo2.SetParameterValue("prv2", prv2);
                                    repo2.SetParameterValue("tst", tst);
                                    repo2.SetParameterValue("per", per);
                                    repo2.SetParameterValue("larg", lrg);
                                    repo2.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo2;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Super Group Service";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo2.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo2.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Super Group Service";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo2.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo2.Export();
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
                                        sfd1.FileName = "Consumption Per Super Group Service";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo2.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo2.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;

                                case 2:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                                    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                                    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;

                                    // Report4 repo4 = new Report4();
                                    // Report4___Copy repo4 = new Report4___Copy();
                                    Report4ON repo4 = new Report4ON();
                                    repo4.SetDatabaseLogon("APP", "12369");
                                    repo4.SetParameterValue("crda1", dreg1);
                                    repo4.SetParameterValue("crda2", dreg2);
                                    repo4.SetParameterValue("comp1", comp1);
                                    repo4.SetParameterValue("comp2", comp2);
                                    repo4.SetParameterValue("crd1", card1);
                                    repo4.SetParameterValue("crd2", card2);
                                    repo4.SetParameterValue("srda1", srda1);
                                    repo4.SetParameterValue("srda2", srda2);
                                    repo4.SetParameterValue("cls1", cls1);
                                    repo4.SetParameterValue("cls2", cls2);
                                    repo4.SetParameterValue("prv1", prv1);
                                    repo4.SetParameterValue("prv2", prv2);
                                    repo4.SetParameterValue("tst", tst);

                                    repo4.SetParameterValue("per", per);
                                    repo4.SetParameterValue("larg", lrg);
                                    repo4.SetParameterValue("small", sml);

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
                                        sfd.FileName = "Medicine Consumption";
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
                                            sfd1.FileName = "Medicine Consumption";
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
                                        sfd1.FileName = "Medicine Consumption";
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

                                    break;
                                case 3:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                                    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                                    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;

                                    // Report6 repo6 = new Report6();
                                    Report6ON repo6 = new Report6ON();
                                    repo6.SetDatabaseLogon("APP", "12369");
                                    repo6.SetParameterValue("crda1", dreg1);
                                    repo6.SetParameterValue("crda2", dreg2);
                                    repo6.SetParameterValue("comp1", comp1);
                                    repo6.SetParameterValue("comp2", comp2);
                                    repo6.SetParameterValue("crd1", card1);
                                    repo6.SetParameterValue("crd2", card2);
                                    repo6.SetParameterValue("srda1", srda1);
                                    repo6.SetParameterValue("srda2", srda2);
                                    repo6.SetParameterValue("cls1", cls1);
                                    repo6.SetParameterValue("cls2", cls2);
                                    repo6.SetParameterValue("prv1", prv1);
                                    repo6.SetParameterValue("prv2", prv2);
                                    repo6.SetParameterValue("tst", tst);

                                    repo6.SetParameterValue("per", per);
                                    repo6.SetParameterValue("larg", lrg);
                                    repo6.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo6;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Gender";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo6.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo6.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Gender";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo6.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo6.Export();
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
                                        sfd1.FileName = "Consumption Per Gender";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo6.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo6.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 4:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                                    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                                    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;

                                    //  Report7 repo7 = new Report7();
                                    // Report7___Copy_2 repo7 = new Report7___Copy_2();
                                    Report77 repo7 = new Report77();
                                    //    Report7ON repo7 = new Report7ON();
                                    repo7.SetDatabaseLogon("APP", "12369");
                                    repo7.SetParameterValue("crda1", dreg1);
                                    repo7.SetParameterValue("crda2", dreg2);
                                    repo7.SetParameterValue("comp1", comp1);
                                    repo7.SetParameterValue("comp2", comp2);
                                    repo7.SetParameterValue("crd1", card1);
                                    repo7.SetParameterValue("crd2", card2);
                                    repo7.SetParameterValue("srda1", srda1);
                                    repo7.SetParameterValue("srda2", srda2);
                                    repo7.SetParameterValue("cls1", cls1);
                                    repo7.SetParameterValue("cls2", cls2);

                                    repo7.SetParameterValue("prv1", prv1);
                                    repo7.SetParameterValue("prv2", prv2);
                                    repo7.SetParameterValue("tst", tst);
                                    repo7.SetParameterValue("per", per);
                                    repo7.SetParameterValue("larg", lrg);
                                    repo7.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo7;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Employee";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo7.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo7.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Employee";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo7.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo7.Export();
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
                                        sfd1.FileName = "Consumption Per Employee";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo7.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo7.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;


                                //case 8:
                                //    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                //    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                //    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                //    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;

                                //    Report9 repo9 = new Report9();


                                //    repo9.SetDatabaseLogon("APP", "12369");
                                //    repo9.SetParameterValue("crda1", dreg1);
                                //    repo9.SetParameterValue("crda2", dreg2);
                                //    repo9.SetParameterValue("comp1", comp1);
                                //    repo9.SetParameterValue("comp2", comp2);
                                //    repo9.SetParameterValue("srda1", srda1);
                                //    repo9.SetParameterValue("srda2", srda2);


                                //    repo9.SetParameterValue("per", per);
                                //    repo9.SetParameterValue("larg", lrg);
                                //    repo9.SetParameterValue("small", sml);

                                //    showreport.crystalReportViewer1.ReportSource = repo9;
                                //    showreport.ShowDialog();

                                //    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                //    {
                                //        ExportOptions exp = new ExportOptions();
                                //        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                //        PdfFormatOptions expdf = new PdfFormatOptions();
                                //        string sa = "";

                                //        SaveFileDialog sfd = new SaveFileDialog();
                                //        sfd.Filter = "Pdf file|*.pdf";
                                //        sfd.FileName = "Consumption Per Employee";
                                //        if (sfd.ShowDialog() == true)
                                //            sa = sfd.FileName;

                                //        dis.DiskFileName = sa;
                                //        exp = repo9.ExportOptions;
                                //        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                //        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                //        exp.ExportFormatOptions = expdf;
                                //        exp.ExportDestinationOptions = dis;
                                //        repo9.Export();

                                //        MessageBox.Show("Successfull Export to Pdf");

                                //        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                //        {
                                //            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                //            string sa1 = "";

                                //            SaveFileDialog sfd1 = new SaveFileDialog();
                                //            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                //            sfd1.FileName = "Consumption Per Employee";
                                //            if (sfd1.ShowDialog() == true)
                                //                sa1 = sfd1.FileName;

                                //            dis.DiskFileName = sa1;
                                //            exp = repo9.ExportOptions;

                                //            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                //            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                //            exp.ExportFormatOptions = exexl;
                                //            exp.ExportDestinationOptions = dis;
                                //            repo9.Export();
                                //            MessageBox.Show("Successfull Export to Excel");

                                //        }
                                //        else
                                //            MessageBox.Show("Thank you");
                                //    }
                                //    else if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                //    {
                                //        ExportOptions exp = new ExportOptions();
                                //        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                //        ExcelFormatOptions exexl = new ExcelFormatOptions();
                                //        string sa1 = "";

                                //        SaveFileDialog sfd1 = new SaveFileDialog();
                                //        sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                //        sfd1.FileName = "Consumption Per Employee";
                                //        if (sfd1.ShowDialog() == true)
                                //            sa1 = sfd1.FileName;

                                //        dis.DiskFileName = sa1;
                                //        exp = repo9.ExportOptions;

                                //        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                //        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                //        exp.ExportFormatOptions = exexl;
                                //        exp.ExportDestinationOptions = dis;
                                //        repo9.Export();
                                //        MessageBox.Show("Successfull Export to Excel");
                                //    }
                                //    else
                                //        MessageBox.Show("Thank you");


                                //    break;
                                case 5:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);

                                    //Report10 repo10 = new Report10();
                                    // Report10ON repo10 = new Report10ON();
                                    Report10NEW repo10 = new Report10NEW();
                                    repo10.SetDatabaseLogon("APP", "12369");
                                    repo10.SetParameterValue("crda1", dreg1);
                                    repo10.SetParameterValue("crda2", dreg2);
                                    repo10.SetParameterValue("comp1", comp1);
                                    repo10.SetParameterValue("comp2", comp2);
                                    repo10.SetParameterValue("tst", tst);
                                    repo10.SetParameterValue("srda1", srda1);
                                    repo10.SetParameterValue("srda2", srda2);




                                    showreport.crystalReportViewer1.ReportSource = repo10;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Summary";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo10.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo10.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Summary";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo10.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo10.Export();
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
                                        sfd1.FileName = "Consumption Summary";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo10.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo10.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 6:

                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;

                                    // Report11 repo11 = new Report11();

                                    Report11ON repo11 = new Report11ON();

                                    repo11.SetDatabaseLogon("APP", "12369");
                                    repo11.SetParameterValue("crda1", dreg1);
                                    repo11.SetParameterValue("crda2", dreg2);
                                    repo11.SetParameterValue("comp1", comp1);
                                    repo11.SetParameterValue("comp2", comp2);

                                    repo11.SetParameterValue("tst", tst);
                                    repo11.SetParameterValue("srda1", srda1);
                                    repo11.SetParameterValue("srda2", srda2);


                                    repo11.SetParameterValue("per", per);
                                    repo11.SetParameterValue("larg", lrg);
                                    repo11.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo11;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Relation";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo11.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo11.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Relation";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo11.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo11.Export();
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
                                        sfd1.FileName = "Consumption Per Relation";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo11.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo11.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 7:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;

                                    Report12 repo12 = new Report12();
                                    // Report12ON repo12 = new Report12ON();
                                    repo12.SetDatabaseLogon("APP", "12369");
                                    repo12.SetParameterValue("crda1", dreg1);
                                    repo12.SetParameterValue("crda2", dreg2);
                                    repo12.SetParameterValue("comp1", comp1);
                                    repo12.SetParameterValue("comp2", comp2);
                                    repo12.SetParameterValue("crd1", card1);
                                    repo12.SetParameterValue("crd2", card2);
                                    repo12.SetParameterValue("srda1", srda1);
                                    repo12.SetParameterValue("srda2", srda2);

                                    repo12.SetParameterValue("prv1", prv1);
                                    repo12.SetParameterValue("prv2", prv2);
                                    repo12.SetParameterValue("tst", tst);




                                    showreport.crystalReportViewer1.ReportSource = repo12;
                                    showreport.ShowDialog();

                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Details";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo12.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo12.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Details";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo12.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo12.Export();
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
                                        sfd1.FileName = "Consumption Details";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo12.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo12.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 8:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                                    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                                    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;

                                    //Report13 repo13 = new Report13();
                                    Report13ON repo13 = new Report13ON();
                                    repo13.SetDatabaseLogon("APP", "12369");
                                    repo13.SetParameterValue("crda1", dreg1);
                                    repo13.SetParameterValue("crda2", dreg2);
                                    repo13.SetParameterValue("comp1", comp1);
                                    repo13.SetParameterValue("comp2", comp2);
                                    repo13.SetParameterValue("crd1", card1);
                                    repo13.SetParameterValue("crd2", card2);
                                    repo13.SetParameterValue("srda1", srda1);
                                    repo13.SetParameterValue("srda2", srda2);
                                    repo13.SetParameterValue("cls1", cls1);
                                    repo13.SetParameterValue("cls2", cls2);
                                    repo13.SetParameterValue("prv1", prv1);
                                    repo13.SetParameterValue("prv2", prv2);
                                    repo13.SetParameterValue("tst", tst);

                                    repo13.SetParameterValue("per", per);
                                    repo13.SetParameterValue("larg", lrg);
                                    repo13.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo13;
                                    showreport.ShowDialog();

                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Provider";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo13.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo13.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Provider";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo13.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo13.Export();
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
                                        sfd1.FileName = "Consumption Per Provider";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo13.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo13.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 9:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;


                                    //Report14 repo14 = new Report14();
                                    Report14ON repo14 = new Report14ON();
                                    repo14.SetDatabaseLogon("APP", "12369");
                                    repo14.SetParameterValue("crda1", dreg1);
                                    repo14.SetParameterValue("crda2", dreg2);
                                    repo14.SetParameterValue("comp1", comp1);
                                    repo14.SetParameterValue("comp2", comp2);
                                    repo14.SetParameterValue("crd1", card1);
                                    repo14.SetParameterValue("crd2", card2);
                                    repo14.SetParameterValue("srda1", srda1);
                                    repo14.SetParameterValue("srda2", srda2);
                                    repo14.SetParameterValue("prv1", prv1);
                                    repo14.SetParameterValue("prv2", prv2);
                                    repo14.SetParameterValue("tst", tst);

                                    repo14.SetParameterValue("per", per);
                                    repo14.SetParameterValue("larg", lrg);
                                    repo14.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo14;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Service Group";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo14.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo14.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Service Group";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo14.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo14.Export();
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
                                        sfd1.FileName = "Consumption Per Service Group";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo14.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo14.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                default:

                                    break;
                            }
                        }
                        else
                        {
                            switch (cbxReportTypbad.SelectedIndex)
                            {
                                case 0:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                                    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                                    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;

                                    Report3 repo3 = new Report3();
                                    repo3.SetDatabaseLogon("APP", "12369");
                                    repo3.SetParameterValue("crda1", dreg1);
                                    repo3.SetParameterValue("crda2", dreg2);
                                    repo3.SetParameterValue("comp1", comp1);
                                    repo3.SetParameterValue("comp2", comp2);
                                    repo3.SetParameterValue("crd1", card1);
                                    repo3.SetParameterValue("crd2", card2);
                                    repo3.SetParameterValue("srda1", srda1);
                                    repo3.SetParameterValue("srda2", srda2);
                                    repo3.SetParameterValue("cls1", cls1);
                                    repo3.SetParameterValue("cls2", cls2);


                                    repo3.SetParameterValue("per", per);
                                    repo3.SetParameterValue("larg", lrg);
                                    repo3.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo3;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Diagnosis";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo3.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo3.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Diagnosis";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo3.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo3.Export();
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
                                        sfd1.FileName = "Consumption Per Diagnosis";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo3.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo3.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;
                                case 1:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                                    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                                    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;
                                    are1 = cbxRegain.Text == string.Empty ? 0 : Convert.ToInt32(cbxRegain.Text);
                                    are2 = cbxRegain.Text == string.Empty ? 99999 : Convert.ToInt32(cbxRegain.Text);

                                    Report5 repo5 = new Report5();
                                    repo5.SetDatabaseLogon("APP", "12369");
                                    repo5.SetParameterValue("crda1", dreg1);
                                    repo5.SetParameterValue("crda2", dreg2);
                                    repo5.SetParameterValue("comp1", comp1);
                                    repo5.SetParameterValue("comp2", comp2);
                                    repo5.SetParameterValue("crd1", card1);
                                    repo5.SetParameterValue("crd2", card2);
                                    repo5.SetParameterValue("srda1", srda1);
                                    repo5.SetParameterValue("srda2", srda2);
                                    repo5.SetParameterValue("cls1", cls1);
                                    repo5.SetParameterValue("cls2", cls2);
                                    repo5.SetParameterValue("are1", are1);
                                    repo5.SetParameterValue("are2", are2);


                                    repo5.SetParameterValue("per", per);
                                    repo5.SetParameterValue("larg", lrg);
                                    repo5.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo5;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Area";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo5.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo5.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Area";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo5.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo5.Export();
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
                                        sfd1.FileName = "Consumption Per Area";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo5.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo5.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");


                                    break;
                                case 2:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;

                                    Report8 repo8 = new Report8();

                                    repo8.SetDatabaseLogon("APP", "12369");
                                    repo8.SetParameterValue("crda1", dreg1);
                                    repo8.SetParameterValue("crda2", dreg2);
                                    repo8.SetParameterValue("comp1", comp1);
                                    repo8.SetParameterValue("comp2", comp2);
                                    repo8.SetParameterValue("crd1", card1);
                                    repo8.SetParameterValue("crd2", card2);
                                    repo8.SetParameterValue("srda1", srda1);
                                    repo8.SetParameterValue("srda2", srda2);



                                    showreport.crystalReportViewer1.ReportSource = repo8;
                                    showreport.ShowDialog();

                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Employee V.S. Service & Ceiling";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo8.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo8.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Employee V.S. Service & Ceiling";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo8.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo8.Export();
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
                                        sfd1.FileName = "Consumption Per Employee V.S. Service & Ceiling";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo8.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo8.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                default:
                                    break;

                            }
                        }
                    }
                    else if (AmountAndGross.IsChecked == true)
                    {
                        if (cbxReportTyp.Text != string.Empty)
                        {
                            switch (cbxReportTyp.SelectedIndex)
                            {
                                case 0:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;


                                    //Report1 repo = new Report1();
                                    Report1ONGA repo = new Report1ONGA();
                                    repo.SetDatabaseLogon("APP", "12369");
                                    repo.SetParameterValue("crda1", dreg1);
                                    repo.SetParameterValue("crda2", dreg2);
                                    repo.SetParameterValue("comp1", comp1);
                                    repo.SetParameterValue("comp2", comp2);
                                    repo.SetParameterValue("crd1", card1);
                                    repo.SetParameterValue("crd2", card2);
                                    repo.SetParameterValue("srda1", srda1);
                                    repo.SetParameterValue("srda2", srda2);
                                    repo.SetParameterValue("prv1", prv1);
                                    repo.SetParameterValue("prv2", prv2);

                                    showreport.crystalReportViewer1.ReportSource = repo;
                                    showreport.ShowDialog();

                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Employee VS. Service";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Employee VS. Service";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo.Export();
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
                                        sfd1.FileName = "Consumption Per Employee VS. Service";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 1:

                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;

                                    // Report2 repo2 = new Report2();
                                    Report2ONGA repo2 = new Report2ONGA();
                                    repo2.SetDatabaseLogon("APP", "12369");
                                    repo2.SetParameterValue("crda1", dreg1);
                                    repo2.SetParameterValue("crda2", dreg2);
                                    repo2.SetParameterValue("comp1", comp1);
                                    repo2.SetParameterValue("comp2", comp2);
                                    repo2.SetParameterValue("crd1", card1);
                                    repo2.SetParameterValue("crd2", card2);
                                    repo2.SetParameterValue("srda1", srda1);
                                    repo2.SetParameterValue("srda2", srda2);
                                    repo2.SetParameterValue("prv1", prv1);
                                    repo2.SetParameterValue("prv2", prv2);
                                    repo2.SetParameterValue("per", per);
                                    repo2.SetParameterValue("larg", lrg);
                                    repo2.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo2;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Super Group Service";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo2.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo2.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Super Group Service";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo2.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo2.Export();
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
                                        sfd1.FileName = "Consumption Per Super Group Service";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo2.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo2.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;

                                case 2:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                                    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                                    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;

                                    // Report4 repo4 = new Report4();
                                    // Report4___Copy repo4 = new Report4___Copy();
                                    Report4ONGA repo4 = new Report4ONGA();
                                    repo4.SetDatabaseLogon("APP", "12369");
                                    repo4.SetParameterValue("crda1", dreg1);
                                    repo4.SetParameterValue("crda2", dreg2);
                                    repo4.SetParameterValue("comp1", comp1);
                                    repo4.SetParameterValue("comp2", comp2);
                                    repo4.SetParameterValue("crd1", card1);
                                    repo4.SetParameterValue("crd2", card2);
                                    repo4.SetParameterValue("srda1", srda1);
                                    repo4.SetParameterValue("srda2", srda2);
                                    repo4.SetParameterValue("cls1", cls1);
                                    repo4.SetParameterValue("cls2", cls2);
                                    repo4.SetParameterValue("prv1", prv1);
                                    repo4.SetParameterValue("prv2", prv2);
                                    repo4.SetParameterValue("tst", tst);

                                    repo4.SetParameterValue("per", per);
                                    repo4.SetParameterValue("larg", lrg);
                                    repo4.SetParameterValue("small", sml);

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
                                        sfd.FileName = "Medicine Consumption";
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
                                            sfd1.FileName = "Medicine Consumption";
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
                                        sfd1.FileName = "Medicine Consumption";
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

                                    break;
                                case 3:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                                    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                                    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;

                                    // Report6 repo6 = new Report6();
                                    Report6ONGA repo6 = new Report6ONGA();
                                    repo6.SetDatabaseLogon("APP", "12369");
                                    repo6.SetParameterValue("crda1", dreg1);
                                    repo6.SetParameterValue("crda2", dreg2);
                                    repo6.SetParameterValue("comp1", comp1);
                                    repo6.SetParameterValue("comp2", comp2);
                                    repo6.SetParameterValue("crd1", card1);
                                    repo6.SetParameterValue("crd2", card2);
                                    repo6.SetParameterValue("srda1", srda1);
                                    repo6.SetParameterValue("srda2", srda2);
                                    repo6.SetParameterValue("cls1", cls1);
                                    repo6.SetParameterValue("cls2", cls2);
                                    repo6.SetParameterValue("prv1", prv1);
                                    repo6.SetParameterValue("prv2", prv2);
                                    repo6.SetParameterValue("tst", tst);

                                    repo6.SetParameterValue("per", per);
                                    repo6.SetParameterValue("larg", lrg);
                                    repo6.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo6;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Gender";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo6.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo6.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Gender";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo6.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo6.Export();
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
                                        sfd1.FileName = "Consumption Per Gender";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo6.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo6.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 4:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                                    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                                    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;

                                    //  Report7 repo7 = new Report7();
                                    Report7ONGA repo7 = new Report7ONGA();

                                    //    Report7ON repo7 = new Report7ON();
                                    repo7.SetDatabaseLogon("APP", "12369");
                                    repo7.SetParameterValue("crda1", dreg1);
                                    repo7.SetParameterValue("crda2", dreg2);
                                    repo7.SetParameterValue("comp1", comp1);
                                    repo7.SetParameterValue("comp2", comp2);
                                    repo7.SetParameterValue("crd1", card1);
                                    repo7.SetParameterValue("crd2", card2);
                                    repo7.SetParameterValue("srda1", srda1);
                                    repo7.SetParameterValue("srda2", srda2);
                                    repo7.SetParameterValue("cls1", cls1);
                                    repo7.SetParameterValue("cls2", cls2);

                                    repo7.SetParameterValue("prv1", prv1);
                                    repo7.SetParameterValue("prv2", prv2);
                                    repo7.SetParameterValue("per", per);
                                    repo7.SetParameterValue("larg", lrg);
                                    repo7.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo7;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Employee";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo7.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo7.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Employee";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo7.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo7.Export();
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
                                        sfd1.FileName = "Consumption Per Employee";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo7.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo7.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;


                                //case 8:
                                //    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                //    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                //    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                //    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;

                                //    Report9 repo9 = new Report9();


                                //    repo9.SetDatabaseLogon("APP", "12369");
                                //    repo9.SetParameterValue("crda1", dreg1);
                                //    repo9.SetParameterValue("crda2", dreg2);
                                //    repo9.SetParameterValue("comp1", comp1);
                                //    repo9.SetParameterValue("comp2", comp2);
                                //    repo9.SetParameterValue("srda1", srda1);
                                //    repo9.SetParameterValue("srda2", srda2);


                                //    repo9.SetParameterValue("per", per);
                                //    repo9.SetParameterValue("larg", lrg);
                                //    repo9.SetParameterValue("small", sml);

                                //    showreport.crystalReportViewer1.ReportSource = repo9;
                                //    showreport.ShowDialog();

                                //    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                //    {
                                //        ExportOptions exp = new ExportOptions();
                                //        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                //        PdfFormatOptions expdf = new PdfFormatOptions();
                                //        string sa = "";

                                //        SaveFileDialog sfd = new SaveFileDialog();
                                //        sfd.Filter = "Pdf file|*.pdf";
                                //        sfd.FileName = "Consumption Per Employee";
                                //        if (sfd.ShowDialog() == true)
                                //            sa = sfd.FileName;

                                //        dis.DiskFileName = sa;
                                //        exp = repo9.ExportOptions;
                                //        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                //        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                //        exp.ExportFormatOptions = expdf;
                                //        exp.ExportDestinationOptions = dis;
                                //        repo9.Export();

                                //        MessageBox.Show("Successfull Export to Pdf");

                                //        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                //        {
                                //            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                //            string sa1 = "";

                                //            SaveFileDialog sfd1 = new SaveFileDialog();
                                //            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                //            sfd1.FileName = "Consumption Per Employee";
                                //            if (sfd1.ShowDialog() == true)
                                //                sa1 = sfd1.FileName;

                                //            dis.DiskFileName = sa1;
                                //            exp = repo9.ExportOptions;

                                //            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                //            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                //            exp.ExportFormatOptions = exexl;
                                //            exp.ExportDestinationOptions = dis;
                                //            repo9.Export();
                                //            MessageBox.Show("Successfull Export to Excel");

                                //        }
                                //        else
                                //            MessageBox.Show("Thank you");
                                //    }
                                //    else if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                //    {
                                //        ExportOptions exp = new ExportOptions();
                                //        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                //        ExcelFormatOptions exexl = new ExcelFormatOptions();
                                //        string sa1 = "";

                                //        SaveFileDialog sfd1 = new SaveFileDialog();
                                //        sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                //        sfd1.FileName = "Consumption Per Employee";
                                //        if (sfd1.ShowDialog() == true)
                                //            sa1 = sfd1.FileName;

                                //        dis.DiskFileName = sa1;
                                //        exp = repo9.ExportOptions;

                                //        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                //        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                //        exp.ExportFormatOptions = exexl;
                                //        exp.ExportDestinationOptions = dis;
                                //        repo9.Export();
                                //        MessageBox.Show("Successfull Export to Excel");
                                //    }
                                //    else
                                //        MessageBox.Show("Thank you");


                                //    break;
                                case 5:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);

                                    //Report10 repo10 = new Report10();
                                    // Report10ON repo10 = new Report10ON();
                                    Report10NEWGA repo10 = new Report10NEWGA();

                                    repo10.SetDatabaseLogon("APP", "12369");
                                    repo10.SetParameterValue("crda1", dreg1);
                                    repo10.SetParameterValue("crda2", dreg2);
                                    repo10.SetParameterValue("comp1", comp1);
                                    repo10.SetParameterValue("comp2", comp2);
                                    repo10.SetParameterValue("srda1", srda1);
                                    repo10.SetParameterValue("srda2", srda2);




                                    showreport.crystalReportViewer1.ReportSource = repo10;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Summary";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo10.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo10.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Summary";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo10.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo10.Export();
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
                                        sfd1.FileName = "Consumption Summary";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo10.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo10.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 6:

                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;

                                    // Report11 repo11 = new Report11();

                                    Report11ONGA repo11 = new Report11ONGA();

                                    repo11.SetDatabaseLogon("APP", "12369");
                                    repo11.SetParameterValue("crda1", dreg1);
                                    repo11.SetParameterValue("crda2", dreg2);
                                    repo11.SetParameterValue("comp1", comp1);
                                    repo11.SetParameterValue("comp2", comp2);

                                    repo11.SetParameterValue("srda1", srda1);
                                    repo11.SetParameterValue("srda2", srda2);


                                    repo11.SetParameterValue("per", per);
                                    repo11.SetParameterValue("larg", lrg);
                                    repo11.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo11;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Relation";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo11.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo11.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Relation";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo11.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo11.Export();
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
                                        sfd1.FileName = "Consumption Per Relation";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo11.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo11.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 7:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;

                                    Report12GA repo12 = new Report12GA();
                                    // Report12ON repo12 = new Report12ON();
                                    repo12.SetDatabaseLogon("APP", "12369");
                                    repo12.SetParameterValue("crda1", dreg1);
                                    repo12.SetParameterValue("crda2", dreg2);
                                    repo12.SetParameterValue("comp1", comp1);
                                    repo12.SetParameterValue("comp2", comp2);
                                    repo12.SetParameterValue("crd1", card1);
                                    repo12.SetParameterValue("crd2", card2);
                                    repo12.SetParameterValue("srda1", srda1);
                                    repo12.SetParameterValue("srda2", srda2);

                                    repo12.SetParameterValue("prv1", prv1);
                                    repo12.SetParameterValue("prv2", prv2);

                                    showreport.crystalReportViewer1.ReportSource = repo12;
                                    showreport.ShowDialog();

                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Details";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo12.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo12.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Details";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo12.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo12.Export();
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
                                        sfd1.FileName = "Consumption Details";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo12.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo12.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 8:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                                    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                                    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;

                                    //Report13 repo13 = new Report13();
                                    Report13ONGA repo13 = new Report13ONGA();
                                    repo13.SetDatabaseLogon("APP", "12369");
                                    repo13.SetParameterValue("crda1", dreg1);
                                    repo13.SetParameterValue("crda2", dreg2);
                                    repo13.SetParameterValue("comp1", comp1);
                                    repo13.SetParameterValue("comp2", comp2);
                                    repo13.SetParameterValue("crd1", card1);
                                    repo13.SetParameterValue("crd2", card2);
                                    repo13.SetParameterValue("srda1", srda1);
                                    repo13.SetParameterValue("srda2", srda2);
                                    repo13.SetParameterValue("cls1", cls1);
                                    repo13.SetParameterValue("cls2", cls2);
                                    repo13.SetParameterValue("prv1", prv1);
                                    repo13.SetParameterValue("prv2", prv2);

                                    repo13.SetParameterValue("per", per);
                                    repo13.SetParameterValue("larg", lrg);
                                    repo13.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo13;
                                    showreport.ShowDialog();

                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Provider";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo13.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo13.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Provider";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo13.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo13.Export();
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
                                        sfd1.FileName = "Consumption Per Provider";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo13.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo13.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 9:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;


                                    //Report14 repo14 = new Report14();
                                    Report14ONGA repo14 = new Report14ONGA();
                                    repo14.SetDatabaseLogon("APP", "12369");
                                    repo14.SetParameterValue("crda1", dreg1);
                                    repo14.SetParameterValue("crda2", dreg2);
                                    repo14.SetParameterValue("comp1", comp1);
                                    repo14.SetParameterValue("comp2", comp2);
                                    repo14.SetParameterValue("crd1", card1);
                                    repo14.SetParameterValue("crd2", card2);
                                    repo14.SetParameterValue("srda1", srda1);
                                    repo14.SetParameterValue("srda2", srda2);
                                    repo14.SetParameterValue("prv1", prv1);
                                    repo14.SetParameterValue("prv2", prv2);

                                    repo14.SetParameterValue("per", per);
                                    repo14.SetParameterValue("larg", lrg);
                                    repo14.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo14;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Service Group";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo14.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo14.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Service Group";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo14.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo14.Export();
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
                                        sfd1.FileName = "Consumption Per Service Group";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo14.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo14.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;

                                default:

                                    break;
                            }
                        }
                        else
                        {
                            switch (cbxReportTypbad.SelectedIndex)
                            {
                                case 0:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                                    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                                    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;

                                    Report3 repo3 = new Report3();
                                    repo3.SetDatabaseLogon("APP", "12369");
                                    repo3.SetParameterValue("crda1", dreg1);
                                    repo3.SetParameterValue("crda2", dreg2);
                                    repo3.SetParameterValue("comp1", comp1);
                                    repo3.SetParameterValue("comp2", comp2);
                                    repo3.SetParameterValue("crd1", card1);
                                    repo3.SetParameterValue("crd2", card2);
                                    repo3.SetParameterValue("srda1", srda1);
                                    repo3.SetParameterValue("srda2", srda2);
                                    repo3.SetParameterValue("cls1", cls1);
                                    repo3.SetParameterValue("cls2", cls2);


                                    repo3.SetParameterValue("per", per);
                                    repo3.SetParameterValue("larg", lrg);
                                    repo3.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo3;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Diagnosis";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo3.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo3.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Diagnosis";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo3.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo3.Export();
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
                                        sfd1.FileName = "Consumption Per Diagnosis";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo3.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo3.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;
                                case 1:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                                    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                                    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;
                                    are1 = cbxRegain.Text == string.Empty ? 0 : Convert.ToInt32(cbxRegain.Text);
                                    are2 = cbxRegain.Text == string.Empty ? 99999 : Convert.ToInt32(cbxRegain.Text);

                                    Report5 repo5 = new Report5();
                                    repo5.SetDatabaseLogon("APP", "12369");
                                    repo5.SetParameterValue("crda1", dreg1);
                                    repo5.SetParameterValue("crda2", dreg2);
                                    repo5.SetParameterValue("comp1", comp1);
                                    repo5.SetParameterValue("comp2", comp2);
                                    repo5.SetParameterValue("crd1", card1);
                                    repo5.SetParameterValue("crd2", card2);
                                    repo5.SetParameterValue("srda1", srda1);
                                    repo5.SetParameterValue("srda2", srda2);
                                    repo5.SetParameterValue("cls1", cls1);
                                    repo5.SetParameterValue("cls2", cls2);
                                    repo5.SetParameterValue("are1", are1);
                                    repo5.SetParameterValue("are2", are2);


                                    repo5.SetParameterValue("per", per);
                                    repo5.SetParameterValue("larg", lrg);
                                    repo5.SetParameterValue("small", sml);

                                    showreport.crystalReportViewer1.ReportSource = repo5;
                                    showreport.ShowDialog();
                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Area";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo5.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo5.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Area";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo5.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo5.Export();
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
                                        sfd1.FileName = "Consumption Per Area";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo5.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo5.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");


                                    break;
                                case 2:
                                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;

                                    Report8 repo8 = new Report8();

                                    repo8.SetDatabaseLogon("APP", "12369");
                                    repo8.SetParameterValue("crda1", dreg1);
                                    repo8.SetParameterValue("crda2", dreg2);
                                    repo8.SetParameterValue("comp1", comp1);
                                    repo8.SetParameterValue("comp2", comp2);
                                    repo8.SetParameterValue("crd1", card1);
                                    repo8.SetParameterValue("crd2", card2);
                                    repo8.SetParameterValue("srda1", srda1);
                                    repo8.SetParameterValue("srda2", srda2);



                                    showreport.crystalReportViewer1.ReportSource = repo8;
                                    showreport.ShowDialog();

                                    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                    {
                                        ExportOptions exp = new ExportOptions();
                                        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                                        PdfFormatOptions expdf = new PdfFormatOptions();
                                        string sa = "";

                                        SaveFileDialog sfd = new SaveFileDialog();
                                        sfd.Filter = "Pdf file|*.pdf";
                                        sfd.FileName = "Consumption Per Employee V.S. Service & Ceiling";
                                        if (sfd.ShowDialog() == true)
                                            sa = sfd.FileName;

                                        dis.DiskFileName = sa;
                                        exp = repo8.ExportOptions;
                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                                        exp.ExportFormatOptions = expdf;
                                        exp.ExportDestinationOptions = dis;
                                        repo8.Export();

                                        MessageBox.Show("Successfull Export to Pdf");

                                        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                                        {
                                            ExcelFormatOptions exexl = new ExcelFormatOptions();
                                            string sa1 = "";

                                            SaveFileDialog sfd1 = new SaveFileDialog();
                                            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                                            sfd1.FileName = "Consumption Per Employee V.S. Service & Ceiling";
                                            if (sfd1.ShowDialog() == true)
                                                sa1 = sfd1.FileName;

                                            dis.DiskFileName = sa1;
                                            exp = repo8.ExportOptions;

                                            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                            exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                            exp.ExportFormatOptions = exexl;
                                            exp.ExportDestinationOptions = dis;
                                            repo8.Export();
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
                                        sfd1.FileName = "Consumption Per Employee V.S. Service & Ceiling";
                                        if (sfd1.ShowDialog() == true)
                                            sa1 = sfd1.FileName;

                                        dis.DiskFileName = sa1;
                                        exp = repo8.ExportOptions;

                                        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                                        exp.ExportFormatType = ExportFormatType.ExcelRecord;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo8.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                default:
                                    break;

                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Sorry, حدثت مشكلة حاول المحاولة مرة أخرى");
            }

        }
        private void rbreportsas8urmn_Checked(object sender, RoutedEventArgs e)
        {
            txtakbermn.Visibility = Visibility.Hidden;
            txtakbermn.Clear();
            txtas8urmn.Visibility = Visibility.Visible;
        }


        private void rbreportsas8urmn_Unchecked(object sender, RoutedEventArgs e)
        {
            txtakbermn.Visibility = Visibility.Visible;
            txtas8urmn.Visibility = Visibility.Hidden;
            txtas8urmn.Clear();
        }

        private void cbxStartProvider_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cbxStartProvider.ItemsSource = db.RunReaderds("select DISTINCT PR_CODE, PR_ANAME, PR_ENAME from V_SERV_PROVIDERS  where (PR_CODE like '%" + cbxStartProvider.Text + "%' or upper(PR_ANAME) like '%" + cbxStartProvider.Text.ToUpper() + "%' or upper(PR_ENAME) like '%" + cbxStartProvider.Text.ToUpper() + "%') and TERMINATE_FLAG = 'N' AND ROWNUM <= 50 order by PR_CODE").Tables[0].DefaultView;
                cbxStartProvider.IsDropDownOpen = true;
            }
        }

        private void cbxEndProvider_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cbxEndProvider.ItemsSource = db.RunReaderds("select DISTINCT PR_CODE, PR_ANAME, PR_ENAME from V_SERV_PROVIDERS  where (PR_CODE like '%" + cbxEndProvider.Text + "%' or upper(PR_ANAME) like '%" + cbxEndProvider.Text.ToUpper() + "%' or upper(PR_ENAME) like '%" + cbxEndProvider.Text.ToUpper() + "%') and TERMINATE_FLAG = 'N' AND ROWNUM <= 50 order by PR_CODE").Tables[0].DefaultView;
                cbxEndProvider.IsDropDownOpen = true;
            }
        }

    }
}
