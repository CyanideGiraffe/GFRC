<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Theme="Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GFRC ~ Login</title>
</head>
<body>
    <div id="container">
        <div id="header">
            <div id="title">
                <h1>Geelong Food Relief Centre</h1>
                <h2>Login</h2>
            </div>
            <div id="home">

            </div>
        </div>
        <div id="navigation">
        </div>
        <div id="content">
            <div id="login">
                <form id="login_form" runat="server">
                    <table>
                        <tr>
                            <td><asp:Label ID="lblUsername" Text="Username: " runat="server" /></td>
                            <td><asp:TextBox ID="txtUsername" MaxLength="10" runat="server" /></td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblPassword" Text="Password: " runat="server" /></td>
                            <td><asp:TextBox ID="txtPassword" MaxLength="12" TextMode="Password" runat="server" /></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="error">
                                <asp:RequiredFieldValidator ID="reqUsername" ControlToValidate="txtUsername" Text="You must enter a username." Display="Dynamic" runat="server" />
                                <asp:RequiredFieldValidator ID="reqPassword" ControlToValidate="txtPassword" Text="You must enter a password." Display="Dynamic" runat="server" />
                                <asp:Label ID="loginError" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="button">
                                <asp:Button ID="btnLogin" Text="Login" OnClick="Login_Authenticate" runat="server" />
                            </td>
                        </tr>
                    </table>
                </form>
            </div>

        </div>
        <div id="footer">
            <p>
                <a href="Login.aspx">login</a>
                <a href="CreateLogin.aspx">create login</a>
            </p>
            <p>
                Please note: this system is not entirely completed and some areas may not yet be accessable.
            </p>
        </div>
    </div>
</body>
</html>