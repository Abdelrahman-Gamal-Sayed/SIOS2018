using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel;
namespace WpfApplication2.BusinessLayer.Messenger_Confirmation
{
    class ConfirMessServices
    {
        DBManager db = new DBManager();
        ConfirMessData conf = new ConfirMessData();
        //----------------select all Confirmations-----------------------------
        public BindingList<ConfirMessData> SelectAllMessMoving()
        {
            string query = "SELECT ReqNum,CompanyName,Company_Branch,Contact_Person,Company_Address,Dept,Dates,MessengerType,ReqResons,Phone,Area,MessName from MovingMessengerTB where Done='N'";

            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            BindingList<ConfirMessData> list = new BindingList<ConfirMessData>();
            ConfirMessData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    if (User.CompanyName == res.Rows[i]["CompanyName"].ToString())
                    {
                        obj = new ConfirMessData();

                        obj.ReqCode = int.Parse(res.Rows[i]["ReqNum"].ToString());
                        obj.CompanyName = res.Rows[i]["CompanyName"].ToString();
                      //  obj.Branch = res.Rows[i]["Company_Branch"].ToString();
                      //  obj.ContactPerson = res.Rows[i]["Contact_Person"].ToString();
                       // obj.Address = res.Rows[i]["Company_Address"].ToString();
                        obj.Dept = res.Rows[i]["Dept"].ToString();
                        try
                        {
                            obj.Date = DateTime.Parse(res.Rows[i]["Dates"].ToString());

                        }
                        catch { }
                      //  obj.MessengerType = res.Rows[i]["MessengerType"].ToString();
                        obj.RequestResons = res.Rows[i]["ReqResons"].ToString();
                       // obj.Phone = res.Rows[i]["Phone"].ToString();
                       // obj.Area = res.Rows[i]["Area"].ToString();
                      //  obj.MessName = res.Rows[i]["MessName"].ToString();

                        list.Add(obj);
                    }
                }
                return list;
            }
            return null;
        }
        //----------------select all messengers-----------------
        public List<MessangerData> SelectAllMessengers()
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
        //----------------Insert Messenger_Confirm------------------//
        public int InsertMessangerConfirm(ConfirMessData obj)
        {
            string query = string.Format("INSERT INTO ConfirmMessengerTB values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", obj.ReqCode, obj.CompanyName, "", "", "", obj.Dept, obj.Date, "", obj.RequestResons, "", "", "", "", "");
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        //----------------Insert Messenger_Confirm_for_Request------------------//
        public int InsertMessangerConfirm_For_Request(ConfirMessData obj, int ReqCode)
        {
            string query = string.Format("update MessengerRequestTB set Done='{0}' where ReqNum={1}", "True", ReqCode);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        //----------------update Messenger_Moving After Confirm------------------//
        public int UpdateMess_Moving_AfterConfirm(ConfirMessData obj, int ReqCode)
        {
            string query = string.Format("update app.movingmessengertb set Done='{0}',Comments='{1}' where ReqNum={2}", 'Y', "", ReqCode);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        public int UpdateMess_Moving_AfterConfirm2(ConfirMessData obj, int ReqCode)
        {
            string query = string.Format("update app.movingmessengertb set Comments='{0}' where ReqNum={1}", "", ReqCode);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }

        //----------------select MessengerMovingById--------------------
        public ConfirMessData SelectMessengerMovingById(int id)
        {
            string query = "select * from app.movingmessengertb where ReqNum=" + id + "";
            DataTable res = db.ExecuteSelect(query).Tables[0];
            ConfirMessData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new ConfirMessData();
                    obj.ReqCode = int.Parse(dr["ReqNum"].ToString());
                  //  obj.MessName = dr["MessName"].ToString();
                  //  obj.Done = dr["Done"].ToString();
                 //   obj.Comments = dr["Comments"].ToString();
                    return obj;
                }
            }
            return null;


        }
        //----------------Update Messenger_Confirm-----------------------
        public int UpdateMessengerConfirm(ConfirMessData obj, int id)
        {
            string query = string.Format("UPDATE ConfirmMessengerTB set CompanyName='{0}',Company_Branch='{1}',Contact_Person='{2}',Company_Address='{3}',Dept='{4}',Dates='{5}',MessengerType='{6}',ReqResons='{7}',Phone='{8}',Area='{9}',MessName='{10}',Done='{11}',Comments='{12}' Where ReqNum={13}", obj.CompanyName, "", "", "", obj.Dept, obj.Date, "", obj.RequestResons, "", "", "", "", "", id);
            int affected = db.ExecuteNonQuery(query);
            return affected;

        }
    }
}
