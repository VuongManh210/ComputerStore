using System.Data;
using ComputerStore.DAL;

namespace ComputerStore.BLL
{
    public class ProductBLL
    {
        private ProductDAL productDAL = new ProductDAL();

        public DataTable GetAllProducts()
        {
            return productDAL.GetAllProducts();
        }

        public DataTable GetProductById(int productId)
        {
            return productDAL.GetProductById(productId);
        }

        public DataTable GetProductImages(int productId)
        {
            return productDAL.GetProductImages(productId);
        }

        public bool AddProduct(string name, string description, decimal price, int stockQuantity, string imageData)
        {
            return productDAL.AddProduct(name, description, price, stockQuantity, imageData) > 0;
        }

        public bool DeleteProduct(int productId)
        {
            return productDAL.DeleteProduct(productId) > 0;
        }
    }
}