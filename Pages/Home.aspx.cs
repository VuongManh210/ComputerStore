using System;
using ComputerStore.BLL;

namespace ComputerStore.Pages
{
    public partial class Home : System.Web.UI.Page
    {
        private ProductBLL productBLL = new ProductBLL();
        private CategoryBLL categoryBLL = new CategoryBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    rptCategories.DataSource = categoryBLL.GetAllCategories();
                    rptCategories.DataBind();

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