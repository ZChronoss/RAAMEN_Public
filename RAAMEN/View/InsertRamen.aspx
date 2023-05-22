<%@ Page Title="" Language="C#" MasterPageFile="~/View/NavigationBar.Master" AutoEventWireup="true" CodeBehind="InsertRamen.aspx.cs" Inherits="RAAMEN.View.InsertRamen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Insert Ramen</h1>
        <div style="display:flex; flex-direction:column; gap:10px; margin-top:20px;">

            <div class="name">
                <asp:TextBox ID="NameTextBox" runat="server" placeholder="Name"></asp:TextBox><br/>
                <asp:Label ID="NameEmpty" runat="server" Visible="false" style="color:red;"></asp:Label>
            </div>

            <div class="meat" style="display:flex; gap:10px;">
                <asp:Label runat="server" Text="Meat"></asp:Label>
                <asp:DropDownList ID="MeatDropDown" runat="server"></asp:DropDownList>               
            </div>
            <asp:Label ID="MeatEmpty" runat="server" Visible="false" style="color:red;"></asp:Label>

            <div class="broth">
                <asp:TextBox ID="BrothTextBox" runat="server" placeholder="Broth"></asp:TextBox><br />
                <asp:Label ID="BrothEmpty" runat="server" Visible="false" style="color:red;"></asp:Label>
            </div>

            <div class="price">
                <asp:TextBox ID="PriceTextBox" runat="server" placeholder="Price"></asp:TextBox><br />
                <asp:Label ID="PriceEmpty" runat="server" Visible="false" style="color:red;"></asp:Label>
            </div>

            <div class="insertButton">
                <asp:Button ID="InsertBtn" runat="server" Text="Insert" OnClick="InsertBtn_Click" BackColor="LightGreen" Width="100"/>
            </div>

            <div class="backButton">
                <asp:Button ID="BackBtn" runat="server" Text="Back" OnClick="BackBtn_Click" Width="100"/>
            </div>
        </div>
</asp:Content>
