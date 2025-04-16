using System.Data;
using ComputerStore.DAL;

namespace ComputerStore.BLL
{
    public class CategoryBLL
    {
        private CategoryDAL categoryDAL = new CategoryDAL();

        public DataTable GetAllCategories()
        {
            return categoryDAL.GetAllCategories();
        }
    }
}