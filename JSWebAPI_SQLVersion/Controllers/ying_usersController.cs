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
    public class ying_logsController : ApiController
    {
        
        
        //---------------------------------------------------------Insert operation using SQL---------------------------------------------
        [HttpPost]
        [ActionName("Addlog")]
        public int Addlog(string username, string modulename)
        {

           
                    MySqlConnection myConnection = new MySqlConnection();
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

                    MySqlCommand sqlCmd = new MySqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "INSERT INTO ying_logs (username,appname,modulename,usestarttime) Values (@username,'YING',@modulename,NOW())";
                    sqlCmd.Connection = myConnection;


                    sqlCmd.Parameters.AddWithValue("@username", username);
                    sqlCmd.Parameters.AddWithValue("@modulename", modulename);
   


            try
            {
                myConnection.Open();
                int rowInserted = sqlCmd.ExecuteNonQuery();
                //create success
                return 1;
            }
            catch (Exception)
            {
                //create failed.
                return 2;

            }
            finally
            {
                myConnection.Close();
            }
              
        }

        //-----------------------------------queryuserapicounts-----------------------------------------------------------------------------
        [HttpGet]
        [ActionName("QueryAPIUsagebyUsername")]
        public int QueryAPIUsagebyUsername(string username)
        {
            try
            {

                string sql = "Select * from ying_logs where username='" + username + "' and  date(usestarttime)=date(CURDATE())  ";
                string connStr = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
                MySqlConnection connsql = new MySqlConnection(connStr);
                if (connsql.State.ToString() == "Closed") connsql.Open();
                MySqlCommand Cmd = new MySqlCommand(sql, connsql);
                DataTable dt = new DataTable();
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = Cmd;
                sda.Fill(dt);
                if (dt.Rows.Count >= 30)
                {
                    //use API more than 30 times for today
                    return 1;
                }
                else
                {
                    //less than 30 and it's ok
                    return 2;
                }
            }
            catch (Exception)
            {
                //call API error.
                return 3;

            }
        }




    }
}
