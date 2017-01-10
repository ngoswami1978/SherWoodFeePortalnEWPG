using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SherWoodFeePortal.DTO
{
    public class User
    {
        public string UserName { get; set; }
        public string StudentRollno { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Mobno { get; set; }
        public DateTime LastLoginDateTime { get; set; }
    }

    public class PaymentBasket
    {
        public string roll_no { get; set; }
        public string ramount { get; set; }
        public string finyear { get; set; }
        public string status { get; set; }
        public string paymode { get; set; }
        public string orderid { get; set; }
        public string pgamount { get; set; }
        public string PG_TYPE { get; set; }
        public string bank_ref_num { get; set; }
        public string additionalCharges { get; set; }
        
        public string name { get; set; }
        public string house { get; set; }
        public string classsection { get; set; }
        
        public string ramountWord { get; set; }
        public string remarks { get; set; }
        public string mailtemplate { get; set; }
        public string fatheremail { get; set; }        
    }

    public class PayModeTaxes
    {
        public string CCard { get; set; }
        public string DCard { get; set; }
        public string NetBanking { get; set; }        
    }

    public class BillOrders
    {
        public string ID { get; set; }
        public string Name { get; set; }        
    }

}