<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ComputerStore.User.Login" %>
<!DOCTYPE html>
<html>
<head>
    <title>Đăng nhập</title>
    <link rel="stylesheet" href="../Assets/css/bootstrap.min.css" />
</head>
<body>
    <div class="container">
        <h2>Đăng nhập</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <div class="form-group">
            <label>Tên đăng nhập</label>
            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Mật khẩu</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
        <p><a href="Register.aspx">Chưa có tài khoản? Đăng ký</a></p>
    </div>
    <script src="../Assets/js/bootstrap.bundle.min.js"></script>
</body>
</html>