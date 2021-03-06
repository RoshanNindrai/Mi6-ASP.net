﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Text;
using System.IO;
using log4net;
namespace WebApplication4
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        _Default loggerobj = new _Default();
        string  encryption_key = "Thisisourkeyforencryptioninourpr0jectAESxxx", cipher_text;
        SqlConnection connection = new SqlConnection("Server=tcp:dmhec6bljx.database.windows.net,1433;Database=SecureDatabase;User ID=roshan1989@dmhec6bljx;Password=Myyearofbirth89;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
        string[] alphabet = new string[36] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j",
        "k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z","0","1","2","3","4","5","6","7","8","9"};
        string _generated = "";
        static Random _random = new Random();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null || String.IsNullOrEmpty(Session["username"].ToString()))
            {
                Response.Redirect("~/../user_login.aspx");
            }


        }
        protected string generate()
        {
            for (int i = 0; i < 8; i++)
            {
                int _index = _random.Next(0, 35);
                _generated = String.Concat(_generated, alphabet[_index]);
            }
            return _generated;
        }
        public Boolean checkID(string compare, string compare2)
        {
            SqlDataReader input_data;
            SqlCommand SelectCommand;
            SelectCommand = new SqlCommand("SELECT User_Name,Pass_Name FROM login", connection);
            connection.Open();
            input_data = SelectCommand.ExecuteReader();


            while (input_data.Read())
            {

                if (decryptionPlaintext(input_data[0].ToString(), encryption_key).Equals(compare) && decryptionPlaintext(input_data[1].ToString(), encryption_key).Equals(compare2))
                {
                    connection.Close();
                    return true;
                }

            }

            connection.Close();
            return false;

        }
        public string encryptPlaintext(string plain_text, string _encryption_key)
        {
            RijndaelManaged cipher_box = new RijndaelManaged();
            byte[] _PlainText = System.Text.Encoding.Unicode.GetBytes(plain_text);
            byte[] _encapsulate = Encoding.ASCII.GetBytes(_encryption_key.Length.ToString());
            PasswordDeriveBytes _finalkey = new PasswordDeriveBytes(_encryption_key, _encapsulate);
            ICryptoTransform Encryption = cipher_box.CreateEncryptor(_finalkey.GetBytes(16), _finalkey.GetBytes(16));
            MemoryStream stream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(stream, Encryption, CryptoStreamMode.Write);
            cStream.Write(_PlainText, 0, _PlainText.Length);
            cStream.FlushFinalBlock();
            byte[] Cipher = stream.ToArray();
            stream.Close();
            cStream.Close();
            cipher_text = Convert.ToBase64String(Cipher);
            return cipher_text;
        }
        public string decryptionPlaintext(string cipher_text, string _encryption_key)
        {
            RijndaelManaged cipher_box = new RijndaelManaged();
            byte[] cipher = Convert.FromBase64String(cipher_text);
            byte[] _encapsulate = Encoding.ASCII.GetBytes(_encryption_key.Length.ToString());
            PasswordDeriveBytes _finalkey = new PasswordDeriveBytes(_encryption_key, _encapsulate);
            ICryptoTransform Decryption = cipher_box.CreateDecryptor(_finalkey.GetBytes(16), _finalkey.GetBytes(16));
            MemoryStream stream = new MemoryStream(cipher);
            CryptoStream cStream = new CryptoStream(stream, Decryption, CryptoStreamMode.Read);
            byte[] plain_text = new byte[cipher.Length];
            int counter = cStream.Read(plain_text, 0, plain_text.Length);
            stream.Close();
            cStream.Close();
            string plaintext = Encoding.Unicode.GetString(plain_text, 0, counter);
            return plaintext;
        }

        public string checkMatch(string compare, string compare2)
        {
            String falseval = "3";
            SqlDataReader input_data;
            SqlCommand SelectCommand;
            SelectCommand = new SqlCommand("SELECT User_Name,Pass_Name,Status FROM login", connection);
            connection.Open();
            input_data = SelectCommand.ExecuteReader();


            while (input_data.Read())
            {

                if (decryptionPlaintext(input_data[0].ToString(), encryption_key).Equals(compare) && decryptionPlaintext(input_data[1].ToString(), encryption_key).Equals(compare2))
                {

                    falseval = input_data[2].ToString();
                }

            }

            connection.Close();
            return falseval;
        }
        public Boolean checkUser(string compare)
        {

            SqlDataReader input_data;
            SqlCommand SelectCommand;
            SelectCommand = new SqlCommand("SELECT User_name FROM user_agent", connection);
            connection.Open();
            input_data = SelectCommand.ExecuteReader();


            while (input_data.Read())
            {

                if (decryptionPlaintext(input_data[0].ToString(), encryption_key).Equals(compare))
                {
                    connection.Close();
                    return true;
                }

            }

            connection.Close();
            return false;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("DBA_Reset.aspx");
        }
        public Boolean checkAttempt(string compare)
        {

            SqlDataReader input_data;
            SqlCommand SelectCommand;
            SelectCommand = new SqlCommand("SELECT User_name FROM user_agent where User_name=@username", connection);
            SelectCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = encryptPlaintext(compare, encryption_key);
            connection.Open();
            input_data = SelectCommand.ExecuteReader();


            while (input_data.Read())
            {

                if (decryptionPlaintext(input_data[0].ToString(), encryption_key).Equals(compare))
                {

                    return false;
                }

            }

            connection.Close();
            return true;
        }
        public void update(string compare)
        {

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = new SqlCommand(" Update login set Attempts = Attempts + 1 where User_Name=@username", connection);
            adapter.UpdateCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = encryptPlaintext(compare, encryption_key);
            connection.Open();
            adapter.UpdateCommand.ExecuteNonQuery();
            connection.Close();
        }
        public void reset(string compare)
        {

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.UpdateCommand = new SqlCommand(" Update login set Attempts = 0 where User_Name=@username", connection);
            adapter.UpdateCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = encryptPlaintext(compare, encryption_key);
            connection.Open();
            adapter.UpdateCommand.ExecuteNonQuery();
            connection.Close();
        }

        protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
        {
            if (this.CreateUserWizard1.UserName != " " && this.CreateUserWizard1.Password != " ")
            {
                TextBox name = ((TextBox)this.FindControlRecursive(this, "TextBox14"));
                SqlDataAdapter adapter_two = new SqlDataAdapter();
                adapter_two.InsertCommand = new SqlCommand(" INSERT INTO user_agent VALUES(@Name,@Age,@Nationality,@Sex,@Email,@Present_location,@current_mission,@User_Name,@Status, @division)", connection);
                adapter_two.InsertCommand.Parameters.Add("@User_Name", SqlDbType.VarChar).Value = encryptPlaintext(this.CreateUserWizard1.UserName.ToString(), encryption_key);
                adapter_two.InsertCommand.Parameters.Add("@Name", SqlDbType.VarChar).Value = encryptPlaintext(name.Text.ToString(), encryption_key);
                adapter_two.InsertCommand.Parameters.Add("@Age", SqlDbType.VarChar).Value = encryptPlaintext(((TextBox)this.FindControlRecursive(this, "TextBox7")).Text, encryption_key);
                adapter_two.InsertCommand.Parameters.Add("@Nationality", SqlDbType.VarChar).Value = encryptPlaintext(((TextBox)this.FindControlRecursive(this, "TextBox8")).Text, encryption_key);
                adapter_two.InsertCommand.Parameters.Add("@Sex", SqlDbType.VarChar).Value = encryptPlaintext(((DropDownList)this.FindControlRecursive(this, "DropDownList2")).Text, encryption_key);
                adapter_two.InsertCommand.Parameters.Add("@Email", SqlDbType.VarChar).Value = encryptPlaintext(this.CreateUserWizard1.Email, encryption_key);
                adapter_two.InsertCommand.Parameters.Add("@Present_location", SqlDbType.VarChar).Value = encryptPlaintext(((TextBox)this.FindControlRecursive(this, "TextBox11")).Text, encryption_key);
                adapter_two.InsertCommand.Parameters.Add("@current_mission", SqlDbType.VarChar).Value = encryptPlaintext(((TextBox)this.FindControlRecursive(this, "TextBox12")).Text, encryption_key);
                adapter_two.InsertCommand.Parameters.Add("@Status", SqlDbType.VarChar).Value = ((DropDownList)this.FindControlRecursive(this, "DropDownList3")).Text;
                System.Web.Security.Roles.AddUserToRole(this.CreateUserWizard1.UserName, ((DropDownList)this.FindControlRecursive(this, "DropDownList3")).Text.ToString());
                if (((DropDownList)this.FindControlRecursive(this, "DropDownList3")).Text.Equals("Super DBA"))
                {
                    adapter_two.InsertCommand.Parameters.Add("@division", SqlDbType.Int).Value = 0;
                }
                else
                {
                    adapter_two.InsertCommand.Parameters.Add("@division", SqlDbType.Int).Value = ((TextBox)this.FindControlRecursive(this, "TextBox13")).Text;
                }
                connection.Open();
                adapter_two.InsertCommand.ExecuteNonQuery();
                connection.Close();
                loggerobj.logger.Info("user :" + User.Identity.Name.ToString() + " created a new user account user_name: " + this.CreateUserWizard1.UserName.ToString()
                      + " of status: " + ((DropDownList)this.FindControlRecursive(this, "DropDownList3")).Text.ToString() + " name: " + name.Text.ToString());
                Response.Redirect("DBA_login.aspx");
            }
            else
            {
                Response.Redirect("DBA_login.aspx");
            }
        }
        private Control FindControlRecursive(Control rootControl, string controlID)
        {
            if (rootControl.ID == controlID) return rootControl;
            foreach (Control controlToSearch in rootControl.Controls)
            {


                Control controlToReturn =


                            FindControlRecursive(controlToSearch, controlID);


                if (controlToReturn != null) return controlToReturn;


            }


            return null;
        }
        public Boolean checkdiv(string compare, string username)
        {

            SqlDataReader input_data;
            SqlCommand SelectCommand;
            if (!compare.Equals(username))
            {
                SelectCommand = new SqlCommand("SELECT User_name FROM user_agent where division=(select division from user_agent where User_name=@username)", connection);
                SelectCommand.Parameters.Add("@username", SqlDbType.VarChar).Value = encryptPlaintext(username, encryption_key);
                SelectCommand.Parameters.Add("@compare", SqlDbType.VarChar).Value = encryptPlaintext(compare, encryption_key);
                connection.Open();
                input_data = SelectCommand.ExecuteReader();


                while (input_data.Read())
                {

                    if (decryptionPlaintext(input_data[0].ToString(), encryption_key).Equals(compare))
                    {
                        connection.Close();
                        return true;
                    }

                }

            }
            else
            {
                connection.Close();
                return false;

            }

            return false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/DBA/DBA_Reset.aspx");
        }


    }
}