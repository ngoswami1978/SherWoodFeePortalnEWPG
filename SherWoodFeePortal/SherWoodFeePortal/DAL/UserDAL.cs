using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SherWoodFeePortal.BLL;
using SherWoodFeePortal.DTO;
using Microsoft.ApplicationBlocks.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Xml;


namespace SherWoodFeePortal.DAL
{
    public class UserDAL
    {
        protected internal DataSet FetchUserDetails(User ObjUser)
        {
            if (ObjUser != null)
            {
                //string constr = ConfigurationManager.AppSettings["constr"].ToString();

                string constr = WAY.bizShared.bzShared.GetConnectionString();
                //string strSqlConnection = WAY.bizShared.bzShared.GetConnectionString();
                //Dim objSqlConnection As New SqlConnection(bizShared.bzShared.GetConnectionString())

                string query = "dbo.usp_ShrewoodFetchDetails";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@RollNo", ObjUser.StudentRollno));
                parameters.Add(new SqlParameter("@Type", 1));
                
                return SqlHelper.ExecuteDataset(constr, CommandType.StoredProcedure, query, parameters.ToArray());
            }
            else
            {
                return null;
            }
        }
        protected internal DataSet FetchStudentDetails(User ObjUser)
        {
            if (ObjUser != null)
            {   
                //string constr = ConfigurationManager.AppSettings["constr"].ToString();
                string constr = WAY.bizShared.bzShared.GetConnectionString();

                string query = "dbo.usp_ShrewoodFetchDetails";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@RollNo", ObjUser.StudentRollno));
                parameters.Add(new SqlParameter("@Type", 2));

                return SqlHelper.ExecuteDataset(constr, CommandType.StoredProcedure, query, parameters.ToArray());
            }
            else
            {
                return null;
            }
        }
        protected internal DataSet FetchStudentBill(BillOrders ObjBill)
        {
            if (ObjBill != null)
            {
                //string constr = ConfigurationManager.AppSettings["constr"].ToString();
                string constr = WAY.bizShared.bzShared.GetConnectionString();
                string query = "dbo.usp_ShrewoodFetchDetails";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@IDNO", ObjBill.ID));
                parameters.Add(new SqlParameter("@Type", 3));

                return SqlHelper.ExecuteDataset(constr, CommandType.StoredProcedure, query, parameters.ToArray());
            }
            else
            {
                return null;
            }
        }


        protected internal XmlDocument FetchPaymentDetails(User ObjUser)
        {
            XmlDocument objXmlInput = new XmlDocument();
            XmlDocument objXmlOutput = new XmlDocument();
            WAY.bizMaster.bzStudent objStudent = new WAY.bizMaster.bzStudent();

            try
            {
            
            if (ObjUser != null)
            {
                objXmlInput.LoadXml("<MS_VIEWSTUDENT_PAYMENT_DETAIL_INPUT><Rollno></Rollno><orderid></orderid></MS_VIEWSTUDENT_PAYMENT_DETAIL_INPUT>");
                objXmlInput.DocumentElement.SelectSingleNode("Rollno").InnerText = ObjUser.StudentRollno;
                objXmlOutput = objStudent.View(objXmlInput);                
            }
            else
            {
                return objXmlOutput;
            }
            if (objXmlOutput.DocumentElement.SelectSingleNode("Errors").Attributes[@"Status"].Value == "FALSE")
            {
                return objXmlOutput;
            }
            }
            catch (Exception Ex)
            {
                
            }
            return objXmlOutput;
        }


        public static string FetchDetails(string Rollno)
        {
            DataSet oDS = null;
            string Message = "";
            if (Rollno.Length > 0)
            {
                //string constr = ConfigurationManager.AppSettings["constr"].ToString();
                string constr = WAY.bizShared.bzShared.GetConnectionString();
                string query = "dbo.usp_ShrewoodFetchDetails";
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@RollNo", Rollno));
                parameters.Add(new SqlParameter("@Type", 1));

                oDS = SqlHelper.ExecuteDataset(constr, CommandType.StoredProcedure, query, parameters.ToArray());
                if (oDS != null)
                {
                    if (oDS.Tables.Count > 0)
                    {
                        if (oDS.Tables[0] != null)
                        {
                            if (oDS.Tables[0].Rows.Count > 0)
                            {
                                Message = Utility.GetSafestring(oDS.Tables[0].Rows[0]["Message"]);
                            }
                        }
                    }
                }
            }

            return Message;

        }
        


        protected internal void SavePaymentBasket(PaymentBasket ObjBasket) 
        {
            try
            {
                if (ObjBasket != null)
                {
                    //string constr = ConfigurationManager.AppSettings["constr"].ToString();
                    string constr = WAY.bizShared.bzShared.GetConnectionString();
                    string query = "dbo.usp_ShrewoodPaymentBasket";
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    parameters.Add(new SqlParameter("@roll_no", ObjBasket.roll_no));
                    parameters.Add(new SqlParameter("@ramount", ObjBasket.ramount));
                    parameters.Add(new SqlParameter("@pgamount", ObjBasket.pgamount));
                    parameters.Add(new SqlParameter("@finyear", ObjBasket.finyear));
                    parameters.Add(new SqlParameter("@status", ObjBasket.status));
                    parameters.Add(new SqlParameter("@paymode", ObjBasket.paymode));
                    parameters.Add(new SqlParameter("@orderid", ObjBasket.orderid));

                    parameters.Add(new SqlParameter("@PG_TYPE", ObjBasket.PG_TYPE));
                    parameters.Add(new SqlParameter("@bank_ref_num", ObjBasket.bank_ref_num));
                    parameters.Add(new SqlParameter("@additionalCharges", ObjBasket.additionalCharges));
                    parameters.Add(new SqlParameter("@mailtemplate", ObjBasket.mailtemplate));

                    int k = SqlHelper.ExecuteNonQuery(constr, CommandType.StoredProcedure, query, parameters.ToArray());
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }
    }

//protected void SavetoBasket() //{ // UserDAL target = new UserDAL(); // TODO: Initialize to an appropriate value // PaymentBasket ObjBasket = new PaymentBasket(); // ObjBasket.roll_no = "000004"; // ObjBasket.paymode = "DebitCard"; // ObjBasket.ramount = "17000"; // ObjBasket.status = "Success"; // ObjBasket.finyear = "2016"; // target.SavePaymentBasket(ObjBasket); //}
}