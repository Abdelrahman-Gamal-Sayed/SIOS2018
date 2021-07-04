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
using WpfApplication2.DataLayer;
namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for ContractWindow.xaml
    /// </summary>
    public partial class ContractWindow : Window
    {
        public static int number;
        public ContractWindow(int num)
        {
            InitializeComponent();
            number = num;
        }
        Contracts contract = new Contracts();
        public void load_data()
        {
            contractGrid.ItemsSource = contract.get_last_provider_contract(number).DefaultView;
            try
            {
                //contractGrid.Columns[0].Header = "كود مقدم الخدمة";
                //contractGrid.Columns[1].Header = "اسم مقدم الخدمة";
                //contractGrid.Columns[2].Header = "provider name";
                //contractGrid.Columns[3].Header = "نوع القعد";
                //contractGrid.Columns[4].Header = "نوع نسخة العقد";
            }
            catch { }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            load_data();
        }
    }
}
