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
        public int Addlog([FromBody] ying_logs log)
        {

           
                    MySqlConnection myConnection = new MySqlConnection();
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

                    MySqlCommand sqlCmd = new MySqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "INSERT INTO ying_logs (username,appname,modulename,usestarttime) Values (@username,'YING',@modulename,NOW())";
                    sqlCmd.Connection = myConnection;


                    sqlCmd.Parameters.AddWithValue("@username", log.username);
                    sqlCmd.Parameters.AddWithValue("@modulename", log.modulename);
   


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




    }
}
