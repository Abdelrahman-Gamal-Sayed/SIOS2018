
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
    class db_dms_test

    {
        public static string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=dms_test;Password=***";
        
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
                    {
                        MessageBox.Show(Message);
                        test = true;
                    }

                    return test;
                }
                catch (Exception ex)
                {
                    string mess = ex.Message.Split(':')[0];

                    if (mess == "ORA-00001")
                        MessageBox.Show("تم حفظ هذه العملية من قبل");
                    else
                        MessageBox.Show(ex.Message);
                    return test;
                }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
        public async Task<DataTable> RunReader(string Selectstatement)
        {
            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                DataTable tbl = new DataTable();
                try
                {
                    OracleCommand cmd = new OracleCommand();

                    cmd.Connection = conn;
                    cmd.CommandText = Selectstatement;

                    conn.Open();

                    tbl.Load(await cmd.ExecuteReaderAsync());

                    return tbl;
                }
                catch (OracleException ex)
                {
                    MessageBox.Show(ex.ToString());
                    return tbl;
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

