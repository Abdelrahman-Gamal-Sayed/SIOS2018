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
    /// Interaction logic for hrreportmol5sesthlaak.xaml
    /// </summary>
    public partial class hrreportmol5sesthlaak : Window
    {
        public hrreportmol5sesthlaak()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //if (User.Type == "hr")
            //{
            //    CompNumber.Text = User.CompanyID;
            //    CompNumber.IsEnabled = false;
            //    FillCard();
            //}

            if (User.CompanyID != string.Empty)
            {
                CompNumber.Text = User.CompanyID;
                CompNumber.IsEnabled = false;
                FillCard();
            }
            //Page3.cardfromcustomer;
            //DTM5-7
            if (User.Type == "DMS Member" && Page3.compfromcustomer != string.Empty)// && User.Department == "customerservices")
            {
                CompNumber.Text = Page3.compfromcustomer;
                FillCard();
                CardNumber.Text = Page3.cardfromcustomer;
            }

            if (CompNumber.Text.StartsWith("500") == true || Convert.ToInt32(CompNumber.Text) == 800800)
                btnPrintIRS.Visibility = Visibility.Hidden;
            else
                btnPrint.Visibility = Visibility.Hidden;

        }

        DB db = new DB();
        DB dbI = new DB();
        DataSet dt1;
        private void FillCard()
        {
            if (CompNumber.Text.StartsWith("500") == true || Convert.ToInt32(CompNumber.Text) == 800800)
            {
                dt1 = db.RunReaderds("select distinct CARD_NO from ME_AUB WHERE COMP_ID = " + Convert.ToInt32(CompNumber.Text) + " ORDER BY CARD_NO");
                CardNumber.ItemsSource = dt1.Tables[0].DefaultView;
            }
            else
            {
                dt1 = dbI.RunReaderds("select distinct CARD_NO from IRS_V_CLAIM_BILL WHERE COMP_ID = " + Convert.ToInt32(CompNumber.Text) + " ORDER BY CARD_NO");
                CardNumber.ItemsSource = dt1.Tables[0].DefaultView;
            }
        }
        private void btnExite_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            if (CardNumber.Text != string.Empty)
            {
                DataSet dt = db.RunReaderds("select Distinct CARD_NO from ME_AUB WHERE CARD_NO = '" + CardNumber.Text + "' ");
                if (dt.Tables[0].Rows.Count > 0)
                {
                    if (AllCard.IsChecked != true)
                    {
                        Int32 comp, grp1, grp2;
                        string card;
                        DateTime srda1, srda2;

                        CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
                        ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
                        Thread.CurrentThread.CurrentCulture = ci;

                        srda1 = (DateTime)dpStartServes.SelectedDate;
                        srda2 = (DateTime)dpEndServes.SelectedDate;

                        comp = Convert.ToInt32(CompNumber.Text);
                        card = CardNumber.Text;

                        View_Report showreport = new View_Report();
                        try
                        {
                            switch (ServiceType.SelectedIndex)
                            {
                                case 0:
                                    grp1 = 1016;
                                    ReportHr repo = new ReportHr();

                                    repo.SetDatabaseLogon("APP", "12369");

                                    repo.SetParameterValue("srda1", srda1);
                                    repo.SetParameterValue("srda2", srda2);
                                    repo.SetParameterValue("comp", comp);
                                    repo.SetParameterValue("crd", card);
                                    repo.SetParameterValue("grp1", grp1);
                                    repo.SetParameterValue("grp2", grp1);

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


                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;
                                case 1:

                                    grp1 = 1014;

                                    ReportHr repo2 = new ReportHr();

                                    repo2.SetDatabaseLogon("APP", "12369");

                                    repo2.SetParameterValue("srda1", srda1);
                                    repo2.SetParameterValue("srda2", srda2);
                                    repo2.SetParameterValue("comp", comp);
                                    repo2.SetParameterValue("crd", card);
                                    repo2.SetParameterValue("grp1", grp1);
                                    repo2.SetParameterValue("grp2", grp1);

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

                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 2:
                                    grp1 = 1009;

                                    ReportHr repo3 = new ReportHr();

                                    repo3.SetDatabaseLogon("APP", "12369");

                                    repo3.SetParameterValue("srda1", srda1);
                                    repo3.SetParameterValue("srda2", srda2);
                                    repo3.SetParameterValue("comp", comp);
                                    repo3.SetParameterValue("crd", card);
                                    repo3.SetParameterValue("grp1", grp1);
                                    repo3.SetParameterValue("grp2", grp1);

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


                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;
                                case 3:
                                    grp1 = 116;
                                    grp2 = 1013;
                                    ReportHr repo4 = new ReportHr();

                                    repo4.SetDatabaseLogon("APP", "12369");

                                    repo4.SetParameterValue("srda1", srda1);
                                    repo4.SetParameterValue("srda2", srda2);
                                    repo4.SetParameterValue("comp", comp);
                                    repo4.SetParameterValue("crd", card);
                                    repo4.SetParameterValue("grp1", grp1);
                                    repo4.SetParameterValue("grp2", grp2);

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


                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;

                                case 4:

                                    ReportHrPrv repo5 = new ReportHrPrv();

                                    repo5.SetDatabaseLogon("APP", "12369");

                                    repo5.SetParameterValue("srda1", srda1);
                                    repo5.SetParameterValue("srda2", srda2);
                                    repo5.SetParameterValue("comp", comp);
                                    repo5.SetParameterValue("crd", card);


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

                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;
                                default:
                                    break;

                            }
                        }
                        catch
                        {
                            MessageBox.Show("Sorry, حدثت مشكلة حاول المحاولة مرة أخرى");
                        }
                    }
                    else
                    {
                        Int32 comp, grp1, grp2;
                        string card, scard;
                        DateTime srda1, srda2;

                        CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
                        ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
                        Thread.CurrentThread.CurrentCulture = ci;

                        srda1 = (DateTime)dpStartServes.SelectedDate;
                        srda2 = (DateTime)dpEndServes.SelectedDate;

                        comp = Convert.ToInt32(CompNumber.Text);
                        card = CardNumber.Text;

                        scard = card.Substring(0, card.LastIndexOf('-') + 1);

                        View_Report showreport = new View_Report();
                        try
                        {
                            switch (ServiceType.SelectedIndex)
                            {
                                case 0:
                                    grp1 = 1016;
                                    ReportHrSub repo = new ReportHrSub();

                                    repo.SetDatabaseLogon("APP", "12369");

                                    repo.SetParameterValue("srda1", srda1);
                                    repo.SetParameterValue("srda2", srda2);
                                    repo.SetParameterValue("comp", comp);
                                    repo.SetParameterValue("crd", card);
                                    repo.SetParameterValue("grp1", grp1);
                                    repo.SetParameterValue("grp2", grp1);
                                    repo.SetParameterValue("scrd", scard);

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


                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;
                                case 1:

                                    grp1 = 1014;

                                    ReportHrSub repo2 = new ReportHrSub();

                                    repo2.SetDatabaseLogon("APP", "12369");

                                    repo2.SetParameterValue("srda1", srda1);
                                    repo2.SetParameterValue("srda2", srda2);
                                    repo2.SetParameterValue("comp", comp);
                                    repo2.SetParameterValue("crd", card);
                                    repo2.SetParameterValue("grp1", grp1);
                                    repo2.SetParameterValue("grp2", grp1);
                                    repo2.SetParameterValue("scrd", scard);

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

                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 2:
                                    grp1 = 1009;

                                    ReportHrSub repo3 = new ReportHrSub();

                                    repo3.SetDatabaseLogon("APP", "12369");

                                    repo3.SetParameterValue("srda1", srda1);
                                    repo3.SetParameterValue("srda2", srda2);
                                    repo3.SetParameterValue("comp", comp);
                                    repo3.SetParameterValue("crd", card);
                                    repo3.SetParameterValue("grp1", grp1);
                                    repo3.SetParameterValue("grp2", grp1);
                                    repo3.SetParameterValue("scrd", scard);

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


                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;
                                case 3:
                                    grp1 = 116;
                                    grp2 = 1013;
                                    ReportHrSub repo4 = new ReportHrSub();

                                    repo4.SetDatabaseLogon("APP", "12369");

                                    repo4.SetParameterValue("srda1", srda1);
                                    repo4.SetParameterValue("srda2", srda2);
                                    repo4.SetParameterValue("comp", comp);
                                    repo4.SetParameterValue("crd", card);
                                    repo4.SetParameterValue("grp1", grp1);
                                    repo4.SetParameterValue("grp2", grp2);
                                    repo4.SetParameterValue("scrd", scard);

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


                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;

                                case 4:

                                    ReportHrPrvSub repo5 = new ReportHrPrvSub();

                                    repo5.SetDatabaseLogon("APP", "12369");

                                    repo5.SetParameterValue("srda1", srda1);
                                    repo5.SetParameterValue("srda2", srda2);
                                    repo5.SetParameterValue("comp", comp);
                                    repo5.SetParameterValue("crd", card);
                                    repo5.SetParameterValue("scrd", scard);

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

                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;
                                default:
                                    break;

                            }
                        }
                        catch
                        {
                            MessageBox.Show("Sorry, حدثت مشكلة حاول المحاولة مرة أخرى");
                        }
                    }
                }
                else
                    MessageBox.Show("من فضلك أدخل رقم كارت صحيح");
            }
            else
                MessageBox.Show("أدخل رقم الكارت من فضلك");
        }



        /*

          private void cbxStartCompNum_KeyDown(object sender, KeyEventArgs e)
           {
              if (!IsNumber(e.Key))
                  e.Handled = true;

           }
         */
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
        
        private void btnPrint_Copy_Click(object sender, RoutedEventArgs e)
        {
            Int32 comp;

            View_Report showreport = new View_Report();

            comp = CompNumber.Text == string.Empty ? 0 : Convert.ToInt32(CompNumber.Text);

            ReportEmp repoa01 = new ReportEmp();

            repoa01.SetDatabaseLogon("APP", "12369");
            repoa01.SetParameterValue("comp", comp);
            repoa01.SetParameterValue("rel1", 0);
            repoa01.SetParameterValue("rel2", 99999999);

            showreport.crystalReportViewer1.ReportSource = repoa01;
            showreport.ShowDialog();

            if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ExportOptions exp = new ExportOptions();
                DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                PdfFormatOptions expdf = new PdfFormatOptions();
                string sa = "";

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Pdf file|*.pdf";
                sfd.FileName = "Terminated List";
                if (sfd.ShowDialog() == true)
                    sa = sfd.FileName;

                dis.DiskFileName = sa;
                exp = repoa01.ExportOptions;
                exp.ExportDestinationType = ExportDestinationType.DiskFile;
                exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                exp.ExportFormatOptions = expdf;
                exp.ExportDestinationOptions = dis;
                repoa01.Export();

                MessageBox.Show("Successfull Export to Pdf");

                if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExcelFormatOptions exexl = new ExcelFormatOptions();
                    string sa1 = "";

                    SaveFileDialog sfd1 = new SaveFileDialog();
                    sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    sfd1.FileName = "Terminated List";
                    if (sfd1.ShowDialog() == true)
                        sa1 = sfd1.FileName;

                    dis.DiskFileName = sa1;
                    exp = repoa01.ExportOptions;

                    exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    exp.ExportFormatType = ExportFormatType.Excel;
                    exp.ExportFormatOptions = exexl;
                    exp.ExportDestinationOptions = dis;
                    repoa01.Export();
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
                sfd1.FileName = "Terminated List";
                if (sfd1.ShowDialog() == true)
                    sa1 = sfd1.FileName;

                dis.DiskFileName = sa1;
                exp = repoa01.ExportOptions;

                exp.ExportDestinationType = ExportDestinationType.DiskFile;
                exp.ExportFormatType = ExportFormatType.Excel;
                exp.ExportFormatOptions = exexl;
                exp.ExportDestinationOptions = dis;
                repoa01.Export();
                MessageBox.Show("Successfull Export to Excel");
            }
            else
                MessageBox.Show("Thank you");
        }

        private void btnPrint_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Int32 comp;

            View_Report showreport = new View_Report();

            comp = CompNumber.Text == string.Empty ? 0 : Convert.ToInt32(CompNumber.Text);

            ReportEmpY repoa01 = new ReportEmpY();

            repoa01.SetDatabaseLogon("APP", "12369");
            repoa01.SetParameterValue("comp", comp);
            repoa01.SetParameterValue("rel1", 0);
            repoa01.SetParameterValue("rel2", 99999999);

            showreport.crystalReportViewer1.ReportSource = repoa01;
            showreport.ShowDialog();

            if (MessageBox.Show("Do you want save report to pdf file", "Save pdf file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ExportOptions exp = new ExportOptions();
                DiskFileDestinationOptions dis = new DiskFileDestinationOptions();

                PdfFormatOptions expdf = new PdfFormatOptions();
                string sa = "";

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Pdf file|*.pdf";
                sfd.FileName = "Terminated List";
                if (sfd.ShowDialog() == true)
                    sa = sfd.FileName;

                dis.DiskFileName = sa;
                exp = repoa01.ExportOptions;
                exp.ExportDestinationType = ExportDestinationType.DiskFile;
                exp.ExportFormatType = ExportFormatType.PortableDocFormat;
                exp.ExportFormatOptions = expdf;
                exp.ExportDestinationOptions = dis;
                repoa01.Export();

                MessageBox.Show("Successfull Export to Pdf");

                if (MessageBox.Show("Do you want save report to Excel file", "Save Excel file", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ExcelFormatOptions exexl = new ExcelFormatOptions();
                    string sa1 = "";

                    SaveFileDialog sfd1 = new SaveFileDialog();
                    sfd1.Filter = "Excel file|*.xls ; *.xlsx";
                    sfd1.FileName = "Terminated List";
                    if (sfd1.ShowDialog() == true)
                        sa1 = sfd1.FileName;

                    dis.DiskFileName = sa1;
                    exp = repoa01.ExportOptions;

                    exp.ExportDestinationType = ExportDestinationType.DiskFile;
                    exp.ExportFormatType = ExportFormatType.Excel;
                    exp.ExportFormatOptions = exexl;
                    exp.ExportDestinationOptions = dis;
                    repoa01.Export();
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
                sfd1.FileName = "Terminated List";
                if (sfd1.ShowDialog() == true)
                    sa1 = sfd1.FileName;

                dis.DiskFileName = sa1;
                exp = repoa01.ExportOptions;

                exp.ExportDestinationType = ExportDestinationType.DiskFile;
                exp.ExportFormatType = ExportFormatType.Excel;
                exp.ExportFormatOptions = exexl;
                exp.ExportDestinationOptions = dis;
                repoa01.Export();
                MessageBox.Show("Successfull Export to Excel");
            }
            else
                MessageBox.Show("Thank you");
        }


    
   
        private void btnPrintIRS_Click(object sender, RoutedEventArgs e)
        {
            if (CardNumber.Text != string.Empty)
            {
                DataSet dt = dbI.RunReaderds("select Distinct C_COMP_ID from COMP_EMPLOYEESS WHERE CARD_ID = '" + CardNumber.Text + "' ");
                if (dt.Tables[0].Rows.Count > 0)
                {
                    if (AllCard.IsChecked != true)
                    {
                        Int32 comp, grp1, grp2;
                        string card;
                        DateTime srda1, srda2;

                        CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
                        ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
                        Thread.CurrentThread.CurrentCulture = ci;

                        srda1 = (DateTime)dpStartServes.SelectedDate;
                        srda2 = (DateTime)dpEndServes.SelectedDate;

                        comp = Convert.ToInt32(CompNumber.Text);
                        card = CardNumber.Text;

                        View_Report showreport = new View_Report();
                        try
                        {
                            switch (ServiceType.SelectedIndex)
                            {
                                case 0:
                                   // codehere
                                    // grp1 = 1016;
                                    ReportAG.Motalbaat165 repo = new ReportAG.Motalbaat165();
                                    repo.SetDatabaseLogon("APP", "12369");

                                    repo.SetParameterValue("srda1", srda1);
                                    repo.SetParameterValue("srda2", srda2);
                                    repo.SetParameterValue("comp", comp);
                                    repo.SetParameterValue("crd", card);


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


                                    }
                                    else
                                        MessageBox.Show("Thank you");


                                    break;
                                case 1:

                                    grp1 = 1014;

                                    ReportHrIRS repo2 = new ReportHrIRS();

                                    repo2.SetDatabaseLogon("APP", "12369");

                                    repo2.SetParameterValue("srda1", srda1);
                                    repo2.SetParameterValue("srda2", srda2);
                                    repo2.SetParameterValue("comp", comp);
                                    repo2.SetParameterValue("crd", card);
                                    repo2.SetParameterValue("grp1", grp1);
                                    repo2.SetParameterValue("grp2", grp1);

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

                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 2:
                                    grp1 = 1009;

                                    ReportHrIRS repo3 = new ReportHrIRS();

                                    repo3.SetDatabaseLogon("APP", "12369");

                                    repo3.SetParameterValue("srda1", srda1);
                                    repo3.SetParameterValue("srda2", srda2);
                                    repo3.SetParameterValue("comp", comp);
                                    repo3.SetParameterValue("crd", card);
                                    repo3.SetParameterValue("grp1", grp1);
                                    repo3.SetParameterValue("grp2", grp1);

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


                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;
                                case 3:
                                    grp1 = 116;
                                    grp2 = 1013;
                                    ReportHrIRS repo4 = new ReportHrIRS();

                                    repo4.SetDatabaseLogon("APP", "12369");

                                    repo4.SetParameterValue("srda1", srda1);
                                    repo4.SetParameterValue("srda2", srda2);
                                    repo4.SetParameterValue("comp", comp);
                                    repo4.SetParameterValue("crd", card);
                                    repo4.SetParameterValue("grp1", grp1);
                                    repo4.SetParameterValue("grp2", grp2);

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


                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;

                                case 4:

                                    ReportHrPrvIRS repo5 = new ReportHrPrvIRS();

                                    repo5.SetDatabaseLogon("APP", "12369"); ;

                                    repo5.SetParameterValue("srda1", srda1);
                                    repo5.SetParameterValue("srda2", srda2);
                                    repo5.SetParameterValue("comp", comp);
                                    repo5.SetParameterValue("crd", card);


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

                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;
                                default:
                                    break;

                            }
                        }
                        catch
                        {
                            MessageBox.Show("Sorry, حدثت مشكلة حاول المحاولة مرة أخرى");
                        }
                    }
                    else
                    {
                        Int32 comp, grp1, grp2;
                        string card, scard;
                        DateTime srda1, srda2;

                        CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
                        ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
                        Thread.CurrentThread.CurrentCulture = ci;

                        srda1 = (DateTime)dpStartServes.SelectedDate;
                        srda2 = (DateTime)dpEndServes.SelectedDate;

                        comp = Convert.ToInt32(CompNumber.Text);
                        card = CardNumber.Text;

                        scard = card.Substring(0, card.LastIndexOf('-') + 1);

                        View_Report showreport = new View_Report();
                        try
                        {
                            switch (ServiceType.SelectedIndex)
                            {
                                case 0:
                                    grp1 = 1016;
                                    ReportHrSubIRS repo = new ReportHrSubIRS();

                                    repo.SetDatabaseLogon("APP", "12369");

                                    repo.SetParameterValue("srda1", srda1);
                                    repo.SetParameterValue("srda2", srda2);
                                    repo.SetParameterValue("comp", comp);
                                    repo.SetParameterValue("crd", card);
                                    repo.SetParameterValue("grp1", grp1);
                                    repo.SetParameterValue("grp2", grp1);
                                    repo.SetParameterValue("scrd", scard);

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


                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;
                                case 1:

                                    grp1 = 1014;

                                    ReportHrSubIRS repo2 = new ReportHrSubIRS();

                                    repo2.SetDatabaseLogon("APP", "12369");

                                    repo2.SetParameterValue("srda1", srda1);
                                    repo2.SetParameterValue("srda2", srda2);
                                    repo2.SetParameterValue("comp", comp);
                                    repo2.SetParameterValue("crd", card);
                                    repo2.SetParameterValue("grp1", grp1);
                                    repo2.SetParameterValue("grp2", grp1);
                                    repo2.SetParameterValue("scrd", scard);

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

                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;
                                case 2:
                                    grp1 = 1009;

                                    ReportHrSubIRS repo3 = new ReportHrSubIRS();

                                    repo3.SetDatabaseLogon("APP", "12369");

                                    repo3.SetParameterValue("srda1", srda1);
                                    repo3.SetParameterValue("srda2", srda2);
                                    repo3.SetParameterValue("comp", comp);
                                    repo3.SetParameterValue("crd", card);
                                    repo3.SetParameterValue("grp1", grp1);
                                    repo3.SetParameterValue("grp2", grp1);
                                    repo3.SetParameterValue("scrd", scard);

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


                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;
                                case 3:
                                    grp1 = 116;
                                    grp2 = 1013;
                                    ReportHrSubIRS repo4 = new ReportHrSubIRS();

                                    repo4.SetDatabaseLogon("APP", "12369");

                                    repo4.SetParameterValue("srda1", srda1);
                                    repo4.SetParameterValue("srda2", srda2);
                                    repo4.SetParameterValue("comp", comp);
                                    repo4.SetParameterValue("crd", card);
                                    repo4.SetParameterValue("grp1", grp1);
                                    repo4.SetParameterValue("grp2", grp2);
                                    repo4.SetParameterValue("scrd", scard);

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


                                    }
                                    else
                                        MessageBox.Show("Thank you");

                                    break;

                                case 4:

                                    ReportHrPrvSubIRS repo5 = new ReportHrPrvSubIRS();

                                    repo5.SetDatabaseLogon("APP", "12369");

                                    repo5.SetParameterValue("srda1", srda1);
                                    repo5.SetParameterValue("srda2", srda2);
                                    repo5.SetParameterValue("comp", comp);
                                    repo5.SetParameterValue("crd", card);
                                    repo5.SetParameterValue("scrd", scard);

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

                                    }
                                    else
                                        MessageBox.Show("Thank you");
                                    break;
                                default:
                                    break;

                            }
                        }
                        catch
                        {
                            MessageBox.Show("Sorry, حدثت مشكلة حاول المحاولة مرة أخرى");
                        }
                    }
                }
                else
                    MessageBox.Show("من فضلك أدخل رقم كارت صحيح");
            }
            else
                MessageBox.Show("أدخل رقم الكارت من فضلك");
        }
      
        private void CompNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {


                    dt1 = dbI.RunReaderds("select distinct CARD_NO from ME_AUB WHERE COMP_ID = '" + CompNumber.Text + "' ORDER BY CARD_NO");
                    CardNumber.ItemsSource = dt1.Tables[0].DefaultView;
         
                

                 if(CompNumber.Text.StartsWith("500") == true || Convert.ToInt32(CompNumber.Text) == 800800)
                {
                 
                    btnPrint.Visibility = Visibility.Visible;
                    btnPrintIRS.Visibility = Visibility.Hidden;
                }
                else
                {
                
                    btnPrint.Visibility = Visibility.Hidden;
                    btnPrintIRS.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
