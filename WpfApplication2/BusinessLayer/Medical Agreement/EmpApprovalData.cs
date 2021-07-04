using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.BusinessLayer.Medical_Agreement
{
    class EmpApprovalData
    {
        public int PatiantID { get; set; }
        public string PatiantName { get; set; }
       // public string CardNum { get; set; }
        public string DateRecieved { get; set; }
        public string ApprovalType { get; set; }
        public string ApprovalNum { get; set; }
        public string Age { get; set; }
        public string DateSend { get; set; }
        //public string Email { get; set; }
        public string Fax { get; set; }
        public string AppovalProvider { get; set; }
        public string ApprovalReply { get; set; }
        public string ApprovalDoctor { get; set; }
        public string Remarks { get; set; }
        public string CompanyId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
