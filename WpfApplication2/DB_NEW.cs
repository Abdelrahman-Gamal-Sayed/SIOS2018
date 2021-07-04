using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Windows;
namespace WpfApplication2
{
    class DB_NEW

    {
        public static string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
        
        //connection
        //OracleConnection conn = new OracleConnection(connectionStr);
        //queries
        //public OracleCommand cmd = new OracleCommand();

        //public void SetCommand(string SQLStatement)
        //{

        //    // cmd = new OracleCommand();
        //    cmd.Connection = conn;
        //    cmd.CommandText = SQLStatement;
        //}
        public bool RunNonQuery(string SQLStatement, string Message = "")
        {
            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                bool test = false;
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = SQLStatement;

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    if (Message != "")
                        MessageBox.Show(Message);

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
    }
}
