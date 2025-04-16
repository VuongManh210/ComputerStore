using System;
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

                try
                {
                    int userId = int.Parse(Session["UserId"].ToString());
                    rptCart.DataSource = cartBLL.GetCartByUserId(userId);
                    rptCart.DataBind();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Lỗi: " + ex.Message;
                }
            }
        }
    }
}