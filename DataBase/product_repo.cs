using System;
using System.Data;
using System.Data.SqlClient;

namespace SaleManage.DataBase
{
    public class product_repo
    {
        public DataTable GetAllGood()
        {
            string sql = @"SELECT 
                            goods_id,
                            goods_name,
                            goods_price
                           FROM goods_information";

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
                            goods_price
                           FROM goods_information
                           WHERE goods_id = @goodsId";

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

        public void InsertGoods(string name, int price)
        {
            string sql = @"INSERT INTO goods_information
                           (goods_name, goods_price)
                           VALUES(@name, @price)";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@price", price);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateGoods(int goodsId, string name, int price)
        {
            string sql = @"UPDATE goods_information
                           SET goods_name  = @name,
                               goods_price = @price
                           WHERE goods_id  = @goodsId";

            using (SqlConnection conn = new SqlConnection(Database.connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@goodsId", goodsId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteGoods(int goodsId)
        {
            string sql = @"DELETE FROM goods_information 
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