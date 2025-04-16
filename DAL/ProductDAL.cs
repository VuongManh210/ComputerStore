using System;
using System.Data;
using System.Data.SqlClient;

namespace ComputerStore.DAL
{
    public class ProductDAL
    {
        public DataTable GetAllProducts()
        {
            string query = "SELECT product_id, name, description, price, stock_quantity, image_data FROM Products WHERE status = 'available'";
            return DbHelper.ExecuteQuery(query);
        }

        public DataTable GetProductById(int productId)
        {
            string query = "SELECT product_id, name, description, price, stock_quantity, image_data FROM Products WHERE product_id = @productId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@productId", productId)
            };
            return DbHelper.ExecuteQuery(query, parameters);
        }

        public DataTable GetProductImages(int productId)
        {
            string query = "SELECT image_data FROM ProductImages WHERE product_id = @productId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@productId", productId)
            };
            return DbHelper.ExecuteQuery(query, parameters);
        }

        public int AddProduct(string name, string description, decimal price, int stockQuantity, string imageData)
        {
            string query = "INSERT INTO Products (name, description, price, stock_quantity, image_data, status) " +
                          "VALUES (@name, @description, @price, @stockQuantity, @imageData, 'available')";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@name", name),
                new SqlParameter("@description", description ?? (object)DBNull.Value),
                new SqlParameter("@price", price),
                new SqlParameter("@stockQuantity", stockQuantity),
                new SqlParameter("@imageData", string.IsNullOrEmpty(imageData) ? (object)DBNull.Value : imageData)
            };
            return DbHelper.ExecuteNonQuery(query, parameters);
        }

        public int DeleteProduct(int productId)
        {
            string query = "DELETE FROM Products WHERE product_id = @productId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@productId", productId)
            };
            return DbHelper.ExecuteNonQuery(query, parameters);
        }
    }
}