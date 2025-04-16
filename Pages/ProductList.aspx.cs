using System;
using ComputerStore.BLL;

namespace ComputerStore.Pages
{
    public partial class ProductList : System.Web.UI.Page
    {
        private ProductBLL productBLL = new ProductBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    rptProducts.DataSource = productBLL.GetAllProducts();
                    rptProducts.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write("Lỗi: " + ex.Message);
                }
            }
        }
    }
}