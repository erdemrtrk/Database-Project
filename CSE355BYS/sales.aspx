<%@ Page Title="Sales" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="sales.aspx.cs" Inherits="CSE355BYS.SalesPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Sales Operations</h2>
    <asp:Label ID="lblMsg" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
    <br /><br />

    <div style="background-color:#eafaf1; padding:20px; border:1px solid #d5f5e3; border-radius:5px;">
        <h3 style="color:#27ae60;">Step 1: Create Invoice Header (SP: sp_CreateSalesInvoice)</h3>
        <p>Customer: <br />
           <asp:DropDownList ID="ddlCustomer" runat="server" Height="30px" Width="300px"></asp:DropDownList>
        </p>
        <p>Date: <br />
           <asp:TextBox ID="txtDate" runat="server" TextMode="Date" Height="25px"></asp:TextBox>
        </p>
        <p>Currency: <br />
           <asp:DropDownList ID="ddlCurrency" runat="server" Height="30px" Width="200px" AppendDataBoundItems="true">
               <asp:ListItem Value="TRY">TRY</asp:ListItem>
               <asp:ListItem Value="USD">USD</asp:ListItem>
               <asp:ListItem Value="EUR">EUR</asp:ListItem>
               <asp:ListItem Value="GBP">GBP (British Pound)</asp:ListItem>
               <asp:ListItem Value="CHF">CHF (Swiss Franc)</asp:ListItem>
               <asp:ListItem Value="JPY">JPY (Japanese Yen)</asp:ListItem>
           </asp:DropDownList>
        </p>
        <asp:Button ID="btnCreateInvoice" runat="server" Text="Create Header" OnClick="btnCreateInvoice_Click" 
            BackColor="#2ecc71" ForeColor="White" Height="40px" Width="180px" Font-Bold="true" />
    </div>

    <br /><hr /><br />

    <div style="background-color:#fff8e1; padding:20px; border:1px solid #fae5d3; border-radius:5px;">
        <h3 style="color:#d35400;">Step 2: Add Items (Trigger: trg_StockUpdate)</h3>
        <p>Open Invoice: <br />
           <asp:DropDownList ID="ddlOpenInvoices" runat="server" Height="30px" Width="350px"></asp:DropDownList>
        </p>
        <p>Product: <br />
           <asp:DropDownList ID="ddlProduct" runat="server" Height="30px" Width="350px"></asp:DropDownList>
        </p>
        <p>Quantity: <br />
           <asp:TextBox ID="txtQuantity" runat="server" TextMode="Number" Text="1" Width="80px"></asp:TextBox>
        </p>
        <asp:Button ID="btnAddItem" runat="server" Text="Add Item" OnClick="btnAddItem_Click" 
            BackColor="#f39c12" ForeColor="White" Height="40px" Width="180px" Font-Bold="true" />
    </div>

    <br />
    <h3>Recent Invoices</h3>
    <asp:GridView ID="GridView1" runat="server" Width="100%" CellPadding="5" AutoGenerateColumns="true" 
        BackColor="White" BorderColor="#bdc3c7" HeaderStyle-BackColor="#7f8c8d" HeaderStyle-ForeColor="White"></asp:GridView>
</asp:Content>