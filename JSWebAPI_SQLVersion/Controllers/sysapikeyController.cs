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
        public List<sysapikey> GetSysAPIKeyByName(string provider, string keyname, string securitycode)
        {

            if (securitycode == "jsadlappdev")
            {

                string conString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
                var con = new MySqlConnection(conString);

                MySqlCommand cmd = new MySqlCommand(" select id,apikey_provider,apikey_name,apikey_value1,apikey_value2,endpoint_1,endpoint_2,ref_url,is_free,apply_email,apply_password,apply_info,apply_date,expire_date,valid_flag  from sys_public_api_keys where apikey_provider ='" + provider + "' and apikey_name='" + keyname + "' and valid_flag='Y' ", con);
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
                        //   create_date = (DateTime)row["create_date"],
                        //  unvalid_date= (DateTime)row["unvalid_date"],
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

                MySqlCommand cmd = new MySqlCommand(" select id,apikey_provider,apikey_name,apikey_value1,apikey_value2,endpoint_1,endpoint_2,ref_url,is_free,apply_email,apply_password,apply_info,apply_date,expire_date,valid_flag  from sys_public_api_keys where valid_flag='Y'  order by apikey_provider, apikey_name, id ", con);
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
                        //   create_date = (DateTime)row["create_date"],
                        //  unvalid_date= (DateTime)row["unvalid_date"],
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

    }
}
