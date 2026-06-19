using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SaleManage.DataBase
{
    public class SalesRepo
    {
  
        public DataTable GetAllSales()
        {
            string sql = @"
                SELECT
                    s.sales_id,
                    s.purchase_date,
                    c.customer_name,
                    g.goods_name,
                    g.goods_price,
                    s.units_sold,
                    s.amount,
                    s.remarks
                FROM sales_information s
                INNER JOIN customer_information c ON s.customer_id = c.customer_id
                INNER JOIN goods_information    g ON s.goods_id    = g.goods_id
                WHERE s.delete_flg = 0";

            return Fill(sql);
        }

        public DataTable SearchSales(DateTime? date, string customerName)
        {
            string sql = @"
                SELECT
                    s.sales_id,
                    s.purchase_date,
                    c.customer_name,
                    g.goods_name,
                    g.goods_price,
                    s.units_sold,
                    s.amount,
                    s.remarks
                FROM sales_information s
                INNER JOIN customer_information c ON s.customer_id = c.customer_id
                INNER JOIN goods_information    g ON s.goods_id    = g.goods_id
                WHERE s.delete_flg = 0
                  AND (@date IS NULL OR CAST(s.purchase_date AS DATE) = CAST(@date AS DATE))
                  AND c.customer_name LIKE @customerName";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@date", SqlDbType.Date).Value =
                    date.HasValue ? (object)date.Value.Date : DBNull.Value;
                cmd.Parameters.AddWithValue("@customerName", "%" + customerName + "%");
                DataTable dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);
                return dt;
            }
        }

        public DataTable GetSalesById(string salesId)
        {
            string sql = @"
                SELECT
                    s.sales_id,
                    s.purchase_date,
                    s.customer_id,
                    s.goods_id,
                    g.goods_price,
                    s.units_sold,
                    s.amount,
                    s.remarks
                FROM sales_information s
                INNER JOIN goods_information g ON s.goods_id = g.goods_id
                WHERE s.sales_id = @salesId";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@salesId", salesId);
                DataTable dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);
                return dt;
            }
        }

        public void InsertSales(DateTime date, string customerId, string goodsId,
                                int unitsSold, int amount, string remarks)
        {
            string sql = @"
                INSERT INTO sales_information
                    (purchase_date, customer_id, goods_id, units_sold, amount, remarks)
                VALUES
                    (@date, @customerId, @goodsId, @unitsSold, @amount, @remarks)";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@date", date);
                            cmd.Parameters.AddWithValue("@customerId", customerId);
                            cmd.Parameters.AddWithValue("@goodsId", goodsId);
                            cmd.Parameters.AddWithValue("@unitsSold", unitsSold);
                            cmd.Parameters.AddWithValue("@amount", amount);
                            cmd.Parameters.AddWithValue("@remarks", remarks);
                            cmd.ExecuteNonQuery();
                        }

                        new product_repo().DeductStock(goodsId, unitsSold, conn, tx);
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public void UpdateSales(string salesId, DateTime date, string customerId,
                                string goodsId, int unitsSold, int amount, string remarks)
        {
            string sql = @"
                UPDATE sales_information
                SET purchase_date = @date,
                    customer_id   = @customerId,
                    goods_id      = @goodsId,
                    units_sold    = @unitsSold,
                    amount        = @amount,
                    remarks       = @remarks
                WHERE sales_id = @salesId";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        
                        int oldUnitsSold;
                        string oldGoodsId;
                        using (SqlCommand sel = new SqlCommand(
                            "SELECT goods_id, units_sold FROM sales_information WHERE sales_id = @salesId",
                            conn, tx))
                        {
                            sel.Parameters.AddWithValue("@salesId", salesId);
                            using (SqlDataReader reader = sel.ExecuteReader())
                            {
                                if (!reader.Read())
                                    throw new Exception("更新対象のレコードが見つかりませんでした。");

                                oldGoodsId = reader["goods_id"].ToString();
                                oldUnitsSold = Convert.ToInt32(reader["units_sold"]);
                            }
                        }

                        using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                        {
                            cmd.Parameters.AddWithValue("@date", date);
                            cmd.Parameters.AddWithValue("@customerId", customerId);
                            cmd.Parameters.AddWithValue("@goodsId", goodsId);
                            cmd.Parameters.AddWithValue("@unitsSold", unitsSold);
                            cmd.Parameters.AddWithValue("@amount", amount);
                            cmd.Parameters.AddWithValue("@remarks", remarks);
                            cmd.Parameters.AddWithValue("@salesId", salesId);
                            cmd.ExecuteNonQuery();
                        }

                        if (oldGoodsId != goodsId)
                        {
                            using (SqlCommand restore = new SqlCommand(
                                "UPDATE goods_information SET stock = stock + @qty WHERE goods_id = @goodsId",
                                conn, tx))
                            {
                                restore.Parameters.AddWithValue("@qty", oldUnitsSold);
                                restore.Parameters.AddWithValue("@goodsId", oldGoodsId);
                                restore.ExecuteNonQuery();
                            }

                            new product_repo().DeductStock(goodsId, unitsSold, conn, tx);
                        }
                        else
                        {
                            int diff = unitsSold - oldUnitsSold;
                            if (diff > 0)
                            {
                                new product_repo().DeductStock(goodsId, diff, conn, tx);
                            }
                            else if (diff < 0)
                            {
                                using (SqlCommand restore = new SqlCommand(
                                    "UPDATE goods_information SET stock = stock + @qty WHERE goods_id = @goodsId",
                                    conn, tx))
                                {
                                    restore.Parameters.AddWithValue("@qty", Math.Abs(diff));
                                    restore.Parameters.AddWithValue("@goodsId", goodsId);
                                    restore.ExecuteNonQuery();
                                }
                            }
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        public void DeleteSales(int saleId)
        {
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string goodsId = string.Empty;
                        int unitsSold = 0;

                        using (SqlCommand sel = new SqlCommand(
                            "SELECT goods_id, units_sold FROM sales_information WHERE sales_id = @saleId",
                            conn, tx))
                        {
                            sel.Parameters.AddWithValue("@saleId", saleId);
                            using (SqlDataReader r = sel.ExecuteReader())
                            {
                                if (r.Read())
                                {
                                    goodsId = r["goods_id"].ToString();
                                    unitsSold = Convert.ToInt32(r["units_sold"]);
                                }
                            }
                        }

                        using (SqlCommand del = new SqlCommand(
                            "UPDATE sales_information SET delete_flg = 1 WHERE sales_id = @saleId AND delete_flg = 0",
                            conn, tx))
                        {
                            del.Parameters.AddWithValue("@saleId", saleId);
                            if (del.ExecuteNonQuery() == 0)
                                throw new Exception("削除対象のレコードが見つかりませんでした。");
                        }

                        using (SqlCommand restore = new SqlCommand(
                            "UPDATE goods_information SET stock = stock + @qty WHERE goods_id = @goodsId",
                            conn, tx))
                        {
                            restore.Parameters.AddWithValue("@qty", unitsSold);
                            restore.Parameters.AddWithValue("@goodsId", goodsId);
                            restore.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        
        public DataTable GetAllSalesByMonth(DateTime billingMonth)
        {
            string sql = @"
                SELECT
                    s.sales_id,
                    s.purchase_date,
                    c.customer_id,
                    c.customer_name,
                    c.customer_address,
                    g.goods_name,
                    g.goods_price,
                    s.units_sold,
                    s.amount,
                    s.remarks
                FROM sales_information s
                INNER JOIN customer_information c ON s.customer_id = c.customer_id
                INNER JOIN goods_information    g ON s.goods_id    = g.goods_id
                WHERE s.delete_flg = 0
                  AND YEAR(s.purchase_date)  = @year
                  AND MONTH(s.purchase_date) = @month
                ORDER BY c.customer_id, s.purchase_date";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@year", billingMonth.Year);
                cmd.Parameters.AddWithValue("@month", billingMonth.Month);
                DataTable dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);
                return dt;
            }
        }

       
        public DataTable GetSalesByCustomerAndMonth(string customerId, DateTime billingMonth)
        {
            string sql = @"
                SELECT
                    s.sales_id,
                    s.purchase_date,
                    c.customer_id,
                    c.customer_name,
                    c.customer_address,
                    g.goods_name,
                    g.goods_price,
                    s.units_sold,
                    s.amount,
                    s.remarks
                FROM sales_information s
                INNER JOIN customer_information c ON s.customer_id = c.customer_id
                INNER JOIN goods_information    g ON s.goods_id    = g.goods_id
                WHERE s.delete_flg = 0
                  AND s.customer_id            = @customerId
                  AND YEAR(s.purchase_date)    = @year
                  AND MONTH(s.purchase_date)   = @month
                ORDER BY s.purchase_date";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@year", billingMonth.Year);
                cmd.Parameters.AddWithValue("@month", billingMonth.Month);
                DataTable dt = new DataTable();
                new SqlDataAdapter(cmd).Fill(dt);
                return dt;
            }
        }

        
        public DataTable GetAllCustomers()
        {
            string sql = @"
                SELECT customer_id, customer_name
                FROM   customer_information
                WHERE  delete_flg = 0
                ORDER BY customer_id";

            return Fill(sql);
        }

        
        private static DataTable Fill(string sql)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
                new SqlDataAdapter(sql, conn).Fill(dt);
            return dt;
        }
    }
}
