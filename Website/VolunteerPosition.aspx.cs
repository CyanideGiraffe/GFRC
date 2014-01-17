using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VolunteerPosition : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Check if the user is logged in
            if ((Convert.ToBoolean(Session["Check"]) == false) || Session["Check"] == null)
                Response.Redirect("/Login.aspx");

            string display = Request.QueryString["form"];
            if (display == "edit")
            {
                viewPos.Visible = false;
                addPos.Visible = false;
                editPos.Visible = true;
            }
            else if (display == "view")
            {
                viewPos.Visible = true;
                addPos.Visible = false;
                editPos.Visible = false;
            }
            else if (display == "add")
            {
                viewPos.Visible = false;
                addPos.Visible = true;
                editPos.Visible = false;
            }
            else
                Response.Redirect("/Search.aspx");

            uint id = Convert.ToUInt32(Request.QueryString["ID"]);
            Session["Selected"] = id;

            if (viewPos.Visible == true)
                output.Text = displayPosition(id);
            else if (editPos.Visible == true)
                showPosition(id);
            else if (addPos.Visible != true && editPos.Visible != true && viewPos.Visible != true)
                Response.Redirect("/Search.aspx");
        }
    }

    protected string displayPosition(uint id)
    {
        string result = "";

        GFRC.Position display = new GFRC.Position(id, 'o');

        if (display.PositionA == null || display.PositionA == "")
            result = "There is no position record for volunteer ID " + id + ". <a href=\"/VolunteerPosition.aspx?form=add&ID=" + id + "\">Add a record now.</a>";
        else
        {
            result = "<table class=\"display\"><tr><td>";
            result += string.Format("Position ID: </td><td>{0}</td></tr><tr><td>", display.gvpID);
            result += string.Format("Volunteer ID: </td><td>{0}</td></tr><tr><td>", display.gvoID);
            result += string.Format("Position: </td><td>{0}</td></tr><tr><td>", display.PositionA);
            result += string.Format("Driver Category: </td><td>{0}</td></tr><tr><td>", display.DriverCat);
            result += string.Format("Supporting Document: </td><td>{0}</td></tr><tr><td>", display.DriverCatDoc);
            result += string.Format("Driver Transmission: </td><td>{0}</td></tr></table>", display.DriverTrans);
        }

        return result;
    }
    protected void showPosition(uint id)
    {
        GFRC.Position display = new GFRC.Position(id, 'o');

        lbeShowPID.Text = display.gvpID.ToString();
        lbeShowID.Text = display.gvoID.ToString();
        txePosition.Text = display.PositionA;
        dreDriverCat.Items.FindByValue(display.DriverCat).Selected = true;
        txtDCDoc.Text = display.DriverCatDoc;
        dreDriverTrans.Items.FindByValue(display.DriverTrans).Selected = true;
    }
    protected void btnEditPosition_Click(object sender, EventArgs e)
    {
        string position = txePosition.Text, drivercat = dreDriverCat.SelectedItem.Value, drivertrans = dreDriverTrans.SelectedItem.Value;
        string drivercatdoc = feDriverCatDoc.PostedFile.FileName;
        uint gvoid = Convert.ToUInt32(lbeShowID.Text), gvpid = Convert.ToUInt32(lbeShowPID.Text);

        if (drivercat == null || drivercat == "")
            drivercat = " ";
        if (drivertrans == null || drivertrans == "")
            drivertrans = " ";
        if (drivercatdoc == null || drivercatdoc == "")
            drivercatdoc = txtDCDoc.Text;

        GFRC.Position update = new GFRC.Position(gvpid, gvoid, position, drivercat, drivercatdoc, drivertrans);
        update.editPosition(update);
        Response.Redirect("/VolunteerPosition.aspx?form=view&ID=" + update.gvoID);
    }
    protected void btnAddPosition_Click(object sender, EventArgs e)
    {
        string position = txtPosition.Text, drivercat = drpDriverCat.SelectedItem.Value, drivertrans = drpDriverTrans.SelectedItem.Value;
        string drivercatdoc = null;
        if (fDriverCatDoc.HasFile)
            drivercatdoc = fDriverCatDoc.PostedFile.FileName;
        uint gvoid = Convert.ToUInt32(Session["Selected"]);

        if (drivercat == null || drivercat == "")
            drivercat = " ";
        if (drivertrans == null || drivertrans == "")
            drivertrans = " ";
        if (drivercatdoc == null || drivercatdoc == "")
            drivercatdoc = " ";

        GFRC.Position create = new GFRC.Position(gvoid, position, drivercat, drivercatdoc, drivertrans);
        create.createPosition(create);
        Response.Redirect("/VolunteerPosition.aspx?form=view&ID=" + create.gvoID);
    }
}