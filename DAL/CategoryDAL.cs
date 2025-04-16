using System.Data;

namespace ComputerStore.DAL
{
    public class CategoryDAL
    {
        public DataTable GetAllCategories()
        {
            string query = "SELECT category_id, name, description FROM Categories";
            return DbHelper.ExecuteQuery(query);
        }
    }
}