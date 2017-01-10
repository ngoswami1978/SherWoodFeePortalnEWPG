using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SherWoodFeePortal.BLL;
using SherWoodFeePortal.DTO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text;
namespace SherWoodFeePortal
{
    public partial class OnlinePaymentLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private static string GenerateOTP()
        {
            //string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            //string numbers = "1234567890";
            //string characters = numbers;
            //characters += alphabets + small_alphabets + numbers;
            //int length = 6;
            //string otp = string.Empty;
            //for (int i = 0; i < length; i++)
            //{
            //    string character = string.Empty;
            //    do
            //    {
            //        int index = new Random().Next(0, characters.Length);
            //        character = characters.ToCharArray()[index].ToString();
            //    }
            //    while (otp.IndexOf(character) != -1);
            //    otp += character;
            //}
            string otp = string.Empty;

            try
            {
                
                int length = 6;
                string numbers = "0123456789";
                Random objrandom = new Random();
                string strrandom = string.Empty;
                int noofnumbers = length;
                for (int i = 0; i < noofnumbers; i++)
                {
                    int temp = objrandom.Next(0, numbers.Length);
                    strrandom += temp;
                }
                otp= strrandom;                
            }
            catch (Exception ex)
            {
            }

            return otp;
        }

        [System.Web.Services.WebMethod]
        public static string GetOTPResponse(string Mobno)
        {
            var json = "";
            string OTP = "";
            OTP = GenerateOTP();
            //string sURL ="http://mysmsapp.in/api/push.json?apikey=585cc9ef7b097&sender=SERWOD&mobileno=9";
            string sURL = "http://mysmsapp.in/api/push.json?apikey=585cc9ef7b097&sender=SERWOD&mobileno=" + Mobno + "&text=OTPfor Sherwood:" + OTP;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sURL);
            //request.MaximumAutomaticRedirections = 4;
            request.Credentials = CredentialCache.DefaultCredentials;
            try
            {
                HttpWebResponse response =(HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string sResponse = readStream.ReadToEnd();
                response.Close();
                readStream.Close();

                //json = JsonConvert.SerializeObject(new { Exist = "true", Mobno = Utility.GetSafestring(dr[0]) }, Formatting.None);


                //dynamic dynObj =JsonConvert.SerializeObject(sResponse.Replace('.',' '),Formatting.None);
                JObject o = JObject.Parse(sResponse);

                dynamic dynObj = JsonConvert.SerializeObject(sResponse.Replace('.', ' '), Formatting.None);

                json = JsonConvert.SerializeObject(new { status = o["status"].ToString(), OTP = OTP }, Formatting.None);
                return json.ToString();
            }
            catch
            {
                return "";
            }
        }
        [System.Web.Services.WebMethod]
        public static string CheckRollNo(string Rollno)
        {           
            var json = "";
          
            SqlDataReader dr;
            //SqlConnection con = new SqlConnection(ConfigurationManager.AppSettings["constr"].ToString());
            //con.Open();
            string constr = WAY.bizShared.bzShared.GetConnectionString();
            string query = "dbo.usp_ShrewoodFetchDetails";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@RollNo", Rollno));
            parameters.Add(new SqlParameter("@Type", 1));

            dr = SqlHelper.ExecuteReader(constr, CommandType.StoredProcedure, query, parameters.ToArray());
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //hdnMailTemplate.Value = Utility.GetSafestring(dr[3]);

                    json = JsonConvert.SerializeObject(new { Exist = "true", Mobno = Utility.GetSafestring(dr[0]), MailTemplate = Utility.GetSafestring(dr[3]) }, Formatting.None);

                   // retval = "{'Exist':'true','Mobno':" +  "'" + Utility.GetSafestring(dr[0]) + "'" + "}";
                }                
            }
            else
            {
               
                json = JsonConvert.SerializeObject(new { Exist = "false", Mobno = "" }, Formatting.None);
            }

            return json.ToString();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            UserBLL objBll = null;
            User objUser = null;
            lblError.Text = "";
            DataSet oDS = null;
            if(txtRollno.Text.Trim().Length==0)
            {
                lblError.Text = lblError.Text + "Please enter the student roll no!" + "</br>";
                txtRollno.Focus();
            }
            else if (hdnMobileNo.Value.Trim().Length == 0)
            {
                lblError.Text = lblError.Text + "Missing Data!" + "</br>";
                //txtMobileNo.Focus();
            }
            else if (txtOTP.Text.Trim().Length == 0)
            {
                lblError.Text = lblError.Text + "Missing Data!" + "</br>";
                txtOTP.Focus();
            }
            else
            {
                // Logic For verification
                objUser = new DTO.User();
                objBll = new UserBLL();
                //objUser.UserName = txtUsername.Text.Trim();
                //objUser.Password = txtPassword.Text.Trim();
                objUser.StudentRollno = txtRollno.Text.Trim();
                oDS = objBll.FetchUserDetails(objUser);

                if (oDS != null)
                {
                    if (oDS.Tables.Count > 0)
                    {
                        if (oDS.Tables[0] != null)
                        {
                            if (oDS.Tables[0].Rows.Count > 0)
                            {
                                Session["RollNo"] = Utility.GetSafestring(oDS.Tables[0].Rows[0]["roll_no"]);// Student RollNo
                                Session["Name"] = Utility.GetSafestring(oDS.Tables[0].Rows[0]["Stu_Name"] + " " + oDS.Tables[0].Rows[0]["Nick_Name"]);// Name of the Student
                                Session["House"] = Utility.GetSafestring(oDS.Tables[0].Rows[0]["house_code"] );// House
                                Session["Classsection"] = Utility.GetSafestring(oDS.Tables[0].Rows[0]["classcode"] + " " + oDS.Tables[0].Rows[0]["section"]);// Class Section
                                Session["Fatheremail"] = Utility.GetSafestring(oDS.Tables[0].Rows[0]["fatheremail"]); //Father Email

                                Session["islogin"] = "1";
                                Session["MailTemplate"] = hdnMailTemplate.Value.ToString();

                                Response.Redirect("FrmHome.aspx",true);
                            }
                            else
                            {
                                lblError.Text = "Please enter the valid Roll No.!" + "</br>";

                            }
                        }
                        else
                        {
                            lblError.Text = "Please enter the valid Roll No.!" + "</br>";
                        }
                    }
                }
                else
                {
                    lblError.Text = lblError.Text + "Credentials or registered mobile no. does not match!" + "</br>";
                  
                }
            }
        }
    }
}