using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApplication2
{
    public class ReallocationData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { set; get; }
        public string CARD_ID { set; get; }
        public string EMP_ANAME_ST { set; get; }
        public string EMP_ANAME_SC { set; get; }
        public string EMP_ANAME_TH { set; get; }
        public int PRV_TYPE { set; get; }
        public string TYP_ANAME { set; get; }
        public int Prov_Code { set; get; }
        public string Prov_Name { get; set; }
        public int LETTER_ID { set; get; }
        public string SERV_DATE { set; get; }
        public string SERV_NAME { set; get; }
        public string DIAGNOSIS { set; get; }
        public int AGREAMENT_COST { set; get; }
        public string CREATED_BY { set; get; }
        public string CREATED_DATE { set; get; }
        public decimal SRV_PRICE { set; get; }
        public string Nots { set; get; }
        public string UPDATED_BY { set; get; }
        public string UPDATED_DATE { set; get; }
        public int LetterCode { set; get; }
        public string LetterName { set; get; }
        public string REPLY { set; get; }
        public byte[] REPLY_PICTURE { set; get; }
        public string FullCode { set; get; }
    }
}
