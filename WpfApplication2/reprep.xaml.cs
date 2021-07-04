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
    /// Interaction logic for reprep.xaml
    /// </summary>
    public partial class reprep : Window
    {
        public reprep()
        {
            InitializeComponent();
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

           // startfun();
        }

        DB db = new DB();
        string compstartnum, compendnum;
        void startfun()
        {
            fillcompanyNumber();
            viewdetails.Visibility = Visibility.Hidden;
            viewall.Visibility = Visibility.Hidden;
            btnviewdetails.Visibility = Visibility.Hidden;
            btnprintdetails.Visibility = Visibility.Hidden;
            btnback.Visibility = Visibility.Hidden;
            showcard.Visibility = Visibility.Hidden;
            showname.Visibility = Visibility.Hidden;

        }
        DataSet dt1;
        private void fillcompanyNumber()
        {
            dt1 = db.RunReaderds("select distinct C_COMP_ID from M_TOT_MED ORDER BY C_COMP_ID");
            cbxStartCompNum.ItemsSource = dt1.Tables[0].DefaultView;
            cbxEndCompNum.ItemsSource = dt1.Tables[0].DefaultView;

        }

        private void fillCardNumstart()
        {
            DataSet dt = db.RunReaderds(" select  CARD_ID from M_TOT_MED WHERE C_COMP_ID=" + compstartnum + " ORDER BY CARD_ID ");
            cbxStartCardNum.ItemsSource = dt.Tables[0].DefaultView;



        }
        private void fillCardNumend()
        {
            DataSet dt = db.RunReaderds(" select  CARD_ID from M_TOT_MED WHERE C_COMP_ID=" + compendnum + " ORDER BY CARD_ID ");
            cbxEndCardNum.ItemsSource = dt.Tables[0].DefaultView;

        }
        private void fillCardNum()
        {
            DataSet dt = db.RunReaderds(" select  CARD_ID from M_TOT_MED  ORDER BY CARD_ID ");
            cbxEndCardNum.ItemsSource = dt.Tables[0].DefaultView;
            cbxStartCardNum.ItemsSource = dt.Tables[0].DefaultView;

        }


        private void btnExite_Click(object sender, RoutedEventArgs e)
        {
            this.Close();      
            

        }



        private void cbxEndCompNum_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                compendnum = dt1.Tables[0].Rows[cbxEndCompNum.SelectedIndex][0].ToString();
                fillCardNumend();
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
            }
            catch
            { }
        }
        private void cbxStartCompNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                compstartnum = dt1.Tables[0].Rows[cbxStartCompNum.SelectedIndex][0].ToString();
                fillCardNumstart();
            }
            catch
            { }
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
            Int32 comp1, comp2, coun1, coun2, srt;
            double amo1, amo2;
            string card1, card2;
            DateTime dat1, dat2;

            comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
            comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
            card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
            card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
            amo1 = txtakbermnamount.Text == string.Empty ? 0 : Convert.ToDouble(txtakbermnamount.Text);
            amo2 = txtas8urmnamount.Text == string.Empty ? 99999999999999 : Convert.ToDouble(txtas8urmnamount.Text);
            coun1 = txtakbermncount.Text == string.Empty ? 0 : Convert.ToInt32(txtakbermncount.Text);
            coun2 = txtas8urmncount.Text == string.Empty ? 999999999 : Convert.ToInt32(txtas8urmncount.Text);
            dat1 = (DateTime)startdate.SelectedDate.Value.Date;
            dat2 = (DateTime)enddate.SelectedDate.Value.Date;

            srt = Convert.ToInt32(Sortby.SelectedIndex) + 1;

            View_Report showreport = new View_Report();
            if (cbxMainTyp.SelectedIndex == 0)
            {
                try
                {
                    switch (cbxReportTyp.SelectedIndex)
                    {
                        case 0:
                            switch (cbxScoundTyp.SelectedIndex)
                            {
                                case 0:

                                    ReportAA repo4 = new ReportAA();

                                    repo4.SetDatabaseLogon("APP", "12369");
                                    repo4.SetParameterValue("comp1", comp1);
                                    repo4.SetParameterValue("comp2", comp2);
                                    repo4.SetParameterValue("crd1", card1);
                                    repo4.SetParameterValue("crd2", card2);
                                    repo4.SetParameterValue("amo1", amo1);
                                    repo4.SetParameterValue("amo2", amo2);
                                    repo4.SetParameterValue("coun1", coun1);
                                    repo4.SetParameterValue("coun2", coun2);
                                    repo4.SetParameterValue("dat1", dat1);
                                    repo4.SetParameterValue("dat2", dat2);
                                    repo4.SetParameterValue("srt", srt);


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
                                        sfd.FileName = "Details-All";
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
                                            sfd1.FileName = "Details-All";
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
                                        sfd1.FileName = "Details-All";
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

                                case 1:

                                    ReportAD repo5 = new ReportAD();

                                    repo5.SetDatabaseLogon("APP", "12369");
                                    repo5.SetParameterValue("comp1", comp1);
                                    repo5.SetParameterValue("comp2", comp2);
                                    repo5.SetParameterValue("crd1", card1);
                                    repo5.SetParameterValue("crd2", card2);
                                    repo5.SetParameterValue("amo1", amo1);
                                    repo5.SetParameterValue("amo2", amo2);
                                    repo5.SetParameterValue("coun1", coun1);
                                    repo5.SetParameterValue("coun2", coun2);
                                    repo5.SetParameterValue("dat1", dat1);
                                    repo5.SetParameterValue("dat2", dat2);
                                    repo5.SetParameterValue("srt", srt);

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
                                        sfd.FileName = "Details-Daily";
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
                                            sfd1.FileName = "Details-Daily";
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
                                        sfd1.FileName = "Details-Daily";
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
                                    ReportAM repo6 = new ReportAM();

                                    repo6.SetDatabaseLogon("APP", "12369");
                                    repo6.SetParameterValue("comp1", comp1);
                                    repo6.SetParameterValue("comp2", comp2);
                                    repo6.SetParameterValue("crd1", card1);
                                    repo6.SetParameterValue("crd2", card2);
                                    repo6.SetParameterValue("amo1", amo1);
                                    repo6.SetParameterValue("amo2", amo2);
                                    repo6.SetParameterValue("coun1", coun1);
                                    repo6.SetParameterValue("coun2", coun2);
                                    repo6.SetParameterValue("dat1", dat1);
                                    repo6.SetParameterValue("dat2", dat2);
                                    repo6.SetParameterValue("srt", srt);

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
                                        sfd.FileName = "Details-Monthly";
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
                                            sfd1.FileName = "Details-Monthly";
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
                                        sfd1.FileName = "Details-Monthly";
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
                                case 3:
                                    ReportAC repo7 = new ReportAC();

                                    repo7.SetDatabaseLogon("APP", "12369");
                                    repo7.SetParameterValue("comp1", comp1);
                                    repo7.SetParameterValue("comp2", comp2);
                                    repo7.SetParameterValue("crd1", card1);
                                    repo7.SetParameterValue("crd2", card2);
                                    repo7.SetParameterValue("amo1", amo1);
                                    repo7.SetParameterValue("amo2", amo2);
                                    repo7.SetParameterValue("coun1", coun1);
                                    repo7.SetParameterValue("coun2", coun2);
                                    repo7.SetParameterValue("dat1", dat1);
                                    repo7.SetParameterValue("dat2", dat2);
                                    repo7.SetParameterValue("srt", srt);

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
                                        sfd.FileName = "Details-Chronic";
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
                                            sfd1.FileName = "Details-Chronic";
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
                                        sfd1.FileName = "Details-Chronic";
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
                                /*
                                DataSet dt = db.RunReaderds(@"SELECT M_TOT_MED.CARD_ID , M_TOT_MED.C_COMP_ID , M_TOT_MED.CONTRACT_NO , M_TOT_MED.COUNTT , M_TOT_MED.VALUE_CREDIT , 
                                                               COMP_EMPLOYEESS.EMP_ANAME_ST , COMP_EMPLOYEESS.EMP_ANAME_SC , COMP_EMPLOYEESS.EMP_ANAME_TH , COMP_EMPLOYEESS.EMP_ENAME_ST ,
                                                               COMP_EMPLOYEESS.EMP_ENAME_SC , COMP_EMPLOYEESS.EMP_ENAME_TH FROM M_TOT_MED , COMP_EMPLOYEESS WHERE     
                                                               M_TOT_MED.CARD_ID = COMP_EMPLOYEESS.CARD_ID 
                                                               AND M_TOT_MED.C_COMP_ID = COMP_EMPLOYEESS.C_COMP_ID 
                                                               AND M_TOT_MED.CONTRACT_NO = COMP_EMPLOYEESS.CONTRACT_NO
                                                               AND M_TOT_MED.C_COMP_ID BETWEEN " + comp1 + " AND " + comp2 + " AND M_TOT_MED.CARD_ID BETWEEN '" + card1 + "' AND '" + card2 + "' AND M_TOT_MED.COUNTT BETWEEN " + coun1 + " AND " + coun2 + " AND M_TOT_MED.VALUE_CREDIT BETWEEN  " + amo1 + " AND " + amo2 + " ORDER BY M_TOT_MED.CARD_ID");
                                viewall.ItemsSource = dt.Tables[0].DefaultView;

                                viewall.Visibility = Visibility.Visible;
                                btnviewdetails.Visibility = Visibility.Visible;

                                break;
                                */

                                default:
                                    break;

                            }

                            break;

                        case 1:
                            switch (cbxScoundTyp.SelectedIndex)
                            {
                                case 0:
                                    //ReportSA repo4 = new ReportSA();
                                    ReportSA2 repo4 = new ReportSA2();

                                    repo4.SetDatabaseLogon("APP", "12369");
                                    repo4.SetParameterValue("comp1", comp1);
                                    repo4.SetParameterValue("comp2", comp2);
                                    repo4.SetParameterValue("crd1", card1);
                                    repo4.SetParameterValue("crd2", card2);
                                    repo4.SetParameterValue("amo1", amo1);
                                    repo4.SetParameterValue("amo2", amo2);
                                    repo4.SetParameterValue("coun1", coun1);
                                    repo4.SetParameterValue("coun2", coun2);
                                    repo4.SetParameterValue("srt", srt);


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
                                        sfd.FileName = "Summary-All";
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
                                            sfd1.FileName = "Summary-All";
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
                                        sfd1.FileName = "Summary-All";
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
                                case 1:

                                    ReportSD repo5 = new ReportSD();

                                    repo5.SetDatabaseLogon("APP", "12369");
                                    repo5.SetParameterValue("comp1", comp1);
                                    repo5.SetParameterValue("comp2", comp2);
                                    repo5.SetParameterValue("crd1", card1);
                                    repo5.SetParameterValue("crd2", card2);
                                    repo5.SetParameterValue("amo1", amo1);
                                    repo5.SetParameterValue("amo2", amo2);
                                    repo5.SetParameterValue("coun1", coun1);
                                    repo5.SetParameterValue("coun2", coun2);
                                    repo5.SetParameterValue("srt", srt);

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
                                        sfd.FileName = "Summary-Daily";
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
                                            sfd1.FileName = "Summary-Daily";
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
                                        sfd1.FileName = "Summary-Daily";
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
                                    ReportSM repo6 = new ReportSM();

                                    repo6.SetDatabaseLogon("APP", "12369");
                                    repo6.SetParameterValue("comp1", comp1);
                                    repo6.SetParameterValue("comp2", comp2);
                                    repo6.SetParameterValue("crd1", card1);
                                    repo6.SetParameterValue("crd2", card2);
                                    repo6.SetParameterValue("amo1", amo1);
                                    repo6.SetParameterValue("amo2", amo2);
                                    repo6.SetParameterValue("coun1", coun1);
                                    repo6.SetParameterValue("coun2", coun2);
                                    repo6.SetParameterValue("srt", srt);

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
                                        sfd.FileName = "Summary-Monthly";
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
                                            sfd1.FileName = "Summary-Monthly";
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
                                        sfd1.FileName = "Summary-Monthly";
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
                                case 3:
                                    ReportSC repo7 = new ReportSC();

                                    repo7.SetDatabaseLogon("APP", "12369");
                                    repo7.SetParameterValue("comp1", comp1);
                                    repo7.SetParameterValue("comp2", comp2);
                                    repo7.SetParameterValue("crd1", card1);
                                    repo7.SetParameterValue("crd2", card2);
                                    repo7.SetParameterValue("amo1", amo1);
                                    repo7.SetParameterValue("amo2", amo2);
                                    repo7.SetParameterValue("coun1", coun1);
                                    repo7.SetParameterValue("coun2", coun2);
                                    repo7.SetParameterValue("srt", srt);

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
                                        sfd.FileName = "Summary-Chronic";
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
                                            sfd1.FileName = "Summary-Chronic";
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
                                        sfd1.FileName = "Summary-Chronic";
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



                                default:
                                    break;

                            }
                            break;

                        default:

                            break;

                    }
                }

                catch
                { MessageBox.Show("error"); }
            }
            else if(cbxMainTyp.SelectedIndex == 1)
            {
                try
                {
                    switch (cbxReportTyp.SelectedIndex)
                    {
                        case 0:
                            switch (cbxScoundTyp.SelectedIndex)
                            {
                                case 0:

                                    ReportAALoc repo4 = new ReportAALoc();

                                    repo4.SetDatabaseLogon("APP", "12369");
                                    repo4.SetParameterValue("comp1", comp1);
                                    repo4.SetParameterValue("comp2", comp2);
                                    repo4.SetParameterValue("crd1", card1);
                                    repo4.SetParameterValue("crd2", card2);
                                    repo4.SetParameterValue("amo1", amo1);
                                    repo4.SetParameterValue("amo2", amo2);
                                    repo4.SetParameterValue("coun1", coun1);
                                    repo4.SetParameterValue("coun2", coun2);
                                    repo4.SetParameterValue("dat1", dat1);
                                    repo4.SetParameterValue("dat2", dat2);
                                    repo4.SetParameterValue("srt", srt);


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
                                        sfd.FileName = "Details-All";
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
                                            sfd1.FileName = "Details-All";
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
                                        sfd1.FileName = "Details-All";
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

                                case 1:

                                    ReportADLoc repo5 = new ReportADLoc();

                                    repo5.SetDatabaseLogon("APP", "12369");
                                    repo5.SetParameterValue("comp1", comp1);
                                    repo5.SetParameterValue("comp2", comp2);
                                    repo5.SetParameterValue("crd1", card1);
                                    repo5.SetParameterValue("crd2", card2);
                                    repo5.SetParameterValue("amo1", amo1);
                                    repo5.SetParameterValue("amo2", amo2);
                                    repo5.SetParameterValue("coun1", coun1);
                                    repo5.SetParameterValue("coun2", coun2);
                                    repo5.SetParameterValue("dat1", dat1);
                                    repo5.SetParameterValue("dat2", dat2);
                                    repo5.SetParameterValue("srt", srt);

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
                                        sfd.FileName = "Details-Daily";
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
                                            sfd1.FileName = "Details-Daily";
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
                                        sfd1.FileName = "Details-Daily";
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
                                    ReportAMLoc repo6 = new ReportAMLoc();

                                    repo6.SetDatabaseLogon("APP", "12369");
                                    repo6.SetParameterValue("comp1", comp1);
                                    repo6.SetParameterValue("comp2", comp2);
                                    repo6.SetParameterValue("crd1", card1);
                                    repo6.SetParameterValue("crd2", card2);
                                    repo6.SetParameterValue("amo1", amo1);
                                    repo6.SetParameterValue("amo2", amo2);
                                    repo6.SetParameterValue("coun1", coun1);
                                    repo6.SetParameterValue("coun2", coun2);
                                    repo6.SetParameterValue("dat1", dat1);
                                    repo6.SetParameterValue("dat2", dat2);
                                    repo6.SetParameterValue("srt", srt);

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
                                        sfd.FileName = "Details-Monthly";
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
                                            sfd1.FileName = "Details-Monthly";
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
                                        sfd1.FileName = "Details-Monthly";
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
                                case 3:
                                    ReportACLoc repo7 = new ReportACLoc();

                                    repo7.SetDatabaseLogon("APP", "12369");
                                    repo7.SetParameterValue("comp1", comp1);
                                    repo7.SetParameterValue("comp2", comp2);
                                    repo7.SetParameterValue("crd1", card1);
                                    repo7.SetParameterValue("crd2", card2);
                                    repo7.SetParameterValue("amo1", amo1);
                                    repo7.SetParameterValue("amo2", amo2);
                                    repo7.SetParameterValue("coun1", coun1);
                                    repo7.SetParameterValue("coun2", coun2);
                                    repo7.SetParameterValue("dat1", dat1);
                                    repo7.SetParameterValue("dat2", dat2);
                                    repo7.SetParameterValue("srt", srt);

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
                                        sfd.FileName = "Details-Chronic";
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
                                            sfd1.FileName = "Details-Chronic";
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
                                        sfd1.FileName = "Details-Chronic";
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
                                /*
                                DataSet dt = db.RunReaderds(@"SELECT M_TOT_MED.CARD_ID , M_TOT_MED.C_COMP_ID , M_TOT_MED.CONTRACT_NO , M_TOT_MED.COUNTT , M_TOT_MED.VALUE_CREDIT , 
                                                               COMP_EMPLOYEESS.EMP_ANAME_ST , COMP_EMPLOYEESS.EMP_ANAME_SC , COMP_EMPLOYEESS.EMP_ANAME_TH , COMP_EMPLOYEESS.EMP_ENAME_ST ,
                                                               COMP_EMPLOYEESS.EMP_ENAME_SC , COMP_EMPLOYEESS.EMP_ENAME_TH FROM M_TOT_MED , COMP_EMPLOYEESS WHERE     
                                                               M_TOT_MED.CARD_ID = COMP_EMPLOYEESS.CARD_ID 
                                                               AND M_TOT_MED.C_COMP_ID = COMP_EMPLOYEESS.C_COMP_ID 
                                                               AND M_TOT_MED.CONTRACT_NO = COMP_EMPLOYEESS.CONTRACT_NO
                                                               AND M_TOT_MED.C_COMP_ID BETWEEN " + comp1 + " AND " + comp2 + " AND M_TOT_MED.CARD_ID BETWEEN '" + card1 + "' AND '" + card2 + "' AND M_TOT_MED.COUNTT BETWEEN " + coun1 + " AND " + coun2 + " AND M_TOT_MED.VALUE_CREDIT BETWEEN  " + amo1 + " AND " + amo2 + " ORDER BY M_TOT_MED.CARD_ID");
                                viewall.ItemsSource = dt.Tables[0].DefaultView;

                                viewall.Visibility = Visibility.Visible;
                                btnviewdetails.Visibility = Visibility.Visible;

                                break;
                                */

                                default:
                                    break;

                            }

                            break;

                        case 1:
                            switch (cbxScoundTyp.SelectedIndex)
                            {
                                case 0:
                                    //ReportSA repo4 = new ReportSA();
                                    ReportSA2Loc repo4 = new ReportSA2Loc();

                                    repo4.SetDatabaseLogon("APP", "12369");
                                    repo4.SetParameterValue("comp1", comp1);
                                    repo4.SetParameterValue("comp2", comp2);
                                    repo4.SetParameterValue("crd1", card1);
                                    repo4.SetParameterValue("crd2", card2);
                                    repo4.SetParameterValue("amo1", amo1);
                                    repo4.SetParameterValue("amo2", amo2);
                                    repo4.SetParameterValue("coun1", coun1);
                                    repo4.SetParameterValue("coun2", coun2);
                                    repo4.SetParameterValue("srt", srt);


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
                                        sfd.FileName = "Summary-All";
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
                                            sfd1.FileName = "Summary-All";
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
                                        sfd1.FileName = "Summary-All";
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
                                case 1:

                                    ReportSDLoc repo5 = new ReportSDLoc();

                                    repo5.SetDatabaseLogon("APP", "12369");
                                    repo5.SetParameterValue("comp1", comp1);
                                    repo5.SetParameterValue("comp2", comp2);
                                    repo5.SetParameterValue("crd1", card1);
                                    repo5.SetParameterValue("crd2", card2);
                                    repo5.SetParameterValue("amo1", amo1);
                                    repo5.SetParameterValue("amo2", amo2);
                                    repo5.SetParameterValue("coun1", coun1);
                                    repo5.SetParameterValue("coun2", coun2);
                                    repo5.SetParameterValue("srt", srt);

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
                                        sfd.FileName = "Summary-Daily";
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
                                            sfd1.FileName = "Summary-Daily";
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
                                        sfd1.FileName = "Summary-Daily";
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
                                    ReportSMLoc repo6 = new ReportSMLoc();

                                    repo6.SetDatabaseLogon("APP", "12369");
                                    repo6.SetParameterValue("comp1", comp1);
                                    repo6.SetParameterValue("comp2", comp2);
                                    repo6.SetParameterValue("crd1", card1);
                                    repo6.SetParameterValue("crd2", card2);
                                    repo6.SetParameterValue("amo1", amo1);
                                    repo6.SetParameterValue("amo2", amo2);
                                    repo6.SetParameterValue("coun1", coun1);
                                    repo6.SetParameterValue("coun2", coun2);
                                    repo6.SetParameterValue("srt", srt);

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
                                        sfd.FileName = "Summary-Monthly";
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
                                            sfd1.FileName = "Summary-Monthly";
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
                                        sfd1.FileName = "Summary-Monthly";
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
                                case 3:
                                    ReportSCLoc repo7 = new ReportSCLoc();

                                    repo7.SetDatabaseLogon("APP", "12369");
                                    repo7.SetParameterValue("comp1", comp1);
                                    repo7.SetParameterValue("comp2", comp2);
                                    repo7.SetParameterValue("crd1", card1);
                                    repo7.SetParameterValue("crd2", card2);
                                    repo7.SetParameterValue("amo1", amo1);
                                    repo7.SetParameterValue("amo2", amo2);
                                    repo7.SetParameterValue("coun1", coun1);
                                    repo7.SetParameterValue("coun2", coun2);
                                    repo7.SetParameterValue("srt", srt);

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
                                        sfd.FileName = "Summary-Chronic";
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
                                            sfd1.FileName = "Summary-Chronic";
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
                                        sfd1.FileName = "Summary-Chronic";
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



                                default:
                                    break;

                            }
                            break;

                        default:

                            break;

                    }
                }

                catch
                { MessageBox.Show("error"); }
            }
        }
        private void smallamount_Checked(object sender, RoutedEventArgs e)
        {
            txtakbermnamount.Visibility = Visibility.Hidden;
            txtakbermnamount.Clear();
            txtas8urmnamount.Visibility = Visibility.Visible;
        }
        private void smallamount_Unchecked(object sender, RoutedEventArgs e)
        {

            txtakbermnamount.Visibility = Visibility.Visible;
            txtas8urmnamount.Visibility = Visibility.Hidden;
            txtas8urmnamount.Clear();
        }
        private void smallcount_Checked(object sender, RoutedEventArgs e)
        {
            txtakbermncount.Visibility = Visibility.Hidden;
            txtakbermncount.Clear();
            txtas8urmncount.Visibility = Visibility.Visible;
        }
        private void smallcount_Unchecked(object sender, RoutedEventArgs e)
        {
            txtakbermncount.Visibility = Visibility.Visible;
            txtas8urmncount.Visibility = Visibility.Hidden;
            txtas8urmncount.Clear();
        }
        private void btnviewdetails_Click(object sender, RoutedEventArgs e)
        {
            DataDB tmm = new DataDB();
            DateTime dat1, dat2;
            string crd;
            DataRowView row;

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            dat1 = (DateTime)startdate.SelectedDate.Value.Date;
            dat2 = (DateTime)enddate.SelectedDate.Value.Date;

            if (viewall.SelectedIndex >= 0)
            {
                row = (DataRowView)viewall.SelectedItems[0];
                crd = row[0].ToString();
                viewdetails.ItemsSource = tmm.getdetails(crd, dat1, dat2).DefaultView;
                MessageBox.Show(viewdetails.Items.Count.ToString());
                viewall.Visibility = Visibility.Hidden;
                viewdetails.Visibility = Visibility.Visible;
                btnviewdetails.Visibility = Visibility.Hidden;
                showcard.Content = crd;
                showname.Content = row[8].ToString() + " " + row[9].ToString() + " " + row[10].ToString();
                showcard.Visibility = Visibility.Visible;
                showname.Visibility = Visibility.Visible;
                btnback.Visibility = Visibility.Visible;
                btnprintdetails.Visibility = Visibility.Visible;
                btnviewdetails.Visibility = Visibility.Hidden;
            }
            else
                MessageBox.Show("الرجاء قم بإختيار موظف");
        }
        private void btnprintdetails_Click(object sender, RoutedEventArgs e)
        {
            /* DateTime dat1, dat2;
             string crd;
             DataRowView row;

             CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
             ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
             Thread.CurrentThread.CurrentCulture = ci;

             dat1 = (DateTime)startdate.SelectedDate.Value.Date;
             dat2 = (DateTime)enddate.SelectedDate.Value.Date;
             row = (DataRowView)viewall.SelectedItems[0];
             crd = row[0].ToString();
             */



        }
        private void btnback_Click(object sender, RoutedEventArgs e)
        {
            viewdetails.Visibility = Visibility.Hidden;
            viewall.Visibility = Visibility.Visible;
            btnviewdetails.Visibility = Visibility.Visible;
            btnprintdetails.Visibility = Visibility.Hidden;
            showcard.Visibility = Visibility.Hidden;
            showname.Visibility = Visibility.Hidden;
            btnback.Visibility = Visibility.Hidden;
        }
        private void LastContract_Checked(object sender, RoutedEventArgs e)
        {
            if (cbxStartCompNum.Text != string.Empty && cbxEndCompNum.Text != string.Empty)
            {
                if (Int32.Parse(cbxStartCompNum.Text) == Int32.Parse(cbxEndCompNum.Text))
                {
                    DataTable dtlst = db.RunReader(@"select DISTINCT C_COMP_ID , START_COVER, END_COVER, CONTRACT_NO FROM V_P_COMP_CONTRACT_CLASS 
                                                     where CONTRACT_NO = (select max(CONTRACT_NO) from V_P_COMP_CONTRACT_CLASS                                                     
                                                     where C_COMP_ID = " + Int32.Parse(cbxStartCompNum.Text) + ") AND C_COMP_ID = " + Int32.Parse(cbxStartCompNum.Text) + " ORDER BY START_COVER DESC").Result;

                    startdate.Text = dtlst.Rows[0][1].ToString();
                    enddate.Text = dtlst.Rows[0][2].ToString();

                    startdate.IsEnabled = false;
                    enddate.IsEnabled = false;
                    cbxStartCompNum.IsEnabled = false;
                    cbxEndCompNum.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("أخر عقد يأتي لشركة واحدة فقط");
                    LastContract.IsChecked = false;
                }
            }
            else
            {
                MessageBox.Show("من فضلك أدخل رقم الشركة");
                LastContract.IsChecked = false;
            }
        }
        private void LastContract_Unchecked(object sender, RoutedEventArgs e)
        {
            startdate.IsEnabled = true;
            enddate.IsEnabled = true;
            cbxStartCompNum.IsEnabled = true;
            cbxEndCompNum.IsEnabled = true;
        }
        private void cbxScoundTyp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((cbxReportTyp.SelectedIndex == 0) && (cbxScoundTyp.SelectedIndex == 0))
                DisplayAll.Visibility = Visibility.Visible;
            else
                DisplayAll.Visibility = Visibility.Hidden;
        }
        private void DisplayAll_Click(object sender, RoutedEventArgs e)
        {
            Int32 comp1, comp2, coun1, coun2;
            double amo1, amo2;
            string card1, card2;

            comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
            comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
            card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
            card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
            amo1 = txtakbermnamount.Text == string.Empty ? 0 : Convert.ToDouble(txtakbermnamount.Text);
            amo2 = txtas8urmnamount.Text == string.Empty ? 99999999999999 : Convert.ToDouble(txtas8urmnamount.Text);
            coun1 = txtakbermncount.Text == string.Empty ? 0 : Convert.ToInt32(txtakbermncount.Text);
            coun2 = txtas8urmncount.Text == string.Empty ? 999999999 : Convert.ToInt32(txtas8urmncount.Text);

            DataSet dt = db.RunReaderds(@"SELECT M_TOT_MED.CARD_ID , M_TOT_MED.C_COMP_ID , M_TOT_MED.CONTRACT_NO , M_TOT_MED.COUNTT , M_TOT_MED.VALUE_CREDIT , 
                                                           COMP_EMPLOYEESS.EMP_ANAME_ST , COMP_EMPLOYEESS.EMP_ANAME_SC , COMP_EMPLOYEESS.EMP_ANAME_TH , COMP_EMPLOYEESS.EMP_ENAME_ST ,
                                                           COMP_EMPLOYEESS.EMP_ENAME_SC , COMP_EMPLOYEESS.EMP_ENAME_TH FROM M_TOT_MED , COMP_EMPLOYEESS WHERE     
                                                           M_TOT_MED.CARD_ID = COMP_EMPLOYEESS.CARD_ID 
                                                           AND M_TOT_MED.C_COMP_ID = COMP_EMPLOYEESS.C_COMP_ID 
                                                           AND M_TOT_MED.CONTRACT_NO = COMP_EMPLOYEESS.CONTRACT_NO
                                                           AND M_TOT_MED.C_COMP_ID BETWEEN " + comp1 + " AND " + comp2 + " AND M_TOT_MED.CARD_ID BETWEEN '" + card1 + "' AND '" + card2 + "' AND M_TOT_MED.COUNTT BETWEEN " + coun1 + " AND " + coun2 + " AND M_TOT_MED.VALUE_CREDIT BETWEEN  " + amo1 + " AND " + amo2 + " ORDER BY M_TOT_MED.CARD_ID");
            viewall.ItemsSource = dt.Tables[0].DefaultView;

            viewall.Visibility = Visibility.Visible;
            btnviewdetails.Visibility = Visibility.Visible;
        }

        private void cbxReportTyp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            if (cbxReportTyp.SelectedIndex == 0)
            {
                cbxScoundTyp.SelectedIndex = 0;
                cbxScoundTyp.IsEnabled = false;
            }
            else
                cbxScoundTyp.IsEnabled = true;
                */
            if ((cbxReportTyp.SelectedIndex == 0) && (cbxScoundTyp.SelectedIndex == 0))
                DisplayAll.Visibility = Visibility.Visible;
            else
                DisplayAll.Visibility = Visibility.Hidden;
        }

    }
}
