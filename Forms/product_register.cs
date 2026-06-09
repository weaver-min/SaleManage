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

            product_repo repo = new product_repo();

            if (string.IsNullOrEmpty(GoodsID)) // ← INSERT
            {
                repo.InsertGoods(
                    txtGoodsName.Text.Trim(),
                    price);

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
                    price);

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