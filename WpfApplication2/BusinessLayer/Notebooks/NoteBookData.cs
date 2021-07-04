using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace WpfApplication2.BusinessLayer.Notebooks
{
    class NoteBookData
    {
        public int TransAction_Code { get; set; }
        [DisplayName("العدد المطلوب")]
        public int Batch_Count { get; set; }
        public int Serial_From { get; set; }
        public int Serial_To { get; set; }
        public int SerialCode { set; get; }
        public int ProvTypeCode { get; set; }

        [DisplayName("نوع مقدم الخدمة ")]
        public string Type_Name { get; set; }

        public int Prov_Code { get; set; }

        public int Key { get; set; }

        [DisplayName("اسم مقدم الخدمة ")]
        public string Prov_Name { get; set; }
        //---------Doctor info------------------
        public string Speciality { get; set; }
        public int Doc_Spec_Code { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }

        //-----------Deliver Types----------
        public int Deliver_Code { get; set; }
        public string Deliver_Type { get; set; }
        //------------messengers---------------
        public int Id { get; set; }
        public string Name { get; set; }
        public string CardNum { get; set; }
        //-----------Notebook Type--------------
        public int Notebook_Type_Code { get; set; }

        [DisplayName("نوع الدفتر ")]
        public string NotebookName { get; set; }

        //-------------Notebook Serial-------------
        public int Deliver_Serial { get; set; }
        //-----------------------------------------//
        [DisplayName("نوع  التوصيل")]
        public int DeliverTypeCode { get; set; }

        public int Speciality_Code { get; set; }
        [DisplayName("الشخص المستلم")]
        public string Deliver_Person { get; set; }
        [DisplayName("رقم البطاقة")]
        public string Deliver_National_ID { get; set; }
        public string Deliver_National_ID_Img { get; set; }

        public int Messenger_Code { get; set; }

        [DisplayName("رقم الايصال")]
        public Int64 Receipt_Num { get; set; }

        public string Receipt_Img { get; set; }
        [DisplayName("تاريخ وصول الطلب")]
        public string Receipt_Date { get; set; }
        [DisplayName("اسم المستلم")]
        public string Receipt_Name { get; set; }
        [DisplayName("اسم شركة البريد")]
        public string Receipt_Comp { get; set; }

        [DisplayName("رقم الطلب")]
        public int Order_Num { get; set; }

        //-----------created-------------------

        public string Created_Date { get; set; }
        public string Created_By { get; set; }

        ////////////////////----Request_Notebook frm -----///////////////////////

        [DisplayName("تاريخ الطلب")]
        public string Request_Date { get; set; }
        public string ConfrimRequest { get; set; }
     
    }
}
