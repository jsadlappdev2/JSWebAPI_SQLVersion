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
    public class todosController : ApiController
    {
        //---------------------------------------------------------Insert operation using SQL---------------------------------------------
        [HttpPost]
        [ActionName("AddTodo")]
        public HttpResponseMessage AddTodo([FromBody] todos todo)
        {

            //check exist or not 
            int count = 0;
            count = checkexistbyDescription(todo.Description);
            if (count.ToString() == "0")
            {

                MySqlConnection myConnection = new MySqlConnection();
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "INSERT INTO TodoItem (Description,DueDate,isDone) Values (@Description,@DueDate,@isDone)";
                sqlCmd.Connection = myConnection;


                sqlCmd.Parameters.AddWithValue("@Description", todo.Description);
                sqlCmd.Parameters.AddWithValue("@DueDate", todo.DueDate);
                sqlCmd.Parameters.AddWithValue("@isDone", todo.isDone);

                try
                {
                    myConnection.Open();
                    int rowInserted = sqlCmd.ExecuteNonQuery();

                    return Request.CreateErrorResponse(HttpStatusCode.Created, "Task for " + todo.Description + " has been created!");
                }
                catch (Exception)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing AddTodo.");

                }
                finally
                {
                    myConnection.Close();
                }
            }
            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, " Task  " + todo.Description.ToString() + " has exited and cannot be created again.");



            }
        }


        //-----------------------------------QuerybyID Operation using SQL-----------------------------------------------------------------------------
        [HttpGet]
        [ActionName("QueryByID")]
        public HttpResponseMessage QueryByID(int id)
        {
            try
            {
                MySqlDataReader reader = null;
                MySqlConnection myConnection = new MySqlConnection();
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = " select *  from TodoItem where id=" + id + " ";
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                reader = sqlCmd.ExecuteReader();
                todos todo = null;
                while (reader.Read())
                {
                    todo = new todos();
                    todo.id = (int)reader.GetValue(0);
                    todo.Description = reader.GetValue(1).ToString();
                    todo.DueDate = reader.GetValue(2).ToString();
                    todo.isDone = (bool)reader.GetValue(3);

                }
                if (todo == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Todo for " + id.ToString() + " Not Found!");
                }
                else
                {
                    return Request.CreateResponse<todos>(HttpStatusCode.OK, todo);
                }
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing QueryByID");

            }
        }


        //-----------------------------------Way2: Query all Operation using SQL-----------------------------------------------------------------------------
        //Better way: can get JSON data in a good format.
        [HttpGet]
        [ActionName("QueryAll2")]
        public List<todos> QueryAll2()
        {



            string conString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
            var con = new MySqlConnection(conString);

            MySqlCommand cmd = new MySqlCommand(" select *  from TodoItem order by  id asc ", con);
            cmd.CommandType = CommandType.Text;
            // Create a DataAdapter to run the command and fill the DataTable
            MySqlDataAdapter da = new MySqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<todos> alltodo = new List<todos>();
            foreach (DataRow row in dt.Rows)
            {
                alltodo.Add(new todos()
                {
                    id = (int)row["id"],
                    Description = row["Description"].ToString(),
                    DueDate = row["DueDate"].ToString(),
                    isDone = (bool)row["isDone"]
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

        //------------------------------------------delete  operation using SQL -----------------------------------------------------------------------------
        [HttpDelete]
        [ActionName("DeleteByID")]
        public HttpResponseMessage DeleteByID(int id)
        {


            //check exist or not 
            int count = 0;
            count = checkexistbyID(id);
            if (count >= 1)
            {

                MySqlConnection myConnection = new MySqlConnection();
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "delete from TodoItem where id=" + id + "";
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                try
                {
                    int rowDeleted = sqlCmd.ExecuteNonQuery();

                    return Request.CreateErrorResponse(HttpStatusCode.OK, "Task " + id.ToString() + " has been deleted!");

                }
                catch
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing DeleteByID.");

                }
                finally
                {
                    myConnection.Close();
                }
            }
            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, " Task " + id.ToString() + " Not Found and Delete didn't execute.");

            }

        }



        //---------------------------------------------update operation using SQL-----------------------------------------------------------------------
        [HttpPut]
        [ActionName("UpdateTodo")]
        public HttpResponseMessage UpdateTodo([FromUri]int id, [FromBody]todos todo)
        {

            //check exist or not 
            int count = 0;
            count = checkexistbyID(id);
            if (count >= 1)
            {
                MySqlConnection myConnection = new MySqlConnection();
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "update  TodoItem set Description=@Description,DueDate=@DueDate,isDone=@isDone  where id=" + id + "";
                sqlCmd.Connection = myConnection;


                sqlCmd.Parameters.AddWithValue("@Description", todo.Description);
                sqlCmd.Parameters.AddWithValue("@DueDate", todo.DueDate);
                sqlCmd.Parameters.AddWithValue("@isDone", todo.isDone);

                myConnection.Open();
                try
                {
                    int rowInserted = sqlCmd.ExecuteNonQuery();
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "Task " + id.ToString() + "'s information has been udpated!");
                }
                catch (Exception)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing UpdateTodo.");

                }
                finally
                {
                    myConnection.Close();
                }

            }
            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, " Task " + id.ToString() + " Not Found and Update didn't execute.");

            }


        }


        //------------------------------------function to check records exist or not----------------------------------------------
        public int checkexistbyDescription(string Description)
        {

            string sql = "Select *  from TodoItem where Description='" + Description + "' ";
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


        public int checkexistbyID(int id)
        {

            string sql = "Select *  from TodoItem where id=" + id + " ";
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