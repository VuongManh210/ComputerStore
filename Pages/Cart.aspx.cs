using System;
using System.Data;
using System.Web.UI.WebControls;
using ComputerStore.BLL;
using ComputerStore; // Thêm namespace

namespace ComputerStore.Pages
{
    public partial class Cart : System.Web.UI.Page
    {
        private CartBLL cartBLL = new CartBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthHelper.RequireUserOrShopowner(Response);

            if (!IsPostBack)
            {
                BindCart();
            }
        }

        private void BindCart()
        {
            try
            {
                int userId = AuthHelper.GetUserId();
                DataTable cart = cartBLL.GetCartByUserId(userId);
                rptCart.DataSource = cart;
                rptCart.DataBind();
                btnCheckout.Visible = cart.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
                lblMessage.CssClass = "alert alert-danger";
                lblMessage.Visible = true;
            }
        }

        protected void btnRemove_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int cartId;
                if (!int.TryParse(e.CommandArgument.ToString(), out cartId))
                {
                    lblMessage.Text = "ID giỏ hàng không hợp lệ.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }
                if (cartBLL.RemoveFromCart(cartId))
                {
                    lblMessage.Text = "Đã xóa sản phẩm khỏi giỏ hàng!";
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;
                    BindCart();
                }
                else
                {
                    lblMessage.Text = "Xóa sản phẩm thất bại.";
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

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Checkout.aspx");
        }
    }
}