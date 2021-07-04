using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
namespace WpfApplication2.BusinessLayer.Printing
{
    class PrintingServices
    {
        DBManager db = new DBManager();
        //---------------select all Employeees---------------------------------
        public List<PrintingData> SelectAllEmployees(string DateFrom, string DateTo)
        {

            try
            {
                string query = string.Format("select c.EMP_CODE,c.CONTRACT_NO, c.EMP_ENAME,c.EMP_ENAME_ST,c.EMP_ENAME_SC,c.EMP_ENAME_TH,vc.C_ANAME ,c.C_COMP_ID,c.CARD_ID from app.COMP_EMPLOYEESS c join app.V_COMPANIES vc on c.C_COMP_ID=vc.C_COMP_ID where  SPECIFIC_DATE between '{0}' and '{1}'", DateFrom, DateTo);
                DataTable res = db.ExecuteSelect2(query).Tables[0];//
                List<PrintingData> list = new List<PrintingData>();
                PrintingData obj;
                if (res != null && res.Rows.Count > 0)
                {
                    for (int i = 0; i < res.Rows.Count; i++)
                    {
                        obj = new PrintingData();
                        obj.EmpID = res.Rows[i]["CARD_ID"].ToString();

                        obj.EmpFirstName = res.Rows[i]["EMP_ENAME_ST"].ToString();
                        obj.EmpSecondName = res.Rows[i]["EMP_ENAME_SC"].ToString();
                        obj.EmpThirdName = res.Rows[i]["EMP_ENAME_TH"].ToString();
                        obj.ContractNo = int.Parse(res.Rows[i]["CONTRACT_NO"].ToString());
                        // obj.EmpName = res.Rows[i]["EMP_ENAME"].ToString();
                        obj.CompanyName = res.Rows[i]["C_ANAME"].ToString();
                        obj.CompID = res.Rows[i]["C_COMP_ID"].ToString();

                        list.Add(obj);
                    }
                    return list;
                }
            }
            catch (OracleException ex)
            {
                string ss = ex.Message;
            }
            finally { }
            return null;
        }

        //--------------- select all Employeees for Receiving Cards ---------------------------------
        //torb12-8 Done
        public List<PrintingData> SelectAllEmployees_For_ReceivingCards(string CompanyId)
        {
            string query = string.Format("select * from PRINTINGXXXX where DELIVERSTATE='{0}' and COMPID={1}", 'Y', CompanyId);
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<PrintingData> list = new List<PrintingData>();
            PrintingData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new PrintingData();
                    obj.EmpID = res.Rows[i]["CustID"].ToString();
                    obj.EmpName = res.Rows[i]["CustName"].ToString();
                    obj.CompanyName = res.Rows[i]["CompanyName"].ToString();
                    obj.RecievedName = res.Rows[i]["RecievedName"].ToString();
                    obj.ReceivedDate = res.Rows[i]["ReceivedDate"].ToString();
                    obj.CompID = res.Rows[i]["CompID"].ToString();
                    obj.PrintingType = res.Rows[i]["PrintingType"].ToString();
                    obj.ContractNo = int.Parse(res.Rows[i]["ContractNo"].ToString());
                    obj.ReceivedDate = DateTime.Now.ToString();
                    obj.PrintedBy = res.Rows[i]["PRINTEDBY"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }
        //--------------- select all Employeees for Receiving Cards ---------------------------------
        //torb7-8
        public List<PrintingData> SelectAllEmployees_For_ReceivingCardsByDate(string Date)
        {
            string query = string.Format("select * from PRINTINGXXXX where DELIVERSTATE='{0}' and PRINTEDDATE = '{1}'", 'N', Date);
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<PrintingData> list = new List<PrintingData>();
            PrintingData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new PrintingData();
                    obj.EmpID = res.Rows[i]["CustID"].ToString();
                    obj.EmpName = res.Rows[i]["CustName"].ToString();
                    obj.CompanyName = res.Rows[i]["CompanyName"].ToString();
                    obj.RecievedName = res.Rows[i]["RecievedName"].ToString();
                    obj.ReceivedDate = res.Rows[i]["ReceivedDate"].ToString();
                    obj.CompID = res.Rows[i]["CompID"].ToString();
                    obj.PrintingType = res.Rows[i]["PrintingType"].ToString();
                    obj.ContractNo = int.Parse(res.Rows[i]["ContractNo"].ToString());
                    obj.PrintedBy = res.Rows[i]["PRINTEDBY"].ToString();
                    obj.ReceivedDate = DateTime.Now.ToString();

                    list.Add(obj);
                }
                return list;
            }
            return null;
        }

