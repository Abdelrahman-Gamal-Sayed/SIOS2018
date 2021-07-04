using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
using System.Windows;

namespace WpfApplication2.DataLayer
{
    class medicine
    {
        public static string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
        

        public DataTable get_last_vcard(int num)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            OracleCommand cmd;
            OracleDataAdapter da;
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            string query = @"select * from(select vd.card_no ,cp.emp_aname_st,cp.emp_aname_sc,cp.emp_aname_th
                           ,cp.emp_aname_fr,cp.emp_aname_fam, vd.c_comp_id , vd.created_by , vd.created_date from v_med_card vd 
                            , COMP_EMPLOYEESS cp where vd.card_no = cp.card_id order by created_date desc) 
                            where rownum >=1 and rownum <=:num";

            cmd = new OracleCommand(query, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(":num", OracleDbType.Int32).Value = num;
            
            try
            {
                da = new OracleDataAdapter(cmd);
                da.Fill(data);
                dt = data.Tables[0];
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();                    
                }
            }
            return dt;
        }
        public DataTable get_all_medicine_group()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand cmd = new OracleCommand("select * from v_medicine_group", conn);
                DataSet dataSet = new DataSet();
                OracleDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
            return dt;
        }
        public int get_super_group_code()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            int code = 0;
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand c = new OracleCommand();
                c.CommandText = "select nvl(max(SUPER_GROUP_CODE),0)+1 from MEDICINE_SUPER_GROUP";
                c.Connection = conn;
                try
                {
                    OracleDataReader dr = c.ExecuteReader();

                    while (dr.Read())
                    {
                        code = Convert.ToInt32(dr["nvl(max(SUPER_GROUP_CODE),0)+1"].ToString());
                    }
                }
                catch (OracleException ex)
                {
                    string ss = ex.Message;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
            return code;
        }
        public void add_medicine(int super, string engName, string arabName, string type, int groupCode)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            conn.Open();
            string query = @"insert into medicine_super_group (super_group_code,ename,aname,group_code,group_type)
                        values(:super_code,:e_name,:a_name,:code,:gtype)";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query; cmd.Connection = conn;
            cmd.Parameters.Add(":super", OracleDbType.Int32).Value = super;
            cmd.Parameters.Add(":e_name", OracleDbType.Varchar2).Value = engName;
            cmd.Parameters.Add(":a_name", OracleDbType.Varchar2).Value = arabName;
            cmd.Parameters.Add(":code", OracleDbType.Varchar2).Value = groupCode;
            cmd.Parameters.Add(":gtype", OracleDbType.Varchar2).Value = type;
            try
            {
                cmd.ExecuteNonQuery();
                conn.Dispose();
                conn.Close();

                 
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }


        }
        public DataTable find_super_group(int super_code)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;
            
            if (conn.State == ConnectionState.Open)
            {
                conn.Dispose();
                conn.Close();                 
            }
            conn.Open();
            string query = @"select * from medicine_super_group where SUPER_GROUP_CODE=:num";

            cmd = new OracleCommand(query, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(":num", OracleDbType.Int32).Value = super_code;

            try
            {
                da = new OracleDataAdapter(cmd);
                da.Fill(data);
                dt = data.Tables[0];
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();                    
                }
            }
            return dt;
        }
        public DataTable find_super_group()
        {
            
                OracleConnection conn = new OracleConnection(connectionStr);
                DataTable dt = new DataTable();
            try { 
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand cmd = new OracleCommand("select distinct super_group_code , aname from medicine_super_group order by super_group_code", conn);
                DataSet dataSet = new DataSet();
                OracleDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
            return dt;
        }
        public DataTable find_super_group(string input)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;
            
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            string query = @"select distinct super_group_code , aname from medicine_super_group where SUPER_GROUP_CODE like '%'||:input||'%' or aname like '%'||:input||'%'";

            cmd = new OracleCommand(query, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(":iput", OracleDbType.Varchar2).Value = input;
            cmd.Parameters.Add(":iput", OracleDbType.Varchar2).Value = input;
            
            try
            {
                da = new OracleDataAdapter(cmd);
                da.Fill(data);
                dt = data.Tables[0];
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();                     
                }
            }
            return dt;
        }
        public void update_super_group(int supercode, string ename, string aname, string type, int groupcode)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = @"update medicine_super_group set ename=:e_name ,aname=:a_name, group_type=:grouptype where
                super_group_code=:supercode and group_code=:groupcode";
            cmd.Connection = conn;
            cmd.Parameters.Add(":e_name", OracleDbType.Varchar2).Value = ename;
            cmd.Parameters.Add(":a_name", OracleDbType.Varchar2).Value = aname;
            cmd.Parameters.Add(":grouptype", OracleDbType.Varchar2).Value = type;
            cmd.Parameters.Add(":supercode", OracleDbType.Int32).Value = supercode;
            cmd.Parameters.Add(":groupcode", OracleDbType.Int32).Value = groupcode;
            try
            {
                cmd.ExecuteNonQuery();
                conn.Dispose();
                conn.Close();

                 
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }

        }
        public void delete_super_group(int superCode, int group)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            conn.Open();
            OracleCommand cmd = new OracleCommand(@"delete from  medicine_super_group 
            where super_group_code=:code and group_code=:groupcode", conn);
            cmd.Parameters.Add(":code", OracleDbType.Int32).Value = superCode;
            cmd.Parameters.Add(":groupcode", OracleDbType.Int32).Value = group;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }
        public DataTable get_med_CodeAndName(string dateFrom,string DateTo,string code)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            OracleCommand cmd;
            OracleDataAdapter da;
            DataSet data = new DataSet();
            DataTable dt = new DataTable();

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            string query = @"select distinct review_name,date_rev,claim_no,med_code,med_name,dosage,
                            system_amt,claim_amt,prv_name,prv_branch_name from a_med_diff where 
                        (date_rev between nvl(:dateF,'01-Jan-02') and nvl(:dateTo,'01-Jan-20')) 
                    and med_code like '%'||:code||'%'";

            cmd = new OracleCommand(query, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(":dateF", OracleDbType.Varchar2).Value = dateFrom;
            cmd.Parameters.Add(":dateTo", OracleDbType.Varchar2).Value = DateTo;
            cmd.Parameters.Add(":code", OracleDbType.Varchar2).Value = code;
            
            try
            {
                da = new OracleDataAdapter(cmd);
                da.Fill(data);
                dt = data.Tables[0];
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
            return dt;
        }
    }
}
