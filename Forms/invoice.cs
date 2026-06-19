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
            // Default dates
            dtpDeliver.Value = DateTime.Today;
            dtpBillingDate.Value = DateTime.Today;
            dtpDeadline.Value = DateTime.Today;

            // ── Build customer picker in code ──
            BuildCustomerPicker();

            cmbIssueType.SelectedIndexChanged -= cmbIssueType_SelectedIndexChanged;
            cmbIssueType.SelectedIndexChanged += cmbIssueType_SelectedIndexChanged;
            cmbIssueType.Items.Clear();
            cmbIssueType.Items.Add("全顧客一括");
            cmbIssueType.Items.Add("顧客ごと");
            cmbIssueType.SelectedIndex = 0;

            // ── Load customer list ──
            LoadCustomerList();
        }

        // ─────────────────────────────────────────
        //  Build customer label + combobox in code
        //  Positioned below 発行区分 row
        // ─────────────────────────────────────────
        private void BuildCustomerPicker()
        {
            // Label
            lblCustomer = new Label
            {
                Text = "顧客名：",
                Location = new Point(cmbIssueType.Left - 80, cmbIssueType.Bottom + 20),
                Size = new Size(70, 23),
                TextAlign = ContentAlignment.MiddleRight,
                Font = cmbIssueType.Font,
                Visible = false
            };

            // ComboBox
            cmbCustomer = new ComboBox
            {
                Location = new Point(cmbIssueType.Left, cmbIssueType.Bottom + 20),
                Size = new Size(cmbIssueType.Width, cmbIssueType.Height),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = cmbIssueType.Font,
                DisplayMember = "customer_name",
                ValueMember = "customer_id",
                Visible = false
            };

            this.Controls.Add(lblCustomer);
            this.Controls.Add(cmbCustomer);
        }

        // ─────────────────────────────────────────
        //  Load customer list from DB
        // ─────────────────────────────────────────
        private void LoadCustomerList()
        {
            try
            {
                SalesRepo repo = new SalesRepo();
                DataTable dt = repo.GetAllCustomers();
                cmbCustomer.DataSource = dt;
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

        // ─────────────────────────────────────────
        //  発行区分 changed — show/hide customer picker
        // ─────────────────────────────────────────
        private void cmbIssueType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isPerCustomer = cmbIssueType.SelectedIndex == 1; // 顧客ごと
            cmbCustomer.Visible = isPerCustomer;
            lblCustomer.Visible = isPerCustomer;
        }

        // ─────────────────────────────────────────
        //  出力ボタン
        // ─────────────────────────────────────────
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
                    // ── 全顧客一括 ──
                    salesData = repo.GetAllSalesByMonth(dtpBillingDate.Value);
                }
                else
                {
                    // ── 顧客ごと ──
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

                // ── 対象データなし ──
                if (salesData == null || salesData.Rows.Count == 0)
                {
                    MessageBox.Show(
                        $"{dtpBillingDate.Value:yyyy年MM月}の対象データが見つかりませんでした。",
                        "確認",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                // ── A10 プレビューを開く ──
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

        // ─────────────────────────────────────────
        //  閉じるボタン
        // ─────────────────────────────────────────
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
