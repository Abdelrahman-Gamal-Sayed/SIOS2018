using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace WpfApplication2.BusinessLayer.Messenger_Confirmation
{
    class ConfirMessData
    {

     //   [DisplayName("اسم المندوب")]
      //  public string MessName { set; get; }

        [DisplayName("كود الطلب")]
        public int ReqCode { get; set; }//
        [DisplayName("اسم الشركة")]
        public string CompanyName { get; set; }//
     //   [DisplayName("اسم الفرع")]
     //   public string Branch { get; set; }
      //  [DisplayName("اسم المنطقة")]
      //  public string Area { get; set; }
      //  [DisplayName("الشخص المتصل")]
      //  public string ContactPerson { get; set; }
     //   [DisplayName("العنوان")]
     //   public string Address { get; set; }
     //   [DisplayName("رقم التليفون")]
     //   public string Phone { set; get; }
        [DisplayName("القسم")]
        public string Dept { set; get; }//
        [DisplayName("اليوم")]
        public DateTime Date { set; get; }//
      //  [DisplayName("نوع المندوب")]
     //   public string MessengerType { set; get; }

        [DisplayName("اسباب الزيارة")]
        public string RequestResons { set; get; }//

    //    [DisplayName("تم")]
    //    public string Done { set; get; }
     //   [DisplayName("Comments")]
    //    public string Comments { set; get; }
    }
}
