using System;
using SaleManage.DataBase;
using System.Data;
using System.Windows.Forms;

namespace SaleManage
{
    public partial class sale_register : Form
    {
        public string SalesID { get; set; }
        private bool _isEditMode = false;

        // ← store original values for cancel
        private string _originalCustomerID;
        private string _originalGoodsID;
        private string _originalDate;
        private string _originalQuantity;
        private string _originalNote;

        public sale_register(string salesId ="")
        {
            InitializeComponent();
            SalesID = salesId;
        }
        private void LoadCustomerList()
        {
            CustomerRepo repo = new CustomerRepo();
            DataTable dt = repo.GetAllCustomers();
            cmbCustomer.DisplayMember = "customer_name";
            cmbCustomer.ValueMember = "customer_id";
            cmbCustomer.DataSource = dt;
            cmbCustomer.SelectedIndex = -1;
        }

        private void LoadGoodsList()
        {
            product_repo repo = new product_repo();
            DataTable dt = repo.GetAllGood();
            cmbGoods.DisplayMember = "goods_name";
            cmbGoods.ValueMember = "goods_id";
            cmbGoods.DataSource = dt;
            cmbGoods.SelectedIndex = -1;
        }

        private void LoadSalesData()
        {
            SalesRepo repo = new SalesRepo();
            DataTable dt = repo.GetSalesById(SalesID);

            if (dt.Rows.Count > 0)
            {
                txtID.Text = dt.Rows[0]["sales_id"].ToString();
                dtpDate.Value = Convert.ToDateTime(dt.Rows[0]["purchase_date"]);
                cmbCustomer.SelectedValue = dt.Rows[0]["customer_id"];
                cmbGoods.SelectedValue = dt.Rows[0]["goods_id"];
                txtUnitPrice.Text = dt.Rows[0]["goods_price"].ToString();
                txtQuantity.Text = dt.Rows[0]["units_sold"].ToString();
                txtAmount.Text = dt.Rows[0]["amount"].ToString();
                txtNote.Text = dt.Rows[0]["remarks"].ToString();

                // ← save original values for cancel
                _originalCustomerID = dt.Rows[0]["customer_id"].ToString();
                _originalGoodsID = dt.Rows[0]["goods_id"].ToString();
                _originalDate = dt.Rows[0]["purchase_date"].ToString();
                _originalQuantity = dt.Rows[0]["units_sold"].ToString();
                _originalNote = dt.Rows[0]["remarks"].ToString();
            }
        }
        private void SetViewMode()
        {
            _isEditMode = false;
            btnRegister.Text = "編集";
            btnClose.Text = "閉じる";

            dtpDate.Enabled = false;
            cmbCustomer.Enabled = false;
            cmbGoods.Enabled = false;
            txtQuantity.ReadOnly = true;
            txtNote.ReadOnly = true;
        }

        // ← edit mode: 登録→完了, 閉じる→キャンセル
        private void SetEditMode()
        {
            _isEditMode = true;
            btnRegister.Text = "完了";
            btnClose.Text = "キャンセル";

            dtpDate.Enabled = true;
            cmbCustomer.Enabled = true;
            cmbGoods.Enabled = true;
            txtQuantity.ReadOnly = false;
            txtNote.ReadOnly = false;
        }

        // ← new registration mode
        private void SetNewMode()
        {
            _isEditMode = true;
            btnRegister.Text = "登録";
            btnClose.Text = "閉じる";

            dtpDate.Enabled = true;
            cmbCustomer.Enabled = true;
            cmbGoods.Enabled = true;
            txtQuantity.ReadOnly = false;
            txtNote.ReadOnly = false;
        }

        private void CalculateAmount()
        {
            if (int.TryParse(txtUnitPrice.Text, out int unitPrice) &&
                int.TryParse(txtQuantity.Text, out int quantity))
            {
                txtAmount.Text = (unitPrice * quantity).ToString();
            }
            else
            {
                txtAmount.Text = "";
            }
        }

        private bool ValidateInput()
        {
            if (cmbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("顧客名を選択してください。");
                cmbCustomer.Focus();
                return false;
            }

            if (cmbGoods.SelectedIndex == -1)
            {
                MessageBox.Show("商品名を選択してください。");
                cmbGoods.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtQuantity.Text))
            {
                MessageBox.Show("個数を入力してください。");
                txtQuantity.Focus();
                return false;
            }

            if (!int.TryParse(txtQuantity.Text, out _))
            {
                MessageBox.Show("個数は数値で入力してください。");
                txtQuantity.Focus();
                return false;
            }

            return true;
        }


        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!_isEditMode && !string.IsNullOrEmpty(SalesID))
            {
                SetEditMode();
                return;
            }


            SalesRepo repo = new SalesRepo();

            if (string.IsNullOrEmpty(SalesID)) // ← INSERT
            {
                repo.InsertSales(
                    dtpDate.Value,
                    cmbCustomer.SelectedValue.ToString(),
                    cmbGoods.SelectedValue.ToString(),
                    int.Parse(txtQuantity.Text),
                    int.Parse(txtAmount.Text),
                    txtNote.Text.Trim());

                MessageBox.Show(
                    "登録が完了しました。",
                    "完了",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else // ← UPDATE
            {
                repo.UpdateSales(
                    SalesID,
                    dtpDate.Value,
                    cmbCustomer.SelectedValue.ToString(),
                    cmbGoods.SelectedValue.ToString(),
                    int.Parse(txtQuantity.Text),
                    int.Parse(txtAmount.Text),
                    txtNote.Text.Trim());

                MessageBox.Show(
                    "更新が完了しました。",
                    "完了",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void sale_register_Load(object sender, EventArgs e)
        {

            LoadCustomerList();
            LoadGoodsList();

            txtID.ReadOnly = true;
            txtUnitPrice.ReadOnly = true;
            txtAmount.ReadOnly = true;

            if (!string.IsNullOrEmpty(SalesID))
            {
                // ← view mode from sales list
                LoadSalesData();
                SetViewMode();
            }
            else
            {
                // ← new registration mode
                txtID.Text = "自動採番";
                dtpDate.Value = DateTime.Today;
                SetNewMode();
            }

        }

        private void cmbGoods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGoods.SelectedValue == null)
                return;

            product_repo repo = new product_repo();
            DataTable dt = repo.GetGoodsById(cmbGoods.SelectedValue.ToString());

            if (dt.Rows.Count > 0)
            {
                txtUnitPrice.Text = dt.Rows[0]["goods_price"].ToString();
                CalculateAmount();
            }

        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        private void txtNote_TextChanged(object sender, EventArgs e)
        {
             
        }
    }
}
