using SaleManage.DataBase;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SaleManage
{
    public partial class sale_list : Form
    {
        public sale_list()
        {
            InitializeComponent();
        }

        private void LoadSalesData()
        {
            SalesRepo repo = new SalesRepo();
            DataTable dt = repo.GetAllSales();
            DisplayData(dt);
        }

        private void DisplayData(DataTable dt)
        {
            dgvSale.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dgvSale.Rows.Add(
                    row["sales_id"],
                    row["purchase_date"],
                    row["customer_name"],
                    row["goods_name"],
                    row["goods_price"],
                    row["units_sold"],
                    row["amount"],
                    row["remarks"]
                );
            }
        }
        private void sale_list_Load(object sender, EventArgs e)
        {
            dgvSale.EnableHeadersVisualStyles = false;
            dgvSale.Font = new Font("Yu Gothic UI", 10);
            dgvSale.ColumnHeadersDefaultCellStyle.Font =
                new Font("Yu Gothic UI", 14, FontStyle.Bold);
            dgvSale.ColumnHeadersHeight = 35;
            dgvSale.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSale.MultiSelect = false;
            dgvSale.AllowUserToAddRows = false;
            dtpSa_lDate.Format = DateTimePickerFormat.Custom;
            dtpSa_lDate.CustomFormat = "yyyy/MM/dd";

            LoadSalesData();
        }

        private void OpenSalesDetail(string salesId)
        {
            sale_register form = new sale_register(salesId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadSalesData();
            }
        }

        // ← DoubleClick to open detail
        private void dgvSale_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string salesId = dgvSale.Rows[e.RowIndex].Cells[0].Value.ToString();
            OpenSalesDetail(salesId);
        }

        // ← Enter key to open detail
        private void dgvSale_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvSale.SelectedRows.Count == 0)
                    return;

                string salesId = dgvSale.SelectedRows[0].Cells[0].Value.ToString();
                OpenSalesDetail(salesId);
                e.Handled = true; // ← prevent default enter behavior
            }
        }

        // ← search button
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) && !dtpSa_lDate.Checked)
            {
                LoadSalesData();
                return;
            }
            SalesRepo repo = new SalesRepo();
            DataTable dt = repo.SearchSales(
                dtpSa_lDate.Value,
                txtCustomerName.Text.Trim());

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(
                    "対象の販売情報が見つかりませんでした。",
                    "検索結果なし",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DisplayData(dt);
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvSale_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                if (e.RowIndex < 0)
                    return;

                if (e.ColumnIndex == dgvSale.Columns["colDelete"].Index)
                {
                    DialogResult result = MessageBox.Show(
                        "この商品を削除しますか？",
                        "削除確認",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        int saleId = Convert.ToInt32(dgvSale.Rows[e.RowIndex].Cells[0].Value);

                        SalesRepo repo = new SalesRepo();
                        repo.DeleteSales(saleId);

                        MessageBox.Show(
                            "削除が完了しました。",
                            "完了",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        LoadSalesData(); // ← refresh
                    }
                }
            }
        }
    }
}