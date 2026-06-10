using System.Data;
using System.Data.SqlClient;

namespace SaleManage.DataBase
{
    public class CustomerRepo
    {
        public DataTable GetAllCustomers()
        {
            string sql = @"SELECT 
                            customer_id,
                            customer_name,
                            customer_furigana 
                           FROM customer_information";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetCustomerById(string customerId)
        {
            string sql = @"SELECT 
                            customer_id,
                            customer_name,
                            customer_furigana,
                            customer_address
                           FROM customer_information
                           WHERE customer_id = @id";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@id", customerId);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable SearchCustomer(string customerName)
        {
            string sql = @"SELECT 
                            customer_id,
                            customer_name,
                            customer_furigana
                           FROM customer_information
                           WHERE customer_name LIKE @customer_name";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@customer_name", "%" + customerName + "%");
                da.Fill(dt);
            }
            return dt;
        }

        // ← new method
        public DataTable GetSalesByCustomerId(string customerId)
        {
            string sql = @"SELECT
                            s.sales_id,
                            s.purchase_date,
                            g.goods_name,
                            g.goods_price,
                            s.units_sold,
                            s.amount,
                            s.remarks
                           FROM sales_information s
                           INNER JOIN goods_information g
                                   ON s.goods_id = g.goods_id
                           WHERE s.customer_id = @customerId";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public void InsertCustomer(string name, string furigana, string address)
        {
            string sql = @"INSERT INTO customer_information
                           (customer_name, customer_furigana, customer_address)
                           VALUES(@name, @furigana, @address)";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@furigana", furigana);
                cmd.Parameters.AddWithValue("@address", address);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateCustomer(string id, string name, string furigana, string address)
        {
            string sql = @"UPDATE customer_information
                           SET customer_name     = @name,
                               customer_furigana = @furigana,
                               customer_address  = @address
                           WHERE customer_id = @id";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@furigana", furigana);
                cmd.Parameters.AddWithValue("@address", address);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(string id)
        {
            string sql = @"DELETE FROM customer_information 
                           WHERE customer_id = @id";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}