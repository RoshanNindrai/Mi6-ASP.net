using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Web.Security;
using System.Configuration;
namespace WebApplication4
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        MemoryStream stream = new MemoryStream();
        WebForm1 DBAobject = new WebForm1();
        _Default loggerobj = new _Default();
        byte[] byteimage;
        String encryption_key = "Thisisourkeyforencryptioninourpr0jectAESxxx";
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            Boolean flag = Roles.IsUserInRole("M");
            if ((!flag) && (Session["username"] == null || String.IsNullOrEmpty(Session["username"].ToString())))
            {
                Response.Redirect("user_login.aspx");
            }
            Label1.Visible = false;
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
            
            if (!input_data.HasRows||input_data==null)
            {
                
                connection.Close();
                return true;
            }
            else
            {
                input_data.Close();
                connection.Close();
                return false;
            }
          

             

            

           
           
        }
        public void sendMail()
        {
           
           


        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            Label2.Visible = false;

            if (DBAobject.checkUser(TextBox5.Text) && CheckBox())
            {
                if (DBAobject.checkdiv(TextBox5.Text, User.Identity.Name))
                {
                    if (checkJob(TextBox5.Text))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter();

                        System.Drawing.Image target = System.Drawing.Image.FromStream(
                         FileUpload1.PostedFile.InputStream);
                        target.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byteimage = stream.ToArray();
                        adapter.InsertCommand = new SqlCommand(" INSERT INTO agent_job VALUES(@User_Name,@Current_mission,@Target_name,@Briefing,@Target_image)", connection);
                        adapter.InsertCommand.Parameters.Add("@User_Name", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(TextBox5.Text, encryption_key);
                        adapter.InsertCommand.Parameters.Add("@Current_mission", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(TextBox10.Text, encryption_key);
                        adapter.InsertCommand.Parameters.Add("@Target_Name", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(TextBox4.Text, encryption_key);
                        adapter.InsertCommand.Parameters.Add("@Briefing", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(TextBox11.Text, encryption_key);
                        adapter.InsertCommand.Parameters.Add("@Target_image", SqlDbType.Image, 0).Value = byteimage;
                        connection.Open();
                        adapter.InsertCommand.ExecuteNonQuery();
                        connection.Close();
                        loggerobj.logger.Info("user :" + User.Identity.Name.ToString() + " inserted a job for user name " + TextBox5.Text.ToString()
                            + " Current Mission: " + TextBox10.Text + " Target Name: " + TextBox4.Text + " Briefing : " + TextBox11.Text);
                    }
                    else
                    {
                        Label2.Visible = true;
                    }
                }
                else
                {
                    Label1.Visible = true;
                }
            }
            else
            {
                Label1.Visible = true;
            }
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            Response.Redirect("Job_Done.aspx");
        }
        protected Boolean CheckBox(){
            if (string.IsNullOrEmpty(TextBox4.Text.ToString()) || string.IsNullOrEmpty(TextBox5.Text.ToString())
               || string.IsNullOrEmpty(TextBox10.Text.ToString()) || string.IsNullOrEmpty(TextBox11.Text.ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}