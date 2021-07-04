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
    public class agents
    {
        public static string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";

      //  OracleConnection conn = new OracleConnection(connectionStr);
        DB db = new DB();
        public async Task<int> validate_agent(string password, string name)
        {
            return Convert.ToInt32(db.RunReader("select count(name) from agent where pass='" + password + "' and upper(name)=upper('" + name + "')").Result.Rows[0][0].ToString());
        }
        public DataTable get_employee_authority(string name)
        {
            return db.RunReader(@"select pass, approvals_inquire,cheques_inquire ,individuals_inquire,net_inquire, 
                            rep_inquire, onlinesys_inquire, medic, contract_inquire, agent_dept, manag_flag,
                            code, code_serial, basic_data, active_flag, comp_name, usertype,
                            print, notebook, store, cusst_serv, report, complain, CONFIRMED,
                            medical_management, complain_dms, hr_request, REVISE, Mail, REQUESHR, NOTI_HRREQUESTS , DELEGATE ,OPERATION,CONTRACT_MEDICAL,TELE_SALES,RE_COLLECT,AFTER_SALES,HIGH_DEPARTMENT ,COMP_ID,collect_data,COLUMN1,OPRATIONNEW,POLICYNEW,CLAIMST,FULL_CONTRACT  from agent where upper(name) =upper('" + name + "' )").Result;
        }
        public int validate_user_name(string name, string dept)
        {
            int userID = 0;
            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                try
                {
                    conn.Open();
                    OracleCommand c = new OracleCommand();
                    c.CommandText = "select count(*) from agent where name=:p";
                    c.Connection = conn;
                    c.Parameters.Add(":p", OracleDbType.Varchar2).Value = name;
                    OracleDataReader dr = c.ExecuteReader();
                    while (dr.Read())
                    {
                        userID = Convert.ToInt32(dr["count(*)"]);
                    }
                    return userID;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); return userID; }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
        public int get_serial(string deptname)//generate department code
        {
            return Convert .ToInt32(db.RunReader("select nvl(max(code_serial),0)+1 MAX from agent").Result.Rows[0][0].ToString());
        }
        public int get_dept_code(string dept_name)
        {
            int result = 0;
            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                try
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand("select dept_code from agent_department where dept_name=:n", conn);
                    cmd.Parameters.Add(":n", OracleDbType.NVarchar2).Value = dept_name;
                    OracleDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        result = Convert.ToInt32(dr["dept_code"]);
                    }
                    return result;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
             
            }
        }
        public string get_pasword(string name)
        {
            string result = "";
            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                try
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand("select pass from agent where name=:n", conn);
                    cmd.Parameters.Add(":n", OracleDbType.NVarchar2).Value = name;
                    OracleDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        result = dr["pass"].ToString();
                    }
                    return result;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); return result; }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }             
            }
        }

        public void update_user(string name, string password, string app_inq, string cheq_inq, string net_inq,
            string rep_inq, string online_inq, string medical_inq, string contract_inq, string indcheck,
            string basic, string dept, string manager, int code, string compname, string active, string usertype
            , string print, string notebook, string store, string customer, string report, string complain, string medicalmanage, string hrreq, string complainmember, string revise, string mail, string REQUESHR, string NotiHr)
        {

            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                try
                {
                    conn.Open();
                    OracleCommand c = new OracleCommand();
                    c.CommandText = @"update agent set pass=:passtxt, approvals_inquire=:appinq , net_inquire=:net , cheques_inquire=:che ,
                            medic=:med , INDIVIDUALS_INQUIRE=:ind,
                            rep_inquire=:rep , onlinesys_inquire=:online_inq, contract_inquire=:contr , agent_dept=:agdep , 
                            manag_flag=:manflag,basic_data=:bdata, active_flag=:activeF , comp_name=:cname
                     ,usertype =:utype ,print=:prints ,notebook=:notes , store=:
,
               cusst_serv=:customer , report=:reports , complain=:complains , MEDICAL_MANAGEMENT=:medical,
                complain_dms=:complaindms , hr_request=:hrreq ,revise=:rev,Mail=:mai,REQUESHR=:REQUESHR,NOTI_HRREQUESTS=:NOTI_HRREQUESTS
                    where name=:n and code=:code_";
                    c.Connection = conn;
                    c.Parameters.Add(":passtxt", OracleDbType.Varchar2).Value = password;
                    c.Parameters.Add(":appinq", OracleDbType.Varchar2).Value = app_inq;
                    c.Parameters.Add(":net", OracleDbType.Varchar2).Value = net_inq;
                    c.Parameters.Add(":che", OracleDbType.Varchar2).Value = cheq_inq;
                    c.Parameters.Add(":med", OracleDbType.Varchar2).Value = medical_inq;
                    c.Parameters.Add(":ind", OracleDbType.Varchar2).Value = indcheck;
                    c.Parameters.Add(":rep", OracleDbType.Varchar2).Value = rep_inq;
                    c.Parameters.Add(":online_inq", OracleDbType.Varchar2).Value = online_inq;
                    c.Parameters.Add(":contr", OracleDbType.Varchar2).Value = contract_inq;
                    c.Parameters.Add(":agdep", OracleDbType.Varchar2).Value = dept;
                    c.Parameters.Add(":manflag", OracleDbType.Varchar2).Value = manager;
                    c.Parameters.Add(":bdata", OracleDbType.Varchar2).Value = basic;
                    c.Parameters.Add(":activeF", OracleDbType.Varchar2).Value = active;
                    c.Parameters.Add(":cname", OracleDbType.Varchar2).Value = compname;
                    c.Parameters.Add(":utype", OracleDbType.Varchar2).Value = usertype;
                    c.Parameters.Add(":prints", OracleDbType.Varchar2).Value = print;
                    c.Parameters.Add(":notes", OracleDbType.Varchar2).Value = notebook;
                    c.Parameters.Add(":stores", OracleDbType.Varchar2).Value = store;
                    c.Parameters.Add(":customer", OracleDbType.Varchar2).Value = customer;
                    c.Parameters.Add(":reports", OracleDbType.Varchar2).Value = report;
                    c.Parameters.Add(":complains", OracleDbType.Varchar2).Value = complain;
                    c.Parameters.Add(":medical", OracleDbType.Varchar2).Value = medicalmanage;
                    c.Parameters.Add(":complaindms", OracleDbType.Varchar2).Value = complainmember;
                    c.Parameters.Add(":hrreq", OracleDbType.Varchar2).Value = hrreq;
                    c.Parameters.Add(":rev", OracleDbType.Varchar2).Value = revise;
                    c.Parameters.Add(":mai", OracleDbType.Varchar2).Value = mail;
                    c.Parameters.Add(":REQUESHR", OracleDbType.Varchar2).Value = REQUESHR;
                    c.Parameters.Add(":n", OracleDbType.Varchar2).Value = name;
                    c.Parameters.Add(":code_", OracleDbType.Int32).Value = code;
                    c.Parameters.Add(":NOTI_HRREQUESTS", OracleDbType.Varchar2).Value = NotiHr;

                    c.ExecuteNonQuery();
                }
                catch (OracleException ex)
                {
                    string str = ex.Message;
                    string sss = ex.Source;
                }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
        //torb2-5 zwdt paremters comp_id
        //torb21-5 Done
        public void add_user(string name, string password, int serial, string app_inq, string cheq_inq, string net_inq,
          string rep_inq, string online_inq, string medical_inq, string contract_inq, string indcheck,
          string basic, string dept, string manager, int code, string compname, string active, string usertype
          , string print, string notebook, string store, string customer, string report, string complain, string medicalmanage, string hrreq, string complainmember, string revisse, string mail, string requesHR, string noti_hr, string name_ar, string name_en, string operation, string contract_medical, string tele_sales, string codz, string re_collect, string after_saless, string high_departmentt, string collect_data, string mainTAb, string OPRATIONNEW, string POLICYNEW, string claimst,string FULL_CONTRACT)
        {
            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                try
                {
                    conn.Open();
                    string query = @"insert into agent (name,pass ,code_serial, APPROVALS_INQUIRE , CHEQUES_INQUIRE ,
                    INDIVIDUALS_INQUIRE , NET_INQUIRE , REP_INQUIRE , ONLINESYS_INQUIRE , MEDIC ,
                    CONTRACT_INQUIRE , AGENT_DEPT , manag_flag,code,basic_data,active_flag,
                    comp_name,usertype,print,notebook,store,cusst_serv,report,complain,    MEDICAL_MANAGEMENT,COMPLAIN_DMS,HR_REQUEST,REVISE,Mail,requesHR,NOTI_HRREQUESTS,NAME_AR,NAME_EN,OPERATION,CONTRACT_MEDICAL,TELE_SALES,COMP_ID,RE_COLLECT,AFTER_SALES,HIGH_DEPARTMENT,collect_data,COLUMN1,OPRATIONNEW,POLICYNEW,claimst,FULL_CONTRACT) 
                                values(:n,:p,:code_ser,:app_inq,:cheq_inq,:ind_inq,:net_inq,:rep_inq,:online_inq,
                            :med_inq,:contr_inq,:dept ,:manflag ,:code_agent,:bdata,:activef,:comname,
                            :user_type,:p_rint,:note_book,:s_tore,:cust,:reports,:complains,
                            :medical_manage,:complaindms,:hrreq,:rev,:mai,:requesHR,:NOTI_HRREQUEST,:name_ar,:name_en,:operation,:contract_medical,:tele_sales,:codz,:re_collect,:after_saless,:high_departmentt,:collect_data,:mainTAbss,'" + OPRATIONNEW + "','" + POLICYNEW + "','" + claimst + "','" + FULL_CONTRACT + "')";

                    OracleCommand cmd = new OracleCommand();

                    cmd.CommandText = query;
                    cmd.Connection = conn;

                    cmd.Parameters.Add(":n", OracleDbType.Varchar2).Value = name;
                    cmd.Parameters.Add(":p", OracleDbType.Varchar2).Value = password;
                    cmd.Parameters.Add(":code_ser", OracleDbType.Int32).Value = serial;
                    cmd.Parameters.Add(":app_inq", OracleDbType.Varchar2).Value = app_inq;
                    cmd.Parameters.Add(":cheq_inq", OracleDbType.Varchar2).Value = cheq_inq;
                    cmd.Parameters.Add(":ind_inq", OracleDbType.Varchar2).Value = indcheck;
                    cmd.Parameters.Add(":net_inq", OracleDbType.Varchar2).Value = net_inq;
                    cmd.Parameters.Add(":rep_inq", OracleDbType.Varchar2).Value = rep_inq;
                    cmd.Parameters.Add(":online_inq", OracleDbType.Varchar2).Value = online_inq;
                    cmd.Parameters.Add(":med_inq", OracleDbType.Varchar2).Value = medical_inq;
                    cmd.Parameters.Add(":contr_inq", OracleDbType.Varchar2).Value = contract_inq;
                    cmd.Parameters.Add(":d", OracleDbType.Varchar2).Value = dept;
                    cmd.Parameters.Add(":manflag", OracleDbType.Varchar2).Value = manager;
                    cmd.Parameters.Add(":code_agent", OracleDbType.Int32).Value = code;
                    cmd.Parameters.Add(":bdata", OracleDbType.Varchar2).Value = basic;
                    cmd.Parameters.Add(":activef", OracleDbType.Varchar2).Value = active;
                    cmd.Parameters.Add(":comname", OracleDbType.Varchar2).Value = compname;
                    cmd.Parameters.Add(":user_type", OracleDbType.Varchar2).Value = usertype;
                    cmd.Parameters.Add(":p_rint", OracleDbType.Varchar2).Value = print;
                    cmd.Parameters.Add(":note_book", OracleDbType.Varchar2).Value = notebook;
                    cmd.Parameters.Add(":s_tore", OracleDbType.Varchar2).Value = store;
                    cmd.Parameters.Add(":cust", OracleDbType.Varchar2).Value = customer;
                    cmd.Parameters.Add(":reports", OracleDbType.Varchar2).Value = report;
                    cmd.Parameters.Add(":complains", OracleDbType.Varchar2).Value = complain;
                    cmd.Parameters.Add(":medical_manage", OracleDbType.Varchar2).Value = medicalmanage;
                    cmd.Parameters.Add(":complaindms", OracleDbType.Varchar2).Value = complainmember;
                    cmd.Parameters.Add(":hrreq", OracleDbType.Varchar2).Value = hrreq;
                    cmd.Parameters.Add(":rev", OracleDbType.Varchar2).Value = revisse;
                    cmd.Parameters.Add(":mai", OracleDbType.Varchar2).Value = mail;
                    cmd.Parameters.Add(":requesHR", OracleDbType.Varchar2).Value = requesHR;
                    cmd.Parameters.Add(":NOTI_HRREQUEST", OracleDbType.Varchar2).Value = noti_hr;
                    cmd.Parameters.Add(":name_ar", OracleDbType.Varchar2).Value = name_ar;
                    cmd.Parameters.Add(":name_en", OracleDbType.Varchar2).Value = name_en;
                    cmd.Parameters.Add(":operation", OracleDbType.Varchar2).Value = operation;
                    cmd.Parameters.Add(":contract_medical", OracleDbType.Varchar2).Value = contract_medical;
                    cmd.Parameters.Add(":tele_sales", OracleDbType.Varchar2).Value = tele_sales;
                    cmd.Parameters.Add(":codz", OracleDbType.Varchar2).Value = codz;
                    cmd.Parameters.Add(":re_collect", OracleDbType.Varchar2).Value = re_collect;
                    cmd.Parameters.Add(":after_saless", OracleDbType.Varchar2).Value = after_saless;
                    cmd.Parameters.Add(":high_departmentt", OracleDbType.Varchar2).Value = high_departmentt;
                    cmd.Parameters.Add(":collect_data", OracleDbType.Varchar2).Value = collect_data;
                    cmd.Parameters.Add(":mainTAbss", OracleDbType.Varchar2).Value = mainTAb;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
        public string get_dept(string name)
        {
            string dept = "";
            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    conn.Open();
                    cmd.CommandText = "select agent_dept from agent where name=:n";
                    cmd.Connection = conn;
                    cmd.Parameters.Add(":n", OracleDbType.Varchar2).Value = name;
                    OracleDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        dept = dr["agent_dept"].ToString();
                    }
                    return dept;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); return dept; }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
        public DataTable get_all_dept()
        {
            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                DataTable dt = new DataTable();
                try
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand("select distinct dept_name from agent_department ", conn);
                    DataSet dataSet = new DataSet();
                    OracleDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                                        
                    return dt;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); return dt; }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
        public int get_emp_code(string name)
        {
            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                int code = 0;
                try
                {
                    OracleCommand cmd = new OracleCommand();
                    conn.Open();
                    cmd.CommandText = "select code from agent where name=:n";
                    cmd.Connection = conn;
                    cmd.Parameters.Add(":n", OracleDbType.Varchar2).Value = name;
                    OracleDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        code = Convert.ToInt32(dr["code"].ToString());
                    }                                        
                    return code;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); return code; }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
        public DataTable get_employees(string dept)
        {
            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                DataTable dt = new DataTable();

                try
                {
                    OracleCommand cmd;
                    OracleDataAdapter da;
                    DataSet data = new DataSet();

                    conn.Open();
                    string query = "select  name,pass,manag_flag,code from agent where AGENT_DEPT=:dept ";

                    cmd = new OracleCommand(query, conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(":dept", OracleDbType.Varchar2).Value = dept;

                    da = new OracleDataAdapter(cmd);
                    da.Fill(data);
                    dt = data.Tables[0];
                    
                    return dt;
                }
                catch (OracleException ex)
                {
                    MessageBox.Show(ex.Message);
                    return dt;
                }
                finally
                {
                    conn.Dispose();
                    conn.Close();

                }
            }            
        }
        public DataTable get_code_dept() // all departments' details
        {
            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                DataTable dt = new DataTable();
                try
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand("select  * from agent_department ", conn);
                    DataSet dataSet = new DataSet();
                    OracleDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    
                    return dt;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); return dt; }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
        public void add_dept(string name)
        {
            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                try
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand("insert  into agent_department ( dept_name) values(:dept)  ", conn);
                    cmd.Parameters.Add(":dept", OracleDbType.Varchar2).Value = name;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message);}
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
        public void update_dept(string name, int code)
        {
            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                try
                {
                    conn.Open();
                    OracleCommand c = new OracleCommand();
                    c.CommandText = @"update agent_department set dept_name=:n   where dept_code=:code";
                    c.Connection = conn;
                    c.Parameters.Add(":dept_name", OracleDbType.Varchar2).Value = name;
                    c.Parameters.Add(":code", OracleDbType.Varchar2).Value = code;
                    c.ExecuteNonQuery();
                }
                catch (OracleException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
        public void del_dept(int code)
        {
            using (OracleConnection conn = new OracleConnection(connectionStr))
            {
                try
                {
                    conn.Open();
                    OracleCommand cmd = new OracleCommand("delete from  agent_department where dept_code=:code ", conn);
                    cmd.Parameters.Add(":code", OracleDbType.Int32).Value = code;
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                finally
                {
                    conn.Dispose();
                    conn.Close();
                     
                }
            }
        }
    }
}
