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
    public class EmployeedetailsController : ApiController
    {

        //Query
        [HttpGet]
        [ActionName("QueryByID")]
        public Employeedetails QueryByID(int id)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["JSWebAPIContext"].ConnectionString;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select id_number,given_name,family_name,preferred_name,gender from dbo.employee_for_test where id_number=" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Employeedetails emp = null;
            while (reader.Read())
            {
                emp = new Employeedetails();
                emp.id_number = Convert.ToInt32(reader.GetValue(0));
                emp.given_name = reader.GetValue(1).ToString();
                emp.family_name = reader.GetValue(2).ToString();
                emp.preferred_name = reader.GetValue(3).ToString();
                emp.gender = reader.GetValue(4).ToString();
            }
            if (emp == null)
            {
                return null;
            }
            else
            {
                return emp;
            }
        }


    }


}
