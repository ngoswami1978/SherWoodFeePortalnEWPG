using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SherWoodFeePortal.DTO;
using SherWoodFeePortal.BLL;

namespace SherWoodFeePortal
{
    public partial class FrmResponseHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PaymentBasket ObjBasket;
            UserBLL objBll = new UserBLL();
            try
            {
                
                
                string[] merc_hash_vars_seq;
                string merc_hash_string = string.Empty;
                string merc_hash = string.Empty;
                string order_id = string.Empty;
                //string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
                string hash_seq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10";
                

                if (Request.Form["status"] == "success")
                {

                    merc_hash_vars_seq = hash_seq.Split('|');
                    Array.Reverse(merc_hash_vars_seq);
                    merc_hash_string = ConfigurationManager.AppSettings["SALT"] + "|" + Request.Form["status"];


                    foreach (string merc_hash_var in merc_hash_vars_seq)
                    {
                        merc_hash_string += "|";
                        merc_hash_string = merc_hash_string + (Request.Form[merc_hash_var] != null ? Request.Form[merc_hash_var] : "");

                    }
                    merc_hash = Generatehash512(merc_hash_string).ToLower();

                    //COMMENT in 02-Jan-2017
                    //if (merc_hash.ToString() != Request.Form["hash"].ToString())
                    //{
                    //    //Value didn't match that means some paramter value change between transaction 
                    //    ObjBasket = new PaymentBasket();
                    //    //Utility.SendEmail("");
                    //    ObjBasket.orderid = Request.Form["txnid"];
                    //    ObjBasket.roll_no = Utility.GetSafestring(Session["RollNo"]);
                    //    ObjBasket.ramount = Request.Form["amount"];
                    //    ObjBasket.finyear = "2017";
                    //    ObjBasket.paymode = Request.Form["mode"];
                    //    ObjBasket.status = Request.Form["status"];
                    //    objBll.SavePaymentBasket(ObjBasket);
                    //    Response.Write("Hash value did not matched");

                    //}
                    //else
                    //{
                    //    //if hash value match for before transaction data and after transaction data
                    //    //that means success full transaction  , see more in response
                    //    order_id = Request.Form["txnid"];
                    //    ObjBasket = new PaymentBasket();
                    //    ObjBasket.orderid = Request.Form["txnid"];
                    //    ObjBasket.roll_no = Utility.GetSafestring(Session["RollNo"]);
                    //    ObjBasket.ramount = Request.Form["amount"];
                    //    ObjBasket.finyear = "2017";
                    //    ObjBasket.paymode = Request.Form["mode"];
                    //    ObjBasket.status = Request.Form["status"];
                    //    objBll.SavePaymentBasket(ObjBasket);


                    //    //Response.Write("value matched");
                    //    Response.Write("Order Placed " + order_id);

                    //    //Hash value did not matched
                    //}

                    order_id = Request.Form["txnid"];
                    ObjBasket = new PaymentBasket();
                    
                    ObjBasket.orderid = Request.Form["txnid"];
                    ObjBasket.roll_no = Utility.GetSafestring(Session["RollNo"]);
                    ObjBasket.ramount = Utility.GetSafestring(Session["subtotal"]);
                    ObjBasket.pgamount = Request.Form["amount"];
                    ObjBasket.PG_TYPE = Request.Form["PG_TYPE"];
                    ObjBasket.bank_ref_num = Request.Form["bank_ref_num"];
                    ObjBasket.additionalCharges = Request.Form["additionalCharges"];
                    ObjBasket.finyear = "2017";
                    ObjBasket.paymode = Request.Form["mode"];
                    ObjBasket.status = Request.Form["status"];
                    ObjBasket.name = Utility.GetSafestring(Session["Name"]);
                    ObjBasket.house =Utility.GetSafestring(Session["House"]);
                    ObjBasket.classsection = Utility.GetSafestring(Session["Classsection"]);
                    ObjBasket.fatheremail = Utility.GetSafestring(Session["Fatheremail"]);

                    try
                    {
                        ObjBasket.ramountWord = Utility.NumberToText(int.Parse(Math.Round(Decimal.Parse(ObjBasket.ramount)).ToString()), false);
                    }
                    catch (FormatException)
                    {}
                    
                    ObjBasket.remarks = "";
                    ObjBasket.mailtemplate = Utility.GetSafestring(Session["MailTemplate"]);
                    ObjBasket.mailtemplate = GenerateMailTemplate(ObjBasket);
                    objBll.SavePaymentBasket(ObjBasket);

                    try
                    {
                        Utility.SendEmail(ObjBasket, Utility.GetSafestring(Session["RollNo"]));
                    }
                    catch (Exception Ex)
                    { }                    

                    Response.Write("Transaction done successfully.</br>");
                    Response.Write("<a href='frmhome.aspx'>Click here to go sherwood online portal!<a>");
                }

                else
                {
                    order_id = Request.Form["txnid"];
                    ObjBasket = new PaymentBasket();
                    ObjBasket.orderid = Request.Form["txnid"];
                    ObjBasket.roll_no = Utility.GetSafestring(Session["RollNo"]);
                    ObjBasket.ramount = Utility.GetSafestring(Session["subtotal"]);
                    ObjBasket.pgamount = Request.Form["amount"];
                    ObjBasket.PG_TYPE = Request.Form["PG_TYPE"];
                    ObjBasket.bank_ref_num = Request.Form["bank_ref_num"];
                    ObjBasket.additionalCharges = Request.Form["additionalCharges"];
                    ObjBasket.finyear = "2017";
                    ObjBasket.paymode = Request.Form["mode"];
                    ObjBasket.status = Request.Form["status"];
                    objBll.SavePaymentBasket(ObjBasket);

                    Response.Write("Transaction Failure!");
                    Response.Write("<a href='frmhome.aspx'>Click here to go sherwood online portal!<a>");
                    // osc_redirect(osc_href_link(FILENAME_CHECKOUT, 'payment' , 'SSL', null, null,true));

                }
            }

