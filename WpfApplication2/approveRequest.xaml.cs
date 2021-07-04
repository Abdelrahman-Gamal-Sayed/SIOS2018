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
    /// Interaction logic for approveRequest.xaml
    /// </summary>
    public partial class approveRequest : Window
    {
        public static string UserName = "";
        public static string UserCompany = "";
        public approveRequest(string user,string company)
        {
            InitializeComponent();
            UserName = user;
            UserCompany = company;
        }
        hrClass hr = new hrClass();
        private void approve_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addrb_Checked(object sender, RoutedEventArgs e)
        {
            empGrid.ItemsSource = hr.get_add_request().DefaultView;
            try
            {
                
                empGrid.Columns[1].Header = "First Name";
                empGrid.Columns[2].Header = "Second Name";
                empGrid.Columns[3].Header = "Third Name";
                empGrid.Columns[4].Header = "Forth Name";
                empGrid.Columns[5].Header = "English Name";
                empGrid.Columns[6].Header = "الاسم الاول";
                empGrid.Columns[7].Header = "الاسم الثاني";
                empGrid.Columns[8].Header = "الاسم الثالث";
                empGrid.Columns[9].Header = "الاسم الرابع";
                empGrid.Columns[10].Header = "الاسم";
                empGrid.Columns[11].Header = "الرقم القومي";
                empGrid.Columns[12].Header = "تاريخ الميلاد";
                empGrid.Columns[13].Header = "النوع";
                empGrid.Columns[14].Header = "رقم الموبايل";
                empGrid.Columns[15].Header = "الايميل ";
                empGrid.Columns[16].Header = "تاريخ البداية";
                empGrid.Columns[17].Header = "العلاقة";
                empGrid.Columns[18].Header = "العنوان";
                empGrid.Columns[19].Header = "الفرع";
                empGrid.Columns[20].Header = "كود الموظف";
                empGrid.Columns[21].Header = "نوع التسجيل";
                empGrid.Columns[22].Header = "Created by";
                empGrid.Columns[23].Header = "Created Date";
                empGrid.Columns[24].Header = "نوع الطلب";
            }
            catch { }
        }

        private void deleterb_Checked(object sender, RoutedEventArgs e)
        {
            empGrid.ItemsSource = hr.get_delete_request().DefaultView;
            try
            {
                
                empGrid.Columns[1].Header = "كود الموظف";
                empGrid.Columns[2].Header = "نوع التسجيل";
                empGrid.Columns[3].Header = "Created by";
                empGrid.Columns[4].Header = "Created date";
                empGrid.Columns[5].Header = "تاريخ تسليم الكارت";
                empGrid.Columns[6].Header = "تم تسليم الكارت ؟";
                empGrid.Columns[7].Header = "تاريخ الانتهاء";
                empGrid.Columns[8].Header = "نوع الطلب";
            }
            catch { }
        }

        private void editrb_Checked(object sender, RoutedEventArgs e)
        {
            empGrid.ItemsSource = hr.get_edit_request().DefaultView;
            try
            {

                empGrid.Columns[1].Header = "رقم الكارت";
                empGrid.Columns[2].Header = "نوع التسجيل";
                empGrid.Columns[3].Header = "Created by";
                empGrid.Columns[4].Header = "Created date";
                empGrid.Columns[5].Header = "الفئة الجديدة";
                empGrid.Columns[6].Header = "سبب تغيير الفئة";
                empGrid.Columns[7].Header = "نوع الطلب";
            }
            catch { }
        }

        private void reprintrb_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
