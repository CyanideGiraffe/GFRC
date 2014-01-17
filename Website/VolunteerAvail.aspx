<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VolunteerAvail.aspx.cs" Inherits="VolunteerAvail" Theme="Main"%>

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
                <% if (editAvail.Visible == true)
                   {
                       Response.Write("<a href=\"VolunteerView.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"sub selected\">View</a><br />");
                           Response.Write("<a href=\"VolunteerPosition.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Position</a><br />");
                           Response.Write("<a href=\"VolunteerAvail.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub selected\">Availabilities</a><br />");
                               Response.Write("<a href=\"VolunteerAvail.aspx?form=edit&ID=" + Session["Selected"].ToString() + "\" class=\"subsubsub selected\">Edit</a><br />");
                           Response.Write("<a href=\"VolunteerQual.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Qualifications</a><br />");
                       Response.Write("<a href=\"VolunteerView.aspx?form=edit&ID=" + Session["Selected"].ToString() + "\" class=\"sub\">Edit</a><br />");
                   }
                   else if (viewAvail.Visible == true || addAvail.Visible == true)
                   {
                       Response.Write("<a href=\"VolunteerView.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"sub selected\">View</a><br />");
                           Response.Write("<a href=\"VolunteerPosition.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Position</a><br />");
                           Response.Write("<a href=\"VolunteerAvail.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub selected\">Availabilities</a><br />");
                               Response.Write("<a href=\"VolunteerAvail.aspx?form=edit&ID=" + Session["Selected"].ToString() + "\" class=\"subsubsub\">Edit</a><br />");
                           Response.Write("<a href=\"VolunteerQual.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Qualifications</a><br />");
                       Response.Write("<a href=\"VolunteerView.aspx?form=edit&ID=" + Session["Selected"].ToString() + "\" class=\"sub\">Edit</a><br />");
                   }%>
                <a href="VolunteerAdd.aspx">Add New</a>
            </div>

            <div id="right">
                <form id="viewAvail" visible="true" runat="server">
                    <asp:Label ID="output" runat="server" />
                </form>
                <form id="addAvail" visible="false" runat="server">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblStart" AssociatedControlID="txtStart" Text="*Start Date: " runat="server" /></td>
                            <td><asp:TextBox ID="txtStart" MaxLength="10" runat="server" /> dd/mm/yyyy</td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblEnd" AssociatedControlID="txtEnd" Text="End Date: " runat="server" /></td>
                            <td><asp:TextBox ID="txtEnd" MaxLength="10" runat="server" /> dd/mm/yyyy</td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblReqHours" AssociatedControlID="txtReqHours" Text="Required Hours Per Week: " runat="server" /></td>
                            <td><asp:TextBox ID="txtReqHours" runat="server" /></td>
                        </tr>
                        <tr><td colspan="2" style="text-align:center;"><strong>Days Available</strong></td></tr>
                        <tr>
                            <td><asp:Label ID="lblMon" AssociatedControlID="chkMon" Text="Monday: " runat="server" /></td>
                            <td><asp:CheckBox ID="chkMon" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblTues" AssociatedControlID="chkTues" Text="Tuesday: " runat="server" /></td>
                            <td><asp:CheckBox ID="chkTues" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblWed" AssociatedControlID="chkWed" Text="Wednesday: " runat="server" /></td>
                            <td><asp:CheckBox ID="chkWed" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblThur" AssociatedControlID="chkThur" Text="Thursday: " runat="server" /></td>
                            <td><asp:CheckBox ID="chkThur" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblFri" AssociatedControlID="chkFri" Text="Friday: " runat="server" /></td>
                            <td><asp:CheckBox ID="chkFri" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblFillIn" AssociatedControlID="chkFillIn" Text="Available To Fill In? " runat="server" /></td>
                            <td><asp:CheckBox ID="chkFillIn" runat="server" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="error">
                                <asp:RegularExpressionValidator ID="rgxStart" ControlToValidate="txtStart" Text="Please enter a valid date" ValidationExpression="^[0-3]?\d/[0-1]?\d/\d{4}$" Display="Dynamic" runat="server" />
                                <asp:RegularExpressionValidator ID="rgxEnd" ControlToValidate="txtEnd" Text="Please enter a valid date" ValidationExpression="(^[0-3]?\d/[0-1]?\d/\d{4}$|^$)" Display="Dynamic" runat="server" />
                                <asp:RegularExpressionValidator ID="rgxReqHours" ControlToValidate="txtReqHours" Text="Required hours must consist of numbers only." ValidationExpression="^\d$" Display="Dynamic" runat="server" />
                                <asp:RequiredFieldValidator ID="reqStart" ControlToValidate="txtStart" Text="You must enter a start date." Display="Dynamic" runat="server" />
                                <asp:Label ID="availError" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="button"><asp:Button ID="btnAddAvail" Text="Add" OnClick="btnAddAvail_Click" runat="server" /></td>
                        </tr>
                    </table>
                </form>
                <form id="editAvail" visible="false" runat="server">
                    <table>
                        <tr>
                            <td><asp:Label ID="lbeAID" Text="Availability ID: " runat="server" /></td>
                            <td><asp:Label ID="lbeShowAID" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeID" Text="Volunteer ID: " runat="server" /></td>
                            <td><asp:Label ID="lbeShowID" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeStart" AssociatedControlID="txeStart" Text="*Start Date: " runat="server" /></td>
                            <td><asp:TextBox ID="txeStart" MaxLength="10" runat="server" /> dd/mm/yyyy</td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeEnd" AssociatedControlID="txeEnd" Text="End Date: " runat="server" /></td>
                            <td><asp:TextBox ID="txeEnd" MaxLength="10" runat="server" /> dd/mm/yyyy</td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeReqHours" AssociatedControlID="txeReqHours" Text="Required Hours Per Week: " runat="server" /></td>
                            <td><asp:TextBox ID="txeReqHours" runat="server" /></td>
                        </tr>
                        <tr><td colspan="2" style="text-align:center;"><strong>Days Available</strong></td></tr>
                        <tr>
                            <td><asp:Label ID="lbeMon" AssociatedControlID="cheMon" Text="Monday: " runat="server" /></td>
                            <td><asp:CheckBox ID="cheMon" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeTues" AssociatedControlID="cheTues" Text="Tuesday: " runat="server" /></td>
                            <td><asp:CheckBox ID="cheTues" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeWed" AssociatedControlID="cheWed" Text="Wednesday: " runat="server" /></td>
                            <td><asp:CheckBox ID="cheWed" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeThur" AssociatedControlID="cheThur" Text="Thursday: " runat="server" /></td>
                            <td><asp:CheckBox ID="cheThur" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeFri" AssociatedControlID="cheFri" Text="Friday: " runat="server" /></td>
                            <td><asp:CheckBox ID="cheFri" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeFillIn" AssociatedControlID="cheFillIn" Text="Available To Fill In? " runat="server" /></td>
                            <td><asp:CheckBox ID="cheFillIn" runat="server" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="error">
                                <asp:RegularExpressionValidator ID="rgeStart" ControlToValidate="txeStart" Text="Please enter a valid date" ValidationExpression="^[0-3]?\d/[0-1]?\d/\d{4}$" Display="Dynamic" runat="server" />
                                <asp:RegularExpressionValidator ID="rgeEnd" ControlToValidate="txeEnd" Text="Please enter a valid date" ValidationExpression="(^[0-3]?\d/[0-1]?\d/\d{4}$|^$)" Display="Dynamic" runat="server" />
                                <asp:RegularExpressionValidator ID="rgeReqHours" ControlToValidate="txeReqHours" Text="Required hours must consist of numbers only." ValidationExpression="^[0-9]*$" Display="Dynamic" runat="server" />
                                <asp:RequiredFieldValidator ID="reeStart" ControlToValidate="txeStart" Text="You must enter a start date." Display="Dynamic" runat="server" />
                                <asp:Label ID="eavailError" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="button"><asp:Button ID="btnEditAvail" Text="Edit" OnClick="btnEditAvail_Click" runat="server" /></td>
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
