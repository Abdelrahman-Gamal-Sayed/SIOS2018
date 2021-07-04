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
    class Stores
    {
        public static string connectionStr = @"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
                                            (HOST=**********)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)
                                            (SERVICE_NAME=ora11g)));User Id=app;Password=******";
      

        public void add_item(Int64 code, string name, Int64 category, string amount, string limit, string price) //update amount
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            conn.Open();
            string query = @"insert into items (code, item_name , item_amount, item_price ,item_limit ,item_category,created_by)
                        values (:c,:n , :amt_itm , :price ,:limit_ , :categ,'" + User.Name + "') ";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(":c", OracleDbType.Int64).Value = code;
            cmd.Parameters.Add(":n", OracleDbType.Varchar2).Value = name;
            cmd.Parameters.Add(":amt_itm", OracleDbType.Varchar2).Value = amount;
            cmd.Parameters.Add(":price", OracleDbType.Varchar2).Value = price;
            cmd.Parameters.Add(":limit_", OracleDbType.Varchar2).Value = limit;
            cmd.Parameters.Add(":categ", OracleDbType.Int64).Value = category;

            try
            {
                cmd.ExecuteNonQuery();
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
        public Int64 get_categ_code(string categ)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
         
            Int64 code = 0;
            try { 
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            else
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = "select category_id from item_category where category_name=:n and DELETE_FLAG='Y'";
                cmd.Parameters.Add(":n", OracleDbType.Varchar2).Value = categ;
                cmd.Connection = conn;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    code = Convert.ToInt64(dr["category_id"].ToString());
                }

                    conn.Dispose();
                    conn.Close();

                     

                }
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
            return code;
        }
        public int validate_item_name(string name)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            int result = 0;
            try { 
            conn.Open();
            OracleCommand c = new OracleCommand();
            c.CommandText = "select count(item_name) from items where item_name=:n ";
            c.Connection = conn;
            c.Parameters.Add(":n", OracleDbType.Varchar2).Value = name;
            OracleDataReader dr = c.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToInt32(dr["count(item_name)"]);
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
        public string get_amount(string name)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            string result = "";
            try { 
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "select item_amount from items where item_name=:n";
            cmd.Connection = conn;
            cmd.Parameters.Add(":n", OracleDbType.Varchar2).Value = name;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                result = (dr["item_amount"].ToString());
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
        public int get_limit(string name)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            int result = 0;
            try { 
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "select item_limit from items where item_name=:n";
            cmd.Connection = conn;
            cmd.Parameters.Add(":n", OracleDbType.Varchar2).Value = name;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToInt32(dr["item_limit"].ToString());
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
        public string get_category_name(int code)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            string category = "";
            try { 
            conn.Open();
            string query = "select category_name from item_category where category_id=:cat ";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(":cat", OracleDbType.Int64).Value = code;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                category = dr["category_name"].ToString();
            }

                conn.Dispose();
                conn.Close();

                 
                return category;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return category; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }
        public DataTable get_imports(string name, string nameitm)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            OracleCommand cmd;
            OracleDataAdapter da;
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            try { 
            conn.Open();
            string query = @"select employee_name_  , employee_dept ,  item.item_name, item.code ,
                    item.item_category,item.ITEM_AMOUNT, buy_date,register_date,bill_num ,tt.item_price from transaction tt , items item ,item_category cat where employee_name_=:name  and type=3 
                    and item_name_ =ITEM_NAME and cat.CATEGORY_ID=item.ITEM_CATEGORY";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                
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
        public DataTable get_item_names(string category_name)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            Int64 categ_code = get_categ_code(category_name);
            OracleCommand cmd;
            OracleDataAdapter da;
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            try { 
            conn.Open();
            string query = "select distinct item_name from items where item_category=:cat ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":cat", OracleDbType.Int64).Value = categ_code;

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
        public DataTable get_item_name_cat_amount(string item)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            OracleCommand cmd;
            OracleDataAdapter da;
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                conn.Open();
                string query = "select  code,item_name , item_amount ,item_category from items where item_name=:itm ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":itm", OracleDbType.Varchar2).Value = item;

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
        public DataTable get_item_names_from_code(string category_name)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            Int64 categ_code = get_categ_code(category_name);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;
            
            try { 
            conn.Open();
            string query = "select distinct item_name from items where item_category=:cat ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":cat", OracleDbType.Int64).Value = categ_code;


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
        public void add_exporter(string type, string name, string deptname, string empname, int amount, string ex_type, string categ)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            conn.Open();
            string query = @"insert into transaction (type ,ITEM_NAME_ ,EMPLOYEE_NAME_ , EMPLOYEE_DEPT , AMOUNT ,export_type,CATEGORY) 
                                values(:ty , :n,:empname ,:dept ,:amtitm ,:type_item,:categ)";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(":ty", OracleDbType.Varchar2).Value = type;
            cmd.Parameters.Add(":n", OracleDbType.Varchar2).Value = name;
            cmd.Parameters.Add(":empname", OracleDbType.Varchar2).Value = empname;
            cmd.Parameters.Add(":dept", OracleDbType.Varchar2).Value = deptname;
            cmd.Parameters.Add(":amtitm", OracleDbType.Varchar2).Value = amount;
            cmd.Parameters.Add(":type_item", OracleDbType.Varchar2).Value = ex_type;
            cmd.Parameters.Add(":categ", OracleDbType.Varchar2).Value = categ;

            try
            {
                cmd.ExecuteNonQuery();
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
        public void update_amount(string name, string amount)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            conn.Open();
            OracleCommand c = new OracleCommand();
            c.CommandText = @"update items set item_amount=:amountitm where item_name=:n";
            c.Connection = conn;
            c.Parameters.Add(":amitm", OracleDbType.Varchar2).Value = amount;
            c.Parameters.Add(":n", OracleDbType.Varchar2).Value = name;
            try
            {
                c.ExecuteNonQuery();
               
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
        public void Update_importer(string name, string deptname, string empname, string amount, string price_item, string date_buy, string date_register, string bill, string cat, string created_by)
        {
            string type = '3'.ToString();
            string export_type = '0'.ToString();
            OracleConnection conn = new OracleConnection(connectionStr);
            try { 
            conn.Open();
            string query = @"insert into transaction (type , employee_name_ ,employee_dept , item_NAME_ , 
                            amount  ,ITEM_PRICE, export_type,buy_date,register_date,bill_num,CATEGORY,created_by ) 
                                            values(:ty ,:empname ,:dept  , :itmname, :amt, :itemprice , :extype,:bdate,:rdate,:bnum,:cat,:created_by)";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(":ty", OracleDbType.Varchar2).Value = type;
            cmd.Parameters.Add(":empname", OracleDbType.Varchar2).Value = empname;
            cmd.Parameters.Add(":dept", OracleDbType.Varchar2).Value = deptname;
            cmd.Parameters.Add(":itmname", OracleDbType.Varchar2).Value = name;
            cmd.Parameters.Add(":amt", OracleDbType.Varchar2).Value = amount;
            cmd.Parameters.Add(":itemprice", OracleDbType.Varchar2).Value = price_item;
            cmd.Parameters.Add(":extype", OracleDbType.Varchar2).Value = export_type;
            cmd.Parameters.Add(":bdate", OracleDbType.Date).Value = date_buy;
            cmd.Parameters.Add(":rdate", OracleDbType.Date).Value = date_register;

            if (bill != string.Empty)
                cmd.Parameters.Add(":bnum", OracleDbType.Int32).Value = Convert.ToInt64(bill);
            else
                cmd.Parameters.Add(":bnum", OracleDbType.Varchar2).Value = bill;
            cmd.Parameters.Add(":cat", OracleDbType.Varchar2).Value = cat;
            cmd.Parameters.Add(":created_by", OracleDbType.Varchar2).Value = created_by;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message);
                string ss = ex.Message;
                string sj = ex.Source;
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


        }
        public DataTable get_category()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            try { 
            conn.Open();
            OracleCommand cmd = new OracleCommand("select distinct category_name , category_id from item_category where delete_flag='Y' ", conn);
            DataSet dataSet = new DataSet();
            OracleDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);

            
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
        public string get_price(string item)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            string dept = "";

            OracleCommand cmd = new OracleCommand();
            try { 
            conn.Open();
            cmd.CommandText = "select item_price from items where item_name=:n";
            cmd.Connection = conn;
            cmd.Parameters.Add(":n", OracleDbType.Varchar2).Value = item;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                dept = dr["item_price"].ToString();
            }
           
            return dept;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return dept; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }
        public void add_transaction(string type, string emp, string dept, string item, string amount, string price)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            conn.Close();
            conn.Open();
            string query = @"insert into transaction ( type , employee_name_,employee_dept , item_name_ , amount , item_price )
                            values(:type_ , :empname , :dept, :itemname , :amt , :price) ";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(":type", OracleDbType.Varchar2).Value = type;
            cmd.Parameters.Add(":empname", OracleDbType.Varchar2).Value = emp;
            cmd.Parameters.Add(":dept", OracleDbType.Varchar2).Value = dept;
            cmd.Parameters.Add(":itemname", OracleDbType.Varchar2).Value = item;
            cmd.Parameters.Add(":amt", OracleDbType.Varchar2).Value = amount;
            cmd.Parameters.Add(":price", OracleDbType.Varchar2).Value = price;
            try
            {
                cmd.ExecuteNonQuery();
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
        public int get_transaction_id()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            int result = 0;
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            try { 
            conn.Open();
            OracleCommand cmd = new OracleCommand("select app.TRANSACTION_SEQ.currval from dual", conn);
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToInt32(dr["currval"]);
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

        }
        public void add_destroy_reason(int id, string reason)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            string query = @"insert into reasons_transaction ( TRANS_ID , REASON_OF_DESTROY)
                            values(:id , :reason) ";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(":id", OracleDbType.Int32).Value = id;
            cmd.Parameters.Add(":reason", OracleDbType.Varchar2).Value = reason;
            try
            {
                cmd.ExecuteNonQuery();
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
        public void add_return_reason(int id, string reason)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            conn.Open();
            string query = @"insert into reasons_transaction ( TRANS_ID , REASON_OF_RETURN)
                            values(:id , :reason) ";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(":id", OracleDbType.Int32).Value = id;
            cmd.Parameters.Add(":reason", OracleDbType.Varchar2).Value = reason;
            try
            {
                cmd.ExecuteNonQuery();
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
        //torb9-8 Done
        public DataTable get_employee_care(string name, string dept)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;

            try
            {
                conn.Open();
                string query = @"select item_name_  , AMOUNT   from transaction where employee_name_=:name and employee_dept=:dpet and type=4 and delete_flag='Y' ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                cmd.Parameters.Add(":dept", OracleDbType.Varchar2).Value = dept;

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
       //torb9-8 Done
        public DataTable get_destroy(string name, string dept)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;

            try
            {
                conn.Open();
                string query = @"select ITEM_NAME_ , AMOUNT  from transaction 
                            where type =1 and EMPLOYEE_NAME_=:name and EMPLOYEE_DEPT=:dept and delete_flag='Y'";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":name", OracleDbType.Varchar2).Value = name;
                cmd.Parameters.Add(":dept", OracleDbType.Varchar2).Value = dept;


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
        //torb9-8 Done
        public DataTable dept_filter(string dept)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;
            
            conn.Open();
            string query = "select  employee_name_,item_name_ , amount ,employee_DEPT from transaction where employee_DEPT=:dept and DELETE_FLAG='Y' ";

            cmd = new OracleCommand(query, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(":dept", OracleDbType.Varchar2).Value = dept;
            
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
        public DataTable category_filter(string category)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;
            Int64 code = get_categ_code(category);

            conn.Open();
            string query = @"select  code , item_name , item_amount ,item_limit ,item_price
                            from items where item_category=:cat ";

            cmd = new OracleCommand(query, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(":cat", OracleDbType.Int64).Value = code;
                       
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
        public DataTable item_filter(string item)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;

            conn.Open();
            string query = @"select  code , item_category , item_amount ,item_limit ,item_price
                            from items where item_name=:name ";

            cmd = new OracleCommand(query, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.Add(":name", OracleDbType.Varchar2).Value = item;
            
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
        //torb9-8 Done
        public DataTable emp_filter(string empname)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;

            try
            {
                conn.Open();
                string query = @"select  employee_name_ , employee_dept ,item_name_ , item_price ,amount ,ext.TYPE_NAME
                            from transaction tr , export_transction ext where employee_name_=:name and tr.type=ext.type_id and DELETE_FLAG='Y' ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":name", OracleDbType.Varchar2).Value = empname;

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
        public DataTable filter_destory()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable destroy = new DataTable();
            DataSet data = new DataSet();
            try { 
            conn.Open();
            OracleCommand cmd = new OracleCommand("select type_name , employee_name_ , employee_dept,item_name_,amount,item_price,id_trans_ from EXPORT_TRANSCTION ,transaction where TYPE_ID=1 and type=1", conn);
            OracleDataReader dr = cmd.ExecuteReader();
            destroy.Load(dr);

           
            return destroy;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return destroy; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }

        }
        public DataTable filter_return()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable destroy = new DataTable();
            DataSet data = new DataSet();
            try { 
            conn.Open();
            OracleCommand cmd = new OracleCommand("select type_name , employee_name_ , employee_dept,item_name_,amount,item_price,id_trans_ from EXPORT_TRANSCTION ,transaction where TYPE_ID=2 and type=2", conn);
            OracleDataReader dr = cmd.ExecuteReader();
            destroy.Load(dr);

           
            return destroy;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return destroy; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }
        public DataTable filter_import()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable destroy = new DataTable();
            DataSet data = new DataSet();
            try { 
            conn.Open();
            OracleCommand cmd = new OracleCommand("select type_name , employee_name_ , employee_dept,item_name_,amount,item_price,id_trans_ from EXPORT_TRANSCTION ,transaction where TYPE_ID=3 and type=3", conn);
            OracleDataReader dr = cmd.ExecuteReader();
            destroy.Load(dr);

            return destroy;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return destroy; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }
        public DataTable filter_export()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable destroy = new DataTable();
            DataSet data = new DataSet();
            try {
                conn.Open();
                OracleCommand cmd = new OracleCommand("select type_name , employee_name_ , employee_dept,item_name_,amount,item_price,id_trans_ from EXPORT_TRANSCTION ,transaction where TYPE_ID=4 and type=4", conn);
                OracleDataReader dr = cmd.ExecuteReader();
                destroy.Load(dr);

               
                return destroy;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return destroy; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }
        public DataTable filter_store()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable store = new DataTable();
            DataSet data = new DataSet();
            try { 
            conn.Open();
            OracleCommand cmd = new OracleCommand("select  * from items order by code ", conn);
            OracleDataReader dr = cmd.ExecuteReader();
            store.Load(dr);

           
            return store;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return store; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }
        public DataTable get_exporter()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable store = new DataTable();
            DataSet data = new DataSet();
            try { 
            if(conn.State ==ConnectionState.Open)
            conn.Close();
            conn.Open();
            OracleCommand cmd = new OracleCommand(@"select ID_TRANS_,ITEM_NAME_ ,EMPLOYEE_NAME_ , EMPLOYEE_DEPT , AMOUNT , ST.export_name,CATEGORY
                                                from transaction tr , export_STATE ST where
                                                tr.type = 4 and tr.EXPORT_TYPE=ST.EXPORT_ID and tr.DELETE_FLAG='Y'", conn);
            OracleDataReader dr = cmd.ExecuteReader();
            store.Load(dr);
           
            return store;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return store; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }
        public DataTable get_items()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable store = new DataTable();
            DataSet data = new DataSet();
            try { 
            conn.Open();
            OracleCommand cmd = new OracleCommand(@"select items.code, items.item_name ,items.item_amount ,items.ITEM_LIMIT,items.ITEM_PRICE ,     
categ.CATEGORY_NAME from items , item_category categ              
where items.item_category=categ.CATEGORY_ID and items.DELETE_FLAG='Y' order by items.code 
 ", conn);
            OracleDataReader dr = cmd.ExecuteReader();
            store.Load(dr);
           
            return store;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return store; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }

        }
        public void add_category(string cat)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            conn.Open();
            OracleCommand cmd = new OracleCommand("insert  into item_category ( category_name,created_by) values(:categ,'" + User.Name + "')  ", conn);
            cmd.Parameters.Add(":categ", OracleDbType.Varchar2).Value = cat;
            try
            {
                cmd.ExecuteNonQuery();
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
        public void update_amount_item(string amount, string name, string category)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            Int64 code = get_categ_code(category);
            
            conn.Open();
            OracleCommand c = new OracleCommand();
            c.CommandText = @"update items set ITEM_AMOUNT=:amountitm where ITEM_NAME=:n and ITEM_CATEGORY=:categ";
            c.Connection = conn;
            c.Parameters.Add(":amountitm", OracleDbType.Varchar2).Value = amount;
            c.Parameters.Add(":n", OracleDbType.Varchar2).Value = name;
            c.Parameters.Add(":categ", OracleDbType.Int32).Value = code;
            try
            {
                c.ExecuteNonQuery();
               
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
        //torb29-5
        public int get_item_serial(Int64 category)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            int result = 0;
            try { 
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "select nvl(max(code),0)+1 from items where item_category=:cat";
            cmd.Connection = conn;
            cmd.Parameters.Add(":cat", OracleDbType.Int64).Value = category;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                result = Convert.ToInt32(dr["nvl(max(code),0)+1"].ToString());
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
        }

        public void add_request(string itmname, string catname, string empname, string deptname, int amount, string attach)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            
            conn.Open();
            string query = @"insert into requesttb (item_name ,item_category ,emp_name , emp_dept , amount,attach) 
                                values(:item , :category ,:empname ,:dept ,:amtitm ,:attach)";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(":item", OracleDbType.Varchar2).Value = itmname;
            cmd.Parameters.Add(":category", OracleDbType.Varchar2).Value = catname;
            cmd.Parameters.Add(":empname", OracleDbType.Varchar2).Value = empname;
            cmd.Parameters.Add(":dept", OracleDbType.Varchar2).Value = deptname;
            cmd.Parameters.Add(":amtitm", OracleDbType.Int32).Value = amount;
            cmd.Parameters.Add(":attach", OracleDbType.Varchar2).Value = attach;
            try
            {
                cmd.ExecuteNonQuery();
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

        public DataTable get_request()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            try { 
            conn.Open();
            OracleCommand cmd = new OracleCommand("select request_id,item_name,item_category,emp_name,emp_dept,amount,nvl(done_flag,'n'),attach from requesttb ", conn);
            DataSet dataSet = new DataSet();
            OracleDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);

            
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

        public DataTable get_request_no()
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            DataTable dt = new DataTable();
            try { 
            conn.Open();
            OracleCommand cmd = new OracleCommand("select request_id,item_name,item_category,emp_name,emp_dept,amount from requesttb where done_flag !='y' or done_flag is null", conn);
            DataSet dataSet = new DataSet();
            OracleDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);

          
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
        public void update_done(string flag, int id)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            try { 
            conn.Open();
            OracleCommand c = new OracleCommand();
            c.CommandText = @"update requesttb set done_flag=:doneflag where request_id=:id ";
            c.Connection = conn;
            c.Parameters.Add(":doneflag", OracleDbType.Varchar2).Value = flag;
            c.Parameters.Add(":id", OracleDbType.Int32).Value = id;
            try
            {
                c.ExecuteNonQuery();
               
            }
            catch (OracleException ex)
            {
                string str = ex.Message;
                string sss = ex.Source;

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
        }

        public string get_image(int id)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            string photo = "";
            try { 
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = "select attach from requesttb where request_id=:id";
            cmd.Connection = conn;
            cmd.Parameters.Add(":id", OracleDbType.Varchar2).Value = id;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                photo = dr["attach"].ToString();
            }
           
            return photo;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return photo; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }
        public DataTable get_request(int id)
        {
           
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;         
            
            try { 
            conn.Open();
            string query = "select request_id,item_name,item_category,emp_name,emp_dept,amount,nvl(done_flag,'n'),attach from requesttb where request_id=:id ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":id", OracleDbType.Varchar2).Value = id;

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
        //12 nov
        public DataTable get_Return_employees_name(string dept)
        {
            
            OracleConnection conn = new OracleConnection(connectionStr);
            DataSet data = new DataSet();
            DataTable dt = new DataTable();
            OracleCommand cmd;
            OracleDataAdapter da;

            try
            {
                conn.Open();
                string query = @"select distinct employee_name_ , code from transaction , agent
            where employee_dept=:dept and employee_name_ = name and type=4 ";

                cmd = new OracleCommand(query, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.Add(":dept", OracleDbType.Varchar2).Value = dept;


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

        //////////////////////////////// get indemnity company name
        public string get_indem_company_name(int compid)
        {
           
            OracleConnection conn = new OracleConnection(connectionStr);
            string compName = "";
            try { 
            conn.Open();
            string query = "select distinct C_ANAME from V_COMPANIES where c_comp_id=:cid ";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(":cid", OracleDbType.Int32).Value = compid;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                compName = dr["C_ANAME"].ToString();
            }

            
            return compName;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return compName; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }

        /////////////////////////////// get request note book provider name
        public string get_provider_notebbok(int id)
        {
                OracleConnection conn = new OracleConnection(connectionStr);
                string providerName = "";
            try { 
                conn.Open();
                string query = "select pr_aname from serv_providers where pr_code=:cid ";
                OracleCommand cmd = new OracleCommand();
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Parameters.Add(":cid", OracleDbType.Int32).Value = id;
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    providerName = dr["pr_aname"].ToString();
                }

           
                return providerName;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return providerName; }
            finally
            {

                if (conn.State != ConnectionState.Closed)
                {
                    conn.Dispose();
                    conn.Close();

                     
                }
            }
        }


        //====================================get company address
        public string GetCompanyAddress(int companyId)
        {
            OracleConnection conn = new OracleConnection(connectionStr);
            string address = "";
            try { 
            conn.Open();
            string query = "select address1 from v_companies where c_comp_id=:cid ";
            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = query;
            cmd.Connection = conn;
            cmd.Parameters.Add(":cid", OracleDbType.Int32).Value = companyId;
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                address = dr["address1"].ToString();
            }

                conn.Dispose();
                conn.Close();

                 
                return address;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return address; }
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
