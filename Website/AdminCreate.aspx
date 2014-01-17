<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminCreate.aspx.cs" Inherits="AdminCreate" Theme="Main"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GFRC ~ Admin</title>
</head>
<body>
    <div id="container">
        <div id="header">
            <div id="title">
                <h1>Geelong Food Relief Centre</h1>
                <h2>Admin</h2>
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
                    Response.Write("<a href=\"Volunteer.aspx\">Volunteer</a> <a href=\"Admin.aspx\" class=\"selected end\">Admin</a>");
                else
                    Response.Write("<a href=\"Volunteer.aspx\" class=\"end\">Volunteer</a>");
            %>
        </div>
        <div id="content">
            <div id="left">
                <h3>Admin Options</h3>
                <a href="AdminList.aspx">List All</a><br />
                <a href="AdminCreate.aspx" class="selected">Add New</a>
            </div>
            <div id="right">
                <form id="createlogin_form" visible="true" runat="server">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblUsername" Text="Username: " runat="server" /></td>
                            <td><asp:TextBox ID="txtUsername" MinLength="2" MaxLength="10" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblPassword" Text="Password: " runat="server" /></td>
                            <td><asp:TextBox ID="txtPassword" MinLength="6" MaxLength="12" TextMode="Password" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblRePassword" Text="Reenter Password: " runat="server" /></td>
                            <td><asp:TextBox ID="txtRePassword" MinLength="6" MaxLength="12" TextMode="Password" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblNote" Text="Note: " runat="server" /></td>
                            <td><asp:TextBox ID="txtNote" MinLength="1" MaxLength="255" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblStatus" Text="Status: " runat="server" /></td>
                            <td><asp:DropDownList ID="drpStatus" runat="server">
                                <asp:ListItem>Administrator</asp:ListItem>
                                <asp:ListItem>Volunteer</asp:ListItem>
                                <asp:ListItem>Temporary</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblGvoID" Text="Volunteer ID: " runat="server" /></td>
                            <td><asp:TextBox ID="txtGvoID" runat="server" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="error">
                                <asp:RequiredFieldValidator ID="reqUsername" ControlToValidate="txtUsername" Text="You must enter a username." Display="Dynamic" runat="server" />
                                <asp:RequiredFieldValidator ID="reqPassword" ControlToValidate="txtPassword" Text="You must enter a password." Display="Dynamic" runat="server" />
                                <asp:CompareValidator ID="comPasswords" ControlToValidate="txtRePassword" ControlToCompare="txtPassword" Text="The passwords you entered do not match." Display="Dynamic" runat="server" />
                                <asp:RequiredFieldValidator ID="reqNote" ControlToValidate="txtNote" Text="You must enter a note. e.g. Name of person using account if no volunteer ID is available" Display="Dynamic" runat="server" />
                                <asp:RegularExpressionValidator ID="rngGvoID" ControlToValidate="txtGvoID" Text="You must enter a value greater than 1" ValidationExpression="(^[1-9][0-9]*$|^$)" Display="Dynamic" runat="server" />
                                <asp:Label ID="createError" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="button">
                                <asp:Button ID="btnCreateLogin" Text="Create" OnClick="btnCreateLogin_Click" runat="server" />
                            </td>
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