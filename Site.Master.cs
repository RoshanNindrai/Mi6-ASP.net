using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication4
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        _Default logger = new _Default();
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            Response.Redirect("About.aspx");
        }
         protected void LoggedOut(object sender, EventArgs e)
        {
            logger.logger.Info("User " + Session["username"].ToString() + " logged out");
            Response.Redirect("~/user_login.aspx");
        }
    }
}
