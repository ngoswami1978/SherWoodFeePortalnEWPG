using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SherWoodFeePortal.BLL;

namespace SherWoodFeePortal
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["islogin"] != null)
            {
                if (Utility.GetSafestring(Session["islogin"]) == "1") // It means regular user
                {

                }
                else
                {
                    //Response.Redirect("Logout.aspx",true);
                }
            }
            else
            {
                //Response.Redirect("Logout.aspx", true);
            }
        }
    }
}
