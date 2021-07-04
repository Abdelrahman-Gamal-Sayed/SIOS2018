using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Oracle.DataAccess.Client;
using System.Data.OracleClient;
//using Oracle.DataAccess.Types;
using System.Data;
using System.Windows;
namespace WpfApplication2.DataLayer
{
    class Contracts
    {
        public static string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=********** )(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";

        public DataTable get_provider_codes()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand cmd = new OracleCommand("select  pr_code ,pr_ename,pr_aname from serv_providers ", conn);
                DataSet dataSet = new DataSet();
                OracleDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);

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
        public DataTable get_provider_codes22()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand cmd = new OracleCommand("select  pr_code ,pr_ename,pr_aname from serv_providers ", conn);
                DataSet dataSet = new DataSet();
                OracleDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);

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

        public DataTable get_company_codes()
        {

            return User.DtAllCompanys; ;
        }



        public DataTable get_selected_company_data(Int64 id)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;

            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = @"select distinct contract_no, vp.c_comp_id , c_aname , c_ename, address1,address2,tel1,tel2
                            from v_companies vc , v_p_comp_contract_class vp 
                        where vc.C_COMP_ID=vp.C_COMP_ID and vp.C_COMP_ID=:id order by contract_no ";


                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":id", OracleType.Number).Value = id;

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


        public DataTable get_selected_provider_data(int code)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = @"select pr_code , pr_ename , pr_aname, address1,address2,tel1,tel2,
                            TERMINATE_DATE,TERMINATE_FLAG,TAX_FLG,STAMP_VALUE, DEV_LOC
                            , dev_ext,FOR_MED_DIS, LOCAL_MED_DIS , prov_degree
                            from serv_providers
                        where pr_code = :param ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":param", OracleType.Number).Value = code;

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
        public DataTable get_selected_provider_data22(int code)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = @"select pr_code , pr_ename , pr_aname, address1,address2,tel1,tel2,
                            TERMINATE_DATE,TERMINATE_FLAG,TAX_FLG,STAMP_VALUE, DEV_LOC
                            , dev_ext,FOR_MED_DIS, LOCAL_MED_DIS , prov_degree
                            from serv_providers
                        where pr_code = :param ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":param", OracleType.Number).Value = code;

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

        public string get_terminate_flag(string input)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            string result = "";
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand cmd = new OracleCommand("select terminate_flag from serv_providers where pr_code=:code", conn);
                cmd.Parameters.Add(":code", OracleType.NVarChar).Value = input;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result = dr["terminate_flag"].ToString();
                }
                conn.Dispose();
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
        public string get_terminate_flag22(string input)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            string result = "";
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand cmd = new OracleCommand("select terminate_flag from serv_providers where pr_code=:code", conn);
                cmd.Parameters.Add(":code", OracleType.NVarChar).Value = input;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result = dr["terminate_flag"].ToString();
                }
                conn.Dispose();
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


        public string get_pr_ename(int input)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            string result = "";
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand cmd = new OracleCommand("select pr_ename from serv_providers where pr_code=:code", conn);
                cmd.Parameters.Add(":code", OracleType.Number).Value = input;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result = dr["pr_ename"].ToString();
                }
                conn.Dispose();
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

        public string get_pr_aname(int input)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            string result = "";
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand cmd = new OracleCommand("select pr_aname from serv_providers where pr_code=:code", conn);
                cmd.Parameters.Add(":code", OracleType.Number).Value = input;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result = dr["pr_aname"].ToString();
                }
                conn.Dispose();
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
        void open()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            if (conn.State == ConnectionState.Open)
            {
                conn.Dispose();
                conn.Close();


                conn.Open();
            }
            else if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        //torb4-3
        void close()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                conn.Dispose();
                conn.Close();


            }
            else if (conn.State == ConnectionState.Open)
            {
                conn.Dispose();
                conn.Close();


            }
        }
        //torb31-7
        public void update_contract(int code, string ename, string aname, string type, string contract_long, List<string> names, int prov_type, string prov_degree)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            try {
                //torb4-3
                open();
                //string query = @"update PROVIDER_CONTRACT set path1='"+"C:\\Users\\torbeny\\Desktop\\IMG_130560676771.jpeg"+"' where PR_CODE = 105";
                string query = @"update  PROVIDER_CONTRACT set pr_ename =:ename ,pr_aname =:aname ,contract_type=:contype,contract_long=:conlen,
            path1=:p1,path2=:p2,path3=:p3,path4=:p4,path5=:p5,path6=:p6,path7=:p7,path8=:p8,path9=:p9,path10=:p10,path11=:p11,path12=:p12,path13=:p13,path14=:p14,path15=:p15,path16=:p16,path17=:p17,path18=:p18,path19=:p19,path20=:p20,
            prv_type=:ptype,prov_degree=:pdeg,contract_date= sysdate where PR_CODE=:code and CONTRACT_TYPE=:contype and DELETE_FLAG='N'";

                OracleCommand cmd = new OracleCommand();
                //:p1,:p2,:p3,:p4,:p5,:p6,:p7 ,:p8,:p9,:p10,:p11,:p12,:p13,:p14,:p15,:p16,:p17,:p18,:p19,:p20,
                // path1,path2,path3,path4,path5,path6,path7,path8,path9,path10,path11,path12,path13,path14,path15,path16,path17,path18,path19,path20,

                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Parameters.Add(":code", OracleType.Number).Value = code;
                cmd.Parameters.Add(":ename", OracleType.VarChar).Value = ename;
                cmd.Parameters.Add(":aname", OracleType.VarChar).Value = aname;
                cmd.Parameters.Add(":contype", OracleType.VarChar).Value = type;
                cmd.Parameters.Add(":conlen", OracleType.VarChar).Value = contract_long;
                cmd.Parameters.Add(":p1", OracleType.VarChar).Value = names[0].ToString();
                cmd.Parameters.Add(":p2", OracleType.VarChar).Value = names[1].ToString();
                cmd.Parameters.Add(":p3", OracleType.VarChar).Value = names[2].ToString();
                cmd.Parameters.Add(":p4", OracleType.VarChar).Value = names[3].ToString();
                cmd.Parameters.Add(":p5", OracleType.VarChar).Value = names[4].ToString();
                cmd.Parameters.Add(":p6", OracleType.VarChar).Value = names[5].ToString();
                cmd.Parameters.Add(":p7", OracleType.VarChar).Value = names[6].ToString();
                cmd.Parameters.Add(":p8", OracleType.VarChar).Value = names[7].ToString();
                cmd.Parameters.Add(":p9", OracleType.VarChar).Value = names[8].ToString();
                cmd.Parameters.Add(":p10", OracleType.VarChar).Value = names[9].ToString();
                cmd.Parameters.Add(":p11", OracleType.VarChar).Value = names[10].ToString();
                cmd.Parameters.Add(":p12", OracleType.VarChar).Value = names[11].ToString();
                cmd.Parameters.Add(":p13", OracleType.VarChar).Value = names[12].ToString();
                cmd.Parameters.Add(":p14", OracleType.VarChar).Value = names[13].ToString();
                cmd.Parameters.Add(":p15", OracleType.VarChar).Value = names[14].ToString();
                cmd.Parameters.Add(":p16", OracleType.VarChar).Value = names[15].ToString();
                cmd.Parameters.Add(":p17", OracleType.VarChar).Value = names[16].ToString();
                cmd.Parameters.Add(":p18", OracleType.VarChar).Value = names[17].ToString();
                cmd.Parameters.Add(":p19", OracleType.VarChar).Value = names[18].ToString();
                cmd.Parameters.Add(":p20", OracleType.VarChar).Value = names[19].ToString();
                cmd.Parameters.Add(":ptype", OracleType.Number).Value = prov_type;
                cmd.Parameters.Add(":pdeg", OracleType.VarChar).Value = prov_degree;
                //torb4-3
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
        public void add_contract(int code, string ename, string aname, string type, string contract_long, List<string> names, int prov_type, string prov_degree)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            //torb4-3

            open();
            string query = @"insert into provider_contract(pr_code,pr_ename,pr_aname,contract_type,contract_long,
            path1,path2,path3,path4,path5,path6,path7,path8,path9,path10,path11,path12,path13,path14,path15,path16,path17,path18,path19,path20,
            prv_type,prov_degree,contract_date)
            values(:code,:ename,:aname,:contype,:conlen,:p1,:p2,:p3,:p4,:p5,:p6,:p7 ,:p8,:p9,:p10,:p11,:p12,:p13,:p14,:p15,:p16,:p17,:p18,:p19,:p20, :ptype,:pdeg,sysdate)";
            OracleCommand cmd = new OracleCommand();
            //:p1,:p2,:p3,:p4,:p5,:p6,:p7 ,:p8,:p9,:p10,:p11,:p12,:p13,:p14,:p15,:p16,:p17,:p18,:p19,:p20,
            // path1,path2,path3,path4,path5,path6,path7,path8,path9,path10,path11,path12,path13,path14,path15,path16,path17,path18,path19,path20,

            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(":code", OracleType.Number).Value = code;
            cmd.Parameters.Add(":ename", OracleType.VarChar).Value = ename;
            cmd.Parameters.Add(":aname", OracleType.VarChar).Value = aname;
            cmd.Parameters.Add(":contype", OracleType.VarChar).Value = type;
            cmd.Parameters.Add(":conlen", OracleType.VarChar).Value = contract_long;
            cmd.Parameters.Add(":p1", OracleType.VarChar).Value = names[0].ToString();
            cmd.Parameters.Add(":p2", OracleType.VarChar).Value = names[1].ToString();
            cmd.Parameters.Add(":p3", OracleType.VarChar).Value = names[2].ToString();
            cmd.Parameters.Add(":p4", OracleType.VarChar).Value = names[3].ToString();
            cmd.Parameters.Add(":p5", OracleType.VarChar).Value = names[4].ToString();
            cmd.Parameters.Add(":p6", OracleType.VarChar).Value = names[5].ToString();
            cmd.Parameters.Add(":p7", OracleType.VarChar).Value = names[6].ToString();
            cmd.Parameters.Add(":p8", OracleType.VarChar).Value = names[7].ToString();
            cmd.Parameters.Add(":p9", OracleType.VarChar).Value = names[8].ToString();
            cmd.Parameters.Add(":p10", OracleType.VarChar).Value = names[9].ToString();
            cmd.Parameters.Add(":p11", OracleType.VarChar).Value = names[10].ToString();
            cmd.Parameters.Add(":p12", OracleType.VarChar).Value = names[11].ToString();
            cmd.Parameters.Add(":p13", OracleType.VarChar).Value = names[12].ToString();
            cmd.Parameters.Add(":p14", OracleType.VarChar).Value = names[13].ToString();
            cmd.Parameters.Add(":p15", OracleType.VarChar).Value = names[14].ToString();
            cmd.Parameters.Add(":p16", OracleType.VarChar).Value = names[15].ToString();
            cmd.Parameters.Add(":p17", OracleType.VarChar).Value = names[16].ToString();
            cmd.Parameters.Add(":p18", OracleType.VarChar).Value = names[17].ToString();
            cmd.Parameters.Add(":p19", OracleType.VarChar).Value = names[18].ToString();
            cmd.Parameters.Add(":p20", OracleType.VarChar).Value = names[19].ToString();
            cmd.Parameters.Add(":ptype", OracleType.Number).Value = prov_type;
            cmd.Parameters.Add(":pdeg", OracleType.VarChar).Value = prov_degree;
            try
            {
                //torb4-3
                cmd.ExecuteNonQuery();
                close();
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
                MessageBox.Show(ex.Message);
            }
            //torb4-3
            finally
            {
                close();
            }
        }

        public int get_prov_type(int code)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            int result = 0;
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                try
                {

                    OracleCommand cmd = new OracleCommand(@"select pt.prv_type from PROVIDER_TYP22 pt ,serv_providers sv 
                                                    where sv.pr_code=:code and sv.prv_type=pt.prv_type", conn);
                    cmd.Parameters.Add(":code", OracleType.Number).Value = code;
                    OracleDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        result = Convert.ToInt32(dr["prv_type"].ToString());
                    }

                }
                catch (OracleException ex)
                {
                    string ss = ex.Message;
                }
                conn.Dispose();
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
        //torb31-7
        public void update_company_contract(int cont_code, int comp_id, string type, string length, string ename, string aname, List<string> names)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            conn.Open();

            string query = @"update company_contract set contract_no=:code,comp_id=:id,ename=:ename,aname=:aname,contract_long=:conlen,contract_type=:contype,
            path1=:p1,path2=:p2,path3=:p3,path4=:p4,path5=:p5,path6=:p6,path7=:p7,path8=:p8,path9=:p9,path10=:p10,path11=:p11,path12=:p12,path13=:p13,path14=:p14,path15=:p15,path16=:p16,path17=:p17,path18=:p18,path19=:p19,path20=:p20,contract_date=sysdate where comp_id=:id and CONTRACT_TYPE=:contype and DELETE_FLAG='N'";
            OracleCommand cmd = new OracleCommand();
            //:p1,:p2,:p3,:p4,:p5,:p6,:p7 ,:p8,:p9,:p10,:p11,:p12,:p13,:p14,:p15,:p16,:p17,:p18,:p19,:p20,
            // path1,path2,path3,path4,path5,path6,path7,path8,path9,path10,path11,path12,path13,path14,path15,path16,path17,path18,path19,path20,
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(":code", OracleType.Number).Value = cont_code;
            cmd.Parameters.Add(":id", OracleType.Number).Value = comp_id;
            cmd.Parameters.Add(":ename", OracleType.VarChar).Value = ename;
            cmd.Parameters.Add(":aname", OracleType.VarChar).Value = aname;
            cmd.Parameters.Add(":conlen", OracleType.VarChar).Value = length;
            cmd.Parameters.Add(":contype", OracleType.VarChar).Value = type;
            cmd.Parameters.Add(":p1", OracleType.VarChar).Value = names[0].ToString();
            cmd.Parameters.Add(":p2", OracleType.VarChar).Value = names[1].ToString();
            cmd.Parameters.Add(":p3", OracleType.VarChar).Value = names[2].ToString();
            cmd.Parameters.Add(":p4", OracleType.VarChar).Value = names[3].ToString();
            cmd.Parameters.Add(":p5", OracleType.VarChar).Value = names[4].ToString();
            cmd.Parameters.Add(":p6", OracleType.VarChar).Value = names[5].ToString();
            cmd.Parameters.Add(":p7", OracleType.VarChar).Value = names[6].ToString();
            cmd.Parameters.Add(":p8", OracleType.VarChar).Value = names[7].ToString();
            cmd.Parameters.Add(":p9", OracleType.VarChar).Value = names[8].ToString();
            cmd.Parameters.Add(":p10", OracleType.VarChar).Value = names[9].ToString();
            cmd.Parameters.Add(":p11", OracleType.VarChar).Value = names[10].ToString();
            cmd.Parameters.Add(":p12", OracleType.VarChar).Value = names[11].ToString();
            cmd.Parameters.Add(":p13", OracleType.VarChar).Value = names[12].ToString();
            cmd.Parameters.Add(":p14", OracleType.VarChar).Value = names[13].ToString();
            cmd.Parameters.Add(":p15", OracleType.VarChar).Value = names[14].ToString();
            cmd.Parameters.Add(":p16", OracleType.VarChar).Value = names[15].ToString();
            cmd.Parameters.Add(":p17", OracleType.VarChar).Value = names[16].ToString();
            cmd.Parameters.Add(":p18", OracleType.VarChar).Value = names[17].ToString();
            cmd.Parameters.Add(":p19", OracleType.VarChar).Value = names[18].ToString();
            cmd.Parameters.Add(":p20", OracleType.VarChar).Value = names[19].ToString();

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
        public void add_company_contract(int cont_code, int comp_id, string type, string length, string ename, string aname, List<string> names)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            conn.Open();
            string query = @"insert into company_contract(contract_no,comp_id,ename,aname,contract_long,contract_type,
            path1,path2,path3,path4,path5,path6,path7,path8,path9,path10,path11,path12,path13,path14,path15,path16,path17,path18,path19,path20,contract_date)
                            values(:code,:id,:ename,:aname,:conlen,:contype,:p1,:p2,:p3,:p4,:p5,:p6,:p7 ,:p8,:p9,:p10,:p11,:p12,:p13,:p14,:p15,:p16,:p17,:p18,:p19,:p20,sysdate)";
            OracleCommand cmd = new OracleCommand();
            //:p1,:p2,:p3,:p4,:p5,:p6,:p7 ,:p8,:p9,:p10,:p11,:p12,:p13,:p14,:p15,:p16,:p17,:p18,:p19,:p20,
            // path1,path2,path3,path4,path5,path6,path7,path8,path9,path10,path11,path12,path13,path14,path15,path16,path17,path18,path19,path20,
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(":code", OracleType.Number).Value = cont_code;
            cmd.Parameters.Add(":id", OracleType.Number).Value = comp_id;
            cmd.Parameters.Add(":ename", OracleType.VarChar).Value = ename;
            cmd.Parameters.Add(":aname", OracleType.VarChar).Value = aname;
            cmd.Parameters.Add(":conlen", OracleType.VarChar).Value = length;
            cmd.Parameters.Add(":contype", OracleType.VarChar).Value = type;
            cmd.Parameters.Add(":p1", OracleType.VarChar).Value = names[0].ToString();
            cmd.Parameters.Add(":p2", OracleType.VarChar).Value = names[1].ToString();
            cmd.Parameters.Add(":p3", OracleType.VarChar).Value = names[2].ToString();
            cmd.Parameters.Add(":p4", OracleType.VarChar).Value = names[3].ToString();
            cmd.Parameters.Add(":p5", OracleType.VarChar).Value = names[4].ToString();
            cmd.Parameters.Add(":p6", OracleType.VarChar).Value = names[5].ToString();
            cmd.Parameters.Add(":p7", OracleType.VarChar).Value = names[6].ToString();
            cmd.Parameters.Add(":p8", OracleType.VarChar).Value = names[7].ToString();
            cmd.Parameters.Add(":p9", OracleType.VarChar).Value = names[8].ToString();
            cmd.Parameters.Add(":p10", OracleType.VarChar).Value = names[9].ToString();
            cmd.Parameters.Add(":p11", OracleType.VarChar).Value = names[10].ToString();
            cmd.Parameters.Add(":p12", OracleType.VarChar).Value = names[11].ToString();
            cmd.Parameters.Add(":p13", OracleType.VarChar).Value = names[12].ToString();
            cmd.Parameters.Add(":p14", OracleType.VarChar).Value = names[13].ToString();
            cmd.Parameters.Add(":p15", OracleType.VarChar).Value = names[14].ToString();
            cmd.Parameters.Add(":p16", OracleType.VarChar).Value = names[15].ToString();
            cmd.Parameters.Add(":p17", OracleType.VarChar).Value = names[16].ToString();
            cmd.Parameters.Add(":p18", OracleType.VarChar).Value = names[17].ToString();
            cmd.Parameters.Add(":p19", OracleType.VarChar).Value = names[18].ToString();
            cmd.Parameters.Add(":p20", OracleType.VarChar).Value = names[19].ToString();

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


        public DataTable get_provider_by_codeOrName22(string input)
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
            string query = @"select pr_code, pr_aname ,pr_ename from serv_providers 
            where pr_code like '%'||:input||'%' or pr_aname like '%'||:input ||'%'";

            cmd = new OracleCommand(query, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(":input", OracleType.VarChar).Value = input;



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
        public DataTable get_provider_by_codeOrName(string input)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = @"select pr_code, pr_aname ,pr_ename from serv_providers 
            where pr_code like '%'||:input||'%' or pr_aname like '%'||:input ||'%'";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":input", OracleType.VarChar).Value = input;

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

        public string get_prov_degree(int pr_code)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            string result = "";
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand cmd = new OracleCommand("select prov_degree from serv_providers where pr_code=:code", conn);
                cmd.Parameters.Add(":code", OracleType.NVarChar).Value = pr_code;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result = dr["prov_degree"].ToString();
                }
                conn.Dispose();
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
        //torb31-7
        public DataTable get_provider_images(int pr_code, string contract_type, string contract_long)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = @"select  path1,path2,path3,path4,path5,path6,path7,path8,path9,
                        path10,path11,path12,path13,path14,path15,path16,path17,path18,path19,path20 
                        from provider_contract where pr_code=:id and contract_type=:contype and contract_long=:clong and DELETE_FLAG='N' ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":id", OracleType.VarChar).Value = pr_code;
                cmd.Parameters.Add(":contype", OracleType.VarChar).Value = contract_type;
                cmd.Parameters.Add(":clong", OracleType.VarChar).Value = contract_long;

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
        /// <summary>
        /// /////////////////// company summary //////////////////////////
        /// </summary>
        /// <param name="comp_code"></param>
        /// <param name="contr_code"></param>
        /// <returns></returns>
        //torb10-9  to_char(date)
        public DataTable get_company_data(int comp_code, int contr_code)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = @"select distinct c_ename,c_aname ,address1,TO_CHAR(start_cover,'DD/MM/YYYY'),TO_CHAR(end_cover,'DD/MM/YYYY') 
                            from v_companies vc , V_P_COMP_CONTRACT_CLASS vp
                            where vc.c_comp_id=:id
                            and vp.C_COMP_ID=:id and vp.contract_no=:contrno";

                cmd = new OracleCommand(query, conn);

                cmd.Parameters.Clear();
                cmd.Parameters.Add(":id", OracleType.Number).Value = comp_code;
                cmd.Parameters.Add(":contrno", OracleType.Number).Value = contr_code;

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

        public DataTable get_class_code(int comp_code, int contr_code)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = @"select DISTINCT vc.class_code , vcn.class_ename from V_P_COMP_CONTRACT_CLASS vc , v_class_name vcn
                                WHERE C_COMP_ID=:id and vc.class_code= vcn.class_code
                                AND CONTRACT_NO=:contrno";
                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":id", OracleType.Number).Value = comp_code;
                cmd.Parameters.Add(":contrno", OracleType.Number).Value = contr_code;

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

        public DataTable get_max_amount_hospital_ambulance(int comp_code, int contr_code, string class_code)
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
            string query = @"select max_amount ,hospital_degree,ambulance from V_P_COMP_CONTRACT_CLASS 
                    WHERE C_COMP_ID=:id
                    AND CONTRACT_NO=:contr_no and class_code=:code";
            cmd = new OracleCommand(query, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(":id", OracleType.Number).Value = comp_code;
            cmd.Parameters.Add(":contrno", OracleType.Number).Value = contr_code;
            cmd.Parameters.Add(":code", OracleType.VarChar).Value = class_code;

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
        //torb31-7
        public DataTable get_company_image(int comp_id, int contr_id, string contract_type, string contract_long)
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
            string query = @"select  path1,path2,path3,path4,path5,path6,path7,path8,path9,
                        path10,path11,path12,path13,path14,path15,path16,path17,path18,path19,path20 
                        from company_contract where comp_id=:id and contract_no=:cno and contract_type=:contype and contract_long=:clong and DELETE_FLAG='N' ";

            cmd = new OracleCommand(query, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(":cno", OracleType.Number).Value = contr_id;
            cmd.Parameters.Add(":contype", OracleType.VarChar).Value = contract_type;
            cmd.Parameters.Add(":clong", OracleType.VarChar).Value = contract_long;

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

        public DataTable get_serv_code(int comp_code, int contr_code, string class_code)
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
            string query = @"select  distinct serv_code, serv_aname from v_services where serv_code in(select d_serv_code from v_p_comp_customized_d 
                    WHERE C_COMP_ID=:id
                    AND CONTRACT_NO=:contr_no and class_code=:code ) order by serv_code";

            cmd = new OracleCommand(query, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(":id", OracleType.Number).Value = comp_code;
            cmd.Parameters.Add(":contrno", OracleType.Number).Value = contr_code;
            cmd.Parameters.Add(":code", OracleType.VarChar).Value = class_code;

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

        public DataTable get_ser_serv(string d_serv, int compid, int contrid, string classcode)
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
            string query = @" select distinct serv_code,serv_aname from v_services 
                    where serv_code in 
                    (select distinct ser_serv  from v_p_comp_customized_d_d vdd 
                    WHERE vdd.d_serv_code=:code and c_comp_id=:id and contract_no=:contr_no 
                        and class_code=:code_class ) order by serv_code";

            cmd = new OracleCommand(query, conn);

            cmd.Parameters.Clear();
            cmd.Parameters.Add(":code", OracleType.VarChar).Value = d_serv;
            cmd.Parameters.Add(":id", OracleType.Number).Value = compid;
            cmd.Parameters.Add(":contr_no", OracleType.Number).Value = contrid;
            cmd.Parameters.Add(":code_class", OracleType.VarChar).Value = classcode;

            try
            {
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

        public string get_d_serv_name(string d_serv)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            string result = "";
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand cmd = new OracleCommand("select distinct serv_aname  from v_services vs where vs.serv_code=:code", conn);
                cmd.Parameters.Add(":code", OracleType.Number).Value = d_serv;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result = dr["serv_aname"].ToString();
                }
                conn.Dispose();
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

        public string get_ser_servname(string ser_serv)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            string result = "";
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OracleCommand cmd = new OracleCommand("select distinct serv_aname  from v_services vs where vs.serv_code=:code", conn);
                cmd.Parameters.Add(":code", OracleType.VarChar).Value = ser_serv;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    result = dr["serv_aname"].ToString();
                }
                conn.Dispose();
                conn.Close();


                return result; }
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
        //torb11-7
        public DataTable get_service_details(int comp_code, int contr_code, string class_code, string serv_code)
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
            string query = @"select d_serv_code,ceiling_amt,ceiling_pert,carr_amt,
