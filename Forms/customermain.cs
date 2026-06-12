using SaleManage.DataBase;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SaleManage
{
    public partial class customermain : Form
    {
        public customermain()
        {
            InitializeComponent();
        }

        private void LoadCustomer()
        {
            CustomerRepo repo = new CustomerRepo();
            DataTable dt = repo.GetAllCustomers();
            dgvCustomer.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dgvCustomer.Rows.Add(
                    row["customer_id"],
                    row["customer_name"],
                    row["customer_furigana"]
                );
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
            DataTable dt = repo.SearchCustomer(txtSearch.Text.Trim());

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(
                    "顧客が見つかりません。",
                    "検索結果なし",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            dgvCustomer.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                dgvCustomer.Rows.Add(
                    row["customer_id"],
                    row["customer_name"],
                    row["customer_furigana"]
                );
            }
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