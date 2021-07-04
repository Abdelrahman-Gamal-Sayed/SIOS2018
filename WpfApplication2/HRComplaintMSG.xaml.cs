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
    /// Interaction logic for HRComplaintMSG.xaml
    /// </summary>
    public partial class HRComplaintMSG : Window
    {
        public HRComplaintMSG(string codeEl4akwa)
        {
            InitializeComponent();
            DB db = new DB();
            try
            {

               Task< System.Data.DataTable> x = db.RunReader(" select * from COMPANIES_HR WHERE CODE = '" + codeEl4akwa + "'");

                System.Data.DataTable s = x.Result;
                if (s.Rows.Count > 0)
                {


                    probLCode.Content= s.Rows[0][0].ToString();
                    probcbxDepartment.Text = s.Rows[0][1].ToString();
                    probcbxEmployee.Text = s.Rows[0][2].ToString();
                    probdpTime.Text = s.Rows[0][3].ToString();
                    probcbxReason.Text = s.Rows[0][4].ToString();
                    probtxtdescribtion.Text = s.Rows[0][5].ToString();
                    probtxtdtime.Text = s.Rows[0][12].ToString();

             


                    if (s.Rows[0][11].ToString() == "")
                        probLReplay.Content = "لم يتم الرد";
                    else
                        probLReplay.Content = s.Rows[0][11].ToString();

                    probLSolve.Content = "حل المشكلة";

                
                }
                else
                {
                    MessageBox.Show("تحقق من الرقم"); return;

                }

                // s.Clear();
            }
            catch { }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
