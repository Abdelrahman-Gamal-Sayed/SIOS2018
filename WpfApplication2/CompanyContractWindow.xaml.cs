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
    /// Interaction logic for CompanyContractWindow.xaml
    /// </summary>
    public partial class CompanyContractWindow : Window
    {
        Contracts contract = new Contracts();
        public static int number;
        public CompanyContractWindow(int num)
        {
            InitializeComponent();
            number = num;
        }
        public void load_data()
        {
            companyCotract.ItemsSource = contract.get_last_company_contract(number).DefaultView;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            load_data();
        }
    }
}
