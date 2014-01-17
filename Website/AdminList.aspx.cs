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

public partial class AdminList : System.Web.UI.Page
{
    #region Database
    public OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|gfrc_database.accdb");
    public OleDbCommand cmd = new OleDbCommand();
    public OleDbDataReader rdr;
    #endregion;

    protected void Page_Load(object sender, EventArgs e)
    {
            // Check if the user is logged in
            if ((Convert.ToBoolean(Session["Check"]) == false) || Session["Check"] == null)
                Response.Redirect("/Login.aspx");

        // Display volunteer table
        logins.Controls.Add(listLogins());
    }

    protected Table listLogins()
    {
        // Query to get volunteer information
        string query = "SELECT glo_id, glo_username, glo_status, glo_active FROM gfrc_login";

        // Table will hold the following information on volunteers:
        // ID, Name, Email, Mobile, DOB, Status
        // As well as a check box to do mass delete, a view button and an edit button on each
        Table result = new Table();
        result.ID = "logins";
        result.CssClass = "list";
        result.CellSpacing = 0;

        // Creater table header
        TableHeaderRow header = new TableHeaderRow();
        header.CssClass = "header";
        TableHeaderCell id = new TableHeaderCell();
        id.CssClass = "id";
        id.Width = 15;
        id.Text = "ID";
        header.Cells.Add(id);
        TableHeaderCell username = new TableHeaderCell();
        username.CssClass = "username";
        username.Text = "USERNAME";
        header.Cells.Add(username);
        TableHeaderCell status = new TableHeaderCell();
        status.CssClass = "status";
        status.Width = 80;
        status.Text = "STATUS";
        header.Cells.Add(status);
        TableHeaderCell active = new TableHeaderCell();
        active.CssClass = "active";
        active.Width = 40;
        active.Text = "ACTIVE";
        header.Cells.Add(active);
        TableHeaderCell view = new TableHeaderCell();
        view.CssClass = "button";
        view.Width = 50;
        header.Cells.Add(view);
        // Add header to table
        result.Rows.Add(header);

        //Declaration for following code
        TableRow row = new TableRow();
        TableCell rid = new TableCell();
        TableCell rname = new TableCell();
        TableCell rstatus = new TableCell();
        TableCell ractive = new TableCell();
        TableCell rview = new TableCell();
        Button btnView = new Button();

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
                    rstatus = new TableCell();
                    ractive = new TableCell();
                    rview = new TableCell();
                    btnView = new Button();;
                    rid.CssClass = "id";
                    rname.CssClass = "username";
                    rstatus.CssClass = "status";
                    ractive.CssClass = "active";
                    rview.CssClass = "button";
                    btnView.Text = "View";

                    rid.Text = rdr.GetValue(0).ToString();
                    rname.Text = rdr.GetValue(1).ToString();
                    rstatus.Text = rdr.GetValue(2).ToString();
                    ractive.Text = rdr.GetValue(3).ToString();

                    btnView.PostBackUrl = string.Format("/AdminView.aspx?ID={0}", rid.Text);

                    rview.Controls.Add(btnView);

                    row = new TableRow();
                    if (rowClass == "even")
                        rowClass = "odd";
                    else
                        rowClass = "even";

                    row.CssClass = rowClass;

                    // Add cells to row
                    row.Cells.Add(rid);
                    row.Cells.Add(rname);
                    row.Cells.Add(rstatus);
                    row.Cells.Add(ractive);
                    row.Cells.Add(rview);

                    

                    // Add row to table
                    result.Rows.Add(row);
                }
            }
        }
        catch (Exception e)
        {
            TableCell error = new TableCell();
            row = new TableRow();
            error.ColumnSpan = 4;
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