using System.Data;
using ComputerStore.DAL;

namespace ComputerStore.BLL
{
    public class CartBLL
    {
        private CartDAL cartDAL = new CartDAL();

        public DataTable GetCartByUserId(int userId)
        {
            return cartDAL.GetCartByUserId(userId);
        }

        public bool AddToCart(int userId, int productId, int quantity)
        {
            return cartDAL.AddToCart(userId, productId, quantity) > 0;
        }
    }
}