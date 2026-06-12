using System.Data;
using System.Data.SqlClient;
using System;

namespace SaleManage.DataBase
{
    public class SystemRepo
    {
        public DataTable GetSettings()
        {
            string sql = @"SELECT 
                            company_name,
                            company_address,
                            company_phone,
                            company_bank,
                            company_AccountData,
                            tax
                           FROM company_information";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetUserSettings()
        {
            string sql = @"SELECT 
                            login_id,
                            login_password
                           FROM user_information";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
            }
            return dt;
        }

        public void UpdateSettings(
            string companyName,
            string address,
            string phone,
            string bank,
            string accountData,
            int tax)
        {
            string sql = @"UPDATE company_information
                           SET company_name        = @companyName,
                               company_address     = @address,
                               company_phone       = @phone,
                               company_bank        = @bank,
                               company_AccountData = @accountData,
                               tax                 = @tax";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@companyName", companyName);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@bank", bank);
                cmd.Parameters.AddWithValue("@accountData", accountData);
                cmd.Parameters.AddWithValue("@tax", tax);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateUserSettings(string loginId, string loginPassword)
        {
            string sql = @"UPDATE user_information
                           SET login_id       = @loginId,
                               login_password = @loginPassword";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@loginId", loginId);
                cmd.Parameters.AddWithValue("@loginPassword", loginPassword);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
       
        public string GetSetting(string key)
        {
            string sql = $"SELECT {key} FROM company_information";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "";
            }
        }
    }
}