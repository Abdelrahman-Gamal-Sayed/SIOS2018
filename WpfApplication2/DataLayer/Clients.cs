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
    class Clients
    {
        public static string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
        //  OracleConnection conn = new OracleConnection(connectionStr);



        public static string connectionStr2 = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";

        //OracleConnection conn2 = new OracleConnection(connectionStr2);

        public void add_client_call(string client_id, string client_name, int code, string agent, string dept, string date, string time, string notcall)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            conn.Open();
            string query = @"insert into client_call (CLIENT_ID,client_ename , agent_id ,agent_ename, AGENT_DEPT , DATE_OF_CALL , DURATION_of_call,NOT_CALL) 
                                values(:clientid ,:clientname, :agentid, :agentname, :dept ,:dateofcall ,:durationcall,:notcall)";
           // OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(":clientid", OracleDbType.Varchar2).Value = client_id;
            cmd.Parameters.Add(":clientname", OracleDbType.Varchar2).Value = client_name;
            cmd.Parameters.Add(":agentid", OracleDbType.Int32).Value = code;
            cmd.Parameters.Add(":agentname", OracleDbType.Varchar2).Value = agent;
            cmd.Parameters.Add(":dept", OracleDbType.Varchar2).Value = dept;
            cmd.Parameters.Add(":dateofcall", OracleDbType.Varchar2).Value = date;
            cmd.Parameters.Add(":durationcall", OracleDbType.Varchar2).Value = time;
            cmd.Parameters.Add(":notcall", OracleDbType.Varchar2).Value = notcall;
            cmd.ExecuteNonQuery();
            conn.Close();

        }
        
        public int get_emp_code(string name)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;

            int result = 0; try
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
                OracleCommand c = new OracleCommand();
                c.CommandText = "select code from agent WHERE name=:n";
                c.Connection = conn;
                c.Parameters.Add(":n", OracleDbType.Varchar2).Value = name;
                OracleDataReader dr = c.ExecuteReader();
                while (dr.Read())
                {
                    result = Convert.ToInt32(dr["code"].ToString());
                }
                return result;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
            // return result;
        }
        ////////////////////////////////////////////////////////////
        public DataTable get_employeeData_FromID(string input)
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
                    conn.Open();
                }
                else if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }             
                
                string query = @"select vc.c_aname,cmp.card_id , cmp.emp_ename_st, cmp.emp_ename_sc
                            , cmp.emp_ename_th , cmp.emp_ename_fr , cmp.emp_ename_fam , cmp.emp_ename
                              , cmp.emp_aname_st,cmp.emp_aname_sc,cmp.emp_aname_th,cmp.emp_aname_fr
                             , cmp.emp_aname_fam ,cmp.emp_aname, cmp.email ,cmp.birth_date , cmp.address1 , cmp.address2, cmp.terminate_flag
                            , cmp.terminate_date , cmp.tel1, cmp.tel2 , cmp.INS_START_DATE, cmp.INS_END_DATE
                            from COMP_EMPLOYEESS cmp  inner join v_companies vc on cmp.c_comp_id=vc.c_comp_id
                            where  cmp.c_comp_id like:id or cmp.tel1 like:id or cmp.card_id like:id ";
                
                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":id", OracleDbType.Varchar2).Value = input;

                da = new OracleDataAdapter(cmd);
                da.Fill(data);
                dt = data.Tables[0];

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
        public DataTable get_employeeData_FromName(string input)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            DataTable dt = new DataTable();
            DataSet data = new DataSet();

            input = input.ToUpper();
            string[] name = input.Split(' ');
            string stname = name[0];
            string scname = name[1];
            string thname = name[2];
            
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
                    string query = @"select vc.c_aname,cmp.card_id , cmp.emp_ename_st, cmp.emp_ename_sc
                            , cmp.emp_ename_th , cmp.emp_ename_fr , cmp.emp_ename_fam , cmp.emp_ename
                              , cmp.emp_aname_st,cmp.emp_aname_sc,cmp.emp_aname_th,cmp.emp_aname_fr
                             , cmp.emp_aname_fam ,cmp.emp_aname, cmp.email ,cmp.birth_date , cmp.address1 , cmp.address2, cmp.terminate_flag
                            , cmp.terminate_date , cmp.tel1, cmp.tel2 , cmp.INS_START_DATE, cmp.INS_END_DATE
                            from COMP_EMPLOYEESS cmp  inner join v_companies vc on cmp.c_comp_id=vc.c_comp_id 
                            where ( upper(cmp.emp_ename_st) like:stn and upper(cmp.emp_ename_sc) like:scn and upper(cmp.emp_ename_th)like:thn ) or ( cmp.emp_aname_st like:stn and cmp.emp_aname_sc like:scn and cmp.emp_aname_th like:thn)";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();

                cmd.Parameters.Add(":stn", OracleDbType.Varchar2).Value = stname;
                cmd.Parameters.Add(":scn", OracleDbType.Varchar2).Value = scname;
                cmd.Parameters.Add(":thn", OracleDbType.Varchar2).Value = thname;

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

        /////////////////////////////// clients ( providers ,companies)/////////////////////
        public DataTable get_provider()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
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
               cmd = new OracleCommand("select typ_aname from provider_typ22 ", conn);
                DataSet dataSet = new DataSet();
                OracleDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
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
        public int get_provider_code(string prov_name)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
         //   OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            int result = 0;
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
            OracleCommand c = new OracleCommand();
            c.CommandText = "select prv_type from provider_typ22 where typ_aname =:taname";
            c.Connection = conn;
            c.Parameters.Add(":taname", OracleDbType.Varchar2).Value = prov_name;
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToInt32(dr["prv_type"].ToString());
            }
            conn.Close();
            return result;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }
        
        public int get_bsCode(string city)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
          //  OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            int result = 0;
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
                OracleCommand c = new OracleCommand();
                c.CommandText = "select bs_code from area_view where bs_aname =:ename and BS_CODE_UP is null";
                c.Connection = conn;
                c.Parameters.Add(":ename", OracleDbType.Varchar2).Value = city;
                OracleDataReader dr = c.ExecuteReader();
                while (dr.Read())
                {
                    result = Convert.ToInt32(dr["bs_code"].ToString());
                }
                conn.Close();
                return result;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }

        public string get_degree(string card)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
           // OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            string result = "";
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
            OracleCommand c = new OracleCommand();
            c.CommandText = "select prv_typ from degree_view where card_id ='" + card + "' ";
            c.Connection = conn;
            //c.Parameters.Add(":card", OracleDbType.Varchar2).Value = card;
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                result = (dr["prv_typ"].ToString());
            }
            conn.Close();
            return result;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }

        public int get_area_code(string area)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            int result = 0;
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
            OracleCommand c = new OracleCommand();
            c.CommandText = "select bs_code from area_view where bs_code_up is not null and bs_ename =:ename";
            c.Connection = conn;
            c.Parameters.Add(":ename", OracleDbType.Varchar2).Value = area;
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToInt32(dr["bs_code"].ToString());
            }
            conn.Close();
            return result;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }

        public int get_doc_code(string doc_spec)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
          //  OracleCommand cmd = new OracleCommand();
            OracleDataAdapter da;
            int result = 0;
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
            OracleCommand c = new OracleCommand();
            c.CommandText = "select bs_code from DOC_SPECIFICATION WHERE BS_ANAME=:name";
            c.Connection = conn;
            c.Parameters.Add(":name", OracleDbType.Varchar2).Value = doc_spec;
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToInt32(dr["bs_code"].ToString());
            }
            conn.Close();
            return result;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }

        public DataTable get_doctor_spec()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
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
            OracleCommand cmd = new OracleCommand("select distinct bs_aname from DOC_SPECIFICATION ", conn);
            DataSet dataSet = new DataSet();
            OracleDataReader dr = cmd.ExecuteReader();
            
            dt.Load(dr);

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


        public DataTable get_doc_by_area(int doc_code, int area_code, string degree)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();            
            DataSet data = new DataSet();
            OracleCommand cmd;
            OracleDataAdapter da;
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
            string query = @"select pr_ename ,tel1,tel1, address1,address2
                            from SERV_PROVIDERS  
                        where doc_spec=:spec and area_code=:code and prv_type=5 and 
                            prov_degree>=:deg AND (TERMINATE_FLAG='N' or TERMINATE_FLAG is NULL)";

                cmd = new OracleCommand(query, conn);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":spec", OracleDbType.Int32).Value = doc_code;
                cmd.Parameters.Add(":code", OracleDbType.Int32).Value = area_code;
                cmd.Parameters.Add(":deg", OracleDbType.Varchar2).Value = degree;

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
        //torb29-4 edit bs_ename to bs_aname in select
        public DataTable get_curr_city()
        {
            OracleConnection conn = new OracleConnection(connectionStr);

            DataTable dt = new DataTable();
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
            OracleCommand cmd = new OracleCommand("select bs_aname from area_view  where bs_code_up is null ", conn);
            DataSet dataSet = new DataSet();
            OracleDataReader dr = cmd.ExecuteReader();
           /// DataTable dt = new DataTable();
            dt.Load(dr);
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
        //torb29-4 edit bs_ename to bs_aname in select
        public DataTable get_curr_area(int bs)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            OracleCommand cmd;
            OracleDataAdapter da;
            DataTable dt = new DataTable();
            DataSet data = new DataSet();
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

                string query = "select  bs_aname  from area_view where BS_CODE_UP=:bs";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":bs", OracleDbType.Varchar2).Value = bs;

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


        public DataTable get_pharmacy(int bs, string degree)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            OracleCommand cmd;
            OracleDataAdapter da;            
            DataTable dt = new DataTable();            
            DataSet data = new DataSet();

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
                //  int deg = Convert.ToInt32(degree);
                string query = @"select pr_ename  , address1,address2,tel1,tel2 from SERV_PROVIDERS_NEW where area_code=:code and prv_type=2 and prov_degree>=:deg  ";

                cmd = new OracleCommand(query, conn);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":code", OracleDbType.Int32).Value = bs;
                cmd.Parameters.Add(":deg", OracleDbType.Varchar2).Value = degree;

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



        public DataTable get_hospital(int bs, string degree)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;
            DataSet data = new DataSet();
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
                string query = @"select pr_ename  , address1,address2,tel1,tel1 from serv_providers where area_code=:code and prv_type=1 and prov_degree>=:deg   ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":code", OracleDbType.Int32).Value = bs;
                cmd.Parameters.Add(":deg", OracleDbType.Varchar2).Value = degree;

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


        public DataTable get_lab(int bs, string degree)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;
            DataSet data = new DataSet();
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
                string query = @"select pr_ename  , address1,address2,tel1,tel1 from SERV_PROVIDERS_NEW where area_code=:code and prv_type=3 and prov_degree>=:deg   ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":code", OracleDbType.Int32).Value = bs;
                cmd.Parameters.Add(":deg", OracleDbType.Varchar2).Value = degree;

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


        public DataTable get_ray(int bs, string degree)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            DataSet data = new DataSet();
            OracleCommand cmd;
            OracleDataAdapter da;
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
                string query = @"select pr_ename  , address1,address2,tel1,tel1 from SERV_PROVIDERS_NEW where area_code=:code and prv_type=4 and prov_degree>=:deg  ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":code", OracleDbType.Int32).Value = bs;
                cmd.Parameters.Add(":deg", OracleDbType.Varchar2).Value = degree;

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


        public DataTable get_ph_therapy(int bs, string degree)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            DataSet data = new DataSet();
            OracleCommand cmd;
            OracleDataAdapter da;

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
                string query = @"select pr_ename , address1,address2,tel1,tel1 from serv_providers where area_code=:code and prv_type=6 and prov_degree>=:deg ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":code", OracleDbType.Int32).Value = bs;
                cmd.Parameters.Add(":deg", OracleDbType.Varchar2).Value = degree;

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


        public DataTable get_dentist(int bs, string degree)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            DataSet data = new DataSet();
            OracleCommand cmd;
            OracleDataAdapter da;

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
                string query = @"select pr_ename , address1,address2,tel1,tel1 from serv_providers where area_code=:code and prv_type=7 and prov_degree>=:deg   ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":code", OracleDbType.Int32).Value = bs;
                cmd.Parameters.Add(":deg", OracleDbType.Varchar2).Value = degree;

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


        public DataTable get_optic(int bs, string degree)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            DataSet data = new DataSet();
            OracleCommand cmd;
            OracleDataAdapter da;

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
                string query = @"select pr_ename  , address1,address2,tel1,tel1 from serv_providers where area_code=:code and prv_type=8 and prov_degree>=:deg   ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":code", OracleDbType.Int32).Value = bs;
                cmd.Parameters.Add(":deg", OracleDbType.Varchar2).Value = degree;

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


        public DataTable get_polyclinic(int bs, string degree)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            DataSet data = new DataSet();
            OracleCommand cmd;
            OracleDataAdapter da;
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
                string query = @"select pr_ename  , address1,address2,tel1,tel1 from serv_providers where area_code=:code and prv_type=9 and prov_degree>=:deg   ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":code", OracleDbType.Int32).Value = bs;
                cmd.Parameters.Add(":deg", OracleDbType.Varchar2).Value = degree;

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

        public int validate_card_num(string card)
        {
            OracleConnection conn2 = new OracleConnection(connectionStr2);
            int result = 0;
            try
            {

                if (conn2.State == ConnectionState.Open)
                {
                    conn2.Close();
                    conn2.Open();
                }
                else if (conn2.State == ConnectionState.Closed)
                {
                    conn2.Open();
                }
                OracleCommand c = new OracleCommand();
                c.CommandText = "select count(card_ID) from COMP_EMPLOYEESS  where card_ID=:card";
                c.Connection = conn2;
                c.Parameters.Add(":card", OracleDbType.Varchar2).Value = card;
                OracleDataReader dr = c.ExecuteReader();
                while (dr.Read())
                {
                    result = Convert.ToInt32(dr["count(card_ID)"].ToString());
                }
                conn2.Close();
                return result;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
            finally
            {

                if (conn2.State != ConnectionState.Closed)
                {
                    conn2.Dispose();
                    conn2.Close();

                     
                }
            }

        }

        public int validate_card_approval(string card)
        {
            OracleConnection conn2 = new OracleConnection(connectionStr2);
            int result = 0;
            try { 
            if (conn2.State == ConnectionState.Open)
            {
                conn2.Close();
                conn2.Open();
            }
            else if (conn2.State == ConnectionState.Closed)
            {
                conn2.Open();
            }
            OracleCommand c = new OracleCommand();
            c.CommandText = "select count(card_no) from v_approval where card_no=:card";
            c.Connection = conn2;
            c.Parameters.Add(":card", OracleDbType.Varchar2).Value = card;
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToInt32(dr["count(card_no)"].ToString());
            }
            conn2.Close();
            return result;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
            finally
            {

                if (conn2.State != ConnectionState.Closed)
                {
                    conn2.Dispose();
                    conn2.Close();

                     
                }
            }
        }

        public int validate_CardInCompEmployees(string card)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            int result = 0;
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
            OracleCommand c = new OracleCommand();
            c.CommandText = "select count(card_id) from COMP_EMPLOYEESS where card_id=:card";
            c.Connection = conn;
            c.Parameters.Add(":card", OracleDbType.Varchar2).Value = card;
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToInt32(dr["count(card_id)"].ToString());
            }
            conn.Close();
            return result;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }
        //torb7-8
        public int validate_cardInPrinting(string card) // تحت  الطباعة
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            int result = 0;
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
            OracleCommand c = new OracleCommand();
            c.CommandText = "select count(custid) from PRINTINGXXXX where custid=:card";
            c.Connection = conn;
            c.Parameters.Add(":card", OracleDbType.Varchar2).Value = card;
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToInt32(dr["count(custid)"].ToString());
            }
            conn.Close();
            return result;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }
        //torb7-8
        public int validate_card_InPrintingDeliverState(string card) //تحت التسليم
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            int result = 0;
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
                OracleCommand c = new OracleCommand();
                c.CommandText = "select count(custid) from PRINTINGXXXX where custid=:card and Deliverstate='N'";
                c.Connection = conn;
                c.Parameters.Add(":card", OracleDbType.Varchar2).Value = card;
                OracleDataReader dr = c.ExecuteReader();
                while (dr.Read())
                {
                    result = Convert.ToInt32(dr["count(custid)"].ToString());
                }
                conn.Close();
                return result;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }
        //torb7-8
        public int validate_cardDelivery(string card) // تم التسليم
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            int result = 0;
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
            OracleCommand c = new OracleCommand();
            c.CommandText = "select count(custid) from PRINTINGXXXX where custid=:card and deliverstate='Y'";
            c.Connection = conn;
            c.Parameters.Add(":card", OracleDbType.Varchar2).Value = card;
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToInt32(dr["count(custid)"].ToString());
            }
            conn.Close();
            return result;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }

        public int count_approve(string card)
        {
            OracleConnection conn2 = new OracleConnection(connectionStr2);
            int result = 0;
            try { 
            if (conn2.State == ConnectionState.Open)
            {
                conn2.Close();
                conn2.Open();
            }
            else if (conn2.State == ConnectionState.Closed)
            {
                conn2.Open();
            }
            OracleCommand c = new OracleCommand();
            c.CommandText = "select count(*) from v_approval where card_no=:card";
            c.Connection = conn2;
            c.Parameters.Add(":card", OracleDbType.Varchar2).Value = card;
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToInt32(dr["count(*)"].ToString());
            }
            conn2.Close();
            return result;
            }

            catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
            finally
            {

                if (conn2.State != ConnectionState.Closed)
                {
                    conn2.Dispose();
                    conn2.Close();

                     
                }
            }
        }

    }
}
