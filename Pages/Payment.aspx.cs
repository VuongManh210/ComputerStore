using System;
using System.Data.SqlClient;
using ComputerStore.DAL;

namespace ComputerStore.Pages
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/User/Login.aspx");
                    return;
                }

                if (int.TryParse(Request.QueryString["orderId"], out int orderId))
                {
                    lblOrderId.Text = orderId.ToString();
                }
                else
                {
                    lblMessage.Text = "ID đơn hàng không hợp lệ.";
                }
            }
        }

        protected void btnCompletePayment_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = int.Parse(Session["UserId"].ToString());
                int orderId = int.Parse(Request.QueryString["orderId"]);
                string paymentMethod = ddlPaymentMethod.SelectedValue;

                // Lấy total_amount từ Orders
                string query = "SELECT total_amount FROM Orders WHERE order_id = @orderId";
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@orderId", orderId)
                };
                object totalAmount = DbHelper.ExecuteScalar(query, parameters);

                if (totalAmount == null)
                {
                    lblMessage.Text = "Đơn hàng không tồn tại.";
                    return;
                }

                // Thêm bản ghi vào Payment
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
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "Thanh toán thất bại.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
            }
        }
    }
}