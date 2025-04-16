using System;
using System.Data;
using System.Web.UI.WebControls;
using ComputerStore.BLL;
using ComputerStore; // Thêm namespace

namespace ComputerStore.Pages
{
    public partial class Checkout : System.Web.UI.Page
    {
        private ProductBLL productBLL = new ProductBLL();
        private OrderBLL orderBLL = new OrderBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthHelper.RequireUserOrShopowner(Response);

            if (!IsPostBack)
            {
                try
                {
                    int productId;
                    int quantity;
                    if (int.TryParse(Request.QueryString["productId"], out productId) &&
                        int.TryParse(Request.QueryString["quantity"], out quantity))
                    {
                        DataTable product = productBLL.GetProductById(productId);
                        if (product.Rows.Count > 0)
                        {
                            int stock = Convert.ToInt32(product.Rows[0]["stock_quantity"]);
                            if (quantity > stock)
                            {
                                lblMessage.Text = "Số lượng yêu cầu vượt quá tồn kho (" + stock + " sản phẩm).";
                                lblMessage.CssClass = "alert alert-danger";
                                lblMessage.Visible = true;
                                return;
                            }

                            DataTable orderItems = new DataTable();
                            orderItems.Columns.Add("product_id", typeof(int));
                            orderItems.Columns.Add("name", typeof(string));
                            orderItems.Columns.Add("price", typeof(decimal));
                            orderItems.Columns.Add("quantity", typeof(int));
                            orderItems.Columns.Add("image_data", typeof(string));

                            DataRow row = orderItems.NewRow();
                            row["product_id"] = productId;
                            row["name"] = product.Rows[0]["name"];
                            row["price"] = product.Rows[0]["price"];
                            row["quantity"] = quantity;
                            row["image_data"] = product.Rows[0]["image_data"];
                            orderItems.Rows.Add(row);

                            rptOrderItems.DataSource = orderItems;
                            rptOrderItems.DataBind();
                        }
                        else
                        {
                            lblMessage.Text = "Sản phẩm không tồn tại.";
                            lblMessage.CssClass = "alert alert-danger";
                            lblMessage.Visible = true;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Thông tin đơn hàng không hợp lệ.";
                        lblMessage.CssClass = "alert alert-danger";
                        lblMessage.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Lỗi: " + ex.Message;
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                }
            }
        }

        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = AuthHelper.GetUserId();
                int productId;
                int quantity;
                if (!int.TryParse(Request.QueryString["productId"], out productId) ||
                    !int.TryParse(Request.QueryString["quantity"], out quantity))
                {
                    lblMessage.Text = "Thông tin đơn hàng không hợp lệ.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }
                string shippingAddress = txtShippingAddress.Text.Trim();

                if (string.IsNullOrEmpty(shippingAddress))
                {
                    lblMessage.Text = "Vui lòng nhập địa chỉ giao hàng.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }

                DataTable product = productBLL.GetProductById(productId);
                if (product.Rows.Count == 0)
                {
                    lblMessage.Text = "Sản phẩm không tồn tại.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }

                int stock = Convert.ToInt32(product.Rows[0]["stock_quantity"]);
                if (quantity > stock)
                {
                    lblMessage.Text = "Số lượng yêu cầu vượt quá tồn kho (" + stock + " sản phẩm).";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }

                int orderId = orderBLL.CreateOrder(userId, productId, quantity, shippingAddress);
                lblMessage.Text = "Đơn hàng #" + orderId + " đã được tạo thành công!";
                lblMessage.CssClass = "alert alert-success";
                lblMessage.Visible = true;

                Response.Redirect("~/Pages/Payment.aspx?orderId=" + orderId);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
                lblMessage.CssClass = "alert alert-danger";
                lblMessage.Visible = true;
            }
        }
    }
}