using System;
using System.Data;
using ComputerStore.BLL;

namespace ComputerStore.Pages
{
    public partial class Cart : System.Web.UI.Page
    {
        private CartBLL cartBLL = new CartBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/User/Login.aspx");
                    return;
                }
                BindCart();
            }
        }

        private void BindCart()
        {
            try
            {
                int userId = int.Parse(Session["UserId"].ToString());
                DataTable cart = cartBLL.GetCartByUserId(userId);
                rptCart.DataSource = cart;
                rptCart.DataBind();
                btnCheckout.Visible = cart.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
            }
        }

        protected void btnRemove_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            try
            {
                int cartId = int.Parse(e.CommandArgument.ToString());
                if (cartBLL.RemoveFromCart(cartId))
                {
                    lblMessage.Text = "Đã xóa sản phẩm khỏi giỏ hàng!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    BindCart();
                }
                else
                {
                    lblMessage.Text = "Xóa sản phẩm thất bại.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Checkout.aspx");
        }
    }
}