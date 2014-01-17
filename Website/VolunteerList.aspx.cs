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

public partial class VolunteerList : System.Web.UI.Page
{
    #region Database
    public OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|gfrc_database.accdb");
    public OleDbCommand cmd = new OleDbCommand();
    public OleDbDataReader rdr;
    #endregion;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            // Check if the user is logged in
            if ((Convert.ToBoolean(Session["Check"]) == false) || Session["Check"] == null)
                Response.Redirect("/Login.aspx");

        // Display volunteer table
        volunteers.Controls.Add(listVolunteers());
    }

    protected Table listVolunteers()
    {
        // Query to get volunteer information
        string query = "SELECT gvo_id, gvo_name, gvo_email, gvo_mobile_ph, gvo_dob, gvo_status FROM gfrc_volunteer";

        // Table will hold the following information on volunteers:
        // ID, Name, Email, Mobile, DOB, Status
        // As well as a check box to do mass delete, a view button and an edit button on each
        Table result = new Table();
        result.ID = "volunteers";
        result.CssClass = "list";
        result.CellSpacing = 0;

        // Creater table header
        TableHeaderRow header = new TableHeaderRow();
        header.CssClass = "header";
        TableHeaderCell id = new TableHeaderCell();
        id.CssClass = "id";
        id.Width = 10;
        id.Text = "ID";
        header.Cells.Add(id);
        TableHeaderCell name = new TableHeaderCell();
        name.CssClass = "name";
        name.Text = "NAME";
        header.Cells.Add(name);
        TableHeaderCell email = new TableHeaderCell();
        email.CssClass = "email";
        email.Text = "EMAIL";
        header.Cells.Add(email);
        TableHeaderCell mobile = new TableHeaderCell();
        mobile.CssClass = "mobile";
        mobile.Width = 70;
        mobile.Text = "MOBILE #";
        header.Cells.Add(mobile);
        TableHeaderCell dob = new TableHeaderCell();
        dob.CssClass = "dob";
        dob.Width = 50;
        dob.Text = "D.O.B";
        header.Cells.Add(dob);
        TableHeaderCell status = new TableHeaderCell();
        status.CssClass = "status";
        status.Width = 50;
        status.Text = "STAT";
        header.Cells.Add(status);
        TableHeaderCell view = new TableHeaderCell();
        view.CssClass = "button";
        view.Width = 50;
        header.Cells.Add(view);
        TableHeaderCell edit = new TableHeaderCell();
        edit.CssClass = "button";
        edit.Width = 50;
        header.Cells.Add(edit);
        // Add header to table
        result.Rows.Add(header);

        //Declaration for following code
        TableRow row = new TableRow();
        TableCell rid = new TableCell();
        TableCell rname = new TableCell();
        TableCell remail = new TableCell();
        TableCell rmobile = new TableCell();
        TableCell rdob = new TableCell();
        TableCell rstatus = new TableCell();
        TableCell rview = new TableCell();
        TableCell redit = new TableCell();
        Button btnView = new Button();
        Button btnEdit = new Button();

        string rowClass = "odd";
        // Add volunteers to table
        try
        {
            using (conn)
            {
                conn.Open();
                cmd = new OleDbCommand(query, conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    DateTime temp = new DateTime();

                    rid = new TableCell();
                    rname = new TableCell();
                    remail = new TableCell();
                    rmobile = new TableCell();
                    rdob = new TableCell();
                    rstatus = new TableCell();
                    rview = new TableCell();
                    redit = new TableCell();
                    btnView = new Button();
                    btnEdit = new Button();
                    rid.CssClass = "id";
                    rname.CssClass = "name";
                    remail.CssClass = "email";
                    rmobile.CssClass = "mobile";
                    rdob.CssClass = "dob";
                    rstatus.CssClass = "status";
                    rview.CssClass = "button";
                    redit.CssClass = "button";
                    btnView.Text = "View";
                    btnEdit.Text = "Edit";

                    rid.Text = rdr.GetValue(0).ToString();
                    rname.Text = rdr.GetValue(1).ToString();
                    remail.Text = rdr.GetValue(2).ToString();
                    rmobile.Text = rdr.GetValue(3).ToString();
                    DateTime.TryParse(rdr.GetValue(4).ToString(), out temp);
                    rdob.Text = temp.ToString("dd/MM/yy");
                    rstatus.Text = rdr.GetValue(5).ToString();

                    btnView.PostBackUrl = string.Format("/VolunteerView.aspx?form=view&ID={0}", rid.Text);
                    btnEdit.PostBackUrl = string.Format("/VolunteerView.aspx?form=edit&ID={0}", rid.Text);

                    rview.Controls.Add(btnView);
                    redit.Controls.Add(btnEdit);

                    row = new TableRow();
                    if (rowClass == "even")
                        rowClass = "odd";
                    else
                        rowClass = "even";

                    row.CssClass = rowClass;

                    // Add cells to row
                    row.Cells.Add(rid);
                    row.Cells.Add(rname);
                    row.Cells.Add(remail);
                    row.Cells.Add(rmobile);
                    row.Cells.Add(rdob);
                    row.Cells.Add(rstatus);
                    row.Cells.Add(rview);
                    row.Cells.Add(redit);

                    

                    // Add row to table
                    result.Rows.Add(row);
                }
            }
        }
        catch (Exception e)
        {
            TableCell error = new TableCell();
            row = new TableRow();
            error.ColumnSpan = 9;
            error.CssClass = "error";
            error.Text = "An error occurred while loading the volunteers";
            row.Cells.Add(error);
            result.Rows.Add(row);
        }
        finally
        {
            if (rdr != null)
                rdr.Close();
        }
        if (conn != null)
            conn.Close();

        return result;
    }
}