case 
when  refund_flag ='Y' then 'نعم' when refund_flag ='N' then 'لا'else ' '
end 
,ind_list_price
                    from v_p_comp_customized_d 
                    WHERE C_COMP_ID=:id
                    AND CONTRACT_NO=:contrno and class_code=:code and d_serv_code=:dservcode";

            cmd = new OracleCommand(query, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(":id", OracleType.Number).Value = comp_code;
            cmd.Parameters.Add(":contrno", OracleType.Number).Value = contr_code;
            cmd.Parameters.Add(":code", OracleType.VarChar).Value = class_code;
            cmd.Parameters.Add(":dservcode", OracleType.VarChar).Value = serv_code;

            try
            {
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
        //torb11-7jobaq
        public DataTable get_ser_serv_details(string ser_serv, int comp_code, int contr_code, string class_code)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = @"select d_serv_code,ceiling_amt,ceiling_pert,carr_amt,
case 
when  refund_flag ='Y' then 'نعم' when refund_flag ='N' then 'لا'else ' '
end 
,ind_list_price
                    from v_p_comp_customized_d_d
                    WHERE C_COMP_ID=:id
                    AND CONTRACT_NO=:cotrno and class_code=:code and ser_serv=:servcode";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":id", OracleType.Number).Value = comp_code;
                cmd.Parameters.Add(":contrno", OracleType.Number).Value = contr_code;
                cmd.Parameters.Add(":code", OracleType.VarChar).Value = class_code;
                cmd.Parameters.Add(":servcode", OracleType.VarChar).Value = ser_serv;


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
  

      
        public DataTable get_last_provider_contract(int num)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;
            
            try {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = @"select * from 
                            (select * from provider_contract order by contract_date desc)
                            where rownum >=1 and rownum <=:num";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":num", OracleType.Number).Value = num;
                
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

        public DataTable get_last_company_contract(int num)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                string query = @"select * from 
                            (select * from company_contract order by contract_date desc)
                            where rownum >=1 and rownum <=:num";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":num", OracleType.Number).Value = num;

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

      
    }
}
