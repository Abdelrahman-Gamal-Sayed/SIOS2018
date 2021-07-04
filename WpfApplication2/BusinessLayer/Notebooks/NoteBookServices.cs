using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;
namespace WpfApplication2.BusinessLayer.Notebooks
{
    class NoteBookServices
    {
        //////////////////------------DeliverNotebook-------------///////////////////

        DBManager db = new DBManager();

        public List<NoteBookData> SelectAllProviderTypes()
        {
            string query = string.Format("select PRV_TYPE,TYP_ANAME from PROVIDER_TYP22 where PRV_TYPE!=2 and PRV_TYPE!=3 and PRV_TYPE!=4");
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<NoteBookData> list = new List<NoteBookData>();
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new NoteBookData();
                    obj.ProvTypeCode = int.Parse(res.Rows[i]["PRV_TYPE"].ToString());
                    obj.Type_Name = res.Rows[i]["TYP_ANAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }

        //----------------select Notebook by OrderNum if it is arequest--------------------
        public NoteBookData SelectNotebookByOrderNum(int OrderNum)
        {
            string query = "select * from DELIVERNOTEBOOKTB where ORDER_NUM=" + OrderNum + "";
            DataTable res = db.ExecuteSelect(query).Tables[0];
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new NoteBookData();
                    try
                    {

                        obj.Order_Num = int.Parse(dr["ORDER_NUM"].ToString());
                        obj.TransAction_Code = int.Parse(dr["TRANSACTION_CODE"].ToString());
                        obj.Batch_Count = int.Parse(dr["BATCH_COUNT"].ToString());
                        try
                        {
                            obj.Serial_From = int.Parse(dr["SERIAL_FROM"].ToString());
                            obj.Serial_To = int.Parse(dr["SERIAL_TO"].ToString());
                            obj.Speciality_Code = int.Parse(dr["SPECIALITY_CODE"].ToString());
                            obj.Deliver_Person = dr["DELIVER_PERSON"].ToString();
                            obj.Deliver_National_ID = dr["DELIVER_NATIONAL_ID"].ToString();
                            obj.Deliver_National_ID_Img = dr["DELIVER_NATIONAL_ID_IMG"].ToString();
                            //------------messenger------------------
                            obj.Messenger_Code = int.Parse(dr["MESSENGER_CODE"].ToString());
                            obj.Receipt_Num = int.Parse(dr["RECEIPT_NUM"].ToString());
                            obj.Receipt_Img = dr["RECEIPT_IMG"].ToString();
                            obj.Receipt_Date = dr["RECEIPT_DATE"].ToString();
                            obj.Receipt_Name = dr["RECEIPT_NAME"].ToString();
                            obj.Receipt_Comp = dr["RECEIPT_COMP"].ToString();
                            obj.Order_Num = int.Parse(dr["ORDER_NUM"].ToString());
                            obj.Notebook_Type_Code = int.Parse(dr["NOTEBOOK_TYPE_CODE"].ToString());
                            obj.Deliver_Code = int.Parse(dr["DELIVER_CODE"].ToString());

                        }
                        catch { }

                        obj.ProvTypeCode = int.Parse(dr["PROV_TYPE_CODE"].ToString());
                        obj.Prov_Code = int.Parse(dr["PROV_CODE"].ToString());


                    }
                    catch { }

                    return obj;
                }
            }
            return null;


        }
        public List<NoteBookData> SelectAllProviderNames(int ProvideCode)
        {
            string query = string.Format("select PR_CODE,PR_ANAME from SERV_PROVIDERS where PRV_TYPE=" + ProvideCode);
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<NoteBookData> list = new List<NoteBookData>();
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new NoteBookData();
                    obj.Prov_Code = int.Parse(res.Rows[i]["PR_CODE"].ToString());
                    obj.Prov_Name = res.Rows[i]["PR_ANAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }
        //-----------select all Companies--------------------------------
        public List<NoteBookData> SelectAllProviderNamesForSearch(int ProviderCode, string ProviderName, int ProviderType)
        {
            string query = string.Format("select PR_CODE,PR_ANAME from SERV_PROVIDERS where (PR_CODE={0} and PRV_TYPE={1}) or (PR_ANAME like '%{2}%' and PRV_TYPE={3})", ProviderCode, ProviderType, ProviderName, ProviderType);
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<NoteBookData> list = new List<NoteBookData>();
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new NoteBookData();
                    obj.Prov_Code = int.Parse(res.Rows[i]["PR_CODE"].ToString());
                    obj.Prov_Name = res.Rows[i]["PR_ANAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }
        //-----------select all info about doctor--------------------------------
        public NoteBookData SelectAllDoctorInfo(int DoctorCode)
        {
            string query = string.Format(" select distinct sv.DOC_SPEC,sv.ADDRESS1,sv.TEL1,sv.PR_ANAME,dc.BS_ANAME from  SERV_PROVIDERS sv , DOC_SPECIFICATION dc where sv.DOC_SPEC=dc.bs_code and pr_code={0}", DoctorCode);

            DataTable res = db.ExecuteSelect(query).Tables[0];
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new NoteBookData();
                    obj.Phone = int.Parse(dr["TEL1"].ToString());
                    obj.Speciality = dr["BS_ANAME"].ToString();
                    obj.Address = dr["ADDRESS1"].ToString();
                    obj.Speciality_Code = int.Parse(dr["DOC_SPEC"].ToString());
                    return obj;
                }
            }
            return null;


        }
        //-------------select Deliver Types-------------------------
        public List<NoteBookData> SelectAllDeliverTypes()
        {
            string query = string.Format("select DELIVER_CODE,DELIVERTYPE from DELIVERTYPETB");
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<NoteBookData> list = new List<NoteBookData>();
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new NoteBookData();
                    obj.Deliver_Code = int.Parse(res.Rows[i]["DELIVER_CODE"].ToString());
                    obj.Deliver_Type = res.Rows[i]["DELIVERTYPE"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }
        //----------------select all messengers-----------------
        public List<NoteBookData> SelectAllMessengers()
        {
            string query = "SELECT Mess_Id,Mess_Name from MessangerTB";
            DataTable res = db.ExecuteSelect(query).Tables[0];//
            List<NoteBookData> list = new List<NoteBookData>();
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new NoteBookData();
                    obj.Id = int.Parse(res.Rows[i]["Mess_Id"].ToString());
                    obj.Name = res.Rows[i]["Mess_Name"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }
        //----------------select MessengerById--------------------
        public NoteBookData SelectMessengerById(int id)
        {
            string query = "select * from MessangerTB where Mess_Id=" + id + "";
            DataTable res = db.ExecuteSelect(query).Tables[0];
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new NoteBookData();
                    obj.Id = int.Parse(dr["Mess_Id"].ToString());
                    obj.CardNum = dr["Mess_CardNum"].ToString();
                    obj.Name = dr["MESS_NAME"].ToString();
                    return obj;
                }
            }
            return null;


        }
        //----------------select All Notebooks Types---------------
        public List<NoteBookData> SelectAllNotebookTypes()
        {
            string query = string.Format("select CODE,ITEM_NAME from ITEMS where item_category=2100");
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<NoteBookData> list = new List<NoteBookData>();
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new NoteBookData();
                    obj.Notebook_Type_Code = int.Parse(res.Rows[i]["CODE"].ToString());
                    obj.NotebookName = res.Rows[i]["ITEM_NAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }
        //----------------SelectMaxNotebook_Id-------------------------//
        public string SelectMaxNotebookTransaction()
        {
            string query = "SELECT MAX(TRANSACTION_CODE) from DELIVERNOTEBOOKTB";
            object affected = db.ExecutSelectMax(query);
            return affected.ToString();
        }
        //----------------Insert Notebook------------------//
        public int InsertNotebook(NoteBookData obj)
        {
            string query = string.Format("INSERT INTO DELIVERNOTEBOOKTB(TRANSACTION_CODE,BATCH_COUNT,SERIAL_FROM,SERIAL_TO,PROV_TYPE_CODE,PROV_CODE,SPECIALITY_CODE,DELIVER_CODE,DELIVER_PERSON,DELIVER_NATIONAL_ID,DELIVER_NATIONAL_ID_IMG,MESSENGER_CODE,RECEIPT_NUM,RECEIPT_IMG,RECEIPT_DATE,RECEIPT_NAME,RECEIPT_COMP,ORDER_NUM,NOTEBOOK_TYPE_CODE,CREATEDDATE,CREATEDBY) values({0},{1},{2},{3},{4},{5},{6},{7},'{8}','{9}','{10}',{11},{12},'{13}','{14}','{15}','{16}',{17},{18},'{19}','{20}')", obj.TransAction_Code, obj.Batch_Count, obj.Serial_From, obj.Serial_To, obj.ProvTypeCode, obj.Prov_Code, obj.Doc_Spec_Code, obj.DeliverTypeCode, obj.Deliver_Person, obj.Deliver_National_ID, obj.Deliver_National_ID_Img, obj.Messenger_Code, obj.Receipt_Num, obj.Receipt_Img, obj.Receipt_Date, obj.Receipt_Name, obj.Receipt_Comp, obj.Order_Num, obj.Notebook_Type_Code, obj.Created_Date, obj.Created_By);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        //----------------Update Notebook----------------------
        public int UpdateNotebook(NoteBookData obj, int TransCode)
        {
            int affected = 0;
            string query = string.Format("UPDATE DELIVERNOTEBOOKTB set SERIAL_FROM={0},SERIAL_TO={1},SPECIALITY_CODE={2},DELIVER_CODE={3},DELIVER_PERSON='{4}',DELIVER_NATIONAL_ID='{5}',DELIVER_NATIONAL_ID_IMG='{6}',MESSENGER_CODE={7},RECEIPT_NUM={8},RECEIPT_IMG='{9}',RECEIPT_DATE='{10}',RECEIPT_NAME='{11}',RECEIPT_COMP='{12}',PROV_TYPE_CODE={13},PROV_CODE={14},BATCH_COUNT={15},NOTEBOOK_TYPE_CODE={16},CREATEDBY='{17}',CREATEDDATE='{18}' where TRANSACTION_CODE={19}", obj.Serial_From, obj.Serial_To, obj.Doc_Spec_Code, obj.DeliverTypeCode, obj.Deliver_Person, obj.Deliver_National_ID, obj.Deliver_National_ID_Img, obj.Messenger_Code, obj.Receipt_Num, obj.Receipt_Img, obj.Receipt_Date, obj.Receipt_Name, obj.Receipt_Comp, obj.ProvTypeCode, obj.Prov_Code, obj.Batch_Count, obj.Notebook_Type_Code, obj.Created_By, obj.Created_Date, TransCode);
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

        //----------------Update Notebook If the user press تسليم-----------------------
        public int UpdateNotebook_If_It_is_a_Request(NoteBookData obj, int OrderNo)
        {
            int affected = 0;
            string query = string.Format("UPDATE DELIVERNOTEBOOKTB set SERIAL_FROM={0},SERIAL_TO={1},SPECIALITY_CODE={2},DELIVER_CODE={3},DELIVER_PERSON='{4}',DELIVER_NATIONAL_ID='{5}',DELIVER_NATIONAL_ID_IMG='{6}',MESSENGER_CODE={7},RECEIPT_NUM={8},RECEIPT_IMG='{9}',RECEIPT_DATE='{10}',RECEIPT_NAME='{11}',RECEIPT_COMP='{12}' Where ORDER_NUM={13}", obj.Serial_From, obj.Serial_To, obj.Doc_Spec_Code, obj.DeliverTypeCode, obj.Deliver_Person, obj.Deliver_National_ID, obj.Deliver_National_ID_Img, obj.Messenger_Code, obj.Receipt_Num, obj.Receipt_Img, obj.Receipt_Date, obj.Receipt_Name, obj.Receipt_Comp, OrderNo);
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

        //----------------Insert Notebook Serial------------------//
        public int InsertNotebook_Serial(NoteBookData obj)
        {
            string query = string.Format("insert into DELIVERSERIALTB(DELIVER_CODE,DELIVER_SERIAL) values({0},{1})", obj.TransAction_Code, obj.Deliver_Serial);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        //----------------Update Notebook Serial------------------//
        public int UpdateNotebook_Serial(NoteBookData obj, int TransCode, int SERIALCODE)
        {
            string query = string.Format("update  DELIVERSERIALTB set DELIVER_CODE={0},DELIVER_SERIAL={1} where DELIVER_CODE={2} and SERIALCODE={3}", obj.TransAction_Code, obj.Deliver_Serial, TransCode, SERIALCODE);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        public int DeleteNotebook_Serial(int transCode)
        {
            string query = string.Format("delete from  DELIVERSERIALTB  where DELIVER_CODE={0}", transCode);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        //----------------select Notebook by transaction code--------------------
        public NoteBookData SelectNotebookByTransCode(int transCode)
        {
            string query = "select * from DELIVERNOTEBOOKTB where transaction_code=" + transCode + "";
            DataTable res = db.ExecuteSelect(query).Tables[0];
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new NoteBookData();
                    try
                    {

                        obj.Order_Num = int.Parse(dr["ORDER_NUM"].ToString());
                        obj.TransAction_Code = int.Parse(dr["TRANSACTION_CODE"].ToString());
                        obj.Batch_Count = int.Parse(dr["BATCH_COUNT"].ToString());
                        try
                        {
                            obj.Serial_From = int.Parse(dr["SERIAL_FROM"].ToString());
                            obj.Serial_To = int.Parse(dr["SERIAL_TO"].ToString());
                            obj.Speciality_Code = int.Parse(dr["SPECIALITY_CODE"].ToString());
                            obj.Deliver_Person = dr["DELIVER_PERSON"].ToString();
                            obj.Deliver_National_ID = dr["DELIVER_NATIONAL_ID"].ToString();
                            obj.Deliver_National_ID_Img = dr["DELIVER_NATIONAL_ID_IMG"].ToString();
                            //------------messenger------------------
                            obj.Messenger_Code = int.Parse(dr["MESSENGER_CODE"].ToString());
                            obj.Receipt_Num = int.Parse(dr["RECEIPT_NUM"].ToString());
                            obj.Receipt_Img = dr["RECEIPT_IMG"].ToString();
                            obj.Receipt_Date = dr["RECEIPT_DATE"].ToString();
                            obj.Receipt_Name = dr["RECEIPT_NAME"].ToString();
                            obj.Receipt_Comp = dr["RECEIPT_COMP"].ToString();
                            obj.Order_Num = int.Parse(dr["ORDER_NUM"].ToString());
                            obj.Notebook_Type_Code = int.Parse(dr["NOTEBOOK_TYPE_CODE"].ToString());
                            obj.Deliver_Code = int.Parse(dr["DELIVER_CODE"].ToString());

                        }
                        catch { }
                        obj.Notebook_Type_Code = int.Parse(dr["NOTEBOOK_TYPE_CODE"].ToString());
                        obj.ProvTypeCode = int.Parse(dr["PROV_TYPE_CODE"].ToString());
                        obj.Prov_Code = int.Parse(dr["PROV_CODE"].ToString());


                    }
                    catch { }

                    return obj;
                }
            }
            return null;


        }
        //----------------select Notebook by transaction code--------------------
        public List<NoteBookData> SelectNotebookSerialByTransCode(int transCode)
        {
            string query = "select * from DELIVERSERIALTB where DELIVER_CODE=" + transCode + "";
            DataTable res = db.ExecuteSelect(query).Tables[0];
            List<NoteBookData> list = new List<NoteBookData>();
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new NoteBookData();
                    obj.TransAction_Code = int.Parse(dr["DELIVER_CODE"].ToString());
                    obj.Deliver_Serial = int.Parse(dr["DELIVER_SERIAL"].ToString());
                    list.Add(obj);
                }
                return list;
            }
            return null;


        }

        //////////////////////---------Request_Notebook frm-------/////////////////////////

        //----------------SelectMaxNotebook_Id-------------------------//
        public string SelectMaxNotebookRequestNo()
        {
            string query = "SELECT MAX(ORDER_NUM) from REQUEST_NOTEBOOKTB";
            object affected = db.ExecutSelectMax(query);
            return affected.ToString();
        }
        //----------------Insert Notebook------------------//
        public int InsertNotebook_Request(NoteBookData obj)
        {
            string query = string.Format("INSERT INTO  REQUEST_NOTEBOOKTB(ORDER_NUM,PROV_TYPE_CODE,PROV_CODE,REQ_DATE,NOTEBOOK_TYPE_CODE,BATCH_COUNT,CONFIRM_REQ,CREATEDBY) values({0},{1},{2},'{3}',{4},{5},'{6}','{7}')", obj.Order_Num, obj.ProvTypeCode, obj.Prov_Code, obj.Request_Date, obj.Notebook_Type_Code, obj.Batch_Count, "N", obj.Created_By);

            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        //----------------select  Notebooks_RequestsBy_Id---------------
        public NoteBookData SelectNotebookRequestsById(int ReqNo)
        {
            string query = string.Format("select REQUEST_NOTEBOOKTB.* , serv_providers.pr_aname from REQUEST_NOTEBOOKTB, serv_providers where ORDER_NUM=" + ReqNo + "and REQUEST_NOTEBOOKTB.prov_code =  serv_providers.pr_code ");
            DataTable res = db.ExecuteSelect2(query).Tables[0];//

            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new NoteBookData();
                    obj.Order_Num = int.Parse(res.Rows[i]["ORDER_NUM"].ToString());
                    obj.ProvTypeCode = int.Parse(res.Rows[i]["PROV_TYPE_CODE"].ToString());
                    obj.Prov_Code = int.Parse(res.Rows[i]["PROV_CODE"].ToString());
                    obj.Request_Date = res.Rows[i]["REQ_DATE"].ToString();
                    obj.Notebook_Type_Code = int.Parse(res.Rows[i]["NOTEBOOK_TYPE_CODE"].ToString());
                    obj.Batch_Count = int.Parse(res.Rows[i]["BATCH_COUNT"].ToString());
                    obj.Prov_Name = res.Rows[i]["pr_aname"].ToString();
                    return obj;
                }

            }
            return null;
        }



        //----------------Update Notebook_Request-----------------------
        public int UpdateNotebook_Request(NoteBookData obj, int id)
        {
            int affected = 0;
            string query = string.Format("UPDATE REQUEST_NOTEBOOKTB set PROV_TYPE_CODE={0},PROV_CODE={1},REQ_DATE='{2}',NOTEBOOK_TYPE_CODE={3},BATCH_COUNT={4},CONFIRM_REQ='{5}',CREATEDBY='{6}' Where ORDER_NUM={7}", obj.ProvTypeCode, obj.Prov_Code, obj.Request_Date, obj.Notebook_Type_Code, obj.Batch_Count, "N", obj.Created_By, id);
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
        //----------------Delete Notebook_Request-----------------------
        public int DeleteNotebook_Request(int id)
        {
            string query = "delete from REQUEST_NOTEBOOKTB where ORDER_NUM=" + id + "";
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }

        /////////////////----------Confirm Request_Notebook-------//////////////////////////

        //--------------------select All Notebooks_Request ---------------
        public List<NoteBookData> SelectAllNotebook_Request()
        {
            string query = string.Format("select ORDER_NUM,PROV_TYPE_CODE,PROV_CODE,NOTEBOOK_TYPE_CODE,BATCH_COUNT, CONFIRM_REQ, REQ_DATE  from REQUEST_NOTEBOOKTB where CONFIRM_REQ='N' order by ORDER_NUM");
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<NoteBookData> list = new List<NoteBookData>();
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new NoteBookData();
                    obj.Order_Num = int.Parse(res.Rows[i]["ORDER_NUM"].ToString());
                    obj.ProvTypeCode = int.Parse(res.Rows[i]["PROV_TYPE_CODE"].ToString());
                    obj.Prov_Code = int.Parse(res.Rows[i]["PROV_CODE"].ToString());
                    obj.Request_Date = res.Rows[i]["REQ_DATE"].ToString();
                    obj.Notebook_Type_Code = int.Parse(res.Rows[i]["NOTEBOOK_TYPE_CODE"].ToString());
                    obj.Batch_Count = int.Parse(res.Rows[i]["BATCH_COUNT"].ToString());
                    obj.ConfrimRequest = res.Rows[i]["CONFIRM_REQ"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }
        //-------------------select prov_type_Name------------------
        public NoteBookData SelectProviderTypeNameById(int ProvideType)
        {
            string query = string.Format("select PRV_TYPE,TYP_ANAME from PROVIDER_TYP22 where PRV_TYPE=" + ProvideType);
            DataTable res = db.ExecuteSelect(query).Tables[0];
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new NoteBookData();
                    obj.ProvTypeCode = int.Parse(dr["PRV_TYPE"].ToString());
                    obj.Type_Name = dr["TYP_ANAME"].ToString();
                    return obj;
                }
            }
            return null;
        }
        //-------------------select prov_Name By Id------------------
        public NoteBookData SelectProviderNameById(int ProvideCode)
        {
            string query = string.Format("select PR_CODE,PR_ANAME from SERV_PROVIDERS where PR_CODE=" + ProvideCode);
            DataTable res = db.ExecuteSelect(query).Tables[0];
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new NoteBookData();
                    obj.Prov_Code = int.Parse(dr["PR_CODE"].ToString());
                    obj.Prov_Name = dr["PR_ANAME"].ToString();
                    return obj;
                }
            }
            return null;
        }
        //-------------------select Notebook_Type_ById------------------
        public NoteBookData SelectNotebookTypeById(int NotebookType_Code)
        {
            string query = string.Format("select CODE,ITEM_NAME from app.items where CODE=" + NotebookType_Code);
            DataTable res = db.ExecuteSelect(query).Tables[0];
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new NoteBookData();
                    obj.Notebook_Type_Code = int.Parse(dr["CODE"].ToString());
                    obj.NotebookName = dr["ITEM_NAME"].ToString();
                    return obj;
                }
            }
            return null;
        }
        //----------------Update Notebook_Request_After Confirm-----------------------
        public int UpdateNotebook_Request_AfterConfirm(NoteBookData obj, int id)
        {
            int affected = 0;
            string query = string.Format("UPDATE REQUEST_NOTEBOOKTB set CONFIRM_REQ='{0}' Where ORDER_NUM={1}", "Y", id);
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
        //----------------Insert in  DeliverNotebook After Press تسليم------------------//
        public int InsertRequest_InNotebook_AfterPressDeliver(NoteBookData obj)
        {
            string query = string.Format("INSERT INTO DELIVERNOTEBOOKTB(TRANSACTION_CODE,BATCH_COUNT,PROV_TYPE_CODE,PROV_CODE,CREATEDDATE,CREATEDBY,ORDER_NUM,REQUESTDATE,Notebook_Type_Code) values({0},{1},{2},{3},'{4}','{5}',{6},'{7}',{8})", obj.TransAction_Code, obj.Batch_Count, obj.ProvTypeCode, obj.Prov_Code, obj.Created_Date, obj.Created_By, obj.Order_Num, obj.Request_Date, obj.Notebook_Type_Code);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }

        ////////////////////// Moving Report Notebook By Date form/////////////////////

        public List<NoteBookData> SelectAllNotebook_Report(string DateFrom, string DateTo)
        {
            string query = string.Format("select * from  DELIVERNOTEBOOKTB where CREATEDDATE between '{0}' and '{1}'", DateFrom, DateTo);
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<NoteBookData> list = new List<NoteBookData>();
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new NoteBookData();
                    obj.Order_Num = int.Parse(res.Rows[i]["ORDER_NUM"].ToString());
                    obj.ProvTypeCode = int.Parse(res.Rows[i]["PROV_TYPE_CODE"].ToString());
                    obj.Prov_Code = int.Parse(res.Rows[i]["PROV_CODE"].ToString());
                    obj.Notebook_Type_Code = int.Parse(res.Rows[i]["NOTEBOOK_TYPE_CODE"].ToString());
                    obj.Batch_Count = int.Parse(res.Rows[i]["BATCH_COUNT"].ToString());
                    obj.TransAction_Code = int.Parse(res.Rows[i]["TRANSACTION_CODE"].ToString());
                    try
                    {
                        obj.DeliverTypeCode = int.Parse(res.Rows[i]["DELIVER_CODE"].ToString());
                        obj.Messenger_Code = int.Parse(res.Rows[i]["MESSENGER_CODE"].ToString());
                        obj.Receipt_Num = int.Parse(res.Rows[i]["RECEIPT_NUM"].ToString());
                    }
                    catch { }
                    obj.Deliver_Person = res.Rows[i]["DELIVER_PERSON"].ToString();
                    obj.Deliver_National_ID = res.Rows[i]["DELIVER_NATIONAL_ID"].ToString();
                    obj.Request_Date = res.Rows[i]["RECEIPT_DATE"].ToString();
                    obj.Receipt_Name = res.Rows[i]["RECEIPT_NAME"].ToString();
                    obj.Receipt_Comp = res.Rows[i]["RECEIPT_COMP"].ToString();
                    obj.Created_By = res.Rows[i]["CREATEDBY"].ToString();
                    obj.Created_Date = res.Rows[i]["CREATEDDATE"].ToString();
                    obj.Request_Date = res.Rows[i]["REQUESTDATE"].ToString();

                    list.Add(obj);
                }
                return list;
            }
            return null;
        }
        public List<NoteBookData> SelectAllNotebook_ReportByCompany(int ProvCode, int PROV_TYPE_CODE)
        {
            string query = string.Format("select * from  DELIVERNOTEBOOKTB where PROV_CODE={0} and PROV_TYPE_CODE={1}", ProvCode, PROV_TYPE_CODE);
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<NoteBookData> list = new List<NoteBookData>();
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new NoteBookData();
                    obj.Order_Num = int.Parse(res.Rows[i]["ORDER_NUM"].ToString());
                    obj.ProvTypeCode = int.Parse(res.Rows[i]["PROV_TYPE_CODE"].ToString());
                    obj.Prov_Code = int.Parse(res.Rows[i]["PROV_CODE"].ToString());
                    obj.Notebook_Type_Code = int.Parse(res.Rows[i]["NOTEBOOK_TYPE_CODE"].ToString());
                    obj.Batch_Count = int.Parse(res.Rows[i]["BATCH_COUNT"].ToString());
                    obj.TransAction_Code = int.Parse(res.Rows[i]["TRANSACTION_CODE"].ToString());
                    try
                    {
                        obj.DeliverTypeCode = int.Parse(res.Rows[i]["DELIVER_CODE"].ToString());
                        obj.Messenger_Code = int.Parse(res.Rows[i]["MESSENGER_CODE"].ToString());
                        obj.Receipt_Num = int.Parse(res.Rows[i]["RECEIPT_NUM"].ToString());
                    }
                    catch { }
                    obj.Deliver_Person = res.Rows[i]["DELIVER_PERSON"].ToString();
                    obj.Deliver_National_ID = res.Rows[i]["DELIVER_NATIONAL_ID"].ToString();
                    obj.Request_Date = res.Rows[i]["RECEIPT_DATE"].ToString();
                    obj.Receipt_Name = res.Rows[i]["RECEIPT_NAME"].ToString();
                    obj.Receipt_Comp = res.Rows[i]["RECEIPT_COMP"].ToString();
                    obj.Created_By = res.Rows[i]["CREATEDBY"].ToString();
                    obj.Created_Date = res.Rows[i]["CREATEDDATE"].ToString();
                    obj.Request_Date = res.Rows[i]["REQUESTDATE"].ToString();

                    list.Add(obj);
                }
                return list;
            }
            return null;
        }

        //======================== aya
        public List<NoteBookData> SelectSpecificProvider(int Pr_Code)
        {
            string query = string.Format("select PR_CODE,PR_ANAME from SERV_PROVIDERS where PR_CODE=" + Pr_Code);
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<NoteBookData> list = new List<NoteBookData>();
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new NoteBookData();
                    obj.Prov_Code = int.Parse(res.Rows[i]["PR_CODE"].ToString());
                    obj.Prov_Name = res.Rows[i]["PR_ANAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }

        //new final
        public List<NoteBookData> SelectAllProviderNames(string input,int ProvideCode)
        {
            string query = string.Format("select PR_CODE,PR_ANAME from SERV_PROVIDERS where pr_code like '%" + input + "%' or pr_aname like '%" + input + "%'and PRV_TYPE=" + ProvideCode +"");
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<NoteBookData> list = new List<NoteBookData>();
            NoteBookData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new NoteBookData();
                    obj.Prov_Code = int.Parse(res.Rows[i]["PR_CODE"].ToString());
                    obj.Prov_Name = res.Rows[i]["PR_ANAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }
    }
}
