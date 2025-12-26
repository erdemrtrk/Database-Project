<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="CSE355BYS._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:center;">
        <h2>System Dashboard</h2>
        <p style="color:#555;">Select a module to proceed.</p>
        
        <div style="margin-top:30px; display:flex; justify-content:center; gap:20px; flex-wrap:wrap;">
            <asp:Button ID="btnProducts" runat="server" Text="Manage Products" PostBackUrl="~/product.aspx" 
                BackColor="#3498db" ForeColor="White" Height="60px" Width="220px" Font-Bold="True" />
            
            <asp:Button ID="btnSales" runat="server" Text="New Sales Entry" PostBackUrl="~/sales.aspx" 
                BackColor="#2ecc71" ForeColor="White" Height="60px" Width="220px" Font-Bold="True" />

            <asp:Button ID="btnPayments" runat="server" Text="Customer Payments" PostBackUrl="~/payment.aspx" 
                BackColor="#f1c40f" ForeColor="Black" Height="60px" Width="220px" Font-Bold="True" />
            
            <asp:Button ID="btnReturns" runat="server" Text="Sales Returns" PostBackUrl="~/return.aspx" 
                BackColor="#e74c3c" ForeColor="White" Height="60px" Width="220px" Font-Bold="True" />
        </div>

        <br /><hr /><br />

        <div style="text-align:left;">
            <h3>Live Stock Overview (Source: vw_StockOnHand)</h3>
            <p style="color:gray; font-size:small;">* Real-time data from SQL View.</p>
            <asp:GridView ID="gvStockView" runat="server" Width="100%" CellPadding="8" AutoGenerateColumns="true" 
                BackColor="#ecf0f1" BorderColor="#bdc3c7" BorderStyle="Solid" BorderWidth="1px">
                <HeaderStyle BackColor="#34495e" ForeColor="White" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>