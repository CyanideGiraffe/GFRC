﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VolunteerSearch.aspx.cs" Inherits="VolunteerSearch" Theme="Main"%>

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
                <a href="VolunteerAdd.aspx">Add New</a>
            </div>

            <div id="right">
                <form id="search" runat="server">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblID" Text="Search ID: " runat="server" /></td>
                            <td><asp:TextBox ID="txtID" runat="server" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="error">
                                <asp:RegularExpressionValidator ID="rgxID" ControlToValidate="txtID" Text="The ID number must consist of numbers only." ValidationExpression="^\d$" Display="Dynamic" runat="server" />
                                <asp:Label ID="searchError" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="button"><asp:Button ID="btnSearchVolunteer" Text="Search" OnClick="btnSearchVolunteer_Click" runat="server" /></td>
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