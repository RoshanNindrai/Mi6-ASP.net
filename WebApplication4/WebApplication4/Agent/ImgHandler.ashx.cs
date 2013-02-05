using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication4
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
           
            SqlDataAdapter adapter = new SqlDataAdapter();
            String user_name = context.Request.QueryString.ToString();
            String encryption_key = "Thisisourkeyforencryptioninourpr0jectAESxxx";
            WebForm1 DBAobject = new WebForm1();
            SqlDataReader input_data;
            SqlCommand SelectCommand;
            SqlConnection connection = new SqlConnection("Server=tcp:dmhec6bljx.database.windows.net,1433;Database=SecureDatabase;User ID=roshan1989@dmhec6bljx;Password=Myyearofbirth89;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            SelectCommand = new SqlCommand("SELECT * FROM agent_job where User_name= @username", connection);
            SelectCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = DBAobject.encryptPlaintext(user_name, encryption_key);
            connection.Open();
            input_data = SelectCommand.ExecuteReader();
            while (input_data.Read())
            {

                Byte[] Image = (byte[])input_data[4];
                context.Response.ContentType = "image/jpg";
                context.Response.BinaryWrite(Image);





            }

            connection.Close();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}