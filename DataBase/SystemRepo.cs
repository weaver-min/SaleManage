using System.Data;
using System.Data.SqlClient;

namespace SaleManage.DataBase
{
    public class SystemRepo
    {
        public string GetSetting(string key)
        {
            string sql = @"SELECT setting_value 
                           FROM system_settings 
                           WHERE setting_key = @key";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@key", key);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "";
            }
        }

        public void UpdateSetting(string key, string value)
        {
            string sql = @"UPDATE system_settings 
                           SET setting_value = @value 
                           WHERE setting_key = @key";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@key", key);
                cmd.Parameters.AddWithValue("@value", value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}