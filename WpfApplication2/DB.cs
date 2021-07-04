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
    class DB

    {
        string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
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
            OracleConnection conn = new OracleConnection(connectionStr);
            //using (OracleConnection conn = new OracleConnection(connectionStr))
            //{
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
                    MessageBox.Show(ex.Message);                    
                    return test;
                }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            //}
        }
        public bool RunNonQuery2(string SQLStatement, string Message = "")
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            //using (OracleConnection conn = new OracleConnection(connectionStr))
            //{
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
                catch
                {
                    return test;
                }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            //}
        }
        public async Task<DataTable> RunReader(string Selectstatement)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            //using (OracleConnection conn = new OracleConnection(connectionStr))
            //{                
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
            //}
        }
        public DataSet RunReaderds(string Selectstatement)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            //using (OracleConnection conn = new OracleConnection(connectionStr))
            //{                
                DataSet dts = new DataSet();
                try
                {
                    OracleCommand cmd = new OracleCommand();

                    cmd.Connection = conn;
                    cmd.CommandText = Selectstatement;

                    conn.Open();
                                       
                    OracleDataAdapter a = new OracleDataAdapter(Selectstatement, conn);

                    a.Fill(dts);                    
                    return dts;
                }
                catch (OracleException ex)
                {
                    MessageBox.Show(ex.ToString());                    
                    return dts;
                }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            //}
        }
    }
}
