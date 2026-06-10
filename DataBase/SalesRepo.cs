using System;
using System.Data;
using System.Data.SqlClient;

namespace SaleManage.DataBase
{
    public class SalesRepo
    {
        public DataTable GetAllSales()
        {
            string sql = @"SELECT 
                            s.sales_id,
                            s.purchase_date,
                            c.customer_name,
                            g.goods_name,
                            g.goods_price,
                            s.units_sold,
                            s.amount,
                            s.remarks
                           FROM sales_information s
                           INNER JOIN customer_information c
                                   ON s.customer_id = c.customer_id
                           INNER JOIN goods_information g
                                   ON s.goods_id = g.goods_id";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable SearchSales(DateTime date, string customerName)
        {
            string sql = @"SELECT 
                            s.sales_id,
                            s.purchase_date,
                            c.customer_name,
                            g.goods_name,
                            g.goods_price,
                            s.units_sold,
                            s.amount,
                            s.remarks
                           FROM sales_information s
                           INNER JOIN customer_information c
                                   ON s.customer_id = c.customer_id
                           INNER JOIN goods_information g
                                   ON s.goods_id = g.goods_id
                           WHERE CAST(s.purchase_date AS DATE) = CAST(@date AS DATE)
                           AND c.customer_name LIKE @customerName";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", date.Date);
                cmd.Parameters.AddWithValue("@customerName", "%" + customerName + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetSalesById(string salesId)
        {
            string sql = @"SELECT 
                            s.sales_id,
                            s.purchase_date,
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
                                int unitsSold, int amount, string remarks)
        {
            string sql = @"INSERT INTO sales_information
                           (purchase_date, customer_id, goods_id, units_sold, amount, remarks)
                           VALUES(@date, @customerId, @goodsId, @unitsSold, @amount, @remarks)";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@goodsId", goodsId);
                cmd.Parameters.AddWithValue("@unitsSold", unitsSold);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@remarks", remarks);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateSales(string salesId, DateTime date, string customerId,
                                string goodsId, int unitsSold, int amount, string remarks)
        {
            string sql = @"UPDATE sales_information
                           SET purchase_date  = @date,
                               customer_id = @customerId,
                               goods_id    = @goodsId,
                               units_sold  = @unitsSold,
                               amount      = @amount,
                               remarks     = @remarks
                           WHERE sales_id  = @salesId";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@goodsId", goodsId);
                cmd.Parameters.AddWithValue("@unitsSold", unitsSold);
                cmd.Parameters.AddWithValue("@amount", amount);
                cmd.Parameters.AddWithValue("@remarks", remarks);
                cmd.Parameters.AddWithValue("@salesId", salesId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}