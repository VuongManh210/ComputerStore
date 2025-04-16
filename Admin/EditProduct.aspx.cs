using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using ComputerStore.BLL;
using ComputerStore; // Thêm namespace

namespace ComputerStore.Admin
{
    public partial class EditProduct : System.Web.UI.Page
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
                try
                {
                    int productId;
                    if (int.TryParse(Request.QueryString["productId"], out productId))
                    {
                        DataTable dt = productBLL.GetProductById(productId);
                        if (dt.Rows.Count > 0)
                        {
                            if (AuthHelper.IsShopowner())
                            {
                                int userId = AuthHelper.GetUserId();
                                if (dt.Rows[0]["shopowner_id"] != DBNull.Value && Convert.ToInt32(dt.Rows[0]["shopowner_id"]) != userId)
                                {
                                    lblMessage.Text = "Bạn không có quyền chỉnh sửa sản phẩm này.";
                                    lblMessage.CssClass = "alert alert-danger";
                                    lblMessage.Visible = true;
                                    btnUpdate.Visible = false;
                                    return;
                                }
                            }

                            txtName.Text = dt.Rows[0]["name"].ToString();
                            txtDescription.Text = dt.Rows[0]["description"].ToString();
                            txtPrice.Text = dt.Rows[0]["price"].ToString();
                            txtStock.Text = dt.Rows[0]["stock_quantity"].ToString();
                            txtImageData.Text = dt.Rows[0]["image_data"].ToString();
                        }
                        else
                        {
                            lblMessage.Text = "Sản phẩm không tồn tại.";
                            lblMessage.CssClass = "alert alert-danger";
                            lblMessage.Visible = true;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "ID sản phẩm không hợp lệ.";
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
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int productId;
                if (!int.TryParse(Request.QueryString["productId"], out productId))
                {
                    lblMessage.Text = "ID sản phẩm không hợp lệ.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }
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

                if (AuthHelper.IsShopowner())
                {
                    int userId = AuthHelper.GetUserId();
                    DataTable dt = productBLL.GetProductById(productId);
                    if (dt.Rows.Count > 0 && dt.Rows[0]["shopowner_id"] != DBNull.Value && Convert.ToInt32(dt.Rows[0]["shopowner_id"]) != userId)
                    {
                        lblMessage.Text = "Bạn không có quyền chỉnh sửa sản phẩm này.";
                        lblMessage.CssClass = "alert alert-danger";
                        lblMessage.Visible = true;
                        return;
                    }
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
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = "Cập nhật sản phẩm thất bại.";
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/ProductManager.aspx");
        }
    }
}