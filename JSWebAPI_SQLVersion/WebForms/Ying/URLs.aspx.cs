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
    public partial class URLs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridViewBind();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //insert new url
            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO ying_urls (type,url,description,entrytime,valid_flag) Values (@type,@url,@description,NOW(),'Y')";
            sqlCmd.Connection = myConnection;


            sqlCmd.Parameters.AddWithValue("@type", type.SelectedItem.Text);
            sqlCmd.Parameters.AddWithValue("@url", url.Text);
            sqlCmd.Parameters.AddWithValue("@description", description.Text);


            try
            {
                myConnection.Open();
                int rowInserted = sqlCmd.ExecuteNonQuery();
                //create success
                msge.Text = "Add new url successfully!";
                GridViewBind();
            }
            catch (Exception ee)
            {
                //create failed.
                msge.Text = "Add new url failed and error is: " + ee.Message.ToString();

            }
            finally
            {
                myConnection.Close();
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //refresh datagrid
            GridViewBind();

        }
        protected void GridViewBind()
        {
            string conString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;
            var con = new MySqlConnection(conString);

            MySqlCommand cmd = new MySqlCommand(" select * from  ying_urls order by type,id desc ", con);
            cmd.CommandType = CommandType.Text;
            // Create a DataAdapter to run the command and fill the DataTable
            MySqlDataAdapter da = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            // LabelPageInfo.Text = "Pages: current  " + (GridView1.PageIndex + 1).ToString() + " page / Total  " + GridView1.PageCount.ToString() + "  pages.";
            con.Close();

        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridViewBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            int i;
            for (i = 0; i < GridView1.Rows.Count; i++)
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='Aqua'");

                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
                }
            }
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            GridViewBind();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int t_no = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());



            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from ying_urls where id=" + t_no + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            try
            {
                int rowDeleted = sqlCmd.ExecuteNonQuery();

                msge.Visible = true;
                msge.Text = "Delete successfully!";
                GridViewBind();

            }
            catch(Exception ee)
            {
                msge.Visible = true;
                msge.Text = "Delete fail!"+ee.Message.ToString();


            }
            finally
            {
                myConnection.Close();
            }





        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GridViewBind();
        }


        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int t_no = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0].ToString());

            string url = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("url")).Text;
            string description = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("description")).Text;
            string valid_flag = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("valid_flag")).Text;




            MySqlConnection myConnection = new MySqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["apidb"].ConnectionString;

            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "update ying_urls set url='"+url+ "', description='"+ description+ "',valid_flag='"+ valid_flag+"'  where id=" + t_no + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            try
            {
                int rowDeleted = sqlCmd.ExecuteNonQuery();

                msge.Visible = true;
                msge.Text = "Update successfully!";
                GridViewBind();

            }
            catch (Exception ee)
            {
                msge.Visible = true;
                msge.Text = "Update fail!" + ee.Message.ToString();


            }
            finally
            {
                myConnection.Close();
            }

        }
    }
}