using System;
using System.Data;
using System.Data.SqlClient;

namespace ComputerStore.DAL
{
    public class OrderDAL
    {
        public int CreateOrder(int userId, decimal totalAmount, string shippingAddress, int? shopownerId = null)
        {
            string query = "INSERT INTO Orders (user_id, shopowner_id, total_amount, shipping_address, order_status) " +
                          "OUTPUT INSERTED.order_id " +
                          "VALUES (@userId, @shopownerId, @totalAmount, @shippingAddress, 'pending')";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@shopownerId", shopownerId ?? (object)DBNull.Value),
                new SqlParameter("@totalAmount", totalAmount),
                new SqlParameter("@shippingAddress", shippingAddress)
            };
            return (int)DbHelper.ExecuteScalar(query, parameters);
        }

        public int AddOrderDetail(int orderId, int productId, int quantity, decimal price, decimal subtotal)
        {
            string query = "INSERT INTO OrderDetails (order_id, product_id, quantity, price, subtotal) " +
                          "VALUES (@orderId, @productId, @quantity, @price, @subtotal)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@orderId", orderId),
                new SqlParameter("@productId", productId),
                new SqlParameter("@quantity", quantity),
                new SqlParameter("@price", price),
                new SqlParameter("@subtotal", subtotal)
            };
            return DbHelper.ExecuteNonQuery(query, parameters);
        }
    }
}