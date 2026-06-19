using SaleManage.DataBase;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SaleManage
{
    public partial class invoice : Form
    {
        private ComboBox cmbCustomer;
        private Label lblCustomer;

        public invoice()
        {
            InitializeComponent();
        }

        private void invoice_Load(object sender, EventArgs e)
        {
            dtpDeliver.Value = DateTime.Today;
            dtpBillingDate.Value = DateTime.Today;
            dtpDeadline.Value = DateTime.Today;

            BuildCustomerPicker();

            cmbIssueType.SelectedIndexChanged -= cmbIssueType_SelectedIndexChanged;
            cmbIssueType.SelectedIndexChanged += cmbIssueType_SelectedIndexChanged;
            cmbIssueType.Items.Clear();
            cmbIssueType.Items.Add("全顧客一括");
            cmbIssueType.Items.Add("顧客ごと");

            LoadCustomerList();   

            cmbIssueType.SelectedIndex = 0;
        }

        private void BuildCustomerPicker()
        {
            Control parent = cmbIssueType.Parent;   

            lblCustomer = new Label
            {
                Text = "顧客名：",
                Location = new Point(cmbIssueType.Left - 80, cmbIssueType.Bottom + 20),
                Size = new Size(70, 23),
                TextAlign = ContentAlignment.MiddleRight,
                Font = cmbIssueType.Font,
                Visible = false
            };

            cmbCustomer = new ComboBox
            {
                Location = new Point(cmbIssueType.Left, cmbIssueType.Bottom + 20),
                Size = new Size(cmbIssueType.Width, cmbIssueType.Height),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = cmbIssueType.Font,
                Visible = false
            };

            parent.Controls.Add(lblCustomer);
            parent.Controls.Add(cmbCustomer);
        }

        private void LoadCustomerList()
        {
            try
            {
                SalesRepo repo = new SalesRepo();
                DataTable dt = repo.GetAllCustomers();

                cmbCustomer.DataSource = dt;
                cmbCustomer.DisplayMember = "customer_name";
                cmbCustomer.ValueMember = "customer_id";

                if (dt.Rows.Count > 0)
                    cmbCustomer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "顧客リストの取得に失敗しました。\n\n" + ex.Message,
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void cmbIssueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCustomer == null || lblCustomer == null)
                return;

            bool isPerCustomer = cmbIssueType.SelectedIndex == 1;
            cmbCustomer.Visible = isPerCustomer;
            lblCustomer.Visible = isPerCustomer;
        }

        private void btnRecipe_Click(object sender, EventArgs e)
        {
            if (cmbIssueType.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "発行区分を選択してください。",
                    "入力確認",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SalesRepo repo = new SalesRepo();
                DataTable salesData;

                if (cmbIssueType.SelectedIndex == 0)
                {
                    
                    salesData = repo.GetAllSalesByMonth(dtpBillingDate.Value);
                }
                else
                {
                    
                    if (cmbCustomer.SelectedValue == null)
                    {
                        MessageBox.Show(
                            "顧客を選択してください。",
                            "入力確認",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    string customerId = cmbCustomer.SelectedValue.ToString();
                    salesData = repo.GetSalesByCustomerAndMonth(
                                    customerId,
                                    dtpBillingDate.Value);
                }

                
                if (salesData == null || salesData.Rows.Count == 0)
                {
                    MessageBox.Show(
                        $"{dtpBillingDate.Value:yyyy年MM月}の対象データが見つかりませんでした。",
                        "確認",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                using (invoice1 preview = new invoice1(
                    salesData,
                    dtpDeliver.Value,
                    dtpBillingDate.Value,
                    dtpDeadline.Value))
                {
                    preview.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "データの取得中にエラーが発生しました。\n\n" + ex.Message,
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}