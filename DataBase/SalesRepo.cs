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
               WHERE s.delete_flg = 0";

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
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand(sql, conn, tx);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@goodsId", goodsId);
                        cmd.Parameters.AddWithValue("@unitsSold", unitsSold);
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@remarks", remarks);
                        cmd.ExecuteNonQuery();
                        product_repo productRepo = new product_repo();
                        productRepo.DeductStock(goodsId, unitsSold, conn, tx);
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
                conn.Open();  // ← only here, once
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string selectSql = "SELECT units_sold FROM sales_information WHERE sales_id = @salesId";
                        SqlCommand selectCmd = new SqlCommand(selectSql, conn, tx);
                        selectCmd.Parameters.AddWithValue("@salesId", salesId);
                        int oldUnitsSold = Convert.ToInt32(selectCmd.ExecuteScalar());

                        SqlCommand cmd = new SqlCommand(sql, conn, tx);
                        cmd.Parameters.AddWithValue("@date", date);
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@goodsId", goodsId);
                        cmd.Parameters.AddWithValue("@unitsSold", unitsSold);
                        cmd.Parameters.AddWithValue("@amount", amount);
                        cmd.Parameters.AddWithValue("@remarks", remarks);
                        cmd.Parameters.AddWithValue("@salesId", salesId);
                        // ← removed conn.Open() here
                        cmd.ExecuteNonQuery();

                        int diff = unitsSold - oldUnitsSold;
                        product_repo productRepo = new product_repo();
                        if (diff > 0)
                            productRepo.DeductStock(goodsId, diff, conn, tx);
                        else if (diff < 0)
                        {
                            string restoreSql = "UPDATE goods_information SET stock = stock + @qty WHERE goods_id = @goodsId";
                            SqlCommand restoreCmd = new SqlCommand(restoreSql, conn, tx);
                            restoreCmd.Parameters.AddWithValue("@qty", Math.Abs(diff));
                            restoreCmd.Parameters.AddWithValue("@goodsId", goodsId);
                            restoreCmd.ExecuteNonQuery();
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
                    conn.Open(); // ← only once here
                    using (SqlTransaction tx = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Get goods_id and units_sold before deleting
                            string selectSql = "SELECT goods_id, units_sold FROM sales_information WHERE sales_id = @saleId";
                            SqlCommand selectCmd = new SqlCommand(selectSql, conn, tx);
                            selectCmd.Parameters.AddWithValue("@saleId", saleId);

                            string goodsId = "";
                            int unitsSold = 0;
                            using (SqlDataReader reader = selectCmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    goodsId = reader["goods_id"].ToString();
                                    unitsSold = Convert.ToInt32(reader["units_sold"]);
                                }
                            }

                            // 2. Delete the sale
                            string deleteSql = "DELETE FROM sales_information WHERE sales_id = @saleId";
                            SqlCommand deleteCmd = new SqlCommand(deleteSql, conn, tx);
                            deleteCmd.Parameters.AddWithValue("@saleId", saleId);
                            int rows = deleteCmd.ExecuteNonQuery();

                            if (rows == 0)
                                throw new Exception("No row deleted. ID not found.");

                            // 3. Restore stock
                            string restoreSql = "UPDATE goods_information SET stock = stock + @qty WHERE goods_id = @goodsId";
                            SqlCommand restoreCmd = new SqlCommand(restoreSql, conn, tx);
                            restoreCmd.Parameters.AddWithValue("@qty", unitsSold);
                            restoreCmd.Parameters.AddWithValue("@goodsId", goodsId);
                            restoreCmd.ExecuteNonQuery();

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
       public DataTable GetSalesByCustomerAndMonth(string customerId, DateTime billingMonth)
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
                   WHERE s.customer_id = @customerId
                   AND YEAR(s.purchase_date)  = @year
                   AND MONTH(s.purchase_date) = @month";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@customerId", customerId);
                cmd.Parameters.AddWithValue("@year", billingMonth.Year);
                cmd.Parameters.AddWithValue("@month", billingMonth.Month);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }
    }
}