using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace WpfApplication2.BusinessLayer.Moving_Messenger
{
    class MovingMessServices
    {
        DBManager db = new DBManager();
        public List<MovingMessData> SelectAllArea_In_governerate(int id)
        {
            //-----------get selected governerate------------------//           
            string query = "SELECT BS_ANAME , BS_CODE from AREA_VIEW where BS_CODE_UP is not null and BS_CODE_up=" + id;
            DataTable res = db.ExecuteSelect(query).Tables[0];//
            List<MovingMessData> list = new List<MovingMessData>();
            MovingMessData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new MovingMessData();
                    //obj.Governorate_Code = int.Parse(res.Rows[i]["BS_CODE"].ToString());
                    // obj.Governorate_Name = res.Rows[i]["BS_ANAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }
        //-----------------select all Requests---------------------------------
        public System.ComponentModel.BindingList<MovingMessData> SelectAllMessMoving()
        {
            string query = "SELECT ReqNum,CompanyName,Company_Branch,Governerate,Contact_Person,Company_Address,Dept,Dates,MessengerType,VIP,ReqResons,Phone,Area,HoldDate from MessengerRequestTB where Moved='N' order by VIP DESC";
            DataTable res = db.ExecuteSelect(query).Tables[0];//
            System.ComponentModel.BindingList<MovingMessData> list = new System.ComponentModel.BindingList<MovingMessData>();
            MovingMessData obj;

            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new MovingMessData();
                    obj.ReqCode = int.Parse(res.Rows[i]["ReqNum"].ToString());
                    obj.CompanyName = res.Rows[i]["CompanyName"].ToString();
                    obj.Branch = res.Rows[i]["Company_Branch"].ToString();
                    obj.ContactPerson = res.Rows[i]["Contact_Person"].ToString();
                    obj.Address = res.Rows[i]["Company_Address"].ToString();
                    obj.Dept = res.Rows[i]["Dept"].ToString();
                    try
                    {
                        obj.Date = res.Rows[i]["Dates"].ToString();
                        obj.Hold = res.Rows[i]["HoldDate"].ToString();
                    }
                    catch { }
                    obj.MessengerType = res.Rows[i]["MessengerType"].ToString();
                    //obj.VIP = res.Rows[i]["VIP"].ToString();
                    obj.RequestResons = res.Rows[i]["ReqResons"].ToString();

                    obj.Phone = res.Rows[i]["Phone"].ToString();
                    obj.Area = res.Rows[i]["Area"].ToString();
                    list.Add(obj);
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
        //----------------Insert Messenger_Request------------------//
        public int InsertMessangerMoving(MovingMessData obj)
        {
            string query = string.Format("INSERT INTO app.movingmessengertb(REQNUM,COMPANYNAME,COMPANY_BRANCH,CONTACT_PERSON,COMPANY_ADDRESS,DEPT,DATES,MESSENGERTYPE,REQRESONS,PHONE,AREA,MESSNAME)  values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", obj.ReqCode, obj.CompanyName, obj.Branch, obj.ContactPerson, obj.Address, obj.Dept, obj.Date, obj.MessengerType, obj.RequestResons, obj.Phone, obj.Area, obj.MessName);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        //----------------select MessengerRequestById--------------------
        public MovingMessData SelectMessengerMovingById(int id)
        {
            string query = "select * from MOVINGMESSENGERTB where ReqNum=" + id + "";
            DataTable res = db.ExecuteSelect(query).Tables[0];
            MovingMessData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new MovingMessData();
                    obj.ReqCode = int.Parse(dr["ReqNum"].ToString());
                    obj.CompanyName = dr["CompanyName"].ToString();
                    obj.Branch = dr["Company_Branch"].ToString();
                    obj.ContactPerson = dr["Contact_Person"].ToString();
                    obj.Address = dr["Company_Address"].ToString();
                    try
                    {
                        obj.Date = dr["Dates"].ToString();
                    }
                    catch { }
                    obj.MessengerType = dr["MessengerType"].ToString();
                    obj.RequestResons = dr["ReqResons"].ToString();
                    obj.Dept = dr["Dept"].ToString();
                    obj.Area = dr["Area"].ToString();
                    obj.Phone = dr["Phone"].ToString();
                    obj.MessName = dr["MessName"].ToString();
                    return obj;
                }
            }
            return null;
        }
        //----------------Update Messenger_Moving-----------------------
        public int UpdateMessengerMoving(MovingMessData obj, int id)
        {
            string query = string.Format("UPDATE MovingMessengerTB set CompanyName='{0}',Company_Branch='{1}',Contact_Person='{2}',Company_Address='{3}',Dept='{4}',Dates='{5}',MessengerType='{6}',ReqResons='{7}',Phone='{8}',Area='{9}',MessName='{10}' Where ReqNum={11}", obj.CompanyName, obj.Branch, obj.ContactPerson, obj.Address, obj.Dept, obj.Date, obj.MessengerType, obj.RequestResons, obj.Phone, obj.Area, obj.MessName, id);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        //----------------Update Messenger_Moving2-----------------------
        public int UpdateMessengerMoving2(MovingMessData obj, int id)
        {
            string query = string.Format("UPDATE MovingMessengerTB set MessName='{0}' Where ReqNum={1}", obj.MessName, id);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        //----------------Delete Messenger_Moving-----------------------
        public int DeleteMessengerMovig(int id)
        {
            string query = "delete from MovingMessengerTB where ReqNum=" + id + "";
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }

        //----------------Select Max ReqMess Id-------------------------//
        public string SelectMaxReqMessId()
        {
            string query = "SELECT MAX(ReqNum) from MessengerRequestTB";
            object affected = db.ExecutSelectMax(query);
            return affected.ToString();
        }
        //----------------Insert UpdateMessangerRequest_If_Hold------------------//
        public int UpdateMessangerRequest_If_Hold(MovingMessData obj, int id)
        {
            DB ddd = new DB();
            int i = -1;
            try
            {

                //ddd.RunNonQuery("update MessengerRequestTb set HoldDate ='" + obj.Hold + "' , Moved='N' ,Hold='Y' where ReqNum='" + id + "'  ");
                string query = string.Format("UPDATE app.MessengerRequestTB set HoldDate='{0}',Moved='{1}',Hold='{2}' Where ReqNum={3}", obj.Hold, 'N', 'Y', id);
                int affected = db.ExecuteNonQuery(query);
                i = 0;
                return affected;
              
            }
            catch
            {
                i = -1;
            }

            return i;
        }

        //----------------Insert Messenger_Request------------------//
        public int UpdateMessangerRequest_To_Moved(int id)
        {
            string query = string.Format("UPDATE app.MessengerRequestTB  set  Moved='{0}',Hold='{1}',HoldDate='{2}' Where ReqNum={3}", 'Y', 'N', "", id);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }

        //========================= Sort ============================
        public List<MovingMessData> SelectAllMessMovingSort(string value)
        {
            string query = "SELECT ReqNum,HoldDate,CompanyName,Company_Branch,Governerate,Contact_Person,Company_Address,Dept,Dates,MessengerType,VIP,ReqResons,Phone,Area from MessengerRequestTB where Moved='N' order by " + value;
            DataTable res = db.ExecuteSelect(query).Tables[0];//
            List<MovingMessData> list = new List<MovingMessData>();
            MovingMessData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new MovingMessData();
                    obj.ReqCode = int.Parse(res.Rows[i]["ReqNum"].ToString());
                    obj.CompanyName = res.Rows[i]["CompanyName"].ToString();
                    obj.Branch = res.Rows[i]["Company_Branch"].ToString();
                    obj.ContactPerson = res.Rows[i]["Contact_Person"].ToString();
                    obj.Address = res.Rows[i]["Company_Address"].ToString();
                    obj.Dept = res.Rows[i]["Dept"].ToString();
                    try
                    {
                        obj.Date = res.Rows[i]["Dates"].ToString();
                        obj.Hold = res.Rows[i]["HoldDate"].ToString();
                    }
                    catch { }
                    obj.MessengerType = res.Rows[i]["MessengerType"].ToString();
                    //obj.VIP = res.Rows[i]["VIP"].ToString();
                    obj.RequestResons = res.Rows[i]["ReqResons"].ToString();
                    obj.Phone = res.Rows[i]["Phone"].ToString();
                    obj.Area = res.Rows[i]["Area"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }
    }
}
