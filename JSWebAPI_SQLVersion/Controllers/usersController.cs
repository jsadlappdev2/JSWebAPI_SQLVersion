using System;
using System.Collections.Generic;
using System.Linq;
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

namespace JSWebAPI_SQLVersion.Controllers
{
    public class usersController : ApiController
    {
         //API status:
         //1: user has existed
         //2: create successfully
         //3: create failed.
        
        
        //---------------------------------------------------------Insert operation using SQL---------------------------------------------
        [HttpPost]
        [ActionName("AddNewUser")]
        public int AddNewUser([FromBody] users user)
        {

            //check exist or not 
            int count = 0;
            count = checkexistbyusername(user.username);
            int email_count = 0;
            email_count = checkexistbyemail(user.email);

            if (count.ToString() == "0")
            {

                if (email_count.ToString() == "0")
                {

                    MySqlConnection myConnection = new MySqlConnection();
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

                    MySqlCommand sqlCmd = new MySqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "INSERT INTO users (username,email,password,create_date,valid_flag) Values (@username,@email,@password,NOW(),1)";
                    sqlCmd.Connection = myConnection;


                    sqlCmd.Parameters.AddWithValue("@username", user.username);
                    sqlCmd.Parameters.AddWithValue("@email", user.email);
                    sqlCmd.Parameters.AddWithValue("@password", user.password);


                    try
                    {
                        myConnection.Open();
                        int rowInserted = sqlCmd.ExecuteNonQuery();
                        //create success
                        return 2;
                    }
                    catch (Exception)
                    {
                      //create failed.
                        return 3;

                    }
                    finally
                    {
                        myConnection.Close();
                    }
                }

                else
                { 
                    //email has exited
                    return 4;

                }
            }
            else
            {

                //username has exited
                return 1;



            }
        }


        //-----------------------------------QuerybyusernameGetdetails-----------------------------------------------------------------------------
        [HttpGet]
        [ActionName("QueryByUsername")]
        public List<users> QueryByUsername(string username)
        {



            string conString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
            var con = new MySqlConnection(conString);

            MySqlCommand cmd = new MySqlCommand(" select *  from users where username ='"+ username +"' and valid_flag=1 ", con);
            cmd.CommandType = CommandType.Text;
            // Create a DataAdapter to run the command and fill the DataTable
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<users> alluser = new List<users>();
            foreach (DataRow row in dt.Rows)
            {
                alluser.Add(new users()
                {
                    userid = (int)row["userid"],
                    username = row["username"].ToString(),
                    email = row["email"].ToString(),
                    password = row["password"].ToString(),
               //   create_date = (DateTime)row["create_date"],
               //  unvalid_date= (DateTime)row["unvalid_date"],
                    valid_flag = (bool)row["valid_flag"]
                });
            }

            if (alluser == null)
            {
                return null;

            }
            else
            {
                return alluser;
            }


        }

        //-----------------------------------Querybyusernametocheck-----------------------------------------------------------------------------
        [HttpGet]
        [ActionName("QueryByUsernameSimple")]
        public int  QueryByUsernameSimple(string username)
        {
            try
            {
                
                string sql = "Select *  from users where username='" + username + "' and valid_flag='1'  ";
                string connStr = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
                MySqlConnection connsql = new MySqlConnection(connStr);
                if (connsql.State.ToString() == "Closed") connsql.Open();
                MySqlCommand Cmd = new MySqlCommand(sql, connsql);
                DataTable dt = new DataTable();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = Cmd;
                sda.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    return 10;
                }
                else
                {
                    return 11;
                }
            }
            catch (Exception)
            {
                return 12;

            }
        }



        //-----------------------------------LoginUserValidation-----------------------------------------------------------------------------
        [HttpGet]
        [ActionName("LoginUserValidation")]
        public int LoginUserValidation(string usernameoremail, string password)
        {
            try
            {

                string sql = "Select *  from users where ( username='" + usernameoremail + "' or email ='" + usernameoremail + "' ) and password='" + password + "' and valid_flag='1'  ";
                string connStr = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
                MySqlConnection connsql = new MySqlConnection(connStr);
                if (connsql.State.ToString() == "Closed") connsql.Open();
                MySqlCommand Cmd = new MySqlCommand(sql, connsql);
                DataTable dt = new DataTable();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = Cmd;
                sda.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    //1: not right user 
                    return 1;
                }
                else
                {
                    //2: right user 
                    return 2;
                }
            }
            catch (Exception)
            {
                //3: checking failed with error
                return 3;

            }
        }




        //------------------------------------function to check records exist or not----------------------------------------------
        public int checkexistbyusername(string username)
        {

            string sql = "Select *  from users where username='" + username + "' and valid_flag=1  ";
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


        public int checkexistbyuserid(int id)
        {

            string sql = "Select *  from users where userid=" + id + "  and valid_flag=1 ";
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


        public int checkexistbyemail(string email)
        {

            string sql = "Select *  from users where email='" + email + "' and valid_flag=1  ";
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



    }
}
