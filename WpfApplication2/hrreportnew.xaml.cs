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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class hrreportnew : Window
    {
        public hrreportnew()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            startfun();

            if (User.Type == "hr")
            {
                cbxStartCompNum.Text = User.CompanyID;
                cbxEndCompNum.Text = User.CompanyID;

                if (User.CompanyID != "10000")
                {
                    cbxStartCompNum.IsEnabled = false;
                    cbxEndCompNum.IsEnabled = false;
                }
                else
                {
                    cbxStartCompNum.IsEnabled = true;
                    cbxEndCompNum.IsEnabled = true;
                }
            }

        }
        DB db = new DB();
        string compstartnum, compendnum;
        void startfun()
        {
            fillcompanyNumber();
            //   fillcatagory();
            //fillCardNum();
            //  fillArea();
            //    fillServiceType();

        }
        DataSet dt1;
        private void fillcompanyNumber()
        {
            dt1 = db.RunReaderds("select distinct COMP_ID from ME_AUB ORDER BY COMP_ID");
            cbxStartCompNum.ItemsSource = dt1.Tables[0].DefaultView;
            cbxEndCompNum.ItemsSource = dt1.Tables[0].DefaultView;
            //    tmpercent.Text = dt1.Tables[0].Rows[0][0].ToString();
        }
        private void fillArea()
        {
            //   DataSet dt = db.RunReaderds(" select distinct AREA_CODE from IRS_AREAS_CODES ORDER BY AREA_CODE ");
            //   cbxRegain.ItemsSource = dt.Tables[0].DefaultView;


        }
        private void fillServiceType()
        {
            // DataSet dt = db.RunReaderds(" select distinct GROUP_ENAME from ME_AUB ORDER BY GROUP_ENAME ");
            //  cbxServiceTyp.ItemsSource = dt.Tables[0].DefaultView;


        }
        private void fillcatagorystart()
        {
            DataSet dt = db.RunReaderds(" select distinct CLASS_CODE from ME_AUB WHERE COMP_ID=" + compstartnum + " ORDER BY CLASS_CODE ");
            //  cbxEndCatog.ItemsSource = dt.Tables[0].DefaultView;
            cbxStartCatog.ItemsSource = dt.Tables[0].DefaultView;

        }
        private void fillcatagoryend()
        {
            DataSet dt = db.RunReaderds(" select distinct CLASS_CODE from ME_AUB WHERE COMP_ID=" + compendnum + " ORDER BY CLASS_CODE ");
            cbxEndCatog.ItemsSource = dt.Tables[0].DefaultView;
            //  cbxStartCatog.ItemsSource = dt.Tables[0].DefaultView;

        }
        private void fillcatagory()
        {
            DataSet dt = db.RunReaderds(" select distinct CLASS_CODE from ME_AUB  ORDER BY CLASS_CODE ");
            cbxEndCatog.ItemsSource = dt.Tables[0].DefaultView;
            cbxStartCatog.ItemsSource = dt.Tables[0].DefaultView;

        }

        private void fillCardNumstart()
        {
            DataSet dt = db.RunReaderds(" select  CARD_NO from ME_AUB WHERE COMP_ID=" + compstartnum + " ORDER BY CARD_NO ");
            cbxStartCardNum.ItemsSource = dt.Tables[0].DefaultView;
        }
        private void fillCardNumend()
        {
            DataSet dt = db.RunReaderds(" select  CARD_NO from ME_AUB WHERE COMP_ID=" + compendnum + " ORDER BY CARD_NO ");
            cbxEndCardNum.ItemsSource = dt.Tables[0].DefaultView;

        }
        private void fillCardNum()
        {
            DataSet dt = db.RunReaderds(" select  CARD_NO from ME_AUB  ORDER BY CARD_NO ");
            cbxEndCardNum.ItemsSource = dt.Tables[0].DefaultView;
            cbxStartCardNum.ItemsSource = dt.Tables[0].DefaultView;

        }


        private void btnExite_Click(object sender, RoutedEventArgs e)
        {
            Close();



        }



        private void cbxEndCompNum_DropDownClosed(object sender, EventArgs e)
        {
            /*  try
              {
                  compendnum = dt1.Tables[0].Rows[cbxEndCompNum.SelectedIndex][0].ToString();
                  fillCardNumend();
                  fillcatagoryend();


              }
              catch
              { }*/
        }

        private void cbxEndCompNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*try
            {
                compendnum = dt1.Tables[0].Rows[cbxEndCompNum.SelectedIndex][0].ToString();
                fillCardNumend();
                fillcatagoryend();


            }
            catch
            { }*/
        }

        private void cbxStartCardNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxStartCompNum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*try
            {
                compstartnum = dt1.Tables[0].Rows[cbxStartCompNum.SelectedIndex][0].ToString();
                fillCardNumstart();
                fillcatagorystart();


            }
            catch
            { }*/
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Int32 comp1, comp2, are1, are2, per, lrg, sml;
            string card1, card2, cls1, cls2;
            DateTime dreg1, dreg2, srda1, srda2;





            dreg1 = (DateTime)dpStartRegest.SelectedDate;
            dreg2 = (DateTime)dpEndRegast.SelectedDate;
            srda1 = (DateTime)dpStartServes.SelectedDate;
            srda2 = (DateTime)dpEndServes.SelectedDate;

            per = tmpercent.Text == string.Empty ? 0 : Convert.ToInt32(tmpercent.Text);
            lrg = txtakbermn.Text == string.Empty ? 0 : Convert.ToInt32(txtakbermn.Text);
            sml = txtas8urmn.Text == string.Empty ? 0 : Convert.ToInt32(txtas8urmn.Text);

            View_Report showreport = new View_Report();
            try
            {
                switch (cbxReportTyp.SelectedIndex)
                {
                    case 0:

                        comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                        comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                        card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                        card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;


                        Reportaub1 repo = new Reportaub1();

                        repo.SetDatabaseLogon("APP", "12369");
                        repo.SetParameterValue("crda1", dreg1);
                        repo.SetParameterValue("crda2", dreg2);
                        repo.SetParameterValue("srda1", srda1);
                        repo.SetParameterValue("srda2", srda2);
                        repo.SetParameterValue("comp1", comp1);
                        repo.SetParameterValue("comp2", comp2);
                        repo.SetParameterValue("crd1", card1);
                        repo.SetParameterValue("crd2", card2);

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




                        //Reportaub2___Copy repo2 = new Reportaub2___Copy();
                        Reportaub2 repo2 = new Reportaub2();
                        repo2.SetDatabaseLogon("APP", "12369");
                        repo2.SetParameterValue("crda1", dreg1);
                        repo2.SetParameterValue("crda2", dreg2);
                        repo2.SetParameterValue("comp1", comp1);
                        repo2.SetParameterValue("comp2", comp2);
                        repo2.SetParameterValue("crd1", card1);
                        repo2.SetParameterValue("crd2", card2);
                        repo2.SetParameterValue("srda1", srda1);
                        repo2.SetParameterValue("srda2", srda2);

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
                    /*
                case 2:
                    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;



                    Reportaub3 repo3 = new Reportaub3();
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
                            exp.ExportFormatType = ExportFormatType.Excel;
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
                        exp.ExportFormatType = ExportFormatType.Excel;
                        exp.ExportFormatOptions = exexl;
                        exp.ExportDestinationOptions = dis;
                        repo3.Export();
                        MessageBox.Show("Successfull Export to Excel");
                    }
                    else
                        MessageBox.Show("Thank you");
                    break;
                     */
                    case 2:
                        comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                        comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                        card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                        card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                        cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                        cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;

                        Reportaub4 repo4 = new Reportaub4();
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
                                exp.ExportFormatType = ExportFormatType.Excel;
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
                            exp.ExportFormatType = ExportFormatType.Excel;
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

                        Reportaub7 repo7 = new Reportaub7();
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

                    case 4:

                        //     MessageBox.Show(((srda2 - srda1).TotalDays).ToString());

                        comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                        comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                        if (ContarctNo.Text != string.Empty)
                        {
                            if ((srda2 - srda1).TotalDays >= 364 && (srda2 - srda1).TotalDays <= 366)
                            {

                                int co = int.Parse(ContarctNo.Text);
                                DataSet dds = db.RunReaderds("select tot_amt from summary_report where contract_no = " + co + "AND comp_id =" + comp1 + " ");

                                if ((dds.Tables[0].Rows.Count == 0) || (dds.Tables[0].Rows[0][0].ToString() == string.Empty))
                                {
                                    Reportaub10 repo10 = new Reportaub10();
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
                                            exp.ExportFormatType = ExportFormatType.Excel;
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
                                        exp.ExportFormatType = ExportFormatType.Excel;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo10.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                }
                                else
                                {
                                    Reportaub10_2 repo10 = new Reportaub10_2();

                                    repo10.SetDatabaseLogon("APP", "12369");
                                    repo10.SetParameterValue("crda1", dreg1);
                                    repo10.SetParameterValue("crda2", dreg2);
                                    repo10.SetParameterValue("comp1", comp1);
                                    repo10.SetParameterValue("comp2", comp2);

                                    repo10.SetParameterValue("srda1", srda1);
                                    repo10.SetParameterValue("srda2", srda2);
                                    repo10.SetParameterValue("cont1", co);




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
                                            exp.ExportFormatType = ExportFormatType.Excel;
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
                                        exp.ExportFormatType = ExportFormatType.Excel;
                                        exp.ExportFormatOptions = exexl;
                                        exp.ExportDestinationOptions = dis;
                                        repo10.Export();
                                        MessageBox.Show("Successfull Export to Excel");
                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                }
                            }
                            else
                                MessageBox.Show("التقرير الملخص يظهر لسنة كاملة فقط وعقد واحد");
                        }
                        else
                            MessageBox.Show("من فضلك أدخل رقم العقد");

                        break;
                    case 5:

                        comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                        comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                        card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                        card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;

                        Reportaub11 repo11 = new Reportaub11();


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
                                exp.ExportFormatType = ExportFormatType.Excel;
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
                            exp.ExportFormatType = ExportFormatType.Excel;
                            exp.ExportFormatOptions = exexl;
                            exp.ExportDestinationOptions = dis;
                            repo11.Export();
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

                        Reportaub12 repo12 = new Reportaub12();

                        repo12.SetDatabaseLogon("APP", "12369");
                        repo12.SetParameterValue("crda1", dreg1);
                        repo12.SetParameterValue("crda2", dreg2);
                        repo12.SetParameterValue("comp1", comp1);
                        repo12.SetParameterValue("comp2", comp2);
                        repo12.SetParameterValue("crd1", card1);
                        repo12.SetParameterValue("crd2", card2);
                        repo12.SetParameterValue("srda1", srda1);
                        repo12.SetParameterValue("srda2", srda2);



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

                    case 7:
                        comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                        comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                        card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                        card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                        cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                        cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;

                        Reportaub13 repo13 = new Reportaub13();
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
                                exp.ExportFormatType = ExportFormatType.Excel;
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
                            exp.ExportFormatType = ExportFormatType.Excel;
                            exp.ExportFormatOptions = exexl;
                            exp.ExportDestinationOptions = dis;
                            repo13.Export();
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


                        //Reportaub14 repo14 = new Reportaub14();
                        Reportaub14New repo14 = new Reportaub14New();

                        repo14.SetDatabaseLogon("APP", "12369");
                        repo14.SetParameterValue("crda1", dreg1);
                        repo14.SetParameterValue("crda2", dreg2);
                        repo14.SetParameterValue("comp1", comp1);
                        repo14.SetParameterValue("comp2", comp2);
                        repo14.SetParameterValue("crd1", card1);
                        repo14.SetParameterValue("crd2", card2);
                        repo14.SetParameterValue("srda1", srda1);
                        repo14.SetParameterValue("srda2", srda2);


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
                                exp.ExportFormatType = ExportFormatType.Excel;
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
                            exp.ExportFormatType = ExportFormatType.Excel;
                            exp.ExportFormatOptions = exexl;
                            exp.ExportDestinationOptions = dis;
                            repo14.Export();
                            MessageBox.Show("Successfull Export to Excel");
                        }
                        else
                            MessageBox.Show("Thank you");

                        break;
                    //case 9:
                    //    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                    //    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                    //    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                    //    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                    //    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                    //    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;
                    //    // are1 = cbxRegain.Text == string.Empty ? 0 : Convert.ToInt32(cbxRegain.Text);
                    //    //  are2 = cbxRegain.Text == string.Empty ? 99999 : Convert.ToInt32(cbxRegain.Text);
                    //    are1 = 0;
                    //    are2 = 99999;
                    //    Reportaub5 repo5 = new Reportaub5();
                    //    repo5.SetDatabaseLogon("APP", "12369");
                    //    repo5.SetParameterValue("crda1", dreg1);
                    //    repo5.SetParameterValue("crda2", dreg2);
                    //    repo5.SetParameterValue("comp1", comp1);
                    //    repo5.SetParameterValue("comp2", comp2);
                    //    repo5.SetParameterValue("crd1", card1);
                    //    repo5.SetParameterValue("crd2", card2);
                    //    repo5.SetParameterValue("srda1", srda1);
                    //    repo5.SetParameterValue("srda2", srda2);
                    //    repo5.SetParameterValue("cls1", cls1);
                    //    repo5.SetParameterValue("cls2", cls2);
                    //    repo5.SetParameterValue("are1", are1);
                    //    repo5.SetParameterValue("are2", are2);


                    //    repo5.SetParameterValue("per", per);
                    //    repo5.SetParameterValue("larg", lrg);
                    //    repo5.SetParameterValue("small", sml);

                    //    showreport.crystalReportViewer1.ReportSource = repo5;
                    //    showreport.ShowDialog();
                    //    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    //    {
                    //        ExportOptions exp = new ExportOptions();
                    //        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                    //        PdfFormatOptions expdf = new PdfFormatOptions();
                    //        string sa = "";

                    //        SaveFileDialog sfd = new SaveFileDialog();
                    //        sfd.Filter = "Pdf file|*.pdf";
                    //        sfd.FileName = "Consumption Per Area";
                    //        if (sfd.ShowDialog() == true)
                    //            sa = sfd.FileName;

                    //        dis.DiskFileName = sa;
                    //        exp = repo5.ExportOptions;
                    //        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    //        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                    //        exp.ExportFormatOptions = expdf;
                    //        exp.ExportDestinationOptions = dis;
                    //        repo5.Export();

                    //        MessageBox.Show("Successfull Export to Pdf");

                    //        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    //        {
                    //            ExcelFormatOptions exexl = new ExcelFormatOptions();
                    //            string sa1 = "";

                    //            SaveFileDialog sfd1 = new SaveFileDialog();
                    //            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    //            sfd1.FileName = "Consumption Per Area";
                    //            if (sfd1.ShowDialog() == true)
                    //                sa1 = sfd1.FileName;

                    //            dis.DiskFileName = sa1;
                    //            exp = repo5.ExportOptions;

                    //            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    //            exp.ExportFormatType = ExportFormatType.Excel;
                    //            exp.ExportFormatOptions = exexl;
                    //            exp.ExportDestinationOptions = dis;
                    //            repo5.Export();
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
                    //        sfd1.FileName = "Consumption Per Area";
                    //        if (sfd1.ShowDialog() == true)
                    //            sa1 = sfd1.FileName;

                    //        dis.DiskFileName = sa1;
                    //        exp = repo5.ExportOptions;

                    //        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    //        exp.ExportFormatType = ExportFormatType.Excel;
                    //        exp.ExportFormatOptions = exexl;
                    //        exp.ExportDestinationOptions = dis;
                    //        repo5.Export();
                    //        MessageBox.Show("Successfull Export to Excel");
                    //    }
                    //    else
                    //        MessageBox.Show("Thank you");
                    //    break;
                    //case 10:
                    //    comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);
                    //    comp2 = cbxEndCompNum.Text == string.Empty ? 999999999 : Convert.ToInt32(cbxEndCompNum.Text);
                    //    card1 = cbxStartCardNum.Text == string.Empty ? "0" : cbxStartCardNum.Text;
                    //    card2 = cbxEndCardNum.Text == string.Empty ? "9999999999999999999999" : cbxEndCardNum.Text;
                    //    cls1 = cbxStartCatog.Text == string.Empty ? "0" : cbxStartCatog.Text;
                    //    cls2 = cbxEndCatog.Text == string.Empty ? "zzzzz" : cbxEndCatog.Text;

                    //    Reportaub6 repo6 = new Reportaub6();
                    //    repo6.SetDatabaseLogon("APP", "12369");
                    //    repo6.SetParameterValue("crda1", dreg1);
                    //    repo6.SetParameterValue("crda2", dreg2);
                    //    repo6.SetParameterValue("comp1", comp1);
                    //    repo6.SetParameterValue("comp2", comp2);
                    //    repo6.SetParameterValue("crd1", card1);
                    //    repo6.SetParameterValue("crd2", card2);
                    //    repo6.SetParameterValue("srda1", srda1);
                    //    repo6.SetParameterValue("srda2", srda2);
                    //    repo6.SetParameterValue("cls1", cls1);
                    //    repo6.SetParameterValue("cls2", cls2);


                    //    repo6.SetParameterValue("per", per);
                    //    repo6.SetParameterValue("larg", lrg);
                    //    repo6.SetParameterValue("small", sml);

                    //    showreport.crystalReportViewer1.ReportSource = repo6;
                    //    showreport.ShowDialog();
                    //    if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    //    {
                    //        ExportOptions exp = new ExportOptions();
                    //        DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                    //        PdfFormatOptions expdf = new PdfFormatOptions();
                    //        string sa = "";

                    //        SaveFileDialog sfd = new SaveFileDialog();
                    //        sfd.Filter = "Pdf file|*.pdf";
                    //        sfd.FileName = "Consumption Per Gender";
                    //        if (sfd.ShowDialog() == true)
                    //            sa = sfd.FileName;

                    //        dis.DiskFileName = sa;
                    //        exp = repo6.ExportOptions;
                    //        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    //        exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                    //        exp.ExportFormatOptions = expdf;
                    //        exp.ExportDestinationOptions = dis;
                    //        repo6.Export();

                    //        MessageBox.Show("Successfull Export to Pdf");

                    //        if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    //        {
                    //            ExcelFormatOptions exexl = new ExcelFormatOptions();
                    //            string sa1 = "";

                    //            SaveFileDialog sfd1 = new SaveFileDialog();
                    //            sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    //            sfd1.FileName = "Consumption Per Gender";
                    //            if (sfd1.ShowDialog() == true)
                    //                sa1 = sfd1.FileName;

                    //            dis.DiskFileName = sa1;
                    //            exp = repo6.ExportOptions;

                    //            exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    //            exp.ExportFormatType = ExportFormatType.Excel;
                    //            exp.ExportFormatOptions = exexl;
                    //            exp.ExportDestinationOptions = dis;
                    //            repo6.Export();
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
                    //        sfd1.FileName = "Consumption Per Gender";
                    //        if (sfd1.ShowDialog() == true)
                    //            sa1 = sfd1.FileName;

                    //        dis.DiskFileName = sa1;
                    //        exp = repo6.ExportOptions;

                    //        exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    //        exp.ExportFormatType = ExportFormatType.Excel;
                    //        exp.ExportFormatOptions = exexl;
                    //        exp.ExportDestinationOptions = dis;
                    //        repo6.Export();
                    //        MessageBox.Show("Successfull Export to Excel");
                    //    }
                    //    else
                    //        MessageBox.Show("Thank you");

                    //    break;
                    default:

                        break;

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

        private void dpStartRegest_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(IsNumber(e.Key) || e.Key == Key.Subtract || e.Key == Key.Divide))
                e.Handled = true;
        }

        private void cbxStartCardNum_TouchLeave(object sender, TouchEventArgs e)
        {
            if (cbxStartCardNum.Text != "hello")
                MessageBox.Show("GOOOOOOOOOD");
        }

        private void ContarctNo_LostFocus(object sender, RoutedEventArgs e)
        {
            Int32 comp1 = cbxStartCompNum.Text == string.Empty ? 0 : Convert.ToInt32(cbxStartCompNum.Text);

            if (ContarctNo.Text != string.Empty)
            {
                int co = int.Parse(ContarctNo.Text);
                DataSet dds = db.RunReaderds("select begin_date, end_date from summary_report where contract_no = " + co + "AND comp_id =" + comp1 + " ");
                if (dds.Tables[0].Rows.Count != 0)
                {
                    dpStartServes.Text = dds.Tables[0].Rows[0][0].ToString();
                    dpEndServes.Text = dds.Tables[0].Rows[0][1].ToString();
                    dpStartServes.IsEnabled = false;
                    dpEndServes.IsEnabled = false;
                }
            }
        }

        private void ContarctNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsNumber(e.Key))
                e.Handled = true;
        }

        private void cbxReportTyp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxReportTyp.SelectedIndex != 4)
            {
                dpStartServes.IsEnabled = true;
                dpEndServes.IsEnabled = true;
            }
            else
            {
                dpStartServes.IsEnabled = false;
                dpEndServes.IsEnabled = false;
            }

        }

        private void cbxStartCompNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsNumber(e.Key))
                e.Handled = true;
        }




    }
}
