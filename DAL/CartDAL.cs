using System.Data;
using System.Data.SqlClient;

namespace ComputerStore.DAL
{
    public class CartDAL
    {
        public DataTable GetCartByUserId(int userId)
        {
            string query = @"SELECT c.cart_id, c.user_id, c.product_id, c.quantity, 
                            p.name AS product_name, p.price, p.image_data 
                            FROM Cart c 
                            INNER JOIN Products p ON c.product_id = p.product_id 
                            WHERE c.user_id = @userId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@userId", userId)
            };
            return DbHelper.ExecuteQuery(query, parameters);
        }

        public int AddToCart(int userId, int productId, int quantity)
        {
            string query = @"IF EXISTS (SELECT 1 FROM Cart WHERE user_id = @userId AND product_id = @productId)
                            UPDATE Cart SET quantity = quantity + @quantity WHERE user_id = @userId AND product_id = @productId
                            ELSE
                            INSERT INTO Cart (user_id, product_id, quantity) VALUES (@userId, @productId, @quantity)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@userId", userId),
                new SqlParameter("@productId", productId),
                new SqlParameter("@quantity", quantity)
            };
            return DbHelper.ExecuteNonQuery(query, parameters);
        }

        public bool RemoveFromCart(int cartId)
        {
            string query = "DELETE FROM Cart WHERE cart_id = @cartId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@cartId", cartId)
            };
            return DbHelper.ExecuteNonQuery(query, parameters) > 0;
        }
    }
}