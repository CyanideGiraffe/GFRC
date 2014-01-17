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

public partial class VolunteerQual : System.Web.UI.Page
{
    #region Database
    public OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|gfrc_database.accdb");
    public OleDbCommand cmd = new OleDbCommand();
    public OleDbDataReader rdr;
    #endregion;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Check if the user is logged in
            if ((Convert.ToBoolean(Session["Check"]) == false) || Session["Check"] == null)
                Response.Redirect("/Login.aspx");
        }
        string display = Request.QueryString["form"];
        if (display == "edit")
        {
            viewQual.Visible = false;
            addQual.Visible = false;
            editQual.Visible = true;
        }
        else if (display == "view")
        {
            viewQual.Visible = true;
            addQual.Visible = false;
            editQual.Visible = false;
        }
        else if (display == "add")
        {
            viewQual.Visible = false;
            addQual.Visible = true;
            editQual.Visible = false;
        }
        else
            Response.Redirect("/Search.aspx");

        uint id = Convert.ToUInt32(Request.QueryString["ID"]);
        uint qd = Convert.ToUInt32(Request.QueryString["QD"]);
        Session["Selected"] = id;
        Session["Qual"] = qd;

            if (viewQual.Visible == true)
                output.Controls.Add(displayQuals(id));
            else if (editQual.Visible == true)
                showQuals(qd);
            else if (addQual.Visible != true && editQual.Visible != true && viewQual.Visible != true)
                Response.Redirect("/Search.aspx");
    }

    protected Table displayQuals(uint id)
    {
        // Query to get volunteer information
        string query = string.Format("SELECT gvq_id, gvq_qual FROM gfrc_volunteer_qualification WHERE (gvo_id = {0})", id);

        // Table will hold the following information on volunteers:
        // ID, Name, Email, Mobile, DOB, Status
        // As well as a check box to do mass delete, a view button and an edit button on each
        Table result = new Table();
        result.ID = "qualifications";
        result.CssClass = "list";
        result.CellSpacing = 0;

        // Creater table header
        TableHeaderRow header = new TableHeaderRow();
        header.CssClass = "header";
        TableHeaderCell rid = new TableHeaderCell();
        rid.CssClass = "id";
        rid.Width = 20;
        rid.Text = "ID";
        header.Cells.Add(rid);
        TableHeaderCell qual = new TableHeaderCell();
        qual.CssClass = "qual";
        qual.Text = "QUALIFICATION";
        header.Cells.Add(qual);
        TableHeaderCell edit = new TableHeaderCell();
        edit.CssClass = "button";
        edit.Width = 50;
        header.Cells.Add(edit);
        // Add header to table
        result.Rows.Add(header);

        //Declaration for following code
        TableRow row = new TableRow();
        TableCell cid = new TableCell();
        TableCell rqual = new TableCell();
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
                    cid = new TableCell();
                    rqual = new TableCell();
                    redit = new TableCell();
                    btnEdit = new Button();
                    cid.CssClass = "id";
                    rqual.CssClass = "qual";
                    redit.CssClass = "button";
                    btnEdit.Text = "Edit";

                    cid.Text = rdr.GetValue(0).ToString();
                    rqual.Text = rdr.GetValue(1).ToString();

                    btnEdit.PostBackUrl = string.Format("/VolunteerQual.aspx?form=edit&ID={0}&QD={1}", id, cid.Text);

                    redit.Controls.Add(btnEdit);

                    row = new TableRow();
                    if (rowClass == "even")
                        rowClass = "odd";
                    else
                        rowClass = "even";

                    row.CssClass = rowClass;

                    // Add cells to row
                    row.Cells.Add(cid);
                    row.Cells.Add(rqual);
                    row.Cells.Add(redit);

                    // Add row to table
                    result.Rows.Add(row);
                }
            }
        }
        catch (Exception e)
        {
            string exception = e.Message.ToString();
            TableCell error = new TableCell();
            row = new TableRow();
            error.ColumnSpan = 3;
            error.CssClass = "error";
            error.Text = "An error occurred while loading the qualifications<Br />\"" + exception + "\"";
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
    
    protected void showQuals(uint qd)
    {
        GFRC.vQualification display = new GFRC.vQualification(qd);

        lbeShowQID.Text = display.gvqID.ToString();
        lbeShowID.Text = display.gvoID.ToString();
        txeQual.Text = display.Qualification;
    }
    protected void btnEditQual_Click(object sender, EventArgs e)
    {
        string qual = txeQual.Text;
        uint gvoid = 0, gvqid = 0;
        UInt32.TryParse(lbeShowID.Text, out gvoid);
        UInt32.TryParse(lbeShowQID.Text, out gvqid);

        GFRC.vQualification update = new GFRC.vQualification(gvqid, gvoid, qual);
        update.editQualification(update);
        Response.Redirect("/VolunteerQual.aspx?form=view&ID=" + update.gvoID);
    }
    protected void btnDelQual_Click(object sender, EventArgs e)
    {
        string qual = txeQual.Text;
        uint gvoid = 0, gvqid = 0;
        UInt32.TryParse(lbeShowID.Text, out gvoid);
        UInt32.TryParse(lbeShowQID.Text, out gvqid);

        GFRC.vQualification update = new GFRC.vQualification();
        update.deleteQualification(gvqid);
        Response.Redirect("/VolunteerQual.aspx?form=view&ID=" + gvoid);
    }
    protected void btnAddQual_Click(object sender, EventArgs e)
    {
        string qual = txtQual.Text;
        uint gvoid = Convert.ToUInt32(Session["Selected"]);

        GFRC.vQualification create = new GFRC.vQualification(gvoid, qual);
        create.createQualification(create);
        Response.Redirect("/VolunteerQual.aspx?form=view&ID=" + create.gvoID);
    }
}