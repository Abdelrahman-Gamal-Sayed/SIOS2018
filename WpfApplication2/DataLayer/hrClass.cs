using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
//using Oracle.DataAccess.Client;
//using Oracle.DataAccess.Types;
using System.Data.OracleClient;
using System.Windows;

namespace WpfApplication2.DataLayer
{
    public class hrClass
    {
        public static string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
       

        public void add_employee_request(string enamest, string enamesc, string enameth, string enamefr, string ename,
                            string anamest, string anamesc, string anameth, string anamefr, string aname,
                            string email, string address, string national, string birthdate, string gender, string branch, string relation,
                            string empid, string mobile, string startdate, string created_by, string zz, string zz2, string empcode, string classcode)
        {
            OracleConnection conn = new OracleConnection(connectionStr);


            try
            {


                conn.Open();
                string query = @"insert into employee_request (emp_ename_st,emp_ename_sc,emp_ename_th,emp_ename_fr,
                        emp_ename,emp_aname_st,emp_aname_sc,emp_aname_th,emp_aname_fr,emp_aname,
                        national_id,birthdate,gender,mobile,email,start_date,emp_relation,address,branch,
                        card_id,REGISTER_TYPE,type,created_by,created_date,approve_flag,GLASSES,DISEASE,EMP_CODE,EMP_CLASS)
                        values(:enamest,:enamesc,:enameth,:enamefr,:ename,:anamest,:anamesc,:anameth,:anamefr,
                            :aname,:nationalid,:bdate,:gen,:mob,:e_mail,:startdate,:relation,:addr,
                            :bra,:code,:regtype,:type , :createby,sysdate,:flag,:zz,:zz2,code_en,zz4
                            )";
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = query; cmd.Connection = conn;
                cmd.Parameters.Add(":enamest", OracleType.VarChar).Value = enamest;
                cmd.Parameters.Add(":enamesc", OracleType.VarChar).Value = enamesc;
                cmd.Parameters.Add(":enameth", OracleType.VarChar).Value = enameth;
                cmd.Parameters.Add(":enamefr", OracleType.VarChar).Value = enamefr;
                cmd.Parameters.Add(":ename", OracleType.VarChar).Value = ename;
                cmd.Parameters.Add(":anamest", OracleType.VarChar).Value = anamest;
                cmd.Parameters.Add(":anamesc", OracleType.VarChar).Value = anamesc;
                cmd.Parameters.Add(":anameth", OracleType.VarChar).Value = anameth;
                cmd.Parameters.Add(":anamefr", OracleType.VarChar).Value = anamefr;
                cmd.Parameters.Add(":aname", OracleType.VarChar).Value = aname;
                cmd.Parameters.Add(":nationalid", OracleType.VarChar).Value = national;
                cmd.Parameters.Add(":bdate", OracleType.VarChar).Value = birthdate;
                cmd.Parameters.Add(":gen", OracleType.VarChar).Value = gender;
                cmd.Parameters.Add(":mob", OracleType.VarChar).Value = mobile;
                cmd.Parameters.Add(":e_mail", OracleType.VarChar).Value = email;
                cmd.Parameters.Add(":startdate", OracleType.VarChar).Value = startdate;
                cmd.Parameters.Add(":relation", OracleType.VarChar).Value = relation;
                cmd.Parameters.Add(":addr", OracleType.VarChar).Value = address;
                cmd.Parameters.Add(":bra", OracleType.VarChar).Value = branch;
                cmd.Parameters.Add(":code", OracleType.VarChar).Value = empid;
                cmd.Parameters.Add(":regtype", OracleType.VarChar).Value = "P";
                cmd.Parameters.Add(":type", OracleType.Number).Value = 1;
                cmd.Parameters.Add(":createby", OracleType.VarChar).Value = created_by;
                cmd.Parameters.Add(":flag", OracleType.VarChar).Value = "n";
                cmd.Parameters.Add(":zz2", OracleType.VarChar).Value = zz;
                cmd.Parameters.Add(":zz", OracleType.VarChar).Value = zz2;
                cmd.Parameters.Add(":code_en", OracleType.VarChar).Value = empcode;
                cmd.Parameters.Add(":zz4", OracleType.VarChar).Value = classcode;
                try
                {
                    cmd.ExecuteNonQuery();
                    conn.Dispose();
                    conn.Close();

                     
                }
                catch (Exception ex) { MessageBox.Show(ex.Message);  }
                finally
                {

                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Dispose();
                        conn.Close();

                         
                    }
                }
            }
            catch { }
        }
        public DataTable get_branch(int compid)
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
            string query = @"select a_name from v_companies_cc where c_comp_id=:num";

            cmd = new OracleCommand(query, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(":num", OracleType.Number).Value = compid;
                        
            try
            {
                da = new OracleDataAdapter(cmd);
                da.Fill(data);
                dt = data.Tables[0];

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
        public void terminate_employee_request(string empid, string term_date, string receive_flag, string receive_date, string createdby)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            conn.Open();
            string query = @"insert into employee_request (card_id,REGISTER_TYPE,type,created_by,created_date,DELIVER_CARD_DATE
                                ,DELIVER_CARD_FLAG,terminate_date,approve_flag)
                        values(:code,:regtype,:type , :createby,sysdate,:deliver_date,:deliver_flag,:term_date,:flag
                            )";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query; cmd.Connection = conn;
            cmd.Parameters.Add(":code", OracleType.VarChar).Value = empid;
            cmd.Parameters.Add(":regtype", OracleType.VarChar).Value = "P";
            cmd.Parameters.Add(":type", OracleType.Number).Value = 3;
            cmd.Parameters.Add(":createby", OracleType.VarChar).Value = createdby;
            cmd.Parameters.Add(":deliver_date", OracleType.VarChar).Value = receive_date;
            cmd.Parameters.Add(":deliver_flag", OracleType.VarChar).Value = receive_flag;
            cmd.Parameters.Add(":term_date", OracleType.VarChar).Value = term_date;
            cmd.Parameters.Add(":flag", OracleType.VarChar).Value = "n";
            try
            {
                cmd.ExecuteNonQuery();
                conn.Dispose();
                conn.Close();

                 

            }
            catch (Exception ex) { MessageBox.Show(ex.Message);  }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }
        public void edit_class_request(string card_id, string newclass, string reason, string createdby, int compid, DateTime changedate)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            if (conn.State == ConnectionState.Open)
            {
                conn.Dispose();
                conn.Close();

                 
            }
            conn.Open();


            string query = @"insert into employee_request (card_id,REGISTER_TYPE,type,created_by,created_date,emp_class,emp_class_reason,approve_flag,COMP_ID,DATE_CHANGE_TYP)
                        values(:code,:regtype,:type , :createby,sysdate,:class,:reason,:flag,:compid,:changedate
                            )";

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query; cmd.Connection = conn;
            cmd.Parameters.Add(":code", OracleType.VarChar).Value = card_id;
            //cmd.Parameters.Add(":compid", compid);

            cmd.Parameters.Add(":compid", OracleType.Number).Value = compid;
            cmd.Parameters.Add(":regtype", OracleType.VarChar).Value = "P";
            cmd.Parameters.Add(":type", OracleType.Number).Value = 2;
            cmd.Parameters.Add(":createby", OracleType.VarChar).Value = createdby;
            cmd.Parameters.Add(":class", OracleType.VarChar).Value = newclass;
            cmd.Parameters.Add(":reason", OracleType.VarChar).Value = reason;
            //torb7-3 8ayrt value approve flag (W)
            cmd.Parameters.Add(":flag", OracleType.VarChar).Value = "N";
            cmd.Parameters.Add(":changedate", OracleType.DateTime).Value = changedate;
            try
            {
                cmd.ExecuteNonQuery();
                conn.Dispose();
                conn.Close();

                 
            }
            catch (Exception ex) { MessageBox.Show(ex.Message);  }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }

        }
        public DataTable get_add_request()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand cmd = new OracleCommand(@"select emp_ename_st,emp_ename_sc,emp_ename_th,emp_ename_fr,
                        emp_ename,emp_aname_st,emp_aname_sc,emp_aname_th,emp_aname_fr,emp_aname,
                        national_id,birthdate,gender,mobile,email,start_date,emp_relation,address,branch,
                        emp_code,REGISTER_TYPE,created_by,created_date , rt.type_name
                    from employee_request re , employee_request_type rt where type=1 and rt.type_id=1 and approve_flag='n' ", conn);
                DataSet dataSet = new DataSet();
                OracleDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Dispose();
                conn.Close();

                 
                return dt; }
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
        public DataTable get_edit_request()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand cmd = new OracleCommand(@"select card_id,REGISTER_TYPE,created_by,created_date
                        ,emp_class,emp_class_reason  , rt.type_name
                    from employee_request re , employee_request_type rt where type=2 and rt.type_id=2 and approve_flag='n' ", conn);
                DataSet dataSet = new DataSet();
                OracleDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Dispose();
                conn.Close();

                 
                return dt; }
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
        public DataTable get_delete_request()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand cmd = new OracleCommand(@"select emp_code,REGISTER_TYPE,created_by,
                                created_date,DELIVER_CARD_DATE
                                ,DELIVER_CARD_FLAG,terminate_date , rt.type_name
                    from employee_request re , employee_request_type rt where type=3 and rt.type_id=3 and approve_flag='n' ", conn);
                DataSet dataSet = new DataSet();
                OracleDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                conn.Dispose();
                conn.Close();

                 
                return dt; }
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
        public DataTable get_class_name(int compid)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            OracleCommand cmd;
            OracleDataAdapter da;
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = @"select CLASS_ENAME ,CLASS_CODE from V_CLASS_NAME 
                        where CLASS_CODE in (select distinct CLASS_CODE
                        from dms_test.COMP_CONTRACT_CLASS where C_COMP_ID=:id)";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":id", OracleType.Number).Value = compid;
                               
                try
                {
                    da = new OracleDataAdapter(cmd);
                    da.Fill(data);
                    dt = data.Tables[0];
                }
                catch (OracleException ex)
                {
                    string ss = ex.Message;
                }

                

                conn.Dispose();
                conn.Close();

                
                return dt; }
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
    }
}
