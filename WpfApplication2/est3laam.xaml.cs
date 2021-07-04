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
    /// Interaction logic for est3laam.xaml
    /// </summary>
    public partial class est3laam : Window
    {
        public est3laam()
        {
            InitializeComponent();

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            CompNum.Text = User.CompanyID;

            if (CompNum.Text == string.Empty)
                CompNum.IsEnabled = true;
            else
                CompNum.IsEnabled = false;


            if (User.Type != "DMS Member")
            {
                btnPrint_Copy1.Visibility = Visibility.Hidden;
                btnPrintNew.Visibility = Visibility.Hidden;
            }
            else
            {
                btnPrint_Copy1.Visibility = Visibility.Visible;
                btnPrintNew.Visibility = Visibility.Visible;
            }

        }
        DB db = new DB();




        private void btnExite_Click(object sender, RoutedEventArgs e)
        {
            Close();

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
            if (CompNum.Text != string.Empty)
            {
                Int64 clno1, clno2, comp, prv1, prv2;
                DateTime sdat1, sdat2, cdat1, cdat2;

                clno1 = ClaimStart.Text == string.Empty ? 0 : Convert.ToInt64(ClaimStart.Text);
                clno2 = ClaimEnd.Text == string.Empty ? 999999999999999999 : Convert.ToInt64(ClaimEnd.Text);
                comp = Convert.ToInt64(CompNum.Text);

                prv1 = ProviderNum.Text == string.Empty ? 0 : Convert.ToInt64(ProviderNum.Text);
                prv2 = ProviderNum.Text == string.Empty ? 9999999999999 : Convert.ToInt64(ProviderNum.Text);

                CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
                ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
                Thread.CurrentThread.CurrentCulture = ci;

                sdat1 = (DateTime)startdateclm.SelectedDate.Value.Date;
                sdat2 = (DateTime)enddateclm.SelectedDate.Value.Date;
                cdat1 = (DateTime)startdatecreat.SelectedDate.Value.Date;
                cdat2 = (DateTime)enddatecreat.SelectedDate.Value.Date;

                View_Report showreport = new View_Report();

                PReportEob repo = new PReportEob();
                // PReportEob___Copy repo = new PReportEob___Copy();
                repo.SetDatabaseLogon("APP", "12369");
                //    repo.SetDatabaseLogon("APP", "12369", "171.0.1.96 /ora11g","APP");
                repo.SetParameterValue("seda1", sdat1);
                repo.SetParameterValue("seda2", sdat2);
                repo.SetParameterValue("crda1", cdat1);
                repo.SetParameterValue("crda2", cdat2);
                repo.SetParameterValue("clno1", clno1);
                repo.SetParameterValue("clno2", clno2);
                repo.SetParameterValue("comp", comp);
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
                    sfd.FileName = "Report";
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
            }
            else
                MessageBox.Show("من فضلك إدخل رقم الشركة");
        }

        private void ClaimStart_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsNumber(e.Key))
                e.Handled = true;
        }

        private void ClaimEnd_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsNumber(e.Key))
                e.Handled = true;
        }

        private void btnPrint_Copy_Click(object sender, RoutedEventArgs e)
        {
            Int64 clno1, clno2, comp, prv1, prv2;
            DateTime sdat1, sdat2, cdat1, cdat2;


            clno1 = ClaimStart.Text == string.Empty ? 0 : Convert.ToInt64(ClaimStart.Text);
            clno2 = ClaimEnd.Text == string.Empty ? 999999999999999999 : Convert.ToInt64(ClaimEnd.Text);
            comp = Convert.ToInt64(CompNum.Text);

            prv1 = ProviderNum.Text == string.Empty ? 0 : Convert.ToInt64(ProviderNum.Text);
            prv2 = ProviderNum.Text == string.Empty ? 9999999999999 : Convert.ToInt64(ProviderNum.Text);

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            sdat1 = (DateTime)startdateclm.SelectedDate.Value.Date;
            sdat2 = (DateTime)enddateclm.SelectedDate.Value.Date;
            cdat1 = (DateTime)startdatecreat.SelectedDate.Value.Date;
            cdat2 = (DateTime)enddatecreat.SelectedDate.Value.Date;

            View_Report showreport = new View_Report();

            PReportEobService repo = new PReportEobService();

            repo.SetDatabaseLogon("APP", "12369");

            repo.SetParameterValue("seda1", sdat1);
            repo.SetParameterValue("seda2", sdat2);
            repo.SetParameterValue("crda1", cdat1);
            repo.SetParameterValue("crda2", cdat2);
            repo.SetParameterValue("clno1", clno1);
            repo.SetParameterValue("clno2", clno2);
            repo.SetParameterValue("comp", comp);
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
                sfd.FileName = "Report";
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
        }

        private void btnPrint_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Int64 clno1, clno2, comp, prv1, prv2;
            DateTime sdat1, sdat2, cdat1, cdat2;


            clno1 = ClaimStart.Text == string.Empty ? 0 : Convert.ToInt64(ClaimStart.Text);
            clno2 = ClaimEnd.Text == string.Empty ? 999999999999999999 : Convert.ToInt64(ClaimEnd.Text);
            comp = Convert.ToInt64(CompNum.Text);

            prv1 = ProviderNum.Text == string.Empty ? 0 : Convert.ToInt64(ProviderNum.Text);
            prv2 = ProviderNum.Text == string.Empty ? 9999999999999 : Convert.ToInt64(ProviderNum.Text);

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;

            sdat1 = (DateTime)startdateclm.SelectedDate.Value.Date;
            sdat2 = (DateTime)enddateclm.SelectedDate.Value.Date;
            cdat1 = (DateTime)startdatecreat.SelectedDate.Value.Date;
            cdat2 = (DateTime)enddatecreat.SelectedDate.Value.Date;

            View_Report showreport = new View_Report();

            ReportEobServiceOut repo = new ReportEobServiceOut();

            repo.SetDatabaseLogon("APP", "12369");

            repo.SetParameterValue("seda1", sdat1);
            repo.SetParameterValue("seda2", sdat2);
            repo.SetParameterValue("crda1", cdat1);
            repo.SetParameterValue("crda2", cdat2);
            repo.SetParameterValue("clno1", clno1);
            repo.SetParameterValue("clno2", clno2);
            repo.SetParameterValue("comp", comp);
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
                sfd.FileName = "Report";
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
        }

        private void btnPrintNew_Click(object sender, RoutedEventArgs e)
        {
            if (ProviderNum.Text != string.Empty)
            {
                Int64 prv, comp;
                DateTime sdat1, sdat2, cdat1, cdat2;


                //  prv1 = ProviderStart.Text == string.Empty ? 0 : Convert.ToInt64(ProviderStart.Text);
                // prv2 = ProviderEnd.Text == string.Empty ? 99999999 : Convert.ToInt64(ProviderEnd.Text);

                comp = Convert.ToInt64(CompNum.Text);
                prv = Convert.ToInt64(ProviderNum.Text);

                CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
                ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
                Thread.CurrentThread.CurrentCulture = ci;

                sdat1 = (DateTime)startdateclm.SelectedDate.Value.Date;
                sdat2 = (DateTime)enddateclm.SelectedDate.Value.Date;
                cdat1 = (DateTime)startdatecreat.SelectedDate.Value.Date;
                cdat2 = (DateTime)enddatecreat.SelectedDate.Value.Date;

                View_Report showreport = new View_Report();

                ReportStatement repo = new ReportStatement();

                repo.SetDatabaseLogon("APP", "12369");

                repo.SetParameterValue("seda1", sdat1);
                repo.SetParameterValue("seda2", sdat2);
                repo.SetParameterValue("crda1", cdat1);
                repo.SetParameterValue("crda2", cdat2);
                repo.SetParameterValue("prv1", prv);
                repo.SetParameterValue("prv2", prv);
                repo.SetParameterValue("comp", comp);

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
                    sfd.FileName = "Report";
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
            }
            else
                MessageBox.Show("من فضلك ادخل رقم مجهز الخدمة");
        }



    }
}
