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
    public partial class WebForm2 : System.Web.UI.Page
    {
        String encryption_key = "Thisisourkeyforencryptioninourpr0jectAESxxx";
        WebForm1 DBAobject = new WebForm1();
        SqlConnection connection = new SqlConnection("  Server=Roshan-PC; Integrated Security=SSPI; Database=SecureDatabase;");
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            String user_name = User.Identity.Name;
            Label1.Text = user_name;
            SqlDataReader input_data;
            SqlCommand SelectCommand;
            SelectCommand = new SqlCommand("SELECT * FROM agent_job where User_name= @username", connection);
            SelectCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(user_name, encryption_key);
            connection.Open();
            input_data = SelectCommand.ExecuteReader();
            while (input_data.Read())
            {

                Label2.Text = DBAobject.decryptionPlaintext(input_data[1].ToString(),encryption_key);
                Label3.Text = DBAobject.decryptionPlaintext(input_data[2].ToString(), encryption_key);
                Label4.Text = DBAobject.decryptionPlaintext(input_data[3].ToString(), encryption_key);
               
                
            }

            connection.Close();
        }
    }
}