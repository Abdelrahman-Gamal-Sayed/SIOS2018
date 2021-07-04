using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace WpfApplication2.BusinessLayer.MessengerRequest
{
    class MessengerRequestServices
    {
        DBManager db = new DBManager();
        public List<MessengerRequestData> SelectAllGovernerators()
        {
            string query = "SELECT BS_ANAME , BS_CODE from AREA_VIEW where BS_CODE_UP is  null";
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<MessengerRequestData> list = new List<MessengerRequestData>();
            MessengerRequestData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new MessengerRequestData();
                    obj.Governorate_Code = int.Parse(res.Rows[i]["BS_CODE"].ToString());
                    obj.Governorate_Name = res.Rows[i]["BS_ANAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }
        public List<MessengerRequestData> SelectAllArea_In_governerate(int id)
        {
            //-----------get selected governerate------------------//           
            string query = "SELECT BS_ANAME , BS_CODE from AREA_VIEW where BS_CODE_UP is not null and BS_CODE_up=" + id;
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<MessengerRequestData> list = new List<MessengerRequestData>();
            MessengerRequestData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new MessengerRequestData();
                    obj.Governorate_Code = int.Parse(res.Rows[i]["BS_CODE"].ToString());
                    obj.Governorate_Name = res.Rows[i]["BS_ANAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }
        //----------------SelectMaxDocId-------------------------//
        public string SelectMaxReqMessId()
        {
            string query = "SELECT MAX(ReqNum) from MessengerRequestTB";
            object affected = db.ExecutSelectMax(query);
            return affected.ToString();
        }
        //----------------Insert Messenger_Request------------------//
        public int InsertMessangerRequest(MessengerRequestData obj)
        {
            string query = string.Format("INSERT INTO MessengerRequestTB(ReqNum,CompanyName,Company_Branch,Governerate,Contact_Person,Company_Address,Dept,Dates,MessengerType,VIP,ReqResons,Phone,Area,Done) values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", obj.ReqCode, obj.CompanyName, obj.Branch, obj.Governorate_Name, obj.ContactPerson, obj.Address, obj.Dept, obj.Date, obj.MessengerType, obj.VIP, obj.RequestResons, obj.Phone, obj.Area, "False");
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        //----------------select MessengerRequestById--------------------
        public MessengerRequestData SelectMessengerRequestById(int id)
        {
            string query = "select * from MessengerRequestTB where ReqNum=" + id + "";
            DataTable res = db.ExecuteSelect(query).Tables[0];
            MessengerRequestData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new MessengerRequestData();
                    obj.ReqCode = int.Parse(dr["ReqNum"].ToString());
                    obj.CompanyName = dr["CompanyName"].ToString();
                    obj.Branch = dr["Company_Branch"].ToString();
                    obj.Governorate_Name = dr["Governerate"].ToString();
                    obj.ContactPerson = dr["Contact_Person"].ToString();
                    obj.Address = dr["Company_Address"].ToString();
                    try
                    {
                        obj.Date = dr["Dates"].ToString();
                    }
                    catch { }
                    obj.MessengerType = dr["MessengerType"].ToString();
                    obj.VIP = dr["VIP"].ToString();
                    obj.RequestResons = dr["ReqResons"].ToString();
                    obj.Dept = dr["Dept"].ToString();
                    obj.Area = dr["Area"].ToString();
                    obj.Phone = dr["Phone"].ToString();
                    return obj;
                }
            }
            return null;


        }
        //----------------Update Messenger_Request-----------------------
        public int UpdateMessengerRequest(MessengerRequestData obj, int id)
        {
            string query = string.Format("UPDATE MessengerRequestTB set CompanyName='{0}',Company_Branch='{1}',Governerate='{2}',Contact_Person='{3}',Dept='{4}',Company_Address='{5}',Dates='{6}',MessengerType='{7}',VIP='{8}',ReqResons='{9}',Phone='{10}',Area='{11}' Where ReqNum={12}", obj.CompanyName, obj.Branch, obj.Governorate_Name, obj.ContactPerson, obj.Dept, obj.Address, obj.Date, obj.MessengerType, obj.VIP, obj.RequestResons, obj.Phone, obj.Area, id);
            int affected = db.ExecuteNonQuery(query);
            return affected;

        }
        //----------------Delete Messenger_Request-----------------------
        public int DeleteMessengerRequest(int id)
        {
            string query = "delete from MessengerRequestTB where ReqNum=" + id + "";
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        //-----------select all MessengersReq--------------------------------
        public List<MessengerRequestData> SelectAllMessengersRequests()
        {
            string query = string.Format("SELECT * from MessengerRequestTB where Done='False'");
            DataTable res = db.ExecuteSelect(query).Tables[0];//
            List<MessengerRequestData> list = new List<MessengerRequestData>();
            MessengerRequestData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new MessengerRequestData();
                    obj.ReqCode = int.Parse(res.Rows[i]["ReqNum"].ToString());
                    obj.CompanyName = res.Rows[i]["CompanyName"].ToString();
                    obj.Branch = res.Rows[i]["Company_Branch"].ToString();
                    obj.Governorate_Name = res.Rows[i]["Governerate"].ToString();
                    obj.ContactPerson = res.Rows[i]["Contact_Person"].ToString();
                    obj.Address = res.Rows[i]["Company_Address"].ToString();
                    obj.Dept = res.Rows[i]["Dept"].ToString();
                    try
                    {
                        obj.Date = res.Rows[i]["Dates"].ToString();
                        obj.HoldDate = res.Rows[i]["HoldDate"].ToString();

                    }
                    catch { }
                    obj.MessengerType = res.Rows[i]["MessengerType"].ToString();
                    obj.VIP = res.Rows[i]["VIP"].ToString();
                    obj.RequestResons = res.Rows[i]["ReqResons"].ToString();
                    obj.Phone = res.Rows[i]["Phone"].ToString();
                    obj.Area = res.Rows[i]["Area"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }
        //-----------select all Companies--------------------------------
        public List<MessengerRequestData> SelectAllCompanies(string companyName, string CompanyCode)
        {
            string query = "select distinct C_COMP_ID,C_ANAME,ADDRESS1,TEL1,TEL2 from V_COMPANIES where C_ANAME like '%" + companyName + "%' or C_COMP_ID like '%" + CompanyCode + "'";

            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<MessengerRequestData> list = new List<MessengerRequestData>();
            MessengerRequestData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new MessengerRequestData();
                    obj.CompanyCode = res.Rows[i]["C_COMP_ID"].ToString();
                    obj.CompanyName = res.Rows[i]["C_ANAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }

        //-----------select all Companies--------------------------------
        public List<MessengerRequestData> SelectAllCompanies()
        {
            string query = "select distinct C_COMP_ID,C_ANAME,ADDRESS1,TEL1,TEL2 from V_COMPANIES";

            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<MessengerRequestData> list = new List<MessengerRequestData>();
            MessengerRequestData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new MessengerRequestData();
                    obj.CompanyCode = res.Rows[i]["C_COMP_ID"].ToString();
                    obj.CompanyName = res.Rows[i]["C_ANAME"].ToString();
                    obj.Address = res.Rows[i]["ADDRESS1"].ToString();
                    obj.Phone = res.Rows[i]["TEL1"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }

        //----------------select company Details--------------------
        public MessengerRequestData SelectAllCompanies_Areas(string companyCode)
        {
            string query = "select distinct C_COMP_ID,ADDRESS1,TEL1 from V_COMPANIES where C_COMP_ID=" + companyCode;
            try
            {
                DataTable res = db.ExecuteSelect2(query).Tables[0];

                MessengerRequestData obj;
                if (res != null && res.Rows.Count > 0)
                {
                    foreach (DataRow dr in res.Rows)
                    {
                        obj = new MessengerRequestData();
                        obj.CompanyCode = dr["C_COMP_ID"].ToString();
                        obj.Address = dr["ADDRESS1"].ToString();
                        obj.Phone = dr["TEL1"].ToString();
                        return obj;
                    }
                }
            }
            catch { }

            return null;
        }
        
        //-----------select all Companies_Branches--------------------------------
        public List<MessengerRequestData> SelectAllCompanies_Branches(string companyCode)
        {
            string query = "select distinct vc.A_NAME, vc.C_COMP_ID , c.ADDRESS1 from V_COMPANIES_CC vc join V_COMPANIES c on vc.C_COMP_ID='" + companyCode+"'where c.ADDRESS1=(select c.ADDRESS1 from v_companies c where c_COMP_ID='"+companyCode+"')";

            try
            {
                DataTable res = db.ExecuteSelect2(query).Tables[0];//

                List<MessengerRequestData> list = new List<MessengerRequestData>();
                MessengerRequestData obj;
                if (res != null && res.Rows.Count > 0)
                {
                    for (int i = 0; i < res.Rows.Count; i++)
                    {
                        obj = new MessengerRequestData();
                        obj.Branch = res.Rows[i]["A_NAME"].ToString();
                        obj.Address = res.Rows[i]["ADDRESS1"].ToString();
                        list.Add(obj);
                    }
                    return list;
                }

                return null;
            }catch
            {
                return null;
            }

        }


        //============================ aya=========================
        public List<MessengerRequestData> SelectAllCompaniesByNameOrCode(string input)
        {
            string query = @"select distinct C_COMP_ID,C_ANAME,ADDRESS1,TEL1,TEL2 from V_COMPANIES 
                        where C_COMP_ID like '%"+input+ "%' or C_ANAME like '%" + input + "%'";

            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<MessengerRequestData> list = new List<MessengerRequestData>();
            MessengerRequestData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new MessengerRequestData();
                    obj.CompanyCode = res.Rows[i]["C_COMP_ID"].ToString();
                    obj.CompanyName = res.Rows[i]["C_ANAME"].ToString();
                    obj.Address = res.Rows[i]["ADDRESS1"].ToString();
                    obj.Phone = res.Rows[i]["TEL1"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }

    }
}
