using System;
using System.Data;
using ComputerStore.BLL;

namespace ComputerStore.Pages
{
    public partial class Checkout : System.Web.UI.Page
    {
        private ProductBLL productBLL = new ProductBLL();
        private OrderBLL orderBLL = new OrderBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/User/Login.aspx");
                    return;
                }

                try
                {
                    if (int.TryParse(Request.QueryString["productId"], out int productId) &&
                        int.TryParse(Request.QueryString["quantity"], out int quantity))
                    {
                        DataTable product = productBLL.GetProductById(productId);
                        if (product.Rows.Count > 0)
                        {
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
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Thông tin đơn hàng không hợp lệ.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Lỗi: " + ex.Message;
                }
            }
        }

        protected void btnConfirmOrder_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/User/Login.aspx");
                return;
            }

            try
            {
                int userId = int.Parse(Session["UserId"].ToString());
                int productId = int.Parse(Request.QueryString["productId"]);
                int quantity = int.Parse(Request.QueryString["quantity"]);
                string shippingAddress = txtShippingAddress.Text.Trim();

                if (string.IsNullOrEmpty(shippingAddress))
                {
                    lblMessage.Text = "Vui lòng nhập địa chỉ giao hàng.";
                    return;
                }

                int orderId = orderBLL.CreateOrder(userId, productId, quantity, shippingAddress);
                lblMessage.Text = $"Đơn hàng #{orderId} đã được tạo thành công!";
                lblMessage.ForeColor = System.Drawing.Color.Green;

                // Chuyển hướng đến trang thanh toán (giả lập)
                Response.Redirect("~/Pages/Payment.aspx?orderId=" + orderId);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
            }
        }
    }
}