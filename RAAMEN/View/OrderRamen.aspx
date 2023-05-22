<%@ Page Title="" Language="C#" MasterPageFile="~/View/NavigationBar.Master" AutoEventWireup="true" CodeBehind="OrderRamen.aspx.cs" Inherits="RAAMEN.View.OrderRamen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Order Ramen</h1>

        <div>
            <asp:GridView ID="RamenGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="RamenGridView_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name"/>
                    <asp:BoundField DataField="Meat.name" HeaderText="Meat" SortExpression="Meat.name" />
                    <asp:BoundField DataField="Broth" HeaderText="Broth" SortExpression="Broth"/>
                    <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price"/>
                    <asp:ButtonField HeaderText="Order" CommandName="Order" Text="Order" />                    
                </Columns>
            </asp:GridView>            
        </div>

        <div>
            <h3>Cart</h3>

            <asp:GridView ID="CartGridView" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Name"/>
                    
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
