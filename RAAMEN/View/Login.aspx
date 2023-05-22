<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RAAMEN.View.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1 style="text-align: center;">Login</h1>
        <div style="display:flex; flex-direction:column; align-items: center; justify-content: center; gap:10px;">
            <div class="username">
                <asp:TextBox ID="UsernameTextBox" runat="server" placeholder="Username"></asp:TextBox><br/>
                <asp:Label ID="UsernameEmpty" runat="server" Visible="false" style="color:red;"></asp:Label>
            </div>
            <div class="password">
                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox><br/>
                <asp:Label ID="PasswordEmpty" runat="server" Visible="false" style="color:red;"></asp:Label>
            </div>
            <div class="remember-me">
                <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me?"/>
            </div>
            <div class="button">
                <asp:Button ID="LoginBtn" runat="server" Text="Login" OnClick="LoginBtn_Click" BackColor="LightGreen" Width="100"/>
            </div>
            <div class="errorMsg">
                <asp:Label ID="NoUser" runat="server" Visible="false" BorderStyle="Solid" BackColor="Red" style="padding:20px;"></asp:Label>
            </div>
        </div>
    </form>
    
</body>
</html>
