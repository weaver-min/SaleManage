using SaleManage.DataBase;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SaleManage
{
    public partial class customermain : Form
    {
        private DataTable _allData = new DataTable();
        private int _currentPage = 1;
        private int _pageSize = 15;

        private int TotalPages =>
            (int)Math.Ceiling((double)_allData.Rows.Count / _pageSize);

        public customermain()
        {
            InitializeComponent();
        }

        private void LoadCustomer()
        {
            CustomerRepo repo = new CustomerRepo();
            _allData = repo.GetAllCustomers();  
            _currentPage = 1;
            ShowPage();
        }

        private void ShowPage()
        {
            dgvCustomer.Rows.Clear();

            int start = (_currentPage - 1) * _pageSize;
            int end = Math.Min(start + _pageSize, _allData.Rows.Count);

            for (int i = start; i < end; i++)
            {
                DataRow row = _allData.Rows[i];
                dgvCustomer.Rows.Add(
                    row["customer_id"],
                    row["customer_name"],
                    row["customer_furigana"]
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

        private void customermain_Load(object sender, EventArgs e)
        {
            dgvCustomer.EnableHeadersVisualStyles = false;
            dgvCustomer.Font = new Font("Yu Gothic UI", 10);
            dgvCustomer.ColumnHeadersDefaultCellStyle.Font =
                new Font("Yu Gothic UI", 14, FontStyle.Bold);
            dgvCustomer.ColumnHeadersHeight = 35;
            dgvCustomer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomer.MultiSelect = false;
            dgvCustomer.AllowUserToAddRows = false;
            LoadCustomer();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CustomerRepo repo = new CustomerRepo();
            _allData = repo.SearchCustomer(txtSearch.Text.Trim());

            if (_allData.Rows.Count == 0)
            {
                MessageBox.Show(
                    "顧客が見つかりません。",
                    "検索結果なし",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            _currentPage = 1;
            ShowPage();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            customerdetails frm = new customerdetails();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadCustomer();
            }
        }

        public string SelectedCustomerID { get; set; }
        public bool IsInvoiceMode { get; set; }

        private void OpenCustomerDetails()
        {
            if (dgvCustomer.CurrentRow == null)
                return;

            if (IsInvoiceMode)
            {
                SelectedCustomerID = dgvCustomer.CurrentRow.Cells[0].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            string customerId = dgvCustomer.CurrentRow.Cells[0].Value.ToString();
            customerdetails frm = new customerdetails(customerId);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadCustomer();
            }
        }

        private void dgvCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            OpenCustomerDetails();
        }

        private void dgvCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                OpenCustomerDetails();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}