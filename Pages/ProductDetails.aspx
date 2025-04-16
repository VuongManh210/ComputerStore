<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="ComputerStore.Pages.ProductDetails" %>
<!DOCTYPE html>
<html>
<head>
    <title>Chi tiết sản phẩm</title>
    <link rel="stylesheet" href="../Assets/css/bootstrap.min.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Chi tiết sản phẩm</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
            <asp:FormView ID="fvProduct" runat="server">
                <ItemTemplate>
                    <div class="row">
                        <div class="col-md-6">
                            <img src='<%# Eval("image_data") != DBNull.Value ? ResolveUrl("~/" + Eval("image_data")) : "~/Assets/images/placeholder.jpg" %>' 
                                 class="img-fluid" 
                                 alt="Sản phẩm" 
                                 style="max-height: 400px; object-fit: cover;" />
                            <div class="mt-3">
                                <h5>Hình ảnh khác</h5>
                                <asp:Repeater ID="rptProductImages" runat="server">
                                    <ItemTemplate>
                                        <img src='<%# ResolveUrl("~/" + Eval("image_data")) %>' 
                                             class="img-thumbnail" 
                                             alt="Hình ảnh sản phẩm" 
                                             style="width: 100px; height: 100px; object-fit: cover; margin-right: 10px;" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <h3><%# Eval("name") %></h3>
                            <p><%# Eval("description") %></p>
                            <p><strong>Giá: </strong><%# Eval("price", "{0:N0} đ") %></p>
                            <p><strong>Kho: </strong><%# Eval("stock_quantity") %></p>
                            <div class="form-group">
                                <label>Số lượng</label>
                                <asp:TextBox ID="txtQuantity" runat="server" Text="1" CssClass="form-control" TextMode="Number" min="1"></asp:TextBox>
                            </div>
                            <asp:Button ID="btnAddToCart" runat="server" Text="Thêm vào giỏ hàng" CssClass="btn btn-primary" OnClick="btnAddToCart_Click" />
                            <asp:Button ID="btnBuyNow" runat="server" Text="Mua ngay" CssClass="btn btn-success" OnClick="btnBuyNow_Click" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:FormView>
        </div>
    </form>
    <script src="../Assets/js/bootstrap.bundle.min.js"></script>
</body>
</html>