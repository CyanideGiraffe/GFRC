<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VolunteerPosition.aspx.cs" Inherits="VolunteerPosition" Theme="Main"%>

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
                <% if (editPos.Visible == true)
                   {
                       Response.Write("<a href=\"VolunteerView.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"sub selected\">View</a><br />");
                           Response.Write("<a href=\"VolunteerPosition.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub selected\">Position</a><br />");
                               Response.Write("<a href=\"VolunteerPosition.aspx?form=edit&ID=" + Session["Selected"].ToString() + "\" class=\"subsubsub selected\">Edit</a><br />");
                           Response.Write("<a href=\"VolunteerAvail.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Availabilities</a><br />");
                           Response.Write("<a href=\"VolunteerQual.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Qualifications</a><br />");
                       Response.Write("<a href=\"VolunteerView.aspx?form=edit&ID=" + Session["Selected"].ToString() + "\" class=\"sub\">Edit</a><br />");
                   }
                   else if (viewPos.Visible == true || addPos.Visible == true)
                   {
                       Response.Write("<a href=\"VolunteerView.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"sub selected\">View</a><br />");
                           Response.Write("<a href=\"VolunteerPosition.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub selected\">Position</a><br />");
                               Response.Write("<a href=\"VolunteerPosition.aspx?form=edit&ID=" + Session["Selected"].ToString() + "\" class=\"subsubsub\">Edit</a><br />");
                           Response.Write("<a href=\"VolunteerAvail.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Availabilities</a><br />");
                           Response.Write("<a href=\"VolunteerQual.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Qualifications</a><br />");
                       Response.Write("<a href=\"VolunteerView.aspx?form=edit&ID=" + Session["Selected"].ToString() + "\" class=\"sub\">Edit</a><br />");
                   }%>
                <a href="VolunteerAdd.aspx">Add New</a>
            </div>

            <div id="right">
                <form id="viewPos" visible="true" runat="server">
                    <asp:Label ID="output" runat="server" />
                </form>
                <form id="addPos" visible="false" runat="server">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblPosition" AssociatedControlID="txtPosition" Text="*Position: " runat="server" /></td>
                            <td><asp:TextBox ID="txtPosition" MaxLength="50" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblDriverCat" AssociatedControlID="drpDriverCat" Text="Driver Category: " runat="server" /></td>
                            <td><asp:DropDownList ID="drpDriverCat" runat="server">
                                <asp:ListItem Value=" " Text="None" />
                                <asp:ListItem Value="Car" Text="C - Car" />
                                <asp:ListItem Value="Light Rigid" Text="LR - Light Rigid" />
                                <asp:ListItem Value="Medium Rigid" Text="MR - Medium Rigid" />
                                <asp:ListItem Value="Heavy Rigid" Text="HR - Heavy Rigid" />
                                <asp:ListItem Value="Heavy Combination" Text="HC - Heavy Combination" />
                                <asp:ListItem Value="Multi Combination" Text="MC - Multi Combination" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblDriverCatDoc" AssociatedControlID="fDriverCatDoc" Text="Supporting Document: " runat="server" /></td>
                            <td><asp:FileUpload ID="fDriverCatDoc" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblDriverTrans" AssociatedControlID="drpDriverTrans" Text="Driver Transmission: " runat="server" /></td>
                            <td><asp:DropDownList ID="drpDriverTrans" runat="server">
                                <asp:ListItem Value=" " Text="Not Applicable" />
                                <asp:ListItem Value="Automatic" Text="Automatic" />
                                <asp:ListItem Value="Manual" Text="Manual" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="error">
                                <asp:RequiredFieldValidator ID="reqPosition" ControlToValidate="txtPosition" Text="You must enter a position." Display="Dynamic" runat="server" />
                                <asp:Label ID="positionError" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="button"><asp:Button ID="btnAddPosition" Text="Add" OnClick="btnAddPosition_Click" runat="server" /></td>
                        </tr>
                    </table>
                </form>
                <form id="editPos" visible="false" runat="server">
                    <table>
                        <tr>
                            <td><asp:Label ID="lbePID" Text="Position ID: " runat="server" /></td>
                            <td><asp:Label ID="lbeShowPID" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeID" Text="Volunteer ID: " runat="server" /></td>
                            <td><asp:Label ID="lbeShowID" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbePosition" AssociatedControlID="txePosition" Text="*Position: " runat="server" /></td>
                            <td><asp:TextBox ID="txePosition" MaxLength="50" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeDriverCat" AssociatedControlID="dreDriverCat" Text="Driver Category: " runat="server" /></td>
                            <td><asp:DropDownList ID="dreDriverCat" runat="server">
                                <asp:ListItem Value=" " Text="None" />
                                <asp:ListItem Value="Car" Text="C - Car" />
                                <asp:ListItem Value="Light Rigid" Text="LR - Light Rigid" />
                                <asp:ListItem Value="Medium Rigid" Text="MR - Medium Rigid" />
                                <asp:ListItem Value="Heavy Rigid" Text="HR - Heavy Rigid" />
                                <asp:ListItem Value="Heavy Combination" Text="HC - Heavy Combination" />
                                <asp:ListItem Value="Multi Combination" Text="MC - Multi Combination" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeDriverCatDoc" AssociatedControlID="feDriverCatDoc" Text="Supporting Document: " runat="server" /></td>
                            <td><asp:FileUpload ID="feDriverCatDoc" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblDCDoc" Text="Current File: " runat="server" /></td>
                            <td><asp:Label ID="txtDCDoc" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lbeDriverTrans" AssociatedControlID="dreDriverTrans" Text="Driver Transmission: " runat="server" /></td>
                            <td><asp:DropDownList ID="dreDriverTrans" runat="server">
                                <asp:ListItem Value=" " Text="Not Applicable" />
                                <asp:ListItem Value="Automatic" Text="Automatic" />
                                <asp:ListItem Value="Manual" Text="Manual" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="error">
                                <asp:RequiredFieldValidator ID="reePosition" ControlToValidate="txtPosition" Text="You must enter a position." Display="Dynamic" runat="server" />
                                <asp:Label ID="epositionError" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="button"><asp:Button ID="btnEditPosition" Text="Edit" OnClick="btnEditPosition_Click" runat="server" /></td>
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
