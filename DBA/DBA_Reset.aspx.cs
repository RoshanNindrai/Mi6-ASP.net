using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace WebApplication4
{
    
    public partial class DBA_Reset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            MembershipUser member = Membership.GetUser(TextBox1.Text.ToString());
            member.UnlockUser();
            Response.Redirect("~/DBA/DBA_login.aspx");
        }
    }
}