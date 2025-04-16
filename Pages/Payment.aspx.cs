using System;
using System.Data.SqlClient;
using ComputerStore.DAL;
using ComputerStore; // Thêm namespace

namespace ComputerStore.Pages
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthHelper.RequireUserOrShopowner(Response);

            if (!IsPostBack)
            {
                try
                {
                    int orderId;
                    if (int.TryParse(Request.QueryString["orderId"], out orderId))
                    {
                        lblOrderId.Text = orderId.ToString();
                    }
                    else
                    {
                        lblMessage.Text = "ID đơn hàng không hợp lệ.";
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

        protected void btnCompletePayment_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = AuthHelper.GetUserId();
                int orderId;
                if (!int.TryParse(Request.QueryString["orderId"], out orderId))
                {
                    lblMessage.Text = "ID đơn hàng không hợp lệ.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }
                string paymentMethod = ddlPaymentMethod.SelectedValue;

                string query = "SELECT total_amount FROM Orders WHERE order_id = @orderId";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@orderId", orderId)
                };
                object totalAmount = DbHelper.ExecuteScalar(query, parameters);

                if (totalAmount == null)
                {
                    lblMessage.Text = "Đơn hàng không tồn tại.";
                    lblMessage.CssClass = "alert alert-danger";
                    lblMessage.Visible = true;
                    return;
                }

                query = "INSERT INTO Payment (order_id, user_id, amount, payment_method, payment_status, transaction_id) " +
                        "VALUES (@orderId, @userId, @amount, @paymentMethod, 'completed', @transactionId)";
                parameters = new SqlParameter[]
                {
                    new SqlParameter("@orderId", orderId),
                    new SqlParameter("@userId", userId),
                    new SqlParameter("@amount", totalAmount),
                    new SqlParameter("@paymentMethod", paymentMethod),
                    new SqlParameter("@transactionId", "TXN" + DateTime.Now.Ticks.ToString())
                };

                if (DbHelper.ExecuteNonQuery(query, parameters) > 0)
                {
                    lblMessage.Text = "Thanh toán thành công!";
                    lblMessage.CssClass = "alert alert-success";
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = "Thanh toán thất bại.";
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
}