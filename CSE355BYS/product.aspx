<%@ Page Title="Products" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="product.aspx.cs" Inherits="CSE355BYS.ProductPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Product Management</h2>
    <asp:Label ID="lblMsg" runat="server" Font-Bold="true"></asp:Label>
    <br /><br />

    <div style="border:1px solid #3498db; padding:15px; background-color:#ebf5fb; margin-bottom:20px;">
        <h3 style="color:#2980b9;">Execute SP: sp_RefreshProductTryPrice</h3>
        <p>Recalculate TRY prices based on latest exchange rates.</p>
        <asp:Button ID="btnRefreshPrices" runat="server" Text="Run SP (Refresh Prices)" OnClick="btnRefreshPrices_Click" 
            BackColor="#3498db" ForeColor="White" Height="40px" Width="250px" Font-Bold="true" />
    </div>

    <div style="border:1px solid #bdc3c7; padding:15px; background-color:#fdfefe;">
        <h3>Insert New Product</h3>
        <p>Category: <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList></p>
        <p>Name: <asp:TextBox ID="txtName" runat="server"></asp:TextBox></p>
        <p>Price: <asp:TextBox ID="txtBasePrice" runat="server"></asp:TextBox> 
           Currency: <asp:DropDownList ID="ddlCurrency" runat="server"></asp:DropDownList></p>
        <asp:Button ID="btnAdd" runat="server" Text="Add Product" OnClick="btnAdd_Click" 
            BackColor="#27ae60" ForeColor="White" Height="35px" Width="150px" />
    </div>
    
    <br />
    <h3>Product List</h3>
    <asp:GridView ID="gvProducts" runat="server" Width="100%" CellPadding="5" AutoGenerateColumns="true"></asp:GridView>
</asp:Content>