using System;
using SaleManage.DataBase;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SaleManage.Forms
{
    public partial class product_register : Form
    {
        public string GoodsID { get; set; }

        public product_register(string goodsId = "")
        {
            InitializeComponent();
            GoodsID = goodsId;

            if (!string.IsNullOrEmpty(GoodsID))
            {
                product_repo repo = new product_repo();
                DataTable dt = repo.GetGoodsById(GoodsID);

                if (dt.Rows.Count > 0)
                {
                    txtGoodsName.Text = dt.Rows[0]["goods_name"].ToString();
                    txtGoodsPrice.Text = dt.Rows[0]["goods_price"].ToString();
                    txtStock.Text = dt.Rows[0]["stock"].ToString();
                    btnRegister.Text = "更新";
                }
            }
        }


        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtGoodsName.Text))
            {
                MessageBox.Show("商品名を入力してください。");
                txtGoodsName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtStock.Text))
            {
                MessageBox.Show("在庫数を入力してください。");
                txtStock.Focus();
                return false;
            }

            if (!int.TryParse(txtStock.Text, out int s) || s < 0)
            {
                MessageBox.Show("在庫数は0以上の数値で入力してください。");
                txtStock.Focus();
                return false;
            }
            return true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            if (!int.TryParse(txtGoodsPrice.Text.Trim(), out int price))
            {
                MessageBox.Show(
                    "価格は数値で入力してください。",
                    "入力エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            int stock = int.Parse(txtStock.Text.Trim());
            product_repo repo = new product_repo();

            if (string.IsNullOrEmpty(GoodsID))
            {
                repo.InsertGoods(
                    txtGoodsName.Text.Trim(),
                    price,stock);

                MessageBox.Show(
                    "登録が完了しました。",
                    "完了",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else // ← UPDATE
            {
                repo.UpdateGoods(
                    int.Parse(GoodsID),
                    txtGoodsName.Text.Trim(),
                    price,stock);

                MessageBox.Show(
                    "更新が完了しました。",
                    "完了",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}