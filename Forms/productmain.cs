using SaleManage.DataBase;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SaleManage
{
    public partial class productmain : Form
    {
        private DataTable _allData = new DataTable();
        private int _currentPage = 1;
        private int _pageSize = 15;

        private int TotalPages =>
            (int)Math.Ceiling((double)_allData.Rows.Count / _pageSize);

        public productmain()
        {
            InitializeComponent();
        }

        private void LoadGoodsData()
        {
            product_repo repo = new product_repo();
            _allData = repo.GetAllGood();  // ← store all data
            _currentPage = 1;
            ShowPage();
        }

        private void ShowPage()
        {
            dgvProduct.Rows.Clear();

            int start = (_currentPage - 1) * _pageSize;
            int end = Math.Min(start + _pageSize, _allData.Rows.Count);

            for (int i = start; i < end; i++)
            {
                DataRow row = _allData.Rows[i];
                dgvProduct.Rows.Add(
                    row["goods_id"],
                    row["goods_name"],
                    row["goods_price"],
                    row["stock"]
                );
            }

            lblPage.Text = $"ページ {_currentPage} / {TotalPages}";
            btnPrev.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < TotalPages;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                ShowPage();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentPage < TotalPages)
            {
                _currentPage++;
                ShowPage();
            }
        }

        private void productmain_Load(object sender, EventArgs e)
        {
            dgvProduct.EnableHeadersVisualStyles = false;
            dgvProduct.Font = new Font("Yu Gothic UI", 10);
            dgvProduct.ColumnHeadersDefaultCellStyle.Font =
                new Font("Yu Gothic UI", 14, FontStyle.Bold);
            dgvProduct.ColumnHeadersHeight = 35;
            dgvProduct.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProduct.MultiSelect = false;
            dgvProduct.AllowUserToAddRows = false;
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
                    LoadGoodsData();
                }
            }
        }
    }
}