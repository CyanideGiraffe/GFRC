using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Check if the user is logged in
            if ((Convert.ToBoolean(Session["Check"]) == false) || Session["Check"] == null)
                Response.Redirect("/Login.aspx");

            uint id = Convert.ToUInt32(Request.QueryString["ID"]);
            Session["Selected"] = id;
            output.Text = displayAdmin(id);
        }
    }

    protected string displayAdmin(uint id)
    {
        string result = "";

        DateTime dt = new DateTime(1901, 1, 1);
        GFRC.Login display = new GFRC.Login(id);
        GFRC.Volunteer link = new GFRC.Volunteer(display.gvoID);
        GFRC.Login modifiedby = new GFRC.Login(display.ModifiedBy);

        result = "<table class=\"display\"><tr><td>";
        result += string.Format("Login ID: </td><td>{0}</td></tr><tr><td>", display.gloID);
        result += string.Format("Username: </td><td>{0}</td></tr><tr><td>", display.Username);
        result += string.Format("Note: </td><td>{0}</td></tr><tr><td>", display.Note);
        result += string.Format("Active? </td><td>{0}</td></tr><tr><td>", display.Active.ToString());
        result += string.Format("Status: </td><td>{0}</td></tr><tr><td>", display.Status);
        result += string.Format("Link To: </td><td>{0}</td></tr><tr><td>", link.ToString());
        if (display.DateModified != dt)
            result += string.Format("Last Modified: </td><td>{0}</td></tr><tr><td>", display.DateModified.ToString("dd/MM/yyyy hh:mm tt"));
        else
            result += string.Format("Last Modified: </td><td></td></tr><tr><td>");
        result += string.Format("Modified By: </td><td>{0}</td></tr></table>", modifiedby.Username);

        return result;
    }
}