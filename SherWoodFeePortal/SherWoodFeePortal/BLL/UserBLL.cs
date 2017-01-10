using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SherWoodFeePortal.DAL;
using SherWoodFeePortal.DTO;
using System.Xml;

namespace SherWoodFeePortal.BLL
{
    public class UserBLL
    {
        protected internal DataSet FetchUserDetails(User ObjUser)
        {
            UserDAL objDal = new UserDAL();
            return objDal.FetchUserDetails(ObjUser);
        }
        protected internal DataSet FetchStudentDetails(User ObjUser)
        {
            UserDAL objDal = new UserDAL();
            return objDal.FetchStudentDetails(ObjUser);
        }
        protected internal DataSet FetchStudentBill(BillOrders ObjBill)
        {
            UserDAL objDal = new UserDAL();
            return objDal.FetchStudentBill(ObjBill);
        }
        protected internal XmlDocument FetchPaymentDetails(User ObjUser)
        {
            UserDAL objDal = new UserDAL();
            return objDal.FetchPaymentDetails(ObjUser);
        }
        protected internal void SavePaymentBasket(PaymentBasket ObjBasket)
        {
            UserDAL objDal = new UserDAL();
            objDal.SavePaymentBasket(ObjBasket);
        }

    }   
}