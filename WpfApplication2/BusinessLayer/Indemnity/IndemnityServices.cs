using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace WpfApplication2.BusinessLayer.Indemnity
{
    class IndemnityServices
    {
        DBManager db = new DBManager();
        public List<IndemnityData> SelectAllIndemtiesForCardNoSearch(string DateFrom, string DateTo, string CardNum)
        {
            string query = string.Format("select DISTINCT CLAIM_NO1,CARD_NO,CLAIM_DATE,CLAIM_SERV_AMOUNT,CLAIM_DEDUCTIONS,CLAIM_DED,serv_amount_apr,CLAIM_AMOUNT_PAYED,CLAIM_NET,CREATED_DATE,dia_ename,dia_notes/*,CREATED_BY*/  from ME_AUB where prv_no=99999 and CLAIM_DATE between '{0}' and '{1}' and (CLAIM_NO1={2} or CARD_NO='{3}')", DateFrom, DateTo, CardNum, CardNum);
            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<IndemnityData> list = new List<IndemnityData>();
            IndemnityData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new IndemnityData();
                    obj.CARD_NO = res.Rows[i]["CARD_NO"].ToString();
                    obj.CLAIM_NO1 = res.Rows[i]["CLAIM_NO1"].ToString();
                    obj.CLAIM_DATE = res.Rows[i]["CLAIM_DATE"].ToString();
                  //  obj.CLAIM_AMOUNT = res.Rows[i]["CLAIM_AMOUNT"].ToString();
                   // obj.CLAIM_ITEM_DED = res.Rows[i]["CLAIM_ITEM_DED"].ToString();
                   // obj.CLAIM_DED = res.Rows[i]["CLAIM_DED"].ToString();
                  //  obj.CLAIM_AMOUNT_APROV = res.Rows[i]["CLAIM_AMOUNT_APROV"].ToString();
                  //  obj.CLAIM_AMOUNT_PAYED = res.Rows[i]["CLAIM_AMOUNT_PAYED"].ToString();
                    obj.CLAIM_NET = res.Rows[i]["CLAIM_NET"].ToString();
                    obj.CREATED_DATE = res.Rows[i]["CREATED_DATE"].ToString();
                 //   obj.CREATED_BY = res.Rows[i]["CREATED_BY"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }
        public List<IndemnityData> SelectAllIndemtiesForCompanyCodeSearch(string DateFrom, string DateTo, int COMP_ID)
        {
            string query = "";
            if (User.Type=="hr")
         query = string.Format("select DISTINCT CLAIM_NO1,CARD_NO,CLAIM_DATE,CLAIM_SERV_AMOUNT,CLAIM_DEDUCTIONS,CLAIM_DED,serv_amount_apr,CLAIM_AMOUNT_PAYED,CLAIM_NET,CREATED_DATE,dia_ename,dia_notes/*,CREATED_BY*/  from ME_AUB where prv_no=99999 and  CLAIM_NO1={2} or( CLAIM_DATE between '{0}' and '{1}' and COMP_ID={3})", DateFrom, DateTo, COMP_ID,User.CompanyID);
            else
                query = string.Format("select DISTINCT CLAIM_NO1,CARD_NO,CLAIM_DATE,CLAIM_SERV_AMOUNT,CLAIM_DEDUCTIONS,CLAIM_DED,serv_amount_apr,CLAIM_AMOUNT_PAYED,CLAIM_NET,CREATED_DATE,dia_ename,dia_notes/*,CREATED_BY*/  from ME_AUB where prv_no=99999 and  CLAIM_NO1={2} or( CLAIM_DATE between '{0}' and '{1}' and COMP_ID={2})", DateFrom, DateTo, COMP_ID);

            DataTable res = db.ExecuteSelect2(query).Tables[0];//
            List<IndemnityData> list = new List<IndemnityData>();
            IndemnityData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    //  CLAIM_NET
                    if (res.Rows[i]["CLAIM_NET"].ToString() != "")
                    {
                        obj = new IndemnityData();
                        obj.CARD_NO = res.Rows[i]["CARD_NO"].ToString();
                        obj.CLAIM_NO1 = res.Rows[i]["CLAIM_NO1"].ToString();
                        obj.CLAIM_DATE = res.Rows[i]["CLAIM_DATE"].ToString();
                        // obj.CLAIM_AMOUNT = res.Rows[i]["CLAIM_AMOUNT"].ToString();
                        // obj.CLAIM_ITEM_DED = res.Rows[i]["CLAIM_ITEM_DED"].ToString();
                        //  obj.CLAIM_DED = res.Rows[i]["CLAIM_DED"].ToString();
                        //    obj.CLAIM_AMOUNT_APROV = res.Rows[i]["CLAIM_AMOUNT_APROV"].ToString();
                        //    obj.CLAIM_AMOUNT_PAYED = res.Rows[i]["CLAIM_AMOUNT_PAYED"].ToString();
                        obj.CLAIM_NET = res.Rows[i]["DIA_NOTES"].ToString();
                        obj.CREATED_DATE = res.Rows[i]["CREATED_DATE"].ToString();
                        //    obj.CREATED_BY = res.Rows[i]["CREATED_BY"].ToString();
                        list.Add(obj);
                    }
                }
                return list;
            }
            return null;
        }
    
    }
}
