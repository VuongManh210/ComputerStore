<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ComputerStore.Pages.Home" %>
<!DOCTYPE html>
<html>
<head>
    <title>Trang chủ - ComputerStore</title>
    <link rel="stylesheet" href="../Assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../Assets/css/custom.css" />
</head>
<body>
    <div class="container">
        <h2>Chào mừng đến với ComputerStore</h2>
        <h3>Danh mục</h3>
        <asp:Repeater ID="rptCategories" runat="server">
            <ItemTemplate>
                <div class="card mb-3">
                    <div class="card-body">
                        <h5><%# Eval("name") %></h5>
                        <p><%# Eval("description") %></p>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <h3>Sản phẩm nổi bật</h3>
        <asp:Repeater ID="rptProducts" runat="server">
            <ItemTemplate>
                <div class="col-md-4">
                    <div class="card mb-4">
                        <img src='<%# Eval("image_data") != DBNull.Value ? ResolveUrl("~/" + Eval("image_data")) : "~/Assets/images/placeholder.jpg" %>' 
                             class="card-img-top" 
                             alt="Sản phẩm" 
                             style="height: 200px; object-fit: cover;" />
                        <div class="card-body">
                            <h5><%# Eval("name") %></h5>
                            <p><%# Eval("description") %></p>
                            <p><strong>Giá: </strong><%# Eval("price", "{0:N0} đ") %></p>
                            <a href='ProductDetails.aspx?productId=<%# Eval("product_id") %>' class="btn btn-primary">Xem chi tiết</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <script src="../Assets/js/bootstrap.bundle.min.js"></script>
</body>
</html>