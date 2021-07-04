using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace WpfApplication2.BusinessLayer
{
    class MessangerData
    {
       [ DisplayName("كود المندوب")]
        public int Id { get; set; }
        [DisplayName("اسم المندوب")]
        public string Name { get; set; }
        [DisplayName("اسم المستخدم")]
        public string UserName { get; set; }
        [DisplayName("الباسورد")]
        public string Password { get; set; }
        [DisplayName("تاريخ الميلاد")]
        public string DateOfBirth { get; set; }
        [DisplayName("العنوان")]
        public string Address { get; set; }
        [DisplayName("نوع المندوب")]
        public string Type { set; get; }
        [DisplayName("رقم البطاقة")]
        public string CardNum { set; get; }
    }
}
