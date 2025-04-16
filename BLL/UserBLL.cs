using System;
using System.Data;
using ComputerStore.DAL;

namespace ComputerStore.BLL
{
    public class UserBLL
    {
        private UserDAL userDAL = new UserDAL();

        public DataTable GetUserByCredentials(string username, string password)
        {
            return userDAL.GetUserByCredentials(username, password);
        }

        public bool RegisterUser(string username, string password, string email, string fullName, string phone, string address)
        {
            if (userDAL.CheckUsernameExists(username))
                throw new Exception("Tên đăng nhập đã tồn tại.");
            if (userDAL.CheckEmailExists(email))
                throw new Exception("Email đã tồn tại.");
            return userDAL.RegisterUser(username, password, email, fullName, phone, address) > 0;
        }
    }
}