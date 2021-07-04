using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace WpfApplication2.BusinessLayer.Printing
{
    class PrintingData
    {
        public string PrintNo { get; set; }
        public string PrintingCount { get; set; }
        public string EmpID { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpSecondName { get; set; }
        public string EmpThirdName { get; set; }
        public int ContractNo { get; set; }
        public string EmpName { get; set; }
        public string CompID { get; set; }
        public string CompanyName { get; set; }
        public string PrintingType { get; set; }
        public string PrintedBy { get; set; }
        public string PrintedDate { get; set; }
        public string Cases { get; set; }
        public int ResonCode { get; set; }
        public string ReceivedDate { get; set; }
        public string ReceivedState { set; get; }
        [DisplayName("اسم المستلم")]
        public string RecievedName { get; set; }
    }
}
