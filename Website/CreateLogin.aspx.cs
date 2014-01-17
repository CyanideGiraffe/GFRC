using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the user is already logged in
        if (Convert.ToBoolean(Session["Check"]) == true)
            Response.Redirect("/AdminCreate.aspx");
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
            this.createlogin_form.Visible = false;
            this.success_form.Visible = true;
        }
        else
        {
            createError.Text = "Could not create a new login, please try again. This may have occurred because the username already exists.";
        }
    }
}