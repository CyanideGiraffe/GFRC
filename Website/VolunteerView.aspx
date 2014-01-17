<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VolunteerView.aspx.cs" Inherits="VolunteerView" Theme="Main"%>

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
                <% if (editVol.Visible == true)
                   {
                       Response.Write("<a href=\"VolunteerView.aspx?form=view&ID=" + Session["Selected"].ToString() +"\" class=\"sub\">View</a><br />");
                       Response.Write("<a href=\"VolunteerView.aspx?form=edit&ID=" + Session["Selected"].ToString() + "\" class=\"sub selected\">Edit</a><br />");
                   }
                   else if (viewVol.Visible == true)
                   {
                       Response.Write("<a href=\"VolunteerView.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"sub selected\">View</a><br />");
                           Response.Write("<a href=\"VolunteerPosition.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Position</a><br />");
                           Response.Write("<a href=\"VolunteerAvail.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Availabilities</a><br />");
                           Response.Write("<a href=\"VolunteerQual.aspx?form=view&ID=" + Session["Selected"].ToString() + "\" class=\"subsub\">Qualifications</a><br />");
                       Response.Write("<a href=\"VolunteerView.aspx?form=edit&ID=" + Session["Selected"].ToString() + "\" class=\"sub\">Edit</a><br />");
                   }%>
                <a href="VolunteerAdd.aspx">Add New</a>
            </div>

            <div id="right">
                <form id="viewVol" visible="true" runat="server">
                    <asp:Label ID="output" runat="server" />
                </form>
                <form id="editVol" visible="true" runat="server">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblID" Text="ID: " runat="server" /></td>
                            <td><asp:Label ID="lblShowID" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblName" AssociatedControlID="txtName" Text="*Name: " runat="server" /></td>
                            <td><asp:TextBox ID="txtName" MaxLength="50" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblAddress" AssociatedControlID="txtAddress" Text="*Address: " runat="server" /></td>
                            <td><asp:TextBox ID="txtAddress" MaxLength="100" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblPostal" AssociatedControlID="txtPostal" Text="Postal Address: " runat="server" /></td>
                            <td><asp:TextBox ID="txtPostal" MaxLength="100" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblEmail" AssociatedControlID="txtEmail" Text="*Email: " runat="server" /></td>
                            <td><asp:TextBox ID="txtEmail" MaxLength="50" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblHomePh" AssociatedControlID="txtHomePh" Text="Home No.: " runat="server" /></td>
                            <td><asp:TextBox ID="txtHomePh" MaxLength="10" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblMobilePh" AssociatedControlID="txtMobilePh" Text="*Mobile No.: " runat="server" /></td>
                            <td><asp:TextBox ID="txtMobilePh" MaxLength="10" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblDOB" AssociatedControlID="txtDOB" Text="*D.O.B.: " runat="server" /></td>
                            <td><asp:TextBox ID="txtDOB" MaxLength="10" runat="server" /> dd/mm/yyyy</td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblStatus" AssociatedControlID="drpStatus" Text="Status: " runat="server" /></td>
                            <td><asp:DropDownList ID="drpStatus" runat="server">
                                <asp:ListItem Value="Active" Text="Active" />
                                <asp:ListItem Value="Inactive" Text="Inactive" />
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblReferred" AssociatedControlID="txtReferred" Text="Referred By: " runat="server" /></td>
                            <td><asp:TextBox ID="txtReferred" MaxLength="20" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblReferredDoc" AssociatedControlID="fReferredDoc" Text="Supporting Document: " runat="server" /></td>
                            <td><asp:FileUpload ID="fReferredDoc" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblRefDoc" Text="Current File: " runat="server" /></td>
                            <td><asp:Label ID="txtRefDoc" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblPolice" AssociatedControlID="chkPolice" Text="Police Check? " runat="server" /></td>
                            <td><asp:CheckBox ID="chkPolice" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblPoliceDoc" AssociatedControlID="fPoliceDoc" Text="Supporting Document: " runat="server" /></td>
                            <td><asp:FileUpload ID="fPoliceDoc" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblPolDoc" Text="Current File: " runat="server" /></td>
                            <td><asp:Label ID="txtPolDoc" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblInduction" AssociatedControlID="chkInduction" Text="Had Induction? " runat="server" /></td>
                            <td><asp:CheckBox ID="chkInduction" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblApplication" AssociatedControlID="fApplication" Text="Application: " runat="server" /></td>
                            <td><asp:FileUpload ID="fApplication" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblApp" Text="Current File: " runat="server" /></td>
                            <td><asp:Label ID="txtApp" runat="server" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="error">
                                <asp:RequiredFieldValidator ID="reqName" ControlToValidate="txtName" Text="You must enter a name." Display="Dynamic" runat="server" />
                                <asp:RequiredFieldValidator ID="reqAddress" ControlToValidate="txtAddress" Text="You must enter an address." Display="Dynamic" runat="server" />
                                <asp:RequiredFieldValidator ID="reqEmail" ControlToValidate="txtEmail" Text="You must enter an email." Display="Dynamic" runat="server" />
                                <asp:RegularExpressionValidator ID="rgxEmail" ControlToValidate="txtEmail" Text="Please enter a valid email address" ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$" Display="Dynamic" runat="server" />
                                <asp:RequiredFieldValidator ID="reqMobile" ControlToValidate="txtMobilePh" Text="You must enter a mobile number." Display="Dynamic" runat="server" />
                                <asp:RequiredFieldValidator ID="reqDOB" ControlToValidate="txtDOB" Text="You must enter a date of birth." Display="Dynamic" runat="server" />
                                <asp:RegularExpressionValidator ID="rgxDOB" ControlToValidate="txtDOB" Text="Please enter a valid date of birth" ValidationExpression="^[0-3]?\d/[0-1]?\d/\d{4}$" Display="Dynamic" runat="server" />
                                <asp:RegularExpressionValidator ID="rgxHome" ControlToValidate="txtHomePh" Text="The home number must consist of numbers only." ValidationExpression="(^\d{10}$|^$)" Display="Dynamic" runat="server" />
                                <asp:RegularExpressionValidator ID="rgxMobile" ControlToValidate="txtMobilePh" Text="The mobile number must consist of numbers only." ValidationExpression="^\d{10}$" Display="Dynamic" runat="server" />
                                <asp:Label ID="volunteerError" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="button"><asp:Button ID="btnEditVolunteer" Text="Edit" OnClick="btnEditVolunteer_Click" runat="server" /></td>
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