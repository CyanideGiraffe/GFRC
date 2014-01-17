using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VolunteerSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            // Check if the user is logged in
            if ((Convert.ToBoolean(Session["Check"]) == false) || Session["Check"] == null)
                Response.Redirect("/Login.aspx");
    }
    protected void btnSearchVolunteer_Click(object sender, EventArgs e)
    {
        uint id = Convert.ToUInt32(txtID.Text);
        GFRC.Volunteer search = new GFRC.Volunteer(id);
        if (search.Name == "")
            searchError.Text = "The user you searched for doesn't seem to exist. Please try again.";
        else
            Response.Redirect("/VolunteerView.aspx?form=view&ID=" + id);
    }
}