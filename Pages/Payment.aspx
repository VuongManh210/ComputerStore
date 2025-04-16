<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ComputerStore.Pages.Payment" %>
<!DOCTYPE html>
<html>
<head>
    <title>Thanh toán</title>
    <link rel="stylesheet" href="../Assets/css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Thanh toán đơn hàng</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <p>Đơn hàng #<asp:Label ID="lblOrderId" runat="server"></asp:Label> đã được tạo.</p>
            <p>Vui lòng chọn phương thức thanh toán và hoàn tất.</p>
            <div class="form-group">
                <label>Phương thức thanh toán</label>
                <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control">
                    <asp:ListItem Value="cod">Thanh toán khi nhận hàng</asp:ListItem>
                    <asp:ListItem Value="credit_card">Thẻ tín dụng</asp:ListItem>
                    <asp:ListItem Value="bank_transfer">Chuyển khoản ngân hàng</asp:ListItem>
                    <asp:ListItem Value="paypal">Paypal</asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Button ID="btnCompletePayment" runat="server" Text="Hoàn tất thanh toán" CssClass="btn btn-primary" OnClick="btnCompletePayment_Click" />
        </div>
    </form>
    <script src="../Assets/js/bootstrap.bundle.min.js"></script>
</body>
</html>