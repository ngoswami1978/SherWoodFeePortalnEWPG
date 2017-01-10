using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SherWoodFeePortal.BLL;
namespace SherWoodFeePortal
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["islogin"] != null)
            {
                if (Utility.GetSafestring(Session["islogin"]) == "1") // It means regular user
                {                    

                    lblName.Text = "Mr." + "  " + Utility.GetSafestring(Session["Name"]);
                }
                else
                {
                    Session.Clear();
                    Session.RemoveAll();
                    Session.Abandon();
                    Response.Redirect("Logout.aspx",true);
                }
            }
            else
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("Logout.aspx", true);
            }           
        }
    }
}
