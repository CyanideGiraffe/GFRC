using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data.SqlClient;
using System.Data.OleDb;
using GFRC;
using Vector;

public partial class VolunteerView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the user is logged in
        if (!IsPostBack && !IsCallback)
        {
            if ((Convert.ToBoolean(Session["Check"]) == false) || Session["Check"] == null)
                Response.Redirect("/Login.aspx");

            string display = Request.QueryString["form"];
            if (display == "edit")
            {
                viewVol.Visible = false;
                editVol.Visible = true;
            }
            else if (display == "view")
            {
                viewVol.Visible = true;
                editVol.Visible = false;
            }
            else
                Response.Redirect("/Search.aspx");

            uint id = Convert.ToUInt32(Request.QueryString["ID"]);
            Session["Selected"] = id;

            if (viewVol.Visible == true)
                output.Text = displayVolunteer(id);
            else if (editVol.Visible == true)
                showVolunteer(id);
            else
                Response.Redirect("/Search.aspx");
        }
    }

    protected string displayVolunteer(uint id)
    {
        string result = "";

        DateTime dt = new DateTime(1901, 1, 1);
        GFRC.Volunteer display = new GFRC.Volunteer(id);
        GFRC.Login modifiedby = new GFRC.Login(display.ModifiedBy);

        result = "<table class=\"display\"><tr><td>";
        result += string.Format("Volunteer ID: </td><td>{0}</td></tr><tr><td>", display.gvoID);
        result += string.Format("Name: </td><td>{0}</td></tr><tr><td>", display.Name);
        result += string.Format("Address: </td><td>{0}</td></tr><tr><td>", display.Address);
        result += string.Format("Postal Address: </td><td>{0}</td></tr><tr><td>", display.PostalAddress);
        result += string.Format("Email: </td><td>{0}</td></tr><tr><td>", display.Email);
        result += string.Format("Home #: </td><td>{0}</td></tr><tr><td>", display.HomePh);
        result += string.Format("Mobile #: </td><td>{0}</td></tr><tr><td>", display.MobilePh);
        result += string.Format("D.O.B: </td><td>{0}</td></tr><tr><td>", display.DOB.ToString("dd MMMM yyyy"));
        result += string.Format("Status: </td><td>{0}</td></tr><tr><td>", display.Status);
        result += string.Format("Refferred by: </td><td>{0}</td></tr><tr><td>", display.Referred);
        result += string.Format("Documentation: </td><td>{0}</td></tr><tr><td>", display.ReferredDoc);
        result += string.Format("Police Check? </td><td>{0}</td></tr><tr><td>", display.Police.ToString());
        result += string.Format("Documentation: </td><td>{0}</td></tr><tr><td>", display.PoliceDoc);
        result += string.Format("Had Induction? </td><td>{0}</td></tr><tr><td>", display.Induction.ToString());
        result += string.Format("Application: </td><td>{0}</td></tr><tr><td>", display.Application);
        if (display.DateModified != dt)
            result += string.Format("Last Modified: </td><td>{0}</td></tr><tr><td>", display.DateModified.ToString("dd/MM/yyyy hh:mm tt"));
        else
            result += string.Format("Last Modified: </td><td></td></tr><tr><td>");
        result += string.Format("Modified By: </td><td>{0}</td></tr></table>", modifiedby.Username);

        return result;
    }

    protected void showVolunteer(uint id)
    {
        GFRC.Volunteer display = new GFRC.Volunteer(id);

        lblShowID.Text = display.gvoID.ToString();
        txtName.Text = display.Name;
        txtAddress.Text = display.Address;
        txtPostal.Text =  display.PostalAddress;
        txtEmail.Text =  display.Email;
        txtHomePh.Text = display.HomePh;
        txtMobilePh.Text = display.MobilePh;
        txtDOB.Text = display.DOB.ToString("dd/MM/yyyy");
        drpStatus.Items.FindByValue(display.Status).Selected = true;
        txtReferred.Text = display.Referred;
        txtRefDoc.Text = display.ReferredDoc;
        chkPolice.Checked = display.Police;
        txtPolDoc.Text = display.PoliceDoc;
        chkInduction.Checked = display.Induction;
        txtApp.Text = display.Application;
    }
    protected void btnEditVolunteer_Click(object sender, EventArgs e)
    {
        string name = txtName.Text, address = txtAddress.Text, postal = txtPostal.Text, email = txtEmail.Text, home = txtHomePh.Text, mobile = txtMobilePh.Text, status = drpStatus.SelectedItem.Value, referred = txtReferred.Text;
        string referreddoc = fReferredDoc.PostedFile.FileName, policedoc = fPoliceDoc.PostedFile.FileName, application = fApplication.PostedFile.FileName;
        bool police = chkPolice.Checked, induction = chkInduction.Checked;
        DateTime dob = Convert.ToDateTime(txtDOB.Text), datemodified = DateTime.Now;
        uint gvoid = Convert.ToUInt32(lblShowID.Text), modifiedby = Convert.ToUInt32(Session["UserID"]);

        if (postal == null || postal == "")
            postal = " ";
        if (home == null || home == "")
            home = " ";
        if (referred == null || referred == "")
            referred = " ";
        if (referreddoc == null || referreddoc == "")
            referreddoc = txtRefDoc.Text;
        if (policedoc == null || policedoc == "")
            policedoc = txtRefDoc.Text;
        if (application == null || application == "")
            application = txtApp.Text;

        GFRC.Volunteer update = new GFRC.Volunteer(gvoid, name, address, postal, email, home, mobile, dob, status, referred, referreddoc, police, policedoc, induction, application, datemodified, modifiedby);
        update.editVolunteer(update);
        Response.Redirect("/VolunteerView.aspx?form=view&ID=" + update.gvoID);
    }
}