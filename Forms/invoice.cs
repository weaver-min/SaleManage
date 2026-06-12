using SaleManage.DataBase;
using System;
using System.Data;
using System.Windows.Forms;

namespace SaleManage
{
    public partial class invoice : Form
    {
     
            public invoice()
            {
                InitializeComponent();
            }

            private void invoice_Load(object sender, EventArgs e)
            {
                dtpDeliver.Value = DateTime.Today;
                dtpBillingDate.Value = DateTime.Today;
                dtpDeadline.Value = DateTime.Today;

                cmbGenerate.Items.Add("全顧客一括");
                cmbGenerate.Items.Add("顧客ごと");
                cmbGenerate.SelectedIndex = 0;
            }

        private void btnRecipe_Click(object sender, EventArgs e)
        {
            if (cmbGenerate.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "発行区分を選択してください。",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (cmbGenerate.SelectedItem.ToString() == "顧客ごと")
            {
                // ← open customer select
                customermain frm = new customermain();
                frm.IsInvoiceMode = true;
                if (frm.ShowDialog() != DialogResult.OK)
                    return;

                if (string.IsNullOrEmpty(frm.SelectedCustomerID))
                {
                    MessageBox.Show(
                        "顧客を選択してください。",
                        "エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
            

                // check data exists
                SalesRepo repo = new SalesRepo();
                DataTable dt = repo.GetSalesByCustomerAndMonth(
                    frm.SelectedCustomerID,
                    dtpBillingDate.Value);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "対象のデータがありません。",
                        "エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                invoice1 inv = new invoice1(
                    frm.SelectedCustomerID,
                    dtpDeliver.Value,
                    dtpBillingDate.Value,
                    dtpDeadline.Value);
                inv.ShowDialog();
            }
            else // ← 全顧客一括
            {
                CustomerRepo customerRepo = new CustomerRepo();
                DataTable customers = customerRepo.GetAllCustomers();

                if (customers.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "対象のデータがありません。",
                        "エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }
                bool found = false;
                foreach (DataRow row in customers.Rows)
                {
                    string customerId = row["customer_id"].ToString();

                    SalesRepo repo = new SalesRepo();
                    DataTable dt = repo.GetSalesByCustomerAndMonth(
                        customerId,
                        dtpBillingDate.Value);

                    if (dt.Rows.Count == 0)
                        continue; // ← skip customers with no sales
                    found = true;
                    invoice1 inv = new invoice1(
                        customerId,
                        dtpDeliver.Value,
                        dtpBillingDate.Value,
                        dtpDeadline.Value);
                    inv.ShowDialog();
                }
                if (!found)
                {
                    MessageBox.Show(
                        "対象のデータがありません。",
                        "エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}  
