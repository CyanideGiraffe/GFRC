using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VolunteerAvail : System.Web.UI.Page
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
                viewAvail.Visible = false;
                addAvail.Visible = false;
                editAvail.Visible = true;
            }
            else if (display == "view")
            {
                viewAvail.Visible = true;
                addAvail.Visible = false;
                editAvail.Visible = false;
            }
            else if (display == "add")
            {
                viewAvail.Visible = false;
                addAvail.Visible = true;
                editAvail.Visible = false;
            }
            else
                Response.Redirect("/Search.aspx");

            uint id = Convert.ToUInt32(Request.QueryString["ID"]);
            Session["Selected"] = id;

            if (viewAvail.Visible == true)
                output.Text = displayAvails(id);
            else if (editAvail.Visible == true)
                showAvails(id);
            else if (addAvail.Visible != true && editAvail.Visible != true && viewAvail.Visible != true)
                Response.Redirect("/Search.aspx");
        }
    }

    protected string displayAvails(uint id)
    {
        string result = "";
        string reqhours = "";
        string end = "";
        DateTime dt = new DateTime(1901, 1, 1);
        GFRC.Availability display = new GFRC.Availability(id);
        GFRC.Login modifiedby = new GFRC.Login(display.ModifiedBy);

        if (display.gvaID == 0)
            result = "There is no availability record for volunteer ID " + id + ". <a href=\"/VolunteerAvail.aspx?form=add&ID=" + id + "\">Add a record now.</a>";
        else
        {
            if (display.ReqHours == 0)
                reqhours = "Not Applicable";
            else
                reqhours = display.ReqHours.ToString();

            if (display.End != dt)
                end = display.End.ToString("dd MMMM yyyy");

            result = "<table class=\"display\"><tr><td>";
            result += string.Format("Availability ID: </td><td>{0}</td></tr><tr><td>", display.gvaID);
            result += string.Format("Volunteer ID: </td><td>{0}</td></tr><tr><td>", display.gvoID);
            result += string.Format("Start Date: </td><td>{0}</td></tr><tr><td>", display.Start.ToString("dd MMMM yyyy"));
            result += string.Format("End Date: </td><td>{0}</td></tr><tr><td>", end);
            result += string.Format("Required Hours Per Week: </td><td>{0}</td></tr><tr><td colspan=\"2\" style=\"text-align:center;\">", reqhours);
            result += string.Format("<strong>Days Available</strong></td></tr><tr><td>");
            result += string.Format("Monday: </td><td>{0}</td></tr><tr><td>", display.Mon);
            result += string.Format("Tuesday: </td><td>{0}</td></tr><tr><td>", display.Tues);
            result += string.Format("Wednesday: </td><td>{0}</td></tr><tr><td>", display.Wed);
            result += string.Format("Thursday: </td><td>{0}</td></tr><tr><td>", display.Thur);
            result += string.Format("Friday: </td><td>{0}</td></tr><tr><td>", display.Fri);
            result += string.Format("Available To Fill In? </td><td>{0}</td></tr><tr><td>", display.FillIn);
            if (display.DateModified != dt)
                result += string.Format("Last Modified: </td><td>{0}</td></tr><tr><td>", display.DateModified.ToString("dd/MM/yyyy hh:mm tt"));
            else
                result += string.Format("Last Modified: </td><td></td></tr><tr><td>");
            result += string.Format("Modified By: </td><td>{0}</td></tr></table>", modifiedby.Username);

        }

        return result;
    }
    protected void showAvails(uint id)
    {
        DateTime dt = new DateTime(1901, 1, 1);
        GFRC.Availability display = new GFRC.Availability(id);

        lbeShowAID.Text = display.gvaID.ToString();
        lbeShowID.Text = display.gvoID.ToString();
        txeStart.Text = display.Start.ToString("dd/MM/yyyy");
        if (display.End == dt)
            txeEnd.Text = "";
        else
            txeEnd.Text = display.End.ToString("dd/MM/yyyy");
        if (display.ReqHours == 0)
            txeReqHours.Text = "";
        else
            txeReqHours.Text = display.ReqHours.ToString();
        cheMon.Checked = display.Mon;
        cheTues.Checked = display.Tues;
        cheWed.Checked = display.Wed;
        cheThur.Checked = display.Thur;
        cheFri.Checked = display.Fri;
        cheFillIn.Checked = display.FillIn;
    }
    protected void btnEditAvail_Click(object sender, EventArgs e)
    {
        bool mon = cheMon.Checked, tues = cheTues.Checked, wed = cheWed.Checked, thur = cheThur.Checked, fri = cheFri.Checked, fillin = cheFillIn.Checked;
        uint gvoid = 0, gvaid = 0, reqhours = 0, modifiedby = 0;
        UInt32.TryParse(txeReqHours.Text.ToString(), out reqhours);
        UInt32.TryParse(lbeShowID.Text, out gvoid);
        UInt32.TryParse(lbeShowAID.Text, out gvaid);
        UInt32.TryParse(Session["UserID"].ToString(), out modifiedby);
        DateTime start = new DateTime(1901, 1, 1), datemodified = DateTime.Now, end = new DateTime(1901, 1, 1);
        DateTime.TryParse(txeStart.Text, out start);
        if (!(txeEnd.Text == null || txeEnd.Text == ""))
            DateTime.TryParse(txeEnd.Text, out end);

        GFRC.Availability update = new GFRC.Availability(gvaid, gvoid, start, end, reqhours, mon, tues, wed, thur, fri, fillin, datemodified, modifiedby);
        update.editAvailability(update);
        Response.Redirect("/VolunteerAvail.aspx?form=view&ID=" + update.gvoID);
    }
    protected void btnAddAvail_Click(object sender, EventArgs e)
    {
        bool mon = chkMon.Checked, tues = chkTues.Checked, wed = chkWed.Checked, thur = chkThur.Checked, fri = chkFri.Checked, fillin = chkFillIn.Checked;
        uint gvoid = 0, reqhours = 0;
        UInt32.TryParse(Session["Selected"].ToString(), out gvoid);
        UInt32.TryParse(txtReqHours.Text, out reqhours);
        DateTime start = new DateTime(1901, 1, 1), end = new DateTime(1901, 1, 1);
        DateTime.TryParse(txtStart.Text, out start);
        if (!(txtEnd.Text == null || txtEnd.Text == ""))
            DateTime.TryParse(txtEnd.Text, out end);

        GFRC.Availability create = new GFRC.Availability(gvoid, start, end, reqhours, mon, tues, wed, thur, fri, fillin);
        create.createAvailability(create);
        Response.Redirect("/VolunteerAvail.aspx?form=view&ID=" + create.gvoID);
    }
}