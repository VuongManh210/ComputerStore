using System;
using System.Data;
using System.Data.SqlClient;
using ComputerStore.BLL;
using ComputerStore.DAL;

namespace ComputerStore.Admin
{
    public partial class EditProduct : System.Web.UI.Page
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

                try
                {
                    if (int.TryParse(Request.QueryString["productId"], out int productId))
                    {
                        DataTable dt = productBLL.GetProductById(productId);
                        if (dt.Rows.Count > 0)
                        {
                            txtName.Text = dt.Rows[0]["name"].ToString();
                            txtDescription.Text = dt.Rows[0]["description"].ToString();
                            txtPrice.Text = dt.Rows[0]["price"].ToString();
                            txtStock.Text = dt.Rows[0]["stock_quantity"].ToString();
                            txtImageData.Text = dt.Rows[0]["image_data"].ToString();
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int productId = int.Parse(Request.QueryString["productId"]);
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

                string query = "UPDATE Products SET name = @name, description = @description, price = @price, " +
                              "stock_quantity = @stock, image_data = @imageData WHERE product_id = @productId";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@name", name),
                    new SqlParameter("@description", description ?? (object)DBNull.Value),
                    new SqlParameter("@price", price),
                    new SqlParameter("@stock", stock),
                    new SqlParameter("@imageData", string.IsNullOrEmpty(imageData) ? (object)DBNull.Value : imageData),
                    new SqlParameter("@productId", productId)
                };

                if (DbHelper.ExecuteNonQuery(query, parameters) > 0)
                {
                    lblMessage.Text = "Cập nhật sản phẩm thành công!";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "Cập nhật sản phẩm thất bại.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductManager.aspx");
        }
    }
}