<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ComputerStore.User.Login" %>
<!DOCTYPE html>
<html>
<head>
    <title>Đăng nhập</title>
    <link rel="stylesheet" href="../Assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../Assets/css/custom.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Đăng nhập</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger" Visible="false"></asp:Label>
            <div class="form-group">
                <label>Tên đăng nhập</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Mật khẩu</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
            <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
            <p class="mt-3">
                Chưa có tài khoản? <a href="Register.aspx">Đăng ký</a>
            </p>
        </div>
    </form>
    <script src="../Assets/js/bootstrap.bundle.min.js"></script>
</body>
</html>