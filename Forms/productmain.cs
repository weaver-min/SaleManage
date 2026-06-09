using SaleManage.DataBase;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SaleManage
{
    public partial class productmain : Form
    {
        public productmain()
        {
            InitializeComponent();
        }

        private void LoadGoodsData()
        {
            product_repo repo = new product_repo();
            DataTable dt = repo.GetAllGood();
            dgvProduct.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dgvProduct.Rows.Add(
                    row["goods_id"],
                    row["goods_name"],
                    row["goods_price"]
                );
            }
        }

        private void productmain_Load(object sender, EventArgs e)
        {
           dgvProduct.ColumnHeadersHeight = 35;
            LoadGoodsData();
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            Forms.product_register form = new Forms.product_register();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadGoodsData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProduct.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "編集する商品を選択してください。",
                    "選択エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            string goodsId = dgvProduct.SelectedRows[0].Cells[0].Value.ToString();

            Forms.product_register form = new Forms.product_register(goodsId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadGoodsData();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == dgvProduct.Columns["colDelete"].Index)
            {
                DialogResult result = MessageBox.Show(
                    "この商品を削除しますか？",
                    "削除確認",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    int goodsId = Convert.ToInt32(dgvProduct.Rows[e.RowIndex].Cells[0].Value);

                    product_repo repo = new product_repo();
                    repo.DeleteGoods(goodsId);

                    MessageBox.Show(
                        "削除が完了しました。",
                        "完了",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoadGoodsData(); // ← refresh
                }
            }
        }
    }
}