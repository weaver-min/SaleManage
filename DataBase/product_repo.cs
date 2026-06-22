using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SaleManage.DataBase
{
    public class product_repo
    {
        public DataTable GetAllGood()
        {
            string sql = @"SELECT 
                            goods_id,
                            goods_name,
                            goods_price,
                            stock
                           FROM goods_information
                           WHERE delete_flg = 0";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetGoodsById(string goodsId)
        {
            string sql = @"SELECT 
                            goods_id,
                            goods_name,
                            goods_price,
                            stock
                           FROM goods_information
                           WHERE delete_flg = 0 
                           AND goods_id = @goodsId";

            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@goodsId", goodsId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        public void InsertGoods(string name, int price, int stock)
        {
            string sql = @"INSERT INTO goods_information
                           (goods_name, goods_price,stock)
                           VALUES(@name, @price,@stock)";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@stock", stock);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateGoods(int goodsId, string name, int price, int stock)
        {
            string sql = @"UPDATE goods_information
                           SET goods_name  = @name,
                               goods_price = @price,
                               stock = @stock
                           WHERE goods_id  = @goodsId";
            
            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@stock", stock);
                cmd.Parameters.AddWithValue("@goodsId", goodsId);
                conn.Open();
                cmd.ExecuteNonQuery();
                
            }
        }

        public void DeductStock(string goodsId, int quantity, SqlConnection conn, SqlTransaction tx)
        {
            string sql = @"UPDATE goods_information
                   SET stock = stock - @quantity
                   WHERE goods_id = @goodsId
                   AND stock >= @quantity"; 

            SqlCommand cmd = new SqlCommand(sql, conn, tx);
            cmd.Parameters.AddWithValue("@goodsId", goodsId);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            int rows = cmd.ExecuteNonQuery();

            if (rows == 0)
                throw new Exception("在庫が不足しています。");
        }
        public void DeleteGoods(int goodsId)
        {
            string sql = @"UPDATE goods_information
                   SET delete_flg = 1
                   WHERE goods_id = @goodsId";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@goodsId", goodsId);
                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }
    }
}