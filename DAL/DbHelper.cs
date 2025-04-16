using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ComputerStore.DAL
{
    public class DbHelper
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["ComputerStoreConnection"].ConnectionString;

        /// <summary>
        /// Thực thi truy vấn SELECT và trả về DataTable.
        /// </summary>
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Lỗi cơ sở dữ liệu: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thực thi truy vấn: {ex.Message}", ex);
            }
            return dt;
        }

        /// <summary>
        /// Thực thi truy vấn INSERT, UPDATE, DELETE và trả về số hàng bị ảnh hưởng.
        /// </summary>
        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Lỗi cơ sở dữ liệu: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thực thi truy vấn: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Thực thi truy vấn và trả về giá trị đơn (scalar).
        /// </summary>
        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Lỗi cơ sở dữ liệu: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thực thi truy vấn: {ex.Message}", ex);
            }
        }
    }
}