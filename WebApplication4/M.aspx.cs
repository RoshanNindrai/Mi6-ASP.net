using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
namespace WebApplication4
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        WebForm1 DBAobject = new WebForm1();
        String encryption_key = "Thisisourkeyforencryptioninourpr0jectAESxxx";
        SqlConnection connection = new SqlConnection("  Server=Roshan-PC; Integrated Security=SSPI; Database=SecureDatabase;");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (DBAobject.checkUser(TextBox5.Text)&&TextBox6.Text.Length!=0)
            {
                Boolean flag=checkdiv(TextBox5.Text, User.Identity.Name.ToString());
                if (false)
                {
                    checkJob(TextBox5.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    byte[] Target_image = new byte[FileUpload1.PostedFile.ContentLength];
                    HttpPostedFile Imagepath = FileUpload1.PostedFile;
                    Imagepath.InputStream.Read(Target_image, 0, (int)FileUpload1.PostedFile.ContentLength);
                    adapter.InsertCommand = new SqlCommand(" INSERT INTO agent_job VALUES(@User_Name,@Current_mission,@Target_name,@Briefing,@Target_image)", connection);
                    adapter.InsertCommand.Parameters.Add("@User_Name", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(TextBox5.Text, encryption_key);
                    adapter.InsertCommand.Parameters.Add("@Current_mission", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(TextBox10.Text, encryption_key);
                    adapter.InsertCommand.Parameters.Add("@Target_Name", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(TextBox4.Text, encryption_key);
                    adapter.InsertCommand.Parameters.Add("@Briefing", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(TextBox11.Text, encryption_key);
                    adapter.InsertCommand.Parameters.Add("@Target_image", SqlDbType.Image, Target_image.Length).Value = Target_image;
                    connection.Open();
                    adapter.InsertCommand.ExecuteNonQuery();
                    connection.Close();
                    sendMail();
                }
                else
                {
                    Response.Redirect("Agent.aspx");
                }
            }
        }
        public Boolean checkdiv(string compare, string username)
        {

            SqlDataReader input_data;
            SqlCommand SelectCommand;
            SelectCommand = new SqlCommand("SELECT User_name FROM user_agent where division=1", connection);
            //SelectCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = encryptPlaintext(username, encryption_key);
            connection.Open();
            input_data = SelectCommand.ExecuteReader();
            String name = null;

            while (input_data.Read())
            {
                name = DBAobject.decryptionPlaintext(input_data[0].ToString(), encryption_key);
                if (name.Equals(compare))
                {

                    return false;
                }

            }

            connection.Close();
            return false;
        }
        public Boolean checkJob(string compare)
        {
           
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlDataReader input_data;
            SqlCommand SelectCommand;
            SelectCommand = new SqlCommand("SELECT * FROM agent_job where User_name=@username", connection);
            SelectCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(compare, encryption_key);
            connection.Open();
            input_data = SelectCommand.ExecuteReader();
            input_data.Close();

          

                    adapter.DeleteCommand = new SqlCommand("delete from agent_job where User_name=@username", connection);
                    adapter.DeleteCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(compare, encryption_key);
                    adapter.DeleteCommand.ExecuteNonQuery();
                    connection.Close();
                    return true;
                

            

            connection.Close();
            return false;
        }
        public void sendMail()
        {
           
           


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Job_Done.aspx");
        }

    }
}