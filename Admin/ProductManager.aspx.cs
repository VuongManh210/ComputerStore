using System;
using System.Data;
using System.Web.UI.WebControls;
using ComputerStore.BLL;
using ComputerStore; // Thêm namespace

namespace ComputerStore.Admin
{
    public partial class ProductManager : System.Web.UI.Page
    {
        private ProductBLL productBLL = new ProductBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthHelper.IsAdmin() && !AuthHelper.IsShopowner())
            {
                Response.Redirect("~/User/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                BindProducts();
            }
        }

        private void BindProducts()
        {
            try
            {
                DataTable dt;
                if (AuthHelper.IsShopowner())
                {
                    int userId = AuthHelper.GetUserId();
                    dt = productBLL.GetProductsByShopowner(userId);
                }
                else
                {
                    dt = productBLL.GetAllProducts();
                }
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
                lblMessage.CssClass = "alert alert-danger";
                lblMessage.Visible = true;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                string description = txtDescription.Text.Trim();
                decimal price;
                if (!decimal.TryParse(txtPrice.Text.Trim(), out price))
                {
                    lblMessage.Text = "Giá không hợp lệ.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }
                int stock;
                if (!int.TryParse(txtStock.Text.Trim(), out stock))
                {
                    lblMessage.Text = "Số lượng tồn kho không hợp lệ.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }
                string imageData = txtImageData.Text.Trim();

                if (!string.IsNullOrEmpty(imageData) && !imageData.StartsWith("images/"))
                {
                    lblMessage.Text = "Hình ảnh phải bắt đầu bằng 'images/'.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }

                int? shopownerId = null;
                if (AuthHelper.IsShopowner())
                {
                    shopownerId = AuthHelper.GetUserId();
                }

                if (productBLL.AddProduct(name, description, price, stock, imageData, shopownerId))
                {
                    lblMessage.Text = "Thêm sản phẩm thành công!";
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;
                    BindProducts();
                    ClearForm();
                }
                else
                {
                    lblMessage.Text = "Thêm sản phẩm thất bại.";
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

        protected void gvProducts_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int productId = Convert.ToInt32(gvProducts.DataKeys[e.NewEditIndex].Value);
            Response.Redirect("~/Admin/EditProduct.aspx?productId=" + productId);
        }

        protected void gvProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int productId = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value);
                if (productBLL.DeleteProduct(productId))
                {
                    lblMessage.Text = "Xóa sản phẩm thành công!";
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;
                    BindProducts();
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

        private void ClearForm()
        {
            txtName.Text = "";
            txtDescription.Text = "";
            txtPrice.Text = "";
            txtStock.Text = "";
            txtImageData.Text = "";
        }
    }
}