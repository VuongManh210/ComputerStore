using System;
using System.Data;
using ComputerStore.BLL;

namespace ComputerStore.Pages
{
    public partial class Home : System.Web.UI.Page
    {
        private ProductBLL productBLL = new ProductBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    DataTable dt = productBLL.GetAllProducts();
                    rptProducts.DataSource = dt;
                    rptProducts.DataBind();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Lỗi: " + ex.Message;
                    lblMessage.Visible = true;
                }
            }
        }
    }
}