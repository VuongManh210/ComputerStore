using System;
using System.Data;
using System.Web.UI.WebControls;
using ComputerStore.BLL;
using ComputerStore; // Thêm namespace

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
                    int productId;
                    if (int.TryParse(Request.QueryString["productId"], out productId))
                    {
                        DataTable dt = productBLL.GetProductById(productId);
                        if (dt.Rows.Count > 0)
                        {
                            fvProduct.DataSource = dt;
                            fvProduct.DataBind();

                            Repeater rptImages = (Repeater)fvProduct.FindControl("rptProductImages");
                            if (rptImages != null)
                            {
                                rptImages.DataSource = productBLL.GetProductImages(productId);
                                rptImages.DataBind();
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Sản phẩm không tồn tại.";
                            lblMessage.Visible = true;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "ID sản phẩm không hợp lệ.";
                        lblMessage.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Lỗi: " + ex.Message;
                    lblMessage.Visible = true;
                }
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            AuthHelper.RequireUserOrShopowner(Response);

            try
            {
                int userId = AuthHelper.GetUserId();
                int productId;
                if (!int.TryParse(Request.QueryString["productId"], out productId))
                {
                    lblMessage.Text = "ID sản phẩm không hợp lệ.";
                    lblMessage.Visible = true;
                    return;
                }
                TextBox txtQuantity = (TextBox)fvProduct.FindControl("txtQuantity");
                int quantity;
                if (!int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
                {
                    lblMessage.Text = "Số lượng không hợp lệ.";
                    lblMessage.Visible = true;
                    return;
                }

                DataTable product = productBLL.GetProductById(productId);
                if (product.Rows.Count == 0)
                {
                    lblMessage.Text = "Sản phẩm không tồn tại.";
                    lblMessage.Visible = true;
                    return;
                }

                int stock = Convert.ToInt32(product.Rows[0]["stock_quantity"]);
                if (quantity > stock)
                {
                    lblMessage.Text = "Số lượng yêu cầu vượt quá tồn kho (" + stock + " sản phẩm).";
                    lblMessage.Visible = true;
                    return;
                }

                if (cartBLL.AddToCart(userId, productId, quantity))
                {
                    lblMessage.Text = "Đã thêm vào giỏ hàng!";
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = "Thêm vào giỏ hàng thất bại.";
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

        protected void btnBuyNow_Click(object sender, EventArgs e)
        {
            AuthHelper.RequireUserOrShopowner(Response);

            try
            {
                int productId;
                if (!int.TryParse(Request.QueryString["productId"], out productId))
                {
                    lblMessage.Text = "ID sản phẩm không hợp lệ.";
                    lblMessage.Visible = true;
                    return;
                }
                TextBox txtQuantity = (TextBox)fvProduct.FindControl("txtQuantity");
                int quantity;
                if (!int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
                {
                    lblMessage.Text = "Số lượng không hợp lệ.";
                    lblMessage.Visible = true;
                    return;
                }

                DataTable product = productBLL.GetProductById(productId);
                if (product.Rows.Count == 0)
                {
                    lblMessage.Text = "Sản phẩm không tồn tại.";
                    lblMessage.Visible = true;
                    return;
                }

                int stock = Convert.ToInt32(product.Rows[0]["stock_quantity"]);
                if (quantity > stock)
                {
                    lblMessage.Text = "Số lượng yêu cầu vượt quá tồn kho (" + stock + " sản phẩm).";
                    lblMessage.Visible = true;
                    return;
                }

                Response.Redirect("~/Pages/Checkout.aspx?productId=" + productId + "&quantity=" + quantity);
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