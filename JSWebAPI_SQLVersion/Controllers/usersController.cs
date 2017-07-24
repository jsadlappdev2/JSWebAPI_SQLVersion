﻿using System;
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
            if (count.ToString() == "0")
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

                    return 2;
                }
                catch (Exception)
                {

                    return 3;

                }
                finally
                {
                    myConnection.Close();
                }
            }
            else
            {

                return 1;



            }
        }



        //------------------------------------function to check records exist or not----------------------------------------------
        public int checkexistbyusername(string username)
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


        public int checkexistbyuserid(int id)
        {

            string sql = "Select *  from users where userid=" + id + " ";
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
