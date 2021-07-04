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
using WpfApplication2.DataLayer;
using Microsoft.Win32;
using System.Globalization;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        public Page4()
        {
            InitializeComponent();
        }
        Contracts c = new Contracts();
        private ObservableCollection<String> Activities 
        { get; set; }
        public static string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=41.33.128.139)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
        OracleConnection conn = new OracleConnection(connectionStr);
        public int get_count()
        {
            int result = 0;
            conn.Open();
            OracleCommand c = new OracleCommand();
            c.CommandText = "select count(*) from provider_contract where path1 is not null";
            c.Connection = conn;
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToInt32(dr["count(*)"].ToString());
            }
            conn.Close();
            return result;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        //    this.Activities = new ObservableCollection<string>();
            
        //    this.Activities.Add("jfjs");
        //    this.Activities.Add("sof");
        //    cmb.ItemsSource = this.Activities;
            //int code = 304;
            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Columns.Add("col1", typeof(string));
            //dt.Columns.Add("col2", typeof(int));
            //dt.Rows.Add("aya", 1);
            //grid1.ItemsSource = dt.DefaultView;
            //List<string> providerData = new List<string>();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    for (int j = 0; j < dt.Rows[i].ItemArray.Length; j++)
            //    {
            //        providerData[i] += dt.Rows[i].ItemArray[j].ToString();
            //    }
            //}
            List<string> list=new List<string>();
            list.Add("aya");
            list.Add("au");
            for (int i = 0; i < list.Count; i++)
            {
                dt.Rows.Add(list[i]);
            }
       
            grid1.ItemsSource = dt.DefaultView;
            
        }
        public static string str;
        private void combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            
        }

        private void content1_Selected(object sender, RoutedEventArgs e)
        {
            str = (((ComboBoxItem)sender).Content).ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(str);
        }

        private void TabItem_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
             if (TAB2.IsSelected==true)
            {
                MessageBox.Show("pk");
            }
        }

        private void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dis = new DispatcherTimer();
            dis.Tick += new EventHandler(dis_tick);
            dis.Interval = new TimeSpan(0,0 , 1);
            dis.Start();
        }
        
        public int increase()
        {
            
         int y=0;
            y+= 1;
            return y;
        }
        public static int x;

          bool test = false;
        public void dis_tick(object sender, EventArgs e)
        {
             int y = get_count();
             if (test == false)
             {
                 x = y;
                 test = true;
             }
            	if (x< y)
            {
                System.Windows.Forms.NotifyIcon not = new System.Windows.Forms.NotifyIcon();
                //not.BalloonTipText = "OK";
                not.Icon = new System.Drawing.Icon("C:/Users/it/Desktop/Icons8-Windows-8-Numbers-1-Black.ico");
                not.ShowBalloonTip(5000,"OK","AYA",System.Windows.Forms.ToolTipIcon.Info);
                not.Visible = true;
                
                //MessageBox.Show("OK");
                System.Media.SystemSounds.Asterisk.Play();
                lbl.FontWeight = FontWeights.ExtraBold;
                lbl.Foreground = Brushes.Red;
                lbl.FontWeight = FontWeights.Normal;
               
            }
        }
    }
}
