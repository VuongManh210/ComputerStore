<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="ComputerStore.Pages.Cart" %>
<!DOCTYPE html>
<html>
<head>
    <title>Giỏ hàng</title>
    <link rel="stylesheet" href="../Assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../Assets/css/custom.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Giỏ hàng</h2>
            <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-danger" Visible="false"></asp:Label>
            <asp:Repeater ID="rptCart" runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Hình ảnh</th>
                                <th>Sản phẩm</th>
                                <th>Giá</th>
                                <th>Số lượng</th>
                                <th>Tổng</th>
                                <th>Thao tác</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <img src='<%# Eval("image_data") != DBNull.Value ? ResolveUrl("~/" + Eval("image_data")) : "~/Assets/images/placeholder.jpg" %>' 
                                 style="width: 50px; height: 50px; object-fit: cover;" />
                        </td>
                        <td><%# Eval("product_name") %></td>
                        <td><%# Eval("price", "{0:N0} đ") %></td>
                        <td><%# Eval("quantity") %></td>
                        <td><%# ((decimal)Eval("price") * (int)Eval("quantity")).ToString("N0") %> đ</td>
                        <td>
                            <asp:Button ID="btnRemove" runat="server" Text="Xóa" CssClass="btn btn-danger btn-sm" 
                                        CommandArgument='<%# Eval("cart_id") %>' OnCommand="btnRemove_Command" />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Button ID="btnCheckout" runat="server" Text="Thanh toán" CssClass="btn btn-primary" OnClick="btnCheckout_Click" />
        </div>
    </form>
    <script src="../Assets/js/bootstrap.bundle.min.js"></script>
</body>
</html>