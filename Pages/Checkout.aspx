<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="ComputerStore.Pages.Checkout" %>
<!DOCTYPE html>
<html>
<head>
    <title>Thanh toán</title>
    <link rel="stylesheet" href="../Assets/css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Thanh toán</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <div class="form-group">
                <label>Địa chỉ giao hàng</label>
                <asp:TextBox ID="txtShippingAddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Phương thức thanh toán</label>
                <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-control">
                    <asp:ListItem Value="cod">Thanh toán khi nhận hàng</asp:ListItem>
                    <asp:ListItem Value="credit_card">Thẻ tín dụng</asp:ListItem>
                    <asp:ListItem Value="bank_transfer">Chuyển khoản ngân hàng</asp:ListItem>
                    <asp:ListItem Value="paypal">Paypal</asp:ListItem>
                </asp:DropDownList>
            </div>
            <asp:Repeater ID="rptOrderItems" runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Hình ảnh</th>
                                <th>Sản phẩm</th>
                                <th>Giá</th>
                                <th>Số lượng</th>
                                <th>Tổng</th>
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
                        <td><%# Eval("name") %></td>
                        <td><%# Eval("price", "{0:N0} đ") %></td>
                        <td><%# Eval("quantity") %></td>
                        <td><%# ((decimal)Eval("price") * (int)Eval("quantity")).ToString("N0") %> đ</td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                        </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Button ID="btnConfirmOrder" runat="server" Text="Xác nhận đơn hàng" CssClass="btn btn-primary" OnClick="btnConfirmOrder_Click" />
        </div>
    </form>
    <script src="../Assets/js/bootstrap.bundle.min.js"></script>
</body>
</html>