            catch (Exception ex)
            {
                ObjBasket = new PaymentBasket();

                ObjBasket.orderid = Request.Form["txnid"];
                ObjBasket.roll_no = Utility.GetSafestring(Session["RollNo"]);
                ObjBasket.ramount = Utility.GetSafestring(Session["subtotal"]);
                ObjBasket.pgamount = Request.Form["amount"];
                ObjBasket.finyear = "2017";
                ObjBasket.PG_TYPE = Request.Form["PG_TYPE"];
                ObjBasket.bank_ref_num = Request.Form["bank_ref_num"];
                ObjBasket.additionalCharges = Request.Form["additionalCharges"];

                ObjBasket.paymode = Request.Form["mode"];
                ObjBasket.status = Request.Form["status"];
                objBll.SavePaymentBasket(ObjBasket);


                Response.Write("<span style='color:red'>" + ex.Message + "</span>");
                Response.Write("<a href='frmhome.aspx'>Click here to go sherwood online portal!<a>");
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
        }

        /// <summary>
        /// Generate HASH for encrypt all parameter passing while transaction
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Generatehash512(string text)
        {

            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

        }


        /// <summary>
        /// Generate MAIL TEMPLATE 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string GenerateMailTemplate(PaymentBasket objPayBasket)
        {
            string MailTemplate;
            string AuthorizedSign;

            AuthorizedSign = ConfigurationManager.AppSettings["AuthorizedSign"];
            MailTemplate = objPayBasket.mailtemplate;
            MailTemplate = MailTemplate.Replace("[[Bill_No]]", objPayBasket.orderid);
            MailTemplate = MailTemplate.Replace("[[Roll_No]]", objPayBasket.roll_no);
            MailTemplate = MailTemplate.Replace("[[Name]]", objPayBasket.name);
            MailTemplate = MailTemplate.Replace("[[Class]]", objPayBasket.classsection);
            MailTemplate = MailTemplate.Replace("[[House]]", objPayBasket.house);
            MailTemplate = MailTemplate.Replace("[[Amount_Word]]", objPayBasket.ramountWord);
            MailTemplate = MailTemplate.Replace("[[Amount]]", objPayBasket.ramount);
            MailTemplate = MailTemplate.Replace("[[AuthorizedSign]]", AuthorizedSign.ToString());

            return MailTemplate;
        }
    }
}