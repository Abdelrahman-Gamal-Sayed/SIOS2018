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
    /// Interaction logic for DMSComplaintsMSG.xaml
    /// </summary>
    public partial class DMSComplaintsMSG : Window
    {
      
     
        public DMSComplaintsMSG(string CODE, string DEPARTMENT, string EMPLOYEE, string COMP_DATE, string REASON, string DESCRIPTION, string CREATED_BY  )
        {
            InitializeComponent();
            CODEs = CODE;
            lablllll.Content = CODE;
            lablllll2.Content = REASON;
            lablllll3.Content = DESCRIPTION;
            lablllll4.Content = DEPARTMENT;
            lablllll5.Content = EMPLOYEE;
            lablllll6.Content = CREATED_BY;


        }
        string CODEs;
        DB db = new DB();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dmsbtnSave_Click(object sender, RoutedEventArgs e)
        {
            db.RunNonQuery(@"UPDATE COMPANIES_HR SET SOLUTION ='" + dmsprobtxtsolution.Text + "', REPLAYED = 'Y'  where CODE =" + CODEs, "تم ارسال الرد");

            this.Close();
        }
    }
}
