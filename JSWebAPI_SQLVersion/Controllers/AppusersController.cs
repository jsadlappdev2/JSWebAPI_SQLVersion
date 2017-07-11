using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.OleDb;
using System.Data;
using System.Configuration;
using JSWebAPI_SQLVersion.Models;
using System.Data.SqlClient;

namespace JSWebAPI_SQLVersion.Controllers
{
    public class AppusersController : ApiController
    {



        //Query by UserID
        [HttpGet]
        [ActionName("GetUserByID")]
        public appusers Get(int id)
        {
            //return listEmp.First(e => e.ID == id);   
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["JSWebAPIContext"].ConnectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select id,username,userpassword,useremail from app_users  where  id  =" + id + " ";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            appusers users = null;
            while (reader.Read())
            {
                users = new appusers();
                users.UserId = Convert.ToInt32(reader.GetValue(0));
                users.UserName = reader.GetValue(1).ToString();
                users.UserPassword = reader.GetValue(2).ToString();
                users.UserMail = reader.GetValue(3).ToString();

            }

            return users;


        }

        //Query by Username and Password
        [HttpGet]
        [ActionName("GetUserByNameandPassword")]
        [Route("api/Appusers/GetUserByNameandPassword/{username}/{password}")]
        public appusers Get(string username, string password)
        {
            //return listEmp.First(e => e.ID == id);   
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["JSWebAPIContext"].ConnectionString;




            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select id,username,userpassword,useremail from app_users  where  username ='"+username+"' and userpassword ='"+password+"'  ";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            appusers users = null;
            while (reader.Read())
            {
                users = new appusers();
                users.UserId = Convert.ToInt32(reader.GetValue(0));
                users.UserName = reader.GetValue(1).ToString();
                users.UserPassword = reader.GetValue(2).ToString();
                users.UserMail = reader.GetValue(3).ToString();

            }

            return users;


        }

        //insert
        [HttpPost]
        [ActionName("AddUser")]
        public void AddUser(appusers appuser)
        {
           
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["JSWebAPIContext"].ConnectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO app_users(username,userpassword,useremail,valid_flag,createdate) Values (@UserName,@UserPassword,@UserMail,'Y', convert(date,GETDATE()))";

            sqlCmd.Connection = myConnection;
          


           sqlCmd.Parameters.AddWithValue("@UserName", appuser.UserName);
           sqlCmd.Parameters.AddWithValue("@UserPassword", appuser.UserPassword);
           sqlCmd.Parameters.AddWithValue("@UserMail", appuser.UserMail);
        
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        //delete
        [HttpDelete]
        [ActionName("DeleteUserByID")]
        public void DeleteUserByID(int id)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["JSWebAPIContext"].ConnectionString;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from app_users where id=" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            int rowDeleted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }



        //update
        [HttpPut]
        [ActionName("UpdateUserByID")]
        public void UpdateUserByID(int id, appusers appuser)
        {
           

            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["JSWebAPIContext"].ConnectionString;
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "update  app_users set userpassword=@UserPassword, useremail=@UserMail, updatedate=convert(date,GETDATE())  where id=" + id + "";
            sqlCmd.Connection = myConnection;
         

            sqlCmd.Parameters.AddWithValue("@UserPassword", appuser.UserPassword);
            sqlCmd.Parameters.AddWithValue("@UserMail", appuser.UserMail);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }


    }
}
