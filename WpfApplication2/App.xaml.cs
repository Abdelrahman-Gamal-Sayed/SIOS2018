using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            closeSession();
            //dtm.RunNonQuery(@"UPDATE LOGINHIST SET END_LOGIN = systimestamp , ACTIVE = 'N' WHERE USERNAME = '" + User.Name + "'");
            //   MessageBox.Show("bye");
        }
        public static void closeSession()
        {
            DB dtm = new DB();
            dtm.RunNonQuery(@"UPDATE LOGINHIST SET END_LOGIN = systimestamp , ACTIVE = 'N' WHERE USERNAME = '" + User.Name + "' and active = 'Y'");
        }

    }
}
