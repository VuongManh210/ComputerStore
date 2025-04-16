using System;
using System.Data;
using ComputerStore.DAL;

namespace ComputerStore.BLL
{
    public class OrderBLL
    {
        private OrderDAL orderDAL = new OrderDAL();
        private ProductDAL productDAL = new ProductDAL();

        public int CreateOrder(int userId, int productId, int quantity, string shippingAddress)
        {
            try
            {
                // Lấy thông tin sản phẩm
                DataTable product = productDAL.GetProductById(productId);
                if (product.Rows.Count == 0)
                    throw new Exception("Sản phẩm không tồn tại.");

                decimal price = Convert.ToDecimal(product.Rows[0]["price"]);
                int stock = Convert.ToInt32(product.Rows[0]["stock_quantity"]);
                int? shopownerId = product.Rows[0]["shopowner_id"] != DBNull.Value ? Convert.ToInt32(product.Rows[0]["shopowner_id"]) : (int?)null;

                if (quantity > stock)
                    throw new Exception("Số lượng yêu cầu vượt quá tồn kho.");

                decimal subtotal = price * quantity;
                decimal totalAmount = subtotal; // Có thể thêm phí vận chuyển, thuế sau

                // Tạo đơn hàng
                int orderId = orderDAL.CreateOrder(userId, totalAmount, shippingAddress, shopownerId);

                // Thêm chi tiết đơn hàng
                orderDAL.AddOrderDetail(orderId, productId, quantity, price, subtotal);

                return orderId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tạo đơn hàng: {ex.Message}", ex);
            }
        }
    }
}