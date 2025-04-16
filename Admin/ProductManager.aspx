<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductManager.aspx.cs" Inherits="ComputerStore.Admin.ProductManager" %>
<!DOCTYPE html>
<html>
<head>
    <title>Quản lý sản phẩm</title>
    <link rel="stylesheet" href="../Assets/css/bootstrap.min.css" />
</head>
<body>
    <div class="container">
        <h2>Quản lý sản phẩm</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
        <h3>Thêm sản phẩm</h3>
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
        <asp:Button ID="btnAdd" runat="server" Text="Thêm sản phẩm" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
        <h3>Danh sách sản phẩm</h3>
        <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered" 
                      OnRowEditing="gvProducts_RowEditing" OnRowDeleting="gvProducts_RowDeleting">
            <Columns>
                <asp:BoundField DataField="product_id" HeaderText="ID" />
                <asp:BoundField DataField="name" HeaderText="Tên" />
                <asp:BoundField DataField="description" HeaderText="Mô tả" />
                <asp:BoundField DataField="price" HeaderText="Giá" DataFormatString="{0:N0} đ" />
                <asp:BoundField DataField="stock_quantity" HeaderText="Kho" />
                <asp:BoundField DataField="image_data" HeaderText="Hình ảnh" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </div>
    <script src="../Assets/js/bootstrap.bundle.min.js"></script>
</body>
</html>