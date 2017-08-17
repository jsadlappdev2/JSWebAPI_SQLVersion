using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace JSWebAPI_SQLVersion.WebForms.Ying
{
    public partial class ying_longin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int usercount = checkexist(username.Text.Trim(), password.Text.Trim());

            if (usercount == 1)
            {
                Response.Redirect("~/WebForms/Ying/ying_mainpage.aspx");

            }
            else
            {
                msg.Text = "Invalid username and password. Please try again.";
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            username.Text = "";
            password.Text = "";
            msg.Text = "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        public int checkexist(string username, string password)
        {

            string sql = "Select *  from sys_users_apiwebserver where app_name ='Ying' and admin_name='" + username + "' and admin_password='" + password + "' and valid_flag='Y' ";
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