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
    public class newtodosController : ApiController
    {
        //API status:
        //1: user has existed
        //2: create successfully
        //3: create failed.
        //4: delete successfully
        //5: delete failed.
        //6: delete not found userid.
        //7: update successfully
        //8: delete failed.
        //9: delete not found userid.

        //---------------------------------------------------------Insert operation using SQL---------------------------------------------
        [HttpPost]
        [ActionName("AddTodo")]
        public int AddTodo([FromBody] newtodos todo)
        {

            //check exist or not 
            int count = 0;
            count = checkexistbyDescription(todo.username,todo.Description);
            int userexit = 0;
            userexit = checkusername(todo.username);
            if (userexit > 0)
            {
                if (count.ToString() == "0")
                {

                    MySqlConnection myConnection = new MySqlConnection();
                    myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

                    MySqlCommand sqlCmd = new MySqlCommand();
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.CommandText = "INSERT INTO NewTodoItem (username,Description,DueDate,isDone) Values (@username,@Description,@DueDate,@isDone)";
                    sqlCmd.Connection = myConnection;

                    sqlCmd.Parameters.AddWithValue("@username", todo.username);
                    sqlCmd.Parameters.AddWithValue("@Description", todo.Description);
                    sqlCmd.Parameters.AddWithValue("@DueDate", todo.DueDate);
                    sqlCmd.Parameters.AddWithValue("@isDone", todo.isDone);

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
            else
            {
                //no existing username
                return 4;


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
                sqlCmd.CommandText = " select *  from NewTodoItem where id=" + id + " ";
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                reader = sqlCmd.ExecuteReader();
                newtodos todo = null;
                while (reader.Read())
                {
                    todo = new newtodos();
                    todo.id = (int)reader.GetValue(0);
                    todo.username =  reader.GetValue(1).ToString();
                    todo.Description = reader.GetValue(2).ToString();
                    todo.DueDate = reader.GetValue(3).ToString();
                    todo.isDone = (bool)reader.GetValue(4);

                }
                if (todo == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Todo for " + id.ToString() + " Not Found!");
                }
                else
                {
                    return Request.CreateResponse<newtodos>(HttpStatusCode.OK, todo);                 }
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
        public List<todos> QueryAll2(string  username)
        {



            string conString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
            var con = new MySqlConnection(conString);

            MySqlCommand cmd = new MySqlCommand(" select *  from NewTodoItem where username="+username+" order by  isdone,id asc ", con);
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
        public int DeleteByID(int id)
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
                sqlCmd.CommandText = "delete from NewTodoItem where id=" + id + "";
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                try
                {
                    int rowDeleted = sqlCmd.ExecuteNonQuery();

                    return 4;

                }
                catch
                {
                    return 5;

                }
                finally
                {
                    myConnection.Close();
                }
            }
            else
            {

                return 6;

            }

        }



        //---------------------------------------------update operation using SQL-----------------------------------------------------------------------
        [HttpPut]
        [ActionName("UpdateTodo")]
        public int UpdateTodo([FromUri]int id, [FromBody]newtodos todo)
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
                sqlCmd.CommandText = "update  NewTodoItem set Description=@Description,DueDate=@DueDate,isDone=@isDone  where  id=" + id + "";
                sqlCmd.Connection = myConnection;


                sqlCmd.Parameters.AddWithValue("@Description", todo.Description);
                sqlCmd.Parameters.AddWithValue("@DueDate", todo.DueDate);
                sqlCmd.Parameters.AddWithValue("@isDone", todo.isDone);

                myConnection.Open();
                try
                {
                    int rowInserted = sqlCmd.ExecuteNonQuery();
                    return 7;
                }
                catch (Exception)
                {
                    return 8;

                }
                finally
                {
                    myConnection.Close();
                }

            }
            else
            {

                return 9;

            }


        }


        //------------------------------------function to check records exist or not----------------------------------------------
        public int checkexistbyDescription(string username,string Description)
        {

            string sql = "Select *  from NewTodoItem where username='"+ username + "' and  Description='" + Description + "' ";
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

            string sql = "Select *  from NewTodoItem where id=" + id + " ";
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


        public int checkusername(string  username)
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


    }
}