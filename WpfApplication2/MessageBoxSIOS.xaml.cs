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
    /// Interaction logic for MessageBoxSIOS.xaml
    /// </summary>
    public partial class MessageBoxSIOS : Window
    {
        public  MessageBoxSIOS(string Message="",string title="")
        {
            InitializeComponent();
            messageTXT.Text = Message;
            titleTXT.Text = title;
        }
        public string Body
        {
            get { return messageTXT.Text; }
        }
        public  MessageBoxSIOS(string title = "")
        {
            InitializeComponent();
            messageTXT.IsEnabled = true;
            titleTXT.Text = title;
            


        }

        private void Image_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }
    }
}
