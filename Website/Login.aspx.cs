using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Web.UI;
using System.Web.UI.WebControls;
using GFRC;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the user is already logged in
        if (Convert.ToBoolean(Session["Check"]) == true)
            Response.Redirect("/Default.aspx");
    }

    protected void Login_Authenticate(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string password = txtPassword.Text;
        GFRC.Login login = new GFRC.Login(username, password);

        if (login.gloID > 0)
        {
            Session.Timeout = 8;
            Session["Check"] = true;
            Session["Status"] = login.Status;
            Session["UserID"] = login.gloID;
            Response.Redirect("/Default.aspx");
        }
        else if (login.Active != true)
        {
            Session["Check"] = false;
            loginError.Text = "The login details you entered have been deactivate, please try again.";
        }
        else
        {
            Session["Check"] = false;
            loginError.Text = "The username and password you entered do not match, please try again.";
        }
    }

}