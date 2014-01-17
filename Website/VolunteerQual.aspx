<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VolunteerQual.aspx.cs" Inherits="VolunteerQual" Theme="Main"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GFRC ~ Volunteer</title>
</head>
<body>
    <div id="container">
        <div id="header">
            <div id="title">
                <h1>Geelong Food Relief Centre</h1>
                <h2>Volunteer</h2>
            </div>
            <div id="home">
                <a href="Default.aspx">Home</a>
                <a href="Logout.aspx">Logout</a>
            </div>
        </div>
        <div id="navigation">
            <!-- WAS ONLY ABLE TO COMPLETE VOLUNTEER AND ADMIN SECTIONS
            <a href="Volunteer.aspx" class="selected">Volunteer</a>
            <a href="Board.aspx">Board Member</a>
            <a href="Donor.aspx">Donor</a>
            <a href="Agency.aspx">Agency</a>
            <a href="Appeal.aspx">Appeal</a>
            <% //if (Session["Status"].ToString() == "Administrator")
               //    Response.Write("<a href=\"Report.aspx\">Reports</a> <a href=\"Admin.aspx\" class=\"end\">Admin</a>");
               //else
               //    Response.Write("<a href=\"Report.aspx\" class=\"end\">Reports</a>");
            %>
            -->
            <%
                if (Session["Status"].ToString() == "Administrator")
                    Response.Write("<a href=\"Volunteer.aspx\" class=\"selected\">Volunteer</a> <a href=\"Admin.aspx\" class=\"end\">Admin</a>");
                else
                    Response.Write("<a href=\"Volunteer.aspx\" class=\"selected end\">Volunteer</a>");
            %>
        </div>
        <div id="content">
            <div id="left">
                <h3>Volunteer Options</h3>
                <a href="VolunteerList.aspx">List All</a><br />
                <a href="VolunteerSearch.aspx" class="selected">Search</a><br />
                <% if (viewQual.Visible == true || editQual.Visible == true)
                   {
                       Response.Write("<a href=\"VolunteerView.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"sub selected\">View</a><br />");
                           Response.Write("<a href=\"VolunteerPosition.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Position</a><br />");
                           Response.Write("<a href=\"VolunteerAvail.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Availabilities</a><br />");
                           Response.Write("<a href=\"VolunteerQual.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub selected\">Qualifications</a><br />");
                               Response.Write("<a href=\"VolunteerQual.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsubsub selected\">View</a><br />");
                               Response.Write("<a href=\"VolunteerQual.aspx?form=add&ID=" + Session["Selected"].ToString() + "\" class=\"subsubsub\">Add</a><br />");
                       Response.Write("<a href=\"VolunteerView.aspx?form=edit&ID=" + Session["Selected"].ToString() + "\" class=\"sub\">Edit</a><br />");
                   }
                   else if (addQual.Visible == true)
                   {
                       Response.Write("<a href=\"VolunteerView.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"sub selected\">View</a><br />");
                           Response.Write("<a href=\"VolunteerPosition.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Position</a><br />");
                           Response.Write("<a href=\"VolunteerAvail.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Availabilities</a><br />");
                           Response.Write("<a href=\"VolunteerQual.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub selected\">Qualifications</a><br />");
                               Response.Write("<a href=\"VolunteerQual.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsubsub\">View</a><br />");
                               Response.Write("<a href=\"VolunteerQual.aspx?form=add&ID=" + Session["Selected"].ToString() + "\" class=\"subsubsub selected\">Add</a><br />");
                       Response.Write("<a href=\"VolunteerView.aspx?form=edit&ID=" + Session["Selected"].ToString() + "\" class=\"sub\">Edit</a><br />");
                   }
               %>
                <a href="VolunteerAdd.aspx">Add New</a>
            </div>

            <div id="right">
                <form id="viewQual" visible="true" runat="server">
                    <asp:Placeholder ID="output" runat="server"></asp:Placeholder>
                </form>
                <form id="addQual" visible="false" runat="server">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblQual" AssociatedControlID="txtQual" Text="*Qualification: " runat="server" /></td>
                            <td><asp:TextBox ID="txtQual" MaxLength="255" runat="server" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="error">
                                <asp:RequiredFieldValidator ID="reqQual" ControlToValidate="txtQual" Text="You must enter a qualification." Display="Dynamic" runat="server" />
                                <asp:Label ID="qualError" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="button"><asp:Button ID="btnAddQual" Text="Add" OnClick="btnAddQual_Click" runat="server" /></td>
                        </tr>
                    </table>
                </form>
                <form id="editQual" visible="false" runat="server">
                    <table>
                        <tr>
                            <td><asp:Label ID="lbeQID" Text="Qualification ID: " runat="server" /></td>
                            <td><asp:Label ID="lbeShowQID" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeID" Text="Volunteer ID: " runat="server" /></td>
                            <td><asp:Label ID="lbeShowID" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeQual" AssociatedControlID="txeQual" Text="*Qualification: " runat="server" /></td>
                            <td><asp:TextBox ID="txeQual" MaxLength="255" runat="server" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="error">
                                <asp:RequiredFieldValidator ID="reeQual" ControlToValidate="txeQual" Text="You must enter a qualification." Display="Dynamic" runat="server" />
                                <asp:Label ID="equalError" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="button"><asp:Button ID="btnEditQual" Text="Edit" OnClick="btnEditQual_Click" runat="server" /><asp:Button ID="btnDelQual" Text="Delete" OnClick="btnDelQual_Click" runat="server" /></td>
                        </tr>
                    </table>
                </form>
            </div>

        </div>
        <div id="footer">
            <p>
                <a href="Default.aspx">home</a>
                <a href="Volunteer.aspx">volunteer</a>
                <!-- <a href="Board.aspx">board member</a>
                <a href="Donor.aspx">donor</a>
                <a href="Agency.aspx">agency</a>
                <a href="Appeal.aspx">appeal</a>
                <a href="Report.aspx">reports</a> -->
                <% if (Session["Status"].ToString() == "Administrator")
                       Response.Write("<a href=\"Admin.aspx\">admin</a>"); %>
                <a href="Logout.aspx">logout</a>
            </p>
            <p>
                Please note: this system is not entirely completed and some areas may not yet be accessable.
            </p>
        </div>
    </div>
</body>
</html>
