using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;
using System.Configuration;
namespace WebApplication4
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        _Default loggerobj = new _Default();
        String encryption_key = "Thisisourkeyforencryptioninourpr0jectAESxxx";
        WebForm1 DBAobject = new WebForm1();
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (((!Roles.IsUserInRole("M"))) && (Session["username"] == null || String.IsNullOrEmpty(Session["username"].ToString())))
            {
                Response.Redirect("user_login.aspx");
            }
            Label2.Text = "";
            Label3.Text = "";
            Label4.Text = "";
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label5.Visible = false;
            String User_name = TextBox1.Text;
            String m_id = User.Identity.Name.ToString();
            SqlDataReader input_data;
            SqlCommand SelectCommand;
            if (DBAobject.checkdiv(User_name, m_id))
            {
                SelectCommand = SelectCommand = new SqlCommand("SELECT * FROM agent_job where User_name= @username", connection);

                SelectCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(User_name, encryption_key);

                connection.Open();
                input_data = SelectCommand.ExecuteReader();
                while (input_data.Read())
                {

                    Label2.Text = DBAobject.decryptionPlaintext(input_data[1].ToString(), encryption_key);
                    Label3.Text = DBAobject.decryptionPlaintext(input_data[2].ToString(), encryption_key);
                    Label4.Text = DBAobject.decryptionPlaintext(input_data[3].ToString(), encryption_key);


                }
                connection.Close();
                loggerobj.logger.Info("user :" + User.Identity.Name.ToString() + " viewed job of user name " + User_name.ToString());
            }
            else
            {
                loggerobj.logger.Warn("user :" + User.Identity.Name.ToString() + " tried to view the job for user name " + User_name.ToString());
                Label5.Visible = true;
                TextBox1.Text = " ";
                Response.Redirect("~/M/Job_Done.aspx");
            }

            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            String User_name = TextBox1.Text;
           SqlDataAdapter adapter = new SqlDataAdapter();
           connection.Open();
            adapter.DeleteCommand = new SqlCommand("delete from agent_job where User_name=@username", connection);
            adapter.DeleteCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(User_name, encryption_key);
            adapter.DeleteCommand.ExecuteNonQuery();
            connection.Close();
            loggerobj.logger.Warn("user :" + User.Identity.Name.ToString() + " deleted a job for user name " + User_name.ToString());
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("M.aspx");
        }
      

    }
}