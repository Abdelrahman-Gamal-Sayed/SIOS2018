using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Windows;

namespace WpfApplication2.DataLayer
{
    class HRNetwork
    {
        public static string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
       

        public DataTable get_Class_Code(int compid)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Open();
                }
                else if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                OracleCommand cmd;
                OracleDataAdapter da;
                DataSet data = new DataSet();

                string query = @"select distinct vc.class_code , vcn.class_ename from V_P_COMP_CONTRACT_CLASS vc , v_class_name vcn
                                WHERE C_COMP_ID=:id and vc.class_code= vcn.class_code
                                AND CONTRACT_NO=(select max(contract_no) 
                                from v_P_COMP_CONTRACT_CLASS where c_comp_id =:id)";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":id", OracleDbType.Varchar2).Value = compid;

                da = new OracleDataAdapter(cmd);
                da.Fill(data);
                dt = data.Tables[0];

                conn.Dispose();
                conn.Close();
                return dt;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return dt; }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();
                }
            }

        }

        public int get_max_contract(int commpid)
        {
            OracleConnection conn = new OracleConnection(connectionStr);

            int contract = 0;
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    conn.Open();
                }
                else if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = @"select max(contract_no) from 
                        v_P_COMP_CONTRACT_CLASS where c_comp_id =:id";
                cmd.Connection = conn;
                cmd.Parameters.Add(":id", OracleDbType.Int32).Value = commpid;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    contract = Convert.ToInt32(dr["max(contract_no)"].ToString());
                }
                conn.Close();
                return contract;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return contract; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }

        public string get_hospital_degree(int compid, string classcode)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            string degree = "";
            //int code = Convert.ToInt32(classcode);
            string code = classcode;
            int contr = get_max_contract(compid);
           
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Open();
            }
            else if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {

                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = @"select hospital_degree from v_p_comp_contract_class where 
                        c_comp_id =:id and contract_no =:contr and class_code =:xxyyzz";
                cmd.Connection = conn;

                cmd.Parameters.Add(":id", OracleDbType.Int32).Value = compid;
                cmd.Parameters.Add(":contr", OracleDbType.Int32).Value = contr;
                cmd.Parameters.Add(":xxyyzz", OracleDbType.Varchar2).Value = code;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    degree = dr["hospital_degree"].ToString();
                }
            }
            
            catch (Exception ex) { MessageBox.Show(ex.Message);  }
            finally
            {
                
                    if (conn.State != ConnectionState.Closed)
                {    conn.Dispose();
                    conn.Close();

                     
                }
            }
           
            return degree;
        }
    }
}
