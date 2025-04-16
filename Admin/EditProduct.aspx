<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="ComputerStore.Admin.EditProduct" %>
<!DOCTYPE html>
<html>
<head>
    <title>Chỉnh sửa sản phẩm</title>
    <link rel="stylesheet" href="../Assets/css/bootstrap.min.css" />
</head>
<body>
    <div class="container">
        <h2>Chỉnh sửa sản phẩm</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <div class="form-group">
            <label>Tên sản phẩm</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Mô tả</label>
            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Giá</label>
            <asp:TextBox ID="txtPrice" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Số lượng tồn</label>
            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
        </div>
        <div class="form-group">
            <label>Hình ảnh (đường dẫn images/...)</label>
            <asp:TextBox ID="txtImageData" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button ID="btnUpdate" runat="server" Text="Cập nhật" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Hủy" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
    </div>
    <script src="../Assets/js/bootstrap.bundle.min.js"></script>
</body>
</html>