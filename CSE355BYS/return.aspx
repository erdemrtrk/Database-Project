<%@ Page Title="Returns" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="return.aspx.cs" Inherits="CSE355BYS.ReturnPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Sales Returns</h2>
    <asp:Label ID="lblReturnMsg" runat="server" Font-Bold="true"></asp:Label>
    <br /><br />

    <div style="background-color:#fadbd8; padding:20px; border:1px solid #f5b7b1; border-radius:5px;">
        <h3>Create Return (SP: sp_CreateSalesReturn)</h3>
        <p>Sales Invoice ID: <br /> <asp:TextBox ID="txtReturnSalesInvID" runat="server"></asp:TextBox></p>
        <p>Return Date: <br /> <asp:TextBox ID="txtReturnDate" runat="server" TextMode="Date"></asp:TextBox></p>
        <p>Reason: <br /> <asp:TextBox ID="txtReason" runat="server" Width="300px"></asp:TextBox></p>

        <asp:Button ID="btnCreateReturn" runat="server" Text="Process Return" OnClick="btnCreateReturn_Click" 
            BackColor="#e74c3c" ForeColor="White" Height="40px" Width="200px" Font-Bold="true" />
    </div>
</asp:Content>