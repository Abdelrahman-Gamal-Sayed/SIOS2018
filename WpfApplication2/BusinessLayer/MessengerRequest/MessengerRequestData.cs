using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.BusinessLayer.MessengerRequest
{
    class MessengerRequestData
    {
        public int ReqCode { get; set; }
        public string CompanyName { get; set; }
        public string Branch { get; set; }
        public string Area { get; set; }
        public string CompanyCode { set; get; }
        public int Governorate_Code { set; get; }
        public string Governorate_Name { set; get; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Phone { set; get; }
        public string Dept { set; get; }
        public string Date { set; get; }
        public string MessengerType { set; get; }
        public string VIP { set; get; }
        public string RequestResons { set; get; }
        public string HoldDate { set; get; }




        public string Done { set; get; }
    }
}
