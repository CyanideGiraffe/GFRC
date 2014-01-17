using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminCreate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            // Check if the user is logged in
            if ((Convert.ToBoolean(Session["Check"]) == false) || Session["Check"] == null)
                Response.Redirect("/Login.aspx");
    }
    protected void btnCreateLogin_Click(object sender, EventArgs e)
    {
        uint gvoid;
        if (!UInt32.TryParse(txtGvoID.Text, out gvoid))
            gvoid = 0;

        bool result = false;
        GFRC.Login create = new GFRC.Login(txtUsername.Text, txtPassword.Text, txtNote.Text, drpStatus.SelectedItem.Text, gvoid);
        result = create.createLogin(create.Username, create.Password, create.Note, create.Active, create.Status, create.gvoID);

        if (result == true)
        {
            Response.Redirect("/AdminView.aspx?ID=" + create.gloID);
        }
        else
        {
            createError.Text = "Could not create a new login, please try again. This may have occurred because the username already exists.";
        }
    }
}