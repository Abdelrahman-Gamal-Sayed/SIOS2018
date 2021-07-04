using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32;
using System.Windows;
//using MySql.Data;
using MySql.Data.MySqlClient;

namespace WpfApplication2
{
    class DataAccess
    {
        //SqlConnection cn = new SqlConnection(@"server=171.0.10.200 ; Database=I_queudatabase ; Integrated Security = true");

        //MySqlConnection cn = new MySqlConnection(@"SERVER=217.139.1.60; DATABASE= I-queuedatabase ; UID = DMS; Pwd=DMS_123;");
        MySqlConnection cn = new MySqlConnection(@"SERVER=171.0.10.200; DATABASE= I-queuedatabase ; UID = DMS; Pwd=DMS_123;");

        MySqlCommand cmd = new MySqlCommand();        

        public void open()
        {
            if (cn.State != ConnectionState.Open)
                cn.Open();
        }

        public void close()
        {
            if (cn.State != ConnectionState.Closed)
                cn.Close();
        }
        
        // read data from db

        public DataTable GetData(string query)
        {
            MySqlCommand cmd = new MySqlCommand();

           // cn.Open();                       
            cmd.Connection = cn;
            cmd.CommandText = query;

            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try {
                
                da.Fill(dt);
            }
            catch { }
            return dt;
        }
        // method to insert , update, and delete data from db
        
        public void ExQuery(string query)
        {
            cmd.CommandText = query;
            cmd.Connection = cn;

            open();
            cmd.ExecuteNonQuery();
            close();
        }
        
    }
}
