<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" 
    Inherits="CSE355BYS.HomePage" MasterPageFile="~/template.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <h2>FurnicolorDB - Home</h2>
    <p>Use the buttons below to manage the system.</p>

    <div style="margin-top:20px;">
        <asp:Button ID="btnProducts" runat="server" Text="Products" PostBackUrl="~/product.aspx" 
            BackColor="#007bff" ForeColor="White" Height="50px" Width="150px" Font-Bold="True" />
        &nbsp;
        <asp:Button ID="btnSales" runat="server" Text="Sales" PostBackUrl="~/sales.aspx" 
            BackColor="#28a745" ForeColor="White" Height="50px" Width="150px" Font-Bold="True" />
        &nbsp;
        <asp:Button ID="btnPayments" runat="server" Text="Payments" PostBackUrl="~/payment.aspx" 
            BackColor="#ffc107" ForeColor="Black" Height="50px" Width="150px" Font-Bold="True" />
        &nbsp;
        <asp:Button ID="btnReturns" runat="server" Text="Returns" PostBackUrl="~/return.aspx" 
            BackColor="#dc3545" ForeColor="White" Height="50px" Width="150px" Font-Bold="True" />
    </div>
</asp:Content>