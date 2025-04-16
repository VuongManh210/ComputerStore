using System;
using System.Data;
using System.Data.SqlClient;

namespace ComputerStore.DAL
{
    public class UserDAL
    {
        public DataTable GetUserByCredentials(string username, string password)
        {
            string query = "SELECT user_id, username, role, status, password FROM Users WHERE username = @username AND status = 'active'";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@username", username)
            };
            return DbHelper.ExecuteQuery(query, parameters);
        }

        public int RegisterUser(string username, string password, string email, string fullName, string phone, string address)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            string query = "INSERT INTO Users (username, password, email, full_name, phone, address, role, status) " +
                          "VALUES (@username, @password, @email, @fullName, @phone, @address, 'user', 'active')";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@username", username),
                new SqlParameter("@password", hashedPassword),
                new SqlParameter("@email", email),
                new SqlParameter("@fullName", fullName ?? (object)DBNull.Value),
                new SqlParameter("@phone", phone ?? (object)DBNull.Value),
                new SqlParameter("@address", address ?? (object)DBNull.Value)
            };
            return DbHelper.ExecuteNonQuery(query, parameters);
        }

        public bool CheckUsernameExists(string username)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE username = @username";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@username", username)
            };
            return (int)DbHelper.ExecuteScalar(query, parameters) > 0;
        }

        public bool CheckEmailExists(string email)
        {
            string query = "SELECT COUNT(*) FROM Users WHERE email = @email";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@email", email)
            };
            return (int)DbHelper.ExecuteScalar(query, parameters) > 0;
        }
    }
}