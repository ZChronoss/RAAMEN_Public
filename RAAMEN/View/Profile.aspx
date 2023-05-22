﻿<%@ Page Title="" Language="C#" MasterPageFile="~/View/NavigationBar.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="RAAMEN.View.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 style="text-align: center;">Update Profile</h1>
        <div style="display:flex; flex-direction:column; align-items: center; justify-content: center; gap:10px;">
            <div class="username" style="text-align:center;">
                <asp:TextBox ID="UsernameTextBox" runat="server" placeholder="Username"></asp:TextBox><br />
                <asp:Label ID="UsernameValid" runat="server" Visible="false" style="color:red;"></asp:Label>
            </div>
            <div class="email">
                <asp:TextBox ID="EmailTextBox" runat="server" placeholder="Email" TextMode="Email"></asp:TextBox><br />
                <asp:Label ID="EmailValid" runat="server" Visible="false" style="color:red;"></asp:Label>
            </div>
            <div class="gender" style="display:flex; gap:10px;">
                <asp:Label ID="GenderLbl" runat="server" Text="Gender"></asp:Label>
                <asp:DropDownList ID="GenderList" runat="server" >
                    <asp:ListItem Text="--Select--"></asp:ListItem>
                    <asp:ListItem Text="Male"></asp:ListItem>
                    <asp:ListItem Text="Female"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Label ID="GenderValid" runat="server" Visible="false" style="color:red;"></asp:Label>
            <div class="password">
                <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox><br />
                <asp:Label ID="PasswordValid" runat="server" Visible="false" style="color:red;"></asp:Label>
            </div>
            <div class="button">
                <asp:Button ID="UpdateProfBtn" runat="server" Text="Update" OnClick="UpdateProfBtn_Click" BackColor="LightGreen" Width="100"/>
            </div>
            <div class="errorMessage">
                <asp:Label ID="ErrorLabel" runat="server" Visible="false" BorderStyle="Solid" BackColor="Red" style="padding:20px;"></asp:Label>
            </div>
        </div>
</asp:Content>
