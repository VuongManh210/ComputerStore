using System;
using System.Data;
using ComputerStore.BLL;

namespace ComputerStore.Pages
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        private ProductBLL productBLL = new ProductBLL();
        private CartBLL cartBLL = new CartBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (int.TryParse(Request.QueryString["productId"], out int productId))
                    {
                        DataTable dt = productBLL.GetProductById(productId);
                        if (dt.Rows.Count > 0)
                        {
                            fvProduct.DataSource = dt;
                            fvProduct.DataBind();

                            // Gán dữ liệu cho Repeater hình ảnh
                            System.Web.UI.WebControls.Repeater rptImages = (System.Web.UI.WebControls.Repeater)fvProduct.FindControl("rptProductImages");
                            if (rptImages != null)
                            {
                                rptImages.DataSource = productBLL.GetProductImages(productId);
                                rptImages.DataBind();
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Sản phẩm không tồn tại.";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "ID sản phẩm không hợp lệ.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Lỗi: " + ex.Message;
                }
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
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
                int quantity = int.Parse((fvProduct.FindControl("txtQuantity") as System.Web.UI.WebControls.TextBox).Text);

                if (cartBLL.AddToCart(userId, productId, quantity))
                {
                    lblMessage.Text = "Đã thêm vào giỏ hàng!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "Thêm vào giỏ hàng thất bại.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
            }
        }

        protected void btnBuyNow_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/User/Login.aspx");
                return;
            }

            try
            {
                int productId = int.Parse(Request.QueryString["productId"]);
                int quantity = int.Parse((fvProduct.FindControl("txtQuantity") as System.Web.UI.WebControls.TextBox).Text);
                Response.Redirect($"~/Pages/Checkout.aspx?productId={productId}&quantity={quantity}");
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
            }
        }
    }
}