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
    public class sysapikeyController : ApiController
    {
        //-----------------------------------QueryGetAPIKeys By Name-----------------------------------------------------------------------------
        [HttpGet]
        [ActionName("GetSysAPIKeyByName")]
        public List<sysapikey> GetSysAPIKeyByName(string appname,string provider, string keyname, string securitycode)
        {

            if (securitycode == "jsadlappdev")
            {

                string conString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
                var con = new MySqlConnection(conString);

                MySqlCommand cmd = new MySqlCommand(" select id,app_name,apikey_provider,apikey_name,apikey_value1,apikey_value2,endpoint_1,endpoint_2,ref_url,is_free,apply_email,apply_password,apply_info,apply_date,expire_date,valid_flag  from sys_public_api_keys where app_name ='" + appname + "' and apikey_provider ='" + provider + "' and apikey_name='" + keyname + "' and valid_flag='Y' ", con);
                cmd.CommandType = CommandType.Text;
                // Create a DataAdapter to run the command and fill the DataTable
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<sysapikey> allkeys = new List<sysapikey>();
                foreach (DataRow row in dt.Rows)
                {
                    allkeys.Add(new sysapikey()
                    {
                        id = (int)row["id"],
                        app_name = row["app_name"].ToString(),
                        apikey_provider = row["apikey_provider"].ToString(),
                        apikey_name = row["apikey_name"].ToString(),
                        apikey_value1 = row["apikey_value1"].ToString(),
                        apikey_value2 = row["apikey_value2"].ToString(),
                        endpoint_1 = row["endpoint_1"].ToString(),
                        endpoint_2 = row["endpoint_2"].ToString(),
                        ref_url = row["ref_url"].ToString(),
                        is_free = row["is_free"].ToString(),
                        apply_email = row["apply_email"].ToString(),
                        apply_password = row["apply_password"].ToString(),
                        apply_info = row["apply_info"].ToString(),
                        apply_date = (DateTime)row["apply_date"],
                        expire_date= (DateTime)row["expire_date"],
                        valid_flag = row["valid_flag"].ToString()
                    });
                }

                if (allkeys == null)
                {
                    return null;

                }
                else
                {
                    return allkeys;
                }


            }

            else
            {
                return null;
            }
        }



        //-----------------------------------QueryGetAPIKeysALL-----------------------------------------------------------------------------
        [HttpGet]
        [ActionName("GetSysAPIKeyALL")]
        public List<sysapikey> GetSysAPIKeyALL(string securitycode)
        {

            if (securitycode == "jsadlappdev")
            {

                string conString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
                var con = new MySqlConnection(conString);

                MySqlCommand cmd = new MySqlCommand(" select id,app_name,apikey_provider,apikey_name,apikey_value1,apikey_value2,endpoint_1,endpoint_2,ref_url,is_free,apply_email,apply_password,apply_info,apply_date,expire_date,valid_flag  from sys_public_api_keys  order by apikey_provider, apikey_name, id ", con);
                cmd.CommandType = CommandType.Text;
                // Create a DataAdapter to run the command and fill the DataTable
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                List<sysapikey> allkeys = new List<sysapikey>();
                foreach (DataRow row in dt.Rows)
                {
                    allkeys.Add(new sysapikey()
                    {
                        id = (int)row["id"],
                        app_name = row["app_name"].ToString(),
                        apikey_provider = row["apikey_provider"].ToString(),
                        apikey_name = row["apikey_name"].ToString(),
                        apikey_value1 = row["apikey_value1"].ToString(),
                        apikey_value2 = row["apikey_value2"].ToString(),
                        endpoint_1 = row["endpoint_1"].ToString(),
                        endpoint_2 = row["endpoint_2"].ToString(),
                        ref_url = row["ref_url"].ToString(),
                        is_free = row["is_free"].ToString(),
                        apply_email = row["apply_email"].ToString(),
                        apply_password = row["apply_password"].ToString(),
                        apply_info = row["apply_info"].ToString(),
                        apply_date = (DateTime)row["apply_date"],
                        expire_date = (DateTime)row["expire_date"],
                        valid_flag = row["valid_flag"].ToString()
                    });
                }

                if (allkeys == null)
                {
                    return null;

                }
                else
                {
                    return allkeys;
                }


            }

            else
            {
                return null;
            }
        }



        //---------------------------------------------------------Insert new api keys---------------------------------------------
        [HttpPost]
        [ActionName("AddNewAPIKey")]
        public int AddNewAPIKey([FromBody] sysapikey newapi)
        {

           

                    MySqlConnection myConnection = new MySqlConnection();
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

                    MySqlCommand sqlCmd = new MySqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "INSERT INTO sys_public_api_keys (app_name,apikey_provider,apikey_name,apikey_value1,apikey_value2,endpoint_1,endpoint_2,ref_url,is_free,apply_email,apply_password,apply_info,apply_date,expire_date,valid_flag) Values (@app_name,@apikey_provider,@apikey_name,@apikey_value1,@apikey_value2,@endpoint_1,@endpoint_2,@ref_url,@is_free,@apply_email,@apply_password,@apply_info,(CURDATE()),@expire_date,'Y')";
                    sqlCmd.Connection = myConnection;

                    sqlCmd.Parameters.AddWithValue("@app_name", newapi.app_name);
            sqlCmd.Parameters.AddWithValue("@apikey_provider", newapi.apikey_provider);
            sqlCmd.Parameters.AddWithValue("@apikey_name", newapi.apikey_name);
            sqlCmd.Parameters.AddWithValue("@apikey_value1", newapi.apikey_value1);
            sqlCmd.Parameters.AddWithValue("@apikey_value2", newapi.apikey_value2);
            sqlCmd.Parameters.AddWithValue("@endpoint_1", newapi.endpoint_1);
            sqlCmd.Parameters.AddWithValue("@endpoint_2", newapi.endpoint_2);
            sqlCmd.Parameters.AddWithValue("@ref_url", newapi.ref_url);
            sqlCmd.Parameters.AddWithValue("@is_free", newapi.is_free);
            sqlCmd.Parameters.AddWithValue("@apply_email", newapi.apply_email);
            sqlCmd.Parameters.AddWithValue("@apply_password", newapi.apply_password);
            sqlCmd.Parameters.AddWithValue("@apply_info", newapi.apply_info);
            sqlCmd.Parameters.AddWithValue("@expire_date", newapi.expire_date);




            try
                    {
                        myConnection.Open();
                        int rowInserted = sqlCmd.ExecuteNonQuery();

                        return 1;
                    }
                    catch (Exception)
                    {

                        return 2;

                    }
                    finally
                    {
                        myConnection.Close();
                    }
             
        }



        //------------------------------------------delete  operation using SQL -----------------------------------------------------------------------------
        [HttpDelete]
        [ActionName("DeleteByID")]
        public int DeleteByID(int id)
        {


            

                MySqlConnection myConnection = new MySqlConnection();
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "delete from sys_public_api_keys where id=" + id + "";
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                try
                {
                    int rowDeleted = sqlCmd.ExecuteNonQuery();

                    return 1;

                }
                catch
                {
                    return 2;

                }
                finally
                {
                    myConnection.Close();
                }
          

        }


        //---------------------------------------------update operation using SQL-----------------------------------------------------------------------
        [HttpPut]
        [ActionName("UpdateValidFlag")]
        public int UpdateValidFlag(int id)
        {

            
                MySqlConnection myConnection = new MySqlConnection();
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "update  sys_public_api_keys set valid_flag='N' where  id=" + id + "";
                sqlCmd.Connection = myConnection;

            

                myConnection.Open();
                try
                {
                    int rowInserted = sqlCmd.ExecuteNonQuery();
                    return 1;
                }
                catch (Exception)
                {
                    return 2;

                }
                finally
                {
                    myConnection.Close();
                }

          


        }



    }
}