        //-----------select all Companies for receiving cards--------------------------------
        public List<PrintingData> SelectAllCompaniesForReceivingCards()
        {
            string query = "select distinct C_COMP_ID,C_ANAME,ADDRESS1,TEL1,TEL2 from V_COMPANIES";

            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<PrintingData> list = new List<PrintingData>();
            PrintingData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new PrintingData();
                    obj.CompID = res.Rows[i]["C_COMP_ID"].ToString();
                    obj.CompanyName = res.Rows[i]["C_ANAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }

        public List<PrintingData> SelectCompanyForReceivingCard(string name)
        {
            string query = string.Format("select distinct C_COMP_ID,C_ENAME,ADDRESS1,TEL1,TEL2 from V_COMPANIES where c_ename='{0}'", name);

            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<PrintingData> list = new List<PrintingData>();
            PrintingData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new PrintingData();
                    obj.CompID = res.Rows[i]["C_COMP_ID"].ToString();
                    obj.CompanyName = res.Rows[i]["C_ENAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }
        //-----------select all Companies when search--------------------------------
        public List<PrintingData> SelectAllCompaniesForSearch(string companyName, string CompanyCode)
        {
            string query = "select distinct C_COMP_ID,C_ANAME,ADDRESS1,TEL1,TEL2 from V_COMPANIES where lower(C_ANAME) like '%" + companyName + "%' or C_COMP_ID like '%" + CompanyCode + "'";

            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<PrintingData> list = new List<PrintingData>();
            PrintingData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new PrintingData();
                    obj.CompID = res.Rows[i]["C_COMP_ID"].ToString();
                    obj.CompanyName = res.Rows[i]["C_ANAME"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }
        //----------------select Emp by Id--------------------
        //torb7-8
        public PrintingData SelectEmpById(string EmpID, int ContarctID)
        {
            string query = string.Format("SELECT * from PRINTINGXXXX where CustID='{0}' and CONTRACTNO={1}", EmpID, ContarctID);
            DataTable res = db.ExecuteSelect(query).Tables[0];
            PrintingData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new PrintingData();
                    obj = new PrintingData();
                    obj.EmpID = dr["CustID"].ToString();
                    obj.EmpName = dr["CustName"].ToString();
                    obj.CompanyName = dr["CompanyName"].ToString();
                    obj.PrintingType = dr["PrintingType"].ToString();
                    obj.PrintNo = dr["PRINT_NO"].ToString();
                    return obj;
                }
            }
            return null;
        }

        //=========================update======================================
        //----------------Update Emp in printing-----------------------
        //torb7-8
        public int UpdateEmp_In_Printing(PrintingData obj, string id)
        {
            string query = string.Format("UPDATE PRINTINGXXXX set CustName='{0}',CompanyName='{1}',PrintingType='{2}',CompID={3},PRINTEDBY='{4}',PRINTEDDATE='{5}' Where CustID='{6}'", obj.EmpName, obj.CompanyName, obj.PrintingType, obj.CompID, obj.PrintedBy, obj.PrintedDate, id);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        //torb7-8
        public int UpdateEmp_In_Printing_SecondTime(PrintingData obj, string id, int ContractNo)
        {
            //DB db = new DB();
            //db.RunNonQuery("update PINTINGTBZ set CUSTNAME='"+ obj.EmpName + "',COMPANYNAME='"+ obj.CompanyName + "',PRINTINGTYPE='"+ obj.PrintingType + "',CASES='2',PRINT_COUNT='"+ obj.PrintingCount + "',PRINTEDBY='"+User.Name+ "',PRINTEDDATE=sysdate where CUSTID='"+ id + "' and CONTRACTNO='"+ ContractNo + "'","تم التعديل");
            string query = string.Format("UPDATE PRINTINGXXXX set CUSTNAME='{0}',COMPANYNAME='{1}',PRINTINGTYPE='{2}',CASES='{3}',PRINT_COUNT={4},PRINTEDBY='{5}',PRINTEDDATE='{6}' Where CUSTID='{7}' and CONTRACTNO={8}", obj.EmpName, obj.CompanyName, obj.PrintingType, "2", obj.PrintingCount, obj.PrintedBy, obj.PrintedDate, id, ContractNo);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        //torb7-8
        public int UpdateEmp_In_Printing_CardReceiving(PrintingData obj, string id)
        {
            string query = string.Format("UPDATE PRINTINGXXXX set DeliverState='{0}',ReceivedDate='{1}',RecievedName='{2}' Where CustID='{3}'", obj.ReceivedState, obj.ReceivedDate, obj.RecievedName, id);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }


        /////////////////////////////Print first time//////////////////////////////
        //----------------SelectMaxPrintNO-------------------------//
        //torb7-8 Move
        public string SelectMaxPrintingId()
        {
            string query = "SELECT MAX(PRINT_NO) from PRINTINGXXXX";
            object affected = db.ExecutSelectMax(query);
            return affected.ToString();
        }

        //----------------SelectPrint_Count-------------------------//
        //torb7-8
        public string SelectPrintingCount(string CustID, int ContractNo)
        {
            try
            {
                string query = string.Format("SELECT PRINT_COUNT from PRINTINGXXXX where CustID='{0}' and CONTRACTNO={1}", CustID, ContractNo);
                object affected = db.ExecutSelectMax(query);
                return affected.ToString();
            }
            catch
            {
                return null;
            }

        }
        //=========================Insert=========================================
        //----------------Insert Emp In Printing------------------//
        //torb7-8
        public int InsertEmp_In_Printing(PrintingData obj)
        {
            string query = string.Format("INSERT INTO PRINTINGXXXX(PRINT_NO,CustID,CustName,CompanyName,CONTRACTNO,PrintingType,Cases,CompID,DeliverState,PRINT_COUNT,PRINTEDDATE,PRINTEDBY) values({0},'{1}','{2}','{3}',{4},'{5}','{6}',{7},'{8}',{9},'{10}','{11}')", obj.PrintNo, obj.EmpID, obj.EmpName, obj.CompanyName, obj.ContractNo, obj.PrintingType, "1", obj.CompID, "N", "1", DateTime.Now.ToShortDateString(), obj.PrintedBy);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }

        ///////////////////////////print second time//////////////////////////////
        //torb7-8
        public List<PrintingData> SelectAllContracts()
        {
            string query = "SELECT distinct CONTRACTNO from PRINTINGXXXX";
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<PrintingData> list = new List<PrintingData>();
            PrintingData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new PrintingData();
                    obj.ContractNo = int.Parse(res.Rows[i]["ContractNo"].ToString());
                    list.Add(obj);
                }
                return list;
            }
            return null;

        }
        //----------------select  Reason BY ID--------------------
        public PrintingData SelectResonById(int ID)
        {
            string query = string.Format("SELECT * from REASONS where REAS_CODE={0}", ID);
            DataTable res = db.ExecuteSelect(query).Tables[0];
            PrintingData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new PrintingData();
                    obj = new PrintingData();
                    obj.EmpID = dr["REAS_CODE"].ToString();
                    obj.EmpName = dr["REAS_ANAME"].ToString();
                    obj.CompanyName = dr["REAS_ENAME"].ToString();
                    return obj;
                }
            }
            return null;
        }
        //----------------Insert Emp In Printing------------------//
        public int InsertReson_In_PRINT_REAS(PrintingData obj)
        {
            string query = string.Format("INSERT INTO PRINT_REAS(PRINT_NO,REAS_CODE,PRINT_COUNT) values({0},{1},{2})", obj.PrintNo, obj.ResonCode, obj.PrintingCount);
            int affected = db.ExecuteNonQuery(query);
            return affected;
        }
        /////////////////////////ReceivedCards////////////////////////////////////
        //----------------select Emp by Id--------------------
        public PrintingData SelectEmpById_For_Receiving(string EmpID, int ContarctID)
        {
            string query = string.Format("SELECT * from PRINTINGXXXX where CustID='{0}' and CONTRACTNO={1}", EmpID, ContarctID);
            DataTable res = db.ExecuteSelect(query).Tables[0];
            PrintingData obj;
            if (res != null && res.Rows.Count > 0)
            {
                foreach (DataRow dr in res.Rows)
                {
                    obj = new PrintingData();
                    obj = new PrintingData();
                    obj.EmpID = dr["CustID"].ToString();
                    obj.EmpName = dr["CustName"].ToString();
                    obj.CompanyName = dr["CompanyName"].ToString();
                    obj.PrintingType = dr["PrintingType"].ToString();
                    obj.PrintNo = dr["PRINT_NO"].ToString();
                    obj.PrintedBy = dr["PRINTEDBY"].ToString();
                    return obj;
                }
            }
            return null;
        }
        //----------------SelectMaxContractNO-------------------------//
        //torb7-8
        public string SelectMaxContractNo(string EmpId)
        {
            string query = string.Format("SELECT MAX(CONTRACTNO) from PRINTINGXXXX where CUSTID='{0}'", EmpId);
            object affected = db.ExecutSelectMax(query);
            return affected.ToString();
        }

        /////////////////////// Info About Card /////////////////////////////////////
        //--------------- select all Employeees for Receiving Cards ---------------------------------
        //torb7-8
        public List<PrintingData> SelectAllEmployees_For_SearchAboutCard(string CardNum)
        {
            string query = string.Format("select * from PRINTINGXXXX where CUSTID='{0}'", CardNum);
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<PrintingData> list = new List<PrintingData>();
            PrintingData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new PrintingData();
                    obj.EmpID = res.Rows[i]["CustID"].ToString();
                    obj.EmpName = res.Rows[i]["CustName"].ToString();
                    obj.CompanyName = res.Rows[i]["CompanyName"].ToString();
                    obj.RecievedName = res.Rows[i]["RecievedName"].ToString();
                    obj.ReceivedDate = res.Rows[i]["ReceivedDate"].ToString();
                    obj.CompID = res.Rows[i]["CompID"].ToString();
                    obj.PrintingType = res.Rows[i]["PrintingType"].ToString();
                    obj.ContractNo = int.Parse(res.Rows[i]["ContractNo"].ToString());
                    obj.ReceivedDate = DateTime.Now.ToString();
                    obj.PrintedBy = res.Rows[i]["PRINTEDBY"].ToString();
                    obj.PrintingCount = res.Rows[i]["PRINT_COUNT"].ToString();
                    obj.ReceivedState = res.Rows[i]["DELIVERSTATE"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }
    }
}
