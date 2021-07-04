using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Windows;

namespace WpfApplication2
{
    public class DBManager2
    {
    //    public static SqlConnection con = new SqlConnection("data source=Eng_milad\\SQLEXPRESS; initial catalog=dmsDB;integrated security=true");
    //    public static SqlCommand com;
    //    public static SqlDataAdapter adap;
    //    public static DataSet res;

    //    public static int ExecuteNonQuery(string query)
    //    {
    //        try
    //        {
    //            com = new SqlCommand(query, con);
    //            con.Open();
    //            int affected = com.ExecuteNonQuery();
    //            return affected;
    //        }
    //        catch
    //        {
    //            return -1;

    //        }
    //        finally
    //        {
    //            con.Close();
    //        }
    //    }
    //    public static DataSet ExecuteSelect(string query)
    //    {
    //        com = new SqlCommand(query, con);
    //        adap = new SqlDataAdapter(com);
    //        res = new DataSet();
    //        try
    //        {

    //            adap.Fill(res);
    //            return res;
    //        }
    //        catch
    //        {
    //            return null;
    //        }

    //        finally
    //        {
    //            con.Close();
    //        }

    //    }
    //    public static object ExecutSelectMax(string query)
    //    {
    //        com = new SqlCommand(query, con);
    //        try
    //        {
    //            con.Open();
    //            object affected = com.ExecuteScalar();
    //            return affected;
    //        }
    //        catch
    //        {
    //            return null;

    //        }
    //        finally
    //        {
    //            con.Close();
    //        }
    //    }



        //=================================================================================================



        //=================================================================================================

        public static string con2 = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
        //public static OracleConnection conn = new OracleConnection(con2);
        //public static OracleCommand com2;
        //public static OracleDataAdapter adap2;
        public static DataSet res2;

        public static DataSet ExecuteSelect2(string query)
        {  OracleConnection conn = new OracleConnection(con2);
          OracleCommand com2;
          OracleDataAdapter adap2;
        com2 = new OracleCommand(query, conn);
            adap2 = new OracleDataAdapter(com2);
            res2 = new DataSet();
            try
            {
                adap2.Fill(res2);
                return res2;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return res2; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }

        }
        public static DataSet ExecuteSelect(string query)
        {
            OracleConnection conn = new OracleConnection(con2);
            OracleCommand com2;
            OracleDataAdapter adap2;
            com2 = new OracleCommand(query, conn);
            adap2 = new OracleDataAdapter(com2);
            res2 = new DataSet();
            try
            {
                adap2.Fill(res2);
                return res2;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return res2; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }

        }
        public static int ExecuteNonQuery(string query)
        {
            OracleConnection conn = new OracleConnection(con2);
            OracleCommand com2;
            OracleDataAdapter adap2;
            try
            {
                com2 = new OracleCommand(query, conn);
                conn.Open();
                int affected = com2.ExecuteNonQuery();
                return affected;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);  return 0; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }

        }

        public static object ExecutSelectMax(string query)
        {
            OracleConnection conn = new OracleConnection(con2);
            OracleCommand com2;
            OracleDataAdapter adap2;
            com2 = new OracleCommand(query, conn);
            try
            {
                conn.Open();
                object affected = com2.ExecuteScalar();
                return affected;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }

        }
    }
}




