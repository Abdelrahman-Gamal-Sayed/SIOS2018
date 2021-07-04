using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication2.DataLayer;
using System.Net;
namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        
        public Page1()
        {
            InitializeComponent();
            //WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            // ShowsNavigationUI = false;

            if (Properties.Settings.Default.Open == "No")
            {
                nametxt.Text = Properties.Settings.Default.NameUser;
                nametxt.IsEnabled = false;
            }
            else
                nametxt.IsEnabled = true;


            DB db = new DB();
            User.DtAllCompanys = db.RunReader(" select distinct C_COMP_ID , C_ANAME from V_COMPANIES  ORDER BY C_COMP_ID ").Result;
            nametxt.Focus();
        }
        //torb12-6
        void start() {
            try {
                agents agent = new agents();
                string name = "";
                string pass = "";
                int result = 0;

                name = nametxt.Text.ToString();
                pass = passtxt.Password.ToString();

                DB dbt = new DB();

                System.Data.DataTable xname = dbt.RunReader("select name from agent where  upper(name)=upper('" + name + "')").Result;
                if(xname.Rows.Count > 0)
                    name = xname.Rows[0][0].ToString();
                if(name != "" && pass != "") {
                    result = agent.validate_agent(pass, name).Result;

                    string confirmed = "";
                    string net = "";
                    string app = "";
                    string medic = "";
                    string ind = "";
                    string rep = "";
                    string cheq = "";
                    string online = "";
                    string contr = "";
                    string flag = "";
                    string dept = "";
                    string basic = "";
                    string active = "";
                    string company = "";
                    string usertype = ""; string print = ""; string note = "", cust = "", complain = "";
                    string report = ""; string store = "";
                    string tele_sales = ""; string re_collect = "";
                    string after_sales = ""; string high_department = "";
                    string compid = "";
                    string collect_data = "", maintab = "";
                    System.Data.DataTable user = agent.get_employee_authority(name);
                    
                    if(result == 1) {
                        System.Data.DataTable dlognold = dbt.RunReader(@"select * from LOGINHIST where USERNAME not like 'admin%' and USERNAME = '" + name + "' and MACHINE_NAME = '" + Environment.MachineName + "' and active = 'Y'").Result;

                        if(dlognold.Rows.Count == 1)
                            dbt.RunNonQuery(@"UPDATE LOGINHIST SET END_LOGIN = systimestamp , ACTIVE = 'N' WHERE USERNAME = '" + name + "' and MACHINE_NAME = '" + Environment.MachineName + "' and active = 'Y'");

                        System.Data.DataTable dlogin = dbt.RunReader(@"select * from LOGINHIST where USERNAME not like 'admin%' and USERNAME != 'dr.Reham' and USERNAME = '" + name + "' and active = 'Y'").Result;

                        if(dlogin.Rows.Count == 0) {
                            net = user.Rows[0].ItemArray[4].ToString();
                            app = user.Rows[0].ItemArray[1].ToString();
                            cheq = user.Rows[0].ItemArray[2].ToString();
                            ind = user.Rows[0].ItemArray[3].ToString();
                            rep = user.Rows[0].ItemArray[5].ToString();
                            online = user.Rows[0].ItemArray[6].ToString();
                            medic = user.Rows[0].ItemArray[7].ToString();
                            contr = user.Rows[0].ItemArray[8].ToString();
                            dept = user.Rows[0].ItemArray[9].ToString();
                            User.Department = dept;
                            User.Name = name;
                            flag = user.Rows[0].ItemArray[10].ToString();
                            User.Manegar = flag;
                            basic = user.Rows[0].ItemArray[13].ToString();
                            active = user.Rows[0].ItemArray[14].ToString();
                            User.Code = user.Rows[0].ItemArray[11].ToString();
                            company = user.Rows[0].ItemArray[15].ToString();
                            User.CompanyName = company;

                            usertype = user.Rows[0].ItemArray[16].ToString();
                            User.Type = usertype;
                            print = user.Rows[0].ItemArray[17].ToString();
                            note = user.Rows[0].ItemArray[18].ToString();
                            store = user.Rows[0].ItemArray[19].ToString();
                            cust = user.Rows[0].ItemArray[20].ToString();
                            report = user.Rows[0].ItemArray[21].ToString();
                            complain = user.Rows[0].ItemArray[22].ToString();
                            confirmed = user.Rows[0].ItemArray[23].ToString();
                            User.medicalManage = user.Rows[0].ItemArray[24].ToString();
                            User.complainMember = user.Rows[0].ItemArray[25].ToString();
                            User.hr_request = user.Rows[0].ItemArray[29].ToString();
                            User.revise = user.Rows[0].ItemArray[27].ToString();
                            User.Mail = user.Rows[0].ItemArray[28].ToString();
                            User.REQUESHR = user.Rows[0].ItemArray[29].ToString();
                            User.Noti.HrRequests = user.Rows[0].ItemArray[30].ToString();
                            tele_sales = user.Rows[0].ItemArray[34].ToString();
                            tele_sales = user.Rows[0].ItemArray[34].ToString();
                            re_collect = user.Rows[0].ItemArray[35].ToString();
                            after_sales = user.Rows[0].ItemArray[36].ToString();
                            high_department = user.Rows[0].ItemArray[37].ToString();
                            User.CompanyID = compid = user.Rows[0].ItemArray[38].ToString();
                            collect_data = user.Rows[0].ItemArray[39].ToString();
                            maintab = user.Rows[0].ItemArray[40].ToString();
                            User.Opration = user.Rows[0].ItemArray[41].ToString();
                            User.Policy = user.Rows[0].ItemArray[42].ToString();
                            User.Claim = user.Rows[0].ItemArray[43].ToString();
                            User.FULL_CONTRACT = user.Rows[0].ItemArray[44].ToString();
                            if(confirmed != "N") {

                                if(active == "y" || active == "Y") {

                                    dbt.RunNonQuery(@"INSERT INTO LOGINHIST (USERNAME, DATE_LOGIN, MACHINE_NAME, MACHINE_IP, ACTIVE ) VALUES ('" + name + "', systimestamp, '" + Environment.MachineName + "', '" + Dns.GetHostByName(Environment.MachineName).AddressList[0].ToString() + "', 'Y')");

                                    if(off.IsChecked == true) {
                                        User.page3English = new page3English(name, app, net, medic, ind, rep, contr, cheq, online, flag, dept, basic, active, company, usertype, print, store, note, cust, report, complain);
                                        this.NavigationService.Navigate(User.page3English);
                                    } else {

                                        User.page3 = new Page3(name, app, net, medic, ind, rep, contr, cheq, online, flag, dept, basic, active, company, usertype, print, store, note, cust, report, complain, tele_sales, re_collect, after_sales, high_department, compid, collect_data, maintab);
                                        this.NavigationService.Navigate(User.page3);
                                    }
                                } else if(active == "n" || active == "N") {

                                    MessageBox.Show("مستخدم منتهي");
                                }


                            } else {
                                //  Page1 p1 = new Page1();
                                this.Visibility = Visibility.Hidden;
                                Password a = new Password(this);
                                a.ShowDialog();
                                nametxt.Text = "";
                                passtxt.Password = "";

                                //  this.Visibility = Visibility.Visible;

                            }
                        } else {
                            MessageBox.Show("تم التسجيل إلى السيستم بواسطة هذا المستخدم ولم يتم تسجيل الخروج");
                        }
                        ////////////here                        
                    } else {
                        MessageBox.Show("Username or password incorrect");
                    }
                } else if(name == "" || pass == "") {
                    MessageBox.Show("make sure you enter username & password");
                }

            } catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void passtxt_KeyDown(object sender, KeyEventArgs e){ if (e.Key == Key.Enter) start();}
        private void nametxt_KeyDown(object sender, KeyEventArgs e){if (e.Key == Key.Enter) start();}
        private void Button_Click(object sender, RoutedEventArgs e){start();}

        private void ON_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                zxzx.Visibility = Visibility.Visible;
            zczc.Visibility = Visibility.Visible;
            aaaaabtn.Visibility = Visibility.Visible;

            zxzx_Copy.Visibility = Visibility.Hidden;
            zczc_Copy.Visibility = Visibility.Hidden;
            aaaaabtn_Copy.Visibility = Visibility.Hidden;


                asas.FlowDirection = FlowDirection.RightToLeft;
                asas.Header = "اللغة";
            }
            catch
            { }

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                zxzx.Visibility = Visibility.Hidden;
                zczc.Visibility = Visibility.Hidden;
                aaaaabtn.Visibility = Visibility.Hidden;

                zxzx_Copy.Visibility = Visibility.Visible;
                zczc_Copy.Visibility = Visibility.Visible;
                aaaaabtn_Copy.Visibility = Visibility.Visible;
                asas.FlowDirection = FlowDirection.LeftToRight;
                asas.Header = "Languge";
            }
            catch
            { }
        }

        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (nametxt.Text != string.Empty && nametxt.Text.ToLower().StartsWith("admin") == false)
            {
                OpenAndLock.Visibility = Visibility.Visible;
                OpenAndLock.Focus();
            }
            else
            {
                MessageBox.Show("من فضلك أدخل اسم المستخدم");
                nametxt.Focus();
            }
        }
        private void OpenAndLock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (OpenAndLock.Password == Properties.Settings.Default.Password)
                {
                    if (nametxt.IsEnabled == true)
                    {
                        if (nametxt.Text != string.Empty && nametxt.Text.ToLower().StartsWith("admin") == false)
                        {
                            nametxt.IsEnabled = false;
                            Properties.Settings.Default.Open = "No";
                            Properties.Settings.Default.NameUser = nametxt.Text;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            MessageBox.Show("من فضلك أدخل اسم المستخدم");
                            nametxt.Focus();
                        }
                    }
                    else
                    {
                        nametxt.IsEnabled = true;
                        Properties.Settings.Default.Open = "Yes";
                        Properties.Settings.Default.Save();
                    }
                }
                else
                    MessageBox.Show("Password is Wrong");

                OpenAndLock.Clear();
                OpenAndLock.Visibility = Visibility.Hidden;
            }
        }

 
    }
}
