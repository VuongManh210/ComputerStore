<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="ComputerStore.Pages.ProductList" %>
<!DOCTYPE html>
<html>
<head>
    <title>Danh sách sản phẩm</title>
    <link rel="stylesheet" href="../Assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../Assets/css/custom.css" />
</head>
<body>
    <div class="container">
        <h2>Sản phẩm</h2>
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
                            <p><strong>Kho: </strong><%# Eval("stock_quantity") %></p>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <script src="../Assets/js/bootstrap.bundle.min.js"></script>
</body>
</html>