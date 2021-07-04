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
using System.Windows.Shapes;
using System.Data;
using WpfApplication2.DataLayer;
using System.Drawing;
using System.IO;
using System.Data.OracleClient;
using Microsoft.Win32;

namespace WpfApplication2
{

    //dd-MMM-yy

    /// <summary>
    /// Interaction logic for HrRequest.xaml
    /// </summary>
    public partial class HrRequest : Window
    {
       public HrRequest(string user, string company)
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
            UserName = user;
            UserCompany = company;

            //abdo
            username = user;
            companyNameqw = company;

            System.Threading.Thread.CurrentThread.CurrentCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd-MMM-yy";


            if (User.Type == "DMS Member")
            {
                NewEmpCompCombo.ItemsSource = db.RunReader("select distinct C_COMP_ID ,C_ANAME from V_COMPANIES order by c_comp_id").Result.DefaultView;
            }
            else
            {
                NewEmpCompCombo.Visibility = Visibility.Hidden;
                zxcv.Visibility = Visibility.Hidden;
                NewEmpCompanySrchBtn.Visibility = Visibility.Hidden;

            }


        }

        public static string UserName = "";
        public static string UserCompany = "";

        Reports report = new Reports();
        Clients client = new Clients();
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
                else if (branchCombo.Text == null && branchCombo.ItemsSource != null)
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
                    if (branchCombo.ItemsSource != null && branchCombo.Text != null)
                    {
                        branch = branchCombo.Text.ToString();
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
                        gender = "2";
                    }
                    else if (malerb.IsChecked == true)
                    {
                        gender = "1";
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
                            , address, nationalid, birthdate, gender, branch, relation, empid, mob, startdate, UserName, zz, zz2,"","");
                        DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where national_id = '" + nationalid + "' ").Result;
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
                        }
                        else
                            MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString() + "\n" + "مع مراعاة انها لم يتم ارفاق صورة الموظف");



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
                if (deleteEmpNumtxt.Text == null)
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
                        DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where card_id ='" + empid + "' and terminate_date ='" + term_date + "' and DELIVER_CARD_FLAG ='" + flag + "'  and approve_flag ='n' and REGISTER_TYPE ='P'and type='3'").Result;
                        lbl22.Content = temp.Rows[0][0].ToString();
                        MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString());
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
            lbl22.Content="*********";
            delEmpCompCombo.Text = "";
        }

        private void EditClassSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cardnumtxt22.Text == "" || classtxt.Text == null)
                {
                    MessageBox.Show("من فضلك ادخل البيانات كاملة");
                }
                else
                {

                     
                    string card = cardnumtxt22.Text.ToString();
                    string[] arr = card.Split('-');
                    int comp = Convert.ToInt32(arr[0].ToString());
                    int compid=Convert.ToInt32( report.get_comp_id(UserCompany));
                    if (comp == compid)
                    {
                        int count = client.validate_CardInCompEmployees(card);
                        if (count >= 1)
                        {
                            string newclass = classtxt.Text.ToString();
                            string richText = new TextRange(reasontxt22.Document.ContentStart, reasontxt22.Document.ContentEnd).Text;
                           // hr.edit_class_request(card, newclass, richText, UserName,compid);

                            DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where card_id ='"+ card + "' and REGISTER_TYPE ='P' and type='2' and created_by ='"+ UserName + "' and emp_class ='"+ newclass + "' and approve_flag ='n'").Result;
                            lblz.Content = temp.Rows[0][0].ToString();
                            MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString());
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
        DB db = new DB();
        HRNetwork hrnet = new HRNetwork();
        private void fill_emp_code()
        {
            try
            {
             int compid = Convert.ToInt32(report.get_comp_id(UserCompany));
              DataSet  dataset_emp_card = db.RunReaderds("select distinct  emp_code ,EMP_eNAME_ST ,EMP_eNAME_SC,EMP_eNAME_TH  from COMP_EMPLOYEESS WHERE C_COMP_ID=" + compid + " ORDER BY emp_code ");
              deleteEmpNumtxt.ItemsSource = dataset_emp_card.Tables[0].DefaultView;
            }
            catch { }
        }
        private void fill_card_id(ComboBox c)
        {
            try
            {
                int compid=0;
                if (User.Type == "hr")
                {
                     compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                }
                else
                {
                    compid = Convert.ToInt32(classEmpCompCombo.Text.ToString());
                }
                int contract = hrnet.get_max_contract(compid);
                DataSet dataset_emp_card = db.RunReaderds("select distinct  card_id ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH from COMP_EMPLOYEESS WHERE C_COMP_ID=" + compid + " and contract_no="+contract+" ORDER BY card_id ");
                c.ItemsSource = dataset_emp_card.Tables[0].DefaultView;
            }
            catch { }
        }
        private void employeetab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (addemp.IsSelected == true && branchCombo.Text == null && empnumtxt.Text == "" && empnumtxt.Visibility == Visibility.Hidden && NewEmpCompCombo.ItemsSource==null)
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
                        zxcv.Visibility = Visibility.Hidden;
                        NewEmpCompanySrchBtn.Visibility = Visibility.Hidden;

                    }

                    int compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                    DataTable data = hr.get_branch(compid);
                    branchCombo.Items.Clear();
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        branchCombo.Items.Add(data.Rows[i].ItemArray[0].ToString());
                    }
                    fill_card_id(empnumtxt);
                }
                catch { }
            }
            else if (deleteemp.IsSelected == true && deleteEmpNumtxt.Text == "" && deleteEmpNumtxt.ItemsSource==null )
            {

                if (User.Type == "DMS Member" && delEmpCompCombo.ItemsSource == null)
                {
                    delEmpCompCombo.ItemsSource = db.RunReader("select distinct C_COMP_ID ,C_ANAME from V_COMPANIES order by c_comp_id").Result.DefaultView;
                }
                else if(User.Type=="hr")
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
            else if (editemp.IsSelected == true && cardnumtxt22.Text == null && classtxt.Text == null)
            {
                try
                {


                    if (User.Type == "DMS Member" && classEmpCompCombo.ItemsSource==null)
                    {
                        classEmpCompCombo.ItemsSource = db.RunReader("select distinct C_COMP_ID ,C_ANAME from V_COMPANIES order by c_comp_id").Result.DefaultView;


                    }
                    else if(User.Type=="hr")
                    {
                        eteEmpSrchCardBtn_Copy.Visibility = Visibility.Hidden;
                        classEmpCompCombo.Visibility = Visibility.Hidden;
                        editcomplbl.Visibility = Visibility.Hidden;
                        fill_card_id(cardnumtxt22);
                        int compid = Convert.ToInt32(report.get_comp_id(UserCompany));
                        DataTable classcode = hr.get_class_name(compid);
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
            if (reprint.IsSelected == true && cbxcardnoqw.ItemsSource == null )
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
                    else if (User.Type=="hr")
                    {
                        fillqw();
                        AutoNumeqw();
                        addpic.Visibility = Visibility.Hidden;
                        addpic_Copy.Visibility = Visibility.Hidden;
                        poiu.Visibility = Visibility.Hidden;
                        cbxclassEmpCompCombo.Visibility = Visibility.Hidden;
                        imgsearchCust_Copy221.Visibility = Visibility.Hidden;
                        fillcardname(cbxcardnoqw);
                    }

                }
                catch { }
            }
            if (addproviderwq.IsSelected == true && cbxstateqw.ItemsSource == null)
            {
                fillqw();
                AutoNumeqw();
                fillcardname(cbxcardnoqw);
                addpic.Visibility = Visibility.Hidden;
                addpic_Copy.Visibility = Visibility.Hidden;
              
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
                DataTable dtouto = db.RunReader("select count(CODE) from HR_PROVIDERS_REQUEST ").Result;

                if (dtouto.Rows[0][0].ToString() != DBNull.Value.ToString())
                    numqw = (Convert.ToInt32(dtouto.Rows[0][0].ToString()) + 1).ToString();
                else
                    numqw = "1";
            }
            catch { }



        }
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

        private void fillcardname(ComboBox c,string card)
        {
            try
            {
                //  MessageBox.Show(companyIDqw);
                dataset_emp_card = db.RunReaderds(@"select distinct CARD_ID ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH 
                                                 from COMP_EMPLOYEESS WHERE (card_id like '%"+card+ "%' or emp_aname_st like '%" + card + "%' or emp_aname_sc like '%" + card + "%' or emp_aname_th like '%" + card + "%') and C_COMP_ID=" + companyIDqw + " ORDER BY CARD_ID ");
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
                DataTable dd;
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
        string path;
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


                    Bitmap newimg = new Bitmap(path);

                }




                btnsaveagainqw.IsEnabled = true;
            }
            catch { }
        }

        private void btnsaveagainqw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.RunNonQuery(@"INSERT INTO EMPLOYEE_REQUEST (CARD_ID, CREATED_BY, CREATED_DATE, PRINT_REASON, REGISTER_TYPE, TYPE) VALUES   " +
                    "('" + cbxcardnoqw.Text + "','" + username + "','" + DateTime.Now.ToString("dd-MMM-yy") + "','" + cbxreasonqw.Text + "','p','4')");

               DataTable temp= db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where CARD_ID ='" + cbxcardnoqw.Text + "' and CREATED_BY ='" + username + "'" +
                   " and CREATED_DATE ='" + DateTime.Now.ToString("dd-MMM-yy") + "' and PRINT_REASON ='" + cbxreasonqw.Text + "' and REGISTER_TYPE ='p' and  TYPE='4'").Result;
                lbl2.Content = temp.Rows[0][0].ToString();
                MessageBox.Show(" تم ارسال الطلب بنجاح" +"\n"+ "رقم الطلب ----> " + temp.Rows[0][0].ToString());

            }
            catch { }
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
        
            
                DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where CARD_ID ='" + cbxcardnoqw.Text + "' and CREATED_BY ='" + username + "'" +
             " and CREATED_DATE ='" + DateTime.Now.ToString("dd-MMM-yy") + "' and REGISTER_TYPE ='p' and  TYPE='4'").Result;
                lbl4.Content = temp.Rows[0][0].ToString();
     
                db.RunNonQuery("update EMPLOYEE_REQUEST set PRINT_REASON ='" + cbxreasonqw.Text + "' where REQUEST_CODE ='" + temp.Rows[0][0].ToString() + "'");
                MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString());


            }
            catch { }
        }

        private void btnsavereopenqw_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.RunNonQuery(@"INSERT INTO EMPLOYEE_REQUEST (CARD_ID, CREATED_BY, CREATED_DATE, REOPEN_DATE, REGISTER_TYPE, TYPE ) VALUES   " +
           "('" + cbxcardnoqw.Text + "','" + username + "','" + DateTime.Now.ToString("dd-MMM-yy") + "','" + txtcardnumqw_Copy.Text + "','p','5')");

                DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where CARD_ID ='" + cbxcardnoqw.Text + "' and CREATED_BY ='" + username + "'" +
             " and CREATED_DATE ='" + DateTime.Now.ToString("dd-MMM-yy") + "' and REOPEN_DATE='" + txtcardnumqw_Copy.Text + "' and REGISTER_TYPE ='p' and  TYPE='5'").Result;
                lbl7.Content = temp.Rows[0][0].ToString();
                MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString());
            }
            catch { }

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
            lbl9.Content= "********";
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
            string card = txtcardnumnewqw_Copy.Text + '-' + txtcardnumnewqw_Copy2.Text + '-' + newcardid +'-'+ txtcardnaumnewqw_Copy2.Text;
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


                DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where CARD_ID ='" + cbxcardnoqw.Text + "' and CREATED_BY ='" + username + "'" +
        " and CREATED_DATE ='" + DateTime.Now.ToString("dd-MMM-yy") + "'  and REGISTER_TYPE ='p' and  TYPE='6' and NEW_CARD_ID='" + newcardid + "'").Result;
                lbl9.Content = temp.Rows[0][0].ToString();
                MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString());
            }

        }
        //joba23-9
        void sersaddprovgvbdjhddv()
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

                    if (s.Rows[0][8].ToString() == "W")
                    {
                        txtdmsreplayqw.Text = "لم يتم الرد";
                        btnEditeqw.IsEnabled = true;
                    }
                    else if (s.Rows[0][8].ToString() == "Y")
                    {
                        txtdmsreplayqw.Text = " تم الموافقة علي الطلب";
                        btnEditeqw.IsEnabled = false;
                    }
                    else
                    {
                        txtdmsreplayqw.Text = "تم رفض الطلب ";
                        btnEditeqw.IsEnabled = false;
                    }

                }
                else
                {
                    MessageBox.Show("تحقق من الرقم"); return;

                }

                // s.Clear();
            }
            catch { }
        }
        //joba23-9
        private void txtSearchqw_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                sersaddprovgvbdjhddv();
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
                    newcardqw.Visibility = Visibility.Hidden ;
                    reopenqw_Copy.Visibility = Visibility.Visible;
                }


            }
            catch { }
        }

        //joba24-9
        private void qwbtnSearch_Click(object sender, RoutedEventArgs e)
        {

            sersaddprovgvbdjhddv();

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
                    DataTable dd;
                    //   select distinct   SPECIFIC_DATE ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH from COMP_EMPLOYEESS WHERE C_COMP_ID = " + companyIDqw + " ORDER BY CARD_ID
                    if (User.Type != "DMS Member")
                    {
                   dd  = db.RunReader("  select distinct   SPECIFIC_DATE ,EMP_ANAME_ST ,EMP_ANAME_SC,EMP_ANAME_TH from COMP_EMPLOYEESS WHERE CARD_ID = '" + cbxcardnoqw.Text + "' and CARD_ID like '" + User.CompanyID + "%'").Result;
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

            DataTable data = db.RunReader(@"select emp_ename_st,emp_ename_sc,emp_ename_th,emp_ename_fr,
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
                if(gender=="1")
                {
                    malerb.IsChecked = true;
                }
                else if(gender=="2")
                {
                    femalerb.IsChecked = true;
                }

                string relation = data.Rows[0][19].ToString();
                if(relation=="self")
                {
                    emprb.IsChecked = true;

                }
                else if(relation== "Son/Daughter")
                {
                    childrb.IsChecked = true;
                }
                else if(relation== "father/mother")
                {
                    parentrb.IsChecked = true;
                }
                else if(relation== "husband/wife")
                {
                    husbandrb.IsChecked = true;
                }

                if (gla == "Y") ndara.IsChecked = true;
                if (mrad == "Y") mred.IsChecked = true;

                try {
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
            db.RunNonQuery(@"UPDATE EMPLOYEE_REQUEST SET  EMP_ENAME_ST = '" + enamesttxt.Text + "', EMP_ENAME_SC = '" + enamesctxt.Text + "', EMP_ENAME_TH = '" + enamethtxt.Text + "', EMP_ENAME_FR = '" + enamefrtxt.Text + "', EMP_ENAME = '" + enametxt.Text + "', EMP_ANAME_ST = '" + anamesttxt.Text + "', EMP_ANAME_SC = '" + anamesctxt.Text + "', EMP_ANAME_TH = '" + anamethtxt.Text + "', EMP_ANAME_FR = '" + anamefrtxt.Text + "', EMP_ANAME = '" + anametxt.Text + "', NATIONAL_ID = '" + nationalidtxt.Text + "', BIRTHDATE = '" + birthdatetxt.Text + "', MOBILE = '" + mobnumtxt.Text + "', EMAIL ='" + emailtxt.Text + "', START_DATE = '" + startdatetxt.Text + "', ADDRESS = '" + addrtxt.Text + "' WHERE REQUEST_CODE = '" + searchtxt.Text + "' " ,"تم التعديل بنجاح");
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


                    Bitmap newimg = new Bitmap(path2);

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


                DataTable temp = db.RunReader(@"select REQUEST_CODE from EMPLOYEE_REQUEST where CARD_ID ='" + cbxcardnoqw.Text + "' and CREATED_BY ='" + username + "'" +
        "  and REGISTER_TYPE ='p' and  TYPE='7' and emp_ename='" + txtcardnumqw2_Copy9.Text + "'and emp_aname='" + txtcardnumqw2_Copy8.Text + "'").Result;
                lbl88.Content = temp.Rows[0][0].ToString();
                MessageBox.Show(" تم ارسال الطلب بنجاح" + "\n" + "رقم الطلب ----> " + temp.Rows[0][0].ToString());


            } catch(Exception ex)
            { MessageBox.Show(ex.ToString()); }
            
        }

        private void txtcardnumqw2_Copy3_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtcardnumqw2_Copy_TextChanged(object sender, TextChangedEventArgs e){txtcardnumqw2_Copy8.Text = txtcardnumqw2_Copy.Text +" "+ txtcardnumqw2_Copy1.Text + " " + txtcardnumqw2_Copy3.Text + " " + txtcardnumqw2_Copy4.Text;}

        private void txtcardnumqw2_Copy1_TextChanged(object sender, TextChangedEventArgs e){txtcardnumqw2_Copy8.Text = txtcardnumqw2_Copy.Text +" "+ txtcardnumqw2_Copy1.Text + " " + txtcardnumqw2_Copy3.Text + " " + txtcardnumqw2_Copy4.Text;}

        private void txtcardnumqw2_Copy3_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            txtcardnumqw2_Copy8.Text = txtcardnumqw2_Copy.Text +" "+ txtcardnumqw2_Copy1.Text + " " + txtcardnumqw2_Copy3.Text + " " + txtcardnumqw2_Copy4.Text;

        }

        private void txtcardnumqw2_Copy4_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtcardnumqw2_Copy8.Text = txtcardnumqw2_Copy.Text +" "+ txtcardnumqw2_Copy1.Text + " " + txtcardnumqw2_Copy3.Text + " " + txtcardnumqw2_Copy4.Text;

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
                DataTable data = hr.get_branch(Convert.ToInt32(NewEmpCompCombo.Text));
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
            if(e.Key==Key.Enter)
            {

                DataTable data = hr.get_branch(Convert.ToInt32(NewEmpCompCombo.Text));
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
            if(e.Key==Key.Enter)
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
                DataTable classcode = hr.get_class_name(compid);
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
            DataTable classcode = hr.get_class_name(compid);
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
            if(e.Key==Key.Enter)
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
    }
}
