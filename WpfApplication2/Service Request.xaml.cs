using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.OleDb;
using Microsoft.Win32;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Service_Request.xaml
    /// </summary>
    public partial class Service_Request : Window
    {
        public Service_Request()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            fillProvider();
            fillsubject();
            fillEsclated();
            AutoNume();
            btnedite.Visibility = Visibility.Hidden;
            dtpcom.Text = DateTime.Now.ToString();

            //    dtpcom.SelectedDate=date

        }
        public Service_Request(string prov_id, string prov_name, string bran_id, string bran_name)
        {
            InitializeComponent();

            AutoNume();

            provider_id = prov_id;
            provider_name = prov_name;
            branch_id = bran_id;
            branch_addrs = bran_name;
            btnedite.Visibility = Visibility.Hidden;
            dtpcom.Text = DateTime.Now.ToString();
            fillProvider();
            fillsubject();
            fillEsclated();
            cbxprovidersr.Text = provider_id + " - " + provider_name;
            cbxbranchsr.Text = branch_id + " - " + branch_addrs;


            //    dtpcom.SelectedDate=date

        }
        DB db = new DB();
        DataTable tbl = new DataTable();
        string provider_id, provider_name, branch_id, branch_addrs, subject_name, subject_id, sclated_name, sclated_id, solved_name, solved_id;

        private void Filltbl(string SelectStatment)
        {
            tbl.Clear();
            tbl.Columns.Clear();
            tbl = db.RunReader(SelectStatment).Result;

        }
        DataSet dt, dt2, dt3, dt4;

        private void fillProvider()
        {
            dt = db.RunReaderds(" select distinct USER_CO , USER_N from V_PROVIDERS ORDER BY USER_CO ");
            cbxprovidersr.ItemsSource = dt.Tables[0].DefaultView;


        }
        private void fillPranch()
        {

            dt2 = db.RunReaderds(" select distinct USER_ID , ADDRS from V_PROVIDERS  where USER_CO=" + provider_id + "ORDER BY USER_ID");
            cbxbranchsr.ItemsSource = dt2.Tables[0].DefaultView;

        }

        private void fillsubject()
        {
            dt3 = db.RunReaderds(" select SUBJECT_CODE ,SUBJECT_NAME from IMS_COM_SUBJECT  ORDER BY SUBJECT_CODE");
            cbxsubjectsr.ItemsSource = dt3.Tables[0].DefaultView;
        }
        private void fillEsclated()
        {
            dt4 = db.RunReaderds(" select MEMBER_ID , MEMBER_NAME from IMS_ESCLATION_MEMBER  ORDER BY MEMBER_ID");
            cbxesclatedsrsr.ItemsSource = dt4.Tables[0].DefaultView;
            cbxsolvedbysr.ItemsSource = dt4.Tables[0].DefaultView;
        }
        private void AutoNume()
        {

            Filltbl("select max(COM_SER) from IMS_COMPLAINTS ");
            if (tbl.Rows[0][0].ToString() != DBNull.Value.ToString())
                txtCoId.Text = (Convert.ToInt32(tbl.Rows[0][0].ToString()) + 1).ToString();
            else
                txtCoId.Text = "1";

            tbcby.Visibility = Visibility.Hidden;
            tbbranch.Visibility = Visibility.Hidden;
            tbprovider.Visibility = Visibility.Hidden;
            tbsubject.Visibility = Visibility.Hidden;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        string provider_id2, provider_type;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            checkfun();

            //Visits a = new Visits(provider_id, provider_name, branch_id, branch_addrs, sclated_id, sclated_name,provider_id2, provider_type);
            //a.Show();
        }

        private void cbxprovidersr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                provider_id = dt.Tables[0].Rows[cbxprovidersr.SelectedIndex][0].ToString();
                provider_name = dt.Tables[0].Rows[cbxprovidersr.SelectedIndex][1].ToString();
                cbxprovidersr.Text = provider_id + " - " + provider_name;
                fillPranch();

            }
            catch
            { }





        }

        private void cbxbranchsr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {





        }

        private void cbxsubjectsr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        void cleardata()
        {
            AutoNume();
            btnedite.Visibility = Visibility.Hidden;
            txtsearchsr.Clear();
            cbxbranchsr.Text = " ";
            cbxprovidersr.Text = " ";
            cbxsubjectsr.Text = " ";
            cbxesclatedsrsr.Text = "";
            cbxsolvedbysr.Text = "";
            txtproblem.Text = "";
            txtreplay.Text = "";
            txtcreatedby.Text = "";
            txtupdatedby.Text = "";
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
            path = "";
            check = 0;



        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //  btnedite.Visibility = Visibility.Hidden;
            cleardata();

        }

        private void cbxprovidersr_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        private void cbxbranchsr_MouseLeave(object sender, MouseEventArgs e)
        {
        }

        private void cbxprovidersr_DropDownClosed(object sender, EventArgs e)
        {

            try
            {

                cbxprovidersr.Text = provider_id + " - " + provider_name;
                cbxbranchsr.Text = "";
                DataTable s = db.RunReader(" select * from IMS_COMPLAINTS WHERE PROVIDER_CODE = " + provider_id + " ORDER BY COM_DATE desc").Result;
                this.Height = 600;
                tbcby.Visibility = Visibility.Visible;
                tbprovider.Visibility = Visibility.Visible;
                dgprovider.DataContext = s.DefaultView;
            }
            catch
            {
                // MessageBox.Show(ex.ToString());
            }

        }

        private void cbxbranchsr_DropDownClosed(object sender, EventArgs e)
        {

            try
            {

                branch_id = dt2.Tables[0].Rows[cbxbranchsr.SelectedIndex][0].ToString();
                branch_addrs = dt2.Tables[0].Rows[cbxbranchsr.SelectedIndex][1].ToString();
                cbxbranchsr.Text = branch_id + " - " + branch_addrs;
                DataTable s = db.RunReader(" select * from IMS_COMPLAINTS WHERE BRANCH_CODE = " + branch_id + " ORDER BY COM_DATE desc ").Result;
                //  tbcby.Visibility = tbbranch.Visibility;
                tbbranch.Visibility = Visibility.Visible;
                dgbranch.DataContext = s.DefaultView;

            }
            catch
            {
            }
        }

        private void cbxsubjectsr_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                subject_id = dt3.Tables[0].Rows[cbxsubjectsr.SelectedIndex][0].ToString();
                subject_name = dt3.Tables[0].Rows[cbxsubjectsr.SelectedIndex][1].ToString();
                cbxsubjectsr.Text = subject_name;
                DataTable s = db.RunReader(" select * from IMS_COMPLAINTS WHERE SUBJECT_CODE = " + subject_id + " ORDER BY COM_DATE desc").Result;
                tbsubject.Visibility = Visibility.Visible;
                tbcby.Visibility = Visibility.Visible;
                dgcomplaint.DataContext = s.DefaultView;

            }
            catch
            {
                // MessageBox.Show(ex.ToString());
            }
        }



        private void cbxesclatedsrsr_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                sclated_id = dt4.Tables[0].Rows[cbxesclatedsrsr.SelectedIndex][0].ToString();
                sclated_name = dt4.Tables[0].Rows[cbxesclatedsrsr.SelectedIndex][1].ToString();
                cbxesclatedsrsr.Text = sclated_id + " - " + sclated_name;

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

            if (cbxprovidersr.Text == "" || cbxbranchsr.Text == " - ")
            {
                provider_id = "NULL";
                provider_name = "NULL";
            }
            if (cbxbranchsr.Text == "" || cbxbranchsr.Text == " - ")
            {
                branch_id = "NULL";
                branch_addrs = "NULL";
            }
            if (cbxsubjectsr.Text == "" || cbxsubjectsr.Text == " - ")
            {
                subject_id = "NULL";
                subject_name = "NULL";
            }
            if (cbxsolvedbysr.Text == "" || cbxsolvedbysr.Text == " - ")
            {
                solved_id = "NULL";
                solved_name = "NULL";
            }
            if (cbxesclatedsrsr.Text == "" || cbxesclatedsrsr.Text == " - ")
            {
                sclated_id = "NULL";
                sclated_name = "NULL";
            }


        }
        private void Button_Click_3(object sender, System.Windows.RoutedEventArgs e)
        {

            DateTime datet = dtpcom.SelectedDate.Value.Date;
            string comDate = datet.ToString("dd-MMM-yyyy");

            int n = int.Parse(datet.ToString("ddMMyyyy"));
            string s = n.ToString() + txtCoId.Text;

            checkfun();


            db.RunNonQuery("INSERT INTO IMS_COMPLAINTS (COMPLAINT_ID, COM_SER, BRANCH_CODE, PROVIDER_CODE, SUBJECT_CODE, ESCLATED_TO, PROPLEM,  COM_DATE, COM_REPLAY, SOLVED_BY, CREATED_BY, UPDATED_BY, COM_CHECKED, PROVIDER_NAME, BRANCH_NAME ,COMM_ATTACH) VALUES   ('"
                                                       + s + "'," + txtCoId.Text + "," + branch_id + "," + provider_id + "," + subject_id + "," + sclated_id + ",'" + txtproblem.Text + "','" + comDate + "','" + txtreplay.Text + "'," + solved_id + "," + txtcreatedby.Text + "," + txtupdatedby.Text + "," + check + ",'" + provider_name + "','" + branch_addrs + "','" + path + "')", "تم الحفظ بنجاح");
            txtCoId_Copy.Text = s;
            cleardata();
        }
        public string path = "";

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // txtsearc.Text = "";
        }
        bool f = false;
        private void txtsearc_MouseEnter(object sender, MouseEventArgs e)
        {
            if (f == false)
            {
                txtsearchsr.Text = "";
                f = true;
            }

        }
        void searchfill()
        {
            btnedite.Visibility = Visibility.Visible;
            // cleardata();
            try
            {
                DataTable s = db.RunReader(" select * from IMS_COMPLAINTS WHERE COMPLAINT_ID = '" + txtsearchsr.Text + "'").Result;
                if (s.Rows.Count > 0)
                {
                    txtCoId_Copy.Text = s.Rows[0][0].ToString();
                    txtCoId.Text = s.Rows[0][1].ToString();
                    branch_id = s.Rows[0][2].ToString(); branch_addrs = s.Rows[0][14].ToString();
                    cbxbranchsr.Text = branch_id + " - " + branch_addrs;
                    provider_id = s.Rows[0][3].ToString(); provider_name = s.Rows[0][13].ToString();
                    cbxprovidersr.Text = provider_id + " - " + provider_name;
                    subject_id = s.Rows[0][4].ToString();
                    DataTable tem = db.RunReader(" select SUBJECT_NAME from IMS_COM_SUBJECT WHERE SUBJECT_CODE = " + subject_id).Result;
                    subject_name = tem.Rows[0][0].ToString();
                    cbxsubjectsr.Text = subject_id + " - " + subject_name;
                    sclated_id = s.Rows[0][5].ToString();
                    tem = db.RunReader(" select MEMBER_NAME from IMS_ESCLATION_MEMBER WHERE MEMBER_ID = " + sclated_id).Result;
                    sclated_name = tem.Rows[0][0].ToString();
                    cbxesclatedsrsr.Text = sclated_id + " - " + sclated_name;
                    txtproblem.Text = s.Rows[0][6].ToString();
                    //imgPhoto.GetValue( s.Rows[0][7].ToString(); )
                    dtpcom.Text = s.Rows[0][7].ToString();
                    txtreplay.Text = s.Rows[0][8].ToString();
                    solved_id = s.Rows[0][10].ToString();
                    tem = db.RunReader(" select MEMBER_NAME from IMS_ESCLATION_MEMBER WHERE MEMBER_ID = " + solved_id).Result;
                    solved_name = tem.Rows[0][0].ToString(); ;
                    cbxsolvedbysr.Text = solved_id + " - " + solved_name;
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
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            searchfill();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            RotateTransform rotateTransform = new RotateTransform(350);
            imgsearchsr.RenderTransform = rotateTransform;

        }

        private void Button_Click_4(object sender, System.Windows.RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            //    path = Path.GetFlowDirection(op);
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                path = op.FileName;
            }

        }

        private void cbxsolvedbysr_DropDownClosed(object sender, EventArgs e)
        {
            try
            {

                solved_id = dt4.Tables[0].Rows[cbxsolvedbysr.SelectedIndex][0].ToString();
                solved_name = dt4.Tables[0].Rows[cbxsolvedbysr.SelectedIndex][1].ToString();
                cbxsolvedbysr.Text = solved_id + " - " + solved_name;

            }
            catch
            {// MessageBox.Show(ex.ToString()); 
            }
        }

        private void txtsearchsr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchfill();
            }
        }

        private void imgsearchsr_MouseLeave(object sender, MouseEventArgs e)
        {
            RotateTransform rotateTransform = new RotateTransform(0);
            imgsearchsr.RenderTransform = rotateTransform;
        }

        private void btnedite_Click(object sender, RoutedEventArgs e)
        {
            btnedite.Visibility = Visibility.Hidden;
            DateTime datet = dtpcom.SelectedDate.Value.Date;
            string comDate = datet.ToString("dd-MMM-yyyy");
            //  checkfun();
            db.RunNonQuery("UPDATE IMS_COMPLAINTS SET BRANCH_CODE ='" + branch_id + "', PROVIDER_CODE = '" + provider_id + "', SUBJECT_CODE = '" + subject_id + "', ESCLATED_TO = '" + sclated_id + "', PROPLEM = '" + txtproblem.Text + "', COM_DATE = '" + comDate
                           + "', COM_REPLAY = '" + txtreplay.Text + "', SOLVED_BY = '" + solved_id + "', CREATED_BY = '" + txtcreatedby.Text + "', UPDATED_BY = '" + txtupdatedby.Text + "', COM_CHECKED = '" + check + "', PROVIDER_NAME = '" + provider_name + "', BRANCH_NAME = '" + branch_addrs + "',COMM_ATTACH = '" + path + "' WHERE COMPLAINT_ID = '" + txtsearchsr.Text + "'", "تم التعديل بنجاح");
            cleardata();



        }

        private void cbxsubjectsr_KeyDown(object sender, KeyEventArgs e)
        {
            cbxsubjectsr.IsDropDownOpen = true;

        }

        private void cbxesclatedsrsr_KeyDown(object sender, KeyEventArgs e)
        {
            cbxesclatedsrsr.IsDropDownOpen = true;

        }

        private void cbxsolvedbysr_KeyDown(object sender, KeyEventArgs e)
        {
            cbxsolvedbysr.IsDropDownOpen = true;


        }

        private void txtsearchsr_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbcby_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtCoId_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cbxprovider_DropDownClosed(object sender, EventArgs e)
        {

        }


        private void aaaa(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            //    path = Path.GetFlowDirection(op);
            if (op.ShowDialog() == true)
            {
                imgPhoto.Source = new BitmapImage(new Uri(op.FileName));
                path = op.FileName;
            }
        }

        private void cbxesclatedsr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxesclatedsr_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void cbxesclatedsr_KeyDown(object sender, KeyEventArgs e)
        {

        }

        int check = 0;
        private void checkBox1sr_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            check = 1;
        }

        private void dtpcom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxprovidersr_PreviewTouchDown(object sender, TouchEventArgs e)
        {

        }

        private void cbxprovidersr_TouchEnter(object sender, TouchEventArgs e)
        {
            // cbxprovidersr.AllowDrop = true;
        }

        private void cbxprovidersr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxprovidersr.AllowDrop = true;
        }

        private void cbxprovidersr_TextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void cbxprovidersr_PreviewKeyDown(object sender, KeyEventArgs e)
        {
        }

        private void txtesclated_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void cbxesclatedsrsr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbxprovidersr_KeyDown(object sender, KeyEventArgs e)
        {
            cbxprovidersr.IsDropDownOpen = true;
        }

        private void cbxbranchsr_KeyDown(object sender, KeyEventArgs e)
        {
            cbxbranchsr.IsDropDownOpen = true;
        }
    }
}
