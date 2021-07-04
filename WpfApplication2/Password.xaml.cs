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

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Password.xaml
    /// </summary>
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        Page1 ss;
        public Password( Page1 s)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            ss = s;
        }


        private void tbPasswordConf_KeyUp(object sender, KeyEventArgs e)
        {
            if(tbPasswordConf.Password== tbPassword.Password)
            {
                ri.Visibility = Visibility.Visible;
                r.Visibility = Visibility.Hidden;

            }
            else
            {
                ri.Visibility = Visibility.Hidden;
                r.Visibility = Visibility.Visible;
            }
        }

        private void tbPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbPasswordConf.Password.ToString()!="")
            {
                if (tbPasswordConf.Password == tbPassword.Password)
                {
                    ri.Visibility = Visibility.Visible;
                    r.Visibility = Visibility.Hidden;

                }
                else
                {
                    ri.Visibility = Visibility.Hidden;
                    r.Visibility = Visibility.Visible;
                }
            }
        }
        DB db = new DB();
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (tbPasswordConf.Password == tbPassword.Password && tbPasswordConf.Password.ToString()!="")
            {
                db.RunNonQuery("UPDATE AGENT SET CONFIRMED = 'Y' ,PASS='"+ tbPasswordConf.Password.ToString() + "' where CODE='"+User.Code+"'");
                MessageBox.Show("Set Password Done !");
                ss.Visibility = Visibility.Visible;
               
                this.Close();
               
                
            }
            else
            {
                MessageBox.Show("تاكد من الرقم السرى");
            }

        }
    }
}
