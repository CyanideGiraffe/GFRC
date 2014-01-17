using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VolunteerAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            // Check if the user is logged in
            if ((Convert.ToBoolean(Session["Check"]) == false) || Session["Check"] == null)
                Response.Redirect("/Login.aspx");
    }
    protected void btnAddVolunteer_Click(object sender, EventArgs e)
    {
        string name = txtName.Text, address = txtAddress.Text, postal = txtPostal.Text, email = txtEmail.Text, home = txtHomePh.Text, mobile = txtMobilePh.Text, status = drpStatus.SelectedItem.Value, referred = txtReferred.Text;
        string referreddoc = fReferredDoc.PostedFile.FileName, policedoc = fPoliceDoc.PostedFile.FileName, application = fApplication.PostedFile.FileName;
        bool police = chkPolice.Checked, induction = chkInduction.Checked;
        DateTime dob = Convert.ToDateTime(txtDOB.Text);

        if (postal == null || postal == "")
            postal = " ";
        if (home == null || home == "")
            home = " ";
        if (referred == null || referred == "")
            referred = " ";
        if (referreddoc == null || referreddoc == "")
            referreddoc = " ";
        if (policedoc == null || policedoc == "")
            policedoc = " ";
        if (application == null || application == "")
            application = " ";

        GFRC.Volunteer create = new GFRC.Volunteer(name, address, postal, email, home, mobile, dob, status, referred, referreddoc, police, policedoc, induction, application);
        create.createVolunteer(create);
            Response.Redirect("/VolunteerView.aspx?form=view&ID=" + create.gvoID);
    }
}