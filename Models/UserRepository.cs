using System;
using System.Data.SqlClient;

namespace SaleManage.Database
{
    public class UserRepository
    {
        public static bool Login(string loginId, string password)
        {
            string sql =
            @"SELECT COUNT(*)
              FROM user_information
              WHERE login_id = @login_id
              AND login_password = @login_password";

            using (SqlConnection conn =
                   new SqlConnection(connection.GetDBPass()))
            {
                conn.Open();

                using (SqlCommand cmd =
                       new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@login_id", loginId);
                    cmd.Parameters.AddWithValue("@login_password", password);

                    int count =
                        Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
            }
        }
    }
}