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
    public class EmployeesController : ApiController
    {
        //Query
        [HttpGet]
        [ActionName("GetEmployeeByID")]
        public Employee Get(int id)
        {
            //return listEmp.First(e => e.ID == id);   
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["JSWebAPIContext"].ConnectionString;
           



            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from Employee where EmployeeId=" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Employee emp = null;
            while (reader.Read())
            {
                emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(reader.GetValue(0));
                emp.Name = reader.GetValue(1).ToString();
                emp.ManagerId = Convert.ToInt32(reader.GetValue(2));
            }
           
                return emp;
    

        }

        //insert
        [HttpPost]
        [ActionName("AddEmployee")]
        public void AddEmployee(Employee employee)
        {
            //int maxId = listEmp.Max(e => e.ID);   
            //employee.ID = maxId + 1;   
            //listEmp.Add(employee);   


            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["JSWebAPIContext"].ConnectionString;
            //SqlCommand sqlCmd = new SqlCommand("INSERT INTO tblEmployee (EmployeeId,Name,ManagerId) Values (@EmployeeId,@Name,@ManagerId)", myConnection);   
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO Employee (Name,ManagerId) Values (@Name,@ManagerId)";
            sqlCmd.Connection = myConnection;
        

            sqlCmd.Parameters.AddWithValue("@Name", employee.Name);
            sqlCmd.Parameters.AddWithValue("@ManagerId", employee.ManagerId);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        //delete
        [HttpDelete]
        [ActionName("DeleteEmployeeByID")]
        public void DeleteEmployeeByID(int id)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["JSWebAPIContext"].ConnectionString;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from Employee where EmployeeId=" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            int rowDeleted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }



        //update
        [HttpPut]
        [ActionName("UpdateEmployee")]
        public void UpdateEmployeebyID(int id, Employee employee)
        {
            //int maxId = listEmp.Max(e => e.ID);   
            //employee.ID = maxId + 1;   
            //listEmp.Add(employee);   


            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["JSWebAPIContext"].ConnectionString;
            //SqlCommand sqlCmd = new SqlCommand("INSERT INTO tblEmployee (EmployeeId,Name,ManagerId) Values (@EmployeeId,@Name,@ManagerId)", myConnection);   
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "update  Employee set name=@Name,managerid=@ManagerId  where EmployeeId=" + id + "";
            sqlCmd.Connection = myConnection;
        

            sqlCmd.Parameters.AddWithValue("@Name", employee.Name);
            sqlCmd.Parameters.AddWithValue("@ManagerId", employee.ManagerId);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

    }







}
