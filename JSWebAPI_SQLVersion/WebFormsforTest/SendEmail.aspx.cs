using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace JSWebAPI_SQLVersion
{
    public partial class SendEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MailMessage mm = new MailMessage("jsappserver@gmail.com", "shenjr81@gmail.com", "test from asp.net from jsappserver@gmail.com", "test");
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";

            smtp.EnableSsl = true;

            NetworkCredential NetworkCred = new NetworkCredential("jsappserver@gmail.com", "sjerry81");

            smtp.UseDefaultCredentials = true;

            smtp.Credentials = NetworkCred;

            smtp.Port = 587;
            try
            {

                smtp.Send(mm);

                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
            }
            catch (Exception)
            {
                string error ="Email send fialed: "+ e.ToString();

                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('"+error+"');", true);

            }


        }
    }
}