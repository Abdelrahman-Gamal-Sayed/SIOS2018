using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using System.Data;
using System.Windows;

namespace WpfApplication2.DataLayer
{
  public  class Reports
    {
        public static string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
        
     

        public string get_comp_id(string compName)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            string compid = "";
            try {

              
                conn.Open();
                OracleCommand cmd = new OracleCommand(@"select c_comp_id from v_companies where c_ename=:comp or c_aname=:comp", conn);
                cmd.Parameters.Add(":comp", OracleDbType.Varchar2).Value = compName;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    compid = (dr["c_comp_id"].ToString());
                }
                conn.Dispose();
                conn.Close();

                 
                return compid;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);  return compid; }
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
