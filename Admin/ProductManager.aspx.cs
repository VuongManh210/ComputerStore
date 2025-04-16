using System;
using System.Data;
using ComputerStore.BLL;

namespace ComputerStore.Admin
{
    public partial class ProductManager : System.Web.UI.Page
    {
        private ProductBLL productBLL = new ProductBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Role"] == null || Session["Role"].ToString() != "admin")
                {
                    Response.Redirect("~/User/Login.aspx");
                    return;
                }
                BindProducts();
            }
        }

        private void BindProducts()
        {
            try
            {
                gvProducts.DataSource = productBLL.GetAllProducts();
                gvProducts.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                string description = txtDescription.Text.Trim();
                decimal price = decimal.Parse(txtPrice.Text.Trim());
                int stock = int.Parse(txtStock.Text.Trim());
                string imageData = txtImageData.Text.Trim();

                if (!imageData.StartsWith("images/") && !string.IsNullOrEmpty(imageData))
                {
                    lblMessage.Text = "Hình ảnh phải bắt đầu bằng 'images/'.";
                    return;
                }

                if (productBLL.AddProduct(name, description, price, stock, imageData))
                {
                    lblMessage.Text = "Thêm sản phẩm thành công!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    BindProducts();
                    ClearForm();
                }
                else
                {
                    lblMessage.Text = "Thêm sản phẩm thất bại.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
            }
        }

        protected void gvProducts_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            // Chuyển hướng hoặc hiển thị form chỉnh sửa
            Response.Redirect($"EditProduct.aspx?productId={gvProducts.DataKeys[e.NewEditIndex].Value}");
        }

        protected void gvProducts_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
        {
            try
            {
                int productId = Convert.ToInt32(gvProducts.DataKeys[e.RowIndex].Value);
                if (productBLL.DeleteProduct(productId))
                {
                    lblMessage.Text = "Xóa sản phẩm thành công!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    BindProducts();
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