using System;
using SaleManage.DataBase;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace SaleManage
{
    public partial class sale_register : Form
    {
        public string SalesID { get; set; }
        private bool _isEditMode = false;

        private string _originalCustomerID;
        private string _originalGoodsID;
        private DateTime _originalDate;
        private string _originalUnitsSold;
        private string _originalRemarks;

        public sale_register(string salesId ="")
        {
            InitializeComponent();
            SalesID = salesId;
            txtID.ReadOnly = true;
            txtUnitPrice.ReadOnly = true;
            txtAmount.ReadOnly = true;

            LoadCustomerList();
            LoadGoodsList();

            if (!string.IsNullOrEmpty(SalesID))
            {
                LoadSalesData();
                SetViewMode();
            }
            else
            {   
                dtpSa_lDate.Value = DateTime.Today;
                txtID.Text = "(自動採番)";
                SetNewMode();
            }
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
            cmbGoods.SelectedIndexChanged -= cmbGoods_SelectedIndexChanged;
            product_repo repo = new product_repo();
            DataTable dt = repo.GetAllGood();
            cmbGoods.DisplayMember = "goods_name";
            cmbGoods.ValueMember = "goods_id";
            cmbGoods.DataSource = dt;
            cmbGoods.SelectedIndex = -1;

            cmbGoods.SelectedIndexChanged += cmbGoods_SelectedIndexChanged;
        }

        private void LoadSalesData()
        {
            SalesRepo repo = new SalesRepo();
            DataTable dt = repo.GetSalesById(SalesID);
            
            if (dt.Rows.Count > 0)
            {
                txtID.Text = dt.Rows[0]["sales_id"].ToString();
                dtpSa_lDate.Value = Convert.ToDateTime(dt.Rows[0]["purchase_date"]);
                cmbCustomer.SelectedValue = dt.Rows[0]["customer_id"];
                cmbGoods.SelectedValue = dt.Rows[0]["goods_id"];
                txtUnitPrice.Text = dt.Rows[0]["goods_price"].ToString();
                txtQuantity.Text = dt.Rows[0]["units_sold"].ToString();
                txtAmount.Text = dt.Rows[0]["amount"].ToString();
                txtRemarks.Text = dt.Rows[0]["remarks"].ToString();


                _originalCustomerID = dt.Rows[0]["customer_id"].ToString();
                _originalGoodsID = dt.Rows[0]["goods_id"].ToString();
                _originalDate = Convert.ToDateTime(dt.Rows[0]["purchase_date"]);
                _originalUnitsSold = dt.Rows[0]["units_sold"].ToString();
                _originalRemarks = dt.Rows[0]["remarks"].ToString();
            }
        }
        private void SetViewMode()
        {
            _isEditMode = false;
            btnRegister.Text = "編集";
            btnClose.Text = "閉じる";

            dtpSa_lDate.Enabled = false;
            cmbCustomer.Enabled = false;
            cmbGoods.Enabled = false;
            txtQuantity.ReadOnly = true;
            txtRemarks.ReadOnly = true;
        }

       
        private void SetEditMode()
        {
            _isEditMode = true;
            btnRegister.Text = "完了";
            btnClose.Text = "キャンセル";

            dtpSa_lDate.Enabled = true;
            cmbCustomer.Enabled = true;
            cmbGoods.Enabled = true;
            txtQuantity.ReadOnly = false;
            txtRemarks.ReadOnly = false;
        }

        
        private void SetNewMode()
        {
            _isEditMode = true;
            btnRegister.Text = "登録";
            btnClose.Text = "閉じる";

            dtpSa_lDate.Enabled = true;
            cmbCustomer.Enabled = true;
            cmbGoods.Enabled = true;
            txtQuantity.ReadOnly = false;
            txtRemarks.ReadOnly = false;
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

            if (!ValidateInput())
                return;

            SalesRepo repo = new SalesRepo();

            try
            {
                if (string.IsNullOrEmpty(SalesID)) 
                {
                    repo.InsertSales(
                        dtpSa_lDate.Value,
                        cmbCustomer.SelectedValue.ToString(),
                        cmbGoods.SelectedValue.ToString(),
                        int.Parse(txtQuantity.Text),
                        int.Parse(txtAmount.Text),
                        txtRemarks.Text.Trim());

                    MessageBox.Show("登録が完了しました。", "完了",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else 
                {
                    repo.UpdateSales(
                        SalesID,
                        dtpSa_lDate.Value,
                        cmbCustomer.SelectedValue.ToString(),
                        cmbGoods.SelectedValue.ToString(),
                        int.Parse(txtQuantity.Text),
                        int.Parse(txtAmount.Text),
                        txtRemarks.Text.Trim());

                    MessageBox.Show("更新が完了しました。", "完了",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sale_register_Load(object sender, EventArgs e)
        {

            LoadCustomerList();
            LoadGoodsList();

            txtID.ReadOnly = true;
            txtUnitPrice.ReadOnly = true;
            txtAmount.ReadOnly = true;
            dtpSa_lDate.Format = DateTimePickerFormat.Custom;
            dtpSa_lDate.CustomFormat = "yyyy/MM/dd";

            if (!string.IsNullOrEmpty(SalesID))
            {
                
                LoadSalesData();
                SetViewMode();
            }
            else
            {

                dtpSa_lDate.Value = DateTime.Today;
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
                int stock = Convert.ToInt32(dt.Rows[0]["stock"]);
                lblStock.Text = $"在庫: {stock} 個";              
                lblStock.ForeColor = stock == 0 ? Color.Red : Color.Black;
                CalculateAmount();
            }

        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateAmount();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_isEditMode && !string.IsNullOrEmpty(SalesID))
            {
               
                cmbCustomer.SelectedValue = _originalCustomerID;
                cmbGoods.SelectedValue = _originalGoodsID;
                dtpSa_lDate.Value = _originalDate;
                txtQuantity.Text = _originalUnitsSold;
                txtRemarks.Text = _originalRemarks;
                CalculateAmount();
                SetViewMode();
            }
            else
            {
                this.Close();
            }
        }
    }
}
