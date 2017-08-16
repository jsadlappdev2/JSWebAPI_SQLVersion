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
    public class ying_urlsController : ApiController
    {
        
        
        //---------------------------------------------------------Insert operation using SQL---------------------------------------------
        [HttpPost]
        [ActionName("Addurl")]
        public int Addurl([FromBody] ying_urls url)
        {

           
                    MySqlConnection myConnection = new MySqlConnection();
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

                    MySqlCommand sqlCmd = new MySqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "INSERT INTO ying_urls (type,url,description,entrytime,valid_flag) Values (@type,@url,@description,NOW(),'Y')";
                    sqlCmd.Connection = myConnection;


                    sqlCmd.Parameters.AddWithValue("@type", url.type);
                    sqlCmd.Parameters.AddWithValue("@url", url.url);
                    sqlCmd.Parameters.AddWithValue("@description", url.description);



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


        //----------------------------------- Query all Operation using SQL-----------------------------------------------------------------------------
        //Better way: can get JSON data in a good format.
        [HttpGet]
        [ActionName("QueryAll")]
        public List<ying_urls> QueryAll()
        {



            string conString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
            var con = new MySqlConnection(conString);

            MySqlCommand cmd = new MySqlCommand(" select id,type,url, description ,valid_flag, entrytime from ying_urls where valid_flag='Y' order by  type,id desc ", con);
            cmd.CommandType = CommandType.Text;
            // Create a DataAdapter to run the command and fill the DataTable
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<ying_urls> alltodo = new List<ying_urls>();
            foreach (DataRow row in dt.Rows)
            {
                alltodo.Add(new ying_urls()
                {
                    id = (int)row["id"],
                    type = row["type"].ToString(),
                    url = row["url"].ToString(),
                    description = row["description"].ToString(),
                    valid_flag = row["valid_flag"].ToString(),
                    entrytime = (DateTime)row["entrytime"]

                });
            }

            if (alltodo == null)
            {
                return null;

            }
            else
            {
                return alltodo;
            }


        }




    }
}
