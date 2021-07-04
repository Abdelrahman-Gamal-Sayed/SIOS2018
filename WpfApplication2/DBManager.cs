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
    class DBManager
    {


        public static string con2 = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";

        //public OracleConnection conn = new OracleConnection(con2);
        //public OracleCommand com2;
        //public OracleDataAdapter adap2;
        //public DataSet res2;

        public DataSet ExecuteSelect2(string query)
        {
            using (OracleConnection conn = new OracleConnection(con2))
            {
                OracleCommand com2 = new OracleCommand(query, conn);
                OracleDataAdapter adap2 = new OracleDataAdapter(com2);
                DataSet res2 = new DataSet();

                try
                {
                    adap2.Fill(res2);
                    return res2;
                }
                catch (OracleException ex)
                {                    
                    MessageBox.Show(ex.Message);
                    return null;
                }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
        public DataSet ExecuteSelect(string query)
        {
            using (OracleConnection conn = new OracleConnection(con2))
            {
                OracleCommand com2 = new OracleCommand(query, conn);
                OracleDataAdapter adap2 = new OracleDataAdapter(com2);
                DataSet res2 = new DataSet();

                try
                {
                    adap2.Fill(res2);
                    return res2;
                }
                catch
                {
                    return null;
                }

                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
        public int ExecuteNonQuery(string query)
        {
            using (OracleConnection conn = new OracleConnection(con2))
            {
                try
                {
                    OracleCommand com2 = new OracleCommand(query, conn);
                    conn.Open();
                    int affected = com2.ExecuteNonQuery();
                    return affected;
                }
                catch
                {
                    return -1;

                }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
        public object ExecutSelectMax(string query)
        {
            using (OracleConnection conn = new OracleConnection(con2))
            {
                try
                {
                    OracleCommand com2 = new OracleCommand(query, conn);
                    conn.Open();
                    object affected = com2.ExecuteScalar();
                    return affected;
                }
                catch
                {
                    return null;
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
