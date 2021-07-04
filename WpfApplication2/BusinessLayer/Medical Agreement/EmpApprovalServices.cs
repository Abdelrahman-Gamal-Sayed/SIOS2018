using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace WpfApplication2.BusinessLayer.Medical_Agreement
{
    class EmpApprovalServices
    {
        DBManager db = new DBManager();
        public List<EmpApprovalData> SelectAllApprovals(string CardNo, string PatiantName)
        {
            string query = string.Format("SELECT * FROM (SELECT distinct * FROM V_APPROVAL where CARD_NO='{0}' or PATIENT_NAME like '{1}' order by DATE_SEND desc) where rownum<=5", CardNo, PatiantName);
            DataTable res = db.ExecuteSelect(query).Tables[0];
            List<EmpApprovalData> list = new List<EmpApprovalData>();
            EmpApprovalData obj;
            if (res != null && res.Rows.Count > 0)
            {
                for (int i = 0; i < res.Rows.Count; i++)
                {
                    obj = new EmpApprovalData();
                    obj.PatiantID = int.Parse(res.Rows[i]["PATIENT_ID"].ToString());
                    obj.PatiantName = res.Rows[i]["PATIENT_NAME"].ToString();
                 //   obj.CardNum = res.Rows[i]["CARD_NO"].ToString();
                    obj.DateRecieved = res.Rows[i]["DATE_RECIVE"].ToString();
                    obj.DateSend = res.Rows[i]["DATE_SEND"].ToString();
                    // obj.Email = res.Rows[i]["EMAIL"].ToString();
                    obj.EndDate = res.Rows[i]["EN_DATE"].ToString();
                    obj.Fax = res.Rows[i]["FAX"].ToString();
                    obj.Remarks = res.Rows[i]["REMARKS"].ToString();
                    obj.StartDate = res.Rows[i]["ST_DATE"].ToString();
                    obj.CompanyId = res.Rows[i]["COMP_ID"].ToString();
                    obj.ApprovalType = res.Rows[i]["APROV_TYP"].ToString();
                    if (res.Rows[i]["APROV_TYP"].ToString() == "10")
                        obj.ApprovalType = "Inpatient";
                    else if (res.Rows[i]["APROV_TYP"].ToString() == "20")
                        obj.ApprovalType = "Outpatient services";
                    else if (res.Rows[i]["APROV_TYP"].ToString() == "30")
                        obj.ApprovalType = "Dental services outpatient";
                    else if (res.Rows[i]["APROV_TYP"].ToString() == "40")
                        obj.ApprovalType = "Medications outpatient";
                    else if (res.Rows[i]["APROV_TYP"].ToString() == "50")
                        obj.ApprovalType = "Maternity";
                    else if (res.Rows[i]["APROV_TYP"].ToString() == "60")
                        obj.ApprovalType = "Optical";

                    if (res.Rows[i]["APROV_REPLY"].ToString()=="Y")
                    obj.ApprovalReply ="Yes";
                    else
                        obj.ApprovalReply = "No";

                    obj.ApprovalNum = res.Rows[i]["APROV_NO"].ToString();
                    obj.ApprovalDoctor = res.Rows[i]["APROV_DOCTOR"].ToString();
                    obj.AppovalProvider = res.Rows[i]["APROV_PROVIDER"].ToString();
                    obj.Age = res.Rows[i]["AGE"].ToString();
                    list.Add(obj);
                }
                return list;
            }
            return null;
        }

    }
}
