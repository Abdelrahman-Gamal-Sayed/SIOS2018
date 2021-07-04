using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
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
using WpfApplication2.DataLayer;
using System.Data;
using System.IO;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using iTextSharp.text;
using System.Windows.Controls;
using System.Windows.Threading;
using iTextSharp.text.html;
using WpfApplication2.BusinessLayer.Indemnity;
using WpfApplication2.BusinessLayer.Medical_Agreement;
using WpfApplication2.BusinessLayer.Printing;
using WpfApplication2.BusinessLayer.Notebooks;
using WpfApplication2.BusinessLayer.Messenger_Confirmation;
using WpfApplication2.BusinessLayer.MessengerRequest;
using WpfApplication2.BusinessLayer.Moving_Messenger;
using WpfApplication2.BusinessLayer;
using System.Globalization;
using System.Threading;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;
using System.Data.OracleClient;
using System.Net.Mail;
using System.Web;
using System.Net;
using WpfApplication2.ReportsLayer;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for page3English.xaml
    /// </summary>
    public partial class page3English : System.Windows.Controls.Page
    {
        DB_IRS db_IRS = new DB_IRS();
        private DispatcherTimer timer;
        public static string UserCompany = User.CompanyName;
        public static string card_medicine = "";
        public static string CompanyCode;
        public static string UserType = "";
        Reports report = new Reports();
        Stores store = new Stores();
        agents agent = new agents();
        Contracts contract = new Contracts();
        HRNetwork hrnet = new HRNetwork();
        IndemnityServices ind = new IndemnityServices();
        EmpApprovalServices emp = new EmpApprovalServices();
        PrintingServices printserv = new PrintingServices();
        Clients client = new Clients();
        NoteBookServices note = new NoteBookServices();
        MovingMessServices mov = new MovingMessServices();
        ConfirMessServices conf = new ConfirMessServices();
        MessengerRequestServices req = new MessengerRequestServices();
        MessangerServices mes = new MessangerServices();
        medicine Medicie = new medicine();
        public static int indemnity_id;
        public static string color_1, color_2, color_3, color_4;
        public static List<string> image_paths_provider;
        public static List<string> image_paths_company;
        public static int hour, min, sec;
        public static int hour_end, min_end, sec_end;
        public page3English(string name, string app, string net, string medic, string ind, string rep, string contr, string cheq, string online, string flag, string dept, string basic, string active, string company, string usertype, string print, string store, string note, string cust, string reportss, string complain)
        {

            InitializeComponent();

            //WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            UserType = usertype;
            UserCompany = company;
            CompanyCode = report.get_comp_id(UserCompany);
            User.CompanyID = CompanyCode;
            //  MessageBox.Show(Environment.CurrentDirectory.ToString());
            //abdo
            jjCompanyName = company;
            jjUesrname = name;
            jjUserType = usertype;
            //abdo 2
            jj22CompanyName = company;
            jj22Uesrname = name;
            jj22UserType = usertype;
            //abdo
            //  UserNameLogin = name;
            AbdoUserType = usertype;
            //online , medic , cheq , flag , dept

            if (User.REQUESHR != "y" && User.REQUESHR != "Y")
                requets.Visibility = Visibility.Collapsed;


            //=============================
            if (medic == "y" || medic == "y")
            {
                month22.Visibility = Visibility.Visible;
            }
            else
            {
                month22.Visibility = Visibility.Collapsed;
            }
            if (reportss == "y" || reportss == "Y")
            {
                reportTab.Visibility = Visibility.Visible;
            }
            else
            {
                reportTab.Visibility = Visibility.Collapsed;
            }
            ////////////////////////////////////////
            if (print == "y" || print == "Y")
            {
                printTab.Visibility = Visibility.Visible;
            }
            else
            {
                printTab.Visibility = Visibility.Collapsed;
            }
            ////////////////////////////////////////////
            if (store == "y" || store == "Y")
            {
                storeTab.Visibility = Visibility.Visible;
            }
            else
            {
                storeTab.Visibility = Visibility.Collapsed;
            }
            ///////////////////////////////////////////
            if (usertype == "DMS Member")
            {
                if (flag == "y" || flag == "Y")
                {
                    managerReqTab.Visibility = Visibility.Visible;
                    userReqGroup.Visibility = Visibility.Hidden;
                }
                else
                {
                    managerReqTab.Visibility = Visibility.Hidden;
                    userReqGroup.Visibility = Visibility.Visible;
                }
            }
            else if (usertype == "hr")
            {
                managerReqTab.Visibility = Visibility.Hidden;
                userReqGroup.Visibility = Visibility.Hidden;
                CallsTab.Visibility = Visibility.Collapsed;
                //  MessReqz.Visibility = Visibility.Collapsed;
                //   messConfTabz.Visibility = Visibility.Collapsed;
                //   messConfTabz_Copy.Visibility = Visibility.Collapsed;
                medical_managementTab.Visibility = Visibility.Collapsed;
                imgcust.Source = new BitmapImage(new Uri("media/customer-serviceColor.png", UriKind.RelativeOrAbsolute));
                txtblck.Text = "HR";
                reportTab_Copy.Visibility = Visibility.Visible;
                month22.Visibility = Visibility.Collapsed;
                PROBLEMdms.Visibility = Visibility.Collapsed;
                ReviComp.Visibility = Visibility.Collapsed;
                //PrintSecondTab.Visibility = Visibility.Hidden;
                //ReceivingFrm.Visibility = Visibility.Hidden;
            }
            ////////////////////////////////////////
            if (ind == "y" || ind == "Y")
            {
                IndemnityTab.Visibility = Visibility.Visible;
            }
            else
                IndemnityTab.Visibility = Visibility.Collapsed;
            ///////////////////////////////////////////
            if (note == "y" || note == "Y")
            {
                notebookTab.Visibility = Visibility.Visible;
            }
            else
                notebookTab.Visibility = Visibility.Collapsed;
            /////////////////////////////////////
            if (print == "y" || print == "Y")
            {
                printTab.Visibility = Visibility.Visible;
            }
            else
                printTab.Visibility = Visibility.Collapsed;
            ///////////////////////////////////////////
            if (complain == "y")
            {
                complainsTab.Visibility = Visibility.Visible;
            }
            else
                complainsTab.Visibility = Visibility.Collapsed;
            ///////////////////////////////////////////////
            if (cust == "y")
            {
                customerServiceTab.Visibility = Visibility.Visible;
            }
            else
                customerServiceTab.Visibility = Visibility.Collapsed;
            ///////////////////////////////////////////
            if (contr == "y" || contr == "Y")
            {
                PolicyTab.Visibility = Visibility.Visible;
            }
            else
                PolicyTab.Visibility = Visibility.Collapsed;
            //=================
            if (User.revise == "y")
            {
                ReviTab.Visibility = Visibility.Visible;
            }
            else
            {
                ReviTab.Visibility = Visibility.Collapsed;
            }
            //=====================

            //======================
            if (User.medicalManage == "y")
            {
                medical_managementTab.Visibility = Visibility.Visible;
            }
            else
            {
                medical_managementTab.Visibility = Visibility.Collapsed;
            }
            //=======================
            if (User.complainMember == "y")
            {
                PROBLEMdms.Visibility = Visibility.Visible;
            }
            else
            {
                PROBLEMdms.Visibility = Visibility.Collapsed;
            }
            deptnameHome.Text = dept;

            System.Threading.Thread.CurrentThread.CurrentCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd-MMM-yy";
            this.ShowsNavigationUI = false;
            codelbl.Visibility = Visibility.Hidden;
            codetxt.Visibility = Visibility.Hidden;
            g1.Visibility = Visibility.Hidden;
            txtResons.Visibility = Visibility.Hidden;
            NameTab.Header = name;
            if (app == "y" || app == "Y")
            {
                ApprovalsTab.Visibility = Visibility;
                if (UserType != "hr")
                {
                    approvalCompLbl.Visibility = Visibility.Visible;
                    approvalCompanySrchBtn.Visibility = Visibility.Visible;
                    approvalCompCombo.Visibility = Visibility.Visible;
                }
                else if (UserType == "hr")
                {
                    approvalCompLbl.Visibility = Visibility.Hidden;
                    approvalCompanySrchBtn.Visibility = Visibility.Hidden;
                    approvalCompCombo.Visibility = Visibility.Hidden;
                }
            }
            else
                ApprovalsTab.Visibility = Visibility.Collapsed;

            if (basic == "y" || basic == "Y")
            {
                basicDataTab.Visibility = Visibility.Visible;
            }
            else
                basicDataTab.Visibility = Visibility.Collapsed;
            if (rep == "y" || rep == "Y")
            {
                MessTab.Visibility = Visibility.Visible;
            }
            else
                MessTab.Visibility = Visibility.Collapsed;
            if (net == "y" || net == "Y")
            {
                NetworkTab.Visibility = Visibility.Visible;
                if ((net == "y" || net == "Y") && cust != "y")
                {
                    customerServiceTab.Visibility = Visibility.Visible;
                    NetworkTab.Visibility = Visibility.Visible;
                    CallsTab.Visibility = Visibility.Collapsed;
                    InfoCardTab.Visibility = Visibility.Collapsed;
                    summaryTabast.Visibility = Visibility.Collapsed;
                    ApprovalsTabz.Visibility = Visibility.Collapsed;
                    MessReqz.Visibility = Visibility.Collapsed;
                    messConfTabz.Visibility = Visibility.Collapsed;
                    messConfTabئz.Visibility = Visibility.Collapsed;
                    month.Visibility = Visibility.Collapsed;
                    problems.Visibility = Visibility.Collapsed;

                    reportTab_Copy.Visibility = Visibility.Collapsed;
                    month22.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (usertype == "hr")
                {
                    NetworkTab.Visibility = Visibility.Visible;
                }
                else
                {
                    NetworkTab.Visibility = Visibility.Collapsed;
                }

                if ((net == "y" || net == "Y") && cust != "y")
                {
                    customerServiceTab.Visibility = Visibility.Visible;
                    NetworkTab.Visibility = Visibility.Visible;
                    CallsTab.Visibility = Visibility.Collapsed;
                    InfoCardTab.Visibility = Visibility.Collapsed;
                    summaryTabast.Visibility = Visibility.Collapsed;
                    ApprovalsTabz.Visibility = Visibility.Collapsed;
                    MessReqz.Visibility = Visibility.Collapsed;
                    messConfTabz.Visibility = Visibility.Collapsed;
                    messConfTabئz.Visibility = Visibility.Collapsed;
                    month.Visibility = Visibility.Collapsed;
                    problems.Visibility = Visibility.Collapsed;

                    reportTab_Copy.Visibility = Visibility.Collapsed;
                    month22.Visibility = Visibility.Collapsed;
                    ReviTab.Visibility = Visibility.Collapsed;
                }
            }
        }


        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {


        }

        #region printing to excel,pdf

        private void exportToPDF(DataGrid dg, string name)
        {

            PdfPTable table = new PdfPTable(dg.Columns.Count);
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream("D:\\Report" + name + ".pdf", System.IO.FileMode.Create));
            doc.Open();
            for (int j = 0; j < dg.Columns.Count; j++)
            {
                table.AddCell(new Phrase(dg.Columns[j].Header.ToString()));
            }
            table.HeaderRows = 1;
            IEnumerable itemsSource = dg.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    DataGridRow row = dg.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (row != null)
                    {
                        System.Windows.Controls.Primitives.DataGridCellsPresenter presenter = FindVisualChild<System.Windows.Controls.Primitives.DataGridCellsPresenter>(row);
                        for (int i = 0; i < dg.Columns.Count; ++i)
                        {
                            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                            TextBlock txt = cell.Content as TextBlock;
                            if (txt != null)
                            {
                                table.AddCell(new Phrase(txt.Text));
                            }
                        }
                    }
                }

                doc.Add(table);
                MessageBox.Show("File path -->D:\\Report" + name + ".pdf");
                doc.Close();
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
        where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public static childItem FindVisualChild<childItem>(DependencyObject obj)
        where childItem : DependencyObject
        {
            foreach (childItem child in FindVisualChildren<childItem>(obj))
            {
                return child;
            }

            return null;
        }
        #endregion

        #region basicData

        private void newDeptBtn_Click(object sender, RoutedEventArgs e)
        {
            add_dept(deptname);
        }

        private void editDeptBtn_Click(object sender, RoutedEventArgs e)
        {
            if (deptgrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("من فضلك اختر قسم ");
            }
            if (deptname == null)
            {
                MessageBox.Show("ادخل اسم قسم صحيح");
            }
            else
            {
                for (int i = 0; i < deptgrid.SelectedItems.Count; i++)
                {
                    System.Data.DataRowView dr = (System.Data.DataRowView)deptgrid.SelectedItems[0];
                    int code = Convert.ToInt32(dr.Row.ItemArray[0].ToString());
                    agent.update_dept(deptname, code);
                    MessageBox.Show("تم تعديل البيانات بنجاح");
                }
                System.Data.DataTable deptDT = agent.get_code_dept();
                deptgrid.ItemsSource = deptDT.DefaultView;
                deptgrid.Columns[0].IsReadOnly = true;
                try
                {
                    deptgrid.Columns[0].Header = "كود القسم";
                    deptgrid.Columns[1].Header = "اسم القسم";
                }
                catch { }

                deptgrid.Items.Refresh();
            }
        }

        private void delDeptBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("هل انت متأكد ؟", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (deptgrid.SelectedItems.Count == 0)
                {
                    MessageBox.Show("من فضلك اختر قسم");
                }
                else
                {
                    for (int i = 0; i < deptgrid.SelectedItems.Count; i++)
                    {
                        System.Data.DataRowView dr = (System.Data.DataRowView)deptgrid.SelectedItems[i];
                        int code = Convert.ToInt32(dr.Row.ItemArray[0].ToString());
                        agent.del_dept(code);

                    }
                    MessageBox.Show("تم مسح البيانات بنجاح");
                    System.Data.DataTable deptDT = agent.get_code_dept();
                    deptgrid.ItemsSource = deptDT.DefaultView;
                    deptgrid.Columns[0].IsReadOnly = true;
                    try
                    {
                        deptgrid.Columns[0].Header = "كود القسم";
                        deptgrid.Columns[1].Header = "اسم القسم";
                    }
                    catch { }

                    deptgrid.Items.Refresh();
                }
            }
            else
            {

            }

        }

        private void basicdataDemptCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string dept = basicdataDemptCombo.SelectedItem.ToString();
                basicdataEmpCombo.Items.Clear();
                System.Data.DataTable data = agent.get_employees(dept);
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات");
                }
                else
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        basicdataEmpCombo.Items.Add(data.Rows[i].ItemArray[3].ToString() + " " + data.Rows[i].ItemArray[0].ToString());
                    }
                }
            }
            catch { }
        }

        private void basicdataEmpCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string selected = basicdataEmpCombo.SelectedItem.ToString();
                string[] arr = selected.Split(' ');
                string name = arr[1];
                string code = arr[0];
                basicdataGroup.Header = name;
                empnametxt.Text = name;
                empcodetxt.Text = code;
                string password = agent.get_pasword(name);
                emppasstxt.Text = password;
                newDeptCombo.Text = basicdataDemptCombo.SelectedItem.ToString();
                string net = "";
                string app = "";
                string medic = "";
                string indem = "";
                string rep = "";
                string cheq = "";
                string online = "";
                string contr = "";
                string flag = "";
                string dept = "";
                string basic = "";
                string active = "";
                string company = "";
                string usertype = ""; string prints = "";
                string notes = ""; string cust = ""; string complains = "";
                string reports = ""; string stores = "";
                string complaindms = ""; string medicalmanage = ""; string hrreq = "";
                System.Data.DataTable user = agent.get_employee_authority(name);
                try
                {
                    for (int i = 0; i < user.Rows.Count; i++)
                    {
                        net = user.Rows[i].ItemArray[4].ToString();
                        app = user.Rows[i].ItemArray[1].ToString();
                        cheq = user.Rows[i].ItemArray[2].ToString();
                        indem = user.Rows[i].ItemArray[3].ToString();
                        rep = user.Rows[i].ItemArray[5].ToString();
                        online = user.Rows[i].ItemArray[6].ToString();
                        medic = user.Rows[i].ItemArray[7].ToString();
                        contr = user.Rows[i].ItemArray[8].ToString();
                        dept = user.Rows[i].ItemArray[9].ToString();
                        flag = user.Rows[i].ItemArray[10].ToString();
                        basic = user.Rows[i].ItemArray[13].ToString();
                        active = user.Rows[i].ItemArray[14].ToString();
                        company = user.Rows[i].ItemArray[15].ToString();
                        usertype = user.Rows[i].ItemArray[16].ToString();
                        prints = user.Rows[i].ItemArray[17].ToString();
                        notes = user.Rows[i].ItemArray[18].ToString();
                        stores = user.Rows[i].ItemArray[19].ToString();
                        cust = user.Rows[i].ItemArray[20].ToString();
                        reports = user.Rows[i].ItemArray[21].ToString();
                        complains = user.Rows[i].ItemArray[22].ToString();
                        medicalmanage = user.Rows[i].ItemArray[24].ToString();
                        complaindms = user.Rows[i].ItemArray[25].ToString();
                        hrreq = user.Rows[i].ItemArray[26].ToString();
                    }
                }
                catch { }

                if (hrreq == "y")
                {
                    hr_reqChk.IsChecked = true;
                }
                else
                {
                    hr_reqChk.IsChecked = false;
                }
                //=====================
                if (medicalmanage == "y")
                {
                    medical_manageChk.IsChecked = true;
                }
                else
                {
                    medical_manageChk.IsChecked = false;
                }
                //====================
                if (complaindms == "y")
                {
                    complain_dmsChk.IsChecked = true;
                }
                else
                {
                    complain_dmsChk.IsChecked = false;
                }
                //========================
                if (active == "y" || active == "Y")
                {
                    activeChk.IsChecked = true;
                }
                else
                    activeChk.IsChecked = false;

                if (flag == "y" || flag == "Y")
                {
                    empyesrb.IsChecked = true;
                }
                else if (flag == "N" || flag == "n")
                {
                    empnorb.IsChecked = true;
                }
                ////////
                if (cheq == "y" || cheq == "Y")
                {
                    chequesChk.IsChecked = true;
                }
                else if (cheq == "n" || cheq == "N")
                {
                    chequesChk.IsChecked = false;
                }
                //////////
                if (online == "y" || online == "Y")
                {
                    onlineChk.IsChecked = true;
                }
                else if (online == "N" || online == "n")
                {
                    onlineChk.IsChecked = false;
                }
                ///////////////////
                if (basic == "y" || basic == "Y")
                {
                    bscDtaChk.IsChecked = true;
                }
                else if (basic == "N" || basic == "n")
                {
                    bscDtaChk.IsChecked = false;
                }
                ///////////////////
                if (net == "y" || net == "Y")
                {
                    networkChk.IsChecked = true;
                }
                else if (net == "N" || net == "n")
                {
                    networkChk.IsChecked = false;
                }
                ///////////////////
                if (contr == "y" || contr == "Y")
                {
                    contractChk.IsChecked = true;
                }
                else if (contr == "N" || contr == "n")
                {
                    contractChk.IsChecked = false;
                }
                ///////////////////
                if (medic == "y" || medic == "Y")
                {
                    medicineChk.IsChecked = true;
                }
                else if (medic == "N" || medic == "n")
                {
                    medicineChk.IsChecked = false;
                }
                ///////////////////
                if (indem == "y" || indem == "Y")
                {
                    indemnityChk.IsChecked = true;
                }
                else if (indem == "N" || indem == "n")
                {
                    indemnityChk.IsChecked = false;
                }
                ///////////////////
                if (app == "y" || app == "Y")
                {
                    approvalChk.IsChecked = true;
                }
                else if (app == "N" || app == "n")
                {
                    approvalChk.IsChecked = false;
                }
                ///////////////////
                if (rep == "y" || rep == "Y")
                {
                    messangerChk.IsChecked = true;
                }
                else if (rep == "N" || rep == "n")
                {
                    messangerChk.IsChecked = false;
                }
                ///////////////////
                if (usertype == "hr")
                {
                    hrtyperb.IsChecked = true;
                }
                else if (usertype == "DMS Member")
                {
                    dmstyperb.IsChecked = true;
                }
                ///////////////////////////
                if (prints == "y")
                {
                    printChk.IsChecked = true;
                }
                else
                    printChk.IsChecked = false;
                ////////////////////////
                if (notes == "y")
                {
                    notebookChk.IsChecked = true;
                }
                else
                    notebookChk.IsChecked = false;
                /////////////////////////////
                if (reports == "y")
                {
                    reportChk.IsChecked = true;
                }
                else
                    reportChk.IsChecked = false;
                //////////////////////
                if (stores == "y")
                {
                    storeChk.IsChecked = true;
                }
                else
                    storeChk.IsChecked = false;
                ///////////////////////
                if (cust == "y")
                {
                    custChk.IsChecked = true;
                }
                else
                    custChk.IsChecked = false;
                ///////////////////////////
                if (complains == "y")
                {
                    complainChk.IsChecked = true;
                }
                else
                    complainChk.IsChecked = false;
            }
            catch { }
        }

        //13 nov
        private void basicDataEditEmpBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = empnametxt.Text.ToString();
            int code = Convert.ToInt32(empcodetxt.Text.ToString());
            string dept = newDeptCombo.Text.ToString();
            string password = emppasstxt.Text.ToString();
            string companyname = "";
            string mail = empcodetxt_Copy.Text;
            if (empCompCombo.Text == "")
            {
                companyname = "";
            }
            else
            {
                companyname = empCompCombo.Text.ToString();
            }
            string manager = "";
            string online = "";
            string cheque = "";
            string approve = "";
            string net = "";
            string individual = "";
            string basicdata = "";
            string messanger = "";
            string medicine = "";
            string contract = "";
            string active = "";
            string type = "";
            string prints = "";
            string notes = "";
            string reports = "";
            string stores = "";
            string complains = "";
            string customerserv = "";
            string medicalmanage = ""; string hrreq = ""; string complaindms = ""; string revise = "";
            string REQUESHR = "";
            string NotiHr = "";
            if (rev_chk_Copy.IsChecked == true)
                REQUESHR = "y";
            else
                REQUESHR = "n";

            if (notiHrRequest1.IsChecked == true)
                NotiHr = "Y";
            else
                NotiHr = "n";

            if (activeChk.IsChecked == true)
            {
                active = "y";
            }
            else
                active = "n";
            ///////////////////////////////////////
            if (empyesrb.IsChecked == true)
            {
                manager = "y";
            }
            else
                manager = "n";
            if (empnorb.IsChecked == true)
            {
                manager = "n";
            }
            else
                manager = "y";
            ///////////////////
            if (onlineChk.IsChecked == true)
            {
                online = "y";
            }
            else
                online = "n";
            //////////////////
            if (chequesChk.IsChecked == true)
            {
                cheque = "y";
            }
            else
                cheque = "n";
            ///////////////////////
            if (approvalChk.IsChecked == true)
            {
                approve = "y";
            }
            else
                approve = "n";
            ///////////////////////
            if (networkChk.IsChecked == true)
            {
                net = "y";
            }
            else
                net = "n";
            ///////////////////////
            if (indemnityChk.IsChecked == true)
            {
                individual = "y";
            }
            else
                individual = "n";
            ///////////////////////
            if (contractChk.IsChecked == true)
            {
                contract = "y";
            }
            else
                contract = "n";
            ///////////////////////
            if (medicineChk.IsChecked == true)
            {
                medicine = "y";
            }
            else
                medicine = "n";
            ///////////////////////
            if (messangerChk.IsChecked == true)
            {
                messanger = "y";
            }
            else
                messanger = "n";
            ///////////////////////
            if (bscDtaChk.IsChecked == true)
            {
                basicdata = "y";
            }
            else
                basicdata = "n";
            ///////////////////////
            if (custChk.IsChecked == true)
            {
                customerserv = "y";
            }
            else
                customerserv = "n";
            ////////////////////////////////
            if (printChk.IsChecked == true)
            {
                prints = "y";
            }
            else
                prints = "n";
            ////////////////////////
            if (notebookChk.IsChecked == true)
            {
                notes = "y";
            }
            else
                notes = "n";
            ////////////////////////////
            if (storeChk.IsChecked == true)
            {
                stores = "y";
            }
            else
                stores = "n";
            //////////////////////////
            if (reportChk.IsChecked == true)
            {
                reports = "y";
            }
            else
                reports = "n";
            //////////////////////////////
            if (complainChk.IsChecked == true)
            {
                complains = "y";
            }
            else
            {
                complains = "n";
            }
            //////////////////////
            if (hrtyperb.IsChecked == true)
            {
                type = "hr";
            }
            if (dmstyperb.IsChecked == true)
            {
                type = "DMS Member";
            }
            //[===================
            if (medical_manageChk.IsChecked == true)
            {
                medicalmanage = "y";
            }
            else
            {
                medicalmanage = "n";
            }
            //==================
            if (hr_reqChk.IsChecked == true)
            {
                hrreq = "y";
            }
            else
            {
                hrreq = "n";
            }
            //===================
            if (complain_dmsChk.IsChecked == true)
            {
                complaindms = "y";
            }
            else
            {
                complaindms = "n";
            }
            //===========
            if (rev_chk.IsChecked == true)
            {
                revise = "y";
            }
            else
                revise = "n";
            agent.update_user(name, password, approve, cheque, net, messanger, online, medicine, contract, individual, basicdata, dept, manager, code, companyname, active, type, prints, notes, stores, customerserv, reports, complains, medicalmanage, hrreq, complaindms, revise, mail, REQUESHR, NotiHr);
            MessageBox.Show("تم تعديل البيانات بنجاح");
        }
        #endregion

        #region reports
        //public System.Data.DataTable get_report13(int claimnum, int current, int active, float sum, int compidF, int compidT, string claimDateFrom, string claimDateTo, string creatDateFrom, string createDateTo)
        //{
        //    claimnum = report.get_claim_num(compidF,compidT, claimDateFrom, claimDateTo, creatDateFrom, createDateTo);
        //    current = report.get_current_users(compidF, compidT, claimDateFrom, claimDateTo, creatDateFrom, createDateTo);
        //    active = report.get_active_users(compidF,compidT);
        //    sum = report.get_claim_net(compidF,compidT, claimDateFrom, claimDateTo, creatDateFrom, createDateTo);
        //    float avg1 = sum / active;
        //    float avg = avg1 / active;
        //    float curr_avg1 = claimnum / current;
        //    float curr_avg = curr_avg1 / current;
        //    System.Data.DataTable result = new System.Data.DataTable();
        //    result.Columns.Add("عدد الموظفين المتعاقدين", typeof(int));
        //    result.Columns.Add("عدد الموظفين المستخدمين للكروت", typeof(int));
        //    result.Columns.Add("عدد المطالبات", typeof(int));
        //    result.Columns.Add("اجمالي قيمة المطالبات", typeof(float));
        //    result.Columns.Add("متوسط المطالبات للفرد", typeof(float));
        //    result.Columns.Add("متوسط قيمة المطالبات للفرد", typeof(float));
        //    result.Columns.Add("متوسط المطالبات الفعلي", typeof(float));
        //    result.Columns.Add("متوسط قيمة المطالبات الفعلي", typeof(float));
        //    result.Rows.Add(active, current, claimnum, sum, avg1, avg, curr_avg1, curr_avg);
        //    return result;
        //}



        public void load_reports()
        {
            System.Data.DataTable DeptDT = agent.get_all_dept();
            System.Data.DataTable CategoryDT = store.get_category();
            employeeCareDeptCombo.Items.Clear();
            DeptFilterCombo.Items.Clear();
            destroyFilterDeptCombo.Items.Clear();
            empFilterDeptCombo.Items.Clear();
            categoryFilterCategoryCombo.Items.Clear();
            ItemFilterCategoryCombo.Items.Clear();
            for (int i = 0; i < DeptDT.Rows.Count; i++)
            {
                employeeCareDeptCombo.Items.Add(DeptDT.Rows[i].ItemArray[0].ToString());
                DeptFilterCombo.Items.Add(DeptDT.Rows[i].ItemArray[0].ToString());
                destroyFilterDeptCombo.Items.Add(DeptDT.Rows[i].ItemArray[0].ToString());
                empFilterDeptCombo.Items.Add(DeptDT.Rows[i].ItemArray[0].ToString());
            }
            for (int i = 0; i < CategoryDT.Rows.Count; i++)
            {
                categoryFilterCategoryCombo.Items.Add(CategoryDT.Rows[i].ItemArray[0].ToString());
                ItemFilterCategoryCombo.Items.Add(CategoryDT.Rows[i].ItemArray[0].ToString());
            }

        }

        /// <store reports>
        /// //////////////////////////
        /// </store reports>
        private void destroyFilterRB_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Data.DataTable data = store.filter_destory();
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show(" لا توجد بيانات");
                }
                else
                {
                    filterGrid.ItemsSource = data.DefaultView;
                }
                typeFilterCounttxt.Content = "Items count : " + data.Rows.Count.ToString();
            }
            catch { }
        }

        private void exportFilterRB_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Data.DataTable data = store.filter_export();
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show(" لا توجد بيانات");
                }
                else
                {
                    filterGrid.ItemsSource = data.DefaultView;
                }
                typeFilterCounttxt.Content = "Items count : " + data.Rows.Count.ToString();
            }
            catch { }
        }

        private void importFilterRB_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Data.DataTable data = store.filter_import();
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show(" لا توجد بيانات");
                }
                else
                {
                    filterGrid.ItemsSource = data.DefaultView;
                }
                typeFilterCounttxt.Content = "Items count : " + data.Rows.Count.ToString();
            }
            catch { }
        }

        private void returnFilterRB_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Data.DataTable data = store.filter_return();
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show(" لا توجد بيانات");
                }
                else
                {
                    filterGrid.ItemsSource = data.DefaultView;
                }
                typeFilterCounttxt.Content = "Items count : " + data.Rows.Count.ToString();
            }
            catch { }
        }
        public void load_store_filter()
        {
            try
            {
                System.Data.DataTable data = store.filter_store();
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show(" لا توجد بيانات");
                }
                else
                {
                    storeFilter.ItemsSource = data.DefaultView;
                }
                storeFilterItemCounttxt.Content = "Items Count : " + data.Rows.Count.ToString();
            }
            catch { }
        }
        #endregion

        #region store
        //public void load_category()
        //{
        //    System.Data.DataTable category = store.get_category();
        //    if (category.Rows.Count == 0)
        //    {
        //        MessageBox.Show("لا توجد بيانات");
        //    }
        //    else
        //    {
        //        //categorycombo.Items.Clear();
        //        categoryCombo.Items.Clear();
        //        for (int i = 0; i < category.Rows.Count; i++)
        //        {
        //            categoryCombo.Items.Add(category.Rows[i].ItemArray[0].ToString());
        //            //categorycombo.Items.Add(category.Rows[i].ItemArray[0].ToString());
        //        }
        //    }
        //}

        public void register_category_load()
        {
            try
            {
                System.Data.DataTable dt = store.get_category();
                GridCategory.ItemsSource = dt.DefaultView;
                //GridCategory.Columns[0].Header = "اسم الفئة";
            }
            catch { }
        }

        public void store_item_load()
        {
            try
            {
                System.Data.DataTable category = store.get_category();
                if (category.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات");
                }
                else
                {
                    categorycombo.Items.Clear();
                    for (int i = 0; i < category.Rows.Count; i++)
                    {
                        categorycombo.Items.Add(category.Rows[i].ItemArray[0].ToString());
                    }
                }
            }
            catch { }

        }

        public void return_item_load()
        {
            try
            {
                //System.Data.DataTable deptData = agent.get_all_dept();
                //deptCombo.Items.Clear();
                //for (int i = 0; i < deptData.Rows.Count; i++)
                //{
                //    deptCombo.Items.Add(deptData.Rows[i].ItemArray[0].ToString());
                //}

                deptCombo.ItemsSource = db.RunReader("select distinct DEPT_CODE ,DEPT_NAME from agent_department order by DEPT_NAME").Result.DefaultView;
                System.Data.DataTable categoryData = store.get_category();
                categCombo.Items.Clear();
                for (int i = 0; i < categoryData.Rows.Count; i++)
                {
                    categCombo.Items.Add(categoryData.Rows[i].ItemArray[0].ToString());
                }
            }
            catch { }
        }

        //new final
        public void export_load()
        {
            try
            {
                System.Data.DataTable categDT = store.get_category();
                exportcategCombo.Items.Clear();
                for (int i = 0; i < categDT.Rows.Count; i++)
                {
                    exportcategCombo.Items.Add(categDT.Rows[i].ItemArray[0].ToString());
                }
            }
            catch { }
        }

        //private void categoryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    string category = categoryCombo.SelectedItem.ToString(); //try
        //    itemCombo.Items.Clear();
        //    System.Data.DataTable items = store.get_item_names_from_code(category);
        //    if (items.Rows.Count == 0)
        //    {
        //        MessageBox.Show("لا توجد بيانات");
        //    }
        //    else
        //    {

        //        for (int i = 0; i < items.Rows.Count; i++)
        //        {
        //            itemCombo.Items.Add(items.Rows[i].ItemArray[0].ToString());
        //        }
        //    }
        //}

        private void itemCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //public void load_data_amount()
        //{
        //    System.Data.DataTable data = store.get_item_name_cat_amount();
        //    amountGrid.ItemsSource = data.DefaultView;
        //}

        //private void amounttxt_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        string itemname = itemCombo.SelectedItem.ToString();
        //        string category = categoryCombo.SelectedItem.ToString();
        //        string amount = amounttxt.Text.ToString();
        //        string amount1 = store.get_amount(itemname);
        //        int diff = int.Parse(amount) + int.Parse(amount1);

        //        if (itemname == "" || category == "" || amount == null)
        //        {
        //            MessageBox.Show("ادخل كل البيانات");
        //        }
        //        else
        //        {
        //            //dal.add_item(itemname, category, amount);
        //            store.update_amount_item(diff.ToString(), itemname, category);
        //            MessageBox.Show("تم ادخال البيانات بنجاح");
        //            load_data_amount();
        //        }
        //    }
        //}

        private void limtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (categorycombo.SelectedItem == null || categorycombo.SelectedItem.ToString() == "")
                    {
                        MessageBox.Show("من فضلك اختر فئة");
                    }
                    else
                    {
                        string category = categorycombo.SelectedItem.ToString();
                        string item = itemname.Text.ToString();
                        Int64 code = store.get_categ_code(category);
                        Int64 itemcode = store.get_item_serial(code);
                        string itemCodeStr = itemcode.ToString();
                        string codeStr = code.ToString();
                        string itemCodeNew = codeStr + itemCodeStr;
                        Int64 itemCodeInt = Convert.ToInt32(itemCodeNew);
                        string amount = amttxt.Text.ToString();
                        string price = prictxt.Text.ToString();
                        string lim = limtxt.Text.ToString();
                        store.add_item(itemCodeInt, item, code, amount, lim, price);
                        itemGrid.Items.Refresh();
                        itemGrid.ItemsSource = store.get_items().DefaultView;
                        itemGrid.Columns[0].Header = "كود القطعة";
                        itemGrid.Columns[1].Header = "اسم القطعة";
                        itemGrid.Columns[2].Header = "الكمية";
                        itemGrid.Columns[3].Header = "حد الطلب";
                        itemGrid.Columns[4].Header = "سعر القطعة";
                        itemGrid.Columns[5].Header = "الفئة";
                        //categoryCombo.Items.Refresh();
                    }
                }
                catch { }
            }
        }

        private void storeTab_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void cat_txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    string cat = cat_txt.Text.ToString();
                    store.add_category(cat);
                    MessageBox.Show("تم اضافة الفئة : " + cat);
                    register_category_load();
                    storeNewCategoryItemCountxt.Content = GridCategory.Items.Count - 1;
                    GridCategory.Columns[0].Header = "اسم الفئة";
                    GridCategory.Columns[1].Header = "كود الفئة";
                    //load_category();
                    store_item_load();
                }
                catch { }
            }
        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void storeAmountTab_Loaded(object sender, RoutedEventArgs e)
        {
            //load_category();
        }

        private void storeItemTab_Loaded(object sender, RoutedEventArgs e)
        {
            //store_item_load();
        }
        public static string deptname;
        private void deptgrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            deptname = ((System.Windows.Controls.TextBox)e.EditingElement).Text.ToString();

        }

        public void add_dept(string deptname)
        {
            try
            {
                if (deptname == null)
                {
                    MessageBox.Show("من فضلك ادخل اسم قسم صحيح");
                }
                else
                {
                    agent.add_dept(deptname);
                    MessageBox.Show("تم ادخال القسم بنجاح");
                    int code = agent.get_dept_code(deptname);
                    System.Data.DataTable deptDT = agent.get_code_dept();
                    //  NewEmpDeptCombo.Items.Clear();
                    basicdataDemptCombo.Items.Clear();
                    for (int i = 0; i < deptDT.Rows.Count; i++)
                    {
                        // NewEmpDeptCombo.Items.Add(deptDT.Rows[i].ItemArray[0].ToString());
                        basicdataDemptCombo.Items.Add(deptDT.Rows[i].ItemArray[0].ToString());
                    }
                    deptgrid.ItemsSource = deptDT.DefaultView;
                    deptgrid.Columns[0].Header = "كود القسم";
                    deptgrid.Columns[1].Header = "اسم القسم";
                    deptgrid.Columns[0].IsReadOnly = true;
                    deptgrid.Items.Refresh();
                }
            }
            catch { }
        }
        private void newempTab_Loaded(object sender, RoutedEventArgs e)
        {
            //if (newempTab.IsSelected == true)
            //{
            //    NewEmpDeptCombo.Items.Clear();
            //    System.Data.DataTable DeptDt = agent.get_all_dept();
            //    for (int i = 0; i < DeptDt.Rows.Count; i++)
            //    {
            //        NewEmpDeptCombo.Items.Add(DeptDt.Rows[i].ItemArray[0].ToString());
            //    }
            //}
        }


        //torb2-5 zwdt paremters comp_id
        private void saveEmpBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (hrtype.IsChecked == true && basicDataComp.Text == null)
                {

                    MessageBox.Show("يرجي اختيار الشركة ");
                }
                else
                {
                    string name = nametxt.Text.ToString();
                    string password = passtxt.Text.ToString();
                    string dept = NewEmpDeptCombo.Text;
                    string app_inq;
                    string operation;
                    string contracts_medical;
                    string cheq_inq;
                    string net_inq;
                    string rep_inq;
                    string online_inq;
                    string medical_inq;
                    string medical_manag;
                    string complainMember;
                    string hr_req;
                    string contract_inq;
                    string indcheck;
                    string manager = "";
                    string basic;
                    string compname = "";
                    string active = "";
                    string stores = "";
                    string reports = "";
                    string notebook = "";
                    string prints = "";
                    string complains = "";
                    string usertype = "";
                    string customer = "";
                    string revise = "";
                    string mail = "";
                    string requesHR = "";
                    string tele_sales = "";
                    string noti_hr = "";
                    string recollect = "";
                    string afteer_sales = "";
                    string high_dep = "";
                    string name_ar = name_arTxt.Text.ToString();
                    string name_en = name_enTxt.Text.ToString();
                    if (chkRev_Copy3.IsChecked == true)
                        afteer_sales = "Y";
                    else
                        afteer_sales = "N";
                    if (chkRev_Copy4.IsChecked == true)
                        high_dep = "Y";
                    else
                        high_dep = "N";
                    ////////////////////////
                    if (notiHrRequest.IsChecked == true)
                        noti_hr = "Y";
                    else
                        noti_hr = "N";
                    if (cbrequets.IsChecked == true)
                        requesHR = "y";
                    else
                        requesHR = "N";

                    if (chkRev.IsChecked == true)
                    {
                        revise = "y";
                    }
                    else
                    {
                        revise = "n";
                    }
                    //==================
                    if (chkHrReq.IsChecked == true)
                    {
                        hr_req = "y";
                    }
                    else
                    {
                        hr_req = "n";
                    }
                    //====================
                    if (chkMedicalManage.IsChecked == true)
                    {
                        medical_manag = "y";
                    }
                    else
                    {
                        medical_manag = "n";
                    }
                    //========================
                    if (chkComplaindms.IsChecked == true)
                    {
                        complainMember = "y";
                    }
                    else
                    {
                        complainMember = "n";
                    }
                    //=========================
                    if (basicDataComp.Text == "")
                    {
                        compname = "";
                    }
                    else
                    {
                        compname = basicDataComp.Text;
                    }
                    //=======================
                    if (chkCustomer.IsChecked == true)
                    {
                        customer = "y";
                    }
                    else
                    {
                        customer = "n";
                    }
                    /////////////////////////


                    if (chkRev_Copy.IsChecked == true)
                    {
                        operation = "y";
                    }
                    else
                    {
                        operation = "n";
                    }
                    //////////////////////////
                    if (chkRev_Copy1.IsChecked == true)
                    {
                        contracts_medical = "y";
                    }
                    else
                    {
                        contracts_medical = "n";
                    }
                    //======================
                    if (chkReport.IsChecked == true)
                    {
                        reports = "y";
                    }
                    else
                    {
                        reports = "n";
                    }
                    /////////////////////////////
                    if (hrtype.IsChecked == true)
                    {
                        usertype = "hr";
                    }
                    else if (dmsMembertype.IsChecked == true)
                    {
                        usertype = "DMS Member";
                    }
                    ///////////////////////////////
                    if (chkStore.IsChecked == true)
                    {
                        stores = "y";
                    }
                    else
                    {
                        stores = "n";
                    }
                    /////////////////////////////////
                    if (chkPrint.IsChecked == true)
                    {
                        prints = "y";
                    }
                    else
                    {
                        prints = "n";
                    }
                    //////////////////////////////
                    if (chkNote.IsChecked == true)
                    {
                        notebook = "y";
                    }
                    else
                    {
                        notebook = "n";
                    }
                    //////////////////////////////
                    if (chkComplain.IsChecked == true)
                    {
                        complains = "y";
                    }
                    else
                    {
                        complains = "n";
                    }
                    if (activeChk1.IsChecked == true)
                    {
                        active = "y";
                    }
                    else if (activeChk1.IsChecked == false)
                    {
                        active = "n";
                    }
                    if (netchk.IsChecked == true)
                    {
                        net_inq = "y";
                    }
                    else
                        net_inq = "n";
                    if (basicdataCheck.IsChecked == true)
                    {
                        basic = "y";
                    }
                    else
                        basic = "n";

                    if (chequechk.IsChecked == true)
                    {
                        cheq_inq = "y";
                    }
                    else
                        cheq_inq = "n";

                    if (appchk.IsChecked == true)
                    {
                        app_inq = "y";
                    }
                    else
                        app_inq = "n";
                    if (indemchk.IsChecked == true)
                    {
                        indcheck = "y";
                    }
                    else
                        indcheck = "n";
                    if (contrchk.IsChecked == true)
                    {
                        contract_inq = "y";
                    }
                    else
                        contract_inq = "n";
                    //////////////////////////////
                    if (monthmedchk.IsChecked == true)
                    {
                        medical_inq = "y";
                    }
                    else
                        medical_inq = "n";
                    ///////////////////////////
                    if (onlinesyschk.IsChecked == true)
                    {
                        online_inq = "y";
                    }
                    else
                        online_inq = "n";
                    ////////////////
                    if (repchk.IsChecked == true)
                    {
                        rep_inq = "y";
                    }
                    else
                        rep_inq = "n";
                    ////////////////
                    if (chkRev_Copy2.IsChecked == true)
                    {
                        recollect = "Y";
                    }
                    else if (chkRev_Copy2.IsChecked == true)
                    {
                        recollect = "N";
                    }
                    //////////////
                    if (yesrb.IsChecked == true)
                    {
                        manager = "y";
                    }
                    else if (norb.IsChecked == true)
                    {
                        manager = "n";
                    }
                    if (name == "" || password == "" || dept == "" || manager == "" || active == "")
                    {
                        MessageBox.Show("من فضلك ادخل كل البيانات");
                    }
                    else
                    {
                        string codz = null;
                        if (hrtype.IsChecked == true)
                        {
                            codz = report.get_comp_id(basicDataComp.Text);
                        }

                        string t7seel_Taab="jbn";
                        int dept_code = agent.get_dept_code(dept);
                        int serial = agent.get_serial(dept);
                        string dept_code_str = dept_code.ToString();
                        string serial_str = serial.ToString();
                        string code_str = dept_code + serial_str;
                        int code = Convert.ToInt32(code_str);
                        mail = mailtxt.Text;
                        codetxt.Text = code.ToString();
                        codelbl.Visibility = Visibility.Visible;
                        codetxt.Visibility = Visibility.Visible;
                      //  agent.add_user(name, password, serial, app_inq, cheq_inq, net_inq, rep_inq, online_inq, medical_inq, contract_inq, indcheck, basic, dept, manager, code, compname, active, usertype, prints, notebook, stores, customer, reports, complains, medical_manag, hr_req, complainMember, revise, mail, requesHR, noti_hr, name_ar, name_en, operation, contracts_medical, tele_sales, codz, recollect, afteer_sales, high_dep, t7seel_Taab);
                        MessageBox.Show("تم ادخال مستخدم جديد");
                    }
                }
            }
            catch { }
        }




        private void categCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string category = categCombo.SelectedItem.ToString();
                System.Data.DataTable itemDT = store.get_item_names_from_code(category);
                //itemCombo.Items.Clear();
                if (itemDT.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد اصناف في هذه الفئة");
                }
                else
                {
                    for (int i = 0; i < itemDT.Rows.Count; i++)
                    {
                        comboItems.Items.Add(itemDT.Rows[i].ItemArray[0].ToString());
                    }
                }
            }
            catch { }
        }

        public void load_return_grid()
        {
            try
            {
                string item = comboItems.SelectedItem.ToString();
                System.Data.DataTable dt = store.get_item_name_cat_amount(item);
                returnItem_Grid.Visibility = Visibility.Visible;
                returnItem_Grid.ItemsSource = dt.DefaultView;
                returnItem_Grid.Columns[0].Header = "كود القطعة";
                returnItem_Grid.Columns[1].Header = "اسم القطعة";
                returnItem_Grid.Columns[2].Header = "الكمية";
                returnItem_Grid.Columns[3].Header = "الفئة";
                returnItem_GridItemCounttxt.Content = dt.Rows.Count.ToString();
            }
            catch { }
        }

        private void saveReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (deptCombo.Text == null
                    || empCombo.SelectedItem == null || comboItems.SelectedItem == null
                     || categCombo.SelectedItem == null || ((ComboBoxItem)stateCombo.SelectedItem).Content.ToString() == ""
                     || pricetxt.Text == "" || amount_txt.Text == "" || reasontxt.Document.Blocks.Count == 0)
                {
                    MessageBox.Show("ادخل كل البيانات");
                }
                else
                {
                    string dept = deptCombo.Text;
                    string emp = empCombo.SelectedItem.ToString();
                    string item = comboItems.SelectedItem.ToString();
                    int amount = Convert.ToInt32(amount_txt.Text.ToString());
                    string price = store.get_price(item);
                    string categ = categCombo.SelectedItem.ToString();
                    pricetxt.Text = price;
                    int choice = 0;
                    if (((ComboBoxItem)stateCombo.SelectedItem).Content.ToString() == "مسترجع")
                    {
                        choice = 2;
                    }
                    else if (((ComboBoxItem)stateCombo.SelectedItem).Content.ToString() == "هالِك")
                    {
                        choice = 1;
                    }
                    store.add_transaction(choice.ToString(), emp, dept, item, amount.ToString(), price);
                    int id = store.get_transaction_id();
                    TextRange range = new TextRange(reasontxt.Document.ContentStart, reasontxt.Document.ContentEnd);
                    string reason = range.Text.ToString();
                    if (choice == 1)
                    {
                        store.add_destroy_reason(id, reason);
                    }
                    else if (choice == 2)
                    {
                        store.add_return_reason(id, reason);
                    }
                    string original_amt = store.get_amount(item);
                    int diff = 0;
                    if (choice == 1)
                    {
                        diff = int.Parse(original_amt);
                    }
                    else if (choice == 2)
                    {
                        diff = int.Parse(original_amt) + amount;
                    }
                    store.update_amount_item(diff.ToString(), item, categ);
                    load_return_grid();
                    MessageBox.Show("تم حفظ البيانات");
                }
            }
            catch { }
        }

        public bool check_amount(int amt, int user_amt, string nameitem)
        {
            bool result = false;
            try
            {

                if (nameitem == "")
                {
                    MessageBox.Show("اختر صنف");
                }
                string amount = store.get_amount(nameitem);
                int limit = store.get_limit(nameitem);
                int difference = int.Parse(amount) - user_amt;
                if (limit < difference)
                {
                    result = true;
                }
                else
                    result = false;

            }
            catch { }
            return result;
        }

        private void exportSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (exportItemCombo.SelectedItem == null || export_amounttxt.Text == "" || exportDeptCombo.Text == ""
                    || exportEmpCombo.Text == ""
                    || ((ComboBoxItem)exportTypeCombo.SelectedItem).Content == null
                )
                {
                    MessageBox.Show(" من فضلك ادخل كل البيانات");
                }
                else
                {
                    string categ = exportcategCombo.Text;
                    string item = exportItemCombo.SelectedItem.ToString();
                    string amount = store.get_amount(item);
                    int user_amount = Convert.ToInt32(export_amounttxt.Text.ToString());
                    bool result = check_amount(int.Parse(amount), user_amount, item);
                    int difference = int.Parse(amount) - user_amount;
                    string dept = exportDeptCombo.Text.ToString();
                    string emp = exportEmpCombo.Text.ToString();
                    string typeDB = "";
                    string type = (((ComboBoxItem)exportTypeCombo.SelectedItem).Content.ToString());
                    string export_type = '4'.ToString();
                    if (type == "مؤقتة")
                    {
                        typeDB = '2'.ToString();
                    }
                    else if (type == "دائمة")
                    {
                        typeDB = '1'.ToString();
                    }
                    if (result == true)
                    {
                        store.add_exporter(export_type, item, dept, emp, user_amount, typeDB, categ);
                        store.update_amount(item, difference.ToString());
                        MessageBox.Show("تم حفظ البيانات بنجاح");
                        System.Data.DataTable dt = store.get_exporter();
                        exportGrid.ItemsSource = dt.DefaultView;
                        exportItemCounttxt.Content = dt.Rows.Count.ToString();
                        exportGrid.Columns[0].Header = "اسم القطعة";
                        exportGrid.Columns[1].Header = "اسم الموظف";
                        exportGrid.Columns[2].Header = "القِسم";
                        exportGrid.Columns[3].Header = "الكمية";
                        exportGrid.Columns[4].Header = "نوع العهدة";
                        exportGrid.Items.Refresh();
                    }

                    else if (result == false)
                    {
                        MessageBox.Show("المخزن غير كافي");
                    }
                }
            }
            catch { }
        }

        private void ExportTab_Loaded(object sender, RoutedEventArgs e)
        {
            //export_load();
        }

        private void exportcategCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string category = exportcategCombo.SelectedItem.ToString();
                System.Data.DataTable itemDT = store.get_item_names_from_code(category);
                exportItemCombo.Items.Clear();
                if (itemDT.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد اصناف في هذه الفئة");
                }
                else
                {
                    for (int i = 0; i < itemDT.Rows.Count; i++)
                    {
                        exportItemCombo.Items.Add(itemDT.Rows[i].ItemArray[0].ToString());
                    }
                }
            }
            catch { }

        }

        private void imortTab_Loaded(object sender, RoutedEventArgs e)
        {
            //System.Data.DataTable depts = agent.get_all_dept();
            //imprtDeptCombo.Items.Clear();
            //if (depts.Rows.Count == 0)
            //{
            //    MessageBox.Show("لا توجد نتائج");
            //}
            //else
            //{
            //    for (int i = 0; i < depts.Rows.Count; i++)
            //    {
            //        imprtDeptCombo.Items.Add(depts.Rows[i].ItemArray[0].ToString());
            //    }
            //}
            //System.Data.DataTable category = store.get_category();
            //if (category.Rows.Count == 0)
            //{
            //    MessageBox.Show("لا توجد نتائج");
            //}
            //else
            //{
            //    for (int i = 0; i < category.Rows.Count; i++)
            //    {
            //        importCategoryCombo.Items.Add(category.Rows[i].ItemArray[0].ToString());
            //    }
            //}
        }

        private void imprtSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (importItemCombo.SelectedItem == null || importCategoryCombo.SelectedItem == null
                    || importEmpCombo.Text == "" || imprtDeptCombo.Text == ""
                    || imprt_amounttxt.Text == "" || imprtPricelbl.Text == "" || buyDatetxt.Text == ""
                    || regDatetxt.Text == "" || billtxt.Text == "")
                {
                    MessageBox.Show("ادخل كل البيانات");
                }
                else
                {

                    string name = importItemCombo.SelectedItem.ToString();
                    string category = importCategoryCombo.SelectedItem.ToString();
                    string empname = importEmpCombo.Text.ToString();
                    string deptname = imprtDeptCombo.Text.ToString();
                    string amount = imprt_amounttxt.Text.ToString();
                    string price = (imprtPricelbl.Text.ToString());
                    string datebuy = buyDatetxt.Text.ToString();
                    string datereg = regDatetxt.Text.ToString();
                    string billnum = billtxt.Text;

                    //int total_price = 0;
                    string amount1 = store.get_amount(name);
                    int diff = Convert.ToInt32(amount) + Convert.ToInt32(amount1);
                    if (name == "" || empname == "" || deptname == "" || price == null)
                    {
                        MessageBox.Show("ادخل كل البيانات");
                    }
                    else
                    {
                        store.Update_importer(name, deptname, empname, amount, price, datebuy, datereg, billnum, importCategoryCombo.Text, User.Name);
                        MessageBox.Show("تم حفظ البيانات بنجاح");
                        store.update_amount_item(diff.ToString(), name, category);
                        System.Data.DataTable imp = store.get_imports(empname, name);
                        importCategoryCombo.Text = "";
                        importEmpCombo.Text = "";
                        imprtDeptCombo.Text = "";
                        importItemCombo.Text = "";
                        imprt_amounttxt.Text = "";
                        imprtPricelbl.Text = "";
                        buyDatetxt.Text = "";
                        regDatetxt.Text = "";
                        billtxt.Text = "";
                        grid_import.ItemsSource = imp.DefaultView;
                        importItemCount.Content = "Items Count : " + imp.Rows.Count.ToString();

                    }
                }
            }
            catch { }
        }


        private void importCategoryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string cat = importCategoryCombo.SelectedItem.ToString();
                System.Data.DataTable items = store.get_item_names_from_code(cat);
                importItemCombo.Items.Clear();
                if (items.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد نتائج");
                }
                else
                {
                    for (int i = 0; i < items.Rows.Count; i++)
                    {
                        importItemCombo.Items.Add(items.Rows[i].ItemArray[0].ToString());
                    }
                }
            }
            catch { }
        }

        private void importItemCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string item = importItemCombo.SelectedItem.ToString();
                imprtPricelbl.Text = store.get_price(item);
            }
            catch { }
        }
        #endregion

        #region Indemnity

        private void dtpFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                //int id = Convert.ToInt32(listbox1.SelectedValue.ToString());
                string DateFrom = dtpFrom.Text;
                string DateTo = dtpTo.Text;
                int CompCode = int.Parse(IndemnityCompanyCombo.Text.ToString());

                List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCompanyCodeSearch(DateFrom, DateTo, CompCode);
                IndemnityGrid.Visibility = Visibility.Visible;
                IndemnityGrid.ItemsSource = Indemnities;
            }
            catch { }
        }
        private void dtpTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                indemnity_id = Convert.ToInt32(IndemnityCompanyCombo.Text.ToString());
                string DateFrom = dtpFrom.Text;
                string DateTo = dtpTo.Text;
                //int CompCode = int.Parse(listbox1.SelectedValue.ToString());

                List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCompanyCodeSearch(DateFrom, DateTo, indemnity_id);
                IndemnityGrid.Visibility = Visibility.Visible;
                IndemnityGrid.ItemsSource = Indemnities;
                indemnityItemCounttxt.Content = "Items count : " + Indemnities.Count.ToString();
                //lblItemsCount.Text = "Items_Count: " + Indemnities.Count;
            }
            catch { }
        }




        #endregion

        #region policy





        private void summaryTab_Loaded(object sender, RoutedEventArgs e)
        {
            if (UserType == "hr" || User.Department == "customerservices")
            {
                providertabst.Visibility = Visibility.Collapsed;
            }

        }
        private void SummaryProviderSrchBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                g1.Visibility = Visibility.Visible;
                int code = Convert.ToInt32(PrCodeComboMain.Text.ToString());
                string contr_long = (((ComboBoxItem)SummaryProviderContrLongCombo.SelectedItem).Content.ToString());
                string contr_type = (((ComboBoxItem)SummaryProviderContrTypeCombo.SelectedItem).Content.ToString());
                System.Data.DataTable images = contract.get_provider_images(code, contr_type, contr_long);
                List<string> image = new List<string>();
                if (images.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد صور لهذا العقد");
                }
                else
                {
                    for (int i = 0; i < images.Rows.Count; i++)
                    {
                        for (int j = 0; j < images.Columns.Count; j++)
                        {
                            image.Add(images.Rows[i].ItemArray[j].ToString());
                        }
                    }
                    List<string> true_image = new List<string>();
                    for (int i = 0; i < image.Count; i++)
                    {
                        if (!(image[i].Contains("null")))
                        {
                            true_image.Add(image[i].ToString());

                        }
                        else
                            continue;
                    }
                    string imgname = "";
                    for (int i = 0; i < true_image.Count; i++)
                    {
                        int j = i + 1;
                        imgname = "img" + j;
                        if (imgname == this.img1.Name)
                        {
                            img1.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));

                        }
                        else if (imgname == this.img2.Name)
                        {
                            img2.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.img3.Name)
                        {
                            img3.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.img4.Name)
                        {
                            img4.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.img5.Name)
                        {
                            img5.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.img6.Name)
                        {
                            img6.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.img7.Name)
                        {
                            img7.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.img8.Name)
                        {
                            img8.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.img9.Name)
                        {
                            img9.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                    }
                }
                System.Data.DataTable data = contract.get_selected_provider_data(code);
                prCodetxt.Text = data.Rows[0].ItemArray[0].ToString();
                prEnametxt.Text = data.Rows[0].ItemArray[1].ToString();
                prAnametxt.Text = data.Rows[0].ItemArray[2].ToString();
                prAddr1.Text = data.Rows[0].ItemArray[3].ToString();
                prAddr2.Text = data.Rows[0].ItemArray[4].ToString();
                prTel1.Text = data.Rows[0].ItemArray[5].ToString();
                prTel2.Text = data.Rows[0].ItemArray[6].ToString();
                prTermDate.Text = data.Rows[0].ItemArray[7].ToString();
                prTaxFlag.Text = data.Rows[0].ItemArray[9].ToString();
                prStampVal.Text = data.Rows[0].ItemArray[10].ToString();
                prDevLoc.Text = data.Rows[0].ItemArray[11].ToString();
                prDevext.Text = data.Rows[0].ItemArray[12].ToString();
                prForMedDis.Text = data.Rows[0].ItemArray[13].ToString();
                prLocMedDis.Text = data.Rows[0].ItemArray[14].ToString();
                string degree = data.Rows[0].ItemArray[15].ToString();
                if (degree == "1")
                {
                    prDeg.Text = "A+";
                }
                else if (degree == "2")
                {
                    prDeg.Text = "A";
                }
                else if (degree == "3")
                {
                    prDeg.Text = "B";
                }
                else if (degree == "4")
                {
                    prDeg.Text = "ASO";
                }
                string flag = contract.get_terminate_flag(code.ToString());
                prTermDate.Text = data.Rows[0].ItemArray[7].ToString();
                if (flag == "N" || flag == "n")
                {
                    prTermDate.Visibility = Visibility.Hidden;
                    lbltermDate.Visibility = Visibility.Hidden;
                    prTermFlag.Text = "لا";
                }
                else if (flag == "Y" || flag == "y")
                {
                    prTermFlag.Text = "نعم";
                    prTermFlag.Foreground = Brushes.Red;
                    prTermDate.Foreground = Brushes.Red;
                    prTermDate.Foreground = Brushes.Red;
                    prTermDate.Visibility = Visibility.Visible;
                    lbltermDate.Visibility = Visibility.Visible;
                }
            }
            catch { }
        }
        public static string DServiceCode;
        private void dservcodetxt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string Codeselected = ClassCodeCombo.SelectedItem.ToString();
                string[] arrCode = Codeselected.Split(' ');
                string class_code = arrCode[0].ToString();
                //string classcode = ClassCodeCombo.SelectedItem.ToString();
                int compid = Convert.ToInt32(CompanyComboBoxMain.Text.ToString());
                int contrid = Convert.ToInt32(summaryMainContractCompany.SelectedItem.ToString());

                string ceilingamt = "";
                string ceilingpert = "";
                string indlist = "";
                string carramt = "";
                string refund = "";

                string selected = dservcodetxt.SelectedItem.ToString();
                string[] arr = selected.Split(' ');
                string codetxt = arr[0].ToString();
                string dservName = contract.get_d_serv_name(codetxt);
                System.Data.DataTable serServ = contract.get_ser_serv(codetxt, compid, contrid, class_code);
                serServCombo.Items.Clear();
                for (int i = 0; i < serServ.Rows.Count; i++)
                {
                    serServCombo.Items.Add(serServ.Rows[i].ItemArray[0].ToString() + " " + serServ.Rows[i].ItemArray[1].ToString());
                }
                System.Data.DataTable serviceDetails = contract.get_service_details(compid, contrid, class_code, codetxt);
                System.Data.DataTable data = new System.Data.DataTable();
                data.Columns.Add("كود الخدمة", typeof(string));
                data.Columns.Add("اسم الخدمة", typeof(string));
                data.Columns.Add("الحد الاقصى للتغطية", typeof(string));
                data.Columns.Add("نسبة التغطية", typeof(string));
                data.Columns.Add("قائمة اسعار الاستردادات", typeof(string));
                data.Columns.Add("قيمة التحمل", typeof(string));
                data.Columns.Add("الاسترداد ؟", typeof(string));
                for (int i = 0; i < serviceDetails.Rows.Count; i++)
                {
                    ceilingamt = serviceDetails.Rows[i].ItemArray[1].ToString();
                    ceilingpert = serviceDetails.Rows[i].ItemArray[2].ToString();
                    indlist = serviceDetails.Rows[i].ItemArray[3].ToString();
                    carramt = serviceDetails.Rows[i].ItemArray[4].ToString();
                    refund = serviceDetails.Rows[i].ItemArray[5].ToString();
                }
                data.Rows.Add(codetxt, dservName, ceilingamt, ceilingpert, indlist, carramt, refund);
                servDetailsGrid.ItemsSource = data.DefaultView;

            }
            catch { }
        }


        public static int provider_code;



        private void providerattachBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((ComboBoxItem)providerContrTypeCombo.SelectedItem != null) && ((ComboBoxItem)providerContrLongCombo.SelectedItem != null))
                {
                    get_provider_path();
                    string imgname = "";
                    for (int i = 0; i < image_paths_provider.Count; i++)
                    {
                        int j = i + 1;
                        imgname = "providerContract" + j;
                        if (imgname == this.providerContract1.Name)
                        {
                            providerContract1.Source = new BitmapImage(new Uri(image_paths_provider[i].ToString(), UriKind.RelativeOrAbsolute));

                        }
                        else if (imgname == this.providerContract2.Name)
                        {
                            providerContract2.Source = new BitmapImage(new Uri(image_paths_provider[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.providerContract3.Name)
                        {
                            providerContract3.Source = new BitmapImage(new Uri(image_paths_provider[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.providerContract4.Name)
                        {
                            providerContract4.Source = new BitmapImage(new Uri(image_paths_provider[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.providerContract5.Name)
                        {
                            providerContract5.Source = new BitmapImage(new Uri(image_paths_provider[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.providerContract6.Name)
                        {
                            providerContract6.Source = new BitmapImage(new Uri(image_paths_provider[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.providerContract7.Name)
                        {
                            providerContract7.Source = new BitmapImage(new Uri(image_paths_provider[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.providerContract8.Name)
                        {
                            providerContract8.Source = new BitmapImage(new Uri(image_paths_provider[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                    }
                }
                else
                    MessageBox.Show("من فضلك حدد نوع العقد ونوع نسخة العقد");

                SaveContractProvider.IsEnabled = true;
                // loop on string array to get only file name and concatenate D:\\ then copy it to list
                // loop to add image source 
            }
            catch { }

        }

        public List<string> get_provider_path()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                op.Multiselect = true;
                List<string> filenames = new List<string>();
                string[] arr;
                if (op.ShowDialog() == true)
                {

                    arr = op.FileNames;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        filenames.Add(arr[i]);
                    }

                }

                image_paths_provider = new List<string>();
                for (int i = 0; i < 20; i++)
                {
                    image_paths_provider.Add("null");
                }
                string directory = "C:\\New folder\\";
                for (int i = 0; i < filenames.Count; i++)
                {
                    System.IO.File.Copy(filenames[i], directory + System.IO.Path.GetFileName(filenames[i]));
                    image_paths_provider[i] = directory + System.IO.Path.GetFileName(filenames[i]);
                }

            }
            catch { }
            return image_paths_provider;
        }


        private void SaveContractProvider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((((ComboBoxItem)providerContrTypeCombo.SelectedItem)) == null || (((ComboBoxItem)providerContrLongCombo.SelectedItem)) == null)
                {
                    MessageBox.Show("اختر كود مقدم الخدمة");
                }
                else
                {
                    provider_code = Convert.ToInt32(prCombo.Text.ToString());
                    string contract_type = (((ComboBoxItem)providerContrTypeCombo.SelectedItem).Content).ToString();
                    string contr_long = (((ComboBoxItem)providerContrLongCombo.SelectedItem).Content).ToString();
                    string prov_degree = contract.get_prov_degree(provider_code);
                    string ename = contract.get_pr_ename(provider_code);
                    string aname = contract.get_pr_aname(provider_code);
                    int prov_type = contract.get_prov_type(provider_code);
                    contract.add_contract(provider_code, ename, aname, contract_type, contr_long, image_paths_provider, prov_type, prov_degree);
                    MessageBox.Show("تم حفظ البيانات بنجاح");
                }
            }
            catch { }
        }

        #endregion


        #region approvals

        private void ApprovaltxtCardNum_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    string CardNo = "";
                    int comp_emp = 0;
                    int card_approve = 0;
                    if (User.Type != "DMS Member")
                    {
                        CardNo = ApprovaltxtCardNum.Text.ToString();
                        string[] arr = CardNo.Split('-');
                        string comp = arr[0].ToString();
                        if (comp == CompanyCode)
                        {
                            comp_emp = client.validate_card_num(CardNo);
                            card_approve = client.validate_card_approval(CardNo);
                            if (comp_emp >= 1)
                            {
                                if (card_approve >= 1)
                                {
                                    string value_PatiantName = ApprovaltxtCardNum.Text;
                                    List<EmpApprovalData> Branches = emp.SelectAllApprovals(CardNo, value_PatiantName);
                                    approvalGrid.ItemsSource = Branches;
                                    totalApproveCount.Content = client.count_approve(CardNo).ToString();
                                    approvalGrid.Columns[6].Visibility = Visibility.Hidden;
                                    approvalGrid.Columns[7].Visibility = Visibility.Hidden;
                                    approvalGrid.Columns[8].Visibility = Visibility.Hidden;
                                    approvalGrid.Columns[9].Visibility = Visibility.Hidden;
                                    approvalGrid.Columns[11].Visibility = Visibility.Hidden;
                                }
                                else
                                {
                                    MessageBox.Show("لا توجد موافقة لهذا الكارت");
                                    ApprovaltxtCardNum.Text = "";
                                    approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;
                                    totalApproveCount.Content = "0";
                                }
                            }
                            else
                            {
                                MessageBox.Show("رقم كارت غير موجود");
                                ApprovaltxtCardNum.Text = "";
                                approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;
                                totalApproveCount.Content = "0";
                            }
                        }
                        else
                        {
                            MessageBox.Show("غير مسموح ببيانات هذه الشركة");
                        }

                    }
                    else
                    {
                        CardNo = ApprovaltxtCardNum.Text;
                        comp_emp = client.validate_card_num(CardNo);
                        card_approve = client.validate_card_approval(CardNo);
                        if (comp_emp >= 1)
                        {
                            if (card_approve >= 1)
                            {
                                string value_PatiantName = ApprovaltxtCardNum.Text;
                                List<EmpApprovalData> Branches = emp.SelectAllApprovals(CardNo, value_PatiantName);
                                approvalGrid.ItemsSource = Branches;
                                totalApproveCount.Content = client.count_approve(CardNo).ToString();
                                approvalGrid.Columns[6].Visibility = Visibility.Hidden;
                                approvalGrid.Columns[7].Visibility = Visibility.Hidden;
                                approvalGrid.Columns[8].Visibility = Visibility.Hidden;
                                approvalGrid.Columns[9].Visibility = Visibility.Hidden;
                                approvalGrid.Columns[11].Visibility = Visibility.Hidden;
                                ApprovaltxtCardNum.Text = "";
                                approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;

                            }
                            else
                            {
                                MessageBox.Show("لا توجد موافقة لهذا الكارت");
                                ApprovaltxtCardNum.Text = "";
                                approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;
                                totalApproveCount.Content = "0";
                            }
                        }
                        else
                        {
                            MessageBox.Show("رقم كارت غير موجود");
                            ApprovaltxtCardNum.Text = "";
                            approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;
                            totalApproveCount.Content = "0";
                        }

                    }
                }
            }
            catch { }
        }
        #endregion


        #region customer services
        private void InfotxtCardNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                List<PrintingData> Branches;
                int cardvalidate = 0;
                int underprint = 0;
                int underdeliver = 0;
                int carddeliver = 0;
                try
                {
                    string CardNo = InfotxtCardNum.Text;
                    if (UserType == "hr")
                    {
                        string comp = report.get_comp_id(UserCompany);
                        string[] arr = CardNo.Split('-');
                        string compid = arr[0].ToString();
                        if (comp == compid)
                        {
                            Branches = printserv.SelectAllEmployees_For_SearchAboutCard(CardNo);
                            cardvalidate = client.validate_CardInCompEmployees(CardNo);
                            underprint = client.validate_cardInPrinting(CardNo);
                            underdeliver = client.validate_card_InPrintingDeliverState(CardNo);
                            carddeliver = client.validate_cardDelivery(CardNo);

                            {
                                if (cardvalidate >= 1)
                                {
                                    if (underprint >= 1)
                                    {
                                        if (underdeliver >= 1)
                                        {
                                            if (carddeliver >= 1)
                                            {
                                                MessageBox.Show("تم تسليم الكارت");
                                            }
                                        }
                                        else
                                            MessageBox.Show("الكارت تحت التسليم");

                                    }
                                    else
                                        MessageBox.Show("الكارت تحت الطباعة");
                                }
                                else
                                {
                                    MessageBox.Show("الكارت تحت التسجيل");
                                }
                            }
                            if (Branches != null)
                            {
                                InfoGrid.ItemsSource = Branches;
                                InfoGrid.Columns[0].Visibility = Visibility.Hidden;
                                InfoGrid.Columns[3].Visibility = Visibility.Hidden;
                                InfoGrid.Columns[4].Visibility = Visibility.Hidden;
                                InfoGrid.Columns[5].Visibility = Visibility.Hidden;
                                InfoGrid.Columns[13].Visibility = Visibility.Hidden;
                                InfoGrid.Columns[14].Visibility = Visibility.Hidden;
                                infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                            }
                            else
                            {
                                MessageBox.Show("لا توجد بيانات");
                                InfoGrid.ItemsSource = null;
                                infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                            }
                        }
                        else
                        {
                            MessageBox.Show("غير مسموح");
                        }
                    }
                    else
                    {
                        Branches = printserv.SelectAllEmployees_For_SearchAboutCard(CardNo);
                        cardvalidate = client.validate_CardInCompEmployees(CardNo);
                        underprint = client.validate_cardInPrinting(CardNo);
                        underdeliver = client.validate_card_InPrintingDeliverState(CardNo);
                        carddeliver = client.validate_cardDelivery(CardNo);

                        {
                            if (cardvalidate >= 1)
                            {
                                if (underprint >= 1)
                                {
                                    if (underdeliver >= 1)
                                    {
                                        if (carddeliver >= 1)
                                        {
                                            MessageBox.Show("تم تسليم الكارت");
                                        }
                                    }
                                    else
                                        MessageBox.Show("الكارت تحت التسليم");

                                }
                                else
                                    MessageBox.Show("الكارت تحت الطباعة");
                            }
                            else
                            {
                                MessageBox.Show("الكارت تحت التسجيل");
                            }
                        }
                        if (Branches != null)
                        {
                            InfoGrid.ItemsSource = Branches;
                            InfoGrid.Columns[0].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[3].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[4].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[5].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[13].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[14].Visibility = Visibility.Hidden;
                            infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                        }
                        else
                        {
                            MessageBox.Show("لا توجد بيانات");
                            InfoGrid.ItemsSource = null;
                            infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                        }
                    }
                }
                catch { }
            }
        }

        /// <calls>
        /// //////////////////
        /// </calls>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void generalSrchCalls_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string input = CallstxtSrch.Text.ToString();
                System.Data.DataTable data = client.get_employeeData_FromID(input);
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات");
                }
                else
                {
                    startCallBtn.IsEnabled = true;
                    hour = DateTime.Now.Hour;
                    min = DateTime.Now.Minute;
                    sec = DateTime.Now.Second;
                    endCallBtn.IsEnabled = true;
                    endCallBtn.Visibility = Visibility.Visible;
                    callDurationtxt.Visibility = Visibility.Visible;
                    endCallBtn.IsEnabled = true;

                    CallsGrid.ItemsSource = data.DefaultView;
                    CallsGrid.Columns[0].Header = "اسم الشركة";
                    CallsGrid.Columns[1].Header = "رقم الكارت";
                    CallsGrid.Columns[2].Header = "الاسم الاول";
                    CallsGrid.Columns[3].Header = "الاسم الثاني";
                    CallsGrid.Columns[4].Header = "الاسم الثالث";
                    CallsGrid.Columns[5].Header = "الاسم الرابع";
                    CallsGrid.Columns[6].Header = "لقب العائلة";
                    CallsGrid.Columns[7].Header = "الاسم باللغة الانجليزية";
                    CallsGrid.Columns[8].Header = "الاسم الاول";
                    CallsGrid.Columns[9].Header = "الاسم الثاني";
                    CallsGrid.Columns[10].Header = "الاسم الثالث";
                    CallsGrid.Columns[11].Header = "الاسم الرابع";
                    CallsGrid.Columns[12].Header = "لقب العائلة";
                    CallsGrid.Columns[13].Header = "الاسم باللغة العربية";
                    CallsGrid.Columns[14].Header = "البريد الاليكتروني";
                    CallsGrid.Columns[15].Header = "تاريخ الميلاد";
                    CallsGrid.Columns[16].Header = "عنوان 1";
                    CallsGrid.Columns[17].Header = "عنوان 2";
                    CallsGrid.Columns[18].Header = "منتهي ؟";
                    CallsGrid.Columns[19].Header = "تاريخ الانتهاء";
                    CallsGrid.Columns[20].Header = "هاتف 1";
                    CallsGrid.Columns[21].Header = "هاتف 2";
                    CallsGrid.Columns[22].Header = "بداية تاريخ التأمين";
                    CallsGrid.Columns[23].Header = "نهاية تاريخ التأمين";

                }
            }
            catch { }
        }

        private void CallsSrchNameBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string input = CallstxtSrch.Text.ToString();
                string[] name = input.Split(' ');

                if (name.Length == 3)
                {
                    System.Data.DataTable data = client.get_employeeData_FromName(input);
                    if (data.Rows.Count == 0)
                    {
                        MessageBox.Show("لا توجد بيانات");
                    }
                    else
                    {
                        startCallBtn.IsEnabled = true;
                        hour = DateTime.Now.Hour;
                        min = DateTime.Now.Minute;
                        sec = DateTime.Now.Second;
                        endCallBtn.IsEnabled = true;
                        endCallBtn.Visibility = Visibility.Visible;
                        callDurationtxt.Visibility = Visibility.Visible;
                        endCallBtn.IsEnabled = true;

                        CallsGrid.ItemsSource = data.DefaultView;
                        CallsGrid.Columns[0].Header = "اسم الشركة";
                        CallsGrid.Columns[1].Header = "رقم الكارت";
                        CallsGrid.Columns[2].Header = "الاسم الاول";
                        CallsGrid.Columns[3].Header = "الاسم الثاني";
                        CallsGrid.Columns[4].Header = "الاسم الثالث";
                        CallsGrid.Columns[5].Header = "الاسم الرابع";
                        CallsGrid.Columns[6].Header = "لقب العائلة";
                        CallsGrid.Columns[7].Header = "الاسم باللغة الانجليزية";
                        CallsGrid.Columns[8].Header = "الاسم الاول";
                        CallsGrid.Columns[9].Header = "الاسم الثاني";
                        CallsGrid.Columns[10].Header = "الاسم الثالث";
                        CallsGrid.Columns[11].Header = "الاسم الرابع";
                        CallsGrid.Columns[12].Header = "لقب العائلة";
                        CallsGrid.Columns[13].Header = "الاسم باللغة العربية";
                        CallsGrid.Columns[14].Header = "البريد الاليكتروني";
                        CallsGrid.Columns[15].Header = "تاريخ الميلاد";
                        CallsGrid.Columns[16].Header = "عنوان 1";
                        CallsGrid.Columns[17].Header = "عنوان 2";
                        CallsGrid.Columns[18].Header = "منتهي ؟";
                        CallsGrid.Columns[19].Header = "تاريخ الانتهاء";
                        CallsGrid.Columns[20].Header = "هاتف 1";
                        CallsGrid.Columns[21].Header = "هاتف 2";
                        CallsGrid.Columns[22].Header = "بداية تاريخ التأمين";
                        CallsGrid.Columns[23].Header = "نهاية تاريخ التأمين";
                    }
                }
                else
                    MessageBox.Show("من فضلك قم بأدخال الأسم الثلاثي");
            }
            catch { }
        }

        private void CallsEditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < CallsGrid.SelectedItems.Count; i++)
                {
                    System.Data.DataRowView dr = (System.Data.DataRowView)CallsGrid.SelectedItems[i];
                    string cardno = (dr.Row.ItemArray[1].ToString());
                    string empenamest = (dr.Row.ItemArray[2].ToString());
                    string empenamesc = (dr.Row.ItemArray[3].ToString());
                    string empEnameth = (dr.Row.ItemArray[4].ToString());
                    string empEnameFr = (dr.Row.ItemArray[5].ToString());
                    string empEnameFam = (dr.Row.ItemArray[6].ToString());
                    string empEname = (dr.Row.ItemArray[7].ToString());
                    string empAnameSt = (dr.Row.ItemArray[8].ToString());
                    string empAnameSc = (dr.Row.ItemArray[9].ToString());
                    string empAnameth = (dr.Row.ItemArray[10].ToString());
                    string empAnameFr = (dr.Row.ItemArray[11].ToString());
                    string empAnameFam = (dr.Row.ItemArray[12].ToString());
                    string empAname = (dr.Row.ItemArray[13].ToString());
                    string email = (dr.Row.ItemArray[14].ToString());
                    string birthdate = (dr.Row.ItemArray[15].ToString());
                    string addr1 = (dr.Row.ItemArray[16].ToString());
                    string addr2 = (dr.Row.ItemArray[17].ToString());
                    string termin_flag = (dr.Row.ItemArray[18].ToString());
                    string termin_date = (dr.Row.ItemArray[19].ToString());
                    string tel1 = (dr.Row.ItemArray[20].ToString());
                    string tel2 = (dr.Row.ItemArray[21].ToString());
                    string ins_start_date = (dr.Row.ItemArray[22].ToString());
                    string ins_end_date = (dr.Row.ItemArray[23].ToString());
                    //client.update_data(cardno, empenamest, empenamesc, empEnameth, empEnameFr, empEnameFam, empEname, empAnameSt, empAnameSc, empAnameth, empAnameFr, empAnameFam, empAname, email, birthdate, addr1, addr2, termin_flag, termin_date, tel1, tel2, ins_start_date, ins_end_date);
                    //agent.del_dept(code);
                    MessageBox.Show("تم تعديل البيانات بنجاح");
                }
            }
            catch { }
        }
        #endregion


        #region PRINTING

        private ObservableCollection<String> CardType
        { get; set; }
        private void PrintSCCodetxt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    string id = PrintSCCodetxt.Text.ToString();
                    if (id == "")
                    {
                        MessageBox.Show("ادخل رقم صحيح");
                    }
                    else
                    {
                        string ContractNo = printserv.SelectMaxContractNo(id); // check lw hwa "" popup messagebox
                        int ContractNo2 = int.Parse(ContractNo.ToString());
                        PrintingData obj = printserv.SelectEmpById(id, ContractNo2);
                        txtEmpId.Text = obj.EmpID.ToString();
                        txtEmpName.Text = obj.EmpName.ToString();
                        txtCompanyNamePrintSC.Text = obj.CompanyName;
                        cmbCardType.Text = obj.PrintingType;
                        //cmbCardType.Items.Clear();
                        //cmbCardType.Items.Add(obj.PrintingType);
                    }
                }
            }
            catch { }
        }

        private void PrintSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            #region UpdateWithCheckPrintingCount
            try
            {
                if (PrintGrid.SelectedItems.Count > 0)
                {
                    PrintingData obj = new PrintingData();
                    object item = PrintGrid.SelectedItem;
                    obj.EmpID = (PrintGrid.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                    obj.ContractNo = int.Parse((PrintGrid.SelectedCells[8].Column.GetCellContent(item) as TextBlock).Text);

                    string Count = printserv.SelectPrintingCount(obj.EmpID, obj.ContractNo);
                    if (Count == null || Count == "0") //means it is the first time to print
                    {
                        #region Selectmax_PrintNO
                        string max2 = printserv.SelectMaxPrintingId();
                        int maxx2 = 0;
                        if (max2 == "")
                        {
                            obj.PrintNo = "1";
                        }
                        else
                        {
                            maxx2 = int.Parse(max2) + 1;
                            obj.PrintNo = maxx2.ToString();
                        }
                        #endregion
                        obj.EmpID = (PrintGrid.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                        obj.EmpFirstName = (PrintGrid.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;
                        obj.EmpSecondName = (PrintGrid.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text;
                        obj.EmpThirdName = (PrintGrid.SelectedCells[7].Column.GetCellContent(item) as TextBlock).Text;
                        obj.EmpName = obj.EmpFirstName + " " + obj.EmpSecondName + " " + obj.EmpThirdName;
                        obj.CompanyName = (PrintGrid.SelectedCells[10].Column.GetCellContent(item) as TextBlock).Text;
                        obj.CompID = (PrintGrid.SelectedCells[9].Column.GetCellContent(item) as TextBlock).Text;
                        obj.PrintingType = (PrintGrid.SelectedCells[11].Column.GetCellContent(item) as TextBlock).Text;
                        obj.ContractNo = int.Parse((PrintGrid.SelectedCells[8].Column.GetCellContent(item) as TextBlock).Text);
                        obj.PrintedBy = NameTab.Header.ToString();
                        //--------check if this messenger already exist------------------
                        PrintingData OldEmp = printserv.SelectEmpById(obj.EmpID, obj.ContractNo);
                        if (OldEmp != null)
                        {
                            //-------update it only when it press Edit otherwise show the message-------------
                        }
                        else            //----insert the new messenger
                        {
                            int affected = printserv.InsertEmp_In_Printing(obj);
                            if (affected > 0)
                            {
                                //---------
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("لقد قمت بالطباعة فى المرة الاولى يمكنك الطباعة للمرة الثانية فى الشاشة الاخرى");

                    }
                }

            }

            catch { }
            #endregion           

        }
        private void printSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((PrintDateFrom.SelectedDate != null) && (PrintDateTo.SelectedDate != null))
                {
                    string dateFrom = PrintDateFrom.SelectedDate.Value.ToShortDateString();
                    string dateTo = PrintDateTo.SelectedDate.Value.ToShortDateString();
                    List<PrintingData> Emps = printserv.SelectAllEmployees(dateFrom, dateTo);
                    PrintGrid.ItemsSource = Emps;
                    printItemCounttxt.Content = PrintGrid.Items.Count - 1;
                }
                else
                    MessageBox.Show("من فضلك أدخل الفترة المراد البحث فيها");

                #region HideSomeColumns
                //PrintGrid.Columns[1].Visibility = Visibility.Visible;
                //PrintGrid.Columns[13].Visibility = Visibility.Hidden;
                //PrintGrid.Columns[0].Visibility = Visibility.Hidden;
                //PrintGrid.Columns[2].Visibility = Visibility.Hidden;
                ////PrintGrid.Columns["ResonCode"].Visibility = false;
                //PrintGrid.Columns[10].Visibility = Visibility.Hidden;
                //PrintGrid.Columns[11].Visibility = Visibility.Hidden;
                //PrintGrid.Columns[12].Visibility = Visibility.Hidden;
                //PrintGrid.Columns[6].Visibility = Visibility.Hidden;
                //PrintGrid.Columns[7].Visibility = Visibility.Hidden;
                //PrintGrid.Columns[8].Visibility = Visibility.Hidden;
                //PrintGrid.Columns[9].Visibility = Visibility.Hidden;
                #endregion
            }
            catch { }
        }

        private void color1_Selected(object sender, RoutedEventArgs e)
        {
            color_1 = (((ComboBoxItem)sender).Content).ToString();
        }

        private void color2_Selected(object sender, RoutedEventArgs e)
        {
            color_2 = (((ComboBoxItem)sender).Content).ToString();
        }

        private void color3_Selected(object sender, RoutedEventArgs e)
        {
            color_3 = (((ComboBoxItem)sender).Content).ToString();
        }

        private void color4_Selected(object sender, RoutedEventArgs e)
        {
            color_4 = (((ComboBoxItem)sender).Content).ToString();
        }
        private void FirstPrintTab_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void PrintingEditBtn_Click(object sender, RoutedEventArgs e)
        {
            #region Update_Without_CheckPrintingCount
            try
            {
                if (PrintGrid.SelectedItems.Count > 0)
                {
                    PrintingData obj = new PrintingData();
                    object item = PrintGrid.SelectedItem;
                    obj.EmpID = (PrintGrid.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                    obj.ContractNo = int.Parse((PrintGrid.SelectedCells[8].Column.GetCellContent(item) as TextBlock).Text);

                    string Count = printserv.SelectPrintingCount(obj.EmpID, obj.ContractNo);
                    if (Count == null || Count == "0") //means it is the first time to print
                    {
                        #region Selectmax_PrintNO
                        string max2 = printserv.SelectMaxPrintingId();
                        int maxx2 = 0;
                        if (max2 == "")
                        {
                            obj.PrintNo = "1";
                        }
                        else
                        {
                            maxx2 = int.Parse(max2) + 1;
                            obj.PrintNo = maxx2.ToString();
                        }
                        #endregion
                        obj.EmpID = (PrintGrid.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                        obj.EmpFirstName = (PrintGrid.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;
                        obj.EmpSecondName = (PrintGrid.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text;
                        obj.EmpThirdName = (PrintGrid.SelectedCells[7].Column.GetCellContent(item) as TextBlock).Text;
                        obj.EmpName = obj.EmpFirstName + " " + obj.EmpSecondName + " " + obj.EmpThirdName;
                        obj.CompanyName = (PrintGrid.SelectedCells[11].Column.GetCellContent(item) as TextBlock).Text;
                        obj.CompID = (PrintGrid.SelectedCells[10].Column.GetCellContent(item) as TextBlock).Text;
                        if (color_1 != null)
                        {
                            obj.PrintingType = color_1;

                        }
                        else if (color_2 != null)
                        {
                            obj.PrintingType = color_2;
                        }
                        else if (color_3 != null)
                        {
                            obj.PrintingType = color_3;

                        }
                        else if (color_4 != null)
                        {
                            obj.PrintingType = color_4;
                        }
                        obj.ContractNo = int.Parse((PrintGrid.SelectedCells[8].Column.GetCellContent(item) as TextBlock).Text);
                        obj.PrintedBy = NameTab.Header.ToString();
                        //--------check if this messenger already exist------------------
                        PrintingData OldEmp = printserv.SelectEmpById(obj.EmpID, obj.ContractNo);
                        if (OldEmp != null)
                        {//--------check if this messenger already exist------------------
                            if (obj.EmpID == OldEmp.EmpID)//---means that it is exist so update it
                            {
                                int affected = printserv.UpdateEmp_In_Printing(obj, obj.EmpID);
                                if (affected > 0)
                                {
                                    MessageBox.Show("تم تعديل نوع كارت الطباعة");
                                }
                            }
                        }
                        else            //----insert the new messenger
                        {
                            int affected = printserv.InsertEmp_In_Printing(obj);
                            if (affected > 0)
                            {
                                //---------
                            }
                        }
                    }

                    else
                    {
                        MessageBox.Show("هذا الكارت مطبوع مسبقا ، يمكنك تغيير نوع الكارت في شاشة طباعة مرة ثانية");
                    }
                }
            }
            catch { }

            #endregion
        }

        private void btnSavePrintSC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string id = PrintSCCodetxt.Text;

                string ContractNo = printserv.SelectMaxContractNo(id);
                int ContractNo2 = int.Parse(ContractNo.ToString());

                PrintingData obj = printserv.SelectEmpById(id, ContractNo2);
                if (obj != null)
                {
                    string Count = printserv.SelectPrintingCount(id, ContractNo2);
                    int count2 = int.Parse(Count.ToString());
                    obj.PrintingCount = (count2 + 1).ToString();
                }

                obj.EmpID = txtEmpId.Text;
                obj.EmpName = txtEmpName.Text;
                //  obj.CompanyName = txtCompanyName.Text;

                obj.PrintingType = cmbCardType.Text.ToString();
                obj.ContractNo = ContractNo2;
                obj.PrintedBy = NameTab.Header.ToString();
                obj.PrintedDate = DateTime.Now.ToShortDateString();
                //----------------select Reson from Resons table-------------------
                int ResonId = 0;
                if (chkReOpen.IsChecked == true)
                {
                    ResonId = 1;

                    PrintingData objReson = printserv.SelectResonById(ResonId);
                    if (objReson != null)
                    {
                        PrintingData objPrintNO = printserv.SelectEmpById(id, ContractNo2);
                        obj.PrintNo = objPrintNO.PrintNo;
                        obj.ResonCode = ResonId;
                        int affected = printserv.InsertReson_In_PRINT_REAS(obj);
                        if (affected > 0)
                        {
                            //---------
                        }
                    }
                }
                if (chkLost.IsChecked == true)
                {
                    ResonId = 2;

                    PrintingData objReson = printserv.SelectResonById(ResonId);
                    if (objReson != null)
                    {
                        PrintingData objPrintNO = printserv.SelectEmpById(id, ContractNo2);
                        obj.PrintNo = objPrintNO.PrintNo;
                        obj.ResonCode = ResonId;
                        int affected = printserv.InsertReson_In_PRINT_REAS(obj);
                        if (affected > 0)
                        {
                            //---------
                        }
                    }
                }


                if (chkChangePic.IsChecked == true)
                {
                    ResonId = 3;

                    PrintingData objReson = printserv.SelectResonById(ResonId);
                    if (objReson != null)
                    {
                        PrintingData objPrintNO = printserv.SelectEmpById(id, ContractNo2);
                        obj.PrintNo = objPrintNO.PrintNo;
                        obj.ResonCode = ResonId;
                        int affected = printserv.InsertReson_In_PRINT_REAS(obj);
                        if (affected > 0)
                        {
                            //---------
                        }
                    }
                }
                if (chkChangeName.IsChecked == true)
                {
                    ResonId = 4;

                    PrintingData objReson = printserv.SelectResonById(ResonId);
                    if (objReson != null)
                    {
                        PrintingData objPrintNO = printserv.SelectEmpById(id, ContractNo2);
                        obj.PrintNo = objPrintNO.PrintNo;
                        obj.ResonCode = ResonId;
                        int affected = printserv.InsertReson_In_PRINT_REAS(obj);
                        if (affected > 0)
                        {
                            //---------
                        }
                    }
                }


                int affected2 = printserv.UpdateEmp_In_Printing_SecondTime(obj, obj.EmpID, obj.ContractNo);
                if (affected2 > 0)
                {
                    string richtxt = new TextRange(txtResons.Document.ContentStart, txtResons.Document.ContentEnd).Text.ToString();

                    MessageBox.Show("تم الحفظ بنجاح");
                    PrintSCCodetxt.Text = "";
                    txtEmpId.Text = "";
                    txtEmpName.Text = "";
                    richtxt = "";
                    cmbCardType.Text = "";
                    txtCompanyNamePrintSC.Text = "";
                    chkReOpen.IsChecked = false;
                    chkLost.IsChecked = false;
                    chkChangeName.IsChecked = false;
                    chkChangePic.IsChecked = false;
                    chkOther.IsChecked = false;
                    txtResons.Document.Blocks.Clear();
                }
            }
            catch { }
        }

        private void chkOther_Checked(object sender, RoutedEventArgs e)
        {
            if (chkOther.IsChecked == true)
            {
                txtResons.Visibility = Visibility.Visible;
            }
            else if (chkOther.IsChecked == false)
            {
                txtResons.Visibility = Visibility.Hidden;
            }
        }

        private void txtSearchRecieving_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string companyName = txtSearchRecieving.Text.ToLower().ToString();
                List<PrintingData> Companylist = printserv.SelectAllCompaniesForSearch(companyName, companyName);
                listBox1Recieving.ItemsSource = Companylist;
                listBox1Recieving.DisplayMemberPath = "CompanyName";
                listBox1Recieving.SelectedValuePath = "CompID";
            }
            catch { }
        }
        private void tmimgprintcompany_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dsprint = db.RunReaderds(" select distinct C_COMP_ID , C_ANAME from V_COMPANIES where C_COMP_ID  LIKE '%" + tmcompanyprint.Text + "%' or C_ANAME LIKE '%" + tmcompanyprint.Text + "%' ORDER BY C_COMP_ID ");
                tmcompanyprint.ItemsSource = dsprint.Tables[0].DefaultView;
                tmcompanyprint.IsDropDownOpen = true;
            }
            catch { }



        }
        private void listBox1Recieving_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // int index = listBox1Recieving.SelectedIndex;
            //MessageBox.Show( listBox1Recieving.Items[index].ToString());
            //string li = (((ListBoxItem)listBox1Recieving.SelectedItem).Content.ToString());
            // lblCompanyNameRecieving.Content = listBox1Recieving.SelectedItems[index].ToString();
            //---------------select company_area and some details about it----------------------
            try
            {
                string companyCode = listBox1Recieving.SelectedValue.ToString();
                List<PrintingData> list = printserv.SelectAllEmployees_For_ReceivingCards(companyCode);
                ReceivingGrid.ItemsSource = list;
                receiveItemconttxt.Content = list.Count.ToString();
                if (list != null)
                {
                    ReceivingGrid.Columns[7].Visibility = Visibility.Visible;
                    receiveItemconttxt.Content = list.Count.ToString();
                    //ReceivingGrid.Columns["EmpName"].Width = 200;
                    //ReceivingGrid.Columns["RecievedName"].Width = 200;
                    //----Hide some columns--------------
                    #region HideSomeColumns
                    ReceivingGrid.Columns[8].Visibility = Visibility.Visible;
                    ReceivingGrid.Columns[16].Visibility = Visibility.Visible;
                    // ReceivingGrid.Columns["ReceivedState"].DefaultCellStyle.NullValue = "N";
                    ReceivingGrid.Columns[1].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[0].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[14].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[15].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[17].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[10].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[11].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[12].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[3].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[4].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[5].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[13].Visibility = Visibility.Hidden;
                    #endregion
                }
                else
                {
                    //---------- from pc--------
                    MessageBox.Show("لا توجد نتائج");
                    receiveItemconttxt.Content = "0";
                    ReceivingGrid.Columns[15].Visibility = Visibility.Visible;
                }
                ReceivingGrid.Columns[17].Visibility = Visibility.Visible;
            }
            catch
            {
                ReceivingGrid.ItemsSource = null;
                receiveItemconttxt.Content = "0";
            }
        }

        private void ReceivingFrm_Loaded(object sender, RoutedEventArgs e)
        {
            //if (printTab.Visibility == Visibility.Visible)
            //{
            //    recevingGroup.Visibility = Visibility.Hidden;
            //    dtpDateReceiving.Visibility = Visibility.Hidden;
            //    ReceivingGrid.Visibility = Visibility.Hidden;
            //    btnSearchDateReceving.Visibility = Visibility.Hidden;


            //    try
            //    {
            //        List<PrintingData> Companylist = printserv.SelectAllCompaniesForReceivingCards();
            //        listBox1Recieving.ItemsSource = Companylist;
            //        listBox1Recieving.DisplayMemberPath = "CompanyName";
            //        listBox1Recieving.SelectedValuePath = "CompID";
            //        string CompCode = listBox1Recieving.SelectedValue.ToString();

            //        List<PrintingData> Cards = printserv.SelectAllEmployees_For_ReceivingCards(CompCode);
            //        ReceivingGrid.ItemsSource = Cards;

            //        #region HideSomeColumns
            //        ReceivingGrid.Columns[1].Width = 300;
            //        ReceivingGrid.Columns[3].Width = 300;
            //        ReceivingGrid.Columns[2].Visibility = Visibility.Visible;
            //        //----Hide some columns--------------
            //        #region HideSomeColumns
            //        ReceivingGrid.Columns[1].Visibility = Visibility.Visible;
            //        ReceivingGrid.Columns[13].Visibility = Visibility.Hidden;
            //        ReceivingGrid.Columns[0].Visibility = Visibility.Hidden;
            //        ReceivingGrid.Columns[10].Visibility = Visibility.Hidden;
            //        ReceivingGrid.Columns[11].Visibility = Visibility.Hidden;
            //        ReceivingGrid.Columns[12].Visibility = Visibility.Hidden;
            //        ReceivingGrid.Columns[6].Visibility = Visibility.Hidden;
            //        ReceivingGrid.Columns[7].Visibility = Visibility.Hidden;
            //        ReceivingGrid.Columns[8].Visibility = Visibility.Hidden;

            //        ReceivingGrid.Columns[9].Visibility = Visibility.Hidden;
            //        #endregion

            //        #endregion
            //    }
            //    catch { }
            //}
        }

        private void rdCompanyName_Checked(object sender, RoutedEventArgs e)
        {
            recevingGroup.Visibility = Visibility.Visible;
            ReceivingGrid.Visibility = Visibility.Visible;
            dtpDateReceiving.Visibility = Visibility.Hidden;
            btnSearchDateReceving.Visibility = Visibility.Hidden;

            dsprint = db.RunReaderds(" select distinct C_COMP_ID , C_ANAME from V_COMPANIES ORDER BY C_COMP_ID ");
            tmcompanyprint.ItemsSource = dsprint.Tables[0].DefaultView;


            listBox1Recieving.Visibility = Visibility.Hidden;
            txtSearchRecieving.Visibility = Visibility.Hidden;

        }

        private void btnSearchDateReceving_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string date = dtpDateReceiving.SelectedDate.Value.ToShortDateString();
                List<PrintingData> list = printserv.SelectAllEmployees_For_ReceivingCardsByDate(date);
                ReceivingGrid.ItemsSource = list;
                if (list != null)
                {
                    #region HideSomeColumns
                    ReceivingGrid.Columns[8].Visibility = Visibility.Visible;
                    ReceivingGrid.Columns[16].Visibility = Visibility.Visible; //
                                                                               // ReceivingGrid.Columns["ReceivedState"].DefaultCellStyle.NullValue = "N";
                    ReceivingGrid.Columns[1].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[0].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[14].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[17].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[10].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[11].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[12].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[3].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[4].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[5].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[13].Visibility = Visibility.Hidden;
                    #endregion
                }
                else
                {
                    MessageBox.Show("لا توجد نتائج");
                    // ----------from pc--------
                    ReceivingGrid.Columns[15].Visibility = Visibility.Visible;
                }
                receiveItemconttxt.Content = list.Count.ToString();
                ReceivingGrid.Columns[17].Visibility = Visibility.Visible;
                ReceivingGrid.Columns[17].Header = "ادخل اسم المستلم";
            }
            catch
            {
                ReceivingGrid.ItemsSource = null;
            }
        }

        private void rdDate_Checked(object sender, RoutedEventArgs e)
        {
            btnSearchDateReceving.Visibility = Visibility.Visible;
            dtpDateReceiving.Visibility = Visibility.Visible;
            recevingGroup.Visibility = Visibility.Hidden;
            ReceivingGrid.Visibility = Visibility.Visible;
        }

        #endregion


        #region notebooks
        private void txtTransSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnUpdateRequest.IsEnabled = true;
                btnDeleteRequest.IsEnabled = true;
                try
                {
                    dtpRequestDate.IsEnabled = true;
                    txtCount.IsEnabled = true;
                    int ReqNo = int.Parse(txtTransSearch.Text);
                    NoteBookData obj = note.SelectNotebookRequestsById(ReqNo);
                    txtRequestNo.Text = obj.Order_Num.ToString();
                    txtCount.Text = obj.Batch_Count.ToString();
                    dtpRequestDate.Text = obj.Request_Date.ToString();
                    cmbNotebookTypes.SelectedValue = obj.Notebook_Type_Code;
                    cmbProviderTypeRequestNotebook.SelectedValue = obj.ProvTypeCode;
                    int prcode = obj.Prov_Code;
                    List<NoteBookData> ProviderNames = note.SelectSpecificProvider(prcode);
                    listboxRequestNote.ItemsSource = ProviderNames;
                    listboxRequestNote.DisplayMemberPath = "Prov_Name";
                    listboxRequestNote.SelectedValuePath = "Prov_Code";
                }
                catch
                {
                    MessageBox.Show("لا توجد نتائج مطابقة");
                    btnUpdateRequest.IsEnabled = false;
                    btnDeleteRequest.IsEnabled = false;
                    txtCount.Text = "";
                    txtRequestNo.Text = "";
                }

            }
        }

        private void cmbProviderTypeRequestNotebook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //------------------fill Provide Names--------------------
                int ProvideCode = int.Parse(cmbProviderTypeRequestNotebook.SelectedValue.ToString());
                List<NoteBookData> ProviderNames = note.SelectAllProviderNames(ProvideCode);
                if (ProviderNames == null)
                {
                    MessageBox.Show("لا توجد نتائج");
                }
                else
                {
                    listboxRequestNote.ItemsSource = ProviderNames;
                    listboxRequestNote.DisplayMemberPath = "Prov_Name";
                    listboxRequestNote.SelectedValuePath = "Prov_Code";
                }
            }
            catch { }
        }
        private void txtProviderName_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int ProvideCode = int.Parse(txtProviderName.Text);
                string ProvideName = txtProviderName.Text;
                if (cmbProviderTypeRequestNotebook.SelectedValue.ToString() == null || cmbProviderTypeRequestNotebook.SelectedValue.ToString() == "")
                {
                    MessageBox.Show("اختر مقدم الخدمة");
                }
                else
                {
                    int ProviderType = int.Parse(cmbProviderTypeRequestNotebook.SelectedValue.ToString());
                    List<NoteBookData> ProviderNames = note.SelectAllProviderNamesForSearch(ProvideCode, ProvideName, ProviderType);
                    listboxRequestNote.ItemsSource = ProviderNames;
                    listboxRequestNote.DisplayMemberPath = "Prov_Name";
                    listboxRequestNote.SelectedValuePath = "Prov_Code";
                }
            }
            catch
            {
                int ProvideCode = -1;
                string ProvideName = txtProviderName.Text;
                int ProviderType = int.Parse(cmbProviderTypeRequestNotebook.SelectedValue.ToString());
                List<NoteBookData> ProviderNames = note.SelectAllProviderNamesForSearch(ProvideCode, ProvideName, ProviderType);
                listboxRequestNote.ItemsSource = ProviderNames;
                listboxRequestNote.DisplayMemberPath = "Prov_Name";
                listboxRequestNote.SelectedValuePath = "Prov_Code";
            }
        }

        private void listboxRequestNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(listboxRequestNote.SelectedValue.ToString());
                lblProviderName.Text = store.get_provider_notebbok(id);
            }
            catch { }
        }
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtCount.IsEnabled = true;
                txtCount.Text = "";
                lblProviderName.Text = "";
                txtProviderName.Text = "";
                cmbNotebookTypes.Text = "";
                cmbProviderTypeRequestNotebook.Text = "";
                listboxRequestNote.ItemsSource = null;
                btnSaveRequest.IsEnabled = true;
                txtTransSearch.Text = "";
                btnDeleteRequest.IsEnabled = false;
                btnUpdateRequest.IsEnabled = false;
                dtpRequestDate.IsEnabled = true;
                dtpRequestDate.Text = "";
                //----------select max of Notebook Request-----------------------
                #region Selectmax_Notebook_RequestNo
                string max2 = note.SelectMaxNotebookRequestNo();
                int maxx2 = 0;
                if (max2 == "")
                {
                    txtRequestNo.Text = "1";
                }

                else
                {
                    maxx2 = int.Parse(max2) + 1;
                    txtRequestNo.Text = maxx2.ToString();
                }
                #endregion
            }
            catch { }
        }
        private void btnSaveRequest_Click(object sender, RoutedEventArgs e)
        {
            if (txtCount.Text == "")
            {
                MessageBox.Show("ادخل العدد المطلوب");
            }
            else
            {
                try
                {
                    NoteBookData obj = new NoteBookData();
                    obj.Order_Num = int.Parse(txtRequestNo.Text);

                    if (cmbProviderTypeRequestNotebook.SelectedValue == null || listboxRequestNote.SelectedValue == null || dtpRequestDate.Text == "" || cmbNotebookTypes.SelectedValue == null || txtCount.Text == "")
                    {
                        MessageBox.Show("ادخل البيانات كاملة");
                    }
                    else
                    {
                        obj.ProvTypeCode = int.Parse(cmbProviderTypeRequestNotebook.SelectedValue.ToString());
                        obj.Prov_Code = int.Parse(listboxRequestNote.SelectedValue.ToString());
                        obj.Request_Date = dtpRequestDate.Text;
                        obj.Notebook_Type_Code = int.Parse(cmbNotebookTypes.SelectedValue.ToString());
                        string amount = store.get_amount(cmbNotebookTypes.Text.ToString());
                        bool result = check_amount(Convert.ToInt32(amount), Convert.ToInt32(txtCount.Text.ToString()), cmbNotebookTypes.Text.ToString());
                        obj.Batch_Count = int.Parse(txtCount.Text);
                        obj.Created_By = NameTab.Header.ToString();
                        if (result == true)
                        {
                            int affected = note.InsertNotebook_Request(obj);
                            if (affected >= 1)
                            {
                                MessageBox.Show("تمت عملية الحفظ");
                                txtCount.Text = "";
                                txtRequestNo.Text = "";
                                btnSaveRequest.IsEnabled = false;
                                cmbNotebookTypes.Text = "";
                                cmbProviderTypeRequestNotebook.Text = "";
                                dtpRequestDate.Text = "";
                                lblProviderName.Text = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show("المخزن غير كافي");
                        }
                    }

                }
                catch { }
            }
        }

        private void btnUpdateRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtRequestNo.Text == "")
                {
                    MessageBox.Show("ابحث  بكود الطلب الذى تريد تعديلة");
                }

                else
                {
                    NoteBookData obj = new NoteBookData();
                    obj.Order_Num = int.Parse(txtRequestNo.Text);
                    obj.ProvTypeCode = int.Parse(cmbProviderTypeRequestNotebook.SelectedValue.ToString());
                    obj.Prov_Code = int.Parse(listboxRequestNote.SelectedValue.ToString());
                    obj.Request_Date = dtpRequestDate.Text;
                    obj.Notebook_Type_Code = int.Parse(cmbNotebookTypes.SelectedValue.ToString());
                    obj.Batch_Count = int.Parse(txtCount.Text);
                    obj.Created_By = NameTab.Header.ToString();
                    int affected = note.UpdateNotebook_Request(obj, obj.Order_Num);
                    if (affected >= 1)
                    {
                        MessageBox.Show("تمت التحديث بنجاح");

                    }
                }
            }
            catch { }
        }

        private void btnDeleteRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtRequestNo.Text == "")
                {
                    MessageBox.Show("ابحث  بكود الطلب الذى تريد حذفة");
                }
                else
                {
                    int ReqNo = int.Parse(txtRequestNo.Text);
                    int affectrd = note.DeleteNotebook_Request(ReqNo);
                    if (affectrd >= 1)
                    {
                        MessageBoxResult result = MessageBox.Show("هل أنت متأكد ", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Yes);
                        if (result == MessageBoxResult.Yes)
                        {
                            MessageBox.Show("تمت عملية الحذف");
                            txtRequestNo.Text = "";
                            dtpRequestDate.Text = "";
                            cmbNotebookTypes.Text = "";
                            cmbProviderTypeRequestNotebook.Text = "";
                            txtProviderName.Text = "";
                            lblProviderName.Text = "";
                            listboxRequestNote.Items.Clear();
                            txtTransSearch.Text = "";
                            cmbNotebookTypes.SelectedItem = "";
                            txtCount.Text = "";
                            btnSaveRequest.IsEnabled = false;
                        }
                        else { }
                    }
                }
            }
            catch { }
        }

        /// <moving notebook>
        /// //////////////////////////////////moving notebook
        /// </moving notebook>

        private void MovingNoteBook_Loaded(object sender, RoutedEventArgs e)
        {
            //Date_panel.Visibility = Visibility.Hidden;
            //grbProviderNameSearch.Visibility = Visibility.Hidden;
            //deliverNoteGrid.Visibility = Visibility.Hidden;
            ////------------------fill provider type---------------
            //List<NoteBookData> ProviderType = note.SelectAllProviderTypes();
            //cmbProviderTypeDeliver.ItemsSource = ProviderType;
            //cmbProviderTypeDeliver.DisplayMemberPath = "Type_Name";
            //cmbProviderTypeDeliver.SelectedValuePath = "ProvTypeCode";

        }

        private void rdSearchByCompany_Checked(object sender, RoutedEventArgs e)
        {
            grbProviderNameSearch.Visibility = Visibility.Visible;
            Date_panel.Visibility = Visibility.Hidden;
            deliverNoteGrid.Visibility = Visibility.Visible;
        }

        private void rdSearchByDeliverDate_Checked(object sender, RoutedEventArgs e)
        {
            grbProviderNameSearch.Visibility = Visibility.Hidden;
            Date_panel.Visibility = Visibility.Visible;
            deliverNoteGrid.Visibility = Visibility.Visible;
        }

        private void cmbProviderTypeDeliver_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //------------------fill Provide Names--------------------
            try
            {
                int ProvideCode = int.Parse(cmbProviderTypeDeliver.SelectedValue.ToString());
                List<NoteBookData> ProviderNames = note.SelectAllProviderNames(ProvideCode);
                cmbProviderTypeDeliver_Copy.ItemsSource = ProviderNames;
                listboxDeliver.ItemsSource = ProviderNames;
                listboxDeliver.DisplayMemberPath = "Prov_Name";
                listboxDeliver.SelectedValuePath = "Prov_Code";
            }
            catch { }
        }

        private void listboxDeliver_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int ProvideCode = int.Parse(listboxDeliver.SelectedValue.ToString());
                int ProvType = int.Parse(cmbProviderTypeDeliver.SelectedValue.ToString());
                List<NoteBookData> list = note.SelectAllNotebook_ReportByCompany(ProvideCode, ProvType);
                deliverNoteGrid.ItemsSource = list;
                if (list == null)
                {
                    MessageBox.Show("لا توجد بيانات");
                    deliverNoteItmCounttxt.Content = "0";
                }
                else
                {
                    deliverNoteItmCounttxt.Content = list.Count.ToString();
                }
            }
            catch { }
        }

        private void txtProviderNameDeliver_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int ProvideCode = int.Parse(txtProviderNameDeliver.Text);
                string ProvideName = txtProviderNameDeliver.Text;
                int ProviderType = int.Parse(cmbProviderTypeDeliver.SelectedValue.ToString());

                List<NoteBookData> ProviderNames = note.SelectAllProviderNamesForSearch(ProvideCode, ProvideName, ProviderType);
                listboxDeliver.ItemsSource = ProviderNames;
                listboxDeliver.DisplayMemberPath = "Prov_Name";
                listboxDeliver.SelectedValuePath = "Prov_Code";
            }
            catch
            {
                int ProvideCode = -1;
                string ProvideName = txtProviderNameDeliver.Text;
                int ProviderType = int.Parse(cmbProviderTypeDeliver.SelectedValue.ToString());
                List<NoteBookData> ProviderNames = note.SelectAllProviderNamesForSearch(ProvideCode, ProvideName, ProviderType);
                listboxDeliver.ItemsSource = ProviderNames;
                listboxDeliver.DisplayMemberPath = "Prov_Name";
                listboxDeliver.SelectedValuePath = "Prov_Code";
            }
        }

        private void btnSearchByDate_Click(object sender, RoutedEventArgs e)
        {
            deliverNoteItmCounttxt.Content = "";
            string DateFrom = dtpFromDeliver.Text;
            string DateTo = dtpToDeliver.Text;

            try
            {
                List<NoteBookData> list = note.SelectAllNotebook_Report(DateFrom, DateTo);
                deliverNoteGrid.ItemsSource = list;
                deliverNoteItmCounttxt.Content = list.Count;

            }
            catch { }



        }
        #endregion

        #region network


        private void fill_card(ComboBox c, string card)
        {
            try
            {
                dataset_emp_cardzz = db.RunReaderds("select distinct  card_id ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH from COMP_EMPLOYEESS WHERE card_id LIKE '%" + card + "%' ORDER BY card_id ");
                c.ItemsSource = dataset_emp_cardzz.Tables[0].DefaultView;
            }
            catch { }
        }

        private void fill_card(ComboBox c, string card, string compid)
        {
            try
            {
                int contrid = hrnet.get_max_contract(Convert.ToInt32(compid));
                dataset_emp_cardzz = db.RunReaderds("select distinct  CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH from COMP_EMPLOYEESS WHERE card_id LIKE '%" + card + "%' and contract_no=" + contrid + " ORDER BY card_id ");
                c.ItemsSource = dataset_emp_cardzz.Tables[0].DefaultView;
            }
            catch { }
        }


        private void fill_pr(ComboBox c, int code)
        {
            try
            {
                DataSet prData = new DataSet();
                prData = db.RunReaderds("select distinct  pr_code ,pr_aname from serv_providers where prv_type=" + code + " ORDER BY pr_code ");
                c.ItemsSource = prData.Tables[0].DefaultView;
            }
            catch { }
        }

        private void fill_pr(ComboBox c, string name)
        {
            DataSet dataPr = new DataSet();
            dataPr = db.RunReaderds("select distinct pr_code, pr_aname from serv_providers where pr_code LIKE '%" + name + "%' or pr_aname LIKE '%" + name + "%'");
            c.ItemsSource = dataPr.Tables[0].DefaultView;

        }


        private void srchNetwork_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string provider = "";
                string area = "";
                string degree = "";
                int area_code = 0;
                int provider_code = 0;
                string card = "";
                bool test = false;

                int compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                if (UserType == "hr")
                {

                    if (classrb.IsChecked == true)
                    {

                        string selected = networkClassCodeCombo.SelectedItem.ToString();
                        string[] arr = selected.Split(new char[] { '"', '|', '"' });
                        string classcode = arr[0];
                        degree = hrnet.get_hospital_degree(compid, classcode);
                        test = true;
                        networkcardcombo_Copy.Visibility = Visibility.Hidden;
                        imgsearch1.Visibility = Visibility.Hidden;
                        networkcardcombo.Visibility = Visibility.Hidden;
                        imgsearch1_Copy.Visibility = Visibility.Hidden;
                    }
                    else if (cardrb.IsChecked == true)
                    {
                        networkcardcombo_Copy.Visibility = Visibility.Visible;
                        networkcardcombo_Copy.IsEnabled = false;
                        networkcardcombo_Copy.Text = User.CompanyName;
                        imgsearch1.Visibility = Visibility.Visible;
                        imgsearch1.IsEnabled = false;
                        networkcardcombo.Visibility = Visibility.Visible;
                        imgsearch1_Copy.Visibility = Visibility.Visible;
                        string cardtxt = networkcardcombo.Text.ToString();

                        string[] arr = cardtxt.Split('-');
                        card = arr[0];
                        if (card == compid.ToString())
                        {
                            degree = client.get_degree(cardtxt);
                            test = true;
                        }
                        else
                        {
                            MessageBox.Show("غير مسموح");
                            test = false;
                        }
                    }
                }
                else
                {

                    if (networkcardcombo.Text == "" || networkcardcombo_Copy.Text == "")
                    { }

                    else
                    {

                        card = networkcardcombo.Text.ToString();
                        degree = client.get_degree(card);
                        test = true;
                    }
                }
                if (governComboNetwork.SelectedItem == null && test == true)
                {
                    MessageBox.Show("اختر المحافظة");
                }
                else if (governComboNetwork.SelectedItem != null && test == true)
                {
                    string city = governComboNetwork.SelectedItem.ToString();
                    int bs_code = client.get_bsCode(city);
                    if (areaComboNetwork.SelectedItem == null)
                    {
                        MessageBox.Show("من فضلك اختر المنطقة");
                    }
                    else
                    {
                        if (providerComboNetwork.SelectedItem == null)
                        {
                            MessageBox.Show("اختر مقدم الخدمة");
                        }
                        else
                        {
                            provider = providerComboNetwork.SelectedItem.ToString();
                            area = areaComboNetwork.SelectedItem.ToString();
                            area_code = client.get_area_code(area);
                            provider_code = client.get_provider_code(provider);
                            if (provider_code == 5)
                            {
                                string doc = docSpecComboNetwork.SelectedItem.ToString();
                                int doc_code = client.get_doc_code(doc);
                                System.Data.DataTable doctorDT = client.get_doc_by_area(doc_code, area_code, degree);
                                if (doctorDT.Rows.Count == 0)
                                {
                                    MessageBox.Show("لا توجد نتائج");

                                    newtworkCounttxt.Content = doctorDT.Rows.Count.ToString();
                                }
                                else
                                {
                                    NetworkGrid.Visibility = Visibility.Visible;

                                    newtworkCounttxt.Content = doctorDT.Rows.Count.ToString();

                                    NetworkGrid.ItemsSource = doctorDT.DefaultView;
                                }
                            }
                            else if (provider_code == 1)
                            {
                                System.Data.DataTable hospitalDT = client.get_hospital(area_code, degree);
                                if (hospitalDT.Rows.Count == 0)
                                {
                                    MessageBox.Show("لا توجد نتائج");

                                    newtworkCounttxt.Content = hospitalDT.Rows.Count.ToString();
                                }
                                else
                                {
                                    NetworkGrid.Visibility = Visibility.Visible;

                                    newtworkCounttxt.Content = hospitalDT.Rows.Count.ToString();
                                    NetworkGrid.ItemsSource = hospitalDT.DefaultView;
                                }
                            }
                            else if (provider_code == 2)
                            {
                                System.Data.DataTable pharmaDT = client.get_pharmacy(area_code, degree);
                                if (pharmaDT.Rows.Count == 0)
                                {
                                    MessageBox.Show("لا توجد نتائج");

                                    newtworkCounttxt.Content = pharmaDT.Rows.Count.ToString();
                                }
                                else
                                {
                                    NetworkGrid.Visibility = Visibility.Visible;

                                    newtworkCounttxt.Content = pharmaDT.Rows.Count.ToString();

                                    NetworkGrid.ItemsSource = pharmaDT.DefaultView;

                                }
                            }

                            else if (provider_code == 3)
                            {
                                System.Data.DataTable lab = client.get_lab(area_code, degree);
                                if (lab.Rows.Count == 0)
                                {
                                    MessageBox.Show("لا توجد نتائج");

                                    newtworkCounttxt.Content = lab.Rows.Count.ToString();
                                }
                                else
                                {
                                    NetworkGrid.Visibility = Visibility.Visible;

                                    newtworkCounttxt.Content = lab.Rows.Count.ToString();
                                    NetworkGrid.ItemsSource = lab.DefaultView;
                                }
                            }
                            else if (provider_code == 4)
                            {
                                System.Data.DataTable ray = client.get_ray(area_code, degree);
                                if (ray.Rows.Count == 0)
                                {
                                    MessageBox.Show("لا توجد نتائج");

                                    newtworkCounttxt.Content = ray.Rows.Count.ToString();
                                }
                                else
                                {
                                    NetworkGrid.Visibility = Visibility.Visible;

                                    newtworkCounttxt.Content = ray.Rows.Count.ToString();
                                    NetworkGrid.ItemsSource = ray.DefaultView;
                                }
                            }
                            else if (provider_code == 6)
                            {
                                System.Data.DataTable ph_therapy = client.get_ph_therapy(area_code, degree);
                                if (ph_therapy.Rows.Count == 0)
                                {
                                    MessageBox.Show("لا توجد نتائج");

                                    newtworkCounttxt.Content = ph_therapy.Rows.Count.ToString();
                                }
                                else
                                {
                                    NetworkGrid.Visibility = Visibility.Visible;
                                    NetworkGrid.ItemsSource = ph_therapy.DefaultView;

                                    newtworkCounttxt.Content = ph_therapy.Rows.Count.ToString();
                                }
                            }
                            else if (provider_code == 7)
                            {
                                System.Data.DataTable dentistDT = client.get_dentist(area_code, degree);
                                if (dentistDT.Rows.Count == 0)
                                {
                                    MessageBox.Show("لا توجد نتائج");

                                    newtworkCounttxt.Content = dentistDT.Rows.Count.ToString();
                                }
                                else
                                {
                                    NetworkGrid.Visibility = Visibility.Visible;

                                    newtworkCounttxt.Content = dentistDT.Rows.Count.ToString();
                                    NetworkGrid.ItemsSource = dentistDT.DefaultView;
                                }
                            }
                            else if (provider_code == 8)
                            {
                                System.Data.DataTable opticDT = client.get_optic(area_code, degree);
                                if (opticDT.Rows.Count == 0)
                                {
                                    MessageBox.Show("لا توجد نتائج");

                                    newtworkCounttxt.Content = opticDT.Rows.Count.ToString();
                                }
                                else
                                {
                                    NetworkGrid.Visibility = Visibility.Visible;

                                    newtworkCounttxt.Content = opticDT.Rows.Count.ToString();
                                    NetworkGrid.ItemsSource = opticDT.DefaultView;
                                }
                            }
                            else if (provider_code == 9)
                            {
                                System.Data.DataTable polyDT = client.get_polyclinic(area_code, degree);
                                if (polyDT.Rows.Count == 0)
                                {
                                    MessageBox.Show("لا توجد نتائج");

                                    newtworkCounttxt.Content = polyDT.Rows.Count.ToString();
                                }
                                else
                                {
                                    NetworkGrid.Visibility = Visibility.Visible;
                                    NetworkGrid.ItemsSource = polyDT.DefaultView;

                                    newtworkCounttxt.Content = polyDT.Rows.Count.ToString();
                                }
                            }
                        }
                    }
                }

            }
            catch { }


        }

        private void NetworkTab_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void governComboNetwork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string city = governComboNetwork.SelectedItem.ToString();
                int code = client.get_bsCode(city);
                System.Data.DataTable areaDT = client.get_curr_area(code);
                areaComboNetwork.Items.Clear();
                for (int i = 0; i < areaDT.Rows.Count; i++)
                {
                    areaComboNetwork.Items.Add(areaDT.Rows[i].ItemArray[0].ToString());
                }
            }
            catch { }
        }

        private void providerComboNetwork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string provider = providerComboNetwork.SelectedItem.ToString();
                int code = client.get_provider_code(provider);
                if (code == 5)
                {
                    lblSpec.Visibility = Visibility.Visible;
                    docSpecComboNetwork.Visibility = Visibility.Visible;
                    System.Data.DataTable docDT = client.get_doctor_spec();
                    docSpecComboNetwork.Items.Clear();
                    for (int i = 0; i < docDT.Rows.Count; i++)
                    {
                        docSpecComboNetwork.Items.Add(docDT.Rows[i].ItemArray[0].ToString());
                    }
                }
                else
                {
                    lblSpec.Visibility = Visibility.Hidden;
                    docSpecComboNetwork.Visibility = Visibility.Hidden;
                }
            }
            catch { }

        }

        private void areaComboNetwork_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion
        private void groupByCombo_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
        private void cardTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        public static int company_id;
        public static int contr_id;

        private void CompanyContrType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void COmpanyGridDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                for (int i = 0; i < COmpanyGridDetails.SelectedItems.Count; i++)
                {
                    System.Data.DataRowView dr = (System.Data.DataRowView)COmpanyGridDetails.SelectedItems[i];
                    contr_id = Convert.ToInt32(dr.Row.ItemArray[0].ToString());
                }
                companyContractNumtxt.Text = contr_id.ToString();
            }
            catch { }
        }

        private void CompanySaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ename = "";
                string aname = "";
                string contrType = "";
                string contrLong = "";

                if ((((ComboBoxItem)CompanyContrType.SelectedItem)) == null || COmpanyGridDetails.SelectedItems.Count == 0 || (((ComboBoxItem)CompanyContrLong.SelectedItem)) == null || contr_id == 0 || company_id == 0)
                {
                    MessageBox.Show("اختر الشركة");
                }
                else
                {
                    for (int i = 0; i < COmpanyGridDetails.SelectedItems.Count; i++)
                    {
                        System.Data.DataRowView dr = (System.Data.DataRowView)COmpanyGridDetails.SelectedItems[i];
                        aname = dr.Row.ItemArray[2].ToString();
                        ename = (dr.Row.ItemArray[3].ToString());
                    }
                    contrType = (((ComboBoxItem)CompanyContrType.SelectedItem).Content).ToString();
                    contrLong = (((ComboBoxItem)CompanyContrLong.SelectedItem).Content).ToString();
                    contract.add_company_contract(contr_id, company_id, contrType, contrLong, ename, aname, image_paths_company);
                    MessageBox.Show(" تم حفظ العقد");
                }
            }
            catch { }
        }
        public List<string> get_company_path()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                op.Multiselect = true;
                List<string> filenames = new List<string>();
                string[] arr;
                if (op.ShowDialog() == true)
                {

                    arr = op.FileNames;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        filenames.Add(arr[i]);
                    }

                }

                image_paths_company = new List<string>();
                for (int i = 0; i < 20; i++)
                {
                    image_paths_company.Add("null");
                }
                string directory = "C:\\New folder\\";
                for (int i = 0; i < filenames.Count; i++)
                {
                    System.IO.File.Copy(filenames[i], directory + System.IO.Path.GetFileName(filenames[i].ToString()));
                    image_paths_company[i] = directory + System.IO.Path.GetFileName(filenames[i].ToString());
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return image_paths_company;
        }

        private void CompanyattachBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (((ComboBoxItem)CompanyContrLong.SelectedItem != null) && ((ComboBoxItem)CompanyContrType.SelectedItem != null))
                {
                    get_company_path();
                    string imgname = "";
                    for (int i = 0; i < image_paths_company.Count; i++)
                    {
                        int j = i + 1;
                        imgname = "CompanyContract" + j;
                        if (imgname == this.CompanyContract1.Name)
                        {
                            CompanyContract1.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));

                        }
                        else if (imgname == this.CompanyContract2.Name)
                        {
                            CompanyContract2.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract3.Name)
                        {
                            CompanyContract3.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract4.Name)
                        {
                            CompanyContract4.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract5.Name)
                        {
                            CompanyContract5.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract6.Name)
                        {
                            CompanyContract6.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract7.Name)
                        {
                            CompanyContract7.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract8.Name)
                        {
                            CompanyContract8.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract9.Name)
                        {
                            CompanyContract9.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract10.Name)
                        {
                            CompanyContract10.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract11.Name)
                        {
                            CompanyContract11.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract12.Name)
                        {
                            CompanyContract12.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract13.Name)
                        {
                            CompanyContract13.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract14.Name)
                        {
                            CompanyContract14.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract15.Name)
                        {
                            CompanyContract15.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract16.Name)
                        {
                            CompanyContract16.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract17.Name)
                        {
                            CompanyContract17.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract18.Name)
                        {
                            CompanyContract18.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract19.Name)
                        {
                            CompanyContract19.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                        else if (imgname == this.CompanyContract20.Name)
                        {
                            CompanyContract20.Source = new BitmapImage(new Uri(image_paths_company[i].ToString(), UriKind.RelativeOrAbsolute));
                        }
                    }
                }
                else
                    MessageBox.Show("من فضلك حدد نوع العقد ونوع نسخة العقد");

                CompanySaveBtn.IsEnabled = true;
            }
            catch { }
        }

        private void newMessBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //------select maxMessenger----------------------
                string affected2 = mes.SelectMaxMessId();
                int aff = int.Parse(affected2.ToString());
                int Next = (aff + 1);
                messangerCodetxt.Text = Next.ToString();
                messangerNametxt.Text = "";
                messangerPasstxt.Text = "";
                messangerUserNametxt.Text = "";
                messangerIDtxt.Text = "";
                SrchCodeMessReg.Text = "";
                MessangerTypCombo.Text = "";
                //address_txt_reg_mandob.Text = "";
                messangerBDate.Text = "";
              //  refrashmess();
                saveMessBtn.IsEnabled = true;
                editMessBtn.IsEnabled = false;
                delMessBtn.IsEnabled = false;
            }
            catch { }
        }

        private void saveMessBtn_Click(object sender, RoutedEventArgs e)
        {
            #region valid
            bool valid = false;
            if (messangerIDtxt.Text != ""
                    && messangerCodetxt.Text != "" && messangerNametxt.Text != ""
                    && messangerUserNametxt.Text != "" && messangerPasstxt.Text != ""
                        && messangerBDate.Text != "")
            {
                valid = true;
            }
            #endregion
            if (valid == true)
            {
                if (messangerIDtxt.Text.Length == 14)
                {
                    try
                    {
                        Int64 cardNum = Convert.ToInt64(messangerIDtxt.Text);
                        MessangerData obj = new MessangerData();
                        obj.Id = int.Parse(messangerCodetxt.Text);
                        obj.Name = messangerNametxt.Text;
                        obj.UserName = messangerUserNametxt.Text;
                        obj.Password = messangerPasstxt.Text;
                        obj.Type = (((ComboBoxItem)MessangerTypCombo.SelectedItem).Content).ToString();
                        obj.Address = new TextRange(messangeraddrtxt.Document.ContentStart, messangeraddrtxt.Document.ContentEnd).Text;
                        obj.DateOfBirth = messangerBDate.Text;
                        obj.CardNum = cardNum.ToString();

                        //--------check if this messenger already exist------------------
                        MessangerData OldMessenger = mes.SelectMessengerById(obj.Id);
                        if (OldMessenger != null)
                        {
                            MessageBox.Show("موجود بالفعل");
                        }
                        else            //----insert the new messenger
                        {
                            int affected = mes.InsertMessanger(obj);
                            if (affected > 0)
                            {
                                MessageBox.Show("تم الحفظ بنجاح", "Sucess");
                                #region SelectMaxId
                                string affected2 = mes.SelectMaxMessId();
                                if (affected2 == "")
                                {

                                }
                                else
                                {
                                    //-------clear selection mode at first time when load 
                                    //-------Fill  DataGridView--------------
                                    try
                                    {
                                        List<MessangerData> list = mes.SelectAllMessengers();
                                        messGrid.ItemsSource = list;
                                        messangerReqItemCount.Content = "Items Count : " + list.Count.ToString();
                                        //dataGridView1.Rows[0].Cells[0].Selected = false;
                                        messGrid.SelectedCells.Clear();
                                        messangerNametxt.Text = "";
                                        messangerUserNametxt.Text = "";
                                        messangerPasstxt.Text = "";
                                        messangerBDate.Text = "";
                                        MessangerTypCombo.Text = "";
                                    }
                                    catch
                                    {
                                        messangerNametxt.Text = "";
                                        messangerUserNametxt.Text = "";
                                        messangerPasstxt.Text = "";
                                    }
                                    //------select maxMessenger----------------------
                                    int aff = int.Parse(affected2.ToString());
                                    int Next = (aff + 1);
                                    messangerCodetxt.Text = Next.ToString();

                                }
                                #endregion

                            }
                            else
                            {
                                MessageBox.Show("رجاء ، اسم المستخدم مكرر ادخل اسم جديد", "Fail");
                            }
                            messangerNametxt.Text = "";
                            messangerPasstxt.Text = "";
                            messangerUserNametxt.Text = "";
                            messangerIDtxt.Text = "";
                            messangeraddrtxt.Document.Blocks.Clear();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("رقم البطاقة غير صالح");
                    }
                }

                else
                {
                    MessageBox.Show("ادخل 14 رقم للبطاقة من فضلك");
                }


            }

            else
            {
                MessageBox.Show("املا كل البيانات من فضلك");
            }
        }

        private void editMessBtn_Click(object sender, RoutedEventArgs e)
        {
            //--------check if this messenger already exist------------------
            if (messangerIDtxt.Text.Length == 14)
            {
                try
                {
                    Int64 cardNum = Int64.Parse(messangerIDtxt.Text);
                    MessangerData obj = new MessangerData();
                    obj.Id = int.Parse(messangerCodetxt.Text);
                    obj.Name = messangerNametxt.Text;
                    obj.UserName = messangerUserNametxt.Text;
                    obj.Password = messangerPasstxt.Text;
                    obj.Type = (((ComboBoxItem)MessangerTypCombo.SelectedItem).Content).ToString();
                    obj.Address = new TextRange(messangeraddrtxt.Document.ContentStart, messangeraddrtxt.Document.ContentEnd).Text;
                    obj.DateOfBirth = messangerBDate.Text;
                    obj.CardNum = cardNum.ToString();
                    MessangerData OldMessenger = mes.SelectMessengerById(obj.Id);
                    if (obj.Id == OldMessenger.Id)//---means that it is exist so update it
                    {
                        int affected = mes.UpdateMessenger(obj, obj.Id);
                        if (affected > 0)
                        {
                            MessageBox.Show("تم التحديث بنجاح", "Success");
                            //------ To Reflect on The cmb-------------//
                            List<MessangerData> list = mes.SelectAllMessengers();
                            messGrid.ItemsSource = list;
                            messangerReqItemCount.Content = "Items Count : " + list.Count.ToString();
                        }
                    }
                }
                catch
                {

                    MessageBox.Show("رقم البطاقة غير صالح");
                }
            }
            else
            {
                MessageBox.Show("ادخل 14 رقم للبطاقة");
            }
        }

        private void delMessBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(messangerCodetxt.Text);
                int affected = mes.DeleteMessenger(id);
                if (affected > 0)
                {
                    MessageBoxResult result = MessageBox.Show("هل انت متأكد ؟", "Delete", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Yes);
                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("تمت عملية الحذف بنجاح", "Success");
                        //------To Reflect on The cmb---------------------//
                        List<MessangerData> list = mes.SelectAllMessengers();
                        messGrid.ItemsSource = list;
                        messangerReqItemCount.Content = "Items Count : " + list.Count.ToString();
                        //-------select Max_Messenger----------------------//
                        string max = mes.SelectMaxMessId();
                        int max2 = int.Parse(max) + 1;
                        messangerCodetxt.Text = max2.ToString();
                        //-----------To Clear TextBoxes-------------------//
                        messangerNametxt.Text = "";
                        messangerUserNametxt.Text = "";
                        messangerPasstxt.Text = "";
                        messangeraddrtxt.Document.Blocks.Clear();
                        messangerBDate.Text = "";
                        MessangerTypCombo.Text = "";
                        messangerIDtxt.Text = "";
                    }
                    else
                    { }
                }

            }
            catch
            {
                messGrid.ItemsSource = null;
                messangerNametxt.Text = "";
                messangerUserNametxt.Text = "";
                messangerPasstxt.Text = "";
                messangeraddrtxt.Document.Blocks.Clear();
            }
        }

        DataSet dataset_emp_cardzz = new DataSet();
        private void fill_card(ComboBox c, int compid)
        {
            try
            {
                int contrid = hrnet.get_max_contract(compid);
                dataset_emp_cardzz = db.RunReaderds("select distinct   CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE C_COMP_ID=" + compid + " and contract_no=" + contrid + " ORDER BY CARD_ID ");
                c.ItemsSource = dataset_emp_cardzz.Tables[0].DefaultView;
            }
            catch { }
        }

        private void fill_comp(ComboBox c)
        {
            try
            {
                DataSet company_dataSet = new DataSet();
                company_dataSet = db.RunReaderds("select distinct C_COMP_ID ,C_ANAME from V_COMPANIES order by c_comp_id");
                c.ItemsSource = company_dataSet.Tables[0].DefaultView;
            }
            catch { }
        }

        private void fill_comp(ComboBox c, string name)
        {
            try
            {
                DataSet compData = db.RunReaderds("select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + name.ToUpper() + "%' or upper(C_ANAME) LIKE '%" + name.ToUpper() + "%'  ORDER BY C_COMP_ID ");
                c.ItemsSource = compData.Tables[0].DefaultView;
            }
            catch { }
        }

        private void messGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (messGrid.SelectedItems.Count >= 1)
                {
                    object item = messGrid.SelectedItem;
                    int id = Convert.ToInt32((messGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    MessangerData obj = mes.SelectMessengerById(id);
                    messangerCodetxt.Text = obj.Id.ToString();
                    messangerNametxt.Text = obj.Name;
                    messangerUserNametxt.Text = obj.UserName;
                    messangerPasstxt.Text = obj.Password;
                    string txt = new TextRange(messangeraddrtxt.Document.ContentStart, messangeraddrtxt.Document.ContentEnd).Text;
                    txt = obj.Address;
                    messangeraddrtxt.Document.Blocks.Clear();
                    messangeraddrtxt.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(txt)));
                    MessangerTypCombo.Text = obj.Type;
                    messangerBDate.Text = obj.DateOfBirth.ToString();
                    messangerIDtxt.Text = obj.CardNum.ToString();



                }
            }
            catch
            {
            }
        }


        private void messReqNewBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SrchCodeMessReq.Text = "";
                messangerRequestreasontxt.Text = "";
                string affected2 = req.SelectMaxReqMessId();
                int aff = int.Parse(affected2.ToString());
                int Next = (aff + 1);
                messReqCodetxt.Text = Next.ToString();
                #region clearAll
                cbxcompcomp.Text = "";
                messReqCityCombo.Text = "";
                messReqareaCombo.Text = "";
                messReqbranchCombo.Text = "";
                // messReqDate.Text = "";
                messReqContactPersontxt.Text = "";
                // messReqDeptxt.Text = "";
                messReqothertxt.Document.Blocks.Clear();
                messReqphonetxt.Text = "";
                messReqaddrtxt.Text = "";
                messangerIDtxt.Text = "";
                chkReadyCardsReson.IsChecked = false;
                chkReadyCheek.IsChecked = false;
                chkDeliverPaper.IsChecked = false;
                chkOtherResons.IsChecked = false;
                chkReqResonDeliverContracts.IsChecked = false;
                chkReqResonReceiveCheek.IsChecked = false;
                chkReqResonReceiveContracts.IsChecked = false;
                chkReqResonReceiveEmpData.IsChecked = false;
                chkReqResonSMS.IsChecked = false;
                lblCompName.Content = "";
                messReqMessTypeCombo.Text = "";
                #endregion
                // -------clear selection mode at first time when load ---------------
                messReqGrid.SelectedCells.Clear();
            }
            catch
            {
                //txtReqCode.Text = "1";
                string affected2 = req.SelectMaxReqMessId();
                int aff = int.Parse(affected2.ToString());
                int Next = (aff + 1);
                messReqCodetxt.Text = Next.ToString();
            }
        }

        private void messReqSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //dataGridView1.Columns["Governorate_Code"].Visible = false;
            }
            catch { }


            bool valid = false;
            #region test
            if (messReqCodetxt.Text != "" && messReqContactPersontxt.Text != "")
            {
                valid = true;
            }


            #endregion

            #region valid
            //if (txtReqCode.Text != "" && cmbGoverneratorNames.Text != "" &&txtContactPerson.Text!=""  && cmbBranchNames.Text != "" &&cmbMessengerType.Text!="" && cmcbAreaInGovernerate.Text != "")
            //{
            //    valid = true;
            //}
            #endregion
            if (valid == true)
            {
                MessengerRequestData obj = new MessengerRequestData();
                obj.ReqCode = int.Parse(messReqCodetxt.Text);
                obj.Governorate_Name = messReqCityCombo.Text.ToString();
                obj.CompanyName = lblCompName.Content.ToString();
                obj.Branch = messReqbranchCombo.Text.ToString();
                obj.Area = messReqareaCombo.Text.ToString();
                obj.Address = messReqaddrtxt.Text.ToString();
                obj.Dept = messReqDeptxt.Text.ToString();
                obj.ContactPerson = messReqContactPersontxt.Text.ToString();
                obj.Phone = messReqphonetxt.Text;
                obj.Date = messReqDate.Text.ToString();
                obj.MessengerType = (((ComboBoxItem)messReqMessTypeCombo.SelectedItem).Content).ToString();

                if (chkReadyCardsReson.IsChecked == true)
                {
                    obj.RequestResons += chkReadyCardsReson.Content + " - ";
                }
                if (chkReadyCheek.IsChecked == true)
                {
                    obj.RequestResons += chkReadyCheek.Content + " - ";
                }
                if (chkDeliverPaper.IsChecked == true)
                {
                    obj.RequestResons += chkDeliverPaper.Content + " - ";
                }
                string txt = new TextRange(messReqothertxt.Document.ContentStart, messReqothertxt.Document.ContentEnd).Text.ToString();
                if (chkOtherResons.IsChecked == true)
                {
                    obj.RequestResons += txt + " - ";
                }

                if (chkReqResonReceiveCheek.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonReceiveCheek.Content + " - ";
                }
                if (chkReqResonDeliverContracts.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonDeliverContracts.Content + " - ";
                }
                if (chkReqResonReceiveEmpData.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonReceiveEmpData.Content + " - ";
                }
                if (chkReqResonReceiveContracts.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonReceiveContracts.Content + " - ";
                }
                if (chkReqResonSMS.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonSMS.Content + " - ";
                }
                if (deliverIndemnityChk.IsChecked == true)
                {
                    obj.RequestResons += deliverIndemnityChk.Content + " - ";
                }
                if (receiveIndemnityChk.IsChecked == true)
                {
                    obj.RequestResons += receiveIndemnityChk.Content;
                }


                if (chkVIP.IsChecked == true)
                {
                    obj.VIP = "1";
                }
                else
                {
                    obj.VIP = "0";
                }

                //--------check if this messenger already exist------------------
                MessengerRequestData OldMessenger = req.SelectMessengerRequestById(obj.ReqCode);
                if (OldMessenger != null)
                {
                    MessageBox.Show("موجود بالفعل");
                }
                else            //----insert the new messenger
                {
                    int affected = req.InsertMessangerRequest(obj);
                    if (affected > 0)
                    {
                        //messReqGrid.Columns["RequestResons"].Width = 300;
                        MessageBox.Show("تم الحفظ بنجاح", "Sucess");
                        //------To Reflect on The cmb---------------------//
                        List<MessengerRequestData> list = req.SelectAllMessengersRequests();
                        messReqGrid.ItemsSource = list;
                        messangerRequestItemCount.Content = "Items Count : " + list.Count.ToString();
                        #region clearAll
                        cbxcompcomp.Text = "";
                        messReqCityCombo.Text = "";
                        messReqareaCombo.Text = "";
                        messReqbranchCombo.Text = "";
                        messReqDate.Text = "";
                        messReqContactPersontxt.Text = "";
                        messReqDeptxt.Text = "";
                        messReqothertxt.Document.Blocks.Clear();
                        messReqphonetxt.Text = "";
                        messReqaddrtxt.Text = "";
                        messangerIDtxt.Text = "";
                        chkReadyCardsReson.IsChecked = false;
                        receiveIndemnityChk.IsChecked = false;
                        messangerRequestreasontxt.Text = "";
                        chkReadyCheek.IsChecked = false;
                        chkDeliverPaper.IsChecked = false;
                        chkOtherResons.IsChecked = false;
                        chkReqResonDeliverContracts.IsChecked = false;
                        chkReqResonReceiveCheek.IsChecked = false;
                        chkReqResonReceiveContracts.IsChecked = false;
                        chkReqResonReceiveEmpData.IsChecked = false;
                        chkReqResonSMS.IsChecked = false;
                        //lblCompanyName.Content = "";
                        messReqMessTypeCombo.Text = "";
                        #endregion
                        #region SelectMaxId
                        string affected2 = req.SelectMaxReqMessId();
                        if (affected2 == "")
                        {

                        }
                        else
                        {
                            //-------clear selection mode at first time when load 
                            List<MessengerRequestData> list2 = req.SelectAllMessengersRequests();
                            messReqGrid.ItemsSource = list2;
                            //dataGridView1.Rows[0].Cells[0].Selected = false;
                            //dataGridView1.Rows[1].Cells[0].Selected = false;
                            //txtReqCode.Text = ""; cmbGoverneratorNames.Text = ""; txtCompanyName.Text = ""; cmbBranchNames.Text = ""; cmcbAreaInGovernerate.Text = "";
                            ////------select maxMessenger----------------------
                            int aff = int.Parse(affected2.ToString());
                            int Next = (aff + 1);
                            messReqCodetxt.Text = Next.ToString();
                        }
                        #endregion
                        try
                        {
                            //dataGridView1.Columns[3].Visible = false;
                            //dataGridView1.Columns[4].Visible = false;
                        }
                        catch { }

                    }
                    else
                    {
                        MessageBox.Show("رجاء ، اسم المستخدم مكرر ادخل اسم جديد", "Fail");
                    }
                }
            }

            else
            {
                MessageBox.Show("املا كل البيانات من فضلك");
            }
        }

        private void messReqEditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //--------check if this messenger already exist------------------
                MessengerRequestData obj = new MessengerRequestData();
                obj.ReqCode = int.Parse(messReqCodetxt.Text);
                obj.Governorate_Name = messReqCityCombo.Text.ToString();
                obj.CompanyName = lblCompName.Content.ToString();
                if (messReqbranchCombo.Text == null || messReqbranchCombo.Text == "")
                {
                    obj.Branch = "";
                }
                else
                {
                    obj.Branch = messReqbranchCombo.Text.ToString();
                }
                obj.Area = messReqareaCombo.Text.ToString();
                obj.Address = messReqaddrtxt.Text;
                obj.Dept = messReqDeptxt.Text.ToString();
                obj.ContactPerson = messReqContactPersontxt.Text;
                obj.Phone = messReqphonetxt.Text;
                obj.Date = messReqDate.Text.ToString();
                obj.MessengerType = (((ComboBoxItem)messReqMessTypeCombo.SelectedItem).Content).ToString();
                if (chkReadyCardsReson.IsChecked == true)
                {
                    obj.RequestResons += chkReadyCardsReson.Content + " - ";
                }
                if (chkReadyCheek.IsChecked == true)
                {
                    obj.RequestResons += chkReadyCheek.Content + " - ";
                }
                if (chkDeliverPaper.IsChecked == true)
                {
                    obj.RequestResons += chkDeliverPaper.Content + " - ";
                }
                string txt = new TextRange(messReqothertxt.Document.ContentStart, messReqothertxt.Document.ContentEnd).Text.ToString();
                if (chkOtherResons.IsChecked == true)
                {
                    obj.RequestResons += txt + " - ";
                }

                if (chkReqResonReceiveCheek.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonReceiveCheek.Content + " - ";
                }
                if (chkReqResonDeliverContracts.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonDeliverContracts.Content + " - ";
                }
                if (chkReqResonReceiveEmpData.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonReceiveEmpData.Content + " - ";
                }
                if (chkReqResonReceiveContracts.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonReceiveContracts.Content + " - ";
                }
                if (chkReqResonSMS.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonSMS.Content;
                }
                if (chkVIP.IsChecked == true)
                {
                    obj.VIP = "1";
                }
                else
                {
                    obj.VIP = "0";
                }

                MessengerRequestData OldMessenger = req.SelectMessengerRequestById(obj.ReqCode);
                if (OldMessenger != null)
                {
                    if (obj.ReqCode == OldMessenger.ReqCode)//---means that it is exist so update it
                    {
                        int affected = req.UpdateMessengerRequest(obj, obj.ReqCode);
                        if (affected > 0)
                        {
                            MessageBox.Show("تم التحديث بنجاح", "Success");
                            #region clearAll
                            cbxcompcomp.Text = "";
                            messReqCityCombo.Text = "";
                            messReqareaCombo.Text = "";
                            messReqbranchCombo.Text = "";
                            messReqDate.Text = "";
                            messReqContactPersontxt.Text = "";
                            messReqDeptxt.Text = "";
                            messReqothertxt.Document.Blocks.Clear();
                            messReqphonetxt.Text = "";
                            messReqaddrtxt.Text = "";
                            messangerIDtxt.Text = "";
                            chkReadyCardsReson.IsChecked = false;
                            chkReadyCheek.IsChecked = false;
                            chkDeliverPaper.IsChecked = false;
                            chkOtherResons.IsChecked = false;
                            chkReqResonDeliverContracts.IsChecked = false;
                            chkReqResonReceiveCheek.IsChecked = false;
                            chkReqResonReceiveContracts.IsChecked = false;
                            chkReqResonReceiveEmpData.IsChecked = false;
                            chkReqResonSMS.IsChecked = false;
                            lblCompName.Content = "";
                            messReqMessTypeCombo.Text = "";
                            #endregion
                            #region SelectMaxId
                            string affected2 = req.SelectMaxReqMessId();
                            if (affected2 == "")
                            {

                            }
                            else
                            {
                                //-------clear selection mode at first time when load 
                                List<MessengerRequestData> list2 = req.SelectAllMessengersRequests();
                                messReqGrid.ItemsSource = list2;
                                messangerRequestItemCount.Content = "Items Count : " + list2.Count.ToString();
                                try
                                {
                                    messReqGrid.SelectedCells.Clear();
                                }
                                catch { }
                                //txtReqCode.Text = ""; cmbGoverneratorNames.Text = ""; txtCompanyName.Text = ""; cmbBranchNames.Text = ""; cmcbAreaInGovernerate.Text = "";
                                ////------select maxMessenger----------------------
                                int aff = int.Parse(affected2.ToString());
                                int Next = (aff + 1);
                                messReqCodetxt.Text = Next.ToString();

                            }
                            #endregion
                            //------ To Reflect on The cmb-------------//

                            List<MessengerRequestData> list = req.SelectAllMessengersRequests();
                            messReqGrid.ItemsSource = list;
                            messangerRequestItemCount.Content = "Items Count : " + list.Count.ToString();
                        }
                    }
                }
            }
            catch { }
        }

        private void messReqDelBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(messReqCodetxt.Text);
                int affected = req.DeleteMessengerRequest(id);
                if (affected > 0)
                {
                    MessageBoxResult result = MessageBox.Show("هل انت متأكد ؟", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Yes);
                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("تمت عملية الحذف بنجاح", "Success");
                        //------To Reflect on The cmb---------------------//
                        List<MessengerRequestData> list = req.SelectAllMessengersRequests();
                        messReqGrid.ItemsSource = list;
                        messangerRequestItemCount.Content = "Items Count : " + list.Count.ToString();
                        ////-------select Max_Messenger----------------------//
                        string max = req.SelectMaxReqMessId();
                        try
                        {
                            int max2 = int.Parse(max) + 1;
                            messReqCodetxt.Text = max2.ToString();
                        }
                        catch { }
                        //-----------To Clear TextBoxes-------------------//
                        #region clearAll
                        cbxcompcomp.Text = "";
                        messReqCityCombo.Text = "";
                        messReqareaCombo.Text = "";
                        messReqbranchCombo.Text = "";
                        messReqDate.Text = "";
                        messReqContactPersontxt.Text = "";
                        messReqDeptxt.Text = "";
                        messReqothertxt.Document.Blocks.Clear();
                        messReqphonetxt.Text = "";
                        messReqaddrtxt.Text = "";
                        messangerIDtxt.Text = "";
                        chkReadyCardsReson.IsChecked = false;
                        chkReadyCheek.IsChecked = false;
                        chkDeliverPaper.IsChecked = false;
                        chkOtherResons.IsChecked = false;
                        chkReqResonDeliverContracts.IsChecked = false;
                        chkReqResonReceiveCheek.IsChecked = false;
                        chkReqResonReceiveContracts.IsChecked = false;
                        chkReqResonReceiveEmpData.IsChecked = false;
                        chkReqResonSMS.IsChecked = false;
                        lblCompName.Content = "";
                        messReqMessTypeCombo.Text = "";
                        #endregion                // -------clear selection mode at first time when load ---------------
                        try
                        {
                            messReqGrid.SelectedCells.Clear();
                            #region clearAll
                            cbxcompcomp.Text = "";
                            messReqCityCombo.Text = "";
                            messReqareaCombo.Text = "";
                            messReqbranchCombo.Text = "";
                            messReqDate.Text = "";
                            messReqContactPersontxt.Text = "";
                            messReqDeptxt.Text = "";
                            messReqothertxt.Document.Blocks.Clear();
                            messReqphonetxt.Text = "";
                            messReqaddrtxt.Text = "";
                            messangerIDtxt.Text = "";
                            chkReadyCardsReson.IsChecked = false;
                            chkReadyCheek.IsChecked = false;
                            chkDeliverPaper.IsChecked = false;
                            chkOtherResons.IsChecked = false;
                            chkReqResonDeliverContracts.IsChecked = false;
                            chkReqResonReceiveCheek.IsChecked = false;
                            chkReqResonReceiveContracts.IsChecked = false;
                            chkReqResonReceiveEmpData.IsChecked = false;
                            chkReqResonSMS.IsChecked = false;
                            lblCompName.Content = "";
                            messReqMessTypeCombo.Text = "";
                            #endregion                // -------clear selection mode at first time when load ---------------
                        }
                        catch { }
                    }
                    else { }
                }
            }
            catch { }
        }

        private void chkOtherResons_Checked(object sender, RoutedEventArgs e)
        {
            messReqothertxt.Visibility = Visibility.Visible;
        }

        private void messReqComptxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string companyName = messReqComptxt.Text;
            //try
            //{
            //    List<MessengerRequestData> Companylist = req.SelectAllCompanies(companyName, companyName);
            //    messReqCompanyList.ItemsSource = Companylist;
            //    messReqCompanyList.DisplayMemberPath = "CompanyName";
            //    messReqCompanyList.SelectedValuePath = "CompanyCode";
            //}
            //catch { }
        }
        private void messReqareaCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void messReqCityCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                int id = int.Parse(messReqCityCombo.SelectedValue.ToString());
                List<MessengerRequestData> list2 = req.SelectAllArea_In_governerate(id);
                messReqareaCombo.ItemsSource = list2;
                messReqareaCombo.DisplayMemberPath = "Governorate_Name";
                messReqareaCombo.SelectedValuePath = "Governorate_Code";
                //messReqGrid.Columns["Governorate_Code"].Visible = false;
            }
            catch { }
        }

        private void messReqGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (messReqGrid.SelectedItems.Count >= 1)
                {
                    object item = messReqGrid.SelectedItem;
                    int id = Convert.ToInt32((messReqGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    MessengerRequestData obj = req.SelectMessengerRequestById(id);
                    messReqCodetxt.Text = obj.ReqCode.ToString();
                    lblCompName.Content = obj.CompanyName;
                    messReqContactPersontxt.Text = obj.ContactPerson;
                    messReqDeptxt.Text = agent.get_dept(NameTab.Header.ToString());
                    messReqaddrtxt.Text = obj.Address;
                    messReqbranchCombo.Text = obj.Branch;
                    messReqCityCombo.Text = obj.Governorate_Name;
                    messangerRequestreasontxt.Text = obj.RequestResons;
                    messReqMessTypeCombo.Text = obj.MessengerType;
                    // txtOtherResons.Text = obj.RequestReson_Other;
                    try
                    {
                        messReqDate.Text = obj.Date.ToString();
                    }
                    catch { }
                    messReqphonetxt.Text = obj.Phone.ToString();
                    messReqareaCombo.Text = obj.Area.ToString();
                }
            }
            catch
            {
            }
        }

        private void PrintGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            #region UpdateWithCheckPrintingCount
            try
            {
                if (PrintGrid.Items.Count > 0)
                {
                    PrintingData obj = new PrintingData();
                    object item = PrintGrid.SelectedItem;
                    obj.EmpID = (PrintGrid.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                    obj.ContractNo = int.Parse((PrintGrid.SelectedCells[8].Column.GetCellContent(item) as TextBlock).Text);

                    string Count = printserv.SelectPrintingCount(obj.EmpID, obj.ContractNo);
                    if (Count == null) //means it is the first time to print
                    {
                        #region Selectmax_PrintNO
                        string max2 = printserv.SelectMaxPrintingId();
                        int maxx2 = 0;
                        if (max2 == "")
                        {
                            obj.PrintNo = "1";
                        }
                        else
                        {
                            maxx2 = int.Parse(max2) + 1;
                            obj.PrintNo = maxx2.ToString();
                        }
                        #endregion
                        obj.EmpID = (PrintGrid.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                        obj.EmpFirstName = (PrintGrid.SelectedCells[5].Column.GetCellContent(item) as TextBlock).Text;
                        obj.EmpSecondName = (PrintGrid.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text;
                        obj.EmpThirdName = (PrintGrid.SelectedCells[7].Column.GetCellContent(item) as TextBlock).Text;
                        obj.EmpName = obj.EmpFirstName + " " + obj.EmpSecondName + " " + obj.EmpThirdName;
                        obj.CompanyName = (PrintGrid.SelectedCells[10].Column.GetCellContent(item) as TextBlock).Text;
                        obj.CompID = (PrintGrid.SelectedCells[9].Column.GetCellContent(item) as TextBlock).Text;
                        obj.PrintingType = (PrintGrid.SelectedCells[11].Column.GetCellContent(item) as TextBlock).Text;
                        obj.ContractNo = int.Parse((PrintGrid.SelectedCells[8].Column.GetCellContent(item) as TextBlock).Text);
                        obj.PrintedBy = NameTab.Header.ToString();
                        //--------check if this messenger already exist------------------
                        PrintingData OldEmp = printserv.SelectEmpById(obj.EmpID, obj.ContractNo);
                        if (OldEmp != null)
                        {
                            //-------update it only when it press Edit otherwise show the message-------------
                        }
                        else            //----insert the new messenger
                        {
                            int affected = printserv.InsertEmp_In_Printing(obj);
                            if (affected > 0)
                            {
                                //---------
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("لقد قمت بالطباعة فى المرة الاولى يمكنك الطباعة للمرة الثانية فى الشاشة الاخرى");

                    }
                }

            }

            catch { }
            #endregion           

        }

        private void SummaryCompSrch_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                int compid = Convert.ToInt32(CompanyComboBoxMain.Text.ToString());
                string type;
                string longcontr;
                int contrid;
                System.Data.DataTable compDataTable;
                System.Data.DataTable classCodeDT;
                if (UserType == "hr")
                {
                    int comp = Convert.ToInt32(report.get_comp_id(UserCompany));
                    if (comp != compid)
                    {
                        MessageBox.Show("غير مسموح");
                    }
                    else
                    {
                        if (((ComboBoxItem)CompanyContractType.SelectedItem).Content.ToString() == ""
                            || ((ComboBoxItem)CompanyContractType.SelectedItem).Content == null
                            || ((ComboBoxItem)CompanyContractLong.SelectedItem).Content.ToString() == ""
                            || ((ComboBoxItem)CompanyContractLong.SelectedItem).Content == null
                             || summaryMainContractCompany.SelectedItem == null
                            || CompanyIDtxt.Text == ""
                            )
                        {
                            MessageBox.Show("ادخل البيانات كاملة");

                        }
                        else
                        {
                            type = (((ComboBoxItem)CompanyContractType.SelectedItem).Content.ToString());
                            longcontr = (((ComboBoxItem)CompanyContractLong.SelectedItem).Content.ToString());
                            contrid = Convert.ToInt32(summaryMainContractCompany.SelectedItem.ToString());
                            compDataTable = contract.get_company_data(compid, contrid);
                            Companyaname.Text = compDataTable.Rows[0].ItemArray[0].ToString();
                            CompanyEname.Text = compDataTable.Rows[0].ItemArray[1].ToString();
                            addr1txt.Text = compDataTable.Rows[0].ItemArray[2].ToString();
                            startDatetxt.Text = compDataTable.Rows[0].ItemArray[3].ToString();
                            endDatetxt.Text = compDataTable.Rows[0].ItemArray[4].ToString();
                            classCodeDT = contract.get_class_code(compid, contrid);
                            ClassCodeCombo.Items.Clear();
                            for (int i = 0; i < classCodeDT.Rows.Count; i++)
                            {
                                ClassCodeCombo.Items.Add(classCodeDT.Rows[i].ItemArray[0].ToString() + " " + classCodeDT.Rows[i].ItemArray[1].ToString());
                            }
                            #region load images
                            System.Data.DataTable images = contract.get_company_image(compid, contrid, type, longcontr);
                            if (images.Rows.Count == 0)
                            {
                                MessageBox.Show("لا توجد صور");
                            }
                            else
                            {
                                try
                                {
                                    List<string> image = new List<string>();
                                    for (int i = 0; i < images.Rows.Count; i++)
                                    {
                                        for (int j = 0; j < images.Columns.Count; j++)
                                        {
                                            image.Add(images.Rows[i].ItemArray[j].ToString());
                                        }
                                    }
                                    List<string> true_image = new List<string>();
                                    for (int i = 0; i < image.Count; i++)
                                    {
                                        if (!(image[i].Contains("null")))
                                        {
                                            true_image.Add(image[i].ToString());

                                        }
                                        else
                                            continue;
                                    }
                                    string imgname = "";
                                    for (int i = 0; i < true_image.Count; i++)
                                    {
                                        int j = i + 1;
                                        imgname = "companyContract" + j;
                                        if (imgname == this.companyContract1.Name)
                                        {
                                            companyContract1.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));

                                        }
                                        else if (imgname == this.companyContract2.Name)
                                        {
                                            companyContract2.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                        }
                                        else if (imgname == this.companyContract3.Name)
                                        {
                                            companyContract3.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                        }
                                        else if (imgname == this.companyContract4.Name)
                                        {
                                            companyContract4.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                        }
                                        else if (imgname == this.companyContract5.Name)
                                        {
                                            companyContract5.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                        }
                                        else if (imgname == this.companyContract6.Name)
                                        {
                                            companyContract6.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                        }
                                        else if (imgname == this.companyContract7.Name)
                                        {
                                            companyContract7.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                        }
                                        else if (imgname == this.companyContract8.Name)
                                        {
                                            companyContract8.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                        }
                                        else if (imgname == this.companyContract9.Name)
                                        {
                                            companyContract9.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                        }
                                    }
                                }
                                catch { }
                            }

                            #endregion
                        }
                    }
                }
                else
                {
                    if (((ComboBoxItem)CompanyContractType.SelectedItem).Content.ToString() == ""
                           || ((ComboBoxItem)CompanyContractType.SelectedItem).Content == null
                           || ((ComboBoxItem)CompanyContractLong.SelectedItem).Content.ToString() == ""
                           || ((ComboBoxItem)CompanyContractLong.SelectedItem).Content == null
                            || summaryMainContractCompany.SelectedItem == null
                           || CompanyComboBoxMain.Text == ""
                           )
                    {
                        MessageBox.Show("ادخل البيانات كاملة");

                    }
                    else
                    {
                        compid = Convert.ToInt32(CompanyComboBoxMain.Text.ToString());
                        type = (((ComboBoxItem)CompanyContractType.SelectedItem).Content.ToString());
                        longcontr = (((ComboBoxItem)CompanyContractLong.SelectedItem).Content.ToString());
                        contrid = Convert.ToInt32(summaryMainContractCompany.SelectedItem.ToString());
                        compDataTable = contract.get_company_data(compid, contrid);
                        Companyaname.Text = compDataTable.Rows[0].ItemArray[0].ToString();
                        CompanyEname.Text = compDataTable.Rows[0].ItemArray[1].ToString();
                        addr1txt.Text = compDataTable.Rows[0].ItemArray[2].ToString();
                        startDatetxt.Text = compDataTable.Rows[0].ItemArray[3].ToString();
                        endDatetxt.Text = compDataTable.Rows[0].ItemArray[4].ToString();
                        classCodeDT = contract.get_class_code(compid, contrid);
                        ClassCodeCombo.Items.Clear();
                        for (int i = 0; i < classCodeDT.Rows.Count; i++)
                        {
                            ClassCodeCombo.Items.Add(classCodeDT.Rows[i].ItemArray[0].ToString() + " " + classCodeDT.Rows[i].ItemArray[1].ToString());
                        }
                        #region load images
                        System.Data.DataTable images = contract.get_company_image(compid, contrid, type, longcontr);
                        if (images.Rows.Count == 0)
                        {
                            MessageBox.Show("لا توجد صور");
                        }
                        else
                        {
                            try
                            {
                                List<string> image = new List<string>();
                                for (int i = 0; i < images.Rows.Count; i++)
                                {
                                    for (int j = 0; j < images.Columns.Count; j++)
                                    {
                                        image.Add(images.Rows[i].ItemArray[j].ToString());
                                    }
                                }
                                List<string> true_image = new List<string>();
                                for (int i = 0; i < image.Count; i++)
                                {
                                    if (!(image[i].Contains("null")))
                                    {
                                        true_image.Add(image[i].ToString());

                                    }
                                    else
                                        continue;
                                }
                                string imgname = "";
                                for (int i = 0; i < true_image.Count; i++)
                                {
                                    int j = i + 1;
                                    imgname = "companyContract" + j;
                                    if (imgname == this.companyContract1.Name)
                                    {
                                        companyContract1.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));

                                    }
                                    else if (imgname == this.companyContract2.Name)
                                    {
                                        companyContract2.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                    }
                                    else if (imgname == this.companyContract3.Name)
                                    {
                                        companyContract3.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                    }
                                    else if (imgname == this.companyContract4.Name)
                                    {
                                        companyContract4.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                    }
                                    else if (imgname == this.companyContract5.Name)
                                    {
                                        companyContract5.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                    }
                                    else if (imgname == this.companyContract6.Name)
                                    {
                                        companyContract6.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                    }
                                    else if (imgname == this.companyContract7.Name)
                                    {
                                        companyContract7.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                    }
                                    else if (imgname == this.companyContract8.Name)
                                    {
                                        companyContract8.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                    }
                                    else if (imgname == this.companyContract9.Name)
                                    {
                                        companyContract9.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                    }
                                }
                            }
                            catch { }
                        }

                        #endregion
                    }
                }
            }
            catch { }
        }

        private void startCallBtn_Click(object sender, RoutedEventArgs e)
        {
            hour = DateTime.Now.Hour;
            min = DateTime.Now.Minute;
            sec = DateTime.Now.Second;
            endCallBtn.IsEnabled = true;
            endCallBtn.Visibility = Visibility.Visible;
            callDurationtxt.Visibility = Visibility.Visible;
        }

        private void endCallBtn_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    hour_end = Math.Abs(DateTime.Now.Hour - hour);
            //    min_end = Math.Abs(DateTime.Now.Minute - min);
            //    sec_end = Math.Abs(DateTime.Now.Second - sec);
            //    callDurationtxt.Content = hour_end.ToString() + " : " + min_end.ToString() + " : " + sec_end.ToString();
            //    string name = NameTab.Header.ToString();
            //    string date = DateTime.Now.ToShortDateString();
            //    string input = CallstxtSrch.Text.ToString();
            //    string nameEmp = "";
            //    string card = "";
            //    int agent_code = client.get_emp_code(name);
            //    char f = input[0];
            //    if (!Char.IsDigit(f) && cardnumtxt.Text == "")
            //    {
            //        nameEmp = input;
            //        cardNolbl.Visibility = Visibility.Visible;
            //        cardnumtxt.Visibility = Visibility.Visible;
            //        MessageBox.Show("لقياس جودة المكالمة ، ادخل رقم الكارت");
            //    }
            //    else if (!Char.IsDigit(f) && cardnumtxt.Text != "")
            //    {
            //        card = cardnumtxt.Text.ToString();
            //        System.Data.DataTable data = client.get_employeeData_FromID(card);
            //        for (int i = 0; i < data.Rows.Count; i++)
            //        {
            //            nameEmp = (data.Rows[i].ItemArray[2].ToString() + " " + data.Rows[i].ItemArray[3].ToString() + " " + data.Rows[i].ItemArray[4].ToString() + " " + data.Rows[i].ItemArray[5].ToString());

            //        }
            //        string dept = agent.get_dept(name);
            //        string duration = callDurationtxt.Content.ToString();
            //        string not = CallstxtSrch_Copy2.Text;
            //        client.add_client_call(card, nameEmp, agent_code, name, dept, date, duration, not); MessageBoxResult result = MessageBox.Show("Summary ? ", "Summary", MessageBoxButton.YesNoCancel, MessageBoxImage.Information, MessageBoxResult.OK);
            //        if (result == MessageBoxResult.Yes)
            //        {
            //            //SummaryPage sum = new SummaryPage(name, dept, duration, date, input, companyname);
            //            Summary_Window sum = new Summary_Window(name, dept, date, duration, card, nameEmp, agent_code, not);
            //            sum.ShowDialog();
            //            //add client call 
            //        }
            //        else
            //        { }
            //    }
            //    else
            //    {
            //        card = input;
            //        System.Data.DataTable data = client.get_employeeData_FromID(card);
            //        for (int i = 0; i < data.Rows.Count; i++)
            //        {
            //            nameEmp = (data.Rows[i].ItemArray[2].ToString() + " " + data.Rows[i].ItemArray[3].ToString() + " " + data.Rows[i].ItemArray[4].ToString() + " " + data.Rows[i].ItemArray[5].ToString());

            //        }
            //        string dept = agent.get_dept(name);
            //        string duration = callDurationtxt.Content.ToString();
            //        string not = CallstxtSrch_Copy2.Text;
            //        client.add_client_call(card, nameEmp, agent_code, name, dept, date, duration, not);
            //        MessageBoxResult result = MessageBox.Show("Summary ? ", "Summary", MessageBoxButton.YesNoCancel, MessageBoxImage.Information, MessageBoxResult.OK);
            //        if (result == MessageBoxResult.Yes)
            //        {
            //            //SummaryPage sum = new SummaryPage(name, dept, duration, date, input, companyname);
            //            Summary_Window sum = new Summary_Window(name, dept, date, duration, card, nameEmp, agent_code, not);
            //            sum.ShowDialog();
            //            //add client call 
            //        }
            //        else
            //        { }
            //    }

            //}
            //catch { }
        }

        private void nametxt_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                string name = nametxt.Text.ToString();
                string dept = NewEmpDeptCombo.Text;
                string company = "";
                if (basicDataComp.Text == "")
                {
                    company = "";
                }
                else
                {
                    company = basicDataComp.Text;
                }
                int result = agent.validate_user_name(name, dept);
                if (result == 0)
                {
                    saveEmpBtn.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("اسم موجود بالفعل ، اختر اسم آخر");
                    saveEmpBtn.IsEnabled = false;
                }
            }
            catch
            {
                MessageBox.Show("اختر قسم");
            }
        }
        public static string request_path;
        private void attachImgReqBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (of.ShowDialog() == true)
            {
                string file = of.FileName;
                string destn = "C:\\";
                System.IO.File.Copy(file, destn + System.IO.Path.GetFileName(file));
                request_path = destn + System.IO.Path.GetFileName(file);
                reqImg.Source = new BitmapImage(new Uri(request_path, UriKind.RelativeOrAbsolute));
            }
        }

        private void saveReqBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string dept = deptnameHome.Text.ToString();
                string emp = NameTab.Header.ToString();
                if (categoryComboMain.SelectedItem.ToString() == null || categoryComboMain.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("من فضلك اختر الفئة");

                }
                else
                {
                    if (itemComboMain.SelectedItem.ToString() == null || itemComboMain.SelectedItem.ToString() == "")
                    {
                        MessageBox.Show("من فضلك اختر الصنف");
                    }
                    else
                    {
                        string category = categoryComboMain.SelectedItem.ToString();
                        string item = itemComboMain.SelectedItem.ToString();
                        int amt = Convert.ToInt32(store.get_amount(item));
                        int amount = Convert.ToInt32(amounttxtMain.Text.ToString());
                        bool result = check_amount(amt, amount, item);
                        if (result == true)
                        {
                            store.add_request(item, category, emp, dept, amount, request_path);
                            MessageBox.Show("تم حفظ الطلب");


                        }
                        else
                            MessageBox.Show("المخزن غير كافي");
                    }
                }
            }
            catch { }

        }

        private void NameTab_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string dept = deptnameHome.Text.ToString();
                string emp = NameTab.Header.ToString();
                depttxtMain.Content = dept;
                emptxtMain.Content = emp;
                System.Data.DataTable cat = store.get_category();
                for (int i = 0; i < cat.Rows.Count; i++)
                {
                    categoryComboMain.Items.Add(cat.Rows[i].ItemArray[0].ToString());
                }
            }
            catch
            { }
        }

        private void categoryComboMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string cat = categoryComboMain.SelectedItem.ToString();
                System.Data.DataTable item = store.get_item_names(cat);
                itemComboMain.Items.Clear();
                for (int i = 0; i < item.Rows.Count; i++)
                {
                    itemComboMain.Items.Add(item.Rows[i].ItemArray[0].ToString());
                }
            }
            catch { }
        }


        private void managerSaveReqBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (managerViewReqGrid.SelectedItems.Count == 0)
                {
                    MessageBox.Show("من فضلك اختر طلب");
                }
                else
                {
                    string ok = "";
                    int id = 0;
                    if (okChk.IsChecked == true)
                    {
                        ok = "y";
                    }
                    else
                    {
                        ok = "n";
                    }
                    for (int i = 0; i < managerViewReqGrid.SelectedItems.Count; i++)
                    {
                        object item = managerViewReqGrid.SelectedItem;
                        id = Convert.ToInt32((managerViewReqGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);

                    }
                    store.update_done(ok, id);
                    MessageBox.Show("تم تعديل الطلب");
                    System.Data.DataTable data = store.get_request_no();
                    managerViewReqGrid.ItemsSource = data.DefaultView;

                    managerViewReqGrid.Columns[0].Header = "رقم الطلب";
                    managerViewReqGrid.Columns[1].Header = "اسم القطعة";
                    managerViewReqGrid.Columns[2].Header = "الفئة";
                    managerViewReqGrid.Columns[3].Header = "اسم الموظف";
                    managerViewReqGrid.Columns[4].Header = "القسم";
                    managerViewReqGrid.Columns[5].Header = "الكمية";
                }
            }
            catch { }
        }

        private void managerSrchtxt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    int id;
                    if (managerSrchtxt.Text == "")
                    {
                        System.Data.DataTable data = store.get_request();
                        managerSrchReqGrid.ItemsSource = data.DefaultView;
                    }
                    else if (!Char.IsDigit(managerSrchtxt.Text.ToString()[0]))
                    {
                        MessageBox.Show("ادخل رقم كارت صحيح");
                    }
                    else
                    {
                        id = Convert.ToInt32(managerSrchtxt.Text.ToString());
                        System.Data.DataTable reqTb = store.get_request(id);
                        managerSrchReqGrid.ItemsSource = reqTb.DefaultView;
                    }
                    managerSrchReqGrid.Columns[0].Header = "رقم الطلب";
                    managerSrchReqGrid.Columns[1].Header = "اسم القطعة";
                    managerSrchReqGrid.Columns[2].Header = "الفئة";
                    managerSrchReqGrid.Columns[3].Header = "اسم الموظف";
                    managerSrchReqGrid.Columns[4].Header = "القسم";
                    managerSrchReqGrid.Columns[5].Header = "الكمية";
                    managerSrchReqGrid.Columns[6].Header = "تم الموافقة ؟";
                    managerSrchReqGrid.Columns[7].Header = "مكان صورة الطلب";
                    managerSrchtxt.Text = "";
                }
            }
            catch { }
        }
        #region request store
        private void managerReqTab_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (managerReqTab.Visibility == Visibility.Visible)
                {
                    System.Data.DataTable data = store.get_request();
                    managerSrchReqGrid.ItemsSource = data.DefaultView;
                    System.Data.DataTable data1 = store.get_request_no();
                    managerViewReqGrid.ItemsSource = data1.DefaultView; //flag = n

                    managerViewReqGrid.Columns[0].Header = "رقم الطلب";
                    managerViewReqGrid.Columns[1].Header = "اسم القطعة";
                    managerViewReqGrid.Columns[2].Header = "الفئة";
                    managerViewReqGrid.Columns[3].Header = "اسم الموظف";
                    managerViewReqGrid.Columns[4].Header = "القسم";
                    managerViewReqGrid.Columns[5].Header = "الكمية";

                }
            }
            catch { }
        }

        private void managerViewReqGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string img = "";
                int id = 0;
                for (int i = 0; i < managerViewReqGrid.SelectedItems.Count; i++)
                {
                    object item = managerViewReqGrid.SelectedItem;
                    id = Convert.ToInt32((managerViewReqGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    img = store.get_image(id);
                    managerimg.Source = new BitmapImage(new Uri(img, UriKind.RelativeOrAbsolute));
                }
            }
            catch { }
        }

        private void managerViewReqGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (managerReqTab.Visibility == Visibility.Visible)
                {
                    managerViewReqGrid.Columns[0].Header = "رقم الطلب";
                    managerViewReqGrid.Columns[1].Header = "اسم القطعة";
                    managerViewReqGrid.Columns[2].Header = "الفئة";
                    managerViewReqGrid.Columns[3].Header = "اسم الموظف";
                    managerViewReqGrid.Columns[4].Header = "القسم";
                    managerViewReqGrid.Columns[5].Header = "الكمية";
                }
            }
            catch { }
        }

        private void managerSrchReqGrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (managerSrchReq.IsSelected == true)
                {
                    managerSrchReqGrid.Columns[0].Header = "رقم الطلب";
                    managerSrchReqGrid.Columns[1].Header = "اسم القطعة";
                    managerSrchReqGrid.Columns[2].Header = "الفئة";
                    managerSrchReqGrid.Columns[3].Header = "اسم الموظف";
                    managerSrchReqGrid.Columns[4].Header = "القسم";
                    managerSrchReqGrid.Columns[5].Header = "الكمية";
                    managerSrchReqGrid.Columns[6].Header = "تم الموافقة ؟";
                    managerSrchReqGrid.Columns[7].Header = "مكان الصورة";
                }
            }
            catch { }
        }
        #endregion
        private void deptgrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (departmentTab.IsSelected == true)
                {
                    System.Data.DataTable deptDT = agent.get_code_dept();
                    deptgrid.ItemsSource = deptDT.DefaultView;
                    deptgrid.Columns[0].IsReadOnly = true;

                    deptgrid.Columns[0].Header = "كود القسم";
                    deptgrid.Columns[1].Header = "اسم القسم";
                }
            }
            catch { }
        }

        private void itemname_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                string item = itemname.Text.ToString();
                int result = store.validate_item_name(item);
                if (result >= 1)
                {
                    MessageBox.Show("اسم صنف موجود بالفعل ، ادخل اسم جديد");
                    limtxt.IsEnabled = false;
                }
                else
                {
                    limtxt.IsEnabled = true;
                }
            }
            catch { }
        }

        private void cardnumtxt_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void ReceivingGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

        }


        #region ComplaintsAbdo

        #region solvedComplaintForm
        DB db = new DB();
        System.Data.DataTable dtSolvedComplaint = new System.Data.DataTable("IMS_COM_SUBJECT");
        private void txtshowdataSolvedComplaint_Click(object sender, RoutedEventArgs e)
        {
            dtSolvedComplaint = db_IRS.RunReader("Select * FROM IMS_COMPLAINTS WHERE COM_CHECKED='N'").Result;
            dataGridSolvedComplaint.ItemsSource = dtSolvedComplaint.DefaultView;
        }

        private void Btn_SearchSolvedComplaint_Click(object sender, RoutedEventArgs e)
        {
            dtSolvedComplaint = db_IRS.RunReader("Select * FROM IMS_COMPLAINTS WHERE COM_CHECKED='N' AND COMPLAINT_ID  LIKE '" + txtSearchSolvedComplaint.Text + "%'").Result;
            dataGridSolvedComplaint.ItemsSource = dtSolvedComplaint.DefaultView;
        }

        private void txtSearchSolvedComplaint_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                dtSolvedComplaint = db_IRS.RunReader("Select * FROM IMS_COMPLAINTS WHERE COM_CHECKED='N' AND COMPLAINT_ID  LIKE '" + txtSearchSolvedComplaint.Text + "%'").Result;
                dataGridSolvedComplaint.ItemsSource = dtSolvedComplaint.DefaultView;
            }
            catch { }
        }



        #endregion

        #region Complaintsubject


        System.Data.DataTable dtComplaintsubject = new System.Data.DataTable("IMS_COM_SUBJECT");

        private void btnSearchComplainsubject_Click(object sender, RoutedEventArgs e)
        {
            dtComplaintsubject = db_IRS.RunReader("Select * FROM IMS_COM_SUBJECT WHERE SUBJECT_CODE LIKE '" + textsearchComplaintsubject.Text + "%'").Result;
            dgviewcomplaintsubject.ItemsSource = dtComplaintsubject.DefaultView;
        }

        private void btnviowcomplainstubject_Click(object sender, RoutedEventArgs e)
        {
            dtComplaintsubject = db_IRS.RunReader("Select * FROM IMS_COM_SUBJECT   ").Result;
            dgviewcomplaintsubject.ItemsSource = dtComplaintsubject.DefaultView;
        }

        private void textsearchComplaintsubject_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtComplaintsubject = db_IRS.RunReader("Select * FROM IMS_COM_SUBJECT WHERE SUBJECT_CODE LIKE '" + textsearchComplaintsubject.Text + "%'").Result;
            dgviewcomplaintsubject.ItemsSource = dtComplaintsubject.DefaultView;
        }





        #endregion

        #region Visit

        string provider_idVisit, provider_nameVisit, branch_idVisit, branch_addrsVisit, person_nameVisit, person_idVisit;




        DataSet dtVisit, dtVisit2, dtVisit3;
        void searchfillVisit()
        {
            btnediteeVisit.Visibility = Visibility.Visible;
            // cleardataVisit();
            try
            {


                System.Data.DataTable s = db_IRS.RunReader(" select * from IMS_VISITS WHERE VISIT_ID = '" + txtsearchVisit.Text + "'").Result;
                if (s.Rows.Count > 0)
                {

                    txtIdVisit.Text = s.Rows[0][4].ToString();
                    branch_idVisit = s.Rows[0][6].ToString(); branch_addrsVisit = s.Rows[0][8].ToString();
                    cbxbranchVisit.Text = branch_idVisit + " - " + branch_addrsVisit;
                    provider_idVisit = s.Rows[0][1].ToString(); provider_nameVisit = s.Rows[0][7].ToString();
                    cbxproviderVisit.Text = provider_idVisit + " - " + provider_nameVisit;
                    person_idVisit = s.Rows[0][3].ToString();
                    System.Data.DataTable tem = db_IRS.RunReader(" select MEMBER_NAME from IMS_ESCLATION_MEMBER WHERE MEMBER_ID = " + person_idVisit).Result;
                    person_nameVisit = tem.Rows[0][0].ToString();
                    cbxpersonVisit.Text = person_idVisit + " - " + person_nameVisit;
                    txtproblemVisited.Text = s.Rows[0][9].ToString();
                    dtpcomVisit.Text = s.Rows[0][2].ToString();
                    if (s.Rows[0][5].ToString() == "1")
                    {
                        cbvisited.IsChecked = true;
                    }


                }
                else
                {
                    MessageBox.Show("invalied number", s.Rows[0][4].ToString()); return;

                }

                s.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private void cbxproviderVisit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxproviderVisit.AllowDrop = true;
        }

        private void cbxproviderVisit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                dtVisit = db.RunReaderds("  select  distinct USER_CO, USER_N from V_PROVIDERS WHERE USER_CO = '" + cbxproviderVisit.Text + "'");

                if (dtVisit.Tables[0].Rows.Count > 0)
                {
                    provider_idVisit = cbxproviderVisit.Text;
                    provider_nameVisit = dtVisit.Tables[0].Rows[0][1].ToString();
                    cbxproviderVisit.Text = provider_idVisit + " - " + provider_nameVisit;
                    cbxbranchVisit.Text = "";

                    fillPranchVisit();

                }
                else
                {
                    MessageBox.Show("الرقم غير صحيح");
                }
            }

        }

        private void cbxproviderVisit_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                cbxproviderVisit.Text = provider_idVisit + " - " + provider_nameVisit;
                cbxbranchVisit.Text = "";
                // System.Data.DataTable s = db.RunReader(" select * from IMS_COMPLAINTS WHERE PROVIDER_CODE = " + provider_idVisit + " ORDER BY COM_DATE ");

            }
            catch
            {
                // MessageBox.Show(ex.ToString());
            }
        }

        private void cbxbranchVisit_KeyDown(object sender, KeyEventArgs e)
        {
            cbxbranchVisit.IsDropDownOpen = true;
        }

        private void cbxbranchVisit_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                branch_idVisit = dtVisit2.Tables[0].Rows[cbxbranchVisit.SelectedIndex][0].ToString();
                branch_addrsVisit = dtVisit2.Tables[0].Rows[cbxbranchVisit.SelectedIndex][1].ToString();
                cbxbranchVisit.Text = branch_idVisit + " - " + branch_addrsVisit;


            }
            catch
            {
            }
        }


        private void cbxpersonVisit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxpersonVisit_KeyDown(object sender, KeyEventArgs e)
        {
            cbxpersonVisit.IsDropDownOpen = true;
        }

        private void cbxpersonVisit_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                person_idVisit = dtVisit3.Tables[0].Rows[cbxpersonVisit.SelectedIndex][0].ToString();
                person_nameVisit = dtVisit3.Tables[0].Rows[cbxpersonVisit.SelectedIndex][1].ToString();
                cbxpersonVisit.Text = person_idVisit + " - " + person_nameVisit;

            }
            catch
            {// MessageBox.Show(ex.ToString());
            }
        }
        void cleardataVisit()
        {
            AutoNumeVisit();
            btnediteeVisit.Visibility = Visibility.Hidden;
            txtsearchVisit.Clear();
            cbxbranchVisit.Text = " ";
            cbxproviderVisit.Text = " ";
            cbxpersonVisit.Text = "";
            txtproblemVisited.Text = "";
            cbvisited.IsChecked = false;

            // cbvisited.IsChecked = false;


            dtVisit3.Clear();
            fillProviderVisit();
            fillPersonVisit();

            check = 0;



        }
        void checkfunVisit()
        {


            if (cbxproviderVisit.Text == "" || cbxbranchVisit.Text == " - ")
            {
                provider_idVisit = "NULL";
                provider_nameVisit = "NULL";
            }
            if (cbxbranchVisit.Text == "" || cbxbranchVisit.Text == " - ")
            {
                branch_idVisit = "NULL";
                branch_addrsVisit = "NULL";
            }
            if (cbxpersonVisit.Text == "" || cbxpersonVisit.Text == " - ")
            {
                person_idVisit = "NULL";
                person_nameVisit = "NULL";
            }



        }
        private void btnsaveVisit_Click(object sender, RoutedEventArgs e)
        {

            DateTime datet = dtpcomVisit.SelectedDate.Value.Date;
            string comDate = datet.ToString("dd-MMM-yy");
            int n = int.Parse(datet.ToString("ddMMyyyy"));
            string s = n.ToString() + txtIdVisit.Text;

            checkfunVisit();


            db_IRS.RunNonQuery("INSERT INTO IMS_VISITS (VISIT_ID, PR_CODE, VISIT_DATE, TECHNICAL_ID, VIS_SER, VIS_CHECKED, BRANCH_CODE, PR_NAME, BR_NAME, VIS_REASON) VALUES ('" + s + "', " + provider_idVisit + ",' " + comDate + "', " + person_idVisit + ", " + txtIdVisit.Text + ", " + check + ", " + branch_idVisit + ", '" + provider_nameVisit + "',' " + branch_addrsVisit + "', '" + txtproblemVisited.Text + "')", "تم الحفظ بنجاح كود الزيارة " + s);
            cleardataVisit();
        }

        private void txtproblemVisited_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        //check = 0;
        private void cbvisited_Checked(object sender, RoutedEventArgs e)
        {
            check = 1;
        }


        private void txtsearchVisit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchfillVisit();
            }
        }
        bool f = false;
        private void txtsearchVisit_MouseEnter(object sender, MouseEventArgs e)
        {

            if (f == false)
            {
                txtsearchVisit.Text = "";
                f = true;
            }
        }

        private void btnClearVisit_Click(object sender, RoutedEventArgs e)
        {
            cleardataVisit();
        }

        private void btnediteeVisit_Click(object sender, RoutedEventArgs e)
        {
            DateTime datet = dtpcomVisit.SelectedDate.Value.Date;
            string comDate = datet.ToString("dd-MMM-yy");
            db_IRS.RunNonQuery("UPDATE IMS_VISITS SET PR_CODE = " + provider_idVisit + ", VISIT_DATE = " + comDate + ", TECHNICAL_ID = " + person_idVisit + ", VIS_SER = " + txtIdVisit.Text + ", VIS_CHECKED = " + check + ", BRANCH_CODE =" + branch_idVisit + ", PR_NAME = " + provider_nameVisit + ", BR_NAME =" + branch_addrsVisit + ", VIS_REASON = " + txtproblemVisited.Text + " WHERE VISIT_ID = '" + txtsearchVisit.Text + "'", "تم التعديل بنجاح");
            cleardataVisit();

        }

        void printxps()
        {
            string comDate = dtpcomVisit.SelectedDate.Value.ToString("dd-MMM-yy");
            PrintDialog printDlg = new PrintDialog();
            FlowDocument doc = new FlowDocument(new System.Windows.Documents.Paragraph(new Run("VISIT_ID = " + txtsearchVisit.Text + " \n PR_CODE = " + provider_idVisit + " \n VISIT_DATE = " + comDate + " \n TECHNICAL_ID = " + person_idVisit + " \n VIS_SER = " + txtIdVisit.Text + " \n BRANCH_CODE =" + branch_idVisit + " \n PR_NAME = " + provider_nameVisit + "\n BR_NAME =" + branch_addrsVisit + "\n VIS_REASON = " + txtproblemVisited.Text)));
            doc.Name = "FlowDoc";
            IDocumentPaginatorSource idpSource = doc;
            printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");

        }

        private void btnPrintVisit_Click(object sender, RoutedEventArgs e)
        {
            string comDate = dtpcomVisit.SelectedDate.Value.ToString("dd-MMM-yy");
            PrintDialog printDlg = new PrintDialog();
            FlowDocument doc = new FlowDocument(new System.Windows.Documents.Paragraph(new Run("ماموريات قسم الدعم الفنى\n" + "تاريخ الزيارة : " + dtpcomVisit.Text + "               رقم الزيارة : " + txtIdVisit.Text + "------------------------------------------------------------" + "  \n مقدم الخدمة : " + provider_nameVisit + "\nالعنوان :" + branch_addrsVisit + "\nاسم الموظف :" + person_nameVisit + "\n سبب الزيارة : " + txtproblemVisited.Text + "------------------------------------------------------------\n" + "  توقيع الموظف          توقيع المدير المباشر         \n\n\n\n توقيع توقيع المدير الادارى")));
            //   doc.FontStyle = FontStyles.;
            doc.TextAlignment = TextAlignment.Center;
            doc.Name = "FlowDoc";
            IDocumentPaginatorSource idpSource = doc;
            printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");
        }



        private void cbxproviderVisit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                provider_idVisit = dtVisit.Tables[0].Rows[cbxproviderVisit.SelectedIndex][0].ToString();
                provider_nameVisit = dtVisit.Tables[0].Rows[cbxproviderVisit.SelectedIndex][1].ToString();
                cbxproviderVisit.Text = provider_idVisit + " - " + provider_nameVisit;
                fillPranchVisit();

            }
            catch
            { }
        }

        private void fillProviderVisit()
        {
            dtVisit = db.RunReaderds(" select distinct USER_CO , USER_N from V_PROVIDERS where KIND_NO IN (1,3,4) ORDER BY USER_CO ");
            cbxproviderVisit.ItemsSource = dtVisit.Tables[0].DefaultView;


        }
        private void fillPranchVisit()
        {

            dtVisit2 = db.RunReaderds(" select distinct USER_ID , ADDRS from V_PROVIDERS  where USER_CO=" + provider_idVisit);
            cbxbranchVisit.ItemsSource = dtVisit2.Tables[0].DefaultView;

        }
        private void fillPersonVisit()
        {
            dtVisit3 = db_IRS.RunReaderds(" select MEMBER_ID , MEMBER_NAME from IMS_ESCLATION_MEMBER  ORDER BY MEMBER_ID");
            cbxpersonVisit.ItemsSource = dtVisit3.Tables[0].DefaultView;
        }
        private void AutoNumeVisit()
        {
            Filltbl("select max(VIS_SER) from IMS_VISITS ");
            if (tbl.Rows[0][0].ToString() != DBNull.Value.ToString())
                txtIdVisit.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
            else
                txtIdVisit.Text = "1";
        }

        System.Data.DataTable tbl = new System.Data.DataTable();
        private void Filltbl(string SelectStatment)
        {
            tbl.Clear();
            tbl.Columns.Clear();
            tbl = db_IRS.RunReader(SelectStatment).Result;

        }

        #endregion

        #region FollowUp
        System.Data.DataTable tblFollow = new System.Data.DataTable();

        DataSet dtFollow, dtFollow2;
        string provider_idFollow, provider_nameFollow, branch_idFollow, branch_addrsFollow;

        private void cbxbranchFollow_KeyDown(object sender, KeyEventArgs e)
        {
            cbxbranchFollow.IsDropDownOpen = true;
        }

        private void cbxbranchFollow_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                branch_idFollow = dtFollow2.Tables[0].Rows[cbxbranchFollow.SelectedIndex][0].ToString();
                branch_addrsFollow = dtFollow2.Tables[0].Rows[cbxbranchFollow.SelectedIndex][1].ToString();
                cbxbranchFollow.Text = branch_idFollow + " - " + branch_addrsFollow;
                System.Data.DataTable s = db.RunReader(" select KIND_NO , USER_CO , USER_N ,USER_ID , USER_NAME , USER_PWD , TEL , MOB , ADDRS , GOVER_ID , GOVER_NAME from V_PROVIDERS WHERE USER_ID = " + branch_idFollow).Result;
                dgFollow.DataContext = s.DefaultView;
                txtbranch_id.Text = branch_idFollow;
                txtbranch_name.Text = branch_addrsFollow;


                System.Data.DataTable tblFollow1 = db_IRS.RunReader(" select  WORKS , COMMENTS , CONTACT_1 , CONTACT_2 , PHONE_1 , PHONE_2 , TITLE_1 , TITLE_2  from IMS_PRV WHERE USER_ID = " + branch_idFollow).Result;
                try
                {
                    string x = tblFollow1.Rows[0][0].ToString();
                    //MessageBox.Show("feeh mwgoood");
                    btnediteeFollow.Visibility = Visibility.Visible;
                    btnsaveFollow.Visibility = Visibility.Hidden;
                }
                catch
                {
                    btnsaveFollow.Visibility = Visibility.Visible;
                    btnediteeFollow.Visibility = Visibility.Hidden;
                }



                System.Data.DataTable tblFollow = db_IRS.RunReader(" select  WORKS , COMMENTS , CONTACT_1 , CONTACT_2 , PHONE_1 , PHONE_2 , TITLE_1 , TITLE_2  from IMS_PRV WHERE USER_ID = " + branch_idFollow).Result;
                try
                {
                    cbxstate.Text = tblFollow.Rows[0][0].ToString();
                    txtcomment.Text = tblFollow.Rows[0][1].ToString();
                    txtcontact1.Text = tblFollow.Rows[0][2].ToString();
                    txtcontact2.Text = tblFollow.Rows[0][3].ToString();
                    txtphone1.Text = tblFollow.Rows[0][4].ToString();
                    txtphone2.Text = tblFollow.Rows[0][5].ToString();
                    txtaddress1.Text = tblFollow.Rows[0][6].ToString();
                    txtaddres2.Text = tblFollow.Rows[0][7].ToString();
                }
                catch { }
                cleardataFollow();




            }
            catch
            {
            }
        }

        private void cbxproviderFollow_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxproviderFollow.AllowDrop = true;
        }



        private void cbxproviderFollow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                dtFollow = db.RunReaderds("  select  distinct USER_CO, USER_N from V_PROVIDERS WHERE USER_CO = '" + cbxproviderFollow.Text + "'");

                if (dtFollow.Tables[0].Rows.Count > 0)
                {
                    provider_idFollow = cbxproviderFollow.Text;
                    provider_nameFollow = dtFollow.Tables[0].Rows[0][1].ToString();
                    cbxproviderFollow.Text = provider_idFollow + " - " + provider_nameFollow;
                    cbxbranchFollow.Text = "";
                    System.Data.DataTable s = db.RunReader(" select KIND_NO , USER_CO , USER_N ,USER_ID , USER_NAME , USER_PWD , TEL , MOB , ADDRS , GOVER_ID , GOVER_NAME from V_PROVIDERS WHERE USER_CO = " + provider_idFollow).Result;
                    dgFollow.DataContext = s.DefaultView;
                    txtprovider_idFollow.Text = provider_idFollow;
                    txtprovider_nameFollow.Text = provider_nameFollow;

                    fillPranchFollow();

                }
                else
                {
                    MessageBox.Show("الرقم غير صحيح");
                }
            }

        }

        private void cbxproviderFollow_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                cbxproviderFollow.Text = provider_idFollow + " - " + provider_nameFollow;
                cbxbranchFollow.Text = "";
                System.Data.DataTable s = db.RunReader(" select KIND_NO , USER_CO , USER_N ,USER_ID , USER_NAME , USER_PWD , TEL , MOB , ADDRS , GOVER_ID , GOVER_NAME from V_PROVIDERS WHERE USER_CO = " + provider_idFollow).Result;
                dgFollow.DataContext = s.DefaultView;
                txtprovider_idFollow.Text = provider_idFollow;
                txtprovider_nameFollow.Text = provider_nameFollow;


            }
            catch
            {

            }
        }



        private void dgFollow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dgFollow.SelectedItems[0];
                // DataRow
                cleardataFollow();
                branch_idFollow = row[3].ToString();
                branch_addrsFollow = row[8].ToString();

            }
            catch { }

            System.Data.DataTable tblFollow1 = db_IRS.RunReader(" select  WORKS , COMMENTS , CONTACT_1 , CONTACT_2 , PHONE_1 , PHONE_2 , TITLE_1 , TITLE_2  from IMS_PRV WHERE USER_ID = " + branch_idFollow).Result;
            try
            {
                string x = tblFollow1.Rows[0][0].ToString();
                //MessageBox.Show("feeh mwgoood");
                btnediteeFollow.Visibility = Visibility.Visible;
                btnsaveFollow.Visibility = Visibility.Hidden;
            }
            catch
            {
                btnsaveFollow.Visibility = Visibility.Visible;
                btnediteeFollow.Visibility = Visibility.Hidden;
            }



            txtbranch_id.Text = branch_idFollow;
            txtbranch_name.Text = branch_addrsFollow;
            //  MessageBox.Show(row[3].ToString());

            System.Data.DataTable tblFollow = db_IRS.RunReader(" select  WORKS , COMMENTS , CONTACT_1 , CONTACT_2 , PHONE_1 , PHONE_2 , TITLE_1 , TITLE_2  from IMS_PRV WHERE USER_ID = " + branch_idFollow).Result;
            try
            {
                cbxstate.Text = tblFollow.Rows[0][0].ToString();
                txtcomment.Text = tblFollow.Rows[0][1].ToString();
                txtcontact1.Text = tblFollow.Rows[0][2].ToString();
                txtcontact2.Text = tblFollow.Rows[0][3].ToString();
                txtphone1.Text = tblFollow.Rows[0][4].ToString();
                txtphone2.Text = tblFollow.Rows[0][5].ToString();
                txtaddress1.Text = tblFollow.Rows[0][6].ToString();
                txtaddres2.Text = tblFollow.Rows[0][7].ToString();
            }
            catch { }

        }
        void cleardataFollow()
        {
            cbxstate.Text = "";
            txtcomment.Text = "";
            txtcontact1.Text = "";
            txtcontact2.Text = "";
            txtphone1.Text = "";
            txtphone2.Text = "";
            txtaddress1.Text = "";
            txtaddres2.Text = "";
            txtbranch_id.Text = "";
            txtbranch_name.Text = "";
            cbxstate_Copy.Text = "";
        }
        private void btnsaveFollow_Click(object sender, RoutedEventArgs e)
        {
            db_IRS.RunNonQuery("INSERT INTO IMS_PRV (USER_ID, WORKS, COMMENTS, USER_CO, CONTACT_1, PHONE_1, TITLE_1, TITLE_2, CONTACT_2, PHONE_2) VALUES ('" +
                                                      branch_idFollow + "', '" + cbxstate.Text + "', '" + txtcomment.Text + "', '" + provider_idFollow + "', '" + txtcontact1.Text + "', '" + txtphone1.Text
                                                      + "', '" + txtaddress1.Text + "', '" + txtaddres2.Text + "', '" + txtcontact2.Text + "', '" + txtphone2.Text + "')", "Done");
            cleardataFollow();
        }

        private void btnediteeFollow_Click(object sender, RoutedEventArgs e)
        {
            db_IRS.RunNonQuery("UPDATE IMS_PRV SET WORKS = '" + cbxstate.Text + "', COMMENTS = '" + txtcomment.Text + "', USER_CO = '" + provider_idFollow + "', CONTACT_1 = '" + txtcontact1.Text + "', PHONE_1 = '" + txtphone1.Text + "', TITLE_1 = '" + txtaddress1.Text + "', TITLE_2 = '" + txtaddres2.Text + "', CONTACT_2 = '" + txtcontact2.Text + "', PHONE_2 = '" + txtphone2.Text + "' WHERE USER_ID =" + txtbranch_id.Text, "تم التعديل بنجاح");
            cleardataFollow();
        }


        private void cbxstate_DropDownClosed(object sender, EventArgs e)
        {

            if (cbxstate.SelectedIndex == 1)
            {
                Service_Request s = new Service_Request(provider_idFollow, provider_nameFollow, branch_idFollow, branch_addrsFollow);
                s.Show();





            }
        }
        string x;
        private void cbxstate_Copy_DropDownClosed(object sender, EventArgs e)
        {

            switch (cbxstate_Copy.SelectedIndex)
            {
                case 0:
                    x = "يعمل";
                    break;
                case 1:
                    x = "لا يعمل";
                    break;
                case 2:
                    x = "لايوجد نظام";
                    break;
                case 3:
                    x = "لم يرد";
                    break;
                case 4:
                    x = "الرقم غير صحيح";
                    break;
            }
            //  MessageBox.Show(x);
            System.Data.DataTable s = db_IRS.RunReader(" select USER_ID , USER_CO , ANSWER ,COMMENTS , CONTACT_1 , CONTACT_2 , PHONE_1 , PHONE_1 , TITLE_1 , TITLE_2  from IMS_PRV WHERE WORKS like '" + x + "'").Result;

            dgFollow.DataContext = s.DefaultView;
            txtprovider_idFollow.Text = provider_idFollow;
            txtprovider_nameFollow.Text = provider_nameFollow;
        }

        private void btnclearFollow_c(object sender, RoutedEventArgs e)
        {
            cleardataFollow();
            cbxproviderFollow.Text = "";
            cbxbranchFollow.Text = "";
            cbxstate_Copy.Text = "";
            txtprovider_nameFollow.Text = "";
            txtprovider_idFollow.Text = "";
            txtbranch_id.Text = "";
            txtbranch_name.Text = "";
            dgFollow.ItemsSource = null;
        }

        private void cbxproviderFollow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                provider_idFollow = dt.Tables[0].Rows[cbxproviderFollow.SelectedIndex][0].ToString();
                provider_nameFollow = dt.Tables[0].Rows[cbxproviderFollow.SelectedIndex][1].ToString();
                cbxproviderFollow.Text = provider_idFollow + " - " + provider_nameFollow;
                fillPranchFollow();

            }
            catch
            { }
        }




        private void fillProviderFollow()
        {
            dtFollow = db.RunReaderds(" select distinct USER_CO , USER_N from V_PROVIDERS where KIND_NO IN (1,3,4) ORDER BY USER_CO ");
            cbxproviderFollow.ItemsSource = dtFollow.Tables[0].DefaultView;




        }
        private void fillPranchFollow()
        {

            dtFollow2 = db.RunReaderds(" select distinct USER_ID , ADDRS from V_PROVIDERS  where USER_CO=" + provider_idFollow + "ORDER BY USER_ID");
            cbxbranchFollow.ItemsSource = dtFollow2.Tables[0].DefaultView;

        }
        #endregion


        #region Serviec_Request
        System.Data.DataTable tblService = new System.Data.DataTable();
        string provider_id, provider_name, branch_id, branch_addrs, subject_name, subject_id, sclated_name, sclated_id, solved_name, solved_id;

        private void FilltblService(string SelectStatment)
        {
            tblService.Clear();
            tblService.Columns.Clear();
            tblService = db_IRS.RunReader(SelectStatment).Result;

        }
        DataSet dt, dt2, dt3, dt4;

        private void fillProvider()
        {
            dt = db.RunReaderds(" select distinct USER_CO , USER_N from V_PROVIDERS where KIND_NO IN (1,3,4) ORDER BY USER_CO ");
            cbxprovider.ItemsSource = dt.Tables[0].DefaultView;


        }
        private void fillPranch()
        {

            dt2 = db.RunReaderds(" select distinct USER_ID , ADDRS from V_PROVIDERS  where USER_CO=" + provider_id + "ORDER BY USER_ID");
            cbxbranch.ItemsSource = dt2.Tables[0].DefaultView;

        }

        private void fillsubject()
        {

            dt3 = db_IRS.RunReaderds(" select SUBJECT_CODE ,SUBJECT_NAME from IMS_COM_SUBJECT Where SUB_TYPE = 'PR'  ORDER BY SUBJECT_CODE");
            cbxsubject.ItemsSource = dt3.Tables[0].DefaultView;
        }
        private void fillEsclated()
        {
            dt4 = db_IRS.RunReaderds(" select MEMBER_ID , MEMBER_NAME from IMS_ESCLATION_MEMBER  ORDER BY MEMBER_ID");
            cbxesclated.ItemsSource = dt4.Tables[0].DefaultView;
            cbxsolvedby.ItemsSource = dt4.Tables[0].DefaultView;
        }
        private void AutoNume()
        {

            FilltblService("select max(COM_SER) from IMS_COMPLAINTS ");
            if (tblService.Rows[0][0].ToString() != DBNull.Value.ToString())
                txtCoId.Text = (Convert.ToInt32(tblService.Rows[0][0].ToString()) + 1).ToString();
            else
                txtCoId.Text = "1";

            tbcby.Visibility = Visibility.Hidden;
            tbbranch.Visibility = Visibility.Hidden;
            tbprovider.Visibility = Visibility.Hidden;
            tbsubject.Visibility = Visibility.Hidden;
            tbsubject_Copy.Visibility = Visibility.Hidden;


        }

        string provider_id2, provider_type;
        private void Buttonvisit(object sender, RoutedEventArgs e)
        {
            checkfun();

            //Visits a = new Visits(provider_id, provider_name, branch_id, branch_addrs, sclated_id, sclated_name, provider_id2, provider_type);
            //a.Show();
        }

        private void cbxprovider_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                provider_id = dt.Tables[0].Rows[cbxprovider.SelectedIndex][0].ToString();
                provider_name = dt.Tables[0].Rows[cbxprovider.SelectedIndex][1].ToString();
                cbxprovider.Text = provider_id + " - " + provider_name;
                fillPranch();

            }
            catch
            { }





        }

        void cleardata()
        {
            AutoNume();
            btnedite.Visibility = Visibility.Hidden;
            txtsearch.Clear();
            cbxbranch.Text = " ";
            cbxprovider.Text = " ";
            cbxsubject.Text = " ";
            cbxesclated.Text = "";
            cbxsolvedby.Text = "";
            txtproblem.Text = "";
            txtreplay.Text = "";
            txtcreatedby.Text = "";
            txtupdatedby.Text = "";
            txtCoId_Copy.Text = "";
            imgPhoto.Source = null;
            checkBox1sr.IsChecked = false;

            dt.Clear();
            try
            {
                dt2.Clear();
            }
            catch
            { }
            dt3.Clear();
            fillProvider();
            fillsubject();
            pathaa = "";
            checkServies = 0;



        }
        private void clear_cli(object sender, RoutedEventArgs e)
        {
            //  btnedite.Visibility = Visibility.Hidden;
            cleardata();

        }



        private void cbxprovider_DropDownClosed(object sender, EventArgs e)
        {

            try
            {

                cbxprovider.Text = provider_id + " - " + provider_name;
                cbxbranch.Text = "";
                System.Data.DataTable s = db_IRS.RunReader(" select * from IMS_COMPLAINTS WHERE PROVIDER_CODE = " + provider_id + " ORDER BY COM_DATE desc").Result;
                // //this.Height = 600;
                s = fivevaluindatatable(s);
                tbcby.Visibility = Visibility.Visible;
                tbprovider.Visibility = Visibility.Visible;
                dgprovider.DataContext = s.DefaultView;
            }
            catch
            {
                // MessageBox.Show(ex.ToString());
            }

        }

        private void cbxbranch_DropDownClosed(object sender, EventArgs e)
        {

            try
            {

                branch_id = dt2.Tables[0].Rows[cbxbranch.SelectedIndex][0].ToString();
                branch_addrs = dt2.Tables[0].Rows[cbxbranch.SelectedIndex][1].ToString();
                cbxbranch.Text = branch_id + " - " + branch_addrs;
                System.Data.DataTable s = db_IRS.RunReader(" select * from IMS_COMPLAINTS WHERE BRANCH_CODE = " + branch_id + " ORDER BY COM_DATE desc ").Result;
                s = fivevaluindatatable(s);
                //  tbcby.Visibility = tbbranch.Visibility;
                tbbranch.Visibility = Visibility.Visible;
                dgbranch.DataContext = s.DefaultView;

            }
            catch
            {
            }
        }

        private void cbxsubject_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                subject_id = dt3.Tables[0].Rows[cbxsubject.SelectedIndex][0].ToString();
                subject_name = dt3.Tables[0].Rows[cbxsubject.SelectedIndex][1].ToString();
                cbxsubject.Text = subject_name;
                System.Data.DataTable s = db_IRS.RunReader(" select * from IMS_COMPLAINTS WHERE SUBJECT_CODE = " + subject_id + " ORDER BY COM_DATE desc").Result;
                s = fivevaluindatatable(s);
                tbsubject.Visibility = Visibility.Visible;
                tbcby.Visibility = Visibility.Visible;
                tbsubject_Copy.Visibility = Visibility.Visible;
                dgcomplaint.DataContext = s.DefaultView;
                s = db_IRS.RunReader(" select * from IMS_COMPLAINTS WHERE SUBJECT_CODE = " + subject_id + " and  PROVIDER_CODE = " + provider_id + " ORDER BY COM_DATE desc").Result;
                s = fivevaluindatatable(s);
                dgcomplaint1.ItemsSource = s.DefaultView;


            }
            catch
            {
                // MessageBox.Show(ex.ToString());
            }
        }

        System.Data.DataTable fivevaluindatatable(System.Data.DataTable a)
        {
            int x = 1;
            foreach (DataRow row in a.Rows)
            {
                if (x > 5)
                {
                    row.Delete();
                }

                x++;
            }



            return a;

        }

        private void cbxesclated_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                sclated_id = dt4.Tables[0].Rows[cbxesclated.SelectedIndex][0].ToString();
                sclated_name = dt4.Tables[0].Rows[cbxesclated.SelectedIndex][1].ToString();
                cbxesclated.Text = sclated_id + " - " + sclated_name;

            }
            catch
            {// MessageBox.Show(ex.ToString());
            }
        }

        //   long elapsedTicks = dtpcom.Ticks - centuryBegin.Ticks;
        //    TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);

        void checkfun()
        {

            if (txtcreatedby.Text == "")
                txtcreatedby.Text = "NULL";

            if (txtupdatedby.Text == "")
                txtupdatedby.Text = "NULL";

            if (cbxprovider.Text == "" || cbxbranch.Text == " - ")
            {
                provider_id = "NULL";
                provider_name = "NULL";
            }
            if (cbxbranch.Text == "" || cbxbranch.Text == " - ")
            {
                branch_id = "NULL";
                branch_addrs = "NULL";
            }
            if (cbxsubject.Text == "" || cbxsubject.Text == " - ")
            {
                subject_id = "NULL";
                subject_name = "NULL";
            }
            if (cbxsolvedby.Text == "" || cbxsolvedby.Text == " - ")
            {
                solved_id = "NULL";
                solved_name = "NULL";
            }
            if (cbxesclated.Text == "" || cbxesclated.Text == " - ")
            {
                sclated_id = "NULL";
                sclated_name = "NULL";
            }


        }
        private void Buttonsave(object sender, System.Windows.RoutedEventArgs e)
        {

            DateTime datet = dtpcom.SelectedDate.Value.Date;
            string comDate = datet.ToString("dd-MMM-yy");

            int n = int.Parse(datet.ToString("ddMMyyyy"));
            string s = n.ToString() + txtCoId.Text;

            checkfun();
            string timess = probcbxtime31.Text + ":" + probcbxtime32.Text + " " + probcbxtime33.Text;


            db_IRS.RunNonQuery("INSERT INTO IMS_COMPLAINTS (COMPLAINT_ID, COM_SER, BRANCH_CODE, PROVIDER_CODE, SUBJECT_CODE, ESCLATED_TO, PROPLEM,  COM_DATE, COM_REPLAY, SOLVED_BY, CREATED_BY, UPDATED_BY, COM_CHECKED, PROVIDER_NAME, BRANCH_NAME ,COMM_ATTACH ,TIME) VALUES   ('"
                                                       + s + "'," + txtCoId.Text + "," + branch_id + "," + provider_id + "," + subject_id + "," + sclated_id + ",'" + txtproblem.Text + "','" + comDate + "','" + txtreplay.Text + "'," + solved_id + "," + txtcreatedby.Text + "," + txtupdatedby.Text + "," + checkServies + ",'" + provider_name + "','" + branch_addrs + "','" + pathaa + "','" + timess + "')", "تم الحفظ بنجاح");
            txtCoId_Copy.Text = s;
            // cleardata();
        }
        public string pathaa = "";

        private void imgsearch_Copy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dt = db.RunReaderds("  select  distinct USER_CO, USER_N from V_PROVIDERS WHERE KIND_NO IN (1,3,4) and USER_CO  LIKE '%" + cbxprovider.Text + "%' or USER_N LIKE '%" + cbxprovider.Text + "%'  ORDER BY USER_CO ");
            cbxprovider.ItemsSource = dt.Tables[0].DefaultView;
            cbxprovider.IsDropDownOpen = true;

        }

        private void imgsearch_branch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dt2 = db.RunReaderds(" select distinct USER_ID , ADDRS from V_PROVIDERS  where USER_CO=" + provider_id + " and USER_ID  LIKE '%" + cbxbranch.Text + "%' or ADDRS LIKE '%" + cbxbranch.Text + "%'ORDER BY USER_ID");
            cbxbranch.ItemsSource = dt2.Tables[0].DefaultView;
            cbxbranch.IsDropDownOpen = true;
        }

        private void imgsearch_sub_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dt3 = db_IRS.RunReaderds(" select SUBJECT_CODE ,SUBJECT_NAME from IMS_COM_SUBJECT where SUBJECT_CODE  LIKE '%" + cbxsubject.Text + "%' or SUBJECT_NAME LIKE '%" + cbxsubject.Text + "%'   ORDER BY SUBJECT_CODE");
            cbxsubject.ItemsSource = dt3.Tables[0].DefaultView;
            cbxsubject.IsDropDownOpen = true;
        }

        private void imgsearch_providfollow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dt = db.RunReaderds("  select  distinct USER_CO, USER_N from V_PROVIDERS WHERE KIND_NO IN (1,3,4) and USER_CO  LIKE '%" + cbxproviderFollow.Text + "%' or USER_N LIKE '%" + cbxproviderFollow.Text + "%'  ORDER BY USER_CO ");
            cbxproviderFollow.ItemsSource = dt.Tables[0].DefaultView;
            cbxproviderFollow.IsDropDownOpen = true;
        }

        private void cbxsubject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void imgsearch_branchfollow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dt2 = db.RunReaderds(" select distinct USER_ID , ADDRS from V_PROVIDERS  where USER_CO=" + provider_idFollow + " and USER_ID  LIKE '%" + cbxbranchFollow.Text + "%' or ADDRS LIKE '%" + cbxbranchFollow.Text + "%'ORDER BY USER_ID");
            cbxbranchFollow.ItemsSource = dt2.Tables[0].DefaultView;
            cbxbranchFollow.IsDropDownOpen = true;
        }

        private void imgsearch_providVisit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dtVisit = db.RunReaderds("  select  distinct USER_CO, USER_N from V_PROVIDERS WHERE KIND_NO IN (1,3,4) and USER_CO  LIKE '%" + cbxproviderVisit.Text + "%' or USER_N LIKE '%" + cbxproviderVisit.Text + "%'  ORDER BY USER_CO ");
            cbxproviderVisit.ItemsSource = dtVisit.Tables[0].DefaultView;
            cbxproviderVisit.IsDropDownOpen = true;

        }

        private void imgsearchbranch_MouseDown(object sender, MouseButtonEventArgs e)
        {

            dtVisit2 = db.RunReaderds(" select distinct USER_ID , ADDRS from V_PROVIDERS  where USER_CO=" + provider_idVisit + " and USER_ID  LIKE '%" + cbxbranchVisit.Text + "%' or ADDRS LIKE '%" + cbxbranchVisit.Text + "%'ORDER BY USER_ID");
            cbxbranchVisit.ItemsSource = dtVisit2.Tables[0].DefaultView;
            cbxbranchVisit.IsDropDownOpen = true;
        }

        private void imgsearchbrsssanch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dtVisit3 = db.RunReaderds(" select MEMBER_ID , MEMBER_NAME from IMS_ESCLATION_MEMBER where  MEMBER_ID  LIKE '%" + cbxpersonVisit.Text + "%' or MEMBER_NAME LIKE '%" + cbxpersonVisit.Text + "%'  ORDER BY MEMBER_ID");
            cbxpersonVisit.ItemsSource = dtVisit3.Tables[0].DefaultView;
        }


        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // txtsearc.Text = "";
        }
        // bool faa = false;
        private void txtsearc_MouseEnter(object sender, MouseEventArgs e)
        {
            if (faa == false)
            {
                txtsearch.Text = "";
                faa = true;
            }

        }
        void searchfill()
        {
            btnedite.Visibility = Visibility.Visible;
            // cleardata();
            try
            {
                System.Data.DataTable s = db.RunReader(" select * from IMS_COMPLAINTS WHERE COMPLAINT_ID = '" + txtsearch.Text + "'").Result;
                if (s.Rows.Count > 0)
                {
                    txtCoId_Copy.Text = s.Rows[0][0].ToString();
                    txtCoId.Text = s.Rows[0][1].ToString();
                    branch_id = s.Rows[0][2].ToString(); branch_addrs = s.Rows[0][14].ToString();
                    cbxbranch.Text = branch_id + " - " + branch_addrs;
                    provider_id = s.Rows[0][3].ToString(); provider_name = s.Rows[0][13].ToString();
                    cbxprovider.Text = provider_id + " - " + provider_name;
                    subject_id = s.Rows[0][4].ToString();
                    System.Data.DataTable tem = db.RunReader(" select SUBJECT_NAME from IMS_COM_SUBJECT WHERE SUBJECT_CODE = " + subject_id).Result;
                    subject_name = tem.Rows[0][0].ToString();
                    cbxsubject.Text = subject_id + " - " + subject_name;
                    sclated_id = s.Rows[0][5].ToString();
                    tem = db.RunReader(" select MEMBER_NAME from IMS_ESCLATION_MEMBER WHERE MEMBER_ID = " + sclated_id).Result;
                    sclated_name = tem.Rows[0][0].ToString();
                    cbxesclated.Text = sclated_id + " - " + sclated_name;
                    txtproblem.Text = s.Rows[0][6].ToString();
                    //imgPhoto.GetValue( s.Rows[0][7].ToString(); )
                    dtpcom.Text = s.Rows[0][7].ToString();
                    txtreplay.Text = s.Rows[0][8].ToString();
                    solved_id = s.Rows[0][9].ToString();
                    tem = db.RunReader(" select MEMBER_NAME from IMS_ESCLATION_MEMBER WHERE MEMBER_ID = " + solved_id).Result;
                    solved_name = tem.Rows[0][0].ToString(); ;
                    cbxsolvedby.Text = solved_id + " - " + solved_name;
                    txtcreatedby.Text = s.Rows[0][10].ToString();
                    txtupdatedby.Text = s.Rows[0][11].ToString();
                    if (s.Rows[0][12].ToString() == "1")
                    {
                        checkBox1sr.IsChecked = true;
                    }
                    // provider_name = s.Rows[0][14].ToString();

                }
                else
                {
                    MessageBox.Show("الرقم خاطئ"); return;

                }

                // s.Clear();
            }
            catch { }

        }



        private void btneditee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            searchfill();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            RotateTransform rotateTransform = new RotateTransform(350);
            imgsearch.RenderTransform = rotateTransform;

        }



        private void cbxsolvedby_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                solved_id = dt4.Tables[0].Rows[cbxsolvedby.SelectedIndex][0].ToString();
                solved_name = dt4.Tables[0].Rows[cbxsolvedby.SelectedIndex][1].ToString();
                cbxsolvedby.Text = solved_id + " - " + solved_name;

            }
            catch
            {// MessageBox.Show(ex.ToString()); 
            }
        }

        private void txtsearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchfill();
            }
        }

        private void imgsearch_MouseLeave(object sender, MouseEventArgs e)
        {
            RotateTransform rotateTransform = new RotateTransform(0);
            imgsearch.RenderTransform = rotateTransform;
        }

        private void btnedite_Click(object sender, RoutedEventArgs e)
        {
            btnedite.Visibility = Visibility.Hidden;
            DateTime datet = dtpcom.SelectedDate.Value.Date;
            string comDate = datet.ToString("dd-MMM-yy");
            //  checkfun();
            db.RunNonQuery("UPDATE IMS_COMPLAINTS SET BRANCH_CODE ='" + branch_id + "', PROVIDER_CODE = '" + provider_id + "', SUBJECT_CODE = '" + subject_id + "', ESCLATED_TO = '" + sclated_id + "', PROPLEM = '" + txtproblem.Text + "', COM_DATE = '" + comDate
                           + "', COM_REPLAY = '" + txtreplay.Text + "', SOLVED_BY = '" + solved_id + "', CREATED_BY = '" + txtcreatedby.Text + "', UPDATED_BY = '" + txtupdatedby.Text + "', COM_CHECKED = '" + checkServies + "', PROVIDER_NAME = '" + provider_name + "', BRANCH_NAME = '" + branch_addrs + "',COMM_ATTACH = '" + pathaa + "' WHERE COMPLAINT_ID = '" + txtsearch.Text + "'", "تم التعديل بنجاح");
            cleardata();



        }

        private void cbxsubject_KeyDown(object sender, KeyEventArgs e)
        {
            //cbxsubject.IsDropDownOpen = true;

        }

        private void cbxesclated_KeyDown(object sender, KeyEventArgs e)
        {
            cbxesclated.IsDropDownOpen = true;

        }

        private void cbxsolvedby_KeyDown(object sender, KeyEventArgs e)
        {
            cbxsolvedby.IsDropDownOpen = true;


        }

        private void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void tbcby_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtCoId_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        int checkServies = 0;
        private void checkBox1sr_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            checkServies = 1;
        }


        private void cbxprovider_PreviewTouchDown(object sender, TouchEventArgs e)
        {

        }





        private void cbxprovider_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void cbxprovider_PreviewKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtesclated_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cbxesclated_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxprovider_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {


            }





        }

        private void cbxbranch_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion
        #endregion
        #region Complaintsubject2


        System.Data.DataTable dtComplaintsubjectCust = new System.Data.DataTable("IMS_COM_SUBJECT");

        private void btnSearchComplainsubject_Clickcust(object sender, RoutedEventArgs e)
        {
            dtComplaintsubjectCust = db.RunReader("Select * FROM IMS_COM_SUBJECT WHERE SUBJECT_CODE LIKE '" + textsearchComplaintsubjectCust.Text + "%'").Result;
            dgviewcomplaintsubjectCust.ItemsSource = dtComplaintsubjectCust.DefaultView;
        }

        private void btnviowcomplainstubject_ClickCust(object sender, RoutedEventArgs e)
        {
            dtComplaintsubjectCust = db.RunReader("Select * FROM IMS_COM_SUBJECT   ").Result;
            dgviewcomplaintsubjectCust.ItemsSource = dtComplaintsubjectCust.DefaultView;
        }

        private void textsearchComplaintsubject_TextChangedCust(object sender, TextChangedEventArgs e)
        {
            dtComplaintsubjectCust = db.RunReader("Select * FROM IMS_COM_SUBJECT WHERE SUBJECT_CODE LIKE '" + textsearchComplaintsubjectCust.Text + "%'").Result;
            dgviewcomplaintsubjectCust.ItemsSource = dtComplaintsubjectCust.DefaultView;
        }





        #endregion
        #region solvedComplaintFormCust


        System.Data.DataTable dtSolvedComplaintCust = new System.Data.DataTable("IMS_COM_SUBJECT");
        private void txtshowdataSolvedComplaint_ClicCust(object sender, RoutedEventArgs e)
        {
            dtSolvedComplaintCust = db.RunReader("Select * FROM IMS_COMPLAINTS WHERE COM_CHECKED='N'  ").Result;
            dataGridSolvedComplaintCust.ItemsSource = dtSolvedComplaintCust.DefaultView;
        }

        private void Btn_SearchSolvedComplaint_ClickCust(object sender, RoutedEventArgs e)
        {
            dtSolvedComplaintCust = db.RunReader("Select * FROM IMS_COMPLAINTS WHERE COM_CHECKED=1 AND COMPLAINT_ID  LIKE '" + txtSearchSolvedComplaintCust.Text + "%'").Result;
            dataGridSolvedComplaintCust.ItemsSource = dtSolvedComplaintCust.DefaultView;
        }


        //check tany 7war com-checked da
        private void txtSearchSolvedComplaint_TextChangedCust(object sender, TextChangedEventArgs e)
        {
            dtSolvedComplaintCust = db.RunReader("Select * FROM IMS_COMPLAINTS WHERE COMPLAINT_ID  LIKE '" + txtSearchSolvedComplaintCust.Text + "%'").Result;
            dataGridSolvedComplaintCust.ItemsSource = dtSolvedComplaintCust.DefaultView;
        }



        #endregion


        #region VisitCust

        string provider_idVisitCust, provider_nameVisitCust, branch_idVisitCust, branch_addrsVisitCust, person_nameVisitCust, person_idVisitCust;




        DataSet dtVisitCust, dtVisitCust2, dtVisitCust3;
        void searchfillVisitCust()
        {
            btnediteeVisitCust.Visibility = Visibility.Visible;
            // cleardataVisitCust();
            try
            {


                System.Data.DataTable s = db.RunReader(" select * from IMS_VISITS WHERE VISIT_ID = '" + txtsearchVisitCust.Text + "'").Result;
                if (s.Rows.Count > 0)
                {

                    txtIdVisitCust.Text = s.Rows[0][4].ToString();
                    branch_idVisitCust = s.Rows[0][6].ToString(); branch_addrsVisitCust = s.Rows[0][8].ToString();
                    cbxbranchVisitCust.Text = branch_idVisitCust + " - " + branch_addrsVisitCust;
                    provider_idVisitCust = s.Rows[0][1].ToString(); provider_nameVisitCust = s.Rows[0][7].ToString();
                    cbxproviderVisitCust.Text = provider_idVisitCust + " - " + provider_nameVisitCust;
                    person_idVisitCust = s.Rows[0][3].ToString();
                    System.Data.DataTable tem = db.RunReader(" select MEMBER_NAME from IMS_ESCLATION_MEMBER WHERE MEMBER_ID = " + person_idVisitCust).Result;
                    person_nameVisitCust = tem.Rows[0][0].ToString();
                    cbxpersonVisitCust.Text = person_idVisitCust + " - " + person_nameVisitCust;
                    txtproblemVisitedCust.Text = s.Rows[0][9].ToString();
                    dtpcomVisitCust.Text = s.Rows[0][2].ToString();



                }
                else
                {
                    MessageBox.Show("invalied number", s.Rows[0][4].ToString()); return;

                }

                s.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
        private void cbxproviderVisitCust_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxproviderVisitCust.AllowDrop = true;
        }

        //private void cbxproviderVisitCust_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {

        //        dtVisitCust = db.RunReaderds("  select  distinct USER_CO, USER_N from V_PROVIDERS WHERE USER_CO = '" + cbxproviderVisitCust.Text + "'");

        //        if (dtVisitCust.Tables[0].Rows.Count > 0)
        //        {
        //            provider_idVisitCust = cbxproviderVisitCust.Text;
        //            provider_nameVisitCust = dtVisitCust.Tables[0].Rows[0][1].ToString();
        //            cbxproviderVisitCust.Text = provider_idVisitCust + " - " + provider_nameVisitCust;
        //            cbxbranchVisitCust.Text = "";

        //            fillPranchVisitCust();

        //        }
        //        else
        //        {
        //            MessageBox.Show("الرقم غير صحيح");
        //        }
        //    }

        //}

        private void cbxproviderVisitCust_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                cbxproviderVisitCust.Text = provider_idVisitCust + " - " + provider_nameVisitCust;
                cbxbranchVisitCust.Text = "";
                // System.Data.DataTable s = db.RunReader(" select * from IMS_COMPLAINTS WHERE PROVIDER_CODE = " + provider_idVisitCust + " ORDER BY COM_DATE ");

            }
            catch
            {
                // MessageBox.Show(ex.ToString());
            }
        }

        private void cbxbranchVisitCust_KeyDown(object sender, KeyEventArgs e)
        {
            cbxbranchVisitCust.IsDropDownOpen = true;
        }

        private void cbxbranchVisitCust_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                branch_idVisitCust = dtVisitCust2.Tables[0].Rows[cbxbranchVisitCust.SelectedIndex][0].ToString();
                branch_addrsVisitCust = dtVisitCust2.Tables[0].Rows[cbxbranchVisitCust.SelectedIndex][1].ToString();
                cbxbranchVisitCust.Text = branch_idVisitCust + " - " + branch_addrsVisitCust;


            }
            catch
            {
            }
        }


        private void cbxpersonVisitCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxpersonVisitCust_KeyDown(object sender, KeyEventArgs e)
        {
            cbxpersonVisitCust.IsDropDownOpen = true;
        }

        private void cbxpersonVisitCust_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                person_idVisitCust = dtVisitCust3.Tables[0].Rows[cbxpersonVisitCust.SelectedIndex][0].ToString();
                person_nameVisitCust = dtVisitCust3.Tables[0].Rows[cbxpersonVisitCust.SelectedIndex][1].ToString();
                cbxpersonVisitCust.Text = person_idVisitCust + " - " + person_nameVisitCust;

            }
            catch
            {// MessageBox.Show(ex.ToString());
            }
        }
        void cleardataVisitCust()
        {
            AutoNumeVisitCust();
            btnediteeVisitCust.Visibility = Visibility.Hidden;
            txtsearchVisitCust.Clear();
            cbxbranchVisitCust.Text = " ";
            cbxproviderVisitCust.Text = " ";
            cbxpersonVisitCust.Text = "";
            txtproblemVisitedCust.Text = "";
            //  cbvisitedCust.IsChecked = false;

            // cbvisitedCust.IsChecked = false;


            dtVisitCust3.Clear();
            fillProviderVisitCust();
            fillPersonVisitCust();

            check = 0;



        }
        void checkfunVisitCust()
        {


            if (cbxproviderVisitCust.Text == "" || cbxbranchVisitCust.Text == " - ")
            {
                provider_idVisitCust = "NULL";
                provider_nameVisitCust = "NULL";
            }
            if (cbxbranchVisitCust.Text == "" || cbxbranchVisitCust.Text == " - ")
            {
                branch_idVisitCust = "NULL";
                branch_addrsVisitCust = "NULL";
            }
            if (cbxpersonVisitCust.Text == "" || cbxpersonVisitCust.Text == " - ")
            {
                person_idVisitCust = "NULL";
                person_nameVisitCust = "NULL";
            }



        }
        private void btnsaveVisitCust_Click(object sender, RoutedEventArgs e)
        {

            DateTime datet = dtpcomVisitCust.SelectedDate.Value.Date;
            string comDate = datet.ToString("dd-MMM-yyy");
            int n = int.Parse(datet.ToString("ddMMyyyy"));
            string s = n.ToString() + txtIdVisitCust.Text;

            checkfunVisitCust();


            db.RunNonQuery("INSERT INTO IMS_VISITS (VISIT_ID, PR_CODE, VISIT_DATE, TECHNICAL_ID, VIS_SER, VIS_CHECKED, BRANCH_CODE, PR_NAME, BR_NAME, VIS_REASON) VALUES ('" + s + "', " + provider_idVisitCust + ",' " + comDate + "', " + person_idVisitCust + ", " + txtIdVisitCust.Text + ", " + check + ", " + branch_idVisitCust + ", '" + provider_nameVisitCust + "',' " + branch_addrsVisitCust + "', '" + txtproblemVisitedCust.Text + "')", "تم الحفظ بنجاح كود الزيارة " + s);
            cleardataVisitCust();
        }

        private void txtproblemVisitedCust_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        int check = 0;
        private void cbvisitedCust_Checked(object sender, RoutedEventArgs e)
        {
            check = 1;
        }


        private void txtsearchVisitCust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchfillVisitCust();
            }
        }

        private void txtsearchVisitCust_MouseEnter(object sender, MouseEventArgs e)
        {

            if (f == false)
            {
                txtsearchVisitCust.Text = "";
                f = true;
            }
        }

        private void btnClearVisitCust_Click(object sender, RoutedEventArgs e)
        {
            cleardataVisitCust();
            dtpcomVisitCust.Text = DateTime.Now.ToShortDateString().ToString();
        }

        private void btnediteeVisitCust_Click(object sender, RoutedEventArgs e)
        {
            DateTime datet = dtpcomVisitCust.SelectedDate.Value.Date;
            string comDate = datet.ToString("dd-MMM-yy");
            db.RunNonQuery("UPDATE IMS_VISITS SET PR_CODE = " + provider_idVisitCust + ", VISIT_DATE = '" + comDate + "', TECHNICAL_ID = " + person_idVisitCust + ", VIS_SER = " + txtIdVisitCust.Text + ", VIS_CHECKED = " + check + ", BRANCH_CODE =" + branch_idVisitCust + ", PR_NAME = '" + provider_nameVisitCust + "', BR_NAME ='" + branch_addrsVisitCust + "', VIS_REASON = '" + txtproblemVisitedCust.Text + "' WHERE VISIT_ID = " + txtsearchVisitCust.Text + "", "تم التعديل بنجاح");
            cleardataVisitCust();
            dtpcomVisitCust.Text = DateTime.Now.ToShortDateString().ToString();

        }

        void printxpsCust()
        {
            string comDate = dtpcomVisitCust.SelectedDate.Value.ToString("dd-MMM-yy");
            PrintDialog printDlg = new PrintDialog();
            FlowDocument doc = new FlowDocument(new System.Windows.Documents.Paragraph(new Run("VISIT_ID = " + txtsearchVisitCust.Text + " \n PR_CODE = " + provider_idVisitCust + " \n VISIT_DATE = " + comDate + " \n TECHNICAL_ID = " + person_idVisitCust + " \n VIS_SER = " + txtIdVisitCust.Text + " \n BRANCH_CODE =" + branch_idVisitCust + " \n PR_NAME = " + provider_nameVisitCust + "\n BR_NAME =" + branch_addrsVisitCust + "\n VIS_REASON = " + txtproblemVisitedCust.Text)));
            doc.Name = "FlowDoc";
            IDocumentPaginatorSource idpSource = doc;
            printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");

        }

        private void btnPrintVisitCust_Click(object sender, RoutedEventArgs e)
        {
            string comDate = dtpcomVisitCust.SelectedDate.Value.ToString("dd-MMM-yy");
            PrintDialog printDlg = new PrintDialog();
            FlowDocument doc = new FlowDocument(new System.Windows.Documents.Paragraph(new Run("ماموريات قسم الدعم الفنى\n" + "تاريخ الزيارة : " + dtpcomVisitCust.Text + "               رقم الزيارة : " + txtIdVisitCust.Text + "------------------------------------------------------------" + "  \n مقدم الخدمة : " + provider_nameVisitCust + "\nالعنوان :" + branch_addrsVisitCust + "\nاسم الموظف :" + person_nameVisitCust + "\n سبب الزيارة : " + txtproblemVisitedCust.Text + "------------------------------------------------------------\n" + "  توقيع الموظف          توقيع المدير المباشر         \n\n\n\n توقيع توقيع المدير الادارى")));
            //   doc.FontStyle = FontStyles.;
            doc.TextAlignment = TextAlignment.Center;
            doc.Name = "FlowDoc";
            IDocumentPaginatorSource idpSource = doc;
            printDlg.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");
        }



        private void cbxproviderVisitCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                provider_idVisitCust = dtVisitCust.Tables[0].Rows[cbxproviderVisitCust.SelectedIndex][0].ToString();
                provider_nameVisitCust = dtVisitCust.Tables[0].Rows[cbxproviderVisitCust.SelectedIndex][1].ToString();
                cbxproviderVisitCust.Text = provider_idVisitCust + " - " + provider_nameVisitCust;
                fillPranchVisitCust();

            }
            catch
            { }
        }

        private void fillProviderVisitCust()
        {
            dtVisitCust = db.RunReaderds(" select distinct USER_CO , USER_N from V_PROVIDERS where KIND_NO IN (1,3,4) ORDER BY USER_CO ");
            cbxproviderVisitCust.ItemsSource = dtVisitCust.Tables[0].DefaultView;


        }
        private void fillPranchVisitCust()
        {

            dtVisitCust2 = db.RunReaderds(" select distinct USER_ID , ADDRS from V_PROVIDERS  where USER_CO=" + provider_idVisitCust);
            cbxbranchVisitCust.ItemsSource = dtVisitCust2.Tables[0].DefaultView;

        }
        private void fillPersonVisitCust()
        {
            dtVisitCust3 = db.RunReaderds(" select MEMBER_ID , MEMBER_NAME from IMS_ESCLATION_MEMBER  ORDER BY MEMBER_ID");
            cbxpersonVisitCust.ItemsSource = dtVisitCust3.Tables[0].DefaultView;
        }
        private void AutoNumeVisitCust()
        {
            Filltbl("select max(VIS_SER) from IMS_VISITS ");
            if (tbl.Rows[0][0].ToString() != DBNull.Value.ToString())
                txtIdVisitCust.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
            else
                txtIdVisitCust.Text = "1";
        }




        #endregion

        #region FollowUpCust
        System.Data.DataTable tblFollowCust = new System.Data.DataTable();

        DataSet dtFollowCust, dtFollowCust2;
        string provider_idFollowCust, provider_nameFollowCust, branch_idFollowCust, branch_addrsFollowCust;

        private void cbxbranchFollowCust_KeyDown(object sender, KeyEventArgs e)
        {
            cbxbranchFollowCust.IsDropDownOpen = true;
        }

        private void cbxbranchFollowCust_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                branch_idFollowCust = dtFollowCust2.Tables[0].Rows[cbxbranchFollowCust.SelectedIndex][0].ToString();
                branch_addrsFollowCust = dtFollowCust2.Tables[0].Rows[cbxbranchFollowCust.SelectedIndex][1].ToString();
                cbxbranchFollowCust.Text = branch_idFollowCust + " - " + branch_addrsFollowCust;
                System.Data.DataTable s = db.RunReader(" select KIND_NO , USER_CO , USER_N ,USER_ID , USER_NAME , USER_PWD , TEL , MOB , ADDRS , GOVER_ID , GOVER_NAME from V_PROVIDERS WHERE USER_ID = " + branch_idFollowCust).Result;
                dgFollowCust.DataContext = s.DefaultView;
                txtbranch_idCust.Text = branch_idFollowCust;
                txtbranch_nameCust.Text = branch_addrsFollowCust;


                System.Data.DataTable tblFollowCust1 = db.RunReader(" select  WORKS , COMMENTS , CONTACT_1 , CONTACT_2 , PHONE_1 , PHONE_2 , TITLE_1 , TITLE_2  from IMS_PRV WHERE USER_ID = " + branch_idFollowCust).Result;
                try
                {
                    string x = tblFollowCust1.Rows[0][0].ToString();
                    //MessageBox.Show("feeh mwgoood");
                    btnediteeFollowCust.Visibility = Visibility.Visible;
                    btnsaveFollowCust.Visibility = Visibility.Hidden;
                }
                catch
                {
                    btnsaveFollowCust.Visibility = Visibility.Visible;
                    btnediteeFollowCust.Visibility = Visibility.Hidden;
                }



                System.Data.DataTable tblFollowCust = db.RunReader(" select  WORKS , COMMENTS , CONTACT_1 , CONTACT_2 , PHONE_1 , PHONE_2 , TITLE_1 , TITLE_2  from IMS_PRV WHERE USER_ID = " + branch_idFollowCust).Result;
                try
                {
                    //txtbranch_nameCust.Text = tblFollowCust.Rows[0][0].ToString();
                    txtcommentCust.Text = tblFollowCust.Rows[0][1].ToString();
                    txtcontact1Cust.Text = tblFollowCust.Rows[0][2].ToString();
                    txtcontact2Cust.Text = tblFollowCust.Rows[0][3].ToString();
                    txtphone1Cust.Text = tblFollowCust.Rows[0][4].ToString();
                    txtphone2Cust.Text = tblFollowCust.Rows[0][5].ToString();
                    txtaddress1Cust.Text = tblFollowCust.Rows[0][6].ToString();
                    txtaddres2Cust.Text = tblFollowCust.Rows[0][7].ToString();
                }
                catch { }
                // cleardataFollowCust();




            }
            catch
            {
            }
        }

        private void cbxproviderFollowCust_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxproviderFollowCust.AllowDrop = true;
        }



        private void cbxproviderFollowCust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                dtFollowCust = db.RunReaderds("  select  distinct USER_CO, USER_N from V_PROVIDERS WHERE USER_CO = '" + cbxproviderFollowCust.Text + "'");

                if (dtFollowCust.Tables[0].Rows.Count > 0)
                {
                    provider_idFollowCust = cbxproviderFollowCust.Text;
                    provider_nameFollowCust = dtFollowCust.Tables[0].Rows[0][1].ToString();
                    cbxproviderFollowCust.Text = provider_idFollowCust + " - " + provider_nameFollowCust;
                    cbxbranchFollowCust.Text = "";
                    System.Data.DataTable s = db.RunReader(" select KIND_NO , USER_CO , USER_N ,USER_ID , USER_NAME , USER_PWD , TEL , MOB , ADDRS , GOVER_ID , GOVER_NAME from V_PROVIDERS WHERE USER_CO = " + provider_idFollowCust).Result;
                    dgFollowCust.DataContext = s.DefaultView;
                    txtprovider_idFollowCust.Text = provider_idFollowCust;
                    txtprovider_nameFollowCust.Text = provider_nameFollowCust;

                    fillPranchFollowCust();

                }
                else
                {
                    MessageBox.Show("الرقم غير صحيح");
                }
            }

        }

        private void cbxproviderFollowCust_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                cbxproviderFollowCust.Text = provider_idFollowCust + " - " + provider_nameFollowCust;
                cbxbranchFollowCust.Text = "";
                System.Data.DataTable s = db.RunReader(" select KIND_NO , USER_CO , USER_N ,USER_ID , USER_NAME , USER_PWD , TEL , MOB , ADDRS , GOVER_ID , GOVER_NAME from V_PROVIDERS WHERE USER_CO = " + provider_idFollowCust).Result;
                dgFollowCust.DataContext = s.DefaultView;
                txtprovider_idFollowCust.Text = provider_idFollowCust;
                txtprovider_nameFollowCust.Text = provider_nameFollowCust;


            }
            catch
            {

            }
        }



        private void dgFollowCust_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dgFollowCust.SelectedItems[0];
                // DataRow
                cleardataFollowCust();
                branch_idFollowCust = row[3].ToString();
                branch_addrsFollowCust = row[8].ToString();

            }
            catch { }

            System.Data.DataTable tblFollowCust1 = db.RunReader(" select  WORKS , COMMENTS , CONTACT_1 , CONTACT_2 , PHONE_1 , PHONE_2 , TITLE_1 , TITLE_2  from IMS_PRV WHERE USER_ID = " + branch_idFollowCust).Result;
            try
            {
                string x = tblFollowCust1.Rows[0][0].ToString();
                //MessageBox.Show("feeh mwgoood");
                btnediteeFollowCust.Visibility = Visibility.Visible;
                btnsaveFollowCust.Visibility = Visibility.Hidden;
            }
            catch
            {
                btnsaveFollowCust.Visibility = Visibility.Visible;
                btnediteeFollowCust.Visibility = Visibility.Hidden;
            }



            txtbranch_idCust.Text = branch_idFollowCust;
            txtbranch_nameCust.Text = branch_addrsFollowCust;
            //  MessageBox.Show(row[3].ToString());

            System.Data.DataTable tblFollowCust = db.RunReader(" select  WORKS , COMMENTS , CONTACT_1 , CONTACT_2 , PHONE_1 , PHONE_2 , TITLE_1 , TITLE_2  from IMS_PRV WHERE USER_ID = " + branch_idFollowCust).Result;
            try
            {
                txtbranch_nameCust.Text = tblFollowCust.Rows[0][0].ToString();
                txtcommentCust.Text = tblFollowCust.Rows[0][1].ToString();
                txtcontact1Cust.Text = tblFollowCust.Rows[0][2].ToString();
                txtcontact2Cust.Text = tblFollowCust.Rows[0][3].ToString();
                txtphone1Cust.Text = tblFollowCust.Rows[0][4].ToString();
                txtphone2Cust.Text = tblFollowCust.Rows[0][5].ToString();
                txtaddress1Cust.Text = tblFollowCust.Rows[0][6].ToString();
                txtaddres2Cust.Text = tblFollowCust.Rows[0][7].ToString();
            }
            catch { }

        }
        void cleardataFollowCust()
        {
            txtbranch_nameCust.Text = "";
            txtcommentCust.Text = "";
            txtcontact1Cust.Text = "";
            txtcontact2Cust.Text = "";
            txtphone1Cust.Text = "";
            txtphone2Cust.Text = "";
            txtaddress1Cust.Text = "";
            txtaddres2Cust.Text = "";
            txtbranch_idCust.Text = "";
            txtbranch_nameCust.Text = "";
            //  txtbranch_name_Copy.Text = "";
        }
        private void btnsaveFollowCust_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("INSERT INTO IMS_PRV (USER_ID, WORKS, COMMENTS, USER_CO, CONTACT_1, PHONE_1, TITLE_1, TITLE_2, CONTACT_2, PHONE_2) VALUES ('" +
                                                      branch_idFollowCust + "', '" + cbxstateCust.Text + "', '" + txtcommentCust.Text + "', '" + provider_idFollowCust + "', '" + txtcontact1Cust.Text + "', '" + txtphone1Cust.Text
                                                      + "', '" + txtaddress1Cust.Text + "', '" + txtaddres2Cust.Text + "', '" + txtcontact2Cust.Text + "', '" + txtphone2Cust.Text + "')", "Done");
            cleardataFollowCust();
        }

        private void btnediteeFollowCust_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("UPDATE IMS_PRV SET WORKS = '" + txtbranch_nameCust.Text + "', COMMENTS = '" + txtcommentCust.Text + "', USER_CO = '" + provider_idFollowCust + "', CONTACT_1 = '" + txtcontact1Cust.Text + "', PHONE_1 = '" + txtphone1Cust.Text + "', TITLE_1 = '" + txtaddress1Cust.Text + "', TITLE_2 = '" + txtaddres2Cust.Text + "', CONTACT_2 = '" + txtcontact2Cust.Text + "', PHONE_2 = '" + txtphone2Cust.Text + "' WHERE USER_ID =" + txtbranch_idCust.Text, "تم التعديل بنجاح");
            cleardataFollowCust();
        }


        private void txtbranch_nameCust_DropDownClosed(object sender, EventArgs e)
        {


            Service_Request s = new Service_Request(provider_idFollowCust, provider_nameFollowCust, branch_idFollowCust, branch_addrsFollowCust);
            s.Show();






        }


        private void btnclearFollow_cust(object sender, RoutedEventArgs e)
        {
            cleardataFollowCust();
            cbxbranchFollowCust.Text = "";
            cbxproviderFollowCust.Text = "";
            cbxstateCust_Copy.Text = "";
            dgFollowCust.ItemsSource = null;
            txtprovider_nameFollowCust.Text = "";
            cbxstateCust.Text = "";
            txtprovider_idFollowCust.Text = "";
        }

        private void cbxproviderFollowCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                provider_idFollowCust = dtFollowCust.Tables[0].Rows[cbxproviderFollowCust.SelectedIndex][0].ToString();
                provider_nameFollowCust = dtFollowCust.Tables[0].Rows[cbxproviderFollowCust.SelectedIndex][1].ToString();
                cbxproviderFollowCust.Text = provider_idFollowCust + " - " + provider_nameFollowCust;
                fillPranchFollowCust();

            }
            catch
            { }
        }




        private void fillProviderFollowCust()
        {
            dtFollowCust = db.RunReaderds(" select distinct USER_CO , USER_N from V_PROVIDERS where KIND_NO IN (1,3,4) ORDER BY USER_CO ");
            cbxproviderFollowCust.ItemsSource = dtFollowCust.Tables[0].DefaultView;




        }
        private void imgsearchCust_branchfollow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dt2 = db.RunReaderds(" select distinct USER_ID , ADDRS from V_PROVIDERS  where USER_CO=" + provider_idtFollow + " and USER_ID  LIKE '%" + cbxbranchFollowCust.Text + "%' or ADDRS LIKE '%" + cbxbranchFollowCust.Text + "%'ORDER BY USER_ID");
            cbxbranchFollowCust.ItemsSource = dt2.Tables[0].DefaultView;
            cbxbranchFollowCust.IsDropDownOpen = true;
        }

        private void imgsearchCust_providVisit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dtFollowCust = db.RunReaderds("  select  distinct USER_CO, USER_N from V_PROVIDERS WHERE KIND_NO IN (1,3,4) and USER_CO  LIKE '%" + cbxproviderVisitCust.Text + "%' or USER_N LIKE '%" + cbxproviderVisitCust.Text + "%'  ORDER BY USER_CO ");
            cbxproviderVisitCust.ItemsSource = dt.Tables[0].DefaultView;
            cbxproviderVisitCust.IsDropDownOpen = true;

        }

        private void imgsearchCustbranch_MouseDown(object sender, MouseButtonEventArgs e)
        {

            dtVisitCust2 = db.RunReaderds(" select distinct USER_ID , ADDRS from V_PROVIDERS  where USER_CO=" + provider_idCust + " and USER_ID  LIKE '%" + cbxbranchVisitCust.Text + "%' or ADDRS LIKE '%" + cbxbranchVisitCust.Text + "%'ORDER BY USER_ID");
            cbxbranchVisitCust.ItemsSource = dt2.Tables[0].DefaultView;
            cbxbranchVisitCust.IsDropDownOpen = true;
        }

        private void imgsearchCustbrsssanch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dtVisitCust3 = db.RunReaderds(" select MEMBER_ID , MEMBER_NAME from IMS_ESCLATION_MEMBER where  MEMBER_ID  LIKE '%" + cbxpersonVisitCust.Text + "%' or MEMBER_NAME LIKE '%" + cbxpersonVisitCust.Text + "%'  ORDER BY MEMBER_ID");
            cbxpersonVisitCust.ItemsSource = dtVisitCust3.Tables[0].DefaultView;
        }

        private void imgsearchCust_providfollow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dt = db.RunReaderds("  select  distinct USER_CO, USER_N from V_PROVIDERS WHERE KIND_NO IN (1,3,4) and USER_CO  LIKE '%" + cbxproviderFollowCust.Text + "%' or USER_N LIKE '%" + cbxproviderFollowCust.Text + "%'  ORDER BY USER_CO ");
            cbxproviderFollowCust.ItemsSource = dt.Tables[0].DefaultView;
            cbxproviderFollowCust.IsDropDownOpen = true;
        }


        private void fillPranchFollowCust()
        {

            dtFollowCust2 = db.RunReaderds(" select distinct USER_ID , ADDRS from V_PROVIDERS  where USER_CO=" + provider_idFollowCust + "ORDER BY USER_ID");
            cbxbranchFollowCust.ItemsSource = dtFollowCust2.Tables[0].DefaultView;

        }
        #endregion



        #region Serviec_Requestcust
        System.Data.DataTable tblServiceCust = new System.Data.DataTable();
        string provider_idCust, provider_nameCust, branch_idCust, branch_addrsCust, subject_nameCust, subject_idCust, sclated_nameCust, sclated_idCust, solved_nameCust, solved_idCust;

        private void FilltblServiceCust(string SelectStatment)
        {
            try
            {
                tblServiceCust.Clear();
                tblServiceCust.Columns.Clear();
                tblServiceCust = db.RunReader(SelectStatment).Result;
            }
            catch { }
        }
        DataSet dtserviceCust, dtserviceCust2, dtserviceCust3, dtserviceCust4, dsprint;
        void fillprovidertypeCust()
        {
            try
            {
                DataSet zz = db.RunReaderds(" select distinct PRV_TYPE , TYP_ANAME from PROVIDER_TYP ORDER BY PRV_TYPE ");
                cbxproviderCust_Copy.ItemsSource = zz.Tables[0].DefaultView;

            }
            catch { }
        }
        private void fillProviderCust()
        {
            try
            {
                // select distinct PR_CODE , PR_ENAME from SERV_PROVIDERS where PRV_TYPE = '" + cbxproviderCust_Copy.Text + "' ORDER BY PR_CODE
                //  MessageBox.Show(cbxproviderCust_Copy.Text);
                dtserviceCust = db.RunReaderds(" select distinct PR_CODE , PR_ENAME from SERV_PROVIDERS where PRV_TYPE ='" + cbxproviderCust_Copy.Text + "' ORDER BY PR_CODE ");
                cbxproviderCust.ItemsSource = dtserviceCust.Tables[0].DefaultView;

            }
            catch { }
        }


        private void fillPranchCust()
        {
            try
            {
                dtserviceCust2 = db.RunReaderds(" select distinct USER_ID , ADDRS from V_PROVIDERS  where USER_CO=" + provider_idCust + "ORDER BY USER_ID");
                cbxbranchCust.ItemsSource = dtserviceCust2.Tables[0].DefaultView;
            }
            catch { }
        }

        private void fillsubjectCust()
        {
            try
            {
                dtserviceCust3 = db.RunReaderds(" select SUBJECT_CODE ,SUBJECT_NAME from IMS_COM_SUBJECT where SUB_TYPE='PR'  ORDER BY SUBJECT_CODE");
                cbxsubjectCust.ItemsSource = dtserviceCust3.Tables[0].DefaultView;
            }
            catch { }
        }
        private void AutoNumeCust()
        {
            try
            {
                FilltblServiceCust("select max(COM_SER) from IMS_COMPLAINTS ");
                if (tblServiceCust.Rows[0][0].ToString() != DBNull.Value.ToString())
                    txtCoIdCust.Text = (Convert.ToInt32(tblServiceCust.Rows[0][0].ToString()) + 1).ToString();
                else
                    txtCoIdCust.Text = "1";


            }
            catch { }

        }


        private void ButtonvisitCust(object sender, RoutedEventArgs e)
        {
            try
            {
                checkfunCust();

                //Visits a = new Visits(provider_idCust, provider_nameCust, branch_idCust, branch_addrsCust, sclated_idCust, sclated_nameCust, provider_id2, provider_type);
                //a.Show();
            }
            catch { }
        }

        private void cbxproviderCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                provider_idCust = dtserviceCust.Tables[0].Rows[cbxproviderCust.SelectedIndex][0].ToString();
                provider_nameCust = dtserviceCust.Tables[0].Rows[cbxproviderCust.SelectedIndex][1].ToString();
                fillPranchCust();
                cbxbranchCust.Text = "";
            }
            catch
            { }
        }


        private void clear_cliCust(object sender, RoutedEventArgs e)
        {
            try
            {
                //  btnediteCust.Visibility = Visibility.Hidden;
                ClearCust();
                datpcomCust.Text = "";
            }
            catch { }
        }
        private void cbxproviderCust_Copy_DropDownClosed(object sender, EventArgs e)
        {
            fillProviderCust();
        }



        private void cbxbranchCust_DropDownClosed(object sender, EventArgs e)
        {

            try
            {

                branch_idCust = dtserviceCust2.Tables[0].Rows[cbxbranchCust.SelectedIndex][0].ToString();
                branch_addrsCust = dtserviceCust2.Tables[0].Rows[cbxbranchCust.SelectedIndex][1].ToString();



            }
            catch
            {
                branch_idCust = "0";

            }
        }

        private void cbxsubjectCust_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                subject_idCust = dtserviceCust3.Tables[0].Rows[cbxsubjectCust.SelectedIndex][0].ToString();
                subject_nameCust = dtserviceCust3.Tables[0].Rows[cbxsubjectCust.SelectedIndex][1].ToString();



            }
            catch
            {
                // MessageBox.Show(ex.ToString());
            }
        }




        //   long elapsedTicks = datpcomCust.Ticks - centuryBegin.Ticks;
        //    TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);

        void checkfunCust()
        {
            try
            {
                if (cbxproviderCust.Text == "" || cbxbranchCust.Text == " - ")
                {
                    provider_idCust = "0";
                    provider_nameCust = "NULL";
                }
                if (cbxbranchCust.Text == "" || cbxbranchCust.Text == " - ")
                {
                    branch_idCust = "0";
                    branch_addrsCust = "NULL";
                }
                if (cbxsubjectCust.Text == "" || cbxsubjectCust.Text == " - ")
                {
                    subject_idCust = "0";
                    subject_nameCust = "NULL";
                }

            }
            catch { }
            if (branch_idCust == "")
                branch_idCust = "0";

        }
        void ClearCust()
        {
            try
            {
                AutoNumeCust();
                btnediteCust.Visibility = Visibility.Hidden;
                txtsearchCust.Clear();
                cbxbranchCust.Text = " ";
                cbxproviderCust.Text = " ";
                cbxsubjectCust.Text = " ";

                txtproblemCust.Text = "";
                txtreplayCust.Text = "";
                txtCoIdCust_Copy.Text = "";

                dtserviceCust.Clear();
                try
                {
                    dtserviceCust2.Clear();
                }
                catch
                { }
                dtserviceCust3.Clear();

                fillprovidertypeCust();
                fillsubjectCust();

                checkServiesCust = 0;
            }
            catch { }


        }
        private void ButtonsaveCust(object sender, System.Windows.RoutedEventArgs e)
        {

            DateTime datet = datpcomCust.SelectedDate.Value.Date;
            string comDate = datet.ToString("dd-MMM-yy");
            int n = int.Parse(datet.ToString("ddMMyy"));
            string s = n.ToString() + txtCoIdCust.Text;

            checkfunCust();
            string timess = probcbxtime21.Text + ":" + probcbxtime22.Text + " " + probcbxtime23.Text;

            db.RunNonQuery(@"INSERT INTO IMS_COMPLAINTS (COMPLAINT_ID, COM_SER, BRANCH_CODE, PROVIDER_CODE, SUBJECT_CODE,  PROPLEM,  COM_DATE, CREATED_BY,COM_CHECKED, PROVIDER_NAME, BRANCH_NAME ,PROVIDER_TYPE ,TIME ) VALUES   ('"
                                 + s + "','" + txtCoIdCust.Text + "','" + branch_idCust + "','" + provider_idCust + "','" + subject_idCust + "','" + txtproblemCust.Text + "','" + comDate + "','" + jjUesrname + "','N','" + provider_nameCust + "','" + branch_addrsCust + "','" + cbxproviderCust_Copy.Text + "','" + timess + "')", "خدمتكم محل اهتمامنا وسوف يتم اتخاد الاجراءات اللازمة " + "\n" + " والرجوع لسيادتكم فى اسرع وقت ممكن ");
            txtCoIdCust_Copy.Text = s;
            // checkfunCust();
        }


        private void imgsearchCust_Copy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // select distinct PR_CODE , PR_ENAME from SERV_PROVIDERS where PRV_TYPE = '" + cbxproviderCust_Copy.Text + "' ORDER BY PR_CODE
                dtserviceCust = db.RunReaderds(" select distinct PR_CODE , PR_ENAME from SERV_PROVIDERS where PRV_TYPE ='" + cbxproviderCust_Copy.Text + "' and (PR_CODE  LIKE '%" + cbxproviderCust.Text + "%' or uper(PR_ENAME) LIKE '%" + cbxproviderCust.Text.ToUpper() + "%' ) ORDER BY PR_CODE ");
                cbxproviderCust.ItemsSource = dtserviceCust.Tables[0].DefaultView;
                cbxproviderCust.IsDropDownOpen = true;
            }
            catch { }
        }

        private void imgsearchCust_branch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dtserviceCust2 = db.RunReaderds(" select distinct USER_ID , ADDRS from V_PROVIDERS  where USER_CO=" + provider_idCust + " and USER_ID  LIKE '%" + cbxbranchCust.Text + "%' or ADDRS LIKE '%" + cbxbranchCust.Text + "%'ORDER BY USER_ID");
                cbxbranchCust.ItemsSource = dtserviceCust2.Tables[0].DefaultView;
                cbxbranchCust.IsDropDownOpen = true;
            }
            catch { }
        }

        private void imgsearchCust_sub_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dtserviceCust3 = db.RunReaderds(" select SUBJECT_CODE ,SUBJECT_NAME from IMS_COM_SUBJECT where SUBJECT_CODE  LIKE '%" + cbxsubjectCust.Text + "%' or SUBJECT_NAME LIKE '%" + cbxsubjectCust.Text + "%'   ORDER BY SUBJECT_CODE");
                cbxsubjectCust.ItemsSource = dtserviceCust3.Tables[0].DefaultView;
                cbxsubjectCust.IsDropDownOpen = true;
            }
            catch { }
        }

        bool faa = false;
        private void txtsearc_MouseEnterCust(object sender, MouseEventArgs e)
        {
            if (faa == false)
            {
                txtsearchCust.Text = "";
                faa = true;
            }

        }
        void searchfillCust()
        {
            btnediteCust.Visibility = Visibility.Visible;
            // checkfunCust();
            try
            {
                System.Data.DataTable s = db.RunReader(" select * from IMS_COMPLAINTS WHERE COMPLAINT_ID = '" + txtsearchCust.Text + "'").Result;
                if (s.Rows.Count > 0)
                {

                    txtCoIdCust_Copy.Text = s.Rows[0][0].ToString();
                    txtCoIdCust.Text = s.Rows[0][1].ToString();
                    branch_idCust = s.Rows[0][2].ToString(); branch_addrsCust = s.Rows[0][14].ToString();
                    cbxbranchCust.Text = s.Rows[0][14].ToString();
                    provider_idCust = s.Rows[0][3].ToString(); provider_nameCust = s.Rows[0][13].ToString();
                    cbxproviderCust.Text = provider_nameCust;
                    subject_idCust = s.Rows[0][4].ToString();
                    System.Data.DataTable tem = db.RunReader(" select SUBJECT_NAME from IMS_COM_SUBJECT WHERE SUBJECT_CODE = " + subject_idCust).Result;
                    subject_nameCust = tem.Rows[0][0].ToString();
                    cbxsubjectCust.Text = subject_nameCust;

                    txtproblemCust.Text = s.Rows[0][6].ToString();
                    datpcomCust.Text = s.Rows[0][7].ToString();
                    //  cbxproviderCust_Copy.SelectedIndex =Convert.ToInt32( s.Rows[0][18].ToString());


                }
                else
                {
                    MessageBox.Show("الرقم خاطئ"); return;

                }

                // s.Clear();
            }
            catch { }

        }



        private void btnediteCuste_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Image_CustMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                searchfillCust();
            }
            catch { }
        }

        private void Image_CustMouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                RotateTransform rotateTransform = new RotateTransform(350);
                imgsearchCust.RenderTransform = rotateTransform;
            }
            catch { }
        }

        private void downloadpic(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";

                if (op.ShowDialog() == true)
                {
                    imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                    pathaa = op.FileName;
                }
            }
            catch { }
        }


        private void txtsearchCust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchfillCust();
            }
        }

        private void imgsearchCust_MouseLeave(object sender, MouseEventArgs e)
        {
            RotateTransform rotateTransform = new RotateTransform(0);
            imgsearchCust.RenderTransform = rotateTransform;
        }

        private void btnediteCust_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnediteCust.Visibility = Visibility.Hidden;
                DateTime datet = datpcomCust.SelectedDate.Value.Date;
                string comDate = datet.ToString("dd-MMM-yy");
                db.RunNonQuery("UPDATE IMS_COMPLAINTS SET BRANCH_CODE ='" + branch_idCust + "', PROVIDER_CODE = '" + provider_idCust + "', SUBJECT_CODE = '" + subject_idCust + "', PROPLEM = '" + txtproblemCust.Text + "', COM_DATE = '" + comDate
                               + "', PROVIDER_NAME = '" + provider_nameCust + "', BRANCH_NAME = '" + branch_addrsCust + "',PROVIDER_TYPE = '" + cbxproviderCust_Copy.Text + "' ,UPDATED_BY = '" + jjUesrname + "' WHERE COMPLAINT_ID = '" + txtCoIdCust_Copy.Text + "'", "تم التعديل بنجاح");
                ClearCust();
            }
            catch { }
        }

        private void cbxsubjectCust_KeyDown(object sender, KeyEventArgs e)
        {
            //cbxsubjectCust.IsDropDownOpen = true;

        }



        private void txtsearchCust_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbcbyCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtCoIdCust_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        int checkServiesCust = 0;
        private void checkBox1srCus_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            checkServiesCust = 1;
        }

        private void datpcomCust_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxproviderCust_PreviewTouchDown(object sender, TouchEventArgs e)
        {

        }

        private void cbxproviderCust_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void cbxproviderCust_PreviewKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void cbxesclatedCust_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxproviderCust_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            { }
        }

        private void cbxbranchCust_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion


        #region abdo Problem


        string probDepartName, probEmployee, probnum;
        DataSet dtproblemdepart, dtprobEmployee, dtprobreason;
        private void probAutoNume()
        {

            FilltblService("select count(CODE) from COMPANIES_HR ");
            if (tblService.Rows[0][0].ToString() != DBNull.Value.ToString())
                probnum = (Convert.ToInt32(tblService.Rows[0][0].ToString()) + 1).ToString();
            else
                probnum = "1";




        }
        void fillproblemdepart()
        {
            dtproblemdepart = db.RunReaderds(" select distinct  DEPT_NAME from AGENT_DEPARTMENT ORDER BY DEPT_NAME ");
            probcbxDepartment.ItemsSource = dtproblemdepart.Tables[0].DefaultView;

        }

        private void fillProbEmployee()
        {


            dtprobEmployee = db.RunReaderds(" select distinct NAME from AGENT  where USERTYPE='DMS Member' and AGENT_DEPT like '%" + probcbxDepartment.Text + "%' ORDER BY NAME");
            probcbxEmployee.ItemsSource = dtprobEmployee.Tables[0].DefaultView;

        }
        private void fillprobreason()
        {
            dtprobreason = db.RunReaderds(" select SUBJECT_NAME from IMS_COM_SUBJECT where SUB_TYPE ='HR' ORDER BY SUBJECT_NAME");
            probcbxReason.ItemsSource = dtprobreason.Tables[0].DefaultView;
        }
        void clearprob()
        {
            probcbxDepartment.Text = "";
            probcbxEmployee.Text = "";
            probdpTime.Text = "";
            probtxtdtime.Text = "";
            probcbxReason.Text = "";
            probtxtdescribtion.Text = "";
            probDepartName = "";
            probEmployee = "";
            probnum = "";
            probtxtSearch.Text = "";
            fillProbEmployee();
            probLCode.Content = "**********";
            probLReplay.Content = "";
            probtxtdtime.IsEnabled = false;
            probcbxtime3.Text = "";
            probcbxtime2.Text = "";
            probcbxtime1.Text = "";
            try
            {
                probcbxtime3.Visibility = Visibility.Visible;
                probcbxtime2.Visibility = Visibility.Visible;
                probcbxtime1.Visibility = Visibility.Visible;
                probtxtdtime.Visibility = Visibility.Hidden;
                probbtnSave.IsEnabled = true;
                probbtnEdite.IsEnabled = false;
            }
            catch { }


        }
        private void probcbxDepartment_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                probDepartName = dtproblemdepart.Tables[0].Rows[probcbxDepartment.SelectedIndex][0].ToString();
                fillProbEmployee();


            }
            catch
            {

            }
        }
        private void probcbxEmployee_DropDownClosed(object sender, EventArgs e)
        {
            probEmployee = probcbxEmployee.Text;
        }
        private void probbtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    DateTime datesst = probdpTime.SelectedDate.Value.Date;

                }
                catch
                {
                    MessageBox.Show("ادخل تاريخ ");
                }

                DateTime datet = probdpTime.SelectedDate.Value.Date;
                string comDate = datet.ToString("dd-MMM-yy");
                datet = DateTime.Now;
                string comDatenow = datet.ToString("dd-MMM-yy");

                int n = int.Parse(datet.ToString("ddMMyyyy"));
                string s = n.ToString() + probnum;
                probLCode.Content = s;





                db.RunNonQuery(@"INSERT INTO COMPANIES_HR (CODE, DEPARTMENT, EMPLOYEE, COMP_DATE, REASON, DESCRIPTION, CREATED_BY,  CREATED_DATE,COMP_TIME , REPLAYED)
VALUES   ('" + s + "','" + probDepartName + "','" + probcbxEmployee.Text + "','" + comDate + "','" + probcbxReason.Text + "','" + probtxtdescribtion.Text + "','" + User.Name + "','" + comDatenow + "','" + probcbxtime1.Text + ":" + probcbxtime2.Text + " " + probcbxtime3.Text + "','N')", "تم الحفظ بنجاح رقم العملية  " + s);

                probAutoNume();
                probLReplay.Content = "خدمتكم محل اهتمامنا وسوف يتم اتخاد الاجراءات اللازمة " + "\n" + " والرجوع لسيادتكم فى اسرع وقت ممكن ";
                probtxtSearch.Text = s;
            }
            catch { }
        }

        private void probbtnnew_Click(object sender, RoutedEventArgs e)
        { clearprob(); }

        private void probbtnEdite_Click(object sender, RoutedEventArgs e)
        {
            DateTime datet = probdpTime.SelectedDate.Value.Date;
            string comDate = datet.ToString("dd-MMM-yy");
            datet = DateTime.Now;
            string comDatenow = datet.ToString("dd-MMM-yy");
            //  checkfun();
            db.RunNonQuery(@"UPDATE COMPANIES_HR SET DEPARTMENT ='" + probcbxDepartment.Text + "', EMPLOYEE = '" + probcbxEmployee.Text + "', COMP_DATE = '" + comDate + "', REASON = '" + probcbxReason.Text + "', DESCRIPTION = '" + probtxtdescribtion.Text + "', COMP_TIME = '" + probtxtdtime.Text + "', UPDATED_BY = '" + User.Name + "', UPDATED_DATE = '" + comDatenow + "' where CODE =" + probtxtSearch.Text, "تم التعديل بنجاح");
            probbtnSave.IsEnabled = true;
            probbtnEdite.IsEnabled = false;

            clearprob();
        }
        private void probbtnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                System.Data.DataTable s = db.RunReader(" select * from COMPANIES_HR WHERE CODE = '" + probtxtSearch.Text + "'").Result;
                if (s.Rows.Count > 0)
                {
                    probcbxtime3.Visibility = Visibility.Hidden;
                    probcbxtime2.Visibility = Visibility.Hidden;
                    probcbxtime1.Visibility = Visibility.Hidden;
                    probtxtdtime.Visibility = Visibility.Visible;

                    probbtnSave.IsEnabled = false;
                    probbtnEdite.IsEnabled = true;


                    probcbxDepartment.Text = s.Rows[0][1].ToString();
                    probcbxEmployee.Text = s.Rows[0][2].ToString();
                    probdpTime.Text = s.Rows[0][3].ToString();
                    probcbxReason.Text = s.Rows[0][4].ToString();
                    probtxtdescribtion.Text = s.Rows[0][5].ToString();
                    probtxtdtime.Text = s.Rows[0][12].ToString();

                    probDepartName = s.Rows[0][1].ToString();
                    probEmployee = s.Rows[0][2].ToString();


                    if (s.Rows[0][11].ToString() == "")
                        probLReplay.Content = "لم يتم الرد";
                    else
                        probLReplay.Content = s.Rows[0][11].ToString();

                    probLSolve.Content = "حل المشكلة";

                    probLCode.Content = probtxtSearch.Text;
                }
                else
                {
                    MessageBox.Show("تحقق من الرقم"); return;

                }

                // s.Clear();
            }
            catch { }



        }

        private void probtxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    probbtnSave.IsEnabled = false;
                    probbtnEdite.IsEnabled = true;
                    System.Data.DataTable s = db.RunReader(" select * from COMPANIES_HR WHERE CODE = '" + probtxtSearch.Text + "'").Result;
                    if (s.Rows.Count > 0)
                    {
                        probcbxDepartment.Text = s.Rows[0][1].ToString();
                        probcbxEmployee.Text = s.Rows[0][2].ToString();
                        probdpTime.Text = s.Rows[0][3].ToString();
                        probcbxReason.Text = s.Rows[0][4].ToString();
                        probtxtdescribtion.Text = s.Rows[0][5].ToString();
                        probtxtdtime.Text = s.Rows[0][12].ToString();

                        probDepartName = s.Rows[0][1].ToString();
                        probEmployee = s.Rows[0][2].ToString();


                        if (s.Rows[0][11].ToString() == "")
                            probLReplay.Content = "لم يتم الرد";
                        else
                            probLReplay.Content = s.Rows[0][11].ToString();

                        probLSolve.Content = "حل المشكلة";

                        probLCode.Content = probtxtSearch.Text;
                    }
                    else
                    {
                        MessageBox.Show("تحقق من الرقم"); return;

                    }

                    // s.Clear();
                }
                catch { }

            }
        }
        #endregion

        #region DMSProplem abdo

        System.Data.DataTable dmsproblem;
        string ipss;
        private void dmsbtnSave_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery(@"UPDATE COMPANIES_HR SET SOLUTION ='" + dmsprobtxtsolution.Text + "', REPLAYED = 'Y'  where CODE =" + ipss, "تم التعديل بنجاح");
            if (User.Department == "customerservices" || User.Department == "After Sales")// and DEPARTMENT='"+User.Department+"'
                dmsproblem = db.RunReader("Select CODE,DEPARTMENT,EMPLOYEE,COMP_DATE,COMP_TIME,REASON,DESCRIPTION,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE FROM COMPANIES_HR WHERE REPLAYED='N' ORDER BY COMP_DATE desc").Result;
            else
                dmsproblem = db.RunReader("Select CODE,DEPARTMENT,EMPLOYEE,COMP_DATE,COMP_TIME,REASON,DESCRIPTION,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE FROM COMPANIES_HR WHERE REPLAYED='N' and DEPARTMENT='" + User.Department + "'  ORDER BY COMP_DATE desc").Result;
            dmsprobdg.ItemsSource = dmsproblem.DefaultView;

            lablllll.Content = "";
            lablllll2.Content = "";
            lablllll3.Content = "";
            dmsprobtxtsolution.Text = "";
            string path = AppDomain.CurrentDomain.BaseDirectory;
            // MessageBox.Show(path);



        }


        private void dmsprobdg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dmsprobdg.SelectedItems[0];


                lablllll.Content = "";
                ipss = row[0].ToString();
                lablllll.Content = row[0].ToString();
                lablllll2.Content = row[5].ToString();
                lablllll3.Content = row[6].ToString();
            }
            catch { }

        }
        #endregion
        #region abdo check


        string varCompanyID;
        Int32 State;
        DataSet abdoCompanyData, abdoCheckNumber, abdocustname;
        void FillCompanyabdoCbxCompany()
        {
            if (User.Type == "DMS Member")
            {
                abdoCompanyData = db_IRS.RunReaderds(" select distinct COMP_ID , COMP_ENAME from IRS_COMPANIES where COMP_ID like '%" + jjCompanyID + "%'  ORDER BY COMP_ID ");
                abdoCbxCompany.ItemsSource = abdoCompanyData.Tables[0].DefaultView;
                abdoCheckNumber = db.RunReaderds(" select distinct CHECK_NO from IND_CHECKS ORDER BY CHECK_NO ");
                abdoCbxCheckNumber.ItemsSource = abdoCheckNumber.Tables[0].DefaultView;
                abdocustname = db.RunReaderds(" select distinct CLIENT_NAME from IND_CHECKS ORDER BY CLIENT_NAME ");
                abdoCbxCustomerName.ItemsSource = abdocustname.Tables[0].DefaultView;
            }
            else
            {

                searchabdoCbxCompany.IsEnabled = false;
                abdoCbxCheckNumber.Visibility = Visibility.Hidden;
                hidithr1.Visibility = Visibility.Hidden;
                jjdtCompany = db.RunReaderds(" select distinct C_COMP_ID  from V_COMPANIES where C_ENAME ='" + jjCompanyName + "'");
                jjCompanyID = jjdtCompany.Tables[0].Rows[0][0].ToString();

                varCompanyID = jjCompanyID;
                abdoCompanyData = db_IRS.RunReaderds(" select distinct COMP_ID , COMP_ENAME from IRS_COMPANIES where COMP_ID like '%" + jjCompanyID + "%'  ORDER BY COMP_ID ");
                abdoCbxCompany.IsEnabled = false;
                abdoCbxCompany.Text = jjCompanyID;
                abdoCheckNumber = db.RunReaderds(" select distinct CHECK_NO from IND_CHECKS where COMP_ID like '%" + jjCompanyID + "%'   ORDER BY CHECK_NO ");
                abdoCbxCheckNumber.ItemsSource = abdoCheckNumber.Tables[0].DefaultView;
                abdocustname = db.RunReaderds(" select distinct CLIENT_NAME from IND_CHECKS where COMP_ID like '%" + jjCompanyID + "%'  ORDER BY CLIENT_NAME ");
                abdoCbxCustomerName.ItemsSource = abdocustname.Tables[0].DefaultView;

            }

        }


        private void abdoCbxCompany_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                varCompanyID = abdoCompanyData.Tables[0].Rows[abdoCbxCompany.SelectedIndex][0].ToString();
                abdoCbxCompany.Text = varCompanyID;
            }
            catch
            { }
        }
        Int32 a1, a4;
        DateTime a2, a5, a6, a22, testtime;

        private void abdoCbxCheckNumber_KeyDown(object sender, KeyEventArgs e)
        {
            abdoCbxCheckNumber.IsDropDownOpen = true;
        }

        private void abdoCbxCustomerName_KeyDown(object sender, KeyEventArgs e)
        {

            abdoCbxCustomerName.IsDropDownOpen = true;
        }

        private void Button_Click_1ss(object sender, RoutedEventArgs e)
        {
            abdoCompanyData = db_IRS.RunReaderds("  select distinct COMP_ID , COMP_ENAME from IRS_COMPANIES WHERE  COMP_ID  LIKE '%" + abdoCbxCompany.Text + "%' or COMP_ENAME LIKE '%" + abdoCbxCompany.Text + "%'  ORDER BY COMP_ID ");
            abdoCbxCompany.ItemsSource = abdoCompanyData.Tables[0].DefaultView;
            abdoCbxCompany.IsDropDownOpen = true;
        }

        private void Button_Clickss(object sender, RoutedEventArgs e)
        {
            abdocleardata();
            if (User.Type == "DMS Member") abdoCbxCompany.Text = "";
            abdoDtDeliveryDate_Copy.Text = "";

            //abdoCompanyData = db.RunReaderds(" select distinct COMP_ID , COMP_ENAME from IRS_COMPANIES ORDER BY COMP_ID ");
            //abdoCbxCompany.ItemsSource = abdoCompanyData.Tables[0].DefaultView;


        }

        private void abdoBtnSearch_Click(object sender, RoutedEventArgs e)
        {

            try
            { a1 = Convert.ToInt32(abdoCbxCheckNumber.Text); }
            catch
            { }
            try
            {
                a2 = Convert.ToDateTime(abdoDtCheckDate.Text);
            }
            catch
            { //a2 = Convert.ToDateTime(abdoDtResiveDate.Text); 
            }
            try
            { a4 = Convert.ToInt32(abdoCbxCompany.Text); }
            catch
            { }
            try
            { a5 = Convert.ToDateTime(abdoDtDeliveryDate.Text); }
            catch
            { }
            try
            { a6 = Convert.ToDateTime(abdoDtResiveDate.Text); }
            catch
            { }
            try
            { testtime = Convert.ToDateTime(abdoDttest.Text); }
            catch
            { }
            try
            {
                a22 = Convert.ToDateTime(abdoDtDeliveryDate_Copy.Text);

            }
            catch
            { }







            string a3 = abdoCbxCustomerName.Text;
            Int32 a7 = State;

            DataDB a = new DataDB();
            System.Data.DataTable z = a.getlData(a1, a2, a3, a4, a5, a6, a7, testtime, a22);
            z.Columns[0].ColumnName = "رقم الشيك";
            z.Columns[1].ColumnName = "تاريخ الشيك";
            z.Columns[2].ColumnName = "رقم البنك";
            z.Columns[3].ColumnName = "اسم البنك";
            z.Columns[4].ColumnName = "اسم العميل";
            z.Columns[5].ColumnName = "رقم الشركة";
            z.Columns[6].ColumnName = "تاريخ الاستلام";
            z.Columns[7].ColumnName = "تاريخ التسليم";
            z.Columns[8].ColumnName = "الحالة";

            abdoDgView.ItemsSource = z.DefaultView;



        }

        private void abdoCbxState_DropDownClosed(object sender, EventArgs e)
        {

            switch (abdoCbxState.SelectedIndex)
            {
                case 0:
                    State = 1;
                    break;
                case 1:
                    State = 2;
                    break;
            }
        }

        void abdocleardata()
        {
            abdoCbxCheckNumber.Text = "";
            abdoDtCheckDate.Text = "";
            abdoCbxCustomerName.Text = "";
            abdoDtDeliveryDate.Text = "";
            abdoDtResiveDate.Text = "";
            State = 0;
            a1 = 0;
            a2 = texsdf;
            a4 = 0;
            a5 = texsdf;
            a6 = texsdf;
            abdoCbxState.SelectedIndex = -1;
            abdoDgView.ItemsSource = null;

        }


        #endregion
        #region abdo2 check



        DataSet abdo2CompanyData, abdo2CheckNumber, abdo2custname;
        void FillCompanyabdo2CbxCompany()
        {
            if (User.Type == "DMS Member")
            {
                abdo2CompanyData = db_IRS.RunReaderds(" select distinct COMP_ID , COMP_ENAME from IRS_COMPANIES where COMP_ID like '%" + jjCompanyID + "%'  ORDER BY COMP_ID ");
                abdo2CbxCompany.ItemsSource = abdo2CompanyData.Tables[0].DefaultView;
                abdo2CheckNumber = db.RunReaderds(" select distinct CHECK_NO from IND_CHECKS ORDER BY CHECK_NO ");
                abdo2CbxCheckNumber.ItemsSource = abdo2CheckNumber.Tables[0].DefaultView;
                abdo2custname = db.RunReaderds(" select distinct CLIENT_NAME from IND_CHECKS ORDER BY CLIENT_NAME ");
                abdo2CbxCustomerName.ItemsSource = abdo2custname.Tables[0].DefaultView;
            }
            else
            {

                searchabdo2CbxCompany.IsEnabled = false;
                abdo2CbxCheckNumber.Visibility = Visibility.Hidden;
                hidithr12.Visibility = Visibility.Hidden;
                jjdtCompany = db.RunReaderds(" select distinct C_COMP_ID  from V_COMPANIES where C_ENAME ='" + jjCompanyName + "'");
                jjCompanyID = jjdtCompany.Tables[0].Rows[0][0].ToString();

                varCompanyID = jjCompanyID;
                //  abdo2CompanyData = db.RunReaderds(" select distinct COMP_ID , COMP_ENAME from IRS_COMPANIES where COMP_ID like '%" + jjCompanyID + "%'  ORDER BY COMP_ID ");
                abdo2CbxCompany.IsEnabled = false;
                abdo2CbxCompany.Text = jjCompanyID;
                abdo2CheckNumber = db.RunReaderds(" select distinct CHECK_NO from IND_CHECKS where COMP_ID like '%" + jjCompanyID + "%'   ORDER BY CHECK_NO ");
                abdo2CbxCheckNumber.ItemsSource = abdo2CheckNumber.Tables[0].DefaultView;
                abdo2custname = db.RunReaderds(" select distinct CLIENT_NAME from IND_CHECKS where COMP_ID like '%" + jjCompanyID + "%'  ORDER BY CLIENT_NAME ");
                abdo2CbxCustomerName.ItemsSource = abdo2custname.Tables[0].DefaultView;

            }

        }


        private void abdo2CbxCompany_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                varCompanyID = abdo2CompanyData.Tables[0].Rows[abdo2CbxCompany.SelectedIndex][0].ToString();
                abdo2CbxCompany.Text = varCompanyID;
            }
            catch
            { }
        }


        private void abdo2CbxCheckNumber_KeyDown(object sender, KeyEventArgs e)
        {
            abdo2CbxCheckNumber.IsDropDownOpen = true;
        }

        private void abdo2CbxCustomerName_KeyDown(object sender, KeyEventArgs e)
        {

            abdo2CbxCustomerName.IsDropDownOpen = true;
        }

        private void Button2_Click_1ss(object sender, RoutedEventArgs e)
        {
            abdo2CompanyData = db_IRS.RunReaderds("  select distinct COMP_ID , COMP_ENAME from IRS_COMPANIES WHERE  COMP_ID  LIKE '%" + abdo2CbxCompany.Text + "%' or COMP_ENAME LIKE '%" + abdo2CbxCompany.Text + "%'  ORDER BY COMP_ID ");
            abdo2CbxCompany.ItemsSource = abdo2CompanyData.Tables[0].DefaultView;
            abdo2CbxCompany.IsDropDownOpen = true;
        }

        private void Button2_Clickss(object sender, RoutedEventArgs e)
        {
            abdo2cleardata();
            abdo2CbxCompany.Text = "";
            abdo2DtDeliveryDate_Copy.Text = "";
            //abdo2CompanyData = db.RunReaderds(" select distinct COMP_ID , COMP_ENAME from IRS_COMPANIES ORDER BY COMP_ID ");
            //abdo2CbxCompany.ItemsSource = abdo2CompanyData.Tables[0].DefaultView;


        }

        private void abdo2BtnSearch_Click(object sender, RoutedEventArgs e)
        {

            try
            { a1 = Convert.ToInt32(abdo2CbxCheckNumber.Text); }
            catch
            { }
            try
            {
                a2 = Convert.ToDateTime(abdo2DtCheckDate.Text);
            }
            catch
            { //a2 = Convert.ToDateTime(abdo2DtResiveDate.Text); 
            }
            try
            { a4 = Convert.ToInt32(abdo2CbxCompany.Text); }
            catch
            { }
            try
            { a5 = Convert.ToDateTime(abdo2DtDeliveryDate.Text); }
            catch
            { }
            try
            { a6 = Convert.ToDateTime(abdo2DtResiveDate.Text); }
            catch
            { }
            try
            { testtime = Convert.ToDateTime(abdo2Dttest.Text); }
            catch
            { }
            try
            {
                a22 = Convert.ToDateTime(abdo2DtDeliveryDate_Copy.Text);

            }
            catch
            { }







            string a3 = abdo2CbxCustomerName.Text;
            Int32 a7 = State;

            DataDB a = new DataDB();
            System.Data.DataTable z = a.getlData(a1, a2, a3, a4, a5, a6, a7, testtime, a22);
            z.Columns[0].ColumnName = "رقم الشيك";
            z.Columns[1].ColumnName = "تاريخ الشيك";
            z.Columns[2].ColumnName = "رقم البنك";
            z.Columns[3].ColumnName = "اسم البنك";
            z.Columns[4].ColumnName = "اسم العميل";
            z.Columns[5].ColumnName = "رقم الشركة";
            z.Columns[6].ColumnName = "تاريخ الاستلام";
            z.Columns[7].ColumnName = "تاريخ التسليم";
            z.Columns[8].ColumnName = "الحالة";

            abdo2DgView.ItemsSource = z.DefaultView;



        }

        private void abdo2CbxState_DropDownClosed(object sender, EventArgs e)
        {

            switch (abdo2CbxState.SelectedIndex)
            {
                case 0:
                    State = 1;
                    break;
                case 1:
                    State = 2;
                    break;
            }
        }
        DateTime texsdf;
        void abdo2cleardata()
        {
            abdo2CbxCheckNumber.Text = "";
            abdo2DtCheckDate.Text = "";
            abdo2CbxCustomerName.Text = "";
            abdo2DtDeliveryDate.Text = "";
            abdo2DtResiveDate.Text = "";
            State = 0;
            a1 = 0;
            a2 = texsdf;
            a4 = 0;
            a5 = texsdf;
            a6 = texsdf;
            abdo2CbxState.SelectedIndex = -1;
            abdo2DgView.ItemsSource = null;

        }


        #endregion
        #region month Abdo

        static string jjUesrname, jjCompanyName, jjCompanyID, jjcardID, jjcardName, jjUserType;
        DataSet jjdtCompany, jjdtcard;

        void jjstartmonth()
        {

            if (User.Type == "DMS Member")
            {
                jjdtCompany = db.RunReaderds(" select distinct C_COMP_ID , C_ANAME from V_COMPANIES ORDER BY C_COMP_ID ");
                abdocbxmonthCompany.ItemsSource = jjdtCompany.Tables[0].DefaultView;

            }
            else
            {
                abdocbxmonthCompany.IsEnabled = false;
                imgsearch_Copaay.IsEnabled = false;

                //   jjdtCompany = db.RunReaderds(" select distinct C_COMP_ID  from V_COMPANIES where C_ENAME ='" + jjCompanyName + "'");
                jjCompanyID = User.CompanyID;
                abdocbxmonthCompany.Text = User.CompanyID;
                jjfillCard();
                System.Data.DataTable a = db.RunReader(@"select V_MED_CARD.CARD_NO , COMP_EMPLOYEESS.EMP_ENAME_ST , COMP_EMPLOYEESS.EMP_ENAME_SC,COMP_EMPLOYEES.EMP_ENAME_TH, V_MED_CARD.PROVIDER_CODE,V_PROVIDERS.USER_N ,V_MED_CARD.GROUP_NAME 
                                                          from V_MED_CARD ,COMP_EMPLOYEES ,V_PROVIDERS where V_MED_CARD.CARD_NO like '" + jjCompanyID + "-%' and V_MED_CARD.PROVIDER_CODE=V_PROVIDERS.USER_CO and V_MED_CARD.CARD_NO=COMP_EMPLOYEES.CARD_ID order by V_MED_CARD.CARD_NO").Result;
                if (a.Rows.Count > 1)
                {

                    a = db.RunReader(@" select distinct V_MED_CARD.CARD_NO , COMP_EMPLOYEESS.EMP_ENAME_ST , COMP_EMPLOYEESS.EMP_ENAME_SC,COMP_EMPLOYEES.EMP_ENAME_TH, V_MED_CARD.PROVIDER_CODE,V_PROVIDERS.USER_N ,V_MED_CARD.GROUP_NAME 
                                                          from V_MED_CARD ,COMP_EMPLOYEES ,V_PROVIDERS where V_MED_CARD.CARD_NO like '" + jjCompanyID + "-%' and V_MED_CARD.PROVIDER_CODE=V_PROVIDERS.USER_CO and V_MED_CARD.CARD_NO=COMP_EMPLOYEES.CARD_ID and V_PROVIDERS.USER_ID_ID=1 order by V_MED_CARD.CARD_NO").Result;

                }
                abdodgmonthdata.ItemsSource = a.DefaultView;


            }
        }


        void jjfillCard()
        {

            //   jjdtcard = db.RunReaderds(" select   CARD_NO ,EMP_ENAME  from IRS_EMPLOYEES WHERE COMP_ID=" + jjCompanyID + " ORDER BY CARD_NO ");
            try
            {
                jjdtcard = db_IRS.RunReaderds(" select distinct   V_MED_MEDICINE.CARD_NO ,IRS_EMPLOYEES.EMP_ENAME  from V_MED_MEDICINE ,IRS_EMPLOYEES where  V_MED_MEDICINE.CARD_NO = IRS_EMPLOYEES.CARD_NO and   IRS_EMPLOYEES.COMP_ID=" + jjCompanyID + "  ORDER BY V_MED_MEDICINE.CARD_NO ");
                abdocbxmonthCard.ItemsSource = jjdtcard.Tables[0].DefaultView;
            }
            catch
            { }

        }

        private void abdocbxmonthCompany_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                jjCompanyID = jjdtCompany.Tables[0].Rows[abdocbxmonthCompany.SelectedIndex][0].ToString();
                jjfillCard();
                //    System.Data.DataTable a = db.RunReader(" select CARD_NO ,MED_CODE ,MED_NAME from V_MED_MEDICINE WHERE V_MED_MEDICINE.CARD_NO like '" + jjCompanyID+ "-%'  ORDER BY CARD_NO");
                System.Data.DataTable a = db.RunReader(@" select distinct V_MED_CARD.CARD_NO , COMP_EMPLOYEESS.EMP_ENAME_ST , COMP_EMPLOYEESS.EMP_ENAME_SC,COMP_EMPLOYEES.EMP_ENAME_TH, V_MED_CARD.PROVIDER_CODE,V_PROVIDERS.USER_N ,V_MED_CARD.GROUP_NAME 
                                                          from V_MED_CARD ,COMP_EMPLOYEES ,V_PROVIDERS where V_MED_CARD.CARD_NO like '" + jjCompanyID + "-%' and V_MED_CARD.PROVIDER_CODE=V_PROVIDERS.USER_CO and V_MED_CARD.CARD_NO=COMP_EMPLOYEES.CARD_ID order by V_MED_CARD.CARD_NO").Result;
                if (a.Rows.Count > 1)
                {

                    a = db.RunReader(@" select distinct V_MED_CARD.CARD_NO , COMP_EMPLOYEESS.EMP_ENAME_ST , COMP_EMPLOYEESS.EMP_ENAME_SC,COMP_EMPLOYEES.EMP_ENAME_TH, V_MED_CARD.PROVIDER_CODE,V_PROVIDERS.USER_N ,V_MED_CARD.GROUP_NAME 
                                                          from V_MED_CARD ,COMP_EMPLOYEES ,V_PROVIDERS where V_MED_CARD.CARD_NO like '" + jjCompanyID + "-%' and V_MED_CARD.PROVIDER_CODE=V_PROVIDERS.USER_CO and V_MED_CARD.CARD_NO=COMP_EMPLOYEES.CARD_ID and V_PROVIDERS.USER_ID_ID=1 order by V_MED_CARD.CARD_NO").Result;

                }
                else if (a.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات");
                }
                abdodgmonthdata.ItemsSource = a.DefaultView;
                abdocbxmonthCard.Text = "";

            }
            catch
            { }
        }

        private void imgsearch_Copaay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            jjdtCompany = db.RunReaderds("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + abdocbxmonthCompany.Text + "%' or C_ANAME LIKE '%" + abdocbxmonthCompany.Text + "%'  ORDER BY C_COMP_ID ");
            abdocbxmonthCompany.ItemsSource = jjdtCompany.Tables[0].DefaultView;
            abdocbxmonthCompany.IsDropDownOpen = true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            abdocbxmonthCard.Text = "";

            if (User.Type == "DMS Member") abdocbxmonthCompany.Text = "";

            abdodgmonthdata.ItemsSource = null;
            abdodgmonthdata2.ItemsSource = null;

            txtabdoLastDate.Text = "";
            txtabdoProvider.Text = "";

            if (jjCompanyName == "")
            {
                jjdtCompany = db.RunReaderds(" select distinct C_COMP_ID  from V_COMPANIES where C_ENAME ='" + jjCompanyName + "'");
                abdocbxmonthCompany.Text = "";
                abdodgmonthdata.ItemsSource = null;
            }

        }

        private void imgsearch_Copaay_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            jjdtcard = db.RunReaderds("  select distinct   V_MED_MEDICINE.CARD_NO ,IRS_EMPLOYEES.EMP_ENAME  from V_MED_MEDICINE ,IRS_EMPLOYEES where  V_MED_MEDICINE.CARD_NO = IRS_EMPLOYEES.CARD_NO and   ( V_MED_MEDICINE.CARD_NO  LIKE '%" + abdocbxmonthCard.Text + "%' or IRS_EMPLOYEES.EMP_ENAME  LIKE '%" + abdocbxmonthCard.Text + "%' ) ");
            abdocbxmonthCard.ItemsSource = jjdtcard.Tables[0].DefaultView;
            abdocbxmonthCard.IsDropDownOpen = true;
        }
        /// <summary>
        /// this of
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        private void abdocbxmonthCard_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                jjcardID = jjdtcard.Tables[0].Rows[abdocbxmonthCard.SelectedIndex][0].ToString();
                jjcardName = jjdtcard.Tables[0].Rows[abdocbxmonthCard.SelectedIndex][1].ToString();
                //System.Data.DataTable a = db.RunReader(" select CARD_NO ,MED_CODE ,MED_NAME from V_MED_MEDICINE WHERE CARD_NO like '" + jjcardID + "'  ORDER BY CARD_NO");
                System.Data.DataTable a = db.RunReader(@"select distinct V_MED_CARD.CARD_NO , COMP_EMPLOYEESS.EMP_ENAME_ST , COMP_EMPLOYEESS.EMP_ENAME_SC,COMP_EMPLOYEES.EMP_ENAME_TH, V_MED_CARD.PROVIDER_CODE,V_PROVIDERS.USER_N ,V_MED_CARD.GROUP_NAME 
                                                          from V_MED_CARD ,COMP_EMPLOYEES ,V_PROVIDERS where V_MED_CARD.CARD_NO like '" + jjcardID + "' and V_MED_CARD.PROVIDER_CODE=V_PROVIDERS.USER_CO and V_MED_CARD.CARD_NO=COMP_EMPLOYEES.CARD_ID order by V_MED_CARD.CARD_NO").Result;

                if (a.Rows.Count > 1)
                {

                    a = db.RunReader(@" select distinct V_MED_CARD.CARD_NO , COMP_EMPLOYEESS.EMP_ENAME_ST , COMP_EMPLOYEESS.EMP_ENAME_SC,COMP_EMPLOYEES.EMP_ENAME_TH, V_MED_CARD.PROVIDER_CODE,V_PROVIDERS.USER_N ,V_MED_CARD.GROUP_NAME 
                                                          from V_MED_CARD ,COMP_EMPLOYEES ,V_PROVIDERS where V_MED_CARD.CARD_NO like '" + jjcardID + "' and V_MED_CARD.PROVIDER_CODE=V_PROVIDERS.USER_CO and V_MED_CARD.CARD_NO=COMP_EMPLOYEES.CARD_ID and V_PROVIDERS.USER_ID_ID=1 order by V_MED_CARD.CARD_NO").Result;

                }
                abdodgmonthdata.ItemsSource = a.DefaultView;
                System.Data.DataTable b = db.RunReader(" select distinct D_DATE,EMP_SUB  from V_TRANSACTION where CARD_ID='" + jjcardID + "' order by D_DATE desc").Result;
                txtabdoLastDate.Text = b.Rows[0][0].ToString();
                txtabdoProvider.Text = b.Rows[0][1].ToString();


                System.Data.DataTable c = db.RunReader(" select  MED_CODE,MED_NAME,DOSAGE_FORM,UNIT_NO,PACK_SIZE,DOS_DUR,TOT_DUR,CON_MED,ACTIVE from V_MED_MEDICINE where CARD_NO='" + jjcardID + "'").Result;
                c.Columns.Add("ActiveState", typeof(String));

                //System.Data.DataRow s;
                int i = 0;
                foreach (DataRow row in c.Rows)
                {


                    if (c.Rows[i]["ACTIVE"].ToString() == "Y")
                    {
                        row["ActiveState"] = "Active";
                    }
                    else
                    {
                        row["ActiveState"] = "Lock";
                    }


                    i++;
                }
                c.Columns.RemoveAt(8);
                abdodgmonthdata2.ItemsSource = c.DefaultView;

            }
            catch
            {

            }

        }
        private void abdodgmonthdata_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)abdodgmonthdata.SelectedItems[0];
                jjcardID = row[0].ToString();
                System.Data.DataTable a = db.RunReader(" select MED_CODE,MED_NAME,DOSAGE_FORM,UNIT_NO,PACK_SIZE,DOS_DUR,TOT_DUR,CON_MED,ACTIVE from V_MED_MEDICINE where CARD_NO='" + jjcardID + "'").Result;
                a.Columns.Add("ActiveState", typeof(String));

                //System.Data.DataRow s;
                int i = 0;
                foreach (DataRow rowz in a.Rows)
                {


                    if (a.Rows[i]["ACTIVE"].ToString() == "Y")
                    {
                        rowz["ActiveState"] = "Active";
                    }
                    else
                    {
                        rowz["ActiveState"] = "Lock";
                    }


                    i++;
                }
                a.Columns.RemoveAt(8);
                abdodgmonthdata2.ItemsSource = a.DefaultView;


                System.Data.DataTable b = db.RunReader(" select D_DATE,EMP_SUB  from V_TRANSACTION where CARD_ID='" + jjcardID + "' order by D_DATE desc").Result;
                txtabdoLastDate.Text = b.Rows[0][0].ToString();
                txtabdoProvider.Text = b.Rows[0][1].ToString();
            }
            catch
            { }



        }
        #endregion

        #region month22 Abdo

        static string jj22Uesrname, jj22CompanyName, jj22CompanyID, jj22cardID, jj22cardName, jj22UserType;







        DataSet jj22dtCompany, jj22dtcard;

        void jj22startmonth22()
        {
            if (User.Type == "DMS Member")
            {
                jj22dtCompany = db.RunReaderds(" select distinct C_COMP_ID , C_ANAME from V_COMPANIES ORDER BY C_COMP_ID ");
                abdocbxmonth22Company.ItemsSource = jj22dtCompany.Tables[0].DefaultView;

            }
            else
            {
                abdocbxmonth22Company.IsEnabled = false;
                imgsearch22_Copaay.IsEnabled = false;
                abdocbxmonth22Company.Text = jj22CompanyName;
                jj22dtCompany = db.RunReaderds(" select distinct C_COMP_ID  from V_COMPANIES where C_ENAME ='" + jj22CompanyName + "'");
                jj22CompanyID = jj22dtCompany.Tables[0].Rows[0][0].ToString();
                jj22fillCard();
                System.Data.DataTable a = db.RunReader(@"select V_MED_CARD.CARD_NO , COMP_EMPLOYEESS.EMP_ENAME_ST , COMP_EMPLOYEESS.EMP_ENAME_SC,COMP_EMPLOYEES.EMP_ENAME_TH, V_MED_CARD.PROVIDER_CODE,V_PROVIDERS.USER_N ,V_MED_CARD.GROUP_NAME 
                                                          from V_MED_CARD ,COMP_EMPLOYEES ,V_PROVIDERS where V_MED_CARD.CARD_NO like '" + jj22CompanyID + "-%' and V_MED_CARD.PROVIDER_CODE=V_PROVIDERS.USER_CO and V_MED_CARD.CARD_NO=COMP_EMPLOYEES.CARD_ID order by V_MED_CARD.CARD_NO").Result;
                abdodgmonth22data.ItemsSource = a.DefaultView;


            }
        }


        void jj22fillCard()
        {

            //   jj22dtcard = db.RunReaderds(" select   CARD_NO ,EMP_ENAME  from IRS_EMPLOYEES WHERE COMP_ID=" + jj22CompanyID + " ORDER BY CARD_NO ");
            try
            {
                jj22dtcard = db_IRS.RunReaderds(" select distinct   V_MED_MEDICINE.CARD_NO ,IRS_EMPLOYEES.EMP_ENAME  from V_MED_MEDICINE ,IRS_EMPLOYEES where  V_MED_MEDICINE.CARD_NO = IRS_EMPLOYEES.CARD_NO and   IRS_EMPLOYEES.COMP_ID=" + jj22CompanyID + "  ORDER BY V_MED_MEDICINE.CARD_NO ");
                abdocbxmonth22Card.ItemsSource = jj22dtcard.Tables[0].DefaultView;
            }
            catch { }


        }


        private void abdocbxmonth22Company_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                jj22CompanyID = jj22dtCompany.Tables[0].Rows[abdocbxmonth22Company.SelectedIndex][0].ToString();


                //  Timer_Tick(sender, e);
                //    System.Data.DataTable a = db.RunReader(" select CARD_NO ,MED_CODE ,MED_NAME from V_MED_MEDICINE WHERE V_MED_MEDICINE.CARD_NO like '" + jj22CompanyID+ "-%'  ORDER BY CARD_NO");
                //System.Data.DataTable a = db.RunReader(@" select distinct V_MED_CARD.CARD_NO , COMP_EMPLOYEESS.EMP_ENAME_ST , COMP_EMPLOYEESS.EMP_ENAME_SC,COMP_EMPLOYEES.EMP_ENAME_TH, V_MED_CARD.PROVIDER_CODE,V_PROVIDERS.USER_N ,V_MED_CARD.GROUP_NAME 
                //                                          from V_MED_CARD ,COMP_EMPLOYEES ,V_PROVIDERS where V_MED_CARD.CARD_NO like '" + jj22CompanyID + "-%' and V_MED_CARD.PROVIDER_CODE=V_PROVIDERS.USER_CO and V_MED_CARD.CARD_NO=COMP_EMPLOYEES.CARD_ID order by V_MED_CARD.CARD_NO");
                //if (a.Rows.Count > 1)
                //{

                System.Data.DataTable a = db.RunReader(@" select distinct V_MED_CARD.CARD_NO , COMP_EMPLOYEESS.EMP_ENAME_ST , COMP_EMPLOYEESS.EMP_ENAME_SC,COMP_EMPLOYEES.EMP_ENAME_TH, V_MED_CARD.PROVIDER_CODE,V_PROVIDERS.USER_N ,V_MED_CARD.GROUP_NAME 
                from V_MED_CARD ,COMP_EMPLOYEES ,V_PROVIDERS where V_MED_CARD.CARD_NO like '" + jj22CompanyID + "-%' and V_MED_CARD.PROVIDER_CODE=V_PROVIDERS.USER_CO and V_MED_CARD.CARD_NO=COMP_EMPLOYEES.CARD_ID and V_PROVIDERS.USER_ID_ID=1 order by V_MED_CARD.CARD_NO").Result;

                //}
                abdodgmonth22data.ItemsSource = a.DefaultView;
                abdocbxmonth22Card.Text = "";

            }
            catch
            { }
        }

        private void imgsearch22_Copaay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            jj22dtCompany = db.RunReaderds("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + abdocbxmonth22Company.Text + "%' or C_ANAME LIKE '%" + abdocbxmonth22Company.Text + "%'  ORDER BY C_COMP_ID ");
            abdocbxmonth22Company.ItemsSource = jj22dtCompany.Tables[0].DefaultView;
            abdocbxmonth22Company.IsDropDownOpen = true;
        }
        private void Button_Click22(object sender, RoutedEventArgs e)
        {
            abdocbxmonth22Card.Text = "";
            abdocbxmonth22Company.Text = "";
            abdodgmonth22data.ItemsSource = null;
            abdodgmonth22data2.ItemsSource = null;

            txtabdo22LastDate.Text = "";
            txtabdo22Provider.Text = "";

            if (jj22CompanyName == "")
            {
                jj22dtCompany = db.RunReaderds(" select distinct C_COMP_ID  from V_COMPANIES where C_ENAME ='" + jj22CompanyName + "'");
                abdocbxmonth22Company.Text = "";
                abdodgmonth22data.ItemsSource = null;
            }

        }

        private void imgsearch22_Copaay_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            jj22dtcard = db.RunReaderds("  select distinct   V_MED_MEDICINE.CARD_NO ,IRS_EMPLOYEES.EMP_ENAME  from V_MED_MEDICINE ,IRS_EMPLOYEES where  V_MED_MEDICINE.CARD_NO = IRS_EMPLOYEES.CARD_NO and   ( V_MED_MEDICINE.CARD_NO  LIKE '%" + abdocbxmonth22Card.Text + "%' or IRS_EMPLOYEES.EMP_ENAME  LIKE '%" + abdocbxmonth22Card.Text + "%' ) ");
            abdocbxmonth22Card.ItemsSource = jj22dtcard.Tables[0].DefaultView;
            abdocbxmonth22Card.IsDropDownOpen = true;
        }
        /// <summary>
        /// this of
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abdocbxmonth22Card_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                jj22cardID = jj22dtcard.Tables[0].Rows[abdocbxmonth22Card.SelectedIndex][0].ToString();
                jj22cardName = jj22dtcard.Tables[0].Rows[abdocbxmonth22Card.SelectedIndex][1].ToString();
                //System.Data.DataTable a = db.RunReader(" select CARD_NO ,MED_CODE ,MED_NAME from V_MED_MEDICINE WHERE CARD_NO like '" + jj22cardID + "'  ORDER BY CARD_NO");
                System.Data.DataTable a = db.RunReader(@"select distinct V_MED_CARD.CARD_NO , COMP_EMPLOYEESS.EMP_ENAME_ST , COMP_EMPLOYEESS.EMP_ENAME_SC,COMP_EMPLOYEES.EMP_ENAME_TH, V_MED_CARD.PROVIDER_CODE,V_PROVIDERS.USER_N ,V_MED_CARD.GROUP_NAME 
                                                          from V_MED_CARD ,COMP_EMPLOYEES ,V_PROVIDERS where V_MED_CARD.CARD_NO like '" + jj22cardID + "' and V_MED_CARD.PROVIDER_CODE=V_PROVIDERS.USER_CO and V_MED_CARD.CARD_NO=COMP_EMPLOYEES.CARD_ID order by V_MED_CARD.CARD_NO").Result;

                if (a.Rows.Count > 1)
                {

                    a = db.RunReader(@" select distinct V_MED_CARD.CARD_NO , COMP_EMPLOYEESS.EMP_ENAME_ST , COMP_EMPLOYEESS.EMP_ENAME_SC,COMP_EMPLOYEES.EMP_ENAME_TH, V_MED_CARD.PROVIDER_CODE,V_PROVIDERS.USER_N ,V_MED_CARD.GROUP_NAME 
                                                          from V_MED_CARD ,COMP_EMPLOYEES ,V_PROVIDERS where V_MED_CARD.CARD_NO like '" + jj22cardID + "' and V_MED_CARD.PROVIDER_CODE=V_PROVIDERS.USER_CO and V_MED_CARD.CARD_NO=COMP_EMPLOYEES.CARD_ID and V_PROVIDERS.USER_ID_ID=1 order by V_MED_CARD.CARD_NO").Result;

                }
                abdodgmonth22data.ItemsSource = a.DefaultView;
                System.Data.DataTable b = db.RunReader(" select distinct D_DATE,EMP_SUB  from V_TRANSACTION where CARD_ID='" + jj22cardID + "' order by D_DATE desc").Result;
                txtabdo22LastDate.Text = b.Rows[0][0].ToString();
                txtabdo22Provider.Text = b.Rows[0][1].ToString();


                System.Data.DataTable c = db.RunReader(" select  MED_CODE,MED_NAME,DOSAGE_FORM,UNIT_NO,PACK_SIZE,DOS_DUR,TOT_DUR,CON_MED,ACTIVE from V_MED_MEDICINE where CARD_NO='" + jj22cardID + "'").Result;
                c.Columns.Add("ActiveState", typeof(String));

                //System.Data.DataRow s;
                int i = 0;
                foreach (DataRow row in c.Rows)
                {


                    if (c.Rows[i]["ACTIVE"].ToString() == "Y")
                    {
                        row["ActiveState"] = "Active";
                    }
                    else
                    {
                        row["ActiveState"] = "Lock";
                    }


                    i++;
                }
                c.Columns.RemoveAt(8);
                abdodgmonth22data2.ItemsSource = c.DefaultView;

            }
            catch
            {

            }

        }
        private void abdodgmonth22data_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)abdodgmonth22data.SelectedItems[0];
                jj22cardID = row[0].ToString();
                System.Data.DataTable a = db.RunReader(" select MED_CODE,MED_NAME,DOSAGE_FORM,UNIT_NO,PACK_SIZE,DOS_DUR,TOT_DUR,CON_MED,ACTIVE from V_MED_MEDICINE where CARD_NO='" + jj22cardID + "'").Result;
                a.Columns.Add("ActiveState", typeof(String));

                //System.Data.DataRow s;
                int i = 0;
                foreach (DataRow rowz in a.Rows)
                {


                    if (a.Rows[i]["ACTIVE"].ToString() == "Y")
                    {
                        rowz["ActiveState"] = "Active";
                    }
                    else
                    {
                        rowz["ActiveState"] = "Lock";
                    }


                    i++;
                }
                a.Columns.RemoveAt(8);
                abdodgmonth22data2.ItemsSource = a.DefaultView;


                System.Data.DataTable b = db.RunReader(" select D_DATE,EMP_SUB  from V_TRANSACTION where CARD_ID='" + jj22cardID + "' order by D_DATE desc").Result;
                txtabdo22LastDate.Text = b.Rows[0][0].ToString();
                txtabdo22Provider.Text = b.Rows[0][1].ToString();
            }
            catch
            { }



        }
        #endregion

        private void DeliverForm_Click(object sender, RoutedEventArgs e)
        {
            MovingFrm movF = new MovingFrm();
            movF.ShowDialog();
        }
        private void btnCR(object sender, RoutedEventArgs e)
        {

            ConfirmNotebook_RequestFrm cr = new ConfirmNotebook_RequestFrm(NameTab.Header.ToString());
            cr.ShowDialog();
        }

        private void btnRD(object sender, RoutedEventArgs e)
        {
            DeliverNotebookFrm deliver = new DeliverNotebookFrm(NameTab.Header.ToString());
            deliver.ShowDialog();
        }


        //new final
        private void saveReceiveBtn_Click(object sender, RoutedEventArgs e) // printing received
        {
            try
            {

                if (ReceivingGrid.SelectedItems.Count > 0)
                {
                    PrintingData obj = new PrintingData();
                    object item = ReceivingGrid.SelectedItem;
                    obj.EmpID = (ReceivingGrid.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                    obj.ContractNo = int.Parse((ReceivingGrid.SelectedCells[6].Column.GetCellContent(item) as TextBlock).Text);
                    if ((ReceivingGrid.SelectedCells[17].Column.GetCellContent(item) as TextBlock).Text != "")
                    {
                        obj.RecievedName = (ReceivingGrid.SelectedCells[17].Column.GetCellContent(item) as TextBlock).Text.ToString();
                        obj.ReceivedState = "Y";
                        obj.ReceivedDate = (ReceivingGrid.SelectedCells[15].Column.GetCellContent(item) as TextBlock).Text.ToString();
                        //--------check if this messenger already exist------------------
                        PrintingData OldEmp = printserv.SelectEmpById_For_Receiving(obj.EmpID, obj.ContractNo);
                        if (OldEmp != null)//-------update it-------------
                        {
                            int affected = printserv.UpdateEmp_In_Printing_CardReceiving(obj, obj.EmpID);
                            if (affected > 0)
                            {
                                //-----------
                                MessageBox.Show("تم الحفظ بنجاح");
                            }
                        }

                    }
                }
            }
            catch { }
        }

        private void txtTransSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtTransSearch.Text, "[^0-9]"))
                {
                    txtTransSearch.Text = txtTransSearch.Text.Remove(txtTransSearch.Text.Length - 1);
                }
            }
            catch { }
        }

        private void prictxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(prictxt.Text, "[^0-9]"))
                {
                    prictxt.Text = prictxt.Text.Remove(prictxt.Text.Length - 1);
                }
            }
            catch { }
        }

        private void amttxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(amttxt.Text, "[^0-9]"))
                {
                    amttxt.Text = amttxt.Text.Remove(amttxt.Text.Length - 1);
                }
            }
            catch { }
        }

        private void limtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(limtxt.Text, "[^0-9]"))
                {
                    limtxt.Text = limtxt.Text.Remove(limtxt.Text.Length - 1);
                }
            }
            catch { }
        }

        private void export_amounttxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(export_amounttxt.Text, "[^0-9]"))
                {
                    export_amounttxt.Text = export_amounttxt.Text.Remove(export_amounttxt.Text.Length - 1);
                }
            }
            catch { }
        }

        private void importPrintBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                exportToPDF(grid_import, "Imports");
            }
            catch { }
        }

        private void imprtPricelbl_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(imprtPricelbl.Text, "[^0-9]"))
                {
                    imprtPricelbl.Text = imprtPricelbl.Text.Remove(imprtPricelbl.Text.Length - 1);
                }
            }
            catch { }
        }

        private void imprt_amounttxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(imprt_amounttxt.Text, "[^0-9]"))
                {
                    imprt_amounttxt.Text = imprt_amounttxt.Text.Remove(imprt_amounttxt.Text.Length - 1);
                }
            }
            catch { }
        }

        private void billtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(billtxt.Text, "[^0-9]"))
                {
                    billtxt.Text = billtxt.Text.Remove(billtxt.Text.Length - 1);
                }
            }
            catch { }
        }

        private void CallsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                object item = CallsGrid.SelectedItem;
                string cardid = (((CallsGrid).SelectedCells[1].Column.GetCellContent(item)) as TextBlock).Text.ToString();
                string[] arr = cardid.Split('-');
                string comp = arr[0];
                CompanyComboSummary.Text = comp;
                networkcardcombo_Copy.Text = comp;
                txtCardNumz.Text = cardid;
                ApprovaltxtCardNumz.Text = cardid;
                approvalcardcombo.Text = cardid;
                networkcardcombo.Text = cardid;
                InfotxtCardNum.Text = cardid;
                infoCardCompanyCombo.Text = comp;
                infocardcombo.Text = cardid;
            }
            catch { }
        }




        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void TabItem_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void newDestroySrchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                destroyFilterEmpCombo.Text = "";
                destroyFilterDeptCombo.Text = "";

                destroyFilterGrid.ItemsSource = null;
                destroyItemCount.Text = "";
                destroyCounttxt.Content = "Items count : ";
            }
            catch { }
        }

        private void deptFilternewSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DeptFilterCombo.Text = "";
                DeptFilerGrid.ItemsSource = null;
                deptFilterCounttxt.Content = "Items count : ";
            }
            catch { }
        }

        private void empfilternewSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                empFilterEmpCombo.Text = "";
                empFilterDeptCombo.Text = "";
                EmployeeFilterGrid.ItemsSource = null;
                empFilterCounttxt.Content = "Items count : ";
            }
            catch { }
        }

        private void categoryFilternewSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                categoryFilterCategoryCombo.Text = "";
                categoryFilterGrid.ItemsSource = null;
                CategoryFilterCounttxt.Content = "Items count : ";
            }
            catch { }
        }

        private void itemFilternewSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ItemFilterCombo.Text = "";
                ItemFilterCategoryCombo.Text = "";
                ItemFilterGrid.ItemsSource = null;
                itemFilterCounttxt.Content = "Items count : ";
            }
            catch { }
        }

        private void typeFilternewSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                typeFilterCounttxt.Content = "Items count : ";
                destroyFilterRB.IsChecked = false;
                returnFilterRB.IsChecked = false;
                importFilterRB.IsChecked = false;
                exportFilterRB.IsChecked = false;
                filterGrid.ItemsSource = null;
            }
            catch { }
        }

        private void NotebookMoveNewSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cmbProviderTypeDeliver_Copy.Text = "";
                cmbProviderTypeDeliver.Text = "";
                txtProviderNameDeliver.Text = "";
                listboxDeliver.ItemsSource = null;
                deliverNoteGrid.ItemsSource = null;
                dtpFromDeliver.Text = "";
                dtpToDeliver.Text = "";
                rdSearchByCompany.IsChecked = false;
                rdSearchByDeliverDate.IsChecked = false;
                deliverNoteGrid.ItemsSource = null;
                deliverNoteItmCounttxt.Content = "0";
                deliverNoteItmCounttxt.Content = "0";
            }
            catch { }
        }

        private void SummaryProviderCodetxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(SummaryProviderCodetxt.Text, "[^0-9]"))
                {
                    SummaryProviderCodetxt.Text = SummaryProviderCodetxt.Text.Remove(SummaryProviderCodetxt.Text.Length - 1);
                }
            }
            catch { }
        }

        private void CompanyIDtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(CompanyIDtxt.Text, "[^0-9]"))
                {
                    CompanyIDtxt.Text = CompanyIDtxt.Text.Remove(CompanyIDtxt.Text.Length - 1);
                }
            }
            catch { }
        }

        //private void contractNumtxt_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (System.Text.RegularExpressions.Regex.IsMatch(contractNumtxt.Text, "[^0-9]"))
        //    {
        //        contractNumtxt.Text = contractNumtxt.Text.Remove(contractNumtxt.Text.Length - 1);
        //    }
        //}

        private void indemnitynewSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dtpFrom.Text = "";
                dtpTo.Text = "";
                IndemnityGrid.ItemsSource = null;
                IndemnityCompanyCombo.Text = "";
                IndemnityCardCombo.Text = "";
                indemnityItemCounttxt.Content = "Items count : ";
            }
            catch { }
        }

        private void StoreTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // azwd condition 3shan may5oshesh feha dayman

                if (storeItemTab.IsSelected == true && categorycombo.SelectedItem == null)
                {
                    store_item_load();
                }
                else if (cat_register.IsSelected == true && cat_txt.Text == "")
                {
                    register_category_load();
                }
                else if (returnItem.IsSelected == true && deptCombo.ItemsSource == null)
                {
                    returnItem_Grid.Visibility = Visibility.Hidden;
                    return_item_load();
                }
                //12 nov
                else if (ExportTab.IsSelected == true && exportDeptCombo.ItemsSource == null)
                {
                    export_load();
                    DataSet deptDT = db.RunReaderds("select dept_code,dept_name from agent_department");
                    exportDeptCombo.ItemsSource = deptDT.Tables[0].DefaultView;
                }
                //12 nov
                else if (imortTab.IsSelected == true && imprtDeptCombo.ItemsSource == null)
                {
                    try
                    {
                        DataSet deptDT = db.RunReaderds("select dept_code,dept_name from agent_department");
                        imprtDeptCombo.ItemsSource = deptDT.Tables[0].DefaultView;

                        System.Data.DataTable category = store.get_category();
                        if (category.Rows.Count == 0)
                        {
                            MessageBox.Show("لا توجد نتائج");
                        }
                        else
                        {
                            for (int i = 0; i < category.Rows.Count; i++)
                            {
                                importCategoryCombo.Items.Add(category.Rows[i].ItemArray[0].ToString());
                            }
                        }
                    }
                    catch { }
                }
                //12 nov
                else if (EmployeeCareTab.IsSelected == true && employeeCareDeptCombo.ItemsSource == null)
                //&& destroyFilterDeptCombo.SelectedItem == null
                //&& DeptFilterCombo.SelectedItem == null
                //&& empFilterDeptCombo.SelectedItem == null
                //&& categoryFilterCategoryCombo.SelectedItem == null
                //&& ItemFilterCategoryCombo.SelectedItem == null)
                {
                    //load_reports(); 
                    //load_store_filter();
                    DataSet deptDT = db.RunReaderds("select dept_code,dept_name from agent_department");
                    employeeCareDeptCombo.ItemsSource = deptDT.Tables[0].DefaultView;
                }
                //12 nov
                else if (destroyedTab.IsSelected == true && destroyFilterDeptCombo.ItemsSource == null)
                {
                    DataSet deptDT = db.RunReaderds("select dept_code,dept_name from agent_department");
                    destroyFilterDeptCombo.ItemsSource = deptDT.Tables[0].DefaultView;
                }

                //12 nov
                else if (FilterByDeptTab.IsSelected == true && DeptFilterCombo.ItemsSource == null)
                {
                    DataSet deptDT = db.RunReaderds("select dept_code,dept_name from agent_department");
                    DeptFilterCombo.ItemsSource = deptDT.Tables[0].DefaultView;
                }

                //12 nov
                else if (EmployeeFilterTab.IsSelected == true && empFilterDeptCombo.ItemsSource == null)
                {
                    DataSet deptDT = db.RunReaderds("select dept_code,dept_name from agent_department");
                    empFilterDeptCombo.ItemsSource = deptDT.Tables[0].DefaultView;
                }

                //12 nov
                else if (CategoryFilterTab.IsSelected == true && categoryFilterCategoryCombo.ItemsSource == null)
                {
                    DataSet categoryData = db.RunReaderds("select distinct category_name,category_id from item_category");
                    categoryFilterCategoryCombo.ItemsSource = categoryData.Tables[0].DefaultView;
                }

                //12 nov
                else if (ItemFilterTab.IsSelected == true && ItemFilterCategoryCombo.ItemsSource == null)
                {
                    DataSet categoryData = db.RunReaderds("select distinct category_name,category_id from item_category");
                    ItemFilterCategoryCombo.ItemsSource = categoryData.Tables[0].DefaultView;

                }

                //12 nov
                else if (StoreFilterTab.IsSelected == true && storeFilter.ItemsSource == null)
                {
                    load_store_filter();
                }
            }
            catch { }
        }

        private void employeesTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (newempTab.IsSelected == true && (NewEmpDeptCombo.ItemsSource == null) && basicDataComp.ItemsSource == null)
            {
                try
                {
                    basicDataComp.ItemsSource = db.RunReader("select distinct C_COMP_ID, C_ANAME from v_companies order by C_COMP_ID ").Result.DefaultView;
                    NewEmpDeptCombo.ItemsSource = db.RunReader("select distinct DEPT_CODE ,DEPT_NAME from agent_department order by DEPT_NAME").Result.DefaultView;
                }
                catch { }
            }
            else if (EditEmpTab.IsSelected == true && basicdataDemptCombo.ItemsSource == null
                && empCompCombo.ItemsSource == null)
            {
                try
                {
                    fill_comp(empCompCombo);
                    //empCompCombo.Items.Clear();
                    //System.Data.DataTable data = agent.get_comp_name();
                    //for (int i = 0; i < data.Rows.Count; i++)
                    //{
                    //    empCompCombo.Items.Add(data.Rows[i].ItemArray[0].ToString());
                    //}
                    DataSet deptDT = db.RunReaderds("select distinct dept_name,dept_code from agent_department");

                    basicdataDemptCombo.ItemsSource = deptDT.Tables[0].DefaultView;
                    newDeptCombo.ItemsSource = deptDT.Tables[0].DefaultView;
                    //basicdataDemptCombo.Items.Clear();
                    //newDeptCombo.Items.Clear();
                    //System.Data.DataTable dt = agent.get_all_dept();
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    basicdataDemptCombo.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    //    newDeptCombo.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                    //}
                }
                catch { }
            }


        }

        private void SrchCodeMessReg_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(SrchCodeMessReg.Text, "[^0-9]"))
                {
                    SrchCodeMessReg.Text = SrchCodeMessReg.Text.Remove(SrchCodeMessReg.Text.Length - 1);
                }
            }
            catch { }
        }

        private void SrchCodeMessReg_KeyDown(object sender, KeyEventArgs e) // new mandoob
        {
            if (e.Key == Key.Enter)
            {
                try
                {//joba
                    DataTable row = new DataTable();
                    row = db.RunReader("select RUN_ID,RUN_ANAME,RUN_NATIONAL,RUN_BIRTHDATE,RUN_USERNME,RUN_PWD,RUN_TYPE,RUN_ADDRESS from ENUM_RUNNER_DATA where RUN_ID like '%" + SrchCodeMessReg.Text + "%' and STATE='1'").Result;
                    if (row.Rows.Count > 0)
                    //messangerCodetxt.Text = row.Rows[0][0].ToString();
                    //messangerNametxt.Text = row.Rows[0][1].ToString();
                    //messangerIDtxt.Text = row.Rows[0][2].ToString();
                    //messangerBDate.Text = row.Rows[0][3].ToString();
                    //messangerUserNametxt.Text = row.Rows[0][4].ToString();
                    //messangerPasstxt.Text = row.Rows[0][5].ToString();
                    //MessangerTypCombo.Text = row.Rows[0][6].ToString();
                    //address_txt_reg_mandob.Text = row.Rows[0][7].ToString();
                    {
                        messGrid.ItemsSource = row.DefaultView;
                        messGrid.Columns[0].Header = "كود المندوب";
                        messGrid.Columns[1].Header = "اسم المندوب";
                        messGrid.Columns[2].Header = "الرقم القومي";
                        messGrid.Columns[3].Header = "تاريخ الميلاد";
                        messGrid.Columns[4].Header = "اسم المستخدم";
                        messGrid.Columns[5].Header = "كلمة السر";
                        messGrid.Columns[6].Header = "نوع المندوب";
                        messGrid.Columns[7].Header = "العنوان";

                        saveMessBtn.IsEnabled = false;
                        editMessBtn.IsEnabled = true;
                        delMessBtn.IsEnabled = true;
                    }
                    else
                    {
                        if (row.Rows.Count == 0)
                        {

                            MessageBox.Show("لا يوجد بيانات");
                        }
                        else MessageBox.Show("خطـأ");
                        messGrid.ItemsSource = null;
                    }
                }
                catch
                {

                }
            }

            //try
            //{
            //    if (e.Key == Key.Enter)
            //    {
            //        //object item = messGrid.SelectedItem;
            //        int id = Convert.ToInt32(SrchCodeMessReg.Text.ToString());
            //        MessangerData obj = mes.SelectMessengerById(id);
            //        messangerCodetxt.Text = obj.Id.ToString();
            //        messangerNametxt.Text = obj.Name;
            //        messangerUserNametxt.Text = obj.UserName;
            //        messangerPasstxt.Text = obj.Password;
            //        string txt = new TextRange(messangeraddrtxt.Document.ContentStart, messangeraddrtxt.Document.ContentEnd).Text;
            //        txt = obj.Address;
            //        messangeraddrtxt.Document.Blocks.Clear();
            //        messangeraddrtxt.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(txt)));
            //        MessangerTypCombo.Text = obj.Type;
            //        messangerBDate.Text = obj.DateOfBirth.ToString();
            //        messangerIDtxt.Text = obj.CardNum.ToString();



            //    }
            //}
            //catch
            //{
            //}
        }

        private void SrchCodeMessReq_KeyDown(object sender, KeyEventArgs e) //request mandoob
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    int id = Convert.ToInt32(SrchCodeMessReq.Text.ToString());
                    MessengerRequestData obj = req.SelectMessengerRequestById(id);
                    messReqCodetxt.Text = obj.ReqCode.ToString();
                    lblCompName.Content = obj.CompanyName;
                    messReqContactPersontxt.Text = obj.ContactPerson;
                    messReqDeptxt.Text = agent.get_dept(NameTab.Header.ToString());
                    messangerRequestreasontxt.Text = obj.RequestResons;

                    messReqaddrtxt.Text = obj.Address;
                    messReqbranchCombo.Text = obj.Branch;
                    messReqCityCombo.Text = obj.Governorate_Name;
                    messReqMessTypeCombo.Text = obj.MessengerType;
                    messReqothertxt.Document.Blocks.Clear();
                    //  messReqCompanyList.SelectedItem = lblCompName.Content;
                    //messReqothertxt.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(obj.RequestResons)));
                    // txtOtherResons.Text = obj.RequestReson_Other;
                    try
                    {
                        messReqDate.Text = obj.Date.ToString();
                    }
                    catch { }
                    messReqphonetxt.Text = obj.Phone.ToString();
                    messReqareaCombo.Text = obj.Area.ToString();
                }
            }
            catch
            {
            }
        }

        private void SrchCodeMessReq_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(SrchCodeMessReq.Text, "[^0-9]"))
                {
                    SrchCodeMessReq.Text = SrchCodeMessReq.Text.Remove(SrchCodeMessReq.Text.Length - 1);
                }
            }
            catch { }
        }


        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SummaryProviderSrchBtnst_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                g1st.Visibility = Visibility.Visible;
                int code = Convert.ToInt32(prCodeComboBox.Text.ToString());
                string contr_long = (((ComboBoxItem)SummaryProviderContrLongCombost.SelectedItem).Content.ToString());
                string contr_type = (((ComboBoxItem)SummaryProviderContrTypeCombost.SelectedItem).Content.ToString());
                System.Data.DataTable images = contract.get_provider_images(code, contr_type, contr_long);
                if (images.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد صور لهذا العقد");
                }
                List<string> image = new List<string>();
                for (int i = 0; i < images.Rows.Count; i++)
                {
                    for (int j = 0; j < images.Columns.Count; j++)
                    {
                        image.Add(images.Rows[i].ItemArray[j].ToString());
                    }
                }
                List<string> true_image = new List<string>();
                for (int i = 0; i < image.Count; i++)
                {
                    if (!(image[i].Contains("null")))
                    {
                        true_image.Add(image[i].ToString());

                    }
                    else
                        continue;
                }
                string imgname = "";

                pathesOfImage.Clear();
                spliter = 0;
                foreach (string item in true_image)
                {
                    pathesOfImage.Add(item);
                }
                img1st.Source = new BitmapImage(new Uri(pathesOfImage[spliter].ToString()));

                //for (int i = 0; i < true_image.Count; i++)
                //{
                //    int j = i + 1;
                //    imgname = "img" + j + "st";
                //    if (imgname == this.img1st.Name)
                //    {
                //        img1st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));

                //    }
                //    else if (imgname == this.img2st.Name)
                //    {
                //        img2st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                //    }
                //    else if (imgname == this.img3st.Name)
                //    {
                //        img3st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                //    }
                //    else if (imgname == this.img4st.Name)
                //    {
                //        img4st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                //    }
                //    else if (imgname == this.img5st.Name)
                //    {
                //        img5st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                //    }
                //    else if (imgname == this.img6st.Name)
                //    {
                //        img6st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                //    }
                //    else if (imgname == this.img7st.Name)
                //    {
                //        img7st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                //    }
                //    else if (imgname == this.img8st.Name)
                //    {
                //        img8st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                //    }
                //    else if (imgname == this.img9st.Name)
                //    {
                //        img9st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                //    }
                //}
                System.Data.DataTable data = contract.get_selected_provider_data(code);
                prCodetxtst.Text = data.Rows[0].ItemArray[0].ToString();
                prEnametxtst.Text = data.Rows[0].ItemArray[1].ToString();
                prAnametxtst.Text = data.Rows[0].ItemArray[2].ToString();
                prAddr1st.Text = data.Rows[0].ItemArray[3].ToString();
                prAddr2st.Text = data.Rows[0].ItemArray[4].ToString();
                prTel1st.Text = data.Rows[0].ItemArray[5].ToString();
                prTel2st.Text = data.Rows[0].ItemArray[6].ToString();
                prTermDatest.Text = data.Rows[0].ItemArray[7].ToString();
                prTaxFlagst.Text = data.Rows[0].ItemArray[9].ToString();
                prStampValst.Text = data.Rows[0].ItemArray[10].ToString();
                prDevLocst.Text = data.Rows[0].ItemArray[11].ToString();
                prDevextst.Text = data.Rows[0].ItemArray[12].ToString();
                prForMedDisst.Text = data.Rows[0].ItemArray[13].ToString();
                prLocMedDisst.Text = data.Rows[0].ItemArray[14].ToString();
                string degree = data.Rows[0].ItemArray[15].ToString();
                if (degree == "1")
                {
                    prDegst.Text = "A+";
                }
                else if (degree == "2")
                {
                    prDegst.Text = "A";
                }
                else if (degree == "3")
                {
                    prDegst.Text = "B";
                }
                else if (degree == "4")
                {
                    prDegst.Text = "ASO";
                }
                string flag = contract.get_terminate_flag(code.ToString());
                prTermDatest.Text = data.Rows[0].ItemArray[7].ToString();
                if (flag == "N" || flag == "n")
                {
                    prTermDatest.Visibility = Visibility.Hidden;
                    lbltermDatest.Visibility = Visibility.Hidden;
                    prTermFlagst.Text = "لا";
                }
                else if (flag == "Y" || flag == "y")
                {
                    prTermFlagst.Text = "نعم";
                    prTermFlagst.Foreground = Brushes.Red;
                    prTermDatest.Foreground = Brushes.Red;
                    prTermDatest.Foreground = Brushes.Red;
                    prTermDatest.Visibility = Visibility.Visible;
                    lbltermDatest.Visibility = Visibility.Visible;
                }
            }
            catch { }
        }

        private void SummaryProviderCodetxtst_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(SummaryProviderCodetxtst.Text, "[^0-9]"))
                {
                    SummaryProviderCodetxtst.Text = SummaryProviderCodetxtst.Text.Remove(SummaryProviderCodetxtst.Text.Length - 1);
                }
            }
            catch { }
        }
        private void CompanyIDtxtst_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(CompanyIDtxtst.Text, "[^0-9]"))
                {
                    CompanyIDtxtst.Text = CompanyIDtxtst.Text.Remove(CompanyIDtxtst.Text.Length - 1);
                }
            }
            catch { }
        }
        static public int spliter = 0;
        public List<string> pathesOfImage = new List<string>();
        private void SummaryCompSrchst_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string type;
                string longcontr;
                int contrid;
                System.Data.DataTable compDataTable;
                System.Data.DataTable classCodeDT;
                if (UserType == "hr")
                {
                    int comp = Convert.ToInt32(report.get_comp_id(UserCompany));



                    if (((ComboBoxItem)CompanyContractTypest.SelectedItem).Content.ToString() == ""
                        || ((ComboBoxItem)CompanyContractTypest.SelectedItem).Content == null
                        || ((ComboBoxItem)CompanyContractLongst.SelectedItem).Content.ToString() == ""
                        || ((ComboBoxItem)CompanyContractLongst.SelectedItem).Content == null
                         || summaryCompanyContract.SelectedItem == null
                        )
                    {
                        MessageBox.Show("ادخل البيانات كاملة");

                    }
                    else
                    {
                        type = (((ComboBoxItem)CompanyContractTypest.SelectedItem).Content.ToString());
                        longcontr = (((ComboBoxItem)CompanyContractLongst.SelectedItem).Content.ToString());
                        contrid = Convert.ToInt32(summaryCompanyContract.SelectedItem.ToString());
                        #region load images
                        System.Data.DataTable images = contract.get_company_image(comp, contrid, type, longcontr);
                        if (images.Rows.Count == 0)
                        {
                            MessageBox.Show("لا توجد صور");
                        }
                        else
                        {
                            try
                            {
                                List<string> image = new List<string>();
                                for (int i = 0; i < images.Rows.Count; i++)
                                {
                                    for (int j = 0; j < images.Columns.Count; j++)
                                    {
                                        image.Add(images.Rows[i].ItemArray[j].ToString());
                                    }
                                }
                                List<string> true_image = new List<string>();
                                for (int i = 0; i < image.Count; i++)
                                {
                                    if (!(image[i].Contains("null")))
                                    {
                                        true_image.Add(image[i].ToString());

                                    }
                                    else
                                        continue;
                                }
                                string imgname = "";

                                pathesOfImage.Clear();
                                spliter = 0;
                                foreach (string item in true_image)
                                {
                                    pathesOfImage.Add(item);
                                }
                                companyContract1st.Source = new BitmapImage(new Uri(pathesOfImage[spliter].ToString()));

                                //for (int i = 0; i < true_image.Count; i++)
                                //{
                                //    int j = i + 1;
                                //    imgname = "companyContract" + j + "st";
                                //    if (imgname == this.companyContract1st.Name)
                                //    {
                                //        companyContract1st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));

                                //    }
                                //    else if (imgname == this.companyContract2st.Name)
                                //    {
                                //        companyContract2st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract3st.Name)
                                //    {
                                //        companyContract3st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract4st.Name)
                                //    {
                                //        companyContract4st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract5st.Name)
                                //    {
                                //        companyContract5st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract6st.Name)
                                //    {
                                //        companyContract6st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract7st.Name)
                                //    {
                                //        companyContract7st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract8st.Name)
                                //    {
                                //        companyContract8st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract9st.Name)
                                //    {
                                //        companyContract9st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //}
                            }
                            catch { }
                        }

                        #endregion
                    }

                }
                else
                {

                    if (((ComboBoxItem)CompanyContractTypest.SelectedItem).Content.ToString() == ""
                           || ((ComboBoxItem)CompanyContractTypest.SelectedItem).Content == null
                           || ((ComboBoxItem)CompanyContractLongst.SelectedItem).Content.ToString() == ""
                           || ((ComboBoxItem)CompanyContractLongst.SelectedItem).Content == null
                            || summaryCompanyContract.SelectedItem == null
                           || CompanyComboSummary.Text == ""
                           )
                    {
                        MessageBox.Show("ادخل البيانات كاملة");

                    }
                    else
                    {
                        int compid = Convert.ToInt32(CompanyComboSummary.Text.ToString());
                        type = (((ComboBoxItem)CompanyContractTypest.SelectedItem).Content.ToString());
                        longcontr = (((ComboBoxItem)CompanyContractLongst.SelectedItem).Content.ToString());
                        contrid = Convert.ToInt32(summaryCompanyContract.SelectedItem.ToString());
                        compDataTable = contract.get_company_data(compid, contrid);
                        Companyanamest.Text = compDataTable.Rows[0].ItemArray[0].ToString();
                        CompanyEnamest.Text = compDataTable.Rows[0].ItemArray[1].ToString();
                        addr1txtst.Text = compDataTable.Rows[0].ItemArray[2].ToString();
                        startDatetxtst.Text = compDataTable.Rows[0].ItemArray[3].ToString();
                        endDatetxtst.Text = compDataTable.Rows[0].ItemArray[4].ToString();
                        classCodeDT = contract.get_class_code(compid, contrid);
                        ClassCodeCombost.Items.Clear();
                        for (int i = 0; i < classCodeDT.Rows.Count; i++)
                        {
                            ClassCodeCombost.Items.Add(classCodeDT.Rows[i].ItemArray[0].ToString() + " " + classCodeDT.Rows[i].ItemArray[1].ToString());
                        }
                        #region load images
                        System.Data.DataTable images = contract.get_company_image(compid, contrid, type, longcontr);
                        if (images.Rows.Count == 0)
                        {
                            MessageBox.Show("لا توجد صور");
                        }
                        else
                        {
                            try
                            {
                                List<string> image = new List<string>();
                                for (int i = 0; i < images.Rows.Count; i++)
                                {
                                    for (int j = 0; j < images.Columns.Count; j++)
                                    {
                                        image.Add(images.Rows[i].ItemArray[j].ToString());
                                    }
                                }
                                List<string> true_image = new List<string>();
                                for (int i = 0; i < image.Count; i++)
                                {
                                    if (!(image[i].Contains("null")))
                                    {
                                        true_image.Add(image[i].ToString());

                                    }
                                    else
                                        continue;
                                }
                                string imgname = "";

                                pathesOfImage = true_image;
                                companyContract1st.Source = new BitmapImage(new Uri(pathesOfImage[spliter].ToString(), UriKind.RelativeOrAbsolute));

                                //for (int i = 0; i < true_image.Count; i++)
                                //{
                                //    int j = i + 1;
                                //    imgname = "companyContract" + j + "st";
                                //    if (imgname == this.companyContract1st.Name)
                                //    {
                                //        companyContract1st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));

                                //    }
                                //    else if (imgname == this.companyContract2st.Name)
                                //    {
                                //        companyContract2st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract3st.Name)
                                //    {
                                //        companyContract3st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract4st.Name)
                                //    {
                                //        companyContract4st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract5st.Name)
                                //    {
                                //        companyContract5st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract6st.Name)
                                //    {
                                //        companyContract6st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract7st.Name)
                                //    {
                                //        companyContract7st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract8st.Name)
                                //    {
                                //        companyContract8st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract9st.Name)
                                //    {
                                //        companyContract9st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract10st.Name)
                                //    {
                                //        companyContract10st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract11st.Name)
                                //    {
                                //        companyContract11st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract12st.Name)
                                //    {
                                //        companyContract12st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract13st.Name)
                                //    {
                                //        companyContract13st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract14st.Name)
                                //    {
                                //        companyContract14st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract15st.Name)
                                //    {
                                //        companyContract15st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract16st.Name)
                                //    {
                                //        companyContract16st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract17st.Name)
                                //    {
                                //        companyContract17st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract18st.Name)
                                //    {
                                //        companyContract18st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract19st.Name)
                                //    {
                                //        companyContract19st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    else if (imgname == this.companyContract20st.Name)
                                //    {
                                //        companyContract20st.Source = new BitmapImage(new Uri(true_image[i].ToString(), UriKind.RelativeOrAbsolute));
                                //    }
                                //    }
                            }
                            catch { }
                        }

                        #endregion
                    }
                }
            }
            catch { }
        }

        private void dtpFromz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dtpTo_SelectedDateChangedz(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                indemnity_id = Convert.ToInt32(IndemnityCompanyComboz.Text.ToString());

                string DateFrom = dtpFromz.Text;
                string DateTo = dtpToz.Text;
                List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCompanyCodeSearch(DateFrom, DateTo, indemnity_id);
                IndemnityGridz.Visibility = Visibility.Visible;
                IndemnityGridz.ItemsSource = Indemnities;
                indemnityItmCounttxtz.Content = Indemnities.Count;
            }
            catch { }
        }


        private void abdodgmonthdata_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)abdodgmonthdata.SelectedItems[0];
                jjcardID = row[0].ToString();
                System.Data.DataTable a = db.RunReader(" select MED_CODE,MED_NAME,DOSAGE_FORM,UNIT_NO,PACK_SIZE,DOS_DUR,TOT_DUR,CON_MED,ACTIVE from V_MED_MEDICINE where CARD_NO='" + jjcardID + "'").Result;
                a.Columns.Add("ActiveState", typeof(String));

                //System.Data.DataRow s;
                int i = 0;
                foreach (DataRow rowz in a.Rows)
                {


                    if (a.Rows[i]["ACTIVE"].ToString() == "Y")
                    {
                        rowz["ActiveState"] = "Active";
                    }
                    else
                    {
                        rowz["ActiveState"] = "Lock";
                    }


                    i++;
                }
                a.Columns.RemoveAt(8);
                abdodgmonthdata2.ItemsSource = a.DefaultView;


                System.Data.DataTable b = db.RunReader(" select D_DATE,EMP_SUB  from V_TRANSACTION where CARD_ID='" + jjcardID + "' order by D_DATE desc").Result;
                txtabdoLastDate.Text = b.Rows[0][0].ToString();
                txtabdoProvider.Text = b.Rows[0][1].ToString();
            }
            catch
            { }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //Provider_Request s = new Provider_Request(jjUesrname, jj22CompanyName);
            //s.Show();
        }

        private void txtCardNum_KeyDownz(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string DateFrom = dtpFromz.Text;
                string DateTo = dtpToz.Text;

                string CardNum = txtCardNumz.Text.ToString();
                try
                {
                    if (UserType == "hr")
                    {
                        string[] arr = CardNum.Split('-');
                        string comp = arr[0].ToString();
                        string compid = report.get_comp_id(UserCompany);
                        if (comp == compid)
                        {
                            List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCardNoSearch(DateFrom, DateTo, CardNum);
                            if (Indemnities.Count == null)
                            {
                                MessageBox.Show("لا توجد بيانات");
                            }
                            else
                            {
                                IndemnityGridz.Visibility = Visibility.Visible;
                                IndemnityGridz.ItemsSource = Indemnities;
                                indemnityItmCounttxtz.Content = Indemnities.Count.ToString();
                            }
                        }
                        else
                        {
                            MessageBox.Show("غير مسموح بهذه الشركة");
                        }
                    }
                    else
                    {
                        List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCardNoSearch(DateFrom, DateTo, CardNum);
                        if (Indemnities == null)
                        {
                            MessageBox.Show("لا توجد بيانات");
                        }
                        else
                        {
                            IndemnityGridz.Visibility = Visibility.Visible;
                            IndemnityGridz.ItemsSource = Indemnities;
                            indemnityItmCounttxtz.Content = Indemnities.Count.ToString();
                        }
                    }
                }
                catch { }
            }
        }


        private void messReqCodeSrchz_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(messReqCodeSrchz.Text, "[^0-9]"))
                {
                    messReqCodeSrchz.Text = messReqCodeSrchz.Text.Remove(messReqCodeSrchz.Text.Length - 1);
                }
            }
            catch { }
        }
        private void messReqCodeSrchz_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    int id = Convert.ToInt32(messReqCodeSrchz.Text.ToString());
                    MessengerRequestData obj = req.SelectMessengerRequestById(id);
                    messReqCodetxtz.Text = obj.ReqCode.ToString();
                    lblCompNamez.Content = obj.CompanyName;
                    messReqContactPersontxtz.Text = obj.ContactPerson;
                    messReqDeptxtz.Text = agent.get_dept(NameTab.Header.ToString());
                    messReqaddrtxtz.Text = obj.Address;
                    messReqbranchComboz.Text = obj.Branch;
                    messReqCityComboz.Text = obj.Governorate_Name;
                    messReqMessTypeComboz.Text = obj.MessengerType;
                    messReqothertxtz.Document.Blocks.Clear();
                    messReqCompanyListz.SelectedItem = lblCompNamez.Content;
                    messangerRequestReasontxtz.Text = obj.RequestResons;
                    //messReqothertxtz.Document.Blocks.Add(new System.Windows.Documents.Paragraph(new Run(obj.RequestResons)));
                    // txtOtherResons.Text = obj.RequestReson_Other;
                    try
                    {
                        messReqDatez.Text = obj.Date.ToString();
                    }
                    catch { }
                    messReqphonetxtz.Text = obj.Phone.ToString();
                    messReqareaComboz.Text = obj.Area.ToString();
                }
            }
            catch
            {
            }
        }
        private void messReqGrid_SelectionChangedz(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (messReqGridz.SelectedItems.Count >= 1)
                {
                    object item = messReqGridz.SelectedItem;
                    int id = Convert.ToInt32((messReqGridz.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    MessengerRequestData obj = req.SelectMessengerRequestById(id);
                    messReqCodetxtz.Text = obj.ReqCode.ToString();
                    lblCompNamez.Content = obj.CompanyName;
                    messReqContactPersontxtz.Text = obj.ContactPerson;
                    messReqDeptxtz.Text = agent.get_dept(NameTab.Header.ToString());
                    messReqaddrtxtz.Text = obj.Address;
                    messReqbranchComboz.Text = obj.Branch;
                    messReqCityComboz.Text = obj.Governorate_Name;
                    messReqMessTypeComboz.Text = obj.MessengerType;
                    messangerRequestReasontxtz.Text = obj.RequestResons;
                    // txtOtherResons.Text = obj.RequestReson_Other;
                    try
                    {
                        messReqDatez.Text = obj.Date.ToString();
                    }
                    catch { }
                    messReqphonetxtz.Text = obj.Phone.ToString();
                    messReqareaComboz.Text = obj.Area.ToString();
                }
            }
            catch
            {
            }
        }



        private void messReqSaveBtn_Clickz(object sender, RoutedEventArgs e)
        {
            try
            {
                //dataGridView1.Columns["Governorate_Code"].Visible = false;



                bool valid = false;
                #region test
                if (messReqCodetxtz.Text != "" && messReqContactPersontxtz.Text != "")
                {
                    valid = true;
                }


                #endregion

                #region valid
                //if (txtReqCode.Text != "" && cmbGoverneratorNames.Text != "" &&txtContactPerson.Text!=""  && cmbBranchNames.Text != "" &&cmbMessengerType.Text!="" && cmcbAreaInGovernerate.Text != "")
                //{
                //    valid = true;
                //}
                #endregion
                if (valid == true)
                {
                    MessengerRequestData obj = new MessengerRequestData();
                    obj.ReqCode = int.Parse(messReqCodetxtz.Text);
                    obj.Governorate_Name = messReqCityComboz.Text.ToString();
                    obj.CompanyName = lblCompNamez.Content.ToString();
                    obj.Branch = messReqbranchComboz.Text.ToString();
                    obj.Area = messReqareaComboz.Text.ToString();
                    obj.Address = messReqaddrtxtz.Text.ToString();
                    obj.Dept = agent.get_dept(NameTab.Header.ToString());
                    obj.ContactPerson = messReqContactPersontxtz.Text.ToString();
                    obj.Phone = messReqphonetxtz.Text;
                    obj.Date = messReqDatez.Text.ToString();
                    obj.MessengerType = (((ComboBoxItem)messReqMessTypeComboz.SelectedItem).Content).ToString();

                    if (chkReadyCardsResonz.IsChecked == true)
                    {
                        obj.RequestResons += chkReadyCardsResonz.Content + " - ";
                    }
                    if (chkReadyCheekz.IsChecked == true)
                    {
                        obj.RequestResons += chkReadyCheekz.Content + " - ";
                    }
                    if (chkDeliverPaperz.IsChecked == true)
                    {
                        obj.RequestResons += chkDeliverPaperz.Content + " - ";
                    }
                    string txt = new TextRange(messReqothertxtz.Document.ContentStart, messReqothertxtz.Document.ContentEnd).Text.ToString();
                    if (chkOtherResonsz.IsChecked == true)
                    {
                        obj.RequestResons += txt + " - ";
                    }

                    if (chkReqResonReceiveCheekz.IsChecked == true)
                    {
                        obj.RequestResons += chkReqResonReceiveCheekz.Content + " - ";
                    }
                    if (chkReqResonDeliverContractsz.IsChecked == true)
                    {
                        obj.RequestResons += chkReqResonDeliverContractsz.Content + " - ";
                    }
                    if (chkReqResonReceiveEmpDataz.IsChecked == true)
                    {
                        obj.RequestResons += chkReqResonReceiveEmpDataz.Content + " - ";
                    }
                    if (chkReqResonReceiveContractsz.IsChecked == true)
                    {
                        obj.RequestResons += chkReqResonReceiveContractsz.Content + " - ";
                    }
                    if (chkReqResonSMSz.IsChecked == true)
                    {
                        obj.RequestResons += chkReqResonSMSz.Content;
                    }


                    if (chkVIPz.IsChecked == true)
                    {
                        obj.VIP = "1";
                    }
                    else
                    {
                        obj.VIP = "0";
                    }

                    //--------check if this messenger already exist------------------
                    MessengerRequestData OldMessenger = req.SelectMessengerRequestById(obj.ReqCode);
                    if (OldMessenger != null)
                    {
                        MessageBox.Show("موجود بالفعل");
                    }
                    else            //----insert the new messenger
                    {
                        int affected = req.InsertMessangerRequest(obj);
                        if (affected > 0)
                        {
                            //messReqGrid.Columns["RequestResons"].Width = 300;
                            MessageBox.Show("تم الحفظ بنجاح", "Sucess");
                            //------To Reflect on The cmb---------------------//
                            List<MessengerRequestData> list = req.SelectAllMessengersRequests();
                            messReqGridz.ItemsSource = list;
                            messReqitemCountz.Content = list.Count.ToString();
                            #region clearAll
                            messReqCodeSrchz.Text = "";
                            messReqComptxtz.Text = "";
                            messReqCityComboz.Text = "";
                            messReqareaComboz.Text = "";
                            messReqbranchComboz.Text = "";
                            messReqDatez.Text = "";
                            messReqContactPersontxtz.Text = "";
                            //messReqDeptxtz.Text = "";
                            messReqothertxtz.Document.Blocks.Clear();
                            messReqphonetxtz.Text = "";
                            messReqaddrtxtz.Text = "";
                            chkReadyCardsResonz.IsChecked = false;
                            chkReadyCheekz.IsChecked = false;
                            chkDeliverPaperz.IsChecked = false;
                            chkOtherResonsz.IsChecked = false;
                            chkReqResonDeliverContractsz.IsChecked = false;
                            chkReqResonReceiveCheekz.IsChecked = false;
                            chkReqResonReceiveContractsz.IsChecked = false;
                            chkReqResonReceiveEmpDataz.IsChecked = false;
                            chkReqResonSMSz.IsChecked = false;
                            //lblCompanyNamez.Content = "";
                            messReqMessTypeComboz.Text = "";
                            #endregion
                            #region SelectMaxId
                            string affected2 = req.SelectMaxReqMessId();
                            if (affected2 == "")
                            {

                            }
                            else
                            {
                                //-------clear selection mode at first time when load 
                                List<MessengerRequestData> list2 = req.SelectAllMessengersRequests();
                                messReqGridz.ItemsSource = list2;
                                //dataGridView1.Rows[0].Cells[0].Selected = false;
                                //dataGridView1.Rows[1].Cells[0].Selected = false;
                                //txtReqCode.Text = ""; cmbGoverneratorNames.Text = ""; txtCompanyName.Text = ""; cmbBranchNames.Text = ""; cmcbAreaInGovernerate.Text = "";
                                ////------select maxMessenger----------------------
                                int aff = int.Parse(affected2.ToString());
                                int Next = (aff + 1);
                                messReqCodetxtz.Text = Next.ToString();
                            }
                            #endregion
                            try
                            {
                                //dataGridView1.Columns[3].Visible = false;
                                //dataGridView1.Columns[4].Visible = false;
                            }
                            catch { }

                        }
                        else
                        {
                            MessageBox.Show("رجاء ، اسم المستخدم مكرر ادخل اسم جديد", "Fail");
                        }
                    }
                }

                else
                {
                    MessageBox.Show("املا كل البيانات من فضلك");
                }
            }
            catch { }
        }

        private void messReqEditBtn_Clickz(object sender, RoutedEventArgs e)
        {
            try
            {
                //--------check if this messenger already exist------------------
                MessengerRequestData obj = new MessengerRequestData();
                obj.ReqCode = int.Parse(messReqCodetxtz.Text);
                obj.Governorate_Name = messReqCityComboz.Text.ToString();
                obj.CompanyName = lblCompNamez.Content.ToString();
                if (messReqbranchComboz.Text == null || messReqbranchComboz.Text == "")
                {
                    obj.Branch = "";
                }
                else
                {
                    obj.Branch = messReqbranchComboz.Text.ToString();
                }
                obj.Area = messReqareaComboz.Text.ToString();
                obj.Address = messReqaddrtxtz.Text;
                obj.Dept = messReqDeptxtz.Text.ToString();
                obj.ContactPerson = messReqContactPersontxtz.Text;
                obj.Phone = messReqphonetxtz.Text;
                obj.Date = messReqDatez.Text.ToString();
                obj.MessengerType = (((ComboBoxItem)messReqMessTypeComboz.SelectedItem).Content).ToString();
                if (chkReadyCardsResonz.IsChecked == true)
                {
                    obj.RequestResons += chkReadyCardsResonz.Content + " - ";
                }
                if (chkReadyCheekz.IsChecked == true)
                {
                    obj.RequestResons += chkReadyCheekz.Content + " - ";
                }
                if (chkDeliverPaperz.IsChecked == true)
                {
                    obj.RequestResons += chkDeliverPaperz.Content + " - ";
                }
                string txt = new TextRange(messReqothertxtz.Document.ContentStart, messReqothertxtz.Document.ContentEnd).Text.ToString();
                if (chkOtherResonsz.IsChecked == true)
                {
                    obj.RequestResons += txt + " - ";
                }

                if (chkReqResonReceiveCheekz.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonReceiveCheekz.Content + " - ";
                }
                if (chkReqResonDeliverContractsz.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonDeliverContractsz.Content + " - ";
                }
                if (chkReqResonReceiveEmpDataz.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonReceiveEmpDataz.Content + " - ";
                }
                if (chkReqResonReceiveContractsz.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonReceiveContractsz.Content + " - ";
                }
                if (chkReqResonSMSz.IsChecked == true)
                {
                    obj.RequestResons += chkReqResonSMSz.Content + " - ";
                }
                if (deliverIndemnityChkz.IsChecked == true)
                {
                    obj.RequestResons += deliverIndemnityChkz.Content + " - ";
                }
                if (receiveIndemnityChkz.IsChecked == true)
                {
                    obj.RequestResons += receiveIndemnityChkz.Content;
                }
                if (chkVIPz.IsChecked == true)
                {
                    obj.VIP = "1";
                }
                else
                {
                    obj.VIP = "0";
                }

                MessengerRequestData OldMessenger = req.SelectMessengerRequestById(obj.ReqCode);
                if (OldMessenger != null)
                {
                    if (obj.ReqCode == OldMessenger.ReqCode)//---means that it is exist so update it
                    {
                        int affected = req.UpdateMessengerRequest(obj, obj.ReqCode);
                        if (affected > 0)
                        {
                            MessageBox.Show("تم التحديث بنجاح", "Success");
                            #region clearAll
                            messReqComptxtz.Text = "";
                            messReqCityComboz.Text = "";
                            messReqareaComboz.Text = "";
                            messReqbranchComboz.Text = "";
                            messReqDatez.Text = "";
                            messReqContactPersontxtz.Text = "";
                            //messReqDeptxtz.Text = "";
                            messReqothertxtz.Document.Blocks.Clear();
                            messReqphonetxtz.Text = "";
                            messReqaddrtxtz.Text = "";
                            //messangerIDtxtz.Text = "";
                            chkReadyCardsResonz.IsChecked = false;
                            chkReadyCheekz.IsChecked = false;
                            chkDeliverPaperz.IsChecked = false;
                            chkOtherResonsz.IsChecked = false;
                            chkReqResonDeliverContractsz.IsChecked = false;
                            chkReqResonReceiveCheekz.IsChecked = false;
                            chkReqResonReceiveContractsz.IsChecked = false;
                            chkReqResonReceiveEmpDataz.IsChecked = false;
                            chkReqResonSMSz.IsChecked = false;
                            deliverIndemnityChkz.IsChecked = false;
                            receiveIndemnityChkz.IsChecked = false;
                            messangerRequestReasontxtz.Text = "";
                            lblCompNamez.Content = "";
                            messReqMessTypeComboz.Text = "";
                            messReqCodeSrchz.Text = "";
                            #endregion
                            #region SelectMaxId
                            string affected2 = req.SelectMaxReqMessId();
                            if (affected2 == "")
                            {

                            }
                            else
                            {
                                //-------clear selection mode at first time when load 
                                List<MessengerRequestData> list2 = req.SelectAllMessengersRequests();
                                messReqGridz.ItemsSource = list2;
                                try
                                {
                                    messReqGridz.SelectedCells.Clear();
                                }
                                catch { }
                                //txtReqCode.Text = ""; cmbGoverneratorNames.Text = ""; txtCompanyName.Text = ""; cmbBranchNames.Text = ""; cmcbAreaInGovernerate.Text = "";
                                ////------select maxMessenger----------------------
                                int aff = int.Parse(affected2.ToString());
                                int Next = (aff + 1);
                                messReqCodetxtz.Text = Next.ToString();

                            }
                            #endregion
                            //------ To Reflect on The cmb-------------//

                            List<MessengerRequestData> list = req.SelectAllMessengersRequests();
                            messReqGridz.ItemsSource = list;
                            messReqitemCountz.Content = list.Count.ToString();

                        }
                    }
                }
            }
            catch { }
        }

        private void messReqDelBtn_Clickz(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = int.Parse(messReqCodetxtz.Text);
                int affected = req.DeleteMessengerRequest(id);
                if (affected > 0)
                {
                    MessageBoxResult result = MessageBox.Show("هل انت متأكد ؟", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        MessageBox.Show("تمت عملية الحذف بنجاح", "Success");
                        //------To Reflect on The cmb---------------------//
                        List<MessengerRequestData> list = req.SelectAllMessengersRequests();
                        messReqGridz.ItemsSource = list;
                        messReqitemCountz.Content = list.Count.ToString();
                        ////-------select Max_Messenger----------------------//
                        string max = req.SelectMaxReqMessId();
                        try
                        {
                            int max2 = int.Parse(max) + 1;
                            messReqCodetxtz.Text = max2.ToString();
                        }
                        catch { }
                        //-----------To Clear TextBoxes-------------------//
                        #region clearAll
                        messReqComptxtz.Text = "";
                        messReqCityComboz.Text = "";
                        messReqareaComboz.Text = "";
                        messReqbranchComboz.Text = "";
                        messReqDatez.Text = "";
                        messReqContactPersontxtz.Text = "";
                        //messReqDeptxtz.Text = "";
                        messReqothertxtz.Document.Blocks.Clear();
                        messReqphonetxtz.Text = "";
                        messReqaddrtxtz.Text = "";
                        //messangerIDtxtz.Text = "";
                        chkReadyCardsResonz.IsChecked = false;
                        chkReadyCheekz.IsChecked = false;
                        chkDeliverPaperz.IsChecked = false;
                        chkOtherResonsz.IsChecked = false;
                        chkReqResonDeliverContractsz.IsChecked = false;
                        chkReqResonReceiveCheekz.IsChecked = false;
                        chkReqResonReceiveContractsz.IsChecked = false;
                        chkReqResonReceiveEmpDataz.IsChecked = false;
                        chkReqResonSMSz.IsChecked = false;
                        lblCompNamez.Content = "";
                        messReqMessTypeComboz.Text = "";
                        #endregion            // -------clear selection mode at first time when load ---------------
                        try
                        {
                            messReqGridz.SelectedCells.Clear();
                            #region clearAll
                            messReqCodeSrchz.Text = "";
                            messReqComptxtz.Text = "";
                            messReqCityComboz.Text = "";
                            messReqareaComboz.Text = "";
                            messReqbranchComboz.Text = "";
                            messReqDatez.Text = "";
                            messReqContactPersontxtz.Text = "";
                            // messReqDeptxtz.Text = "";
                            messReqothertxtz.Document.Blocks.Clear();
                            messReqphonetxtz.Text = "";
                            messReqaddrtxtz.Text = "";
                            //messangerIDtxtz.Text = "";
                            chkReadyCardsResonz.IsChecked = false;
                            chkReadyCheekz.IsChecked = false;
                            chkDeliverPaperz.IsChecked = false;
                            chkOtherResonsz.IsChecked = false;
                            chkReqResonDeliverContractsz.IsChecked = false;
                            chkReqResonReceiveCheekz.IsChecked = false;
                            chkReqResonReceiveContractsz.IsChecked = false;
                            chkReqResonReceiveEmpDataz.IsChecked = false;
                            chkReqResonSMSz.IsChecked = false;
                            lblCompNamez.Content = "";
                            messReqMessTypeComboz.Text = "";
                            #endregion    // -------clear selection mode at first time when load ---------------
                        }
                        catch { }
                    }
                    else { }
                }
            }
            catch { }
        }

        private void messReqNewBtn_Clickz(object sender, RoutedEventArgs e)
        {
            messReqareaComboz_Copy.Text = "";
            messReqareaComboz_Copy.ItemsSource = User.ALL_Company().DefaultView;
            try
            {
                messangerRequestReasontxtz.Text = "";
                messReqCodeSrchz.Text = "";
                string affected2 = req.SelectMaxReqMessId();
                int aff = int.Parse(affected2.ToString());
                int Next = (aff + 1);
                messReqCodetxtz.Text = Next.ToString();
                #region clearAll
                messReqComptxtz.Text = "";
                messReqCityComboz.Text = "";
                messReqareaComboz.Text = "";
                messReqbranchComboz.Text = "";
                messReqDatez.Text = "";
                messReqContactPersontxtz.Text = "";
                //messReqDeptxtz.Text = "";
                messReqothertxtz.Document.Blocks.Clear();
                messReqphonetxtz.Text = "";
                messReqaddrtxtz.Text = "";
                //messangerIDtxtz.Text = "";
                chkReadyCardsResonz.IsChecked = false;
                chkReadyCheekz.IsChecked = false;
                chkDeliverPaperz.IsChecked = false;
                chkOtherResonsz.IsChecked = false;
                chkReqResonDeliverContractsz.IsChecked = false;
                chkReqResonReceiveCheekz.IsChecked = false;
                chkReqResonReceiveContractsz.IsChecked = false;
                chkReqResonReceiveEmpDataz.IsChecked = false;
                chkReqResonSMSz.IsChecked = false;
                lblCompNamez.Content = "";
                messReqMessTypeComboz.Text = "";
                messReqCodeSrchz.Text = "";
                #endregion
                // -------clear selection mode at first time when load ---------------
                messReqGridz.SelectedCells.Clear();
            }
            catch
            {
                //txtReqCode.Text = "1";
                string affected2 = req.SelectMaxReqMessId();
                int aff = int.Parse(affected2.ToString());
                int Next = (aff + 1);
                messReqCodetxtz.Text = Next.ToString();
            }
        }

        private void messReqComptxt_TextChangedz(object sender, TextChangedEventArgs e)
        {
            string companyName = messReqComptxtz.Text;
            try
            {
                List<MessengerRequestData> Companylist = req.SelectAllCompanies(companyName, companyName);
                messReqCompanyListz.ItemsSource = Companylist;
                messReqCompanyListz.DisplayMemberPath = "CompanyName";
                messReqCompanyListz.SelectedValuePath = "CompanyCode";
            }
            catch { }
        }

        private void messReqCompanyList_SelectionChangedz(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int compId = Convert.ToInt32(messReqCompanyListz.SelectedValue.ToString());
                string compName = store.get_indem_company_name(compId);
                lblCompNamez.Content = compName;
                // messReqbranchCombo.Items.Clear();
                messReqaddrtxtz.Text = store.GetCompanyAddress(compId);
                List<MessengerRequestData> Branches = req.SelectAllCompanies_Branches(compId.ToString());
                //if (Branches == null)
                //{
                //    MessageBox.Show("لا توجد فروع ");
                //}
                messReqbranchComboz.ItemsSource = Branches;
                messReqbranchComboz.DisplayMemberPath = "Branch";
                messReqbranchComboz.SelectedValuePath = "Branch";
            }
            catch { }
        }

        private void saveMedicineBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataMedcine.Rows.Count == 0)
                {
                    MessageBox.Show("من فضلك اختر group");
                }
                else
                {
                    for (int i = 0; i < dataMedcine.Rows.Count; i++)
                    {
                        int supercode = Convert.ToInt32(dataMedcine.Rows[i].ItemArray[0].ToString());
                        string ename = dataMedcine.Rows[i].ItemArray[1].ToString();
                        string aname = dataMedcine.Rows[i].ItemArray[2].ToString();
                        string type = dataMedcine.Rows[i].ItemArray[3].ToString();
                        int groupcode = Convert.ToInt32(dataMedcine.Rows[i].ItemArray[4].ToString());
                        Medicie.add_medicine(supercode, ename, aname, type, groupcode);
                    }
                    medicineGroupGrid.ItemsSource = Medicie.get_all_medicine_group().DefaultView;
                    MessageBox.Show("تم حفظ البيانات");
                }
            }
            catch { }
        }



        private void messReqCityCombo_SelectionChangedz(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                int id = int.Parse(messReqCityComboz.SelectedValue.ToString());
                List<MessengerRequestData> list2 = req.SelectAllArea_In_governerate(id);
                messReqareaComboz.ItemsSource = list2;
                messReqareaComboz.DisplayMemberPath = "Governorate_Name";
                messReqareaComboz.SelectedValuePath = "Governorate_Code";
                //messReqGrid.Columns["Governorate_Code"].Visible = false;
            }
            catch { }
        }

        private void ApprovaltxtCardNum_KeyDownz(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    string CardNo = ApprovaltxtCardNumz.Text.ToString();
                    int comp_emp = 0;
                    int card_approve = 0;
                    if (UserType == "hr")
                    {
                        string[] arr = CardNo.Split('-');
                        string comp = arr[0].ToString();
                        if (comp == CompanyCode)
                        {
                            comp_emp = client.validate_card_num(CardNo);
                            card_approve = client.validate_card_approval(CardNo);
                            if (comp_emp >= 1)
                            {
                                if (card_approve >= 1)
                                {
                                    string value_PatiantName = ApprovaltxtCardNumz.Text;
                                    List<EmpApprovalData> Branches = emp.SelectAllApprovals(CardNo, value_PatiantName);
                                    approvalGridz.ItemsSource = Branches;
                                    totalApprovalCountz.Content = client.count_approve(CardNo).ToString();
                                    approvalGridz.Columns[6].Visibility = Visibility.Hidden;
                                    approvalGridz.Columns[7].Visibility = Visibility.Hidden;
                                    approvalGridz.Columns[8].Visibility = Visibility.Hidden;
                                    approvalGridz.Columns[9].Visibility = Visibility.Hidden;
                                    approvalGridz.Columns[11].Visibility = Visibility.Hidden;
                                    approvalItemCounttxtz.Content = Branches.Count.ToString();
                                }
                                else
                                {
                                    MessageBox.Show("لا توجد موافقة لهذا الكارت");
                                    ApprovaltxtCardNumz.Text = "";
                                    approvalItemCounttxtz.Content = approvalGridz.Items.Count - 1;
                                    totalApprovalCountz.Content = "0";
                                }
                            }
                            else
                            {
                                MessageBox.Show("رقم كارت غير موجود");
                                ApprovaltxtCardNumz.Text = "";
                                approvalItemCounttxtz.Content = approvalGridz.Items.Count - 1;
                                totalApprovalCountz.Content = "0";
                            }
                        }
                        else
                        {
                            MessageBox.Show("غير مسموح ببيانات هذه الشركة");
                        }

                    }
                    else
                    {
                        comp_emp = client.validate_card_num(CardNo);
                        card_approve = client.validate_card_approval(CardNo);
                        if (comp_emp >= 1)
                        {
                            if (card_approve >= 1)
                            {
                                string value_PatiantName = ApprovaltxtCardNumz.Text;
                                List<EmpApprovalData> Branches = emp.SelectAllApprovals(CardNo, value_PatiantName);
                                approvalGridz.ItemsSource = Branches;
                                totalApprovalCountz.Content = client.count_approve(CardNo).ToString();
                                approvalGridz.Columns[6].Visibility = Visibility.Hidden;
                                approvalGridz.Columns[7].Visibility = Visibility.Hidden;
                                approvalGridz.Columns[8].Visibility = Visibility.Hidden;
                                approvalGridz.Columns[9].Visibility = Visibility.Hidden;
                                approvalGridz.Columns[11].Visibility = Visibility.Hidden;
                                approvalItemCounttxtz.Content = Branches.Count.ToString();
                            }
                            else
                            {
                                MessageBox.Show("لا توجد موافقة لهذا الكارت");
                                ApprovaltxtCardNumz.Text = "";
                                approvalItemCounttxtz.Content = approvalGridz.Items.Count - 1;
                                totalApprovalCountz.Content = "0";
                            }
                        }
                        else
                        {
                            MessageBox.Show("رقم كارت غير موجود");
                            ApprovaltxtCardNumz.Text = "";
                            approvalItemCounttxt.Content = approvalGridz.Items.Count - 1;
                            totalApprovalCountz.Content = "0";
                        }
                    }
                }
            }
            catch { }
        }


        private void chkOtherResons_Checkedz(object sender, RoutedEventArgs e)
        {
            messReqothertxtz.Visibility = Visibility.Visible;
        }

        private void ClassCodeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                dservcodetxt.Items.Clear();
                string selected = ClassCodeCombo.SelectedItem.ToString();
                string[] arr = selected.Split(' ');
                string class_code = arr[0].ToString();
                //string classcode = ClassCodeCombo.SelectedItem.ToString();
                int compid = Convert.ToInt32(CompanyComboBoxMain.Text.ToString());
                int contrid = Convert.ToInt32(summaryMainContractCompany.SelectedItem.ToString());
                System.Data.DataTable data = contract.get_max_amount_hospital_ambulance(compid, contrid, class_code);
                maxamounttxt.Text = data.Rows[0].ItemArray[0].ToString();
                string hospital = data.Rows[0].ItemArray[1].ToString();
                if (hospital == "1")
                {
                    hospitaltxt.Text = "A+";
                }
                else if (hospital == "2")
                {
                    hospitaltxt.Text = "A";
                }
                else if (hospital == "3")
                {
                    hospitaltxt.Text = "B";
                }
                else if (hospital == "4")
                {
                    hospitaltxt.Text = "ASO";
                }
                ambulancetxt.Text = data.Rows[0].ItemArray[2].ToString();
                dservcodetxt.Items.Clear();
                System.Data.DataTable servCodes = contract.get_serv_code(compid, contrid, class_code);
                for (int i = 0; i < servCodes.Rows.Count; i++)
                {
                    dservcodetxt.Items.Add(servCodes.Rows[i].ItemArray[0].ToString() + " " + servCodes.Rows[i].ItemArray[1].ToString());
                }
                //int max_amt = contract.get_max_amount(compid, contrid, classcode);
                //maxamounttxt.Text = max_amt.ToString();
            }
            catch { }
        }

        private void ClassCodeCombo_SelectionChangedst(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                dservcodetxtst.Items.Clear();
                string selected = ClassCodeCombost.SelectedItem.ToString();
                string[] arr = selected.Split(' ');
                string class_code = arr[0].ToString();
                int compid = 0;
                if (UserType == "hr")
                {
                    compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                }
                else
                {
                    compid = Convert.ToInt32(CompanyComboSummary.Text.ToString());
                }
                int contrid = Convert.ToInt32(summaryCompanyContract.SelectedItem.ToString());
                System.Data.DataTable data = contract.get_max_amount_hospital_ambulance(compid, contrid, class_code);
                maxamounttxtst.Text = data.Rows[0].ItemArray[0].ToString();
                hospitaltxtst.Text = data.Rows[0].ItemArray[1].ToString();
                ambulancetxtst.Text = data.Rows[0].ItemArray[2].ToString();
                dservcodetxtst.Items.Clear();
                System.Data.DataTable servCodes = contract.get_serv_code(compid, contrid, class_code);
                for (int i = 0; i < servCodes.Rows.Count; i++)
                {
                    dservcodetxtst.Items.Add(servCodes.Rows[i].ItemArray[0].ToString() + " " + servCodes.Rows[i].ItemArray[1].ToString());
                }
                //int max_amt = contract.get_max_amount(compid, contrid, classcode);
                //maxamounttxt.Text = max_amt.ToString();
            }
            catch { }
        }

        private void dservcodetxt_SelectionChangedst(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string Codeselected = ClassCodeCombost.SelectedItem.ToString();
                string[] arrCode = Codeselected.Split(' ');
                string class_code = arrCode[0].ToString();
                int compid = 0;
                if (UserType == "hr")
                {
                    compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                }
                else
                {
                    compid = Convert.ToInt32(CompanyComboSummary.Text.ToString());
                }
                int contrid = Convert.ToInt32(summaryCompanyContract.SelectedItem.ToString());
                string selected = dservcodetxtst.SelectedItem.ToString();
                string[] arr = selected.Split(' ');
                string codetxt = arr[0].ToString();
                System.Data.DataTable serServ = contract.get_ser_serv(codetxt, compid, contrid, class_code);
                serServzCombo.Items.Clear();
                for (int i = 0; i < serServ.Rows.Count; i++)
                {
                    serServzCombo.Items.Add(serServ.Rows[i].ItemArray[0].ToString() + " " + serServ.Rows[i].ItemArray[1].ToString());
                }
                string ceilingamt = "";
                string ceilingpert = "";
                string indlist = "";
                string carramt = "";
                string refund = "";
                string dservName = contract.get_d_serv_name(codetxt);
                System.Data.DataTable serviceDetails = contract.get_service_details(compid, contrid, class_code, codetxt);
                System.Data.DataTable data = new System.Data.DataTable();
                data.Columns.Add("كود الخدمة", typeof(string));
                data.Columns.Add("اسم الخدمة", typeof(string));
                data.Columns.Add("الحد الاقصى للتغطية", typeof(string));
                data.Columns.Add("نسبة التغطية", typeof(string));
                data.Columns.Add("قائمة اسعار الاستردادات", typeof(string));
                data.Columns.Add("قيمة التحمل", typeof(string));
                data.Columns.Add("الاسترداد ؟", typeof(string));
                for (int i = 0; i < serviceDetails.Rows.Count; i++)
                {
                    ceilingamt = serviceDetails.Rows[i].ItemArray[1].ToString();
                    ceilingpert = serviceDetails.Rows[i].ItemArray[2].ToString();
                    indlist = serviceDetails.Rows[i].ItemArray[3].ToString();
                    carramt = serviceDetails.Rows[i].ItemArray[4].ToString();
                    refund = serviceDetails.Rows[i].ItemArray[5].ToString();
                }
                data.Rows.Add(codetxt, dservName, ceilingamt, ceilingpert, indlist, carramt, refund);
                servDetailsGridst.ItemsSource = data.DefaultView;
            }
            catch { }
        }

        private void messConfGrid_Loadedz(object sender, RoutedEventArgs e)
        {

        }

        private void messtabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (messConfTab.IsSelected==true)
            //{
            //    MessengerConfirmation confirm = new MessengerConfirmation();
            //    confirm.ShowDialog();
            //}
            //if (MovingMessanger.IsSelected==true)
            //{
            //    MovingFrm movF = new MovingFrm();
            //    movF.ShowDialog();
            //}
        }


        private void approvalGridz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void serServCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string selected = ClassCodeCombo.SelectedItem.ToString();
                string[] arr = selected.Split(' ');
                string classcode = arr[0].ToString();

                int compid = Convert.ToInt32(CompanyComboBoxMain.Text.ToString());
                int contrid = Convert.ToInt32(summaryMainContractCompany.SelectedItem.ToString());

                string dservselected = dservcodetxt.SelectedItem.ToString();
                string[] dservarr = dservselected.Split(' ');
                string codetxt = dservarr[0].ToString();

                string servselected = serServCombo.SelectedItem.ToString();
                string[] servarr = servselected.Split(' ');
                string servcode = servarr[0].ToString();

                string dservName = contract.get_d_serv_name(codetxt);
                string serServName = contract.get_ser_servname(servcode);
                string ceilingamt1 = ""; string ceilingpert1 = ""; string indlist1 = ""; string carramt1 = ""; string refund1 = "";

                System.Data.DataTable data = contract.get_ser_serv_details(servcode, compid, contrid, classcode);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    ceilingamt1 = data.Rows[i].ItemArray[1].ToString();
                    ceilingpert1 = data.Rows[i].ItemArray[2].ToString();
                    indlist1 = data.Rows[i].ItemArray[3].ToString();
                    carramt1 = data.Rows[i].ItemArray[4].ToString();
                    refund1 = data.Rows[i].ItemArray[5].ToString();
                }
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("كود الخدمة", typeof(string));
                dt.Columns.Add("اسم الخدمة", typeof(string));
                dt.Columns.Add("الحد الاقصى للتغطية", typeof(string));
                dt.Columns.Add("نسبة التغطية", typeof(string));
                dt.Columns.Add("قائمة اسعار الاستردادات", typeof(string));
                dt.Columns.Add("قيمة التحمل", typeof(string));
                dt.Columns.Add("الاسترداد ؟", typeof(string));
                dt.Rows.Add(servcode, serServName, ceilingamt1, ceilingpert1, indlist1, carramt1, refund1);
                servGrid.ItemsSource = dt.DefaultView;
            }
            catch { }
        }

        private void serServzCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {

                string selected = ClassCodeCombost.SelectedItem.ToString();
                string[] arr = selected.Split(' ');
                string classcode = arr[0].ToString();
                int compid = 0;
                if (UserType == "hr")
                {
                    compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                }
                else
                {
                    compid = Convert.ToInt32(CompanyComboSummary.Text.ToString());
                }
                int contrid = Convert.ToInt32(summaryCompanyContract.SelectedItem.ToString());

                string dservselected = dservcodetxtst.SelectedItem.ToString();
                string[] dservarr = dservselected.Split(' ');
                string codetxt = dservarr[0].ToString();

                string servselected = serServzCombo.SelectedItem.ToString();
                string[] servarr = servselected.Split(' ');
                string servcode = servarr[0].ToString();
                string dservName = contract.get_d_serv_name(codetxt);
                string serServName = contract.get_ser_servname(servcode);
                string ceilingamt1 = ""; string ceilingpert1 = ""; string indlist1 = ""; string carramt1 = ""; string refund1 = "";

                System.Data.DataTable data = contract.get_ser_serv_details(servcode, compid, contrid, classcode);
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    ceilingamt1 = data.Rows[i].ItemArray[1].ToString();
                    ceilingpert1 = data.Rows[i].ItemArray[2].ToString();
                    indlist1 = data.Rows[i].ItemArray[3].ToString();
                    carramt1 = data.Rows[i].ItemArray[4].ToString();
                    refund1 = data.Rows[i].ItemArray[5].ToString();
                }
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("كود الخدمة", typeof(string));
                dt.Columns.Add("اسم الخدمة", typeof(string));
                dt.Columns.Add("الحد الاقصى للتغطية", typeof(string));
                dt.Columns.Add("نسبة التغطية", typeof(string));
                dt.Columns.Add("قائمة اسعار الاستردادات", typeof(string));
                dt.Columns.Add("قيمة التحمل", typeof(string));
                dt.Columns.Add("الاسترداد ؟", typeof(string));
                dt.Rows.Add(servcode, serServName, ceilingamt1, ceilingpert1, indlist1, carramt1, refund1);
                serservgrid.ItemsSource = dt.DefaultView;
            }
            catch { }
        }


        private void printTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReceivingFrm.IsSelected == true)
            {
                if (printTab.Visibility == Visibility.Visible && listBox1Recieving.ItemsSource == null)
                {
                    recevingGroup.Visibility = Visibility.Hidden;
                    dtpDateReceiving.Visibility = Visibility.Hidden;
                    ReceivingGrid.Visibility = Visibility.Hidden;
                    btnSearchDateReceving.Visibility = Visibility.Hidden;


                    try
                    {
                        List<PrintingData> Companylist = printserv.SelectAllCompaniesForReceivingCards();
                        listBox1Recieving.ItemsSource = Companylist;
                        listBox1Recieving.DisplayMemberPath = "CompanyName";
                        listBox1Recieving.SelectedValuePath = "CompID";
                        string CompCode = listBox1Recieving.SelectedValue.ToString();

                        List<PrintingData> Cards = printserv.SelectAllEmployees_For_ReceivingCards(CompCode);
                        ReceivingGrid.ItemsSource = Cards;

                        #region HideSomeColumns
                        ReceivingGrid.Columns[1].Width = 300;
                        ReceivingGrid.Columns[3].Width = 300;
                        ReceivingGrid.Columns[2].Visibility = Visibility.Visible;
                        //----Hide some columns--------------
                        #region HideSomeColumns
                        ReceivingGrid.Columns[1].Visibility = Visibility.Visible;
                        ReceivingGrid.Columns[13].Visibility = Visibility.Hidden;
                        ReceivingGrid.Columns[0].Visibility = Visibility.Hidden;
                        ReceivingGrid.Columns[10].Visibility = Visibility.Hidden;
                        ReceivingGrid.Columns[11].Visibility = Visibility.Hidden;
                        ReceivingGrid.Columns[12].Visibility = Visibility.Hidden;
                        ReceivingGrid.Columns[6].Visibility = Visibility.Hidden;
                        ReceivingGrid.Columns[7].Visibility = Visibility.Hidden;
                        ReceivingGrid.Columns[8].Visibility = Visibility.Hidden;

                        ReceivingGrid.Columns[9].Visibility = Visibility.Hidden;
                        #endregion

                        #endregion
                    }
                    catch { }
                }
            }
        }

        private void cbxstateCust_Copy_DropDownClosed(object sender, EventArgs e)
        {

        }

        public string provider_idtFollow { get; set; }

        public static int contract_provider_count;
        public static int contract_company;
        public static int company_tmp, provider_tmp, vcompany_tmp, vmedcard_tmp;
        public bool test = false;
        public static int contract_provider_difference, contract_company_differece, vcompany_difference, vcard_difference;



        public static int vcompany_count;
        public static int v_medcardd_count;

        //abdo
        public static int asly, copy, difference;
        string AbdoUserType;
        System.Data.DataTable dtbeforreplay, dtreplay;
        DataRow replaydata;


        // timer start when page loads
        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            dis.Tick += new EventHandler(dis_tick);
            dis.Interval = new TimeSpan(0, 0, 3);
            dis.Start();
        }

        //calculate get database record count , copy it , calculate difference between copy and original value
        public void dis_tick(object sender, EventArgs e)
        {
            //company_tmp = contract.get_count_company();
            //provider_tmp = contract.get_count_provider();
            //vcompany_tmp = contract.count_vcompanies();
            //vmedcard_tmp = Medicie.count_vcard();

            //abdo
            //System.Data.DataTable she = db.RunReader("Select Count(CODE) FROM COMPANIES_HR WHERE REPLAYED='N' ORDER BY COMP_DATE desc").Result;
            //try
            //{
            //    asly = Convert.ToInt32(she.Rows[0][0].ToString());
            //}
            //catch { }
            //dtreplay = db.RunReader("Select * FROM COMPANIES_HR ORDER BY CODE  ").Result;



            ////end abdo
            //if (test == false)
            //{
            //    //abdo
            //    copy = asly;
            //    dtbeforreplay = dtreplay;

            //    //end abdo
            //    //contract_company = company_tmp;
            //    //contract_provider_count = provider_tmp;
            //    //vcompany_count = vcompany_tmp;
            //    //v_medcardd_count = vmedcard_tmp;
            //    test = true;
            //}

            ////abdo
            //if (AbdoUserType == "DMS Member")
            //{
            //    if (asly > copy)
            //    {
            //        difference = asly - copy;
            //        System.Media.SystemSounds.Asterisk.Play();
            //        System.Windows.Forms.NotifyIcon abdodmsmember = new System.Windows.Forms.NotifyIcon();
            //        abdodmsmember.Icon = new System.Drawing.Icon(Environment.CurrentDirectory + "/Notifcation.ico");
            //        abdodmsmember.Visible = true;
            //        abdodmsmember.ShowBalloonTip(5000, "Notification", "تم اضافة شكوى", System.Windows.Forms.ToolTipIcon.Info);
            //        copy = asly;
            //        abdodmsmember.BalloonTipClicked += abdodmsmember_y_BalloonTipClicked;
            //    }
            //}
            //if (AbdoUserType == "hr")
            //{
            //    if (dtbeforreplay != dtreplay)
            //    {

            //        for (int i = 0; i < dtbeforreplay.Rows.Count; i++)
            //        {

            //            if (dtreplay.Rows[i][11].ToString() != dtbeforreplay.Rows[i][11].ToString())
            //            {
            //                //  MessageBox.Show("dtreplay.Rows[i][11] = " + dtreplay.Rows[i][11].ToString() + "   dtbeforreplay.Rows[i][11]= " + dtbeforreplay.Rows[i][11].ToString());
            //                replaydata = dtreplay.Rows[i];
            //                System.Media.SystemSounds.Asterisk.Play();
            //                System.Windows.Forms.NotifyIcon notifyContractCompany = new System.Windows.Forms.NotifyIcon();
            //                notifyContractCompany.Icon = new System.Drawing.Icon(Environment.CurrentDirectory + "/Notifcation.ico");
            //                notifyContractCompany.Visible = true;
            //                notifyContractCompany.ShowBalloonTip(5000, "Notification", "تم رد على شكوى رقم  " + dtreplay.Rows[i][0].ToString(), System.Windows.Forms.ToolTipIcon.Info);
            //                dtbeforreplay = dtreplay;
            //                notifyContractCompany.BalloonTipClicked += replayed_BalloonTipClicked;
            //            }

            //        }

            //    }
            //}

            //end abdo
            //if (vmedcard_tmp > v_medcardd_count)
            //{
            //    vcard_difference = Math.Abs(vcompany_tmp - v_medcardd_count);
            //    System.Media.SystemSounds.Asterisk.Play();
            //    System.Windows.Forms.NotifyIcon medicineNotify = new System.Windows.Forms.NotifyIcon();
            //    medicineNotify.Icon = new System.Drawing.Icon(Environment.CurrentDirectory + "/Notifcation.ico");
            //    medicineNotify.Visible = true;
            //    medicineNotify.ShowBalloonTip(5000, "علاج جديد", "تم اضافة علاج شهري", System.Windows.Forms.ToolTipIcon.Info);
            //    v_medcardd_count = vmedcard_tmp;
            //    medicineNotify.BalloonTipClicked += medicineNotify_BalloonTipClicked;

            //}
            ////if (vcompany_tmp > vcompany_count)
            //{
            //    vcompany_difference = vcompany_tmp - vcompany_count;
            //    System.Media.SystemSounds.Asterisk.Play();
            //    System.Windows.Forms.NotifyIcon notifyVCompany = new System.Windows.Forms.NotifyIcon();
            //    notifyVCompany.Icon = new System.Drawing.Icon(Environment.CurrentDirectory + "/Notifcation.ico");
            //    notifyVCompany.Visible = true;
            //    notifyVCompany.ShowBalloonTip(5000, "Notification", "تم اضافة شركة", System.Windows.Forms.ToolTipIcon.Info);
            //    vcompany_count = vcompany_tmp;
            //    notifyVCompany.BalloonTipClicked += notifyVCompany_BalloonTipClicked;
            //}
            ////    if (company_tmp>contract_company )
            //    {
            //        contract_company_differece = company_tmp - contract_company;
            //        System.Media.SystemSounds.Asterisk.Play();
            //        System.Windows.Forms.NotifyIcon notifyContractCompany = new System.Windows.Forms.NotifyIcon();
            //        notifyContractCompany.Icon = new System.Drawing.Icon(Environment.CurrentDirectory+"/Notifcation.ico");
            //        notifyContractCompany.Visible = true;
            //        notifyContractCompany.ShowBalloonTip(5000, "Notification", "تم اضافة عقد شركة", System.Windows.Forms.ToolTipIcon.Info);
            //        contract_company = company_tmp;
            //        notifyContractCompany.BalloonTipClicked +=notifyContractCompany_BalloonTipClicked;
            //    }
            //    else if (provider_tmp > contract_provider_count)
            //    {
            //        contract_provider_difference = provider_tmp - contract_provider_count;
            //        System.Media.SystemSounds.Asterisk.Play();
            //        System.Windows.Forms.NotifyIcon notifyContract = new System.Windows.Forms.NotifyIcon();
            //        notifyContract.Icon = new System.Drawing.Icon(Environment.CurrentDirectory + "/Notifcation.ico");
            //        notifyContract.Visible = true;
            //        notifyContract.ShowBalloonTip(5000, "Notification", "تم اضافة عقد مقدم خدمة", System.Windows.Forms.ToolTipIcon.Info);
            //        contract_provider_count = provider_tmp;
            //        notifyContract.BalloonTipClicked += notifyContract_Click;
            //    }

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //  if(User.Noti)
            if (User.Type == "DMS Member" && User.Noti.HrRequests == "Y" && User.Manegar == "y" && ON.IsChecked == true)
            {
                System.Data.DataTable notitable = db.RunReader("select NOTI_CODE ,NOTI_SERV,CREATED_BY  from app.noti where ACTION = 'N' and NOTI_TYP = '3'").Result;
                if (notitable.Rows.Count > 0)
                {
                    for (int i = 0; i < notitable.Rows.Count; i++)
                    {
                        System.Media.SystemSounds.Asterisk.Play();
                        System.Windows.Forms.NotifyIcon NewRequest = new System.Windows.Forms.NotifyIcon();
                        NewRequest.Icon = new System.Drawing.Icon(Environment.CurrentDirectory + "/Notifcation.ico");
                        NewRequest.Visible = true;
                        NewRequest.ShowBalloonTip(5000, "HR Request", "تم ارسال طلب جديد", System.Windows.Forms.ToolTipIcon.None);
                        codeofthisrequest = notitable.Rows[i][0].ToString();
                        NewRequest.MouseClick += NewRequest_y_BalloonTipClicked;
                        db.RunNonQuery("update app.NOTI set ACTION ='A' where NOTI_CODE='" + codeofthisrequest + "'");

                    }
                }

            }
            else if (User.Type == "DMS Member" && User.Noti.HrRequests == "Y" && User.Manegar == "n" && ON.IsChecked == true)
            {
                System.Data.DataTable notitable = db.RunReader("select NOTI_CODE ,NOTI_SERV,CREATED_BY  from app.noti where ACTION = 'N' and NOTI_TYP = '1'").Result;
                if (notitable.Rows.Count > 0)
                {
                    for (int i = 0; i < notitable.Rows.Count; i++)
                    {
                        System.Media.SystemSounds.Asterisk.Play();
                        System.Windows.Forms.NotifyIcon NewRequest = new System.Windows.Forms.NotifyIcon();
                        NewRequest.Icon = new System.Drawing.Icon(Environment.CurrentDirectory + "/Notifcation.ico");
                        NewRequest.Visible = true;
                        NewRequest.ShowBalloonTip(5000, "HR Request", "تم ارسال طلب جديد", System.Windows.Forms.ToolTipIcon.None);
                        codeofthisrequest = notitable.Rows[i][0].ToString();
                        NewRequest.BalloonTipClicked += NewRequest_y_BalloonTipClicked;
                        db.RunNonQuery("update app.NOTI set ACTION ='A' where NOTI_CODE='" + codeofthisrequest + "'");

                    }
                }
            }
            else if (User.Type == "hr" && ON.IsChecked == true)
            {
                System.Data.DataTable notitable = db.RunReader("select NOTI_CODE ,NOTI_SERV  from app.noti where ACTION = 'N' and NOTI_TYP = '2' and CREATED_BY = '" + User.Name + "'").Result;
                if (notitable.Rows.Count > 0)
                {
                    for (int i = 0; i < notitable.Rows.Count; i++)
                    {
                        System.Media.SystemSounds.Asterisk.Play();
                        System.Windows.Forms.NotifyIcon NewRequest = new System.Windows.Forms.NotifyIcon();
                        NewRequest.Icon = new System.Drawing.Icon(Environment.CurrentDirectory + "/Notifcation.ico");
                        NewRequest.Visible = true;
                        NewRequest.ShowBalloonTip(5000, "تم الرد على طلب", notitable.Rows[i][0].ToString(), System.Windows.Forms.ToolTipIcon.Warning);
                        codeofthisrequest = notitable.Rows[i][0].ToString();
                        NewRequest.BalloonTipClicked += REpRequest_y_BalloonTipClicked;
                        db.RunNonQuery("update app.NOTI set ACTION ='A' where NOTI_CODE='" + codeofthisrequest + "'");

                    }
                }
            }
        }
        string codeofthisrequest;
        private void REpRequest_y_BalloonTipClicked(object sender, System.EventArgs e)
        {
            dis.Stop();
            //    MessageBox.Show("aaaaaaaaaaaaaaa");
            db.RunNonQuery("update app.NOTI set ACTION ='C' where NOTI_CODE='" + codeofthisrequest + "'");
            dis.Start();
        }
        private void NewRequest_y_BalloonTipClicked(object sender, System.EventArgs e)
        {
            dis.Stop();
            System.Data.DataTable temmsg = db.RunReader("update app.NOTI set ACTION ='C' where NOTI_CODE='" + codeofthisrequest + "'").Result;
            MessageBox.Show("done");
            db.RunNonQuery("update app.NOTI set ACTION ='C' where NOTI_CODE='" + codeofthisrequest + "'");
            dis.Start();

        }

        private void abdodmsmember_y_BalloonTipClicked(object sender, System.EventArgs e)
        {
            dis.Stop();
            //CompanyContractWindow concw = new CompanyContractWindow(contract_company_differece);
            //concw.ShowDialog();
            System.Data.DataTable she = db.RunReader("Select * FROM COMPANIES_HR WHERE REPLAYED='N' ORDER BY CODE desc").Result;




            string CODE = she.Rows[0][0].ToString();
            string DEPARTMENT = she.Rows[0][1].ToString();
            string EMPLOYEE = she.Rows[0][2].ToString();
            string COMP_DATE = she.Rows[0][3].ToString();
            string REASON = she.Rows[0][4].ToString();
            string DESCRIPTION = she.Rows[0][5].ToString();
            string CREATED_BY = she.Rows[0][6].ToString();

            DMSComplaintsMSG ss = new DMSComplaintsMSG(CODE, DEPARTMENT, EMPLOYEE, COMP_DATE, REASON, DESCRIPTION, CREATED_BY);
            ss.Show();
            dis.Start();

        }
        private void replayed_BalloonTipClicked(object sender, System.EventArgs e)
        {
            dis.Stop();
            // replaydata
            HRComplaintMSG ss = new HRComplaintMSG(replaydata[0].ToString());
            ss.Show();
            dis.Start();

        }


        DispatcherTimer dis = new DispatcherTimer();

        private void medicineNotify_BalloonTipClicked(object sender, System.EventArgs e)
        {
            dis.Stop();
            newMedicineWindow newmed = new newMedicineWindow(vcard_difference);
            newmed.ShowDialog();
            dis.Start();
        }

        private void tmcompanyprint_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string companyCode = tmcompanyprint.Text.ToString();
                List<PrintingData> list = printserv.SelectAllEmployees_For_ReceivingCards(companyCode);
                ReceivingGrid.ItemsSource = list;
                if (list != null)
                {
                    ReceivingGrid.Columns[7].Visibility = Visibility.Visible;
                    receiveItemconttxt.Content = list.Count.ToString();
                    //ReceivingGrid.Columns["EmpName"].Width = 200;
                    //ReceivingGrid.Columns["RecievedName"].Width = 200;
                    //----Hide some columns--------------
                    #region HideSomeColumns
                    ReceivingGrid.Columns[8].Visibility = Visibility.Visible;
                    ReceivingGrid.Columns[16].Visibility = Visibility.Visible;
                    // ReceivingGrid.Columns["ReceivedState"].DefaultCellStyle.NullValue = "N";
                    ReceivingGrid.Columns[1].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[0].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[14].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[10].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[11].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[12].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[3].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[4].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[5].Visibility = Visibility.Hidden;
                    ReceivingGrid.Columns[13].Visibility = Visibility.Hidden;
                    #endregion
                }
                else
                {
                    //---------- from pc--------
                    MessageBox.Show("لا توجد نتائج");
                    receiveItemconttxt.Content = "0";
                    ReceivingGrid.Columns[15].Visibility = Visibility.Visible;
                }
                ReceivingGrid.Columns[17].Visibility = Visibility.Visible;
            }
            catch
            {
                ReceivingGrid.ItemsSource = null;
                receiveItemconttxt.Content = "0";
            }


        }

        private void tmcompanyprint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string tmcompid;
                ReceivingGrid.ItemsSource = null;
                receiveItemconttxt.Content = "";
                try
                {
                    tmcompid = dsprint.Tables[0].Rows[tmcompanyprint.SelectedIndex][0].ToString();
                    dsprint = db.RunReaderds("select * from PRINTINGTB where DELIVERSTATE= 'N' and COMPID=" + tmcompid);
                    if (dsprint.Tables[0].Rows.Count != 0)
                    {
                        ReceivingGrid.ItemsSource = dsprint.Tables[0].DefaultView;
                        receiveItemconttxt.Content = dsprint.Tables[0].Rows.Count.ToString();
                        tmcompanyprint.Text = "";

                    }
                    else
                        MessageBox.Show("لا يوجد بيانات");

                    try
                    {
                        dsprint = db.RunReaderds(" select distinct C_COMP_ID , C_ANAME from V_COMPANIES ORDER BY C_COMP_ID ");
                        tmcompanyprint.ItemsSource = dsprint.Tables[0].DefaultView;
                    }
                    catch { }
                }
                catch
                { }
            }

        }

        private void tmnewsearch_Click(object sender, RoutedEventArgs e)
        {
            empCompCombo.Text = "";
            basicdataDemptCombo.Text = "";
            basicdataEmpCombo.Text = "";
            empnametxt.Clear();
            newDeptCombo.Text = "";
            empcodetxt.Clear();
            emppasstxt.Clear();
            empyesrb.IsChecked = false;
            empnorb.IsChecked = false;
            hrtyperb.IsChecked = false;
            dmstyperb.IsChecked = false;
            activeChk.IsChecked = false;
            custChk.IsChecked = false;
            bscDtaChk.IsChecked = false;
            reportChk.IsChecked = false;
            networkChk.IsChecked = false;
            notebookChk.IsChecked = false;
            onlineChk.IsChecked = false;
            contractChk.IsChecked = false;
            complainChk.IsChecked = false;
            messangerChk.IsChecked = false;
            indemnityChk.IsChecked = false;
            printChk.IsChecked = false;
            approvalChk.IsChecked = false;
            chequesChk.IsChecked = false;
            storeChk.IsChecked = false;
            medicineChk.IsChecked = false;
            complain_dmsChk.IsChecked = false;
            hr_reqChk.IsChecked = false;
            medical_manageChk.IsChecked = false;
            cbrequets.IsChecked = false;
            notiHrRequest1.IsChecked = false;
            rev_chk_Copy.IsChecked = false;
        }

        private void networkcardcomzbo_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void abdocbxmonthzzCompany_DropDownClosed(object sender, EventArgs e)
        {
            //{try
            //    {
            //        networkcardcombo.ItemsSource = (User.Employee_in_Company(abdocbxmonthzzCompany.Text)).DefaultView;
            //    }
            //    catch { }
        }

        private void cbxcompcomp_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                int compId = Convert.ToInt32(cbxcompcomp.Text);
                string compName = store.get_indem_company_name(compId);
                lblCompName.Content = compName;
                // messReqbranchCombo.Items.Clear();
                List<MessengerRequestData> Branches = req.SelectAllCompanies_Branches(compId.ToString());
                //if (Branches == null)
                //{
                //    MessageBox.Show("لا توجد فروع ");
                //}
                messReqaddrtxt.Text = store.GetCompanyAddress(compId);
                messReqbranchCombo.ItemsSource = Branches;
                messReqbranchCombo.DisplayMemberPath = "Branch";
                messReqbranchCombo.SelectedValuePath = "Branch";
            }
            catch { }
        }



        //lood
        private void ScrollViewer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (chattab.IsSelected == true)
            {
                myTimer.Tick += new EventHandler(timerjoba);
                myTimer.Interval = new TimeSpan(0, 0, 10);
                myTimer.Start();


                myTimer.Start();
                if (User.Type == "DMS Member")
                {
                    System.Data.DataTable ine = db.RunReader("select DEPT_NAME from AGENT_DEPARTMENT").Result;

                    com_deprt.ItemsSource = ine.DefaultView;
                }
                else if (User.Type == "hr")
                {
                    sarsher();
                }




            }

            else if (PROBLEMdms.IsSelected == true && dmsprobdg.ItemsSource == null)
            {
                if (User.Department == "customerservices" || User.Department == "After Sales")// and DEPARTMENT='"+User.Department+"'
                    dmsproblem = db.RunReader("Select CODE,DEPARTMENT,EMPLOYEE,COMP_DATE,COMP_TIME,REASON,DESCRIPTION,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE FROM COMPANIES_HR WHERE REPLAYED='N' ORDER BY COMP_DATE desc").Result;
                else
                    dmsproblem = db.RunReader("Select CODE,DEPARTMENT,EMPLOYEE,COMP_DATE,COMP_TIME,REASON,DESCRIPTION,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE FROM COMPANIES_HR WHERE REPLAYED='N' and DEPARTMENT='" + User.Department + "' ORDER BY COMP_DATE desc").Result;
                dmsprobdg.ItemsSource = dmsproblem.DefaultView;
            }
            else
            {
                if (ApprovalsTab.IsSelected == true && approvalcardcombo.ItemsSource == null)
                {
                    if (UserType == "hr")
                    {
                        int compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                        fill_card(ApprovaltxtCardNum, compid);
                    }
                    else
                    {
                        fill_comp(approvalCompCombo);
                    }
                }
                else
                {
                    if (month22.IsSelected == true && abdocbxmonth22Company.ItemsSource == null)
                    {
                        jj22startmonth22();
                    }
                    else
                    {



                        if (logout.IsSelected == true)
                        {
                            User.page3English.Visibility = Visibility.Collapsed;
                            this.NavigationService.Navigate(User.page1);


                        }
                        else
                        {
                            if (online.IsSelected == true)
                            {
                                if (User.Type == "hr")
                                    System.Diagnostics.Process.Start("IExplore.exe", "http://171.0.1.96 :9001/forms/frmservlet?config=jpi&form=C:%5CFUTSHER%5CMS%5CIT%5CIT_AR%5CLogin_dms.fmx&userid=&otherparams=useSDI=yes&lookAndFeel=generic&colorScheme=blue");

                                else
                                    System.Diagnostics.Process.Start("IExplore.exe", "http://171.0.1.96 :9001/forms/frmservlet?config=jpi&form=C:%5CFUTSHER%5CMS%5CIT%5CIT_AR%5CLogin_dms.fmx&userid=&otherparams=useSDI=yes&lookAndFeel=generic&colorScheme=blue");

                                online.IsSelected = false;
                                NameTab.IsSelected = true;

                            }
                            else
                            {
                                if (MessTab.IsSelected == true)
                                {
                                    try
                                    {
                                        //-------Fill  DataGridView--------------
                                        List<MessangerData> list = mes.SelectAllMessengers();
                                        messGrid.ItemsSource = list;
                                        messReqDeptxt.Text = agent.get_dept(NameTab.Header.ToString());
                                        #region Selectmax_MessId
                                        string max2 = mes.SelectMaxMessId();
                                        if (max2 == "")
                                        {
                                            messangerCodetxt.Text = "1";
                                        }
                                        else
                                        {
                                            int maxx2 = int.Parse(max2) + 1;
                                            messangerCodetxt.Text = maxx2.ToString();
                                        }
                                        #endregion
                                        //-------clear selection mode at first time when load 
                                        try
                                        {
                                            messGrid.SelectedCells.Clear();
                                        }
                                        catch { }
                                        messangeraddrtxt.Document.Blocks.Clear();
                                        messangerIDtxt.Text = "";
                                        messangerNametxt.Text = ""; messangerUserNametxt.Text = ""; messangerPasstxt.Text = "";


                                        ////////////////////////// request
                                        List<MessengerRequestData> Companylist = req.SelectAllCompanies();
                                       
                                        cbxcompcomp.ItemsSource = Companylist;
                                        //messReqCompanyList.DisplayMemberPath = "CompanyName";
                                        //messReqCompanyList.SelectedValuePath = "CompanyCode";
                                        //messReqDeptxt.Text = agent.get_dept(NameTab.Header.ToString());
                                        #region Selectmax_MessRequestId
                                        string req_max2 = req.SelectMaxReqMessId();
                                        int req_maxx2 = 0;
                                        if (req_max2 == "")
                                        {
                                            messReqCodetxt.Text = "1";
                                        }

                                        else
                                        {
                                            req_maxx2 = int.Parse(req_max2) + 1;
                                            messReqCodetxt.Text = req_max2.ToString();
                                        }
                                        #endregion

                                        List<MessengerRequestData> listReq = req.SelectAllMessengersRequests();
                                        messReqGrid.ItemsSource = listReq;
                                        //dataGridView1.Columns["HoldDate"].DisplayIndex = 12;
                                        // -------clear selection mode at first time when load ---------------
                                        try
                                        {
                                            messReqGrid.SelectedCells.Clear();
                                            messReqCodetxt.Text = req_maxx2.ToString();
                                        }
                                        catch
                                        {
                                            // txtReqCode.Text = "1";
                                        }
                                        //-----------To Clear TextBoxes-------------------//
                                        #region clearAll
                                        //messReqComptxt.Text = "";
                                        messReqContactPersontxt.Text = "";
                                        // messReqDeptxt.Text = "";
                                        cbxcompcomp.Text = "";
                                        messReqothertxt.Document.Blocks.Clear();
                                        chkReadyCardsReson.IsChecked = false;
                                        chkReadyCheek.IsChecked = false;
                                        chkDeliverPaper.IsChecked = false;
                                        chkOtherResons.IsChecked = false;
                                        #endregion
                                        try
                                        {
                                            //dataGridView1.Columns[3].Visible = false;
                                            //dataGridView1.Columns[4].Visible = false;
                                            //dataGridView1.Columns["Done"].Visible = false;
                                        }
                                        catch { }
                                        List<MessengerRequestData> listCity = req.SelectAllGovernerators();
                                        messReqCityCombo.ItemsSource = listCity;
                                        messReqCityCombo.DisplayMemberPath = "Governorate_Name";
                                        messReqCityCombo.SelectedValuePath = "Governorate_Code";
                                    }
                                    catch { }
                                }
                            }
                        }

                    }
                }
            }



        }

        private void networkcardcombo_Copy_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                networkcardcombo.ItemsSource = User.Employee_in_Company(networkcardcombo_Copy.Text).DefaultView;
            }
            catch { }
        }

  
        private void txtshowdataSolvedComplaintCust_Copy_Click(object sender, RoutedEventArgs e)
        {
            txtSearchSolvedComplaintCust.Text = "";
            dataGridSolvedComplaintCust.ItemsSource = null;

        }

        private void btnviowcomplainstubjectCust_Copy_Click(object sender, RoutedEventArgs e)
        {
            textsearchComplaintsubjectCust.Text = "";
            dgviewcomplaintsubjectCust.ItemsSource = null;
        }



        private void managernewSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                managerSrchtxt.Text = "";
                System.Data.DataTable data = store.get_request();
                managerSrchReqGrid.ItemsSource = data.DefaultView;
                managerSrchReqGrid.Columns[0].Header = "رقم الطلب";
                managerSrchReqGrid.Columns[1].Header = "اسم القطعة";
                managerSrchReqGrid.Columns[2].Header = "الفئة";
                managerSrchReqGrid.Columns[3].Header = "اسم الموظف";
                managerSrchReqGrid.Columns[4].Header = "القسم";
                managerSrchReqGrid.Columns[5].Header = "الكمية";
                managerSrchReqGrid.Columns[6].Header = "تم الموافقة ؟";
                managerSrchReqGrid.Columns[7].Header = "مكان صورة الطلب";
            }
            catch { }
        }


        private void newPrintSecondBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintSCCodetxt.Text = "";
            txtEmpId.Text = "";
            txtEmpName.Text = "";
            txtCompanyNamePrintSC.Text = "";
            cmbCardType.Text = "";
            chkReOpen.IsChecked = false;
            chkChangeName.IsChecked = false;
            chkLost.IsChecked = false;
            chkChangePic.IsChecked = false;
            chkOther.IsChecked = false;
            txtResons.Document.Blocks.Clear();
        }


        private void saveItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (categorycombo.SelectedItem == null || categorycombo.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("من فضلك اختر فئة");
                }
                else
                {
                    string category = categorycombo.SelectedItem.ToString();
                    string item = itemname.Text.ToString();
                    Int64 code = store.get_categ_code(category);
                    int itemcode = store.get_item_serial(code);
                    string itemCodeStr = itemcode.ToString();
                    string codeStr = code.ToString();
                    string itemCodeNew = codeStr + itemCodeStr;
                    Int64 itemCodeInt = Convert.ToInt32(itemCodeNew);
                    string amount = amttxt.Text.ToString();
                    string price = prictxt.Text.ToString();
                    string lim = limtxt.Text.ToString();
                    store.add_item(itemCodeInt, item, code, amount, lim, price);
                    MessageBox.Show("تم الحفظ بنجاح");
                    itemGrid.Items.Refresh();
                    itemGrid.ItemsSource = store.get_items().DefaultView;
                    itemGrid.Columns[0].Header = "كود القطعة";
                    itemGrid.Columns[1].Header = "اسم القطعة";
                    itemGrid.Columns[2].Header = "الكمية";
                    itemGrid.Columns[3].Header = "حد الطلب";
                    itemGrid.Columns[4].Header = "سعر القطعة";
                    itemGrid.Columns[5].Header = "الفئة";
                    //categoryCombo.Items.Refresh();
                    storenewItemCounttxt.Content = itemGrid.Items.Count - 1;
                }
            }
            catch { }
        }


        private void newItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                storenewItemCounttxt.Content = "";
                categorycombo.Text = "";
                itemname.Text = "";
                amttxt.Text = "";
                prictxt.Text = "";
                limtxt.Text = "";
                itemGrid.ItemsSource = null;
            }
            catch { }
        }


        private void newReturnItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                returnItem_GridItemCounttxt.Content = "";
                empCombo.Text = "";
                deptCombo.Text = "";
                comboItems.Text = "";
                categCombo.Text = "";
                stateCombo.Text = "";
                pricetxt.Text = "";
                amount_txt.Text = "";
                returnItem_Grid.ItemsSource = null;
                reasontxt.Document.Blocks.Clear();
            }
            catch { }
        }


        private void exportItemNewBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                exportItemCounttxt.Content = "";
                exportEmpCombo.Text = "";
                exportDeptCombo.Text = "";
                exportItemCombo.Text = "";
                exportcategCombo.Text = "";
                export_amounttxt.Text = "";
                exportTypeCombo.Text = "";
                exportGrid.ItemsSource = null;
            }
            catch { }
        }


        private void newImportItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                importItemCount.Content = "Items count : ";
                importEmpCombo.Text = "";
                imprtDeptCombo.Text = "";
                importItemCombo.Text = "";
                importCategoryCombo.Text = "";
                imprt_amounttxt.Text = "";
                imprtPricelbl.Text = "";
                billtxt.Text = "";
                regDatetxt.Text = "";
                buyDatetxt.Text = "";
                grid_import.ItemsSource = null;
            }
            catch { }
        }


        private void newEmployeeCareBtn_Click(object sender, RoutedEventArgs e)
        {
            employeeCareEmpCombo.Text = "";
            employeeCareDeptCombo.Text = "";
            employeeCareCounttxt.Content = "Items count : ";
            EmployeeCareGrid.ItemsSource = null;
        }

        private void approvalCompanySrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string compname = approvalCompCombo.Text.ToString();
            fill_comp(approvalCompCombo, compname);
            approvalCompCombo.IsDropDownOpen = true;

        }

        private void approvalCompCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                int compid = Convert.ToInt32((approvalCompCombo.Text.ToString()));
                fill_card(ApprovaltxtCardNum, compid);
            }
            catch { }

        }




        //window hatzhr feha el records el gdida elly da5lt
        private void notifyContractCompany_BalloonTipClicked(object sender, System.EventArgs e)
        {
            dis.Stop();
            CompanyContractWindow concw = new CompanyContractWindow(contract_company_differece);
            concw.ShowDialog();
            dis.Start();
        }



        private void notifyContract_Click(object sender, System.EventArgs e)
        {
            dis.Stop();
            ContractWindow cw = new ContractWindow(contract_provider_difference);
            cw.ShowDialog();
            dis.Start();
            //cw.Closed+=cw_Closed;

        }

        private void notifyVCompany_BalloonTipClicked(object sender, System.EventArgs e)
        {
          //  dis.Stop();
          //  VCompanyWindow vcomp = new VCompanyWindow(vcompany_difference);
        //    vcomp.ShowDialog();
         //   dis.Start();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // DispatcherTimer dis = new DispatcherTimer();
            dis.Tick += new EventHandler(dis_tick);
            dis.Interval = new TimeSpan(0, 0, 3);
            dis.Start();
        }

        //private void newEmpSrchComptxt_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (System.Text.RegularExpressions.Regex.IsMatch(newEmpSrchComptxt.Text, "[^0-9]"))
        //        {
        //            newEmpSrchComptxt.Text = newEmpSrchComptxt.Text.Remove(newEmpSrchComptxt.Text.Length - 1);
        //        }
        //    }
        //    catch { }
        //}

        //private void newEmpSrchComptxt_KeyDown(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Key == Key.Enter)
        //        {

        //            if (newEmpSrchComptxt.Text == "")
        //            {
        //                basicDataComp.Items.Clear();
        //                System.Data.DataTable data = agent.get_comp_name();
        //                for (int i = 0; i < data.Rows.Count; i++)
        //                {
        //                    basicDataComp.Items.Add(data.Rows[i].ItemArray[0].ToString());
        //                }
        //            }
        //            else
        //            {
        //                int id = Convert.ToInt32(newEmpSrchComptxt.Text.ToString());
        //                string nameComp = agent.get_company_fromID(id);
        //                if (nameComp == "" || nameComp == null)
        //                {
        //                    MessageBox.Show("لا توجد شركات بهذا الرقم");
        //                }
        //                else
        //                {
        //                    basicDataComp.Text = nameComp;
        //                }
        //            }
        //        }
        //    }
        //    catch { }
        //}

        private void newApprovalSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            approvalGrid.ItemsSource = null;
            approvalItemCounttxt.Content = "";
            ApprovaltxtCardNum.Text = "";
            totalApproveCount.Content = "";
            approvalCompCombo.Text = "";
        }

        private void imgsearch1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            networkcardcombo_Copy.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + networkcardcombo_Copy.Text + "%' or C_ANAME LIKE '%" + networkcardcombo_Copy.Text + "%'  ORDER BY C_COMP_ID ").Result.DefaultView;
            networkcardcombo_Copy.IsDropDownOpen = true;
        }

        private void imgsearch1_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            networkcardcombo.ItemsSource = db.RunReader(@"select distinct CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,
           EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE C_COMP_ID=" + networkcardcombo_Copy.Text + " " +
           "and ( CARD_ID LIKE '%" + networkcardcombo.Text + "%' OR EMP_ANAME_ST LIKE '%" + networkcardcombo.Text + "%'  OR EMP_ANAME_SC LIKE '%" + networkcardcombo.Text + "%'  OR EMP_ANAME_TH LIKE '%" + networkcardcombo.Text + "%' OR (emp_aname_st || ' ' ||emp_aname_sc||' '||emp_aname_th LIKE '%" + networkcardcombo.Text + "%' )) " +
           "ORDER BY CARD_ID ").Result.DefaultView;
            networkcardcombo.IsDropDownOpen = true;
        }

        private void imgsearch2_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            infoCardCompanyCombo.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + infoCardCompanyCombo.Text + "%' or C_ANAME LIKE '%" + infoCardCompanyCombo.Text + "%'  ORDER BY C_COMP_ID ").Result.DefaultView;
            infoCardCompanyCombo.IsDropDownOpen = true;

        }

        private void imgsearch2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


            infocardcombo.ItemsSource = db.RunReader(@"select distinct CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,
            EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE C_COMP_ID=" + infoCardCompanyCombo.Text + " " +
            "and ( CARD_ID LIKE '%" + infocardcombo.Text + "%' OR EMP_ANAME_ST LIKE '%" + infocardcombo.Text + "%'  " +
            "OR EMP_ANAME_SC LIKE '%" + infocardcombo.Text + "%'  OR EMP_ANAME_TH LIKE '%" + infocardcombo.Text + "%' OR (emp_aname_st || ' ' ||emp_aname_sc||' '||emp_aname_th LIKE '%" + infocardcombo.Text + "%' )) " +
            "ORDER BY CARD_ID ").Result.DefaultView;
            infocardcombo.IsDropDownOpen = true;
        }

        private void imgsearch3_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CompanyComboSummary.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + CompanyComboSummary.Text + "%' or C_ANAME LIKE '%" + CompanyComboSummary.Text + "%'  ORDER BY C_COMP_ID ").Result.DefaultView;
            CompanyComboSummary.IsDropDownOpen = true;
        }

        private void imgsearch4_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ApprovalCompanyCombo.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + ApprovalCompanyCombo.Text + "%' or C_ANAME LIKE '%" + ApprovalCompanyCombo.Text + "%'  ORDER BY C_COMP_ID ").Result.DefaultView;
            ApprovalCompanyCombo.IsDropDownOpen = true;
        }

        private void imgsearch4_Copy1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            approvalcardcombo.ItemsSource = db.RunReader(@" select distinct CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  
                from COMP_EMPLOYEESS WHERE C_COMP_ID=" + ApprovalCompanyCombo.Text + " " +
                "and ( CARD_ID LIKE '%" + approvalcardcombo.Text + "%' OR EMP_ANAME_ST LIKE '%" + approvalcardcombo.Text + "%'  " +
                "OR EMP_ANAME_SC LIKE '%" + approvalcardcombo.Text + "%'  OR EMP_ANAME_TH LIKE '%" + approvalcardcombo.Text + "%' OR (emp_aname_st || ' ' ||emp_aname_sc||' '||emp_aname_th LIKE '%" + approvalcardcombo.Text + "%' ) ) " +
                "ORDER BY CARD_ID ").Result.DefaultView;
            approvalcardcombo.IsDropDownOpen = true;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            tmcompanyprint.Text = "";
            ReceivingGrid.ItemsSource = null;
            dtpDateReceiving.Text = "";
            receiveItemconttxt.Content = "";

            rdCompanyName.IsChecked = false;
            rdDate.IsChecked = false;
            recevingGroup.Visibility = Visibility.Hidden;
            dtpDateReceiving.Visibility = Visibility.Hidden;
            //ReceivingGrid.Visibility = Visibility.Hidden;
            btnSearchDateReceving.Visibility = Visibility.Hidden;


            try
            {
                List<PrintingData> Companylist = printserv.SelectAllCompaniesForReceivingCards();
                listBox1Recieving.ItemsSource = Companylist;
                listBox1Recieving.DisplayMemberPath = "CompanyName";
                listBox1Recieving.SelectedValuePath = "CompID";
                string CompCode = listBox1Recieving.SelectedValue.ToString();

                List<PrintingData> Cards = printserv.SelectAllEmployees_For_ReceivingCards(CompCode);
                ReceivingGrid.ItemsSource = Cards;

                #region HideSomeColumns
                ReceivingGrid.Columns[1].Width = 300;
                ReceivingGrid.Columns[3].Width = 300;
                ReceivingGrid.Columns[2].Visibility = Visibility.Visible;
                //----Hide some columns--------------
                #region HideSomeColumns
                ReceivingGrid.Columns[1].Visibility = Visibility.Visible;
                ReceivingGrid.Columns[13].Visibility = Visibility.Hidden;
                ReceivingGrid.Columns[0].Visibility = Visibility.Hidden;
                ReceivingGrid.Columns[10].Visibility = Visibility.Hidden;
                ReceivingGrid.Columns[11].Visibility = Visibility.Hidden;
                ReceivingGrid.Columns[12].Visibility = Visibility.Hidden;
                ReceivingGrid.Columns[6].Visibility = Visibility.Hidden;
                ReceivingGrid.Columns[7].Visibility = Visibility.Hidden;
                ReceivingGrid.Columns[8].Visibility = Visibility.Hidden;

                ReceivingGrid.Columns[9].Visibility = Visibility.Hidden;
                #endregion

                #endregion
            }
            catch { }
        }

        private void printNewSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintDateFrom.Text = "";
            PrintDateTo.Text = "";
            PrintGrid.ItemsSource = null;
            printItemCounttxt.Content = "0";
        }

        private void providerTypeCombost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int code = client.get_provider_code(providerTypeCombost.SelectedItem.ToString());
                fill_pr(prCodeComboBox, code);
            }
            catch { }
        }

        private void providerSrchBtnSt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                fill_pr(prCodeComboBox, prCodeComboBox.Text.ToString());
                prCodeComboBox.IsDropDownOpen = true;
            }
            catch { }
        }

        private void providerTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string providerName = providerTypeCombo.SelectedItem.ToString();
                int code = client.get_provider_code(providerName);
                fill_pr(PrCodeComboMain, code);
            }
            catch { }
        }

        private void providerSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                fill_pr(PrCodeComboMain, PrCodeComboMain.Text.ToString());
                PrCodeComboMain.IsDropDownOpen = true;
            }
            catch { }
        }

        private void companySrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                fill_comp(CompanyComboBoxMain, CompanyComboBoxMain.Text.ToString());
                CompanyComboBoxMain.IsDropDownOpen = true;
            }
            catch { }
        }

        private void dmsprobtxtsolution_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (User.Department == "customerservices" || User.Department == "After Sales")// and DEPARTMENT='"+User.Department+"'
                dmsprobdg.ItemsSource = db.RunReader("Select CODE,DEPARTMENT,EMPLOYEE,COMP_DATE,COMP_TIME,REASON,DESCRIPTION,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE FROM COMPANIES_HR WHERE REPLAYED='N' and CODE like '" + dmsprobtxtsolution_Copy.Text + "%' ORDER BY COMP_DATE desc").Result.DefaultView;
            else
                dmsprobdg.ItemsSource = db.RunReader("Select CODE,DEPARTMENT,EMPLOYEE,COMP_DATE,COMP_TIME,REASON,DESCRIPTION,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE FROM COMPANIES_HR WHERE REPLAYED='N' and CODE like '" + dmsprobtxtsolution_Copy.Text + "%' and DEPARTMENT='" + User.Department + "' ORDER BY COMP_DATE desc").Result.DefaultView;

        }

        private void imgsearch12_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) { basicDataComp.ItemsSource = db.RunReader("select distinct C_COMP_ID, C_ANAME from v_companies where C_COMP_ID like '%" + basicDataComp.Text + "%' or C_ANAME like '%" + basicDataComp.Text + "%'  order by C_COMP_ID ").Result.DefaultView; basicDataComp.IsDropDownOpen = true; }

        //new final
        private void deptCombo_DropDownClosed(object sender, EventArgs e)
        {
            empCombo.ItemsSource = store.get_Return_employees_name(deptCombo.Text).DefaultView;
            //empCombo.ItemsSource = db.RunReader("select name,code from agent where agent_dept='" + deptCombo.Text + "'").DefaultView;
        }



        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (summaryCompany.IsSelected == true)
            {
                if (UserType == "hr" && summaryCompanyContract.ItemsSource == null && summaryCompanyContract.SelectedItem == null)
                {
                    lblContrLong.Visibility = Visibility.Hidden;
                    CompanyContractLongst.Visibility = Visibility.Hidden;
                    compidlbl.Visibility = Visibility.Hidden;
                    CompanyIDtxtst.Visibility = Visibility.Hidden;
                    CompanyComboSummary.Visibility = Visibility.Hidden;
                    providertabst.Visibility = Visibility.Collapsed;
                    complete.Visibility = Visibility.Collapsed;
                    int compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                    summaryCompanyContract.Items.Clear();
                    CompanyContractLongst.Text = abb.Content.ToString();
                    System.Data.DataTable data = contract.get_selected_company_data(compid);
                    imgsearch3_Copy.Visibility = Visibility.Hidden;
                    summaryCompanyContract.Items.Clear();
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        summaryCompanyContract.Items.Add(data.Rows[i].ItemArray[0].ToString());
                    }
                }
                else
                {
                    if (CompanyComboSummary.ItemsSource == null)
                        fill_comp(CompanyComboSummary);
                }


            }
            else
            {
                if (providertabst.IsSelected == true && providerTypeCombost.SelectedItem == null)
                {
                    System.Data.DataTable providerTable = client.get_provider();
                    providerTypeCombost.Items.Clear();
                    for (int i = 0; i < providerTable.Rows.Count; i++)
                    {
                        providerTypeCombost.Items.Add(providerTable.Rows[i].ItemArray[0].ToString());
                    }
                    //fill_pr(prCodeComboBox);
                }
            }
        }



        private void ccccccccccccccc(object sender, MouseButtonEventArgs e)
        {
            if (ContactUsTab.IsSelected == true)
            {
                if (datagridconnectus.ItemsSource == null)
                {
                    System.Data.DataTable qw = db.RunReader("select dep_name,dep_num  from CONNECTUS").Result;

                    qw.Columns[0].ColumnName = "اسم القسم";
                    qw.Columns[1].ColumnName = "رقم القسم";
                    datagridconnectus.ItemsSource = qw.DefaultView;



                }
            }

            else if (problems.IsSelected == true && probcbxDepartment.ItemsSource == null)
            {

                probdpTime.Text = DateTime.Now.ToShortTimeString();
                fillproblemdepart();
                fillprobreason();
                probAutoNume();
                fillProbEmployee();
                probtxtdtime.Visibility = Visibility.Hidden;

                if (User.Type == "hr")
                {
                    complaintsubjectss.Visibility = Visibility.Collapsed;
                    SolvedCompliantss.Visibility = Visibility.Collapsed;
                    Visitss.Visibility = Visibility.Collapsed;
                    Followss.Visibility = Visibility.Collapsed;

                    try
                    {
                        // fillProviderCust();
                        fillprovidertypeCust();
                        fillprovidertypeCust();
                        fillsubjectCust();

                        AutoNumeCust();
                        btnediteCust.Visibility = Visibility.Hidden;
                        datpcomCust.Text = DateTime.Now.ToString();
                    }
                    catch { }

                }
            }
            else
            {








            }

        }

        private void zzzzzzzzz(object sender, MouseButtonEventArgs e)
        {
            if (Followss.IsSelected == true && cbxproviderFollowCust.ItemsSource == null)
            {
                fillProviderFollowCust();
                btnediteeFollowCust.Visibility = Visibility.Hidden;
                btnsaveFollowCust.Visibility = Visibility.Hidden;
            }
            else
            {
                if (Serviec_requestss.IsSelected == true && cbxproviderCust_Copy.ItemsSource == null)
                {
                    try
                    {
                        // fillProviderCust();
                        fillprovidertypeCust();
                        fillprovidertypeCust();
                        fillsubjectCust();

                        AutoNumeCust();
                        btnediteCust.Visibility = Visibility.Hidden;
                        datpcomCust.Text = DateTime.Now.ToString();
                    }
                    catch { }
                }
                else
                {
                    if (Visitss.IsSelected == true && cbxproviderVisitCust.ItemsSource == null)
                    {
                        dtpcomVisitCust.Text = DateTime.Now.ToString();
                        fillProviderVisitCust();
                        fillPersonVisitCust();
                        AutoNumeVisitCust();
                        btnediteeVisitCust.Visibility = Visibility.Hidden;
                    }

                }
            }
        }

        private void aaaaaaaaaa(object sender, MouseButtonEventArgs e)
        {
            //{
            //    if (IndemnewCopy.IsSelected == true && IndemnityCompanyComboz1.ItemsSource == null)
            //    {
            //        IndemnityCompanyComboz1.ItemsSource = User.ALL_Company().DefaultView;
            //        if (User.Type == "hr")
            //        {
            //            IndemntiyCompanySrchBtnz1.IsEnabled = false;
            //            IndemnityCompanyComboz1.IsEnabled = false;
            //            IndemnityCompanyComboz1.Text = User.CompanyID;
            //            IndemnityCardComboz1_Copy1.IsEnabled = false;


            //        }


            //    }
            if (loadmotalbaTab.IsSelected == true && reqcompaddcbx2.ItemsSource == null && reqcardaddcbx2.ItemsSource == null)
            {

                if (User.Type == "DMS Member")
                {
                    reqcompaddcbx2.ItemsSource = User.ALL_Company().DefaultView;

                }
                else if (User.Type == "hr")
                {
                    searchreqadd2.IsEnabled = false;
                    reqcompaddcbx2.IsEnabled = false;
                    reqcompaddcbx2.Text = User.CompanyID;
                    reqcardaddcbx2.ItemsSource = User.Employee_in_Company().DefaultView;

                }

            }


            else if (checkabdo.IsSelected == true && abdoCbxCustomerName.ItemsSource == null)
            {
                FillCompanyabdoCbxCompany();
            }
            else
            {

                if (IndemnityTabz.IsSelected == true && IndemnityCompanyComboz.ItemsSource == null)
                {

                    try
                    {
                        if (UserType == "hr" && IndemnityCardComboz.ItemsSource == null)
                        {
                            int compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                            IndemnityCompanyComboz.Text = compid.ToString();
                            panel_CardNumz.Visibility = Visibility.Hidden;

                            txtCardNumz.Visibility = Visibility.Hidden;
                            IndemnityCompanyComboz.IsEnabled = false;
                            IndemnityCompanyComboz.Text = User.CompanyID;
                            IndemntiyCompanySrchBtnz.IsEnabled = false;

                            List<PrintingData> Companylist = printserv.SelectCompanyForReceivingCard(UserCompany);

                            indemnity_id = Convert.ToInt32(IndemnityCompanyComboz.Text.ToString());
                            fill_card(IndemnityCardComboz, indemnity_id);
                            string dateFrom = dtpFromz.Text.ToString();
                            string dateTo = dtpToz.Text.ToString();

                            List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCompanyCodeSearch(dateFrom, dateTo, indemnity_id);
                            if (Indemnities == null || Indemnities.Count == 0)
                            {
                                // MessageBox.Show("لا توجد بيانات");
                            }
                            else
                            {
                                IndemnityGridz.ItemsSource = Indemnities;
                                indemnityItmCounttxtz.Content = Indemnities.Count;
                            }
                            fill_card(IndemnityCardComboz, compid);
                        }
                        else if (IndemnityCompanyComboz.ItemsSource == null && User.Type == "DMS Member")
                        {
                            fill_comp(IndemnityCompanyComboz);
                        }
                    }
                    catch { }

                }
            }
        }

        private void eeeeeeeeeee(object sender, MouseButtonEventArgs e)
        {
            if (IndemnityTabz1_Copy1.IsSelected == true && dgbyanatmotalbatfardiaa1_Copy.ItemsSource == null)
            {
                researshtrue();
                cbxindcompany_Copy.ItemsSource = User.ALL_Company().DefaultView;
                cbxindcompany1_Copy.ItemsSource = db.RunReader("select BANK_ID ,BANK_NAME   from V_BANK_ACT order by BANK_ID").Result.DefaultView;

                refsecandgrid();

            }



            else if (IndemnityTabz1_Copy.IsSelected == true && dgbyanatmotalbatfardiaa.ItemsSource == null)
            {
                researshfalse();
                cbxindcompany.ItemsSource = User.ALL_Company().DefaultView;
                cbxdiscresoun.ItemsSource = db.RunReader("select GRUOP_ID ,GRUOP_NAME   from A_DISC_REASON order by GRUOP_ID").Result.DefaultView;

            }


            if (checkabdo2.IsSelected == true && abdo2CbxCustomerName.ItemsSource == null)
            {
                FillCompanyabdo2CbxCompany();
            }
            else
            {
                if (IndemnityTabz1.IsSelected == true && IndemnityCompanyCombo.ItemsSource == null)
                {
                    fill_comp(IndemnityCompanyCombo);
                }


            }
        }

        private void tttttttttt(object sender, MouseButtonEventArgs e)
        {
            if (departmentTab.IsSelected == true && deptgrid.ItemsSource == null)
            {

                System.Data.DataTable deptDT = agent.get_code_dept();
                deptgrid.ItemsSource = deptDT.DefaultView;
                deptgrid.Columns[0].IsReadOnly = true;
                try
                {
                    deptgrid.Columns[0].Header = "كود القسم";
                    deptgrid.Columns[1].Header = "اسم القسم";
                }
                catch { }
                System.Data.DataTable data = agent.get_all_dept();
                basicdataDemptCombo.Items.Clear();
                newDeptCombo.Items.Clear();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    basicdataDemptCombo.Items.Add(data.Rows[i].ItemArray[0].ToString());
                    newDeptCombo.Items.Add(data.Rows[i].ItemArray[0].ToString());
                }



            }
            else
            {

            }
        }

        private void qqqqqqqq(object sender, MouseButtonEventArgs e)
        {
            if (Follow.IsSelected == true && cbxproviderFollow.ItemsSource == null)
            {
                fillProviderFollow();
                btnediteeFollow.Visibility = Visibility.Hidden;
                btnsaveFollow.Visibility = Visibility.Hidden;
            }
            else
            {
                if (Serviec_request.IsSelected == true && cbxprovider.ItemsSource == null)
                {
                    fillProvider();
                    fillsubject();
                    fillEsclated();
                    AutoNume();
                    btnedite.Visibility = Visibility.Hidden;
                    dtpcom.Text = DateTime.Now.ToString();
                }
                else
                {
                    if (Visit.IsSelected == true && cbxproviderVisit.ItemsSource == null)
                    {
                        dtpcomVisit.Text = DateTime.Now.ToString();
                        fillProviderVisit();
                        fillPersonVisit();
                        AutoNumeVisit();
                        btnediteeVisit.Visibility = Visibility.Hidden;
                    }

                }
            }
        }

        private void wwwwwwww(object sender, MouseButtonEventArgs e)
        {
            if (MiladColeec.IsSelected == true && cmbCompanies.ItemsSource == null)
            {
                //-------fill companies-------------
                List<ReallocationData> list = ReallocationServices.SelectAllCompanies();
                cmbCompanies.ItemsSource = list;
                //  ---fill Prov type---------------
                List<ReallocationData> provTypes = ReallocationServices.SelectAllProvTypes();
                cmbServProvType.ItemsSource = provTypes;

                //-----------fill letter types---------
                List<ReallocationData> letterTypes = ReallocationServices.SelectAllLetterTypes();
                cmbLetterTypes.ItemsSource = letterTypes;
                txtReallocate_Code.IsReadOnly = true;


                //===============================
                grbReply.Visibility = Visibility.Hidden;
                btnUpdate_Reallocation.IsEnabled = false;
            }
            else
            {
                if (MiladColeec_Copy.IsSelected == true && cmbCompaniesz.ItemsSource == null)
                {
                    canvSearchCode.Visibility = Visibility.Hidden;
                    canvSearchDate.Visibility = Visibility.Hidden;
                    canvSearchCompany.Visibility = Visibility.Hidden;
                    grbReplyz.Visibility = Visibility.Hidden;
                    btnUpdateReply.Visibility = Visibility.Hidden;

                    //-------fill companies-------------
                    List<ReallocationData> list = ReallocationServices.SelectAllCompanies();
                    cmbCompaniesz.ItemsSource = list;
                }

            }

        }

        private void receivingcardSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                fill_comp(tmcompanyprint, tmcompanyprint.Text.ToString());
                tmcompanyprint.IsDropDownOpen = true;
            }
            catch { }
        }
        private void NumericOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9.]+");
            e.Handled = reg.IsMatch(e.Text);

        }
        private void NumberOnly(System.Object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("[^0-9]");
            e.Handled = reg.IsMatch(e.Text);

        }


        private void addCategoryNewBtn_Click(object sender, RoutedEventArgs e)
        {
            cat_txt.Text = "";
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {

                string CardNo = "";
                int comp_emp = 0;
                int card_approve = 0;
                if (User.Type != "DMS Member")
                {
                    CardNo = ApprovaltxtCardNum.Text.ToString();
                    string[] arr = CardNo.Split('-');
                    string comp = arr[0].ToString();
                    if (comp == CompanyCode)
                    {
                        comp_emp = client.validate_card_num(CardNo);
                        card_approve = client.validate_card_approval(CardNo);
                        if (comp_emp >= 1)
                        {
                            if (card_approve >= 1)
                            {
                                string value_PatiantName = ApprovaltxtCardNum.Text;
                                List<EmpApprovalData> Branches = emp.SelectAllApprovals(CardNo, value_PatiantName);
                                approvalGrid.ItemsSource = Branches;
                                totalApproveCount.Content = client.count_approve(CardNo).ToString();
                                approvalGrid.Columns[6].Visibility = Visibility.Hidden;
                                approvalGrid.Columns[7].Visibility = Visibility.Hidden;
                                approvalGrid.Columns[8].Visibility = Visibility.Hidden;
                                approvalGrid.Columns[9].Visibility = Visibility.Hidden;
                                approvalGrid.Columns[11].Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                MessageBox.Show("لا توجد موافقة لهذا الكارت");
                                ApprovaltxtCardNum.Text = "";
                                approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;
                                totalApproveCount.Content = "0";
                            }
                        }
                        else
                        {
                            MessageBox.Show("رقم كارت غير موجود");
                            ApprovaltxtCardNum.Text = "";
                            approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;
                            totalApproveCount.Content = "0";
                        }
                    }
                    else
                    {
                        MessageBox.Show("غير مسموح ببيانات هذه الشركة");
                    }

                }
                else
                {
                    CardNo = ApprovaltxtCardNum.Text;
                    comp_emp = client.validate_card_num(CardNo);
                    card_approve = client.validate_card_approval(CardNo);
                    if (comp_emp >= 1)
                    {
                        if (card_approve >= 1)
                        {
                            string value_PatiantName = ApprovaltxtCardNum.Text;
                            List<EmpApprovalData> Branches = emp.SelectAllApprovals(CardNo, value_PatiantName);
                            approvalGrid.ItemsSource = Branches;
                            totalApproveCount.Content = client.count_approve(CardNo).ToString();
                            approvalGrid.Columns[6].Visibility = Visibility.Hidden;
                            approvalGrid.Columns[7].Visibility = Visibility.Hidden;
                            approvalGrid.Columns[8].Visibility = Visibility.Hidden;
                            approvalGrid.Columns[9].Visibility = Visibility.Hidden;
                            approvalGrid.Columns[11].Visibility = Visibility.Hidden;
                            ApprovaltxtCardNum.Text = "";
                            approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;

                        }
                        else
                        {
                            MessageBox.Show("لا توجد موافقة لهذا الكارت");
                            ApprovaltxtCardNum.Text = "";
                            approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;
                            totalApproveCount.Content = "0";
                        }
                    }
                    else
                    {
                        MessageBox.Show("رقم كارت غير موجود");
                        ApprovaltxtCardNum.Text = "";
                        approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;
                        totalApproveCount.Content = "0";
                    }

                }
            }

            catch { }
        }

        private void newEmpSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            nametxt.Text = "";
            passtxt.Text = "";
            NewEmpDeptCombo.Text = "";
            basicDataComp.Text = "";
            codetxt.Text = "";
            yesrb.IsChecked = false;
            norb.IsChecked = false;
            repchk.IsChecked = false;
            indemchk.IsChecked = false;
            //  activeChk1.IsChecked = false;
            chequechk.IsChecked = false;
            monthmedchk.IsChecked = false;
            netchk.IsChecked = false;
            appchk.IsChecked = false;
            contrchk.IsChecked = false;
            hrtype.IsChecked = false;
            dmsMembertype.IsChecked = false;
            chkStore.IsChecked = false;
            chkNote.IsChecked = false;
            chkPrint.IsChecked = false;
            chkCustomer.IsChecked = false;
            chkComplain.IsChecked = false;
            chkReport.IsChecked = false;
            cbrequets.IsChecked = false;
            notiHrRequest.IsChecked = false;
            basicDataComp.ItemsSource = db.RunReader("select distinct C_COMP_ID, C_ANAME from v_companies order by C_COMP_ID ").Result.DefaultView;
            // newEmpSrchComptxt.Text = "";
        }

        private void newApprovalSrchBtnz_Click(object sender, RoutedEventArgs e)
        {
            approvalGridz.ItemsSource = null;
            approvalItemCounttxtz.Content = "";
            ApprovaltxtCardNumz.Text = "";
            approvalcardcombo.Text = "";
            totalApprovalCountz.Content = "";
            approvalcardcombo.Text = "";
            if (User.Type == "DMS Member") ApprovalCompanyCombo.Text = "";
        }

        private void newProviderSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProviderListDetails.ItemsSource = null;
                prCombo.Text = "";
                providerContract1.Source = null;
                providerContract2.Source = null;
                providerContract3.Source = null;
                providerContract4.Source = null;
                providerContract5.Source = null;
                providerContract6.Source = null;
                providerContract7.Source = null;
                providerContract8.Source = null;
                providerContrTypeCombo.Text = "";
                providerContrLongCombo.Text = "";
            }
            catch { }
        }
        private void newCompanySrchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CompanyCombo.Text = "";
                COmpanyGridDetails.ItemsSource = null;
                COmpanyGridDetails.SelectedItems.Clear();
                companyContractNumtxt.Text = "";
                CompanyContract1.Source = null;
                CompanyContract2.Source = null;
                CompanyContract3.Source = null;
                CompanyContract4.Source = null;
                CompanyContract5.Source = null;
                CompanyContract6.Source = null;
                CompanyContract7.Source = null;
                CompanyContract8.Source = null;
                CompanyContract9.Source = null;
                CompanyContract10.Source = null;
                CompanyContract11.Source = null;
                CompanyContract12.Source = null;
                CompanyContract13.Source = null;
                CompanyContract14.Source = null;
                CompanyContract15.Source = null;
                CompanyContract16.Source = null;
                CompanyContract17.Source = null;
                CompanyContract18.Source = null;
                CompanyContract19.Source = null;
                CompanyContract20.Source = null;
                CompanyContrType.Text = "";
                CompanyContrLong.Text = "";
            }
            catch { }
        }

        private void summaryProvidernewSrchBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                prCodetxt.Text = "";
                PrCodeComboMain.Text = "";
                SummaryProviderCodetxt.Text = "";
                prAnametxt.Text = "";
                prEnametxt.Text = "";
                prDevLoc.Text = "";
                prDevext.Text = "";
                prDeg.Text = "";
                prAddr1.Text = "";
                prAddr2.Text = "";
                prTel1.Text = "";
                prTel2.Text = "";
                prStampVal.Text = "";
                prTaxFlag.Text = "";
                prTermDate.Text = "";
                prTermFlag.Text = ""; prLocMedDis.Text = ""; prForMedDis.Text = "";
                img1.Source = null;
                img2.Source = null;
                img3.Source = null;
                img4.Source = null;
                img5.Source = null;
                img6.Source = null;
                img7.Source = null;
                img8.Source = null;
                img9.Source = null;
                providerTypeCombo.Text = "";
            }
            catch { }
        }

        private void summaryCompanynewSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CompanyComboBoxMain.Text = "";
                CompanyIDtxt.Text = "";
                Companyaname.Text = "";
                CompanyEname.Text = "";
                startDatetxt.Text = "";
                endDatetxt.Text = "";
                addr1txt.Text = "";
                hospitaltxt.Text = "";
                maxamounttxt.Text = "";
                ambulancetxt.Text = "";
                servDetailsGrid.ItemsSource = null;
                serServCombo.Items.Clear();
                dservcodetxt.Items.Clear();
                ClassCodeCombo.Items.Clear();
                summaryMainContractCompany.Items.Clear();
                companyContract1.Source = null;
                companyContract2.Source = null;
                companyContract3.Source = null;
                companyContract4.Source = null;
                companyContract5.Source = null;
                companyContract6.Source = null;
                companyContract7.Source = null;
                companyContract8.Source = null;
                companyContract9.Source = null;
                servGrid.ItemsSource = null;
                CompanyContractType.Text = "";
                CompanyContractLong.Text = "";
            }
            catch { }
        }

        private void customerNewSrchBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CompanyIDtxtst.Text = "";
                txtCardNumz.Text = "";
                ApprovaltxtCardNumz.Text = "";
                cardtxtNetwork.Text = "";
                InfotxtCardNum.Text = "";
                CallstxtSrch.Text = "";
                CallsGrid.ItemsSource = null;
                callDurationtxt.Content = "00 : 00 : 00";
                cardNolbl.Visibility = Visibility.Hidden;
                cardnumtxt.Text = "";
                cardnumtxt.Visibility = Visibility.Hidden;
            }
            catch { }
        }

        private void basicdataTabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (medicineTab.IsSelected == true && medicineGroupGrid.ItemsSource == null && superGroupCodeCombo.ItemsSource == null)
            {
                superGroupCodeCombo.ItemsSource = Medicie.find_super_group().DefaultView;
                medicineGroupGrid.ItemsSource = Medicie.get_all_medicine_group().DefaultView;

            }

        }

        private void medicineGroupGrid_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void CompanyIDtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                try
                {

                    int id = Convert.ToInt32(CompanyIDtxt.Text.ToString());
                    if (UserType == "hr")
                    {
                        int comp = Convert.ToInt32(report.get_comp_id(UserCompany));
                        if (id != comp)
                        {
                            MessageBox.Show("غير مسموح");
                        }
                        else
                        {
                            System.Data.DataTable data = contract.get_selected_company_data(id);
                            summaryMainContractCompany.Items.Clear();
                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                summaryMainContractCompany.Items.Add(data.Rows[i].ItemArray[0].ToString());
                            }
                        }

                    }
                    else
                    {
                        System.Data.DataTable data = contract.get_selected_company_data(id);
                        summaryMainContractCompany.Items.Clear();
                        for (int i = 0; i < data.Rows.Count; i++)
                        {
                            summaryMainContractCompany.Items.Add(data.Rows[i].ItemArray[0].ToString());
                        }
                    }
                }
                catch { }

            }
        }

        private void CompanyIDtxtst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    int id = Convert.ToInt32(CompanyIDtxtst.Text.ToString());
                    if (UserType == "hr")
                    {
                        int comp = Convert.ToInt32(report.get_comp_id(UserCompany));
                        if (id != comp)
                        {
                            MessageBox.Show("غير مسموح");
                        }
                        else
                        {
                            System.Data.DataTable data = contract.get_selected_company_data(id);
                            summaryCompanyContract.Items.Clear();
                            for (int i = 0; i < data.Rows.Count; i++)
                            {
                                summaryCompanyContract.Items.Add(data.Rows[i].ItemArray[0].ToString());
                            }
                        }
                    }
                    else
                    {
                        System.Data.DataTable data = contract.get_selected_company_data(id);
                        summaryCompanyContract.Items.Clear();
                        for (int i = 0; i < data.Rows.Count; i++)
                        {
                            summaryCompanyContract.Items.Add(data.Rows[i].ItemArray[0].ToString());
                        }
                    }
                }
                catch { }
            }
        }


        System.Data.DataTable dataMedcine = new System.Data.DataTable();

        private void selectBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = medicineGroupGrid.SelectedItem;
                DataGridRow drow = medicineGroupGrid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                drow.Background = Brushes.DarkOrchid;
                string id = (medicineGroupGrid.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                supergroupcodetxt.Text = Medicie.get_super_group_code().ToString();
                int code = Convert.ToInt32(supergroupcodetxt.Text.ToString());
                string ename = superenametxt.Text.ToString();
                string aname = superanametxt.Text.ToString();
                string type = "";
                if (superyesrb.IsChecked == true)
                {
                    type = "yes";
                }
                else if (supernorb.IsChecked == true)
                {
                    type = "no";
                }
                if (dataMedcine.Rows.Count == 0)
                {
                    dataMedcine.Columns.Add("Super Group Code", typeof(int));
                    dataMedcine.Columns.Add("Super Group Name", typeof(string));
                    dataMedcine.Columns.Add("اسم ال super group", typeof(string));
                    dataMedcine.Columns.Add("Super group type", typeof(string));
                    dataMedcine.Columns.Add("group Codes", typeof(int));
                }

                dataMedcine.Columns[4].Unique = true;
                try
                {
                    dataMedcine.Rows.Add(code, ename, aname, type, id);
                }
                catch
                {
                    MessageBox.Show("لقد قمت بإضافة هذا الجروب من قبل");
                }
                finalMedicineGrid.ItemsSource = dataMedcine.DefaultView;
                if (finalMedicineGrid.Columns.Count != 0)
                {
                    finalMedicineGrid.Columns[0].IsReadOnly = true;
                    finalMedicineGrid.Columns[4].IsReadOnly = true;
                }
            }
            catch { }
        }

        private void customerServTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void qweeeee(object sender, MouseButtonEventArgs e)
        {
            if (clientsReport_Copy.IsSelected == true)
            {
                reprep aa = new reprep();
                aa.ShowDialog();
            }
            if (employeesReport.IsSelected == true)
            {
                Report rep = new Report(UserCompany);
                rep.ShowDialog();
            }
          
             if (clientsReport_Copy1.IsSelected == true)
            {
                fwateer aaa = new fwateer();
                aaa.ShowDialog();

            }
        }

        private void messReqareaComboz_Copy_DropDownClosed(object sender, EventArgs e)
        {

            try
            {
                int compId = Convert.ToInt32(messReqareaComboz_Copy.Text);
                string compName = store.get_indem_company_name(compId);
                lblCompNamez.Content = compName;
                // messReqbranchCombo.Items.Clear();
                messReqaddrtxtz.Text = store.GetCompanyAddress(compId);
                List<MessengerRequestData> Branches = req.SelectAllCompanies_Branches(compId.ToString());
                //if (Branches == null)
                //{
                //    MessageBox.Show("لا توجد فروع ");
                //}
                messReqbranchComboz.ItemsSource = Branches;
                messReqbranchComboz.DisplayMemberPath = "Branch";
                messReqbranchComboz.SelectedValuePath = "Branch";
            }
            catch { }
        }




        private void srchSuperGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                superGroupCodeCombo.Text = "";
                supergroupcodetxt.Text = "";
                superenametxt.Text = "";
                superanametxt.Text = "";
                supernorb.IsChecked = false;
                superyesrb.IsChecked = false;
                finalMedicineGrid.ItemsSource = null;
                medicineGroupGrid.ItemsSource = null;
                medicineGroupGrid.ItemsSource = Medicie.get_all_medicine_group().DefaultView;
                dataMedcine.Rows.Clear();
                dataMedcine.Columns[4].Unique = false;
                dataMedcine.Columns.Clear();
            }
            catch { }
        }


        private void editSuperGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            if (finalMedicineGrid.SelectedItems.Count == 0)
            {
                MessageBox.Show("من فضلك اختر super group");
            }
            else
            {
                for (int i = 0; i < finalMedicineGrid.SelectedItems.Count; i++)
                {
                    System.Data.DataRowView dr = (System.Data.DataRowView)finalMedicineGrid.SelectedItems[0];
                    int code = Convert.ToInt32(dr.Row.ItemArray[0].ToString());
                    string ename = dr.Row.ItemArray[1].ToString();
                    string aname = dr.Row.ItemArray[2].ToString();
                    int group = Convert.ToInt32(dr.Row.ItemArray[3].ToString());
                    string type = dr.Row.ItemArray[4].ToString();
                    Medicie.update_super_group(code, ename, aname, type, group);

                }
                MessageBox.Show("تم تعديل البيانات بنجاح");


                try
                {
                    finalMedicineGrid.Columns[0].IsReadOnly = true;
                    finalMedicineGrid.Columns[3].IsReadOnly = true;
                }
                catch { }

                finalMedicineGrid.Items.Refresh();
            }

        }

        private void deleteSuperGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (finalMedicineGrid.SelectedItems.Count == 0)
                {
                    MessageBox.Show("من فضلك اختر super group");
                }
                else
                {
                    int group = 0;
                    int code = 0;
                    for (int i = 0; i < finalMedicineGrid.SelectedItems.Count; i++)
                    {
                        System.Data.DataRowView dr = (System.Data.DataRowView)finalMedicineGrid.SelectedItems[0];
                        code = Convert.ToInt32(dr.Row.ItemArray[0].ToString());
                        group = Convert.ToInt32(dr.Row.ItemArray[3].ToString());
                        Medicie.delete_super_group(code, group);
                    }
                    MessageBox.Show("تم مسح البيانات بنجاح");
                    try
                    {
                        System.Data.DataTable data = Medicie.find_super_group(code);
                        if (data.Rows.Count == 0)
                        {
                            MessageBox.Show("لا توجد بيانات");
                        }
                        else
                        {
                            finalMedicineGrid.ItemsSource = data.DefaultView;
                            finalMedicineGrid.Columns[0].IsReadOnly = true;
                            finalMedicineGrid.Columns[3].IsReadOnly = true;
                        }
                    }
                    catch { }

                    try
                    {
                        finalMedicineGrid.Columns[0].IsReadOnly = true;
                        finalMedicineGrid.Columns[3].IsReadOnly = true;
                    }
                    catch { }

                    finalMedicineGrid.Items.Refresh();
                }
            }
            catch { }
        }

        private void complainsTabss_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void cardrb_Checked(object sender, RoutedEventArgs e)
        {
            if (UserType == "hr")
            {
                cardtxtNetwork.Visibility = Visibility.Hidden;
                networkClassCodeCombo.Visibility = Visibility.Hidden;


                aqaq.Visibility = Visibility.Visible;
                qwqw1.Visibility = Visibility.Visible;
                networkcardcombo_Copy.Visibility = Visibility.Visible;
                networkcardcombo.Visibility = Visibility.Visible;
                imgsearch1.Visibility = Visibility.Visible;
                imgsearch1_Copy.Visibility = Visibility.Visible;

            }
            else
            {
                cardtxtNetwork.Visibility = Visibility.Visible;
                networkcardcombo.Visibility = Visibility.Hidden;
                networkClassCodeCombo.Visibility = Visibility.Hidden;
            }
        }

        //new final
        private void cmbProviderTypeDeliver_Copy_DropDownClosed(object sender, EventArgs e)
        {
            //------------------fill Provide Names--------------------
            try
            {
                int ProviderType = int.Parse(cmbProviderTypeDeliver.SelectedValue.ToString());
                int ProviderCode = int.Parse(cmbProviderTypeDeliver_Copy.Text.ToString());
                List<NoteBookData> Providers = note.SelectAllNotebook_ReportByCompany(ProviderCode, ProviderType);
                if (Providers != null)
                {
                    deliverNoteGrid.ItemsSource = Providers;
                    deliverNoteItmCounttxt.Content = Providers.Count.ToString();
                }
                else
                {
                    MessageBox.Show("لا توجد بيانات");
                }

            }
            catch { }
        }

        private void zaqwsx(object sender, MouseButtonEventArgs e)
        {
            if (MovingNoteBook.IsSelected == true && cmbProviderTypeDeliver.ItemsSource == null)
            {

                Date_panel.Visibility = Visibility.Hidden;
                grbProviderNameSearch.Visibility = Visibility.Hidden;
                deliverNoteGrid.Visibility = Visibility.Hidden;
                //------------------fill provider type---------------
                List<NoteBookData> ProviderType = note.SelectAllProviderTypes();
                cmbProviderTypeDeliver.ItemsSource = ProviderType;
                cmbProviderTypeDeliver.DisplayMemberPath = "Type_Name";
                cmbProviderTypeDeliver.SelectedValuePath = "ProvTypeCode";

            }
            else if (RequestNotebook.IsSelected == true && cmbProviderTypeRequestNotebook.ItemsSource == null)
            {

                btnUpdateRequest.IsEnabled = false;
                btnDeleteRequest.IsEnabled = false;
                dtpRequestDate.SelectedDateFormat = DatePickerFormat.Short;
                dtpRequestDate.IsEnabled = false;
                txtCount.IsEnabled = false;
                btnSaveRequest.IsEnabled = false;
                try
                {
                    //------------------fill provider type---------------
                    List<NoteBookData> ProviderType = note.SelectAllProviderTypes();
                    cmbProviderTypeRequestNotebook.ItemsSource = ProviderType;
                    cmbProviderTypeRequestNotebook.DisplayMemberPath = "Type_Name";
                    cmbProviderTypeRequestNotebook.SelectedValuePath = "ProvTypeCode";
                    //------------------fill Provide Names--------------------

                    //---------------------Fill Notebook Types---------------------
                    List<NoteBookData> NotebookTypes = note.SelectAllNotebookTypes();
                    cmbNotebookTypes.ItemsSource = NotebookTypes;
                    cmbNotebookTypes.DisplayMemberPath = "NotebookName";
                    cmbNotebookTypes.SelectedValuePath = "Notebook_Type_Code";
                }
                catch { }

            }
            //else if(tiTsleem.IsSelected==true)
            //{
            //    RequestNotebook.IsSelected = true;
            //    DeliverNotebookFrm deliver = new DeliverNotebookFrm(NameTab.Header.ToString());
            //    deliver.ShowDialog();

            //}
            //else if(tita2ked.IsSelected == true)
            //{
            //    RequestNotebook.IsSelected = true;
            //    ConfirmNotebook_RequestFrm cr = new ConfirmNotebook_RequestFrm(NameTab.Header.ToString());
            //    cr.ShowDialog();
            //}
        }

        private void classrb_Checked(object sender, RoutedEventArgs e)
        {
            cardtxtNetwork.Visibility = Visibility.Hidden;
            networkcardcombo.Visibility = Visibility.Hidden;
            networkClassCodeCombo.Visibility = Visibility.Visible;

            aqaq.Visibility = Visibility.Hidden;
            qwqw1.Visibility = Visibility.Hidden;
            networkcardcombo_Copy.Visibility = Visibility.Hidden;
            networkcardcombo.Visibility = Visibility.Hidden;
            imgsearch1.Visibility = Visibility.Hidden;
            imgsearch1_Copy.Visibility = Visibility.Hidden;
        }


        private void networknewSrchbtn_Click(object sender, RoutedEventArgs e)
        {
            networkClassCodeCombo.Text = "";
            cardtxtNetwork.Text = "";
            classrb.IsChecked = false;
            cardrb.IsChecked = false;
            providerComboNetwork.Text = "";
            governComboNetwork.Text = "";
            areaComboNetwork.Text = "";
            NetworkGrid.ItemsSource = null;
            docSpecComboNetwork.Text = "";
            networkcardcombo.Text = "";
            newtworkCounttxt.Content = "";
            if (User.Type == "DMS Member") networkcardcombo_Copy.Text = "";

        }

        private void dmsbtnSave_Copy_Click(object sender, RoutedEventArgs e)
        {
            dmsprobtxtsolution_Copy.Text = "";
        }

        private void imgsearch6_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            messReqareaComboz_Copy.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + messReqareaComboz_Copy.Text + "%' or C_ANAME LIKE '%" + messReqareaComboz_Copy.Text + "%'  ORDER BY C_COMP_ID ").Result.DefaultView;
            messReqareaComboz_Copy.IsDropDownOpen = true;
        }

        private void imgsearch8_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string input = cbxcompcomp.Text.ToString();
            cbxcompcomp.ItemsSource = req.SelectAllCompaniesByNameOrCode(input);
            cbxcompcomp.IsDropDownOpen = true;
        }

        private void newCustomerSummaryCompanyBtn_Click(object sender, RoutedEventArgs e)
        {
            serservgrid.ItemsSource = null;
            CompanyIDtxtst.Text = "";
            CompanyComboSummary.Text = "";
            Companyanamest.Text = "";
            CompanyEnamest.Text = "";
            startDatetxtst.Text = "";
            endDatetxtst.Text = "";
            addr1txtst.Text = "";
            hospitaltxtst.Text = "";
            maxamounttxtst.Text = "";
            ambulancetxtst.Text = "";
            servDetailsGridst.ItemsSource = null;
            serServzCombo.Items.Clear();
            dservcodetxtst.Items.Clear();
            CompanyContractLongst.Text = "";
            CompanyContractTypest.Text = "";
            ClassCodeCombost.Items.Clear();
            companyContract1st.Source = null;
            //companyContract2st.Source = null;
            //companyContract3st.Source = null;
            //companyContract4st.Source = null;
            //companyContract5st.Source = null;
            //companyContract6st.Source = null;
            //companyContract7st.Source = null;
            //companyContract8st.Source = null;
            //companyContract9st.Source = null;
            summaryCompanyContract.Text = "";
            summaryCompanyContract.SelectedIndex = -1;
            CompanyComboSummary.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  ORDER BY C_COMP_ID ").Result.DefaultView;


        }

        private void summaryCompanyContract_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (summaryCompanyContract.SelectedIndex != -1)
            {
                int contrno = Convert.ToInt32(summaryCompanyContract.SelectedItem.ToString());

                int compid = 0;
                if (UserType == "hr")
                {
                    compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                }
                else
                {
                    compid = Convert.ToInt32(CompanyComboSummary.Text.ToString());
                }
                System.Data.DataTable compDataTable = contract.get_company_data(compid, contrno);
                startDatetxtst.Text = compDataTable.Rows[0].ItemArray[3].ToString();
                endDatetxtst.Text = compDataTable.Rows[0].ItemArray[4].ToString();
                Companyanamest.Text = compDataTable.Rows[0].ItemArray[0].ToString();
                CompanyEnamest.Text = compDataTable.Rows[0].ItemArray[1].ToString();
                addr1txtst.Text = compDataTable.Rows[0].ItemArray[2].ToString();
                DateTime x = Convert.ToDateTime(compDataTable.Rows[0].ItemArray[3].ToString());
                startDatetxtst.Text = x.ToString("dd-MMM-yyyy");
                DateTime y = Convert.ToDateTime(compDataTable.Rows[0].ItemArray[4].ToString());
                endDatetxtst.Text = y.ToString("dd-MMM-yyyy");
                System.Data.DataTable classCodeDT = contract.get_class_code(compid, contrno);
                ClassCodeCombost.Items.Clear();
                for (int i = 0; i < classCodeDT.Rows.Count; i++)
                {
                    ClassCodeCombost.Items.Add(classCodeDT.Rows[i].ItemArray[0].ToString() + " " + classCodeDT.Rows[i].ItemArray[1].ToString());
                }
            }
        }


        private void newInfoBtn_Click(object sender, RoutedEventArgs e)
        {

            InfotxtCardNum.Text = "";
            infocardcombo.Text = "";
            infoItemCounttxt.Content = "Items count : 0";
            InfoGrid.ItemsSource = null;
            if (User.Type == "DMS Member") infoCardCompanyCombo.Text = "";
        }



        #region New

        private void ReviCompcbx_DropDownClosed(object sender, EventArgs e) { try { ReviCompcbx_Copy.ItemsSource = User.Employee_in_Company(ReviCompcbx.Text).DefaultView; ReviCompcbx_Copy1.ItemsSource = db.RunReader("select DISTINCT dms_test.COMP_CONTRACT_CLASS.CLASS_CODE , V_CLASS_NAME.CLASS_ENAME from dms_test.COMP_CONTRACT_CLASS , V_CLASS_NAME where V_CLASS_NAME.CLASS_CODE=dms_test.COMP_CONTRACT_CLASS.CLASS_CODE and dms_test.COMP_CONTRACT_CLASS.C_COMP_ID= '" + ReviCompcbx.Text + "'").Result.DefaultView; } catch { } }

        private void pqpqppq(object sender, MouseButtonEventArgs e)
        {
            if (ReviComp.IsSelected == true && ReviCompcbx.ItemsSource == null)
            {
                ReviCompcbx.ItemsSource = User.ALL_Company().DefaultView;

                ReviCompcbx_Copy2.ItemsSource = db.RunReader("select DISTINCT V_DIFF_PERT.D_SERV_CODE ,V_SERVICES.SERV_ANAME  from V_DIFF_PERT ,V_SERVICES where V_DIFF_PERT.D_SERV_CODE=V_SERVICES.SERV_CODE order by V_DIFF_PERT.D_SERV_CODE ").Result.DefaultView;


            }
            else if (Resouns.IsSelected == true && ReviCompcbx1.ItemsSource == null)
            {
                ReviCompcbx1.ItemsSource = db.RunReader("select DISTINCT BATCH_NO  from A_BATCH_S  order by BATCH_NO").Result.DefaultView;
                ReviCompcbx_Copy4.ItemsSource = db.RunReader("select DISTINCT CHECK_STATUS  from A_BATCH_S  order by CHECK_STATUS").Result.DefaultView;
                ReviCompcbx_Copy3.ItemsSource = db.RunReader(" select distinct USER_CO , USER_N from V_PROVIDERS where KIND_NO IN (1,3,4) ORDER BY USER_CO ").Result.DefaultView;

            }
            else if (Resouns_Copy.IsSelected == true && ReviCompcbx2.ItemsSource == null)
            {
                ReviCompcbx2.ItemsSource = db.RunReader("select DISTINCT BATCH_NO  from A_BATCH_D  order by BATCH_NO").Result.DefaultView;
                ReviCompcbx_Copy5.ItemsSource = db.RunReader(" select distinct USER_CO , USER_N from V_PROVIDERS where KIND_NO IN (1,3,4) ORDER BY USER_CO ").Result.DefaultView;
                // ReviCompcbx_Copy6.ItemsSource = db.RunReader("select DISTINCT CLAIM_NO  from A_BATCH_D  order by CLAIM_NO").DefaultView;

            }

            //21 nov
            else if (Med_Diff_tab.IsSelected == true && medCodeCombo.ItemsSource == null)
            {
                medCodeCombo.ItemsSource = db.RunReader("select distinct MED_CODE,MED_NAME from A_MED_DIFF").Result.DefaultView;

            }

        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            //select CONTRACT_NO ,CLASS_CODE ,CARD_ID,C_COMP_ID ,CEILING_AMT_D,CEILING_AMT,SER_SERV,D_SERV_CODE,CEILING_PERT_D,CEILING_PERT from V_DIFF_PERT
            System.Data.DataTable tt = db.RunReader("select  C_COMP_ID ,  CARD_ID , CLASS_CODE , CONTRACT_NO ,D_SERV_CODE,SER_SERV,CEILING_AMT, CEILING_AMT_D,CEILING_PERT , CEILING_PERT_D from V_DIFF_PERT where CLASS_CODE like '%" + ReviCompcbx_Copy1.Text + "%' and CARD_ID like '%" + ReviCompcbx_Copy.Text + "%' and C_COMP_ID like '%" + ReviCompcbx.Text + "%' and D_SERV_CODE like '%" + ReviCompcbx_Copy2.Text + "%'").Result;
            tt.Columns[0].ColumnName = "رقم الشركة";
            tt.Columns[1].ColumnName = "رقم الكارت";
            tt.Columns[2].ColumnName = "كود الفئة";
            tt.Columns[3].ColumnName = "رقم العقد";
            tt.Columns[4].ColumnName = "كود جروب الخدمة";
            tt.Columns[5].ColumnName = "كود الخدمة";
            tt.Columns[6].ColumnName = "الحد الاقصى للتغطية - الموظف";
            tt.Columns[7].ColumnName = "الحد الاقصى للتغطية - العقد";
            tt.Columns[8].ColumnName = "نسبة التغطية - الموظف";
            tt.Columns[9].ColumnName = "نسبة التغطية - العقد";
            ReviData.ItemsSource = tt.DefaultView;
        }

        private void imgsearcha_Copaay_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReviCompcbx.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + ReviCompcbx.Text + "%' or C_ANAME LIKE '%" + ReviCompcbx.Text + "%'  ORDER BY C_COMP_ID ").Result.DefaultView;
            ReviCompcbx.IsDropDownOpen = true;
        }

        private void imgsearcha_Copaay_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReviCompcbx_Copy.ItemsSource = db.RunReader(" select distinct   CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE C_COMP_ID='" + ReviCompcbx.Text + "' and ( CARD_ID LIKE '%" + ReviCompcbx_Copy.Text + "%' OR EMP_ANAME_ST LIKE '%" + ReviCompcbx_Copy.Text + "%'  OR EMP_ANAME_SC LIKE '%" + ReviCompcbx_Copy.Text + "%'  OR EMP_ANAME_TH LIKE '%" + ReviCompcbx_Copy.Text + "%' ) ORDER BY CARD_ID ").Result.DefaultView;
            ReviCompcbx_Copy.IsDropDownOpen = true;
        }

        private void imgsearcha_Copaay_Copy1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReviCompcbx_Copy1.ItemsSource = db.RunReader("select DISTINCT dms_test.COMP_CONTRACT_CLASS.CLASS_CODE , V_CLASS_NAME.CLASS_ENAME from dms_test.COMP_CONTRACT_CLASS , V_CLASS_NAME where V_CLASS_NAME.CLASS_CODE=dms_test.COMP_CONTRACT_CLASS.CLASS_CODE and dms_test.COMP_CONTRACT_CLASS.C_COMP_ID= '" + ReviCompcbx.Text + "' and(dms_test.COMP_CONTRACT_CLASS.CLASS_CODE like '%" + ReviCompcbx_Copy1.Text + "%' or V_CLASS_NAME.CLASS_ENAME like '%" + ReviCompcbx_Copy1.Text + "%')").Result.DefaultView;
            ReviCompcbx_Copy1.IsDropDownOpen = true;
        }

        private void imgsearcha_Copaay_Copy2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReviCompcbx_Copy2.ItemsSource = db.RunReader("select DISTINCT V_DIFF_PERT.D_SERV_CODE ,V_SERVICES.SERV_ANAME  from V_DIFF_PERT ,V_SERVICES where V_DIFF_PERT.D_SERV_CODE=V_SERVICES.SERV_CODE and(V_DIFF_PERT.D_SERV_CODE like '%" + ReviCompcbx_Copy2.Text + "%' or V_SERVICES.SERV_ANAME like '%" + ReviCompcbx_Copy2.Text + "%' ) order by V_DIFF_PERT.D_SERV_CODE").Result.DefaultView;
            ReviCompcbx_Copy2.IsDropDownOpen = true;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            ReviCompcbx.Text = "";
            ReviCompcbx_Copy1.Text = "";
            ReviCompcbx_Copy.Text = "";
            ReviCompcbx_Copy2.Text = "";
            ReviData.ItemsSource = null;
            ReviCompcbx.ItemsSource = User.ALL_Company().DefaultView;
            ReviCompcbx_Copy2.ItemsSource = db.RunReader("select DISTINCT V_DIFF_PERT.D_SERV_CODE ,V_SERVICES.SERV_ANAME  from V_DIFF_PERT ,V_SERVICES where V_DIFF_PERT.D_SERV_CODE=V_SERVICES.SERV_CODE order by V_DIFF_PERT.D_SERV_CODE ").Result.DefaultView;

        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            if (ReviCompcbx_Copy3.Text != "" || ReviCompcbx1.Text != "" || ReviCompcbx_Copy4.Text != "")
            {
                System.Data.DataTable templ;
                if (ReviCompcbx_Copy3.Text != "" && ReviCompcbx1.Text != "") templ = db.RunReader("select * from A_BATCH_S where BATCH_NO like '" + ReviCompcbx1.Text + "' and PRV_NO = '" + ReviCompcbx_Copy3.Text + "' and CHECK_STATUS like '" + ReviCompcbx_Copy4.Text + "%'  order by BATCH_NO").Result;
                else if (ReviCompcbx_Copy3.Text != "") templ = db.RunReader("select * from A_BATCH_S where BATCH_NO like '" + ReviCompcbx1.Text + "%' and PRV_NO = '" + ReviCompcbx_Copy3.Text + "' and CHECK_STATUS like '" + ReviCompcbx_Copy4.Text + "%'  order by BATCH_NO").Result;
                else if (ReviCompcbx1.Text != "") templ = db.RunReader("select * from A_BATCH_S where BATCH_NO like '" + ReviCompcbx1.Text + "' and PRV_NO like '" + ReviCompcbx_Copy3.Text + "%' and CHECK_STATUS like '" + ReviCompcbx_Copy4.Text + "%'  order by BATCH_NO").Result;
                else templ = db.RunReader("select * from A_BATCH_S where BATCH_NO like '" + ReviCompcbx1.Text + "%' and PRV_NO like '" + ReviCompcbx_Copy3.Text + "%' and CHECK_STATUS like '" + ReviCompcbx_Copy4.Text + "%'  order by BATCH_NO").Result;

                templ.Columns[0].ColumnName = "BATCH-NO";
                templ.Columns[1].ColumnName = "GROUPaa";
                templ.Columns[2].ColumnName = "PRV-NO";
                templ.Columns[3].ColumnName = "PRV-NAME";
                templ.Columns[4].ColumnName = "SYSTEM-AMT";
                templ.Columns[5].ColumnName = "CHECK-AMT";
                templ.Columns[6].ColumnName = "CHECK-STATUS";
                templ.Columns[7].ColumnName = "SYSTEM-NO";
                templ.Columns[8].ColumnName = "MANUAL-NO";
                templ.Columns[9].ColumnName = "M-VD";
                templ.Columns[10].ColumnName = "FIN-CLM";
                templ.Columns[11].ColumnName = "MONY-STETMENT";
                templ.Columns[12].ColumnName = "DATE-STETMENT";
                templ.Columns[13].ColumnName = "SERV-DATE-STETMENT";


                templ.Columns.Add("GROUP", typeof(String));


                int i = 0;
                foreach (DataRow row in templ.Rows)
                {


                    if (templ.Rows[i]["GROUPaa"].ToString() == "1")
                    {
                        row["GROUP"] = "DMS";
                    }
                    else
                    {
                        row["GROUP"] = "DMS+";
                    }


                    i++;
                }
                templ.Columns.RemoveAt(1);
                templ.Columns["GROUP"].SetOrdinal(1);



                templ.Columns[0].ColumnName = "رقم الباتش";
                templ.Columns[1].ColumnName = "جروب الشركة";
                templ.Columns[2].ColumnName = "كود مقدم الخدمة";
                templ.Columns[3].ColumnName = "اسم مقدم الخدمة";
                templ.Columns[4].ColumnName = "قيمة مبلغ السيستم";
                templ.Columns[5].ColumnName = "قيمة مبلغ الشيك";
                templ.Columns[6].ColumnName = "موقف الشيك";
                templ.Columns[7].ColumnName = "عدد المطالبات السيستم";
                templ.Columns[8].ColumnName = "عدد المطالبات المانويل";
                templ.Columns[9].ColumnName = "قيمة مبلغ المانويل";
                templ.Columns[10].ColumnName = "المبلغ بعد المراجعه";
                templ.Columns[11].ColumnName = "مبلغ المطالبة(ستيتمنت)";
                templ.Columns[12].ColumnName = "تاريخ المطالبة (ستيتمنت)";
                templ.Columns[13].ColumnName = "خدمات شهر";
                templ.Columns[14].ColumnName = "ملاحظات";




                ReviData1.ItemsSource = templ.DefaultView;
            }
            else
                MessageBox.Show("برجاء ملأ بيانات");
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            ReviCompcbx1.Text = "";
            ReviCompcbx_Copy3.Text = "";
            ReviCompcbx_Copy4.Text = "";
            ReviData1.ItemsSource = null;
        }

        private void imgsearcha_Copaay_Copy3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReviCompcbx_Copy3.ItemsSource = db.RunReader("  select  distinct USER_CO, USER_N from V_PROVIDERS WHERE KIND_NO IN (1,3,4) and USER_CO  LIKE '%" + ReviCompcbx_Copy3.Text + "%' or USER_N LIKE '%" + ReviCompcbx_Copy3.Text + "%'  ORDER BY USER_CO ").Result.DefaultView;
            ReviCompcbx_Copy3.IsDropDownOpen = true;
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            if (ReviCompcbx2.Text != "" || ReviCompcbx_Copy5.Text != "" || ReviCompcbx_Copy6.Text != "")
            {
                claimsum_Copy1.Background = Brushes.White;
                Sumzq1.Background = Brushes.White;
                Sumzq2.Background = Brushes.White;
                Sumzq3.Background = Brushes.White;
                Sumzq4.Background = Brushes.White;
                Sumzq5.Background = Brushes.White;
                Sumzq6.Background = Brushes.White;
                Sumzq7.Background = Brushes.White;
                Sumzq8.Background = Brushes.White;
                Sumzq9.Background = Brushes.White;
                Sumzq10.Background = Brushes.White;
                Sumzq11.Background = Brushes.White;
                Sumzq12.Background = Brushes.White;
                Sumzq13.Background = Brushes.White;
                Sumzq14.Background = Brushes.White;
                Sumzq15.Background = Brushes.White;
                Sumzq16.Background = Brushes.White;
                Sumzq17.Background = Brushes.White;
                Sumzq18.Background = Brushes.White;
                Sumzq19.Background = Brushes.White;
                Sumzq20.Background = Brushes.White;
                Sumzq21.Background = Brushes.White;
                Sumzq22.Background = Brushes.White;
                Sumzq23.Background = Brushes.White;
                Sumzq24.Background = Brushes.White;
                Sumzq25.Background = Brushes.White;
                Sumzq26.Background = Brushes.White;

                System.Data.DataTable templ;
                System.Data.DataTable temp2;
                System.Data.DataTable count_2dwyaa;
                if (ReviCompcbx_Copy5.Text != "" && ReviCompcbx2.Text != "")
                {
                    templ = db.RunReader("select * from A_BATCH_D where BATCH_NO like '" + ReviCompcbx2.Text + "' and PRV_NO = '" + ReviCompcbx_Copy5.Text + "' and CLAIM_NO like '" + ReviCompcbx_Copy6.Text + "%'  order by BATCH_NO").Result;
                    temp2 = db.RunReader("select DISTINCT CLAIM_NO from A_BATCH_D where BATCH_NO like '" + ReviCompcbx2.Text + "' and PRV_NO = '" + ReviCompcbx_Copy5.Text + "' and CLAIM_NO like '" + ReviCompcbx_Copy6.Text + "%' ").Result;
                    count_2dwyaa = db.RunReader("select DISTINCT MED_CODE from A_BATCH_D where BATCH_NO like '" + ReviCompcbx2.Text + "' and PRV_NO = '" + ReviCompcbx_Copy5.Text + "' and CLAIM_NO like '" + ReviCompcbx_Copy6.Text + "%' ").Result;
                }
                else if (ReviCompcbx_Copy5.Text != "")
                {
                    templ = db.RunReader("select * from A_BATCH_D where BATCH_NO like '" + ReviCompcbx2.Text + "%' and PRV_NO = '" + ReviCompcbx_Copy5.Text + "' and CLAIM_NO like '" + ReviCompcbx_Copy6.Text + "%'  order by BATCH_NO").Result;
                    temp2 = db.RunReader("select DISTINCT CLAIM_NO from A_BATCH_D where BATCH_NO like '" + ReviCompcbx2.Text + "%' and PRV_NO = '" + ReviCompcbx_Copy5.Text + "' and CLAIM_NO like '" + ReviCompcbx_Copy6.Text + "%'  ").Result;
                    count_2dwyaa = db.RunReader("select DISTINCT MED_CODE from A_BATCH_D where BATCH_NO like '" + ReviCompcbx2.Text + "%' and PRV_NO = '" + ReviCompcbx_Copy5.Text + "' and CLAIM_NO like '" + ReviCompcbx_Copy6.Text + "%'  ").Result;

                }
                else if (ReviCompcbx2.Text != "")
                {
                    templ = db.RunReader("select * from A_BATCH_D where BATCH_NO like '" + ReviCompcbx2.Text + "' and PRV_NO like '" + ReviCompcbx_Copy5.Text + "%' and CLAIM_NO like '" + ReviCompcbx_Copy6.Text + "%'  order by BATCH_NO").Result;

                    temp2 = db.RunReader("select DISTINCT CLAIM_NO from A_BATCH_D where BATCH_NO like '" + ReviCompcbx2.Text + "' and PRV_NO like '" + ReviCompcbx_Copy5.Text + "%' and CLAIM_NO like '" + ReviCompcbx_Copy6.Text + "%'  ").Result;

                    count_2dwyaa = db.RunReader("select DISTINCT MED_CODE from A_BATCH_D where BATCH_NO like '" + ReviCompcbx2.Text + "' and PRV_NO like '" + ReviCompcbx_Copy5.Text + "%' and CLAIM_NO like '" + ReviCompcbx_Copy6.Text + "%'  ").Result;

                }
                else
                {
                    templ = db.RunReader("select * from A_BATCH_D where BATCH_NO like '" + ReviCompcbx2.Text + "%' and PRV_NO like '" + ReviCompcbx_Copy5.Text + "%' and CLAIM_NO like '" + ReviCompcbx_Copy6.Text + "%'  order by BATCH_NO").Result;
                    temp2 = db.RunReader("select DISTINCT CLAIM_NO from A_BATCH_D where BATCH_NO like '" + ReviCompcbx2.Text + "%' and PRV_NO like '" + ReviCompcbx_Copy5.Text + "%' and CLAIM_NO like '" + ReviCompcbx_Copy6.Text + "%' ").Result;

                    count_2dwyaa = db.RunReader("select DISTINCT MED_CODE from A_BATCH_D where BATCH_NO like '" + ReviCompcbx2.Text + "%' and PRV_NO like '" + ReviCompcbx_Copy5.Text + "%' and CLAIM_NO like '" + ReviCompcbx_Copy6.Text + "%' ").Result;
                }
                if (templ.Rows.Count == 0) MessageBox.Show("لا يوجد خصومات");
                else
                {

                    templ.Columns[0].ColumnName = "BATCH-NO";
                    templ.Columns[1].ColumnName = "PRV-NO";
                    templ.Columns[2].ColumnName = "PRV-NAME";
                    templ.Columns[3].ColumnName = "PRV-BRANCH-NO";
                    templ.Columns[4].ColumnName = "PRV-BRANCH-NAME";
                    templ.Columns[5].ColumnName = "CARD-ID";
                    templ.Columns[6].ColumnName = "CLAIM-NO";
                    templ.Columns[7].ColumnName = "MED-CODE";
                    templ.Columns[8].ColumnName = "MED-NAME";
                    templ.Columns[9].ColumnName = "SYSTEM-AMT";
                    templ.Columns[10].ColumnName = "CLAIM-AMT";
                    templ.Columns[11].ColumnName = "DIFFERENCE-AMT";
                    templ.Columns[12].ColumnName = "DISC-CODE";
                    templ.Columns[13].ColumnName = "GROUP";
                    templ.Columns[14].ColumnName = "UNIT";





                    double system_amt = 0, check_amt = 0, diff = 0, zq4 = 0, zq5 = 0, zq6 = 0, zq7 = 0, zq8 = 0, zq9 = 0, zq10 = 0, zq11 = 0, zq12 = 0, zq13 = 0, zq14 = 0, zq15 = 0, zq16 = 0, zq17 = 0, zq18 = 0, zq19 = 0, zq20 = 0, zq21 = 0, zq22 = 0, zq23 = 0, zq24 = 0, zq25 = 0, zq26 = 0;
                    foreach (DataRow row in templ.Rows)
                    {
                        system_amt = system_amt + Convert.ToDouble(row[9].ToString());
                        check_amt = check_amt + Convert.ToDouble(row[10].ToString());
                        diff = diff + Convert.ToDouble(row[11].ToString());
                        if (row[13].ToString() == "خصم خطاء - دواء محلى ومحتسب مستورد") zq4 = zq4 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "أخرى") zq5 = zq5 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "فروق تحمل") zq6 = zq6 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "خطاء نقل") zq7 = zq7 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "أدوية مكتوبة بخط اليد") zq8 = zq8 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "خطا تسعير") zq9 = zq9 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "أدوية غير موصوفة") zq10 = zq10 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "ادوية غير واضحة") zq11 = zq11 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "أسعار معدلة") zq12 = zq12 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "صرف بديل أرخص") zq13 = zq13 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "خصم خطاء - دواء مستورد ومحتسب محلى") zq14 = zq14 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "صرف بديل اغلي") zq15 = zq15 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "صرف تركيز اعلي") zq16 = zq16 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "خطاء جمع") zq17 = zq17 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "فروق أسعار") zq18 = zq18 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "تعديل وحده وسعر") zq19 = zq19 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "جرعات زائدة") zq20 = zq20 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "موافقة") zq21 = zq21 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "أدوية غير مغطاة") zq22 = zq22 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "إسم بدون إستمارة") zq23 = zq23 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "تصفير دواء") zq24 = zq24 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "تعديل وحده وسعر") zq25 = zq25 + Convert.ToDouble(row[11].ToString());
                        else if (row[13].ToString() == "غير مغطاة") zq26 = zq26 + Convert.ToDouble(row[11].ToString());



                    }



                    Sumzq1.Text = system_amt.ToString();
                    Sumzq2.Text = check_amt.ToString();
                    claimsum_Copy.Text = templ.Rows.Count.ToString();
                    claimsum_Copy1.Text = count_2dwyaa.Rows.Count.ToString();
                    Sumzq3.Text = diff.ToString();
                    Sumzq4.Text = zq4.ToString();
                    Sumzq5.Text = zq5.ToString();
                    Sumzq6.Text = zq6.ToString();
                    Sumzq7.Text = zq7.ToString();
                    Sumzq8.Text = zq8.ToString();
                    Sumzq9.Text = zq9.ToString();
                    Sumzq10.Text = zq10.ToString();
                    Sumzq11.Text = zq11.ToString();
                    Sumzq12.Text = zq12.ToString();
                    Sumzq13.Text = zq13.ToString();
                    Sumzq14.Text = zq14.ToString();
                    Sumzq15.Text = zq15.ToString();
                    Sumzq16.Text = zq16.ToString();
                    Sumzq17.Text = zq17.ToString();
                    Sumzq18.Text = zq18.ToString();
                    Sumzq19.Text = zq19.ToString();
                    Sumzq20.Text = zq20.ToString();
                    Sumzq21.Text = zq21.ToString();
                    Sumzq22.Text = zq22.ToString();
                    Sumzq23.Text = zq23.ToString();
                    Sumzq24.Text = zq24.ToString();
                    Sumzq25.Text = zq25.ToString();
                    Sumzq26.Text = zq26.ToString();
                    var bc = new BrushConverter();
                    if (zq4 != 0) Sumzq4.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq5 != 0) Sumzq5.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq6 != 0) Sumzq6.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq7 != 0) Sumzq7.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq8 != 0) Sumzq8.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq9 != 0) Sumzq9.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq10 != 0) Sumzq10.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq11 != 0) Sumzq11.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq12 != 0) Sumzq12.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq13 != 0) Sumzq13.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq14 != 0) Sumzq14.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq15 != 0) Sumzq15.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq16 != 0) Sumzq16.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq17 != 0) Sumzq17.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq18 != 0) Sumzq18.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq19 != 0) Sumzq19.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq20 != 0) Sumzq20.Background = (Brush)bc.ConvertFrom("#3b5998");
                    //  if (zq21 != 0) Sumzq21.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq22 != 0) Sumzq22.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq23 != 0) Sumzq23.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq24 != 0) Sumzq24.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq25 != 0) Sumzq25.Background = (Brush)bc.ConvertFrom("#3b5998");
                    if (zq26 != 0) Sumzq26.Background = (Brush)bc.ConvertFrom("#3b5998");
                    claimsum_Copy1.Background = (Brush)bc.ConvertFrom("#3b5998");
                    Sumzq1.Background = (Brush)bc.ConvertFrom("#3b5998");
                    Sumzq2.Background = (Brush)bc.ConvertFrom("#3b5998");
                    Sumzq3.Background = (Brush)bc.ConvertFrom("#3b5998");
                    claimsum.Background = (Brush)bc.ConvertFrom("#3b5998");
                    claimsum_Copy.Background = (Brush)bc.ConvertFrom("#3b5998");
                    claimsum.Text = temp2.Rows.Count.ToString();

                    templ.SetColumnsOrder("CARD-ID", "CLAIM-NO", "MED-CODE", "MED-NAME", "SYSTEM-AMT", "CLAIM-AMT", "DIFFERENCE-AMT", "DISC-CODE", "GROUP", "UNIT", "BATCH-NO", "PRV-NO", "PRV-NAME", "PRV-BRANCH-NO", "PRV-BRANCH-NAME");




                    templ.Columns[0].ColumnName = "رقم كارت المريض";
                    templ.Columns[1].ColumnName = "رقم الطالبة";
                    templ.Columns[2].ColumnName = "كود الدواء";
                    templ.Columns[3].ColumnName = "اسم الدواء";
                    templ.Columns[4].ColumnName = "مبلغ قبل المراجعة";
                    templ.Columns[5].ColumnName = "مبلغ بعد المراجعة";
                    templ.Columns[6].ColumnName = "مبلغ الخصم";
                    templ.Columns[7].ColumnName = "كود الخصم";
                    templ.Columns[8].ColumnName = "الخصم";
                    templ.Columns[9].ColumnName = "عدد الوحدات";
                    templ.Columns[10].ColumnName = "رقم الباتش";
                    templ.Columns[11].ColumnName = "كود مقدم الخدمة";
                    templ.Columns[12].ColumnName = "اسم مقدم الخدمة";
                    templ.Columns[13].ColumnName = "كود فرع مقدم الخدمة";
                    templ.Columns[14].ColumnName = "اسم فرع مقدم الخدمة";

                    ReviData2.ItemsSource = templ.DefaultView;
                }
            }
            else
                MessageBox.Show("برجاء ملأ بيانات");
        }

        private void imgsearcha_Copaay_Copy4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ReviCompcbx_Copy5.ItemsSource = db.RunReader("  select  distinct USER_CO, USER_N from V_PROVIDERS WHERE KIND_NO IN (1,3,4) and USER_CO  LIKE '%" + ReviCompcbx_Copy5.Text + "%' or USER_N LIKE '%" + ReviCompcbx_Copy5.Text + "%'  ORDER BY USER_CO ").Result.DefaultView;
            ReviCompcbx_Copy5.IsDropDownOpen = true;
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            ReviCompcbx_Copy5.Text = "";
            ReviCompcbx2.Text = "";
            ReviCompcbx_Copy6.Text = "";
            ReviData2.ItemsSource = null;
            Sumzq1.Text = "";
            Sumzq2.Text = "";
            claimsum_Copy.Text = "";
            claimsum_Copy1.Text = "";
            Sumzq3.Text = "";
            Sumzq4.Text = "";
            Sumzq5.Text = "";
            Sumzq6.Text = "";
            Sumzq7.Text = "";
            Sumzq8.Text = "";
            Sumzq9.Text = "";
            Sumzq10.Text = "";
            Sumzq11.Text = "";
            Sumzq12.Text = "";
            Sumzq13.Text = "";
            Sumzq14.Text = "";
            Sumzq15.Text = "";
            Sumzq16.Text = "";
            Sumzq17.Text = "";
            Sumzq18.Text = "";
            Sumzq19.Text = "";
            Sumzq20.Text = "";
            Sumzq21.Text = "";
            Sumzq22.Text = "";
            Sumzq23.Text = "";
            Sumzq24.Text = "";
            Sumzq25.Text = "";
            Sumzq26.Text = "";
            claimsum.Text = "";
            Sumzq4.Background = Brushes.White;
            Sumzq5.Background = Brushes.White;
            Sumzq6.Background = Brushes.White;
            Sumzq7.Background = Brushes.White;
            Sumzq8.Background = Brushes.White;
            Sumzq9.Background = Brushes.White;
            Sumzq10.Background = Brushes.White;
            Sumzq11.Background = Brushes.White;
            Sumzq12.Background = Brushes.White;
            Sumzq13.Background = Brushes.White;
            Sumzq14.Background = Brushes.White;
            Sumzq15.Background = Brushes.White;
            Sumzq16.Background = Brushes.White;
            Sumzq17.Background = Brushes.White;
            Sumzq18.Background = Brushes.White;
            Sumzq19.Background = Brushes.White;
            Sumzq20.Background = Brushes.White;
            Sumzq21.Background = Brushes.White;
            Sumzq22.Background = Brushes.White;
            Sumzq23.Background = Brushes.White;
            Sumzq24.Background = Brushes.White;
            Sumzq25.Background = Brushes.White;
            Sumzq26.Background = Brushes.White;
            Sumzq1.Background = Brushes.White;
            Sumzq2.Background = Brushes.White;
            Sumzq3.Background = Brushes.White;
            claimsum.Background = Brushes.White;
            claimsum_Copy.Background = Brushes.White;
            claimsum_Copy1.Background = Brushes.White;
        }


        #endregion

        //final
        private void superGroupCodeSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string input = superGroupCodeCombo.Text.ToString();
            superGroupCodeCombo.ItemsSource = Medicie.find_super_group(input).DefaultView;
            superGroupCodeCombo.IsDropDownOpen = true;
        }

        //final
        private void IndemnityCardComboz_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string DateFrom = dtpFromz.Text;
                string DateTo = dtpToz.Text;

                string CardNum = IndemnityCardComboz.Text.ToString();
                if (UserType == "hr")
                {

                    string[] arr = CardNum.Split('-');
                    string comp = arr[0].ToString();
                    string compid = report.get_comp_id(UserCompany);
                    if (comp == compid)
                    {
                        if (DateFrom == "" || DateTo == "")
                        {
                            MessageBox.Show("من فضلك اختر تاريخ");
                        }
                        else
                        {
                            List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCardNoSearch(DateFrom, DateTo, CardNum);
                            if (Indemnities == null)
                            {
                                MessageBox.Show("لا توجد بيانات");
                                IndemnityGridz.Visibility = Visibility.Hidden;
                                IndemnityGridz.ItemsSource = null;
                                indemnityItmCounttxtz.Content = 0;
                            }
                            else
                            {
                                IndemnityGridz.Visibility = Visibility.Visible;
                                IndemnityGridz.ItemsSource = Indemnities;
                                indemnityItmCounttxtz.Content = Indemnities.Count.ToString();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("غير مسموح بهذه الشركة");
                    }
                }
                else
                {
                    List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCardNoSearch(DateFrom, DateTo, CardNum);
                    if (Indemnities == null)
                    {
                        MessageBox.Show("لا توجد بيانات");
                    }
                    else
                    {
                        IndemnityGridz.Visibility = Visibility.Visible;
                        IndemnityGridz.ItemsSource = Indemnities;
                        indemnityItmCounttxtz.Content = Indemnities.Count.ToString();
                    }
                }
            }
            catch { }
        }
        //final
        private void superGroupCodeCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                int code = Convert.ToInt32(superGroupCodeCombo.Text.ToString());
                System.Data.DataTable data = Medicie.find_super_group(code);
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات");
                }
                else
                {
                    finalMedicineGrid.ItemsSource = data.DefaultView;
                    finalMedicineGrid.Columns[0].IsReadOnly = true;
                    finalMedicineGrid.Columns[3].IsReadOnly = true;
                }
            }
            catch { }
        }

        private void indemnitynewSrchBtnz_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (User.Type != "hr")
                {

                    IndemnityCompanyComboz.Text = "";
                }
                dtpFromz.Text = "";
                dtpToz.Text = "";
                txtCardNumz.Text = "";
                IndemnityGridz.ItemsSource = null;
                indemnitycardtxt.Text = "";
                IndemnityCardComboz.Text = "";
                indemnityItmCounttxtz.Content = "0";
            }
            catch { }
        }
        //aya
        private void indemnitycardtxt_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string DateFrom = dtpFromz.Text;
            string DateTo = dtpToz.Text;

            string CardNum = dataset_emp_cardzz.Tables[0].Rows[indemnitycardtxt.SelectedIndex][0].ToString();
            try
            {
                if (UserType == "hr")
                {
                    string[] arr = CardNum.Split('-');
                    string comp = arr[0].ToString();
                    string compid = report.get_comp_id(UserCompany);
                    if (comp == compid)
                    {
                        List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCardNoSearch(DateFrom, DateTo, CardNum);
                        if (Indemnities == null)
                        {
                            MessageBox.Show("لا توجد بيانات");
                        }
                        else
                        {
                            IndemnityGridz.Visibility = Visibility.Visible;
                            IndemnityGridz.ItemsSource = Indemnities;
                            indemnityItmCounttxtz.Content = Indemnities.Count.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("غير مسموح بهذه الشركة");
                    }
                }
                else
                {
                    List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCardNoSearch(DateFrom, DateTo, CardNum);
                    if (Indemnities == null)
                    {
                        MessageBox.Show("لا توجد بيانات");
                    }
                    else
                    {
                        IndemnityGridz.Visibility = Visibility.Visible;
                        IndemnityGridz.ItemsSource = Indemnities;
                        indemnityItmCounttxtz.Content = Indemnities.Count.ToString();
                    }
                }
            }
            catch { }
        }
        //aya
        private void infocardcombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            List<PrintingData> Branches;
            int cardvalidate = 0;
            int underprint = 0;
            int underdeliver = 0;
            int carddeliver = 0;
            try
            {
                string CardNo = dataset_emp_cardzz.Tables[0].Rows[infocardcombo.SelectedIndex][0].ToString();
                if (UserType == "hr")
                {
                    string comp = report.get_comp_id(UserCompany);
                    string[] arr = CardNo.Split('-');
                    string compid = arr[0].ToString();
                    if (comp == compid)
                    {
                        Branches = printserv.SelectAllEmployees_For_SearchAboutCard(CardNo);
                        cardvalidate = client.validate_CardInCompEmployees(CardNo);
                        underprint = client.validate_cardInPrinting(CardNo);
                        underdeliver = client.validate_card_InPrintingDeliverState(CardNo);
                        carddeliver = client.validate_cardDelivery(CardNo);

                        {
                            if (cardvalidate >= 1)
                            {
                                if (underprint >= 1)
                                {
                                    if (underdeliver >= 1)
                                    {
                                        if (carddeliver >= 1)
                                        {
                                            MessageBox.Show("تم تسليم الكارت");
                                        }
                                    }
                                    else
                                        MessageBox.Show("الكارت تحت التسليم");

                                }
                                else
                                    MessageBox.Show("الكارت تحت الطباعة");
                            }
                            else
                            {
                                MessageBox.Show("الكارت تحت التسجيل");
                            }
                        }
                        if (Branches != null)
                        {
                            InfoGrid.ItemsSource = Branches;
                            InfoGrid.Columns[0].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[3].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[4].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[5].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[13].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[14].Visibility = Visibility.Hidden;
                            infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                        }
                        else
                        {
                            MessageBox.Show("لا توجد بيانات");
                            InfoGrid.ItemsSource = null;
                            infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("غير مسموح");
                    }
                }
                else
                {
                    Branches = printserv.SelectAllEmployees_For_SearchAboutCard(CardNo);
                    cardvalidate = client.validate_CardInCompEmployees(CardNo);
                    underprint = client.validate_cardInPrinting(CardNo);
                    underdeliver = client.validate_card_InPrintingDeliverState(CardNo);
                    carddeliver = client.validate_cardDelivery(CardNo);

                    {
                        if (cardvalidate >= 1)
                        {
                            if (underprint >= 1)
                            {
                                if (underdeliver >= 1)
                                {
                                    if (carddeliver >= 1)
                                    {
                                        MessageBox.Show("تم تسليم الكارت");
                                    }
                                }
                                else
                                    MessageBox.Show("الكارت تحت التسليم");

                            }
                            else
                                MessageBox.Show("الكارت تحت الطباعة");
                        }
                        else
                        {
                            MessageBox.Show("الكارت تحت التسجيل");
                        }
                    }
                    if (Branches != null)
                    {
                        InfoGrid.ItemsSource = Branches;
                        InfoGrid.Columns[0].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[3].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[4].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[5].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[13].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[14].Visibility = Visibility.Hidden;
                        infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                    }
                    else
                    {
                        MessageBox.Show("لا توجد بيانات");
                        InfoGrid.ItemsSource = null;
                        infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                    }
                }
            }
            catch { }
        }
        //aya
        private void approvalcardcombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string CardNo = dataset_emp_cardzz.Tables[0].Rows[approvalcardcombo.SelectedIndex][0].ToString();
            int comp_emp = 0;
            int card_approve = 0;
            if (UserType == "hr")
            {
                string[] arr = CardNo.Split('-');
                string comp = arr[0].ToString();
                if (comp == CompanyCode)
                {
                    comp_emp = client.validate_card_num(CardNo);
                    card_approve = client.validate_card_approval(CardNo);
                    if (comp_emp >= 1)
                    {
                        if (card_approve >= 1)
                        {
                            string value_PatiantName = ApprovaltxtCardNumz.Text;
                            List<EmpApprovalData> Branches = emp.SelectAllApprovals(CardNo, value_PatiantName);
                            approvalGridz.ItemsSource = Branches;
                            totalApprovalCountz.Content = client.count_approve(CardNo).ToString();
                            approvalGridz.Columns[6].Visibility = Visibility.Hidden;
                            approvalGridz.Columns[7].Visibility = Visibility.Hidden;
                            approvalGridz.Columns[8].Visibility = Visibility.Hidden;
                            approvalGridz.Columns[9].Visibility = Visibility.Hidden;
                            approvalGridz.Columns[11].Visibility = Visibility.Hidden;
                            approvalItemCounttxtz.Content = Branches.Count.ToString();
                        }
                        else
                        {
                            MessageBox.Show("لا توجد موافقة لهذا الكارت");
                            ApprovaltxtCardNumz.Text = "";
                            approvalItemCounttxtz.Content = approvalGridz.Items.Count - 1;
                            totalApprovalCountz.Content = "0";
                        }
                    }
                    else
                    {
                        MessageBox.Show("رقم كارت غير موجود");
                        ApprovaltxtCardNumz.Text = "";
                        approvalItemCounttxtz.Content = approvalGridz.Items.Count - 1;
                        totalApprovalCountz.Content = "0";
                    }
                }
                else
                {
                    MessageBox.Show("غير مسموح ببيانات هذه الشركة");
                }

            }
            else
            {
                comp_emp = client.validate_card_num(CardNo);
                card_approve = client.validate_card_approval(CardNo);
                if (comp_emp >= 1)
                {
                    if (card_approve >= 1)
                    {
                        string value_PatiantName = ApprovaltxtCardNumz.Text;
                        List<EmpApprovalData> Branches = emp.SelectAllApprovals(CardNo, value_PatiantName);
                        approvalGridz.ItemsSource = Branches;
                        totalApprovalCountz.Content = client.count_approve(CardNo).ToString();
                        approvalGridz.Columns[6].Visibility = Visibility.Hidden;
                        approvalGridz.Columns[7].Visibility = Visibility.Hidden;
                        approvalGridz.Columns[8].Visibility = Visibility.Hidden;
                        approvalGridz.Columns[9].Visibility = Visibility.Hidden;
                        approvalGridz.Columns[11].Visibility = Visibility.Hidden;
                        approvalItemCounttxtz.Content = Branches.Count.ToString();
                    }
                    else
                    {
                        MessageBox.Show("لا توجد موافقة لهذا الكارت");
                        ApprovaltxtCardNumz.Text = "";
                        approvalItemCounttxtz.Content = approvalGridz.Items.Count - 1;
                        totalApprovalCountz.Content = "0";
                    }
                }
                else
                {
                    MessageBox.Show("رقم كارت غير موجود");
                    ApprovaltxtCardNumz.Text = "";
                    approvalItemCounttxt.Content = approvalGridz.Items.Count - 1;
                    totalApprovalCountz.Content = "0";
                }
            }
        }
        //aya

        private void networkcardcombo_SelectionChanged_1(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void infocardcombo_DropDownClosed(object sender, EventArgs e)
        {
            List<PrintingData> Branches;
            int cardvalidate = 0;
            int underprint = 0;
            int underdeliver = 0;
            int carddeliver = 0;
            try
            {
                string CardNo = dataset_emp_cardzz.Tables[0].Rows[infocardcombo.SelectedIndex][0].ToString();
                if (UserType == "hr")
                {
                    string comp = report.get_comp_id(UserCompany);
                    string[] arr = CardNo.Split('-');
                    string compid = arr[0].ToString();
                    if (comp == compid)
                    {
                        Branches = printserv.SelectAllEmployees_For_SearchAboutCard(CardNo);
                        cardvalidate = client.validate_CardInCompEmployees(CardNo);
                        underprint = client.validate_cardInPrinting(CardNo);
                        underdeliver = client.validate_card_InPrintingDeliverState(CardNo);
                        carddeliver = client.validate_cardDelivery(CardNo);

                        {
                            if (cardvalidate >= 1)
                            {
                                if (underprint >= 1)
                                {
                                    if (underdeliver >= 1)
                                    {
                                        if (carddeliver >= 1)
                                        {
                                            MessageBox.Show("تم تسليم الكارت");
                                        }
                                    }
                                    else
                                        MessageBox.Show("الكارت تحت التسليم");

                                }
                                else
                                    MessageBox.Show("الكارت تحت الطباعة");
                            }
                            else
                            {
                                MessageBox.Show("الكارت تحت التسجيل");
                            }
                        }
                        if (Branches != null)
                        {
                            InfoGrid.ItemsSource = Branches;
                            InfoGrid.Columns[0].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[3].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[4].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[5].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[13].Visibility = Visibility.Hidden;
                            InfoGrid.Columns[14].Visibility = Visibility.Hidden;
                            infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                        }
                        else
                        {
                            MessageBox.Show("لا توجد بيانات");
                            InfoGrid.ItemsSource = null;
                            infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("غير مسموح");
                    }
                }
                else
                {
                    Branches = printserv.SelectAllEmployees_For_SearchAboutCard(CardNo);
                    cardvalidate = client.validate_CardInCompEmployees(CardNo);
                    underprint = client.validate_cardInPrinting(CardNo);
                    underdeliver = client.validate_card_InPrintingDeliverState(CardNo);
                    carddeliver = client.validate_cardDelivery(CardNo);

                    {
                        if (cardvalidate >= 1)
                        {
                            if (underprint >= 1)
                            {
                                if (underdeliver >= 1)
                                {
                                    if (carddeliver >= 1)
                                    {
                                        MessageBox.Show("تم تسليم الكارت");
                                    }
                                }
                                else
                                    MessageBox.Show("الكارت تحت التسليم");

                            }
                            else
                                MessageBox.Show("الكارت تحت الطباعة");
                        }
                        else
                        {
                            MessageBox.Show("الكارت تحت التسجيل");
                        }
                    }
                    if (Branches != null)
                    {
                        InfoGrid.ItemsSource = Branches;
                        InfoGrid.Columns[0].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[3].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[4].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[5].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[13].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[14].Visibility = Visibility.Hidden;
                        infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                    }
                    else
                    {
                        MessageBox.Show("لا توجد بيانات");
                        InfoGrid.ItemsSource = null;
                        infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                    }
                }
            }
            catch { }
        }

        private void srchinfocard_Click(object sender, RoutedEventArgs e)
        {
            List<PrintingData> Branches;
            int cardvalidate = 0;
            int underprint = 0;
            int underdeliver = 0;
            int carddeliver = 0;
            try
            {
                string CardNo = infocardcombo.Text.ToString();
                if (UserType == "hr")
                {

                    string comp = report.get_comp_id(UserCompany);
                    string[] arr = CardNo.Split('-');
                    string compid = arr[0].ToString();
                    DataSet s = db_IRS.RunReaderds("select CARD_NO from IRS_EMPLOYEES where CARD_NO='" + infocardcombo.Text + "' ");
                    if (s.Tables[0].Rows.Count >= 1)
                    {
                        if (comp == compid)
                        {
                            Branches = printserv.SelectAllEmployees_For_SearchAboutCard(CardNo);
                            cardvalidate = client.validate_CardInCompEmployees(CardNo);
                            underprint = client.validate_cardInPrinting(CardNo);
                            underdeliver = client.validate_card_InPrintingDeliverState(CardNo);
                            carddeliver = client.validate_cardDelivery(CardNo);

                            {
                                if (cardvalidate >= 1)
                                {
                                    if (underprint >= 1)
                                    {
                                        if (underdeliver >= 1)
                                        {
                                            if (carddeliver >= 1)
                                            {
                                                MessageBox.Show("تم تسليم الكارت");
                                            }
                                        }
                                        else

                                            MessageBox.Show("الكارت تحت التسليم");

                                    }
                                    else
                                    {

                                        MessageBox.Show("الكارت تحت الطباعة");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("الكارت تحت التسجيل");
                                }
                            }
                            if (Branches != null)
                            {
                                InfoGrid.ItemsSource = Branches;
                                InfoGrid.Columns[0].Visibility = Visibility.Hidden;
                                InfoGrid.Columns[3].Visibility = Visibility.Hidden;
                                InfoGrid.Columns[4].Visibility = Visibility.Hidden;
                                InfoGrid.Columns[5].Visibility = Visibility.Hidden;
                                InfoGrid.Columns[13].Visibility = Visibility.Hidden;
                                InfoGrid.Columns[14].Visibility = Visibility.Hidden;
                                infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                            }
                            else
                            {
                                MessageBox.Show("لا توجد بيانات");
                                InfoGrid.ItemsSource = null;
                                infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                            }
                        }
                        else
                        {
                            MessageBox.Show("غير مسموح");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Operation" + "برجاء التاكد من صحة البيانات و مراجعة قسم ");
                    }
                }
                else
                {
                    Branches = printserv.SelectAllEmployees_For_SearchAboutCard(CardNo);
                    cardvalidate = client.validate_CardInCompEmployees(CardNo);
                    underprint = client.validate_cardInPrinting(CardNo);
                    underdeliver = client.validate_card_InPrintingDeliverState(CardNo);
                    carddeliver = client.validate_cardDelivery(CardNo);

                    {
                        if (cardvalidate >= 1)
                        {
                            if (underprint >= 1)
                            {
                                if (underdeliver >= 1)
                                {
                                    if (carddeliver >= 1)
                                    {
                                        MessageBox.Show("تم تسليم الكارت");
                                    }
                                }
                                else
                                    MessageBox.Show("الكارت تحت التسليم");

                            }
                            else
                                MessageBox.Show("الكارت تحت الطباعة");
                        }
                        else
                        {
                            MessageBox.Show("الكارت تحت التسجيل");
                        }
                    }
                    if (Branches != null)
                    {
                        InfoGrid.ItemsSource = Branches;
                        InfoGrid.Columns[0].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[3].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[4].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[5].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[13].Visibility = Visibility.Hidden;
                        InfoGrid.Columns[14].Visibility = Visibility.Hidden;
                        infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                    }
                    else
                    {
                        MessageBox.Show("لا توجد بيانات");
                        InfoGrid.ItemsSource = null;
                        infoItemCounttxt.Content = "Items Count : " + (InfoGrid.Items.Count - 1).ToString();
                    }
                }
            }
            catch { }
        }

        private void approvalsrchbtnz_Click(object sender, RoutedEventArgs e)
        {

            string CardNo = approvalcardcombo.Text.ToString();
            int comp_emp = 0;
            int card_approve = 0;
            if (UserType == "hr")
            {
                string[] arr = CardNo.Split('-');
                string comp = arr[0].ToString();
                if (User.CompanyID == CompanyCode)
                {
                    comp_emp = client.validate_card_num(CardNo);
                    card_approve = client.validate_card_approval(CardNo);
                    if (comp_emp >= 1)
                    {
                        if (card_approve >= 1)
                        {
                            string value_PatiantName = ApprovaltxtCardNumz.Text;
                            List<EmpApprovalData> Branches = emp.SelectAllApprovals(CardNo, value_PatiantName);
                            approvalGridz.ItemsSource = Branches;
                            totalApprovalCountz.Content = client.count_approve(CardNo).ToString();
                            approvalGridz.Columns[6].Visibility = Visibility.Hidden;
                            approvalGridz.Columns[7].Visibility = Visibility.Hidden;
                            approvalGridz.Columns[8].Visibility = Visibility.Hidden;
                            approvalGridz.Columns[9].Visibility = Visibility.Hidden;
                            //approvalGridz.Columns[11].Visibility = Visibility.Hidden;
                            approvalItemCounttxtz.Content = Branches.Count.ToString();
                        }
                        else
                        {
                            MessageBox.Show("لا توجد موافقة لهذا الكارت");
                            ApprovaltxtCardNumz.Text = "";
                            approvalItemCounttxtz.Content = approvalGridz.Items.Count - 1;
                            totalApprovalCountz.Content = "0";
                        }
                    }
                    else
                    {
                        MessageBox.Show("رقم كارت غير موجود");
                        ApprovaltxtCardNumz.Text = "";
                        approvalItemCounttxtz.Content = approvalGridz.Items.Count - 1;
                        totalApprovalCountz.Content = "0";
                    }
                }
                else
                {
                    MessageBox.Show("غير مسموح ببيانات هذه الشركة");
                }

            }
            else
            {
                comp_emp = client.validate_card_num(CardNo);
                card_approve = client.validate_card_approval(CardNo);
                if (comp_emp >= 1)
                {
                    if (card_approve >= 1)
                    {
                        string value_PatiantName = ApprovaltxtCardNumz.Text;
                        List<EmpApprovalData> Branches = emp.SelectAllApprovals(CardNo, value_PatiantName);
                        approvalGridz.ItemsSource = Branches;
                        totalApprovalCountz.Content = client.count_approve(CardNo).ToString();
                        approvalGridz.Columns[6].Visibility = Visibility.Hidden;
                        approvalGridz.Columns[7].Visibility = Visibility.Hidden;
                        approvalGridz.Columns[8].Visibility = Visibility.Hidden;
                        approvalGridz.Columns[9].Visibility = Visibility.Hidden;
                        approvalGridz.Columns[11].Visibility = Visibility.Hidden;
                        approvalItemCounttxtz.Content = Branches.Count.ToString();
                    }
                    else
                    {
                        MessageBox.Show("لا توجد موافقة لهذا الكارت");
                        ApprovaltxtCardNumz.Text = "";
                        approvalItemCounttxtz.Content = approvalGridz.Items.Count - 1;
                        totalApprovalCountz.Content = "0";
                    }
                }
                else
                {
                    MessageBox.Show("رقم كارت غير موجود");
                    ApprovaltxtCardNumz.Text = "";
                    approvalItemCounttxt.Content = approvalGridz.Items.Count - 1;
                    totalApprovalCountz.Content = "0";
                }
            }

        }

        private void indemnitysrchcardbtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string DateFrom = dtpFromz.Text;
                string DateTo = dtpToz.Text;

                string CardNum = indemnitycardtxt.Text.ToString();
                if (UserType == "hr")
                {
                    string[] arr = CardNum.Split('-');
                    string comp = arr[0].ToString();
                    string compid = report.get_comp_id(UserCompany);
                    if (comp == compid)
                    {
                        List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCardNoSearch(DateFrom, DateTo, CardNum);
                        if (Indemnities == null)
                        {
                            MessageBox.Show("لا توجد بيانات");
                        }
                        else
                        {
                            IndemnityGridz.Visibility = Visibility.Visible;
                            IndemnityGridz.ItemsSource = Indemnities;
                            indemnityItmCounttxtz.Content = Indemnities.Count.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("غير مسموح بهذه الشركة");
                    }
                }
                else
                {
                    List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCardNoSearch(DateFrom, DateTo, CardNum);
                    if (Indemnities == null)
                    {
                        MessageBox.Show("لا توجد بيانات");
                    }
                    else
                    {
                        IndemnityGridz.Visibility = Visibility.Visible;
                        IndemnityGridz.ItemsSource = Indemnities;
                        indemnityItmCounttxtz.Content = Indemnities.Count.ToString();
                    }
                }
            }
            catch { }
        }

        private void cbxstateCust_Copy_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
        #region milad




        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {

            //-------fill companies-------------
            List<ReallocationData> list = ReallocationServices.SelectAllCompanies();
            cmbCompanies.ItemsSource = list;
            //  ---fill Prov type---------------
            List<ReallocationData> provTypes = ReallocationServices.SelectAllProvTypes();
            cmbServProvType.ItemsSource = provTypes;

            //-----------fill letter types---------
            List<ReallocationData> letterTypes = ReallocationServices.SelectAllLetterTypes();
            cmbLetterTypes.ItemsSource = letterTypes;
            txtReallocate_Code.IsReadOnly = true;


            //===============================
            grbReply.Visibility = Visibility.Hidden;
            btnUpdate_Reallocation.IsEnabled = false;

        }

        private void btnNew_Reallocation_Click(object sender, RoutedEventArgs e)
        {
            btnAdd_Reallocation.IsEnabled = true;
            btnUpdate_Reallocation.IsEnabled = false;
            grbReply.Visibility = Visibility.Hidden;
            cmbCompanies.Text = ""; cmbEmpCard.Text = ""; cmbLetterTypes.Text = ""; cmbServProvider.Text = ""; cmbLetterTypes.Text = ""; txtServiceName.Text = ""; txtServicePrice.Text = ""; txtDiag.Text = ""; cmbServProvType.Text = "";
            dtpServiceDate.Text = "";
            txtReallocationNotes.Text = "";
            txtReallocate_Code.Text = "";
            txtSearch.Text = "";

            lblProviderNamea.Content = "";
            lblProviderTypeName.Content = "";
            lblLetterType.Content = "";
            lblEmployeeName.Content = "";
            lblCompanyNamea.Content = "";
            AgreementPic.Source = null;
            txtAgreementCost.Text = "";
            txtReplyNotes.Text = "";

        }

        private void cmbCompanies_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                cmbEmpCard.ItemsSource = null;
                //--------fill Emp Cards------------
                string CompId = cmbCompanies.Text;
                List<ReallocationData> Cards = ReallocationServices.SelectAllEmpCards(CompId);
                cmbEmpCard.ItemsSource = Cards;
            }
            catch { }
        }

        private void MovingMessangerBtn_Click(object sender, RoutedEventArgs e)
        {
            MovingFrm movF = new MovingFrm();
            movF.ShowDialog();
        }

        private void messConfTabBtn_Click(object sender, RoutedEventArgs e)
        {
            MessengerConfirmation confirm = new MessengerConfirmation();
            confirm.ShowDialog();
        }

        private void cmbCompanies_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                cmbEmpCard.ItemsSource = null;
                //--------fill Emp Cards------------
                string CompId = cmbCompanies.Text;
                List<ReallocationData> Cards = ReallocationServices.SelectAllEmpCards(CompId);
                cmbEmpCard.ItemsSource = Cards;
            }
            catch { }
        }

        private void btnAdd_Reallocation_Click(object sender, RoutedEventArgs e)
        {
            #region valid
            bool valid = false;
            if (dtpServiceDate.Text != "" && cmbCompanies.Text != "" && cmbEmpCard.Text != "" && cmbLetterTypes.Text != "" && cmbServProvider.Text != "" && cmbLetterTypes.Text != "" && txtServiceName.Text != "" && txtServicePrice.Text != "" && txtDiag.Text != "")
            {
                valid = true;
            }
            #endregion
            if (valid == true)
            {
                try
                {
                    ReallocationData obj = new ReallocationData();
                    obj.Id = int.Parse(cmbCompanies.Text);

                    obj.CARD_ID = cmbEmpCard.Text;
                    DateTime datet = dtpServiceDate.SelectedDate.Value.Date;
                    string ServiceDate = datet.ToString("dd-MMM-yy");

                    obj.SERV_DATE = ServiceDate;
                    DateTime now = DateTime.Now;
                    string custonDate = now.ToString("dd-MMM-yy");
                    obj.CREATED_DATE = custonDate;

                    obj.PRV_TYPE = int.Parse(cmbServProvType.Text);
                    obj.Prov_Code = int.Parse(cmbServProvider.Text);
                    obj.LETTER_ID = int.Parse(cmbLetterTypes.Text);
                    obj.SERV_NAME = txtServiceName.Text;
                    obj.SRV_PRICE = int.Parse(txtServicePrice.Text);
                    obj.DIAGNOSIS = txtDiag.Text;
                    obj.Nots = txtReallocationNotes.Text;

                    int affected = ReallocationServices.InsertReallocation(obj);

                    if (affected > 0)
                    {
                        #region GetCode and clear texts
                        string OperationCode = ReallocationServices.SelectReallocateCode(obj.Id, obj.CARD_ID, obj.PRV_TYPE, obj.Prov_Code);
                        DateTime today = DateTime.Now;
                        string CustomToday = today.ToString("ddMMyy");

                        string fullCode = (OperationCode + "" + CustomToday);
                        int affectedFullCode = ReallocationServices.UpdateFullCode(fullCode, OperationCode);
                        if (OperationCode == "")
                        {
                        }
                        else
                        {
                            //cmbCompanies.Text = ""; cmbEmpCard.Text = ""; cmbLetterTypes.Text = ""; cmbServProvider.Text = ""; cmbLetterTypes.Text = ""; txtServiceName.Text = ""; txtServicePrice.Text = ""; txtDiag.Text = ""; cmbServProvType.Text = ""; txtReallocationNotes.Text = "";
                            if (affectedFullCode > 0)
                            {
                                MessageBox.Show("تمت عملية الحفظ وكود العملية:" + fullCode);
                                txtReallocate_Code.Text = fullCode.ToString();
                            }

                        }
                        #endregion

                    }
                }
                catch { }
            }
            else
            {
                MessageBox.Show("املا كل الحقول من فضلك");
            }

        }

        private void btnUpdate_Reallocation_Click(object sender, RoutedEventArgs e)
        {

            #region valid
            bool valid = false;
            if (dtpServiceDate.Text != "" && txtReallocate_Code.Text != "" && cmbCompanies.Text != "" && cmbEmpCard.Text != "" && cmbLetterTypes.Text != "" && cmbServProvider.Text != "" && cmbLetterTypes.Text != "" && txtServiceName.Text != "" && txtServicePrice.Text != "" && txtDiag.Text != "")
            {
                valid = true;
            }
            #endregion
            if (valid == true)
            {
                try
                {
                    Int64 ReallocateCode = Int64.Parse(txtReallocate_Code.Text);
                    ReallocationData obj = new ReallocationData();
                    obj.Id = int.Parse(cmbCompanies.Text);
                    obj.CARD_ID = cmbEmpCard.Text;
                    DateTime datet = dtpServiceDate.SelectedDate.Value.Date;
                    string ServiceDate = datet.ToString("dd-MMM-yy");
                    obj.SERV_DATE = ServiceDate;

                    DateTime now = DateTime.Now;
                    string customDate = now.ToString("dd-MMM-yy");
                    obj.UPDATED_DATE = customDate;

                    obj.PRV_TYPE = int.Parse(cmbServProvType.Text);
                    obj.Prov_Code = int.Parse(cmbServProvider.Text);
                    obj.LETTER_ID = int.Parse(cmbLetterTypes.Text);
                    obj.SERV_NAME = txtServiceName.Text;
                    obj.SRV_PRICE = int.Parse(txtServicePrice.Text);
                    obj.DIAGNOSIS = txtDiag.Text;
                    obj.Nots = txtReallocationNotes.Text;
                    obj.REPLY = txtReplyNotes.Text;
                    try
                    {
                        obj.AGREAMENT_COST = int.Parse(txtAgreementCost.Text);
                    }
                    catch { }

                    ReallocationData OldReallocate = ReallocationServices.SelectReallocateById(ReallocateCode);
                    if (Int64.Parse(OldReallocate.FullCode.ToString()) == Int64.Parse(ReallocateCode.ToString()))//---means that it is exist so update it
                    {
                        if (path != "")
                        {
                            ReallocationServices.UpdateReallocationPic(ReallocateCode, path);
                        }
                        int affected = ReallocationServices.UpdateReallocation(obj, ReallocateCode);
                        if (affected > 0)
                        {
                            MessageBox.Show("تم التحديث بنجاح", "Success");
                            //grbReply.Visibility = Visibility.Hidden;
                            //btnUpdate_Reallocation.IsEnabled = false;
                            //cmbCompanies.Text = ""; txtReallocationNotes.Text=""; cmbEmpCard.Text = ""; cmbLetterTypes.Text = ""; cmbServProvider.Text = ""; cmbLetterTypes.Text = ""; txtServiceName.Text = ""; txtServicePrice.Text = ""; txtDiag.Text = "";
                            //dtpServiceDate.Text = ""; cmbServProvType.Text = "";
                            //AgreementPic.Source = null;
                            //txtAgreementCost.Text = "";
                            //txtReplyNotes.Text = "";
                        }
                    }
                }
                catch { }
            }
            else
            {
                MessageBox.Show("املا كل الحقول من فضلك");
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            btnAdd_Reallocation.IsEnabled = false;
            if (txtSearch.Text != "")
            {
                try
                {
                    Int64 ReallocateCode = Int64.Parse(txtSearch.Text);
                    ReallocationData obj = ReallocationServices.SelectReallocateById(ReallocateCode);
                    if (obj != null)
                    {
                        btnUpdate_Reallocation.IsEnabled = true;
                        txtReallocate_Code.Text = obj.FullCode.ToString();
                        cmbCompanies.Text = obj.Id.ToString();
                        cmbEmpCard.Text = obj.CARD_ID;
                        cmbLetterTypes.Text = obj.LETTER_ID.ToString();
                        cmbServProvider.Text = obj.Prov_Code.ToString();
                        cmbServProvType.Text = obj.PRV_TYPE.ToString();
                        txtServiceName.Text = obj.SERV_NAME.ToString();
                        txtServicePrice.Text = obj.SRV_PRICE.ToString();
                        txtDiag.Text = obj.DIAGNOSIS.ToString();
                        txtReallocationNotes.Text = obj.Nots.ToString();
                        dtpServiceDate.Text = obj.SERV_DATE;

                        txtAgreementCost.Text = obj.AGREAMENT_COST.ToString();
                        txtReplyNotes.Text = obj.REPLY;
                        grbReply.Visibility = Visibility.Visible;

                        try
                        {
                            AgreementPic.Source = ReallocationServices.BitmapImageFromBytes(obj.REPLY_PICTURE);
                        }
                        catch { }


                    }
                    else
                    {
                        MessageBox.Show("غير موجود");
                        btnAdd_Reallocation.IsEnabled = true;
                    }
                }
                catch
                {
                    MessageBox.Show("رقم كبير جدا");
                }

            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;

            String newText = String.Empty;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsDigit(c) || Char.IsControl(c)) newText += c;
            }

            textBox.Text = newText;

            textBox.SelectionStart = selectionStart <= textBox.Text.Length ?
                selectionStart : textBox.Text.Length;
        }

        private void ReviCompcbx_Copy5_DropDownClosed(object sender, EventArgs e)
        {
            ReviCompcbx2.ItemsSource = db.RunReader("select DISTINCT BATCH_NO  from A_BATCH_D where PRV_NO='" + ReviCompcbx_Copy5.Text + "'  order by BATCH_NO").Result.DefaultView;
            ReviCompcbx_Copy6.ItemsSource = db.RunReader("select DISTINCT CLAIM_NO  from A_BATCH_D where PRV_NO='" + ReviCompcbx_Copy5.Text + "'   order by CLAIM_NO").Result.DefaultView;

        }

        private void ReviCompcbx_Copy5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ReviCompcbx2.ItemsSource = db.RunReader("select DISTINCT BATCH_NO  from A_BATCH_D where PRV_NO='" + ReviCompcbx_Copy5.Text + "'  order by BATCH_NO").Result.DefaultView;
                ReviCompcbx_Copy6.ItemsSource = db.RunReader("select DISTINCT CLAIM_NO  from A_BATCH_D where PRV_NO='" + ReviCompcbx_Copy5.Text + "'   order by CLAIM_NO").Result.DefaultView;

            }
        }

        private void approveRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                approveRequest app = new approveRequest(User.Name, User.CompanyName);
                app.ShowDialog();
            }
            catch { }
        }

        private void Image_Initialized(object sender, EventArgs e)
        {

        }
        string path = "";
        private void btnUploadPhoto_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                // path = Path.GetFlowDirection(op);
                if (op.ShowDialog() == true)
                {
                    AgreementPic.Source = new BitmapImage(new Uri(op.FileName));
                    path = op.FileName;
                    System.Drawing.Bitmap newimg = new System.Drawing.Bitmap(path);

                }
            }
            catch { }

        }

        private void txtAgreementCost_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //=======Changed
        private void cmbCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ////---get Company Name
            try
            {
                List<ReallocationData> provNames = ReallocationServices.SelectAllCompanies();
                lblCompanyNamea.Content = provNames[cmbCompanies.SelectedIndex].Name;
            }
            catch { }
        }

        private void cmbEmpCard_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //--------get Emp Cards------------
                string CompId = cmbCompanies.Text;
                List<ReallocationData> Cards = ReallocationServices.SelectAllEmpCards(CompId);
                lblEmployeeName.Content = Cards[cmbEmpCard.SelectedIndex].EMP_ANAME_SC + "  " + Cards[cmbEmpCard.SelectedIndex].EMP_ANAME_ST + " " + Cards[cmbEmpCard.SelectedIndex].EMP_ANAME_TH;
            }
            catch { }
        }

        private void cmbLetterTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //---dispaly letter Name
            try
            {
                List<ReallocationData> letterTypes = ReallocationServices.SelectAllLetterTypes();
                lblLetterType.Content = letterTypes[cmbLetterTypes.SelectedIndex].LetterName;
            }
            catch { }
        }
        List<ReallocationData> provNames;
        private void cmbServProvType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //  ---fill Prov Names---------------
            try
            {
                int TypeId = int.Parse(cmbServProvType.Text);
                provNames = ReallocationServices.SelectAllProviderNames(TypeId);
                cmbServProvider.ItemsSource = provNames;
            }
            catch { }
            try
            {
                //-----------dispaly provider Type Name---------
                List<ReallocationData> provTypes = ReallocationServices.SelectAllProvTypes();
                lblProviderNamea.Content = "";
                lblProviderTypeName.Content = provTypes[cmbServProvType.SelectedIndex].TYP_ANAME;
            }
            catch { }
        }

        private void cmbServProvider_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                lblProviderNamea.Content = provNames[cmbServProvider.SelectedIndex].Prov_Name;
            }
            catch { }
        }

        private void btnPrintLetter_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cmbServProvType_DropDownClosed(object sender, EventArgs e)
        {
            //  ---fill Prov Names---------------
            try
            {
                int TypeId = int.Parse(cmbServProvType.Text);
                provNames = ReallocationServices.SelectAllProviderNames(TypeId);
                cmbServProvider.ItemsSource = provNames;
            }
            catch { }
            try
            {
                //-----------dispaly provider Type Name---------
                List<ReallocationData> provTypes = ReallocationServices.SelectAllProvTypes();
                lblProviderNamea.Content = "";
                lblProviderTypeName.Content = provTypes[cmbServProvType.SelectedIndex].TYP_ANAME;
            }
            catch { }
        }

        private void cmbServProvider_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {


        }


        private void rdSearchByReallocateCode_Checked(object sender, RoutedEventArgs e)
        {
            canvSearchCode.Visibility = Visibility.Visible;
            canvSearchDate.Visibility = Visibility.Hidden;
            canvSearchCompany.Visibility = Visibility.Hidden;

            txtAgreementCostz.Text = "";
            txtReplyNotesz.Text = "";
            grdSearchResult.ItemsSource = null;
            grbReplyz.Visibility = Visibility.Hidden;
            btnUpdateReply.Visibility = Visibility.Hidden;
        }

        private void rdSearchByCompanyz_Checked(object sender, RoutedEventArgs e)
        {
            canvSearchCode.Visibility = Visibility.Hidden;
            canvSearchDate.Visibility = Visibility.Hidden;
            canvSearchCompany.Visibility = Visibility.Visible;

            txtAgreementCostz.Text = "";
            txtReplyNotesz.Text = "";
            grdSearchResult.ItemsSource = null;
            grbReplyz.Visibility = Visibility.Hidden;
            btnUpdateReply.Visibility = Visibility.Hidden;
        }

        private void rdSearchByReallocationDate_Checked(object sender, RoutedEventArgs e)
        {
            grdSearchResult.ItemsSource = null;
            canvSearchCode.Visibility = Visibility.Hidden;
            canvSearchDate.Visibility = Visibility.Visible;
            canvSearchCompany.Visibility = Visibility.Hidden;

            txtAgreementCostz.Text = "";
            txtReplyNotesz.Text = "";
            grdSearchResult.ItemsSource = null;
            grbReplyz.Visibility = Visibility.Hidden;
            btnUpdateReply.Visibility = Visibility.Hidden;
        }

        private void cmbCompaniesz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ////---get Company Name
            try
            {
                List<ReallocationData> provNames = ReallocationServices.SelectAllCompanies();
                lblCompanyNamezz.Content = provNames[cmbCompaniesz.SelectedIndex].Name;
            }
            catch { }
        }

        private void cmbCompaniesz_KeyUp(object sender, KeyEventArgs e)
        {
        }



        //------------Search Events---------------
        private void btnSearchByReallocationCode_Click(object sender, RoutedEventArgs e)
        {
            txtAgreementCostz.Text = "";
            txtReplyNotesz.Text = "";
            grdSearchResult.ItemsSource = null;
            grbReplyz.Visibility = Visibility.Hidden;

            grdSearchResult.ItemsSource = null;
            string SearchType = "FULLCODE";
            if (txtSearchByReallocationCode.Text != "")
            {
                Int64 ReallocationCode = Int64.Parse(txtSearchByReallocationCode.Text);
                List<ReallocationData> list = ReallocationServices.SelectReallocateBy(SearchType, ReallocationCode);
                if (list != null && list.Count > 0)
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Columns.Add("Full Code", typeof(string));
                    dt.Columns.Add("Letter Id", typeof(string));
                    dt.Columns.Add("Notes", typeof(string));
                    dt.Columns.Add("Provider Type", typeof(string));
                    dt.Columns.Add("Provider Code", typeof(string));
                    dt.Columns.Add("Service Date", typeof(string));
                    dt.Columns.Add("Service Price", typeof(string));
                    dt.Columns.Add("Reply", typeof(string));
                    dt.Columns.Add("Comp ID", typeof(string));
                    dt.Columns.Add("Created Date", typeof(string));
                    dt.Columns.Add("Created By", typeof(string));
                    for (int i = 0; i < list.Count; i++)
                    {
                        dt.Rows.Add(list[i].FullCode, list[i].LETTER_ID, list[i].Nots, list[i].PRV_TYPE, list[i].Prov_Code, list[i].SERV_DATE, list[i].SRV_PRICE, list[i].REPLY, list[i].Id, list[i].CREATED_DATE, list[i].CREATED_BY);
                    }
                    grdSearchResult.ItemsSource = dt.DefaultView;
                    grdSearchResult.IsReadOnly = true;
                }
                else
                {
                    MessageBox.Show("لا توجد نتائج مماثلة للبحث");
                    btnUpdateReply.Visibility = Visibility.Hidden;

                }
            }

        }



        private void btnSearchByCompany_Click(object sender, RoutedEventArgs e)
        {
            txtAgreementCostz.Text = "";
            txtReplyNotesz.Text = "";
            grdSearchResult.ItemsSource = null;
            grbReplyz.Visibility = Visibility.Hidden;

            grdSearchResult.ItemsSource = null;
            string SearchType = "COMP_ID";
            if (cmbCompaniesz.Text != "")
            {
                Int64 CompCode = Int64.Parse(cmbCompaniesz.Text);
                List<ReallocationData> list = ReallocationServices.SelectReallocateBy(SearchType, CompCode);
                System.Data.DataTable dt = new System.Data.DataTable();
                if (list != null && list.Count > 0)
                {
                    dt.Columns.Add("Full Code", typeof(string));
                    dt.Columns.Add("Letter Id", typeof(string));
                    dt.Columns.Add("Notes", typeof(string));
                    dt.Columns.Add("Provider Type", typeof(string));
                    dt.Columns.Add("Provider Code", typeof(string));
                    dt.Columns.Add("Service Date", typeof(string));
                    dt.Columns.Add("Service Price", typeof(string));
                    dt.Columns.Add("Reply", typeof(string));
                    dt.Columns.Add("Comp ID", typeof(string));
                    dt.Columns.Add("Created Date", typeof(string));
                    dt.Columns.Add("Created By", typeof(string));

                    for (int i = 0; i < list.Count; i++)
                    {
                        dt.Rows.Add(list[i].FullCode, list[i].LETTER_ID, list[i].Nots, list[i].PRV_TYPE, list[i].Prov_Code, list[i].SERV_DATE, list[i].SRV_PRICE, list[i].REPLY, list[i].Id, list[i].CREATED_DATE, list[i].CREATED_BY);
                    }
                    grdSearchResult.ItemsSource = dt.DefaultView;
                    grdSearchResult.IsReadOnly = true;
                }
                else
                {
                    MessageBox.Show("لا يوجد نتائج مماثلة للبحث");
                    btnUpdateReply.Visibility = Visibility.Hidden;
                }
            }
        }

        private void btnSearchByReallocationDate_Click(object sender, RoutedEventArgs e)
        {
            txtAgreementCostz.Text = "";
            txtReplyNotesz.Text = "";
            grdSearchResult.ItemsSource = null;
            grbReplyz.Visibility = Visibility.Hidden;

            grdSearchResult.ItemsSource = null;
            string SearchType = "CREATED_DATE";
            try
            {
                DateTime datet = dtpSearchByReallocationDate.SelectedDate.Value.Date;
                if (datet != null)
                {
                    string CreatedDate = datet.ToString("dd-MMM-yy");
                    List<ReallocationData> list = ReallocationServices.SelectReallocateByDate(SearchType, CreatedDate);
                    System.Data.DataTable dt = new System.Data.DataTable();
                    if (list != null && list.Count > 0)
                    {
                        dt.Columns.Add("Full Code", typeof(string));
                        dt.Columns.Add("Letter Id", typeof(string));
                        dt.Columns.Add("Notes", typeof(string));
                        dt.Columns.Add("Provider Type", typeof(string));
                        dt.Columns.Add("Provider Code", typeof(string));
                        dt.Columns.Add("Service Date", typeof(string));
                        dt.Columns.Add("Service Price", typeof(string));
                        dt.Columns.Add("Reply", typeof(string));
                        dt.Columns.Add("Comp ID", typeof(string));
                        dt.Columns.Add("Created Date", typeof(string));
                        dt.Columns.Add("Created By", typeof(string));

                        for (int i = 0; i < list.Count; i++)
                        {
                            dt.Rows.Add(list[i].FullCode, list[i].LETTER_ID, list[i].Nots, list[i].PRV_TYPE, list[i].Prov_Code, list[i].SERV_DATE, list[i].SRV_PRICE, list[i].REPLY, list[i].Id, list[i].CREATED_DATE, list[i].CREATED_BY);
                        }
                        grdSearchResult.ItemsSource = dt.DefaultView;
                        grdSearchResult.IsReadOnly = true;
                    }
                    else
                    {
                        MessageBox.Show("لا توجد نتائج مماثلة");
                        btnUpdateReply.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch { }
        }

        //====------ Update Reply Section ----========//
        string pathqq = "";
        private void btnUploadPhotoz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                if (op.ShowDialog() == true)
                {
                    AgreementPicz.Source = new BitmapImage(new Uri(op.FileName));
                    pathqq = op.FileName;
                    System.Drawing.Bitmap newimg = new System.Drawing.Bitmap(pathqq);

                }
            }
            catch { }
        }

        private void grdSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                AgreementPicz.Source = null;
                grbReplyz.Visibility = Visibility.Visible;
                btnUpdateReply.Visibility = Visibility.Visible;
                object item1 = grdSearchResult.SelectedItem;
                string fullCode = (grdSearchResult.SelectedCells[0].Column.GetCellContent(item1) as TextBlock).Text;
                Int64 FullCode = Int64.Parse(fullCode.ToString());
                ReallocationData obj = ReallocationServices.SelectReallocateById(FullCode);

                txtAgreementCostz.Text = obj.AGREAMENT_COST.ToString();
                txtReplyNotesz.Text = obj.REPLY.ToString();
                try
                {
                    AgreementPicz.Source = ReallocationServices.BitmapImageFromBytes(obj.REPLY_PICTURE);
                }
                catch { }
            }
            catch
            {

            }
        }

        private void btnUpdateReply_Click(object sender, RoutedEventArgs e)
        {
            object item1 = grdSearchResult.SelectedItem;
            string fullCode = (grdSearchResult.SelectedCells[0].Column.GetCellContent(item1) as TextBlock).Text;
            if (fullCode != "" && txtAgreementCostz.Text != "")
            {
                Int64 FullCode = Int64.Parse(fullCode.ToString());
                ReallocationData OldReallocate = ReallocationServices.SelectReallocateById(FullCode);
                if (Int64.Parse(OldReallocate.FullCode.ToString()) == Int64.Parse(FullCode.ToString()))//---means that it is exist so update it
                {
                    if (pathqq != "")
                    {
                        ReallocationServices.UpdateReallocationPic(FullCode, pathqq);
                    }
                    ReallocationData obj = new ReallocationData();
                    obj.REPLY = txtReplyNotesz.Text;
                    obj.AGREAMENT_COST = Int16.Parse(txtAgreementCostz.Text);
                    int affected = ReallocationServices.UpdateReallocationReply(obj, FullCode);
                    if (affected > 0)
                    {
                        MessageBox.Show("تم التحديث بنجاح", "Success");
                    }
                }
            }
            else
            {
                MessageBox.Show("املا كل البيانات من فضلك");
            }
        }

        private void txtSearchByReallocationCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;

            String newText = String.Empty;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsDigit(c) || Char.IsControl(c)) newText += c;
            }

            textBox.Text = newText;
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ?
                selectionStart : textBox.Text.Length;
        }

        private void txtAgreementCostz_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
            Int32 selectionStart = textBox.SelectionStart;
            Int32 selectionLength = textBox.SelectionLength;

            String newText = String.Empty;
            foreach (Char c in textBox.Text.ToCharArray())
            {
                if (Char.IsDigit(c) || Char.IsControl(c)) newText += c;
            }
            textBox.Text = newText;
            textBox.SelectionStart = selectionStart <= textBox.Text.Length ?
                selectionStart : textBox.Text.Length;
        }

        private void cmbServProvider_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                lblProviderNamea.Content = provNames[cmbServProvider.SelectedIndex].Prov_Name;
            }
            catch { }
        }
        private void rdSearchByCompany_Checkedzz(object sender, RoutedEventArgs e)
        {
            canvSearchCode.Visibility = Visibility.Hidden;
            canvSearchDate.Visibility = Visibility.Hidden;
            canvSearchCompany.Visibility = Visibility.Visible;

            txtAgreementCost.Text = "";
            txtReplyNotes.Text = "";
            grdSearchResult.ItemsSource = null;
            grbReply.Visibility = Visibility.Hidden;
            btnUpdateReply.Visibility = Visibility.Hidden;


        }


        #endregion
        private void approvalCardSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string card = ApprovaltxtCardNum.Text.ToString();
                fill_card(ApprovaltxtCardNum, card);
            }
            catch { }
        }


        private void ApprovaltxtCardNum_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string CardNo = "";
                int comp_emp = 0;
                int card_approve = 0;
                if (User.Type != "DMS Member")
                {
                    CardNo = ApprovaltxtCardNum.Text.ToString();
                    string[] arr = CardNo.Split('-');
                    string comp = arr[0].ToString();
                    if (comp == CompanyCode)
                    {
                        comp_emp = client.validate_card_num(CardNo);
                        card_approve = client.validate_card_approval(CardNo);
                        if (comp_emp >= 1)
                        {
                            if (card_approve >= 1)
                            {
                                string value_PatiantName = ApprovaltxtCardNum.Text;
                                List<EmpApprovalData> Branches = emp.SelectAllApprovals(CardNo, value_PatiantName);
                                approvalGrid.ItemsSource = Branches;
                                totalApproveCount.Content = client.count_approve(CardNo).ToString();
                                approvalGrid.Columns[6].Visibility = Visibility.Hidden;
                                approvalGrid.Columns[7].Visibility = Visibility.Hidden;
                                approvalGrid.Columns[8].Visibility = Visibility.Hidden;
                                approvalGrid.Columns[9].Visibility = Visibility.Hidden;
                                approvalGrid.Columns[11].Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                MessageBox.Show("لا توجد موافقة لهذا الكارت");
                                approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;
                                totalApproveCount.Content = "0";
                            }
                        }
                        else
                        {
                            MessageBox.Show("رقم كارت غير موجود");
                            approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;
                            totalApproveCount.Content = "0";
                        }
                    }
                    else
                    {
                        MessageBox.Show("غير مسموح ببيانات هذه الشركة");
                    }

                }
                else
                {
                    CardNo = ApprovaltxtCardNum.Text;
                    comp_emp = client.validate_card_num(CardNo);
                    card_approve = client.validate_card_approval(CardNo);
                    if (comp_emp >= 1)
                    {
                        if (card_approve >= 1)
                        {
                            string value_PatiantName = ApprovaltxtCardNum.Text;
                            List<EmpApprovalData> Branches = emp.SelectAllApprovals(CardNo, value_PatiantName);
                            approvalGrid.ItemsSource = Branches;
                            totalApproveCount.Content = client.count_approve(CardNo).ToString();
                            approvalGrid.Columns[6].Visibility = Visibility.Hidden;
                            approvalGrid.Columns[7].Visibility = Visibility.Hidden;
                            approvalGrid.Columns[8].Visibility = Visibility.Hidden;
                            approvalGrid.Columns[9].Visibility = Visibility.Hidden;
                            approvalGrid.Columns[11].Visibility = Visibility.Hidden;
                            approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;

                        }
                        else
                        {
                            MessageBox.Show("لا توجد موافقة لهذا الكارت");
                            approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;
                            totalApproveCount.Content = "0";
                        }
                    }
                    else
                    {
                        MessageBox.Show("رقم كارت غير موجود");
                        approvalItemCounttxt.Content = approvalGrid.Items.Count - 1;
                        totalApproveCount.Content = "0";
                    }

                }
            }
            catch { }
        }

        private void infoCardCompanyCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                int compid = Convert.ToInt32((infoCardCompanyCombo.Text.ToString()));
                fill_card(infocardcombo, compid);
            }
            catch { }
        }


        // 12 nov
        private void EditEmpSrchCompanyBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                fill_comp(empCompCombo, empCompCombo.Text.ToString());
                empCompCombo.IsDropDownOpen = true;
            }
            catch { }
        }
        //12 nov
        private void basicdataDemptCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string dept = basicdataDemptCombo.Text.ToString();
                DataSet empDT = db.RunReaderds("select name,code from agent where agent_dept='" + dept + "'");
                basicdataEmpCombo.ItemsSource = empDT.Tables[0].DefaultView;

            }
            catch { }
        }
        //12 nov
        private void basicdataEmpCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                //string selected = basicdataEmpCombo.SelectedItem.ToString();
                //string[] arr = selected.Split(' ');
                string name = basicdataEmpCombo.Text.ToString();
                string code = agent.get_emp_code(name).ToString();
                basicdataGroup.Header = name;
                empnametxt.Text = name;
                empcodetxt.Text = code;
                string password = agent.get_pasword(name);
                emppasstxt.Text = password;
                newDeptCombo.Text = basicdataDemptCombo.Text.ToString();
                string net = "";
                string app = "";
                string medic = "";
                string indem = "";
                string rep = "";
                string cheq = "";
                string online = "";
                string contr = "";
                string flag = "";
                string dept = "";
                string basic = "";
                string active = "";
                string company = "";
                string usertype = ""; string prints = "";
                string notes = ""; string cust = ""; string complains = "";
                string reports = ""; string stores = "";

                string mail = "";
                string complaindms = ""; string medicalmanage = ""; string hrreq = ""; string revise = "";
                System.Data.DataTable user = agent.get_employee_authority(name);
                try
                {
                    for (int i = 0; i < user.Rows.Count; i++)
                    {
                        net = user.Rows[i].ItemArray[4].ToString();
                        app = user.Rows[i].ItemArray[1].ToString();
                        cheq = user.Rows[i].ItemArray[2].ToString();
                        indem = user.Rows[i].ItemArray[3].ToString();
                        rep = user.Rows[i].ItemArray[5].ToString();
                        online = user.Rows[i].ItemArray[6].ToString();
                        medic = user.Rows[i].ItemArray[7].ToString();
                        contr = user.Rows[i].ItemArray[8].ToString();
                        dept = user.Rows[i].ItemArray[9].ToString();
                        flag = user.Rows[i].ItemArray[10].ToString();
                        basic = user.Rows[i].ItemArray[13].ToString();
                        active = user.Rows[i].ItemArray[14].ToString();
                        company = user.Rows[i].ItemArray[15].ToString();
                        usertype = user.Rows[i].ItemArray[16].ToString();
                        prints = user.Rows[i].ItemArray[17].ToString();
                        notes = user.Rows[i].ItemArray[18].ToString();
                        stores = user.Rows[i].ItemArray[19].ToString();
                        cust = user.Rows[i].ItemArray[20].ToString();
                        reports = user.Rows[i].ItemArray[21].ToString();
                        complains = user.Rows[i].ItemArray[22].ToString();
                        medicalmanage = user.Rows[i].ItemArray[24].ToString();
                        complaindms = user.Rows[i].ItemArray[25].ToString();
                        hrreq = user.Rows[i].ItemArray[26].ToString();
                        revise = user.Rows[i].ItemArray[27].ToString();
                        mail = user.Rows[i].ItemArray[28].ToString();
                        if (user.Rows[i].ItemArray[29].ToString() == "y")
                            rev_chk_Copy.IsChecked = true;
                        if (user.Rows[i].ItemArray[30].ToString() == "Y")
                            notiHrRequest1.IsChecked = true;
                    }
                }
                catch { }
                empcodetxt_Copy.Text = mail;
                empCompCombo.Text = company;
                if (revise == "y")
                {
                    rev_chk.IsChecked = true;
                }
                else
                {
                    rev_chk.IsChecked = false;
                }
                //======================
                if (hrreq == "y")
                {
                    hr_reqChk.IsChecked = true;
                }
                else
                {
                    hr_reqChk.IsChecked = false;
                }
                //=====================
                if (medicalmanage == "y")
                {
                    medical_manageChk.IsChecked = true;
                }
                else
                {
                    medical_manageChk.IsChecked = false;
                }
                //====================
                if (complaindms == "y")
                {
                    complain_dmsChk.IsChecked = true;
                }
                else
                {
                    complain_dmsChk.IsChecked = false;
                }
                //========================
                if (active == "y" || active == "Y")
                {
                    activeChk.IsChecked = true;
                }
                else
                    activeChk.IsChecked = false;

                if (flag == "y" || flag == "Y")
                {
                    empyesrb.IsChecked = true;
                }
                else if (flag == "N" || flag == "n")
                {
                    empnorb.IsChecked = true;
                }
                ////////
                if (cheq == "y" || cheq == "Y")
                {
                    chequesChk.IsChecked = true;
                }
                else if (cheq == "n" || cheq == "N")
                {
                    chequesChk.IsChecked = false;
                }
                //////////
                if (online == "y" || online == "Y")
                {
                    onlineChk.IsChecked = true;
                }
                else if (online == "N" || online == "n")
                {
                    onlineChk.IsChecked = false;
                }
                ///////////////////
                if (basic == "y" || basic == "Y")
                {
                    bscDtaChk.IsChecked = true;
                }
                else if (basic == "N" || basic == "n")
                {
                    bscDtaChk.IsChecked = false;
                }
                ///////////////////
                if (net == "y" || net == "Y")
                {
                    networkChk.IsChecked = true;
                }
                else if (net == "N" || net == "n")
                {
                    networkChk.IsChecked = false;
                }
                ///////////////////
                if (contr == "y" || contr == "Y")
                {
                    contractChk.IsChecked = true;
                }
                else if (contr == "N" || contr == "n")
                {
                    contractChk.IsChecked = false;
                }
                ///////////////////
                if (medic == "y" || medic == "Y")
                {
                    medicineChk.IsChecked = true;
                }
                else if (medic == "N" || medic == "n")
                {
                    medicineChk.IsChecked = false;
                }
                ///////////////////
                if (indem == "y" || indem == "Y")
                {
                    indemnityChk.IsChecked = true;
                }
                else if (indem == "N" || indem == "n")
                {
                    indemnityChk.IsChecked = false;
                }
                ///////////////////
                if (app == "y" || app == "Y")
                {
                    approvalChk.IsChecked = true;
                }
                else if (app == "N" || app == "n")
                {
                    approvalChk.IsChecked = false;
                }
                ///////////////////
                if (rep == "y" || rep == "Y")
                {
                    messangerChk.IsChecked = true;
                }
                else if (rep == "N" || rep == "n")
                {
                    messangerChk.IsChecked = false;
                }
                ///////////////////
                if (usertype == "hr")
                {
                    hrtyperb.IsChecked = true;
                }
                else if (usertype == "DMS Member")
                {
                    dmstyperb.IsChecked = true;
                }
                ///////////////////////////
                if (prints == "y")
                {
                    printChk.IsChecked = true;
                }
                else
                    printChk.IsChecked = false;
                ////////////////////////
                if (notes == "y")
                {
                    notebookChk.IsChecked = true;
                }
                else
                    notebookChk.IsChecked = false;
                /////////////////////////////
                if (reports == "y")
                {
                    reportChk.IsChecked = true;
                }
                else
                    reportChk.IsChecked = false;
                //////////////////////
                if (stores == "y")
                {
                    storeChk.IsChecked = true;
                }
                else
                    storeChk.IsChecked = false;
                ///////////////////////
                if (cust == "y")
                {
                    custChk.IsChecked = true;
                }
                else
                    custChk.IsChecked = false;
                ///////////////////////////
                if (complains == "y")
                {
                    complainChk.IsChecked = true;
                }
                else
                    complainChk.IsChecked = false;
            }
            catch { }
        }

        //12 nov
        private void EditEmpSrchEmpBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataSet empdata = db.RunReaderds("select name,code from agent where name like '%" + basicdataEmpCombo.Text + "%' or code like '%" + basicdataEmpCombo.Text + "%' and agent_dept='" + basicdataDemptCombo.Text.ToString() + "' ORDER BY NAME");
                basicdataEmpCombo.ItemsSource = empdata.Tables[0].DefaultView;
                basicdataEmpCombo.IsDropDownOpen = true;
            }
            catch { }
        }

        //12 nov
        private void employeeCareDeptCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                //  employeeCareEmpCombo.Items.Clear();
                string dept = employeeCareDeptCombo.Text.ToString();
                DataSet emptDT = db.RunReaderds("select name,code from agent where agent_dept='" + dept + "'");
                System.Data.DataTable empDT = agent.get_employees(dept);
                if (empDT == null)
                {
                    MessageBox.Show("لا يوجد موظفين في هذا القسم");
                }
                else
                {
                    employeeCareEmpCombo.ItemsSource = emptDT.Tables[0].DefaultView;
                }
            }
            catch { }
        }
        //12 nov
        private void employeeCareEmpCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string dept = employeeCareDeptCombo.Text.ToString();
                string emp = employeeCareEmpCombo.Text.ToString();
                System.Data.DataTable data = store.get_employee_care(emp, dept);
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات");
                }
                else
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        EmployeeCareGrid.ItemsSource = data.DefaultView;
                    }
                }
                employeeCareCounttxt.Content = "Items count : " + data.Rows.Count.ToString();
            }
            catch { }
        }
        //12 nov
        private void destroyFilterDeptCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string dept = destroyFilterDeptCombo.Text.ToString();
                DataSet empdata = db.RunReaderds("select code,name from agent where agent_dept ='" + dept + "'");

                if (empdata == null)
                {
                    MessageBox.Show("لا يوجد موظفين في هذا القسم");
                }
                else
                {
                    destroyFilterEmpCombo.ItemsSource = empdata.Tables[0].DefaultView;
                }
            }
            catch { }
        }

        //12 nov
        private void destroyFilterEmpCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string dept = destroyFilterDeptCombo.Text.ToString();
                string emp = destroyFilterEmpCombo.Text.ToString();
                System.Data.DataTable data = store.get_destroy(emp, dept);
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات");
                }
                else
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        destroyFilterGrid.ItemsSource = data.DefaultView;
                    }

                    destroyItemCount.Text = data.Rows.Count.ToString();
                    destroyCounttxt.Content = "Items count : " + data.Rows.Count.ToString();
                }
            }
            catch { }
        }

        //12 nov
        private void DeptFilterCombo_DropDownClosed(object sender, EventArgs e)
        {

            try
            {
                string dept = DeptFilterCombo.Text.ToString();
                System.Data.DataTable data = store.dept_filter(dept);
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show(" لا توجد بيانات");
                }
                else
                {

                    DeptFilerGrid.ItemsSource = data.DefaultView;
                }
                deptFilterCounttxt.Content = "Items count : " + data.Rows.Count.ToString();
            }
            catch { }
        }

        //12 nov
        private void empFilterDeptCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string dept = empFilterDeptCombo.Text.ToString();
                DataSet empdata = db.RunReaderds("select code,name from agent where agent_dept ='" + dept + "'");

                if (empdata == null)
                {
                    MessageBox.Show("لا يوجد موظفين في هذا القسم");
                }
                else
                {
                    empFilterEmpCombo.ItemsSource = empdata.Tables[0].DefaultView;
                }
            }
            catch { }
        }

        //12 nov
        private void empFilterEmpCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string dept = empFilterDeptCombo.Text.ToString();
                string emp = empFilterEmpCombo.Text.ToString();
                System.Data.DataTable data = store.emp_filter(emp);
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show(" لا توجد بيانات");
                }
                else
                {
                    EmployeeFilterGrid.ItemsSource = data.DefaultView;
                }
                empFilterCounttxt.Content = "Items count : " + data.Rows.Count.ToString();
            }
            catch { }
        }

        //12 nov
        private void categoryFilterCategoryCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string category = categoryFilterCategoryCombo.Text.ToString();
                System.Data.DataTable data = store.category_filter(category);
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show(" لا توجد بيانات");
                }
                else
                {
                    categoryFilterGrid.ItemsSource = data.DefaultView;
                }
                CategoryFilterCounttxt.Content = "Items count : " + data.Rows.Count.ToString();
            }
            catch { }
        }

        //12 nov
        private void ItemFilterCategoryCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string category = ItemFilterCategoryCombo.Text.ToString();
                DataSet itemData = db.RunReaderds("select distinct item_name , code from items where item_category=" + category + "");

                if (itemData == null)
                {
                    MessageBox.Show(" لا توجد بيانات");
                }
                else
                {
                    ItemFilterCombo.ItemsSource = itemData.Tables[0].DefaultView;
                }
            }
            catch { }
        }

        //12 nov
        private void ItemFilterCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string category = ItemFilterCategoryCombo.Text.ToString();
                string item = ItemFilterCombo.Text.ToString();
                System.Data.DataTable data = store.item_filter(item);
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show(" لا توجد بيانات");
                }
                else
                {
                    ItemFilterGrid.ItemsSource = data.DefaultView;
                }
                itemFilterCounttxt.Content = "Items count : " + data.Rows.Count.ToString();
            }
            catch { }
        }
        //12 nov
        private void prCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                int provider_code = Convert.ToInt32(prCombo.Text.ToString());

                System.Data.DataTable data = contract.get_selected_provider_data(provider_code);
                if (data.Rows.Count == 0)
                {
                    MessageBox.Show("لا توجد نتائج");
                }
                else
                {
                    string termFlag = contract.get_terminate_flag(provider_code.ToString());
                    if (termFlag == "y" || termFlag == "Y")
                    {
                        providerattachBtn.Visibility = Visibility.Hidden;
                        providerImageGroup.Visibility = Visibility.Hidden;
                        MessageBox.Show("العقد منتهي");
                        ProviderListDetails.Foreground = Brushes.Orchid;
                        SaveContractProvider.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        providerattachBtn.Visibility = Visibility.Visible;
                        providerImageGroup.Visibility = Visibility.Visible;
                        SaveContractProvider.Visibility = Visibility.Visible;
                        ProviderListDetails.Foreground = Brushes.Black;
                    }
                    ProviderListDetails.ItemsSource = data.DefaultView;
                }
            }
            catch { }
        }

        //12 nov
        private void providerContractSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Data.DataTable dataDT = contract.get_provider_by_codeOrName(prCombo.Text.ToString());
                prCombo.ItemsSource = dataDT.DefaultView;
                prCombo.IsDropDownOpen = true;
            }
            catch { }
        }
        //12 nov
        private void CompanySrchBtn_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                fill_comp(CompanyCombo, CompanyCombo.Text.ToString());
                CompanyCombo.IsDropDownOpen = true;
            }
            catch { }
        }
        //12 nov
        private void CompanyCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                company_id = Convert.ToInt32(CompanyCombo.Text.ToString());

                System.Data.DataTable data = contract.get_selected_company_data(company_id);
                COmpanyGridDetails.ItemsSource = data.DefaultView;
                COmpanyGridDetails.Columns[0].Header = "رقم العقد";
                COmpanyGridDetails.Columns[1].Header = "كود الشركة";
                COmpanyGridDetails.Columns[2].Header = "اسم الشركة";
                COmpanyGridDetails.Columns[3].Header = "Company Name";
                COmpanyGridDetails.Columns[4].Header = "عنوان 1";
                COmpanyGridDetails.Columns[5].Header = "عنوان 2";
                COmpanyGridDetails.Columns[6].Header = "هاتف 1";
                COmpanyGridDetails.Columns[7].Header = "هاتف 2";
            }
            catch { }

        }
        //12 nov
        private void exportDeptCombo_DropDownClosed(object sender, EventArgs e) { exportEmpCombo.ItemsSource = db.RunReader("select name,code from agent where agent_dept='" + exportDeptCombo.Text + "'").Result.DefaultView; }

        //12 nov
        private void imprtDeptCombo_DropDownClosed(object sender, EventArgs e) { importEmpCombo.ItemsSource = db.RunReader("select name,code from agent where agent_dept='" + imprtDeptCombo.Text + "'").Result.DefaultView; }



        private void PolicyTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (UserType == "hr")
            {
                providerContract.Visibility = Visibility.Collapsed;
                companyContract.Visibility = Visibility.Collapsed;
                summaryProvider.Visibility = Visibility.Collapsed;
            }

            else
            {
                if (summaryProvider.IsSelected == true && providerTypeCombo.SelectedItem == null)
                {
                    System.Data.DataTable provider = client.get_provider();
                    providerTypeCombo.Items.Clear();
                    for (int i = 0; i < provider.Rows.Count; i++)
                    {
                        providerTypeCombo.Items.Add(provider.Rows[i].ItemArray[0].ToString());
                    }
                    //fill_pr(PrCodeComboMain);
                }
                else if (providerContract.IsSelected == true && prCombo.ItemsSource == null)
                {
                    //ProviderList.ItemsSource = contract.get_provider_codes().DefaultView;
                    prCombo.ItemsSource = contract.get_provider_codes().DefaultView;

                }
                //13 nov
                else if (companyContract.IsSelected == true && CompanyCombo.ItemsSource == null)
                {
                    CompanyCombo.ItemsSource = contract.get_company_codes().DefaultView;
                }
                else if (SummaryCompanyTab.IsSelected == true && CompanyComboBoxMain.ItemsSource == null)
                {
                    fill_comp(CompanyComboBoxMain);
                }
            }
        }

        private void newProviderSrchBtnz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                prCodeComboBox.Text = "";
                prCodetxtst.Text = "";
                prEnametxtst.Text = "";
                prAnametxtst.Text = "";
                prAddr1st.Text = "";
                prAddr2st.Text = "";
                prTel1st.Text = "";
                prTel2st.Text = "";
                prTaxFlagst.Text = "";
                prStampValst.Text = "";
                prDegst.Text = "";
                prDevextst.Text = "";
                prDevLocst.Text = "";
                prTermDatest.Text = "";
                prTermFlagst.Text = "";
                prForMedDisst.Text = "";
                prLocMedDisst.Text = "";
                SummaryProviderContrLongCombost.Text = "";
                SummaryProviderContrTypeCombost.Text = ""; img1st.Source = null;
                providerTypeCombost.Text = "";
            }
            catch { }
        }


        private void CompanyComboSummary_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(CompanyComboSummary.Text.ToString());

                System.Data.DataTable data = contract.get_selected_company_data(id);
                summaryCompanyContract.Items.Clear();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    summaryCompanyContract.Items.Add(data.Rows[i].ItemArray[0].ToString());
                }

            }
            catch { }
        }

        private void CompanyComboBoxMain_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                int id = Convert.ToInt32(CompanyComboBoxMain.Text.ToString());

                System.Data.DataTable data = contract.get_selected_company_data(id);
                summaryMainContractCompany.Items.Clear();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    summaryMainContractCompany.Items.Add(data.Rows[i].ItemArray[0].ToString());
                }

            }
            catch { }
        }

        private void ApprovalCompanyCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                int compid = Convert.ToInt32(ApprovalCompanyCombo.Text.ToString());
                fill_card(approvalcardcombo, compid);
            }
            catch { }
        }


        //13 nov
        private void IndemnityCompanySrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fill_comp(IndemnityCompanyCombo, IndemnityCompanyCombo.Text.ToString());
            IndemnityCompanyCombo.IsDropDownOpen = true;
        }

        //13 nov
        private void IndemnityCompanyCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                fill_card(IndemnityCardCombo, Convert.ToInt32(IndemnityCompanyCombo.Text.ToString()));
                indemnity_id = Convert.ToInt32((IndemnityCompanyCombo.Text.ToString()));
                string dateFrom = dtpFrom.Text.ToString();
                string dateTo = dtpTo.Text.ToString();
                IndemnityGrid.Visibility = Visibility.Visible;
                List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCompanyCodeSearch(dateFrom, dateTo, indemnity_id);
                if (Indemnities == null || Indemnities.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات");
                }
                else
                {
                    IndemnityGrid.ItemsSource = Indemnities;
                    indemnityItemCounttxt.Content = "Items count : " + Indemnities.Count.ToString();
                }
            }
            catch { }
        }
        //13 nov
        private void IndemnityCardCombo_DropDownClosed(object sender, EventArgs e)
        {

            string DateFrom = dtpFrom.Text;
            string DateTo = dtpTo.Text;

            string CardNum = IndemnityCardCombo.Text.ToString();
            try
            {
                if (UserType == "hr")
                {
                    string[] arr = CardNum.Split('-');
                    string comp = arr[0].ToString();
                    string compid = report.get_comp_id(UserCompany);
                    if (comp == compid)
                    {
                        List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCardNoSearch(DateFrom, DateTo, CardNum);
                        if (Indemnities == null)
                        {
                            MessageBox.Show("لا توجد بيانات");
                        }
                        else
                        {
                            IndemnityGrid.Visibility = Visibility.Visible;
                            IndemnityGrid.ItemsSource = Indemnities;
                            indemnityItemCounttxt.Content = "Items Count : " + Indemnities.Count.ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("غير مسموح بهذه الشركة");
                    }
                }
                else
                {
                    List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCardNoSearch(DateFrom, DateTo, CardNum);
                    if (Indemnities == null)
                    {
                        MessageBox.Show("لا توجد بيانات");
                    }
                    else
                    {
                        IndemnityGrid.Visibility = Visibility.Visible;
                        IndemnityGrid.ItemsSource = Indemnities;
                        indemnityItemCounttxt.Content = "Items Count : " + Indemnities.Count.ToString();
                    }
                }
            }
            catch { }

        }
        //13 nov
        private void IndemnityCardSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fill_card(IndemnityCardCombo, IndemnityCardCombo.Text.ToString(), IndemnityCompanyCombo.Text.ToString());
            IndemnityCardCombo.IsDropDownOpen = true;
        }

        //13 nov
        private void IndemntiyCompanySrchBtnz_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            fill_comp(IndemnityCompanyComboz, IndemnityCompanyComboz.Text.ToString());
            IndemnityCompanyComboz.IsDropDownOpen = true;
        }

        //13 nov
        private void IndemnityCompanyComboz_DropDownClosed(object sender, EventArgs e)
        {


            indemnity_id = Convert.ToInt32(IndemnityCompanyComboz.Text.ToString());
            fill_card(IndemnityCardComboz, indemnity_id);
            string dateFrom = dtpFromz.Text.ToString();
            string dateTo = dtpToz.Text.ToString();
            IndemnityGridz.Visibility = Visibility.Visible;
            List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCompanyCodeSearch(dateFrom, dateTo, indemnity_id);
            if (Indemnities == null || Indemnities.Count == 0)
            {
                MessageBox.Show("لا توجد بيانات");
            }
            else
            {
                IndemnityGridz.ItemsSource = Indemnities;
                indemnityItmCounttxtz.Content = Indemnities.Count;
            }

        }
        //13 nov
        private void IndemnityCardSrchBtnz_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                fill_card(IndemnityCardComboz, IndemnityCardComboz.Text.ToString());
                IndemnityCardComboz.IsDropDownOpen = true;
            }
            catch { }
        }



        //new final
        private void NewEmpDeptSrch_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataSet deptDataset = db.RunReaderds("select dept_code,dept_name from agent_department where dept_name like '%" + NewEmpDeptCombo.Text + "%' or dept_code like '%" + NewEmpDeptCombo.Text + "%'");
            NewEmpDeptCombo.ItemsSource = deptDataset.Tables[0].DefaultView;
            NewEmpDeptCombo.IsDropDownOpen = true;
        }

        //new final
        private void EditEmpDeptSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataSet deptDataset = db.RunReaderds("select dept_code,dept_name from agent_department where dept_name like '%" + basicdataDemptCombo.Text + "%' or dept_code like '%" + basicdataDemptCombo.Text + "%'");
            basicdataDemptCombo.ItemsSource = deptDataset.Tables[0].DefaultView;
            basicdataDemptCombo.IsDropDownOpen = true;
        }


        // new final

        private void movingFrmProviderNameSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int code = int.Parse(cmbProviderTypeDeliver.SelectedValue.ToString());
                string input = cmbProviderTypeDeliver_Copy.Text.ToString();
                List<NoteBookData> obj = note.SelectAllProviderNames(input, code);
                cmbProviderTypeDeliver_Copy.ItemsSource = obj;
                cmbProviderTypeDeliver_Copy.IsDropDownOpen = true;
            }
            catch { }
        }


        //new final
        private void ReturnDeptSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataSet depts = db.RunReaderds("select dept_code,dept_name from agent_department where dept_code like '%" + deptCombo.Text + "%' or dept_name like '%" + deptCombo.Text + "%'");
            deptCombo.ItemsSource = depts.Tables[0].DefaultView;
            deptCombo.IsDropDownOpen = true;
        }
        //new final
        private void ExportDeptSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataSet depts = db.RunReaderds("select dept_code,dept_name from agent_department where dept_code like '%" + exportDeptCombo.Text + "%' or dept_name like '%" + exportDeptCombo.Text + "%'");
            exportDeptCombo.ItemsSource = depts.Tables[0].DefaultView;
            exportDeptCombo.IsDropDownOpen = true;
        }

        //new final
        private void ImportDeptSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataSet depts = db.RunReaderds("select dept_code,dept_name from agent_department where dept_code like '%" + imprtDeptCombo.Text + "%' or dept_name like '%" + imprtDeptCombo.Text + "%'");
            imprtDeptCombo.ItemsSource = depts.Tables[0].DefaultView;
            imprtDeptCombo.IsDropDownOpen = true;
        }

        //new final
        private void EmployeeCareDeptSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataSet depts = db.RunReaderds("select dept_code,dept_name from agent_department where dept_code like '%" + employeeCareDeptCombo.Text + "%' or dept_name like '%" + employeeCareDeptCombo.Text + "%'");
            employeeCareDeptCombo.ItemsSource = depts.Tables[0].DefaultView;
            employeeCareDeptCombo.IsDropDownOpen = true;
        }

        //new final
        private void DestroyedDeptSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataSet depts = db.RunReaderds("select dept_code,dept_name from agent_department where dept_code like '%" + destroyFilterDeptCombo.Text + "%' or dept_name like '%" + destroyFilterDeptCombo.Text + "%'");
            destroyFilterDeptCombo.ItemsSource = depts.Tables[0].DefaultView;
            destroyFilterDeptCombo.IsDropDownOpen = true;
        }

        //new final
        private void DeptFilterSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataSet depts = db.RunReaderds("select dept_code,dept_name from agent_department where dept_code like '%" + DeptFilterCombo.Text + "%' or dept_name like '%" + DeptFilterCombo.Text + "%'");
            DeptFilterCombo.ItemsSource = depts.Tables[0].DefaultView;
            DeptFilterCombo.IsDropDownOpen = true;
        }

        //new final
        private void EmployeeFilterDeptSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataSet depts = db.RunReaderds("select dept_code,dept_name from agent_department where dept_code like '%" + empFilterDeptCombo.Text + "%' or dept_name like '%" + empFilterDeptCombo.Text + "%'");
            empFilterDeptCombo.ItemsSource = depts.Tables[0].DefaultView;
            empFilterDeptCombo.IsDropDownOpen = true;
        }

        //new final
        private void ReturnEmpSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataSet empdata = db.RunReaderds(@"select distinct employee_name_ , code from 
                                                    transaction , agent where employee_dept='" + deptCombo.Text + "' and (name like '%" + empCombo.Text + "%' or code like '%" + empCombo.Text + "%') and employee_name_ = name and type=4 order by employee_name_");
                empCombo.ItemsSource = empdata.Tables[0].DefaultView;
                empCombo.IsDropDownOpen = true;
            }
            catch { }
        }

        //new final
        private void ExportEmpSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataSet empdata = db.RunReaderds("select name,code from agent where ( name like '%" + exportEmpCombo.Text + "%' or code like '%" + exportEmpCombo.Text + "%' ) and agent_dept='" + exportDeptCombo.Text.ToString() + "' ORDER BY NAME");
                exportEmpCombo.ItemsSource = empdata.Tables[0].DefaultView;
                exportEmpCombo.IsDropDownOpen = true;
            }
            catch { }
        }

        //new final
        private void ImportEmpSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataSet empdata = db.RunReaderds("select name,code from agent where( name like '%" + importEmpCombo.Text + "%' or code like '%" + importEmpCombo.Text + "%' ) and agent_dept='" + imprtDeptCombo.Text.ToString() + "' ORDER BY NAME");
                importEmpCombo.ItemsSource = empdata.Tables[0].DefaultView;
                importEmpCombo.IsDropDownOpen = true;
            }
            catch { }
        }

        //new final
        private void EmployeeCardEmpSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataSet empdata = db.RunReaderds("select name,code from agent where ( name like '%" + employeeCareEmpCombo.Text + "%' or code like '%" + employeeCareEmpCombo.Text + "%' ) and agent_dept='" + employeeCareDeptCombo.Text.ToString() + "' ORDER BY NAME");
                employeeCareEmpCombo.ItemsSource = empdata.Tables[0].DefaultView;
                employeeCareEmpCombo.IsDropDownOpen = true;
            }
            catch { }
        }

        //ew final
        private void DestroyedEmpSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataSet empdata = db.RunReaderds("select name,code from agent where ( name like '%" + destroyFilterEmpCombo.Text + "%' or code like '%" + destroyFilterEmpCombo.Text + "%' ) and agent_dept='" + destroyFilterDeptCombo.Text.ToString() + "' ORDER BY NAME");
                destroyFilterEmpCombo.ItemsSource = empdata.Tables[0].DefaultView;
                destroyFilterEmpCombo.IsDropDownOpen = true;
            }
            catch { }
        }

        //new final
        private void EmployeeFilterEmpSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataSet empdata = db.RunReaderds("select name,code from agent where ( name like '%" + empFilterEmpCombo.Text + "%' or code like '%" + empFilterEmpCombo.Text + "%' ) and agent_dept='" + empFilterDeptCombo.Text.ToString() + "' ORDER BY NAME");
                empFilterEmpCombo.ItemsSource = empdata.Tables[0].DefaultView;
                empFilterEmpCombo.IsDropDownOpen = true;
            }
            catch { }
        }

        //new final
        private void CategoryFilterSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataSet categoryData = db.RunReaderds("select category_name , category_id from item_category where ( category_name like '%" + categoryFilterCategoryCombo.Text + "%' or category_id like '%" + categoryFilterCategoryCombo.Text + "%' ) ORDER BY CATEGORY_NAME");
                categoryFilterCategoryCombo.ItemsSource = categoryData.Tables[0].DefaultView;
                categoryFilterCategoryCombo.IsDropDownOpen = true;
            }
            catch { }

        }

        //new final
        private void ItemFilterCategorySrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            try
            {
                DataSet categoryData = db.RunReaderds("select category_name , category_id from item_category where ( category_name like '%" + ItemFilterCategoryCombo.Text + "%' or category_id like '%" + ItemFilterCategoryCombo.Text + "%' ) ORDER BY CATEGORY_NAME");
                ItemFilterCategoryCombo.ItemsSource = categoryData.Tables[0].DefaultView;
                ItemFilterCategoryCombo.IsDropDownOpen = true;
            }
            catch { }
        }

        //new final
        private void ItemFilterItemSrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            try
            {
                string category = ItemFilterCategoryCombo.Text.ToString();
                DataSet itemData = db.RunReaderds("select distinct item_name , code from items where ( item_name like '%" + ItemFilterCombo.Text + "%' or code like '%" + ItemFilterCombo.Text + "%') and item_category=" + ItemFilterCategoryCombo.Text + " ");
                ItemFilterCombo.ItemsSource = itemData.Tables[0].DefaultView;
                ItemFilterCombo.IsDropDownOpen = true;
            }
            catch { }
        }

        private void tmprintdetails_Click(object sender, RoutedEventArgs e)
        {
            Int64 batch1, prv1, clm1, batch2, prv2, clm2;





            batch1 = ReviCompcbx2.Text == string.Empty ? 0 : Convert.ToInt64(ReviCompcbx2.Text);
            batch2 = ReviCompcbx2.Text == string.Empty ? 9999999999999999 : Convert.ToInt64(ReviCompcbx2.Text);

            prv1 = ReviCompcbx_Copy5.Text == string.Empty ? 0 : Convert.ToInt64(ReviCompcbx_Copy5.Text);
            prv2 = ReviCompcbx_Copy5.Text == string.Empty ? 9999999999999999 : Convert.ToInt64(ReviCompcbx_Copy5.Text);

            clm1 = ReviCompcbx_Copy6.Text == string.Empty ? 0 : Convert.ToInt64(ReviCompcbx_Copy6.Text);
            clm2 = ReviCompcbx_Copy6.Text == string.Empty ? 9999999999999999 : Convert.ToInt64(ReviCompcbx_Copy6.Text);

            View_Report showreport = new View_Report();



            ReportBatch repo4 = new ReportBatch();

            repo4.SetDatabaseLogon("APP", "12369");
            repo4.SetParameterValue("batch1", batch1);
            repo4.SetParameterValue("batch2", batch2);
            repo4.SetParameterValue("prv1", prv1);
            repo4.SetParameterValue("prv2", prv2);
            repo4.SetParameterValue("clm1", clm1);
            repo4.SetParameterValue("clm2", clm2);

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
        }

        //21 nov
        private void MedDiffRegDateTo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string datefrom = MedDiffRegDateFrom.Text.ToString();
                string dateto = MedDiffRegDateTo.Text.ToString();
                string med_code = medCodeCombo.Text.ToString();
                int code;
                if (med_code == "")
                {
                    code = 0;
                }
                DataSet MedData = db.RunReaderds(@"select distinct review_name,date_rev,claim_no,med_code,
                                    med_name,dosage,system_amt,claim_amt , prv_name,prv_branch_name 
                                    from a_med_diff where (date_rev between nvl('" + datefrom + "','01-Jan-90') and nvl('" + dateto + "','01-Jan-90')) " +
                                    "or (med_code like nvl('" + med_code + "',0))");
                MedDiffGrid.ItemsSource = MedData.Tables[0].DefaultView;
                MedDiffGrid.Columns[0].Header = "اسم المُراجع";
                MedDiffGrid.Columns[1].Header = "تاريخ المُراجعة";
                MedDiffGrid.Columns[2].Header = "رقم الموافقة";
                MedDiffGrid.Columns[3].Header = "كود الدواء";
                MedDiffGrid.Columns[4].Header = "اسم الدواء";
                MedDiffGrid.Columns[5].Header = "الجرعة";
                MedDiffGrid.Columns[6].Header = "مبلغ السيستم";
                MedDiffGrid.Columns[7].Header = "مبلغ المرافعة";
                MedDiffGrid.Columns[8].Header = "اسم الصيدلية";
                MedDiffGrid.Columns[9].Header = "اسم الفرع";
                diffItemsCounttxt.Content = MedDiffGrid.Items.Count - 1;

            }
            catch { }
        }

        //21 nov
        private void medCodeCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                string datefrom = MedDiffRegDateFrom.Text.ToString();
                string dateto = MedDiffRegDateTo.Text.ToString();
                string med_code = medCodeCombo.Text.ToString();

                System.Data.DataTable medData = Medicie.get_med_CodeAndName(datefrom, dateto, med_code);

                MedDiffGrid.ItemsSource = medData.DefaultView;
                MedDiffGrid.Columns[0].Header = "اسم المُراجع";
                MedDiffGrid.Columns[1].Header = "تاريخ المُراجعة";
                MedDiffGrid.Columns[2].Header = "رقم الموافقة";
                MedDiffGrid.Columns[3].Header = "كود الدواء";
                MedDiffGrid.Columns[4].Header = "اسم الدواء";
                MedDiffGrid.Columns[5].Header = "الجرعة";
                MedDiffGrid.Columns[6].Header = "مبلغ السيستم";
                MedDiffGrid.Columns[7].Header = "مبلغ المرافعة";
                MedDiffGrid.Columns[8].Header = "اسم الصيدلية";
                MedDiffGrid.Columns[9].Header = "اسم الفرع";
                diffItemsCounttxt.Content = MedDiffGrid.Items.Count - 1;


            }
            catch { }
        }

        //21 nov
        private void SrchMedBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string name = medCodeCombo.Text.ToString();
                DataSet MedData = db.RunReaderds("select distinct med_code ,med_name from a_med_diff where med_code like '%" + name + "%' or lower(med_name) like '%" + name.ToLower() + "%' ");
                medCodeCombo.ItemsSource = MedData.Tables[0].DefaultView;
                medCodeCombo.IsDropDownOpen = true;
            }
            catch { }
        }

        //21 nov
        private void MedDiffNewBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MedDiffRegDateFrom.Text = "";
                MedDiffRegDateTo.Text = "";
                medCodeCombo.Text = "";
                MedDiffGrid.ItemsSource = null;
                diffItemsCounttxt.Content = "0";

            }
            catch { }
        }


        #region hr


        public static string UserName = "";

        private void newnewnew(object sender, MouseButtonEventArgs e)
        {

            if (InfoCardTab.IsSelected == true && infocardcombo.ItemsSource == null)
            {
                if (UserType == "hr")
                {

                    //   int compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                    infoCardCompanyCombo.Text = User.CompanyID;
                    infoCardCompanyCombo.IsEnabled = false;
                    imgsearch2_Copy.IsEnabled = false;

                    InfotxtCardNum.IsEnabled = false;
                    InfotxtCardNum.Text = User.CompanyName;
                    imgsearch1.IsEnabled = false;
                    //   fill_card(infocardcombo, compid);
                    searchnewre();
                }

                else if (User.Type != "hr" && infoCardCompanyCombo.ItemsSource == null)
                {
                    // infoCardCompanyCombo.Visibility = Visibility.Visible;
                    infoCardlblCompany.Visibility = Visibility.Visible;
                    fill_comp(infoCardCompanyCombo);
                    // infocardcombo_Copy.ItemsSource = User.ALL_Company().DefaultView;
                }


            }
            else if (hrrequestsTab.IsSelected == true)
            {
                UserName = User.Name;
                UserCompany = User.CompanyName;

                //abdo
                username = User.Name;
                companyNameqw = User.CompanyName;

                System.Threading.Thread.CurrentThread.CurrentCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd-MMM-yy";


                if (User.Type == "DMS Member")
                {
                    NewEmpCompCombo.ItemsSource = db.RunReader("select distinct C_COMP_ID ,C_ANAME from V_COMPANIES order by c_comp_id").Result.DefaultView;
                }
                else
                {
                    NewEmpCompCombo.Visibility = Visibility.Hidden;
                    //  zxcv.Visibility = Visibility.Hidden;
                    NewEmpCompanySrchBtn.Visibility = Visibility.Hidden;

                }



            }
        }



        private void problemCBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (problemCBX.SelectedIndex == 0)
            {
                problem_DMS.Visibility = Visibility.Visible;
                problem_prov.Visibility = Visibility.Hidden;
            }
            else if (problemCBX.SelectedIndex == 1)
            {
                problem_prov.Visibility = Visibility.Visible;
                problem_DMS.Visibility = Visibility.Hidden;
                Serviec_requestss.IsSelected = true;

            }
        }

        private void aaaaaaaa22(object sender, MouseButtonEventArgs e)
        {
            if (NetworkTab.IsSelected == true && providerComboNetwork.ItemsSource == null)
            {

                if (UserType == "hr")
                {

                    if (networkClassCodeCombo.ItemsSource == null && networkcardcombo.ItemsSource == null)
                    {
                        classrb.Visibility = Visibility.Visible;
                        aqaq.Visibility = Visibility.Hidden;
                        qwqw1.Visibility = Visibility.Hidden;
                        networkcardcombo_Copy.Visibility = Visibility.Hidden;
                        networkcardcombo.Visibility = Visibility.Hidden;
                        imgsearch1.Visibility = Visibility.Hidden;
                        imgsearch1_Copy.Visibility = Visibility.Hidden;

                        networkcardcombo_Copy.Text = User.CompanyID;

                        networkClassCodeCombo.Visibility = Visibility.Hidden;
                        networkcardcombo_Copy.IsEnabled = false;
                        imgsearch1.IsEnabled = false;
                        networkcardcombo.ItemsSource = User.Employee_in_Company().DefaultView;
                        networkClassCodeCombo.Items.Clear();

                        System.Data.DataTable data = hrnet.get_Class_Code(Convert.ToInt32(User.CompanyID));
                        for (int i = 0; i < data.Rows.Count; i++)
                        {
                            networkClassCodeCombo.Items.Add(data.Rows[i].ItemArray[0].ToString() + " | " + data.Rows[i].ItemArray[1].ToString());
                        }



                        //////////////////////////////
                    }
                }

                else if (User.Type != "hr" && networkcardcombo_Copy.ItemsSource == null)
                {

                    networkcardcombo_Copy.ItemsSource = User.ALL_Company().DefaultView;
                    networkcardcombo_Copy.Visibility = Visibility.Visible;
                    imgsearch1.Visibility = Visibility.Visible;
                    networkcardcombo.Visibility = Visibility.Visible;
                    imgsearch1_Copy.Visibility = Visibility.Visible;

                    networkClassCodeCombo.Visibility = Visibility.Hidden;
                    classrb.Visibility = Visibility.Hidden;
                    cardrb.Visibility = Visibility.Hidden;

                }
                NetworkGrid.Visibility = Visibility.Hidden;
                lblSpec.Visibility = Visibility.Hidden;
                docSpecComboNetwork.Visibility = Visibility.Hidden;
                System.Data.DataTable providerTable = client.get_provider();
                providerComboNetwork.Items.Clear();
                for (int i = 0; i < providerTable.Rows.Count; i++)
                {
                    providerComboNetwork.Items.Add(providerTable.Rows[i].ItemArray[0].ToString());
                }

                governComboNetwork.Items.Clear();
                System.Data.DataTable governTable = client.get_curr_city();
                for (int i = 0; i < governTable.Rows.Count; i++)
                {
                    governComboNetwork.Items.Add(governTable.Rows[i].ItemArray[0].ToString());
                }

            }
            else if (addnewprov.IsSelected == true && cbxcardnoqw.ItemsSource == null)
            {
                try
                {



                    if (User.Type == "DMS Member" && cbxclassEmpCompCombo.ItemsSource == null)
                    {
                        fillqw();
                        AutoNumeqw();
                        addpic.Visibility = Visibility.Hidden;
                        addpic_Copy.Visibility = Visibility.Hidden;
                        cbxclassEmpCompCombo.ItemsSource = User.ALL_Company().DefaultView;
                    }
                    else if (User.Type == "hr")
                    {
                        fillqw();
                        AutoNumeqw();
                        addpic.Visibility = Visibility.Hidden;
                        addpic_Copy.Visibility = Visibility.Hidden;
                        poiu.Visibility = Visibility.Hidden;
                        cbxclassEmpCompCombo.Visibility = Visibility.Hidden;
                        imgsearchCust_Copy122.Visibility = Visibility.Hidden;
                        fillcardname(cbxcardnoqw);
                    }

                }
                catch { }
            }


        }

        private void aksfkkc(object sender, MouseButtonEventArgs e)
        {
            if (ApprovalsTabz.IsSelected == true && approvalcardcombo.ItemsSource == null)
            {
                try
                {

                    //if (User.Type != "DMS Member")cbxstateqw
                    //{
                    //    DataSet tem = db.RunReaderds("select   CARD_NO ,EMP_ENAME  from IRS_EMPLOYEES WHERE COMP_ID=" + User.CompanyID + " ORDER BY CARD_NO ");
                    //    ApprovaltxtCardNum.ItemsSource = tem.Tables[0].DefaultView;
                    //}
                    if (UserType == "hr")
                    {
                        int compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                        // approvalCompanylblz.Visibility = Visibility.Hidden;
                        ApprovalCompanyCombo.Text = User.CompanyID;
                        ApprovalCompanyCombo.IsEnabled = false;
                        imgsearch4_Copy.IsEnabled = false;
                        ApprovaltxtCardNumz.Visibility = Visibility.Hidden;
                        fill_card(approvalcardcombo, compid);
                    }
                    else if (User.Type != "hr" && ApprovalCompanyCombo.ItemsSource == null)
                    {
                        //  approvalCompanylblz.Visibility = Visibility.Visible;

                        fill_comp(ApprovalCompanyCombo);

                    }
                }
                catch { }
            }
            else if (TlabMwaf2aTab.IsSelected == true && reqcardaddcbx_Copy.ItemsSource == null)
            {

                if (User.Type == "DMS Member")
                {
                    reqcompaddcbx.ItemsSource = User.ALL_Company().DefaultView;
                    //   reqcardaddcbx_Copy.ItemsSource = db_IRS.RunReader("select SUPER_GROUP_CODE,SUPER_GROUP_ENAME from IRS_SERVICES_SUPER_GROUP").Result.DefaultView;

                }
                else if (User.Type == "hr")
                {
                    searchreqadd.IsEnabled = false;
                    reqcompaddcbx.IsEnabled = false;
                    reqcompaddcbx.Text = User.CompanyID;
                    reqcardaddcbx.ItemsSource = User.Employee_in_Company().DefaultView;
                    //    reqcardaddcbx_Copy.ItemsSource = db_IRS.RunReader("select SUPER_GROUP_CODE,SUPER_GROUP_ENAME from IRS_SERVICES_SUPER_GROUP").Result.DefaultView;

                }

            }

        }

        private void aaaaa223aaaa(object sender, MouseButtonEventArgs e)
        {
            if (tlbedafaTab.IsSelected == true && reqcompaddcbx1.ItemsSource == null && reqcardaddcbx1.ItemsSource == null)
            {

                if (User.Type == "DMS Member")
                {
                    reqcompaddcbx1.ItemsSource = User.ALL_Company().DefaultView;

                }
                else if (User.Type == "hr")
                {
                    searchreqadd1.IsEnabled = false;
                    reqcompaddcbx1.IsEnabled = false;
                    reqcompaddcbx1.Text = User.CompanyID;
                    reqcardaddcbx1.ItemsSource = User.Employee_in_Company().DefaultView;

                }



            }
            else if (month12.IsSelected == true && abdocbxmonthCard.ItemsSource == null)
            {
                jjstartmonth();
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            spliter++;
            if (pathesOfImage.Count > spliter)
                companyContract1st.Source = new BitmapImage(new Uri(pathesOfImage[spliter].ToString()));
            else
            {
                MessageBox.Show("final");
                spliter--;
            }

        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            spliter--;
            if (spliter >= 0)

                companyContract1st.Source = new BitmapImage(new Uri(pathesOfImage[spliter].ToString()));
            else
            {
                MessageBox.Show("frist");
                spliter++;
            }
        }

        private void companyContract1st_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CompanyContractLongst_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                spliter--;
                if (spliter >= 0)

                    companyContract1st.Source = new BitmapImage(new Uri(pathesOfImage[spliter].ToString()));
                else
                {
                    MessageBox.Show("frist");
                    spliter++;
                }

            }
            else if (e.Key == Key.Left)
            {
                spliter++;
                if (pathesOfImage.Count > spliter)
                    companyContract1st.Source = new BitmapImage(new Uri(pathesOfImage[spliter].ToString()));
                else
                {
                    MessageBox.Show("final");
                    spliter--;
                }

            }
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            spliter++;
            if (pathesOfImage.Count > spliter)
                img1st.Source = new BitmapImage(new Uri(pathesOfImage[spliter].ToString()));
            else
            {
                MessageBox.Show("final");
                spliter--;
            }
        }

        private void Image_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {

            spliter--;
            if (spliter >= 0)

                img1st.Source = new BitmapImage(new Uri(pathesOfImage[spliter].ToString()));
            else
            {
                MessageBox.Show("frist");
                spliter++;
            }
        }

        private void searchreqadd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            reqcompaddcbx.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + reqcompaddcbx.Text + "%' or C_ANAME LIKE '%" + reqcompaddcbx.Text + "%'  ORDER BY C_COMP_ID ").Result.DefaultView;
            reqcompaddcbx.IsDropDownOpen = true;
        }

        private void reqcompaddcbx_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                reqcardaddcbx.ItemsSource = User.Employee_in_Company(reqcompaddcbx.Text).DefaultView;
            }
            catch { }
        }

        private void reqcompaddcbx_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    reqcardaddcbx.ItemsSource = User.Employee_in_Company(reqcompaddcbx.Text).DefaultView;
                }
                catch { }
            }
        }

        private void searchreqadd_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string com = "";
                if (User.Type == "DMS Member")
                    com = reqcompaddcbx.Text;
                else if (User.Type == "hr")
                    com = User.CompanyID;
                reqcardaddcbx.ItemsSource = db.RunReader(" select distinct   CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE C_COMP_ID=" + com + " and (TERMINATE_FLAG='N' or TERMINATE_FLAG is NULL) and ( CARD_ID LIKE '%" + reqcardaddcbx.Text + "%' OR EMP_ANAME_ST LIKE '%" + reqcardaddcbx.Text + "%'  OR EMP_ANAME_SC LIKE '%" + reqcardaddcbx.Text + "%'  OR EMP_ANAME_TH LIKE '%" + reqcardaddcbx.Text + "%' ) ORDER BY CARD_ID ").Result.DefaultView;
                reqcardaddcbx.IsDropDownOpen = true;
            }
            catch { }
        }
        string pathreq;
        private void btnuploudqwa_Copy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                //    path = Path.GetFlowDirection(op);
                if (op.ShowDialog() == true)
                {
                    adwefsdewf.Source = new BitmapImage(new Uri(op.FileName));
                    pathreq = op.FileName;


                    System.Drawing.Bitmap newimg = new System.Drawing.Bitmap(pathreq);

                }




                btnsaveagainqw.IsEnabled = true;
            }
            catch { }
        }

        private void savesaEmpBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Data.DataTable a = db.RunReader(@"select Max(ID) from ENUM_REQUESTS ").Result;
            int i = Convert.ToInt32(a.Rows[0][0].ToString());
            i++;
            string id = i.ToString();
            db.RunNonQuery("insert into ENUM_REQUESTS (id,TYPE,CARD_ID,REQ_DATE,EMP_ENAME,REQ_TYPE,NOTES,REQUEST_TYP) values ('" + id + "','" + reqcardaddcbx_Copy.Text + "','" + reqcardaddcbx.Text + "','" + DateTime.Now.ToShortDateString() + "','" + User.Name + "','M','" + notetxt.Text + "','Desktop')");



            if (adwefsdewf.Source != null)
            {
                FileStream fls;
                fls = new FileStream(@pathreq, FileMode.Open, FileAccess.Read);
                //a byte array to read the image 
                byte[] blob = new byte[fls.Length];
                fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                fls.Close();


                string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=********** )(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
                conn = new OracleConnection(connectionStr);
                conn.Open();
                OracleCommand cmnd;
                string query;
                query = @"UPDATE ENUM_REQUESTS SET  IMAGEBLOB=:BlobParameter where ID ='" + id + "'";
                //insert the byte as oracle parameter of type blob 
                OracleParameter blobParameter = new OracleParameter();
                blobParameter.OracleType = OracleType.Blob;
                blobParameter.ParameterName = "BlobParameter";
                blobParameter.Value = blob;
                cmnd = new OracleCommand(query, conn);
                cmnd.Parameters.Add(blobParameter);
                cmnd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + id);
                lblf_Copy.Content = id;
            }
            else
            {
                MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + id + "\n" + "مع مراعاة انها لم يتم ارفاق صورة ");
                lblf_Copy.Content = id;
            }





        }

        private void btnclearqw_Copy_Click(object sender, RoutedEventArgs e)
        {
            adwefsdewf.Source = null;
            if (User.Type == "DMS Member")
                reqcompaddcbx.Text = "";
            reqcardaddcbx.Text = "";
            reqcardaddcbx_Copy.Text = "";
            notetxt.Text = "";
            lblf_Copy.Content = "******";
        }

        private void btnclearqw_Copy1_Click(object sender, RoutedEventArgs e)
        {
            adwefsdewf1.Source = null;
            if (User.Type == "DMS Member")
                reqcompaddcbx1.Text = "";
            reqcardaddcbx1.Text = "";
            reqcardaddcbx_Copy1.Text = "";
            notetxt1.Text = "";
            lblf_Copy1.Content = "******";
        }

        private void btnuploudqwa_Copy1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                //    path = Path.GetFlowDirection(op);
                if (op.ShowDialog() == true)
                {
                    adwefsdewf1.Source = new BitmapImage(new Uri(op.FileName));
                    pathreq = op.FileName;


                    System.Drawing.Bitmap newimg = new System.Drawing.Bitmap(pathreq);

                }




                btnsaveagainqw.IsEnabled = true;
            }
            catch { }
        }

        private void savesaEmpBtn1_Click(object sender, RoutedEventArgs e)
        {
            System.Data.DataTable a = db.RunReader(@"select Max(ID) from ENUM_REQUESTS ").Result;
            int i = Convert.ToInt32(a.Rows[0][0].ToString());
            i++;
            string id = i.ToString();
            db.RunNonQuery("insert into ENUM_REQUESTS (id,TYPE,CARD_ID,REQ_DATE,EMP_ENAME,REQ_TYPE,NOTES,REQUEST_TYP) values ('" + id + "','" + reqcardaddcbx_Copy1.Text + "','" + reqcardaddcbx1.Text + "','" + DateTime.Now.ToShortDateString() + "','" + User.Name + "','C','" + notetxt1.Text + "','Desktop')");



            if (adwefsdewf1.Source != null)
            {
                FileStream fls;
                fls = new FileStream(@pathreq, FileMode.Open, FileAccess.Read);
                //a byte array to read the image 
                byte[] blob = new byte[fls.Length];
                fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                fls.Close();


                string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=********** )(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
                conn = new OracleConnection(connectionStr);
                conn.Open();
                OracleCommand cmnd;
                string query;
                query = @"UPDATE ENUM_REQUESTS SET  IMAGEBLOB=:BlobParameter where ID ='" + id + "'";
                //insert the byte as oracle parameter of type blob 
                OracleParameter blobParameter = new OracleParameter();
                blobParameter.OracleType = OracleType.Blob;
                blobParameter.ParameterName = "BlobParameter";
                blobParameter.Value = blob;
                cmnd = new OracleCommand(query, conn);
                cmnd.Parameters.Add(blobParameter);
                cmnd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + id);
                lblf_Copy1.Content = id;
            }
            else
            {
                MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + id + "\n" + "مع مراعاة انها لم يتم ارفاق صورة ");
                lblf_Copy1.Content = id;
            }

        }

        private void searchreqadd1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            reqcompaddcbx1.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + reqcompaddcbx1.Text + "%' or C_ANAME LIKE '%" + reqcompaddcbx1.Text + "%'  ORDER BY C_COMP_ID ").Result.DefaultView;
            reqcompaddcbx1.IsDropDownOpen = true;
        }

        private void reqcompaddcbx1_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                reqcardaddcbx1.ItemsSource = User.Employee_in_Company(reqcompaddcbx1.Text).DefaultView;
            }
            catch { }
        }

        private void reqcompaddcbx1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                reqcardaddcbx1.ItemsSource = User.Employee_in_Company(reqcompaddcbx1.Text).DefaultView;
            }
            catch { }
        }

        private void searchreqadd_Copy1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string com = "";
                if (User.Type == "DMS Member")
                    com = reqcompaddcbx2.Text;
                else if (User.Type == "hr")
                    com = User.CompanyID;
                reqcardaddcbx2.ItemsSource = db.RunReader(" select distinct   CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE C_COMP_ID=" + com + " and (TERMINATE_FLAG='N' or TERMINATE_FLAG is NULL) and ( CARD_ID LIKE '%" + reqcardaddcbx2.Text + "%' OR EMP_ANAME_ST LIKE '%" + reqcardaddcbx2.Text + "%'  OR EMP_ANAME_SC LIKE '%" + reqcardaddcbx2.Text + "%'  OR EMP_ANAME_TH LIKE '%" + reqcardaddcbx2.Text + "%' ) ORDER BY CARD_ID ").Result.DefaultView;
                reqcardaddcbx2.IsDropDownOpen = true;
            }
            catch { }
        }

        private void searchreqadd2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            reqcompaddcbx2.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + reqcompaddcbx2.Text + "%' or C_ANAME LIKE '%" + reqcompaddcbx2.Text + "%'  ORDER BY C_COMP_ID ").Result.DefaultView;
            reqcompaddcbx2.IsDropDownOpen = true;

        }

        private void reqcompaddcbx2_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                reqcardaddcbx2.ItemsSource = User.Employee_in_Company(reqcompaddcbx2.Text).DefaultView;
            }
            catch { }
        }

        private void reqcompaddcbx2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                reqcardaddcbx2.ItemsSource = User.Employee_in_Company(reqcompaddcbx2.Text).DefaultView;
            }
            catch { }
        }

        private void searchreqadd_Copy2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            try
            {
                string com = "";
                if (User.Type == "DMS Member")
                    com = reqcompaddcbx2.Text;
                else if (User.Type == "hr")
                    com = User.CompanyID;
                reqcardaddcbx2.ItemsSource = db.RunReader(" select distinct   CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE C_COMP_ID=" + com + " and (TERMINATE_FLAG='N' or TERMINATE_FLAG is NULL) and ( CARD_ID LIKE '%" + reqcardaddcbx2.Text + "%' OR EMP_ANAME_ST LIKE '%" + reqcardaddcbx2.Text + "%'  OR EMP_ANAME_SC LIKE '%" + reqcardaddcbx2.Text + "%'  OR EMP_ANAME_TH LIKE '%" + reqcardaddcbx2.Text + "%' ) ORDER BY CARD_ID ").Result.DefaultView;
                reqcardaddcbx2.IsDropDownOpen = true;
            }
            catch { }
        }

        private void btnuploudqwa_Copy2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                //    path = Path.GetFlowDirection(op);
                if (op.ShowDialog() == true)
                {
                    adwefsdewf2.Source = new BitmapImage(new Uri(op.FileName));
                    pathreq = op.FileName;


                    System.Drawing.Bitmap newimg = new System.Drawing.Bitmap(pathreq);

                }




                //  btnsaveagainqw.IsEnabled = true;
            }
            catch { }
        }

        private void btnclearqw_Copy2_Click(object sender, RoutedEventArgs e)
        {
            adwefsdewf2.Source = null;
            if (User.Type == "DMS Member")
                reqcompaddcbx2.Text = "";
            reqcardaddcbx2.Text = "";
            // reqcardaddcbx_Copy2.Text = "";
            notetxt2.Text = "";
            lblf_Copy2.Content = "******";

        }

        private void savesaEmpBtn2_Click(object sender, RoutedEventArgs e)
        {
            System.Data.DataTable a = db.RunReader(@"select Max(ID) from ENUM_REQUESTS ").Result;
            int i = Convert.ToInt32(a.Rows[0][0].ToString());
            i++;
            string id = i.ToString();
            db.RunNonQuery("insert into ENUM_REQUESTS (id,CARD_ID,REQ_DATE,EMP_ENAME,REQ_TYPE,NOTES,REQUEST_TYP) values ('" + id + "','" + reqcardaddcbx2.Text + "','" + DateTime.Now.ToShortDateString() + "','" + User.Name + "','A','" + notetxt2.Text + "','Desktop')");



            if (adwefsdewf2.Source != null)
            {
                FileStream fls;
                fls = new FileStream(@pathreq, FileMode.Open, FileAccess.Read);
                //a byte array to read the image 
                byte[] blob = new byte[fls.Length];
                fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                fls.Close();


                string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=********** )(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
                conn = new OracleConnection(connectionStr);
                conn.Open();
                OracleCommand cmnd;
                string query;
                query = @"UPDATE ENUM_REQUESTS SET  IMAGEBLOB=:BlobParameter where ID ='" + id + "'";
                //insert the byte as oracle parameter of type blob 
                OracleParameter blobParameter = new OracleParameter();
                blobParameter.OracleType = OracleType.Blob;
                blobParameter.ParameterName = "BlobParameter";
                blobParameter.Value = blob;
                cmnd = new OracleCommand(query, conn);
                cmnd.Parameters.Add(blobParameter);
                cmnd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + id);
                lblf_Copy2.Content = id;
            }
            else
            {
                MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + id + "\n" + "مع مراعاة انها لم يتم ارفاق صورة ");
                lblf_Copy2.Content = id;
            }
        }
        private void adasdxcsdcsd(object sender, MouseButtonEventArgs e)
        {
            if (datagridDeletEmpReqprint.ItemsSource == null && DeleEempEequestTabprint.IsSelected == true)
            {


                if (User.Manegar == "Y" || User.Manegar == "y")
                {
                    System.Data.DataTable dts = db.RunReader(@"select  REQUEST_CODE,CARD_ID,PRINT_REASON ,CREATED_BY ,CREATED_DATE ,REGISTER_TYPE from employee_request where type='4'  and approve_flag='W'").Result;
                    dts.Columns["REQUEST_CODE"].ColumnName = "رقم الطلب";
                    dts.Columns[1].ColumnName = "رقم الكارت";
                    dts.Columns[2].ColumnName = "سبب الطباعة";
                    dts.Columns[3].ColumnName = "طلب من";
                    dts.Columns[4].ColumnName = "تاريخ الطلب";

                    dts.Columns.Add("عن طريق", typeof(String));


                    foreach (DataRow rowz in dts.Rows)
                    {
                        if (rowz[5].ToString() == "P" || rowz[5].ToString() == "p")
                            rowz["عن طريق"] = "سيستم";
                        else
                            rowz["عن طريق"] = "موبايل";



                    }
                    dts.Columns.RemoveAt(5);
                    datagridDeletEmpReqprint.ItemsSource = dts.DefaultView;
                }
                else
                {
                    System.Data.DataTable dts = db.RunReader(@"select  REQUEST_CODE,CARD_ID,PRINT_REASON ,CREATED_BY ,CREATED_DATE ,REGISTER_TYPE from employee_request where type='4'  and approve_flag='n'").Result;
                    dts.Columns["REQUEST_CODE"].ColumnName = "رقم الطلب";
                    dts.Columns[1].ColumnName = "رقم الكارت";
                    dts.Columns[2].ColumnName = "سبب الطباعة";
                    dts.Columns[3].ColumnName = "طلب من";
                    dts.Columns[4].ColumnName = "تاريخ الطلب";

                    dts.Columns.Add("عن طريق", typeof(String));


                    foreach (DataRow rowz in dts.Rows)
                    {
                        if (rowz[5].ToString() == "P" || rowz[5].ToString() == "p")
                            rowz["عن طريق"] = "سيستم";
                        else
                            rowz["عن طريق"] = "موبايل";



                    }
                    dts.Columns.RemoveAt(5);
                    datagridDeletEmpReqprint.ItemsSource = dts.DefaultView;
                }



            }

            else if (datagridaddEmpReq.ItemsSource == null && AddEempRequestTab.IsSelected == true)
            {
                if (User.Manegar == "Y" || User.Manegar == "y")
                {
                    System.Data.DataTable dts = db.RunReader(@"select REQUEST_CODE,EMP_ANAME,EMP_ENAME,NATIONAL_ID,START_DATE,BIRTHDATE,GENDER,EMP_RELATION,CARD_ID,MOBILE,GLASSES,DISEASE,REGISTER_TYPE,CREATED_BY,CREATED_DATE from EMPLOYEE_REQUEST where TYPE='1'  and approve_flag='W'").Result;
                    dts.Columns["REQUEST_CODE"].ColumnName = "رقم الطلب";
                    dts.Columns[1].ColumnName = "اسم الموظف";
                    dts.Columns[2].ColumnName = "اسم الموظف انجليزى";
                    dts.Columns[3].ColumnName = "رقم البطاقة";
                    dts.Columns[4].ColumnName = "تاريخ البداية";
                    dts.Columns[5].ColumnName = "تاريخ الميلاد";
                    dts.Columns[6].ColumnName = "النوع";
                    dts.Columns[7].ColumnName = "صلة القرابة";
                    dts.Columns[8].ColumnName = "رقم كارت او كود طلب";
                    dts.Columns[9].ColumnName = "الموبايل";
                    dts.Columns[13].ColumnName = "الطلب من";
                    dts.Columns[14].ColumnName = "تاريخ الطلب";

                    dts.Columns.Add("يرتدى نظارة", typeof(String));
                    dts.Columns.Add("يوجد امراض سابقة", typeof(String));
                    dts.Columns.Add("نوع الجهاز", typeof(String));

                    foreach (DataRow rowz in dts.Rows)
                    {
                        if (rowz[12].ToString() == "P" || rowz[12].ToString() == "p")
                            rowz["نوع الجهاز"] = "سيستم";
                        else
                            rowz["نوع الجهاز"] = "موبايل";

                        if (rowz[10].ToString() == "N")
                            rowz["يرتدى نظارة"] = "لا";
                        else
                            rowz["يرتدى نظارة"] = "نعم";

                        if (rowz[11].ToString() == "Y")
                            rowz["يوجد امراض سابقة"] = "نعم";
                        else
                            rowz["يوجد امراض سابقة"] = "لا";

                    }
                    dts.Columns.RemoveAt(10);
                    dts.Columns.RemoveAt(10);
                    dts.Columns.RemoveAt(10);
                    datagridaddEmpReq.ItemsSource = dts.DefaultView;
                }
                else
                {
                    System.Data.DataTable dts = db.RunReader(@"select REQUEST_CODE,EMP_ANAME,EMP_ENAME,NATIONAL_ID,START_DATE,BIRTHDATE,GENDER,EMP_RELATION,CARD_ID,MOBILE,GLASSES,DISEASE,REGISTER_TYPE,CREATED_BY,CREATED_DATE from EMPLOYEE_REQUEST where TYPE='1'  and approve_flag='n'").Result;
                    dts.Columns["REQUEST_CODE"].ColumnName = "رقم الطلب";
                    dts.Columns[1].ColumnName = "اسم الموظف";
                    dts.Columns[2].ColumnName = "اسم الموظف انجليزى";
                    dts.Columns[3].ColumnName = "رقم البطاقة";
                    dts.Columns[4].ColumnName = "تاريخ البداية";
                    dts.Columns[5].ColumnName = "تاريخ الميلاد";
                    dts.Columns[6].ColumnName = "النوع";
                    dts.Columns[7].ColumnName = "صلة القرابة";
                    dts.Columns[8].ColumnName = "رقم كارت او كود طلب";
                    dts.Columns[9].ColumnName = "الموبايل";
                    dts.Columns[13].ColumnName = "الطلب من";
                    dts.Columns[14].ColumnName = "تاريخ الطلب";

                    dts.Columns.Add("يرتدى نظارة", typeof(String));
                    dts.Columns.Add("يوجد امراض سابقة", typeof(String));
                    dts.Columns.Add("نوع الجهاز", typeof(String));

                    foreach (DataRow rowz in dts.Rows)
                    {
                        if (rowz[12].ToString() == "P" || rowz[12].ToString() == "p")
                            rowz["نوع الجهاز"] = "سيستم";
                        else
                            rowz["نوع الجهاز"] = "موبايل";

                        if (rowz[10].ToString() == "N")
                            rowz["يرتدى نظارة"] = "لا";
                        else
                            rowz["يرتدى نظارة"] = "نعم";

                        if (rowz[11].ToString() == "Y")
                            rowz["يوجد امراض سابقة"] = "نعم";
                        else
                            rowz["يوجد امراض سابقة"] = "لا";

                    }
                    dts.Columns.RemoveAt(10);
                    dts.Columns.RemoveAt(10);
                    dts.Columns.RemoveAt(10);
                    datagridaddEmpReq.ItemsSource = dts.DefaultView;
                }


            }

            else if (DeleEempEequestTab.IsSelected == true && datagridDeletEmpReq.ItemsSource == null)
            {


                if (User.Manegar == "n" || User.Manegar == "N")
                {
                    System.Data.DataTable dts = db.RunReader(@"select  REQUEST_CODE , card_id  ,DELIVER_CARD_FLAG,DELIVER_CARD_DATE ,terminate_date,created_by,created_date,REGISTER_TYPE from employee_request where  type='3' and approve_flag='n'").Result;
                    dts.Columns["REQUEST_CODE"].ColumnName = "رقم الطلب";
                    dts.Columns[1].ColumnName = "رقم الكارت";
                    dts.Columns[3].ColumnName = "تاريخ استلام الكارت";
                    dts.Columns[4].ColumnName = "تاريخ الحذف";
                    dts.Columns[5].ColumnName = "طلب من";
                    dts.Columns[6].ColumnName = "تاريخ الطلب";

                    dts.Columns.Add("عن طريق", typeof(String));
                    dts.Columns.Add("استلم الكارت ؟", typeof(String));

                    foreach (DataRow rowz in dts.Rows)
                    {
                        if (rowz[7].ToString() == "P" || rowz[7].ToString() == "p")
                            rowz["عن طريق"] = "سيستم";
                        else
                            rowz["عن طريق"] = "موبايل";

                        if (rowz[2].ToString() == "1")
                            rowz["استلم الكارت ؟"] = "نعم";
                        else
                            rowz["استلم الكارت ؟"] = "لا";

                    }
                    dts.Columns.RemoveAt(7);
                    dts.Columns.RemoveAt(2);
                    dts.SetColumnsOrder("رقم الطلب", "رقم الكارت", "استلم الكارت ؟", "تاريخ استلام الكارت", "تاريخ الحذف", "طلب من", "تاريخ الطلب", "عن طريق");


                    datagridDeletEmpReq.ItemsSource = dts.DefaultView;
                }
                else if (User.Manegar == "Y" || User.Manegar == "y")
                {
                    System.Data.DataTable dts = db.RunReader(@"select  REQUEST_CODE , card_id  ,DELIVER_CARD_FLAG,DELIVER_CARD_DATE ,terminate_date,created_by,created_date,REGISTER_TYPE from employee_request where  type='3' and approve_flag='W'").Result;
                    dts.Columns["REQUEST_CODE"].ColumnName = "رقم الطلب";
                    dts.Columns[1].ColumnName = "رقم الكارت";
                    dts.Columns[3].ColumnName = "تاريخ استلام الكارت";
                    dts.Columns[4].ColumnName = "تاريخ الحذف";
                    dts.Columns[5].ColumnName = "طلب من";
                    dts.Columns[6].ColumnName = "تاريخ الطلب";

                    dts.Columns.Add("عن طريق", typeof(String));
                    dts.Columns.Add("استلم الكارت ؟", typeof(String));

                    foreach (DataRow rowz in dts.Rows)
                    {
                        if (rowz[7].ToString() == "P" || rowz[7].ToString() == "p")
                            rowz["عن طريق"] = "سيستم";
                        else
                            rowz["عن طريق"] = "موبايل";

                        if (rowz[2].ToString() == "1")
                            rowz["استلم الكارت ؟"] = "نعم";
                        else
                            rowz["استلم الكارت ؟"] = "لا";

                    }
                    dts.Columns.RemoveAt(7);
                    dts.Columns.RemoveAt(2);
                    dts.SetColumnsOrder("رقم الطلب", "رقم الكارت", "استلم الكارت ؟", "تاريخ استلام الكارت", "تاريخ الحذف", "طلب من", "تاريخ الطلب", "عن طريق");


                    datagridDeletEmpReq.ItemsSource = dts.DefaultView;
                }

            }

            else if (cheang_emp_tap.IsSelected == true && data_grad_chang_name.ItemsSource == null)
            {
                if (User.Manegar == "Y" || User.Manegar == "y")
                {
                    System.Data.DataTable ine = db.RunReader(@"select REQUEST_CODE,CARD_ID,EMP_ANAME,EMP_ENAME,CREATED_BY,CREATED_DATE,REGISTER_TYPE from EMPLOYEE_REQUEST where TYPE='7' AND APPROVE_FLAG = 'W'  ").Result;
                    ine.Columns[0].ColumnName = "رقم الطلب";
                    ine.Columns[1].ColumnName = "رقم الكارت";
                    ine.Columns[2].ColumnName = "اسم الموظف الجديد بالعربي";
                    ine.Columns[3].ColumnName = "اسم الموظف بالغلة الانجليزيه";
                    ine.Columns[4].ColumnName = "طلب من";
                    ine.Columns[5].ColumnName = "تاريخ الطلب";

                    ine.Columns.Add("عن طريق", typeof(String));

                    foreach (DataRow rowz in ine.Rows)
                    {
                        if (rowz[6].ToString() == "P" || rowz[6].ToString() == "p")
                            rowz["عن طريق"] = "سيستم";
                        else
                            rowz["عن طريق"] = "موبايل";


                    }
                    ine.Columns.RemoveAt(6);
                    data_grad_chang_name.ItemsSource = ine.DefaultView;
                }
                else
                {
                    System.Data.DataTable ine = db.RunReader(@"select REQUEST_CODE,CARD_ID,EMP_ANAME,EMP_ENAME,CREATED_BY,CREATED_DATE,REGISTER_TYPE from EMPLOYEE_REQUEST where TYPE='7' AND APPROVE_FLAG = 'n'  ").Result;
                    ine.Columns[0].ColumnName = "رقم الطلب";
                    ine.Columns[1].ColumnName = "رقم الكارت";
                    ine.Columns[2].ColumnName = "اسم الموظف الجديد بالعربي";
                    ine.Columns[3].ColumnName = "اسم الموظف بالغلة الانجليزيه";
                    ine.Columns[4].ColumnName = "طلب من";
                    ine.Columns[5].ColumnName = "تاريخ الطلب";

                    ine.Columns.Add("عن طريق", typeof(String));

                    foreach (DataRow rowz in ine.Rows)
                    {
                        if (rowz[6].ToString() == "P" || rowz[6].ToString() == "p")
                            rowz["عن طريق"] = "سيستم";
                        else
                            rowz["عن طريق"] = "موبايل";


                    }
                    ine.Columns.RemoveAt(6);
                    data_grad_chang_name.ItemsSource = ine.DefaultView;
                }

            }

            else if (cheang_fah_tap.IsSelected == true && data_find.ItemsSource == null)
            {


                if (User.Manegar == "Y" || User.Manegar == "y")
                {
                    System.Data.DataTable ine = db.RunReader(@"select REQUEST_CODE  , CARD_ID  , EMP_CLASS ,EMP_CLASS_REASON , CREATED_BY , CREATED_DATE ,REGISTER_TYPE   from EMPLOYEE_REQUEST WHERE   REGISTER_TYPE='P' AND approve_flag='W' AND TYPE='2'  ").Result;
                    ine.Columns[0].ColumnName = "رقم الطلب";
                    ine.Columns[1].ColumnName = "رقم الكارت";
                    ine.Columns[2].ColumnName = "رقم الفئة";
                    ine.Columns[3].ColumnName = "سبب التغير";
                    ine.Columns[4].ColumnName = "طلب من";
                    ine.Columns[5].ColumnName = "تاريخ الطلب";
                    ine.Columns.Add("عن طريق", typeof(String));

                    foreach (DataRow rowz in ine.Rows)
                    {
                        if (rowz[6].ToString() == "P" || rowz[6].ToString() == "p")
                            rowz["عن طريق"] = "سيستم";
                        else
                            rowz["عن طريق"] = "موبايل";


                    }
                    ine.Columns.RemoveAt(6);


                    data_find.ItemsSource = ine.DefaultView;
                }
                else
                {
                    System.Data.DataTable ine = db.RunReader(@"select REQUEST_CODE  , CARD_ID  , EMP_CLASS ,EMP_CLASS_REASON , CREATED_BY , CREATED_DATE ,REGISTER_TYPE   from EMPLOYEE_REQUEST WHERE   REGISTER_TYPE='P' AND approve_flag='n' AND TYPE='2'  ").Result;
                    ine.Columns[0].ColumnName = "رقم الطلب";
                    ine.Columns[1].ColumnName = "رقم الكارت";
                    ine.Columns[2].ColumnName = "رقم الفئة";
                    ine.Columns[3].ColumnName = "سبب التغير";
                    ine.Columns[4].ColumnName = "طلب من";
                    ine.Columns[5].ColumnName = "تاريخ الطلب";
                    ine.Columns.Add("عن طريق", typeof(String));

                    foreach (DataRow rowz in ine.Rows)
                    {
                        if (rowz[6].ToString() == "P" || rowz[6].ToString() == "p")
                            rowz["عن طريق"] = "سيستم";
                        else
                            rowz["عن طريق"] = "موبايل";


                    }
                    ine.Columns.RemoveAt(6);


                    data_find.ItemsSource = ine.DefaultView;
                }

            }

            else if (retrn_emp_tap.IsSelected == true && data_searsh_retrn_emp.ItemsSource == null)
            {
                if (User.Manegar == "Y" || User.Manegar == "y")
                {
                    System.Data.DataTable ine = db.RunReader(@"select REQUEST_CODE  , CARD_ID  , REOPEN_DATE , CREATED_BY , CREATED_DATE ,REGISTER_TYPE   from EMPLOYEE_REQUEST WHERE   approve_flag='W' AND TYPE='5'  ").Result;
                    ine.Columns[0].ColumnName = "رقم الطلب";
                    ine.Columns[1].ColumnName = "رقم الكارت";
                    ine.Columns[2].ColumnName = "تاريخ التفعيل";
                    ine.Columns[3].ColumnName = " طلب من";
                    ine.Columns[4].ColumnName = "تاريخ الطلب";

                    ine.Columns.Add("عن طريق", typeof(String));

                    foreach (DataRow rowz in ine.Rows)
                    {
                        if (rowz[5].ToString() == "P" || rowz[5].ToString() == "p")
                            rowz["عن طريق"] = "سيستم";
                        else
                            rowz["عن طريق"] = "موبايل";


                    }
                    ine.Columns.RemoveAt(5);


                    data_searsh_retrn_emp.ItemsSource = ine.DefaultView;
                }
                else
                {
                    System.Data.DataTable ine = db.RunReader(@"select REQUEST_CODE  , CARD_ID  , REOPEN_DATE , CREATED_BY , CREATED_DATE ,REGISTER_TYPE   from EMPLOYEE_REQUEST WHERE   approve_flag='n' AND TYPE='5'  ").Result;
                    ine.Columns[0].ColumnName = "رقم الطلب";
                    ine.Columns[1].ColumnName = "رقم الكارت";
                    ine.Columns[2].ColumnName = "تاريخ التفعيل";
                    ine.Columns[3].ColumnName = " طلب من";
                    ine.Columns[4].ColumnName = "تاريخ الطلب";

                    ine.Columns.Add("عن طريق", typeof(String));

                    foreach (DataRow rowz in ine.Rows)
                    {
                        if (rowz[5].ToString() == "P" || rowz[5].ToString() == "p")
                            rowz["عن طريق"] = "سيستم";
                        else
                            rowz["عن طريق"] = "موبايل";


                    }
                    ine.Columns.RemoveAt(5);


                    data_searsh_retrn_emp.ItemsSource = ine.DefaultView;
                }


            }

            else if (chang_nom_emp_tap.IsSelected == true && data_chang_nom_emp.ItemsSource == null)
            {
                if (User.Manegar == "Y" || User.Manegar == "y")
                {
                    System.Data.DataTable ine = db.RunReader(@"select REQUEST_CODE  , CARD_ID  ,NEW_CARD_ID , CREATED_BY , CREATED_DATE ,REGISTER_TYPE   from EMPLOYEE_REQUEST WHERE   approve_flag='W' AND TYPE='6'  ").Result;
                    ine.Columns[0].ColumnName = "رقم الطلب";
                    ine.Columns[1].ColumnName = "رقم الكارت";
                    ine.Columns[2].ColumnName = "رقم الموظف الجديد";
                    ine.Columns[3].ColumnName = "طلب من";
                    ine.Columns[4].ColumnName = "تاريخ الطلب";

                    ine.Columns.Add("عن طريق", typeof(String));

                    foreach (DataRow rowz in ine.Rows)
                    {
                        if (rowz[5].ToString() == "P" || rowz[5].ToString() == "p")
                            rowz["عن طريق"] = "سيستم";
                        else
                            rowz["عن طريق"] = "موبايل";


                    }
                    ine.Columns.RemoveAt(5);


                    data_chang_nom_emp.ItemsSource = ine.DefaultView;
                }
                else
                {
                    System.Data.DataTable ine = db.RunReader(@"select REQUEST_CODE  , CARD_ID  ,NEW_CARD_ID , CREATED_BY , CREATED_DATE ,REGISTER_TYPE   from EMPLOYEE_REQUEST WHERE   approve_flag='n' AND TYPE='6'  ").Result;
                    ine.Columns[0].ColumnName = "رقم الطلب";
                    ine.Columns[1].ColumnName = "رقم الكارت";
                    ine.Columns[2].ColumnName = "رقم الموظف الجديد";
                    ine.Columns[3].ColumnName = "طلب من";
                    ine.Columns[4].ColumnName = "تاريخ الطلب";

                    ine.Columns.Add("عن طريق", typeof(String));

                    foreach (DataRow rowz in ine.Rows)
                    {
                        if (rowz[5].ToString() == "P" || rowz[5].ToString() == "p")
                            rowz["عن طريق"] = "سيستم";
                        else
                            rowz["عن طريق"] = "موبايل";


                    }
                    ine.Columns.RemoveAt(5);


                    data_chang_nom_emp.ItemsSource = ine.DefaultView;
                }

            }

        }

        private void datagridDeletEmpReq_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lablRequeHR.Content = "";
                lablRequeHR1.Content = "";
                lablRequeHR2.Content = "";
                lablRequeHR3.Content = "";
                lablRequeHR4.Content = "";
                lablRequeHR5.Content = "";
                lablRequeHR6.Content = "";
                lablRequeHR1_Copy.Content = "";
                DataRowView row = (DataRowView)datagridDeletEmpReq.SelectedItem;
                lablRequeHR.Content = row[0].ToString();
                lablRequeHR1.Content = row[1].ToString();
                lablRequeHR2.Content = row[2].ToString();
                lablRequeHR3.Content = row[3].ToString();
                lablRequeHR6.Content = row[4].ToString();
                lablRequeHR4.Content = row[5].ToString();
                lablRequeHR5.Content = row[6].ToString();
                System.Data.DataTable name = db.RunReader("select distinct   EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE CARD_ID ='" + row[1].ToString() + "'").Result;
                lablRequeHR1_Copy.Content = name.Rows[0][0].ToString() + " " + name.Rows[0][1].ToString() + " " + name.Rows[0][2].ToString();

            }
            catch { }



        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.Key == Key.Enter)

                searchdeleteEmp();

        }
        DateTime reqdatefrom, reqdateto;
        void searchdeleteEmp()
        {

            string temp = "";
            if (cbxdeltyp.SelectedIndex == 0)
                temp = "";
            else if (cbxdeltyp.SelectedIndex == 1)
                temp = "m";
            else if (cbxdeltyp.SelectedIndex == 2)
                temp = "P";

            if (fromdatereqdelete.Text == "")

                reqdatefrom = Convert.ToDateTime("01-Jan-1990");
            else
                reqdatefrom = Convert.ToDateTime(fromdatereqdelete.Text);


            if (todatereqdelete.Text == "")
            {
                reqdateto = DateTime.Today;
                reqdateto = reqdateto.AddDays(1);
            }
            else
            {
                reqdateto = Convert.ToDateTime(todatereqdelete.Text);
                reqdateto = reqdateto.AddDays(1);
            }

            System.Data.DataTable dts;
            if (User.Manegar == "Y" || User.Manegar == "y")
                dts = db.RunReader(@"select  REQUEST_CODE , card_id  ,DELIVER_CARD_FLAG,DELIVER_CARD_DATE ,terminate_date,created_by,created_date,REGISTER_TYPE from employee_request where    REGISTER_TYPE like '%" + temp + "%' and type='3' and approve_flag='W' and (REQUEST_CODE like '%" + txthrrequqw.Text + "%' or card_id  like '%" + txthrrequqw.Text + "%' or created_by like '%" + txthrrequqw.Text + "%') and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' )  ").Result;
            else
                dts = db.RunReader(@"select  REQUEST_CODE , card_id  ,DELIVER_CARD_FLAG,DELIVER_CARD_DATE ,terminate_date,created_by,created_date,REGISTER_TYPE from employee_request where    REGISTER_TYPE like '%" + temp + "%' and type='3' and approve_flag='n' and (REQUEST_CODE like '%" + txthrrequqw.Text + "%' or card_id  like '%" + txthrrequqw.Text + "%' or created_by like '%" + txthrrequqw.Text + "%') and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' )  ").Result;




            dts.Columns["REQUEST_CODE"].ColumnName = "رقم الطلب";
            dts.Columns[1].ColumnName = "رقم الكارت";
            dts.Columns[3].ColumnName = "تاريخ استلام الكارت";
            dts.Columns[4].ColumnName = "تاريخ الحذف";
            dts.Columns[5].ColumnName = "طلب من";
            dts.Columns[6].ColumnName = "تاريخ الطلب";

            dts.Columns.Add("عن طريق", typeof(String));
            dts.Columns.Add("استلم الكارت ؟", typeof(String));

            foreach (DataRow rowz in dts.Rows)
            {
                if (rowz[7].ToString() == "P" || rowz[7].ToString() == "p")
                    rowz["عن طريق"] = "سيستم";
                else
                    rowz["عن طريق"] = "موبايل";

                if (rowz[2].ToString() == "1")
                    rowz["استلم الكارت ؟"] = "نعم";
                else
                    rowz["استلم الكارت ؟"] = "لا";

            }
            dts.Columns.RemoveAt(7);
            dts.Columns.RemoveAt(2);
            dts.SetColumnsOrder("رقم الطلب", "رقم الكارت", "استلم الكارت ؟", "تاريخ استلام الكارت", "تاريخ الحذف", "طلب من", "تاريخ الطلب", "عن طريق");

            datagridDeletEmpReq.ItemsSource = dts.DefaultView;
        }

        private void requesthrsearch_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            searchdeleteEmp();
        }

        private void reqdeletehr_Copy1_Click(object sender, RoutedEventArgs e)
        {
            lablRequeHR.Content = "";
            lablRequeHR1.Content = "";
            lablRequeHR2.Content = "";
            lablRequeHR3.Content = "";
            lablRequeHR4.Content = "";
            lablRequeHR5.Content = "";
            lablRequeHR6.Content = "";
            lablRequeHR1_Copy.Content = "";
            txthrrequqw.Text = "";

            System.Data.DataTable dts = db.RunReader(@"select  REQUEST_CODE , card_id  ,DELIVER_CARD_FLAG,DELIVER_CARD_DATE ,terminate_date,created_by,created_date from employee_request where    REGISTER_TYPE='P' and type='3' and approve_flag='n'  ").Result;

            dts.Columns[0].ColumnName = "رقم الطلب";
            dts.Columns[1].ColumnName = "رقم الكارت";
            dts.Columns[2].ColumnName = "استلم الكارت ؟";
            dts.Columns[3].ColumnName = "تاريخ استلام الكارت";
            dts.Columns[4].ColumnName = "تاريخ الحذف";
            dts.Columns[5].ColumnName = "طلب من";
            dts.Columns[6].ColumnName = "تاريخ الطلب";




            datagridDeletEmpReq.ItemsSource = dts.DefaultView;


        }

        private void cbxdeltyp_DropDownClosed(object sender, EventArgs e)
        {
            searchdeleteEmp();
        }


        private void datagridaddEmpReq_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lablRequeHR30.Content = "";
                lablRequeHR31.Content = "";
                lablRequeHR32.Content = "";
                lablRequeHR33.Content = "";
                lablRequeHR34.Content = "";
                lablRequeHR35.Content = "";
                lablRequeHR36.Content = "";
                lablRequeHR37.Content = "";
                lablRequeHR38.Content = "";
                lablRequeHR39.Content = "";
                lablRequeHR310.Content = "";
                picaddrequeaprov.Source = null;


                DataRowView row = (DataRowView)datagridaddEmpReq.SelectedItem;
                lablRequeHR38.Content = row[0].ToString();
                lablRequeHR30.Content = row[1].ToString();
                lablRequeHR31.Content = row[2].ToString();
                lablRequeHR32.Content = row[4].ToString();
                lablRequeHR33.Content = row[5].ToString();
                lablRequeHR34.Content = row[7].ToString();
                lablRequeHR35.Content = row[8].ToString();
                lablRequeHR36.Content = row[13].ToString();
                lablRequeHR37.Content = row[12].ToString();
                lablRequeHR39.Content = row[10].ToString();
                lablRequeHR310.Content = row[11].ToString();


                System.Data.DataTable dpix = db.RunReader("select PRINT_IMG from EMPLOYEE_REQUEST  where REQUEST_CODE = '" + row[0].ToString() + "'").Result;
                if (dpix.Rows.Count > 0)
                {
                    byte[] blob = (byte[])dpix.Rows[0][0];
                    picaddrequeaprov.Source = BitmapImageFromBytes(blob);
                }




            }
            catch
            {

            }
        }

        private void requesthrsearchadd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            searchaddempreq();

        }

        void searchaddempreq()
        {
            string temp = "";
            if (cbxaddtyp.SelectedIndex == 0)
                temp = "";
            else if (cbxaddtyp.SelectedIndex == 1)
                temp = "m";
            else if (cbxaddtyp.SelectedIndex == 2)
                temp = "P";



            if (fromaddnewreq.Text == "")

                reqdatefrom = Convert.ToDateTime("01-Jan-1990");
            else
                reqdatefrom = Convert.ToDateTime(fromaddnewreq.Text);


            if (toaddnewreq.Text == "")
            {
                reqdateto = DateTime.Today;
                reqdateto = reqdateto.AddDays(1);
            }
            else
            {
                reqdateto = Convert.ToDateTime(toaddnewreq.Text);
                reqdateto = reqdateto.AddDays(1);
            }


            System.Data.DataTable dts;
            if (User.Manegar == "Y" || User.Manegar == "y")
                dts = db.RunReader(@"select REQUEST_CODE,EMP_ANAME,EMP_ENAME,NATIONAL_ID,START_DATE,BIRTHDATE,GENDER,EMP_RELATION,CARD_ID,MOBILE,GLASSES,DISEASE,REGISTER_TYPE,CREATED_BY,CREATED_DATE from EMPLOYEE_REQUEST where TYPE='1'  and approve_flag='W' and REGISTER_TYPE like '%" + temp + "%' and (REQUEST_CODE like '%" + txthrrequqwadd.Text + "%' or card_id  like '%" + txthrrequqwadd.Text + "%' or created_by like '%" + txthrrequqwadd.Text + "%') and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' ) ").Result;

            else
                dts = db.RunReader(@"select REQUEST_CODE,EMP_ANAME,EMP_ENAME,NATIONAL_ID,START_DATE,BIRTHDATE,GENDER,EMP_RELATION,CARD_ID,MOBILE,GLASSES,DISEASE,REGISTER_TYPE,CREATED_BY,CREATED_DATE from EMPLOYEE_REQUEST where TYPE='1'  and approve_flag='n' and REGISTER_TYPE like '%" + temp + "%' and (REQUEST_CODE like '%" + txthrrequqwadd.Text + "%' or card_id  like '%" + txthrrequqwadd.Text + "%' or created_by like '%" + txthrrequqwadd.Text + "%') and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' ) ").Result;

            dts.Columns["REQUEST_CODE"].ColumnName = "رقم الطلب";
            dts.Columns[1].ColumnName = "اسم الموظف";
            dts.Columns[2].ColumnName = "اسم الموظف انجليزى";
            dts.Columns[3].ColumnName = "رقم البطاقة";
            dts.Columns[4].ColumnName = "تاريخ البداية";
            dts.Columns[5].ColumnName = "تاريخ الميلاد";
            dts.Columns[6].ColumnName = "النوع";
            dts.Columns[7].ColumnName = "صلة القرابة";
            dts.Columns[8].ColumnName = "رقم كارت او كود طلب";
            dts.Columns[9].ColumnName = "الموبايل";
            dts.Columns[13].ColumnName = "الطلب من";
            dts.Columns[14].ColumnName = "تاريخ الطلب";

            dts.Columns.Add("يرتدى نظارة", typeof(String));
            dts.Columns.Add("يوجد امراض سابقة", typeof(String));
            dts.Columns.Add("نوع الجهاز", typeof(String));

            foreach (DataRow rowz in dts.Rows)
            {
                if (rowz[12].ToString() == "P" || rowz[12].ToString() == "p")
                    rowz["نوع الجهاز"] = "سيستم";
                else
                    rowz["نوع الجهاز"] = "موبايل";

                if (rowz[10].ToString() == "N")
                    rowz["يرتدى نظارة"] = "لا";
                else
                    rowz["يرتدى نظارة"] = "نعم";

                if (rowz[11].ToString() == "Y")
                    rowz["يوجد امراض سابقة"] = "نعم";
                else
                    rowz["يوجد امراض سابقة"] = "لا";

            }
            dts.Columns.RemoveAt(10);
            dts.Columns.RemoveAt(10);
            dts.Columns.RemoveAt(10);
            datagridaddEmpReq.ItemsSource = dts.DefaultView;
        }

        private void cbxaddtyp_DropDownClosed(object sender, EventArgs e)
        {
            searchaddempreq();
        }

        private void txthrrequqwadd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchaddempreq();
            }
        }

        private void reqaddhrfalse_Copy_Click(object sender, RoutedEventArgs e)
        {



            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)picaddrequeaprov.Source));
            using (FileStream stream = new FileStream("D:\\aaa.png", FileMode.Create))
                encoder.Save(stream);








        }

        private void Requests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxdeltypprint_DropDownClosed(object sender, EventArgs e)
        {
            printreasonreque();
        }

        void printreasonreque()
        {
            string temp = "";
            if (cbxdeltypprint.SelectedIndex == 0)
                temp = "";
            else if (cbxdeltypprint.SelectedIndex == 1)
                temp = "m";
            else if (cbxdeltypprint.SelectedIndex == 2)
                temp = "p";


            if (fromreprintreqs.Text == "")

                reqdatefrom = Convert.ToDateTime("01-Jan-1990");
            else
                reqdatefrom = Convert.ToDateTime(fromreprintreqs.Text);


            if (toreprintreqs.Text == "")
            {
                reqdateto = DateTime.Today;
                reqdateto = reqdateto.AddDays(1);
            }
            else
            {
                reqdateto = Convert.ToDateTime(toreprintreqs.Text);
                reqdateto = reqdateto.AddDays(1);
            }
            System.Data.DataTable dts;
            if (User.Manegar == "Y" || User.Manegar == "y")
                dts = db.RunReader(@"select  REQUEST_CODE,CARD_ID,PRINT_REASON ,CREATED_BY ,CREATED_DATE ,REGISTER_TYPE from employee_request where type='4'  and approve_flag='W' and  REGISTER_TYPE like '%" + temp + "%' and  (REQUEST_CODE like '%" + txthrrequqwprint.Text + "%' or card_id  like '%" + txthrrequqwprint.Text + "%' or created_by like '%" + txthrrequqwprint.Text + "%') and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' ) ").Result;

            else
                dts = db.RunReader(@"select  REQUEST_CODE,CARD_ID,PRINT_REASON ,CREATED_BY ,CREATED_DATE ,REGISTER_TYPE from employee_request where type='4'  and approve_flag='n' and  REGISTER_TYPE like '%" + temp + "%' and  (REQUEST_CODE like '%" + txthrrequqwprint.Text + "%' or card_id  like '%" + txthrrequqwprint.Text + "%' or created_by like '%" + txthrrequqwprint.Text + "%') and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' ) ").Result;


            dts.Columns["REQUEST_CODE"].ColumnName = "رقم الطلب";
            dts.Columns[1].ColumnName = "رقم الكارت";
            dts.Columns[2].ColumnName = "سبب الطباعة";
            dts.Columns[3].ColumnName = "طلب من";
            dts.Columns[4].ColumnName = "تاريخ الطلب";

            dts.Columns.Add("عن طريق", typeof(String));


            foreach (DataRow rowz in dts.Rows)
            {
                if (rowz[5].ToString() == "P" || rowz[5].ToString() == "p")
                    rowz["عن طريق"] = "سيستم";
                else
                    rowz["عن طريق"] = "موبايل";



            }
            dts.Columns.RemoveAt(5);
            datagridDeletEmpReqprint.ItemsSource = dts.DefaultView;
        }

        private void requesthrsearchprint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            printreasonreque();
        }

        private void txthrrequqwprint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                printreasonreque();
        }

        private void reqaddhr_Click(object sender, RoutedEventArgs e)
        {
            lablRequeHR30.Content = "";
            lablRequeHR31.Content = "";
            lablRequeHR32.Content = "";
            lablRequeHR33.Content = "";
            lablRequeHR34.Content = "";
            lablRequeHR35.Content = "";
            lablRequeHR36.Content = "";
            lablRequeHR37.Content = "";
            lablRequeHR38.Content = "";
            lablRequeHR39.Content = "";
            lablRequeHR310.Content = "";
            picaddrequeaprov.Source = null;
            txthrrequqwadd.Text = "";
            cbxaddtyp.SelectedIndex = 0;
        }

        private void reqdeletehr_Copy3_Click(object sender, RoutedEventArgs e)
        {
            lablRequeHR8.Content = "";
            lablRequeHR1_Copy1.Content = "";
            lablRequeHR9.Content = "";
            lablRequeHR70.Content = "";
            lablRequeHR11.Content = "";
            lablRequeHR12.Content = "";
            picaddrequeaprov1.Source = null;

            cbxdeltypprint.SelectedIndex = 0;
            txthrrequqwprint.Text = "";
        }

        private void datagridDeletEmpReqprint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lablRequeHR8.Content = "";
            lablRequeHR1_Copy1.Content = "";
            lablRequeHR9.Content = "";
            lablRequeHR70.Content = "";
            lablRequeHR11.Content = "";
            lablRequeHR12.Content = "";
            picaddrequeaprov1.Source = null;

            try
            {
                DataRowView row = (DataRowView)datagridDeletEmpReqprint.SelectedItem;
                lablRequeHR70.Content = row[0].ToString();
                lablRequeHR8.Content = row[1].ToString();


                System.Data.DataTable name = db.RunReader("select distinct   EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE CARD_ID ='" + row[1].ToString() + "'").Result;
                lablRequeHR1_Copy1.Content = name.Rows[0][0].ToString() + " " + name.Rows[0][1].ToString() + " " + name.Rows[0][2].ToString();



                lablRequeHR9.Content = row[2].ToString();
                lablRequeHR11.Content = row[3].ToString();
                lablRequeHR12.Content = row[4].ToString();

                if (row[2].ToString() == "تغيير صورة")
                {

                    System.Data.DataTable dpix = db.RunReader("select PRINT_IMG from EMPLOYEE_REQUEST  where REQUEST_CODE = '" + row[0].ToString() + "'").Result;
                    if (dpix.Rows.Count > 0)
                    {
                        byte[] blob = (byte[])dpix.Rows[0][0];
                        picaddrequeaprov1.Source = BitmapImageFromBytes(blob);
                    }

                }
            }
            catch { }

        }

        private void reqaddhrfalse_Copy1_Click(object sender, RoutedEventArgs e)
        {

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)picaddrequeaprov1.Source));
            using (FileStream stream = new FileStream("D:\\aaa.png", FileMode.Create))
                encoder.Save(stream);


        }

        hrClass hr = new hrClass();
        private void saveEmpBtn22_Click(object sender, RoutedEventArgs e)
        {
            string branch = "";
            try
            {
                if (nationalidtxt.Text.Length != 14)
                {
                    MessageBox.Show("من فضلك ادخل 14 رقم للبطاقة");
                }
                else if (branchCombo.SelectedItem == null && branchCombo.ItemsSource != null)
                {
                    MessageBox.Show("من فضلك اختر الفرع");
                }
                else if (empnumtxt.Text == "" && empnumtxt.Visibility == Visibility.Visible)
                {
                    MessageBox.Show("من فضلك اختر رقم كارت الموظف");
                }
                else
                {
                    string ename = enametxt.Text.ToString();
                    string aname = anametxt.Text.ToString();
                    string enamest = enamesttxt.Text.ToString();
                    string enamesc = enamesctxt.Text.ToString();
                    string enameth = enamethtxt.Text.ToString();
                    string enamefr = enamefrtxt.Text.ToString();
                    string anamest = anamesttxt.Text.ToString();
                    string anamesc = anamesctxt.Text.ToString();
                    string anameth = anamethtxt.Text.ToString();
                    string anamefr = anamefrtxt.Text.ToString();
                    string address = addrtxt.Text.ToString();
                    string mob = mobnumtxt.Text.ToString();
                    string empid = "";
                    if (empnumtxt.Visibility == Visibility.Visible)
                    {
                        empid = empnumtxt.Text.ToString();
                    }
                    if (branchCombo.ItemsSource != null && branchCombo.SelectedItem != null)
                    {
                        branch = branchCombo.SelectedItem.ToString();
                    }
                    else
                    {
                        branch = "";
                    }
                    string email = emailtxt.Text.ToString();
                    string nationalid = (nationalidtxt.Text.ToString());

                    string relation = "";
                    if (emprb.IsChecked == true)
                    {
                        relation = "self";
                    }
                    else if (childrb.IsChecked == true)
                    {
                        relation = "Son/Daughter";
                    }
                    else if (parentrb.IsChecked == true)
                    {
                        relation = "father/mother";
                    }
                    else if (husbandrb.IsChecked == true)
                    {
                        relation = "husband/wife";
                    }
                    string birthdate = birthdatetxt.Text.ToString();
                    string startdate = startdatetxt.Text.ToString();
                    string gender = "";
                    if (femalerb.IsChecked == true)
                    {
                        gender = "Female";
                    }
                    else if (malerb.IsChecked == true)
                    {
                        gender = "Male";
                    }
                    string zz = "N";
                    if (mred.IsChecked == true)
                        zz = "Y";

                    string zz2 = "N";
                    if (ndara.IsChecked == true)
                        zz2 = "Y";


                    System.Data.DataTable a = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where national_id = '" + nationalid + "' ").Result;
                    if (a.Rows.Count > 0)
                        MessageBox.Show("هذا الطلب تحت الدراسة والتنفيذ");
                    else
                    {


                        hr.add_employee_request(enamest, enamesc, enameth, enamefr, ename, anamest, anamesc, anameth, anamefr, aname, email
                            , address, nationalid, birthdate, gender, branch, relation, empid, mob, startdate, UserName, zz, zz2, emailtxt_Copy.Text, classtxt_Copy.Text);
                        System.Data.DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where national_id = '" + nationalid + "' ").Result;
                        lblf.Content = temp.Rows[0][0].ToString();
                        searchtxt.Text = temp.Rows[0][0].ToString();

                        if (empphtoo.Source != null)
                        {
                            FileStream fls;
                            fls = new FileStream(@path2, FileMode.Open, FileAccess.Read);
                            //a byte array to read the image 
                            byte[] blob = new byte[fls.Length];
                            fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                            fls.Close();


                            string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=********** )(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
                            conn = new OracleConnection(connectionStr);
                            conn.Open();
                            OracleCommand cmnd;
                            string query;
                            query = @"UPDATE EMPLOYEE_REQUEST SET  PRINT_IMG=:BlobParameter where REQUEST_CODE ='" + temp.Rows[0][0].ToString() + "'";
                            //insert the byte as oracle parameter of type blob 
                            OracleParameter blobParameter = new OracleParameter();
                            blobParameter.OracleType = OracleType.Blob;
                            blobParameter.ParameterName = "BlobParameter";
                            blobParameter.Value = blob;
                            cmnd = new OracleCommand(query, conn);
                            cmnd.Parameters.Add(blobParameter);
                            cmnd.ExecuteNonQuery();

                            conn.Close();

                            MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString());
                            db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('1','" + temp.Rows[0][0].ToString() + "','N','" + User.Name + "')");

                        }
                        else
                        {
                            MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString() + "\n" + "مع مراعاة انها لم يتم ارفاق صورة الموظف");
                            db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('1','" + temp.Rows[0][0].ToString() + "','N','" + User.Name + "')");

                        }



                        saveEmpBtn22.IsEnabled = false;
                        btnediteemp.IsEnabled = true;

                    }
                }
            }
            catch { }
        }

        private void hrReq_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void newEmpBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NewEmpCompCombo.Text = "";
                mred.IsChecked = false;
                ndara.IsChecked = false;
                zz3.IsChecked = true;
                zz4.IsChecked = true;
                lblf.Content = "******";
                enametxt.Text = "";
                enamesttxt.Text = "";
                enamethtxt.Text = "";
                enamefrtxt.Text = "";
                enamesctxt.Text = "";
                anametxt.Text = "";
                anamesttxt.Text = "";
                anamesctxt.Text = "";
                anamethtxt.Text = "";
                anamefrtxt.Text = "";
                nationalidtxt.Text = "";
                birthdatetxt.Text = "";
                empnumtxt.Text = "";
                startdatetxt.Text = "";
                searchtxt.Text = "";
                addrtxt.Text = "";
                emailtxt.Text = "";
                branchCombo.Text = "";
                mobnumtxt.Text = "";
                femalerb.IsChecked = false; malerb.IsChecked = false;
                emprb.IsChecked = false;
                husbandrb.IsChecked = false;
                childrb.IsChecked = false;
                parentrb.IsChecked = false;
                btnediteemp.IsEnabled = false;
                saveEmpBtn22.IsEnabled = true;
                empphtoo.Source = null;
            }
            catch { }

        }

        private void nationalidtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(nationalidtxt.Text, "[^0-9]"))
                {
                    nationalidtxt.Text = nationalidtxt.Text.Remove(nationalidtxt.Text.Length - 1);
                }
            }
            catch { }
        }

        private void deleteEmpNumtxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(deleteEmpNumtxt.Text, "[^0-9]"))
                {
                    deleteEmpNumtxt.Text = deleteEmpNumtxt.Text.Remove(deleteEmpNumtxt.Text.Length - 1);
                }
            }
            catch { }
        }

        private void deleteEmpSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (deleteEmpNumtxt.SelectedItem == null)
                {
                    MessageBox.Show("من فضلك اختر رقم الموظف");
                }
                else
                {
                    string empid = (deleteEmpNumtxt.Text.ToString());
                    string deliverdate = "";
                    if (receiveCarddatetxt.Visibility == Visibility.Visible && receiveCarddatetxt.Text == "")
                    {
                        MessageBox.Show("من فضلك ادخل تاريخ استلام الكارت");
                    }
                    else
                    {
                        deliverdate = receiveCarddatetxt.Text.ToString();


                        string flag = "";
                        if (deliverCardorb.IsChecked == true)
                        {
                            flag = "2";
                        }
                        else if (deliverCardyesrb.IsChecked == true)
                        {
                            flag = "1";
                        }
                        string term_date = deleteEmpDatetxt.Text.ToString();
                        hr.terminate_employee_request(empid, term_date, flag, deliverdate, UserName);
                        System.Data.DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where card_id ='" + empid + "' and terminate_date ='" + term_date + "' and DELIVER_CARD_FLAG ='" + flag + "'  and approve_flag ='n' and REGISTER_TYPE ='P'and type='3'").Result;
                        lbl22.Content = temp.Rows[0][0].ToString();
                        MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString());
                        db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('1','" + temp.Rows[0][0].ToString() + "','N','" + User.Name + "')");

                        //   MessageBox.Show("تم ارسال طلب تعديل فئة موظف");
                        //  MessageBox.Show("تم ارسال طلب حذف موظف");
                    }
                }
            }
            catch { }
        }

        private void deleteEmpNewBtn_Click(object sender, RoutedEventArgs e)
        {
            deleteEmpNumtxt.Text = "";
            deleteEmpDatetxt.Text = "";
            deliverCardorb.IsChecked = false;
            deliverCardyesrb.IsChecked = false;
            receiveCarddatetxt.Text = "";
            lbl22.Content = "*********";
            delEmpCompCombo.Text = "";
        }

        private void EditClassSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cardnumtxt22.Text == "" || classtxt.SelectedItem == null)
                {
                    MessageBox.Show("من فضلك ادخل البيانات كاملة");
                }
                else
                {


                    string card = cardnumtxt22.Text.ToString();
                    string[] arr = card.Split('-');
                    int comp = Convert.ToInt32(arr[0].ToString());
                    int compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                    if (comp == compid)
                    {
                        int count = client.validate_CardInCompEmployees(card);
                        if (count >= 1)
                        {
                            string newclass = classtxt.SelectedItem.ToString();
                            string richText = new TextRange(reasontxt22.Document.ContentStart, reasontxt22.Document.ContentEnd).Text;
                            hr.edit_class_request(card, newclass, richText, UserName, compid, Convert.ToDateTime(birthdatetxt_Copy.Text));

                            System.Data.DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where card_id ='" + card + "' and REGISTER_TYPE ='P' and type='2' and created_by ='" + UserName + "' and emp_class ='" + newclass + "' and approve_flag ='n'").Result;
                            lblz.Content = temp.Rows[0][0].ToString();
                            MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString());
                            db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('1','" + temp.Rows[0][0].ToString() + "','N','" + User.Name + "')");

                            //   MessageBox.Show("تم ارسال طلب تعديل فئة موظف");
                        }
                        else
                        {
                            MessageBox.Show("كارت غير موجود");
                        }
                    }
                    else
                    {
                        MessageBox.Show("غير مسموح");
                    }
                }
            }
            catch { }
        }

        private void editClassNewBtn_Click(object sender, RoutedEventArgs e)
        {
            classtxt.Text = "";
            reasontxt22.Document.Blocks.Clear();
            cardnumtxt22.Text = "";
            lblz.Content = "******";
            classEmpCompCombo.Text = "";
        }

        //  HRNetwork hrnet = new HRNetwork();
        private void fill_emp_code()
        {
            try
            {
                int compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                DataSet dataset_emp_card = db.RunReaderds("select distinct  emp_code ,EMP_eNAME_ST ,EMP_eNAME_SC,EMP_eNAME_TH  from COMP_EMPLOYEESS WHERE C_COMP_ID=" + compid + " ORDER BY emp_code ");
                deleteEmpNumtxt.ItemsSource = dataset_emp_card.Tables[0].DefaultView;
            }
            catch { }
        }
        private void fill_card_id(ComboBox c)
        {
            try
            {
                int compid = 0;
                if (User.Type == "hr")
                {
                    compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                }
                else
                {
                    compid = Convert.ToInt32(classEmpCompCombo.Text.ToString());
                }
                int contract = hrnet.get_max_contract(compid);
                DataSet dataset_emp_card = db.RunReaderds("select distinct  card_id ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH from COMP_EMPLOYEESS WHERE C_COMP_ID=" + compid + " and contract_no=" + contract + " ORDER BY card_id ");
                c.ItemsSource = dataset_emp_card.Tables[0].DefaultView;
            }
            catch { }
        }
        private void employeetab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (reprint.IsSelected == true && cbxcardnoqw.ItemsSource == null)
            {
                try
                {



                    if (User.Type == "DMS Member" && cbxclassEmpCompCombo.ItemsSource == null)
                    {
                        fillqw();
                        AutoNumeqw();
                        addpic.Visibility = Visibility.Hidden;
                        addpic_Copy.Visibility = Visibility.Hidden;
                        cbxclassEmpCompCombo.ItemsSource = User.ALL_Company().DefaultView;
                    }
                    else if (User.Type == "hr")
                    {
                        fillqw();
                        AutoNumeqw();
                        addpic.Visibility = Visibility.Hidden;
                        addpic_Copy.Visibility = Visibility.Hidden;
                        poiu.Visibility = Visibility.Hidden;
                        cbxclassEmpCompCombo.Visibility = Visibility.Hidden;
                        imgsearchCust_Copy122.Visibility = Visibility.Hidden;
                        fillcardname(cbxcardnoqw);
                    }

                }
                catch { }
            }



            else if (addemp.IsSelected == true && branchCombo.SelectedItem == null && empnumtxt.Text == "" && empnumtxt.Visibility == Visibility.Hidden && NewEmpCompCombo.ItemsSource == null)
            {
                try
                {


                    if (User.Type == "DMS Member")
                    {
                        NewEmpCompCombo.ItemsSource = db.RunReader("select distinct C_COMP_ID ,C_ANAME from V_COMPANIES order by c_comp_id").Result.DefaultView;
                    }
                    else
                    {
                        NewEmpCompCombo.Visibility = Visibility.Hidden;
                        //zxcv.Visibility = Visibility.Hidden;
                        NewEmpCompanySrchBtn.Visibility = Visibility.Hidden;

                    }

                    int compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                    System.Data.DataTable data = hr.get_branch(compid);
                    branchCombo.Items.Clear();
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        branchCombo.Items.Add(data.Rows[i].ItemArray[0].ToString());
                    }
                    fill_card_id(empnumtxt);
                }
                catch { }
            }
            else if (deleteemp.IsSelected == true && deleteEmpNumtxt.Text == "" && deleteEmpNumtxt.ItemsSource == null)
            {

                if (User.Type == "DMS Member" && delEmpCompCombo.ItemsSource == null)
                {
                    delEmpCompCombo.ItemsSource = db.RunReader("select distinct C_COMP_ID ,C_ANAME from V_COMPANIES order by c_comp_id").Result.DefaultView;
                }
                else if (User.Type == "hr")
                {
                    delEmpCompCombo.Visibility = Visibility.Hidden;
                    delcomp.Visibility = Visibility.Hidden;
                    DeleteEmpSrchCardBtn_Copy.Visibility = Visibility.Hidden;
                    try
                    {
                        fill_card_id(deleteEmpNumtxt);
                    }
                    catch { }

                }

            }
            else if (editemp.IsSelected == true && cardnumtxt22.SelectedItem == null && classtxt.SelectedItem == null)
            {
                try
                {


                    if (User.Type == "DMS Member" && classEmpCompCombo.ItemsSource == null)
                    {
                        classEmpCompCombo.ItemsSource = db.RunReader("select distinct C_COMP_ID ,C_ANAME from V_COMPANIES order by c_comp_id").Result.DefaultView;


                    }
                    else if (User.Type == "hr")
                    {
                        eteEmpSrchCardBtn_Copy.Visibility = Visibility.Hidden;
                        classEmpCompCombo.Visibility = Visibility.Hidden;
                        editcomplbl.Visibility = Visibility.Hidden;
                        fill_card_id(cardnumtxt22);
                        int compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                        System.Data.DataTable classcode = hr.get_class_name(compid);
                        classtxt.Items.Clear();
                        for (int i = 0; i < classcode.Rows.Count; i++)
                        {
                            classtxt.Items.Add(classcode.Rows[i].ItemArray[0].ToString());
                        }
                    }
                }
                catch { }
            }
        }

        private void emprb_Checked(object sender, RoutedEventArgs e)
        {
            empnumlbl.Visibility = Visibility.Hidden;
            empnumtxt.Visibility = Visibility.Hidden;
        }

        private void childrb_Checked(object sender, RoutedEventArgs e)
        {
            empnumlbl.Visibility = Visibility.Visible;
            empnumtxt.Visibility = Visibility.Visible;
        }

        private void husbandrb_Checked(object sender, RoutedEventArgs e)
        {
            empnumlbl.Visibility = Visibility.Visible;
            empnumtxt.Visibility = Visibility.Visible;
        }

        private void parentrb_Checked(object sender, RoutedEventArgs e)
        {

            empnumlbl.Visibility = Visibility.Visible;
            empnumtxt.Visibility = Visibility.Visible;
        }

        private void mainTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (reprint.IsSelected == true && cbxcardnoqw.ItemsSource == null)
            {
                try
                {



                    if (User.Type == "DMS Member" && cbxclassEmpCompCombo.ItemsSource == null)
                    {
                        fillqw();
                        AutoNumeqw();
                        addpic.Visibility = Visibility.Hidden;
                        addpic_Copy.Visibility = Visibility.Hidden;
                        cbxclassEmpCompCombo.ItemsSource = User.ALL_Company().DefaultView;
                    }
                    else if (User.Type == "hr")
                    {
                        fillqw();
                        AutoNumeqw();
                        addpic.Visibility = Visibility.Hidden;
                        addpic_Copy.Visibility = Visibility.Hidden;
                        poiu.Visibility = Visibility.Hidden;
                        cbxclassEmpCompCombo.Visibility = Visibility.Hidden;
                        imgsearchCust_Copy122.Visibility = Visibility.Hidden;
                        fillcardname(cbxcardnoqw);
                    }

                }
                catch { }
            }

        }

        #region abdo
        private void addproviderwq_Loaded(object sender, RoutedEventArgs e)
        {

        }
        //   DB db = new DB();
        string numqw, username, companyNameqw, companyIDqw, areacodeqw;
        DataSet dataset_emp_card;
        void fillqw()
        {
            try
            {

                DataSet dt = db.RunReaderds(" select distinct BS_CODE,BS_ANAME from AREA_VIEW where BS_CODE_UP is null  ORDER BY BS_CODE ");
                cbxstateqw.ItemsSource = dt.Tables[0].DefaultView;

                dt = db.RunReaderds("   select distinct PRV_TYPE,TYP_ANAME from PROVIDER_TYP22  ORDER BY PRV_TYPE");
                cbxprovidertypqw.ItemsSource = dt.Tables[0].DefaultView;

                //    MessageBox.Show(companyNameqw);
                DataSet s = db.RunReaderds(" select distinct C_COMP_ID  from V_COMPANIES where C_ENAME ='" + companyNameqw + "'");
                companyIDqw = s.Tables[0].Rows[0][0].ToString();
                txtcardnumnewqw_Copy.Text = companyIDqw;
            }
            catch { }
        }
        void fillareaqw()
        {
            try
            {
                DataSet dt = db.RunReaderds(" select distinct BS_CODE,BS_ANAME from AREA_VIEW where BS_CODE_UP ='" + areacodeqw + "'  ORDER BY BS_CODE ");
                cbxareaqw.ItemsSource = dt.Tables[0].DefaultView;
            }
            catch { }
        }
        private void AutoNumeqw()
        {
            try
            {
                System.Data.DataTable dtouto = db.RunReader("select count(CODE) from HR_PROVIDERS_REQUEST ").Result;

                if (dtouto.Rows[0][0].ToString() != DBNull.Value.ToString())
                    numqw = (Convert.ToInt32(dtouto.Rows[0][0].ToString()) + 1).ToString();
                else
                    numqw = "1";
            }
            catch { }



        }

        private void reqdeletehr_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'W' WHERE REQUEST_CODE ='" + lablRequeHR.Content.ToString() + "'", "سيتم مراجعة الطلب");
            db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('3','" + lablRequeHR.Content.ToString() + "','N','" + lablRequeHR4.Content.ToString() + "')");

            searchdeleteEmp();
        }

        private void reqaddhrtru_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'W' WHERE REQUEST_CODE ='" + lablRequeHR38.Content.ToString() + "'", "سيتم مراجعة الطلب");
            db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('3','" + lablRequeHR38.Content.ToString() + "','N','" + lablRequeHR39.Content.ToString() + "')");

            searchaddempreq();
        }

        private void reqdeletehr1_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'W' WHERE REQUEST_CODE ='" + lablRequeHR70.Content.ToString() + "'", "سيتم مراجعة الطلب");
            db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('3','" + lablRequeHR70.Content.ToString() + "','N','" + lablRequeHR11.Content.ToString() + "')");

            printreasonreque();

        }

        private void reqdelqetehr1_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'W' WHERE REQUEST_CODE ='" + label_request.Content.ToString() + "'", "سيتم مراجعة الطلب");
            db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('3','" + label_request.Content.ToString() + "','N','" + label_request_about.Content.ToString() + "')");

            searchshangeempname();
        }

        private void reqdeletehr2_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'W' WHERE REQUEST_CODE ='" + labl_Request_code.Content.ToString() + "'", "سيتم مراجعة الطلب");
            db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('3','" + labl_Request_code.Content.ToString() + "','N','" + labl_Created_By.Content.ToString() + "')");

            sarsh_chang();
        }

        private void reqdeletehr3_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'W' WHERE REQUEST_CODE ='" + labl_Request_code1.Content.ToString() + "'", "سيتم مراجعة الطلب");
            db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('3','" + labl_Request_code1.Content.ToString() + "','N','" + labl_Created_By1.Content.ToString() + "')");

            sarsh_chang2();
        }

        private void reqdeletehr4_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'W' WHERE REQUEST_CODE ='" + labl_Request_code2.Content.ToString() + "'", "سيتم مراجعة الطلب");
            db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('3','" + labl_Request_code2.Content.ToString() + "','N','" + labl_Created_By2.Content.ToString() + "')");

            sarsh_chang3();
        }

        private void reqdeletehr_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (refdelreq.Text != "")
            {

                System.Data.DataTable mail = db.RunReader("select mail from agent where name='" + lablRequeHR4.Content.ToString() + "'").Result;

                if (mail.Rows[0][0].ToString() == "")
                {

                    db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'F' WHERE REQUEST_CODE ='" + lablRequeHR.Content.ToString() + "'", "تم حفظ البيانات مع مراعاة انه لم يتم ارسال بريد الكترونى");
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('2','" + lablRequeHR.Content.ToString() + "','N','" + lablRequeHR4.Content.ToString() + "')");
                    searchdeleteEmp();
                }
                else
                {

                    var sssssssssssss = new MailAddress("zajjof@gmail.com", "DMS");
                    var dddddddd = new MailAddress(mail.Rows[0][0].ToString(), "From DMS To You");
                    const string fromPassword = "Abd0.G@m@1";
                    string subject = " تم رفض طلب حذف موظف ";
                    string talb = @"كود الطلب  " + "\n"
                                 + lablRequeHR.Content.ToString() + "\n"
                                 + "رقم الكارت: " + "\n" +
                                 lablRequeHR1.Content.ToString() + "\n" +
                                   "اسم الموظف : " + "\n" +
                                 lablRequeHR1_Copy.Content.ToString() + "\n" +
                                    "استلم الكارت ؟ : " + "\n" +
                                 lablRequeHR2.Content.ToString() + "\n" +
                                    "تاريخ استلام الكارت : " + "\n" +
                                 lablRequeHR3.Content.ToString() + "\n" +
                                    "تاريخ الحذف : " + "\n" +
                                 lablRequeHR6.Content.ToString() + "\n" +
                                    "تاريخ الطلب : " + "\n" +
                                 lablRequeHR5.Content.ToString() + "\n" +
                                     "سبب الرفض : " + "\n" +
                                 refdelreq.Text.ToString() + "\n";
                    Document s = new Document();


                    string body = talb;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sssssssssssss.Address, fromPassword)
                    };
                    using (var message = new MailMessage(sssssssssssss, dddddddd)
                    {
                        Subject = subject,
                        Body = body



                    })
                    {
                        smtp.Send(message);
                    }
                    db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'F' WHERE REQUEST_CODE ='" + lablRequeHR.Content.ToString() + "'", "تم حفظ البيانات و تم ارسال البريد بنجاح");
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('2','" + lablRequeHR.Content.ToString() + "','N','" + lablRequeHR4.Content.ToString() + "')");

                    searchdeleteEmp();

                }


            }
            else
                MessageBox.Show("برجاء كتابة سبب الرفض");
        }

        private void reqaddhrfalse_Click(object sender, RoutedEventArgs e)
        {
            if (addreprequ.Text != "")
            {

                System.Data.DataTable mail = db.RunReader("select mail from agent where name='" + lablRequeHR39.Content.ToString() + "'").Result;

                if (mail.Rows[0][0].ToString() == "")
                {

                    db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'F' WHERE REQUEST_CODE ='" + lablRequeHR38.Content.ToString() + "'", "تم حفظ البيانات مع مراعاة انه لم يتم ارسال بريد الكترونى");
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('2','" + lablRequeHR38.Content.ToString() + "','N','" + lablRequeHR39.Content.ToString() + "')");
                    searchaddempreq();
                }
                else
                {

                    var sssssssssssss = new MailAddress("zajjof@gmail.com", "DMS");
                    var dddddddd = new MailAddress(mail.Rows[0][0].ToString(), "From DMS To Name");
                    const string fromPassword = "Abd0.G@m@1";
                    string subject = " تم رفض اضافة تغطية تامينية ";
                    string talb = @"كود الطلب  " + "\n"
                                 + lablRequeHR38.Content.ToString() + "\n"
                                 + "اسم الموظف انجليزى: " + "\n" +
                                 lablRequeHR31.Content.ToString() + "\n" +
                                   "اسم الموظف : " + "\n" +
                                 lablRequeHR30.Content.ToString() + "\n" +
                                    "صلة القرابة : " + "\n" +
                                 lablRequeHR34.Content.ToString() + "\n" +
                                    "رقم الكارت او الطلب : " + "\n" +
                                 lablRequeHR35.Content.ToString() + "\n" +

                                    "تاريخ الطلب : " + "\n" +
                                 lablRequeHR310.Content.ToString() + "\n" +
                                     "سبب الرفض : " + "\n" +
                                 addreprequ.Text.ToString() + "\n";
                    Document s = new Document();


                    string body = talb;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sssssssssssss.Address, fromPassword)
                    };
                    using (var message = new MailMessage(sssssssssssss, dddddddd)
                    {
                        Subject = subject,
                        Body = body



                    })
                    {
                        smtp.Send(message);
                    }
                    db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'F' WHERE REQUEST_CODE ='" + lablRequeHR38.Content.ToString() + "'", "تم حفظ البيانات و تم ارسال البريد بنجاح");
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('2','" + lablRequeHR38.Content.ToString() + "','N','" + lablRequeHR39.Content.ToString() + "')");
                    searchaddempreq();

                }


            }
            else
                MessageBox.Show("برجاء كتابة سبب الرفض");
        }

        private void reqdeletehr_Copy2_Click(object sender, RoutedEventArgs e)
        {
            if (reprentresun.Text != "")
            {

                System.Data.DataTable mail = db.RunReader("select mail from agent where name='" + lablRequeHR11.Content.ToString() + "'").Result;

                if (mail.Rows[0][0].ToString() == "")
                {

                    db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'F' WHERE REQUEST_CODE ='" + lablRequeHR70.Content.ToString() + "'", "تم حفظ البيانات مع مراعاة انه لم يتم ارسال بريد الكترونى");
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('2','" + lablRequeHR70.Content.ToString() + "','N','" + lablRequeHR11.Content.ToString() + "')");
                    printreasonreque();
                }
                else
                {

                    var sssssssssssss = new MailAddress("zajjof@gmail.com", "DMS");
                    var dddddddd = new MailAddress(mail.Rows[0][0].ToString(), "From DMS To Name");
                    const string fromPassword = "Abd0.G@m@1";
                    string subject = " تم رفض اعادة طباعة ";
                    string talb = @"كود الطلب  " + "\n"
                                 + lablRequeHR70.Content.ToString() + "\n"
                                 + "رقم الكارت: " + "\n" +
                                 lablRequeHR8.Content.ToString() + "\n" +
                                   "اسم الموظف : " + "\n" +
                                 lablRequeHR1_Copy1.Content.ToString() + "\n" +
                                    "سبب التغير : " + "\n" +
                                 lablRequeHR9.Content.ToString() + "\n" +

                                    "تاريخ الطلب : " + "\n" +
                                 lablRequeHR12.Content.ToString() + "\n" +
                                     "سبب الرفض : " + "\n" +
                                 reprentresun.Text.ToString() + "\n";
                    Document s = new Document();


                    string body = talb;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sssssssssssss.Address, fromPassword)
                    };
                    using (var message = new MailMessage(sssssssssssss, dddddddd)
                    {
                        Subject = subject,
                        Body = body



                    })
                    {
                        smtp.Send(message);
                    }
                    db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'F' WHERE REQUEST_CODE ='" + lablRequeHR70.Content.ToString() + "'", "تم حفظ البيانات و تم ارسال البريد بنجاح");
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('2','" + lablRequeHR70.Content.ToString() + "','N','" + lablRequeHR11.Content.ToString() + "')");
                    printreasonreque();

                }


            }
            else
                MessageBox.Show("برجاء كتابة سبب الرفض");
        }

        private void reqdeletqehr_Copy2_Click(object sender, RoutedEventArgs e)
        {
            if (txt_sbb_elrafd.Text != "")
            {

                System.Data.DataTable mail = db.RunReader("select mail from agent where name='" + label_request_about.Content.ToString() + "'").Result;

                if (mail.Rows[0][0].ToString() == "")
                {

                    db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'F' WHERE REQUEST_CODE ='" + label_request.Content.ToString() + "'", "تم حفظ البيانات مع مراعاة انه لم يتم ارسال بريد الكترونى");
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('2','" + label_request.Content.ToString() + "','N','" + label_request_about.Content.ToString() + "')");
                    searchshangeempname();
                }
                else
                {

                    var sssssssssssss = new MailAddress("zajjof@gmail.com", "DMS");
                    var dddddddd = new MailAddress(mail.Rows[0][0].ToString(), "From DMS To Name");
                    const string fromPassword = "Abd0.G@m@1";
                    string subject = " تم رفض تغير اسم موظف ";
                    string talb = @"كود الطلب  " + "\n"
                                 + label_request.Content.ToString() + "\n"
                                 + "رقم الكارت: " + "\n" +
                                 label_nom_cod.Content.ToString() + "\n" +
                                   "اسم الموظف : " + "\n" +
                                 label_name_emp.Content.ToString() + "\n" +
                                    "اسم الموظف الجديد : " + "\n" +
                                 label_name_emp_a.Content.ToString() + "\n" +
                                    "تاريخ الطلب : " + "\n" +
                                 label_dat_request.Content.ToString() + "\n" +
                                     "سبب الرفض : " + "\n" +
                                 txt_sbb_elrafd.Text.ToString() + "\n";
                    Document s = new Document();


                    string body = talb;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sssssssssssss.Address, fromPassword)
                    };
                    using (var message = new MailMessage(sssssssssssss, dddddddd)
                    {
                        Subject = subject,
                        Body = body



                    })
                    {
                        smtp.Send(message);
                    }
                    db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'F' WHERE REQUEST_CODE ='" + label_request.Content.ToString() + "'", "تم حفظ البيانات و تم ارسال البريد بنجاح");
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('2','" + label_request.Content.ToString() + "','N','" + label_request_about.Content.ToString() + "')");

                    searchshangeempname();

                }


            }
            else
                MessageBox.Show("برجاء كتابة سبب الرفض");
        }

        private void reqdeletehr_Copy4_Click(object sender, RoutedEventArgs e)
        {
            if (txt_sbb_elrafd1.Text != "")
            {

                System.Data.DataTable mail = db.RunReader("select mail from agent where name='" + labl_Created_By.Content.ToString() + "'").Result;

                if (mail.Rows[0][0].ToString() == "")
                {

                    db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'F' WHERE REQUEST_CODE ='" + labl_Request_code.Content.ToString() + "'", "تم حفظ البيانات مع مراعاة انه لم يتم ارسال بريد الكترونى");
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('2','" + labl_Request_code.Content.ToString() + "','N','" + labl_Created_By.Content.ToString() + "')");

                    sarsh_chang();
                }
                else
                {

                    var sssssssssssss = new MailAddress("zajjof@gmail.com", "DMS");
                    var dddddddd = new MailAddress(mail.Rows[0][0].ToString(), "From DMS To Name");
                    const string fromPassword = "Abd0.G@m@1";
                    string subject = " تم رفض تغير فئة موظف ";
                    string talb = @"كود الطلب  " + "\n"
                                 + labl_Request_code.Content.ToString() + "\n"
                                 + "رقم الكارت: " + "\n" +
                                 labl_Card_Id.Content.ToString() + "\n" +
                                   "اسم الموظف : " + "\n" +
                                 labl_name.Content.ToString() + "\n" +
                                    "رقم الفئة : " + "\n" +
                                 labl_Employee_Class.Content.ToString() + "\n" +
                                    "تاريخ الطلب : " + "\n" +
                                 labl_Created_Data.Content.ToString() + "\n" +
                                     "سبب الرفض : " + "\n" +
                                 txt_sbb_elrafd1.Text.ToString() + "\n";
                    Document s = new Document();


                    string body = talb;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sssssssssssss.Address, fromPassword)
                    };
                    using (var message = new MailMessage(sssssssssssss, dddddddd)
                    {
                        Subject = subject,
                        Body = body



                    })
                    {
                        smtp.Send(message);
                    }
                    db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'F' WHERE REQUEST_CODE ='" + labl_Request_code.Content.ToString() + "'", "تم حفظ البيانات و تم ارسال البريد بنجاح");
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('2','" + labl_Request_code.Content.ToString() + "','N','" + labl_Created_By.Content.ToString() + "')");

                    sarsh_chang();

                }


            }
            else
                MessageBox.Show("برجاء كتابة سبب الرفض");
        }

        private void reqdeletehr_Copy6_Click(object sender, RoutedEventArgs e)
        {
            if (txt_sbb_elrafd2.Text != "")
            {

                System.Data.DataTable mail = db.RunReader("select mail from agent where name='" + labl_Created_By1.Content.ToString() + "'").Result;

                if (mail.Rows[0][0].ToString() == "")
                {

                    db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'F' WHERE REQUEST_CODE ='" + labl_Request_code1.Content.ToString() + "'", "تم حفظ البيانات مع مراعاة انه لم يتم ارسال بريد الكترونى");
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('2','" + labl_Request_code1.Content.ToString() + "','N','" + labl_Created_By1.Content.ToString() + "')");

                    sarsh_chang2();
                }
                else
                {

                    var sssssssssssss = new MailAddress("zajjof@gmail.com", "DMS");
                    var dddddddd = new MailAddress(mail.Rows[0][0].ToString(), "From DMS To Name");
                    const string fromPassword = "Abd0.G@m@1";
                    string subject = " تم رفض اعادة تفعيل موظف ";
                    string talb = @"كود الطلب  " + "\n"
                                 + labl_Request_code1.Content.ToString() + "\n"
                                 + "رقم الكارت: " + "\n" +
                                 labl_Card_Id1.Content.ToString() + "\n" +
                                   "اسم الموظف : " + "\n" +
                                 labl_name1.Content.ToString() + "\n" +
                                    "تاريخ اعادة التفعيل : " + "\n" +
                                 labl_dat_retarn.Content.ToString() + "\n" +
                                    "تاريخ الطلب : " + "\n" +
                                 labl_Created_Data1.Content.ToString() + "\n" +
                                     "سبب الرفض : " + "\n" +
                                 txt_sbb_elrafd2.Text.ToString() + "\n";
                    Document s = new Document();


                    string body = talb;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sssssssssssss.Address, fromPassword)
                    };
                    using (var message = new MailMessage(sssssssssssss, dddddddd)
                    {
                        Subject = subject,
                        Body = body



                    })
                    {
                        smtp.Send(message);
                    }
                    db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'F' WHERE REQUEST_CODE ='" + labl_Request_code1.Content.ToString() + "'", "تم حفظ البيانات و تم ارسال البريد بنجاح");
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('2','" + labl_Request_code1.Content.ToString() + "','N','" + labl_Created_By1.Content.ToString() + "')");

                    sarsh_chang2();

                }


            }
            else
                MessageBox.Show("برجاء كتابة سبب الرفض");
        }

        private void reqdeletehr_Copy8_Click(object sender, RoutedEventArgs e)
        {
            if (txt_sbb_elrafd3.Text != "")
            {

                System.Data.DataTable mail = db.RunReader("select mail from agent where name='" + labl_Created_By2.Content.ToString() + "'").Result;

                if (mail.Rows[0][0].ToString() == "")
                {

                    db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'F' WHERE REQUEST_CODE ='" + labl_Request_code2.Content.ToString() + "'", "تم حفظ البيانات مع مراعاة انه لم يتم ارسال بريد الكترونى");
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('2','" + labl_Request_code2.Content.ToString() + "','N','" + labl_Created_By2.Content.ToString() + "')");

                    sarsh_chang2();
                }
                else
                {

                    var sssssssssssss = new MailAddress("zajjof@gmail.com", "DMS");
                    var dddddddd = new MailAddress(mail.Rows[0][0].ToString(), "From DMS To Name");
                    const string fromPassword = "Abd0.G@m@1";
                    string subject = " تم رفض تغير رقم موظف ";
                    string talb = @"كود الطلب  " + "\n"
                                 + labl_Request_code2.Content.ToString() + "\n"
                                 + "رقم الكارت: " + "\n" +
                                 labl_Card_Id2.Content.ToString() + "\n" +
                                   "اسم الموظف : " + "\n" +
                                 labl_name2.Content.ToString() + "\n" +
                                    "رقم الموطف الجديد : " + "\n" +
                                 labl_Card_Id_new.Content.ToString() + "\n" +
                                    "تاريخ الطلب : " + "\n" +
                                 labl_Created_Data2.Content.ToString() + "\n" +
                                     "سبب الرفض : " + "\n" +
                                 txt_sbb_elrafd3.Text.ToString() + "\n";
                    Document s = new Document();


                    string body = talb;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(sssssssssssss.Address, fromPassword)
                    };
                    using (var message = new MailMessage(sssssssssssss, dddddddd)
                    {
                        Subject = subject,
                        Body = body



                    })
                    {
                        smtp.Send(message);
                    }
                    db.RunNonQuery("UPDATE EMPLOYEE_REQUEST SET APPROVE_FLAG = 'F' WHERE REQUEST_CODE ='" + labl_Request_code2.Content.ToString() + "'", "تم حفظ البيانات و تم ارسال البريد بنجاح");
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('2','" + labl_Request_code2.Content.ToString() + "','N','" + labl_Created_By2.Content.ToString() + "')");

                    sarsh_chang2();

                }


            }
            else
                MessageBox.Show("برجاء كتابة سبب الرفض");
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            dis.Stop();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            dis.Start();
        }


        ////////////////////////////////////////////////////////////

        public string simg = "", stmp = "";
        Proces2 dbtm = new Proces2();

        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            //string sa = "";

            OpenFileDialog sfd = new OpenFileDialog();
            sfd.Filter = "Image file|*.jpeg ; *.bmp; *.jpg";
            if (sfd.ShowDialog() == true)
            {
                simg = sfd.FileName;
                img.Source = new BitmapImage(new Uri(simg));
            }
        }

        private void UploadStamp_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.Filter = "Image file|*.jpeg ; *.bmp; *.jpg";
            if (sfd.ShowDialog() == true)
            {
                stmp = sfd.FileName;
                Stamp.Source = new BitmapImage(new Uri(stmp));
            }
        }

        private void SaveImg_Click(object sender, RoutedEventArgs e)
        {
            if (simg != string.Empty)
            {
                FileStream fs;
                fs = new FileStream(simg, FileMode.Open, FileAccess.Read);

                byte[] bimg = new byte[fs.Length];
                fs.Read(bimg, 0, System.Convert.ToInt32(fs.Length));
                dbtm.IorUimg(Convert.ToInt32(ProviderNumber.Text), bimg, 1);
                MessageBox.Show("Done");
                SaveImgStmp.IsEnabled = false;
                SaveImg.IsEnabled = false;
                SaveStamp.IsEnabled = false;
                img.Source = null;
                Stamp.Source = null;
            }
            else
                MessageBox.Show("Plese Choose Picture before Press");
        }

        private void SaveStamp_Click(object sender, RoutedEventArgs e)
        {
            if (stmp != string.Empty)
            {
                FileStream fs;
                fs = new FileStream(stmp, FileMode.Open, FileAccess.Read);

                byte[] bimg = new byte[fs.Length];
                fs.Read(bimg, 0, System.Convert.ToInt32(fs.Length));
                dbtm.IorUimg(Convert.ToInt32(ProviderNumber.Text), bimg, 2);
                MessageBox.Show("Done");
                SaveImgStmp.IsEnabled = false;
                SaveImg.IsEnabled = false;
                SaveStamp.IsEnabled = false;
                img.Source = null;
                Stamp.Source = null;
            }
            else
                MessageBox.Show("Plese Choose Picture before Press");
        }

        private void ProviderNumber_LostFocus(object sender, RoutedEventArgs e)
        {



        }




        private void ProviderNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // ProviderNumber_LostFocus(sender, e);          
                UpdateImg.IsEnabled = false;
                UpdateStamp.IsEnabled = false;
                UpdateImgstmp.IsEnabled = false;
                DeleteImg.IsEnabled = false;
                DeleteStamp.IsEnabled = false;
                DeleteProvider.IsEnabled = false;
                SaveImgStmp.IsEnabled = false;
                SaveImg.IsEnabled = false;
                SaveStamp.IsEnabled = false;
                img.Source = null;
                Stamp.Source = null;
                if (ProviderNumber.Text != string.Empty)
                {
                    System.Data.DataTable dt = dbtm.testprovider(Convert.ToInt32(ProviderNumber.Text), 1);
                    System.Data.DataTable dt2 = dbtm.testprovider(Convert.ToInt32(ProviderNumber.Text), 2);
                    if (dt.Rows.Count != 0 && dt2.Rows.Count == 0)
                    {
                        SaveImgStmp.IsEnabled = true;
                        SaveImg.IsEnabled = true;
                        SaveStamp.IsEnabled = true;
                        UpdateImg.IsEnabled = false;
                        UpdateStamp.IsEnabled = false;
                        UpdateImgstmp.IsEnabled = false;
                        img.Source = null;
                        Stamp.Source = null;
                    }
                    else if (dt.Rows.Count != 0 && dt2.Rows.Count != 0)
                    {
                        UpdateImg.IsEnabled = true;
                        UpdateStamp.IsEnabled = true;
                        UpdateImgstmp.IsEnabled = true;
                        DeleteImg.IsEnabled = true;
                        DeleteStamp.IsEnabled = true;
                        DeleteProvider.IsEnabled = true;
                        SaveImgStmp.IsEnabled = false;
                        SaveImg.IsEnabled = false;
                        SaveStamp.IsEnabled = false;
                        if (!(dt2.Rows[0][1]).Equals(DBNull.Value))
                        {
                            byte[] bimg = (byte[])dt2.Rows[0][1];
                            img.Source = BitmapImageFromBytes(bimg);
                        }
                        if (!(dt2.Rows[0][2]).Equals(DBNull.Value))
                        {
                            byte[] bimg = (byte[])dt2.Rows[0][2];
                            Stamp.Source = BitmapImageFromBytes(bimg);
                        }
                    }
                    else
                        MessageBox.Show("This Number don't have any providers");
                }
                else
                    MessageBox.Show("Please Enter Number of Provider");

            }

        }

        private void UpdateImg_Click(object sender, RoutedEventArgs e)
        {
            if (simg != string.Empty)
            {
                FileStream fs;
                fs = new FileStream(simg, FileMode.Open, FileAccess.Read);

                byte[] bimg = new byte[fs.Length];
                fs.Read(bimg, 0, System.Convert.ToInt32(fs.Length));
                dbtm.IorUimg(Convert.ToInt32(ProviderNumber.Text), bimg, 3);
                MessageBox.Show("Done");
                UpdateImg.IsEnabled = false;
                UpdateStamp.IsEnabled = false;
                UpdateImgstmp.IsEnabled = false;
                DeleteImg.IsEnabled = false;
                DeleteStamp.IsEnabled = false;
                DeleteProvider.IsEnabled = false;
                img.Source = null;
                Stamp.Source = null;
            }
            else
                MessageBox.Show("Plese Choose Picture before Press");
        }

        private void UpdateStamp_Click(object sender, RoutedEventArgs e)
        {
            if (stmp != string.Empty)
            {
                FileStream fs;
                fs = new FileStream(stmp, FileMode.Open, FileAccess.Read);

                byte[] bimg = new byte[fs.Length];
                fs.Read(bimg, 0, System.Convert.ToInt32(fs.Length));
                dbtm.IorUimg(Convert.ToInt32(ProviderNumber.Text), bimg, 4);
                MessageBox.Show("Done");
            }
            else
                MessageBox.Show("Plese Choose Picture before Press");
        }

        private void DeleteImg_Click(object sender, RoutedEventArgs e)
        {
            dbtm.Delimg(Convert.ToInt32(ProviderNumber.Text), 1);
            MessageBox.Show("Done");
            UpdateImg.IsEnabled = false;
            UpdateStamp.IsEnabled = false;
            UpdateImgstmp.IsEnabled = false;
            DeleteImg.IsEnabled = false;
            DeleteStamp.IsEnabled = false;
            DeleteProvider.IsEnabled = false;
            img.Source = null;
            Stamp.Source = null;
        }

        private void DeleteStamp_Click(object sender, RoutedEventArgs e)
        {
            dbtm.Delimg(Convert.ToInt32(ProviderNumber.Text), 2);
            MessageBox.Show("Done");
            UpdateImg.IsEnabled = false;
            UpdateStamp.IsEnabled = false;
            UpdateImgstmp.IsEnabled = false;
            DeleteImg.IsEnabled = false;
            DeleteStamp.IsEnabled = false;
            DeleteProvider.IsEnabled = false;
            img.Source = null;
            Stamp.Source = null;
        }

        private void DeleteProvider_Click(object sender, RoutedEventArgs e)
        {
            dbtm.Delimg(Convert.ToInt32(ProviderNumber.Text), 3);
            MessageBox.Show("Done");
            UpdateImg.IsEnabled = false;
            UpdateStamp.IsEnabled = false;
            UpdateImgstmp.IsEnabled = false;
            DeleteImg.IsEnabled = false;
            DeleteStamp.IsEnabled = false;
            DeleteProvider.IsEnabled = false;
            img.Source = null;
            Stamp.Source = null;
        }

        private void SaveImgStmp_Click(object sender, RoutedEventArgs e)
        {
            if (simg != string.Empty && stmp != string.Empty)
            {
                FileStream fs, fs2;
                fs = new FileStream(simg, FileMode.Open, FileAccess.Read);
                fs2 = new FileStream(stmp, FileMode.Open, FileAccess.Read);

                byte[] bimg = new byte[fs.Length];
                byte[] bimg2 = new byte[fs2.Length];

                fs.Read(bimg, 0, System.Convert.ToInt32(fs.Length));
                fs2.Read(bimg2, 0, System.Convert.ToInt32(fs2.Length));

                dbtm.IorUimgandstmp(Convert.ToInt32(ProviderNumber.Text), bimg, bimg2, 1);
                MessageBox.Show("Done");
                SaveImgStmp.IsEnabled = false;
                SaveImg.IsEnabled = false;
                SaveStamp.IsEnabled = false;
                img.Source = null;
                Stamp.Source = null;
            }
            else
                MessageBox.Show("Plese Choose Picture before Press");
        }

        private void mnadeb_click(object sender, MouseButtonEventArgs e)
        {
            if (MessReqz.IsSelected == true && messReqCityComboz.ItemsSource == null)
            {

                if (User.Type == "hr")
                {

                    //List<MessengerRequestData> Companylist = req.SelectAllCompanies();
                    //messReqCompanyListz.ItemsSource = Companylist;
                    //messReqCompanyListz.DisplayMemberPath = "CompanyName";
                    //messReqCompanyListz.SelectedValuePath = "CompanyCode";
                    messReqDeptxtz.Text = agent.get_dept(NameTab.Header.ToString());
                    #region Selectmax_MessRequestId
                    string req_max2 = req.SelectMaxReqMessId();
                    int req_maxx2 = 0;
                    if (req_max2 == "")
                    {
                        messReqCodetxtz.Text = "1";
                    }

                    else
                    {
                        req_maxx2 = int.Parse(req_max2) + 1;
                        messReqCodetxtz.Text = req_max2.ToString();
                    }
                    #endregion
                    if(messReqareaComboz_Copy.Text==string.Empty)
                    messReqareaComboz_Copy.ItemsSource = User.ALL_Company().DefaultView;
                    List<MessengerRequestData> listReq = req.SelectAllMessengersRequests();
                    messReqGridz.ItemsSource = listReq;
                    messReqitemCountz.Content = listReq.Count.ToString();
                    //dataGridView1.Columns["HoldDate"].DisplayIndex = 12;
                    // -------clear selection mode at first time when load ---------------
                    try
                    {
                        messReqGridz.SelectedCells.Clear();
                        messReqCodetxtz.Text = req_maxx2.ToString();
                    }
                    catch
                    {
                        // txtReqCode.Text = "1";
                    }
                    //-----------To Clear TextBoxes-------------------//
                    #region clearAll
                    messReqComptxtz.Text = "";
                    messReqContactPersontxtz.Text = "";
                    messReqothertxtz.Document.Blocks.Clear();
                    chkReadyCardsResonz.IsChecked = false;
                    chkReadyCheekz.IsChecked = false;
                    chkDeliverPaperz.IsChecked = false;
                    chkOtherResonsz.IsChecked = false;
                    #endregion

                    //List<MessengerRequestData> listCity = req.SelectAllGovernerators();
                    //messReqCityComboz.ItemsSource = listCity;
                    //messReqCityComboz.DisplayMemberPath = "Governorate_Name";
                    //messReqCityComboz.SelectedValuePath = "Governorate_Code";

                    //messReqCityComboz.ItemsSource=

                    governComboNetwork.Items.Clear();
                    System.Data.DataTable governTable = client.get_curr_city();
                    for (int i = 0; i < governTable.Rows.Count; i++)
                    {
                        messReqCityComboz.Items.Add(governTable.Rows[i].ItemArray[0].ToString());
                    }


                    messReqareaComboz_Copy.Text = User.CompanyID;
                    messReqareaComboz_Copy.IsEnabled = false;
                    imgsearch6_Copy.IsEnabled = false;

                    int compId = Convert.ToInt32(messReqareaComboz_Copy.Text);
                    string compName = store.get_indem_company_name(compId);
                    lblCompNamez.Content = compName;
                    // messReqbranchCombo.Items.Clear();
                    messReqaddrtxtz.Text = store.GetCompanyAddress(compId);
                    List<MessengerRequestData> Branches = req.SelectAllCompanies_Branches(compId.ToString());
                    //if (Branches == null)
                    //{
                    //    MessageBox.Show("لا توجد فروع ");
                    //}
                    messReqbranchComboz.ItemsSource = Branches;
                    messReqbranchComboz.DisplayMemberPath = "Branch";
                    messReqbranchComboz.SelectedValuePath = "Branch";

                }
                else
                {
                    try
                    {
                        List<MessengerRequestData> Companylist = req.SelectAllCompanies();
                        messReqCompanyListz.ItemsSource = Companylist;
                        messReqCompanyListz.DisplayMemberPath = "CompanyName";
                        messReqCompanyListz.SelectedValuePath = "CompanyCode";
                        messReqDeptxtz.Text = agent.get_dept(NameTab.Header.ToString());
                        #region Selectmax_MessRequestId
                        string req_max2 = req.SelectMaxReqMessId();
                        int req_maxx2 = 0;
                        if (req_max2 == "")
                        {
                            messReqCodetxtz.Text = "1";
                        }

                        else
                        {
                            req_maxx2 = int.Parse(req_max2) + 1;
                            messReqCodetxtz.Text = req_max2.ToString();
                        }
                        #endregion
                        if(messReqareaComboz_Copy.Text==string.Empty)
                        messReqareaComboz_Copy.ItemsSource = User.ALL_Company().DefaultView;
                        List<MessengerRequestData> listReq = req.SelectAllMessengersRequests();
                        messReqGridz.ItemsSource = listReq;
                        messReqitemCountz.Content = listReq.Count.ToString();
                        //dataGridView1.Columns["HoldDate"].DisplayIndex = 12;
                        // -------clear selection mode at first time when load ---------------
                        try
                        {
                            messReqGridz.SelectedCells.Clear();
                            messReqCodetxtz.Text = req_maxx2.ToString();
                        }
                        catch
                        {
                            // txtReqCode.Text = "1";
                        }
                        //-----------To Clear TextBoxes-------------------//
                        #region clearAll
                        messReqComptxtz.Text = "";
                        messReqContactPersontxtz.Text = "";
                        messReqothertxtz.Document.Blocks.Clear();
                        chkReadyCardsResonz.IsChecked = false;
                        chkReadyCheekz.IsChecked = false;
                        chkDeliverPaperz.IsChecked = false;
                        chkOtherResonsz.IsChecked = false;
                        #endregion
                        try
                        {
                            //dataGridView1.Columns[3].Visible = false;
                            //dataGridView1.Columns[4].Visible = false;
                            //dataGridView1.Columns["Done"].Visible = false;
                        }
                        catch { }
                        List<MessengerRequestData> listCity = req.SelectAllGovernerators();
                        messReqCityComboz.ItemsSource = listCity;
                        messReqCityComboz.DisplayMemberPath = "Governorate_Name";
                        messReqCityComboz.SelectedValuePath = "Governorate_Code";
                    }
                    catch { }
                }
            }
            else if (messConfTabz.IsSelected == true)
            {
                MessengerConfirmation confirm = new MessengerConfirmation();
                confirm.ShowDialog();
            }
        }

        private void txtfromclaimn_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                lablclaimtotalnum.Content = (Convert.ToDouble(txttoclaimn.Text) - Convert.ToDouble(txtfromclaimn.Text)).ToString();
            }
            catch { }
        }

        private void txttoclaimn_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                lablclaimtotalnum.Content = (Convert.ToDouble(txttoclaimn.Text) - Convert.ToDouble(txtfromclaimn.Text)).ToString();
            }
            catch { }
        }



        private void indemnitynewSrchBtnz_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (User.Type == "hr")
            {

                indemnity_id = Convert.ToInt32(IndemnityCardComboz.Text);
                string dateFrom = dtpFromz.Text.ToString();
                string dateTo = dtpToz.Text.ToString();
                IndemnityGridz.Visibility = Visibility.Visible;
                List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCompanyCodeSearch(dateFrom, dateTo, indemnity_id);
                if (Indemnities == null || Indemnities.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات");
                }
                else
                {

                    IndemnityGridz.ItemsSource = Indemnities;
                    indemnityItmCounttxtz.Content = Indemnities.Count;
                }

            }
            else
            {
                indemnity_id = Convert.ToInt32(IndemnityCompanyComboz.Text.ToString());
                fill_card(IndemnityCardComboz, indemnity_id);
                string dateFrom = dtpFromz.Text.ToString();
                string dateTo = dtpToz.Text.ToString();
                IndemnityGridz.Visibility = Visibility.Visible;
                List<IndemnityData> Indemnities = ind.SelectAllIndemtiesForCompanyCodeSearch(dateFrom, dateTo, indemnity_id);
                if (Indemnities == null || Indemnities.Count == 0)
                {
                    MessageBox.Show("لا توجد بيانات");
                }
                else
                {

                    IndemnityGridz.ItemsSource = Indemnities;
                    indemnityItmCounttxtz.Content = Indemnities.Count;
                }
            }

        }

        private void approvalcardcombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                string CardNo = approvalcardcombo.Text.ToString();
                int comp_emp = 0;
                int card_approve = 0;
                if (UserType == "hr")
                {
                    string[] arr = CardNo.Split('-');
                    string comp = arr[0].ToString();
                    if (User.CompanyID == CompanyCode)
                    {
                        comp_emp = client.validate_card_num(CardNo);
                        card_approve = client.validate_card_approval(CardNo);
                        if (comp_emp >= 1)
                        {
                            if (card_approve >= 1)
                            {
                                string value_PatiantName = ApprovaltxtCardNumz.Text;
                                List<EmpApprovalData> Branches = emp.SelectAllApprovals(CardNo, value_PatiantName);
                                approvalGridz.ItemsSource = Branches;
                                totalApprovalCountz.Content = client.count_approve(CardNo).ToString();
                                approvalGridz.Columns[6].Visibility = Visibility.Hidden;
                                approvalGridz.Columns[7].Visibility = Visibility.Hidden;
                                approvalGridz.Columns[8].Visibility = Visibility.Hidden;
                                approvalGridz.Columns[9].Visibility = Visibility.Hidden;
                                //approvalGridz.Columns[11].Visibility = Visibility.Hidden;
                                approvalItemCounttxtz.Content = Branches.Count.ToString();
                            }
                            else
                            {
                                MessageBox.Show("لا توجد موافقة لهذا الكارت");
                                ApprovaltxtCardNumz.Text = "";
                                approvalItemCounttxtz.Content = approvalGridz.Items.Count - 1;
                                totalApprovalCountz.Content = "0";
                            }
                        }
                        else
                        {
                            MessageBox.Show("رقم كارت غير موجود");
                            ApprovaltxtCardNumz.Text = "";
                            approvalItemCounttxtz.Content = approvalGridz.Items.Count - 1;
                            totalApprovalCountz.Content = "0";
                        }
                    }
                    else
                    {
                        MessageBox.Show("غير مسموح ببيانات هذه الشركة");
                    }

                }
                else
                {
                    comp_emp = client.validate_card_num(CardNo);
                    card_approve = client.validate_card_approval(CardNo);
                    if (comp_emp >= 1)
                    {
                        if (card_approve >= 1)
                        {
                            string value_PatiantName = ApprovaltxtCardNumz.Text;
                            List<EmpApprovalData> Branches = emp.SelectAllApprovals(CardNo, value_PatiantName);
                            approvalGridz.ItemsSource = Branches;
                            totalApprovalCountz.Content = client.count_approve(CardNo).ToString();
                            approvalGridz.Columns[6].Visibility = Visibility.Hidden;
                            approvalGridz.Columns[7].Visibility = Visibility.Hidden;
                            approvalGridz.Columns[8].Visibility = Visibility.Hidden;
                            approvalGridz.Columns[9].Visibility = Visibility.Hidden;
                            approvalGridz.Columns[11].Visibility = Visibility.Hidden;
                            approvalItemCounttxtz.Content = Branches.Count.ToString();
                        }
                        else
                        {
                            MessageBox.Show("لا توجد موافقة لهذا الكارت");
                            ApprovaltxtCardNumz.Text = "";
                            approvalItemCounttxtz.Content = approvalGridz.Items.Count - 1;
                            totalApprovalCountz.Content = "0";
                        }
                    }
                    else
                    {
                        MessageBox.Show("رقم كارت غير موجود");
                        ApprovaltxtCardNumz.Text = "";
                        approvalItemCounttxt.Content = approvalGridz.Items.Count - 1;
                        totalApprovalCountz.Content = "0";
                    }
                }
            }
        }

        private void UpdateImgstmp_Click(object sender, RoutedEventArgs e)
        {
            if (simg != string.Empty && stmp != string.Empty)
            {
                FileStream fs, fs2;
                fs = new FileStream(simg, FileMode.Open, FileAccess.Read);
                fs2 = new FileStream(stmp, FileMode.Open, FileAccess.Read);

                byte[] bimg = new byte[fs.Length];
                byte[] bimg2 = new byte[fs2.Length];

                fs.Read(bimg, 0, System.Convert.ToInt32(fs.Length));
                fs2.Read(bimg2, 0, System.Convert.ToInt32(fs2.Length));

                dbtm.IorUimgandstmp(Convert.ToInt32(ProviderNumber.Text), bimg, bimg2, 2);
                MessageBox.Show("Done");
                UpdateImg.IsEnabled = false;
                UpdateStamp.IsEnabled = false;
                UpdateImgstmp.IsEnabled = false;
                DeleteImg.IsEnabled = false;
                DeleteStamp.IsEnabled = false;
                DeleteProvider.IsEnabled = false;
                img.Source = null;
                Stamp.Source = null;
            }
            else
                MessageBox.Show("Plese Choose Picture before Press");
        }
        /// ////////////////////////////////////

        void fillcardname(ComboBox c)
        {
            try
            {
                //  MessageBox.Show(companyIDqw);
                dataset_emp_card = db.RunReaderds(" select distinct   CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE C_COMP_ID=" + companyIDqw + " ORDER BY CARD_ID ");
                c.ItemsSource = dataset_emp_card.Tables[0].DefaultView;
            }
            catch { }
        }

        private void fillcardname(ComboBox c, string card)
        {
            try
            {
                //  MessageBox.Show(companyIDqw);
                dataset_emp_card = db.RunReaderds(@"select distinct CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH 
                                                 from COMP_EMPLOYEESS WHERE (card_id like '%" + card + "%' or emp_aname_st like '%" + card + "%' or emp_aname_sc like '%" + card + "%' or emp_aname_th like '%" + card + "%') and C_COMP_ID=" + companyIDqw + " ORDER BY CARD_ID ");
                c.ItemsSource = dataset_emp_card.Tables[0].DefaultView;
            }
            catch { }
        }
        private void btnSaveqw_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                if (txtproviderphoneqw_Copy.Text != "")
                {


                    DateTime datet = DateTime.Now;
                    string comDatenow = datet.ToString("dd-MMM-yy");

                    int n = int.Parse(datet.ToString("ddMMyyyy"));
                    string s = numqw + n.ToString();
                    probLCodeqw.Content = s;





                    db.RunNonQuery(@"INSERT INTO HR_PROVIDERS_REQUEST ( AREA_NAME, STATE_NAME, PROVIDER_TYPE, NEW_PROV_NAME, NEW_PROV_ADD,  CONTACT_PERSON,CONTACT_PHONE , REPLAYED , CREATED_BY,CREATED_DATE,POPULATION_NUM)
VALUES   ('" + cbxareaqw.Text + "','" + cbxstateqw.Text + "','" + cbxprovidertypqw.Text + "','" + txtprovidernameqw.Text + "','" + txtprovidernameqw1.Text + "','" + txtresponsqw.Text + "','" + txtproviderphoneqw.Text + "','N','" + username + "','" + comDatenow + "','" + txtproviderphoneqw_Copy.Text + "')");

                    System.Data.DataTable zz = db.RunReader(" select CODE from HR_PROVIDERS_REQUEST WHERE AREA_NAME = '" + cbxareaqw.Text + "' AND STATE_NAME ='" + cbxstateqw.Text + "' AND PROVIDER_TYPE = '" + cbxprovidertypqw.Text + "' AND NEW_PROV_NAME ='" + txtprovidernameqw.Text + "' AND CONTACT_PHONE ='" + txtproviderphoneqw.Text + "'").Result;

                    AutoNumeqw();
                    txtdmsreplayqw.Text = "تم ارسال الطلب بنجاح " + "\n" + " و جارى المتابعة";
                    txtSearchqw.Text = zz.Rows[0][0].ToString();
                    probLCodeqw.Content = zz.Rows[0][0].ToString();
                    MessageBox.Show("تم الحفظ بنجاح رقم العملية  " + zz.Rows[0][0].ToString());
                }
                else
                {
                    MessageBox.Show("برجاء كاتبة عدد السكان ");
                }
            }
            catch { }
        }

        private void btnNewqw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtproviderphoneqw_Copy.Text = "";
                probLCodeqw.Content = "**********";
                cbxareaqw.Text = "";
                cbxstateqw.Text = "";
                cbxprovidertypqw.Text = "";
                txtprovidernameqw.Text = "";
                txtprovidernameqw1.Text = "";
                txtresponsqw.Text = "";
                txtproviderphoneqw.Text = "";
                txtdmsreplayqw.Text = "";
                txtSearchqw.Text = "";
                btnSaveqw.IsEnabled = true;
                btnEditeqw.IsEnabled = false;
                AutoNumeqw();
            }
            catch { }
        }

        private void btnEditeqw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //   DateTime datet = probdpTime.SelectedDate.Value.Date;
                //   string comDate = datet.ToString("dd-MMM-yy");
                //    datet = DateTime.Now;
                //   string comDatenow = datet.ToString("dd-MMM-yy");
                //  checkfun();
                db.RunNonQuery(@"UPDATE HR_PROVIDERS_REQUEST SET AREA_NAME ='" + cbxareaqw.Text + "', STATE_NAME = '" + cbxstateqw.Text + "', PROVIDER_TYPE = '" + cbxprovidertypqw.Text + "', NEW_PROV_NAME = '" + txtprovidernameqw.Text + "', NEW_PROV_ADD = '" + txtprovidernameqw1.Text + "', CONTACT_PERSON = '" + txtresponsqw.Text + "', CONTACT_PHONE = '" + txtproviderphoneqw.Text + "' ,POPULATION_NUM = '" + txtproviderphoneqw_Copy.Text + "' where CODE =" + txtSearchqw.Text, "تم التعديل بنجاح");


                btnSaveqw.IsEnabled = true;
                btnEditeqw.IsEnabled = false;
            }
            catch { }


        }




        private void cbxcardnoqw_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataTable dd;
                //   select distinct   SPECIFIC_DATE ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH from COMP_EMPLOYEESS WHERE C_COMP_ID = " + companyIDqw + " ORDER BY CARD_ID
                if (User.Type != "DMS Member")
                {
                    dd = db.RunReader("  select distinct   SPECIFIC_DATE ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH from COMP_EMPLOYEESS WHERE CARD_ID = '" + cbxcardnoqw.Text + "' and CARD_ID like '" + User.CompanyID + "%'").Result;
                }
                else
                    dd = db.RunReader("  select distinct   SPECIFIC_DATE ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH from COMP_EMPLOYEESS WHERE CARD_ID = '" + cbxcardnoqw.Text + "' and CARD_ID like '" + cbxclassEmpCompCombo.Text + "%'").Result;

                //MessageBox.Show(dataset_emp_card.Tables[0].Rows[cbxcardnoqw.SelectedIndex][0].ToString());

                string name = dd.Rows[0][1].ToString() + " " + dd.Rows[0][2].ToString() + " " + dd.Rows[0][3].ToString();
                // string name = dd.Rows[0][1].ToString()+ dd.Rows[0][2].ToString()+ dd.Rows[0][3].ToString();
                txtempnameqw.Content = name;
                txtspecilc_date.Content = dd.Rows[0][0].ToString();
                txtcardnumqw.Text = cbxcardnoqw.Text;
                txtcardnumqw1.Text = cbxcardnoqw.Text;
                txtcardnumqw2.Text = cbxcardnoqw.Text;
            }
            catch
            {
                MessageBox.Show("تأكد من رقم الكارت ");
            }
        }

        private void cbxreasonqw_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (cbxreasonqw.SelectedIndex == 0)
                {
                    addpic_Copy.Visibility = Visibility.Hidden;
                    btnsaveagainqw.Visibility = Visibility.Visible;
                    lbl1.Visibility = Visibility.Visible;
                    lbl2.Visibility = Visibility.Visible;
                }
                if (cbxreasonqw.SelectedIndex == 1)
                {
                    addpic_Copy.Visibility = Visibility.Visible;
                    btnsaveagainqw.Visibility = Visibility.Hidden;
                    lbl1.Visibility = Visibility.Hidden;
                    lbl2.Visibility = Visibility.Hidden;
                }
            }
            catch { }
        }

        private void btnuploudqw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                //    path = Path.GetFlowDirection(op);
                if (op.ShowDialog() == true)
                {
                    imgPhoto22.Source = new BitmapImage(new Uri(op.FileName));
                    path = op.FileName;


                    System.Drawing.Bitmap newimg = new System.Drawing.Bitmap(path);

                }




                btnsaveagainqw.IsEnabled = true;
            }
            catch { }
        }

        private void btnsaveagainqw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbxreasonqw.Text == "")

                    MessageBox.Show("برجاء اختيار السبب");

                else
                {
                    db.RunNonQuery(@"INSERT INTO EMPLOYEE_REQUEST (CARD_ID, CREATED_BY, CREATED_DATE, PRINT_REASON, REGISTER_TYPE, TYPE) VALUES   " +
                        "('" + cbxcardnoqw.Text + "','" + username + "','" + DateTime.Now.ToString("dd-MMM-yy") + "','" + cbxreasonqw.Text + "','p','4')");

                    System.Data.DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where CARD_ID ='" + cbxcardnoqw.Text + "' and CREATED_BY ='" + username + "'" +
                        " and CREATED_DATE ='" + DateTime.Now.ToString("dd-MMM-yy") + "' and PRINT_REASON ='" + cbxreasonqw.Text + "' and REGISTER_TYPE ='p' and  TYPE='4'").Result;
                    lbl2.Content = temp.Rows[0][0].ToString();
                    MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString());
                    db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('1','" + temp.Rows[0][0].ToString() + "','N','" + User.Name + "')");
                }


            }
            catch { }
        }

        private void reporbtTab_Corgfcn(object sender, MouseButtonEventArgs e)
        {


            if (est3lammotalbaa.IsSelected == true)
            {
                est3laam a = new est3laam();
                a.ShowDialog();
            }
            else if (clientsReport_Copy3.IsSelected == true)
            {
                hrreportmol5sesthlaak a = new hrreportmol5sesthlaak();
                a.ShowDialog();
            }
        }

        private void searchtxt_Copy2_MouseEnter(object sender, MouseEventArgs e)
        {
            searchtxt_Copy2.Text = "";
        }

        OracleConnection conn;
        private void btnsaveagainqw1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileStream fls;
                fls = new FileStream(@path, FileMode.Open, FileAccess.Read);
                //a byte array to read the image 
                byte[] blob = new byte[fls.Length];
                fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                fls.Close();


                string connstr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=********** )(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
                conn = new OracleConnection(connstr);
                conn.Open();
                OracleCommand cmnd;
                string query;
                //   query = "insert into emp(id,name,photo) values(" + txtid.Text + "," + "'" + txtname.Text + "'," + " :BlobParameter )";
                query = @"INSERT INTO EMPLOYEE_REQUEST (CARD_ID, CREATED_BY, CREATED_DATE, PRINT_REASON, REGISTER_TYPE, TYPE ,PRINT_IMG) VALUES   " +
                    "('" + cbxcardnoqw.Text + "','" + username + "','" + DateTime.Now.ToString("dd-MMM-yy") + "','" + cbxreasonqw.Text + "','p','4' ," + " :BlobParameter)";
                //insert the byte as oracle parameter of type blob 
                OracleParameter blobParameter = new OracleParameter();
                blobParameter.OracleType = OracleType.Blob;
                blobParameter.ParameterName = "BlobParameter";
                blobParameter.Value = blob;
                cmnd = new OracleCommand(query, conn);
                cmnd.Parameters.Add(blobParameter);
                cmnd.ExecuteNonQuery();

                conn.Close();


                System.Data.DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where CARD_ID ='" + cbxcardnoqw.Text + "' and CREATED_BY ='" + username + "'" +
             " and CREATED_DATE ='" + DateTime.Now.ToString("dd-MMM-yy") + "' and REGISTER_TYPE ='p' and  TYPE='4'").Result;
                lbl4.Content = temp.Rows[0][0].ToString();

                db.RunNonQuery("update EMPLOYEE_REQUEST set PRINT_REASON ='" + cbxreasonqw.Text + "' where REQUEST_CODE ='" + temp.Rows[0][0].ToString() + "'");
                MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString());
                db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('1','" + temp.Rows[0][0].ToString() + "','N','" + User.Name + "')");



            }
            catch { }
        }

        private void btnsavereopenqw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.RunNonQuery(@"INSERT INTO EMPLOYEE_REQUEST (CARD_ID, CREATED_BY, CREATED_DATE, REOPEN_DATE, REGISTER_TYPE, TYPE ) VALUES   " +
           "('" + cbxcardnoqw.Text + "','" + username + "','" + DateTime.Now.ToString("dd-MMM-yy") + "','" + txtcardnumqw_Copy.Text + "','p','5')");

                System.Data.DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where CARD_ID ='" + cbxcardnoqw.Text + "' and CREATED_BY ='" + username + "'" +
             " and CREATED_DATE ='" + DateTime.Now.ToString("dd-MMM-yy") + "' and REOPEN_DATE='" + txtcardnumqw_Copy.Text + "' and REGISTER_TYPE ='p' and  TYPE='5'").Result;
                lbl7.Content = temp.Rows[0][0].ToString();
                MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString());
                db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('1','" + temp.Rows[0][0].ToString() + "','N','" + User.Name + "')");

            }
            catch { }

        }


        private void searchtxt_Copy2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchnewre();
            }
        }
        void searchnewre()
        {

            System.Data.DataTable ss = db.RunReader(@"select EMPLOYEE_REQUEST.REQUEST_CODE ,EMPLOYEE_REQUEST.CARD_ID,EMPLOYEE_REQUEST_TYPE.TYPE_NAME ,EMPLOYEE_REQUEST.CREATED_DATE ,EMPLOYEE_REQUEST.APPROVE_FLAG from EMPLOYEE_REQUEST,EMPLOYEE_REQUEST_TYPE where EMPLOYEE_REQUEST.TYPE=EMPLOYEE_REQUEST_TYPE.TYPE_ID and CREATED_BY='" + User.Name + "' and ( EMPLOYEE_REQUEST.REQUEST_CODE like '%" + searchtxt_Copy2.Text + "%' or EMPLOYEE_REQUEST.CARD_ID like '%" + searchtxt_Copy2.Text + "%' )  ").Result;


            ss.Columns.Add("status", typeof(String));
            ss.Columns["REQUEST_CODE"].ColumnName = "كود الطلب";
            ss.Columns["CARD_ID"].ColumnName = "رقم الكارت";
            ss.Columns["TYPE_NAME"].ColumnName = "نوع الطلب";
            ss.Columns["CREATED_DATE"].ColumnName = "تاريخ الطلب";
            foreach (DataRow rowz in ss.Rows)
            {
                if (rowz[4].ToString() == "n" || rowz[4].ToString() == "N")
                    rowz["status"] = "Pending";
                else if ((rowz[4].ToString() == "w" || rowz[4].ToString() == "W"))
                    rowz["status"] = "Under Processing";
                else if ((rowz[4].ToString() == "T" || rowz[4].ToString() == "t"))
                    rowz["status"] = "Approved";
                else if ((rowz[4].ToString() == "F" || rowz[4].ToString() == "f"))
                    rowz["status"] = "Rejected";


            }
            ss.Columns.RemoveAt(4);
            InfoGrid.ItemsSource = ss.DefaultView;
        }

        void clearqw()
        {
            txtcardnumqw_Copy.Text = " ";
            txtcardnumqw.Text = "";
            cbxcardnoqw.Text = "";
            txtempnameqw.Content = "";
            txtspecilc_date.Content = "";
            cbxchooseqw.Text = "";
            reopenqw.Visibility = Visibility.Hidden;
            txtcardnumqw1.Text = "";
            txtcardnumnewqw.Text = "";
            newcardqw.Visibility = Visibility.Hidden;
            cbxreasonqw.Text = "";
            imgPhoto22.Source = null;
            addpic_Copy.Visibility = Visibility.Hidden;
            addpic.Visibility = Visibility.Hidden;
            lbl7.Content = "********";
            lbl2.Content = "********";
            lbl4.Content = "********";
            lbl9.Content = "********";
            lbl88.Content = "********";
            txtcardnumqw2.Text = "";
            txtcardnumqw2_Copy.Text = "";
            txtcardnumqw2_Copy1.Text = "";
            txtcardnumqw2_Copy3.Text = "";
            txtcardnumqw2_Copy4.Text = "";
            txtcardnumqw2_Copy8.Text = "";
            txtcardnumqw2_Copy2.Text = "";
            txtcardnumqw2_Copy5.Text = "";
            txtcardnumqw2_Copy6.Text = "";
            txtcardnumqw2_Copy7.Text = "";
            txtcardnumqw2_Copy9.Text = "";
            reopenqw_Copy.Visibility = Visibility.Hidden;


        }

        private void btnclearqw_Click(object sender, RoutedEventArgs e)
        {
            clearqw();

        }

        private void btnsavenewcardqw_Click(object sender, RoutedEventArgs e)
        {
            string newcardid = txtcardnumnewqw.Text;
            string card = txtcardnumnewqw_Copy.Text + '-' + txtcardnumnewqw_Copy2.Text + '-' + newcardid + '-' + txtcardnaumnewqw_Copy2.Text;
            int num = client.validate_card_num(card);
            if (num >= 1)
            {
                MessageBox.Show("رقم كارت موجود بالفعل ، ادخل رقم جديد ");
            }
            else
            {
                db.RunNonQuery(@"INSERT INTO EMPLOYEE_REQUEST (CARD_ID, CREATED_BY, CREATED_DATE,
                            REGISTER_TYPE, TYPE ,NEW_CARD_ID ) VALUES   " + "('" + cbxcardnoqw.Text +
                           "','" + username + "','" + DateTime.Now.ToString("dd-MMM-yy") + "','p','6'," + newcardid + ")");


                System.Data.DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where CARD_ID ='" + cbxcardnoqw.Text + "' and CREATED_BY ='" + username + "'" +
        " and CREATED_DATE ='" + DateTime.Now.ToString("dd-MMM-yy") + "'  and REGISTER_TYPE ='p' and  TYPE='6' and NEW_CARD_ID='" + newcardid + "'").Result;
                lbl9.Content = temp.Rows[0][0].ToString();
                MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString());
                db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('1','" + temp.Rows[0][0].ToString() + "','N','" + User.Name + "')");

            }
        }

        private void txtSearchqw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                try
                {

                    System.Data.DataTable s = db.RunReader(" select * from HR_PROVIDERS_REQUEST WHERE CODE = '" + txtSearchqw.Text + "'").Result;
                    if (s.Rows.Count > 0)
                    {

                        btnSaveqw.IsEnabled = false;
                        btnEditeqw.IsEnabled = true;
                        probLCodeqw.Content = s.Rows[0][0].ToString();

                        cbxareaqw.Text = s.Rows[0][1].ToString();
                        cbxstateqw.Text = s.Rows[0][2].ToString();
                        cbxprovidertypqw.Text = s.Rows[0][3].ToString();
                        txtprovidernameqw.Text = s.Rows[0][4].ToString();
                        txtprovidernameqw1.Text = s.Rows[0][5].ToString();
                        txtresponsqw.Text = s.Rows[0][6].ToString();

                        txtproviderphoneqw.Text = s.Rows[0][7].ToString();
                        txtproviderphoneqw_Copy.Text = s.Rows[0][13].ToString();

                        if (s.Rows[0][9].ToString() == "")
                            txtdmsreplayqw.Text = "لم يتم الرد";
                        else
                            txtdmsreplayqw.Text = s.Rows[0][9].ToString();

                    }
                    else
                    {
                        MessageBox.Show("تحقق من الرقم"); return;

                    }

                    // s.Clear();
                }
                catch { }
            }

        }

        private void cbxstateqw_DropDownClosed(object sender, EventArgs e)
        {

            areacodeqw = cbxstateqw.Text;
            fillareaqw();
        }

        private void imgsearchCust_Copy22_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //  MessageBox.Show(companyIDqw);
                dataset_emp_card = db.RunReaderds(" select distinct   CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE C_COMP_ID=" + companyIDqw + " and ( CARD_ID LIKE '%" + cbxcardnoqw.Text + "%' OR EMP_ANAME_ST LIKE '%" + cbxcardnoqw.Text + "%'  OR EMP_ANAME_SC LIKE '%" + cbxcardnoqw.Text + "%'  OR EMP_ANAME_TH LIKE '%" + cbxcardnoqw.Text + "%' ) ORDER BY CARD_ID ");
                cbxcardnoqw.ItemsSource = dataset_emp_card.Tables[0].DefaultView;

                cbxcardnoqw.IsDropDownOpen = true;
            }
            catch { }
        }

        private void ComboBoxItem_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            addpic_Copy.Visibility = Visibility.Hidden;
        }



        private void cbxchooseqw_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbxchooseqw.SelectedIndex == 0)
                {
                    addpic.Visibility = Visibility.Visible;
                    reopenqw.Visibility = Visibility.Hidden;
                    newcardqw.Visibility = Visibility.Hidden;
                    reopenqw_Copy.Visibility = Visibility.Hidden;
                }
                if (cbxchooseqw.SelectedIndex == 1)
                {
                    addpic.Visibility = Visibility.Hidden;
                    reopenqw.Visibility = Visibility.Visible;
                    newcardqw.Visibility = Visibility.Hidden;
                    reopenqw_Copy.Visibility = Visibility.Hidden;

                }
                if (cbxchooseqw.SelectedIndex == 2)
                {
                    addpic.Visibility = Visibility.Hidden;
                    reopenqw.Visibility = Visibility.Hidden;
                    newcardqw.Visibility = Visibility.Visible;
                    reopenqw_Copy.Visibility = Visibility.Hidden;
                }
                if (cbxchooseqw.SelectedIndex == 3)
                {
                    addpic.Visibility = Visibility.Hidden;
                    reopenqw.Visibility = Visibility.Hidden;
                    newcardqw.Visibility = Visibility.Hidden;
                    reopenqw_Copy.Visibility = Visibility.Visible;
                }


            }
            catch { }
        }


        private void qwbtnSearch_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                System.Data.DataTable s = db.RunReader(" select * from HR_PROVIDERS_REQUEST WHERE CODE = '" + txtSearchqw.Text + "'").Result;
                if (s.Rows.Count > 0)
                {

                    btnSaveqw.IsEnabled = false;
                    btnEditeqw.IsEnabled = true;
                    probLCodeqw.Content = s.Rows[0][0].ToString();

                    cbxareaqw.Text = s.Rows[0][1].ToString();
                    cbxstateqw.Text = s.Rows[0][2].ToString();
                    cbxprovidertypqw.Text = s.Rows[0][3].ToString();
                    txtprovidernameqw.Text = s.Rows[0][4].ToString();
                    txtprovidernameqw1.Text = s.Rows[0][5].ToString();
                    txtresponsqw.Text = s.Rows[0][6].ToString();

                    txtproviderphoneqw.Text = s.Rows[0][7].ToString();
                    txtproviderphoneqw_Copy.Text = s.Rows[0][12].ToString();

                    if (s.Rows[0][8].ToString() == "N")
                        txtdmsreplayqw.Text = "لم يتم الرد";
                    else
                        txtdmsreplayqw.Text = s.Rows[0][9].ToString();

                }
                else
                {
                    MessageBox.Show("تحقق من الرقم"); return;

                }

                // s.Clear();
            }
            catch { }

        }

        #endregion



        private void anamesttxt_KeyUp(object sender, KeyEventArgs e)
        {
            anametxt.Text = anamesttxt.Text + " " + anamesctxt.Text + " " + anamethtxt.Text + " " + anamefrtxt.Text;
        }

        private void anamesctxt_KeyUp(object sender, KeyEventArgs e)
        {
            anametxt.Text = anamesttxt.Text + " " + anamesctxt.Text + " " + anamethtxt.Text + " " + anamefrtxt.Text;

        }

        private void anamethtxt_KeyUp(object sender, KeyEventArgs e)
        {
            anametxt.Text = anamesttxt.Text + " " + anamesctxt.Text + " " + anamethtxt.Text + " " + anamefrtxt.Text;

        }

        private void anamefrtxt_KeyUp(object sender, KeyEventArgs e)
        {
            anametxt.Text = anamesttxt.Text + " " + anamesctxt.Text + " " + anamethtxt.Text + " " + anamefrtxt.Text;

        }

        private void enamesttxt_KeyUp(object sender, KeyEventArgs e)
        {
            enametxt.Text = enamesttxt.Text + " " + enamesctxt.Text + " " + enamethtxt.Text + " " + enamefrtxt.Text;

        }

        private void cbxcardnoqw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    System.Data.DataTable dd;
                    //   select distinct   SPECIFIC_DATE ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH from COMP_EMPLOYEESS WHERE C_COMP_ID = " + companyIDqw + " ORDER BY CARD_ID
                    if (User.Type != "DMS Member")
                    {
                        dd = db.RunReader("  select distinct   SPECIFIC_DATE ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH from COMP_EMPLOYEESS WHERE CARD_ID = '" + cbxcardnoqw.Text + "' and CARD_ID like '" + User.CompanyID + "%'").Result;
                    }
                    else
                        dd = db.RunReader("  select distinct   SPECIFIC_DATE ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH from COMP_EMPLOYEESS WHERE CARD_ID = '" + cbxcardnoqw.Text + "' and CARD_ID like '" + cbxclassEmpCompCombo.Text + "%'").Result;

                    //MessageBox.Show(dataset_emp_card.Tables[0].Rows[cbxcardnoqw.SelectedIndex][0].ToString());

                    string name = dd.Rows[0][1].ToString() + " " + dd.Rows[0][2].ToString() + " " + dd.Rows[0][3].ToString();
                    // string name = dd.Rows[0][1].ToString()+ dd.Rows[0][2].ToString()+ dd.Rows[0][3].ToString();
                    txtempnameqw.Content = name;
                    txtspecilc_date.Content = dd.Rows[0][0].ToString();
                    txtcardnumqw.Text = cbxcardnoqw.Text;
                    txtcardnumqw1.Text = cbxcardnoqw.Text;
                    txtcardnumqw2.Text = cbxcardnoqw.Text;
                }
                catch
                {
                    MessageBox.Show("تأكد من رقم الكارت ");
                }
            }
        }
        void searchemp()
        {
            btnediteemp.IsEnabled = true;
            saveEmpBtn22.IsEnabled = false;

            System.Data.DataTable data = db.RunReader(@"select emp_ename_st,emp_ename_sc,emp_ename_th,emp_ename_fr,
                        emp_ename,emp_aname_st,emp_aname_sc,emp_aname_th,emp_aname_fr,emp_aname,
                        national_id,birthdate,mobile,email,start_date,address,branch,
                        card_id ,PRINT_IMG,emp_relation,gender,GLASSES,DISEASE from EMPLOYEE_REQUEST where REQUEST_CODE = '" + searchtxt.Text + "' and type=1 ").Result;
            if (data.Rows.Count >= 1)
            {

                lblf.Content = searchtxt.Text;
                enametxt.Text = data.Rows[0][4].ToString();
                enamesttxt.Text = data.Rows[0][0].ToString();
                enamethtxt.Text = data.Rows[0][2].ToString();
                enamefrtxt.Text = data.Rows[0][3].ToString();
                enamesctxt.Text = data.Rows[0][1].ToString();

                anametxt.Text = data.Rows[0][9].ToString();
                anamesttxt.Text = data.Rows[0][5].ToString();
                anamesctxt.Text = data.Rows[0][6].ToString();
                anamethtxt.Text = data.Rows[0][7].ToString();
                anamefrtxt.Text = data.Rows[0][8].ToString();
                nationalidtxt.Text = data.Rows[0][10].ToString();
                birthdatetxt.Text = data.Rows[0][11].ToString();
                empnumtxt.Text = data.Rows[0][17].ToString();
                startdatetxt.Text = data.Rows[0][14].ToString();
                addrtxt.Text = data.Rows[0][15].ToString();
                emailtxt.Text = data.Rows[0][13].ToString();
                branchCombo.Text = data.Rows[0][16].ToString();
                mobnumtxt.Text = data.Rows[0][12].ToString();
                string gla = data.Rows[0][21].ToString();
                string mrad = data.Rows[0][22].ToString();
                string gender = data.Rows[0][20].ToString();
                if (gender == "1")
                {
                    malerb.IsChecked = true;
                }
                else if (gender == "2")
                {
                    femalerb.IsChecked = true;
                }

                string relation = data.Rows[0][19].ToString();
                if (relation == "self")
                {
                    emprb.IsChecked = true;

                }
                else if (relation == "Son/Daughter")
                {
                    childrb.IsChecked = true;
                }
                else if (relation == "father/mother")
                {
                    parentrb.IsChecked = true;
                }
                else if (relation == "husband/wife")
                {
                    husbandrb.IsChecked = true;
                }

                if (gla == "Y") ndara.IsChecked = true;
                if (mrad == "Y") mred.IsChecked = true;

                try
                {
                    byte[] blob = (byte[])data.Rows[0][18];

                    empphtoo.Source = BitmapImageFromBytes(blob);
                }
                catch { }
            }
            else
                MessageBox.Show("برجاء التاكد من الكود");
        }

        public static BitmapImage BitmapImageFromBytes(byte[] bytes)
        {
            BitmapImage image = null;
            MemoryStream stream = null;
            try
            {
                stream = new MemoryStream(bytes);
                stream.Seek(0, SeekOrigin.Begin);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                image = new BitmapImage();
                image.BeginInit();
                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                image.StreamSource = ms;
                image.StreamSource.Seek(0, SeekOrigin.Begin);
                image.EndInit();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stream.Close();
                stream.Dispose();
            }
            return image;
        }
        private void searchbtnnew_Click(object sender, RoutedEventArgs e)
        {

            searchemp();

        }

        private void searchtxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                searchemp();

        }

        private void btnediteemp_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery(@"UPDATE EMPLOYEE_REQUEST SET  EMP_ENAME_ST = '" + enamesttxt.Text + "', EMP_ENAME_SC = '" + enamesctxt.Text + "', EMP_ENAME_TH = '" + enamethtxt.Text + "', EMP_ENAME_FR = '" + enamefrtxt.Text + "', EMP_ENAME = '" + enametxt.Text + "', EMP_ANAME_ST = '" + anamesttxt.Text + "', EMP_ANAME_SC = '" + anamesctxt.Text + "', EMP_ANAME_TH = '" + anamethtxt.Text + "', EMP_ANAME_FR = '" + anamefrtxt.Text + "', EMP_ANAME = '" + anametxt.Text + "', NATIONAL_ID = '" + nationalidtxt.Text + "', BIRTHDATE = '" + birthdatetxt.Text + "', MOBILE = '" + mobnumtxt.Text + "', EMAIL ='" + emailtxt.Text + "', START_DATE = '" + startdatetxt.Text + "', ADDRESS = '" + addrtxt.Text + "' WHERE REQUEST_CODE = '" + searchtxt.Text + "' ", "تم التعديل بنجاح");
            if (empphtoo.Source != null)
            {
                FileStream fls;
                fls = new FileStream(@path2, FileMode.Open, FileAccess.Read);
                //a byte array to read the image 
                byte[] blob = new byte[fls.Length];
                fls.Read(blob, 0, System.Convert.ToInt32(fls.Length));
                fls.Close();


                string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=********** )(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
                conn = new OracleConnection(connectionStr);
                conn.Open();
                OracleCommand cmnd;
                string query;
                query = @"UPDATE EMPLOYEE_REQUEST SET  PRINT_IMG=:BlobParameter where REQUEST_CODE ='" + searchtxt.Text + "'";
                //insert the byte as oracle parameter of type blob 
                OracleParameter blobParameter = new OracleParameter();
                blobParameter.OracleType = OracleType.Blob;
                blobParameter.ParameterName = "BlobParameter";
                blobParameter.Value = blob;
                cmnd = new OracleCommand(query, conn);
                cmnd.Parameters.Add(blobParameter);
                cmnd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("تم التعديل بنجاح");
            }
            else
                MessageBox.Show("تم التعديل بنجاح");

        }
        string path2;


        private void btnuploudqwa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                  "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                  "Portable Network Graphic (*.png)|*.png";
                //    path = Path.GetFlowDirection(op);
                if (op.ShowDialog() == true)
                {
                    empphtoo.Source = new BitmapImage(new Uri(op.FileName));
                    path2 = op.FileName;


                    System.Drawing.Bitmap newimg = new System.Drawing.Bitmap(path2);

                }




                btnsaveagainqw.IsEnabled = true;
            }
            catch { }
        }

        private void btnsavereopenqw1_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                db.RunNonQuery(@"INSERT INTO EMPLOYEE_REQUEST (CARD_ID, CREATED_BY, CREATED_DATE,REGISTER_TYPE, TYPE ,emp_ename_st,emp_ename_sc,emp_ename_th,emp_ename_fr,emp_ename,emp_aname_st,emp_aname_sc,emp_aname_th,emp_aname_fr,emp_aname) VALUES   " + "('" + txtcardnumqw2.Text + "','" + User.Name + "','" + DateTime.Now.ToString("dd-MMM-yy") + "','p','7','" + txtcardnumqw2_Copy.Text + "','" + txtcardnumqw2_Copy5.Text + "','" + txtcardnumqw2_Copy6.Text + "','" + txtcardnumqw2_Copy7.Text + "','" + txtcardnumqw2_Copy9.Text + "','" + txtcardnumqw2_Copy.Text + "','" + txtcardnumqw2_Copy1.Text + "','" + txtcardnumqw2_Copy3.Text + "','" + txtcardnumqw2_Copy4.Text + "','" + txtcardnumqw2_Copy8.Text + "')");


                System.Data.DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where CARD_ID ='" + txtcardnumqw2.Text + "' and CREATED_BY ='" + username + "'" +
        "  and REGISTER_TYPE ='p' and  TYPE='7' and emp_ename='" + txtcardnumqw2_Copy9.Text + "'and emp_aname='" + txtcardnumqw2_Copy8.Text + "'").Result;
                lbl88.Content = temp.Rows[0][0].ToString();
                MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString());
                db.RunNonQuery("insert into noti (NOTI_TYP,NOTI_SERV,ACTION,CREATED_BY) values  ('1','" + temp.Rows[0][0].ToString() + "','N','" + User.Name + "')");



            }
            catch (Exception ex)
            { MessageBox.Show(ex.ToString()); }

        }

        private void txtcardnumqw2_Copy3_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtcardnumqw2_Copy_TextChanged(object sender, TextChangedEventArgs e) { txtcardnumqw2_Copy8.Text = txtcardnumqw2_Copy.Text + " " + txtcardnumqw2_Copy1.Text + " " + txtcardnumqw2_Copy3.Text + " " + txtcardnumqw2_Copy4.Text; }

        private void txtcardnumqw2_Copy1_TextChanged(object sender, TextChangedEventArgs e) { txtcardnumqw2_Copy8.Text = txtcardnumqw2_Copy.Text + " " + txtcardnumqw2_Copy1.Text + " " + txtcardnumqw2_Copy3.Text + " " + txtcardnumqw2_Copy4.Text; }

        private void txtcardnumqw2_Copy3_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            txtcardnumqw2_Copy8.Text = txtcardnumqw2_Copy.Text + " " + txtcardnumqw2_Copy1.Text + " " + txtcardnumqw2_Copy3.Text + " " + txtcardnumqw2_Copy4.Text;

        }

        private void birthdatetx_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void txtcardnumqw2_Copy4_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtcardnumqw2_Copy8.Text = txtcardnumqw2_Copy.Text + " " + txtcardnumqw2_Copy1.Text + " " + txtcardnumqw2_Copy3.Text + " " + txtcardnumqw2_Copy4.Text;

        }

        private void txtcardnumqw2_Copy2_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtcardnumqw2_Copy9.Text = txtcardnumqw2_Copy2.Text + " " + txtcardnumqw2_Copy5.Text + " " + txtcardnumqw2_Copy6.Text + " " + txtcardnumqw2_Copy7.Text;

        }

        private void txtcardnumqw2_Copy5_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtcardnumqw2_Copy9.Text = txtcardnumqw2_Copy2.Text + " " + txtcardnumqw2_Copy5.Text + " " + txtcardnumqw2_Copy6.Text + " " + txtcardnumqw2_Copy7.Text;

        }

        private void txtcardnumqw2_Copy6_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtcardnumqw2_Copy9.Text = txtcardnumqw2_Copy2.Text + " " + txtcardnumqw2_Copy5.Text + " " + txtcardnumqw2_Copy6.Text + " " + txtcardnumqw2_Copy7.Text;

        }

        private void txtcardnumqw2_Copy7_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtcardnumqw2_Copy9.Text = txtcardnumqw2_Copy2.Text + " " + txtcardnumqw2_Copy5.Text + " " + txtcardnumqw2_Copy6.Text + " " + txtcardnumqw2_Copy7.Text;

        }

        private void DeleteEmpSrchCardBtn_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            fillcardname(deleteEmpNumtxt, deleteEmpNumtxt.Text.ToString());
            deleteEmpNumtxt.IsDropDownOpen = true;
        }

        private void deliverCardyesrb_Checked(object sender, RoutedEventArgs e)
        {
            deliverCardlbl.Visibility = Visibility.Visible;
            receiveCarddatetxt.Visibility = Visibility.Visible;
        }

        private void deliverCardorb_Checked(object sender, RoutedEventArgs e)
        {
            deliverCardlbl.Visibility = Visibility.Hidden;
            receiveCarddatetxt.Visibility = Visibility.Hidden;
        }

        private void NewEmpCompCombo_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataTable data = hr.get_branch(Convert.ToInt32(NewEmpCompCombo.Text));
                branchCombo.Items.Clear();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    branchCombo.Items.Add(data.Rows[i].ItemArray[0].ToString());
                }
            }
            catch { }

        }

        private void NewEmpCompCombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                System.Data.DataTable data = hr.get_branch(Convert.ToInt32(NewEmpCompCombo.Text));
                branchCombo.Items.Clear();
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    branchCombo.Items.Add(data.Rows[i].ItemArray[0].ToString());
                }
            }
        }

        private void NewEmpCompanySrchBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewEmpCompCombo.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + NewEmpCompCombo.Text + "%' or C_ANAME LIKE '%" + NewEmpCompCombo.Text + "%'  ORDER BY C_COMP_ID ").Result.DefaultView;
            NewEmpCompCombo.IsDropDownOpen = true;
        }

        private void delEmpCompCombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                deleteEmpNumtxt.ItemsSource = User.Employee_in_Company(delEmpCompCombo.Text).DefaultView;

            }
        }

        private void delEmpCompCombo_DropDownClosed(object sender, EventArgs e)
        {
            deleteEmpNumtxt.ItemsSource = User.Employee_in_Company(delEmpCompCombo.Text).DefaultView;

        }

        private void DeleteEmpSrchCardBtn_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            delEmpCompCombo.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + delEmpCompCombo.Text + "%' or C_ANAME LIKE '%" + delEmpCompCombo.Text + "%'  ORDER BY C_COMP_ID ").Result.DefaultView;
            delEmpCompCombo.IsDropDownOpen = true;
        }

        private void eteEmpSrchCardBtn_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            classEmpCompCombo.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + classEmpCompCombo.Text + "%' or C_ANAME LIKE '%" + classEmpCompCombo.Text + "%'  ORDER BY C_COMP_ID ").Result.DefaultView;
            classEmpCompCombo.IsDropDownOpen = true;
        }

        private void classEmpCompCombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int compid = Convert.ToInt32(classEmpCompCombo.Text.ToString());
                fill_card_id(cardnumtxt22);
                System.Data.DataTable classcode = hr.get_class_name(compid);
                classtxt.Items.Clear();
                for (int i = 0; i < classcode.Rows.Count; i++)
                {
                    classtxt.Items.Add(classcode.Rows[i].ItemArray[0].ToString());
                }
            }
        }

        private void classEmpCompCombo_DropDownClosed(object sender, EventArgs e)
        {
            int compid = Convert.ToInt32(classEmpCompCombo.Text.ToString());
            fill_card_id(cardnumtxt22);
            System.Data.DataTable classcode = hr.get_class_name(compid);
            classtxt.Items.Clear();
            for (int i = 0; i < classcode.Rows.Count; i++)
            {
                classtxt.Items.Add(classcode.Rows[i].ItemArray[0].ToString());
            }
        }

        private void eteEmpSrchCardBtn_Copy_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            classEmpCompCombo.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + classEmpCompCombo.Text + "%' or C_ANAME LIKE '%" + classEmpCompCombo.Text + "%'  ORDER BY C_COMP_ID ").Result.DefaultView;
            classEmpCompCombo.IsDropDownOpen = true;
        }

        private void imgsearchCust_Copy221_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            cbxclassEmpCompCombo.ItemsSource = db.RunReader("  select distinct C_COMP_ID , C_ANAME from V_COMPANIES  WHERE  C_COMP_ID  LIKE '%" + cbxclassEmpCompCombo.Text + "%' or C_ANAME LIKE '%" + cbxclassEmpCompCombo.Text + "%'  ORDER BY C_COMP_ID ").Result.DefaultView;
            cbxclassEmpCompCombo.IsDropDownOpen = true;
        }

        private void cbxclassEmpCompCombo_DropDownClosed(object sender, EventArgs e)
        {
            cbxcardnoqw.ItemsSource = User.Employee_in_Company(cbxclassEmpCompCombo.Text).DefaultView;
        }

        private void cbxclassEmpCompCombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cbxcardnoqw.ItemsSource = User.Employee_in_Company(cbxclassEmpCompCombo.Text).DefaultView;
            }
        }

        private void enamesctxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            enametxt.Text = enamesttxt.Text + " " + enamesctxt.Text + " " + enamethtxt.Text + " " + enamefrtxt.Text;
        }

        private void enamethtxt_KeyUp(object sender, KeyEventArgs e)
        {
            enametxt.Text = enamesttxt.Text + " " + enamesctxt.Text + " " + enamethtxt.Text + " " + enamefrtxt.Text;
        }

        private void enamefrtxt_KeyUp(object sender, KeyEventArgs e)
        {
            enametxt.Text = enamesttxt.Text + " " + enamesctxt.Text + " " + enamethtxt.Text + " " + enamefrtxt.Text;
        }

        private void wq_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //    if (reprint.IsSelected == true && cbxcardnoqw.ItemsSource == null)
            //    {
            //        cbxcardnoqw.Items.Clear();
            //        fillcardname();
            //    }
        }

        #endregion
        public void timerjoba(object sender, EventArgs e)
        {

            if (chattab.IsSelected == true && com_rsiver.SelectedIndex != -1 && User.Type == "DMS Member")
            {
                if (chak_out.IsChecked == true)
                {
                    System.Data.DataTable ine = db.RunReader("SELECT MESSAGE, NAME_SENDER  , MESSAGE_DATE  FROM MESSAGE_TEST WHERE ( NAME_SENDER='" + com_rsiver.Text + "'AND NAME_RESIVER = 'customerservices') OR( NAME_SENDER='customerservices' AND NAME_RESIVER = '" + com_rsiver.Text + "') ORDER BY ID ").Result;
                    list_messages.Items.Clear();
                    for (int i = 0; i < ine.Rows.Count; i++)
                    {
                        System.Data.DataRow dr = ine.Rows[i];
                        if (dr[1].ToString() == "customerservices")
                        {
                            string d = ("خدمة العملاء --> " + dr[0].ToString()).ToString();

                            list_messages.Items.Add(d);
                        }
                        else
                        {


                            list_messages.Items.Add(com_rsiver.Text + " --> " + dr[0].ToString());

                        }
                        list_messages.SelectedIndex = list_messages.Items.Count - 1;
                        list_messages.ScrollIntoView(list_messages.SelectedItem);


                    }
                }
                else
                    sarsh();

            }
            else if (chattab.IsSelected == true && User.Type != "DMS Member")
            {
                sarsher();

            }


            else
                myTimer.Stop();
        }







        void sarsh()
        {


            System.Data.DataTable ine = db.RunReader("SELECT MESSAGE, NAME_SENDER  , MESSAGE_DATE  FROM MESSAGE_TEST WHERE ( NAME_SENDER='" + User.Name + "'AND NAME_RESIVER = '" + com_rsiver.Text + "') OR( NAME_SENDER='" + com_rsiver.Text + "'AND NAME_RESIVER = '" + User.Name + "') ORDER BY ID ").Result;


            list_messages.Items.Clear();
            for (int i = 0; i < ine.Rows.Count; i++)
            {
                System.Data.DataRow dr = ine.Rows[i];
                if (dr[1].ToString() == User.Name)
                {
                    string d = ("انت --> " + dr[0].ToString()).ToString();

                    list_messages.Items.Add(d);
                }
                else
                {
                    string e = com_rsiver.Text + " --> " + dr[0].ToString();

                    list_messages.Items.Add(e);

                }
                list_messages.SelectedIndex = list_messages.Items.Count - 1;
                list_messages.ScrollIntoView(list_messages.SelectedItem);


            }
        }

        private void com_rsiver_DropDownClosed(object sender, EventArgs e)
        {
            sarsh();
        }

        private void txt_message_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                insertchat();

            }


        }
        void insertchat()
        {

            if (com_rsiver.SelectedIndex != -1 && User.Type == "DMS Member" && chak_out.IsChecked == false)
            {
                list_messages.Items.Add("انت --> " + txt_message.Text);


                db.RunNonQuery("INSERT INTO MESSAGE_TEST (NAME_SENDER,MESSAGE,NAME_RESIVER,MESSAGE_DATE) VALUES ('" + User.Name + "', '" + txt_message.Text + "','" + com_rsiver.Text + "' ,sysdate)");
                txt_message.Clear();
                list_messages.SelectedIndex = list_messages.Items.Count - 1;
                list_messages.ScrollIntoView(list_messages.SelectedItem);
            }
            else if (User.Type == "hr")
            {
                list_messages.Items.Add("انت --> " + txt_message.Text);

                db.RunNonQuery("INSERT INTO MESSAGE_TEST (NAME_SENDER,MESSAGE,NAME_RESIVER,MESSAGE_DATE) VALUES ('" + User.Name + "', '" + txt_message.Text + "','customerservices' ,sysdate)");
                txt_message.Clear();
                list_messages.SelectedIndex = list_messages.Items.Count - 1;
                list_messages.ScrollIntoView(list_messages.SelectedItem);
            }
            else if (chak_out.IsChecked == true)
            {
                list_messages.Items.Add("خدمة العملاء --> " + txt_message.Text);

                db.RunNonQuery("INSERT INTO MESSAGE_TEST (NAME_SENDER,MESSAGE,NAME_RESIVER,MESSAGE_DATE) VALUES ('customerservices', '" + txt_message.Text + "','" + com_rsiver.Text + "' ,sysdate)");
                txt_message.Clear();
                list_messages.SelectedIndex = list_messages.Items.Count - 1;
                list_messages.ScrollIntoView(list_messages.SelectedItem);
            }
        }


        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            insertchat();


        }

        private void com_deprt_DropDownClosed(object sender, EventArgs e)
        {
            System.Data.DataTable chatine = new System.Data.DataTable();

            list_messages.Items.Clear();
            DataSet empDT = db.RunReaderds("select NAME from AGENT where AGENT_DEPT='" + com_deprt.Text + "' AND USERTYPE = 'DMS Member' ");
            com_rsiver.ItemsSource = empDT.Tables[0].DefaultView;
            if (User.Department == "customerservices")
            {
                chak_out.Visibility = Visibility.Visible;
            }




        }



        void sarsher()
        {

            list_messages.Items.Clear();


            com_rsiver.Visibility = Visibility.Hidden;
            label_cm.Visibility = Visibility.Hidden;
            chak_out.Visibility = Visibility.Hidden;
            list_chat.Visibility = Visibility.Hidden;
            com_deprt.Visibility = Visibility.Hidden;

            list_messages.Items.Clear();

            System.Data.DataTable ine = db.RunReader("SELECT MESSAGE, NAME_SENDER  , MESSAGE_DATE  FROM MESSAGE_TEST WHERE ( NAME_SENDER='" + User.Name + "'AND NAME_RESIVER = 'customerservices') OR( NAME_SENDER='customerservices' AND NAME_RESIVER = '" + User.Name + "') ORDER BY ID ").Result;

            System.Windows.Controls.TextBlock x = new System.Windows.Controls.TextBlock();
            List<System.Windows.Controls.Label> listmessage = new List<System.Windows.Controls.Label>();
            list_messages.Items.Clear();
            for (int i = 0; i < ine.Rows.Count; i++)
            {
                System.Data.DataRow dr = ine.Rows[i];
                if (dr[1].ToString() == User.Name)
                {
                    string d = ("انت --> " + dr[0].ToString()).ToString();

                    list_messages.Items.Add(d);
                }
                else
                {

                    list_messages.Items.Add("خدمة العملاء" + " --> " + dr[0].ToString());

                }
                list_messages.SelectedIndex = list_messages.Items.Count - 1;
                list_messages.ScrollIntoView(list_messages.SelectedItem);
            }

        }


        private void list_chat_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            sarsher();

        }

        DispatcherTimer myTimer = new DispatcherTimer();

        private void list_messages_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void chak_out_Click(object sender, RoutedEventArgs e)
        {
            if (chak_out.IsChecked == true)
            {
                list_messages.Items.Clear();
                com_deprt.Visibility = Visibility.Hidden;
                label_dep.Visibility = Visibility.Hidden;
                DataSet empDT = db.RunReaderds("select NAME from AGENT where USERTYPE = 'hr' ");
                com_rsiver.ItemsSource = empDT.Tables[0].DefaultView;
            }
            else
            {
                list_messages.Items.Clear();
                com_deprt.Visibility = Visibility.Visible;
                label_dep.Visibility = Visibility.Visible;
            }
        }

        private void com_rsiver_DropDownClosed_1(object sender, EventArgs e)
        {
            if (chak_out.IsChecked == true)
            {
                list_messages.Items.Clear();

                System.Data.DataTable ine = db.RunReader("SELECT MESSAGE, NAME_SENDER  , MESSAGE_DATE  FROM MESSAGE_TEST WHERE ( NAME_SENDER='" + com_rsiver.Text + "'AND NAME_RESIVER = 'customerservices') OR( NAME_SENDER='customerservices' AND NAME_RESIVER = '" + com_rsiver.Text + "') ORDER BY ID ").Result;

                System.Windows.Controls.TextBlock x = new System.Windows.Controls.TextBlock();
                List<System.Windows.Controls.Label> listmessage = new List<System.Windows.Controls.Label>();
                list_messages.Items.Clear();
                for (int i = 0; i < ine.Rows.Count; i++)
                {
                    System.Data.DataRow dr = ine.Rows[i];
                    if (dr[1].ToString() == "customerservices")
                    {


                        list_messages.Items.Add("خدمة العملاء --> " + dr[0].ToString()).ToString();
                    }
                    else
                    {

                        list_messages.Items.Add(dr[1].ToString() + " --> " + dr[0].ToString());

                    }
                    list_messages.SelectedIndex = list_messages.Items.Count - 1;
                    list_messages.ScrollIntoView(list_messages.SelectedItem);
                }

            }
            else
            {
                sarsh();
            }
        }


        #region Request_Joba
        private void txt_sarsh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                searchshangeempname();

        }


        private void com_sarsh_DropDownClosed(object sender, EventArgs e)
        {
            string x = "";


            if (com_sarsh.SelectedIndex == 0)
            {
                x = "";
            }
            else if (com_sarsh.SelectedIndex == 1)
            {
                x = "m";
            }
            else if (com_sarsh.SelectedIndex == 2)
            {
                x = "p";
            }

            data_grad_chang_name.ItemsSource = db.RunReader(@"select REQUEST_CODE,CARD_ID,EMP_ANAME,EMP_ENAME,CREATED_BY,CREATED_DATE,REGISTER_TYPE from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%" + txt_sarsh.Text + "%' OR CARD_ID like '%" + txt_sarsh.Text + "%' OR CREATED_BY like '%" + txt_sarsh.Text + "%' )   AND TYPE='7' AND APPROVE_FLAG = 'n' AND REGISTER_TYPE like '%" + x + "%'").Result.DefaultView;


        }

        private void txt_sarsh_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void com_sarsh_DropDownClosed_1(object sender, EventArgs e)
        {
            searchshangeempname();

        }



        void searchshangeempname()
        {
            string x = "";


            if (com_sarsh.SelectedIndex == 0)
            {
                x = "";
            }
            else if (com_sarsh.SelectedIndex == 1)
            {
                x = "m";
            }
            else if (com_sarsh.SelectedIndex == 2)
            {
                x = "p";
            }


            if (fromshangereq1.Text == "")

                reqdatefrom = Convert.ToDateTime("01-Jan-1990");
            else
                reqdatefrom = Convert.ToDateTime(fromshangereq1.Text);


            if (toshangereq1.Text == "")
            {
                reqdateto = DateTime.Today;
                reqdateto = reqdateto.AddDays(1);
            }
            else
            {
                reqdateto = Convert.ToDateTime(toshangereq1.Text);
                reqdateto = reqdateto.AddDays(1);
            }

            System.Data.DataTable ine;

            if (User.Manegar == "Y" || User.Manegar == "y")
                ine = db.RunReader(@"select REQUEST_CODE,CARD_ID,EMP_ANAME,EMP_ENAME,CREATED_BY,CREATED_DATE,REGISTER_TYPE from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%" + txt_sarsh.Text + "%' OR CARD_ID like '%" + txt_sarsh.Text + "%' OR CREATED_BY like '%" + txt_sarsh.Text + "%' )   AND TYPE='7' AND APPROVE_FLAG = 'W' AND REGISTER_TYPE like '%" + x + "%' and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' ) ").Result;

            else
                ine = db.RunReader(@"select REQUEST_CODE,CARD_ID,EMP_ANAME,EMP_ENAME,CREATED_BY,CREATED_DATE,REGISTER_TYPE from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%" + txt_sarsh.Text + "%' OR CARD_ID like '%" + txt_sarsh.Text + "%' OR CREATED_BY like '%" + txt_sarsh.Text + "%' )   AND TYPE='7' AND APPROVE_FLAG = 'n' AND REGISTER_TYPE like '%" + x + "%' and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' ) ").Result;


            ine.Columns[0].ColumnName = "رقم الطلب";
            ine.Columns[1].ColumnName = "رقم الكارت";
            ine.Columns[2].ColumnName = "اسم الموظف الجديد بالعربي";
            ine.Columns[3].ColumnName = "اسم الموظف بالغلة الانجليزيه";
            ine.Columns[4].ColumnName = "طلب من";
            ine.Columns[5].ColumnName = "تاريخ الطلب";
            // ine.Columns[6].ColumnName = "الطلب عن طريق";
            ine.Columns.Add("عن طريق", typeof(String));

            foreach (DataRow rowz in ine.Rows)
            {
                if (rowz[6].ToString() == "P" || rowz[6].ToString() == "p")
                    rowz["عن طريق"] = "سيستم";
                else
                    rowz["عن طريق"] = "موبايل";


            }
            ine.Columns.RemoveAt(6);


            data_grad_chang_name.ItemsSource = ine.DefaultView;

        }

        private void requesthrsearch1_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            searchshangeempname();
        }

        private void data_grad_chang_name_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)data_grad_chang_name.SelectedItems[0];

                label_request.Content = row[0];
                label_nom_cod.Content = row[1];
                label_name_emp_a.Content = row[2];
                label_name_emp_e.Content = row[3];
                label_request_about.Content = row[4];
                label_dat_request.Content = row[5];
                System.Data.DataTable name = db.RunReader("select distinct   EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE CARD_ID ='" + row[1].ToString() + "'").Result;
                label_name_emp.Content = name.Rows[0][0].ToString() + " " + name.Rows[0][1].ToString() + " " + name.Rows[0][2].ToString();



            }
            catch
            {

            }
        }

        private void reqdeletehr_Copy88_Click(object sender, RoutedEventArgs e)
        {
            label_dat_request.Content = "";
            label_name_emp.Content = "";
            label_name_emp_a.Content = "";
            label_name_emp_e.Content = "";
            label_nom_cod.Content = "";
            label_request.Content = "";
            label_request_about.Content = "";
            txt_sarsh.Text = "";
            txt_sbb_elrafd.Text = "";

            com_sarsh.SelectedIndex = 0;
        }

        private void data_find_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)data_find.SelectedItem;
                labl_Request_code.Content = row[0].ToString();
                labl_Card_Id.Content = row[1].ToString();
                labl_Employee_Class.Content = row[2].ToString();
                labl_Employee_Reason.Content = row[3].ToString();
                labl_Created_By.Content = row[4].ToString();
                labl_Created_Data.Content = row[5].ToString();
                System.Data.DataTable x = db.RunReader(@"select distinct EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE CARD_ID ='" + row[1].ToString() + "'").Result;

                labl_name.Content = x.Rows[0][0].ToString() + " " + x.Rows[0][1] + " " + x.Rows[0][2].ToString();
            }
            catch
            {

            }
        }

        void sarsh_chang()
        {
            string x = "";


            if (com_sarsh1.SelectedIndex == 0)
            {
                x = "";
            }
            else if (com_sarsh1.SelectedIndex == 1)
            {
                x = "m";
            }
            else if (com_sarsh1.SelectedIndex == 2)
            {
                x = "P";
            }




            if (fromshangereq.Text == "")

                reqdatefrom = Convert.ToDateTime("01-Jan-1990");
            else
                reqdatefrom = Convert.ToDateTime(fromshangereq.Text);


            if (toshangereq.Text == "")
            {
                reqdateto = DateTime.Today;
                reqdateto = reqdateto.AddDays(1);
            }
            else
            {
                reqdateto = Convert.ToDateTime(toshangereq.Text);
                reqdateto = reqdateto.AddDays(1);
            }


            System.Data.DataTable ine;
            if (User.Manegar == "Y" || User.Manegar == "y")
                ine = db.RunReader(@"select REQUEST_CODE  , CARD_ID  , EMP_CLASS ,EMP_CLASS_REASON , CREATED_BY , CREATED_DATE ,REGISTER_TYPE    from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%" + txt_sarsh1.Text + "%' OR CARD_ID like '%" + txt_sarsh1.Text + "%' OR CREATED_BY like '%" + txt_sarsh1.Text + "%' )   AND REGISTER_TYPE='P' AND approve_flag='W'AND TYPE='2' AND REGISTER_TYPE like '%" + x + "%' and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' ) ").Result;

            else
                ine = db.RunReader(@"select REQUEST_CODE  , CARD_ID  , EMP_CLASS ,EMP_CLASS_REASON , CREATED_BY , CREATED_DATE ,REGISTER_TYPE    from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%" + txt_sarsh1.Text + "%' OR CARD_ID like '%" + txt_sarsh1.Text + "%' OR CREATED_BY like '%" + txt_sarsh1.Text + "%' )   AND REGISTER_TYPE='P' AND approve_flag='n'AND TYPE='2' AND REGISTER_TYPE like '%" + x + "%' and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' ) ").Result;

            ine.Columns[0].ColumnName = "رقم الطلب";
            ine.Columns[1].ColumnName = "رقم الكارت";
            ine.Columns[2].ColumnName = "رقم الفئة";
            ine.Columns[3].ColumnName = "سبب التغير";
            ine.Columns[4].ColumnName = "طلب من";
            ine.Columns[5].ColumnName = "تاريخ الطلب";
            // ine.Columns[6].ColumnName = "الطلب عن طريق";WHERE REQUEST_CODE like '%" + txt_find.Text + "%' AND REGISTER_TYPE='P' AND approve_flag='n'AND TYPE='2'
            ine.Columns.Add("عن طريق", typeof(String));

            foreach (DataRow rowz in ine.Rows)
            {
                if (rowz[6].ToString() == "P" || rowz[6].ToString() == "p")
                    rowz["عن طريق"] = "سيستم";
                else
                    rowz["عن طريق"] = "موبايل";


            }
            ine.Columns.RemoveAt(6);


            data_find.ItemsSource = ine.DefaultView;

        }

        private void txt_sarsh1_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.Key == Key.Enter)

                sarsh_chang();

        }

        private void reqdeletehr_Copy5_Click(object sender, RoutedEventArgs e)
        {
            labl_Card_Id.Content = "";
            labl_Created_By.Content = "";
            labl_Created_Data.Content = "";
            labl_Employee_Class.Content = "";
            labl_Employee_Reason.Content = "";
            labl_name.Content = "";
            labl_Request_code.Content = "";
            txt_sarsh1.Text = "";
            txt_sbb_elrafd1.Text = "";

            com_sarsh1.SelectedIndex = 0;
        }

        private void com_sarsh_DropDownClosed_2(object sender, EventArgs e)
        {
            searchshangeempname();
        }

        private void com_sarsh1_DropDownClosed(object sender, EventArgs e)
        {
            sarsh_chang();
        }

        private void txt_sarsh1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void requesthrsearch2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            sarsh_chang();
        }

        private void requesthrsearch1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            searchshangeempname();
        }

        private void data_searsh_retrn_emp_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)data_searsh_retrn_emp.SelectedItem;
                labl_Request_code1.Content = row[0].ToString();
                labl_Card_Id1.Content = row[1].ToString();
                labl_dat_retarn.Content = row[2].ToString();

                labl_Created_By1.Content = row[3].ToString();
                labl_Created_Data1.Content = row[4].ToString();
                System.Data.DataTable x = db.RunReader(@"select distinct EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE CARD_ID ='" + row[1].ToString() + "'").Result;

                labl_name1.Content = x.Rows[0][0].ToString() + " " + x.Rows[0][1] + " " + x.Rows[0][2].ToString();
            }
            catch
            {

            }
        }

        private void reqdeletehr_Copy7_Click(object sender, RoutedEventArgs e)
        {
            labl_Request_code1.Content = "";

            labl_Card_Id1.Content = "";
            labl_name1.Content = "";
            labl_dat_retarn.Content = "";
            labl_Created_By1.Content = "";
            labl_Created_Data1.Content = "";
            txt_sarsh2.Text = "";
            txt_sbb_elrafd2.Text = "";

            com_sarsh2.SelectedIndex = 0;
        }

        void sarsh_chang2()
        {
            string x = "";


            if (com_sarsh2.SelectedIndex == 0)
            {
                x = "";
            }
            else if (com_sarsh2.SelectedIndex == 1)
            {
                x = "m";
            }
            else if (com_sarsh2.SelectedIndex == 2)
            {
                x = "p";
            }


            if (fromreopenreq.Text == "")

                reqdatefrom = Convert.ToDateTime("01-Jan-1990");
            else
                reqdatefrom = Convert.ToDateTime(fromreopenreq.Text);


            if (toreopenreq.Text == "")
            {
                reqdateto = DateTime.Today;
                reqdateto = reqdateto.AddDays(1);
            }
            else
            {
                reqdateto = Convert.ToDateTime(toreopenreq.Text);
                reqdateto = reqdateto.AddDays(1);
            }



            System.Data.DataTable ine;

            if (User.Manegar == "Y" || User.Manegar == "y")
                ine = db.RunReader(@"select REQUEST_CODE  , CARD_ID  , REOPEN_DATE , CREATED_BY , CREATED_DATE ,REGISTER_TYPE    from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%" + txt_sarsh2.Text + "%' OR CARD_ID like '%" + txt_sarsh2.Text + "%' OR CREATED_BY like '%" + txt_sarsh2.Text + "%' )    AND approve_flag='W'AND TYPE='5' AND REGISTER_TYPE like '%" + x + "%' and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' ) ").Result;
            else
                ine = db.RunReader(@"select REQUEST_CODE  , CARD_ID  , REOPEN_DATE , CREATED_BY , CREATED_DATE ,REGISTER_TYPE    from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%" + txt_sarsh2.Text + "%' OR CARD_ID like '%" + txt_sarsh2.Text + "%' OR CREATED_BY like '%" + txt_sarsh2.Text + "%' )    AND approve_flag='n'AND TYPE='5' AND REGISTER_TYPE like '%" + x + "%' and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' ) ").Result;
            ine.Columns[0].ColumnName = "رقم الطلب";
            ine.Columns[1].ColumnName = "رقم الكارت";
            ine.Columns[2].ColumnName = "تاريخ اعادة التفعيل";

            ine.Columns[3].ColumnName = "طلب من";
            ine.Columns[4].ColumnName = "تاريخ الطلب";
            // ine.Columns[5].ColumnName = "الطلب عن طريق";WHERE REQUEST_CODE like '%" + txt_find.Text + "%' AND REGISTER_TYPE='P' AND approve_flag='n'AND TYPE='2'
            ine.Columns.Add("عن طريق", typeof(String));

            foreach (DataRow rowz in ine.Rows)
            {
                if (rowz[5].ToString() == "P" || rowz[5].ToString() == "p")
                    rowz["عن طريق"] = "سيستم";
                else
                    rowz["عن طريق"] = "موبايل";


            }
            ine.Columns.RemoveAt(5);


            data_searsh_retrn_emp.ItemsSource = ine.DefaultView;

        }


        private void txt_sarsh2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                sarsh_chang2();

        }

        private void com_sarsh2_DropDownClosed(object sender, EventArgs e)
        {
            sarsh_chang2();
        }

        private void requesthrsearch3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            sarsh_chang2();
        }

        private void data_chang_nom_emp_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)data_chang_nom_emp.SelectedItem;
                labl_Request_code2.Content = row[0].ToString();
                labl_Card_Id2.Content = row[1].ToString();
                labl_Card_Id_new.Content = row[2].ToString();

                labl_Created_By2.Content = row[3].ToString();
                labl_Created_Data2.Content = row[4].ToString();
                System.Data.DataTable x = db.RunReader(@"select distinct EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH  from COMP_EMPLOYEESS WHERE CARD_ID ='" + row[1].ToString() + "'").Result;

                labl_name2.Content = x.Rows[0][0].ToString() + " " + x.Rows[0][1] + " " + x.Rows[0][2].ToString();
            }
            catch
            {

            }
        }

        private void reqdeletehr_Copy9_Click(object sender, RoutedEventArgs e)
        {
            labl_Request_code2.Content = "";

            labl_Card_Id2.Content = "";
            labl_name2.Content = "";
            labl_Card_Id_new.Content = "";
            labl_Created_By2.Content = "";
            labl_Created_Data2.Content = "";
            txt_sarsh3.Text = "";
            txt_sbb_elrafd3.Text = "";

            com_sarsh3.SelectedIndex = 0;
        }

        void sarsh_chang3()
        {
            string x = "";


            if (com_sarsh3.SelectedIndex == 0)
            {
                x = "";
            }
            else if (com_sarsh3.SelectedIndex == 1)
            {
                x = "m";
            }
            else if (com_sarsh3.SelectedIndex == 2)
            {
                x = "P";
            }


            if (fromreqshangenum.Text == "")

                reqdatefrom = Convert.ToDateTime("01-Jan-1990");
            else
                reqdatefrom = Convert.ToDateTime(fromreqshangenum.Text);


            if (toreqshangenum.Text == "")
            {
                reqdateto = DateTime.Today;
                reqdateto = reqdateto.AddDays(1);
            }
            else
            {
                reqdateto = Convert.ToDateTime(toreqshangenum.Text);
                reqdateto = reqdateto.AddDays(1);
            }


            System.Data.DataTable ine;
            if (User.Manegar == "Y" || User.Manegar == "y")
                ine = db.RunReader(@"select REQUEST_CODE  , CARD_ID  , NEW_CARD_ID , CREATED_BY , CREATED_DATE ,REGISTER_TYPE    from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%" + txt_sarsh3.Text + "%' OR CARD_ID like '%" + txt_sarsh3.Text + "%' OR CREATED_BY like '%" + txt_sarsh3.Text + "%' )    AND approve_flag='W'AND TYPE='6' AND REGISTER_TYPE like '%" + x + "%' and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' ) and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' ) ").Result;

            else
                ine = db.RunReader(@"select REQUEST_CODE  , CARD_ID  , NEW_CARD_ID , CREATED_BY , CREATED_DATE ,REGISTER_TYPE    from EMPLOYEE_REQUEST where ( REQUEST_CODE like '%" + txt_sarsh3.Text + "%' OR CARD_ID like '%" + txt_sarsh3.Text + "%' OR CREATED_BY like '%" + txt_sarsh3.Text + "%' )    AND approve_flag='n'AND TYPE='6' AND REGISTER_TYPE like '%" + x + "%' and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' ) and (created_date between '" + reqdatefrom.ToShortDateString() + "' and '" + reqdateto.ToShortDateString() + "' ) ").Result;

            ine.Columns[0].ColumnName = "رقم الطلب";
            ine.Columns[1].ColumnName = "رقم الكارت";
            ine.Columns[2].ColumnName = "رقم الموظف الجديد";

            ine.Columns[3].ColumnName = "طلب من";
            ine.Columns[4].ColumnName = "تاريخ الطلب";
            // ine.Columns[5].ColumnName = "الطلب عن طريق";WHERE REQUEST_CODE like '%" + txt_find.Text + "%' AND REGISTER_TYPE='P' AND approve_flag='n'AND TYPE='2'
            ine.Columns.Add("عن طريق", typeof(String));

            foreach (DataRow rowz in ine.Rows)
            {
                if (rowz[5].ToString() == "P" || rowz[5].ToString() == "p")
                    rowz["عن طريق"] = "سيستم";
                else
                    rowz["عن طريق"] = "موبايل";


            }
            ine.Columns.RemoveAt(5);


            data_chang_nom_emp.ItemsSource = ine.DefaultView;

        }

        private void txt_sarsh3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                sarsh_chang3();
        }

        private void com_sarsh3_DropDownClosed(object sender, EventArgs e)
        {
            sarsh_chang3();
        }

        private void requesthrsearch4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            sarsh_chang3();
        }

        // ////////////////// //////////////////////////////////////////////////////////////////////////////////// joba ///////////////////////////////////////
        void researshtrue()
        {
            dgbyanatmotalbatfardiaa1_Copy.ItemsSource = db.RunReader("select CODE as كود_العملية, IND_TYP as نوع_المطالبة ,COMPNY_ID as الشركة , to_char(ARCHIVE_RECEIPT_DATE,'DD/MM/YYYY') as  تاريخ_استلام_اللأرشيف ,to_char( REVIEW_RECEIPT_DATE ,'DD/MM/YYYY') as  تاريخ_استلام_المراجع  ,RECIPIENT_NAME_FROM_ARCH as اسم_المستلم_من_قسم_الارشيف ,RECIPIENT_NAME_FROM_REVIEW as اسم_المستلم_من_قسم_المراحع ,FROM_CLAIM as من_رقم_المطالبة ,TO_CLAIM as الى_رقم_المطالبة ,to_char( SERVICE_DATE ,'DD/MM/YYYY') as  تاريخ_الخدمة  ,CHECK_NO as رقم_الشيك ,CHECK_VALUE as قيمة_الشيك ,CLAIM_VALUE as قيمة_المطالبة ,DISCOUNT_VALUE as قيمة_الخصومات ,ADDED_VALUE as قيمة_الاضافات ,DISCOUNT_REASON as سبب_الخصومات ,NOTS as ملاحظات_المراجة ,CONSULTATIVE_NAME as اسم_المراجع ,to_char( CONSUL_DATE ,'DD/MM/YYYY') as  تاريخ_المراجعة  ,to_char( ACCOUNTS_RECEIPT_DATE,'DD/MM/YYYY') as  تاريخ_استلام_الحسابات   from IND_DATA WHERE DONE='Y'  AND READY='N' order by code DESC").Result.DefaultView;
        }

        void researshfalse()
        {
            dgbyanatmotalbatfardiaa.ItemsSource = db.RunReader("select CODE as كود_العملية, IND_TYP as نوع_المطالبة ,COMPNY_ID as الشركة , to_char(ARCHIVE_RECEIPT_DATE,'DD/MM/YYYY') as  تاريخ_استلام_اللأرشيف ,to_char( REVIEW_RECEIPT_DATE ,'DD/MM/YYYY') as  تاريخ_استلام_المراجع  ,RECIPIENT_NAME_FROM_ARCH as اسم_المستلم_من_قسم_الارشيف ,RECIPIENT_NAME_FROM_REVIEW as اسم_المستلم_من_قسم_المراحع ,FROM_CLAIM as من_رقم_المطالبة ,TO_CLAIM as الى_رقم_المطالبة ,to_char( SERVICE_DATE ,'DD/MM/YYYY') as  تاريخ_الخدمة  ,CHECK_NO as رقم_الشيك ,CHECK_VALUE as قيمة_الشيك ,CLAIM_VALUE as قيمة_المطالبة ,DISCOUNT_VALUE as قيمة_الخصومات ,ADDED_VALUE as قيمة_الاضافات ,DISCOUNT_REASON as سبب_الخصومات ,NOTS as ملاحظات_المراجة ,CONSULTATIVE_NAME as اسم_المراجع ,to_char( CONSUL_DATE ,'DD/MM/YYYY') as  تاريخ_المراجعة  ,to_char( ACCOUNTS_RECEIPT_DATE,'DD/MM/YYYY') as  تاريخ_استلام_الحسابات   from IND_DATA WHERE DONE='N' order by code DESC").Result.DefaultView;
        }

        private void saveEmpBtn22_Copy_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MMM-yy";
            Thread.CurrentThread.CurrentCulture = ci;

            db.RunNonQuery(@"insert into IND_DATA (IND_TYP ,COMPNY_ID ,ARCHIVE_RECEIPT_DATE ,REVIEW_RECEIPT_DATE ,RECIPIENT_NAME_FROM_ARCH ,RECIPIENT_NAME_FROM_REVIEW ,FROM_CLAIM ,TO_CLAIM ,SERVICE_DATE ,CHECK_NO ,CHECK_VALUE ,CLAIM_VALUE ,DISCOUNT_VALUE ,ADDED_VALUE ,DISCOUNT_REASON ,NOTS ,CONSULTATIVE_NAME ,CONSUL_DATE ,ACCOUNTS_RECEIPT_DATE ,CREATED_BY ,CREATED_DATE ) values
('" + cbxindtyp.Text + "','" + cbxindcompany.Text + "','" + dprecivedatearch.SelectedDate.Value.Date.ToShortDateString() + "','" + dprecivedaterev.SelectedDate.Value.Date.ToShortDateString() + "','" + txtrecivenamearch.Text + "','" + txtrecivenamerev.Text + "','" + txtfromclaimn.Text + "','" + txttoclaimn.Text + "','" + dpservdate.SelectedDate.Value.Date.ToShortDateString() + "','" + txtcheckn.Text + "','" + txtcheckvalue.Text + "','" + txtsheckclaim.Text + "','" + txt5asmvalue.Text + "','" + txt5asmvalue1.Text + "','" + cbxdiscresoun.Text + "','" + txtnotsforrev.Text + "','" + txtrevname.Text + "','" + dprevdate.SelectedDate.Value.Date.ToShortDateString() + "','" + dpaccountdate.SelectedDate.Value.Date.ToShortDateString() + "','" + User.Name + "',sysdate)", "تم الحفظ بنجاح");

            researshfalse();


        }



        private void newEmpBtn_Copy_Click(object sender, RoutedEventArgs e)
        {
            dgbyanatmotalbatfardiaa.IsEnabled = true;
            but_don.IsEnabled = false;
            saveEmpBtn22_Copy6.IsEnabled = true;
            btnediteemp_Copy.IsEnabled = false;
            but_don.IsEnabled = true;
            cbxindtyp.SelectedIndex = -1;
            cbxindcompany.Text = "";
            dprecivedatearch.Text = "";
            dprecivedaterev.Text = "";
            txtrecivenamearch.Text = "";
            txtrecivenamerev.Text = "";
            txtfromclaimn.Text = "";
            txttoclaimn.Text = "";
            dpservdate.Text = "";
            txtcheckn.Text = "";
            txtcheckvalue.Text = "";
            txtsheckclaim.Text = "";
            txt5asmvalue.Text = "";
            txt5asmvalue1.Text = "";
            cbxdiscresoun.Text = "";
            txtrevname.Text = "";
            txtrevname.Text = "";
            dpaccountdate.Text = "";
            dprevdate.Text = "";
            lablclaimtotalnum.Content = "";
            searchtxt_Copy.Text = "";
            txtnotsforrev.Text = "";
            txtindcode.Text = "######";
            researshfalse();

        }

        private void dgbyanatmotalbatfardiaa_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                but_don.IsEnabled = true;
                saveEmpBtn22_Copy6.IsEnabled = false;
                btnediteemp_Copy.IsEnabled = true;
                but_don.IsEnabled = true;
                DataRowView row = (DataRowView)dgbyanatmotalbatfardiaa.SelectedItem;
                txtindcode.Text = row[0].ToString();
                cbxindtyp.Text = row[1].ToString();
                cbxindcompany.Text = row[2].ToString();
                dprecivedatearch.Text = row[3].ToString();
                dprecivedaterev.Text = row[4].ToString();
                txtrecivenamearch.Text = row[5].ToString();
                txtrecivenamerev.Text = row[5].ToString();
                txtrecivenamerev.Text = row[6].ToString();
                txtfromclaimn.Text = row[7].ToString();
                txttoclaimn.Text = row[8].ToString();
                dpservdate.Text = row[9].ToString();
                txtcheckn.Text = row[10].ToString();
                txtcheckvalue.Text = row[11].ToString();
                txtsheckclaim.Text = row[12].ToString();
                txt5asmvalue.Text = row[13].ToString();
                txt5asmvalue1.Text = row[14].ToString();
                cbxdiscresoun.Text = row[15].ToString();
                txtnotsforrev.Text = row[16].ToString();
                txtrevname.Text = row[17].ToString();
                dprevdate.Text = row[18].ToString();
                dpaccountdate.Text = row[19].ToString();
                saveEmpBtn22_Copy6.IsEnabled = false;
                btnediteemp_Copy.IsEnabled = true;

            }
            catch
            {

            }
        }

        private void btnediteemp_Copy_Click(object sender, RoutedEventArgs e)
        {

            db.RunNonQuery(@" UPDATE IND_DATA SET IND_TYP ='" + cbxindtyp.Text + "',COMPNY_ID='" + cbxindcompany.Text + "' ,ARCHIVE_RECEIPT_DATE='" + dprecivedatearch.Text + "' ,REVIEW_RECEIPT_DATE='" + dprecivedaterev.Text + "' ,RECIPIENT_NAME_FROM_ARCH ='" + txtrecivenamearch.Text + "',RECIPIENT_NAME_FROM_REVIEW ='" + txtrecivenamearch.Text + "',FROM_CLAIM='" + txtfromclaimn.Text + "' ,TO_CLAIM ='" + txttoclaimn.Text + "',SERVICE_DATE ='" + dpservdate.Text + "',CHECK_NO='" + txtcheckn.Text + "' ,CHECK_VALUE='" + txtcheckvalue.Text + "' ,CLAIM_VALUE='" + txtsheckclaim.Text + "' ,DISCOUNT_VALUE='" + txt5asmvalue.Text + "' ,ADDED_VALUE='" + txt5asmvalue1.Text + "' ,DISCOUNT_REASON ='" + cbxdiscresoun.Text + "',NOTS='" + txtnotsforrev.Text + "' ,CONSULTATIVE_NAME ='" + txtrevname.Text + "',CONSUL_DATE ='" + dprevdate.Text + "',ACCOUNTS_RECEIPT_DATE ='" + dpaccountdate.Text + "'  WHERE CODE='" + txtindcode.Text + "'", "تم التعليل بنجاح");
            researshfalse();

        }

        void resarsh()
        {
            string x = "N";
            System.Data.DataTable ine = db.RunReader("select DONE from IND_DATA  WHERE CODE ='" + searchtxt_Copy.Text + "'").Result;
            if (ine.Rows.Count > 0)
            {

                if (ine.Rows[0][0].ToString() != "N")
                {
                    saveEmpBtn22_Copy6.IsEnabled = false;
                    but_don.IsEnabled = false;
                    btnediteemp_Copy.IsEnabled = false;
                    x = ine.Rows[0][0].ToString();
                    dgbyanatmotalbatfardiaa.IsEnabled = false;
                }



                ine = db.RunReader("select CODE as كود_العملية, IND_TYP as نوع_المطالبة ,COMPNY_ID as الشركة , to_char(ARCHIVE_RECEIPT_DATE,'DD/MM/YYYY') as  تاريخ_استلام_اللأرشيف ,to_char( REVIEW_RECEIPT_DATE ,'DD/MM/YYYY') as  تاريخ_استلام_المراجع  ,RECIPIENT_NAME_FROM_ARCH as اسم_المستلم_من_قسم_الارشيف ,RECIPIENT_NAME_FROM_REVIEW as اسم_المستلم_من_قسم_المراحع ,FROM_CLAIM as من_رقم_المطالبة ,TO_CLAIM as الى_رقم_المطالبة ,to_char( SERVICE_DATE ,'DD/MM/YYYY') as  تاريخ_الخدمة  ,CHECK_NO as رقم_الشيك ,CHECK_VALUE as قيمة_الشيك ,CLAIM_VALUE as قيمة_المطالبة ,DISCOUNT_VALUE as قيمة_الخصومات ,ADDED_VALUE as قيمة_الاضافات ,DISCOUNT_REASON as سبب_الخصومات ,NOTS as ملاحظات_المراجة ,CONSULTATIVE_NAME as اسم_المراجع ,to_char( CONSUL_DATE ,'DD/MM/YYYY') as  تاريخ_المراجعة  ,to_char( ACCOUNTS_RECEIPT_DATE,'DD/MM/YYYY') as  تاريخ_استلام_الحسابات   from IND_DATA  WHERE CODE ='" + searchtxt_Copy.Text + "'  AND DONE='" + x + "'").Result;
                dgbyanatmotalbatfardiaa.ItemsSource = ine.DefaultView;


                txtindcode.Text = ine.Rows[0][0].ToString();
                cbxindtyp.Text = ine.Rows[0][1].ToString();
                cbxindcompany.Text = ine.Rows[0][2].ToString();
                dprecivedatearch.Text = ine.Rows[0][3].ToString();
                dprecivedaterev.Text = ine.Rows[0][4].ToString();
                txtrecivenamearch.Text = ine.Rows[0][5].ToString();
                txtrecivenamerev.Text = ine.Rows[0][5].ToString();
                txtrecivenamerev.Text = ine.Rows[0][6].ToString();
                txtfromclaimn.Text = ine.Rows[0][7].ToString();
                txttoclaimn.Text = ine.Rows[0][8].ToString();
                dpservdate.Text = ine.Rows[0][9].ToString();
                txtcheckn.Text = ine.Rows[0][10].ToString();
                txtcheckvalue.Text = ine.Rows[0][11].ToString();
                txtsheckclaim.Text = ine.Rows[0][12].ToString();
                txt5asmvalue.Text = ine.Rows[0][13].ToString();
                txt5asmvalue1.Text = ine.Rows[0][14].ToString();
                cbxdiscresoun.Text = ine.Rows[0][15].ToString();
                txtnotsforrev.Text = ine.Rows[0][16].ToString();
                txtrevname.Text = ine.Rows[0][17].ToString();
                dprevdate.Text = ine.Rows[0][18].ToString();
                dpaccountdate.Text = ine.Rows[0][19].ToString();


            }
            else
            {
                MessageBox.Show("الكود غير صحيح");
            }
        }

        private void searchtxt_Copy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                resarsh();
            }
        }

        private void searchbtnnew_Copy_Click(object sender, RoutedEventArgs e)
        {
            resarsh();
        }


        private void but_don_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dgbyanatmotalbatfardiaa.SelectedItems.Count; i++)
            {

                DataRowView row = (DataRowView)dgbyanatmotalbatfardiaa.SelectedItems[i];
                db.RunNonQuery("UPDATE IND_DATA  SET DONE='Y' WHERE CODE='" + row[0].ToString() + "'");


            }

            researshfalse();
        }

        void cominsert()
        {
            System.Data.DataTable ine = db.RunReader("select CODE as كود_العملية, IND_TYP as نوع_المطالبة ,COMPNY_ID as الشركة , to_char(ARCHIVE_RECEIPT_DATE,'DD/MM/YYYY') as  تاريخ_استلام_اللأرشيف ,to_char( REVIEW_RECEIPT_DATE ,'DD/MM/YYYY') as  تاريخ_استلام_المراجع  ,RECIPIENT_NAME_FROM_ARCH as اسم_المستلم_من_قسم_الارشيف ,RECIPIENT_NAME_FROM_REVIEW as اسم_المستلم_من_قسم_المراحع ,FROM_CLAIM as من_رقم_المطالبة ,TO_CLAIM as الى_رقم_المطالبة ,to_char( SERVICE_DATE ,'DD/MM/YYYY') as  تاريخ_الخدمة  ,CHECK_NO as رقم_الشيك ,CHECK_VALUE as قيمة_الشيك ,CLAIM_VALUE as قيمة_المطالبة ,DISCOUNT_VALUE as قيمة_الخصومات ,ADDED_VALUE as قيمة_الاضافات ,DISCOUNT_REASON as سبب_الخصومات ,NOTS as ملاحظات_المراجة ,CONSULTATIVE_NAME as اسم_المراجع ,to_char( CONSUL_DATE ,'DD/MM/YYYY') as  تاريخ_المراجعة  ,to_char( ACCOUNTS_RECEIPT_DATE,'DD/MM/YYYY') as  تاريخ_استلام_الحسابات   from IND_DATA WHERE DONE='Y'  AND READY 'W' AND COMPNY_ID like'%" + cbxindcompany_Copy.Text + "' AND   order by code DESC").Result;

            dgbyanatmotalbatfardiaa1_Copy.ItemsSource = ine.DefaultView;
            cbxindcompany_Copy1.ItemsSource = db.RunReader("select CODE from IND_DATA WHERE DONE='Y'  AND READY 'W' AND COMPNY_ID like'%" + cbxindcompany_Copy.Text + "'   AND CHECK_NO  like'%" + cbxindcompany_Copy2.Text + "'  order by code DESC").Result.DefaultView;
            cbxindcompany_Copy2.ItemsSource = db.RunReader("select  CHECK_NO from IND_DATA WHERE DONE='Y'  AND READY 'W' AND COMPNY_ID like '%" + cbxindcompany_Copy.Text + "'   AND CODE  like'%" + cbxindcompany_Copy1.Text + "'  order by code DESC").Result.DefaultView;
        }

        private void cbxindcompany_Copy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                cominsert();
            }
        }

        private void dgbyanatmotalbatfardiaa1_Copy_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)dgbyanatmotalbatfardiaa1_Copy.SelectedItem;
                cbxindcompany_Copy1.Text = row[0].ToString();
                cbxindcompany_Copy.Text = row[2].ToString();
                cbxindcompany_Copy2.Text = row[10].ToString();
                btnediteemp_Copy1.IsEnabled = false;
                btnediteemp_Copy2.IsEnabled = false;

                saveEmpBtn22_Copy3.IsEnabled = true;

            }
            catch
            {

            }

        }

        private void cbxindcompany_Copy_DropDownClosed(object sender, EventArgs e)
        {
            cominsert();

        }

        void updated()
        {
            db.RunNonQuery("UPDATE IND_DATA SET CHECK_DATE='" + dprecivedatearch1_Copy.Text + "',CHECK_FROM='" + txtrecivenamerev1_Copy.Text + "',CHECK_TO='" + cbxindcompany1.Text + "',CHECK_BANK='" + cbxindcompany1_Copy.Text + "',SIGNATURE_DATE='" + dprecivedatearch1_Copy1.Text + "',ACCOUNTING_NOTES='" + txtrecivenamerev1_Copy1.Text + "',CUSTOMER_RECEIPT_DATE='" + dprecivedatearch1_Copy2.Text + "',CUSTOMER_NAME='" + txtrecivenamerev1_Copy2.Text + "',READY='W' WHERE COMPNY_ID='" + cbxindcompany_Copy.Text + "' AND CODE  ='" + cbxindcompany_Copy1.Text + "' AND CHECK_NO  ='" + cbxindcompany_Copy2.Text + "'", "تم");
            researshtrue();
            refsecandgrid();
        }

        private void saveEmpBtn22_Copy3_Click(object sender, RoutedEventArgs e)
        {
            updated();
        }

        private void btnediteemp_Copy1_Click(object sender, RoutedEventArgs e)
        {
            updated();
        }

        private void btnediteemp_Copy2_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dgbyanatmotalbatfardiaa1.SelectedItems.Count; i++)
            {

                DataRowView row = (DataRowView)dgbyanatmotalbatfardiaa1.SelectedItems[i];
                db.RunNonQuery("UPDATE IND_DATA  SET  READY='Y' WHERE CODE='" + row[8].ToString() + "'");

            }
            refsecandgrid();

        }

        private void dgbyanatmotalbatfardiaa1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                //  dgbyanatmotalbatfardiaa1.ItemsSource = db.RunReader("select  CHECK_DATE as تاريخ_اصدار_الشيك ,CHECK_FROM as المسؤل_عن_اصدار_الشيك,CHECK_TO as يصدر_الشيك_لصالح,CHECK_BANK as البنك,SIGNATURE_DATE as تاريخ_توقيع_الادارة,ACCOUNTING_NOTES as الملاحظات,CUSTOMER_RECEIPT_DATE as تاريخ_استلام_العميل,CUSTOMER_NAME as اسم_العميل  , CODE as كود_العملية , COMPNY_ID as الشركة , CHECK_NO as  رقم_الشيك from IND_DATA WHERE DONE='Y'  AND READY='W' order by code DESC").Result.DefaultView;


                DataRowView row = (DataRowView)dgbyanatmotalbatfardiaa1.SelectedItem;
                dprecivedatearch1_Copy.Text = row[0].ToString();
                txtrecivenamerev1_Copy.Text = row[1].ToString();
                cbxindcompany1.Text = row[2].ToString();
                cbxindcompany1_Copy.Text = row[3].ToString();
                dprecivedatearch1_Copy1.Text = row[4].ToString();
                txtrecivenamerev1_Copy1.Text = row[5].ToString();
                dprecivedatearch1_Copy2.Text = row[6].ToString();
                txtrecivenamerev1_Copy2.Text = row[7].ToString();
                cbxindcompany_Copy1.Text = row[8].ToString();
                cbxindcompany_Copy2.Text = row[10].ToString();
                cbxindcompany_Copy.Text = row[9].ToString();
                saveEmpBtn22_Copy3.IsEnabled = false;
                btnediteemp_Copy1.IsEnabled = true;
                btnediteemp_Copy2.IsEnabled = true;

            }
            catch
            {

            }
        }

        void refsecandgrid()
        {
            dgbyanatmotalbatfardiaa1.ItemsSource = db.RunReader("select  CHECK_DATE as تاريخ_اصدار_الشيك ,CHECK_FROM as المسؤل_عن_اصدار_الشيك,CHECK_TO as يصدر_الشيك_لصالح,CHECK_BANK as البنك,SIGNATURE_DATE as تاريخ_توقيع_الادارة,ACCOUNTING_NOTES as الملاحظات,CUSTOMER_RECEIPT_DATE as تاريخ_استلام_العميل,CUSTOMER_NAME as اسم_العميل  , CODE as كود_العملية , COMPNY_ID as الشركة , CHECK_NO as  رقم_الشيك from IND_DATA WHERE DONE='Y'  AND READY='W' order by code DESC").Result.DefaultView;
        }


        private void TabControl_SelectionChanged_2(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }


        private void newEmpBtn_Copy1_Click(object sender, RoutedEventArgs e)
        {
            saveEmpBtn22_Copy3.IsEnabled = true;
            btnediteemp_Copy1.IsEnabled = false;
            btnediteemp_Copy2.IsEnabled = false;
            cbxindcompany_Copy.Text = "";
            cbxindcompany_Copy1.Text = "";
            cbxindcompany_Copy2.Text = "";
            dprecivedatearch1_Copy.Text = "";
            txtrecivenamerev1_Copy.Text = "";
            cbxindcompany1.Text = "";
            cbxindcompany1_Copy.Text = "";
            dprecivedatearch1_Copy1.Text = ""; txtrecivenamerev1_Copy1.Text = "";
            dprecivedatearch1_Copy2.Text = "";
            txtrecivenamerev1_Copy2.Text = "";
            searchtxt_Copy1.Text = "";
            refsecandgrid();


        }

        void stry()
        {
            string x = "W";
            System.Data.DataTable ine = db.RunReader("select READY from IND_DATA  WHERE CODE ='" + searchtxt_Copy1.Text + "'").Result;
            if (ine.Rows.Count > 0)
            {

                if (ine.Rows[0][0].ToString() != "W")
                {
                    saveEmpBtn22_Copy3.IsEnabled = false;
                    btnediteemp_Copy2.IsEnabled = false;
                    btnediteemp_Copy1.IsEnabled = false;
                    x = ine.Rows[0][0].ToString();
                    dgbyanatmotalbatfardiaa1.IsEnabled = false;
                }



                ine = db.RunReader("select  CHECK_DATE as تاريخ_اصدار_الشيك ,CHECK_FROM as المسؤل_عن_اصدار_الشيك,CHECK_TO as يصدر_الشيك_لصالح,CHECK_BANK as البنك,SIGNATURE_DATE as تاريخ_توقيع_الادارة,ACCOUNTING_NOTES as الملاحظات,CUSTOMER_RECEIPT_DATE as تاريخ_استلام_العميل,CUSTOMER_NAME as اسم_العميل  , CODE as كود_العملية , COMPNY_ID as الشركة , CHECK_NO as  رقم_الشيك from IND_DATA WHERE CODE ='" + searchtxt_Copy1.Text + "' AND DONE='Y'  AND READY='W' order by code DESC").Result;
                dgbyanatmotalbatfardiaa1.ItemsSource = ine.DefaultView;

                dprecivedatearch1_Copy.Text = ine.Rows[0][0].ToString();
                txtrecivenamerev1_Copy.Text = ine.Rows[0][1].ToString();
                cbxindcompany1.Text = ine.Rows[0][2].ToString();
                cbxindcompany1_Copy.Text = ine.Rows[0][3].ToString();
                dprecivedatearch1_Copy1.Text = ine.Rows[0][4].ToString();
                txtrecivenamerev1_Copy1.Text = ine.Rows[0][5].ToString();
                dprecivedatearch1_Copy2.Text = ine.Rows[0][6].ToString();
                txtrecivenamerev1_Copy2.Text = ine.Rows[0][7].ToString();
                cbxindcompany_Copy1.Text = ine.Rows[0][8].ToString();
                cbxindcompany_Copy2.Text = ine.Rows[0][10].ToString();
                cbxindcompany_Copy.Text = ine.Rows[0][9].ToString();


            }
            else
            {
                MessageBox.Show("الكود غير صحيح");
            }
        }

        private void searchtxt_Copy1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                stry();
            }
        }

        private void searchbtnnew_Copy1_Click(object sender, RoutedEventArgs e)
        {
            stry();
        }

        private void cbxindcompany_Copy1_DropDownClosed(object sender, EventArgs e)
        {

            cbxindcompany_Copy2.ItemsSource = db.RunReader("select  CHECK_NO from IND_DATA WHERE DONE='Y'  AND READY 'W' AND COMPNY_ID like '%" + cbxindcompany_Copy.Text + "'   AND CODE  like'%" + cbxindcompany_Copy1.Text + "'  order by code DESC").Result.DefaultView;
        }

        private void cbxindcompany_Copy2_DropDownClosed(object sender, EventArgs e)
        {
            cbxindcompany_Copy1.ItemsSource = db.RunReader("select CODE from IND_DATA WHERE DONE='Y'   AND READY 'W' AND COMPNY_ID like'%" + cbxindcompany_Copy.Text + "'   AND CHECK_NO  like'%" + cbxindcompany_Copy2.Text + "'  order by code DESC").Result.DefaultView;
        }





        #endregion
    }


}
