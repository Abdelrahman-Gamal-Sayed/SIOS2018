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
    /// Interaction logic for newMedicineWindow.xaml
    /// </summary>
    public partial class newMedicineWindow : Window
    {
        public static int number=5;
        public static string card;
        public newMedicineWindow(int num)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            number = num;
        }
        medicine med = new medicine();

        public void load_data()
        {
            try
            {
                medicineGrid.ItemsSource = med.get_last_vcard(number).DefaultView;
                medicineGrid.Columns[0].Header = "رقم الكارت";
                medicineGrid.Columns[1].Header = "الاسم الاول";
                medicineGrid.Columns[2].Header = "الاسم الثاني";
                medicineGrid.Columns[3].Header = "الاسم الثالث";
                medicineGrid.Columns[4].Header = "الاسم الرابع";
                medicineGrid.Columns[5].Header = "لقب العائلة";
                medicineGrid.Columns[6].Header = "رقم الشركة";
                medicineGrid.Columns[7].Header = "created by";
                medicineGrid.Columns[8].Header = "created date";
            }
            catch { }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            load_data();
        }

        private void medicineGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object item = medicineGrid.SelectedItem;
                card = (medicineGrid.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            }
            catch { }
        }
        private void ViewDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            // somehow hay-navigate mn el window hena l page 3 w ab3t fi el constructor el card num
            
        }

    }
}
