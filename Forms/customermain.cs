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
            dgvCustomer.DataSource =
                repo.GetAllCustomers();
        }

        private void customermain_Load(object sender, EventArgs e)
        {
            dgvCustomer.EnableHeadersVisualStyles = false;

            dgvCustomer.Font = new Font("Yu Gothic UI", 10);

            dgvCustomer.ColumnHeadersDefaultCellStyle.Font =
                new Font("Yu Gothic UI", 14, FontStyle.Bold);

            dgvCustomer.ColumnHeadersHeight = 35;
            LoadCustomer();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
                CustomerRepo repo = new CustomerRepo();

                DataTable dt =
                    repo.SearchCustomer(txtSearch.Text.Trim());

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("顧客が見つかりません。");
                    return;
                }

                dgvCustomer.DataSource = dt;
            }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            customerdetails frm = new customerdetails();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadCustomer(); // refresh grid
            }
        }
        private void OpenCustomerDetails()
        {
            if (dgvCustomer.CurrentRow == null)
                return;
            customerdetails frm = new customerdetails();
            frm.CustomerID = dgvCustomer.CurrentRow.Cells[0].Value.ToString();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadCustomer();
            }

        }
        private void dgvCustomer_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

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
    }
    }



