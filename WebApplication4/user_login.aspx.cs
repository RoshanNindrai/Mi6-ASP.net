using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication6
{
    public partial class _Default : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);


            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("super dba"))
                {
                    Response.Redirect("DBA_login.aspx");
                }
                else if (User.IsInRole("agent"))
                {
                    Response.Redirect("Agent.aspx");
                }
                else if (User.IsInRole("M"))
                    {
                     Response.Redirect("M.aspx");
                    }

            }
            else
            {
                Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }
        }
    }
}
