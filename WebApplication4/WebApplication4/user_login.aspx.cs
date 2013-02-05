using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
namespace WebApplication4
{
    public partial class _Default : System.Web.UI.Page
    {
        public log4net.ILog logger = log4net.LogManager.GetLogger("Logger");
   protected void Page_Load(object sender, EventArgs e)
        {
          
          Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

             Session["username"] = User.Identity.Name;
           
            if (User.Identity.IsAuthenticated)
            {
                
                if (User.IsInRole("super dba"))
                {
                    logger.Info("User " + User.Identity.Name.ToString() + " Logged in.");
                    Response.Redirect("~/DBA/DBA_login.aspx");
                }
                else if (User.IsInRole("agent"))
                {
                    logger.Info("User " + User.Identity.Name.ToString() + " Logged in.");
                    Response.Redirect("~/Agent/Agent.aspx");
                }
                else if (User.IsInRole("M"))
                    {
                        logger.Info("User " + User.Identity.Name.ToString() + " Logged in.");
                     Response.Redirect("~/M/M.aspx");
                    }

            }
            else
            {
                String logged_out = Session["username"].ToString();
                Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }
        }
       }
}
