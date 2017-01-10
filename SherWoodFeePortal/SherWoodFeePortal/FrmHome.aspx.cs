using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SherWoodFeePortal.BLL;
using SherWoodFeePortal.DTO;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using System.Threading;
using System.IO;
using System.Xml;


namespace SherWoodFeePortal
{
    public partial class FrmHome : System.Web.UI.Page
    {
        public string action1 = string.Empty;
        public string hash1 = string.Empty;
        public string txnid1 = string.Empty;
        public PayModeTaxes objTaxes;
        public String Billsummery;
        public XmlDocument objRecPayment;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["islogin"] != null)
            {
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
                //Response.Cache.SetNoStore();

                if (Utility.GetSafestring(Session["islogin"]) == "1") // It means regular user
                {
                    //lblName.Text = "Mr." + "  " + Utility.GetSafestring(Session["Name"]);
                }
                else
                {
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    Response.Redirect("Logout.aspx", true);
                }
            }
            else
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("Logout.aspx", true);
            }          


            //set merchant key from web.config or AppSettings
            frmError.Visible = false;
            key.Value = ConfigurationManager.AppSettings["MERCHANT_KEY"];

            objTaxes = new PayModeTaxes();
            objTaxes.CCard = ConfigurationManager.AppSettings["CCTax"];
            objTaxes.DCard = ConfigurationManager.AppSettings["DCTax"];
            objTaxes.NetBanking = ConfigurationManager.AppSettings["NetBankingTax"];           


            //lblDueDate.Attributes.Add("style", "text-decoration:blink");                 

            if (!Page.IsPostBack)
            {
                //string script = "$(document).ready(function () { $('[id*=btnPayment]').click(); });";
                //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                //lblOpeningBalance.Text = "Opening Balance as on ";
                lblPeriod.Text = "Period-XX/XX/XXXX To XX/XX/XXXX";
                FillData();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
        }

        protected void FillData()
        {
            UserBLL objBll = null;
            User objUser = null;
            DataSet oDS = null;

            Decimal TotDedit = 0;
            Decimal TotCredit = 0;
            Decimal TotSub = 0;
            Boolean PaymentPaidStatus = false;
            String PaymentPaidMessage;
            Decimal TotPaymentRec=0;
           
            
            objUser = new DTO.User();
            objBll = new UserBLL();
            objUser.StudentRollno = Utility.GetSafestring(Session["RollNo"]);
            //objUser.StudentRollno = "AM0001";            

            oDS = objBll.FetchStudentDetails(objUser);
            objRecPayment = objBll.FetchPaymentDetails(objUser);
            BindDropdwon(objRecPayment);

            if (oDS != null)
            {
                if (oDS.Tables.Count > 0)
                {
                    if (oDS.Tables[0] != null)
                    {
                        if (oDS.Tables[0].Rows.Count > 0)
                        {

                            if (Utility.GetSafestring(oDS.Tables[0].Rows[0]["FeePayStatus"]) == "1")
                            {
                                PaymentPaidStatus = true;
                                PaymentPaidMessage = Utility.GetSafestring(oDS.Tables[0].Rows[0]["FeePayStatusMessage"]);
                                Billsummery = Utility.GetSafestring(oDS.Tables[0].Rows[0]["maillog"]);
                                Session["Billsummery"] = Billsummery;
                                hdnBillrefno.Value = Utility.GetSafestring(oDS.Tables[0].Rows[0]["orderid"]);
                                //PaymentStatus.Text = PaymentPaidMessage.ToString();
                                //btnPayment.Enabled = false;

                                //lnkBill.Enabled = true;
                                //lnkBill.Visible = true;
                            }
                            else if (Utility.GetSafestring(oDS.Tables[0].Rows[0]["FeePayStatus"]) == "0")
                            {
                                PaymentPaidStatus = false;
                                PaymentPaidMessage = "";

                                //PaymentStatus.Text = PaymentPaidMessage.ToString();
                                //btnPayment.Enabled = true;

                                //lnkBill.Enabled = false;
                                //lnkBill.Visible = false;
                            }

                            Table oTable;
                            TableRow oRow;
                            TableHeaderRow oRowHeader;
                            TableHeaderCell oCellHeader;
                            TableCell oCell;

                            oTable = new Table();
                            oTable.Style.Add("border", "1px solid #ddd");
                            oTable.Style.Add("width", "100%");
                            oTable.Style.Add("text-align", "left");
                            oTable.Style.Add("border-collapse", "collapse");

                            oRowHeader = new TableHeaderRow();
                            oCellHeader = new TableHeaderCell();
                            oCellHeader.Text = "Student Name";
                            oCellHeader.Style.Add("padding", "15px");
                            oCellHeader.Style.Add("height", "10px");
                            oRowHeader.Cells.Add(oCellHeader);

                            oCellHeader = new TableHeaderCell();
                            oCellHeader.Text = "Roll No";
                            oCellHeader.Style.Add("padding", "15px");
                            oRowHeader.Cells.Add(oCellHeader);

                            oCellHeader = new TableHeaderCell();
                            oCellHeader.Text = "Class Code";
                            oCellHeader.Style.Add("padding", "15px");
                            oRowHeader.Cells.Add(oCellHeader);

                            //oCellHeader = new TableHeaderCell();
                            //oCellHeader.Text = "Date Of Issue";
                            //oCellHeader.Style.Add("padding", "15px");
                            //oRowHeader.Cells.Add(oCellHeader);

                            oCellHeader = new TableHeaderCell();
                            oCellHeader.Text = "House";
                            oCellHeader.Style.Add("padding", "15px");
                            oRowHeader.Cells.Add(oCellHeader);

                            oTable.Rows.Add(oRowHeader);

                            oRow = new TableRow();
                            //Student Name
                            oCell = new TableCell();
                            oCell.Text = Utility.GetSafestring(oDS.Tables[0].Rows[0]["stu_name"]) + " " + Utility.GetSafestring(oDS.Tables[0].Rows[0]["Nick_Name"]);

                            //SetHidden
                            firstname.Value = Utility.GetSafestring(oDS.Tables[0].Rows[0]["stu_name"]) + " " + Utility.GetSafestring(oDS.Tables[0].Rows[0]["Nick_Name"]);
                            productinfo.Value = "Student Fee";
                            email.Value = Utility.GetSafestring(oDS.Tables[0].Rows[0]["FatherEmail"]); ;
                            phone.Value = Utility.GetSafestring(oDS.Tables[0].Rows[0]["emer_no"]);
                            surl.Value = ConfigurationManager.AppSettings["ResponseUrl"].ToString().Trim();
                            furl.Value = ConfigurationManager.AppSettings["ResponseUrl"].ToString().Trim();

                            oRow.Cells.Add(oCell);

                            //Roll No
                            oCell = new TableCell();
                            oCell.Text = Utility.GetSafestring(oDS.Tables[0].Rows[0]["roll_no"]);
                            oRow.Cells.Add(oCell);


                            //Class Code
                            oCell = new TableCell();
                            oCell.Text = Utility.GetSafestring(oDS.Tables[0].Rows[0]["classcode"]) +  " " + Utility.GetSafestring(oDS.Tables[0].Rows[0]["section"]);
                            oRow.Cells.Add(oCell);

                            //Date Of Issue
                            //oCell = new TableCell();
                            //oCell.Text = Utility.GetSafestring(oDS.Tables[0].Rows[0]["issuedate1"]); ;
                            //oRow.Cells.Add(oCell);

                            //House
                            oCell = new TableCell();
                            oCell.Text = Utility.GetSafestring(oDS.Tables[0].Rows[0]["house_code"]);
                            oRow.Cells.Add(oCell);

                            oTable.Rows.Add(oRow);

                            Panel.Controls.Add(oTable);


                            //Fees Table
                            Table oTablefee;
                            TableRow oRowfee;
                            TableHeaderRow oRowHeaderfee;
                            TableHeaderCell oCellHeaderfee;
                            TableCell oCellfee;
                            TableCell oCellfeeTot;

                            oTablefee = new Table();
                            oTablefee.Style.Add("border", "1px solid #ddd");
                            oTablefee.Style.Add("width", "100%");
                            oTablefee.Style.Add("text-align", "left");
                            oTablefee.Style.Add("border-collapse", "collapse");

                            oRowHeaderfee = new TableHeaderRow();

                            oCellHeaderfee = new TableHeaderCell();
                            oCellHeaderfee.Text = "Particulars";
                            oCellHeaderfee.Style.Add("padding", "15px");
                            oCellHeaderfee.Style.Add("height", "10px");
                            oRowHeaderfee.Cells.Add(oCellHeaderfee);

                            oCellHeaderfee = new TableHeaderCell();
                            oCellHeaderfee.Text = "Debit";
                            oCellHeaderfee.Style.Add("padding", "15px");
                            oRowHeaderfee.Cells.Add(oCellHeaderfee);

                            oCellHeaderfee = new TableHeaderCell();
                            oCellHeaderfee.Text = "Credit";
                            oCellHeaderfee.Style.Add("padding", "15px");
                            oRowHeaderfee.Cells.Add(oCellHeaderfee);
                            
                            oTablefee.Rows.Add(oRowHeaderfee);

                            oRowfee = new TableRow();
                            
                            foreach (DataTable table in oDS.Tables)
                            {
                                foreach (DataRow dr in table.Rows)
                                {                                    
                                        lblPeriod.Text = "Bill Period  -  " + Utility.GetSafestring(dr["datefrom"]) + " To " + Utility.GetSafestring(dr["dateto"]);
                                        lblBillIssueDate.Text = "Bill Issue Date -  " + Utility.GetSafestring(dr["issuedate1"]);
                                        lblDueDate.Text = "Due date -  " + Utility.GetSafestring(dr["duedate"]);

                                        // Total number of rows.
                                        int rowCnt;
                                        // Current row count.
                                        int rowCtr;
                                        // Total number of cells per row (columns).
                                        int cellCtr;
                                        // Current cell counter.
                                        int cellCnt;

                                        rowCnt = table.Rows.Count;
                                        cellCnt = 3;

                                        // Create a new row and add it to the table.
                                        TableRow oRowfee1 = new TableRow();

                                        oTablefee.Rows.Add(oRowfee1);
                                        for (cellCtr = 1; cellCtr <= cellCnt; cellCtr++)
                                        {
                                            // Create a new cell and add it to the row.
                                            oCellfee = new TableCell();
                                            oRowfee1.Cells.Add(oCellfee);

                                            // Add a literal text as control.
                                            if (cellCtr.Equals(1))
                                            {
                                                if (Utility.GetSafestring(dr["ordergroup"]).Trim().ToString().Equals("A"))
                                                {
                                                    oCellfee.Controls.Add(new LiteralControl("OPENING BALANCE " + Utility.GetSafestring(dr["particulars"])));
                                                }
                                                else
                                                {
                                                    oCellfee.Controls.Add(new LiteralControl(Utility.GetSafestring(dr["particulars"])));
                                                }                                                
                                            }
                                            else if (cellCtr.Equals(2))
                                            {
                                                oCellfee.Controls.Add(new LiteralControl(Utility.GetSafestring(dr["debit"])));
                                                TotDedit = TotDedit + Utility.GetSafeint(dr["debit"]);
                                            }
                                            else if (cellCtr.Equals(3))
                                            {
                                                oCellfee.Controls.Add(new LiteralControl(Utility.GetSafestring(dr["credit"])));
                                                TotCredit = TotCredit + Utility.GetSafeint(dr["credit"]);
                                            }

                                            // Create a Hyperlink Web server control and add it to the cell.
                                            //System.Web.UI.WebControls.HyperLink h = new HyperLink();
                                            //h.Text = 1 + ":" + cellCtr;
                                            //h.NavigateUrl = "http://www.microsoft.com/net";
                                            //oCellfee.Controls.Add(h);
                                        }                                        
                                    }

                                // Total number of rows.
                                int rowCntTot;
                                // Current row count.
                                int rowCtrTot;
                                // Total number of cells per row (columns).
                                int cellCtrTot;
                                // Current cell counter.
                                int cellCntTot;

                                cellCntTot = 3;

                                // Create a new row and add it to the table.
                                TableRow oRowfeeTot = new TableRow();
                                oTablefee.Rows.Add(oRowfeeTot);
                                for (cellCtrTot = 1; cellCtrTot <= cellCntTot; cellCtrTot++)
                                {
                                    // Create a new cell and add it to the row.
                                    oCellfeeTot = new TableCell();
                                    oRowfeeTot.Cells.Add(oCellfeeTot);

                                    // Add a literal text as control.
                                    if (cellCtrTot.Equals(1))
                                    {                                        
                                        oCellfeeTot.Font.Bold = true;
                                        oCellfeeTot.Controls.Add(new LiteralControl("TOTAL "));
                                    }
                                    else if (cellCtrTot.Equals(2))
                                    {
                                        oCellfeeTot.Font.Bold = true;
                                        oCellfeeTot.Controls.Add(new LiteralControl(TotDedit.ToString()));
                                    }
                                    else if (cellCtrTot.Equals(3))
                                    {
                                        oCellfeeTot.Font.Bold = true;
                                        oCellfeeTot.Controls.Add(new LiteralControl(TotCredit.ToString()));
                                    }
                                }
                            }
                            PanelFee.Controls.Add(oTablefee);

                            TotSub = TotDedit - TotCredit;                            
                            SubTotal.Text = TotSub.ToString();
                            lblFinalTotal.Text = TotSub.ToString();                            

                            if (objRecPayment.DocumentElement.SelectSingleNode("Errors").Attributes[@"Status"].Value == "FALSE")
                            { 
                                XmlNodeList paynodeLst = objRecPayment.DocumentElement.SelectNodes("PAYDETAIL");
                                foreach (XmlNode no in paynodeLst)
                                {
                                    TotPaymentRec = TotPaymentRec + Decimal.Parse(no.Attributes["FEE_REC_AMOUNT"].Value);
                                }
                            }
                            lblReceive.Text = TotPaymentRec.ToString();
                            NetPayAmount.Value = (Decimal.Parse(lblFinalTotal.Text) - Decimal.Parse(lblReceive.Text)).ToString();

                            //SetHidden
                            amount.Value = NetPayAmount.Value.ToString();
                            Session["subtotal"] = NetPayAmount.Value.ToString();
                        }
                    }
                }
            }
        }

        protected void rdDebit_CheckedChanged(object sender, EventArgs e)
        {
            FillData();
            Decimal Calc = 0;

            if (objTaxes.CCard != null)
            {
                if (objTaxes.CCard =="")
                {
                    Calc = Convert.ToDecimal(SubTotal.Text.ToString());
                }
                else if (int.Parse(objTaxes.CCard) > 1)
                {
                    Calc = Math.Round(Convert.ToDecimal(SubTotal.Text.ToString()) + Convert.ToDecimal(Convert.ToDecimal(int.Parse(objTaxes.CCard) * (Convert.ToDecimal(SubTotal.Text.ToString()))) / 100));
                }
            }

            lblFinalTotal.Text = Convert.ToString(Calc);
            //amount.Value = lblFinalTotal.Text.ToString();
        }

        protected void rdCredit_CheckedChanged(object sender, EventArgs e)
        {
            FillData();

            Decimal Calc = 0;
            if (objTaxes.DCard != null)
            { 
            
                if (objTaxes.DCard == "")
                {
                    Calc = Convert.ToDecimal(SubTotal.Text.ToString());
                }
                else if (int.Parse(objTaxes.DCard) > 1)
                {
                    Calc = Math.Round(Convert.ToDecimal(SubTotal.Text.ToString()) + Convert.ToDecimal(Convert.ToDecimal(int.Parse(objTaxes.DCard) * (Convert.ToDecimal(SubTotal.Text.ToString()))) / 100));
                }
            }
            lblFinalTotal.Text = Convert.ToString(Calc) ;
            //amount.Value = lblFinalTotal.Text.ToString();
        }

        protected void rdNetBanking_CheckedChanged(object sender, EventArgs e)
        {
            FillData();

            Decimal Calc = 0;
            if (objTaxes.NetBanking != null)
            {
                if (objTaxes.NetBanking == "")
                {
                    Calc = Convert.ToDecimal(SubTotal.Text.ToString());
                }
                else if (int.Parse(objTaxes.NetBanking) > 1)
                {
                    Calc = Math.Round(Convert.ToDecimal(SubTotal.Text.ToString()) + int.Parse(objTaxes.NetBanking));
                }
            }
            lblFinalTotal.Text = Convert.ToString(Calc) ;
            //amount.Value = lblFinalTotal.Text.ToString();
        }

        //protected void lnkBill_Clicked(object sender, EventArgs e)
        //{

        //    Byte[] res = null;
        //    if (Session["Billsummery"] != null)
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(Session["Billsummery"].ToString(), PdfSharp.PageSize.A4);
        //            pdf.Save(ms);
        //            res = ms.ToArray();
        //        }
        //        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        //        response.Clear();
        //        response.AddHeader("Content-Type", "binary/octet-stream");
        //        response.AddHeader("Content-Disposition",
        //            "attachment; filename=" + hdnBillrefno.Value + ".pdf" + "; size=" +
        //            res.Length.ToString());
        //        response.Flush();
        //        response.BinaryWrite(res);
        //        response.Flush();
        //        response.End();
        //    }
        //}


        //////////////////////////////////////////////////////////////////
        //////////////////// CODE OF PAYMENT//////////////////////////////
        //////////////////////////////////////////////////////////////////

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            Decimal NetPaymentAmt = 0;
            try
            {
                // Add Fake Delay to simulate long running process.
                //System.Threading.Thread.Sleep(3000);
                //if (amount.Value != NetPayAmount.Value)
                //{
                //    NetPaymentAmt = Decimal.Parse(NetPayAmount.Value);
                //}

                NetPaymentAmt = 1500;

                if ((!rdDebit.Checked) && (!rdCredit.Checked) && (!rdNetBanking.Checked))
                {
                    FillData();

                    frmError.Visible = true;
                    frmError.InnerHtml = "<center><span style='color: red'>Please choose the payment options</span></center>";
                    return;
                }

                string[] hashVarsSeq;
                string hash_string = string.Empty;


                if (string.IsNullOrEmpty(Request.Form["txnid"])) // generating txnid
                {
                    Random rnd = new Random();
                    string strHash = Generatehash512(rnd.ToString() + DateTime.Now);
                    txnid1 = strHash.ToString().Substring(0, 20);

                }
                else
                {
                    txnid1 = Request.Form["txnid"];
                }
                if (string.IsNullOrEmpty(Request.Form["hash"])) // generating hash value
                {
                    if (
                        string.IsNullOrEmpty(ConfigurationManager.AppSettings["MERCHANT_KEY"]) ||
                        string.IsNullOrEmpty(txnid1) ||
                        string.IsNullOrEmpty(Request.Form["amount"]) ||
                        string.IsNullOrEmpty(Request.Form["firstname"]) ||
                        string.IsNullOrEmpty(Request.Form["email"]) ||
                        string.IsNullOrEmpty(Request.Form["phone"]) ||
                        string.IsNullOrEmpty(Request.Form["productinfo"]) ||
                        string.IsNullOrEmpty(Request.Form["surl"]) ||
                        string.IsNullOrEmpty(Request.Form["furl"])
                        )
                    {
                        //error
                        frmError.Visible = true;
                        return;
                    }

                    else
                    {
                        frmError.Visible = false;
                        hashVarsSeq = ConfigurationManager.AppSettings["hashSequence"].Split('|'); // spliting hash sequence from config
                        hash_string = "";
                        foreach (string hash_var in hashVarsSeq)
                        {
                            if (hash_var == "key")
                            {
                                hash_string = hash_string + ConfigurationManager.AppSettings["MERCHANT_KEY"];
                                hash_string = hash_string + '|';
                            }
                            else if (hash_var == "txnid")
                            {
                                hash_string = hash_string + txnid1;
                                hash_string = hash_string + '|';
                            }
                            else if (hash_var == "amount")
                            {
                                hash_string = hash_string + Convert.ToDecimal(Request.Form[hash_var]).ToString("g29");
                                hash_string = hash_string + '|';
                            }
                            else
                            {

                                hash_string = hash_string + (Request.Form[hash_var] != null ? Request.Form[hash_var] : "");// isset if else
                                hash_string = hash_string + '|';
                            }
                        }
                        hash_string += ConfigurationManager.AppSettings["SALT"];// appending SALT
                        hash1 = Generatehash512(hash_string).ToLower();         //generating hash
                        action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";// setting URL
                    }
                }

                else if (!string.IsNullOrEmpty(Request.Form["hash"]))
                {
                    hash1 = Request.Form["hash"];
                    action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";
                }
                

                if (!string.IsNullOrEmpty(hash1))
                {
                    hash.Value = hash1;
                    txnid.Value = txnid1;
                    System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                    data.Add("hash", hash.Value);
                    data.Add("txnid", txnid.Value);
                    data.Add("key", key.Value);
                    string AmountForm = Convert.ToDecimal(amount.Value.Trim()).ToString("g29");// eliminating trailing zeros
                    Session["subtotal"] = AmountForm;
                    //amount.Text = AmountForm;
                    data.Add("amount", Convert.ToDecimal(amount.Value.Trim()).ToString("g29"));
                    data.Add("firstname", firstname.Value.Trim());
                    data.Add("lastname", "".Trim());
                    data.Add("email", email.Value.Trim());
                    data.Add("phone", phone.Value.Trim());
                    data.Add("productinfo", productinfo.Value.Trim());
                    data.Add("surl", surl.Value.Trim());
                    data.Add("furl", furl.Value.Trim());                    
                    data.Add("curl", "".Trim());
                    data.Add("address1", "".Trim());
                    data.Add("address2", "".Trim());
                    data.Add("city", "".Trim());
                    data.Add("state", "".Trim());
                    data.Add("country", "".Trim());
                    data.Add("zipcode", "".Trim());
                    data.Add("udf1", "".Trim());
                    data.Add("udf2", "".Trim());
                    data.Add("udf3", "".Trim());
                    data.Add("udf4", "".Trim());
                    data.Add("udf5", "".Trim());

                    //data.Add("pg", "".Trim()); // FOR DEVELOPMENT
                    data.Add("service_provider", "payu_paisa"); // PayU Money Authorised Only // FOR LIVE

                    string strForm = PreparePOSTForm(action1, data);
                    Page.Controls.Add(new LiteralControl(strForm));
                }

                else
                {
                    //no hash

                }

            }

            catch (Exception ex)
            {
                Response.Write("<span style='color:red'>" + ex.Message + "</span>");

            }
        }

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

        private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
        {
            //Set a name for the form
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");

            foreach (System.Collections.DictionaryEntry key in data)
            {

                strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                               "\" value=\"" + key.Value + "\">");
            }

            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }        
        
        public string GenerateBill(String Bill_id)
        {
            //Session["Billsummery"] = Bill_id.ToString();
            UserBLL objBll = new UserBLL();
            BillOrders ObjOrder = new BillOrders();
            DataSet oDSBillOrder = null;
            String Billsummery =null;

            Byte[] res = null;
            if (Bill_id != null)
            {
                ObjOrder.ID = Bill_id;
                //ObjOrder.Name = DropDownListSelectBill.Text;

                oDSBillOrder = objBll.FetchStudentBill(ObjOrder);

                if (oDSBillOrder != null)
                {
                    if (oDSBillOrder.Tables.Count > 0)
                    {
                        if (oDSBillOrder.Tables[0] != null)
                        {
                            if (oDSBillOrder.Tables[0].Rows.Count > 0)
                            {
                                Billsummery = Utility.GetSafestring(oDSBillOrder.Tables[0].Rows[0]["maillog"]);
                            }
                        }
                    }
                }


                using (MemoryStream ms = new MemoryStream())
                {
                    var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(Billsummery.ToString(), PdfSharp.PageSize.A4);
                    pdf.Save(ms);
                    res = ms.ToArray();
                }
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.Clear();
                response.AddHeader("Content-Type", "binary/octet-stream");
                response.AddHeader("Content-Disposition",
                    "attachment; filename=" + Bill_id.ToString() + ".pdf" + "; size=" +
                    res.Length.ToString());
                response.Flush();
                response.BinaryWrite(res);
                response.Flush();
                response.End();
            }

            return Billsummery;
        }

        protected void DropDownListSelectBill_SelectedIndexChanged(object sender, EventArgs e)
        {           

            GenerateBill(DropDownListSelectBill.SelectedValue.ToString());
            FillData();
        }

        protected void DropDownListSelectBill_TextChanged(object sender, EventArgs e)
        {            

        }

        private void BindDropdwon(XmlDocument objPaymentRec)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));

            if (objPaymentRec.DocumentElement.SelectSingleNode("Errors").Attributes[@"Status"].Value == "FALSE")
            {
                XmlNodeList paynodeLst = objPaymentRec.DocumentElement.SelectNodes("PAYDETAIL");
                foreach (XmlNode no in paynodeLst)
                {
                    table.Rows.Add(no.Attributes["IDNO"].Value, no.Attributes["REC_DTTI"].Value);
                }
            }
            DropDownListSelectBill.DataSource = table;
            DropDownListSelectBill.DataTextField = "Name";
            DropDownListSelectBill.DataValueField = "Id";
            DropDownListSelectBill.DataBind();
            DropDownListSelectBill.Items.Insert(0, new ListItem("--Select Bill--", "0"));

            //ddl1.DataSource = table;
            //ddl1.DataTextField = "Name";
            //ddl1.DataValueField = "Id";
            //ddl1.DataBind();
            //ddl1.Items.Insert(0, new ListItem("--Select Bill--", "0"));
        }
    }
}