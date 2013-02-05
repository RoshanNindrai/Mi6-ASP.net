using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication4
{
    
    public partial class DBA_Reset : System.Web.UI.Page
    {
        WebForm1 DBAobject = new WebForm1();
        String encryption_key = "Thisisourkeyforencryptioninourpr0jectAESxxx";
        SqlConnection connection = new SqlConnection("  Server=Roshan-PC; Integrated Security=SSPI; Database=SecureDatabase;");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Reset_Click(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = new SqlCommand(" Update login set Attempts = 0 where User_Name=@username", connection);
            adapter.UpdateCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(TextBox1.Text, encryption_key);
            connection.Open();
            adapter.UpdateCommand.ExecuteNonQuery();
            connection.Close();
            Response.Redirect("DBA_login.aspx");
        }
    }
}