using System.Net;
using System.Net.Http;
using System.Web.Http;
using JSWebAPI_SQLVersion.Models;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using Newtonsoft.Json;
using MySql.Data.MySqlClient;
using System.Net.Mail;

namespace JSWebAPI_SQLVersion.Controllers
{
    public class emailController : ApiController
    {

        //API: send password to users
        [HttpPost]
        [ActionName("SendPassword")]
        public int SendPassword([FromBody] email myemail)
        {

            //check exist or not 
            int count = 0;
            count = checkuserexistbyemail(myemail.email_to);
            if (count >= 1)
            {

                string emailcontent = getpasswordbyemail(myemail.email_to);


                int sendemail_status = 0;
                sendemail_status = Sendemail(myemail.email_to, "Your Password for DailyLife App", emailcontent);
               if (sendemail_status == 20)
                {


                    MySqlConnection myConnection = new MySqlConnection();
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

                    MySqlCommand sqlCmd = new MySqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "INSERT INTO email_send_history (userid,email_to,content,send_date,send_from) Values (@userid,@email_to,'"+emailcontent+"',NOW(),'ADMIN')";
                    sqlCmd.Connection = myConnection;

                    sqlCmd.Parameters.AddWithValue("@userid", myemail.userid);
                    sqlCmd.Parameters.AddWithValue("@email_to", myemail.email_to);



                    try
                    {
                        myConnection.Open();
                        int rowInserted = sqlCmd.ExecuteNonQuery();
                        //get password and send email success.
                        return 2;
                    }
                    catch
                    {

                        //get password and send email success but insert history failed.
                        return 3;

                    }
                    finally
                    {
                        myConnection.Close();
                    }
                }
                else
                {
                    //4: Get password success but send email failed!
                    return 4;
                }

            }
            else
            {

                //5: No email exists!
                return 5; 



            }
        }


        public int SendPasswordJustCheckemail([FromBody] emailaddress myemail)
        {

            //check exist or not 
            int count = 0;
            count = checkuserexistbyemail(myemail.email_to);
            if (count >= 1)
            {

                string emailcontent = getpasswordbyemail(myemail.email_to);


                int sendemail_status = 0;
                sendemail_status = Sendemail(myemail.email_to, "Your Password for DailyLife App", emailcontent);
                if (sendemail_status == 20)
                {


                    MySqlConnection myConnection = new MySqlConnection();
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

                    MySqlCommand sqlCmd = new MySqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "INSERT INTO email_send_history (userid,email_to,content,send_date,send_from)  select userid,@email_to,'" + emailcontent + "',NOW(),'ADMIN' from users where email=@email_to ";
                    sqlCmd.Connection = myConnection;

                   
                    sqlCmd.Parameters.AddWithValue("@email_to", myemail.email_to);



                    try
                    {
                        myConnection.Open();
                        int rowInserted = sqlCmd.ExecuteNonQuery();
                        //get password and send email success.
                        return 2;
                    }
                    catch
                    {

                        //get password and send email success but insert history failed.
                        return 3;

                    }
                    finally
                    {
                        myConnection.Close();
                    }
                }
                else
                {
                    //4: Get password success but send email failed!
                    return 4;
                }

            }
            else
            {

                //5: No email exists!
                return 5;



            }
        }

        //API: send password to users jsut check emailto
        [HttpPost]
        [ActionName("SendPasswordJustmail")]
        public int SendPasswordJustmail(string emailto)
        {

            //check exist or not 
            int count = 0;
            count = checkuserexistbyemail(emailto);
            if (count >= 1)
            {

                string emailcontent = getpasswordbyemail(emailto);


                int sendemail_status = 0;
                sendemail_status = Sendemail(emailto, "Your Password for DailyLife App (Do not reply)", emailcontent);
                if (sendemail_status == 20)
                {


                    MySqlConnection myConnection = new MySqlConnection();
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

                    MySqlCommand sqlCmd = new MySqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "INSERT INTO email_send_history (userid,email_to,content,send_date,send_from)  select userid,@email_to,'" + emailcontent + "',NOW(),'ADMIN' from users where email=@email_to ";
                    sqlCmd.Connection = myConnection;

                   
                    sqlCmd.Parameters.AddWithValue("@email_to", emailto);



                    try
                    {
                        myConnection.Open();
                        int rowInserted = sqlCmd.ExecuteNonQuery();
                        //get password and send email success.
                        return 2;
                    }
                    catch
                    {

                        //get password and send email success but insert history failed.
                        return 3;

                    }
                    finally
                    {
                        myConnection.Close();
                    }
                }
                else
                {
                    //4: Get password success but send email failed!
                    return 4;
                }

            }
            else
            {

                //5: No email exists!
                return 5;



            }
        }




        //function to send email through gmail
        public int Sendemail(string send_to, string subject, string content)
        {
            //   MailMessage mm = new MailMessage("jsappserver@gmail.com", "shenjr81@gmail.com", "test from asp.net from jsappserver@gmail.com", "test");

            MailMessage mm = new MailMessage("jsappserver@gmail.com", send_to, subject, content);

            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";

            smtp.EnableSsl = true;

            NetworkCredential NetworkCred = new NetworkCredential("jsappserver@gmail.com", "sjerry81");

            smtp.UseDefaultCredentials = true;

            smtp.Credentials = NetworkCred;

            smtp.Port = 587;
            try
            {

                smtp.Send(mm);

                //  ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
                return 20;
            }
            catch
            {
                //   string error = "Email send fialed: " + e.ToString();

                // ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + error + "');", true);
                return 21;

            }

        }

        //check users exist or not
        public int checkuserexistbyusername(string username)
        {

            string sql = "Select *  from users where username='" + username + "' ";
            string connStr = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
            MySqlConnection connsql = new MySqlConnection(connStr);
            if (connsql.State.ToString() == "Closed") connsql.Open();
            MySqlCommand Cmd = new MySqlCommand(sql, connsql);
            DataTable dt = new DataTable();
            MySqlDataAdapter sda = new MySqlDataAdapter();
            sda.SelectCommand = Cmd;
            sda.Fill(dt);
            return dt.Rows.Count;
        }

        public int checkuserexistbyemail(string email)
        {

            string sql = "Select *  from users where email='" + email + "' ";
            string connStr = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
            MySqlConnection connsql = new MySqlConnection(connStr);
            if (connsql.State.ToString() == "Closed") connsql.Open();
            MySqlCommand Cmd = new MySqlCommand(sql, connsql);
            DataTable dt = new DataTable();
            MySqlDataAdapter sda = new MySqlDataAdapter();
            sda.SelectCommand = Cmd;
            sda.Fill(dt);
            return dt.Rows.Count;
        }


        //get password  by email
        public string getpasswordbyemail(string email)
        {


            string yourpassword = "";
            MySqlDataReader reader = null;
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText =  "select concat(concat(concat('Hi ', ' ', username), '! Your password is: ', password), '. Please try again!', ' Thank you!') from users  where email = '" + email + "' ";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            while (reader.Read())
            {
               yourpassword= reader.GetValue(0).ToString();
            }
            return yourpassword;
           
        }
    }

  
}
