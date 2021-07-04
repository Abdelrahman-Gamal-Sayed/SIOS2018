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
    /// Interaction logic for Summary_Window.xaml
    /// </summary>
    public partial class Summary_Window : Window
    {//joba15-2
        public Summary_Window(string name, string dept, string date, string dur, string card, string clientname, int agent_code, string not)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            CustomerName.Content = clientname;
            customerCardtxt.Content = card;
            agentcodetxt.Content = agent_code.ToString();
            agentName.Content = name;
            deptTxt.Content = dept;
            dateOfCalltxt.Content = date;
            CallDurtxt.Content = dur;
            labelnot_call.Content = not;
        }


        //joba15-2
        private void deleteEmpSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
