<%@ Page Title="Payments" Language="C#" MasterPageFile="~/template.Master" AutoEventWireup="true" CodeBehind="payment.aspx.cs" Inherits="CSE355BYS.PaymentPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Customer Payments</h2>
    <asp:Label ID="lblPayMsg" runat="server" Font-Bold="true"></asp:Label>
    <br /><br />

    <div style="background-color:#fcf3cf; padding:20px; border:1px solid #f9e79f; border-radius:5px;">
        <h3>Record Payment (SP: sp_RecordCustomerPayment)</h3>
        <p>Customer: <br /> <asp:DropDownList ID="ddlCustomerPay" runat="server" Height="30px" Width="250px"></asp:DropDownList></p>
        <p>Invoice ID (Optional): <br /> <asp:TextBox ID="txtInvID" runat="server" Placeholder="e.g., 105"></asp:TextBox></p>
        <p>Currency: <br /> 
           <asp:DropDownList ID="ddlPayCurrency" runat="server" Height="30px" Width="100px">
               <asp:ListItem Value="TRY">TRY</asp:ListItem>
               <asp:ListItem Value="USD">USD</asp:ListItem>
               <asp:ListItem Value="EUR">EUR</asp:ListItem>
           </asp:DropDownList>
        </p>
        <p>Date: <br /> <asp:TextBox ID="txtRateDate" runat="server" TextMode="Date"></asp:TextBox></p>
        <p>Amount: <br /> <asp:TextBox ID="txtAmountForeign" runat="server" TextMode="Number" Step="0.01"></asp:TextBox></p>
        
        <asp:Button ID="btnPay" runat="server" Text="Record Payment" OnClick="btnPay_Click" 
            BackColor="#f1c40f" ForeColor="Black" Height="40px" Width="200px" Font-Bold="true" />
    </div>
</asp:Content>