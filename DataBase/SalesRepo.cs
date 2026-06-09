using System;
using System.Data;
using System.Data.SqlClient;

namespace SaleManage.DataBase
{
    public class SalesRepo
    {
        public DataTable GetSalesById(string salesId)
        {
            string sql = @"SELECT 
                            s.sales_id,
                            s.sales_date,
                            s.customer_id,
                            s.goods_id,
                            g.goods_price,
                            s.units_sold,
                            s.amount,
                            s.remarks
                           FROM sales_information s
                           INNER JOIN goods_information g 
                                   ON s.goods_id = g.goods_id
                           WHERE s.sales_id = @salesId";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@salesId", salesId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public void InsertSales(DateTime date, string customerId, string goodsId,
                                int quantity, int amount, string note)
        {
            string sql = @"INSERT INTO sales_information
                           (purchase_date, customer_id, goods_id, units_sold, amount, remarks)
                           VALUES(@date, @customerId, @goodsId, @quantity, @amount, @note)";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@goodsId", goodsId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@note", note);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateSales(string salesId, DateTime date, string customerId,
                                string goodsId, int quantity, int amount, string note)
        {
            string sql = @"UPDATE sales_information
                           SET purchase_date  = @date,
                               customer_id = @customerId,
                               goods_id    = @goodsId,
                               units_sold    = @quantity,
                               amount      = @amount,
                               remarks        = @note
                           WHERE sales_id  = @salesId";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@goodsId", goodsId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@note", note);
                cmd.Parameters.AddWithValue("@salesId", salesId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}