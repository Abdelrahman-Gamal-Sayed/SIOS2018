using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
namespace WpfApplication2.BusinessLayer
{
    class MessangerServices
    {
        DBManager db = new DBManager();
        //----------------SelectMaxDocId-------------------------//
        //torb18-7 Done
        public string SelectMaxMessId()
        {
            string query = "SELECT MAX(RUN_ID) from ENUM_RUNNER_DATA";
            object affected = db.ExecutSelectMax(query);
            return affected.ToString();
        }
        //----------------Insert Messenger------------------//
        public  int InsertMessanger(MessangerData obj)
        {
            string query = string.Format("INSERT INTO MessangerTB values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}')", obj.Id, obj.Name, obj.UserName, obj.Password, obj.DateOfBirth, obj.Address, obj.Type, obj.CardNum);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        //----------------select All Messengers--------------------
        public  List<MessangerData> SelectAllMessengers()
        {
            string query = "SELECT * from MessangerTB";
            DataTable res = db.ExecuteSelect(query).Tables[0];//
            List<MessangerData> list = new List<MessangerData>();
            MessangerData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new MessangerData();
                    obj.Id = int.Parse(res.Rows[i]["Mess_Id"].ToString());
                    obj.Name = res.Rows[i]["Mess_Name"].ToString();
                    obj.UserName = res.Rows[i]["Mess_UserName"].ToString();
                    obj.Password = res.Rows[i]["Mess_Password"].ToString();
                    obj.DateOfBirth = res.Rows[i]["Mess_DateOfBirth"].ToString();
                    try
                    {
                        // obj.DateOfBirth = DateTime.Parse(res.Rows[i]["Mess_DateOfBirth"].ToString());
                    }
                    catch { }
                    obj.Address = res.Rows[i]["Mess_Address"].ToString();
                    obj.Type = res.Rows[i]["Mess_Type"].ToString();
                    obj.CardNum = res.Rows[i]["Mess_CardNum"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }
        //----------------select MessengerById--------------------
        public  MessangerData SelectMessengerById(int id)
        {
            string query = "select * from MessangerTB where Mess_Id=" + id + "";
            DataTable res = db.ExecuteSelect(query).Tables[0];
            MessangerData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new MessangerData();
                    obj.Id = int.Parse(dr["Mess_Id"].ToString());
                    obj.Name = dr["Mess_Name"].ToString();
                    obj.UserName = dr["Mess_UserName"].ToString();
                    obj.Password = dr["Mess_Password"].ToString();
                    obj.DateOfBirth = dr["Mess_DateOfBirth"].ToString();
                    obj.Address = dr["Mess_Address"].ToString();
                    obj.Type = dr["Mess_Type"].ToString();
                    obj.CardNum = dr["Mess_CardNum"].ToString();
                    return obj;
                }
            }
            return null;


        }
        //----------------Update Messenger-----------------------
        public  int UpdateMessenger(MessangerData obj, int id)
        {
            int affected = 0;
            string query = string.Format("UPDATE MessangerTB set Mess_Name='{0}',Mess_UserName='{1}',Mess_Password='{2}',Mess_DateOfBirth='{3}',Mess_Address='{4}',Mess_Type='{5}',Mess_CardNum='{6}' Where Mess_Id={7}", obj.Name, obj.UserName, obj.Password, obj.DateOfBirth, obj.Address, obj.Type, obj.CardNum, id);
            try
            {

                affected = db.ExecuteNonQuery(query);
            }
            catch (OracleException ex)
            {
                string ss = ex.Message;
            }
            return affected;

        }
        //----------------Delete Messenger-----------------------
        public  int DeleteMessenger(int id)
        {
            string query = "delete from MessangerTB where Mess_Id=" + id + "";
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }

        //----------------select all messengers-----------------
        public  List<MessangerData> SelectAllMessengersForMoving()
        {
            string query = "SELECT Mess_Id,Mess_Name from MessangerTB";
            DataTable res = db.ExecuteSelect(query).Tables[0];//
            List<MessangerData> list = new List<MessangerData>();
            MessangerData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new MessangerData();
                    obj.Id = int.Parse(res.Rows[i]["Mess_Id"].ToString());
                    obj.Name = res.Rows[i]["Mess_Name"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }
    }
}
